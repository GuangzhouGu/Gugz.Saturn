/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2021 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnMain
// 文件名称    ：DualStage.cs
// =================================
// 创 建 者    ：guguangzhou
// 创建日期    ：2021/01/29 09:00:00 
// 功能描述    ：平台工位自动流程
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
using HYC.SaturnMain.CabinetInfos;
using HYC.SaturnMain.Entities;

using HYC.SaturnMain.Params;
using HYC.SaturnMain.Devices.Cim;
using HYC.SaturnMain.Devices.ClientPC;
using HYC.SaturnMain.Devices.Motion;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using HYC.SaturnMain.Alarm;
using HYC.SaturnMain;

namespace HYC.SaturnMain.Station
{
    public class DualStage : IStation
    {
        public static event Action<int> OnTriggerSignal;

        #region IStation接口实现
        public event Action<HomeEventArgs> OnHomeStatus;
        public event Action<string, string, Brush> OnShowMsg;
        public event Action OnStop;
        public event Action<int> OnPause;
        public int AutoRunStep { get; set; } = -1;
        public int HomeStep { get; set; } = -1;
        public MotionManager Motion { get; set; } = MotionManager.GetInstance();
        public int StationNum { get; set; } = 0;
        public string StationName { get; set; } = "DualStage";
        #endregion

        #region 状态标志位

        private bool DetectGantryIsAtSafePos = false;//检测龙门在安全位置
        private bool DetectEndBit = false;//检测结束

        private bool PrbAutoChgHomeSts = false;
        private bool PrbAutoChgYArriveSafePos = false;
        /// <summary>
        /// Probe是否需要更换标志位，1：需要更换Probe，0：不需要更换Probe
        /// </summary>
        private int IsNeedChangePrb = -1;
        private bool CabinetPrbChnCom = false;
        private bool PrbAutoChgCom = false;
        //private bool IsHaveArrDetectPos = false;

        #endregion
        #region 状态机运行step 
        private int AxisToSafePosStep = -1;//判断轴是否在安全位置流程状态机步骤
        private int CheckCylinderOrginPosStep = -1;//判断气缸是否在初始位置流程状态机步骤
        private int AlignGlassStep = -1;//规整glass流程状态机步骤
        private int MoveToDetectPosStep = -1;//平台移动到检测位置流程状态机步骤
        private int MoveToAutoChangePosStep = -1;
        private int DetectEndStep = -1;//检测结束流程状态机步骤
        private int MoveToAlignStep = -1;//平台移动到规整气缸规整位置流程状态机步骤
        private int MoveToLoadingStep = -1;//平台移动到上下料位置流程状态机步骤
        private int StagePinUpStep = -1;//平台Z轴上升流程状态机步骤
        private int StagePinDownStep = -1;//平台Z轴下降流程状态机步骤
        private int StageAlignClampStep = -1;//规整气缸加紧流程状态机步骤
        private int StageAlignUnClampStep = -1;//规整气缸松开流程状态机步骤
        private int StageAlignUpStep = -1;//规整气缸上升流程状态机步骤
        private int StageAlignDownStep = -1;//规整气缸下降流程状态机步骤
        private int StageVacuumOffStep = -1;//
        private int StageVacuumOnStep = -1;

