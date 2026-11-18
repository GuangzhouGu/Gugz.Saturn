/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2020 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnMain.Station
// 文件名称    ：PrbAutoChg.cs
// =================================
// 创 建 者    ：guguangzhou
// 创建日期    ：2021/01/29 09:00:00 
// 功能描述    ：对位参数
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
using HYC.HTPCBase.Extension.Entities;
using HYC.HTPCBase.Extension.Recipe;
using HYC.HTPCBase.Alarm;
using HYC.HTPCBase.Motion;
using HYC.HTPCBase.Station;

using HYC.SaturnMain.Params;
using HYC.SaturnMain.Devices.Motion;
using HYC.SaturnMain.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HYC.HTPCBase.Entities;
using HYC.SaturnMain.CabinetInfos;
using HYC.SaturnMain.Entities;
using HYC.SaturnDB.Model;
using HYC.SaturnMain.Alarm;
using HYC.HTPCBase.Param;
using HYC.SaturnMain;
using HYC.HTLog;
using System.Windows.Forms;

namespace HYC.SaturnMain.Station
{
    public class PrbAutoChg : IStation
    {
        public static event Action<int> OnTriggerSignal;

        public static Status PrbLoadSts = Status.None;//Probe自动安装完成
        public static Status PrbUnloadSts = Status.None;//Probe自动安装完成
        #region 状态机步骤
        public int AutoRunStep { get; set; }
        private int AxisToSafePosStep = -1;//判断轴是否在安全位置流程状态机步骤 
        public int PrbAutoChgStep = -1;
        private int LoadPrbStep = -1;
        private int UnloadPrbStep = -1;
        #endregion
        #region IStation接口
        public event Action<HomeEventArgs> OnHomeStatus;
        public event Action OnStop;
        public event Action<int> OnPause;
        public event Action<string, string, Brush> OnShowMsg;
        public MotionManager Motion { get; set; } = MotionManager.GetInstance();
        public int HomeStep { get; set; } = -1;

