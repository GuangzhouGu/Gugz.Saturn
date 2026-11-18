/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2020 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnMain.Station
// 文件名称    ：Cabinet.cs
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
using System.Windows.Forms;
using HYC.SaturnMain.Alarm;
using HYC.HTPCBase.Param;
using HYC.SaturnMain;

namespace HYC.SaturnMain.Station
{
    public class Cabinet : IStation
    {
        public static event Action<int> OnTriggerSignal;

        public static Status CabArredSts = Status.None;

        #region IStation接口实现
        public MotionManager Motion { get; set; } = MotionManager.GetInstance();
        public int StationNum { get; set; } = 4;
        public string StationName { get; set; } = "Cabinet";
        public int AutoRunStep { get; set; }
        public int HomeStep { get; set; }
        #endregion
        public static CabinetPosName CabPosName = 0;
        public event Action<HomeEventArgs> OnHomeStatus;
        public event Action<string, string, Brush> OnShowMsg;
        public event Action OnStop;
        public event Action<int> OnPause;
        public static event Action OnBufferToCabinetFlowCom;
        public static event Action OnCabinetToBufferFlowCom;
        #region 状态机步骤
        private int AxisToSafePosStep = -1;//判断轴是否在安全位置流程状态机步骤 
        public int PrbAutoChgStep = -1;
        public int LoadPrbStep = -1;
        public int UnloadPrbStep = -1;

