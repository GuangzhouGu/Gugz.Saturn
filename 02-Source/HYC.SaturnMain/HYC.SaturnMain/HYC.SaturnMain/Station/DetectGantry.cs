/*----------------------------------------------------------------
// Copyright (C) Suzhou HYC Technology Co.,LTD
// 版权所有。
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnMain
// 文件名称    ：DetectGantry.cs
// =================================
// 创 建 者    ：guguangzhou
// 创建日期    ：2021/01/29 09:00:00 
// 功能描述    ：检测龙门工位自动流程
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
using HYC.SaturnMain.Devices.Scaner;
using HYC.SaturnMain.Entities;
using HYC.SaturnMain.GlassCrds;

using HYC.SaturnMain.Params;
using HYC.SaturnMain.Devices.ClientPC;
using HYC.SaturnMain.Devices.Motion;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HYC.SaturnMain.Alarm;
using HYC.HTPCBase.Param;
using HYC.SaturnMain;
using HYC.SaturnAoi.Entities;
using CIMBOE.Block;
using BOECIM.Devices.CIM;

namespace HYC.SaturnMain.Station
{
    public class DetectGantry : IStation
    {
        public static event Action<int> OnTriggerSignal;
        public event Action<HomeEventArgs> OnHomeStatus;

        public static event Action<int, string> OnUpdateGlassID;
        public static event Action<string> OnKeyInCodeRequest;
        public static event Action<CurrentDetectInfo,int,string,DetectType> OnUpdateCellStatus;
        public static string KeyInCode = string.Empty;
        bool StageHasAtDetectPos = false;//平台是否在测试位置
         bool StageZHomeCom = false;
         bool DetectEndBit = false;//单个panel检测结束
         bool ScanBarcodeReq = false;//单个panel检测结束
        bool AoiDetectReq = false;
        bool MuraDetectReq = false;
        bool MultiAngleDetectReq = false;
        bool SquintMuraDetectReq = false;
        int ContinueFailCount = 0;//扫码连续失败次数，如果大于3次，则手动输入
        #region 状态机运行step
         int AxisToSafePosStep = -1;//判断轴是否在安全位置流程状态机步骤
         int ScanBarcodeStep = -1;//扫码流程状态机步骤
        int DetectStep = -1, Module1DetectStep = -1, Module2DetectStep=-1;
       
        #endregion
        HTINIHelper Ini = new HTINIHelper(AppDomain.CurrentDomain.BaseDirectory + "Config.INI");
        #region IStation接口实现
        public event Action<string, string, Brush> OnShowMsg;
        public event Action OnStop;
        public event Action<int> OnPause;
        public MotionManager Motion { get; set; } = MotionManager.GetInstance();
        public int StationNum { get; set; } = 2;
        public string StationName { get; set; } = "DetectGantry";
        public int HomeStep { get; set; }
        public int AutoRunStep { get; set; } = -1; //总流程状态机步骤
        #endregion

        string ScanerID = string.Empty;
        GlassDataA UpStreamJobData = null;
        string UpStreamID = string.Empty;
        bool ScanerReadResult = false;
        bool ValidationResult = true;
        string TesterPanelID = string.Empty;//离线模式时，如果扫码枪返回的ID和上游下发的ID不一致，发送扫码枪的ID给TesterPC
        Delay Delay = new Delay(), Delay2 = new Delay();
        /// <summary>
        /// 1号检测模组当前检测Cell的索引，如果CellCountY是21，则1号检测模组检测范围是0-10
        /// </summary>
        int Module1DetectCellNum = 0;
        /// <summary>
        /// 2号检测模组当前检测Cell的索引，如果CellCountY是21，则2号检测模组检测范围是11-20
        /// </summary>
        int Module2DetectCellNum = 0;
        /// <summary>
        /// 1号检测模组检测的Cell数量
        /// </summary>
        int Module1DetectCellCount = 0;
        /// <summary>
        /// 2号检测模组检测的Cell数量
        /// </summary>
        int Module2DetectCellCount = 0;
        DetectGtyPosName Module1ZPosName, Module2ZPosName, Module1RotatePosName, Module2RotatePosName;
        AxisName DetectModule1_Z, DetectModule2_Z;
        /// <summary>
        /// 相机在Cell中心位置时，检测龙门的目标位置
        /// </summary>
        double DetectGantryBasePos = 0;
        /// <summary>
        /// 相机在Cell中心位置时，1号检测模组X轴的目标位置
        /// </summary>
        double Module1CamBasePos = 0;
        /// <summary>
        /// 相机在Cell中心位置时，2号检测模组X轴的目标位置
        /// </summary>
        double Module2CamBasePos = 0;
        /// <summary>
        /// 1号检测模组X轴检测第一个Cell时的位置
        /// </summary>
        double Module1XDangerousPos = 0;
        /// <summary>
        /// 2号检测模组X轴检测最后一个Cell时的位置
        /// </summary>
        double Module2XDangerousPos = 0;
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
                        StageHasAtDetectPos = true;
                        break;
                    }
                case (int)StationCommand.StageZHomeCom:
                    {
                        StageZHomeCom = true;
                        break;
                    }
           
            }
        }
        /// <summary>
        /// Probe工位事件处理
        /// </summary>
        /// <param name="cmd"></param>
        private void ProbeGrantryTriggerHandle(int cmd)
        {
            switch (cmd)
            {
                case (int)StationCommand.DetectEnd:
                    {
                        DetectEndBit = true;
                        break;
                    }
                case (int)StationCommand.ScanCode:
                    {
                        ScanBarcodeReq = true;
                        break;
                    }
                case (int)StationCommand.AoiDetectReq:
                    {
                        AoiDetectReq = true;
                        break;
                    }
                case (int)StationCommand.MuraDetectReq:
                    {
                        MuraDetectReq = true;
                        break;
                    }
                case (int)StationCommand.MultiAngleDetectReq:
                    {
                        MultiAngleDetectReq = true;
                        break;
                    }
                case (int)StationCommand.SquintMuraDetectReq:
                    {
                        SquintMuraDetectReq = true;
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
                            OnShowMsg?.Invoke("AutoRun", "DetectGantry->检测龙门工位开始回零", Brushes.Green);
                            OnShowMsg?.Invoke("AutoRun", "DetectGantry->检测模组Z轴和Mura相机旋转轴开始回零开始回零", Brushes.Green);
                            Motion.Home(StationEnum.DetectGantry, AxisName.DetectModule1_Z1);
                            Motion.Home(StationEnum.DetectGantry, AxisName.DetectModule1_Z2);
                            Motion.Home(StationEnum.DetectGantry, AxisName.DetectModule2_Z1);
                            Motion.Home(StationEnum.DetectGantry, AxisName.DetectModule2_Z2);
                            Motion.Home(StationEnum.DetectGantry, AxisName.DetectModule1_Rotate);
                            Motion.Home(StationEnum.DetectGantry, AxisName.DetectModule2_Rotate);
                            HomeStep = 10;
                            break;
                        }
                    case 10:
                        {
                            if (Motion.IsHomeDone(StationEnum.DetectGantry, AxisName.DetectModule1_Z1) && Motion.IsHomeDone(StationEnum.DetectGantry, AxisName.DetectModule1_Z2)&&
                                Motion.IsHomeDone(StationEnum.DetectGantry, AxisName.DetectModule2_Z1) && Motion.IsHomeDone(StationEnum.DetectGantry, AxisName.DetectModule2_Z2)&&
                                Motion.IsHomeDone(StationEnum.DetectGantry, AxisName.DetectModule1_Rotate) && Motion.IsHomeDone(StationEnum.DetectGantry, AxisName.DetectModule2_Rotate))
                            {
                                if (StageZHomeCom)
                                {
                                    StageZHomeCom = false;
                                    Motion.Home(StationEnum.DetectGantry, AxisName.DetectModule1_X);
                                    Motion.Home(StationEnum.DetectGantry, AxisName.DetectModule2_X);
                                    Motion.Home(StationEnum.DetectGantry, AxisName.DetectGantry_Y1);//龙门轴回零buffer是1号
                                    HomeStep = 20;
                                    OnShowMsg?.Invoke("AutoRun", "DetectGantry->平台Z轴，检测模组Z轴回零完成，检测龙门开始回零", Brushes.Green);
                                }
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.DetectGantry, AxisName.DetectModule1_Z1) || Motion.IsMoveTimeOut(StationEnum.DetectGantry, AxisName.DetectModule1_Z2)||
                                Motion.IsMoveTimeOut(StationEnum.DetectGantry, AxisName.DetectModule2_Z1) || Motion.IsMoveTimeOut(StationEnum.DetectGantry, AxisName.DetectModule2_Z2))
                            {
                                OnShowMsg?.Invoke("AutoRun", "DetectGantry->检测模组Z轴回零超时", Brushes.Red);
                                HomeStep = 1000;
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.DetectGantry, AxisName.DetectModule1_Rotate) || Motion.IsMoveTimeOut(StationEnum.DetectGantry, AxisName.DetectModule2_Rotate))
                            {
                                OnShowMsg?.Invoke("AutoRun", "DetectGantry->检测模组Mura相机旋转轴回零超时", Brushes.Red);
                                HomeStep = 1000;
                            }
                            break;
                        }
                    case 20:
                        {
                            if (Motion.IsHomeDone(StationEnum.DetectGantry, AxisName.DetectGantry_Y1))
                            {
                                var info = AxisInfoHelper.GetAxisInfoByName("DetectGantry_Y2");
                                info.IsHomeOK = true;
                             
                                Motion.MoveAbs(StationEnum.DetectGantry, AxisName.DetectModule1_Z1, DetectGtyPosName.SafePos);
                                Motion.MoveAbs(StationEnum.DetectGantry, AxisName.DetectModule1_Z2, DetectGtyPosName.SafePos);
                                Motion.MoveAbs(StationEnum.DetectGantry, AxisName.DetectModule2_Z1, DetectGtyPosName.SafePos);
                                Motion.MoveAbs(StationEnum.DetectGantry, AxisName.DetectModule2_Z2, DetectGtyPosName.SafePos);
                                Motion.MoveAbs(StationEnum.DetectGantry, AxisName.DetectModule1_Rotate, DetectGtyPosName.DirectMuraGrabPos);
                                Motion.MoveAbs(StationEnum.DetectGantry, AxisName.DetectModule2_Rotate, DetectGtyPosName.DirectMuraGrabPos);
                                OnShowMsg?.Invoke("AutoRun", "DetectGantry->检测龙门轴回零完成，检测模组X轴开始回零", Brushes.Green);
                                HomeStep = 30;
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.DetectGantry, AxisName.DetectGantry_Y1))
                            {
                                OnShowMsg?.Invoke("AutoRun", "DetectGantry->检测龙门回零超时", Brushes.Red);
                                HomeStep = 1000;
                            }
                            break;
                        }
                    case 30:
                        {
                            if (Motion.IsHomeDone(StationEnum.DetectGantry, AxisName.DetectModule1_X) && Motion.IsHomeDone(StationEnum.DetectGantry, AxisName.DetectModule2_X))
                            {
                                OnShowMsg?.Invoke("AutoRun", "DetectGantry->检测模组X轴回零完成，检测龙门工位回零完成", Brushes.Green);
                                OnHomeStatus?.Invoke(new HomeEventArgs { StationName = StationName, StationNum = StationNum, HomeResult = true });
                                HomeStep = 0;
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.DetectGantry, AxisName.DetectModule1_X) || Motion.IsMoveTimeOut(StationEnum.DetectGantry, AxisName.DetectModule2_X))
                            {
                                OnShowMsg?.Invoke("AutoRun", "DetectGantry->检测模组X轴回零超时", Brushes.Red);
                                HomeStep = 1000;
                            }
                            break;
                        }
             
                    case 1000:
                        {
                            OnStop?.Invoke();
                            OnHomeStatus?.Invoke(new HomeEventArgs { StationName = StationName, StationNum = StationNum, HomeResult = false });
                            AlarmManager.CreateAlarm((int)MotionAlarmCode.DetectGantryStaHomeFail);
                            HomeStep = -1;
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                AlarmManager.CreateAlarm((int)MotionAlarmCode.DetectGantryStationException);
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
                    case 1://判断各轴都在初始位置
                        {
                            if (AxisToSafePosStep == 0)
                            {
                                OnTriggerSignal?.Invoke((int)StationCommand.IRGtyArrSafePos);
                                AutoRunStep = 10;
                                OnShowMsg?.Invoke("AutoRun", "DetectGantry->各轴都在初始位置，等待平台到达检测位置", Brushes.Green);
                            }
                            else
                            {
                                AxisToSafePos();
                            }
                            break;
                        }
                    case 10://等待平台到达检测位置
                        {
                            if (StageHasAtDetectPos)//收到stage工位发送的平台在检测位置指令
                            {
                                AutoRunStep = 20;
                                StageHasAtDetectPos = false;
                            }
                            break;
                        }
                    case 20:
                        {
                            if (ScanBarcodeReq)
                            {
                                OnShowMsg?.Invoke("AutoRun", "DetectGantry->收到扫码请求", Brushes.Green);
                                ScanBarcodeReq = false;
                                ScanBarcodeStep = 1;
                                AutoRunStep = 30;
                            }
                            else if (AoiDetectReq)
                            {
                                OnShowMsg?.Invoke("AutoRun", "DetectGantry->收到Aoi检测请求", Brushes.Green);
                                AoiDetectReq = false;
                                AutoRunStep = 40;
                                DetectStep = 1;
                            }
                            else if (MuraDetectReq)
                            {
                                OnShowMsg?.Invoke("AutoRun", "DetectGantry->收到直视Mura检测请求", Brushes.Green);
                                MuraDetectReq = false;
                                AutoRunStep = 50;
                                DetectStep = 1;
                            }
                            else if (MultiAngleDetectReq)
                            {
                                OnShowMsg?.Invoke("AutoRun", "DetectGantry->收到MultiAngle检测请求", Brushes.Green);
                                MultiAngleDetectReq = false;
                                AutoRunStep = 60;
                                DetectStep = 1;
                            }
                            else if (SquintMuraDetectReq)
                            {
                                OnShowMsg?.Invoke("AutoRun", "DetectGantry->收到斜视Mura检测请求", Brushes.Green);
                                SquintMuraDetectReq = false;
                                AutoRunStep = 70;
                                DetectStep = 1;
                            }
                            else if (DetectEndBit)
                            {
                                DetectEndBit = false;
                                OnShowMsg?.Invoke("AutoRun", "DetectGantry->检测完毕，检测工位各轴准备回初始位置", Brushes.Green);
                                AutoRunStep = 80;
                                AxisToSafePosStep = 1;
                            }
                            break;
                        }
                    case 30:
                        {
                            if (ScanBarcodeStep == 0)
                            {
                                AutoRunStep = 20;
                            }
                            else
                            {
                                ScanBarcode();
                            }
                            break;
                        }
                    case 40:
                        {
                            if (DetectStep == 0)
                            {
                                OnTriggerSignal?.Invoke((int)StationCommand.AoiDetectCom);
                                AutoRunStep = 20;
                            }
                            else
                            {
                                DetectFlow(DetectType.Aoi);
                            }
                            break;
                        }
                    case 50:
                        {
                            if (DetectStep == 0)
                            {
                                OnTriggerSignal?.Invoke((int)StationCommand.MuraDetectCom);
                                AutoRunStep = 20;
                            }
                            else
                            {
                                DetectFlow(DetectType.Mura);
                            }
                            break;
                        }
                    case 60:
                        {
                            if (DetectStep == 0)
                            {
                                OnTriggerSignal?.Invoke((int)StationCommand.MultiAngleDetectCom);
                                AutoRunStep = 20;
                            }
                            else
                            {
                                DetectFlow(DetectType.MultiAngle);
                            }
                            break;
                        }
                    case 70:
                        {
                            if (DetectStep == 0)
                            {
                                OnTriggerSignal?.Invoke((int)StationCommand.SquintMuraDetectCom);
                                AutoRunStep = 20;
                            }
                            else
                            {
                                DetectFlow(DetectType.SquintMura);
                            }
                            break;
                        }
                    case 80:
                        {
                            if (AxisToSafePosStep == 0)
                            {
                                OnShowMsg?.Invoke("AutoRun", "DetectGantry->检测完毕，检测工位各轴准备回初始位置", Brushes.Green);
                                AutoRunStep = 20;
                            }
                            else
                            {
                                AxisToSafePos();
                            }
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                AlarmManager.CreateAlarm((int)MotionAlarmCode.DetectGantryStationException);
                LogManager.GetLogger("AutoRun").Fatal(ex);
            }
            return true;
        }

        /// <summary>
        /// 工位初始化
        /// </summary>
        /// <returns></returns>
        public bool Init()
        {
            return true;
        }
        public bool Pause()
        {
            return true;
        }
        public bool Stop()
        {
            return true;
        }
        public bool ResetBits()
        {
            AxisToSafePosStep = 1;
            ScanBarcodeStep = -1;//扫码流程状态机步骤
            DetectStep = -1;
            StageHasAtDetectPos = false;//平台是否在测试位置     
            DetectEndBit = false;//单个panel检测结束
            ScanBarcodeReq = false;//单个panel检测结束
            AoiDetectReq = false;
            MuraDetectReq = false;
            SquintMuraDetectReq = false;
            MultiAngleDetectReq = false;
            StageZHomeCom = false;
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
            DualStage.OnTriggerSignal += StageTriggerHandle;
            ProbeGantry.OnTriggerSignal += ProbeGrantryTriggerHandle;
            return true;
        }
        #endregion
        /// <summary>
        /// 扫码流程
        /// </summary>
        private void ScanBarcode()
        {
            try
            {
                switch (ScanBarcodeStep)
                {
                    case 1:
                        {
                            TesterPanelID = string.Empty;
                            UpStreamID = string.Empty;
                            UpStreamJobData = new GlassDataA();
                            OnShowMsg?.Invoke("AutoRun", $"DetectGantry->扫码流程开始", Brushes.Green);
                            Motion.MoveAbs(StationEnum.DetectGantry, AxisName.DetectGantry_Y1, Global.CurDetectInfo.GlassNum == 0 ? DetectGtyPosName.ScanCode1Pos : DetectGtyPosName.ScanCode2Pos);
                            OnShowMsg?.Invoke("AutoRun", $"DetectGantry->检测龙门移动到Glass{Global.CurDetectInfo.GlassNum + 1}扫码位置", Brushes.Green);
                            if (Global.CurDetectInfo.GlassNum == 1)
                            {
                                OnTriggerSignal?.Invoke((int)StationCommand.DetectGantryMoving);
                            }
                            if (Global.IsAutoPosOnly==false)
                            {
                                Motion.MoveAbs(StationEnum.DetectGantry, AxisName.DetectModule1_X, Global.CurDetectInfo.GlassNum == 0 ? DetectGtyPosName.ScanCode1Pos : DetectGtyPosName.ScanCode2Pos);
                                var model = new JobDataDal().GetLatest(Global.CurDetectInfo.GlassNum);
                                if (model != null && string.IsNullOrEmpty(model.JobData) == false)
                                {
                                    UpStreamJobData.FromString(model.JobData);
                                    if (UpStreamJobData != null && !string.IsNullOrEmpty(UpStreamJobData.Glass_ID))
                                    {
                                        UpStreamID = UpStreamJobData.Glass_ID;
                                    }
                                }
                                else
                                {
                                    OnShowMsg?.Invoke("AutoRun", $"DetectGantry->上游JobData信息为空", Brushes.Red);
                                }
                                ScanBarcodeStep = 10;
                            }
                            else
                            {
                                Motion.MoveAbs(StationEnum.DetectGantry, AxisName.DetectModule1_X, DetectGtyPosName.SafePos);
                                ScanBarcodeStep = 90;
                            }
                            if ((Para.PCBase.IsSimulator||Param.Cim.CimShield) && string.IsNullOrEmpty(UpStreamID))
                            {
                                OnShowMsg?.Invoke("AutoRun", $"IRGantry->上游条码为空，模拟模式，使用日期码", Brushes.Green);
                                UpStreamID = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                                GetTempJobData(UpStreamID);
                            }
                            break;
                        }
                    case 10:
                        {
                            if (Motion.IsArrive(StationEnum.DetectGantry, AxisName.DetectGantry_Y1, Global.CurDetectInfo.GlassNum == 0 ? DetectGtyPosName.ScanCode1Pos : DetectGtyPosName.ScanCode2Pos))
                            {
                                if (Motion.IsArrive(StationEnum.DetectGantry, AxisName.DetectModule1_X, Global.CurDetectInfo.GlassNum == 0 ? DetectGtyPosName.ScanCode1Pos : DetectGtyPosName.ScanCode2Pos))
                                {
                                    OnTriggerSignal?.Invoke((int)StationCommand.DetectGantryArrived);
                                    OnShowMsg?.Invoke("AutoRun", $"DetectGantry->扫码枪到达扫码位置，开始扫码", Brushes.Green);
                                    ScanBarcodeStep = 20;
                                }
                                else if (Motion.IsMoveTimeOut(StationEnum.DetectGantry, AxisName.DetectModule1_X))
                                {
                                    ScanBarcodeStep = 1;
                                    OnShowMsg?.Invoke("AutoRun", $"DetectGantry->1号检测模组X轴移动到扫码位置超时", Brushes.Red);
                                    AlarmManager.CreateAlarm((int)MotionAlarmCode.DetectModule1_XMoveTimeout);
                                }
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.DetectGantry, AxisName.DetectGantry_Y1))
                            {
                                OnShowMsg?.Invoke("AutoRun", $"DetectGantry->检测龙门移动到扫码位置超时", Brushes.Red);
                                ScanBarcodeStep = 1;
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.DetectGantry_Y1MoveTimeout);
                            }

                            break;
                        }

                    case 20:
                        {
                            if (Param.Control.ScanerShield||Param.Control.RunMode==RunMode.NoDetect)
                            {
                                ScanerReadResult = true;
                                if (Param.Control.ScanerShield)
                                {
                                    OnShowMsg?.Invoke("AutoRun", $"DetectGantry->扫码枪屏蔽", Brushes.Blue);
                                }
                                if (Param.Control.RunMode == RunMode.NoDetect)
                                {
                                    OnShowMsg?.Invoke("AutoRun", $"DetectGantry->空跑模式", Brushes.Blue);
                                }
                                if (!string.IsNullOrEmpty(UpStreamID))
                                {
                                    ScanerID = UpStreamID;
                                    OnShowMsg?.Invoke("AutoRun", $"DetectGantry->使用从上游接收的条码：{UpStreamID}", Brushes.Green);
                                    ScanBarcodeStep = 60;
                                }
                                else
                                {
                                    OnShowMsg?.Invoke("AutoRun", $"DetectGantry->上游条码为空，请手动输入条码", Brushes.Green);
                                    ScanBarcodeStep = 40;
                                }
                            }
                            else
                            {
                                ScanBarcodeStep = 30;
                            }
                            break;
                        }
                    case 30:
                        {
                            ScanerReadResult = false;
                            string code = ScanerHelper.Instance.Scancode(0);
                            if (code != "NoRead")
                            {
                                ScanerID = code;
                                ScanerReadResult = true;
                                OnShowMsg?.Invoke("AutoRun", $"DetectGantry->扫码成功，条码：{code}", Brushes.Blue);
                                ScanBarcodeStep = 60;
                                ContinueFailCount = 0;
                            }
                            else
                            {
                                ContinueFailCount++;
                                OnShowMsg?.Invoke("AutoRun", $"DetectGantry->扫码失败，第{ContinueFailCount}次", Brushes.Red);
                                if (ContinueFailCount >= Param.Control.ScancodeFailCount)
                                {
                                    OnShowMsg?.Invoke("AutoRun", $"DetectGantry->扫码失败{Param.Control.ScancodeFailCount}次，手动输入条码", Brushes.Red);
                                    ScanBarcodeStep = 40;
                                }
                            }
                            break;
                        }
                    case 40:
                        {
                            KeyInCode = string.Empty;
                            OnKeyInCodeRequest?.Invoke(UpStreamID);
                            ScanBarcodeStep = 50;
                            break;
                        }
                    case 50:
                        {
                            if (string.IsNullOrEmpty(KeyInCode) == false)
                            {
                                if (string.IsNullOrEmpty(UpStreamID))
                                {
                                    UpStreamID = KeyInCode;
                                }
                                OnShowMsg?.Invoke("AutoRun", $"DetectGantry->手动输入条码：{KeyInCode}", Brushes.Blue);
                                ContinueFailCount = 0;
                                ScanerID = KeyInCode;
                                ScanBarcodeStep = 60;
                            }
                            break;
                        }
                    case 60:
                        {
                            OnUpdateGlassID?.Invoke(Global.CurDetectInfo.GlassNum, ScanerID);
                            ValidationResult = true;
                            TesterPanelID = ScanerID;
                            if (UpStreamID.Substring(0,2)=="TD")//TD开头的条码表示是离线模式，如果和扫码枪返回的条码不一致，不用请求Cim校验
                            {
                                if (ScanerID != UpStreamID)
                                {
                                    OnShowMsg?.Invoke("AutoRun", $"DetectGantry->扫码枪返回条码和上游下发条码不一致，离线模式直接使用上游条码", Brushes.Green);
                                    ScanerID = UpStreamID;
                                }
                            }
                            if (ScanerReadResult && ScanerID == UpStreamID)
                            {
                                OnShowMsg?.Invoke("AutoRun", $"DetectGantry->扫码成功，扫码枪条码和上游条码相同，不用校验", Brushes.Blue);
                                Report(ScanerID, 1, Global.CurDetectInfo.GlassNum);
                            }
                            else if (ScanerReadResult && ScanerID != UpStreamID)
                            {
                                OnShowMsg?.Invoke("AutoRun", $"DetectGantry->扫码成功，扫码枪条码和上游条码不同，请求Cim校验", Brushes.Blue);
                                bool ret = CimManager.Instance.VCRValidationRequest(ScanerID, UpStreamJobData,out GlassDataA jobDataReq);
                                if (ret)
                                {
                                    OnShowMsg?.Invoke("AutoRun", $"DetectGantry->Cim校验成功，扫码枪条码正确", Brushes.Green);
                                    Report(ScanerID, 2, Global.CurDetectInfo.GlassNum,true, jobDataReq);
                                }
                                else
                                {
                                    OnShowMsg?.Invoke("AutoRun", $"DetectGantry->扫码枪条码校验失败，校验上游条码", Brushes.Red);
                                    ret = CimManager.Instance.VCRValidationRequest(UpStreamID, UpStreamJobData, out  jobDataReq);
                                    if (ret)
                                    {
                                        OnShowMsg?.Invoke("AutoRun", $"DetectGantry->Cim校验成功，上游条码正确", Brushes.Green);
                                        Report(UpStreamID, 2, Global.CurDetectInfo.GlassNum);
                                        ScanerID = UpStreamID;
                                        TesterPanelID = UpStreamID;
                                    }
                                    else
                                    {
                                        OnShowMsg?.Invoke("AutoRun", $"DetectGantry->Cim校验失败，扫码枪条码和上游条码都不正确，手动输入条码", Brushes.Red);
                                        AlarmManager.CreateAlarm((int)CimAlarmCode.JobIdValidationFail);
                                        ScanerReadResult = false;
                                        ValidationResult = false;
                                    }
                                }
                            }
                            else if (!ScanerReadResult && ScanerID == UpStreamID)
                            {
                                OnShowMsg?.Invoke("AutoRun", $"DetectGantry->扫码失败，KeyIn条码和上游条码相同，不用校验", Brushes.Green);
                                Report(ScanerID, 3, Global.CurDetectInfo.GlassNum);
                            }
                            else if (!ScanerReadResult && ScanerID != UpStreamID)
                            {
                                OnShowMsg?.Invoke("AutoRun", $"DetectGantry->扫码失败，KeyIn条码和上游条码不同，请求Cim校验", Brushes.Green);
                                bool ret = CimManager.Instance.VCRValidationRequest(ScanerID, UpStreamJobData, out GlassDataA jobDataReq);
                                if (ret)
                                {
                                    OnShowMsg?.Invoke("AutoRun", $"DetectGantry->Cim校验成功，KeyIn条码正确", Brushes.Green);
                                    Report(ScanerID, 4, Global.CurDetectInfo.GlassNum, true, jobDataReq);
                                }
                                else
                                {
                                    OnShowMsg?.Invoke("AutoRun", $"DetectGantry->KeyIn条码校验失败，校验上游条码", Brushes.Red);
                                    ret = CimManager.Instance.VCRValidationRequest(UpStreamID, UpStreamJobData, out  jobDataReq);
                                    if (ret)
                                    {
                                        OnShowMsg?.Invoke("AutoRun", $"DetectGantry->Cim校验成功，上游条码正确", Brushes.Green);
                                        Report(UpStreamID, 4, Global.CurDetectInfo.GlassNum);
                                        ScanerID = UpStreamID;
                                        TesterPanelID = UpStreamID;
                                    }
                                    else
                                    {
                                        OnShowMsg?.Invoke("AutoRun", $"DetectGantry->Cim校验失败，KeyIn条码和上游条码都不正确，重新手动输入条码", Brushes.Red);
                                        AlarmManager.CreateAlarm((int)CimAlarmCode.JobIdValidationFail);
                                        ValidationResult = false;
                                    }
                                }
                            }
                            if (ValidationResult)
                            {
                                ScanBarcodeStep = 70;
                            }
                            else
                            {
                                ScanBarcodeStep = 40;
                            }
                            break;
                        }
                    case 70:
                        {
                            OnShowMsg?.Invoke("AutoRun", $"DetectGantry->1号检测模组X轴和Z1轴回初始位置", Brushes.Green);
                            Motion.MoveAbs(StationEnum.DetectGantry, AxisName.DetectModule1_X, DetectGtyPosName.SafePos);
                            ScanBarcodeStep = 80;
                            break;
                        }
                    case 80:
                        {
                            if (Motion.IsArrive(StationEnum.DetectGantry, AxisName.DetectModule1_X, DetectGtyPosName.SafePos))
                            {
                                OnShowMsg?.Invoke("AutoRun", $"DetectGantry->Review相机Z轴，X轴到达初始位置", Brushes.Green);
                                OnTriggerSignal?.Invoke((int)StationCommand.ScanCodeCom);
                                ScanBarcodeStep = 0;
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.DetectGantry, AxisName.DetectModule1_X))
                            {
                                ScanBarcodeStep = 70;
                                OnShowMsg?.Invoke("AutoRun", $"DetectGantry->1号检测模组X轴和Z1轴回初始位置超时", Brushes.Red);
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.DetectModule1_XMoveTimeout);
                            }
                            break;
                        }
                    case 90:
                        {
                            if (Motion.IsArrive(StationEnum.DetectGantry, AxisName.DetectGantry_Y1, Global.CurDetectInfo.GlassNum == 0 ? DetectGtyPosName.ScanCode1Pos : DetectGtyPosName.ScanCode2Pos))
                            {
                                if (Motion.IsArrive(StationEnum.DetectGantry, AxisName.DetectModule1_X, DetectGtyPosName.SafePos))
                                {
                                    OnTriggerSignal?.Invoke((int)StationCommand.ScanCodeCom);
                                    OnTriggerSignal?.Invoke((int)StationCommand.DetectGantryArrived);
                                    ScanBarcodeStep = 0;
                                }
                                else if (Motion.IsMoveTimeOut(StationEnum.DetectGantry, AxisName.DetectModule1_X))
                                {
                                    ScanBarcodeStep = 1;
                                    AlarmManager.CreateAlarm((int)MotionAlarmCode.DetectModule1_XMoveTimeout);
                                }
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.DetectGantry, AxisName.DetectGantry_Y1))
                            {
                                ScanBarcodeStep = 1;
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.DetectGantry_Y1MoveTimeout);
                            }
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                AlarmManager.CreateAlarm((int)MotionAlarmCode.DetectGantryStationException);
                LogManager.GetLogger("AutoRun").Fatal(ex);
            }
        }
        private void GetTempJobData(string glassID)
        {
            DateTime receiveTime = DateTime.Now;
            var dal = new JobDataDal();
            GlassDataA jobData = new GlassDataA();
            jobData.Glass_ID = glassID;
            jobData.Lot_ID = "1234";
            jobData.LOT_Sequence_No = 1000;
            jobData.Slot_Sequence_No = 1001;
            jobData.OPER_ID = "HYC";
            JobDataModel model1 = new JobDataModel { GlassNum = 0, ReceiveTime = receiveTime, GlassID = jobData.Glass_ID, JobData = jobData.ToString2() };
            dal.Insert(model1);
            if (Param.Control.IsSaveJobDataToLog)
            {
                LogManager.GetLogger("JobData").Info(jobData.ToString() + "\n");
            }
            var cellStartTimeDal = new CellStartTimeDal();
            cellStartTimeDal.Insert(new CellStartTimeModel { PanelID = jobData.Glass_ID, StartTime = receiveTime });
            UpStreamJobData = jobData;
        }
        /// <summary>
        /// 扫码上报
        /// </summary>
        /// <param name="code"></param>
        /// <param name="result"></param>
        /// <param name="glassIndex"></param>
        /// <param name="isDifferent"></param>
        /// <param name="jobData"></param>
        private void Report(string code,ushort result,int glassIndex,bool isDifferent = false,GlassDataA jobData=null)
        {
            Task.Factory.StartNew(()=> {
                try
                {
                    Ini.Write("GlassID", $"GlassID{glassIndex + 1}", code);
                    if (Para.PCBase.IsSimulator)
                    {
                        new CellStartTimeDal().Insert(new CellStartTimeModel { PanelID = code, StartTime = DateTime.Now });
                    }
                    if (jobData == null)
                    {
                        jobData = new GlassDataA();
                        var model = new JobDataDal().Get(code);
                        if (model!=null)
                        {
                            jobData.FromString(model.JobData);
                        }
                        else
                        {
                            jobData = new GlassDataA { Glass_ID= code };
                            OnShowMsg?.Invoke("AutoRun", $"DetectGantry->GlassDataA为空，生成新GlassDataA", Brushes.Green);
                        }
                    }
                    if (!Global.GlassDic.Keys.Contains(Global.CurDetectInfo.GlassNum))
                    {
                        Global.GlassDic.Add(Global.CurDetectInfo.GlassNum, new GlassModel { GlassID = Global.CurDetectInfo.GlassID, GlassNum = Global.CurDetectInfo.GlassNum });
                    }
                    if (Param.Cim.CimShield == false)
                    {
                        OnShowMsg?.Invoke("AutoRun", $"DetectGantry->上报VcrReadCompleteReport", Brushes.Green);
                        CimManager.Instance.VCREventReport(UpStreamID, ScanerID, UpStreamJobData, ScanerReadResult, Global.CurDetectInfo.GlassNum);
                    }
                    if (isDifferent)
                    {
                        if (glassIndex == 0)
                        {
                            new CellStartTimeDal().Update(Global.GlassID[0],code);
                            Global.GlassID[0] = code;
                        }
                        else
                        {
                            new CellStartTimeDal().Update(Global.GlassID[1], code);
                            Global.GlassID[1] = code;
                        }
                        var dal = new JobDataDal();
                        JobDataModel model = new JobDataModel { GlassNum = glassIndex, ReceiveTime = DateTime.Now, GlassID = code, JobData = jobData.ToString2() };
                        dal.Insert(model);
                    }
                  
                    TesterPC.Instance.NotifyGlassId(UpStreamID, TesterPanelID, jobData);
                    DBManager.InsertTotalInfo(code, glassIndex);
              
                }
                catch (Exception ex)
                {
                    OnShowMsg?.Invoke("AutoRun", $"DetectGantry->Report方法执行异常，异常信息：{ex}", Brushes.Red);
                }
            });
        }
        /// <summary>
        /// 检测流程
        /// </summary>
        /// <param name="detectType">检测类型</param>
        private void DetectFlow(DetectType detectType)
        {
            switch (DetectStep)
            {
                case 1:
                    {
                        if (Param.Panel.CellCountY % 2==0)
                        {
                            Module2DetectCellCount = Param.Panel.CellCountY / 2;
                        }
                        else
                        {
                            Module2DetectCellCount = Param.Panel.CellCountY / 2+1;
                        }
                        Module1DetectCellCount = Param.Panel.CellCountY - Module2DetectCellCount;
                        Module1DetectCellNum = Module1DetectCellCount-1;//1号检测模组从第3个Cell开始检测，如果CellCountY是9，则1号检测模组检测范围是3-0
                        Module2DetectCellNum = Param.Panel.CellCountY-1;//2号检测模组从第8个Cell开始检测，如果CellCountY是9，则2号检测模组检测范围是8-4
                        OnShowMsg?.Invoke("AutoRun", $"DetectGantry->检测模组1检测Cell数量：{Module1DetectCellCount}，检测模组2检测Cell数量：{Module2DetectCellCount}", Brushes.Blue);
                        var cell = GlassCrdHelper.GetOneCellCrd(Global.CurDetectInfo.ColumnNum, Param.Panel.CellCountY / 2);//获取中间Cell的龙门位置基准点
                        DetectGantryBasePos = BasePointHelper.GetDetectStaBasePt(GetCameraName(detectType, DetectModule.DetectModule1), Global.CurDetectInfo.GlassNum)+cell.X;
                        if (Motion.IsArrive(StationEnum.DetectGantry, AxisName.DetectGantry_Y1, DetectGantryBasePos)==false)
                        {
                            Motion.MoveAbs(StationEnum.DetectGantry, AxisName.DetectGantry_Y1, DetectGantryBasePos);
                        }
                        OnShowMsg?.Invoke("AutoRun", $"DetectGantry->检测龙门移动到第{Param.Panel.CellCountY / 2}个Cell正中心，检测龙门轴目标值：{DetectGantryBasePos}", Brushes.Green);
                        AoiPC.Instance.PowerControlReq(false, 0);
                        AoiPC.Instance.PowerControlReq(false, 1);
                        DetectStep = 10;
                        break;
                    }
                case 10:
                    {
                        if (Motion.IsArrive(StationEnum.DetectGantry, AxisName.DetectGantry_Y1, DetectGantryBasePos))
                        {
                            OnShowMsg?.Invoke("AutoRun", $"DetectGantry->检测龙门到达第{Param.Panel.CellCountY / 2}个Cell正中心，第{Global.CurDetectInfo.ColumnNum+1}列开始{detectType}检测", Brushes.Green);
                            Module1DetectStep = 1;
                            Module2DetectStep = 1;
                            DetectStep = 20;
                        }
                        else if (Motion.IsMoveTimeOut(StationEnum.DetectGantry, AxisName.DetectGantry_Y1))
                        {
                            OnShowMsg?.Invoke("AutoRun", $"DetectGantry->检测龙门移动到第{Param.Panel.CellCountY / 2}个Cell正中心超时", Brushes.Red);
                            AlarmManager.CreateAlarm((int)MotionAlarmCode.DetectGantry_Y1MoveTimeout);
                            DetectStep = 1;
                        }
                        break;
                    }
                case 20:
                    {
                        if (Module1DetectStep == 0&& Module2DetectStep == 0)
                        {
                            OnShowMsg?.Invoke("AutoRun", $"DetectGantry->第{Global.CurDetectInfo.ColumnNum + 1}列{detectType}检测完成，检测模组回到初始检测位置", Brushes.Green);

                            Module1DetectCellNum = Module1DetectCellCount - 1;
                            Module2DetectCellNum = Param.Panel.CellCountY - 1;
                            var cell = GlassCrdHelper.GetOneCellCrd(Global.CurDetectInfo.ColumnNum, Module1DetectCellNum);
                            Module1CamBasePos = BasePointHelper.GetDetectStaBasePt(GetCameraName(detectType, DetectModule.DetectModule1), Global.CurDetectInfo.GlassNum, false) - cell.Y;
                            var cell2 = GlassCrdHelper.GetOneCellCrd(Global.CurDetectInfo.ColumnNum, Module2DetectCellNum);
                            Module2CamBasePos = BasePointHelper.GetDetectStaBasePt(GetCameraName(detectType, DetectModule.DetectModule2), Global.CurDetectInfo.GlassNum, false) + cell2.Y;
                            Motion.MoveAbs(StationEnum.DetectGantry, AxisName.DetectModule1_X, Module1CamBasePos);
                            Motion.MoveAbs(StationEnum.DetectGantry, AxisName.DetectModule2_X, Module2CamBasePos);
                            DetectStep = 30;
                        }
                        else
                        {
                            Module1DetectFlow(detectType);
                            Module2DetectFlow(detectType);
                        }
                        break;
                    }
                case 30:
                    {
                        if (Motion.IsArrive(StationEnum.DetectGantry, AxisName.DetectModule1_X, Module1CamBasePos)&& Motion.IsArrive(StationEnum.DetectGantry, AxisName.DetectModule2_X, Module2CamBasePos))
                        {
                            OnShowMsg?.Invoke("AutoRun", $"DetectGantry->检测模组到达初始检测位置", Brushes.Green);
                            DetectStep = 0;
                        }
                        break;
                    }
            }
        }
        /// <summary>
        /// 1号检测模组检测流程
        /// </summary>
        /// <param name="detectType"></param>
        private void Module1DetectFlow(DetectType detectType)
        {
            switch (Module1DetectStep)
            {
                case 1:
                    {
                        if (Module1DetectCellNum>=0)
                        {
                            var cell = GlassCrdHelper.GetOneCellCrd(Global.CurDetectInfo.ColumnNum, Module1DetectCellNum);
                            Module1CamBasePos = BasePointHelper.GetDetectStaBasePt(GetCameraName(detectType, DetectModule.DetectModule1), Global.CurDetectInfo.GlassNum, false) - cell.Y;
                            if (Motion.GetAxisActPos(StationEnum.DetectGantry, AxisName.DetectModule2_X)+ Module1CamBasePos<Param.Motion.DetectModuleXPreventCrushPos)
                            {                               
                                Motion.MoveAbs(StationEnum.DetectGantry, AxisName.DetectModule1_X, Module1CamBasePos);
                                Module1RotatePosName = GetCamAxisRotatePosName(detectType);
                               Motion.MoveAbs(StationEnum.DetectGantry, AxisName.DetectModule1_Rotate, Module1RotatePosName);
                               OnShowMsg?.Invoke("AutoRun", $"DetectGantry->Module1Rotate轴旋转轴移动到位置{Module1RotatePosName}", Brushes.Green);
                                OnShowMsg?.Invoke("AutoRun", $"DetectGantry->Module1X轴移动到第{Module1DetectCellNum + 1}个Cell正中心，Module1X轴目标值：{Module1CamBasePos}", Brushes.Green);
                                Module1DetectStep = 10;
                            }
                            //else
                            //{
                            //    OnShowMsg?.Invoke("AutoRun", $"DetectGantry->2号检测模组正在检测第{Module2DetectCellNum}个Cell，1号检测模组X轴无法移动到第{Module1DetectCellNum + 1}个Cell中心，否则2个模组会相撞", Brushes.Red);
                            //}
                        }
                        else
                        {
                            OnShowMsg?.Invoke("AutoRun", $"DetectGantry->1号检测模组第{Global.CurDetectInfo.ColumnNum + 1}列{detectType}检测完成，{DetectModule1_Z}轴回起始位置", Brushes.Blue);
                            Motion.MoveAbs(StationEnum.DetectGantry, DetectModule1_Z, DetectGtyPosName.SafePos);
                            Module1DetectStep = 5;
                        }
                        break;
                    }
                case 5:
                    {
                        if (Motion.IsArrive(StationEnum.DetectGantry, DetectModule1_Z, DetectGtyPosName.SafePos))
                        {
                            Motion.MoveAbs(StationEnum.DetectGantry, AxisName.DetectModule1_X, DetectGtyPosName.SafePos);
                            OnShowMsg?.Invoke("AutoRun", $"DetectGantry->{DetectModule1_Z}轴回起始位置完成，{AxisName.DetectModule1_X}轴回起始位置", Brushes.Green);
                            Module1DetectStep = 7;
                        }
                        break;
                    }
                case 7:
                    {
                        if (Motion.IsArrive(StationEnum.DetectGantry, AxisName.DetectModule1_X, DetectGtyPosName.SafePos))
                        {
                            OnShowMsg?.Invoke("AutoRun", $"DetectGantry->{AxisName.DetectModule1_X}轴回起始位置完成", Brushes.Green);
                            Module1DetectStep = 0;
                        }
                    
                        break;
                    }
                case 10:
                    {
                        if (Motion.IsArrive(StationEnum.DetectGantry, AxisName.DetectModule1_X, Module1CamBasePos) && Motion.IsArrive(StationEnum.DetectGantry, AxisName.DetectModule1_Rotate, Module1RotatePosName))
                        {
                            Module1ZPosName= GetCamAxisZPosName(detectType, DetectModule.DetectModule1, Global.CurDetectInfo.GlassNum);
                            if (detectType== DetectType.MultiAngle)
                            {
                                DetectModule1_Z = AxisName.DetectModule1_Z2;
                            }
                            else
                            {
                                DetectModule1_Z = AxisName.DetectModule1_Z1;
                            }
                            OnShowMsg?.Invoke("AutoRun", $"DetectGantry->Module1X轴到达第{Module1DetectCellNum + 1}个Cell正中心，1号检测模组{DetectModule1_Z}轴下降到拍照位置", Brushes.Green);
                            Motion.MoveAbs(StationEnum.DetectGantry, DetectModule1_Z, Module1ZPosName);
                            TesterPC.Instance.TesterModeSwitch("Cell",0, Module1DetectCellNum);//发送给Tester的Cell范围3到0                         
                            Delay.Start(Param.Detect.TesterModeSwitchTimeOut);//Tester切换Cell模式和PG开电超时时间
                            OnShowMsg?.Invoke("AutoRun", $"DetectGantry->请求Tester切换第{Module1DetectCellNum + 1}个Cell到Cell模式", Brushes.Green);
                            Module1DetectStep = 20;
                        }
                        else if (Motion.IsMoveTimeOut(StationEnum.DetectGantry, AxisName.DetectModule1_X))
                        {
                            OnShowMsg?.Invoke("AutoRun", $"DetectGantry->Module1X轴到达第{Module1DetectCellNum + 1}个Cell正中心超时", Brushes.Red);
                            AlarmManager.CreateAlarm((int)MotionAlarmCode.DetectModule1_XMoveTimeout);
                            Module1DetectStep = 1;
                        }
                        else if (Motion.IsMoveTimeOut(StationEnum.DetectGantry, AxisName.DetectModule1_Rotate))
                        {
                            OnShowMsg?.Invoke("AutoRun", $"DetectGantry->Module1Rotate轴移动到{Module1RotatePosName}位置超时", Brushes.Red);
                            OnPause?.Invoke(-1);
                            Module1DetectStep = 1;
                        }
                        break;
                    }
                case 20:
                    {
                        if (Motion.IsArrive(StationEnum.DetectGantry, DetectModule1_Z, Module1ZPosName))
                        {
                            if (TesterPC.TesterModeSwitchResult1 == "OK")
                            {
                                Delay.Start(Param.Detect.PGPowerControlTimeOut);//Tester切换Cell模式和PG开电超时时间
                                AoiPC.Instance.PowerControlReq(true, 0);
                                OnShowMsg?.Invoke("AutoRun", $"DetectGantry->1号模组检测Z轴到达拍照位置，Tester切换到第{Module1DetectCellNum + 1}个Cell到Cell模式完成，请求PG1开电", Brushes.Green);
                                Module1DetectStep = 25;
                            }
                            else if (TesterPC.TesterModeSwitchResult1 == "NG")
                            {
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.Tester1ModeSwitchFail);
                                Module1DetectStep = 10;
                            }
                            else if (Delay.TimeUp())
                            {
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.Tester1ModeSwitchTimeout);
                                Module1DetectStep = 10;
                            }
                        }
                        else if (Motion.IsMoveTimeOut(StationEnum.DetectGantry, DetectModule1_Z))
                        {
                            OnShowMsg?.Invoke("AutoRun", $"DetectGantry->1号模组检测Z轴移动到拍照位置超时", Brushes.Red);
                            AlarmManager.CreateAlarm((int)MotionAlarmCode.DetectModule1_Z1MoveTimeout);
                            Module1DetectStep = 10;
                        }
                        break;
                    }
                case 25:
                    {
                        if (AoiPC.PC1PowerControlResult == "OK")
                        {
                            OnShowMsg?.Invoke("AutoRun", $"DetectGantry->PG1开电完成，请求AoiPC1进行{detectType}检测", Brushes.Green);
                            DetectReqArgs args = new DetectReqArgs();
                            args.DetectType = detectType.ToString();
                            args.GlassID = Global.CurDetectInfo.GlassID;
                            args.GlassNum = Global.CurDetectInfo.GlassNum;
                            args.ColumnNum = Global.CurDetectInfo.ColumnNum;
                            args.PCIndex = 0;//0：AoiPC1，1：AoiPC2
                            args.CellNum = Module1DetectCellNum;
                            args.CellID = Global.GetCellID(args.ColumnNum, Module1DetectCellNum);
                           // Task.Factory.StartNew(() => {
                                AoiPC.Instance.DetectReq(args);
                            //});
                            Delay.Start(Param.Detect.DetectTimeOut);
                            OnUpdateCellStatus?.Invoke(Global.CurDetectInfo, Module1DetectCellNum, "Detecting", detectType);
                            Module1DetectStep = 30;
                        }
                        else if (AoiPC.PC1PowerControlResult == "NG")
                        {
                            AlarmManager.CreateAlarm((int)MotionAlarmCode.PG1PowerOnFail);
                            Module1DetectStep = 20;
                        }
                        else if (Delay.TimeUp())
                        {
                            AlarmManager.CreateAlarm((int)MotionAlarmCode.PG1PowerOnTimeout);
                            Module1DetectStep = 20;
                        }
                        break;
                    }
                case 30:
                    {
                        if (string.IsNullOrEmpty(AoiPC.PC1DetectResultArgs.Result) == false)
                        {
                            OnShowMsg?.Invoke("AutoRun", $"DetectGantry->1号检测模组第{Module1DetectCellNum + 1}个Cell {detectType}检测完成，结果：{AoiPC.PC1DetectResultArgs.Result}，请求PG1关电", Brushes.Green);                       
                            AoiPC.Instance.PowerControlReq(false, 0);
                            Delay.Start(Param.Detect.PGPowerControlTimeOut);
                            Module1DetectStep = 40;
                        }
                        else if (Delay.TimeUp())
                        {
                            AlarmManager.CreateAlarm((int)MotionAlarmCode.PC1DetectTimeout);
                            OnShowMsg?.Invoke("AutoRun", $"DetectGantry->1号检测模组第{Module1DetectCellNum + 1}个Cell {detectType}检测超时", Brushes.Green);
                            Module1DetectStep = 25;
                        }
                        break;
                    }
                case 40:
                    {
                        if (AoiPC.PC1PowerControlResult == "OK")
                        {
                            OnShowMsg?.Invoke("AutoRun", $"DetectGantry->PG1关电完成，发送OneCellDetectEnd指令给TesterPC", Brushes.Green);
                            DBManager.UpdateOneCellResult(AoiPC.PC1DetectResultArgs);
                            //TesterPC.Instance.OneCellDetectEnd(0, Module1DetectCellNum);
                            OnUpdateCellStatus?.Invoke(Global.CurDetectInfo, Module1DetectCellNum, AoiPC.PC1DetectResultArgs.Result, detectType);
                            Module1DetectCellNum--;
                            Module1DetectStep = 1;
                        }
                        else if (AoiPC.PC1PowerControlResult == "NG")
                        {
                            Module1DetectStep = 30;
                            AlarmManager.CreateAlarm((int)MotionAlarmCode.PG1PowerOffFail);
                        }
                        else if (Delay.TimeUp())
                        {
                            Module1DetectStep = 30;
                            AlarmManager.CreateAlarm((int)MotionAlarmCode.PG1PowerOffTimeout);
                        }
                        break;
                    }
            }
        }
        /// <summary>
        /// 2号检测模组检测流程
        /// </summary>
        /// <param name="detectType"></param>
        private void Module2DetectFlow(DetectType detectType)
        {
            switch (Module2DetectStep)
            {
                case 1:
                    {
                        if (Module2DetectCellNum >= Module1DetectCellCount)//检测模组2从第8个Cell开始检测(索引从0开始)
                        {
                            //var cell2 = GlassCrdHelper.GetOneCellCrd(Global.CurDetectInfo.ColumnNum, Module1DetectCellCount-2 );//获取1号检测模组检测第一个Cell时X轴的位置
                            //Module1XDangerousPos = BasePointHelper.GetDetectStaBasePt(GetCameraName(detectType, DetectModule.DetectModule1), Global.CurDetectInfo.GlassNum, false) - cell2.Y;
                            var cell = GlassCrdHelper.GetOneCellCrd(Global.CurDetectInfo.ColumnNum, Module2DetectCellNum);
                            Module2CamBasePos = BasePointHelper.GetDetectStaBasePt(GetCameraName(detectType, DetectModule.DetectModule2), Global.CurDetectInfo.GlassNum, false) + cell.Y;
                            if (Motion.GetAxisActPos(StationEnum.DetectGantry, AxisName.DetectModule1_X) + Module2CamBasePos < Param.Motion.DetectModuleXPreventCrushPos)
                            {
                                Motion.MoveAbs(StationEnum.DetectGantry, AxisName.DetectModule2_X, Module2CamBasePos);
                                Module2RotatePosName = GetCamAxisRotatePosName(detectType);
                                Motion.MoveAbs(StationEnum.DetectGantry, AxisName.DetectModule2_Rotate, Module2RotatePosName);
                                OnShowMsg?.Invoke("AutoRun", $"DetectGantry->Module2Rotate轴旋转轴移动到位置{Module2RotatePosName}", Brushes.Green);
                                OnShowMsg?.Invoke("AutoRun", $"DetectGantry->Module2X轴移动到第{Module2DetectCellNum + 1}个Cell正中心，Module2X轴目标值：{Module2CamBasePos}", Brushes.Green);
                                Module2DetectStep = 10;
                            }
                            //else
                            //{
                            //    OnShowMsg?.Invoke("AutoRun", $"DetectGantry->1号检测模组正在检测第{Module1DetectCellNum}个Cell，2号检测模组X轴无法移动到第{Module2DetectCellNum + 1}个Cell中心，否则2个模组会相撞", Brushes.Red);
                            //}
                        }
                        else
                        {
                            OnShowMsg?.Invoke("AutoRun", $"DetectGantry->2号检测模组第{Global.CurDetectInfo.ColumnNum + 1}列{detectType}检测完成，{DetectModule2_Z}轴回起始位置", Brushes.Blue);
                            Motion.MoveAbs(StationEnum.DetectGantry, DetectModule2_Z, DetectGtyPosName.SafePos);
                            Module2DetectStep = 5;
                        }
                        break;
                    }
                case 5:
                    {
                        if (Motion.IsArrive(StationEnum.DetectGantry, DetectModule2_Z, DetectGtyPosName.SafePos))
                        {
                            OnShowMsg?.Invoke("AutoRun", $"DetectGantry->{DetectModule2_Z}轴回起始位置完成", Brushes.Green);
                            Module2DetectStep = 0;
                        }
                        break;
                    }
                case 10:
                    {
                        if (Motion.IsArrive(StationEnum.DetectGantry, AxisName.DetectModule2_X, Module2CamBasePos)&& Motion.IsArrive(StationEnum.DetectGantry, AxisName.DetectModule2_Rotate, Module2RotatePosName))
                        {
                            if (detectType == DetectType.MultiAngle)
                            {
                                DetectModule2_Z = AxisName.DetectModule2_Z2;
                            }
                            else
                            {
                                DetectModule2_Z = AxisName.DetectModule2_Z1;
                            }
                            OnShowMsg?.Invoke("AutoRun", $"DetectGantry->Module2X轴到达第{Module2DetectCellNum + 1}个Cell正中心，2号检测模组{DetectModule2_Z}轴下降到拍照位置", Brushes.Green);
                            Module2ZPosName = GetCamAxisZPosName(detectType, DetectModule.DetectModule2, Global.CurDetectInfo.GlassNum);
                            Motion.MoveAbs(StationEnum.DetectGantry, DetectModule2_Z, Module2ZPosName);
                            TesterPC.Instance.TesterModeSwitch("Cell",1, Module2DetectCellNum);//发送给Tester的Cell范围8到4
                            Delay2.Start(Param.Detect.TesterModeSwitchTimeOut);
                            OnShowMsg?.Invoke("AutoRun", $"DetectGantry->请求Tester切换第{Module2DetectCellNum + 1}个Cell到Cell模式", Brushes.Green);
                            Module2DetectStep = 20;
                        }
                        else if (Motion.IsMoveTimeOut(StationEnum.DetectGantry, AxisName.DetectModule2_X))
                        {
                            OnShowMsg?.Invoke("AutoRun", $"DetectGantry->Module2X轴到达第{Module2DetectCellNum + 1}个Cell正中心超时", Brushes.Red);
                            AlarmManager.CreateAlarm((int)MotionAlarmCode.DetectModule2_XMoveTimeout);
                            Module2DetectStep = 1;
                        }
                        else if (Motion.IsMoveTimeOut(StationEnum.DetectGantry, AxisName.DetectModule2_Rotate))
                        {
                            OnShowMsg?.Invoke("AutoRun", $"DetectGantry->Module2Rotate轴移动到{Module2RotatePosName}位置超时", Brushes.Red);
                            Module2DetectStep = 1;
                            OnPause?.Invoke(-1);
                        }
                        break;
                    }
                case 20:
                    {
                        if (Motion.IsArrive(StationEnum.DetectGantry, DetectModule2_Z, Module2ZPosName))
                        {
                            if (TesterPC.TesterModeSwitchResult2 == "OK")
                            {
                                Delay2.Start(Param.Detect.PGPowerControlTimeOut);//PG开电超时时间
                                AoiPC.Instance.PowerControlReq(true, 1);
                                OnShowMsg?.Invoke("AutoRun", $"DetectGantry->2号模组检测Z轴到达拍照位置，Tester切换到第{Module2DetectCellNum + 1}个Cell到Cell模式完成，请求PG2开电", Brushes.Green);
                                Module2DetectStep = 25;
                            }
                            else if (TesterPC.TesterModeSwitchResult2 == "NG")
                            {
                                Module2DetectStep = 10;
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.Tester2ModeSwitchFail);
                            }
                            else if (Delay2.TimeUp())
                            {
                                Module2DetectStep = 10;
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.Tester2ModeSwitchTimeout);
                            }
         
                        }
                        else if (Motion.IsMoveTimeOut(StationEnum.DetectGantry, DetectModule2_Z))
                        {
                            OnShowMsg?.Invoke("AutoRun", $"DetectGantry->2号检测模组{DetectModule2_Z}轴下降到拍照位置超时", Brushes.Red);
                            AlarmManager.CreateAlarm((int)MotionAlarmCode.DetectModule2_Z1MoveTimeout);
                            Module2DetectStep = 10;
                        }
                        break;
                    }
                case 25:
                    {
                        if (AoiPC.PC2PowerControlResult == "OK")
                        {
                            OnShowMsg?.Invoke("AutoRun", $"DetectGantry->PG2开电完成，请求AoiPC2进行{detectType}检测", Brushes.Green);
                            DetectReqArgs args = new DetectReqArgs();
                            args.DetectType = detectType.ToString();
                            args.GlassID = Global.CurDetectInfo.GlassID;
                            args.GlassNum = Global.CurDetectInfo.GlassNum;
                            args.ColumnNum = Global.CurDetectInfo.ColumnNum;
                            args.PCIndex = 1;//0：AoiPC1，1：AoiPC2
                            args.CellNum = Module2DetectCellNum;
                            args.CellID = Global.GetCellID(args.ColumnNum, Module2DetectCellNum);
                            //Task.Factory.StartNew(() => {
                                AoiPC.Instance.DetectReq(args);
                            //});
                            Delay2.Start(Param.Detect.DetectTimeOut);
                            OnUpdateCellStatus?.Invoke(Global.CurDetectInfo, Module2DetectCellNum, "Detecting", detectType);
                            Module2DetectStep = 30;
                        }
                        else if (AoiPC.PC2PowerControlResult == "NG")
                        {
                            Module2DetectStep = 20;
                            AlarmManager.CreateAlarm((int)MotionAlarmCode.PG2PowerOnFail);
                        }
                        else if (Delay2.TimeUp())
                        {
                            Module2DetectStep = 20;
                            AlarmManager.CreateAlarm((int)MotionAlarmCode.PG2PowerOnTimeout);
                        }
                        break;
                    }
                case 30:
                    {
                        if (string.IsNullOrEmpty(AoiPC.PC2DetectResultArgs.Result)==false)
                        {
                            OnShowMsg?.Invoke("AutoRun", $"DetectGantry->2号检测模组第{Module2DetectCellNum + 1}个Cell {detectType}检测完成，结果：{AoiPC.PC2DetectResultArgs.Result}，请求PG2关电", Brushes.Green);
                            AoiPC.Instance.PowerControlReq(false, 1);
                            Delay2.Start(Param.Detect.PGPowerControlTimeOut);
                            Module2DetectStep = 40;
                        }
                        else if (Delay2.TimeUp())
                        {
                            AlarmManager.CreateAlarm((int)MotionAlarmCode.PC2DetectTimeout);
                            OnShowMsg?.Invoke("AutoRun", $"DetectGantry->2号检测模组第{Module2DetectCellNum + 1}个Cell {detectType}检测超时", Brushes.Green);
                            Module2DetectStep = 25;
                        }
                        break;
                    }
                case 40:
                    {
                        if (AoiPC.PC2PowerControlResult == "OK")
                        {
                            OnShowMsg?.Invoke("AutoRun", $"DetectGantry->PG2关电完成，发送OneCellDetectEnd指令给TesterPC", Brushes.Green);
                            DBManager.UpdateOneCellResult(AoiPC.PC2DetectResultArgs);
                            //TesterPC.Instance.OneCellDetectEnd(1, Module2DetectCellNum);
                            OnUpdateCellStatus?.Invoke(Global.CurDetectInfo, Module2DetectCellNum, AoiPC.PC2DetectResultArgs.Result, detectType);
                            Module2DetectCellNum--;
                            Module2DetectStep = 1;
                        }
                        else if (AoiPC.PC2PowerControlResult == "NG")
                        {
                            Module2DetectStep = 30;
                            AlarmManager.CreateAlarm((int)MotionAlarmCode.PG2PowerOffFail);
                        }
                        else if (Delay2.TimeUp())
                        {
                            Module2DetectStep = 30;
                            AlarmManager.CreateAlarm((int)MotionAlarmCode.PG2PowerOffTimeout);
                        }
               
                        break;
                    }
            }
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
                        Delay.Start(30);
                        Motion.MoveAbs(StationEnum.DetectGantry, AxisName.DetectModule1_Z1, DetectGtyPosName.SafePos);
                        Motion.MoveAbs(StationEnum.DetectGantry, AxisName.DetectModule1_Z2, DetectGtyPosName.SafePos);
                        Motion.MoveAbs(StationEnum.DetectGantry, AxisName.DetectModule2_Z1, DetectGtyPosName.SafePos);
                        Motion.MoveAbs(StationEnum.DetectGantry, AxisName.DetectModule2_Z2, DetectGtyPosName.SafePos);
                        Motion.MoveAbs(StationEnum.DetectGantry, AxisName.DetectModule1_Rotate, DetectGtyPosName.SafePos);
                        Motion.MoveAbs(StationEnum.DetectGantry, AxisName.DetectModule2_Rotate, DetectGtyPosName.SafePos);
                        AxisToSafePosStep = 10;
                        OnShowMsg?.Invoke("AutoRun", "DetectGantry->检测模组Z轴回初始位置", Brushes.Green);
                        break;
                    }
                case 10:
                    {
                        if (Motion.IsArrive(StationEnum.DetectGantry, AxisName.DetectModule1_Z1, DetectGtyPosName.SafePos)&&
                            Motion.IsArrive(StationEnum.DetectGantry, AxisName.DetectModule1_Z2, DetectGtyPosName.SafePos)&&
                            Motion.IsArrive(StationEnum.DetectGantry, AxisName.DetectModule2_Z1, DetectGtyPosName.SafePos) &&
                            Motion.IsArrive(StationEnum.DetectGantry, AxisName.DetectModule2_Z2, DetectGtyPosName.SafePos))
                        {
                            Delay.Start(60);
                            Motion.MoveAbs(StationEnum.DetectGantry, AxisName.DetectGantry_Y1, DetectGtyPosName.SafePos);
                            Motion.MoveAbs(StationEnum.DetectGantry, AxisName.DetectModule1_X, DetectGtyPosName.SafePos);
                            Motion.MoveAbs(StationEnum.DetectGantry, AxisName.DetectModule2_X, DetectGtyPosName.SafePos);
                            OnShowMsg?.Invoke("AutoRun", $"DetectGantry->检测龙门和检测模组X轴回{DetectGtyPosName.SafePos}位置", Brushes.Green);
                            AxisToSafePosStep = 20;
                        }
                        else if (Delay.TimeUp())
                        {
                            OnShowMsg?.Invoke("AutoRun", "DetectGantry->检测模组Z轴回初始位置超时", Brushes.Red);
                            OnStop?.Invoke();
                        }
                        break;
                    }
                case 20:
                    {
                        if (Motion.IsArrive(StationEnum.DetectGantry, AxisName.DetectGantry_Y1, DetectGtyPosName.SafePos) &&
                            Motion.IsArrive(StationEnum.DetectGantry, AxisName.DetectModule1_X) &&
                            Motion.IsArrive(StationEnum.DetectGantry, AxisName.DetectModule2_X))
                        {
                            OnShowMsg?.Invoke("AutoRun", "DetectGantry->检测龙门工位回初始位置完成", Brushes.Green);
                            AxisToSafePosStep = 0;
                        }
                        else if (Delay.TimeUp())
                        {
                            OnShowMsg?.Invoke("AutoRun", "DetectGantry->检测龙门和检测模组X轴回初始位置超时", Brushes.Red);
                            OnStop?.Invoke();
                        }
                        break;
                    }
           
            }
        }
        /// <summary>
        /// 获取检测模组Z轴点位
        /// </summary>
        /// <param name="detectType">检测类型</param>
        /// <param name="moduleIndex">检测模组索引</param>
        /// <param name="stageIndex">平台索引，0到1</param>
        /// <returns></returns>
        private DetectGtyPosName GetCamAxisZPosName(DetectType detectType,DetectModule moduleIndex,int stageIndex)
        {
            DetectGtyPosName posName= DetectGtyPosName.Stg1AoiCam1GrabPos;
            switch (detectType)
            {
                case DetectType.Aoi:
                    {
                        if (stageIndex == 0)
                        {
                            posName = moduleIndex == 0 ? DetectGtyPosName.Stg1AoiCam1GrabPos : DetectGtyPosName.Stg1AoiCam2GrabPos;
                        }
                        else
                        {
                            posName = moduleIndex == 0 ? DetectGtyPosName.Stg2AoiCam1GrabPos : DetectGtyPosName.Stg2AoiCam2GrabPos;
                        }
                        break;
                    }
                case DetectType.Mura:
                    {
                        if (stageIndex == 0)
                        {
                            posName = moduleIndex == 0 ? DetectGtyPosName.Stg1MuraCam1GrabPos : DetectGtyPosName.Stg1MuraCam2GrabPos;
                        }
                        else
                        {
                            posName = moduleIndex == 0 ? DetectGtyPosName.Stg2MuraCam1GrabPos : DetectGtyPosName.Stg2MuraCam2GrabPos;
                        }
                        break;
                    }
                case DetectType.SquintMura:
                    {
                        if (stageIndex == 0)
                        {
                            posName = moduleIndex == 0 ? DetectGtyPosName.Stg1SquintMuraCam1GrabPos : DetectGtyPosName.Stg1SquintMuraCam2GrabPos;
                        }
                        else
                        {
                            posName = moduleIndex == 0 ? DetectGtyPosName.Stg2SquintMuraCam1GrabPos : DetectGtyPosName.Stg2SquintMuraCam2GrabPos;
                        }
                        break;
                    }
                case DetectType.MultiAngle:
                    {
                        if (stageIndex == 0)
                        {
                            posName = moduleIndex == 0 ? DetectGtyPosName.Stg1MultiAngle1GrabPos : DetectGtyPosName.Stg1MultiAngle2GrabPos;
                        }
                        else
                        {
                            posName = moduleIndex == 0 ? DetectGtyPosName.Stg2MultiAngle1GrabPos : DetectGtyPosName.Stg2MultiAngle2GrabPos;
                        }
                        break;
                    }
                default:
                    break;
            }
           
            return posName;
        }
       /// <summary>
       /// 获取Mura相机旋转轴点位名称
       /// </summary>
       /// <param name="detectType"></param>
       /// <returns></returns>
        private DetectGtyPosName GetCamAxisRotatePosName(DetectType detectType)
        {
            DetectGtyPosName posName = DetectGtyPosName.DirectMuraGrabPos;
            switch (detectType)
            {
                case DetectType.Mura:
                    {
                        posName = DetectGtyPosName.DirectMuraGrabPos;
                        break;
                    }
                case DetectType.SquintMura:
                    {
                        posName = DetectGtyPosName.SquintMuraGrabPos;
                        break;
                    }
                default:
                    break;
            }

            return posName;
        }
        /// <summary>
        /// 获取相机名称
        /// </summary>
        /// <param name="detectType">检测类型</param>
        /// <param name="moduleIndex">检测模组索引</param>
        /// <returns></returns>
        private CameraName GetCameraName(DetectType detectType, DetectModule moduleIndex)
        {
            CameraName cameraName = CameraName.AoiCam1;
            switch (detectType)
            {
                case DetectType.Aoi:
                    {
                        cameraName=moduleIndex == 0 ? CameraName.AoiCam1 : CameraName.AoiCam2;
                        break;
                    }
                case DetectType.Mura:
                    {
                        cameraName = moduleIndex == 0 ? CameraName.MuraCam1 : CameraName.MuraCam2;
                        break;
                    }
                case DetectType.MultiAngle:
                    {
                        cameraName = moduleIndex == 0 ? CameraName.MultiAngle1 : CameraName.MultiAngle2;
                        break;
                    }
                case DetectType.SquintMura:
                    {
                        cameraName = moduleIndex == 0 ? CameraName.SquintMuraCam1 : CameraName.SquintMuraCam2;
                        break;
                    }
                default:
                    break;
            }
            return cameraName;
        }
    }
    /// <summary>
    /// 点位名称
    /// </summary>
    public enum DetectGtyPosName
    {
        /// <summary>
        /// 每个轴的安全位置
        /// </summary>
        SafePos,
        /// <summary>
        /// 平台1Contact相机Z轴拍照位置
        /// </summary>
        Stg1CtcCamGrabPos_Z,
        /// <summary>
        /// 平台2Contact相机Z轴拍照位置
        /// </summary>
        Stg2CtcCamGrabPos_Z,

        /// <summary>
        /// 平台1扫码时检测龙门的位置
        /// </summary>
        ScanCode1Pos,
        /// <summary>
        /// 平台2扫码时检测龙门的位置
        /// </summary>
        ScanCode2Pos,
       // WaitPos,

        /// <summary>
        /// 探针在平台1第五列压接时，探针在Contact相机视野中心时检测龙门的位置
        /// </summary>
        Stg1GantryDetectPos,
        /// <summary>
        /// 探针在平台2第五列压接时，探针在Contact相机视野中心时检测龙门的位置
        /// </summary>
        Stg2GantryDetectPos,
        /// <summary>
        /// 1号检测模组平台1Aoi检测时Z轴位置
        /// </summary>
        Stg1AoiCam1GrabPos,
        /// <summary>
        /// 2号检测模组平台1Aoi检测时Z轴位置
        /// </summary>
        Stg1AoiCam2GrabPos,
        /// <summary>
        /// 1号检测模组平台1直视Mura检测时Z轴位置
        /// </summary>
        Stg1MuraCam1GrabPos,
        /// <summary>
        /// 2号检测模组平台1直视Mura检测时Z轴位置
        /// </summary>
        Stg1MuraCam2GrabPos,
        /// <summary>
        /// 1号检测模组平台1多角度检测时Z轴位置
        /// </summary>
        Stg1MultiAngle1GrabPos,
        /// <summary>
        /// 2号检测模组平台1多角度检测时Z轴位置
        /// </summary>
        Stg1MultiAngle2GrabPos,
        /// <summary>
        /// 1号检测模组平台2Aoi检测时Z轴位置
        /// </summary>
        Stg2AoiCam1GrabPos,
        /// <summary>
        /// 2号检测模组平台2Aoi检测时Z轴位置
        /// </summary>
        Stg2AoiCam2GrabPos,
        /// <summary>
        /// 1号检测模组平台2直视Mura检测时Z轴位置
        /// </summary>
        Stg2MuraCam1GrabPos,
        /// <summary>
        /// 2号检测模组平台2直视Mura检测时Z轴位置
        /// </summary>
        Stg2MuraCam2GrabPos,
        /// <summary>
        /// 1号检测模组平台2多角度检测时Z轴位置
        /// </summary>
        Stg2MultiAngle1GrabPos,
        /// <summary>
        /// 2号检测模组平台2多角度检测时Z轴位置
        /// </summary>
        Stg2MultiAngle2GrabPos,

        /// <summary>
        /// 1号检测模组平台1斜视Mura检测时Z轴位置
        /// </summary>
        Stg1SquintMuraCam1GrabPos,
        /// <summary>
        /// 2号检测模组平台1斜视Mura检测时Z轴位置
        /// </summary>
        Stg1SquintMuraCam2GrabPos,
        /// <summary>
        /// 1号检测模组平台2斜视Mura检测时Z轴位置
        /// </summary>
        Stg2SquintMuraCam1GrabPos,
        /// <summary>
        /// 2号检测模组平台2斜视Mura检测时Z轴位置
        /// </summary>
        Stg2SquintMuraCam2GrabPos,

        /// <summary>
        /// Mura相机旋转轴直视Mura拍照位置
        /// </summary>
        DirectMuraGrabPos,
        /// <summary>
        ///  Mura相机旋转轴斜视Mura拍照位置
        /// </summary>
        SquintMuraGrabPos,
      

    }

}