        public int StationNum { get; set; } = 3;
        public string StationName { get; set; } = "PrbAutoChg";
        #endregion
        private Delay Delay = new Delay();
        private bool ProbeAutoChgBit = false;
        private bool ProbeGantryArrChgPos = false;
        /// <summary>
        /// 料仓左侧检测Probe感应器
        /// </summary>
        string LeftDetPrbSensor;
        /// <summary>
        /// 料仓右侧检测Probe感应器
        /// </summary>
        string RightDetPrbSensor;
        #region 状态标志位
        private bool CabinetZHomeCom = false;
        private bool StageArriveAutoChgPos = false; 
        #endregion
        public bool Init()
        {
            return true;
        }
        public bool Home()
        {
            bool ret = false;
            switch (HomeStep)
            {
                case 1:
                    {
                        if (Para.PCBase.IsSimulator)
                        {
                            Motion.Home(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Z);
                            HomeStep =20;
                        }
                        else
                        {
                            if (Motion.GetDigitalInput(InputName.PrbAutoChgDetLeft) || Motion.GetDigitalInput(InputName.PrbAutoChgDetRight))
                            {
                                OnStop?.Invoke();
                                MessageBox.Show("自动换型手臂检测到Probe，禁止回零，请手动控制轴把Probe放在料仓中");
                                HomeStep = 1000;
                            }
                            else
                            {
                                if (!Motion.GetDigitalInput(InputName.DetectAutoChgY1) || !Motion.GetDigitalInput(InputName.DetectAutoChgY2))
                                {
                                    OnStop?.Invoke();
                                    MessageBox.Show("安全光栅检测到Probe更换模组Y轴，禁止回零，请手动控制轴把自动更换Y轴移出料仓");
                                    HomeStep = 1000;
                                }
                                else
                                {
                                    OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->安全光栅未检测到Probe更换模组Y轴，Probe更换模组Z轴开始回零", Brushes.Green);
                                    Motion.Home(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Z);
                                    HomeStep = 20;
                                }
                            }
                        }
                        break;
                    }
                //case 10:
                //    {
                //        if (Motion.IsPosLimit(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Y) || 
                //            (Motion.GetDigitalInput(InputName.DetectAutoChgY1) && Motion.GetDigitalInput(InputName.DetectAutoChgY2)))
                //        {
                //            Motion.Stop(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Y);
                //            OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->再次确认安全光栅是否检测到Probe更换模组Y轴", Brushes.Green);
                //            if (Motion.GetDigitalInput(InputName.DetectAutoChgY1) &&Motion.GetDigitalInput(InputName.DetectAutoChgY2)||Para.PCBase.IsSimulator)
                //            {
                //                Motion.Home(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Z);
                //                OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->Probe更换模组Y轴在安全位置，Probe更换模组Z轴开始回零", Brushes.Green);
                //                HomeStep = 20;
                //            }
                //            else
                //            {
                //                OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->Y轴已经在安全位置，安全光栅再次被触发，请检查安全光栅是否报警", Brushes.Red);
                //                OnStop?.Invoke();
                //                HomeStep = 1000;
                //            }
                //        }                      
                //        break;
                //    }
                case 20:
                    {
                        if (Motion.IsHomeDone(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Z))
                        {
                            OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->Probe更换模组Z轴回零完成，移动到回零避让位置", Brushes.Green);
                            Motion.MoveAbs(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Z,PrbAutoChgPosName.HomeSafePos);
                            OnTriggerSignal?.Invoke((int)StationCommand.PrbAutoChgYArriveSafePos);
                            HomeStep = 25;
                        }
                        else if (Motion.IsMoveTimeOut(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Z))
                        {
                            OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->Probe更换模组Z轴回零超时", Brushes.Red);
                            HomeStep = 1000;
                        }
                        break;
                    }
                case 25:
                    {
                        if (Motion.IsArrive(StationEnum.PrbAutoChg,AxisName.PrbAutoChg_Z,PrbAutoChgPosName.HomeSafePos))
                        {
                            OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->Probe更换模组Z轴到达回零避让位置，等待料仓龙门回零完成", Brushes.Green);
                            Delay.Start(Param.Motion.WaitCabinetZHomeComTimeout);
                            HomeStep = 30;
                        }
                        else if (Motion.IsMoveTimeOut(StationEnum.PrbAutoChg,AxisName.PrbAutoChg_Z))
                        {
                            OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->Probe更换模组Z轴移动到回零避让位置超时", Brushes.Red);
                            HomeStep = 1000;
                        }
                        break;
                    }
                case 30:
                    {
                        if (CabinetZHomeCom)
                        {
                            CabinetZHomeCom = false;
                            Motion.Home(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Y);
                            HomeStep = 40;
                            OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->料仓龙门回零完成，Probe更换模组Y轴开始回零", Brushes.Green);
                        }
                        else if (Delay.TimeUp())
                        {
                            OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->等待料仓Z轴回零完成超时", Brushes.Red);
                            HomeStep = 1000;
                        }
                        break;
                    }
                case 40:
                    {
                        if (Motion.IsHomeDone(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Y))
                        {
                            Motion.MoveAbs(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Y, PrbAutoChgPosName.SafePos);
                            OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->Probe更换模组Y轴回零完成，去安全位置", Brushes.Green);
                            HomeStep = 50;
                        }
                        else if (Motion.IsMoveTimeOut(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Y))
                        {
                            OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->Probe更换模组Y轴回零超时", Brushes.Red);
                            HomeStep = 1000;
                        }
                        break;
                    }
                case 50:
                    {
                        if (Motion.IsArrive(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Y, PrbAutoChgPosName.SafePos))
                        {
                            OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->Probe更换模组Y轴到达安全位置，Probe更换模组Z轴移动到安全位置", Brushes.Green);
                            Motion.MoveAbs(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Z, PrbAutoChgPosName.SafePos);
                            HomeStep = 60;
                        }
                        else if (Motion.IsMoveTimeOut(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Y))
                        {
                            OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->Probe更换模组Y轴运动超时", Brushes.Red);
                            HomeStep = 1000;
                        }
                        break;
                    }
                case 60:
                    {
                        if (Motion.IsArrive(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Z, PrbAutoChgPosName.SafePos))
                        {
                            OnTriggerSignal?.Invoke((int)StationCommand.PrbAutoChgHomeCom);
                            OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->Probe更换模组Z轴到达安全位置", Brushes.Green);
                            OnHomeStatus?.Invoke(new HomeEventArgs { StationName = StationName, StationNum = StationNum, HomeResult = true });
                            HomeStep = 0;
                        }
                        else if (Motion.IsMoveTimeOut(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Z))
                        {
                            OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->Probe更换模组Y轴运动超时", Brushes.Red);
                            HomeStep = 1000;
                        }
                      
                        break;
                    }
                case 1000:
                    {
                        OnStop?.Invoke();
                        AlarmManager.CreateAlarm((int)MotionAlarmCode.PrbAutoChgStaHomeFail);
                        HomeStep = -1;
                        OnHomeStatus?.Invoke(new HomeEventArgs { StationName = StationName, StationNum = StationNum, HomeResult = false });
                        break;
                    }
            }
            return ret;
        }
        public bool AutoRun()
        {
            switch (AutoRunStep)
            {
                case 1://判断各轴都在初始位置
                    {
                        if (AxisToSafePosStep == 0)
                        {
                            AutoRunStep = 10;
                        }
                        else
                        {
                            AxisToSafePos();
                        }
                        break;
                    }
                case 10:
                    {
                        if (ProbeAutoChgBit)
                        {
                            OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->开始自动更换Probe", Brushes.Green);
                            PrbAutoChgStep = 1;
                            ProbeAutoChgBit = false;
                            AutoRunStep = 20;
                        }
                        break;
                    }
                case 20:
                    {
                        if (PrbAutoChgStep==0)
                        {
                            AutoRunStep = 10;
                        }
                        else
                        {
                            PrbAutoChgFlow();
                        }
                        break;
                    }
            }
            return true;
        }
        public bool Pause()
        {

            return true;
        }
     
