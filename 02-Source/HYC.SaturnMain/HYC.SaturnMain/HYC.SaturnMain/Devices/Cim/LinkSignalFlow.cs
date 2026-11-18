/*----------------------------------------------------------------
// Copyright (C) Suzhou HYC Technology Co.,LTD
// 版权所有。
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnMain.Devices.Cim
// 文件名称    ：LinkSignalFlow.cs
// =================================
// 创 建 者    ：guguangzhou
// 创建日期    ：2021/01/29 09:00:00 
// 功能描述    ：LinkSignal交互类
// 使用说明    ：
//
//
// 创建标识：
//
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using HYC.SaturnDB.DAL;
using HYC.SaturnDB.Model;
using HYC.HTFile.HTINI;
using HYC.HTLog;
using HYC.HTPCBase.Alarm;
using HYC.HTPCBase.Entities;
using HYC.HTPCBase.Extension.Recipe;
using HYC.HTPCBase.Motion;
using HYC.HTPCBase.Station;
using HYC.SaturnMain.Params;
using HYC.SaturnMain.Devices.Motion;
using HYC.SaturnMain.Entities;
using HYC.SaturnMain.Station;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HYC.SaturnMain.Alarm;
using HYC.SaturnMain;
using HYC.SaturnMain.Entities;
using CIMBOE;
using CIMBOE.Block;
using BOECIM.Devices.CIM;

namespace HYC.SaturnMain.Devices.Cim
{
    class LinkSignalFlow
    {
        HTINIHelper Ini = new HTINIHelper(AppDomain.CurrentDomain.BaseDirectory + "LinkInterfaceFlow.INI");
        #region 状态机步骤
        public int NormalInterfaceStep = -1;//正常流程step 
        public int LoadStep = -1;//正常流程step 
        public int UnloadStep = -1;//正常流程step 
        private int RecoveryInterfaceStep = -1;//异常恢复流程step 
        private int LocalToDownRecoveryExceptionStep = -1;
        private int LocalToUpRecoveryExceptionStep = -1;
        private int DownToLocalRecoveryExceptionStep = -1;
        private int UpToLocalRecoveryExceptionStep = -1;
        #endregion
        bool LocalToDownIsHandlingOverGlass = false;//本机做上游，本机异常时，Glass是否交互完成
        bool LocalToUpIsHandlingOverGlass = false;//本机做下游，本机异常时，Glass是否交互完成
        bool DownToLocalIsHandlingOverGlass = false; //本机做上游，下游异常时，Glass是否交互完成
        bool UpToLocalIsHandlingOverGlass = false; //本机做下游，上游异常时，Glass是否交互完成
        Delay Delay = new Delay();//本机信号延时用
        Delay Delay2 = new Delay();//本机信号延时用
        MotionManager MotionManager = MotionManager.GetInstance();//运动控制器管理类
        const int LinkSignalCount = 58;
        public bool[] UpToLocalSignal = new bool[LinkSignalCount];
        public bool[] DownToLocalSignal = new bool[LinkSignalCount];
        public bool[] LocalToDownSignal = new bool[LinkSignalCount];
        public bool[] LocalToUpSignal = new bool[LinkSignalCount];
        public event Action<string, string, Brush> OnShowMsg;
        public event Action<string> OnUpdatePPID;
        public bool IsInInterfaceFlow = false;//是否在上下游交互中
        GlassDataA[] JobDatas = null;
        public static LinkSignalFlow Instance = null;
        bool IsHandlingOverGlass = false;//Glass是否交互完成
        bool IsRecoveryException = false;//是否是异常恢复流程
        string LastGlassID = string.Empty;

        const string BeforeHandlingOver = "BeforeHandlingOverGlass";
        const string AfterHandlingOver = "AfterHandlingOverGlass";
        const string LocalToRobot = "LocalToRobot";
        const string RobotToLocal = "RobotToLocal";
        static LinkSignalFlow()
        {
            Instance = new LinkSignalFlow();
        }
        public void RegisterEvent()
        {
            StationManager.Instance.OnSignalTrigger += Instance_OnSignalTrigger;
        }

        private void Instance_OnSignalTrigger(IOInfo arg1, bool arg2)
        {
            if (IsInInterfaceFlow)//在交互中，如果上游报警，置位RobotTrouble
            {
                if (arg1.IOName == "RobotEMO" && arg2 == false)
                {
                    // Ini.Write("Eqp", "RobotTrouble", "true");
                    SendUpSignal(DownStreamSignal.Downstream_Inline, false);
                    SendUpSignal(DownStreamSignal.Downstream_Trouble, true);
                    AlarmManager.CreateAlarm((int)CimAlarmCode.RobotEmoOff);
                    ResetBits();
                }
                if (arg1.IOName == "RobotEMS" && arg2 == false)
                {
                    //Ini.Write("Eqp", "RobotTrouble", "true");
                    SendUpSignal(DownStreamSignal.Downstream_Inline, false);
                    SendUpSignal(DownStreamSignal.Downstream_Trouble, true);
                    AlarmManager.CreateAlarm((int)CimAlarmCode.RobotEmsOff);
                    ResetBits();
                }
            }
        }

        public void LinkSignalChanged(LinkSignalEventArgs e)
        {
            try
            {
                if (e.Index == 0)
                {
                    UpToLocalSignal = e.Signals;
                    if (e.Glass_Datas != null && e.Glass_Datas.Length > 0)
                    {
                        JobDatas = e.Glass_Datas;
                        if (JobDatas[0].Glass_ID != LastGlassID)
                        {
                            IntertDB(JobDatas);
                            LastGlassID = JobDatas[0].Glass_ID;
                        }
                    }
                    if (UpToLocalSignal[(int)UpStreamSignal.Upstream_Trouble])
                    {
                        if (IsInInterfaceFlow)//在交互中，如果上游报警，置位RobotTrouble
                        {
                            // Ini.Write("Eqp", "RobotTrouble", "true");
                            SendUpSignal(DownStreamSignal.Downstream_Inline, false);
                            SendUpSignal(DownStreamSignal.Downstream_Trouble, true);
                            AlarmManager.CreateAlarm((int)CimAlarmCode.UpStemTrouble);
                            ResetBits();
                        }
                    }
                }
                else if (e.Index == 1)
                {
                    DownToLocalSignal = e.Signals;
                    if (DownToLocalSignal[(int)DownStreamSignal.Downstream_Trouble])
                    {
                        if (IsInInterfaceFlow)//在交互中，如果下游报警，置位RobotTrouble
                        {
                            // Ini.Write("Eqp", "RobotTrouble", "true");
                            SendDownSignal(UpStreamSignal.Upstream_Inline, false);
                            SendDownSignal(UpStreamSignal.Upstream_Trouble, true);
                            AlarmManager.CreateAlarm((int)CimAlarmCode.DownStemTrouble);
                            ResetBits();
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                OnShowMsg?.Invoke("LinkSignalFlow", $"OnLinkSignalChanged方法执行异常，异常信息：{ex.Message}", Brushes.Red);
            }
        }
        private void SignalMonitor()
        {
            if (!UpToLocalSignal[(int)UpStreamSignal.Upstream_Inline])
            {
                AlarmManager.CreateAlarm((int)CimAlarmCode.UpStemNotInline);
                return;
            }
            if (!DownToLocalSignal[(int)DownStreamSignal.Downstream_Inline])
            {
                AlarmManager.CreateAlarm((int)CimAlarmCode.DownStemNotInline);
                return;
            }
        }
        /// <summary>
        /// Robot->Stage流程，Robot把Glass交互给本机，请参照附件4.BOEMY Equipment Link Signal Specification_Rev1.1.9_20190301，Type 3
        /// </summary>
        public void LoadThread()
        {
            switch (LoadStep)
            {
                case 1:
                    {
                        OnShowMsg?.Invoke("LinkSignalFlow", "Load Glass流程开始，本机Receive_Able信号Turn On，等待上游Send_Able信号Turn On", Brushes.Green);
                        SendUpSignal(DownStreamSignal.Receive_Able, true);
                        SendDownSignal(UpStreamSignal.Position_Back, false);
                        SendDownSignal(UpStreamSignal.Position_Front, false);
                        SendUpSignal(DownStreamSignal.Position_Back, false);
                        SendUpSignal(DownStreamSignal.Position_Front, false);
                        Ini.Write(RobotToLocal, BeforeHandlingOver, "true");//本机做下游时，BeforeHandlingOver置位
                        Delay.Start(Param.Detect.IdleDelayTime);
                        LoadStep = 10;
                        break;
                    }
                case 10:
                    {
                        if (UpToLocalSignal[(int)UpStreamSignal.Send_Able])
                        {
                            CimManager.Instance.MachineStatusChangeReport(HTPCBase.Views.EqpSts.RUN);
                            OnShowMsg?.Invoke("LinkSignalFlow", "上游Send_Able信号Turn On，校验JobData", Brushes.Green);
                            if (JobDataCheck())
                            {
                                OnShowMsg?.Invoke("LinkSignalFlow", "JobData校验成功", Brushes.Green);
                                if (MotionManager.IsArrive(StationEnum.DualStage, AxisName.Stage1_Z, StagePosName.UpSecondPos) && 
                                    MotionManager.IsArrive(StationEnum.DualStage, AxisName.Stage2_Z, StagePosName.UpSecondPos))
                                {
                                    if (MotionManager.GetDigitalInput(InputName.ShutterOpenL) && MotionManager.GetDigitalInput(InputName.ShutterOpenR))
                                    {
                                        OnShowMsg?.Invoke("LinkSignalFlow", "本机Pin_State,Shutter_State,Receive_Start信号TurnOn，等待上游Send_Complete信号Turn On", Brushes.Green);
                                        SafeSignal(true);  //  安全信号，pin全部降下去了
                                        IonizerControl(true); ///  ???
                                        SendUpSignal(DownStreamSignal.Receive_Start, true);
                                        Delay.Start(Param.Cim.WaitUpSendCompleteOnTime);
                                        LoadStep = 20;
                                    }
                                    else
                                    {
                                        AlarmManager.CreateAlarm((int)CimAlarmCode.ShutterNotReady);
                                    }
                                }
                                else
                                {
                                    AlarmManager.CreateAlarm((int)CimAlarmCode.StageNotReady);
                                }
                            }
                            else//如果校验失败，回到第5步，复位报警后重新发送SendAble
                            {
                                AlarmManager.CreateAlarm((int)CimAlarmCode.JobDataCheckError);
                                LoadStep = 1;
                            }
                        }
                        else if (Delay.TimeUp())
                        {
                            CimManager.Instance.MachineStatusChangeReport(HTPCBase.Views.EqpSts.IDLE);
                        }
                        break;
                    }

                case 20:
                    {
                        if (UpToLocalSignal[(int)UpStreamSignal.Send_Complete])
                        {
                            SendUpSignal(DownStreamSignal.Receive_Complete, true);   ///  接收 Glass完成
                            OnShowMsg?.Invoke("LinkSignalFlow", "上游Send_Complete信号Turn On，本机Receive_Complete信号Turn On，等待上游Send_Complete信号Turn Off", Brushes.Green);
                            Delay.Start(Param.Cim.WaitUpSendCompleteOffTime);
                            LoadStep = 30;
                        }
                        else if (Delay.TimeUp())
                        {
                            AlarmManager.CreateAlarm((int)CimAlarmCode.WaitUpStemSendComTimeout);

                        }
                        break;
                    }
                case 30:
                    {
                        if (!UpToLocalSignal[(int)UpStreamSignal.Send_Complete]) ///  上游发送完成
                        {
                            SendUpSignal(DownStreamSignal.Receive_Able, false);
                            SendUpSignal(DownStreamSignal.Receive_Complete, false);
                            SendUpSignal(DownStreamSignal.Receive_Start, false);
                            Ini.Write(RobotToLocal, AfterHandlingOver, "false");//本机做下游时，AfterHandlingOver复位
                            OnShowMsg?.Invoke("LinkSignalFlow", "上游Send_Complete信号Turn Off，本机Receive_Complete,Receive_Able,Pin_State信号Turn Off", Brushes.Green);
                            LoadStep = 40;
                        }
                        else if (Delay.TimeUp())
                        {
                            AlarmManager.CreateAlarm((int)CimAlarmCode.WaitUpStemSendComOffTimeout);

                        }
                        break;
                    }
                case 40:
                    {
                        CimManager.Instance.ReceivedJobReport(JobDatas);
                        CimManager.Instance.StoredInReport(JobDatas);
                        CimManager.Instance.ProcessStartReport(JobDatas);
                        IonizerControl(false);
                        if (JobDatas != null && JobDatas.Length > 0)
                        {
                            OnUpdatePPID?.Invoke(JobDatas[0].PPID[1].ToString());
                        }
                        MotionManager.SetDigitalOutput(OutputName.Stage1Vacuum, true);//吸真空
                        MotionManager.SetDigitalOutput(OutputName.Stage2Vacuum, true);
                        OnShowMsg?.Invoke("LinkSignalFlow", "上报ReceivedJobReport，Load Glass流程结束", Brushes.Green);
                        LoadStep = 0;
                        break;
                    }

                default:
                    break;
            }
        }
        /// <summary>
        /// Stage->Robot流程，请参照附件4.BOEMY Equipment Link Signal Specification_Rev1.1.9_20190301，Type 9
        /// </summary>
        public void UnloadThread()
        {
            switch (UnloadStep)
            {
                case 1:
                    {
                        OnShowMsg?.Invoke("LinkSignalFlow", "本机Send_Able信号Turn On，等待下游Receive_Able信号Turn On", Brushes.Green);
                        Ini.Write(LocalToRobot, BeforeHandlingOver, "true");//本机做上游时，BeforeHandlingOver置位
                        SendDownSignal(UpStreamSignal.Send_Start, false);
                        UnloadStep = 20;
                        break;
                    }
                case 20:
                    {
                        if (DownToLocalSignal[(int)DownStreamSignal.Receive_Able])
                        {
                            if (MotionManager.IsArrive(StationEnum.DualStage, AxisName.Stage1_Z, StagePosName.UpSecondPos) &&
                                MotionManager.IsArrive(StationEnum.DualStage, AxisName.Stage2_Z, StagePosName.UpSecondPos))
                            {
                                if (MotionManager.GetDigitalInput(InputName.ShutterOpenL) && MotionManager.GetDigitalInput(InputName.ShutterOpenR))
                                {
                                    OnShowMsg?.Invoke("LinkSignalFlow", "下游Receive_Able信号Turn On，本机Send_Start，Shutter_State，Pin_State信号TurnOn，等待下游Job_Transfer_Signal_1信号Turn On", Brushes.Green);
                                    SafeSignal(true);
                                    IonizerControl(true);
                                    SendDownSignal(UpStreamSignal.Send_Start, true);
                                    Delay.Start(Param.Cim.WaitDownJobTransferSignalOnTime);
                                    UnloadStep = 30;
                                }
                                else
                                {
                                    AlarmManager.CreateAlarm((int)CimAlarmCode.ShutterNotReady);
                                }
                            }
                            else
                            {
                                AlarmManager.CreateAlarm((int)CimAlarmCode.StageNotReady);
                            }
                        }

                        break;
                    }
                case 30:
                    {
                        if (DownToLocalSignal[(int)DownStreamSignal.Job_Transfer_Signal_1])
                        {
                            OnShowMsg?.Invoke("LinkSignalFlow", "本机上报SendOutJobReport，等待下游ReceiveComplete信号Turn On", Brushes.Green);
                            Ini.Write(LocalToRobot, BeforeHandlingOver, "false");//本机做上游时，BeforeHandlingOver复位
                            Ini.Write(LocalToRobot, AfterHandlingOver, "true");//本机做上游时，AfterHandlingOver置位
                            CimManager.Instance.FetchedOutReport(GetJobDatas());
                            CimManager.Instance.SentOutJobReport(GetJobDatas());
                            CimManager.Instance.ProcessEndReport(GetJobDatas());
                            Delay.Start(Param.Cim.WaitDownReceiveAbleOnTime);
                            UnloadStep = 40;
                        }
                        else if (Delay.TimeUp())
                        {
                            AlarmManager.CreateAlarm((int)CimAlarmCode.WaitDnStemJobTranSigTimeout);
                            UnloadStep = 1;
                        }
                        break;
                    }
                case 40:
                    {
                        if (DownToLocalSignal[(int)DownStreamSignal.Receive_Complete])
                        {
                            OnShowMsg?.Invoke("LinkSignalFlow", "下游ReceiveComplete信号Turn On，本机Send_Complete信号Turn On", Brushes.Green);
                            SendDownSignal(UpStreamSignal.Send_Complete, true);
                            Delay.Start(Param.Cim.WaitDownReceiveAbleOnTime);
                            UnloadStep = 50;
                        }
                        else if (Delay.TimeUp())
                        {
                            AlarmManager.CreateAlarm((int)CimAlarmCode.WaitDnStemRecComTimeout);
                        }
                        break;
                    }
                case 50:
                    {
                        if (!DownToLocalSignal[(int)DownStreamSignal.Receive_Complete])
                        {
                            OnShowMsg?.Invoke("LinkSignalFlow", "下游ReceiveComplete信号Turn Off，本机Send_Complete,Pin_State,Shutter_State,Send_Start信号Turn Off，Unload Glass流程结束", Brushes.Green);
                            SendDownSignal(UpStreamSignal.Send_Complete, false);
                            SendDownSignal(UpStreamSignal.Send_Start, false);
                            SendDownSignal(UpStreamSignal.Send_Able, false);
                            IonizerControl(false);
                            Ini.Write(LocalToRobot, AfterHandlingOver, "false");//本机做上游时，AfterHandlingOver复位
                            UnloadStep = 0;
                        }
                        else if (Delay.TimeUp())
                        {
                            AlarmManager.CreateAlarm((int)CimAlarmCode.WaitDnStemRecComOffTimeout);
                        }
                        break;
                    }
                default:
                    break;
            }
        }

        /// <summary>
        /// 上下游正常交互流程，请参照附件4.BOEMY Equipment Link Signal Specification_Rev1.1.9_20190301文档的第150页，5.22 Type 22 Time Chart (Robot-Single Sutter & Pin Stage : Exchange/Two Action)
        /// 整个交互流程分5个节点
        /// 节点1：StageUpstream SendAble
        /// 节点2：RobotDowmstream Job_Transfer_Signal_1
        /// 节点3：RobotDowmstream Receive_Complete
        /// 节点4：RobotUpstream Send_Start
        /// 节点5：RobotUpstream Send_Complete
        /// 节点1到节点2：本机把Glass给Robot流程，Glass在本机
        /// 节点2到节点3：本机把Glass给Robot流程，Glass在Robot
        /// 节点3到节点4：Robot把Glass给本机流程，Glass在Robot
        /// 节点4到节点5：Robot把Glass给本机流程，Glass在本机
        /// </summary>
        public void ExChangeThread()
        {
            SignalMonitor();
            switch (NormalInterfaceStep)
            {
                case 1:
                    {
                        for (int i = 0; i < LinkSignalCount; i++)   //  先复位所有信号，再Online 开始，由设备开始发起交互
                        {
                            LocalToUpSignal[i] = false;
                            LocalToDownSignal[i] = false;
                        }
                        CimManager.Instance.WriteLocalToUp(LocalToUpSignal);
                        CimManager.Instance.WriteLocalToDown(LocalToDownSignal);
                        TurnOnInlineSignal();
                        IsRecoveryException = false;
                        string temp5 = Ini.Read("Eqp", "LocalTrouble", "false");//读取上次交互时机械手是否异常
                        string temp6 = Ini.Read("Eqp", "RobotTrouble", "false");//读取上次交互时本机是否异常
                        bool.TryParse(temp5, out bool isLocalTrouble);
                        bool.TryParse(temp6, out bool isRobotTrouble);
                        RecoveryInterfaceStep = 1;
                        IsHandlingOverGlass = false;
                        if (isLocalTrouble)//上一次交互本机异常恢复
                        {
                            OnShowMsg?.Invoke("LinkSignalFlow", "本机请求异常恢复开始", Brushes.Green);
                            NormalInterfaceStep = 10;
                        }
                        else if (isRobotTrouble)//上一次交互机械手异常恢复
                        {
                            OnShowMsg?.Invoke("LinkSignalFlow", "等待Robot请求异常恢复", Brushes.Green);
                            NormalInterfaceStep = 20;
                        }
                        else//上一次交互正常
                        {
                            RecoveryInterfaceStep = 0;
                            NormalInterfaceStep = 30;
                        }
                        break;
                    }
                case 10:
                    {
                        if (RecoveryInterfaceStep == 0)
                        {
                            OnShowMsg?.Invoke("LinkSignalFlow", "本机异常恢复完成", Brushes.Green);
                            Ini.Write("Eqp", "LocalTrouble", "false");
                            NormalInterfaceStep = 30;
                        }
                        else
                        {
                            InterfaceRecoveryThread(0);//本机异常
                        }
                        break;
                    }
                case 20:
                    {
                        if (RecoveryInterfaceStep == 0)
                        {
                            OnShowMsg?.Invoke("LinkSignalFlow", "Robot异常恢复完成", Brushes.Green);
                            Ini.Write("Eqp", "RobotTrouble", "false");
                            NormalInterfaceStep = 30;
                        }
                        else
                        {
                            InterfaceRecoveryThread(1);//机械手异常
                        }
                        break;
                    }
                case 30:
                    {
                        IsRecoveryException = true;
                        if (IsHandlingOverGlass)//异常恢复完毕，如果是glass已经交互完毕，跳转到180步，下降Z轴，并关闭门禁气缸
                        {
                            OnShowMsg?.Invoke("LinkSignalFlow", "异常恢复完成，Glass已经交互完成", Brushes.Green);
                            NormalInterfaceStep = 180;
                        }
                        else
                        {
                            SendDownSignal(UpStreamSignal.Exchange_Possible, true);
                            SendUpSignal(DownStreamSignal.Exchange_Possible, true);
                            NormalInterfaceStep = 33;
                        }
                        break;
                    }
                case 33:
                    {
                        if (Global.InterfaceMode == 0)
                        {
                            OnShowMsg?.Invoke("LinkSignalFlow", "执行Load Glass流程", Brushes.Green);
                            LoadStep = 1;
                            NormalInterfaceStep = 35;
                        }
                        else
                        {
                            OnShowMsg?.Invoke("LinkSignalFlow", "执行Exchange Glass流程", Brushes.Green);
                            NormalInterfaceStep = 40;
                        }
                        break;
                    }
                case 35:
                    {
                        if (LoadStep == 0)
                        {
                            NormalInterfaceStep = 180;
                        }
                        else
                        {
                            LoadThread();
                        }
                        break;
                    }
                case 40://写入JobData数据给下游
                    {
                        OnShowMsg?.Invoke("LinkSignalFlow", "交互流程开始，本地写入JobData数据", Brushes.Green);
                        CimManager.Instance.WriteJobData(Global.GlassID);
                        Delay.Start(Param.Cim.DelayTime);
                        NormalInterfaceStep = 50;
                        break;
                    }
                case 50:  ///  发起交互
                    {
                        if (Delay.TimeUp())
                        {
                            Ini.Write(LocalToRobot, BeforeHandlingOver, "true");//本机做上游时，BeforeHandlingOver置位
                            SendDownSignal(UpStreamSignal.Send_Able, true);
                            SendDownSignal(UpStreamSignal.Position_Back, true);
                            SendDownSignal(UpStreamSignal.Position_Front, true);
                            SendUpSignal(DownStreamSignal.Position_Back, true);
                            SendUpSignal(DownStreamSignal.Position_Front, true);
                            OnShowMsg?.Invoke("LinkSignalFlow", "本地SendAble信号Turn On，等待下游Receive_Able信号Turn On", Brushes.Green);
                            NormalInterfaceStep = 55;
                            Delay.Start(Param.Detect.IdleDelayTime);
                        }
                        break;
                    }
                case 55:
                    {
                        if (DownToLocalSignal[(int)DownStreamSignal.Receive_Able])  ///  下游开始接收
                        {
                            CimManager.Instance.MachineStatusChangeReport(HTPCBase.Views.EqpSts.RUN);
                            Thread.Sleep(100);
                            if (DownToLocalSignal[(int)DownStreamSignal.Exchange_Execute])
                            {
                                OnShowMsg?.Invoke("LinkSignalFlow", "下游Exchange_Execute信号为On，执行Exchange流程", Brushes.Green);
                                NormalInterfaceStep = 60;
                            }
                            else
                            {
                                OnShowMsg?.Invoke("LinkSignalFlow", "下游Exchange_Execute信号为Off，执行Unload流程", Brushes.Green);
                                UnloadStep = 1;
                                NormalInterfaceStep = 57;
                            }
                        }
                        else if (Delay.TimeUp())
                        {
                            CimManager.Instance.MachineStatusChangeReport(HTPCBase.Views.EqpSts.IDLE);
                        }
                        break;
                    }
                case 57://Unload流程
                    {
                        if (UnloadStep == 0)
                        {
                            OnShowMsg?.Invoke("LinkSignalFlow", "Unload流程结束，执行Load Glass流程", Brushes.Green);
                            LoadStep = 1;
                            NormalInterfaceStep = 58;
                        }
                        else
                        {
                            UnloadThread();
                        }
                        break;
                    }
                case 58://Load Glass流程
                    {
                        if (LoadStep == 0)
                        {
                            NormalInterfaceStep = 180;
                            OnShowMsg?.Invoke("LinkSignalFlow", "Load Glass执行流程结束", Brushes.Green);
                        }
                        else
                        {
                            LoadThread();
                        }
                        break;
                    }
                case 60:  /// 下游Exchange_Execute信号为On，执行Exchange流程
                    {
                        if (UpToLocalSignal[(int)UpStreamSignal.Send_Able])//等待上游SendAble信号Turn On
                        {
                            OnShowMsg?.Invoke("LinkSignalFlow", "上游SendAble信号Turn On，校验接收到JobData", Brushes.Green);
                            if (JobDataCheck())//校验从上游接收的JobData，如果检验成功，Pin升起
                            {
                                if (MotionManager.IsArrive(StationEnum.DualStage, AxisName.Stage1_Z, StagePosName.UpSecondPos) && 
                                    MotionManager.IsArrive(StationEnum.DualStage, AxisName.Stage2_Z, StagePosName.UpSecondPos))
                                {
                                    if (MotionManager.GetDigitalInput(InputName.ShutterOpenL) && MotionManager.GetDigitalInput(InputName.ShutterOpenR))//等待shutter打开到位
                                    {
                                        OnShowMsg?.Invoke("LinkSignalFlow", "JobData校验完成，本地Shutter_State和Pin_State信号Turn On", Brushes.Green);
                                        SafeSignal(true);
                                        IonizerControl(true);
                                        Delay.Start(Param.Cim.DelayTime);
                                        NormalInterfaceStep = 90;
                                    }
                                    else
                                    {
                                        AlarmManager.CreateAlarm((int)CimAlarmCode.ShutterNotReady);
                                    }
                                }
                                else
                                {
                                    AlarmManager.CreateAlarm((int)CimAlarmCode.StageNotReady);
                                }
                            }
                            else//如果校验失败，回到第5步，复位报警后重新发送SendAble
                            {
                                AlarmManager.CreateAlarm((int)CimAlarmCode.JobDataCheckError);
                                NormalInterfaceStep = 50;
                            }
                        }
                        break;
                    }
                case 90:
                    {
                        if (Delay.TimeUp())
                        {
                            if (DownToLocalSignal[(int)DownStreamSignal.Receive_Able])
                            {
                                OnShowMsg?.Invoke("LinkSignalFlow", "本地Send_Start信号Turn On，等待下游Job_Transfer_Signal_1信号Turn On", Brushes.Green);
                                SendDownSignal(UpStreamSignal.Send_Start, true);
                                NormalInterfaceStep = 100;
                                Delay.Start(Param.Cim.WaitDownJobTransferSignalOnTime);
                            }
                            else
                            {
                                AlarmManager.CreateAlarm((int)CimAlarmCode.DownReceiveAbleNotOn);
                            }
                        }
                        break;
                    }
                case 100:
                    {
                        if (DownToLocalSignal[(int)DownStreamSignal.Job_Transfer_Signal_1])//等待下游JobTransferSignal信号Turn On，然后发送SendOutJobReport
                        {
                            OnShowMsg?.Invoke("LinkSignalFlow", "上游Job_Transfer_Signal_1信号Turn On，上报SendOutJobReport，本地Receive_Able信号Turn On", Brushes.Green);
                            CimManager.Instance.FetchedOutReport(GetJobDatas());
                            CimManager.Instance.SentOutJobReport(GetJobDatas());
                            CimManager.Instance.ProcessEndReport(GetJobDatas());
                            SendUpSignal(DownStreamSignal.Receive_Able, true);//本机ReceiveAble Turn On
                            Delay.Start(Param.Cim.DelayTime);
                            // Ini.Write(LocalToRobot, BeforeHandlingOver, "false");//本机做上游时，BeforeHandlingOver标志位复位
                            //Ini.Write(LocalToRobot, AfterHandlingOver, "true");//本机做上游时，AfterHandlingOver置位
                            NormalInterfaceStep = 110;
                        }
                        else if (Delay.TimeUp())
                        {
                            AlarmManager.CreateAlarm((int)CimAlarmCode.WaitDnStemJobTranSigTimeout);
                            NormalInterfaceStep = 90;
                        }
                        break;
                    }
                case 110:
                    {
                        if (Delay.TimeUp())
                        {
                            OnShowMsg?.Invoke("LinkSignalFlow", "本地Receive_Start信号Turn On，等待下游Receive_Complete信号Turn On", Brushes.Green);
                            SendUpSignal(DownStreamSignal.Receive_Start, true);//本机ReceiveStart信号Turn On
                            Delay.Start(Param.Cim.WaitDownReceiveCompleteOnTime);
                            NormalInterfaceStep = 120;
                        }
                        break;
                    }
                case 120:
                    {
                        if (DownToLocalSignal[(int)DownStreamSignal.Receive_Complete])//等待下游ReceiveComplete信号Turn On
                        {
                            OnShowMsg?.Invoke("LinkSignalFlow", "下游Receive_Complete信号Turn On，上报FetchedOutReport，本地Send_Complete信号Turn On", Brushes.Green);
                            OnShowMsg?.Invoke("LinkSignalFlow", "等待下游Receive_Complete信号Turn Off", Brushes.Green);
                            SendDownSignal(UpStreamSignal.Send_Complete, true);
                            Delay.Start(Param.Cim.WaitDownReceiveCompleteOffTime);
                            Ini.Write(LocalToRobot, AfterHandlingOver, "false");//本机做上游时，AfterHandlingOver复位
                            NormalInterfaceStep = 130;
                        }
                        else if (Delay.TimeUp())
                        {
                            AlarmManager.CreateAlarm((int)CimAlarmCode.WaitDnStemRecComTimeout);
                            NormalInterfaceStep = 110;
                        }
                        break;
                    }
                case 130:
                    {
                        if (DownToLocalSignal[(int)DownStreamSignal.Receive_Complete] == false)//等待下游ReceiveComplete信号Turn Off
                        {
                            OnShowMsg?.Invoke("LinkSignalFlow", "下游Receive_Complete信号Turn Off，本地Send_Start，Send_Able，Send_Complete信号Turn Off", Brushes.Green);
                            OnShowMsg?.Invoke("LinkSignalFlow", "等待上游Send_Start信号Turn On", Brushes.Green);
                            SendDownSignal(UpStreamSignal.Send_Start, false);
                            SendDownSignal(UpStreamSignal.Send_Able, false);
                            SendDownSignal(UpStreamSignal.Send_Complete, false);
                            Delay.Start(Param.Cim.WaitUpSendStartOnTime);
                            NormalInterfaceStep = 140;
                        }
                        else if (Delay.TimeUp())
                        {
                            AlarmManager.CreateAlarm((int)CimAlarmCode.WaitDnStemRecComOffTimeout);
                            NormalInterfaceStep = 120;
                        }
                        break;
                    }
                case 140:
                    {
                        if (UpToLocalSignal[(int)UpStreamSignal.Send_Start])
                        {
                            OnShowMsg?.Invoke("LinkSignalFlow", "上游Send_Start信号Turn On，等待上游Job_Transfer_Signal_1信号Turn On", Brushes.Green);
                            OnShowMsg?.Invoke("LinkSignalFlow", "等待上游Job_Transfer_Signal_1信号Turn On", Brushes.Green);
                            Ini.Write(RobotToLocal, BeforeHandlingOver, "true");//本机做下游时，BeforeHandlingOver标志位置位
                            Delay.Start(Param.Cim.WaitUpJobTransferSignalOnTime);
                            NormalInterfaceStep = 150;
                        }
                        else if (Delay.TimeUp())
                        {
                            AlarmManager.CreateAlarm((int)CimAlarmCode.WaitUpStemStartOnTimeout);
                            NormalInterfaceStep = 150;
                        }
                        break;
                    }
                case 150:
                    {
                        if (UpToLocalSignal[(int)UpStreamSignal.Job_Transfer_Signal_1])
                        {
                            OnShowMsg?.Invoke("LinkSignalFlow", "上游Job_Transfer_Signal_1信号Turn On，等待上游Send_Complete信号Turn On", Brushes.Green);
                            Ini.Write(RobotToLocal, BeforeHandlingOver, "false");//本机做下游时，BeforeHandlingOver标志位复位
                            Ini.Write(RobotToLocal, AfterHandlingOver, "true");//本机做下游时，AfterHandlingOver标志位置位
                            Delay.Start(Param.Cim.WaitUpSendCompleteOnTime);
                            NormalInterfaceStep = 160;
                        }
                        else if (Delay.TimeUp())
                        {
                            AlarmManager.CreateAlarm((int)CimAlarmCode.WaitUpJobTransferSignalOnTimeout);
                            NormalInterfaceStep = 140;
                        }
                        break;
                    }
                case 160:
                    {
                        if (UpToLocalSignal[(int)UpStreamSignal.Send_Complete])
                        {
                            OnShowMsg?.Invoke("LinkSignalFlow", "上游Send_Complete信号Turn On，本地Receive_Complete信号Turn On，等待上游Send_Complete信号Turn Off", Brushes.Green);
                            SendUpSignal(DownStreamSignal.Receive_Complete, true);
                            Delay.Start(Param.Cim.WaitUpSendCompleteOffTime);
                            NormalInterfaceStep = 170;
                        }
                        else if (Delay.TimeUp())
                        {
                            AlarmManager.CreateAlarm((int)CimAlarmCode.WaitUpStemSendComTimeout);
                            NormalInterfaceStep = 150;
                        }
                        break;
                    }
                case 170:
                    {
                        if (UpToLocalSignal[(int)UpStreamSignal.Send_Complete] == false)//等待上游SendComplete信号Turn Off
                        {
                            OnShowMsg?.Invoke("LinkSignalFlow", "上游Send_Complete信号Turn Off，复位本机Receive_Start等信号", Brushes.Green);
                            Ini.Write(RobotToLocal, AfterHandlingOver, "false");//本机做下游时，AfterHandlingOver标志位置位
                            SendUpSignal(DownStreamSignal.Receive_Start, false);
                            SendUpSignal(DownStreamSignal.Receive_Able, false);
                            SendUpSignal(DownStreamSignal.Receive_Complete, false);
                            CimManager.Instance.ReceivedJobReport(JobDatas);
                            CimManager.Instance.StoredInReport(JobDatas);
                            CimManager.Instance.ProcessStartReport(JobDatas);
                            if (JobDatas != null && JobDatas.Length > 0)
                            {
                                OnUpdatePPID?.Invoke(JobDatas[0].PPID[1].ToString());
                            }
                            NormalInterfaceStep = 180;
                        }
                        else if (Delay.TimeUp())
                        {
                            AlarmManager.CreateAlarm((int)CimAlarmCode.WaitUpStemSendComOffTimeout);
                            NormalInterfaceStep = 160;
                        }
                        break;
                    }
                case 180:
                    {
                        SendDownSignal(UpStreamSignal.Position_Back, false);
                        SendDownSignal(UpStreamSignal.Position_Front, false);
                        SendUpSignal(DownStreamSignal.Position_Back, false);
                        SendUpSignal(DownStreamSignal.Position_Front, false);
                        SafeSignal(false);
                        IonizerControl(false);
                        SendDownSignal(UpStreamSignal.Exchange_Possible, false);
                        SendUpSignal(DownStreamSignal.Exchange_Possible, false);
                        OnShowMsg?.Invoke("LinkSignalFlow", "交互完毕", Brushes.Green);
                        if (IsRecoveryException == false)
                        {
                            CimManager.Instance.JobDataClear();
                        }
                        UpdateOutEqpTime(Global.GlassID[0]);
                        UpdateOutEqpTime(Global.GlassID[1]);
                        LastGlassID = string.Empty;
                        Global.GlassID[0] = JobDatas[0].Glass_ID;
                        Global.GlassID[1] = JobDatas[1].Glass_ID;
                        ResetInterfaceBits();
                        NormalInterfaceStep = 0;
                        break;
                    }
            }
            IsInInterfaceFlow = NormalInterfaceStep > 0;
        }

        /// <summary>
        /// 本机做下游，发送本机信号给上游
        /// </summary>
        /// <param name="signal"></param>
        /// <param name="value"></param>
        public void SendUpSignal(DownStreamSignal signal, bool value)
        {
            LocalToUpSignal[(int)signal] = value;
            CimManager.Instance.WriteLocalToUp(LocalToUpSignal);
        }

        /// <summary>
        /// 本机做上游，发送本机信号给下游
        /// </summary>
        /// <param name="signal"></param>
        /// <param name="value"></param>
        public void SendDownSignal(UpStreamSignal signal, bool value)
        {
            LocalToDownSignal[(int)signal] = value;
            CimManager.Instance.WriteLocalToDown(LocalToDownSignal);
        }
        /// <summary>
        /// JobData验证
        /// </summary>
        /// <returns></returns>
        private bool JobDataCheck()
        {
            bool ret = false;
            if (JobDatas != null)
            {
                if (JobDatas[0] != null && IsPinDetectGlass(0))
                {
                    if (JobDatas[1] != null && IsPinDetectGlass(1))
                    {
                        OnShowMsg?.Invoke("LinkSignalFlow", "JobDataCheck成功", Brushes.Blue);
                        OnShowMsg?.Invoke("LinkSignalFlow", $"Glass1ID:{JobDatas[0].Glass_ID}，Glass2ID:{JobDatas[1].Glass_ID}", Brushes.Blue);
                        ret = true;
                    }
                    else
                    {
                        if (JobDatas[1] != null && !IsPinDetectGlass(1))
                        {
                            OnShowMsg?.Invoke("LinkSignalFlow", "JobDataCheck失败，接收到JobData1数据，但是平台1没有检测到Glass", Brushes.Red);
                        }
                        else if (JobDatas[1] == null && IsPinDetectGlass(1))
                        {
                            OnShowMsg?.Invoke("LinkSignalFlow", "JobDataCheck失败，没接收到JobData1数据，但是平台1检测到Glass", Brushes.Red);
                        }
                    }
                }
                else
                {
                    if (JobDatas[0] != null && !IsPinDetectGlass(0))
                    {
                        OnShowMsg?.Invoke("LinkSignalFlow", "JobDataCheck失败，接收到JobData1数据，但是平台1没有检测到Glass", Brushes.Red);
                    }
                    else if (JobDatas[0] == null && IsPinDetectGlass(0))
                    {
                        OnShowMsg?.Invoke("LinkSignalFlow", "JobDataCheck失败，没接收到JobData1数据，但是平台1检测到Glass", Brushes.Red);
                    }
                }
            }
            else
            {
                OnShowMsg?.Invoke("LinkSignalFlow", "JobDataCheck失败，JobData数据为空", Brushes.Red);
            }
            return ret;
        }
        /// <summary>
        /// 平台Pin针是否检测到Glass
        /// </summary>
        /// <param name="glassNum"></param>
        /// <returns></returns>
        private bool IsPinDetectGlass(int glassNum)
        {
            string inputName = string.Empty;
            bool ret = true;
            //for (int i = 0; i < 4; i++)
            //{
            //    inputName = $"Stage{glassNum + 1}PinDetect{i + 1}";
            //    ret &= MotionManager.GetDigitalInput(inputName);
            //    Thread.Sleep(10);
            //}            
            return ret;
        }
        /// <summary>
        /// 平台是否检测到Glass
        /// </summary>
        /// <param name="glassNum"></param>
        /// <returns></returns>
        private bool IsDetectGlass(int glassNum)
        {
            string inputName = string.Empty;
            bool ret = true;
            //for (int i = 0; i < 4; i++)
            //{
            //    inputName = $"Stage{glassNum + 1}PinDetect{i + 1}";
            //    ret &= MotionManager.GetDigitalInput(inputName);
            //    Thread.Sleep(10);
            //}            
            return ret;
        }
        /// <summary>
        ///  上下游异常恢复流程，请参照附件4.BOEMY Equipment Link Signal Specification_Rev1.1.9_20190301文档的第8.2章节
        /// </summary>
        /// <param name="localOrRobot">0：本机异常。1：机械手异常</param>
        public void InterfaceRecoveryThread(int localOrRobotTrouble)
        {
            switch (RecoveryInterfaceStep)
            {
                case 1:
                    {
                        string temp1 = Ini.Read(LocalToRobot, BeforeHandlingOver, "false");//本机把Glass给Robot，本机或者Robot发生异常时，Glass是否还在本机
                        string temp2 = Ini.Read(LocalToRobot, AfterHandlingOver, "false");//本机把Glass给Robot，本机或者Robot发生异常时，Glass是否已经在Robot
                        string temp3 = Ini.Read(RobotToLocal, BeforeHandlingOver, "false");//Robot把Glass给本机，本机或者Robot发生异常时，Glass是否还在Robot
                        string temp4 = Ini.Read(RobotToLocal, AfterHandlingOver, "false");//Robot把Glass给本机，本机或者Robot发生异常时，Glass是否已经在本机
                        bool.TryParse(temp1, out bool LocalToRobotBeforeHandlingOver);
                        bool.TryParse(temp2, out bool LocalToRobotAfterHandlingOver);
                        bool.TryParse(temp3, out bool RobotToLocalBeforeHandlingOver);
                        bool.TryParse(temp4, out bool RobotToLocalAfterHandlingOver);
                        if (LocalToRobotBeforeHandlingOver)//本机做上游，异常时，Glass在本机
                        {
                            if (localOrRobotTrouble == 0)//本机做上游，玻璃交互给下游之前，本机发生异常，本机请求从异常中恢复
                            {
                                OnShowMsg?.Invoke("LinkSignalFlow", "本机与下游交互时，本机发生异常，Glass还未交互给下游，本机请求异常恢复开始", Brushes.Green);
                                LocalToDownRecoveryExceptionStep = 1;
                                RecoveryInterfaceStep = 10;
                                LocalToDownIsHandlingOverGlass = false;
                            }
                            else//本机做上游，玻璃交互给下游之前，下游发生异常，下游请求从异常中恢复
                            {
                                OnShowMsg?.Invoke("LinkSignalFlow", "本机与下游交互时，下游发生异常，Glass还未交互给下游，下游请求异常恢复开始", Brushes.Green);
                                DownToLocalRecoveryExceptionStep = 1;
                                RecoveryInterfaceStep = 30;
                                DownToLocalIsHandlingOverGlass = false;
                            }
                        }
                        else if (LocalToRobotAfterHandlingOver)//本机做上游，异常时，Glass在下游
                        {
                            if (localOrRobotTrouble == 0)//本机做上游，玻璃交互给下游之后，本机发生异常，本机请求从异常中恢复
                            {
                                OnShowMsg?.Invoke("LinkSignalFlow", "本机与下游交互时，本机发生异常，Glass已经交互给下游，本机请求异常恢复开始", Brushes.Green);
                                LocalToDownRecoveryExceptionStep = 1;
                                RecoveryInterfaceStep = 10;
                                LocalToDownIsHandlingOverGlass = true;
                            }
                            else//本机做上游，玻璃交互给下游之后，下游发生异常，下游请求从异常中恢复
                            {
                                OnShowMsg?.Invoke("LinkSignalFlow", "本机与下游交互时，下游发生异常，Glass已经交互给下游，下游请求异常恢复开始", Brushes.Green);
                                DownToLocalRecoveryExceptionStep = 1;
                                RecoveryInterfaceStep = 30;
                                DownToLocalIsHandlingOverGlass = true;
                            }
                        }
                        else if (RobotToLocalBeforeHandlingOver)//本机做下游，异常时，Glass在上游
                        {
                            if (localOrRobotTrouble == 0)//本机做下游，玻璃交互给本机之前，本机发生异常，本机请求从异常中恢复
                            {
                                OnShowMsg?.Invoke("LinkSignalFlow", "本机与上游交互时，本机发生异常，Glass还未交互给本机，本机请求异常恢复开始", Brushes.Green);
                                LocalToUpRecoveryExceptionStep = 1;
                                RecoveryInterfaceStep = 20;
                                LocalToUpIsHandlingOverGlass = false;
                            }
                            else//本机做下游，玻璃交互给本机之前，上游发生异常，上游请求从异常中恢复
                            {
                                OnShowMsg?.Invoke("LinkSignalFlow", "本机与上游交互时，上游发生异常，Glass还未交互给本机，上游请求异常恢复开始", Brushes.Green);
                                UpToLocalRecoveryExceptionStep = 1;
                                RecoveryInterfaceStep = 40;
                                UpToLocalIsHandlingOverGlass = false;
                            }
                        }
                        else if (RobotToLocalAfterHandlingOver)//本机做下游，异常时，Glass在本机
                        {
                            if (localOrRobotTrouble == 0)//本机做下游，玻璃交互给本机之后，本机发生异常，本机请求从异常中恢复
                            {
                                OnShowMsg?.Invoke("LinkSignalFlow", "本机与上游交互时，本机发生异常，Glass已经交互给本机，本机请求异常恢复开始", Brushes.Green);
                                LocalToUpRecoveryExceptionStep = 1;
                                RecoveryInterfaceStep = 20;
                                LocalToUpIsHandlingOverGlass = true;
                            }
                            else//本机做下游，玻璃交互给本机之后，上游发生异常，上游请求从异常中恢复
                            {
                                OnShowMsg?.Invoke("LinkSignalFlow", "本机与上游交互时，上游发生异常，Glass已经交互给本机，上游请求异常恢复开始", Brushes.Green);
                                UpToLocalRecoveryExceptionStep = 1;
                                RecoveryInterfaceStep = 40;
                                UpToLocalIsHandlingOverGlass = true;
                            }
                        }
                        else
                        {
                            RecoveryInterfaceStep = 0;
                        }
                        break;
                    }
                case 10:
                    {
                        if (LocalToDownRecoveryExceptionStep == 0)
                        {
                            RecoveryInterfaceStep = 0;
                            IsHandlingOverGlass = LocalToDownIsHandlingOverGlass;
                        }
                        else
                        {
                            LocalToDownRecoveryException(LocalToDownIsHandlingOverGlass);
                        }
                        break;
                    }
                case 20:
                    {
                        if (LocalToUpRecoveryExceptionStep == 0)
                        {
                            RecoveryInterfaceStep = 0;
                            IsHandlingOverGlass = LocalToUpIsHandlingOverGlass;
                        }
                        else
                        {
                            LocalToUpRecoveryException(LocalToUpIsHandlingOverGlass);
                        }
                        break;
                    }
                case 30:
                    {
                        if (DownToLocalRecoveryExceptionStep == 0)
                        {
                            RecoveryInterfaceStep = 0;
                            IsHandlingOverGlass = DownToLocalIsHandlingOverGlass;
                        }
                        else
                        {
                            DownToLocalRecoveryException(DownToLocalIsHandlingOverGlass);
                        }
                        break;
                    }
                case 40:
                    {
                        if (UpToLocalRecoveryExceptionStep == 0)
                        {
                            RecoveryInterfaceStep = 50;
                            IsHandlingOverGlass = UpToLocalIsHandlingOverGlass;
                        }
                        else
                        {
                            UpToLocalRecoveryException(UpToLocalIsHandlingOverGlass);
                        }
                        break;
                    }
                case 50:
                    {
                        Ini.Write(LocalToRobot, BeforeHandlingOver, "false");
                        Ini.Write(LocalToRobot, AfterHandlingOver, "false");
                        Ini.Write(RobotToLocal, BeforeHandlingOver, "false");
                        Ini.Write(RobotToLocal, AfterHandlingOver, "false");
                        RecoveryInterfaceStep = 0;
                        break;
                    }
            }
        }
        /// <summary>
        /// 本机做上游时，与下游交互时本机发生异常，从异常中恢复
        /// </summary>
        /// <param name="isAfterHandleOverGlass">Glass是否已经交互给下游</param>
        private void LocalToDownRecoveryException(bool isAfterHandleOverGlass)
        {
            switch (LocalToDownRecoveryExceptionStep)
            {
                case 1:
                    {
                        if (isAfterHandleOverGlass)
                        {
                            CimManager.Instance.FetchedOutReport(GetJobDatas());
                            CimManager.Instance.SentOutJobReport(GetJobDatas());
                            CimManager.Instance.ProcessEndReport(GetJobDatas());
                            OnShowMsg?.Invoke("LinkSignalFlow", "下游已经接收到Glass，上报SendOutJobReport", Brushes.Green);
                        }
                        else
                        {
                            OnShowMsg?.Invoke("LinkSignalFlow", "下游还未接收到Glass", Brushes.Green);
                        }
                        LocalToDownRecoveryExceptionStep = 10;
                        break;
                    }
                case 10:
                    {
                        OnShowMsg?.Invoke("LinkSignalFlow", "本地Upstream_Inline信号Turn On，Upstream_Trouble信号Turn Off", Brushes.Green);
                        SendDownSignal(UpStreamSignal.Upstream_Inline, true);
                        SendDownSignal(UpStreamSignal.Upstream_Trouble, false);
                        LocalToDownRecoveryExceptionStep = 20;
                        break;
                    }
                case 20:
                    {
                        OnShowMsg?.Invoke("LinkSignalFlow", "本地Resume_Request信号Turn On，等待下游Resume_Ack信号Turn On", Brushes.Green);
                        SendDownSignal(UpStreamSignal.Resume_Request, true);
                        Delay.Start(Param.Cim.WaitDownInitAckTime);
                        CimManager.Instance.JobDataClear();
                        LocalToDownRecoveryExceptionStep = 30;
                        break;
                    }
                case 30:
                    {
                        if (DownToLocalSignal[(int)DownStreamSignal.Resume_Ack])
                        {
                            OnShowMsg?.Invoke("LinkSignalFlow", "下游Resume_Ack信号Turn On，Send_Able信号和Send_Start信号Turn Off", Brushes.Green);
                            SendDownSignal(UpStreamSignal.Send_Able, false);
                            SendDownSignal(UpStreamSignal.Send_Start, false);
                            Delay.Start(Param.Cim.DelayTime);
                            LocalToDownRecoveryExceptionStep = 40;
                        }
                        else if (DownToLocalSignal[(int)DownStreamSignal.Resume_Nack])
                        {
                            AlarmManager.CreateAlarm((int)CimAlarmCode.DnInitNack);
                            LocalToDownRecoveryExceptionStep = 0;
                        }
                        else if (Delay.TimeUp())
                        {
                            AlarmManager.CreateAlarm((int)CimAlarmCode.WaitDnInitAckOnTImeout);
                        }
                        break;
                    }
                case 40:
                    {
                        SendDownSignal(UpStreamSignal.Resume_Request, false);
                        OnShowMsg?.Invoke("LinkSignalFlow", "本地Resume_Request信号Turn On，本机下料流程异常停止恢复完成", Brushes.Green);
                        LocalToDownRecoveryExceptionStep = 0;
                        break;
                    }
            }
        }
        /// <summary>
        ///  本机做下游时，与上游交互时本机发生异常，从异常中恢复
        /// </summary>
        /// <param name="isAfterHandleOverGlass">Glass是否已经交互给下游</param>
        private void LocalToUpRecoveryException(bool isAfterHandleOverGlass)
        {
            switch (LocalToUpRecoveryExceptionStep)
            {
                case 1:
                    {
                        OnShowMsg?.Invoke("LinkSignalFlow", "本地Downstream_Inline信号Turn On，Downstream_Trouble信号Turn Off", Brushes.Green);
                        SendUpSignal(DownStreamSignal.Downstream_Inline, true);
                        SendUpSignal(DownStreamSignal.Downstream_Trouble, false);
                        LocalToUpRecoveryExceptionStep = 20;
                        break;
                    }
                case 20:
                    {
                        OnShowMsg?.Invoke("LinkSignalFlow", "本地Resume_Request信号Turn On，等待下游Resume_Ack信号Turn On", Brushes.Green);
                        SendUpSignal(DownStreamSignal.Resume_Request, true);
                        Delay.Start(Param.Cim.WaitDownInitAckTime);
                        CimManager.Instance.JobDataClear();
                        LocalToUpRecoveryExceptionStep = 30;
                        break;
                    }
                case 30:
                    {
                        if (UpToLocalSignal[(int)UpStreamSignal.Resume_Ack])
                        {
                            OnShowMsg?.Invoke("LinkSignalFlow", "下游Resume_Ack信号Turn On，Receive_Able信号和Receive_Start信号Turn Off", Brushes.Green);
                            SendUpSignal(DownStreamSignal.Receive_Able, false);
                            SendUpSignal(DownStreamSignal.Receive_Start, false);
                            if (isAfterHandleOverGlass)
                            {
                                var datas = GetJobDatas();
                                CimManager.Instance.ReceivedJobReport(datas);
                                CimManager.Instance.StoredInReport(datas);
                                CimManager.Instance.ProcessStartReport(datas);
                                OnShowMsg?.Invoke("LinkSignalFlow", "下游已经接收到Glass，上报SendOutJobReport", Brushes.Green);
                            }
                            else
                            {
                                OnShowMsg?.Invoke("LinkSignalFlow", "下游还未接收到Glass", Brushes.Green);
                            }
                            Delay.Start(Param.Cim.DelayTime);
                            LocalToUpRecoveryExceptionStep = 40;
                        }
                        else if (UpToLocalSignal[(int)UpStreamSignal.Resume_Nack])
                        {
                            AlarmManager.CreateAlarm((int)CimAlarmCode.UpInitNack);
                            LocalToUpRecoveryExceptionStep = 0;
                        }
                        else if (Delay.TimeUp())
                        {
                            AlarmManager.CreateAlarm((int)CimAlarmCode.WaitUpInitAckOnTImeout);
                        }
                        break;
                    }
                case 40:
                    {
                        SendUpSignal(DownStreamSignal.Resume_Request, false);
                        OnShowMsg?.Invoke("LinkSignalFlow", "本地Resume_Request信号Turn On，本机上料流程异常停止恢复完成", Brushes.Green);
                        LocalToUpRecoveryExceptionStep = 0;
                        break;
                    }
            }
        }
        /// <summary>
        /// 本机做上游时，下游交互时发生异常，下游请求从异常中恢复
        /// </summary>
        /// <param name="isAfterHandleOverGlass">Glass是否已经交互给下游</param>
        private void DownToLocalRecoveryException(bool isAfterHandleOverGlass)
        {
            switch (DownToLocalRecoveryExceptionStep)
            {
                case 1:
                    {
                        if (DownToLocalSignal[(int)DownStreamSignal.Downstream_Inline] && !DownToLocalSignal[(int)DownStreamSignal.Downstream_Trouble])
                        {
                            OnShowMsg?.Invoke("LinkSignalFlow", "下游Downstream_Inline信号Turn On，Downstream_Trouble信号Turn Off，等待下游Resume_Request信号Turn On", Brushes.Green);
                            DownToLocalRecoveryExceptionStep = 10;
                        }
                        break;
                    }
                case 10:
                    {
                        if (DownToLocalSignal[(int)DownStreamSignal.Resume_Request])
                        {
                            OnShowMsg?.Invoke("LinkSignalFlow", "下游Resume_Request信号Turn On，本机Upstream_Inline信号Turn On，Upstream_Trouble、Send_Start、Send_Able信号Turn Off", Brushes.Green);
                            SendDownSignal(UpStreamSignal.Upstream_Inline, true);
                            SendDownSignal(UpStreamSignal.Upstream_Trouble, false);
                            SendDownSignal(UpStreamSignal.Send_Start, false);
                            SendDownSignal(UpStreamSignal.Send_Able, false);
                            DownToLocalRecoveryExceptionStep = 20;
                        }
                        break;
                    }
                case 20:
                    {
                        OnShowMsg?.Invoke("LinkSignalFlow", "本机Resume_Ack信号Turn On，等待下游Resume_Request信号Turn Off", Brushes.Green);
                        SendDownSignal(UpStreamSignal.Resume_Ack, true);
                        DownToLocalRecoveryExceptionStep = 30;
                        break;
                    }
                case 30:
                    {
                        if (DownToLocalSignal[(int)DownStreamSignal.Resume_Request] == false)
                        {
                            OnShowMsg?.Invoke("LinkSignalFlow", "下游Resume_Request信号Turn Off，本机Resume_Ack信号Turn Off", Brushes.Green);
                            SendDownSignal(UpStreamSignal.Resume_Ack, false);
                            DownToLocalRecoveryExceptionStep = 0;
                        }
                        break;
                    }
            }
        }
        /// <summary>
        /// 本机做下游时，上游交互时发生异常，上游请求从异常中恢复
        /// </summary>
        /// <param name="isAfterHandleOverGlass">Glass是否已经交互给下游</param>
        private void UpToLocalRecoveryException(bool isAfterHandleOverGlass)
        {
            switch (UpToLocalRecoveryExceptionStep)
            {
                case 1:
                    {
                        if (UpToLocalSignal[(int)UpStreamSignal.Upstream_Inline] && !UpToLocalSignal[(int)UpStreamSignal.Upstream_Trouble])
                        {
                            OnShowMsg?.Invoke("LinkSignalFlow", "上游Upstream_Inline信号Turn On，Upstream_Trouble信号Turn Off，等待上游Resume_Request信号Turn On", Brushes.Green);
                            UpToLocalRecoveryExceptionStep = 10;
                        }
                        break;
                    }
                case 10:
                    {
                        if (UpToLocalSignal[(int)UpStreamSignal.Resume_Request])
                        {
                            OnShowMsg?.Invoke("LinkSignalFlow", "上游Resume_Request信号Turn On，本机Downstream_Inline信号Turn On，Downstream_Trouble、Receive_Able、Receive_Start信号Turn Off", Brushes.Green);
                            SendUpSignal(DownStreamSignal.Downstream_Inline, true);
                            SendUpSignal(DownStreamSignal.Downstream_Trouble, false);
                            SendUpSignal(DownStreamSignal.Receive_Able, false);
                            SendUpSignal(DownStreamSignal.Receive_Start, false);
                            UpToLocalRecoveryExceptionStep = 20;
                        }
                        break;
                    }
                case 20:
                    {
                        if (isAfterHandleOverGlass)
                        {
                            var datas = GetJobDatas();
                            CimManager.Instance.ReceivedJobReport(datas);
                            CimManager.Instance.StoredInReport(datas);
                            CimManager.Instance.ProcessStartReport(datas);
                        }
                        SendUpSignal(DownStreamSignal.Resume_Ack, true);
                        OnShowMsg?.Invoke("LinkSignalFlow", "本机Resume_Ack信号Turn On，等待上游Resume_Request信号Turn Off", Brushes.Green);
                        UpToLocalRecoveryExceptionStep = 30;
                        break;
                    }
                case 30:
                    {
                        if (UpToLocalSignal[(int)UpStreamSignal.Resume_Request] == false)
                        {
                            OnShowMsg?.Invoke("LinkSignalFlow", "上游Resume_Request信号Turn Off，本机Resume_Ack信号Turn Off", Brushes.Green);
                            SendUpSignal(DownStreamSignal.Resume_Ack, false);
                            UpToLocalRecoveryExceptionStep = 0;
                        }
                        break;
                    }
            }
        }

        /// <summary>
        /// 关闭本机LinkSignal在线信号
        /// </summary>
        public void TurnOffInlineSignal(bool isAlarm = false)
        {
            SendDownSignal(UpStreamSignal.Upstream_Inline, false);
            SendUpSignal(DownStreamSignal.Downstream_Inline, false);
            if (isAlarm)
            {
                SendDownSignal(UpStreamSignal.Upstream_Trouble, true);
                SendUpSignal(DownStreamSignal.Downstream_Trouble, true);
            }
            if (IsInInterfaceFlow)//在交互中，遇到停止，或者重报警，置位LocalTrouble
            {
                // Ini.Write("Eqp", "LocalTrouble", "true");
            }
        }
        /// <summary>
        /// 关闭本机LinkSignal在线信号
        /// </summary>
        public void TurnOnInlineSignal(bool isAlarm = false)
        {
            SendDownSignal(UpStreamSignal.Upstream_Inline, true);
            SendUpSignal(DownStreamSignal.Downstream_Inline, true);
            SendDownSignal(UpStreamSignal.Upstream_Trouble, false);
            SendUpSignal(DownStreamSignal.Downstream_Trouble, false);
        }
        private void IntertDB(GlassDataA[] jobDatas)
        {
            try
            {
                DateTime receiveTime = DateTime.Now;
                var dal = new JobDataDal();
                JobDataModel model1 = new JobDataModel { GlassNum = 0, ReceiveTime = receiveTime, GlassID = jobDatas[0].Glass_ID, JobData = jobDatas[0].ToString2() };
                JobDataModel model2 = new JobDataModel { GlassNum = 1, ReceiveTime = receiveTime, GlassID = jobDatas[1].Glass_ID, JobData = jobDatas[1].ToString2() };
                dal.Insert(model1);
                dal.Insert(model2);
                if (Param.Control.IsSaveJobDataToLog)
                {
                    LogManager.GetLogger("JobData").Info(jobDatas[0].ToString() + "\n");
                    LogManager.GetLogger("JobData").Info(jobDatas[1].ToString() + "\n");
                }
                var cellStartTimeDal = new CellStartTimeDal();
                cellStartTimeDal.Insert(new CellStartTimeModel { PanelID = jobDatas[0].Glass_ID, StartTime = receiveTime });
                cellStartTimeDal.Insert(new CellStartTimeModel { PanelID = jobDatas[1].Glass_ID, StartTime = receiveTime });
            }
            catch (Exception ex)
            {
                OnShowMsg?.Invoke("LinkSignalFlow", $"IntertDB异常，异常信息：{ex.Message}", Brushes.Green);
            }
        }
        private void UpdateOutEqpTime(string glassID)
        {
            try
            {
                var info = DBManager.GetTotalInfo(glassID);
                if (info != null)
                {
                    info.EQPOutTime = DateTime.Now;
                    new TotalInfoDal().Update(info);
                }
            }
            catch (Exception ex)
            {

            }

        }
        private GlassDataA[] GetJobDatas()
        {
            try
            {
                var model = new JobDataDal().Get(Global.GlassID[0]);
                GlassDataA jobData = new GlassDataA();
                jobData.FromString(model.JobData);
                var model2 = new JobDataDal().Get(Global.GlassID[1]);
                GlassDataA jobData2 = new GlassDataA();
                jobData2.FromString(model2.JobData);
                return new GlassDataA[] { jobData, jobData2 };
            }
            catch (Exception)
            {
                return null;
            }
        }

        private void ResetBits()
        {
            NormalInterfaceStep = 1;
            IsInInterfaceFlow = false;
        }
        private void SafeSignal(bool onOff)
        {
            SendUpSignal(DownStreamSignal.Pin_State, onOff);
            SendUpSignal(DownStreamSignal.Shutter_State, onOff);
            SendDownSignal(UpStreamSignal.Pin_State, onOff);
            SendDownSignal(UpStreamSignal.Shutter_State, onOff);
            MotionManager.SetDigitalOutput(OutputName.Stage1PinSts, onOff);
            MotionManager.SetDigitalOutput(OutputName.Stage2PinSts, onOff);
            MotionManager.SetDigitalOutput(OutputName.Stage1ShutterSts, onOff);
            MotionManager.SetDigitalOutput(OutputName.Stage2ShutterSts, onOff);
        }
        public void ResetInterfaceBits()
        {
            Ini.Write("Eqp", "LocalTrouble", "false");
            Ini.Write("Eqp", "RobotTrouble", "false");
            Ini.Write(LocalToRobot, BeforeHandlingOver, "false");
            Ini.Write(LocalToRobot, AfterHandlingOver, "false");
            Ini.Write(RobotToLocal, BeforeHandlingOver, "false");
            Ini.Write(RobotToLocal, AfterHandlingOver, "false");
            IsRecoveryException = false;
            RecoveryInterfaceStep = -1;
            IsHandlingOverGlass = false;

        }
        private void IonizerControl(bool value)
        {
            MotionManager.SetDigitalOutput(OutputName.ShutterIonizer, value);
        }
    }
}
