/*----------------------------------------------------------------
// Copyright (C) Suzhou HYC Technology Co.,LTD
// 版权所有。
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnMain
// 文件名称    ：ProbeGantry.cs
// =================================
// 创 建 者    ：guguangzhou
// 创建日期    ：2021/01/29 09:00:00 
// 功能描述    ：Probe龙门工位自动流程
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
using HYC.HTPCBase.Extension.Recipe;
using HYC.SaturnDB.DAL;
using HYC.SaturnDB.Model;
using HYC.HTLog;
using HYC.HTPCBase.Alarm;
using HYC.HTPCBase.Motion;
using HYC.HTPCBase.Station;

using HYC.SaturnMain.Params;
using HYC.SaturnMain.Detections;
using HYC.SaturnMain.Devices.ClientPC;
using HYC.SaturnMain.Devices.Motion;
using HYC.SaturnMain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HYC.HTPCBase.Entities;

using HYC.HTFile.HTINI;
using HYC.SaturnMain.Alarm;
using HYC.SaturnMain;
using HYC.MicroLedATDetect.AutoPos;
using HYC.HTPCBase.Param;
using BOECIM.Devices.CIM;
using CIMBOE.Block;
using HYC.SaturnMain.Devices.Power;

namespace HYC.SaturnMain.Station
{
    public partial class ProbeGantry : IStation
    {
        public static event Action<int> OnTriggerSignal;
        public static event Action<bool> OnUpdateCellStatus;
        public static event Action<int,double[]> OnUpdateXYTValue;
        public static Action<bool,bool> ShowContactCamera;
        #region IStation接口
        public event Action<HomeEventArgs> OnHomeStatus;
        public event Action OnStop;
        public event Action<int> OnPause;
        public event Action<string, string, Brush> OnShowMsg;
        public MotionManager Motion { get; set; } = MotionManager.GetInstance();
        public int AutoRunStep { get; set; }
        public int HomeStep { get; set; } = -1;

        public int StationNum { get; set; } = 1;
        public string StationName { get; set; } = "ProbeGantry";
        #endregion
        Delay Delay = new Delay();
        Delay Delay2 = new Delay();

        bool StageArrivedDetectPos = false;//平台在测试位置
        bool ScanBarcodeCom = false;
        bool DetectGantryArrived = false;
        bool DetectGantryMoving = false;
        bool StageZHomeCom = false;
        bool ProbeAutoChgBit = false;
        bool PrbAutoChgCom = false;
        bool PrbAutoChgHomeCom = false;
        bool AoiDetectCom = false;
        bool MuraDetectCom = false;
        bool SquintMuraDetectCom = false;
        bool MultiAngleDetectCom = false;
        #region 状态机运行step

        public int PrbAutoChgStep = -1; //ProbeCard自动更换流程状态机步骤
        int AxisToSafePosStep = -1;//判断轴是否在安全位置流程状态机步骤
        int AlignmentStep = -1;//自动对位流程状态机步骤

        int OneGlassDetectStep = -1;//自动检测流程状态机步骤
        int DetectEndStep = -1;//检测结束流程状态机步骤
        int ProbeGantryToChgPosStep = -1;//
        int OpenShortDetectStep = -1;
        int AoiDetectStep = -1;
        int MuraDetectStep = -1;
        int SquintMuraDetectStep = -1;
        int MultiAngleDetectStep = -1;
        int ContactCamGrabStep = -1;
        #endregion

        double ProbeGantryBasePointX = 0;
        double DetectGantryBasePointX = 0;
        int AlignFailCount = 0;
        /// <summary>
        /// 平台工位事件处理
        /// </summary>
        /// <param name="cmd"></param>
        private void StageTriggerHandle(int cmd)
        {
            switch (cmd)
            {
                case (int)StationCommand.StgArrDetPos:
                    {
                        StageArrivedDetectPos = true;
                        break;
                    }
                case (int)StationCommand.StageZHomeCom:
                    {
                        StageZHomeCom = true;
                        break;
                    }
                case (int)StationCommand.PrbAutoChg:
                    {
                        ProbeAutoChgBit = true;
                        break;
                    }
            }
        }
        /// <summary>
        /// 检测工位事件处理
        /// </summary>
        /// <param name="cmd"></param>
        private void IRGrantryTriggerHandle(int cmd)
        {
            switch (cmd)
            {
                case (int)StationCommand.ScanCodeCom:
                    {
                        ScanBarcodeCom = true;
                        break;
                    }
                case (int)StationCommand.DetectGantryArrived:
                    {
                        DetectGantryArrived = true;
                        break;
                    }
                case (int)StationCommand.DetectGantryMoving:
                    {
                        DetectGantryMoving = true;
                        break;
                    }
                case (int)StationCommand.AoiDetectCom:
                    {
                        AoiDetectCom = true;
                        break;
                    }
                case (int)StationCommand.MuraDetectCom:
                    {
                        MuraDetectCom = true;
                        break;
                    }
                case (int)StationCommand.SquintMuraDetectCom:
                    {
                        SquintMuraDetectCom = true;
                        break;
                    }
                case (int)StationCommand.MultiAngleDetectCom:
                    {
                        MultiAngleDetectCom = true;
                        break;
                    }
            }
        }

        private void CommandHandle(Command cmd, object value)
        {
            switch (cmd)
            {
                case Command.NGCountAlarm:
                    {
                        NGCountAlarmHandle((string)value);
                        break;
                    }
            }
        }
        #region 底层接口
        /// <summary>
        /// 回零
        /// </summary>
        /// <returns></returns>
        public bool Home()
        {
            try
            {
                switch (HomeStep)
                {
                    case 1:
                        {
                            ResetBits();
                            OnShowMsg?.Invoke("AutoRun", "ProbeGantry->ProbeGantry工位开始回零", Brushes.Green);
                            OnShowMsg?.Invoke("AutoRun", "ProbeGantry->等待Probe自动更换Z轴回零完成", Brushes.Green);
                            HomeStep = 10;
                            break;
                        }
                    case 10:
                        {
                            if (PrbAutoChgHomeCom)
                            {
                                PrbAutoChgHomeCom = false;
                                OnShowMsg?.Invoke("AutoRun", "ProbeGantry->Probe自动更换工位回零完成，Uvw平台开始回零", Brushes.Green);
                                Motion.Home(StationEnum.ProbeGantry, AxisName.UvwStage_Y);
                                Motion.Home(StationEnum.ProbeGantry, AxisName.UvwStage_X1);
                                Motion.Home(StationEnum.ProbeGantry, AxisName.UvwStage_X2);
                                HomeStep = 20;
                            }
                            break;
                        }
                    case 20:
                        {
                            if (Motion.IsHomeDone(StationEnum.ProbeGantry, AxisName.UvwStage_Y) && 
                                Motion.IsHomeDone(StationEnum.ProbeGantry, AxisName.UvwStage_X1) &&
                                Motion.IsHomeDone(StationEnum.ProbeGantry, AxisName.UvwStage_X2))
                            {
                                Delay2.Start(60);
                                OnShowMsg?.Invoke("AutoRun", "ProbeGantry->Uvw平台回零完成，等待平台Z轴回零完成", Brushes.Green);
                                HomeStep = 40;
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.ProbeGantry, AxisName.UvwStage_Y) || 
                                Motion.IsMoveTimeOut(StationEnum.ProbeGantry, AxisName.UvwStage_X1) || 
                                Motion.IsMoveTimeOut(StationEnum.ProbeGantry, AxisName.UvwStage_X2))
                            {
                                OnShowMsg?.Invoke("AutoRun", "ProbeGantry->Uvw平台回零超时", Brushes.Red);
                                HomeStep = 1000;
                            }
                            break;
                        }
                    case 40:
                        {
                            if (StageZHomeCom)
                            {
                                StageZHomeCom = false;
                                Motion.Home(StationEnum.ProbeGantry, AxisName.ProbeGantry_Y1);
                                OnShowMsg?.Invoke("AutoRun", "ProbeGantry->平台Z轴回零完成，Probe龙门轴开始回零", Brushes.Green);
                                HomeStep = 50;
                            }
                            else if (Delay2.TimeUp())
                            {
                                OnShowMsg?.Invoke("AutoRun", "ProbeGantry->等待平台Z轴回零完成超时", Brushes.Red);
                                HomeStep = 1000;
                            }
                            break;
                        }
                    case 50:
                        {
                            if (Motion.IsHomeDone(StationEnum.ProbeGantry, AxisName.ProbeGantry_Y1))
                            {
                                var info = AxisInfoHelper.GetAxisInfoByName("ProbeGantry_Y2");
                                info.IsHomeOK = true;
                                Motion.Home(StationEnum.ProbeGantry, AxisName.ProbeGantryAlig_X1);
                                Motion.Home(StationEnum.ProbeGantry, AxisName.ProbeGantryAlig_X2);
                                OnShowMsg?.Invoke("AutoRun", "ProbeGantry->龙门轴回零完成，对位相机X轴开始回零", Brushes.Green);
                                HomeStep = 60;
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.ProbeGantry, AxisName.ProbeGantry_Y1))
                            {
                                OnShowMsg?.Invoke("AutoRun", "ProbeGantry->Probe龙门轴回零超时", Brushes.Red);
                                HomeStep = 1000;
                            }
                            break;
                        }
                    case 60:
                        {
                            if (Motion.IsHomeDone(StationEnum.ProbeGantry, AxisName.ProbeGantryAlig_X1) && Motion.IsHomeDone(StationEnum.ProbeGantry, AxisName.ProbeGantryAlig_X2))
                            {
                                UvwMoveAbsSync(ProbeGantryPosName.DetectPos, 0);
                                Motion.Cylinder(CylinderName.PcbFixCyl,true);
                                Motion.MoveAbs(StationEnum.ProbeGantry, AxisName.ProbeGantryAlig_X1, ProbeGantryPosName.SafePos);
                                Motion.MoveAbs(StationEnum.ProbeGantry, AxisName.ProbeGantryAlig_X2, ProbeGantryPosName.SafePos);
                                OnShowMsg?.Invoke("AutoRun", "ProbeGantry->对位相机X轴回零完成，Uvw平台去检测位置，Pcb固定气缸锁紧Pcb", Brushes.Green);
                                HomeStep = 70;
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.ProbeGantry, AxisName.ProbeGantryAlig_X1) || Motion.IsMoveTimeOut(StationEnum.ProbeGantry, AxisName.ProbeGantryAlig_X2))
                            {
                                OnShowMsg?.Invoke("AutoRun", "ProbeGantry->对位相机X轴回零超时", Brushes.Red);
                                HomeStep = 1000;
                            }
                            break;
                        }
                    case 70:
                        {
                            if (IsUvwMoveDone == 0 && Motion.IsArrive(StationEnum.ProbeGantry, AxisName.ProbeGantryAlig_X1))
                            {
                                IsUvwMoveDone = -1;
                                OnShowMsg?.Invoke("AutoRun", "ProbeGantry->Probe龙门工位回零完成", Brushes.Green);
                                OnHomeStatus?.Invoke(new HomeEventArgs { StationName = StationName, StationNum = StationNum, HomeResult = true });
                                HomeStep = 0;
                            }
                            else if (IsUvwMoveDone == 1)
                            {
                                OnShowMsg?.Invoke("AutoRun", "ProbeGantry->Uvw平台去检测位置失败", Brushes.Red);
                                HomeStep = 60;
                            }
                            break;
                        }
                    case 1000:
                        {
                            OnStop?.Invoke();
                            OnHomeStatus?.Invoke(new HomeEventArgs { StationName = StationName, StationNum = StationNum, HomeResult = false });
                            HomeStep = -1;
                            AlarmManager.CreateAlarm((int)MotionAlarmCode.ProbeGantryStaHomeFail);
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                OnHomeStatus?.Invoke(new HomeEventArgs { StationName = StationName, StationNum = StationNum, HomeResult = false });
                AlarmManager.CreateAlarm((int)MotionAlarmCode.ProbeGantryStationException);
                LogManager.GetLogger("AutoRun").Fatal(ex.ToString());
            }
            return true;
        }
        /// <summary>
        /// 自动流程
        /// </summary>
        /// <returns></returns>
        public bool AutoRun()
        {
            try
            {
                switch (AutoRunStep)
                {
                    case 1:
                        {
                            if (AxisToSafePosStep == 0)
                            {
                                OnTriggerSignal?.Invoke((int)StationCommand.ProbeGantryArrSafePos);
                                AutoRunStep = 5;
                                OnShowMsg?.Invoke("AutoRun", "ProbeGantry->各轴到达初始位置，等待接收DualStage工位指令", Brushes.Green);
                            }
                            else
                            {
                                AxisToSafePos();
                            }
                            break;
                        }
                    case 5:
                        {
                            if (ProbeAutoChgBit)
                            {
                                OnShowMsg?.Invoke("AutoRun", "ProbeGantry->开始自动更换Probe", Brushes.Green);
                                ProbeAutoChgBit = false;
                                PrbAutoChgStep = 1;
                                AxisToSafePosStep = 1;
                                AutoRunStep = 10;
                            }
                            else if (StageArrivedDetectPos)
                            {
                                StageArrivedDetectPos = false;
                                Global.GlassDic = new Dictionary<int, GlassModel>();
                                Global.CurDetectInfo = new CurrentDetectInfo {GlassNum=0 };
                                OnUpdateCellStatus?.Invoke(true);
                                OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->平台到达检测位置", Brushes.Green);
                                AutoRunStep = 7;
                            }
                            break;
                        }
                    case 7:
                        {
                            AoiPC.Instance.RecipeSync(RecipeManager.CurRecipe.RecipeID, 0, RecipeChangeType.Shift);
                            AoiPC.Instance.RecipeSync(RecipeManager.CurRecipe.RecipeID, 1, RecipeChangeType.Shift);
                            TesterPC.Instance.RecipeSync(RecipeManager.CurRecipe.PPID, RecipeChangeType.Shift);
                            OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->平台到达检测位置，同步机种:{RecipeManager.CurRecipe.RecipeID},PPID:{RecipeManager.CurRecipe.PPID}给从机", Brushes.Green);
                            if (Global.IsAutoPosOnly && Global.AutoPosInfo != null)
                            {
                                Global.CurDetectInfo.GlassNum = Global.AutoPosInfo.GlassNum;
                            }
                            Delay.Start(5);
                            AutoRunStep = 15;
                            break;
                        }
                    case 10:
                        {
                            if (PrbAutoChgStep == 0)
                            {
                                AutoRunStep = 5;
                            }
                            else
                            {
                                PrbAutoChgFlow();
                            }
                            break;
                        }
                    case 15:
                        {
                            if (TesterPC.RecipeSyncResult=="OK"&& AoiPC.PC1RecipeSyncResult == "OK"&& AoiPC.PC2RecipeSyncResult == "OK")
                            {
                                OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->从机同步机种完成", Brushes.Green);
                                AutoRunStep = 20;
                            }
                            else if (TesterPC.RecipeSyncResult == "NG")
                            {
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.TesterPCShiftRecipeFail);
                                AutoRunStep = 7;
                            }
                            else if (AoiPC.PC1RecipeSyncResult == "NG")
                            {
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.AoiPC1ShiftRecipeFail);
                                AutoRunStep = 7;
                            }
                            else if (AoiPC.PC2RecipeSyncResult == "NG")
                            {
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.AoiPC2ShiftRecipeFail);
                                AutoRunStep = 7;
                            }
                            else if (Delay.TimeUp())
                            {
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.ClientShiftRecipeTimeout);
                                AutoRunStep = 7;
                            }
                            break;
                        }
                    case 20:
                        {
                            if (Global.CurDetectInfo.GlassNum < Global.GlassCount)
                            {
                                if (Param.Detect.GlassDetectShield[Global.CurDetectInfo.GlassNum])
                                {
                                    OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->第{Global.CurDetectInfo.GlassNum + 1}片Glass检测屏蔽", Brushes.Blue);
                                    Global.CurDetectInfo.GlassNum++;
                                }
                                else
                                {
                                    //对位或者扫码失败，自动检测下一张Glass
                                    if (AlignmentStep == -1000)
                                    {
                                        AlignmentStep = 1;
                                        Global.CurDetectInfo.GlassNum++;
                                    }
                                    else
                                    {
                                        if (Global.IsAutoPosOnly)
                                        {
                                            AutoRunStep = 25;
                                            AlignmentStep = 1;
                                            OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->第{Global.AutoPosInfo.GlassNum + 1}片Glass开始对位", Brushes.Green);
                                        }
                                        else
                                        {
                                            var model = new JobDataDal().GetLatest(Global.CurDetectInfo.GlassNum);
                                            GlassDataA jobData = new GlassDataA();
                                            if (model != null)
                                            {
                                                jobData.FromString(model.JobData);
                                            }
                                            if (StationManager.Instance.IsSimulator()||Param.Cim.CimShield)
                                            {
                                                jobData.Dummy_Type = 0;
                                            }
                                            if (jobData != null && jobData.Dummy_Type != 0)
                                            {
                                                OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->第{Global.CurDetectInfo.GlassNum + 1}片Glass是Dummy，不检测", Brushes.Red);
                                                Global.CurDetectInfo.GlassNum++;
                                            }
                                            else
                                            {
                                                AutoRunStep = 25;
                                                AlignmentStep = 1;
                                                OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->第{Global.CurDetectInfo.GlassNum + 1}片Glass开始对位+扫码", Brushes.Green);
                                                SequenceStepManager.StartWatch(SequenceName.ProbeTotal, Global.CurDetectInfo.GlassNum);
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                Global.CurDetectInfo.GlassNum = 0;
                                OnTriggerSignal?.Invoke((int)StationCommand.DetectEnd);
                                DetectEndStep = 1;
                                AutoRunStep = 80;
                                OnShowMsg?.Invoke("AutoRun", "ProbeGantry->检测结束，各轴去初始位置", Brushes.Green);
                            }
                            break;
                        }
                    case 25:
                        {
                            if (AlignmentStep == 0)
                            {
                               Global.CurDetectInfo.ColumnNum = Param.Panel.CellCountX-1;//从倒数第一列开始检测
                                if (Global.IsAutoPosOnly && Global.AutoPosInfo != null)
                                {
                                   Global.CurDetectInfo.ColumnNum = Global.AutoPosInfo.ColumnNum;
                                }
                                OneGlassDetectStep = 1;
                                AutoRunStep = 50;
                                Motion.SetDigitalOutput(OutputName.DetectGantryIonizer, true);
                                //if (Global.CurDetectInfo.GlassNum==0)
                                //{
                                //    Motion.SetDigitalOutput(OutputName.Stage1SensorPower, false);
                                //}
                                //else
                                //{
                                //    Motion.SetDigitalOutput(OutputName.Stage2SensorPower, false);
                                //}
                   
                                OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->对位和扫码完成，开始检测", Brushes.Green);
                            }
                            else if (AlignmentStep == -1000)
                            {
                                AutoRunStep = 20;
                            }
                            else
                            {
                                AlignmentAndScancode();
                            }
                            break;
                        }
                    case 50:
                        {
                            if (OneGlassDetectStep == 0)
                            {
                                OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->Glass{Global.CurDetectInfo.GlassNum + 1}检测完成，再次抬起压头", Brushes.Green);
                                StageStopContactSync(false,Global.CurDetectInfo.GlassNum);
                                Motion.SetDigitalOutput(OutputName.DetectGantryIonizer, false);
                                //if (Global.CurDetectInfo.GlassNum == 0)
                                //{
                                //    Motion.SetDigitalOutput(OutputName.Stage1SensorPower, true);
                                //}
                                //else
                                //{
                                //    Motion.SetDigitalOutput(OutputName.Stage2SensorPower, true);
                                //}
                                DBManager.UpdateTotalInfoResult(Global.CurDetectInfo);
                                AutoRunStep = 60;
                            }
                            else
                            {
                                OneGlassDetect();
                            }
                            break;
                        }
                    case 60:
                        {
                            if (StopContactResult == 0 &&
                                Motion.IsArrive(StationEnum.DualStage, AxisName.Stage1_ContactZ, StagePosName.SafePos) &&
                               Motion.IsArrive(StationEnum.DualStage, AxisName.Stage2_ContactZ, StagePosName.SafePos))
                            {
                                OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->压头抬起完成", Brushes.Green);
                                OnTriggerSignal?.Invoke((int)StationCommand.OneGlassDetectCom);
                              
                                GlassDataA jobData1 = new GlassDataA();

                                    Task.Factory.StartNew(() =>
                                    {
                                        TesterPC.Instance.DetectEnd();
                                        string temp1 = new JobDataDal().Get(Global.CurDetectInfo.GlassID).JobData;
                                        jobData1.FromString(temp1);
                                        CimManager.Instance.ProcessDataReport(jobData1, Global.CurDetectInfo.GlassNum);
                                        //CimManager.Instance.PanelDataUpdateReport(Global.CurDetectInfo.GlassID, Global.CurDetectInfo.GlassNum);
                                        //CimManager.Instance.JobJudgeResultReport(jobData1);
                                        //CimManager.Instance.JobDataChangeReport(jobData1);
                                        DFSHelper.DfsUpload(Global.CurDetectInfo.GlassID);
                                    });

                                Global.CurDetectInfo.GlassNum++;
                                AutoRunStep = 20;
                            }
                            else if (StopContactResult == 1)
                            {
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.StageContactTimeout);
                                AutoRunStep = 50;
                            }
                            break;
                        }

                    case 80:
                        {
                            if (DetectEndStep == 0)
                            {
                                AutoRunStep = 1;
                                OnShowMsg?.Invoke("AutoRun", "ProbeGantry->各轴到达初始位置", Brushes.Green);
                            }
                            else
                            {
                                DetectEnd();
                            }
                            break;
                        }

                }

            }
            catch (Exception ex)
            {
                AlarmManager.CreateAlarm((int)MotionAlarmCode.ProbeGantryStationException);
                LogManager.GetLogger("AutoRun").Fatal(ex.ToString());
            }
            return true;
        }
        public bool Init()
        {
            return true;
        }
        public bool ResetBits()
        {
            AxisToSafePosStep = 1;
            AlignmentStep = -1;//自动对位流程状态机步骤
            OneGlassDetectStep = -1;//自动检测流程状态机步骤
            DetectEndStep = -1;//检测结束流程状态机步骤
            ProbeGantryToChgPosStep = -1;//
            OpenShortDetectStep = -1;
             AoiDetectStep = -1;
             MuraDetectStep = -1;
             MultiAngleDetectStep = -1;
             ContactCamGrabStep = -1;
            SquintMuraDetectStep = -1;

            StageArrivedDetectPos = false;//平台在测试位置
            ScanBarcodeCom = false;
            StageZHomeCom = false;
            ProbeAutoChgBit = false;
            AoiDetectCom = false;
            MuraDetectCom = false;
            SquintMuraDetectCom = false;
            MultiAngleDetectCom = false;
            PrbAutoChgHomeCom = false;
            return true;
        }
        public bool Stop()
        {
            return true;
        }
        public bool Pause()
        {
            return true;
        }
        public void OnButtonTrigger(IOInfo io, bool sts)
        {
        }

        public bool Unint()
        {
            return true;
        }

        public bool RegisterEvent()
        {
            ProcessCmd.OnCommand += CommandHandle;
            DualStage.OnTriggerSignal += StageTriggerHandle;
            DetectGantry.OnTriggerSignal += IRGrantryTriggerHandle;
            PrbAutoChg.OnTriggerSignal += PrbAutoChg_OnTriggerSignal;
            AlignSignalProvider.OnStageAlign += AlignSignalProvider_OnStageAlign;
            return true;
        }

        #endregion
        public void PrbAutoChgFlow()
        {
            switch (PrbAutoChgStep)
            {
                case 1:
                    {
                        if (AxisToSafePosStep == 0)
                        {
                            ProbeGantryToChgPosStep = 1;
                            PrbAutoChgStep = 10;
                        }
                        else
                        {
                            AxisToSafePos();
                        }
                        break;
                    }
                case 10:
                    {
                        if (ProbeGantryToChgPosStep == 0)
                        {
                            PrbAutoChgStep = 0;
                        }
                        else
                        {
                            ProbeGantryToChgPos();
                        }
                        break;
                    }
            }
        }

        private void ProbeGantryToChgPos()
        {
            switch (ProbeGantryToChgPosStep)
            {
                case 1:
                    {
                        Delay.Start(Param.Motion.UvwTimeout);
                        UvwMoveAbsSync(ProbeGantryPosName.AutoChgPos, 0,false);
                        OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->Uvw去自动更换位置", Brushes.Green);
                        ProbeGantryToChgPosStep = 10;
                        break;
                    }
                case 10:
                    {
                        if (IsUvwMoveDone == 0)
                        {
                            IsUvwMoveDone = -1;
                            GantryMove(ProbeGantryPosName.AutoChgPos);
                            OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->Uvw到达自动更换位置，Probe龙门去自动更换位置", Brushes.Green);
                            ProbeGantryToChgPosStep = 20;
                        }
                        else if (Delay.TimeUp())
                        {
                            AlarmManager.CreateAlarm((int)MotionAlarmCode.UvwTimeout);
                            ProbeGantryToChgPosStep = 1;
                        }
                        break;
                    }
                case 20:
                    {
                        if (Motion.IsArrive(StationEnum.ProbeGantry, AxisName.ProbeGantry_Y1, ProbeGantryPosName.AutoChgPos))
                        {
                            OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->Probe龙门到达自动更换位置", Brushes.Green);
                            OnTriggerSignal?.Invoke((int)StationCommand.ProbeGantryArrChgPos);
                            ProbeGantryToChgPosStep = 30;
                        }
                        else if (Motion.IsMoveTimeOut(StationEnum.ProbeGantry, AxisName.ProbeGantry_Y1))
                        {
                            OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->Probe龙门移动到自动更换位置超时", Brushes.Red);
                            AlarmManager.CreateAlarm((int)MotionAlarmCode.ProbeGantry_Y1MoveTimeout);
                            ProbeGantryToChgPosStep = 10;
                        }
                        break;
                    }
                case 30:
                    {
                        if (PrbAutoChgCom)
                        {
                            OnShowMsg?.Invoke("AutoRun", $"{Param.Control.PrbChgMode } Probe流程执行完成", Brushes.Blue);
                            PrbAutoChgCom = false;
                            if (Param.Control.PrbChgMode != PrbChgMode.Unload)
                            {
                                bool ret = RecipeManager.ShiftRecipe(Global.ReceivedRecipeName, true);
                                if (ret == false)
                                {
                                    OnStop?.Invoke();
                                    OnShowMsg?.Invoke("AutoRun", $"切换机种：{Global.ReceivedRecipeName}失败，请检查机种参数", Brushes.Red);
                                    ProbeGantryToChgPosStep = 0;
                                }
                                else
                                {
                                    OnShowMsg?.Invoke("AutoRun", $"切换机种：{Global.ReceivedRecipeName}完成", Brushes.Blue);
                                }
                            }
                        
                            ProbeGantryToChgPosStep = 35;
                        }
                        break;
                    }
                case 35:
                    {
                        OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->Probe更换完毕，Probe龙门回初始位置，Uvw平台移动到压接位置", Brushes.Green);
                        Delay.Start(Param.Motion.UvwTimeout);
                        GantryMove(ProbeGantryPosName.SafePos);
                        UvwMoveAbsSync(ProbeGantryPosName.DetectPos, 0, false);
                        ProbeGantryToChgPosStep = 40;
                        break;
                    }
                case 40:
                    {
                        if (Motion.IsArrive(StationEnum.ProbeGantry, AxisName.ProbeGantry_Y1, ProbeGantryPosName.SafePos))
                        {
                            if (IsUvwMoveDone == 0)
                            {
                                OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->Probe龙门到达初始位置，Uvw平台到达压接位置", Brushes.Green);
                                ProbeGantryToChgPosStep = 0;
                            }
                            else if (Delay.TimeUp())
                            {
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.UvwTimeout);
                                PrbAutoChgCom = true;
                                ProbeGantryToChgPosStep = 35;
                            }
                        }
                        else if (Motion.IsMoveTimeOut(StationEnum.ProbeGantry, AxisName.ProbeGantry_Y1))
                        {
                            OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->Probe龙门移动到初始位置超时", Brushes.Red);
                            AlarmManager.CreateAlarm((int)MotionAlarmCode.ProbeGantry_Y1MoveTimeout);
                            PrbAutoChgCom = true;
                            ProbeGantryToChgPosStep = 35;
                        }
                        break;
                    }
            }
        }
        /// <summary>
        /// 判断各轴是否在初始位置
        /// </summary>
        public void AxisToSafePos()
        {
            switch (AxisToSafePosStep)
            {
                case 1:
                    {
                        StageStopContactSync(true);
                        AxisToSafePosStep = 10;
                        break;
                    }
                case 10:
                    {
                        if (StopContactResult == 0&& 
                            Motion.IsArrive(StationEnum.DualStage, AxisName.Stage1_ContactZ, StagePosName.SafePos) && 
                            Motion.IsArrive(StationEnum.DualStage, AxisName.Stage2_ContactZ, StagePosName.SafePos))
                        {
                            AxisToSafePosStep = 20;
                        }
                        else if (StopContactResult == 1)
                        {
                            AlarmManager.CreateAlarm((int)MotionAlarmCode.StageStopContactTimeout);
                            AxisToSafePosStep = 1;
                        }
                        break;
                    }
                case 20:
                    {
                            if (Motion.IsArrive(StationEnum.ProbeGantry, AxisName.ProbeGantry_Y1, ProbeGantryPosName.SafePos))
                            {
                                if (Motion.IsArrive(StationEnum.ProbeGantry, AxisName.ProbeGantryAlig_X1, ProbeGantryPosName.SafePos) && Motion.IsArrive(StationEnum.ProbeGantry, AxisName.ProbeGantryAlig_X2, ProbeGantryPosName.SafePos))
                                {
                                    OnShowMsg?.Invoke("AutoRun", "ProbeGantry->Probe龙门工位各轴都在初始位置", Brushes.Green);
                                    AxisToSafePosStep = 0;
                                }
                                else
                                {
                                    Motion.MoveAbs(StationEnum.ProbeGantry, AxisName.ProbeGantryAlig_X2, ProbeGantryPosName.SafePos);
                                    Motion.MoveAbs(StationEnum.ProbeGantry, AxisName.ProbeGantryAlig_X1, ProbeGantryPosName.SafePos);
                                    AxisToSafePosStep = 25;
                                    OnShowMsg?.Invoke("AutoRun", "ProbeGantry->对位相机X轴不在初始位置，移动到初始位置", Brushes.Green);
                                }
                            }
                            else
                            {
                                if (Motion.IsArrive(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Z, PrbAutoChgPosName.SafePos))
                                {
                                    GantryMove(ProbeGantryPosName.SafePos);
                                    AxisToSafePosStep = 30;
                                    OnShowMsg?.Invoke("AutoRun", "ProbeGantry->龙门轴不在初始位置，移动到初始位置", Brushes.Green);
                                }
                             
                            }
                 
                        break;
                    }
                case 25:
                    {
                        if (Motion.IsArrive(StationEnum.ProbeGantry, AxisName.ProbeGantryAlig_X1, ProbeGantryPosName.SafePos))
                        {
                            if (Motion.IsArrive(StationEnum.ProbeGantry, AxisName.ProbeGantryAlig_X2, ProbeGantryPosName.SafePos))
                            {
                                AxisToSafePosStep = 20;
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.ProbeGantry, AxisName.ProbeGantryAlig_X2))
                            {
                                OnShowMsg?.Invoke("AutoRun", "ProbeGantry->对位相机X2轴回初始化位置超时", Brushes.Green);
                                AxisToSafePosStep = 2000;
                            }
                        }
                        else if (Motion.IsMoveTimeOut(StationEnum.ProbeGantry, AxisName.ProbeGantryAlig_X1))
                        {
                            OnShowMsg?.Invoke("AutoRun", "ProbeGantry->对位相机X1轴回初始化位置超时", Brushes.Green);
                            AxisToSafePosStep = 2000;
                        }

                        break;
                    }
                case 30:
                    {
                        if (Motion.IsArrive(StationEnum.ProbeGantry, AxisName.ProbeGantry_Y1, ProbeGantryPosName.SafePos))
                        {
                            AxisToSafePosStep = 20;
                        }
                        else if (Motion.IsMoveTimeOut(StationEnum.ProbeGantry, AxisName.ProbeGantry_Y1))
                        {
                            OnShowMsg?.Invoke("AutoRun", "ProbeGantry->Probe龙门轴回初始化位置超时", Brushes.Green);
                            AxisToSafePosStep = 2000;
                        }
                        break;
                    }
             
                case 2000:
                    {
                        AxisToSafePosStep = Global.ErrorStep;
                        OnStop?.Invoke();
                        break;
                    }
            }
        }
 
        /// <summary>
        /// 对位+扫码流程
        /// </summary>
        public void AlignmentAndScancode()
        {
            switch (AlignmentStep)
            {
                case 1://龙门轴移动到第一列位置，x轴移动到拍照位置
                    {
                        SequenceStepManager.StartWatch(SequenceName.ProbeToAlignPos, Global.CurDetectInfo.GlassNum);
                        OnTriggerSignal?.Invoke((int)StationCommand.ScanCode);
                        UvwMoveAbsSync(ProbeGantryPosName.DetectPos, Global.CurDetectInfo.GlassNum);
                        OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->请求检测工位扫码", Brushes.Green);
                        if (IsDetectModuleZSafe()==false)
                        {
                            AlarmManager.CreateAlarm((int)MotionAlarmCode.DetectModuleZNotSafe);
                            AlignmentStep = 0;
                        }
                        AlignmentStep = 5;
                        break;
                    }
                case 5:
                    {
                        if (Global.CurDetectInfo.GlassNum == 0)
                        {
                            GantryMove(Global.CurDetectInfo.GlassNum == 0 ? ProbeGantryPosName.Stg1ProbeGantryAlignPos : ProbeGantryPosName.Stg2ProbeGantryAlignPos);
                            Motion.MoveAbs(StationEnum.ProbeGantry, AxisName.ProbeGantryAlig_X1, Global.CurDetectInfo.GlassNum == 0 ? ProbeGantryPosName.Stg1AlignCamGrabPos_X1 : ProbeGantryPosName.Stg2AlignCamGrabPos_X1);
                            Motion.MoveAbs(StationEnum.ProbeGantry, AxisName.ProbeGantryAlig_X2, Global.CurDetectInfo.GlassNum == 0 ? ProbeGantryPosName.Stg1AlignCamGrabPos_X2 : ProbeGantryPosName.Stg2AlignCamGrabPos_X2);
                            OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->Probe龙门和对位相机去拍照位置", Brushes.Green);
                            AlignmentStep = 20;
                        }
                        else
                        {
                            if (DetectGantryMoving)
                            {
                                Thread.Sleep(300);
                                DetectGantryMoving = false;
                                GantryMove(Global.CurDetectInfo.GlassNum == 0 ? ProbeGantryPosName.Stg1ProbeGantryAlignPos : ProbeGantryPosName.Stg2ProbeGantryAlignPos);
                                OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->Probe龙门去拍照位置", Brushes.Green);
                                AlignmentStep = 10;
                            }
                        }
                        break;
                    }
                case 10://z轴移动到拍照位置
                    {
                        if (DetectGantryArrived)
                        {
                            if (Motion.IsArrive(StationEnum.ProbeGantry, AxisName.ProbeGantry_Y1, Global.CurDetectInfo.GlassNum == 0 ? ProbeGantryPosName.Stg1ProbeGantryAlignPos : ProbeGantryPosName.Stg2ProbeGantryAlignPos))
                            {
                                DetectGantryArrived = false;
                                OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->对位相机去拍照位置", Brushes.Green);
                                Motion.MoveAbs(StationEnum.ProbeGantry, AxisName.ProbeGantryAlig_X1, Global.CurDetectInfo.GlassNum == 0 ? ProbeGantryPosName.Stg1AlignCamGrabPos_X1 : ProbeGantryPosName.Stg2AlignCamGrabPos_X1);
                                Motion.MoveAbs(StationEnum.ProbeGantry, AxisName.ProbeGantryAlig_X2, Global.CurDetectInfo.GlassNum == 0 ? ProbeGantryPosName.Stg1AlignCamGrabPos_X2 : ProbeGantryPosName.Stg2AlignCamGrabPos_X2);
                                AlignmentStep = 20;
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.ProbeGantry, AxisName.ProbeGantry_Y1))
                            {
                                OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->Probe龙门去拍照位置超时", Brushes.Red);
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.ProbeGantry_Y1MoveTimeout);
                                AlignmentStep = 5;
                            }
                        }
                         
                        break;
                    }
                case 20:
                    {
                        if (Motion.IsArrive(StationEnum.ProbeGantry, AxisName.ProbeGantry_Y1, Global.CurDetectInfo.GlassNum == 0 ? ProbeGantryPosName.Stg1ProbeGantryAlignPos : ProbeGantryPosName.Stg2ProbeGantryAlignPos) &&
                            Motion.IsArrive(StationEnum.ProbeGantry, AxisName.ProbeGantryAlig_X1, Global.CurDetectInfo.GlassNum == 0 ? ProbeGantryPosName.Stg1AlignCamGrabPos_X1 : ProbeGantryPosName.Stg2AlignCamGrabPos_X1) &&
                            Motion.IsArrive(StationEnum.ProbeGantry, AxisName.ProbeGantryAlig_X2, Global.CurDetectInfo.GlassNum == 0 ? ProbeGantryPosName.Stg1AlignCamGrabPos_X2 : ProbeGantryPosName.Stg2AlignCamGrabPos_X2)&&
                            IsUvwMoveDone == 0)
                        {
                            OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->Probe龙门和对位相机到达拍照位置，平台{Global.CurDetectInfo.GlassNum+1}压接Z轴移动到对位位置", Brushes.Green);
                            Motion.MoveAbs(StationEnum.DualStage, Global.CurDetectInfo.GlassNum == 0 ? AxisName.Stage1_ContactZ : AxisName.Stage2_ContactZ, StagePosName.AlignPos);
                            AlignmentStep = 25;
                            PowerControl(true);//打开对位相机光源
                        }
                        else if (Motion.IsMoveTimeOut(StationEnum.ProbeGantry, AxisName.ProbeGantry_Y1) || Motion.IsMoveTimeOut(StationEnum.ProbeGantry, AxisName.ProbeGantryAlig_X1) || Motion.IsMoveTimeOut(StationEnum.ProbeGantry, AxisName.ProbeGantryAlig_X2))
                        {
                            OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->对位相机去拍照位置超时", Brushes.Red);
                            AlarmManager.CreateAlarm((int)MotionAlarmCode.ProbeGantry_Y1MoveTimeout);
                            AlignmentStep = 10;
                        }
                        break;
                    }
                case 25:
                    {
                        if (Motion.IsArrive(StationEnum.DualStage, Global.CurDetectInfo.GlassNum == 0 ? AxisName.Stage1_ContactZ : AxisName.Stage2_ContactZ, StagePosName.AlignPos))
                        {
                            OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->平台{Global.CurDetectInfo.GlassNum + 1}压接Z轴到达对位位置，开始对位", Brushes.Green);
                            IsUvwMoveDone = -1;
                            AlignFailCount = 0;
                            AlignmentStep = 30;
                        }
                        else if (Motion.IsMoveTimeOut(StationEnum.DualStage, Global.CurDetectInfo.GlassNum == 0 ? AxisName.Stage1_ContactZ : AxisName.Stage2_ContactZ))
                        {
                            OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->平台{Global.CurDetectInfo.GlassNum+1}压接Z轴移动到对位位置超时", Brushes.Red);
                            AlarmManager.CreateAlarm((int)(Global.CurDetectInfo.GlassNum == 0 ? MotionAlarmCode.Stage1_ContactZMoveTimeout: MotionAlarmCode.Stage2_ContactZMoveTimeout));
                            AlignmentStep = 20;
                        }
                        break;
                    }
                case 30:
                    {
                        if (AlignFailCount < Param.Align.ReAlignCount)
                        {
                            Delay.Start(Param.Comm.AlignCamGrabTimeout);
                            if (Param.Control.RunMode == RunMode.NoDetect)
                            {
                                OnShowMsg?.Invoke("AutoRun", "ProbeGantry->空跑模式，不对位", Brushes.Blue);
                                Global.SignalProvider.AutoPosResult = true;
                                AlignmentStep = 40;
                            }
                            else
                            {
                                OnUpdateXYTValue?.Invoke(Global.CurDetectInfo.GlassNum, new double[] { 0, 0, 0 });
                                Global.SignalProvider.StartAutoPos(Global.CurDetectInfo.GlassNum, Global.CurDetectInfo.GlassID);
                                AlignmentStep = 40;
                                OnShowMsg?.Invoke("AutoRun", "ProbeGantry->开始对位", Brushes.Green);
                                SequenceStepManager.StopWatch(SequenceName.ProbeToAlignPos, Global.CurDetectInfo.GlassNum);
                                SequenceStepManager.StartWatch(SequenceName.GlassAlign, Global.CurDetectInfo.GlassNum);
                            }
                        }
                        else
                        {
                            AlignFailCount = 0;
                            OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->对位连续失败{Param.Align.ReAlignCount}次，跳过当前Glass", Brushes.Red);
                            AlignmentStep = -1000;
                        }
                        break;
                    }
                case 40://reviewPC拍照成功，返回xyt偏移值
                    {
                        if (Global.SignalProvider.AutoPosResult || Param.Align.IsAlignShield)
                        {
                            Global.SignalProvider.AutoPosResult = false;
                            AlignFailCount = 0;
                            OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->对位完成，对位{(Param.Align.IsAlignShield ? "屏蔽" : "未屏蔽")}", Brushes.Green);
                            ContactCamXPos = BasePointHelper.GetContactHeadPos(Global.CurDetectInfo.GlassNum, Param.Motion.ContactCamCheckCellNum);
                            Motion.MoveAbs(StationEnum.ProbeGantry, AxisName.ProbeGantryAlig_X1, ContactCamXPos);
                            OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->对位相机1X轴移动到监控第{Param.Motion.ContactCamCheckCellNum + 1}个压头位置，对位相机2X轴和平台压接Z轴移动到初始位置", Brushes.Green);
                            Motion.MoveAbs(StationEnum.ProbeGantry, AxisName.ProbeGantryAlig_X2, ProbeGantryPosName.SafePos);
                            Motion.MoveAbs(StationEnum.DualStage, Global.CurDetectInfo.GlassNum == 0 ? AxisName.Stage1_ContactZ : AxisName.Stage2_ContactZ, StagePosName.SafePos);
                            SequenceStepManager.StopWatch(SequenceName.GlassAlign, Global.CurDetectInfo.GlassNum);
                            AlignmentStep = 50;
                            PowerControl(false);//关闭对位相机光源
                            PowerControl(true,false);//打开Contact相机光源
                        }
                        else if (Delay.TimeUp())
                        {
                            AlignFailCount++;
                            OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->对位失败{AlignFailCount}次", Brushes.Red);
                            AlarmManager.CreateAlarm((int)MotionAlarmCode.AlignmentTimeout);
                            AlignmentStep = 30;
                        }
                        break;
                    }
                case 50:
                    {
                        if (Motion.IsArrive(StationEnum.ProbeGantry, AxisName.ProbeGantryAlig_X1, ContactCamXPos) &&
                            Motion.IsArrive(StationEnum.ProbeGantry, AxisName.ProbeGantryAlig_X2, ProbeGantryPosName.SafePos)&& 
                            Motion.IsArrive(StationEnum.DualStage, Global.CurDetectInfo.GlassNum == 0 ? AxisName.Stage1_ContactZ : AxisName.Stage2_ContactZ, StagePosName.SafePos) && ScanBarcodeCom)
                        {
                            ScanBarcodeCom = false;
                            OnShowMsg?.Invoke("AutoRun", "ProbeGantry->对位相机到达初始位，扫码完成", Brushes.Green);
                            AlignmentStep = 0;
                        }
                        else if (Motion.IsMoveTimeOut(StationEnum.ProbeGantry, AxisName.ProbeGantryAlig_X1))
                        {
                            OnShowMsg?.Invoke("AutoRun", "ProbeGantry->对位相机X1轴回初始位超时", Brushes.Red);
                            AlarmManager.CreateAlarm((int)MotionAlarmCode.ProbeGantryAlig_X1MoveTimeout);
                            Global.SignalProvider.AutoPosResult = true;
                            AlignmentStep = 40;
                        }
                        else if (Motion.IsMoveTimeOut(StationEnum.ProbeGantry, AxisName.ProbeGantryAlig_X2))
                        {
                            OnShowMsg?.Invoke("AutoRun", "ProbeGantry->对位相机X2轴回初始位超时", Brushes.Red);
                            AlarmManager.CreateAlarm((int)MotionAlarmCode.ProbeGantryAlig_X2MoveTimeout);
                            Global.SignalProvider.AutoPosResult = true;
                            AlignmentStep = 40;
                        }
                        else if (Motion.IsMoveTimeOut(StationEnum.DualStage, Global.CurDetectInfo.GlassNum == 0 ? AxisName.Stage1_ContactZ : AxisName.Stage2_ContactZ))
                        {
                            OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->平台{Global.CurDetectInfo.GlassNum+1}压接Z轴移动到初始位置超时", Brushes.Red);
                            AlarmManager.CreateAlarm((int)(Global.CurDetectInfo.GlassNum == 0 ? MotionAlarmCode.Stage1_ContactZMoveTimeout : MotionAlarmCode.Stage2_ContactZMoveTimeout));
                            Global.SignalProvider.AutoPosResult = true;
                            AlignmentStep = 40;
                        }
                        break;
                    }
            }
        }

        /// <summary>
        /// 检测流程
        /// </summary>
        public void OneGlassDetect()
        {
            try
            {
                switch (OneGlassDetectStep)
                {
                    case 1:
                        {
                            if (Global.CurDetectInfo.ColumnNum >= 0)//从第5列开始检测，列号索引从0开始，第5列的列号是4
                            {                             
                                InitColumnInfo(Global.CurDetectInfo.ColumnNum);
                                Global.CurDetectInfo.StartTime = DateTime.Now;
                                DBManager.UpdateTotalInfoTime(Global.CurDetectInfo);
                                //OnUpdateCellStatus?.Invoke(false);
                                ContactCamGrabStep = 1;
                                ShowContactCamera(true,false);
                                OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->第{Global.CurDetectInfo.ColumnNum + 1}列开始检测", Brushes.Blue);
                                OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->Contact相机移动到第{Param.Motion.ContactCamCheckCellNum + 1}个压头", Brushes.Green);
                                OneGlassDetectStep = 10;
                            }
                            else//玻璃检测完，退出检测流程
                            {
                                ShowContactCamera(false, false);
            
                                Global.CurDetectInfo.EndTime = DateTime.Now;
                                DBManager.UpdateTotalInfoTime(Global.CurDetectInfo);
                                OneGlassDetectStep = 0;
                                SequenceStepManager.StopWatch(SequenceName.ProbeTotal, Global.CurDetectInfo.GlassNum);
                            }
                            break;
                        }
                    case 10:
                        {
                            if (ContactCamGrabStep==0)
                            {
                                if (Global.IsAutoPosOnly)
                                {
                                    OnStop?.Invoke();
                                    Global.IsAutoPosOnly = false;
                                    OneGlassDetectStep = 0;
                                }
                                else
                                {
                                    StageContactSync(Global.CurDetectInfo.GlassNum);
                                    OneGlassDetectStep = 20;
                                    OnShowMsg?.Invoke("AutoRun", "ProbeGantry->Contact相机拍照完成，StageZ轴开始压接", Brushes.Green);
                                    SequenceStepManager.StartWatch(SequenceName.ContactDown, Global.CurDetectInfo.GlassNum);
                                }
                            }
                            else
                            {
                                ContactCamGrab();
                            }
                            break;
                        }
                    case 20:
                        {
                            if (ContactResult == 0)
                            {
                                SequenceStepManager.StopWatch(SequenceName.ContactDown, Global.CurDetectInfo.GlassNum);
                                ContactCamManager.Grab(Global.CurDetectInfo, Global.CurDetectInfo.ColumnNum);
                                Motion.MoveAbs(StationEnum.ProbeGantry, AxisName.ProbeGantryAlig_X1, ProbeGantryPosName.SafePos);
                                OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->StageZ轴压接完毕，Contact相机拍照完成，对位相机1X轴回初始位置", Brushes.Green);
                                OneGlassDetectStep = 22;
                            }
                            else if (ContactResult == 1)
                            {
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.StageContactTimeout);
                                OneGlassDetectStep = 10;
                            }
                            break;
                        }
                  
                    case 22:
                        {
                            if (Motion.IsArrive(StationEnum.ProbeGantry, AxisName.ProbeGantryAlig_X1, ProbeGantryPosName.SafePos))
                            {
                                DetectGantryBasePointX = BasePointHelper.GetGantryDetectBasePoint(GantryName.Detect, Global.CurDetectInfo.GlassNum, Global.CurDetectInfo.ColumnNum);
                                Motion.MoveAbs(StationEnum.DetectGantry, AxisName.DetectGantry_Y1, DetectGantryBasePointX);
                                OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->对位相机1X轴到达初始位置，检测龙门移动到第{Global.CurDetectInfo.ColumnNum + 1}列", Brushes.Green);
                                OneGlassDetectStep = 25;
                                PowerControl(false, false);//关闭Contact相机光源
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.ProbeGantry, AxisName.ProbeGantryAlig_X1))
                            {
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.ProbeGantryAlig_X1MoveTimeout);
                                OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->对位相机X轴回初始位置超时", Brushes.Red);
                                OneGlassDetectStep = 20;
                            }
                            break;
                        }
                    case 25:
                        {
                            if (Motion.IsArrive(StationEnum.DetectGantry, AxisName.DetectGantry_Y1, DetectGantryBasePointX))
                            {
                                OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->检测龙门到达第{Global.CurDetectInfo.ColumnNum + 1}列，开始第[{Global.CurDetectInfo.ColumnNum + 1}]列OpenShort检测", Brushes.Green);
                                
                                OpenShortDetectStep = 1;
                                OneGlassDetectStep = 30;
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.DetectGantry, AxisName.DetectGantry_Y1))
                            {
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.DetectGantry_Y1MoveTimeout);
                                OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->检测龙门移动到第{Global.CurDetectInfo.ColumnNum + 1}列超时", Brushes.Red);
                                OneGlassDetectStep = 22;
                            }
                            break;
                        }
                    case 30:
                        {
                            if (OpenShortDetectStep==0)
                            {
                                OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->OpenShort检测完成，开始Aoi检测", Brushes.Green);
                                AoiDetectStep = 1;
                                OneGlassDetectStep = 40;
                            }
                            else
                            {
                                OpenShortDetect();
                            }
                            break;
                        }
                    case 40:
                        {
                            if (AoiDetectStep == 0)
                            {
                                OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->Aoi检测完成，开始直视Mura检测", Brushes.Green);
                                MuraDetectStep = 1;
                                OneGlassDetectStep =50;
                            }
                            else
                            {
                                AoiDetect();
                            }
                            break;
                        }
                    case 50:
                        {
                            if (MuraDetectStep == 0)
                            {
                                OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->直视Mura检测完成，开始斜视Mura检测", Brushes.Green);
                                SquintMuraDetectStep = 1;
                                OneGlassDetectStep = 60;
                            }
                            else
                            {
                                MuraDetect();
                            }
                            break;
                        }
                    case 60:
                        {
                            if (SquintMuraDetectStep == 0)
                            {
                                OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->斜视Mura检测完成，开始MultiAngle检测", Brushes.Green);
                                MultiAngleDetectStep = 1;
                                OneGlassDetectStep = 70;
                            }
                            else
                            {
                                SquintMuraDetect();
                            }
                            break;
                        }
                    case 70:
                        {
                            if (MultiAngleDetectStep == 0)
                            {
                                OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->第{Global.CurDetectInfo.ColumnNum+1}列检测完成，StageZ轴停止压接", Brushes.Blue);
                                StageStopContactSync(true);                             
                                OneGlassDetectStep = 80;
                            }
                            else
                            {
                                MultiAngleDetect();
                            }
                            break;
                        }
                    case 80:
                        {
                            if (StopContactResult == 0 && 
                                Motion.IsArrive(StationEnum.DualStage, AxisName.Stage1_ContactZ, StagePosName.SafePos) &&
                                Motion.IsArrive(StationEnum.DualStage, AxisName.Stage2_ContactZ, StagePosName.SafePos))
                            {
                                OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->StageZ轴停止压接完毕完成", Brushes.Green);
                               Global.CurDetectInfo.ColumnNum--;
                                OneGlassDetectStep = 1;
                                SequenceStepManager.StopWatch(SequenceName.ContactUp, Global.CurDetectInfo.GlassNum);
                            }
                            else if (StopContactResult == 1)
                            {
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.StageStopContactTimeout);
                                OneGlassDetectStep = 70;
                            }
                            break;
                        }           
                }
            }
            catch (Exception ex)
            {
                AlarmManager.CreateAlarm((int)MotionAlarmCode.ProbeGantryStationException);
                OneGlassDetectStep = -1;
                LogManager.GetLogger("AutoRun").Fatal(ex.ToString());
            }
        }
        double ContactCamXPos = 0;
        private void ContactCamGrab()
        {
            switch (ContactCamGrabStep)
            {
                case 1:
                    {
                        Delay.Start(Param.Motion.GantryIntervalTime);
                        DetectGantryBasePointX = BasePointHelper.GetGantryDetectBasePoint(GantryName.Detect, Global.CurDetectInfo.GlassNum,Global.CurDetectInfo.ColumnNum)-Param.Motion.DetectGantryOffset;//在每列检测位置上减去避让值，避让Contact相机
                        Motion.MoveAbs(StationEnum.DetectGantry, AxisName.DetectGantry_Y1, DetectGantryBasePointX);
                        OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->检测龙门移动到第{Global.CurDetectInfo.ColumnNum + 1}列避让位置", Brushes.Green);
                        if (Param.Align.IsEnableOffset[Global.CurDetectInfo.GlassNum])
                        {
                            UvwOffsetSync(Global.CurDetectInfo.GlassNum, Global.CurDetectInfo.ColumnNum);
                            OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->Uvw补偿第{Global.CurDetectInfo.ColumnNum + 1}列，补偿值：{Param.Align.GlassColumnOffsets[Global.CurDetectInfo.GlassNum].ColumnOffsets[Global.CurDetectInfo.ColumnNum]}毫米", Brushes.Green);
                        }
                        ContactCamGrabStep = 10;
                        break;
                    }
                case 10:
                    {
                        if (Delay.TimeUp())
                        {
                            ProbeGantryBasePointX = BasePointHelper.GetGantryDetectBasePoint(GantryName.Probe, Global.CurDetectInfo.GlassNum,Global.CurDetectInfo.ColumnNum);
                            GantryMove(ProbeGantryBasePointX);
                            OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->Probe龙门移动到第{Global.CurDetectInfo.ColumnNum + 1}列", Brushes.Green);
                            ContactCamGrabStep = 20;
                        }
                        break;
                    }
                case 20:
                    {
                        if (Motion.GetAxisActPos(StationEnum.ProbeGantry, AxisName.ProbeGantry_Y1) + Motion.GetAxisActPos(StationEnum.DetectGantry, AxisName.DetectGantry_Y1)<= ProbeGantryBasePointX+ DetectGantryBasePointX+5)
                        {
                            PowerControl(true, false);//打开Contact相机光源
                            ContactCamXPos = BasePointHelper.GetContactHeadPos(Global.CurDetectInfo.GlassNum,Param.Motion.ContactCamCheckCellNum);
                            if (IsDetectModuleZSafe() == false)
                            {
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.DetectModuleZNotSafe);
                                ContactCamGrabStep = 0;
                            }
                            else
                            {
                                Motion.MoveAbs(StationEnum.ProbeGantry, AxisName.ProbeGantryAlig_X1, ContactCamXPos);
                                OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->对位相机1X轴移动到监控第{Param.Motion.ContactCamCheckCellNum + 1}个压头位置", Brushes.Green);
                                ContactCamGrabStep = 30;
                            }
                        }
                        break;
                    }
                case 30:
                    {
                        if (Motion.IsArrive(StationEnum.ProbeGantry, AxisName.ProbeGantry_Y1, ProbeGantryBasePointX) &&
                            Motion.IsArrive(StationEnum.ProbeGantry, AxisName.ProbeGantryAlig_X1, ContactCamXPos))
                        {
                            OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->对位相机1X轴和Probe龙门到达监控第{Param.Motion.ContactCamCheckCellNum + 1}个压头位置", Brushes.Green);
                            if (Param.Align.IsEnableOffset[Global.CurDetectInfo.GlassNum])
                            {
                                ContactCamGrabStep = 40;
                            }
                            else
                            {
                                ContactCamGrabStep = 0;
                            }
                   
                        }
                        else if (Motion.IsMoveTimeOut(StationEnum.ProbeGantry, AxisName.ProbeGantry_Y1))
                        {
                            AlarmManager.CreateAlarm((int)MotionAlarmCode.ProbeGantry_Y1MoveTimeout);
                            OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->Probe龙门移动到第{Global.CurDetectInfo.ColumnNum + 1}列超时", Brushes.Red);
                            ContactCamGrabStep = 10;
                        }
                        else if (Motion.IsMoveTimeOut(StationEnum.ProbeGantry, AxisName.ProbeGantryAlig_X1))
                        {
                            AlarmManager.CreateAlarm((int)MotionAlarmCode.ProbeGantry_Y1MoveTimeout);
                            OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->对位相机1X轴移动到监控第{Param.Motion.ContactCamCheckCellNum + 1}个压头位置超时", Brushes.Red);
                            ContactCamGrabStep = 20;
                        }
                        break;
                    }
                case 40:
                    {
                        if (UvwAutoPosResult==0)
                        {
                            UvwAutoPosResult = -1;
                            OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->Uvw补偿第{Global.CurDetectInfo.ColumnNum + 1}列补偿完成", Brushes.Green);
                            ContactCamGrabStep = 0;
                        }
                        else if (UvwAutoPosResult == 1)
                        {
                            OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->Uvw补偿第{Global.CurDetectInfo.ColumnNum + 1}列补偿超时", Brushes.Red);
                            OnPause?.Invoke(-1);
                            ContactCamGrabStep = 30;
                        }
                        break;
                    }
                default:
                    break;
            }
        }
        private void OpenShortDetect()
        {
            if (Param.Detect.OpenShortDetectShield)
            {
                OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->OpenShort检测屏蔽", Brushes.Blue);
                OpenShortDetectStep=0;
            }
            else if (Param.Detect.ColDetectShield[Global.CurDetectInfo.GlassNum].OpenShortDetectShield[Global.CurDetectInfo.ColumnNum])
            {
                OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->第{Global.CurDetectInfo.ColumnNum+1}列OpenShort检测屏蔽", Brushes.Blue);
                OpenShortDetectStep = 0;
            }
            switch (OpenShortDetectStep)
            {
                case 1:
                    {
                        OneColumnModel oneColumn = DetectionHelper.GetOneColumnModel(Global.CurDetectInfo.GlassNum, Global.CurDetectInfo.ColumnNum);
                        oneColumn.DetectSts = 1;
                        OnUpdateCellStatus?.Invoke(false);
                        TesterPC.Instance.TesterModeSwitch("TOS",0,0);
                        SequenceStepManager.StartWatch(SequenceName.OSTest, Global.CurDetectInfo.GlassNum);
                        OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->Tester切换到TOS模式", Brushes.Green);
                        OpenShortDetectStep = 5;
                        break;
                    }
                case 5:
                    {
                        if (TesterPC.TesterModeSwitchResult1=="OK")
                        {
                            OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->Tester切换到TOS模式成功，开始第[{Global.CurDetectInfo.ColumnNum + 1}]列OpenShort检测", Brushes.Green);
                            Thread.Sleep((int)(Param.Detect.DetectDelayTime * 1000));
                            Delay.Start(Param.Comm.OneColumnTestTimeout);
                            TesterPC.Instance.OneColumnTestStart(Global.CurDetectInfo.ColumnNum);
                            OpenShortDetectStep = 10;
                        }
                        else if (TesterPC.TesterModeSwitchResult1 == "NG")
                        {
                            OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->Tester切换到TOS模式失败", Brushes.Red);
                            OpenShortDetectStep = 1;
                        }
                        break;
                    }
                case 10:
                    {
                        if (TesterPC.OneColumnTestCom)
                        {
                            TesterPC.OneColumnTestCom = false;
                            OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->第[{Global.CurDetectInfo.ColumnNum + 1}]列OpenShort检测完毕", Brushes.Green);
                            OpenShortDetectStep = 0;
                            SequenceStepManager.StopWatch(SequenceName.OSTest, Global.CurDetectInfo.GlassNum);
                        }
                        else if (Delay.TimeUp())
                        {
                            AlarmManager.CreateAlarm((int)MotionAlarmCode.OneColumnTestTimeout);
                            OpenShortDetectStep = 1;
                        }
                        break;
                    }
            
            }
        }
        private void AoiDetect()
        {
            if (Param.Detect.AoiDetectShield)
            {
                OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->Aoi检测屏蔽", Brushes.Blue);
                AoiDetectStep = 0;
            }
            else if (Param.Detect.ColDetectShield[Global.CurDetectInfo.GlassNum].AoiDetectShield[Global.CurDetectInfo.ColumnNum])
            {
                OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->第{Global.CurDetectInfo.ColumnNum +1}列Aoi检测屏蔽", Brushes.Blue);
                AoiDetectStep = 0;
            }
            switch (AoiDetectStep)
            {
                case 1:
                    {
                        OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->请求检测龙门工位Aoi检测", Brushes.Green);
                        OnTriggerSignal?.Invoke((int)StationCommand.AoiDetectReq);
                        AoiDetectStep = 10;
                        break;
                    }
                case 10:
                    {
                        if (AoiDetectCom)
                        {
                            AoiDetectCom = false;
                            OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->第{Global.CurDetectInfo.ColumnNum + 1}列Aoi检测完成", Brushes.Blue);
                            AoiDetectStep = 0;
                        }
                        break;
                    }
           
            }
        }
        private void MuraDetect()
        {
            if (Param.Detect.MuraDetectShield)
            {
                OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->直视Mura检测屏蔽", Brushes.Blue);
                MuraDetectStep = 0;
            }
            else if (Param.Detect.ColDetectShield[Global.CurDetectInfo.GlassNum].MuraDetectShield[Global.CurDetectInfo.ColumnNum])
            {
                OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->第{Global.CurDetectInfo.ColumnNum + 1}列直视Mura检测屏蔽", Brushes.Blue);
                MuraDetectStep = 0;
            }
            switch (MuraDetectStep)
            {
                case 1:
                    {
                        OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->请求检测龙门工位直视Mura检测", Brushes.Green);
                        OnTriggerSignal?.Invoke((int)StationCommand.MuraDetectReq);
                        MuraDetectStep = 10;
                        break;
                    }
                case 10:
                    {
                        if (MuraDetectCom)
                        {
                            MuraDetectCom = false;
                            OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->第{Global.CurDetectInfo.ColumnNum + 1}列直视Mura检测完成", Brushes.Blue);
                            MuraDetectStep = 0;
                        }
                        break;
                    }
            }
        }
        private void SquintMuraDetect()
        {
            if (Param.Detect.SquintMuraDetectShield)
            {
                OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->斜视Mura检测屏蔽", Brushes.Blue);
                SquintMuraDetectStep = 0;
            }
            else if (Param.Detect.ColDetectShield[Global.CurDetectInfo.GlassNum].SquintMuraDetectShield[Global.CurDetectInfo.ColumnNum])
            {
                OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->第{Global.CurDetectInfo.ColumnNum + 1}列斜视Mura检测屏蔽", Brushes.Blue);
                SquintMuraDetectStep = 0;
            }
            switch (SquintMuraDetectStep)
            {
                case 1:
                    {
                        OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->请求检测龙门工位斜视Mura检测", Brushes.Green);
                        OnTriggerSignal?.Invoke((int)StationCommand.SquintMuraDetectReq);
                        SquintMuraDetectStep = 10;
                        break;
                    }
                case 10:
                    {
                        if (SquintMuraDetectCom)
                        {
                            SquintMuraDetectCom = false;
                            OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->第{Global.CurDetectInfo.ColumnNum + 1}列斜视Mura检测完成", Brushes.Blue);
                            SquintMuraDetectStep = 0;
                        }
                        break;
                    }
            }
        }
        private void MultiAngleDetect()
        {
            if (Param.Detect.MultiAngleDetectShield)
            {
                OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->MultiAngle检测屏蔽", Brushes.Blue);
                MultiAngleDetectStep = 0;
            }
            else if (Param.Detect.ColDetectShield[Global.CurDetectInfo.GlassNum].MultiAngleDetectShield[Global.CurDetectInfo.ColumnNum])
            {
                OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->第{Global.CurDetectInfo.ColumnNum + 1}列MultiAngle检测屏蔽", Brushes.Blue);
                MultiAngleDetectStep = 0;
            }
            switch (MultiAngleDetectStep)
            {
                case 1:
                    {
                        OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->请求检测龙门工位MultiAngle检测", Brushes.Green);
                        OnTriggerSignal?.Invoke((int)StationCommand.MultiAngleDetectReq);
                        MultiAngleDetectStep = 10;
                        break;
                    }
                case 10:
                    {
                        if (MultiAngleDetectCom)
                        {
                            MultiAngleDetectCom = false;
                            OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->第{Global.CurDetectInfo.ColumnNum + 1}列MultiAngle检测完成", Brushes.Blue);
                            MultiAngleDetectStep = 0;
                        }
                        break;
                    }
            }
        }
        /// <summary>
        /// 检测结束，各轴回初始位置
        /// </summary>
        private void DetectEnd()
        {
            try
            {
                switch (DetectEndStep)
                {
                    case 1:
                        {
                            GantryMove(ProbeGantryPosName.SafePos);
                            DetectEndStep = 10;
                            OnShowMsg?.Invoke("AutoRun", "ProbeGantry->Probe龙门回初始位置", Brushes.Green);
                            break;
                        }
                    case 10:
                        {
                            if (Motion.IsArrive(StationEnum.ProbeGantry, AxisName.ProbeGantry_Y1, ProbeGantryPosName.SafePos))
                            {
                                OnShowMsg?.Invoke("AutoRun", "ProbeGantry->对位相机X轴回初始位置", Brushes.Green);
                                Motion.MoveAbs(StationEnum.ProbeGantry, AxisName.ProbeGantryAlig_X1, ProbeGantryPosName.SafePos);
                                Motion.MoveAbs(StationEnum.ProbeGantry, AxisName.ProbeGantryAlig_X2, ProbeGantryPosName.SafePos);
                                DetectEndStep = 15;
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.ProbeGantry, AxisName.ProbeGantry_Y1))
                            {
                                OnShowMsg?.Invoke("AutoRun", "ProbeGantry->Probe龙门回初始位超时", Brushes.Red);
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.ProbeGantry_Y1MoveTimeout);
                            }
                            break;
                        }
                    case 15:
                        {
                            if (Motion.IsArrive(StationEnum.ProbeGantry, AxisName.ProbeGantryAlig_X1, ProbeGantryPosName.SafePos) && Motion.IsArrive(StationEnum.ProbeGantry, AxisName.ProbeGantryAlig_X2, ProbeGantryPosName.SafePos))
                            {
                                DetectEndStep = 0;
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.ProbeGantry, AxisName.ProbeGantryAlig_X1) || Motion.IsMoveTimeOut(StationEnum.ProbeGantry, AxisName.ProbeGantryAlig_X2))
                            {
                                OnShowMsg?.Invoke("AutoRun", "ProbeGantry->对位相机X轴回初始位超时", Brushes.Red);
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.ProbeGantryAlig_X1MoveTimeout);
                            }
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                AlarmManager.CreateAlarm((int)MotionAlarmCode.ProbeGantryStationException);
                LogManager.GetLogger("AutoRun").Fatal(ex.ToString());
            }
        }
        public bool GantryMove(ProbeGantryPosName posName)
        {
            bool ret = IsContactZAtSafePos();
            if (ret==false)
            {
                AlarmManager.CreateAlarm((int)MotionAlarmCode.ProbeGantryMoveFail);
                OnStop?.Invoke();
                OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->Probe龙门移动到点位：{posName}失败", Brushes.Red);
            }
            else
            {
                Motion.MoveAbs(StationEnum.ProbeGantry,AxisName.ProbeGantry_Y1,posName);
            }
            return ret;
        }
        public bool GantryMove(double pos)
        {
            bool ret = IsContactZAtSafePos();
            if (ret == false)
            {
                AlarmManager.CreateAlarm((int)MotionAlarmCode.ProbeGantryMoveFail);
                OnStop?.Invoke();
                OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->Probe龙门移动到点位：{pos}失败", Brushes.Red);
            }
            else
            {
                Motion.MoveAbs(StationEnum.ProbeGantry, AxisName.ProbeGantry_Y1, pos);
            }
            return ret;
        }

        private void PrbAutoChg_OnTriggerSignal(int obj)
        {
            switch (obj)
            {
                case (int)StationCommand.PrbAutoChgCom:
                    {
                        PrbAutoChgCom = true;
                        break;
                    }
                case (int)StationCommand.PrbAutoChgHomeCom:
                    {
                        PrbAutoChgHomeCom = true;
                        break;
                    }
            }
        }
        public void InitColumnInfo(int colNum)
        {
            try
            {
                OneColumnModel oneColumnnfo = new OneColumnModel { ColumnNum = colNum };
                oneColumnnfo.PanelList = new List<OpenShortDetectModel>();
                DateTime detectTime = DateTime.Now;
                for (int i = 0; i < Param.Panel.CellCountY; i++)
                {
                    DBManager.InsertOneCellInfo(Global.CurDetectInfo, colNum,i);
                    OpenShortDetectModel oneInfo = new OpenShortDetectModel {ColumnNum=colNum, RowNum = i ,DetectTime= detectTime, GlassNum=Global.CurDetectInfo.GlassNum,GlassID=Global.CurDetectInfo.GlassID };
                    oneColumnnfo.PanelList.Add(oneInfo);
                }
                DetectionHelper.DeleteOneColumnModel(Global.CurDetectInfo.GlassNum, colNum);
                if (Global.GlassDic.Keys.Contains(Global.CurDetectInfo.GlassNum))
                {
                    Global.GlassDic[Global.CurDetectInfo.GlassNum].ColumnList.Add(oneColumnnfo);
                }
                else
                {
                    Global.GlassDic.Add(Global.CurDetectInfo.GlassNum,new GlassModel {GlassID= Global.CurDetectInfo.GlassID, GlassNum =0});
                    Global.GlassDic[Global.CurDetectInfo.GlassNum].ColumnList.Add(oneColumnnfo);
                }
            }
            catch (Exception ex)
            {
                OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->InitColumnInfo方法执行异常，异常信息：{ex.ToString()}", Brushes.Red);
            }
        }
        bool IsInHandleTesterAlarm = false;
        private void NGCountAlarmHandle(string value)
        {
            try
            {
                if (IsInHandleTesterAlarm)
                {
                    OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->上一次报警还未处理完成", Brushes.Green);
                    return;
                }
                Task.Factory.StartNew(() =>
                {
                    IsInHandleTesterAlarm = true;
                    while (IsContactZAtSafePos() == false)
                    {
                        Thread.Sleep(50);
                    }
                    var ngType = value;
                    switch (ngType)
                    {
                        case "PanelNG":
                            {
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.PanelNGCountOverSetValue);
                                break;
                            }
                        case "BlockNG":
                            {
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.BlockNGCountOverSetValue);
                                break;
                            }
                        case "YieldNG":
                            {
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.YieldNGCountOverSetValue);
                                break;
                            }
                    }
                    IsInHandleTesterAlarm = false;
                });
            }
            catch (Exception ex)
            {
                OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->NGCountAlarmHandle执行异常，异常信息：{ex}", Brushes.Red);
            }
        }
        /// <summary>
        /// 对位流程
        /// </summary>
        /// <param name="stageIndex">平台索引,从0开始</param>
        /// <param name="xyt">对位值</param>
        /// <param name="reserve">1：左相机标定，2：右相机标定，3：自动对位</param>
        private bool AlignSignalProvider_OnStageAlign(int stageIndex, double[] xyt, int[] reserve)
        {
            bool ret = false;
            double offSetT = 0;
            if (xyt != null)
            {
                //自动对位
                if (reserve[0] == 3)
                {
                    offSetT = stageIndex == 0 ? Param.Align.OffsetT[0] : Param.Align.OffsetT[1];
                    Motion.MoveRel(StationEnum.ProbeGantry, AxisName.ProbeGantryAlig_X1, xyt[0]);
                    Motion.MoveRel(StationEnum.ProbeGantry, AxisName.ProbeGantryAlig_X2, -xyt[0]);
                    UvwAutoPos(xyt[0]);
                    AlignSignalProvider.XValueSum += xyt[0];
                    OnUpdateXYTValue?.Invoke(stageIndex, new double[] { xyt[0], xyt[1], xyt[2] + offSetT });
                    OnShowMsg?.Invoke("AutoRun", $"{StationName}->对位偏移值：{ xyt[0]}，{ xyt[1]}，{ xyt[2]}", Brushes.Green);
                }
                else//自动标定
                {
                    UvwAutoPosResult = 0;
                    if (reserve[0] == 1)//左相机
                    {
                        Motion.MoveRel(StationEnum.ProbeGantry, AxisName.ProbeGantryAlig_X1, xyt[0]);
                    }
                    else if (reserve[0] == 2)//右相机
                    {
                        Motion.MoveRel(StationEnum.ProbeGantry, AxisName.ProbeGantryAlig_X2, xyt[0]);
                    }
                    OnShowMsg?.Invoke("AutoRun", $"{StationName}->相机标定值：{ xyt[0]}，{ xyt[1]}，{ xyt[2]}", Brushes.Green);
                }
                Motion.MoveRel(StationEnum.DualStage, stageIndex == 0 ? AxisName.Stage1_Y : AxisName.Stage2_Y, xyt[1]);
                Motion.MoveRel(StationEnum.DualStage, stageIndex == 0 ? AxisName.Stage1_T : AxisName.Stage2_T, xyt[2] + offSetT);
                Thread.Sleep(200);
                Stopwatch sw = new Stopwatch();
                sw.Start();
                while (true)
                {
                    if (Motion.IsArrive(StationEnum.ProbeGantry, AxisName.ProbeGantryAlig_X1) &&
                        Motion.IsArrive(StationEnum.ProbeGantry, AxisName.ProbeGantryAlig_X2) &&
                        Motion.IsArrive(StationEnum.DualStage, stageIndex == 0 ? AxisName.Stage1_Y : AxisName.Stage2_Y) &&
                        Motion.IsArrive(StationEnum.DualStage, stageIndex == 0 ? AxisName.Stage1_T : AxisName.Stage2_T))
                    {
                        if (IsAutoPosDone())
                        {
                            ret = true;
                            break;
                        }
                        else if (sw.ElapsedMilliseconds > 10 * 1000)
                        {
                            OnShowMsg?.Invoke("AutoRun", $"{StationName}->Uvw_Y轴移动超时", Brushes.Red);
                            break;
                        }
                    }
                    else if (sw.ElapsedMilliseconds > 10 * 1000)
                    {
                        OnShowMsg?.Invoke("AutoRun", $"{StationName}->龙门和相机移动超时", Brushes.Red);
                        break;
                    }
                    Thread.Sleep(20);
                }
            }
            return ret;
        }
        /// <summary>
        /// 光源通道控制
        /// </summary>
        /// <param name="onOff">true：打开，false：关闭</param>
        /// <param name="isAlignCam">true：对位相机光源，false：Contact相机光源</param>
        private void PowerControl(bool onOff,bool isAlignCam=true)
        {
            try
            {
                if (onOff)
                {
                    if (isAlignCam)
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            PowerHelper.Instance.GetPowerByNum(0)?.PowerOn((HTPower.HTPowerChannel)i);
                        }
                    }
                    else
                    {
                        PowerHelper.Instance.GetPowerByNum(0)?.PowerOn(HTPower.HTPowerChannel.POWER_CHANNEL_3);
                    }

                }
                else
                {
                    if (isAlignCam)
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            PowerHelper.Instance.GetPowerByNum(0)?.PowerOff((HTPower.HTPowerChannel)i);
                        }
                    }
                    else
                    {
                        PowerHelper.Instance.GetPowerByNum(0)?.PowerOff(HTPower.HTPowerChannel.POWER_CHANNEL_3);
                    }
                }
            }
            catch (Exception ex)
            {
                OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->PowerControl执行异常，异常信息：{ex}", Brushes.Red);
            }
        }
        public bool IsDetectModuleZSafe()
        {
           bool ret= Motion.IsArrive(StationEnum.DetectGantry,AxisName.DetectModule1_Z1,DetectGtyPosName.SafePos);
            ret &= Motion.IsArrive(StationEnum.DetectGantry, AxisName.DetectModule1_Z2, DetectGtyPosName.SafePos);
            ret &= Motion.IsArrive(StationEnum.DetectGantry, AxisName.DetectModule2_Z1, DetectGtyPosName.SafePos);
            ret &= Motion.IsArrive(StationEnum.DetectGantry, AxisName.DetectModule2_Z2, DetectGtyPosName.SafePos);
            return ret;
        }
    }

    /// <summary>
    /// Probe工位点位名称
    /// </summary>
    public enum ProbeGantryPosName
    {
        /// <summary>
        /// 每个轴的安全位置
        /// </summary>
        SafePos,
        /// <summary>
        /// 对位1相机的Z轴在平台1拍照位置
        /// </summary>
        Stg1AlignCamGrabPos_Z1,
        /// <summary>
        /// 对位2相机的Z轴在平台1拍照位置
        /// </summary>
        Stg1AlignCamGrabPos_Z2,
        /// <summary>
        /// 对位1相机的Z轴在平台2拍照位置
        /// </summary>
        Stg2AlignCamGrabPos_Z1,
        /// <summary>
        /// 对位2相机的Z轴在平台2拍照位置
        /// </summary>
        Stg2AlignCamGrabPos_Z2,
        /// <summary>
        /// 对位1相机的X轴在平台1拍照位置
        /// </summary>
        Stg1AlignCamGrabPos_X1,
        /// <summary>
        /// 对位2相机的X轴在平台1拍照位置
        /// </summary>
        Stg1AlignCamGrabPos_X2,
        /// <summary>
        /// 对位1相机的X轴在平台2拍照位置
        /// </summary>
        Stg2AlignCamGrabPos_X1,
        /// <summary>
        /// 对位2相机的X轴在平台2拍照位置
        /// </summary>
        Stg2AlignCamGrabPos_X2,    
        /// <summary>
        /// Probe自动更换位置
        /// </summary>
        AutoChgPos,
        /// <summary>
        /// 检测位置
        /// </summary>
        DetectPos,
        /// <summary>
        /// 探针在平台1的第五列压接时，Probe龙门的位置
        /// </summary>
        Stg1ProbeGantryAlignPos,
        /// <summary>
        /// 探针在平台2的第五列压接时，Probe龙门的位置
        /// </summary>
        Stg2ProbeGantryAlignPos,
        /// <summary>
        ///固定Pcb板位置
        /// </summary>
        FixPos,
        Stg1ContactCamMonitorStartPos,
        Stg2ContactCamMonitorStartPos
    }

}