        /// <summary>
        /// 判断各轴是否在安全位置流程
        /// </summary>
        public void AxisToSafePos()
        {
            switch (AxisToSafePosStep)
            {
                case 1:
                    {
                        if (Motion.IsArrive(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Z, CabinetPosName.SafePos))
                        {
                            if (Motion.IsArrive(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Y, ProbeGantryPosName.SafePos))
                            {
                                AxisToSafePosStep = 1000;
                            }
                            else
                            {
                                Motion.MoveAbs(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Y, ProbeGantryPosName.SafePos);
                                AxisToSafePosStep = 20;
                                OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->Probe自动更换Y轴回初始位置", Brushes.Green);
                            }
                        }
                        else
                        {
                            Motion.MoveAbs(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Z, CabinetPosName.SafePos);
                            AxisToSafePosStep = 10;
                            OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->Probe自动更换Z轴回初始位置", Brushes.Green);
                        }
                        break;
                    }
                case 10:
                    {
                        if (Motion.IsArrive(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Z, CabinetPosName.SafePos))
                        {
                            OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->自动更换Z轴回初始位置完成", Brushes.Green);
                            AxisToSafePosStep = 1;
                        }
                        else if (Motion.IsMoveTimeOut(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Z))
                        {
                            OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->Probe自动更换Z轴回初始位置超时", Brushes.Red);
                            AxisToSafePosStep = 2000;
                        }
                        break;
                    }
                case 20:
                    {
                        if (Motion.IsArrive(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Y, ProbeGantryPosName.SafePos))
                        {
                            OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->自动更换Y轴回初始位置完成", Brushes.Green);
                            AxisToSafePosStep = 1000;
                        }
                        else if (Motion.IsMoveTimeOut(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Y))
                        {
                            OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->Probe自动更换Y轴回初始位置超时", Brushes.Red);
                            AxisToSafePosStep = 2000;
                        }
                        break;
                    }
                case 1000:
                    {
                        OnTriggerSignal?.Invoke((int)StationCommand.PrbAutoChgYArriveSafePos);
                        OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->PrbAutoChg工位回初始位完成", Brushes.Green);
                        AxisToSafePosStep = 0;
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

        public void PrbAutoChgFlow()
        {
            try
            {
                switch (PrbAutoChgStep)
                {
                    case 1:
                        {
                            AxisToSafePosStep = 1;
                            PrbAutoChgStep = 10;
                            ProbeGantryArrChgPos = false;
                            StageArriveAutoChgPos = false;
                            Delay.Start(60);
                            break;
                        }
                    case 10:
                        {
                            if (AxisToSafePosStep == 0)
                            {
                                if (StageArriveAutoChgPos && ProbeGantryArrChgPos)
                                {
                                    ProbeGantryArrChgPos = false;
                                    StageArriveAutoChgPos = false;
                                    if (Param.Control.PrbChgMode == PrbChgMode.Load)
                                    {
                                        LoadPrbStep = 1;
                                        UnloadPrbStep = -1;
                                        PrbAutoChgStep = 20;
                                    }
                                    else
                                    {
                                        LoadPrbStep = -1;
                                        UnloadPrbStep = 1;
                                        PrbAutoChgStep = 30;
                                    }
                                }
                                else if (Delay.TimeUp())
                                {
                                    OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->等待平台移动到安全位置超时", Brushes.Red);
                                    PrbAutoChgStep = 0;
                                    OnStop?.Invoke();
                                }
                            }
                            else if (AxisToSafePosStep == Global.ErrorStep)
                            {
                                PrbAutoChgStep = 0;
                            }
                            else
                            {
                                AxisToSafePos();
                            }
                            break;
                        }
                    case 20:
                        {
                            if (LoadPrbStep == 0)
                            {
                                PrbAutoChgStep = 0;
                                OnTriggerSignal?.Invoke((int)StationCommand.PrbAutoChgCom);
                            }
                            else
                            {
                                LoadPrbFlow();
                            }
                            break;
                        }
                    case 30:
                        {
                            if (UnloadPrbStep == 0)
                            {
                                if (Param.Control.PrbChgMode == PrbChgMode.UnloadLoad)
                                {
                                    LoadPrbStep = 1;
                                    PrbAutoChgStep = 20;
                                }
                                else
                                {
                                    PrbAutoChgStep = 0;
                                    OnTriggerSignal?.Invoke((int)StationCommand.PrbAutoChgCom);
                                }
                            }
                            else
                            {
                                UnloadPrbFlow();
                            }
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                OnStop?.Invoke();
                OnShowMsg?.Invoke("AutoRun", $"{StationName}->PrbAutoChgFlow方法执行异常，异常信息：{ex.ToString()}", Brushes.Red);
            }
        }
        /// <summary>
        /// 安装Probe流程
        /// </summary>
        private void LoadPrbFlow()
        {
            try
            {
                switch (LoadPrbStep)
                {
                    case 1:
                        {
                            Motion.MoveAbs(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Y, PrbAutoChgPosName.WaitPos);
                            Motion.Cylinder(CylinderName.PcbFixCyl,false);//Pcb固定气缸松开
                            OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->自动更换Y轴移动到进入料仓前位置", Brushes.Green);
                            LoadPrbStep = 5;
                            break;
                        }
                    case 5:
                        {
                            if (Motion.IsArrive(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Y, PrbAutoChgPosName.WaitPos) &&
                                Motion.IsArrive(StationEnum.ProbeGantry, AxisName.ProbeGantry_Y1, ProbeGantryPosName.AutoChgPos))
                            {
                                Motion.MoveAbs(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Z, PrbAutoChgPosName.PickPrbFromCabDownPos);
                                OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->自动更换Z轴移动到从料仓中取Probe位置", Brushes.Green);
                                LoadPrbStep = 10;
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Y))
                            {
                                OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->Probe自动更换Y轴移动到等待位置超时", Brushes.Red);
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.PrbAutoChg_YMoveTimeout);
                                LoadPrbStep = 1;
                            }
                            break;
                        }
                    case 10:
                        {
                            if (Motion.IsArrive(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Z, PrbAutoChgPosName.PickPrbFromCabDownPos))
                            {
                                Delay.Start(Param.Motion.AutoChgWaitCabTimeout);
                                OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->Y轴到达进入料仓前位置，等待料仓Z轴到位", Brushes.Green);
                                LoadPrbStep = 30;
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Z))
                            {
                                OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->自动更换Z轴移动到从料仓中取Probe位置超时", Brushes.Red);
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.PrbAutoChg_ZMoveTimeout);
                                LoadPrbStep = 1;
                            }
                            break;
                        }
                    case 30:
                        {
                            int rowNum = CabinetInfoHelper.GetRowNum(Global.ReceivedRecipeName);
                            if (Cabinet.CabArredSts == Status.OK && Motion.IsArrive(StationEnum.Cabinet, AxisName.CabGantry_Z1, GetPosName(rowNum)))
                            {
                                Cabinet.CabArredSts = Status.None;
                                LeftDetPrbSensor = $"CabDetectPrbLeft{rowNum + 1}F";
                                RightDetPrbSensor = $"CabDetectPrbRight{rowNum + 1}F";
                                OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->料仓到达取放Probe位置，判断料仓中是否有Probe", Brushes.Green);
                                LoadPrbStep = 35;
                            }
                            else if (Delay.TimeUp())
                            {
                                OnShowMsg?.Invoke("AutoRun", $"料仓移动到第{rowNum + 1}层超时", Brushes.Red);
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.CabGantry_Z1MoveTimeout);
                                LoadPrbStep = 10;
                            }
                            break;
                        }
                    case 35:
                        {
                            if ((Motion.GetDigitalInput(LeftDetPrbSensor) && Motion.GetDigitalInput(RightDetPrbSensor)))
                            {
                                Motion.MoveAbs(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Y, PrbAutoChgPosName.PickFromCabPos);
                                OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->料仓中有Probe，Y轴移动到取放Probe位置", Brushes.Green);
                                LoadPrbStep = 40;
                            }
                            else
                            {
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.CabDetSenserFail);
                                LoadPrbStep = 30;
                            }
                            break;
                        }
                    case 40:
                        {
                            if (Motion.IsArrive(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Y, PrbAutoChgPosName.PickFromCabPos))
                            {
                                Delay.Start(0.5);
                                OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->Y轴到达取放Probe位置，延时0.5秒", Brushes.Green);
                                LoadPrbStep = 45;
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Y))
                            {
                                OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->Y轴移动到取放Probe位置超时", Brushes.Red);
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.PrbAutoChg_YMoveTimeout);
                                Cabinet.CabArredSts = 0;
                                LoadPrbStep = 35;
                            }
                            break;
                        }
                    case 45:
                        {
                            if (Delay.TimeUp())
                            {
                                Motion.MoveAbs(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Z, PrbAutoChgPosName.PickPrbFromCabUpPos);
                                OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->Z轴上升，抬起Probe", Brushes.Green);
                                LoadPrbStep = 50;
                            }
                            break;
                        }
                    case 50:
                        {
                            if (Motion.IsArrive(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Z, PrbAutoChgPosName.PickPrbFromCabUpPos))
                            {
                                OnShowMsg?.Invoke("AutoRun", $"PrbAutoChg->Z轴上升到位，延时{Param.Motion.PrbAutoChgDetPrbTimeout}秒", Brushes.Green);
                                Delay.Start(Param.Motion.PrbAutoChgDetPrbTimeout);
                                LoadPrbStep = 60;
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Z))
                            {
                                OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->Z轴移动到抬起Probe位置超时", Brushes.Red);
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.PrbAutoChg_ZMoveTimeout);
                                LoadPrbStep = 40;
                            }
                            break;
                        }
                    case 60:
                        {
                            if (Delay.TimeUp())
                            {
                                if (Motion.GetDigitalInput(InputName.PrbAutoChgDetLeft) && Motion.GetDigitalInput(InputName.PrbAutoChgDetRight))
                                {
                                    Motion.MoveAbs(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Y, PrbAutoChgPosName.WaitPos);
                                    OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->Probe更换机构检测到Probe，Y轴退出料仓", Brushes.Green);
                                    LoadPrbStep = 80;
                                }
                                else
                                {
                                    AlarmManager.CreateAlarm((int)MotionAlarmCode.PrbAutoChgDetSenserFail);
                                    LoadPrbStep = 50;
                                }
                            }
                            break;
                        }
                    case 80:
                        {
                            if (Motion.IsArrive(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Y, PrbAutoChgPosName.WaitPos))
                            {
                                Motion.MoveAbs(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Z, PrbAutoChgPosName.SafePos);
                                OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->Y轴退出料仓完成，Z轴下降到初始位置", Brushes.Green);
                                PrbLoadSts = Status.OK;
                                LoadPrbStep = 90;
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Y))
                            {
                                OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->Y轴移动到安全位置超时", Brushes.Red);
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.PrbAutoChg_YMoveTimeout);
                                LoadPrbStep = 60;
                            }
                            break;
                        }
                    case 90:
                        {
                            if (Motion.IsArrive(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Z, PrbAutoChgPosName.SafePos))
                            {
                                OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->Z轴到达初始位置，零点定位系统松开", Brushes.Green);
                                UvwZero(false);
                                Delay.Start(Param.Motion.PrbZeroSysTime);
                                LoadPrbStep = 95;
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Z))
                            {
                                OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->Z轴移动到安全位置超时", Brushes.Red);
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.PrbAutoChg_ZMoveTimeout);
                                LoadPrbStep = 80;
                            }
                            break;
                        }
                    case 95:
                        {
                            Motion.MoveAbs(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Y, PrbAutoChgPosName.PickFromUvwPos);
                            OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->Y轴移动到从Uvw取放Probe位置", Brushes.Green);
                            LoadPrbStep = 100;
                            break;
                        }
                    case 100:
                        {
                            if (Motion.IsArrive(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Y, PrbAutoChgPosName.PickFromUvwPos))
                            {
                                Motion.MoveAbs(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Z, PrbAutoChgPosName.BeforeToUvwPos);
                                OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->Y轴到达从Uvw取放Probe位置，Z轴上升到变速位置", Brushes.Green);
                                LoadPrbStep = 110;
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Y))
                            {
                                OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->Y轴移动到从Uvw取放Probe位置超时", Brushes.Red);
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.PrbAutoChg_YMoveTimeout);
                                LoadPrbStep = 90;
                            }
                            break;
                        }
                    case 110:
                        {
                            if (Motion.IsArrive(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Z, PrbAutoChgPosName.BeforeToUvwPos))
                            {
                                if (Delay.TimeUp())
                                {
                                    Motion.MoveAbs(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Z, PrbAutoChgPosName.PickFromUvwPos);
                                    OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->Z轴到达变速位置，继续上升到从Uvw取放Probe位置", Brushes.Green);
                                    LoadPrbStep = 120;
                                }
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Z))
                            {
                                OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->Z轴上升到变速位置超时", Brushes.Red);
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.PrbAutoChg_ZMoveTimeout);
                                LoadPrbStep = 100;
                            }
                            break;
                        }
                    case 120:
                        {
                            if (Motion.IsArrive(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Z, PrbAutoChgPosName.PickFromUvwPos))
                            {
                                Delay.Start(Param.Motion.UvwDetPrbTimeout);
                                OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->Z轴到达从Uvw取放Probe位置，等待Uvw检测到Probe", Brushes.Green);
                                LoadPrbStep = 130;
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Z))
                            {
                                OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->Z轴上升到从Uvw取放Probe位置超时", Brushes.Red);
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.PrbAutoChg_ZMoveTimeout);
                                LoadPrbStep = 110;
                            }
                            break;
                        }
                    case 130:
                        {
                            if (Delay.TimeUp())
                            {
                                if (Motion.GetDigitalInput(InputName.UvwDetPrbLeft) && Motion.GetDigitalInput(InputName.UvwDetPrbRight))
                                {
                                    OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->Uvw检测到Probe，零点系统锁紧", Brushes.Green);
                                    UvwZero(true);
                                    Delay.Start(Param.Motion.PrbZeroSysTime);
                                    LoadPrbStep = 135;
                                }
                                else
                                {
                                    AlarmManager.CreateAlarm((int)MotionAlarmCode.UvwDetSenserFail);
                                    LoadPrbStep = 120;
                                }
                            }
                            break;
                        }
                    case 135:
                        {
                            if (Delay.TimeUp())
                            {
                                Motion.Cylinder(CylinderName.PcbFixCyl,true);
                                Motion.MoveAbs(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Z, PrbAutoChgPosName.AfterUvwPos);
                                OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->Pcb固定气缸锁紧Pcb板，Z轴下降变速位置", Brushes.Green);
                                LoadPrbStep = 140;
                            }
                            break;
                        }
                    case 140:
                        {
                            if (Motion.IsArrive(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Z, PrbAutoChgPosName.AfterUvwPos) &&
                                Motion.IsCylinderArrive(CylinderName.PcbFixCyl, true))
                            {
                                Delay.Start(2);
                                OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->Z轴到达变速位置，延时2秒，确认Uvw零点系统是否锁紧Probe", Brushes.Green);
                                LoadPrbStep = 150;
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Z))
                            {
                                OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->Z轴下降变速位置超时", Brushes.Red);
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.PrbAutoChg_ZMoveTimeout);
                                LoadPrbStep = 130;
                            }
                            else if (Motion.IsCylinderTimeOut(CylinderName.PcbFixCyl))
                            {
                                OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->PCB固定气缸锁紧超时", Brushes.Red);
                                OnPause?.Invoke(-1);
                                LoadPrbStep = 130;
                            }
                            break;
                        }
                    case 150:
                        {
                            if (Delay.TimeUp())
                            {
                                if ((Motion.GetDigitalInput(InputName.UvwDetPrbLeft) && Motion.GetDigitalInput(InputName.UvwDetPrbRight)))
                                {
                                    Motion.MoveAbs(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Z, PrbAutoChgPosName.SafePos);
                                    OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->Uvw零点系统锁紧Probe，Z轴回初始位置", Brushes.Green);
                                    LoadPrbStep = 160;
                                }
                                else
                                {
                                    AlarmManager.CreateAlarm((int)MotionAlarmCode.UvwDetSenserFail);
                                    LoadPrbStep = -1000;
                                }
                            }
                            break;
                        }
                    case 160:
                        {
                            if (Motion.IsArrive(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Z, PrbAutoChgPosName.SafePos))
                            {
                                Motion.MoveAbs(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Y, PrbAutoChgPosName.SafePos);
                                OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->Z轴到达初始位置，Y轴回初始位置", Brushes.Green);
                                ProbeCardHelper.UpdateStatus(ProbeCardSts.InUvw, Global.ReceivedRecipeName);
                                LoadPrbStep = 170;
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Z))
                            {
                                OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->Z轴回初始位置置超时", Brushes.Red);
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.PrbAutoChg_ZMoveTimeout);
                                LoadPrbStep = 150;
                            }
                            break;
                        }
                    case 170:
                        {
                            if (Motion.IsArrive(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Y, PrbAutoChgPosName.SafePos))
                            {
                                OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->Y轴到达初始位置", Brushes.Green);
                                LoadPrbStep = 0;
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Y))
                            {
                                OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->Y轴回初始位置置超时", Brushes.Red);
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.PrbAutoChg_YMoveTimeout);
                                LoadPrbStep = 160;
                            }
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                AlarmManager.CreateAlarm((int)MotionAlarmCode.ProbeChangeStationException);
                LogManager.GetLogger("AutoRun").Fatal(ex);
            }
        }
        /// <summary>
        /// Uvw平台零点定位系统,true:锁紧，false:松开
        /// </summary>
        private void UvwZero(bool onOff)
        {
            if (onOff)
            {
                Motion.SetDigitalOutput(OutputName.UvwZeroSysUnLock, false);
                Thread.Sleep(500);
                Motion.SetDigitalOutput(OutputName.UvwZeroSysLock, true);
            }
            else
            {
                Motion.SetDigitalOutput(OutputName.UvwZeroSysLock, false);
                Thread.Sleep(500);
                Motion.SetDigitalOutput(OutputName.UvwZeroSysUnLock, true);
            }
        }

        /// <summary>
        /// 卸载Probe流程
        /// </summary>
        private void UnloadPrbFlow()
        {
            try
            {
                switch (UnloadPrbStep)
                {
                    case 1:
                        {
                            int cabinetRow = CabinetInfoHelper.GetRowNum(RecipeManager.CurRecipe.RecipeID);
                            string leftDetPrbSensor = $"CabDetectPrbLeft{cabinetRow + 1}F";
                            string rightDetPrbSensor = $"CabDetectPrbRight{cabinetRow + 1}F";
                            if (Motion.GetDigitalInput(leftDetPrbSensor) && Motion.GetDigitalInput(rightDetPrbSensor))
                            {
                                OnShowMsg?.Invoke("AutoRun", $"Cabinet->料仓第{cabinetRow+1}层检测到Probe，请确认料仓和Probe绑定信息是否正确", Brushes.Red);
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.CabinetDetectProbe);
                                UnloadPrbStep = -1;
                            }
                            else
                            {
                                Motion.MoveAbs(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Y, PrbAutoChgPosName.PickFromUvwPos);
                                OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->Y轴移动到Uvw下方位置", Brushes.Green);
                                UnloadPrbStep = 5;
                            }
                            break;
                        }
                    case 5:
                        {
                            if (Motion.IsArrive(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Y, PrbAutoChgPosName.PickFromUvwPos))
                            {
                                OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->Y轴到达Uvw下方位置，Z轴上升到变速位置", Brushes.Green);
                                Motion.MoveAbs(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Z, PrbAutoChgPosName.BeforeToUvwPos);
                                UnloadPrbStep = 10;
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Y))
                            {
                                OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->Y轴移动到Uvw下方位置超时", Brushes.Red);
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.PrbAutoChg_YMoveTimeout);
                                UnloadPrbStep = 1;
                            }
                            break;
                        }
                    case 10:
                        {
                            if (Motion.IsArrive(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Z, PrbAutoChgPosName.BeforeToUvwPos))
                            {
                                Motion.MoveAbs(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Z, PrbAutoChgPosName.PickFromUvwPos);
                                OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->Z轴到达变速位置，Z轴继续上升到从Uvw取放Prb位置", Brushes.Green);
                                UnloadPrbStep = 20;
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Z))
                            {
                                OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->Z轴上升到变速位置超时", Brushes.Red);
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.PrbAutoChg_ZMoveTimeout);
                                UnloadPrbStep = 5;
                            }
                            break;
                        }
                    case 20:
                        {
                            if (Motion.IsArrive(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Z, PrbAutoChgPosName.PickFromUvwPos))
                            {
                                Motion.Cylinder(CylinderName.PcbFixCyl, false);
                                OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->Z轴到达从Uvw取放Prb位置，Pcb固定气缸松开", Brushes.Green);
                                UnloadPrbStep = 25;
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Z))
                            {
                                OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->Z轴上升到从Uvw取放Prb位置超时", Brushes.Red);
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.PrbAutoChg_ZMoveTimeout);
                                UnloadPrbStep = 10;
                            }
                            break;
                        }
                    case 25:
                        {
                            if (Motion.IsCylinderArrive(CylinderName.PcbFixCyl, false))
                            {
                                UvwZero(false);
                                Delay.Start(Param.Motion.PrbZeroSysTime);
                                OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->Pcb固定轴松开，零点定位系统松开，等待自动更换模组检测到Probe", Brushes.Green);
                                UnloadPrbStep = 30;
                            }
                            else if (Motion.IsCylinderTimeOut(CylinderName.PcbFixCyl))
                            {
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.PcbFixCylMoveTimeout);
                                UnloadPrbStep = 20;
                            }
                            break;
                        }
                    case 30:
                        {
                            if (Delay.TimeUp())
                            {
                                //判断感应器有没有感应到Probe
                                if (Motion.GetDigitalInput(InputName.PrbAutoChgDetLeft) && Motion.GetDigitalInput(InputName.PrbAutoChgDetRight))
                                {
                                    Motion.MoveAbs(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Z, PrbAutoChgPosName.AfterUvwPos);
                                    OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->Z轴下降到变速位置", Brushes.Green);
                                    UnloadPrbStep = 40;
                                }
                                else//如果没有感应到Probe，Z轴重新回到变速位置，并报错
                                {
                                    Motion.MoveAbs(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Z, PrbAutoChgPosName.BeforeToUvwPos);
                                    AlarmManager.CreateAlarm((int)MotionAlarmCode.PrbAutoChgDetSenserFail);
                                    UnloadPrbStep = 10;
                                }
                            }
                            break;
                        }
                    case 40:
                        {
                            if (Motion.IsArrive(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Z, PrbAutoChgPosName.AfterUvwPos))
                            {
                                Delay.Start(1.5);
                                OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->Z轴到达变速位置，延时1.5秒，确认Probe是否脱离Uvw", Brushes.Green);
                                UnloadPrbStep = 45;
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Z))
                            {
                                OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->Z轴下降到变速位置超时", Brushes.Red);
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.PrbAutoChg_ZMoveTimeout);
                                UnloadPrbStep = 30;
                            }
                            break;
                        }
                    case 45:
                        {
                            if (Delay.TimeUp())
                            {
                                if (Motion.GetDigitalInput(InputName.PrbAutoChgDetLeft) && Motion.GetDigitalInput(InputName.PrbAutoChgDetRight))
                                {
                                    Motion.MoveAbs(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Z, PrbAutoChgPosName.SafePos);
                                    OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->Probe完全脱离Uvw，Z轴继续下降到初始位置", Brushes.Green);
                                    UnloadPrbStep = 50;
                                }
                                else
                                {
                                    AlarmManager.CreateAlarm((int)MotionAlarmCode.PrbAutoChgDetSenserFail);
                                    UnloadPrbStep = 30;
                                }
                            }
                            break;
                        }
                    case 50:
                        {
                            if (Motion.IsArrive(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Z, PrbAutoChgPosName.SafePos))
                            {
                                Motion.MoveAbs(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Y, PrbAutoChgPosName.WaitPos);
                                OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->Z轴到达初始位置，Y轴去进入料仓前位置", Brushes.Green);
                                UnloadPrbStep = 60;
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Z))
                            {
                                OnShowMsg?.Invoke("AutoRun", $"Z轴去放置Probe位置超时", Brushes.Red);
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.PrbAutoChg_ZMoveTimeout);
                                UnloadPrbStep = 40;
                            }
                            break;
                        }
                    case 60:
                        {
                            if (Motion.IsArrive(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Y, PrbAutoChgPosName.WaitPos))
                            {
                                Motion.MoveAbs(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Z, PrbAutoChgPosName.PutPrbToCabUpPos);
                                OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->Y轴到达进入料仓前位置，Z轴去放置Probe位置，并等待料仓到位", Brushes.Green);
                                Delay.Start(Param.Motion.AutoChgWaitCabTimeout);
                                UnloadPrbStep = 70;
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Y))
                            {
                                OnShowMsg?.Invoke("AutoRun", $"Y轴去进入料仓前位置超时", Brushes.Red);
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.PrbAutoChg_YMoveTimeout);
                                UnloadPrbStep = 50;
                            }
                            break;
                        }
                    case 70:
                        {
                            if (Motion.IsArrive(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Z, PrbAutoChgPosName.PutPrbToCabUpPos))
                            {
                                int rowNum = CabinetInfoHelper.GetRowNum(RecipeManager.CurRecipe.RecipeID);
                                if (Cabinet.CabArredSts == 0 && Motion.IsArrive(StationEnum.Cabinet, AxisName.CabGantry_Z1, GetPosName(rowNum)))
                                {
                                    Cabinet.CabArredSts = Status.None;
                                    Motion.MoveAbs(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Y, PrbAutoChgPosName.PickFromCabPos);
                                    OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->Z轴到达放置Probe位置，Y轴移动到取放Prb位置", Brushes.Green);
                                    UnloadPrbStep = 90;
                                }
                                else if (Delay.TimeUp())
                                {
                                    OnShowMsg?.Invoke("AutoRun", $"料仓移动到第{rowNum + 1}层超时", Brushes.Red);
                                    AlarmManager.CreateAlarm((int)MotionAlarmCode.CabGantry_Z1MoveTimeout);
                                    UnloadPrbStep = 70;
                                }
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Z))
                            {
                                OnShowMsg?.Invoke("AutoRun", $"Z轴去放置Probe位置超时", Brushes.Red);
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.PrbAutoChg_ZMoveTimeout);
                                UnloadPrbStep = 60;
                            }
                            break;
                        }

                    case 90:
                        {
                            if (Motion.IsArrive(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Y, PrbAutoChgPosName.PickFromCabPos))
                            {
                                Motion.MoveAbs(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Z, PrbAutoChgPosName.PutPrbToCabDownPos);
                                OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->Y轴到达取放Prb位置，Z轴下降，放下Probe", Brushes.Green);
                                UnloadPrbStep = 100;
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Y))
                            {
                                OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->Y轴移动到取放Prb位置超时", Brushes.Red);
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.PrbAutoChg_YMoveTimeout);
                                Cabinet.CabArredSts = Status.OK;
                                UnloadPrbStep = 80;
                            }
                            break;
                        }
                    case 100:
                        {
                            if (Motion.IsArrive(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Z, PrbAutoChgPosName.PutPrbToCabDownPos))
                            {
                                int rowNum = CabinetInfoHelper.GetRowNum(RecipeManager.CurRecipe.RecipeID);
                                Delay.Start(Param.Motion.CabDetPrbTimeout);
                                LeftDetPrbSensor = $"CabDetectPrbLeft{rowNum + 1}F";
                                RightDetPrbSensor = $"CabDetectPrbRight{rowNum + 1}F";
                                OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->等待料仓感应器检测到Probe", Brushes.Green);
                                UnloadPrbStep = 110;
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Z))
                            {
                                OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->Z轴移动到放下Probe位置超时", Brushes.Red);
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.PrbAutoChg_ZMoveTimeout);
                                UnloadPrbStep = 90;
                            }
                            break;
                        }
                    case 110:
                        {
                            if (Delay.TimeUp())
                            {
                                if ((Motion.GetDigitalInput(LeftDetPrbSensor) && Motion.GetDigitalInput(RightDetPrbSensor)))
                                {
                                    Motion.MoveAbs(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Y, PrbAutoChgPosName.WaitPos);
                                    OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->料仓检测到Probe，Y轴退出料仓", Brushes.Green);
                                    UnloadPrbStep = 130;
                                }
                                else
                                {
                                    AlarmManager.CreateAlarm((int)MotionAlarmCode.CabDetSenserFail);
                                    UnloadPrbStep = 100;
                                }
                            }

                            break;
                        }
                    case 130:
                        {
                            if (Motion.IsArrive(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Y, PrbAutoChgPosName.WaitPos))
                            {
                                PrbUnloadSts = Status.OK;
                                Motion.MoveAbs(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Y, PrbAutoChgPosName.SafePos);
                                Motion.MoveAbs(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Z, PrbAutoChgPosName.SafePos);
                                OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->Y轴到达等待位置，Y轴和Z轴返回初始位置", Brushes.Green);
                                UnloadPrbStep = 140;
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Y))
                            {
                                OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->Y轴回初始位置超时", Brushes.Red);
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.PrbAutoChg_YMoveTimeout);
                                UnloadPrbStep = 110;
                            }
                            break;
                        }
                    case 140:
                        {
                            if (Motion.IsArrive(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Y, PrbAutoChgPosName.SafePos) && Motion.IsArrive(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Z, PrbAutoChgPosName.SafePos))
                            {
                                OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->Y轴和Z轴到达初始位置，更新ProbeCard状态", Brushes.Green);
                                ProbeCardHelper.UpdateStatus(ProbeCardSts.OutUvw, RecipeManager.CurRecipe.RecipeID);
                                OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->更新ProbeCard状态完成", Brushes.Green);
                                UnloadPrbStep = 0;
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Y) )
                            {
                                OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->Y轴回初始位置超时", Brushes.Red);
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.PrbAutoChg_YMoveTimeout);
                                UnloadPrbStep = 130;
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Z))
                            {
                                OnShowMsg?.Invoke("AutoRun", "PrbAutoChg->Z轴回初始位置超时", Brushes.Red);
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.PrbAutoChg_ZMoveTimeout);
                                UnloadPrbStep = 130;
                            }
                            break;
                        }
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                AlarmManager.CreateAlarm((int)MotionAlarmCode.ProbeChangeStationException);
                LogManager.GetLogger("AutoRun").Fatal(ex);
            }
        }

        /// <summary>
        /// 获取料仓点位名称
        /// </summary>
        /// <param name="type">0：自动更换时料仓点位名称，1：上料时料仓点位名称</param>
        /// <param name="rowNum"></param>
        /// <returns></returns>
        private CabinetPosName GetPosName( int rowNum)
        {
            CabinetPosName ret = CabinetPosName.SafePos;        
            string name = $"Cab{rowNum + 1}FPos";
            ret = EnumHelper.GetEnumValue<CabinetPosName>(name);         
            return ret;
        }

        public bool Stop()
        {
            return true;
        }

        public bool ResetBits()
        {
            AxisToSafePosStep = 1;
            PrbLoadSts = Status.None;
            PrbUnloadSts = Status.None;
            HomeStep = -1;
            PrbAutoChgStep = -1;
            LoadPrbStep = -1;
            UnloadPrbStep = -1;

            CabinetZHomeCom = false;
            StageArriveAutoChgPos = false;
            return true;
        }

        public bool RegisterEvent()
        {
            Cabinet.OnTriggerSignal += Cabinet_OnTriggerSignal;
            DualStage.OnTriggerSignal += DualStage_OnTriggerSignal;
            ProbeGantry.OnTriggerSignal += ProbeGantry_OnTriggerSignal;
            return true;
        }


        private void ProbeGantry_OnTriggerSignal(int obj)
        {
            switch (obj)
            {
                case (int)StationCommand.ProbeGantryArrChgPos:
                    {
                        ProbeGantryArrChgPos = true;
                        break;
                    }
            }
        }

        private void DualStage_OnTriggerSignal(int obj)
        {
            switch (obj)
            {
                case (int)StationCommand.StageArriveAutoChgPos:
                    {
                        StageArriveAutoChgPos = true;
                        break;
                    }
                case (int)StationCommand.PrbAutoChg:
                    {
                        ProbeAutoChgBit = true;
                        break;
                    }
            }
        }

        private void Cabinet_OnTriggerSignal(int obj)
        {
            switch (obj)
            {
                case (int)StationCommand.CabinetZHomeCom:
                    {
                        CabinetZHomeCom = true;
                        break;
                    }
            }
        }

        public void OnButtonTrigger(IOInfo io, bool sts)
        {

        }

        public bool Unint()
        {
            return true;
        }
    }
    /// <summary>
    /// Cabinet工位点位名称
    /// </summary>
    public enum PrbAutoChgPosName
    {
        /// <summary>
        /// 每个轴的安全位置
        /// </summary>
        SafePos,
        /// <summary>
        /// Y、Z轴从料仓中取放Probe的位置
        /// </summary>
        PickFromCabPos,
        /// <summary>
        /// Z轴放置ProbeCard到料仓中的位置
        /// </summary>
        PutPrbToCabUpPos,
        /// <summary>
        /// Y、Z轴从UVW平台取放ProbeCard的位置
        /// </summary>
        PickFromUvwPos,
        /// <summary>
        /// Z轴从Uvw平台慢速下降的位置
        /// </summary>
        AfterUvwPos,
        /// <summary>
        /// Y轴进入料仓前等待位置
        /// </summary>
        WaitPos,
        /// <summary>
        /// Z轴放置ProbeCard到料仓中的位置
        /// </summary>
        PutPrbToCabDownPos,
        /// <summary>
        /// 自动更换Z轴快速上升到Uvw之前的位置
        /// </summary>
        BeforeToUvwPos,
        PickPrbFromCabUpPos,
        PickPrbFromCabDownPos,
       /// <summary>
       /// 自动换型Y轴回零时，Z轴避让位置，防止撞到Probe
       /// </summary>
        HomeSafePos,

    }
}