        public int PrbAutoChgStep = -1;
        public int StageZUpStep = -1;
        public int StageZDownStep = -1;
        #endregion
        private Delay Delay = new Delay();
        private Delay Delay2 = new Delay();
        private bool IsHaveGlass = false;
        bool IsAtAutoChangePos = false;
        ProbeGantry Probe = null;
        public bool ResetBits()
        {
            AxisToSafePosStep = 1;
            CheckCylinderOrginPosStep = -1;//判断气缸是否在初始位置流程状态机步骤
            AlignGlassStep = -1;//规整glass流程状态机步骤
            MoveToDetectPosStep = -1;//平台移动到检测位置流程状态机步骤
            DetectEndStep = -1;//检测结束流程状态机步骤
            MoveToAlignStep = -1;//平台移动到规整气缸规整位置流程状态机步骤
            MoveToLoadingStep = -1;//平台移动到上下料位置流程状态机步骤
            StagePinUpStep = -1;//平台Z轴上升流程状态机步骤
            StagePinDownStep = -1;//平台Z轴下降流程状态机步骤
            StageAlignClampStep = -1;//规整气缸加紧流程状态机步骤
            StageAlignUnClampStep = -1;//规整气缸松开流程状态机步骤
            StageAlignUpStep = -1;//规整气缸上升流程状态机步骤
            StageAlignDownStep = -1;//规整气缸下降流程状态机步骤
            StageVacuumOffStep = -1;//
            StageVacuumOnStep = -1;
            MoveToAutoChangePosStep = -1;
            PrbAutoChgHomeSts = false;
            DetectGantryIsAtSafePos = false;//检测龙门在安全位置
            DetectEndBit = false;//检测结束
            IsHaveGlass = false;
            PrbAutoChgYArriveSafePos = false;
            IsAtAutoChangePos = false;
            return true;
        }

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
                            OnShowMsg?.Invoke("AutoRun", "DualStage->Stage工位开始回零", Brushes.Green);
                            OnShowMsg?.Invoke("AutoRun", "DualStage->平台Z轴和压接轴开始回零", Brushes.Green);
                            Motion.Vacuum(VacuumName.Stage1Vacuum, false);
                            Motion.Vacuum(VacuumName.Stage2Vacuum, false);
                            Motion.Home(StationEnum.DualStage, AxisName.Stage1_Z);
                            Motion.Home(StationEnum.DualStage, AxisName.Stage2_Z);
                            Motion.Home(StationEnum.DualStage, AxisName.Stage1_ContactZ);
                            Motion.Home(StationEnum.DualStage, AxisName.Stage2_ContactZ);
                            HomeStep = 10;
                            break;
                        }
                    case 10:
                        {
                            if (Motion.IsHomeDone(StationEnum.DualStage, AxisName.Stage1_Z) &&
                                Motion.IsHomeDone(StationEnum.DualStage, AxisName.Stage2_Z)&&
                                Motion.IsHomeDone(StationEnum.DualStage, AxisName.Stage1_ContactZ) &&
                                Motion.IsHomeDone(StationEnum.DualStage, AxisName.Stage2_ContactZ))
                            {
                                OnShowMsg?.Invoke("AutoRun", "DualStage->平台Z轴和压接轴回零完成，规整气缸回初始位", Brushes.Green);
                                StageAlignUnClampStep = 1;
                                StageAlignDownStep = 1;
                                HomeStep = 20;
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.DualStage, AxisName.Stage1_Z))
                            {
                                OnShowMsg?.Invoke("AutoRun", "DualStage->平台1Z轴回零超时", Brushes.Red);
                                HomeStep = 1000;
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.DualStage, AxisName.Stage2_Z))
                            {
                                OnShowMsg?.Invoke("AutoRun", "DualStage->平台2Z轴回零超时", Brushes.Red);
                                HomeStep = 1000;
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.DualStage, AxisName.Stage1_ContactZ))
                            {
                                OnShowMsg?.Invoke("AutoRun", "DualStage->平台1压接轴回零超时", Brushes.Red);
                                HomeStep = 1000;
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.DualStage, AxisName.Stage2_ContactZ))
                            {
                                OnShowMsg?.Invoke("AutoRun", "DualStage->平台2压接轴回零超时", Brushes.Red);
                                HomeStep = 1000;
                            }
                            break;
                        }
                    case 20:
                        {
                            if (StageAlignUnClampStep == 0)
                            {
                                if (StageAlignDownStep == 0)
                                {
                                    OnTriggerSignal?.Invoke((int)StationCommand.StageZHomeCom);
                                    OnShowMsg?.Invoke("AutoRun", "DualStage->规整气缸回初始位完成，等待Probe自动更换模组Y轴回零完成", Brushes.Green);
                                    Delay.Start(180);//等待Probe自动更换模组Y轴回零完成时间
                                    HomeStep = 25;
                                }
                                else
                                {
                                    StageAlignDown();
                                }
                            }
                            else
                            {
                                StageAlignUnClamp();
                            }
                            break;
                        }
                    case 25:
                        {
                            if (PrbAutoChgHomeSts)
                            {
                                PrbAutoChgHomeSts = false;
                                OnShowMsg?.Invoke("AutoRun", "DualStage->Probe自动更换工位回零完成，平台Y轴开始回零", Brushes.Green);
                                if (IsDetectGlass(0)) Motion.Vacuum(VacuumName.Stage1Vacuum, true);
                                if (IsDetectGlass(1)) Motion.Vacuum(VacuumName.Stage2Vacuum, true);
                                Motion.Home(StationEnum.DualStage, AxisName.Stage1_Y);
                                Motion.Home(StationEnum.DualStage, AxisName.Stage2_Y);
                                HomeStep = 30;
                            }
                            else if (Delay.TimeUp())
                            {
                                OnShowMsg?.Invoke("AutoRun", "DualStage->Probe自动更换工位回零超时", Brushes.Red);
                                HomeStep = 1000;
                            }
                            break;
                        }
                    case 30:
                        {
                            if (Motion.IsHomeDone(StationEnum.DualStage, AxisName.Stage1_Y) && Motion.IsHomeDone(StationEnum.DualStage, AxisName.Stage2_Y))
                            {
                                OnShowMsg?.Invoke("AutoRun", "DualStage->平台Y轴回零完成，移动到旋转位置", Brushes.Green);
                                Motion.MoveAbs(StationEnum.DualStage, AxisName.Stage1_Y, StagePosName.RotatePos);
                                Motion.MoveAbs(StationEnum.DualStage, AxisName.Stage2_Y, StagePosName.RotatePos);
                                HomeStep = 35;
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.DualStage, AxisName.Stage1_Y) || Motion.IsMoveTimeOut(StationEnum.DualStage, AxisName.Stage2_Y))
                            {
                                OnShowMsg?.Invoke("AutoRun", "DualStage->平台Y轴回零超时", Brushes.Red);
                                HomeStep = 1000;
                            }
                            break;
                        }
                    case 35:
                        {
                            if (Motion.IsArrive(StationEnum.DualStage, AxisName.Stage1_Y, StagePosName.RotatePos) && Motion.IsArrive(StationEnum.DualStage, AxisName.Stage2_Y, StagePosName.RotatePos))
                            {
                                OnShowMsg?.Invoke("AutoRun", "DualStage->平台Y轴到达旋转位置，平台T轴开始回零", Brushes.Green);
                                Motion.Home(StationEnum.DualStage, AxisName.Stage1_T);
                                Motion.Home(StationEnum.DualStage, AxisName.Stage2_T);
                                HomeStep = 40;
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.DualStage, AxisName.Stage1_Y) || Motion.IsMoveTimeOut(StationEnum.DualStage, AxisName.Stage2_Y))
                            {
                                OnShowMsg?.Invoke("AutoRun", "DualStage->平台Y轴移动超时", Brushes.Red);
                                HomeStep = 1000;
                            }
                            break;
                        }
                    case 40:
                        {
                            if (Motion.IsHomeDone(StationEnum.DualStage, AxisName.Stage1_T) && Motion.IsHomeDone(StationEnum.DualStage, AxisName.Stage2_T))
                            {
                                OnShowMsg?.Invoke("AutoRun", "DualStage->平台T轴回零完成，T轴去0度位置", Brushes.Green);
                                Motion.MoveAbs(StationEnum.DualStage, AxisName.Stage1_T, StagePosName.ZeroDegPos);
                                Motion.MoveAbs(StationEnum.DualStage, AxisName.Stage2_T, StagePosName.ZeroDegPos);
                                HomeStep = 50;
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.DualStage, AxisName.Stage1_T))
                            {
                                OnShowMsg?.Invoke("AutoRun", "DualStage->平台T轴回零超时", Brushes.Red);
                                HomeStep = 1000;
                            }
                            break;
                        }
                    case 50:
                        {
                            if (Motion.IsArrive(StationEnum.DualStage, AxisName.Stage1_T, StagePosName.ZeroDegPos) && Motion.IsArrive(StationEnum.DualStage, AxisName.Stage2_T, StagePosName.ZeroDegPos))
                            {
                                try
                                {
                                    if (Param.Motion.IsEnableBacklash)
                                    {
                                        Motion.SetBacklash(StationEnum.DualStage, AxisName.Stage1_T, Param.Motion.Stage1TBacklashValue, Param.Motion.Stage1TBacklashDir == BacklashDir.Postive ? 1 : -1);
                                        Motion.SetBacklash(StationEnum.DualStage, AxisName.Stage2_T, Param.Motion.Stage2TBacklashValue, Param.Motion.Stage2TBacklashDir == BacklashDir.Postive ? 1 : -1);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    OnShowMsg?.Invoke("AutoRun", $"DualStage->反向间隙补偿指令异常，异常信息：{ex.Message}", Brushes.Red);
                                }
                                OnShowMsg?.Invoke("AutoRun", "DualStage->平台T轴到达0度位置，设置反向间隙补偿", Brushes.Green);
                                StageAlignClampStep = 1;
                                HomeStep = 60;
                            }
                            break;
                        }
                    case 60:
                        {
                            if (StageAlignClampStep == 0)
                            {
                                OnShowMsg?.Invoke("AutoRun", "DualStage->DualStage工位回零完成", Brushes.Green);
                                OnHomeStatus?.Invoke(new HomeEventArgs { StationName = StationName, StationNum = StationNum, HomeResult = true });
                                HomeStep = 0;
                            }
                            else
                            {
                                StageAlignClamp();
                            }
                            break;
                        }
                    case 1000:
                        {
                            HomeStep = -1;
                            OnStop?.Invoke();
                            OnHomeStatus?.Invoke(new HomeEventArgs { StationName = StationName, StationNum = StationNum, HomeResult = false });
                            AlarmManager.CreateAlarm((int)MotionAlarmCode.DualStageHomeFail);
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                AlarmManager.CreateAlarm((int)MotionAlarmCode.DualStageStationException);
                OnHomeStatus?.Invoke(new HomeEventArgs { StationName = StationName, StationNum = StationNum, HomeResult = false });

            }
            return true;
        }

        /// <summary>
        /// 初始化工位
        /// </summary>
        /// <returns></returns>
        public bool Init()
        {
            Probe = StationManager.Instance.GetChild("ProbeGantry") as ProbeGantry;
            return true;
        }
        public bool RegisterEvent()
        {
            ProbeGantry.OnTriggerSignal += ProbeGantryTriggerHandle;
            DetectGantry.OnTriggerSignal += DetectGantryTriggerHandle;
            PrbAutoChg.OnTriggerSignal += PrbAutoChg_OnTriggerSignal;
            Cabinet.OnTriggerSignal += CabinetTriggerHandle;
            LinkSignalFlow.Instance.OnUpdatePPID += Instance_OnUpdatePPID;
            return true;
        }

        private void Instance_OnUpdatePPID(string ppid)
        {
            //首先判断接收的RecipeID和当前使用的RecipeID是否一致，如果不一致，需要切换机种
            if (string.IsNullOrEmpty(ppid) == false && ppid != RecipeManager.CurRecipe.PPID)
            {
                StationManager.Instance.ShowMsg("AutoRun", $"ReceivedPPID:{ppid}，CurrentPPID:{RecipeManager.CurRecipe.PPID}", Brushes.Blue);
                if (Param.Control.RecipeAutoChangeShield==false)
                {
                    Recipe recipe = RecipeManager.GetRecipeByPPID(ppid);
                    if (recipe != null)
                    {
                        //然后判断接收的机种的Probe名称和当前使用的机种的Probe名称是否一致
                        var curProbeName = ProbeCardHelper.GetProbeCard(RecipeManager.CurRecipe.RecipeID).ProbeCardName;
                        var receiveProbeName = ProbeCardHelper.GetProbeCard(recipe.RecipeID).ProbeCardName;
                        StationManager.Instance.ShowMsg("AutoRun", $"机种：{RecipeManager.CurRecipe.RecipeID}绑定的Probe名称：{curProbeName}。机种：{recipe.RecipeID}绑定的Probe名称：{receiveProbeName}。", Brushes.Blue);
                        if (curProbeName != receiveProbeName)
                        {
                            Global.ReceivedRecipeName = recipe.RecipeID;
                            IsNeedChangePrb = 1;//如果需要更换Probe，等到更换完Probe以后，再切换机种
                            StationManager.Instance.ShowMsg("AutoRun", $"当前使用的Probe和接收的机种绑定的Probe不一致，准备更换Probe", Brushes.Lime);
                        }
                        else
                        {
                            IsNeedChangePrb = 0;
                            //如果不需要更换Probe，立刻切换机种
                            bool ret = RecipeManager.ShiftRecipe(recipe.RecipeID, true);
                            if (ret == false)
                            {
                                OnStop?.Invoke();
                                StationManager.Instance.ShowMsg("AutoRun", $"切换机种：{recipe.RecipeID}失败，请检查机种参数", Brushes.Red);
                            }
                            else
                            {
                                StationManager.Instance.ShowMsg("AutoRun", $"切换机种：{recipe.RecipeID}完成", Brushes.Blue);
                            }
                        }
                    }
                    else
                    {
                        AlarmManager.CreateAlarm((int)MotionAlarmCode.PPIDNotExist);
                    }
                }
                else
                {
                    StationManager.Instance.ShowMsg("AutoRun", $"切换机种功能被屏蔽", Brushes.Blue);
                }
              
            }
        }

        private void PrbAutoChg_OnTriggerSignal(int obj)
        {
            switch (obj)
            {
                case (int)StationCommand.PrbAutoChgHomeCom:
                    {
                        PrbAutoChgHomeSts = true;
                        break;
                    }
                case (int)StationCommand.PrbAutoChgCom:
                    {
                        PrbAutoChgCom = true;
                        break;
                    }
                case (int)StationCommand.PrbAutoChgYArriveSafePos:
                    {
                        PrbAutoChgYArriveSafePos = true;
                        break;
                    }
            }
        }
        /// <summary>
        /// probe龙门事件处理
        /// </summary>
        /// <param name="cmd"></param>
        private void ProbeGantryTriggerHandle(int cmd)
        {
            switch (cmd)
            {
                case (int)StationCommand.DetectEnd:
                    {
                        DetectEndBit = true;
                        break;
                    }
            }
        }
        /// <summary>
        /// 检测龙门事件处理
        /// </summary>
        /// <param name="cmd"></param>
        private void DetectGantryTriggerHandle(int cmd)
        {
            switch (cmd)
            {
                case (int)StationCommand.IRGtyArrSafePos:
                    {
                        DetectGantryIsAtSafePos = true;
                        break;
                    }
            }
        }
        private void CabinetTriggerHandle(int cmd)
        {
            switch (cmd)
            {
                case (int)StationCommand.PrbAutoChgCom:
                    {
                        CabinetPrbChnCom = true;
                        break;
                    }
            }
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
                            //IsNeedChangePrb = -1;
                            CheckCylinderOrginPosStep = 1;
                            AutoRunStep = 10;
                            break;
                        }
                    case 10:
                        {
                            if (Motion.IsArrive(StationEnum.DualStage, AxisName.Stage1_ContactZ, StagePosName.SafePos) &&
                                Motion.IsArrive(StationEnum.DualStage, AxisName.Stage2_ContactZ, StagePosName.SafePos))// Stage下降， 等待Probe压头抬升到位，
                            {
                                if (CheckCylinderOrginPosStep == 0)//判断气缸是否在起始位置
                                {
                                    OnShowMsg?.Invoke("AutoRun", "DualStage->气缸在初始位置", Brushes.Green);
                                    AutoRunStep = 20;
                                    AxisToSafePosStep = 1;
                                }
                                else
                                {
                                    CylinderToOrginPos();
                                }
                            }
                            break;
                        }
                    case 20://判断轴是否在安全位置
                        {
                            if (AxisToSafePosStep == 0)//判断完毕
                            {
                                OnShowMsg?.Invoke("AutoRun", "DualStage->各轴在初始位置，等待Probe和检测工位各轴回初始位置", Brushes.Green);
                                AutoRunStep = 30;
                            }
                            else
                            {
                                AxisToSafePos();
                            }
                            break;
                        }
                    case 30://判断probe龙门和检测龙门是否在安全位置
                        {
                            if (DetectGantryIsAtSafePos)
                            {
                                OnShowMsg?.Invoke("AutoRun", "DualStage->检测龙门回初始位置完成，检测平台是否有Glass", Brushes.Green);
                                DetectGantryIsAtSafePos = false;
                                AutoRunStep = 40;
                            }
                            break;
                        }
                    case 40:
                        {
                            if (IsDetectGlass(0) || IsDetectGlass(1))  /// 有Glass
                            {
                                if (Param.Control.RunMode == RunMode.ColdRun) /// 千传 ，不做工艺
                                {
                                    Global.GlassID[0] = new JobDataDal().GetLatest(0)?.GlassID;
                                    Global.GlassID[1] = new JobDataDal().GetLatest(1)?.GlassID;
                                    AutoRunStep = 150;  //开始排片
                                    DetectEndStep = 1;
                                    DetectEndBit = true; //  检测结束
                                    OnShowMsg?.Invoke("AutoRun", "DualStage->平台检测到Glass，ColdRun模式，不检测", Brushes.Green);
                                }
                                else
                                {
                                    IsHaveGlass = true;
                                    AutoRunStep = 50;  /// 开始检测
                                    MoveToAlignStep = 1;
                                    OnShowMsg?.Invoke("AutoRun", "DualStage->平台检测到Glass，平台Y轴移动到规整Glass位置", Brushes.Green);
                                }
                            }
                            else//平台上没有检测到Glass
                            {
                                IsHaveGlass = false;
                                Global.InterfaceMode = 0;//Load模式
                                MoveToLoadingStep = 1;
                                OnShowMsg?.Invoke("AutoRun", "DualStage->平台上没有Glass，平台移动到上下料位置", Brushes.Green);
                                AutoRunStep = 70;
                            }
                            break;
                        }
                    case 50:
                        {
                            if (MoveToAlignStep == 0)//平台Y轴到位
                            {
                                AutoRunStep = 60;
                                AlignGlassStep = 1;
                                OnShowMsg?.Invoke("AutoRun", "DualStage->平台到达规整Glass位置，规整气缸规整Glass", Brushes.Green);
                            }
                            else
                            {
                                MoveToAlignPos();
                            }
                            break;
                        }
                    case 60://规整气缸规整glass
                        {
                            if (AlignGlassStep == 0)//规整完毕
                            {
                                if (DetectEndBit)//如果检测结束则去下料位置
                                {
                                    DetectEndBit = false;
                                    MoveToLoadingStep = 1;
                                    Global.InterfaceMode = 1;//exchange模式
                                    AutoRunStep = 70;
                                    OnShowMsg?.Invoke("AutoRun", "DualStage->规整气缸规整Glass完毕，平台移动到下料位置", Brushes.Green);
                                }//否则去检测位置
                                else
                                {
                                    AutoRunStep = 110;
                                    OnShowMsg?.Invoke("AutoRun", "DualStage->规整气缸规整Glass完毕，判断是否需要更换Probe", Brushes.Green);
                                }
                            }
                            else
                            {
                                AlignGlass();
                            }
                            break;
                        }

                    case 70://平台Y轴移动到上下料位置
                        {
                            if (MoveToLoadingStep == 0)//平台Y轴到位
                            {
                                StageZUpStep = 1;
                                AutoRunStep = 80;
                                OnShowMsg?.Invoke("AutoRun", "DualStage->平台到达下料位置，平台Z轴上升到接料位置", Brushes.Green);
                            }
                            else
                            {
                                MoveToLoadingPos();
                            }
                            break;
                        }
                    case 80:  ///  把 顶 Glass Z轴升起来开始交互
                        {
                            if (StageZUpStep == 0)
                            {
                                SequenceStepManager.StopWatch(SequenceName.UnloadTotal);
                                SequenceStepManager.StopWatch(SequenceName.OneCycle);
                                LinkSignalFlow.Instance.NormalInterfaceStep = 1;
                                Motion.SetDigitalOutput(OutputName.Stage1SensorPower, true);
                                Motion.SetDigitalOutput(OutputName.Stage2SensorPower, true);
                                AutoRunStep = 90;
                                OnShowMsg?.Invoke("AutoRun", "DualStage->开始和Robot交互", Brushes.Green);
                            }
                            else
                            {
                                StageZUp();
                            }
                            break;
                        }
                    case 90://开始上下游交互
                        {
                            if ((LinkSignalFlow.Instance.NormalInterfaceStep == 0) || Param.Cim.CimShield)
                            {
                                AutoRunStep = 100;//平台Z轴回安全位置，Shutter关闭
                                StageZDownStep = 1;
                                IsHaveGlass = false;
                                OnShowMsg?.Invoke("AutoRun", "DualStage->上下游交互完成，平台Z轴回安全位置", Brushes.Green);
                                SequenceStepManager.StartWatch(SequenceName.OneCycle);
                            }
                            else
                            {
                                LinkSignalFlow.Instance.ExChangeThread();
                            }
                            break;
                        }
                    case 100:
                        {
                            if (StageZDownStep == 0)
                            {
                                if (IsDetectGlass(0))
                                {
                                    if (IsDetectGlass(1))
                                    {
                                        if (Param.Control.RunMode == RunMode.ColdRun)
                                        {
                                            DetectEndBit = true;
                                        }
                                        IsHaveGlass = true;
                                        AutoRunStep = 60;//开始气缸规整glass
                                        AlignGlassStep = 1;
                                    }
                                    else
                                    {
                                        OnShowMsg?.Invoke("AutoRun", "DualStage->平台2Glass偏移，规整气缸无法规整", Brushes.Red);
                                        AlarmManager.CreateAlarm((int)MotionAlarmCode.Stage2GlassOffset);
                                    }
                                }
                                else
                                {
                                    OnShowMsg?.Invoke("AutoRun", "DualStage->平台1Glass偏移，规整气缸无法规整", Brushes.Red);
                                    AlarmManager.CreateAlarm((int)MotionAlarmCode.Stage1GlassOffset);
                                }
                            }
                            else
                            {
                                StageZDown();
                            }
                            break;
                        }
       
                    case 110:
                        {
                            if (IsNeedChangePrb == 1)//Cim下发的机种和设备当前机种不一致，需要更换Probe
                            {
                                IsNeedChangePrb = -1;
                                OnShowMsg?.Invoke("AutoRun", $"DualStage->接收的Glass机种名：{Global.ReceivedRecipeName}，与当前机种名：{RecipeManager.CurRecipe.RecipeID}不一致", Brushes.Blue);
                                if (Param.Control.ProbeAutoChangeShield)
                                {
                                    OnShowMsg?.Invoke("AutoRun", "DualStage->Probe自动更换屏蔽，平台移动到检测位置", Brushes.Green);
                                    MoveToDetectPosStep = 1;
                                    AutoRunStep = 120;
                                }
                                else
                                {
                                    OnShowMsg?.Invoke("AutoRun", "DualStage->平台移动到Probe自动更换位置", Brushes.Green);
                                    MoveToAutoChangePosStep = 1;
                                    AutoRunStep = 130;
                                }
                            }
                            else if (IsNeedChangePrb == 0 || IsHaveGlass)//Cim下发的机种和设备当前机种一致，判断UVW上是否有Probe
                            {
                                IsNeedChangePrb = -1;
                                if (IsHaveGlass)
                                {
                                    Global.ReceivedRecipeName = RecipeManager.CurRecipe.RecipeID;
                                }
                                OnShowMsg?.Invoke("AutoRun", "DualStage->接收的Glass机种名和设备当前机种一致，检测Uvw上是否有Probe", Brushes.Blue);
                                if (Motion.GetDigitalInput(InputName.UvwDetPrbLeft) == false && Motion.GetDigitalInput(InputName.UvwDetPrbRight) == false)
                                {
                                    OnShowMsg?.Invoke("AutoRun", "DualStage->Uvw上没有检测到Probe，平台移动到Probe自动更换位置", Brushes.Blue);
                                    MoveToAutoChangePosStep = 1;
                                    AutoRunStep = 130;
                                }
                                else
                                {
                                    OnShowMsg?.Invoke("AutoRun", "DualStage->Uvw上有Probe，平台移动到检测位置", Brushes.Blue);
                                    MoveToDetectPosStep = 1;
                                    AutoRunStep = 120;
                                }
                            }
                            break;
                        }
                    case 120:
                        {
                            if (MoveToDetectPosStep == 0)//平台Y轴到达检测位置
                            {
                                OnTriggerSignal?.Invoke((int)StationCommand.StgArrDetPos);
                                AutoRunStep = 150;
                                OnShowMsg?.Invoke("AutoRun", "DualStage->平台到达检测位置，开始检测", Brushes.Green);
                            }
                            else
                            {
                                StageMoveToDetectPos();
                            }
                            break;
                        }
                    case 130:
                        {
                            if (MoveToAutoChangePosStep == 0)
                            {
                                IsAtAutoChangePos = true;
                                OnShowMsg?.Invoke("AutoRun", "DualStage->平台到达Probe自动更换位置，Probe自动更换开始", Brushes.Green);
                                PrbAutoChgStep = 1;
                                if (Motion.GetDigitalInput(InputName.UvwDetPrbLeft) && Motion.GetDigitalInput(InputName.UvwDetPrbRight))
                                {
                                    OnShowMsg?.Invoke("AutoRun", "DualStage->Uvw上检测到Probe，执行Uvw->料仓->Uvw更换模式", Brushes.Green);
                                    Param.Control.PrbChgMode = PrbChgMode.UnloadLoad;
                                }
                                else
                                {
                                    OnShowMsg?.Invoke("AutoRun", "DualStage->Uvw上没有检测到Probe，执行料仓->Uvw更换模式", Brushes.Green);
                                    Param.Control.PrbChgMode = PrbChgMode.Load;
                                }
                                OnTriggerSignal?.Invoke((int)StationCommand.PrbAutoChg);
                                AutoRunStep = 140;
                            }
                            else
                            {
                                StageMoveToAutoChangePos();
                            }
                            break;
                        }
                    case 140:
                        {
                            if (PrbAutoChgStep == 0)
                            {
                                OnShowMsg?.Invoke("AutoRun", "DualStage->Probe更换完成，平台移动到检测位置", Brushes.Green);
                                MoveToDetectPosStep = 1;
                                AutoRunStep = 120;
                            }
                            else
                            {
                                PrbAutoChgFlow();
                            }
                            break;
                        }
                    case 150:
                        {
                            if (DetectEndBit)//这里先不复位DetectEndBit
                            {
                                DetectEndStep = 1;
                                AutoRunStep = 160;
                                OnShowMsg?.Invoke("AutoRun", "DualStage->检测结束，各轴去规整气缸规整位置", Brushes.Green);
                                SequenceStepManager.StartWatch(SequenceName.UnloadTotal);
                            }
                            break;
                        }
                    case 160://检测结束
                        {
                            if (DetectEndStep == 0)
                            {
                                AlignGlassStep = 1;
                                AutoRunStep = 60;
                                OnShowMsg?.Invoke("AutoRun", "DualStage->检测结束，准备规整Glass", Brushes.Green);
                                if (TesterPC.TesterManualDetect)
                                {
                                    TesterPC.TesterManualDetect = false;
                                    //TesterPC.Instance.MoveToOneColumnCom(true);
                                    OnStop?.Invoke();
                                    OnShowMsg?.Invoke("AutoRun", "DualStage->TesterPC手动检测结束", Brushes.Blue);
                                }
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
                AlarmManager.CreateAlarm((int)MotionAlarmCode.DualStageStationException);
                LogManager.GetLogger("AutoRun").Fatal(ex.ToString());
            }
            return true;
        }
        /// <summary>
        /// 自动更换Probe流程
        /// </summary>
        public void PrbAutoChgFlow()
        {
            switch (PrbAutoChgStep)
            {
                case 1:
                    {
                        if (CheckIsCanRun())
                        {
                            OnTriggerSignal?.Invoke(1000);//通知主界面开始更换
                            CabinetPrbChnCom = false;
                            PrbAutoChgCom = false;
                            MoveToAutoChangePosStep = 1;
                            PrbAutoChgStep = 5;
                        }
                        else
                        {
                            PrbAutoChgStep = -1;
                            OnStop?.Invoke();
                        }
                        break;
                    }
                case 5:
                    {
                        if (MoveToAutoChangePosStep == 0|| IsAtAutoChangePos)
                        {
                            IsAtAutoChangePos = false;
                            OnTriggerSignal?.Invoke((int)StationCommand.StageArriveAutoChgPos);
                            OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->开始更换Probe", Brushes.Blue);
                            PrbAutoChgStep = 10;
                        }
                        else
                        {
                            StageMoveToAutoChangePos();
                        }
                        break;
                    }
                case 10:
                    {
                        if (CabinetPrbChnCom && PrbAutoChgCom)
                        {
                            CabinetPrbChnCom = false;
                            PrbAutoChgCom = false;
                            PrbAutoChgStep = 0;                            
                            OnShowMsg?.Invoke("AutoRun", $"ProbeGantry->Probe更换完毕", Brushes.Blue);
                            OnTriggerSignal?.Invoke(1001);//通知主界面更换完毕
                        }
                        break;
                    }
            }
        }
     
        public bool Pause()
        {
            return true;
        }
        /// <summary>
        /// 判断各轴是否在安全起始位置
        /// </summary>
        public void AxisToSafePos()
        {
            switch (AxisToSafePosStep)
            {
                case 1:
                    {
                        if (Motion.IsArrive(StationEnum.DualStage, AxisName.Stage1_Z, StagePosName.SafePos) &&
                            Motion.IsArrive(StationEnum.DualStage, AxisName.Stage2_Z, StagePosName.SafePos))
                        {
                            if (Motion.IsArrive(StationEnum.DualStage, AxisName.Stage1_T, StagePosName.ZeroDegPos) &&
                                Motion.IsArrive(StationEnum.DualStage, AxisName.Stage2_T, StagePosName.ZeroDegPos))
                            {
                                AxisToSafePosStep = 0;//所有轴都在安全位置了退出当前状态机
                            }
                            else//如果T轴不在Zero位置，先把Y轴移动到T轴可以旋转的位置
                            {
                                //等待自动更换模组Y轴回到安全位置
                                if (PrbAutoChgYArriveSafePos)
                                {
                                    PrbAutoChgYArriveSafePos = false;
                                    Delay2.Start(60);
                                    if (Probe.IsContactZAtSafePos())
                                    {
                                        Motion.MoveAbs(StationEnum.DualStage, AxisName.Stage1_Y, StagePosName.RotatePos);
                                        Motion.MoveAbs(StationEnum.DualStage, AxisName.Stage2_Y, StagePosName.RotatePos);
                                        AxisToSafePosStep = 10;
                                        OnShowMsg?.Invoke("AutoRun", "DualStage->平台T轴不在初始位置，Y1轴去平台旋转位置", Brushes.Green);
                                    }
                                    else
                                    {
                                        OnShowMsg?.Invoke("AutoRun", "DualStage->压头不在安全位置，禁止移动平台，请检查程序逻辑", Brushes.Red);
                                        AlarmManager.CreateAlarm((int)MotionAlarmCode.StageMoveFail);
                                    }
                                }
                            }
                        }
                        else//如果Z轴不在安全位置，则回到安全位置，然后跳转到step20
                        {
                            Motion.MoveAbs(StationEnum.DualStage, AxisName.Stage1_Z, StagePosName.SafePos);
                            Motion.MoveAbs(StationEnum.DualStage, AxisName.Stage2_Z, StagePosName.SafePos);
                            AxisToSafePosStep = 20;
                            OnShowMsg?.Invoke("AutoRun", "DualStage->平台Z轴不在初始位置，准备回初始位置", Brushes.Green);
                        }
                        break;
                    }
                case 10://判断Y轴是否在T轴可以旋转的位置，如果在，则旋转T轴至Zero位置
                    {
                        if (Motion.IsArrive(StationEnum.DualStage, AxisName.Stage1_Y, StagePosName.RotatePos))
                        {
                            if (Motion.IsArrive(StationEnum.DualStage, AxisName.Stage2_Y, StagePosName.RotatePos))
                            {
                                if (Motion.IsArrive(StationEnum.ProbeGantry, AxisName.ProbeGantry_Y1, StagePosName.SafePos)) ///  为啥判断probe龙门 是不是多余的？
                                {
                                    Motion.MoveAbs(StationEnum.DualStage, AxisName.Stage1_T, StagePosName.ZeroDegPos);
                                    Motion.MoveAbs(StationEnum.DualStage, AxisName.Stage2_T, StagePosName.ZeroDegPos);
                                    AxisToSafePosStep = 15;
                                    OnShowMsg?.Invoke("AutoRun", "DualStage->Y轴到达平台旋转位置，T轴旋转到初始位置", Brushes.Green);
                                }
                                else if (Delay2.TimeUp())
                                {
                                    OnShowMsg?.Invoke("AutoRun", "DualStage->等待龙门移动到初始位置超时", Brushes.Red);
                                    AlarmManager.CreateAlarm((int)MotionAlarmCode.ProbeGantry_Y1MoveTimeout);
                                    AxisToSafePosStep = 1;
                                }
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.DualStage, AxisName.Stage2_Y))
                            {
                                OnShowMsg?.Invoke("AutoRun", "DualStage->平台2Y轴移动到旋转位置超时", Brushes.Red);
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.Stage2_YMoveTimeout);
                                AxisToSafePosStep = 1;
                            }
                        }
                        else if (Motion.IsMoveTimeOut(StationEnum.DualStage, AxisName.Stage1_Y))
                        {
                            OnShowMsg?.Invoke("AutoRun", "DualStage->平台1Y轴移动到旋转位置超时", Brushes.Red);
                            AlarmManager.CreateAlarm((int)MotionAlarmCode.Stage1_YMoveTimeout);
                            AxisToSafePosStep = 1;
                        }
                        break;
                    }

                case 15://判断T轴是否在Zero位置，如果在，跳转回step1，继续判断Y轴
                    {
                        if (Motion.IsArrive(StationEnum.DualStage, AxisName.Stage1_T, StagePosName.ZeroDegPos))
                        {
                            if (Motion.IsArrive(StationEnum.DualStage, AxisName.Stage2_T, StagePosName.ZeroDegPos))
                            {
                                AxisToSafePosStep = 0;
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.DualStage, AxisName.Stage2_T))
                            {
                                OnShowMsg?.Invoke("AutoRun", "DualStage->平台2T轴移动到0度位置超时", Brushes.Red);
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.Stage2_TMoveTimeout);
                                AxisToSafePosStep = 10;
                            }
                        }
                        else if (Motion.IsMoveTimeOut(StationEnum.DualStage, AxisName.Stage1_T))
                        {
                            OnShowMsg?.Invoke("AutoRun", "DualStage->平台1T轴移动到0度位置超时", Brushes.Red);
                            AlarmManager.CreateAlarm((int)MotionAlarmCode.Stage1_TMoveTimeout);
                            AxisToSafePosStep = 10;
                        }
                        break;
                    }
                case 20://判断Z轴是否在安全位置，如果在，跳转回step1，继续判断T轴
                    {
                        if (Motion.IsArrive(StationEnum.DualStage, AxisName.Stage1_Z, StagePosName.SafePos))
                        {
                            if (Motion.IsArrive(StationEnum.DualStage, AxisName.Stage2_Z, StagePosName.SafePos))
                            {
                                if (IsDetectGlass(0))
                                {
                                    Motion.Vacuum(VacuumName.Stage1Vacuum, true);
                                }
                                if (IsDetectGlass(1))
                                {
                                    Motion.Vacuum(VacuumName.Stage2Vacuum, true);
                                }
                                AxisToSafePosStep = 1;
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.DualStage, AxisName.Stage2_Z))
                            {
                                OnShowMsg?.Invoke("AutoRun", "DualStage->平台2Z轴移动到初始位置超时", Brushes.Red);
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.Stage2_ZMoveTimeout);
                                AxisToSafePosStep = 1;
                            }
                        }
                        else if (Motion.IsMoveTimeOut(StationEnum.DualStage, AxisName.Stage1_Z))
                        {
                            OnShowMsg?.Invoke("AutoRun", "DualStage->平台1Z轴移动到初始位置超时", Brushes.Red);
                            AlarmManager.CreateAlarm((int)MotionAlarmCode.Stage1_ZMoveTimeout);
                            AxisToSafePosStep = 1;
                        }
                        break;
                    }

            }
        }
        /// <summary>
        /// 判断气缸是否在初始位置
        /// </summary>
        public void CylinderToOrginPos()
        {
            switch (CheckCylinderOrginPosStep)
            {
                case 1://
                    {
                        if (IsClyDown(0) && IsClyDown(1))//先判断气缸是否在下降位置
                        {
                            if (IsClyClamp(0) && IsClyClamp(1))//再判断气缸是否在夹紧位置
                            {
                                Motion.SetDigitalOutput(OutputName.Stage1Blow, false);//关闭吹气
                                Motion.SetDigitalOutput(OutputName.Stage2Blow, false);
                                Motion.SetDigitalOutput(OutputName.Stage1Ionizer, false);//关闭离子发生器
                                Motion.SetDigitalOutput(OutputName.Stage2Ionizer, false);
                                CheckCylinderOrginPosStep = 0;
                                OnShowMsg?.Invoke("AutoRun", "DualStage->规整气缸都在初始位置", Brushes.Green);
                            }
                            else
                            {
                                StageAlignClampStep = 1;    /// 夹紧气缸全部伸出
                                CheckCylinderOrginPosStep = 15;
                                OnShowMsg?.Invoke("AutoRun", "DualStage->Clamp气缸不在初始位置，回初始位置", Brushes.Green);
                            }
                        }
                        else
                        {
                            StageAlignUnClampStep = 1;
                            CheckCylinderOrginPosStep = 5;
                            OnShowMsg?.Invoke("AutoRun", "DualStage->Down气缸不在初始位置，回初始位置", Brushes.Green);
                        }

                        break;
                    }
                case 5://气缸松开
                    {
                        if (StageAlignUnClampStep == 0)
                        {
                            StageAlignDownStep = 1;
                            CheckCylinderOrginPosStep = 10;
                        }
                        else
                        {
                            StageAlignUnClamp();
                        }

                        break;
                    }
                case 10://气缸下降
                    {
                        if (StageAlignDownStep == 0)
                        {
                            CheckCylinderOrginPosStep = 1;
                        }
                        else
                        {
                            StageAlignDown();
                        }
                        break;
                    }
                case 15://气缸夹紧
                    {
                        if (StageAlignClampStep == 0)
                        {
                            CheckCylinderOrginPosStep = 1;
                        }
                        else
                        {
                            StageAlignClamp();
                        }
                        break;
                    }

            }
        }
        /// <summary>
        /// 平台规整气缸规整流程
        /// </summary>
        public void AlignGlass()
        {
            if (Probe.IsContactZAtSafePos())
            {
                switch (AlignGlassStep)
                {
                    case 1://判断平台Y轴是否在气缸规整位置
                        {
                            SequenceStepManager.StartWatch(DetectEndBit ? SequenceName.GlassCenter_Unload : SequenceName.GlassCenter_Load);
                            Motion.MoveAbs(StationEnum.DualStage, AxisName.Stage1_Y, StagePosName.AlignPos);
                            Motion.MoveAbs(StationEnum.DualStage, AxisName.Stage2_Y, StagePosName.AlignPos);
                            Delay.Start(60);
                            AlignGlassStep = 10;
                            OnShowMsg?.Invoke("AutoRun", "DualStage->平台Y轴去气缸规整位置，等待龙门到达初始位置", Brushes.Green);
                            break;
                        }
                    case 10:
                        {
                            if (Motion.IsArrive(StationEnum.DualStage, AxisName.Stage1_Y, StagePosName.AlignPos) && 
                                Motion.IsArrive(StationEnum.DualStage, AxisName.Stage2_Y, StagePosName.AlignPos))
                            {
                                if (Motion.IsArrive(StationEnum.ProbeGantry,AxisName.ProbeGantry_Y1,ProbeGantryPosName.SafePos)&&
                                    Motion.IsArrive(StationEnum.DetectGantry, AxisName.DetectGantry_Y1, DetectGtyPosName.SafePos))
                                {
                                    StageVacuumOffStep = 1; ///  关真空
                                    AlignGlassStep = 30;
                                    OnShowMsg?.Invoke("AutoRun", "DualStage->平台Y轴到达气缸规整位置，龙门到达初始位置，真空关闭", Brushes.Green);
                                }
                                else if(Delay.TimeUp())
                                {
                                    AlignGlassStep = 1;
                                    OnShowMsg?.Invoke("AutoRun", "DualStage->等待龙门到达初始位置超时", Brushes.Red);
                                }
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.DualStage, AxisName.Stage1_Y))
                            {
                                OnShowMsg?.Invoke("AutoRun", "DualStage->平台1Y轴移动到规整位置超时", Brushes.Red);
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.Stage1_YMoveTimeout);
                                AlignGlassStep = 1;
                            }
                            break;
                        }
                    case 30:   // 关闭真空
                        {
                            if (StageVacuumOffStep == 0)
                            {
                                StageAlignUnClampStep = 1;
                                AlignGlassStep = 40;
                            }
                            else
                            {
                                StageVacuumOff();
                            }
                            break;
                        }
                    case 40://规整气缸松开
                        {
                            if (StageAlignUnClampStep == 0)//松开到位
                            {
                                OnShowMsg?.Invoke("AutoRun", "DualStage->规整气缸松开到位，规整气缸上升", Brushes.Green);
                                StageAlignUpStep = 1;
                                AlignGlassStep = 60;
                            }
                            else
                            {
                                StageAlignUnClamp();
                            }
                            break;
                        }
                    case 60://规整气缸上升
                        {
                            if (StageAlignUpStep == 0)//上升到位
                            {
                                Delay.Start(Param.Motion.StageBlowTime);//吹气延时
                                Motion.SetDigitalOutput(OutputName.Stage1Blow, true);//平台吹气，把产品吹起
                                Motion.SetDigitalOutput(OutputName.Stage2Blow, true);
                                AlignGlassStep = 65;
                                OnShowMsg?.Invoke("AutoRun", $"DualStage->规整气缸上升到位，打开吹气", Brushes.Green);
                            }
                            else
                            {
                                StageAlignUp();
                            }
                            break;
                        }
                    case 65: ///  开始夹紧
                        {
                            if (Delay.TimeUp())
                            {
                                StageAlignClampStep = 1;
                                AlignGlassStep = 70;
                                OnShowMsg?.Invoke("AutoRun", $"DualStage->规整气缸夹紧", Brushes.Green);
                            }
                            break;
                        }
                    case 70://规整气缸夹紧
                        {
                            if (StageAlignClampStep == 0)//夹紧到位
                            {
                                Delay.Start(Param.Motion.StageAlignTime);//延时
                                AlignGlassStep = 80;
                                OnShowMsg?.Invoke("AutoRun", "DualStage->规整气缸夹紧到位", Brushes.Green);
                            }
                            else
                            {
                                StageAlignClamp();
                            }
                            break;
                        }
                    case 80://平台吸真空
                        {
                            if (Delay.TimeUp())
                            {
                                Motion.SetDigitalOutput(OutputName.Stage1Blow, false);//关闭吹气
                                Motion.SetDigitalOutput(OutputName.Stage2Blow, false);
                                if (DetectEndBit) /// 检测结束，规整完后是不吸附的
                                {
                                    AlignGlassStep = 90;
                                    StageAlignUnClampStep = 1;
                                    OnShowMsg?.Invoke("AutoRun", "DualStage->规整气缸松开", Brushes.Green);
                                }
                                else  ///  多一个开真空步骤
                                {
                                    StageVacuumOnStep = 1;
                                    AlignGlassStep = 120;
                                    OnShowMsg?.Invoke("AutoRun", "DualStage->平台吸真空", Brushes.Green);
                                }
                            }
                            break;
                        }
                    case 90: /// 夹紧松开
                        {
                            if (StageAlignUnClampStep == 0)
                            {
                                StageAlignDownStep = 1;
                                AlignGlassStep = 100;
                                OnShowMsg?.Invoke("AutoRun", "DualStage->规整气缸松开到位，规整气缸下降", Brushes.Green);
                            }
                            else
                            {
                                StageAlignUnClamp();
                            }
                            break;
                        }

                    case 100://规整气缸下降
                        {
                            if (StageAlignDownStep == 0)
                            {
                                StageAlignClampStep = 1;
                                AlignGlassStep = 110;
                                OnShowMsg?.Invoke("AutoRun", "DualStage->规整气缸下降到位，规整气缸夹紧", Brushes.Green);
                            }
                            else
                            {
                                StageAlignDown();
                            }
                            break;
                        }
                    case 110://规整气缸夹紧
                        {
                            if (StageAlignClampStep == 0)
                            {
                                AlignGlassStep = 0;
                                OnShowMsg?.Invoke("AutoRun", "DualStage->规整完毕", Brushes.Green);
                                SequenceStepManager.StopWatch(DetectEndBit ? SequenceName.GlassCenter_Unload : SequenceName.GlassCenter_Load);
                            }
                            else
                            {
                                StageAlignClamp();
                            }
                            break;
                        }
                    case 120://平台吸真空
                        {
                            if (StageVacuumOnStep == 0)
                            {
                                StageAlignUnClampStep = 1;
                                AlignGlassStep = 90;
                            }
                            else
                            {
                                StageVacuumOn();
                            }
                            break;
                        }
                }
            }
            else
            {
                OnShowMsg?.Invoke("AutoRun", "DualStage->压头不在安全位置，禁止移动平台，请检查程序逻辑", Brushes.Red);
                AlarmManager.CreateAlarm((int)MotionAlarmCode.StageMoveFail);
                OnStop?.Invoke();
            }
        }
        public void StageZDown()
        {
            switch (StageZDownStep)
            {
                case 1:
                    {
                        if (Motion.GetDigitalInput(InputName.RobotDetect))
                        {
                            if (Motion.GetDigitalInput(InputName.RobotArmSignal1)&& Motion.GetDigitalInput(InputName.RobotArmSignal1))
                            {
                                SequenceStepManager.StartWatch(SequenceName.PinDown);
                                SequenceStepManager.StartWatch(SequenceName.LoadTotal);
                                Motion.MoveAbs(StationEnum.DualStage, AxisName.Stage1_Z, StagePosName.DownFirstPos);
                                Motion.MoveAbs(StationEnum.DualStage, AxisName.Stage2_Z, StagePosName.DownFirstPos);
                                Motion.Cylinder(CylinderName.ShutterClyL, true);
                                Motion.SetDigitalOutput(OutputName.Stage1Ionizer, true);
                                Motion.SetDigitalOutput(OutputName.Stage2Ionizer, true);
                                OnShowMsg?.Invoke("AutoRun", "DualStage->平台Z轴慢速下降，Shutter关闭", Brushes.Green);
                                StageZDownStep = 10;
                            }
                            else
                            {
                                AlarmManager.CreateAlarm((int)CimAlarmCode.RobotArmSignalOff);
                            }
                        }
                        else
                        {
                            AlarmManager.CreateAlarm((int)CimAlarmCode.RobotInStretchPos);
                        }
                        break;
                    }
                case 10:
                    {
                        if (Motion.IsArrive(StationEnum.DualStage, AxisName.Stage1_Z, StagePosName.DownFirstPos)&& Motion.IsArrive(StationEnum.DualStage, AxisName.Stage2_Z, StagePosName.DownFirstPos))
                        {
                            Motion.MoveAbs(StationEnum.DualStage, AxisName.Stage1_Z, StagePosName.DownSecondPos);
                            Motion.MoveAbs(StationEnum.DualStage, AxisName.Stage2_Z, StagePosName.DownSecondPos);
                            OnShowMsg?.Invoke("AutoRun", "DualStage->平台Z轴快速下降", Brushes.Green);
                            StageZDownStep = 20;
                        }
                        else if (Motion.IsMoveTimeOut(StationEnum.DualStage, AxisName.Stage1_Z)|| Motion.IsMoveTimeOut(StationEnum.DualStage, AxisName.Stage2_Z))
                        {
                            OnShowMsg?.Invoke("AutoRun", "DualStage->平台Z轴移动到变速位置超时", Brushes.Red);
                            AlarmManager.CreateAlarm((int)MotionAlarmCode.Stage1_ZMoveTimeout);
                            StageZDownStep = 1;
                        }     
                        break;
                    }
                case 20:
                    {
                        if (Motion.IsArrive(StationEnum.DualStage, AxisName.Stage1_Z, StagePosName.DownSecondPos) && Motion.IsArrive(StationEnum.DualStage, AxisName.Stage2_Z, StagePosName.DownSecondPos))
                        {
                            if (Motion.IsCylinderArrive(CylinderName.ShutterClyL, true))
                            {
                                Motion.Vacuum(VacuumName.Stage1Vacuum,true);
                                Motion.Vacuum(VacuumName.Stage2Vacuum, true);
                                Motion.SetDigitalOutput(OutputName.Stage1Ionizer, false);
                                Motion.SetDigitalOutput(OutputName.Stage2Ionizer, false);
                                Thread.Sleep(300);
                                OnShowMsg?.Invoke("AutoRun", "DualStage->平台Z轴到达安全位置，平台吸真空", Brushes.Green);
                                StageZDownStep = 0;
                                SequenceStepManager.StopWatch(SequenceName.PinDown);
                            }
                            else if (Motion.IsCylinderTimeOut(CylinderName.ShutterClyL))
                            {
                                OnPause?.Invoke(-1);
                                OnShowMsg?.Invoke("AutoRun", "DualStage->门禁气缸关闭超时", Brushes.Red);
                                StageZDownStep = 10;
                            }
                        }
                        else if (Motion.IsMoveTimeOut(StationEnum.DualStage, AxisName.Stage1_Z) || Motion.IsMoveTimeOut(StationEnum.DualStage, AxisName.Stage2_Z))
                        {
                            OnShowMsg?.Invoke("AutoRun", "DualStage->平台Z轴移动到初始位置超时", Brushes.Red);
                            AlarmManager.CreateAlarm((int)MotionAlarmCode.Stage1_ZMoveTimeout);
                            StageZDownStep = 10;
                        }
                        break;
                    }
            }
        }
        public void StageZUp()
        {
            switch (StageZUpStep)
            {
                case 1:
                    {
                        SequenceStepManager.StartWatch(SequenceName.PinUp);
                        SequenceStepManager.StartWatch(SequenceName.ShutterOpen);
                        Motion.MoveAbs(StationEnum.DualStage, AxisName.Stage1_Z, StagePosName.UpFirstPos);
                        Motion.MoveAbs(StationEnum.DualStage, AxisName.Stage2_Z, StagePosName.UpFirstPos);
                        Motion.Cylinder(CylinderName.ShutterClyL,false);
                        Motion.SetDigitalOutput(OutputName.Stage1Ionizer, true);
                        Motion.SetDigitalOutput(OutputName.Stage2Ionizer, true);
                        OnShowMsg?.Invoke("AutoRun", "DualStage->平台Z轴慢速上升，Shutter打开", Brushes.Green);
                        StageZUpStep = 10;
                        break;
                    }
                case 10:
                    {
                        if (Motion.IsArrive(StationEnum.DualStage, AxisName.Stage1_Z, StagePosName.UpFirstPos) && Motion.IsArrive(StationEnum.DualStage, AxisName.Stage2_Z, StagePosName.UpFirstPos))
                        {
                            Motion.MoveAbs(StationEnum.DualStage, AxisName.Stage1_Z, StagePosName.UpSecondPos);
                            Motion.MoveAbs(StationEnum.DualStage, AxisName.Stage2_Z, StagePosName.UpSecondPos);
                            OnShowMsg?.Invoke("AutoRun", "DualStage->平台Z轴快速上升", Brushes.Green);
                            StageZUpStep = 20;
                        }
                        else if (Motion.IsMoveTimeOut(StationEnum.DualStage, AxisName.Stage1_Z) || Motion.IsMoveTimeOut(StationEnum.DualStage, AxisName.Stage2_Z))
                        {
                            OnShowMsg?.Invoke("AutoRun", "DualStage->平台Z轴移动到变速位置超时", Brushes.Red);
                            AlarmManager.CreateAlarm((int)MotionAlarmCode.Stage1_ZMoveTimeout);
                            StageZUpStep = 1;
                        }
                        break;
                    }
                case 20:
                    {
                        if (Motion.IsArrive(StationEnum.DualStage, AxisName.Stage1_Z, StagePosName.UpSecondPos) && Motion.IsArrive(StationEnum.DualStage, AxisName.Stage2_Z, StagePosName.UpSecondPos))
                        {
                            OnShowMsg?.Invoke("AutoRun", "DualStage->平台Z轴到达接料位置", Brushes.Green);
                            Motion.SetDigitalOutput(OutputName.Stage1Ionizer, false);
                            Motion.SetDigitalOutput(OutputName.Stage2Ionizer, false);
                            StageZUpStep = 30;
                            SequenceStepManager.StopWatch(SequenceName.PinUp);
                        }
                        else if (Motion.IsMoveTimeOut(StationEnum.DualStage, AxisName.Stage1_Z) || Motion.IsMoveTimeOut(StationEnum.DualStage, AxisName.Stage2_Z))
                        {
                            OnShowMsg?.Invoke("AutoRun", "DualStage->平台Z轴移动到顶升位置超时", Brushes.Red);
                            AlarmManager.CreateAlarm((int)MotionAlarmCode.Stage1_ZMoveTimeout);
                            StageZUpStep = 10;
                        }
                        break;
                    }
                case 30:
                    {
                        if (Motion.GetDigitalInput(InputName.ShutterOpenL) && Motion.GetDigitalInput(InputName.ShutterOpenR))
                        {
                            OnShowMsg?.Invoke("AutoRun", "DualStage->Shutter打开完成", Brushes.Green);
                            StageZUpStep = 0;
                            SequenceStepManager.StopWatch(SequenceName.ShutterOpen);
                        }
                        else if (Motion.IsCylinderTimeOut(CylinderName.ShutterClyL))
                        {
                            OnPause?.Invoke(-1);
                            OnShowMsg?.Invoke("AutoRun", "DualStage->Shutter打开超时", Brushes.Green);
                            StageZUpStep = 20;
                        }
                        break;
                    }
                default:
                    break;
            }
        }
        /// <summary>
        /// 平台Y轴移动到检测位置
        /// </summary>
        public void StageMoveToDetectPos()
        {
            if (Probe.IsContactZAtSafePos())
            {
                switch (MoveToDetectPosStep)
                {
                    case 1:
                        {
                            if (Motion.IsArrive(StationEnum.DualStage, AxisName.Stage1_T, StagePosName.Neg180DegPos) &&
                                Motion.IsArrive(StationEnum.DualStage, AxisName.Stage2_T, StagePosName.Neg180DegPos))
                            {
                                Motion.MoveAbs(StationEnum.DualStage, AxisName.Stage2_Y, StagePosName.DetectingPos);
                                Motion.MoveAbs(StationEnum.DualStage, AxisName.Stage1_Y, StagePosName.DetectingPos);
                                MoveToDetectPosStep = 10;
                                OnShowMsg?.Invoke("AutoRun", "DualStage->平台T轴都在检测位置，平台Y轴移动到检测位置", Brushes.Green);
                            }
                            else
                            {
                                Motion.MoveAbs(StationEnum.DualStage, AxisName.Stage1_Y, StagePosName.RotatePos);
                                Motion.MoveAbs(StationEnum.DualStage, AxisName.Stage2_Y, StagePosName.RotatePos);
                                MoveToDetectPosStep = 20;
                                OnShowMsg?.Invoke("AutoRun", "DualStage->平台T轴不在检测位置，平台Y轴移动到平台旋转位置", Brushes.Green);
                            }
                            break;
                        }
                    case 10:
                        {
                            if (Motion.IsArrive(StationEnum.DualStage, AxisName.Stage2_Y, StagePosName.DetectingPos) &&
                                Motion.IsArrive(StationEnum.DualStage, AxisName.Stage1_Y, StagePosName.DetectingPos))
                            {
                                OnShowMsg?.Invoke("AutoRun", "DualStage->平台T轴到达检测位置", Brushes.Green);
                                MoveToDetectPosStep = 0;
                            }
                            break;
                        }
                    case 20:
                        {
                            if (Motion.IsArrive(StationEnum.DualStage, AxisName.Stage1_Y, StagePosName.RotatePos))
                            {
                                if (Motion.IsArrive(StationEnum.DualStage, AxisName.Stage2_Y, StagePosName.RotatePos))
                                {
                                    Motion.MoveAbs(StationEnum.DualStage, AxisName.Stage1_T, StagePosName.Neg180DegPos);
                                    Motion.MoveAbs(StationEnum.DualStage, AxisName.Stage2_T, StagePosName.Neg180DegPos);
                                    MoveToDetectPosStep = 30;
                                    OnShowMsg?.Invoke("AutoRun", "DualStage->平台T轴旋转180度", Brushes.Green);
                                    SequenceStepManager.StopWatch(SequenceName.StageToRotatePos);
                                    SequenceStepManager.StartWatch(SequenceName.StageRotate_Load);
                                }
                                else if (Motion.IsMoveTimeOut(StationEnum.DualStage, AxisName.Stage2_Y))
                                {
                                    OnShowMsg?.Invoke("AutoRun", "DualStage->平台2Y轴移动到旋转位置超时", Brushes.Red);
                                    AlarmManager.CreateAlarm((int)MotionAlarmCode.Stage2_YMoveTimeout);
                                    MoveToDetectPosStep = 1;
                                }
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.DualStage, AxisName.Stage1_Y))
                            {
                                OnShowMsg?.Invoke("AutoRun", "DualStage->平台1Y轴移动到旋转位置超时", Brushes.Red);
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.Stage1_YMoveTimeout);
                                MoveToDetectPosStep = 1;
                            }
                            break;
                        }

                    case 30:
                        {
                            if (Motion.IsArrive(StationEnum.DualStage, AxisName.Stage1_T, StagePosName.Neg180DegPos))
                            {
                                if (Motion.IsArrive(StationEnum.DualStage, AxisName.Stage2_T, StagePosName.Neg180DegPos))
                                {
                                    SequenceStepManager.StopWatch(SequenceName.StageRotate_Load);
                                    Motion.MoveAbs(StationEnum.DualStage, AxisName.Stage1_Y, StagePosName.DetectingPos);
                                    Motion.MoveAbs(StationEnum.DualStage, AxisName.Stage2_Y, StagePosName.DetectingPos);
                                    MoveToDetectPosStep = 40;
                                    OnShowMsg?.Invoke("AutoRun", "DualStage->平台T轴旋转到位，平台Y轴移动到检测位置", Brushes.Green);
                                }
                                else if (Motion.IsMoveTimeOut(StationEnum.DualStage, AxisName.Stage2_T))
                                {
                                    OnShowMsg?.Invoke("AutoRun", "DualStage->平台2T轴移动到180度位置超时", Brushes.Red);
                                    AlarmManager.CreateAlarm((int)MotionAlarmCode.Stage2_TMoveTimeout);
                                    MoveToDetectPosStep = 20;
                                }
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.DualStage, AxisName.Stage1_T))
                            {
                                OnShowMsg?.Invoke("AutoRun", "DualStage->平台1T轴移动到180度位置超时", Brushes.Red);
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.Stage1_TMoveTimeout);
                                MoveToDetectPosStep = 20;
                            }
                            break;
                        }
                    case 40:
                        {
                            if (Motion.IsArrive(StationEnum.DualStage, AxisName.Stage1_Y, StagePosName.DetectingPos))
                            {
                                if (Motion.IsArrive(StationEnum.DualStage, AxisName.Stage2_Y, StagePosName.DetectingPos))
                                {
                                    MoveToDetectPosStep = 0;
                                    SequenceStepManager.StopWatch(SequenceName.LoadTotal);
                                }
                                else if (Motion.IsMoveTimeOut(StationEnum.DualStage, AxisName.Stage2_Y))
                                {
                                    OnShowMsg?.Invoke("AutoRun", "DualStage->平台2Y轴移动到检测位置超时", Brushes.Red);
                                    AlarmManager.CreateAlarm((int)MotionAlarmCode.Stage2_YMoveTimeout);
                                    MoveToDetectPosStep = 30;
                                }
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.DualStage, AxisName.Stage1_Y))
                            {
                                OnShowMsg?.Invoke("AutoRun", "DualStage->平台1Y轴移动到检测位置超时", Brushes.Red);
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.Stage1_YMoveTimeout);
                                MoveToDetectPosStep = 30;
                            }
                            break;
                        }
                }
            }
            else
            {
                OnShowMsg?.Invoke("AutoRun", "DualStage->压头不在安全位置，禁止移动平台，请检查程序逻辑", Brushes.Red);
                AlarmManager.CreateAlarm((int)MotionAlarmCode.StageMoveFail);
                OnStop?.Invoke();
            }
        }
        /// <summary>
        /// 平台Y轴移动到检测位置
        /// </summary>
        public void StageMoveToAutoChangePos()
        {
            if (Probe.IsContactZAtSafePos())
            {
                switch (MoveToAutoChangePosStep)
                {
                    case 1:
                        {
                            if (Motion.IsArrive(StationEnum.DualStage, AxisName.Stage1_T, StagePosName.Neg180DegPos) &&
                                Motion.IsArrive(StationEnum.DualStage, AxisName.Stage2_T, StagePosName.Neg180DegPos))
                            {
                                Motion.MoveAbs(StationEnum.DualStage, AxisName.Stage2_Y, StagePosName.LoadingPos);
                                Motion.MoveAbs(StationEnum.DualStage, AxisName.Stage1_Y, StagePosName.AlignPos);
                                MoveToAutoChangePosStep = 10;
                                OnShowMsg?.Invoke("AutoRun", "DualStage->平台Y轴移动到平台旋转位置", Brushes.Green);
                            }
                            else
                            {
                                if (Motion.IsArrive(StationEnum.PrbAutoChg,AxisName.PrbAutoChg_Y,PrbAutoChgPosName.SafePos)&& PrbAutoChgYArriveSafePos)
                                {
                                    PrbAutoChgYArriveSafePos = false;
                                    Motion.MoveAbs(StationEnum.DualStage, AxisName.Stage1_Y, StagePosName.RotatePos);
                                    Motion.MoveAbs(StationEnum.DualStage, AxisName.Stage2_Y, StagePosName.RotatePos);
                                    MoveToAutoChangePosStep = 20;
                                    OnShowMsg?.Invoke("AutoRun", "DualStage->自动换型Y轴在安全位置，平台Y轴移动到平台旋转位置", Brushes.Green);
                                }
                            }

                            break;
                        }
                    case 10:
                        {
                            if (Motion.IsArrive(StationEnum.DualStage, AxisName.Stage2_Y, StagePosName.LoadingPos) &&
                                Motion.IsArrive(StationEnum.DualStage, AxisName.Stage1_Y, StagePosName.AlignPos))
                            {
                                MoveToAutoChangePosStep = 0;
                            }
                            break;
                        }
                    case 20:
                        {
                            if (Motion.IsArrive(StationEnum.DualStage, AxisName.Stage1_Y, StagePosName.RotatePos))
                            {
                                if (Motion.IsArrive(StationEnum.DualStage, AxisName.Stage2_Y, StagePosName.RotatePos))
                                {
                                    Motion.MoveAbs(StationEnum.DualStage, AxisName.Stage1_T, StagePosName.Neg180DegPos);
                                    Motion.MoveAbs(StationEnum.DualStage, AxisName.Stage2_T, StagePosName.Neg180DegPos);
                                    MoveToAutoChangePosStep = 30;
                                    OnShowMsg?.Invoke("AutoRun", "DualStage->平台T轴旋转180度", Brushes.Green);
                                    SequenceStepManager.StopWatch(SequenceName.StageToRotatePos);
                                    SequenceStepManager.StartWatch(SequenceName.StageRotate_Load);
                                }
                                else if (Motion.IsMoveTimeOut(StationEnum.DualStage, AxisName.Stage2_Y))
                                {
                                    OnShowMsg?.Invoke("AutoRun", "DualStage->平台2Y轴移动到旋转位置超时", Brushes.Red);
                                    AlarmManager.CreateAlarm((int)MotionAlarmCode.Stage2_YMoveTimeout);
                                    MoveToAutoChangePosStep = 1;
                                }
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.DualStage, AxisName.Stage1_Y))
                            {
                                OnShowMsg?.Invoke("AutoRun", "DualStage->平台1Y轴移动到旋转位置超时", Brushes.Red);
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.Stage1_YMoveTimeout);
                                MoveToAutoChangePosStep = 1;
                            }
                            break;
                        }

                    case 30:
                        {
                            if (Motion.IsArrive(StationEnum.DualStage, AxisName.Stage1_T, StagePosName.Neg180DegPos))
                            {
                                if (Motion.IsArrive(StationEnum.DualStage, AxisName.Stage2_T, StagePosName.Neg180DegPos))
                                {
                                    SequenceStepManager.StopWatch(SequenceName.StageRotate_Load);
                                    Motion.MoveAbs(StationEnum.DualStage, AxisName.Stage1_Y, StagePosName.AlignPos);
                                    Motion.MoveAbs(StationEnum.DualStage, AxisName.Stage2_Y, StagePosName.LoadingPos);
                                    MoveToAutoChangePosStep = 40;
                                    OnShowMsg?.Invoke("AutoRun", "DualStage->平台T轴旋转到位，平台Y轴移动到检测位置", Brushes.Green);
                                }
                                else if (Motion.IsMoveTimeOut(StationEnum.DualStage, AxisName.Stage2_T))
                                {
                                    OnShowMsg?.Invoke("AutoRun", "DualStage->平台2T轴移动到180度位置超时", Brushes.Red);
                                    AlarmManager.CreateAlarm((int)MotionAlarmCode.Stage2_TMoveTimeout);
                                    MoveToAutoChangePosStep = 20;
                                }
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.DualStage, AxisName.Stage1_T))
                            {
                                OnShowMsg?.Invoke("AutoRun", "DualStage->平台1T轴移动到180度位置超时", Brushes.Red);
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.Stage1_TMoveTimeout);
                                MoveToAutoChangePosStep = 20;
                            }
                            break;
                        }
                    case 40:
                        {
                            if (Motion.IsArrive(StationEnum.DualStage, AxisName.Stage1_Y, StagePosName.AlignPos))
                            {
                                if (Motion.IsArrive(StationEnum.DualStage, AxisName.Stage2_Y, StagePosName.LoadingPos))
                                {
                                    MoveToAutoChangePosStep = 0;
                                    SequenceStepManager.StopWatch(SequenceName.LoadTotal);
                                }
                                else if (Motion.IsMoveTimeOut(StationEnum.DualStage, AxisName.Stage2_Y))
                                {
                                    OnShowMsg?.Invoke("AutoRun", "DualStage->平台2Y轴移动到上料位置超时", Brushes.Red);
                                    AlarmManager.CreateAlarm((int)MotionAlarmCode.Stage2_YMoveTimeout);
                                    MoveToAutoChangePosStep = 30;
                                }
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.DualStage, AxisName.Stage1_Y))
                            {
                                OnShowMsg?.Invoke("AutoRun", "DualStage->平台1Y轴移动到规整Glass位置超时", Brushes.Red);
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.Stage1_YMoveTimeout);
                                MoveToAutoChangePosStep = 30;
                            }
                            break;
                        }
                }
            }
            else
            {
                OnShowMsg?.Invoke("AutoRun", "DualStage->压头不在安全位置，禁止移动平台，请检查程序逻辑", Brushes.Red);
                AlarmManager.CreateAlarm((int)MotionAlarmCode.StageMoveFail);
                OnStop?.Invoke();
            }
        }
        /// <summary>
        /// 规整气缸上升
        /// </summary>
        public void StageAlignUp()
        {
            switch (StageAlignUpStep)
            {
                case 1:
                    {
                        Delay.Start(10);//气缸超时时间
                        Delay2.Start(10);
                        string temp = string.Empty;
                        for (int i = 0; i < 8; i++)
                        {
                            temp = $"Stage1VerticalCyl{i + 1}";
                            Motion.Cylinder(temp, true);
                            temp = $"Stage2VerticalCyl{i + 1}";
                            Motion.Cylinder(temp, true);
                        }
                        StageAlignUpStep = 5;
                        break;
                    }
                case 5:
                    {
                        if (IsClyUp(0))
                        {
                            if (IsClyUp(1))
                            {
                                StageAlignUpStep = 0;
                            }
                            else if (Delay2.TimeUp())
                            {
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.Stg2AlignCylUpTimeout);
                                StageAlignUpStep = 1;
                            }
                        }
                        else if (Delay.TimeUp())
                        {
                            AlarmManager.CreateAlarm((int)MotionAlarmCode.Stg1AlignCylUpTimeout);
                            StageAlignUpStep = 1;
                        }
                        break;
                    }
            }
        }
        /// <summary>
        /// 规整气缸下降
        /// </summary>
        public void StageAlignDown()
        {
            switch (StageAlignDownStep)
            {
                case 1:
                    {
                        Delay.Start(10);//气缸超时时间
                        Delay2.Start(10);
                        string temp = string.Empty;
                        for (int i = 0; i < 8; i++)
                        {
                            temp = $"Stage1VerticalCyl{i + 1}";
                            Motion.Cylinder(temp, false);
                            temp = $"Stage2VerticalCyl{i + 1}";
                            Motion.Cylinder(temp, false);
                        }
                        StageAlignDownStep = 5;
                        break;
                    }
                case 5:
                    {
                        if (IsClyDown(0))
                        {
                            if (IsClyDown(1))
                            {
                                StageAlignDownStep = 0;
                            }
                            else if (Delay2.TimeUp())
                            {
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.Stg2AlignCylDownTimeout);
                                StageAlignDownStep = 1;
                            }
                        }
                        else if (Delay2.TimeUp())
                        {
                            AlarmManager.CreateAlarm((int)MotionAlarmCode.Stg1AlignCylDownTimeout);
                            StageAlignDownStep = 1;
                        }
                        break;
                    }
            }

        }
        /// <summary>
        /// 规整气缸松开
        /// </summary>
        public void StageAlignUnClamp()
        {
            switch (StageAlignUnClampStep)
            {
                case 1:
                    {
                        AxisInfo info1 = AxisInfoHelper.GetAxisInfoByName(AxisName.Stage1_Y.ToString());
                        AxisInfo info2 = AxisInfoHelper.GetAxisInfoByName(AxisName.Stage2_Y.ToString());
                        if (info1.ActPos + info2.ActPos > Param.Motion.AlignCylinderAlignPos) //  有碰撞风险
                        {
                            if (Motion.IsServoOn(StationEnum.DualStage, AxisName.Stage1_Y)==false)
                            {
                                Motion.ServoOn(StationEnum.DualStage, AxisName.Stage1_Y);
                            }
                            if (IsDetectGlass(0)) //  如果有产品，打开真空
                            {
                                Motion.Vacuum(VacuumName.Stage1Vacuum, true);
                            }
                            Thread.Sleep(300);
                            Motion.MoveRel(StationEnum.DualStage, AxisName.Stage1_Y, -90);  ///  移开 Stage1
                            StageAlignUnClampStep = 5;  /// 判断到位
                        }
                        else
                        {
                            Delay.Start(10);//气缸超时时间
                            Delay2.Start(10);
                            string temp = string.Empty;
                            for (int i = 0; i < 8; i++)  ///  气缸全部伸出
                            {
                                temp = $"Stage1HorizontalCyl{i + 1}";
                                Motion.Cylinder(temp, true);
                                temp = $"Stage2HorizontalCyl{i + 1}";
                                Motion.Cylinder(temp, true);
                            }
                            StageAlignUnClampStep = 10;
                        }
                        break;
                    }
                case 5:
                    {
                        if (Motion.IsArrive(StationEnum.DualStage, AxisName.Stage1_Y))
                        {
                            Delay.Start(10);//气缸超时时间
                            Delay2.Start(10);
                            string temp = string.Empty;
                            for (int i = 0; i < 8; i++)  ///  气缸全部伸出
                            {
                                temp = $"Stage1HorizontalCyl{i + 1}";
                                Motion.Cylinder(temp, true);
                                temp = $"Stage2HorizontalCyl{i + 1}";
                                Motion.Cylinder(temp, true);
                            }
                            StageAlignUnClampStep = 10;
                        }
                        break;
                    }
                case 10:
                    {
                        if (IsClyUnClamp(0)) //   两个Stage 气缸全部伸出
                        {
                            if (IsClyUnClamp(1))
                            {
                                StageAlignUnClampStep = 0;
                            }
                            else if (Delay2.TimeUp())
                            {
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.Stg2AlignCylUnClampTimeout);
                                StageAlignUnClampStep = 1;
                            }
                        }
                        else if (Delay.TimeUp())
                        {
                            AlarmManager.CreateAlarm((int)MotionAlarmCode.Stg1AlignCylUnClampTimeout);
                            StageAlignUnClampStep = 1;
                        }
                        break;
                    }
            }
        }
        /// <summary>
        /// 规整气缸加紧
        /// </summary>
        public void StageAlignClamp()
        {
            switch (StageAlignClampStep)
            {
                case 1:
                    {
                        Delay.Start(10);//气缸超时时间
                        Delay2.Start(10);
                        string temp = string.Empty;
                        for (int i = 0; i < 8; i++)
                        {
                            temp = $"Stage1HorizontalCyl{i + 1}";
                            Motion.Cylinder(temp, false);
                            temp = $"Stage2HorizontalCyl{i + 1}";
                            Motion.Cylinder(temp, false);
                        }
                        StageAlignClampStep = 5;
                        break;
                    }

                case 5:
                    {
                        if (IsClyClamp(0))
                        {
                            if (IsClyClamp(1))
                            {
                                StageAlignClampStep = 0;
                            }
                            else if (Delay2.TimeUp())
                            {
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.Stg2AlignCylClampTimeout);
                                StageAlignClampStep = 1;
                            }
                        }
                        else if (Delay.TimeUp())
                        {
                            AlarmManager.CreateAlarm((int)MotionAlarmCode.Stg1AlignCylClampTimeout);
                            StageAlignClampStep = 1;
                        }
                        break;
                    }
            }
        }
        /// <summary>
        /// 平台吸真空
        /// </summary>
        public void StageVacuumOn()
        {
            switch (StageVacuumOnStep)
            {
                case 1:
                    {
                        Motion.Vacuum(VacuumName.Stage1Vacuum, true);
                        Motion.Vacuum(VacuumName.Stage2Vacuum, true);
                        StageVacuumOnStep = 5;
                        break;
                    }

                case 5:
                    {
                        if (Motion.IsVacuumArrive(VacuumName.Stage1Vacuum, true))
                        {
                            if (Motion.IsVacuumArrive(VacuumName.Stage2Vacuum, true))
                            {
                                OnShowMsg?.Invoke("AutoRun", "DualStage->平台吸真空完毕", Brushes.Green);
                                StageVacuumOnStep = 0;
                            }
                            else if (Motion.IsVacuumTimeOut(VacuumName.Stage2Vacuum))
                            {
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.Stg2VacOnTimeout);
                                StageVacuumOnStep = 1;
                            }
                        }
                        else if (Motion.IsVacuumTimeOut(VacuumName.Stage1Vacuum))
                        {
                            AlarmManager.CreateAlarm((int)MotionAlarmCode.Stg1VacOnTimeout);
                            StageVacuumOnStep = 1;
                        }
                        break;
                    }
            }
        }
        /// <summary>
        /// 平台破真空
        /// </summary>
        public void StageVacuumOff()
        {
            switch (StageVacuumOffStep)
            {
                case 1:
                    {
                        Motion.Vacuum(VacuumName.Stage1Vacuum, false);
                        Motion.Vacuum(VacuumName.Stage2Vacuum, false);
                        OnShowMsg?.Invoke("AutoRun", "DualStage->平台开始破真空", Brushes.Green);
                        StageVacuumOffStep = 5;
                        break;
                    }
                case 5:
                    {
                        if (Motion.IsVacuumArrive(VacuumName.Stage1Vacuum, false))
                        {
                            if (Motion.IsVacuumArrive(VacuumName.Stage2Vacuum, false))
                            {
                                OnShowMsg?.Invoke("AutoRun", "DualStage->平台破真空完毕", Brushes.Green);
                                StageVacuumOffStep = 0;
                            }
                            else if (Motion.IsVacuumTimeOut(VacuumName.Stage2Vacuum))
                            {
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.Stg2VacOffTimeout);
                                StageVacuumOffStep = 1;
                            }
                        }
                        else if (Motion.IsVacuumTimeOut(VacuumName.Stage1Vacuum))
                        {
                            AlarmManager.CreateAlarm((int)MotionAlarmCode.Stg1VacOffTimeout);
                            StageVacuumOffStep = 1;
                        }
                        break;
                    }
            }
        }
        /// <summary>
        /// 离子风扇控制
        /// </summary>
        /// <param name="onOff"></param>
        public void IonizerValveControl(bool onOff)
        {
            Motion.SetDigitalOutput(OutputName.Stage1Ionizer, onOff);
            Motion.SetDigitalOutput(OutputName.Stage2Ionizer, onOff);
        }
    
        /// <summary>
        /// 检测结束流程，各轴去规整气缸规整位置
        /// </summary>
        public void DetectEnd()
        {
            if (Probe.IsContactZAtSafePos())
            {
                switch (DetectEndStep)
                {
                    case 1:
                        {
                            if (Motion.IsArrive(StationEnum.DualStage, AxisName.Stage1_T, StagePosName.ZeroDegPos) && Motion.IsArrive(StationEnum.DualStage, AxisName.Stage2_T, StagePosName.ZeroDegPos))
                            {
                                DetectEndStep = 0;
                            }
                            else
                            {
                                Motion.MoveAbs(StationEnum.DualStage, AxisName.Stage2_Y, StagePosName.RotatePos);
                                Motion.MoveAbs(StationEnum.DualStage, AxisName.Stage1_Y, StagePosName.RotatePos);
                                DetectEndStep = 5;
                                SequenceStepManager.StartWatch(SequenceName.StageToReadyPos);
                            }
                            break;
                        }
                    case 5:
                        {
                            if (Motion.IsArrive(StationEnum.DualStage, AxisName.Stage1_Y, StagePosName.RotatePos))
                            {
                                if (Motion.IsArrive(StationEnum.DualStage, AxisName.Stage2_Y, StagePosName.RotatePos))
                                {
                                    Motion.MoveAbs(StationEnum.DualStage, AxisName.Stage1_T, StagePosName.ZeroDegPos);
                                    Motion.MoveAbs(StationEnum.DualStage, AxisName.Stage2_T, StagePosName.ZeroDegPos);
                                    DetectEndStep = 10;
                                    SequenceStepManager.StopWatch(SequenceName.StageToReadyPos);
                                    SequenceStepManager.StartWatch(SequenceName.StageRotate_Unload);
                                }
                                else if (Motion.IsMoveTimeOut(StationEnum.DualStage, AxisName.Stage2_Y))
                                {
                                    AlarmManager.CreateAlarm((int)MotionAlarmCode.Stage2_YMoveTimeout);
                                    DetectEndStep = 1;
                                }
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.DualStage, AxisName.Stage1_Y))
                            {
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.Stage1_YMoveTimeout);
                                DetectEndStep = 1;
                            }
                            break;
                        }
                    case 10:
                        {
                            if (Motion.IsArrive(StationEnum.DualStage, AxisName.Stage1_T, StagePosName.ZeroDegPos))
                            {
                                if (Motion.IsArrive(StationEnum.DualStage, AxisName.Stage2_T, StagePosName.ZeroDegPos))
                                {
                                    DetectEndStep = 0;
                                    SequenceStepManager.StopWatch(SequenceName.StageRotate_Unload);
                                }
                                else if (Motion.IsMoveTimeOut(StationEnum.DualStage, AxisName.Stage2_T))
                                {
                                    AlarmManager.CreateAlarm((int)MotionAlarmCode.Stage2_TMoveTimeout);
                                    DetectEndStep = 5;
                                }
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.DualStage, AxisName.Stage1_T))
                            {
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.Stage1_TMoveTimeout);
                                DetectEndStep = 5;
                            }
                            break;
                        }
                }
            }
            else
            {
                OnShowMsg?.Invoke("AutoRun", "DualStage->压头不在安全位置，禁止移动平台，请检查程序逻辑", Brushes.Red);
                AlarmManager.CreateAlarm((int)MotionAlarmCode.StageMoveFail);
                OnStop?.Invoke();
            }
          
        }
        /// <summary>
        /// 移动到规整气缸动作位置流程
        /// </summary>
        public void MoveToAlignPos()
        {
            if (Probe.IsContactZAtSafePos())
            {
                switch (MoveToAlignStep)
                {
                    case 1:
                        {
                            StageVacuumOnStep = 1;  /// 平台吸真空
                            MoveToAlignStep = 5;
                            break;
                        }
                    case 5:
                        {
                            if (StageVacuumOnStep == 0)  /// 真空吸附好了再移动
                            {
                                Motion.MoveAbs(StationEnum.DualStage, AxisName.Stage1_Y, StagePosName.AlignPos);
                                Motion.MoveAbs(StationEnum.DualStage, AxisName.Stage2_Y, StagePosName.AlignPos);
                                MoveToAlignStep = 10;
                            }
                            else
                            {
                                StageVacuumOn();
                            }
                            break;
                        }
                    case 10:
                        {
                            if (Motion.IsArrive(StationEnum.DualStage, AxisName.Stage1_Y, StagePosName.AlignPos))
                            {
                                if (Motion.IsArrive(StationEnum.DualStage, AxisName.Stage2_Y, StagePosName.AlignPos))
                                {
                                    MoveToAlignStep = 0;
                                }
                                else if (Motion.IsMoveTimeOut(StationEnum.DualStage, AxisName.Stage2_Y))
                                {
                                    OnShowMsg?.Invoke("AutoRun", "DualStage->平台2Y轴移动到规整位置超时", Brushes.Red);
                                    AlarmManager.CreateAlarm((int)MotionAlarmCode.Stage2_YMoveTimeout);
                                    MoveToAlignStep = 5;
                                }
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.DualStage, AxisName.Stage1_Y))
                            {
                                OnShowMsg?.Invoke("AutoRun", "DualStage->平台1Y轴移动到规整位置超时", Brushes.Red);
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.Stage1_YMoveTimeout);
                                MoveToAlignStep = 5;
                            }
                            break;
                        }
                }
            }
            else
            {
                OnShowMsg?.Invoke("AutoRun", "DualStage->压头不在安全位置，禁止移动平台，请检查程序逻辑", Brushes.Red);
                AlarmManager.CreateAlarm((int)MotionAlarmCode.StageMoveFail);
                OnStop?.Invoke();
            }
        }
        /// <summary>
        /// 移动到上下料位置流程
        /// </summary>
        public void MoveToLoadingPos()
        {
            if (Probe.IsContactZAtSafePos())
            {
                switch (MoveToLoadingStep)
                {
                    case 1:
                        {
                            if (Motion.IsArrive(StationEnum.DualStage, AxisName.Stage1_T, StagePosName.ZeroDegPos) && Motion.IsArrive(StationEnum.DualStage, AxisName.Stage2_T, StagePosName.ZeroDegPos))
                            {
                                SequenceStepManager.StartWatch(DetectEndBit ? SequenceName.StageToUnloadPos : SequenceName.StageToDetectPos);
                                Motion.MoveAbs(StationEnum.DualStage, AxisName.Stage2_Y, StagePosName.LoadingPos);
                                Motion.MoveAbs(StationEnum.DualStage, AxisName.Stage1_Y, StagePosName.AlignPos);
                                MoveToLoadingStep = 10;
                            }
                            else
                            {
                                Motion.MoveAbs(StationEnum.DualStage, AxisName.Stage1_Y, StagePosName.RotatePos);
                                Motion.MoveAbs(StationEnum.DualStage, AxisName.Stage2_Y, StagePosName.RotatePos);
                                MoveToLoadingStep = 30;
                            }
                            break;
                        }
                    case 10:
                        {
                            if (Motion.IsArrive(StationEnum.DualStage, AxisName.Stage1_Y, StagePosName.AlignPos))
                            {
                                if (Motion.IsArrive(StationEnum.DualStage, AxisName.Stage2_Y, StagePosName.LoadingPos))
                                {
                                    Motion.MoveAbs(StationEnum.DualStage, AxisName.Stage1_Y, StagePosName.LoadingPos);
                                    MoveToLoadingStep = 20;
                                }
                                else if (Motion.IsMoveTimeOut(StationEnum.DualStage, AxisName.Stage2_Y))
                                {
                                    OnShowMsg?.Invoke("AutoRun", "DualStage->平台2Y轴移动到上料位置超时", Brushes.Red);
                                    AlarmManager.CreateAlarm((int)MotionAlarmCode.Stage2_YMoveTimeout);
                                    MoveToLoadingStep = 1;
                                }
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.DualStage, AxisName.Stage1_Y))
                            {
                                OnShowMsg?.Invoke("AutoRun", "DualStage->平台1Y轴移动到规整位置超时", Brushes.Red);
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.Stage1_YMoveTimeout);
                                MoveToLoadingStep = 1;
                            }
                            break;
                        }
                    case 20:
                        {
                            if (Motion.IsArrive(StationEnum.DualStage, AxisName.Stage1_Y, StagePosName.LoadingPos))
                            {
                                MoveToLoadingStep = 0;
                                SequenceStepManager.StopWatch(DetectEndBit ? SequenceName.StageToUnloadPos : SequenceName.StageToDetectPos);
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.DualStage, AxisName.Stage1_Y))
                            {
                                OnShowMsg?.Invoke("AutoRun", "DualStage->平台1Y轴移动到上料位置超时", Brushes.Red);
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.Stage1_YMoveTimeout);
                                MoveToLoadingStep = 5;
                            }
                            break;
                        }
                    case 30:
                        {
                            if (Motion.IsArrive(StationEnum.DualStage, AxisName.Stage1_Y, StagePosName.RotatePos))
                            {
                                if (Motion.IsArrive(StationEnum.DualStage, AxisName.Stage2_Y, StagePosName.RotatePos))
                                {
                                    Motion.MoveAbs(StationEnum.DualStage, AxisName.Stage1_T, StagePosName.ZeroDegPos);
                                    Motion.MoveAbs(StationEnum.DualStage, AxisName.Stage2_T, StagePosName.ZeroDegPos);
                                    MoveToLoadingStep = 40;
                                }
                                else if (Motion.IsMoveTimeOut(StationEnum.DualStage, AxisName.Stage2_Y))
                                {
                                    OnShowMsg?.Invoke("AutoRun", "DualStage->平台2Y轴移动到旋转位置超时", Brushes.Red);
                                    AlarmManager.CreateAlarm((int)MotionAlarmCode.Stage2_YMoveTimeout);
                                    MoveToLoadingStep = 1;
                                }
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.DualStage, AxisName.Stage1_Y))
                            {
                                OnShowMsg?.Invoke("AutoRun", "DualStage->平台1Y轴移动到旋转位置超时", Brushes.Red);
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.Stage1_YMoveTimeout);
                                MoveToLoadingStep = 1;
                            }
                            break;
                        }

                    case 40:
                        {
                            if (Motion.IsArrive(StationEnum.DualStage, AxisName.Stage1_T, StagePosName.ZeroDegPos))
                            {
                                if (Motion.IsArrive(StationEnum.DualStage, AxisName.Stage2_T, StagePosName.ZeroDegPos))
                                {
                                    MoveToLoadingStep = 1;
                                }
                                else if (Motion.IsMoveTimeOut(StationEnum.DualStage, AxisName.Stage2_T))
                                {
                                    OnShowMsg?.Invoke("AutoRun", "DualStage->平台2T轴移动到0度位置超时", Brushes.Red);
                                    AlarmManager.CreateAlarm((int)MotionAlarmCode.Stage2_TMoveTimeout);
                                    MoveToLoadingStep = 30;
                                }
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.DualStage, AxisName.Stage1_T))
                            {
                                OnShowMsg?.Invoke("AutoRun", "DualStage->平台1T轴移动到0度位置超时", Brushes.Red);
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.Stage1_TMoveTimeout);
                                MoveToLoadingStep = 30;
                            }
                            break;
                        }
                }
            }
            else
            {
                OnShowMsg?.Invoke("AutoRun", "DualStage->压头不在安全位置，禁止移动平台，请检查程序逻辑", Brushes.Red);
                AlarmManager.CreateAlarm((int)MotionAlarmCode.StageMoveFail);
                OnStop?.Invoke();
            }
        }
        /// <summary>
        /// 平台Z轴上升流程
        /// </summary>
        public void StageClyUp()
        {
            switch (StagePinUpStep)
            {
                case 1:
                    {
                        if (StageVacuumOffStep == 0)
                        {
                            Motion.MoveAbs(StationEnum.DualStage, AxisName.Stage1_Z, StagePosName.UpFirstPos);
                            Motion.MoveAbs(StationEnum.DualStage, AxisName.Stage2_Z, StagePosName.UpFirstPos);
                            OnShowMsg?.Invoke("AutoRun", "DualStage->平台Z轴低速上升到位置1", Brushes.Green);
                            StagePinUpStep = 5;
                        }
                        else
                        {
                            StageVacuumOff();
                        }
                        break;
                    }

                case 5:
                    {
                        if (Motion.IsArrive(StationEnum.DualStage, AxisName.Stage1_Z, StagePosName.UpFirstPos))
                        {
                            if (Motion.IsArrive(StationEnum.DualStage, AxisName.Stage2_Z, StagePosName.UpFirstPos))
                            {
                                Motion.MoveAbs(StationEnum.DualStage, AxisName.Stage1_Z, StagePosName.UpSecondPos);
                                Motion.MoveAbs(StationEnum.DualStage, AxisName.Stage2_Z, StagePosName.UpSecondPos);
                                OnShowMsg?.Invoke("AutoRun", "DualStage->平台Z轴高速上升到位置2", Brushes.Green);
                                StagePinUpStep = 10;
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.DualStage, AxisName.Stage2_Z))
                            {
                                OnShowMsg?.Invoke("AutoRun", "DualStage->平台2Z轴移动到变速位置超时", Brushes.Red);
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.Stage2_ZMoveTimeout);
                            }
                        }
                        else if (Motion.IsMoveTimeOut(StationEnum.DualStage, AxisName.Stage1_Z))
                        {
                            OnShowMsg?.Invoke("AutoRun", "DualStage->平台1Z轴移动到变速位置超时", Brushes.Red);
                            AlarmManager.CreateAlarm((int)MotionAlarmCode.Stage1_ZMoveTimeout);
                        }
                        break;
                    }
                case 10:
                    {
                        if (Motion.IsArrive(StationEnum.DualStage, AxisName.Stage1_Z, StagePosName.UpSecondPos))
                        {
                            if (Motion.IsArrive(StationEnum.DualStage, AxisName.Stage2_Z, StagePosName.UpSecondPos))
                            {
                                OnShowMsg?.Invoke("AutoRun", "DualStage->平台Z轴上升到位", Brushes.Green);
                                StagePinUpStep = 0;
                                IonizerValveControl(true);//离子风扇开启
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.DualStage, AxisName.Stage2_Z))
                            {
                                OnShowMsg?.Invoke("AutoRun", "DualStage->平台2Z轴移动到顶升位置超时", Brushes.Red);
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.Stage2_ZMoveTimeout);
                            }
                        }
                        else if (Motion.IsMoveTimeOut(StationEnum.DualStage, AxisName.Stage1_Z))
                        {
                            OnShowMsg?.Invoke("AutoRun", "DualStage->平台1Z轴移动到顶升位置超时", Brushes.Red);
                            AlarmManager.CreateAlarm((int)MotionAlarmCode.Stage1_ZMoveTimeout);
                        }
                        break;
                    }
            }
        }
        /// <summary>
        /// 平台Z轴下降流程
        /// </summary>
        public void StageCylDown()
        {
            switch (StagePinDownStep)
            {
                case 1:
                    {
                        Motion.MoveAbs(StationEnum.DualStage, AxisName.Stage1_Z, StagePosName.DownFirstPos);
                        Motion.MoveAbs(StationEnum.DualStage, AxisName.Stage2_Z, StagePosName.DownFirstPos);
                        OnShowMsg?.Invoke("AutoRun", "DualStage->平台Z轴低速下降到位置1", Brushes.Green);
                        StagePinDownStep = 5;
                        break;
                    }
                case 5:
                    {
                        if (Motion.IsArrive(StationEnum.DualStage, AxisName.Stage1_Z, StagePosName.DownFirstPos))
                        {
                            if (Motion.IsArrive(StationEnum.DualStage, AxisName.Stage2_Z, StagePosName.DownFirstPos))
                            {
                                Motion.MoveAbs(StationEnum.DualStage, AxisName.Stage1_Z, StagePosName.DownSecondPos);
                                Motion.MoveAbs(StationEnum.DualStage, AxisName.Stage2_Z, StagePosName.DownSecondPos);
                                OnShowMsg?.Invoke("AutoRun", "DualStage->平台Z轴高速下降到位置2", Brushes.Green);
                                StagePinDownStep = 10;
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.DualStage, AxisName.Stage2_Z))
                            {
                                OnShowMsg?.Invoke("AutoRun", "DualStage->平台2Z轴移动到变速位置超时", Brushes.Red);
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.Stage2_ZMoveTimeout);
                            }

                        }
                        else if (Motion.IsMoveTimeOut(StationEnum.DualStage, AxisName.Stage1_Z))
                        {
                            OnShowMsg?.Invoke("AutoRun", "DualStage->平台1Z轴移动到变速位置超时", Brushes.Red);
                            AlarmManager.CreateAlarm((int)MotionAlarmCode.Stage1_ZMoveTimeout);
                        }
                        break;
                    }
                case 10:
                    {
                        if (Motion.IsArrive(StationEnum.DualStage, AxisName.Stage1_Z, StagePosName.DownSecondPos))
                        {
                            if (Motion.IsArrive(StationEnum.DualStage, AxisName.Stage2_Z, StagePosName.DownSecondPos))
                            {
                                OnShowMsg?.Invoke("AutoRun", "DualStage->平台Z轴下降到位", Brushes.Green);
                                StagePinDownStep = 0;
                                IonizerValveControl(false);//离子风扇关闭
                            }
                            else if (Motion.IsMoveTimeOut(StationEnum.DualStage, AxisName.Stage2_Z))
                            {
                                OnShowMsg?.Invoke("AutoRun", "DualStage->平台2Z轴移动到初始位置超时", Brushes.Red);
                                AlarmManager.CreateAlarm((int)MotionAlarmCode.Stage2_ZMoveTimeout);
                            }
                        }
                        else if (Motion.IsMoveTimeOut(StationEnum.DualStage, AxisName.Stage1_Z))
                        {
                            OnShowMsg?.Invoke("AutoRun", "DualStage->平台1Z轴移动到初始位置超时", Brushes.Red);
                            AlarmManager.CreateAlarm((int)MotionAlarmCode.Stage1_ZMoveTimeout);
                        }
                        break;
                    }
            }
        }
        private bool IsDetectGlass(int glassNum)
        {
            string inputName = string.Empty;
            bool ret = true;
            for (int i = 0; i < 4; i++)
            {
                inputName = $"Stage{glassNum + 1}GlassDetect{i + 1}";
                ret &= Motion.GetDigitalInput(inputName);
                Thread.Sleep(10);
            }
            return ret;
        }

        /// <summary>
        /// 上下规整气缸是否全部在原点
        /// </summary>
        /// <returns></returns>
        public bool IsClyDown(int glassNum)
        {
            string clyName = string.Empty;
            bool ret = true;
            for (int i = 0; i < 8; i++)
            {
                clyName = $"Stage{glassNum+1}VerticalCyl{i + 1}";
                ret &= Motion.IsCylinderArrive(clyName,false);
                Thread.Sleep(10);
            }
            return ret;
        }
        /// <summary>
        /// 上下规整气缸是否全部在动点
        /// </summary>
        /// <returns></returns>
        public bool IsClyUp(int glassNum)
        {
            string clyName = string.Empty;
            bool ret = true;
            for (int i = 0; i < 8; i++)
            {
                clyName = $"Stage{glassNum + 1}VerticalCyl{i + 1}";
                ret &= Motion.IsCylinderArrive(clyName, true);
                Thread.Sleep(10);
            }
            return ret;
        }
        /// <summary>
        /// 左右规整气缸是否全部在原点
        /// </summary>
        /// <returns></returns>
        public bool IsClyClamp(int glassNum)
        {
            string clyName = string.Empty;
            bool ret = true;
            for (int i = 0; i < 8; i++)
            {
                clyName = $"Stage{glassNum + 1}HorizontalCyl{i + 1}";
                ret &= Motion.IsCylinderArrive(clyName, false);
                Thread.Sleep(10);
            }
            return ret;
        }
        /// <summary>
        /// 左右规整气缸是否全部在动点
        /// </summary>
        /// <returns></returns>
        public bool IsClyUnClamp(int glassNum)
        {
            string clyName = string.Empty;
            bool ret = true;
            for (int i = 0; i < 8; i++)
            {
                clyName = $"Stage{glassNum + 1}HorizontalCyl{i + 1}";
                ret &= Motion.IsCylinderArrive(clyName, true);
                Thread.Sleep(10);
            }
            return ret;
        }
        private bool CheckIsCanRun()
        {
            bool ret = true;
            if (Motion.GetDigitalInput(InputName.PrbLoaderDetectPrb1) || Motion.GetDigitalInput(InputName.PrbLoaderDetectPrb1))
            {
                StationManager.Instance.ShowMsg("AutoRun", $"DualStage->自动更换手臂上检测到Probe，无法进行更换Probe", Brushes.Red);
                OnStop?.Invoke();
                return false;
            }
            if (Param.Control.PrbChgMode == PrbChgMode.Unload || Param.Control.PrbChgMode == PrbChgMode.UnloadLoad)//卸载Prb或者卸载+安装Prb模式
            {
                //检测Uvw上是否有Prb，如果没有Prb则不能继续卸载
                if (Motion.GetDigitalInput(InputName.UvwDetPrbLeft) && Motion.GetDigitalInput(InputName.UvwDetPrbRight))
                {
                    if (string.IsNullOrEmpty(RecipeManager.CurRecipe.RecipeID))//判断Uvw上的ProbeCard机种名是否为空，如果为空，无法查询此ProbeCard放置在料仓哪一层
                    {
                        AlarmManager.CreateAlarm((int)MotionAlarmCode.CanNotUnloadCuzCurProbeCardNoRecipeName);
                        ret = false;
                    }
                    else
                    {
                        ret &= true;
                    }
                }
                else
                {
                    ret = false;
                    AlarmManager.CreateAlarm((int)MotionAlarmCode.CanNotUnoadPrbCuzUvwDetectNoPrb);
                }
                int rowNum = CabinetInfoHelper.GetRowNum(RecipeManager.CurRecipe.RecipeID);
                if (rowNum < 0)
                {
                    StationManager.Instance.ShowMsg("AutoRun", $"DualStage->获取机种:{RecipeManager.CurRecipe.RecipeID}压头所在料仓层号失败，请检查配置信息", Brushes.Red);
                    OnStop?.Invoke();
                    return false;
                }
                string leftSensor = $"CabDetectPrbLeft{ rowNum + 1}F";
                string rightSensor = $"CabDetectPrbRight{ rowNum + 1}F";
                if (Motion.GetDigitalInput(leftSensor) || Motion.GetDigitalInput(rightSensor))
                {
                    AlarmManager.CreateAlarm((int)MotionAlarmCode.CanNotUnloadPrbCuzCabinetDetectPrb);
                }
                else
                {
                    ret &= true;
                }
            }
            else//安装Prb模式
            {
                //检测Uvw上是否有Prb，如果已经有了Prb则不能继续安装
                if (Motion.GetDigitalInput(InputName.UvwDetPrbLeft) || Motion.GetDigitalInput(InputName.UvwDetPrbRight))
                {
                    AlarmManager.CreateAlarm((int)MotionAlarmCode.CanNotLoadPrbCuzUvwDetectPrb);
                    ret = false;
                }
                else
                {
                    ret &= true;
                }
                int rowNum = CabinetInfoHelper.GetRowNum(Global.ReceivedRecipeName);
                if (rowNum < 0)
                {
                    StationManager.Instance.ShowMsg("AutoRun", $"DualStage->获取机种:{RecipeManager.CurRecipe.RecipeID}压头所在料仓层号失败，请检查配置信息", Brushes.Red);
                    OnStop?.Invoke();
                    return false;
                }
                string leftSensor = $"CabDetectPrbLeft{ rowNum + 1}F";
                string rightSensor = $"CabDetectPrbRight{ rowNum + 1}F";
                if ((Motion.GetDigitalInput(leftSensor) && Motion.GetDigitalInput(rightSensor)))
                {
                    ret &= true;
                }
                else
                {
                    AlarmManager.CreateAlarm((int)MotionAlarmCode.CanNotLoadPrbCuzCabinetDetectNoPrb);
                    ret = false;
                }

            }
            return ret;
        }
     
        public bool Stop()
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
    }


    /// <summary>
    /// 点位名称
    /// </summary>
    public enum StagePosName
    {
        /// <summary>
        /// 每个轴的安全位置
        /// </summary>
        SafePos,
        /// <summary>
        /// 旋转平台旋转时Y轴位置
        /// </summary>
        RotatePos,
        /// <summary>
        /// Y1Y2上下料时位置
        /// </summary>
        LoadingPos,
        /// <summary>
        /// Y1Y2检测位置
        /// </summary>
        DetectingPos,
        /// <summary>
        /// 规整气缸可以动作的位置
        /// </summary>
        AlignPos,
        /// <summary>
        /// 旋转平台旋转角度，0度
        /// </summary>
        ZeroDegPos,
        /// <summary>
        /// 旋转平台旋转角度，90度
        /// </summary>
        NintyDegPos,
        /// <summary>
        /// 旋转平台旋转角度，-90度
        /// </summary>
        Neg90DegPos,
        /// <summary>
        /// 旋转平台旋转角度，-180度
        /// </summary>
        Neg180DegPos,
        /// <summary>
        /// 平台Z轴上升位置1，低速
        /// </summary>
        UpFirstPos,
        /// <summary>
        /// 平台Z轴上升位置2，高速
        /// </summary>
        UpSecondPos,
        /// <summary>
        /// 平台Z轴下降位置1，低速
        /// </summary>
        DownFirstPos,
        /// <summary>
        /// 平台Z轴下降位置2，高速
        /// </summary>
        DownSecondPos,

    }
}