        #endregion
        private Delay Delay = new Delay();
        private bool PrbAutoChgYArriveSafePos = false;
        private bool ProbeAutoChgBit = false;
        public bool Init()
        {
            return true;
        }
        public bool Home()
        {
            switch (HomeStep)
            {
                case 1:
                    {
                        ResetBits();
                        OnShowMsg?.Invoke("AutoRun", "Cabinet->料仓工位开始回零", Brushes.Green);
                        if (Motion.GetDigitalInput(InputName.PrbLoaderDetectPrb1) && Motion.GetDigitalInput(InputName.PrbLoaderDetectPrb2) && Motion.GetDigitalInput(InputName.PrbBufferDetectPrb1) && Motion.GetDigitalInput(InputName.PrbBufferDetectPrb2))
                        {
                            OnShowMsg?.Invoke("AutoRun", "Cabinet->上料平台和缓存平台检测到Probe，零点定位系统松开", Brushes.Green);
                            CabinetZero(false);
                            Delay.Start(Param.Motion.PrbZeroSysTime);
                            HomeStep = 5;
                        }
                        else if (Motion.GetDigitalInput(InputName.PrbLoaderDetectPrb1) || Motion.GetDigitalInput(InputName.PrbLoaderDetectPrb2) && (!Motion.GetDigitalInput(InputName.PrbBufferDetectPrb1) && !Motion.GetDigitalInput(InputName.PrbBufferDetectPrb2)))
                        {
                            StationManager.Instance.Stop();
                            MessageBox.Show("Probe上料平台感应到Probe，请把Probe移走");
                            OnShowMsg?.Invoke("AutoRun", "Cabinet->Probe上料平台感应到Probe，请把Probe移走", Brushes.Red);
                            HomeStep = 1000;
                        }
                        else
                        {
                            OnShowMsg?.Invoke("AutoRun", "Cabinet->Probe上料平台没有Probe，Probe上料平台Z轴抬升15毫米", Brushes.Green);
                            Motion.MoveRel(StationEnum.Cabinet, AxisName.PrbLoader_Z, -50);
                            Thread.Sleep(200);
                            HomeStep = 20;
                        }
                        break;
                    }
                case 5:
                    {
                        if (Delay.TimeUp())
                        {
                            Motion.MoveRel(StationEnum.Cabinet, AxisName.PrbLoader_Z, -50);
                            Thread.Sleep(200);
                            OnShowMsg?.Invoke("AutoRun", "Cabinet->Probe上料平台Z轴抬升30毫米，脱离Probe", Brushes.Green);
                            HomeStep = 10;
                        }
                        break;
                    }
                case 10:
                    {
                        if (Motion.IsArrive(StationEnum.Cabinet, AxisName.PrbLoader_Z) || Motion.IsNegLimit(StationEnum.Cabinet, AxisName.PrbLoader_Z))
                        {
                            if (!Motion.GetDigitalInput(InputName.PrbLoaderDetectPrb1) && !Motion.GetDigitalInput(InputName.PrbLoaderDetectPrb2))
                            {
                                Delay.Start(60);
                                Motion.Jog(StationEnum.Cabinet, AxisName.PrbLoader_Y1, -1);
                                Motion.Jog(StationEnum.Cabinet, AxisName.PrbLoader_Y2, -1);
                                OnShowMsg?.Invoke("AutoRun", "Cabinet->Probe上料平台Y轴移动到负限位", Brushes.Green);
                                HomeStep = 30;
                            }
                            else
                            {
                                MessageBox.Show("Probe上料平台没有脱离Probe，请确认！");
                                OnShowMsg?.Invoke("AutoRun", "Cabinet->Probe上料平台没有脱离Probe，请确认！", Brushes.Red);
                                HomeStep = 1000;
                            }
                        }
                        else if (Motion.IsMoveTimeOut(StationEnum.Cabinet, AxisName.PrbLoader_Z))
                        {
                            OnShowMsg?.Invoke("AutoRun", "Cabinet->Probe上料平台Z轴移动超时", Brushes.Red);
                            HomeStep = 1000;
                        }
                        break;
                    }
                case 20:
                    {
                        if (Motion.IsArrive(StationEnum.Cabinet, AxisName.PrbLoader_Z) || Motion.IsNegLimit(StationEnum.Cabinet, AxisName.PrbLoader_Z))
                        {
                            if (Para.PCBase.IsSimulator == false)
                            {
                                Delay.Start(60);
                                Motion.Jog(StationEnum.Cabinet, AxisName.PrbLoader_Y1, -1);
                                Motion.Jog(StationEnum.Cabinet, AxisName.PrbLoader_Y2, -1);
                            }
                            OnShowMsg?.Invoke("AutoRun", "Cabinet->Probe上料平台Y轴移动到负限位", Brushes.Green);
                            HomeStep = 30;
                        }
                        else if (Motion.IsMoveTimeOut(StationEnum.Cabinet, AxisName.PrbLoader_Z))
                        {
                            OnShowMsg?.Invoke("AutoRun", "Cabinet->Probe上料平台Z轴移动超时", Brushes.Red);
                            HomeStep = 1000;
                        }
                        break;
                    }
                case 30:
                    {
                        if (Motion.IsNegLimit(StationEnum.Cabinet, AxisName.PrbLoader_Y1) && Motion.IsNegLimit(StationEnum.Cabinet, AxisName.PrbLoader_Y2) || Para.PCBase.IsSimulator)
                        {
                            Motion.Home(StationEnum.Cabinet, AxisName.PrbLoader_Z);
                            Delay.Start(180);//等待Probe更换模组Y轴回零完成时间
                            OnShowMsg?.Invoke("AutoRun", "Cabinet->Probe上料平台Y轴到达负限位，Probe上料平台Z轴开始回零", Brushes.Green);
                            HomeStep = 40;
                        }
                        else if (Delay.TimeUp())
                        {
                            OnShowMsg?.Invoke("AutoRun", "Cabinet->Probe上料平台Y轴回零超时", Brushes.Red);
                            HomeStep = 1000;
                        }
                        break;
                    }
                case 40:
                    {
                        if (PrbAutoChgYArriveSafePos)
                        {
                            PrbAutoChgYArriveSafePos = false;
                            if (CabinetIsCanMove())
                            {
                                Motion.Home(StationEnum.Cabinet, AxisName.CabGantry_Z1);
                                OnShowMsg?.Invoke("AutoRun", "Cabinet->Probe自动更换Y轴到达安全位置，料仓龙门Z轴开始回零", Brushes.Green);
                                HomeStep = 50;
                            }
                        }
                        else if (Delay.TimeUp())
                        {
                            OnShowMsg?.Invoke("AutoRun", "Cabinet->Probe自动更换Y轴回零超时", Brushes.Red);
                            HomeStep = 1000;
                        }
                        break;
                    }
                case 50:
                    {
                        if (Motion.IsHomeDone(StationEnum.Cabinet, AxisName.CabGantry_Z1))
                        {
                            var info = AxisInfoHelper.GetAxisInfoByName("CabGantry_Z2");
                            info.IsHomeOK = true;
                            Motion.MoveAbs(StationEnum.Cabinet, AxisName.CabGantry_Z1,CabinetPosName.HomeSafePos);
                            OnShowMsg?.Invoke("AutoRun", "Cabinet->料仓龙门回零完成，料仓龙门移动到安全位置", Brushes.Green);
                            HomeStep = 60;
                        }
                        else if (Motion.IsMoveTimeOut(StationEnum.Cabinet, AxisName.CabGantry_Z1))
                        {
                            OnShowMsg?.Invoke("AutoRun", "Cabinet->料仓龙门Z轴回零超时", Brushes.Red);
                            HomeStep = 1000;
                        }
                        break;
                    }
                case 60:
                    {
                        if (Motion.IsHomeDone(StationEnum.Cabinet, AxisName.PrbLoader_Z))
                        {
                            if (Motion.IsArrive(StationEnum.Cabinet, AxisName.CabGantry_Z1, CabinetPosName.HomeSafePos))
                            {
                                OnTriggerSignal?.Invoke((int)StationCommand.CabinetZHomeCom);
                                Motion.Home(StationEnum.Cabinet, AxisName.PrbLoader_Y1);
                                Motion.Home(StationEnum.Cabinet, AxisName.PrbLoader_Y2);
                                Motion.Home(StationEnum.Cabinet, AxisName.PrbBuffer_Y);
                                OnShowMsg?.Invoke("AutoRun", "Cabinet->Probe上料平台Z轴和料仓龙门回零完成，Probe缓存平台Y轴和Probe上料平台Y轴开始回零", Brushes.Green);
                                HomeStep = 70;
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.Cabinet, AxisName.CabGantry_Z1))
                            {
                                OnShowMsg?.Invoke("AutoRun", "Cabinet->料仓龙门移动到安全位置超时", Brushes.Red);
                                HomeStep = 1000;
                            }
                        }
                        else if (Motion.IsMoveTimeOut(StationEnum.Cabinet, AxisName.PrbLoader_Z))
                        {
                            OnShowMsg?.Invoke("AutoRun", "Cabinet->Probe上料平台Z轴回零超时", Brushes.Red);
                            HomeStep = 1000;
                        }
                        break;
                    }

                case 70:
                    {
                        if (Motion.IsHomeDone(StationEnum.Cabinet, AxisName.PrbLoader_Y1) && Motion.IsHomeDone(StationEnum.Cabinet, AxisName.PrbLoader_Y2))
                        {
                            if (Motion.IsHomeDone(StationEnum.Cabinet, AxisName.PrbBuffer_Y))
                            {
                                Motion.MoveAbs(StationEnum.Cabinet, AxisName.PrbBuffer_Y, CabinetPosName.PrbBuffStgLdPos);
                                OnShowMsg?.Invoke("AutoRun", "Cabinet->缓存平台Y轴和Probe上料平台Y轴回零完成，缓存平台Y轴去安全位置", Brushes.Green);
                                HomeStep = 80;
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.Cabinet, AxisName.PrbBuffer_Y))
                            {
                                OnShowMsg?.Invoke("AutoRun", "Cabinet->Probe缓存平台Y轴回零超时", Brushes.Red);
                                HomeStep = 1000;
                            }
                        }
                        else if (Motion.IsMoveTimeOut(StationEnum.Cabinet, AxisName.PrbLoader_Y1))
                        {
                            OnShowMsg?.Invoke("AutoRun", "Cabinet->Probe上料平台Y轴回零超时", Brushes.Red);
                            HomeStep = 1000;
                        }
                        break;
                    }
                case 80:
                    {
                        if (Motion.IsArrive(StationEnum.Cabinet, AxisName.PrbBuffer_Y, CabinetPosName.PrbBuffStgLdPos))
                        {
                            Motion.MoveAbs(StationEnum.Cabinet, AxisName.PrbLoader_Z, CabinetPosName.SafePos);
                            OnShowMsg?.Invoke("AutoRun", "Cabinet->缓存平台Y轴到达安全位置，Probe上料平台Z轴下降到等待位置", Brushes.Green);
                            HomeStep = 90;
                        }
                        else if (Motion.IsMoveTimeOut(StationEnum.Cabinet, AxisName.PrbBuffer_Y))
                        {
                            OnShowMsg?.Invoke("AutoRun", "Cabinet->缓存平台Y轴去安全位置超时", Brushes.Red);
                            HomeStep = 1000;
                        }
                        break;
                    }
                case 90:
                    {
                        if (Motion.IsArrive(StationEnum.Cabinet, AxisName.PrbLoader_Z, CabinetPosName.SafePos))
                        {
                            OnShowMsg?.Invoke("AutoRun", "Cabinet->Probe上料平台Z轴到达等待位置，料仓工位回零完成", Brushes.Green);
                            OnHomeStatus?.Invoke(new HomeEventArgs { StationName = StationName, StationNum = StationNum, HomeResult = true });
                            HomeStep = 0;
                        }
                        else if (Motion.IsMoveTimeOut(StationEnum.Cabinet, AxisName.PrbLoader_Z))
                        {
                            OnShowMsg?.Invoke("AutoRun", "Cabinet->Probe缓存Y轴轴回零超时", Brushes.Red);
                            HomeStep = 1000;
                        }
                        break;
                    }
                case 1000:
                    {
                        AlarmManager.CreateAlarm((int)MotionAlarmCode.CabinetStaHomeFail);
                        OnHomeStatus?.Invoke(new HomeEventArgs { StationName = StationName, StationNum = StationNum, HomeResult = false });
                        HomeStep = -1;
                        OnStop?.Invoke();
                        break;
                    }
            }
            return true;
        }

        public bool RegisterEvent()
        {
            PrbAutoChg.OnTriggerSignal += PrbAutoChg_OnTriggerSignal;
            DualStage.OnTriggerSignal += DualStage_OnTriggerSignal;
            return true;
        }

        private void DualStage_OnTriggerSignal(int obj)
        {
            switch (obj)
            {
                case (int)StationCommand.PrbAutoChg:
                    {
                        ProbeAutoChgBit = true;
                        break;
                    }
            }
        }

        private void PrbAutoChg_OnTriggerSignal(int obj)
        {
            switch (obj)
            {
                case (int)StationCommand.PrbAutoChgYArriveSafePos:
                    {
                        PrbAutoChgYArriveSafePos = true;
                        break;
                    }
            }
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
                            OnShowMsg?.Invoke("AutoRun", "Cabinet->开始自动更换Probe", Brushes.Green);
                            PrbAutoChgStep = 1;
                            ProbeAutoChgBit = false;
                            AutoRunStep = 20;
                        }
                        break;
                    }
                case 20:
                    {
                        if (PrbAutoChgStep == 0)
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
        private void AxisToSafePos()
        {
            switch (AxisToSafePosStep)
            {
                case 1:
                    {
                        if (Motion.IsArrive(StationEnum.Cabinet, AxisName.PrbLoader_Y1, CabinetPosName.PrbModuleYClampPrbFromBuffPos) ||
                            Motion.IsArrive(StationEnum.Cabinet, AxisName.PrbLoader_Y2, CabinetPosName.PrbModuleYClampPrbFromBuffPos))
                        {
                            if (Motion.IsArrive(StationEnum.Cabinet, AxisName.PrbBuffer_Y, CabinetPosName.PrbBuffStgLdPos))
                            {
                                if (Motion.IsArrive(StationEnum.Cabinet, AxisName.PrbLoader_Z, CabinetPosName.SafePos))
                                {
                                    AxisToSafePosStep = 1000;
                                }
                                else
                                {
                                    Motion.MoveAbs(StationEnum.Cabinet, AxisName.PrbLoader_Z, CabinetPosName.SafePos);
                                    AxisToSafePosStep = 30;
                                    OnShowMsg?.Invoke("AutoRun", "Cabinet->Probe上料平台Z轴回初始位置", Brushes.Green);
                                }
                            }
                            else
                            {
                                var posInfo = AxisInfoHelper.GetPosInfoByName(AxisName.PrbLoader_Z, CabinetPosName.SafePos);
                                LoaderZTempPos = posInfo.Pos -Param.Motion.LoaderZPreventCrushPos;
                                Motion.MoveAbs(StationEnum.Cabinet, AxisName.PrbLoader_Z, LoaderZTempPos);
                                AxisToSafePosStep = 40;
                                OnShowMsg?.Invoke("AutoRun", "Cabinet->缓存平台Y轴不在安全位置，Probe上料平台Z轴先移动到避让位置", Brushes.Green);
                            }
                        }
                        else
                        {
                            PosInfo pos = AxisInfoHelper.GetPosInfoByName(AxisName.PrbLoader_Z, CabinetPosName.PickPrbFromBufferPos);
                            if (Motion.GetAxisActPos(StationEnum.Cabinet, AxisName.PrbLoader_Z) > pos.Pos - 15)
                            {
                                OnShowMsg?.Invoke("AutoRun", "Cabinet->Probe上料平台Z轴在缓存平台取料位置，抬升30毫米，移到安全位置", Brushes.Green);
                                Motion.MoveRel(StationEnum.Cabinet, AxisName.PrbLoader_Z, -30);
                                Thread.Sleep(200);
                                AxisToSafePosStep = 5;
                            }
                            else
                            {
                                Motion.Interpolation(StationEnum.Cabinet, UnionName.PrbLoaderUnion, CabinetPosName.PrbModuleYClampPrbFromBuffPos);
                                AxisToSafePosStep = 10;
                                OnShowMsg?.Invoke("AutoRun", "Cabinet->Probe上料平台Y轴回初始位置", Brushes.Green);
                            }
                        }
                        break;
                    }
                case 5:
                    {
                        if (Motion.IsArrive(StationEnum.Cabinet, AxisName.PrbLoader_Z))
                        {
                            Motion.Interpolation(StationEnum.Cabinet, UnionName.PrbLoaderUnion, CabinetPosName.PrbModuleYClampPrbFromBuffPos);
                            AxisToSafePosStep = 10;
                            OnShowMsg?.Invoke("AutoRun", "Cabinet->Probe上料平台Y轴回初始位置", Brushes.Green);
                        }
                        break;
                    }
                case 10:
                    {
                        Motion.WaitMotionEnd(StationEnum.Cabinet, UnionName.PrbLoaderUnion, true);
                        AxisToSafePosStep = 20;
                        break;
                    }
                case 20:
                    {
                        if (Motion.IsArrive(StationEnum.Cabinet, AxisName.PrbLoader_Y1, CabinetPosName.PrbModuleYClampPrbFromBuffPos) &&
                            Motion.IsArrive(StationEnum.Cabinet, AxisName.PrbLoader_Y2, CabinetPosName.PrbModuleYClampPrbFromBuffPos))
                        {
                            AxisToSafePosStep = 1;
                        }
                        else if (Motion.IsMoveTimeOut(StationEnum.Cabinet, UnionName.PrbLoaderUnion, true))
                        {
                            OnShowMsg?.Invoke("AutoRun", "Cabinet->Probe上料平台Y轴回初始位置失败", Brushes.Red);
                            AxisToSafePosStep = 2000;
                        }
                        break;
                    }
                case 30:
                    {
                        if (Motion.IsArrive(StationEnum.Cabinet, AxisName.PrbLoader_Z, CabinetPosName.SafePos))
                        {
                            AxisToSafePosStep = 1;
                        }
                        else if (Motion.IsMoveTimeOut(StationEnum.Cabinet, AxisName.PrbLoader_Z))
                        {
                            OnShowMsg?.Invoke("AutoRun", "Cabinet->Probe上料平台Z轴回初始位置超时", Brushes.Red);
                            AxisToSafePosStep = 2000;
                        }
                        break;
                    }
                case 40:
                    {
                        if (Motion.IsArrive(StationEnum.Cabinet, AxisName.PrbLoader_Z, LoaderZTempPos))
                        {
                            Motion.MoveAbs(StationEnum.Cabinet, AxisName.PrbBuffer_Y, CabinetPosName.PrbBuffStgLdPos);
                            AxisToSafePosStep =50;
                            OnShowMsg?.Invoke("AutoRun", "Cabinet->Probe上料平台Z轴到达避让位置，缓存平台Y轴移动到安全位置", Brushes.Green);
                        }
                        else if (Motion.IsMoveTimeOut(StationEnum.Cabinet, AxisName.PrbLoader_Z))
                        {
                            OnShowMsg?.Invoke("AutoRun", "Cabinet->Probe缓存Y轴回初始位置超时", Brushes.Red);
                            AxisToSafePosStep = 2000;
                        }
                        break;
                    }
                case 50:
                    {
                        if (Motion.IsArrive(StationEnum.Cabinet, AxisName.PrbBuffer_Y, CabinetPosName.PrbBuffStgLdPos))
                        {
                            Motion.MoveAbs(StationEnum.Cabinet, AxisName.PrbLoader_Z, CabinetPosName.SafePos);
                            OnShowMsg?.Invoke("AutoRun", "Cabinet->缓存平台Y轴到达安全位置，Probe上料平台Z轴移动到安全位置", Brushes.Green);
                            AxisToSafePosStep = 60;
                        }
                        else if (Motion.IsMoveTimeOut(StationEnum.Cabinet, AxisName.PrbBuffer_Y))
                        {
                            OnShowMsg?.Invoke("AutoRun", "Cabinet->Probe缓存Y轴回初始位置超时", Brushes.Red);
                            AxisToSafePosStep = 2000;
                        }
                        break;
                    }
                case 60:
                    {
                        if (Motion.IsArrive(StationEnum.Cabinet, AxisName.PrbLoader_Z, CabinetPosName.SafePos))
                        {
                            OnShowMsg?.Invoke("AutoRun", "Cabinet->Probe上料平台Z轴到达安全位置", Brushes.Green);
                            AxisToSafePosStep = 1000;
                        }
                        else if (Motion.IsMoveTimeOut(StationEnum.Cabinet, AxisName.PrbBuffer_Y))
                        {
                            OnShowMsg?.Invoke("AutoRun", "Cabinet->Probe缓存Y轴回初始位置超时", Brushes.Red);
                            AxisToSafePosStep = 2000;
                        }
                        break;
                    }
                case 1000:
                    {
                        OnShowMsg?.Invoke("AutoRun", "Cabinet->料仓工位回初始位完成", Brushes.Green);
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
                            break;
                        }
                    case 10:
                        {
                            if (AxisToSafePosStep == 0)
                            {
                                if (Param.Control.PrbChgMode == PrbChgMode.Load)
                                {
                                    LoadPrbStep = 1;
                                    UnloadPrbStep = -1;
                                    PrbAutoChgStep = 20;
                                }
                                else
                                {
                                    UnloadPrbStep = 1;
                                    LoadPrbStep = -1;
                                    PrbAutoChgStep = 30;
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
                                PrbAutoChgStep = 40;
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
                                    PrbAutoChgStep = 40;
                                }
                            }
                            else
                            {
                                UnloadPrbFlow();
                            }
                            break;
                        }
                    case 40:
                        {
                            PrbAutoChgStep = 0;
                            OnTriggerSignal?.Invoke((int)StationCommand.PrbAutoChgCom);
                            break;
                        }
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                OnStop?.Invoke();
                OnShowMsg?.Invoke("AutoRun", $"{StationName}->PrbAutoChgFlow方法执行异常，异常信息：{ex.ToString()}", Brushes.Blue);
            }
        }
        int TargetRowNum = -1;
        /// <summary>
        /// 料仓->Uvw
        /// </summary>
        private void LoadPrbFlow()
        {
            try
            {
                switch (LoadPrbStep)
                {
                    case 1:
                        {
                            if (Motion.GetDigitalInput(InputName.UvwDetPrbLeft) && Motion.GetDigitalInput(InputName.UvwDetPrbRight))
                            {
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.UvwDetectProbe);
                                OnShowMsg?.Invoke("AutoRun", $"Cabinet->Uvw检测到Probe，请确认料仓和Probe绑定信息是否正确", Brushes.Red);
                                LoadPrbStep = -1;
                            }
                            else
                            {
                                CabArredSts = Status.None;
                                TargetRowNum = CabinetInfoHelper.GetRowNum(Global.ReceivedRecipeName);
                                if (TargetRowNum < 0)
                                {
                                    OnShowMsg?.Invoke("AutoRun", $"Cabinet->获取机种:{Global.ReceivedRecipeName}所在料仓层号失败，请检查配置信息", Brushes.Red);
                                    OnStop?.Invoke();
                                    return;
                                }
                                CabPosName = GetPosName(TargetRowNum);
                                if (CabinetIsCanMove())
                                {
                                    Motion.MoveAbs(StationEnum.Cabinet, AxisName.CabGantry_Z1, CabPosName);
                                    OnShowMsg?.Invoke("AutoRun", $"Cabinet->料仓移动到第{TargetRowNum + 1}层", Brushes.Green);
                                    LoadPrbStep = 10;
                                }
                            }
                            break;
                        }
                    case 10:
                        {
                            if (Motion.IsArrive(StationEnum.Cabinet, AxisName.CabGantry_Z1, CabPosName))
                            {
                                Delay.Start(Param.Motion.CabWaitAutoChgTimeout);
                                CabArredSts = Status.OK;
                                OnShowMsg?.Invoke("AutoRun", $"Cabinet->料仓到达，等待自动更换机构取出Probe", Brushes.Green);
                                LoadPrbStep = 20;
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.Cabinet, AxisName.CabGantry_Z1))
                            {
                                OnShowMsg?.Invoke("AutoRun", $"Cabinet->料仓移动到第{TargetRowNum + 1}层超时", Brushes.Red);
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.CabGantry_Z1MoveTimeout);
                                LoadPrbStep = 1;
                            }
                            break;
                        }
                    case 20:
                        {
                            if (PrbAutoChg.PrbLoadSts == Status.OK)
                            {
                                PrbAutoChg.PrbLoadSts = Status.None;
                                if (CabinetIsCanMove())
                                {
                                    OnShowMsg?.Invoke("AutoRun", $"Cabinet->自动更换机构更换Probe完成，料仓下降到初始位置", Brushes.Green);
                                    Motion.MoveAbs(StationEnum.Cabinet, AxisName.CabGantry_Z1, CabinetPosName.SafePos);
                                    LoadPrbStep = 30;
                                }
                            }
                            else if (Delay.TimeUp())
                            {
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.CabWaitAutoChgTimeout);
                                LoadPrbStep = 10;
                            }
                            break;
                        }
                    case 30:
                        {
                            if (Motion.IsArrive(StationEnum.Cabinet, AxisName.CabGantry_Z1, CabinetPosName.SafePos))
                            {
                                LoadPrbStep = 0;
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.Cabinet, AxisName.CabGantry_Z1))
                            {
                                OnShowMsg?.Invoke("AutoRun", $"Cabinet->料仓下降到初始位置超时", Brushes.Red);
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.CabGantry_Z1MoveTimeout);
                                LoadPrbStep = 20;
                            }
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                OnStop?.Invoke();
                OnShowMsg?.Invoke("AutoRun", $"{StationName}->LoadPrbFlow方法执行异常，异常信息：{ex.ToString()}", Brushes.Red);
            }
        }
        /// <summary>
        /// Uvw->料仓
        /// </summary>
        private void UnloadPrbFlow()
        {
            try
            {
                switch (UnloadPrbStep)
                {
                    case 1:
                        {
                           TargetRowNum = CabinetInfoHelper.GetRowNum(RecipeManager.CurRecipe.RecipeID);
                            if (TargetRowNum < 0)
                            {
                                OnShowMsg?.Invoke("AutoRun", $"DualStage->获取机种:{RecipeManager.CurRecipe.RecipeID}压头所在料仓层号失败，请检查配置信息", Brushes.Red);
                                OnStop?.Invoke();
                                return;
                            }
                            CabArredSts = Status.None;
                            CabPosName = GetPosName(TargetRowNum);
                            if (CabinetIsCanMove())
                            {
                                Motion.MoveAbs(StationEnum.Cabinet, AxisName.CabGantry_Z1, CabPosName);
                                OnShowMsg?.Invoke("AutoRun", $"Cabinet->料仓移动到第{TargetRowNum}+1层", Brushes.Green);
                                UnloadPrbStep = 10;
                            }
                            break;
                        }
                    case 10:
                        {
                            if (Motion.IsArrive(StationEnum.Cabinet, AxisName.CabGantry_Z1, CabPosName))
                            {
                                Delay.Start(Param.Motion.CabWaitAutoChgTimeout);
                                CabArredSts = Status.OK;
                                OnShowMsg?.Invoke("AutoRun", $"Cabinet->料仓到达第{TargetRowNum+1}层，等待自动更换机构放下Probe", Brushes.Green);
                                UnloadPrbStep = 20;
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.Cabinet, AxisName.CabGantry_Z1))
                            {
                                OnShowMsg?.Invoke("AutoRun", $"Cabinet->料仓下降到第{TargetRowNum + 1}层超时", Brushes.Red);
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.CabGantry_Z1MoveTimeout);
                                UnloadPrbStep = 1;
                            }
                            break;
                        }
                    case 20:
                        {
                            if (PrbAutoChg.PrbUnloadSts == Status.OK)
                            {
                                PrbAutoChg.PrbUnloadSts = Status.None;
                                OnShowMsg?.Invoke("AutoRun", $"Cabinet->自动更换机构更换Probe完成，料仓下降到初始位置", Brushes.Green);
                                if (Param.Control.PrbChgMode != PrbChgMode.UnloadLoad)
                                {
                                    if (CabinetIsCanMove())
                                    {
                                        Motion.MoveAbs(StationEnum.Cabinet, AxisName.CabGantry_Z1, CabinetPosName.SafePos);
                                        UnloadPrbStep = 30;
                                    }
                                }
                                else
                                {
                                    UnloadPrbStep = 0;
                                }
                            }
                            else if (Delay.TimeUp())
                            {
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.CabWaitAutoChgTimeout);
                                UnloadPrbStep = 10;
                            }
                            break;
                        }
                    case 30:
                        {
                            if (Motion.IsArrive(StationEnum.Cabinet, AxisName.CabGantry_Z1, CabinetPosName.SafePos))
                            {
                                OnShowMsg?.Invoke("AutoRun", $"Cabinet->料仓到达初始位置", Brushes.Green);
                                UnloadPrbStep = 0;
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.Cabinet, AxisName.CabGantry_Z1))
                            {
                                OnShowMsg?.Invoke("AutoRun", $"Cabinet->料仓下降到初始位置超时", Brushes.Red);
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.CabGantry_Z1MoveTimeout);
                                UnloadPrbStep = 20;
                            }
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                OnStop?.Invoke();
                OnShowMsg?.Invoke("AutoRun", $"{StationName}->UnloadPrbFlow方法执行异常，异常信息：{ex.ToString()}", Brushes.Red);
            }
        }
        string CabinetRowName = string.Empty;
        /// <summary>
        /// Probe缓存平台->料仓
        /// </summary>
        /// <param name="targetRowNum">料仓层号</param>
        /// <returns></returns>
        public void BufferToCabinetFlow(int targetRowNum)
        {
            try
            {
                switch (LoadPrbStep)
                {
                    case 1:
                        {
                            OnTriggerSignal?.Invoke(1000);//缓存平台到料仓更换Probe
                            OnShowMsg?.Invoke("AutoRun", "开始Load ProbeCard，各轴回初始位", Brushes.Blue);
                            AxisToSafePosStep = 1;
                            LoadPrbStep = 10;
                            break;
                        }
                    case 10:
                        {
                            if (AxisToSafePosStep == 0)
                            {
                                if (CabinetIsCanMove())
                                {
                                    OnShowMsg?.Invoke("AutoRun", $"料仓移动到第{targetRowNum + 1}层", Brushes.Green);
                                    CabinetRowName = $"Cab{targetRowNum + 1}FPos";
                                    Motion.MoveAbs(StationEnum.Cabinet, AxisName.CabGantry_Z1, CabinetRowName);
                                    OnShowMsg?.Invoke("AutoRun", $"Probe上料平台Y轴移动到从缓存平台上抓取ProbeCrad位置，Probe缓存平台移动到上料位置", Brushes.Green);
                                    Motion.Interpolation(StationEnum.Cabinet, UnionName.PrbLoaderUnion, CabinetPosName.PrbModuleYClampPrbFromBuffPos);
                                    Motion.MoveAbs(StationEnum.Cabinet, AxisName.PrbBuffer_Y, CabinetPosName.PrbBuffStgLdPos);
                                    LoadPrbStep = 20;
                                }
                            }
                            else if (AxisToSafePosStep == -1000)
                            {
                                LoadPrbStep = 0;
                            }
                            else
                            {
                                AxisToSafePos();
                            }
                            break;
                        }
              
                    case 20:
                        {
                            if (Motion.IsArrive(StationEnum.Cabinet, AxisName.PrbBuffer_Y, CabinetPosName.PrbBuffStgLdPos))
                            {
                                OnShowMsg?.Invoke("AutoRun", $"ProbeCard缓存平台到达待抓取位置，等待上料平台到达取Probe位置", Brushes.Green);
                                Motion.WaitMotionEnd(StationEnum.Cabinet, UnionName.PrbLoaderUnion, true);
                                LoadPrbStep = 30;
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.Cabinet, AxisName.PrbBuffer_Y))
                            {
                                OnShowMsg?.Invoke("AutoRun", $"ProbeCard缓存平台移动到待抓取位置超时", Brushes.Red);
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.PrbBuffer_YMoveTimeout);
                                LoadPrbStep = 10;
                            }

                            break;
                        }
                    case 30:
                        {
                            if (Motion.IsArrive(StationEnum.Cabinet, AxisName.PrbLoader_Y1, CabinetPosName.PrbModuleYClampPrbFromBuffPos) && 
                                Motion.IsArrive(StationEnum.Cabinet, AxisName.PrbLoader_Y2, CabinetPosName.PrbModuleYClampPrbFromBuffPos))
                            {
                                OnShowMsg?.Invoke("AutoRun", $"上料平台Y轴到达取Probe位置，Probe上料平台Z轴移动到缓存平台上方位置", Brushes.Green);
                                Motion.MoveAbs(StationEnum.Cabinet, AxisName.PrbLoader_Z, CabinetPosName.ToBufferHighVelPos);
                                LoadPrbStep = 40;
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.Cabinet, UnionName.PrbLoaderUnion, true))
                            {
                                OnShowMsg?.Invoke("AutoRun", "Cabinet->上料平台移动到取Probe位置超时", Brushes.Red);
                                OnPause?.Invoke(-1);
                                LoadPrbStep = 10;
                            }
                            break;
                        }
                    case 40:
                        {
                            if (Motion.IsArrive(StationEnum.Cabinet, AxisName.PrbLoader_Z, CabinetPosName.ToBufferHighVelPos))
                            {
                                OnShowMsg?.Invoke("AutoRun", $"Probe上料平台Z轴到达到缓存平台上放位置，变速移动到抓取Probe位置", Brushes.Green);
                                Motion.MoveAbs(StationEnum.Cabinet, AxisName.PrbLoader_Z, CabinetPosName.PickPrbFromBufferPos);
                                LoadPrbStep = 45;
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.Cabinet, AxisName.PrbLoader_Z))
                            {
                                OnShowMsg?.Invoke("AutoRun", $"Probe上料平台Z轴移动到缓存平台上方位置超时", Brushes.Red);
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.PrbLoader_ZMoveTimeout);
                                LoadPrbStep = 30;
                            }
                            break;
                        }
                    case 45:
                        {
                            if (Motion.IsArrive(StationEnum.Cabinet, AxisName.PrbLoader_Z, CabinetPosName.PickPrbFromBufferPos))
                            {
                                Delay.Start(1);
                                OnShowMsg?.Invoke("AutoRun", $"Probe上料平台Z轴到达抓取Probe位置", Brushes.Green);
                                LoadPrbStep = 47;
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.Cabinet, AxisName.PrbLoader_Z))
                            {
                                OnShowMsg?.Invoke("AutoRun", $"Probe上料平台Z轴移动到缓存平台上方位置超时", Brushes.Red);
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.PrbLoader_ZMoveTimeout);
                                LoadPrbStep = 40;
                            }
                            break;
                        }
                    case 47:
                        {
                            if (Delay.TimeUp())
                            {
                                Delay.Start(Param.Motion.PrbZeroSysTime);
                                OnShowMsg?.Invoke("AutoRun", $"零点定位系统锁紧", Brushes.Green);
                                CabinetZero(true);
                                LoadPrbStep = 50;
                            }
                            break;
                        }
                    case 50:
                        {
                            if (Delay.TimeUp())
                            {
                                if ((Motion.GetDigitalInput(InputName.PrbLoaderDetectPrb1) && Motion.GetDigitalInput(InputName.PrbLoaderDetectPrb2)))
                                {
                                    OnShowMsg?.Invoke("AutoRun", $"Probe上料平台Z轴上升到变速位置", Brushes.Green);
                                    Motion.MoveAbs(StationEnum.Cabinet, AxisName.PrbLoader_Z, CabinetPosName.LeaveBufferLowVelPos);
                                    LoadPrbStep = 60;
                                }
                                else
                                {
                                    AlarmManager.CreateAlarm((int)MotionAlarmCode.PrbLoaderDetSenserFail);
                                    LoadPrbStep = 47;
                                }
                            }
                            break;
                        }

                    case 60:
                        {
                            if (Motion.IsArrive(StationEnum.Cabinet, AxisName.PrbLoader_Z, CabinetPosName.LeaveBufferLowVelPos))
                            {
                                if (Motion.GetDigitalInput(InputName.PrbLoaderDetectPrb1) && Motion.GetDigitalInput(InputName.PrbLoaderDetectPrb2))
                                {
                                    OnShowMsg?.Invoke("AutoRun", $"Probe上料平台Z轴移动到放置ProbeCard到料仓中位置，并且等待料仓到达{targetRowNum + 1}层", Brushes.Green);
                                    Motion.MoveAbs(StationEnum.Cabinet, AxisName.PrbLoader_Z, CabinetPosName.LoadZBeforeToCabPos);
                                    LoadPrbStep = 70;
                                }
                                else if (Delay.TimeUp())
                                {
                                    AlarmManager.CreateAlarm((int)MotionAlarmCode.PrbLoaderDetSenserFail);
                                    LoadPrbStep = 50;
                                }
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.Cabinet, AxisName.PrbLoader_Z))
                            {
                                OnShowMsg?.Invoke("AutoRun", $"Probe上料平台Z轴上升到变速位置超时", Brushes.Red);
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.PrbLoader_ZMoveTimeout);
                                LoadPrbStep = 50;
                            }
                            break;
                        }
                    case 70:
                        {
                            if (Motion.IsArrive(StationEnum.Cabinet, AxisName.PrbLoader_Z, CabinetPosName.LoadZBeforeToCabPos) && Motion.IsArrive(StationEnum.Cabinet, AxisName.CabGantry_Z1, CabinetRowName))
                            {
                                OnShowMsg?.Invoke("AutoRun", $"Probe上料平台Z轴到达放置ProbeCard位置，Y轴移动到放置ProbeCard位置", Brushes.Green);
                                Motion.Interpolation(StationEnum.Cabinet, UnionName.PrbLoaderUnion, CabinetPosName.PrbModuleYClampPrbFromCabPos);
                                LoadPrbStep = 80;
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.Cabinet, AxisName.PrbLoader_Z))
                            {
                                OnShowMsg?.Invoke("AutoRun", $"Cabinet->Probe上料平台Z轴移动到放置ProbeCard位置超时", Brushes.Red);
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.PrbLoader_ZMoveTimeout);
                                LoadPrbStep = 60;
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.Cabinet, AxisName.CabGantry_Z1))
                            {
                                OnShowMsg?.Invoke("AutoRun", $"Cabinet->料仓移动到第{targetRowNum + 1}层超时", Brushes.Red);
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.CabGantry_Z1MoveTimeout);
                                LoadPrbStep = 60;
                            }
                            break;
                        }
                    case 80:
                        {
                            Motion.WaitMotionEnd(StationEnum.Cabinet, UnionName.PrbLoaderUnion, true);
                            LoadPrbStep = 85;
                            break;
                        }
                    case 85:
                        {
                            if (Motion.IsArrive(StationEnum.Cabinet, AxisName.PrbLoader_Y1, CabinetPosName.PrbModuleYClampPrbFromCabPos) &&
                                Motion.IsArrive(StationEnum.Cabinet, AxisName.PrbLoader_Y2, CabinetPosName.PrbModuleYClampPrbFromCabPos))
                            {
                                OnShowMsg?.Invoke("AutoRun", $"Probe上料平台Z轴下降，把ProbeCard放在料仓中", Brushes.Green);
                                Motion.MoveAbs(StationEnum.Cabinet, AxisName.PrbLoader_Z, CabinetPosName.PickPutPrbFromCabDownPos);
                                LoadPrbStep = 90;
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.Cabinet, UnionName.PrbLoaderUnion, true))
                            {
                                OnShowMsg?.Invoke("AutoRun", "Cabinet->上料平台Y轴移动到放置ProbeCard位置超时", Brushes.Red);
                                OnPause?.Invoke(-1);
                                LoadPrbStep = 70;
                            }
                            break;
                        }
                    case 90:
                        {
                            if (Motion.IsArrive(StationEnum.Cabinet, AxisName.PrbLoader_Z, CabinetPosName.PickPutPrbFromCabDownPos))
                            {
                                Delay.Start(Param.Motion.CabDetPrbTimeout);
                                OnShowMsg?.Invoke("AutoRun", "等待料仓感应到ProbeCard", Brushes.Green);
                                LoadPrbStep = 100;
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.Cabinet, AxisName.PrbLoader_Z))
                            {
                                OnShowMsg?.Invoke("AutoRun", $"Probe上料平台Z轴移动到把ProbeCard放在料仓中位置超时", Brushes.Red);
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.PrbLoader_ZMoveTimeout);
                                LoadPrbStep = 80;
                            }
                            break;
                        }
                    case 100:
                        {
                            if (Delay.TimeUp())
                            {
                                string leftDetPrbSensor = $"CabDetectPrbLeft{targetRowNum + 1}F";
                                string rightDetPrbSensor = $"CabDetectPrbRight{targetRowNum + 1}F";
                                if ((Motion.GetDigitalInput(leftDetPrbSensor) && Motion.GetDigitalInput(rightDetPrbSensor)))
                                {
                                    OnShowMsg?.Invoke("AutoRun", "料仓感应到ProbeCard，Probe上料平台零点系统松开", Brushes.Green);
                                    CabinetZero(false);
                                    Delay.Start(Param.Motion.PrbZeroSysTime);
                                    LoadPrbStep = 110;
                                }
                                else
                                {
                                    AlarmManager.CreateAlarm((int)MotionAlarmCode.CabDetSenserFail);
                                    LoadPrbStep = 90;
                                }
                            }
                            break;
                        }

                    case 110:
                        {
                            if (Delay.TimeUp())
                            {
                                OnShowMsg?.Invoke("AutoRun", $"Probe上料平台Z轴抬升", Brushes.Green);
                                Motion.MoveAbs(StationEnum.Cabinet, AxisName.PrbLoader_Z, CabinetPosName.PickPutPrbFromCabUpPos);
                                LoadPrbStep = 120;
                            }
                            break;
                        }
                    case 120:
                        {
                            if (Motion.IsArrive(StationEnum.Cabinet, AxisName.PrbLoader_Z, CabinetPosName.PickPutPrbFromCabUpPos))
                            {
                                OnShowMsg?.Invoke("AutoRun", "Probe上料平台Y轴退出料仓回安全位置，零点定位系统关闭", Brushes.Green);
                                Motion.Interpolation(StationEnum.Cabinet, UnionName.PrbLoaderUnion, CabinetPosName.PrbModuleYClampPrbFromBuffPos);
                                LoadPrbStep = 130;
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.Cabinet, AxisName.PrbLoader_Z))
                            {
                                OnShowMsg?.Invoke("AutoRun", $"Probe上料平台Z轴抬升超时", Brushes.Red);
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.PrbLoader_ZMoveTimeout);
                                LoadPrbStep = 110;
                            }
                            break;
                        }
                    case 130:
                        {
                            Motion.WaitMotionEnd(StationEnum.Cabinet, UnionName.PrbLoaderUnion, true);
                            LoadPrbStep = 135;
                            break;
                        }
                    case 135:
                        {
                            if (Motion.IsArrive(StationEnum.Cabinet, AxisName.PrbLoader_Y1, CabinetPosName.PrbModuleYClampPrbFromBuffPos) &&
                                Motion.IsArrive(StationEnum.Cabinet, AxisName.PrbLoader_Y2, CabinetPosName.PrbModuleYClampPrbFromBuffPos))
                            {
                                if (CabinetIsCanMove())
                                {
                                    OnBufferToCabinetFlowCom?.Invoke();
                                    OnShowMsg?.Invoke("AutoRun", "Probe上料平台Y轴和料仓龙门回到安全位置", Brushes.Green);
                                    Motion.MoveAbs(StationEnum.Cabinet, AxisName.PrbBuffer_Y, CabinetPosName.PrbBuffStgLdPos);
                                    Motion.MoveAbs(StationEnum.Cabinet, AxisName.CabGantry_Z1, CabinetPosName.SafePos);
                                    LoadPrbStep = 140;
                                }
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.Cabinet, UnionName.PrbLoaderUnion, true))
                            {
                                OnShowMsg?.Invoke("AutoRun", "Cabinet->Probe上料平台Y轴退出料仓回安全位置超时", Brushes.Red);
                                OnPause?.Invoke(-1);
                                LoadPrbStep = 120;
                            }
                            break;
                        }
                    case 140:
                        {
                            if (Motion.IsArrive(StationEnum.Cabinet, AxisName.PrbBuffer_Y, CabinetPosName.PrbBuffStgLdPos) &&
                                Motion.IsArrive(StationEnum.Cabinet, AxisName.CabGantry_Z1, CabinetPosName.SafePos))
                            {
                                Motion.MoveAbs(StationEnum.Cabinet, AxisName.PrbLoader_Z, CabinetPosName.SafePos);
                                OnShowMsg?.Invoke("AutoRun", "缓存平台和料仓龙门到达安全位置，Probe上料平台Z轴回到安全位置", Brushes.Green);
                                LoadPrbStep = 150;
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.Cabinet, AxisName.PrbBuffer_Y))
                            {
                                OnShowMsg?.Invoke("AutoRun", "Probe缓存平台Y轴回安全位置超时", Brushes.Red);
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.PrbBuffer_YMoveTimeout);
                                LoadPrbStep = 135;
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.Cabinet, AxisName.CabGantry_Z1))
                            {
                                OnShowMsg?.Invoke("AutoRun", "料仓龙门回安全位置超时", Brushes.Red);
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.CabGantry_Z1MoveTimeout);
                                LoadPrbStep = 135;
                            }
                            break;
                        }
                    case 150:
                        {
                            if (Motion.IsArrive(StationEnum.Cabinet, AxisName.PrbLoader_Z, CabinetPosName.SafePos))
                            {
                                OnShowMsg?.Invoke("AutoRun", "Probe上料平台Z轴到达安全位置，Load ProbeCard结束", Brushes.Green);
                                LoadPrbStep = 0;
                                OnTriggerSignal?.Invoke(1001);//缓存平台到料仓更换Probe完成
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.Cabinet, AxisName.PrbLoader_Z))
                            {
                                OnShowMsg?.Invoke("AutoRun", $"Probe上料平台Z轴回到安全位置超时", Brushes.Red);
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.PrbLoader_ZMoveTimeout);
                                LoadPrbStep = 140;
                            }
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                OnStop?.Invoke();
                OnShowMsg?.Invoke("AutoRun", $"{StationName}->PrbModuleLoadPrb方法执行异常，异常信息：{ex.ToString()}", Brushes.Red);
            }
        }
        double LoaderZTempPos = 0;
        /// <summary>
        /// 料仓->Probe缓存平台
        /// </summary>
        /// <param name="targetRowNum">料仓层号</param>
        /// <returns></returns>
        public void CabinetToBufferFlow(int targetRowNum)
        {
            try
            {
                switch (UnloadPrbStep)
                {
                    case 1:
                        {
                            OnShowMsg?.Invoke("AutoRun", "开始UnLoad ProbeCard，各轴回初始位", Brushes.Blue);
                            AxisToSafePosStep = 1;
                            UnloadPrbStep = 10;
                            OnTriggerSignal?.Invoke(1000);//缓存平台到料仓更换Probe
                            break;
                        }
                    case 10:
                        {
                            if (AxisToSafePosStep == 0)
                            {
                                if (CabinetIsCanMove())
                                {
                                    OnShowMsg?.Invoke("AutoRun", $"料仓移动到第{targetRowNum + 1}层，Z轴移动到从料仓中取放ProbeCard位置", Brushes.Green);
                                    CabinetRowName = $"Cab{targetRowNum + 1}FPos";
                                    Motion.MoveAbs(StationEnum.Cabinet, AxisName.CabGantry_Z1, CabinetRowName);
                                    Motion.MoveAbs(StationEnum.Cabinet, AxisName.PrbLoader_Z, CabinetPosName.LoadZBeforeToCabPos);
                                    UnloadPrbStep = 20;
                                }
                            }
                            else
                            {
                                AxisToSafePos();
                            }
                            break;
                        }
                    case 20:
                        {
                            if (Motion.IsArrive(StationEnum.Cabinet, AxisName.CabGantry_Z1, CabinetRowName))
                            {
                                if (Motion.IsArrive(StationEnum.Cabinet, AxisName.PrbLoader_Z, CabinetPosName.LoadZBeforeToCabPos))
                                {
                                    Motion.MoveAbs(StationEnum.Cabinet, AxisName.PrbBuffer_Y, CabinetPosName.PrbBuffStgLdPos);
                                    OnShowMsg?.Invoke("AutoRun", $"料仓到达第{targetRowNum + 1}层，Z轴到达从料仓中取放ProbeCard位置，Probe缓存平台Y轴移动到取放ProbeCard位置", Brushes.Green);
                                    UnloadPrbStep = 22;
                                }
                                else if (Motion.IsMoveTimeOut(StationEnum.Cabinet, AxisName.PrbLoader_Z))
                                {
                                    OnShowMsg?.Invoke("AutoRun", $"Z轴移动到从料仓中取放ProbeCard位置超时", Brushes.Red);
                                    AlarmManager.CreateAlarm((int)MotionAlarmCode.PrbLoader_ZMoveTimeout);
                                    UnloadPrbStep = 10;
                                }
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.Cabinet, AxisName.CabGantry_Z1))
                            {
                                OnShowMsg?.Invoke("AutoRun", $"料仓龙门移动到第{targetRowNum + 1}层超时", Brushes.Red);
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.CabGantry_Z1MoveTimeout);
                                UnloadPrbStep = 10;
                            }

                            break;
                        }
                    case 22:
                        {
                            if (Motion.IsArrive(StationEnum.Cabinet, AxisName.PrbBuffer_Y, CabinetPosName.PrbBuffStgLdPos))
                            {
                                Delay.Start(Param.Motion.PrbZeroSysTime);
                                CabinetZero(false);
                                OnShowMsg?.Invoke("AutoRun", $"Probe缓存平台Y轴到达取放ProbeCard位置，零点定位系统松开", Brushes.Green);
                                UnloadPrbStep = 25;
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.Cabinet, AxisName.PrbBuffer_Y))
                            {
                                OnShowMsg?.Invoke("AutoRun", $"Probe缓存平台Y轴移动到取放ProbeCard位置超时", Brushes.Red);
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.PrbBuffer_YMoveTimeout);
                                UnloadPrbStep = 20;
                            }
                            break;
                        }
                    case 25:
                        {
                            if (Delay.TimeUp())
                            {
                                OnShowMsg?.Invoke("AutoRun", $"Probe上料平台Y轴移动到从料仓中达抓取ProbeCrad位置", Brushes.Green);
                                Motion.Interpolation(StationEnum.Cabinet, UnionName.PrbLoaderUnion, CabinetPosName.PrbModuleYClampPrbFromCabPos);
                                UnloadPrbStep = 30;
                            }
                            break;
                        }
                    case 30:
                        {
                            Motion.WaitMotionEnd(StationEnum.Cabinet, UnionName.PrbLoaderUnion, true);
                            UnloadPrbStep = 40;
                            break;
                        }
                    case 40:
                        {
                            if (Motion.IsArrive(StationEnum.Cabinet, AxisName.PrbLoader_Y1, CabinetPosName.PrbModuleYClampPrbFromCabPos) &&
                                Motion.IsArrive(StationEnum.Cabinet, AxisName.PrbLoader_Y2, CabinetPosName.PrbModuleYClampPrbFromCabPos))
                            {
                                OnShowMsg?.Invoke("AutoRun", $"Probe上料平台Y轴到达从料仓中达抓取ProbeCrad位置", Brushes.Green);
                                OnShowMsg?.Invoke("AutoRun", $"Probe上料平台Z轴下降", Brushes.Green);
                                Motion.MoveAbs(StationEnum.Cabinet, AxisName.PrbLoader_Z, CabinetPosName.PickPutPrbFromCabDownPos);
                                UnloadPrbStep = 50;
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.Cabinet,UnionName.PrbLoaderUnion,true))
                            {
                                OnShowMsg?.Invoke("AutoRun", $"Probe上料平台Y轴移动到从料仓中达抓取ProbeCrad位置超时", Brushes.Green);
                                OnPause?.Invoke(-1);
                                UnloadPrbStep = 25;
                            }
                            break;
                        }
                    case 50:
                        {
                            if (Motion.IsArrive(StationEnum.Cabinet, AxisName.PrbLoader_Z, CabinetPosName.PickPutPrbFromCabDownPos))
                            {
                                Thread.Sleep(1000);
                                Delay.Start(Param.Motion.PrbZeroSysTime);
                                OnShowMsg?.Invoke("AutoRun", $"Probe上料手臂贴紧ProbeCard，零点定位系统锁紧", Brushes.Green);
                                CabinetZero(true);
                                UnloadPrbStep = 60;
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.Cabinet, AxisName.PrbLoader_Z))
                            {
                                OnShowMsg?.Invoke("AutoRun", $"Probe上料平台Z轴下降超时", Brushes.Red);
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.PrbLoader_ZMoveTimeout);
                                UnloadPrbStep = 40;
                            }
                            break;
                        }
                    case 60:
                        {
                            if (Delay.TimeUp())
                            {
                                if ((Motion.GetDigitalInput(InputName.PrbLoaderDetectPrb1) && Motion.GetDigitalInput(InputName.PrbLoaderDetectPrb2)))
                                {
                                    OnShowMsg?.Invoke("AutoRun", $"Probe上料平台检测到ProbeCard，Probe上料平台Z轴上升，抓起ProbeCard", Brushes.Green);
                                    Motion.MoveAbs(StationEnum.Cabinet, AxisName.PrbLoader_Z, CabinetPosName.PickPutPrbFromCabUpPos);
                                    UnloadPrbStep = 80;
                                }
                                else
                                {
                                    AlarmManager.CreateAlarm((int)MotionAlarmCode.PrbLoaderDetSenserFail);
                                    UnloadPrbStep = 50;
                                }
                            }
                            break;
                        }

                    case 80:
                        {
                            if (Motion.IsArrive(StationEnum.Cabinet, AxisName.PrbLoader_Z, CabinetPosName.PickPutPrbFromCabUpPos))
                            {
                                Delay.Start(2);
                                OnShowMsg?.Invoke("AutoRun", $"Z轴抬升到位，延时2秒，确认PrbLoader是否抓紧Probe", Brushes.Green);
                                UnloadPrbStep = 85;
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.Cabinet, AxisName.PrbLoader_Z))
                            {
                                OnShowMsg?.Invoke("AutoRun", $"Probe上料平台Z轴上升超时", Brushes.Red);
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.PrbLoader_ZMoveTimeout);
                                UnloadPrbStep = 60;
                            }
                            break;
                        }
                    case 85:
                        {
                            if (Delay.TimeUp())
                            {
                                if ((Motion.GetDigitalInput(InputName.PrbLoaderDetectPrb1) && Motion.GetDigitalInput(InputName.PrbLoaderDetectPrb2)))
                                {
                                    OnShowMsg?.Invoke("AutoRun", $"PrbLoader是否抓紧Probe，Probe上料平台Y轴退出料仓", Brushes.Green);
                                    Motion.Interpolation(StationEnum.Cabinet, UnionName.PrbLoaderUnion, CabinetPosName.PrbModuleYClampPrbFromBuffPos);
                                    UnloadPrbStep = 90;
                                }
                                else
                                {
                                    AlarmManager.CreateAlarm((int)MotionAlarmCode.PrbLoaderDetSenserFail);
                                    UnloadPrbStep = 80;
                                }
                            }
                            break;
                        }
                    case 90:
                        {
                            Motion.WaitMotionEnd(StationEnum.Cabinet, UnionName.PrbLoaderUnion, true);
                            UnloadPrbStep = 100;
                            break;
                        }
                    case 100:
                        {
                            if (Motion.IsArrive(StationEnum.Cabinet, AxisName.PrbLoader_Y1, CabinetPosName.PrbModuleYClampPrbFromBuffPos) && 
                                Motion.IsArrive(StationEnum.Cabinet, AxisName.PrbLoader_Y2, CabinetPosName.PrbModuleYClampPrbFromBuffPos))
                            {
                                if (CabinetIsCanMove())
                                {
                                    OnShowMsg?.Invoke("AutoRun", $"Probe上料平台Z轴移动到变速位置，料仓回初始位置", Brushes.Green);
                                    Motion.MoveAbs(StationEnum.Cabinet, AxisName.PrbLoader_Z, CabinetPosName.ToBufferHighVelPos);
                                    Motion.MoveAbs(StationEnum.Cabinet, AxisName.CabGantry_Z1, CabinetPosName.SafePos);
                                    UnloadPrbStep = 110;
                                }
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.Cabinet, UnionName.PrbLoaderUnion, true))
                            {
                                OnShowMsg?.Invoke("AutoRun", $"Probe上料平台Y轴退出料仓超时", Brushes.Red);
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.PrbLoader_Y1MoveTimeout);
                                UnloadPrbStep = 85;
                            }
                            break;
                        }
                    case 110:
                        {
                            if (Motion.IsArrive(StationEnum.Cabinet, AxisName.PrbLoader_Z, CabinetPosName.ToBufferHighVelPos))
                            {
                                OnShowMsg?.Invoke("AutoRun", $"Probe上料平台Z轴到达变速位置，变速移动到抓取Probe位置", Brushes.Green);
                                Motion.MoveAbs(StationEnum.Cabinet, AxisName.PrbLoader_Z, CabinetPosName.PickPrbFromBufferPos);
                                UnloadPrbStep = 115;
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.Cabinet, AxisName.PrbLoader_Z))
                            {
                                OnShowMsg?.Invoke("AutoRun", $"Probe上料平台Z轴移动到变速位置超时", Brushes.Red);
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.PrbLoader_ZMoveTimeout);
                                UnloadPrbStep = 100;
                            }
                            break;
                        }
                    case 115:
                        {
                            if (Motion.IsArrive(StationEnum.Cabinet, AxisName.PrbLoader_Z, CabinetPosName.PickPrbFromBufferPos))
                            {
                                Delay.Start(Param.Motion.PrbBufferDetPrbTimeout);
                                UnloadPrbStep = 120;
                                OnShowMsg?.Invoke("AutoRun", $"Probe上料平台Z轴到达从缓存平台上抓取ProbeCard位置，等待缓存平台检测到Probe", Brushes.Green);
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.Cabinet, AxisName.PrbLoader_Z))
                            {
                                OnShowMsg?.Invoke("AutoRun", $"Probe上料平台Z轴移动到抓取Probe位置超时", Brushes.Red);
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.PrbLoader_ZMoveTimeout);
                                UnloadPrbStep = 110;
                            }
                            break;
                        }
                    case 120:
                        {
                            if (Delay.TimeUp())
                            {
                                if (Motion.GetDigitalInput(InputName.PrbBufferDetectPrb1) && Motion.GetDigitalInput(InputName.PrbBufferDetectPrb2))
                                {
                                    OnShowMsg?.Invoke("AutoRun", $"Probe上料平台Z轴到达从缓存平台上放置ProbeCard位置，零点定位系统松开", Brushes.Green);
                                    CabinetZero(false);
                                    Delay.Start(Param.Motion.PrbZeroSysTime);
                                    UnloadPrbStep = 125;
                                }
                                else
                                {
                                    AlarmManager.CreateAlarm((int)MotionAlarmCode.BufferDetSenserFail);
                                    UnloadPrbStep = 115;
                                }
                            }
                            break;
                        }
                    case 125:
                        {
                            if (Delay.TimeUp())
                            {
                                OnShowMsg?.Invoke("AutoRun", $"Probe上料平台Z轴移动到变速位置，确认缓存平台是否感应到Probe", Brushes.Green);
                                Motion.MoveAbs(StationEnum.Cabinet, AxisName.PrbLoader_Z, CabinetPosName.LeaveBufferLowVelPos);
                                UnloadPrbStep = 130;
                            }
                            break;
                        }
                    case 130:
                        {
                            if (Motion.IsArrive(StationEnum.Cabinet, AxisName.PrbLoader_Z, CabinetPosName.LeaveBufferLowVelPos))
                            {
                                if (Motion.GetDigitalInput(InputName.PrbBufferDetectPrb1) && Motion.GetDigitalInput(InputName.PrbBufferDetectPrb2))
                                {
                                    OnCabinetToBufferFlowCom?.Invoke();
                                    OnShowMsg?.Invoke("AutoRun", $"缓存平台感应到Probe，Probe上料平台Z轴移动到避让位置", Brushes.Green);
                                    var posInfo = AxisInfoHelper.GetPosInfoByName(AxisName.PrbLoader_Z, CabinetPosName.SafePos);
                                    LoaderZTempPos = posInfo.Pos - Param.Motion.LoaderZPreventCrushPos;
                                    Motion.MoveAbs(StationEnum.Cabinet, AxisName.PrbLoader_Z, LoaderZTempPos);
                                    UnloadPrbStep = 140;
                                }
                                else
                                {
                                    AlarmManager.CreateAlarm((int)MotionAlarmCode.BufferDetSenserFail);
                                    UnloadPrbStep = 125;
                                }
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.Cabinet, AxisName.PrbLoader_Z))
                            {
                                OnShowMsg?.Invoke("AutoRun", $"Probe上料平台Z轴移动到变速位置超时", Brushes.Red);
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.PrbLoader_ZMoveTimeout);
                                UnloadPrbStep = 125;
                            }
                            break;
                        }
                    case 140:
                        {
                            if (Motion.IsArrive(StationEnum.Cabinet, AxisName.PrbLoader_Z, LoaderZTempPos))
                            {
                                Motion.MoveAbs(StationEnum.Cabinet, AxisName.PrbBuffer_Y, CabinetPosName.PrbBuffStgUnldPos);
                                OnShowMsg?.Invoke("AutoRun", $"Probe上料平台Z轴到达避让位置，Probe缓存平台Y轴去下料位置", Brushes.Green);
                                UnloadPrbStep = 150;
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.Cabinet, AxisName.PrbLoader_Z))
                            {
                                OnShowMsg?.Invoke("AutoRun", $"Probe上料平台Z轴移动到避让位置超时", Brushes.Red);
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.PrbLoader_ZMoveTimeout);
                                UnloadPrbStep = 130;
                            }
                            break;
                        }
                    case 150:
                        {
                            if (Motion.IsArrive(StationEnum.Cabinet, AxisName.PrbBuffer_Y, CabinetPosName.PrbBuffStgUnldPos))
                            {
                                if (Motion.IsArrive(StationEnum.Cabinet, AxisName.CabGantry_Z1, CabinetPosName.SafePos))
                                {
                                    OnShowMsg?.Invoke("AutoRun", $"Probe缓存平台Y轴到达下料位置，料仓龙门到达起始位置", Brushes.Green);
                                    UnloadPrbStep = 0;
                                    OnShowMsg?.Invoke("AutoRun", $"UnLoad ProbeCard完成", Brushes.Blue);
                                    OnTriggerSignal?.Invoke(1001);//缓存平台到料仓更换Probe
                                }
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.Cabinet, AxisName.PrbBuffer_Y))
                            {
                                OnShowMsg?.Invoke("AutoRun", $"Probe缓存平台Y轴去下料位置超时", Brushes.Red);
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.PrbBuffer_YMoveTimeout);
                                UnloadPrbStep = 140;
                            }
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                OnStop?.Invoke();
                OnShowMsg?.Invoke("AutoRun", $"{StationName}->PrbModuleUnLoadPrb方法执行异常，异常信息：{ex.ToString()}", Brushes.Red);
            }
        }

        /// <summary>
        /// 料仓上料机构零点定位系统锁紧,true:锁紧，false:松开
        /// </summary>
        private void CabinetZero(bool onOff)
        {
            if (onOff)
            {
                Motion.SetDigitalOutput(OutputName.PrbLoaderZeroSysUnLock1, false);
                Motion.SetDigitalOutput(OutputName.PrbLoaderZeroSysUnLock2, false);
                Thread.Sleep(200);
                Motion.SetDigitalOutput(OutputName.PrbLoaderZeroSysLock1, true);
                Motion.SetDigitalOutput(OutputName.PrbLoaderZeroSysLock2, true);
            }
            else
            {
                Motion.SetDigitalOutput(OutputName.PrbLoaderZeroSysLock1, false);
                Motion.SetDigitalOutput(OutputName.PrbLoaderZeroSysLock2, false);
                Thread.Sleep(200);
                Motion.SetDigitalOutput(OutputName.PrbLoaderZeroSysUnLock1, true);
                Motion.SetDigitalOutput(OutputName.PrbLoaderZeroSysUnLock2, true);
            }

        }

        /// <summary>
        /// 获取料仓点位名称
        /// </summary>
        /// <param name="type">0：自动更换时料仓点位名称，1：上料时料仓点位名称</param>
        /// <param name="rowNum"></param>
        /// <returns></returns>
        private CabinetPosName GetPosName(int rowNum)
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
            CabArredSts = Status.None;
            PrbAutoChgStep = -1;
            LoadPrbStep = -1;
            UnloadPrbStep = -1;
            PrbAutoChgYArriveSafePos = false;
            return true;
        }
        public void OnButtonTrigger(IOInfo io, bool sts)
        {

        }
        public bool Unint()
        {
            return true;
        }
        private bool CabinetIsCanMove()
        {
            bool ret = true;
            if (!Motion.GetDigitalInput(InputName.DetectAutoChgY1) || !Motion.GetDigitalInput(InputName.DetectAutoChgY2))
            {
                AlarmManager.CreateAlarm((int)PosErrorCode.CabinetStopCuzAutoChgYNotAtSafePos);
                ret = false;
            }
            else if (Motion.GetAxisActPos(StationEnum.Cabinet, AxisName.PrbLoader_Y1) > Param.Motion.ProbeLoaderAxisYSafePos)
            {
                AlarmManager.CreateAlarm((int)PosErrorCode.CabinetStopCuzLoaderYNotAtSafePos);
                ret = false;
            }
            return ret;
        }
    }

    /// <summary>
    /// Cabinet工位点位名称，
    /// 名词缩写解释。Cab-Cabinet，Prb-ProbeCard，Chg-Change，Ld-Load，F-Floor
    /// </summary>
    public enum CabinetPosName
    {
        /// <summary>
        /// 每个轴的安全位置
        /// </summary>
        SafePos,
        /// <summary>
        /// Probe自动更换时，第一层料仓的位置
        /// </summary>
        Cab1FPos,
        Cab2FPos,
        Cab3FPos,
        Cab4FPos,
        Cab5FPos,
        Cab6FPos,
        Cab7FPos,
        Cab8FPos,

        /// <summary>
        /// Probe缓存平台下料时的位置
        /// </summary>
        PrbBuffStgUnldPos,
        /// <summary>
        /// Probe缓存上料时的位置
        /// </summary>
        PrbBuffStgLdPos,
        /// <summary>
        /// Probe上料平台从Probe缓存平台中取放Probe时，Y轴的位置
        /// </summary>
        PrbModuleYClampPrbFromBuffPos,
        /// <summary>
        ///  Probe上料平台从料仓中取放Probe时，Y轴的位置
        /// </summary>
        PrbModuleYClampPrbFromCabPos,


        /// <summary>
        /// LoaderZ轴快速去缓存平台位置
        /// </summary>
        ToBufferHighVelPos,
        /// <summary>
        /// LoaderZ轴慢速离开缓存平台位置
        /// </summary>
        LeaveBufferLowVelPos,
        /// <summary>
        /// LoaderZ轴从缓存平台抓取Probe位置
        /// </summary>
        PickPrbFromBufferPos,
        /// <summary>
        /// 在料仓中取放Probe上升位置
        /// </summary>
        PickPutPrbFromCabUpPos,
        /// <summary>
        /// 在料仓中取放Probe下降位置
        /// </summary>
        PickPutPrbFromCabDownPos,
        /// <summary>
        /// 进入料仓前位置
        /// </summary>
        LoadZBeforeToCabPos,
        HomeSafePos

    }
 
}
