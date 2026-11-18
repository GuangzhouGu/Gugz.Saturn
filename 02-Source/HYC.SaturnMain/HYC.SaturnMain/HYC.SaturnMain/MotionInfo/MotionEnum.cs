/*----------------------------------------------------------------
// Copyright (C)  Suzhou HYC Technology Co.,LTD
// 版权所有。
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnMain.Devices.Motion
// 文件名称    ：MotionEnum.cs
// =================================
// 创 建 者    ：guguangzhou
// 创建日期    ：2021/01/29 09:00:00 
// 功能描述    ：枚举类
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYC.SaturnMain.Devices.Motion
{
    public enum StationEnum
    {
        /// <summary>
        /// 平台工位
        /// </summary>
        DualStage,
        /// <summary>
        /// Probe龙门工位
        /// </summary>
        ProbeGantry,
        /// <summary>
        /// 检测龙门工位
        /// </summary>
        DetectGantry,
        /// <summary>
        /// Probe自动更换工位
        /// </summary>
        PrbAutoChg,
        /// <summary>
        /// 料仓工位
        /// </summary>
        Cabinet
    }
    public enum AxisName
    {
        None = -1,
        #region 平台工位轴名称
        /// <summary>
        /// 平台1Y轴
        /// </summary>
        Stage1_Y,
        /// <summary>
        /// 平台2Y轴
        /// </summary>
        Stage2_Y,
        /// <summary>
        /// 平台1T轴
        /// </summary>
        Stage1_T,
        /// <summary>
        /// 平台2T轴
        /// </summary>
        Stage2_T,
        /// <summary>
        /// 平台1Z轴
        /// </summary>
        Stage1_Z,
        /// <summary>
        /// 平台2Z轴
        /// </summary>
        Stage2_Z,
        /// <summary>
        /// 平台1压接轴
        /// </summary>
        Stage1_ContactZ,
        /// <summary>
        /// 平台2压接轴
        /// </summary>
        Stage2_ContactZ,
        #endregion

        #region Probe龙门工位轴名称
        /// <summary>
        /// Probe龙门主轴
        /// </summary>
        ProbeGantry_Y1,
        /// <summary>
        /// Probe龙门从轴
        /// </summary>
        ProbeGantry_Y2,
        /// <summary>
        /// 对位相机1X轴
        /// </summary>
        ProbeGantryAlig_X1,
        /// <summary>
        /// 对位相机2X轴
        /// </summary>
        ProbeGantryAlig_X2,

        /// <summary>
        /// Uvw平台Y轴
        /// </summary>
        UvwStage_Y,
        /// <summary>
        /// Uvw平台X1轴
        /// </summary>
        UvwStage_X1,
        /// <summary>
        /// Uvw平台X2轴
        /// </summary>
        UvwStage_X2,

        #endregion

        #region 检测龙门工位轴名称
        /// <summary>
        /// 检测龙门主轴
        /// </summary>
        DetectGantry_Y1,
        /// <summary>
        /// 检测龙门从轴
        /// </summary>
        DetectGantry_Y2,
        /// <summary>
        /// 一号检测模组X轴
        /// </summary>
        DetectModule1_X,
        /// <summary>
        /// 一号检测模组Z1轴
        /// </summary>
        DetectModule1_Z1,
        /// <summary>
        /// 一号检测模组Z2轴
        /// </summary>
        DetectModule1_Z2,
        /// <summary>
        /// 二号检测模组X轴
        /// </summary>
        DetectModule2_X,
        /// <summary>
        /// 二号检测模组Z1轴
        /// </summary>
        DetectModule2_Z1,
        /// <summary>
        /// 二号检测模组Z2轴
        /// </summary>
        DetectModule2_Z2,
        DetectModule1_Rotate,
        DetectModule2_Rotate,
        #endregion

        #region 料仓工位轴名称
        /// <summary>
        /// 料仓升降龙门主轴
        /// </summary>
        CabGantry_Z1,
        /// <summary>
        /// 料仓升降龙门从轴
        /// </summary>
        CabGantry_Z2,
        /// <summary>
        /// Probe上料主轴
        /// </summary>
        PrbLoader_Y1,
        /// <summary>
        /// Probe上料从轴
        /// </summary>
        PrbLoader_Y2,
        /// <summary>
        /// Probe上料平台升降轴
        /// </summary>
        PrbLoader_Z,
        /// <summary>
        /// Probe缓存平台平移轴
        /// </summary>
        PrbBuffer_Y,
        #endregion

        #region Probe自动更换工位轴名称
        /// <summary>
        /// Probe自动更换Y轴
        /// </summary>
        PrbAutoChg_Y,
        /// <summary>
        /// Probe自动更换Z轴
        /// </summary>
        PrbAutoChg_Z,
        #endregion

    }
    /// <summary>
    /// 插补名称
    /// </summary>
    public enum UnionName
    {
        PrbLoaderUnion,
    }
    public enum InputName
    {
        #region InputModule1 64bit
        Stage1Align1Clamp,//规整气缸夹紧
        Stage1Align1UnClamp,//规整气缸上松开
        Stage1Align2Clamp,
        Stage1Align2UnClamp,
        Stage1Align3Clamp,
        Stage1Align3UnClamp,
        Stage1Align4Clamp,
        Stage1Align4UnClamp,
        Stage1Align5Clamp,
        Stage1Align5UnClamp,
        Stage1Align6Clamp,
        Stage1Align6UnClamp,
        Stage1Align7Clamp,
        Stage1Align7UnClamp,
        Stage1Align8Clamp,
        Stage1Align8UnClamp,

        Stage1Align1Up,//规整气缸上
        Stage1Align1Down,//规整气缸下
        Stage1Align2Up,
        Stage1Align2Down,
        Stage1Align3Up,
        Stage1Align3Down,
        Stage1Align4Up,
        Stage1Align4Down,
        Stage1Align5Up,
        Stage1Align5Down,
        Stage1Align6Up,
        Stage1Align6Down,
        Stage1Align7Up,
        Stage1Align7Down,
        Stage1Align8Up,
        Stage1Align8Down,

        Stage2Align1Clamp,
        Stage2Align1UnClamp,
        Stage2Align2Clamp,
        Stage2Align2UnClamp,
        Stage2Align3Clamp,
        Stage2Align3UnClamp,
        Stage2Align4Clamp,
        Stage2Align4UnClamp,
        Stage2Align5Clamp,
        Stage2Align5UnClamp,
        Stage2Align6Clamp,
        Stage2Align6UnClamp,
        Stage2Align7Clamp,
        Stage2Align7UnClamp,
        Stage2Align8Clamp,
        Stage2Align8UnClamp,

        Stage2Align1Up,
        Stage2Align1Down,
        Stage2Align2Up,
        Stage2Align2Down,
        Stage2Align3Up,
        Stage2Align3Down,
        Stage2Align4Up,
        Stage2Align4Down,
        Stage2Align5Up,
        Stage2Align5Down,
        Stage2Align6Up,
        Stage2Align6Down,
        Stage2Align7Up,
        Stage2Align7Down,
        Stage2Align8Up,
        Stage2Align8Down,
        #endregion

        #region InputModule2
        ShutterOpenL,
        ShutterOpenR,
        ShutterCloseL,
        ShutterCloseR,
        Stage1YSafeSensor,
        Stage2YSafeSensor,
        Stage2YSafeSensor2,
        PrbAutoChgYSafeSensor,
        PrbAutoChgDetLeft,
        PrbAutoChgDetRight,
        RobotDetect,

        Stage1PinDetect1,
        Stage1PinDetect2,
        Stage1PinDetect3,
        Stage1PinDetect4,
        Stage1GlassDetect1,
        Stage1GlassDetect2,
        Stage1GlassDetect3,
        Stage1GlassDetect4,
        Stage2PinDetect1,
        Stage2PinDetect2,
        Stage2PinDetect3,
        Stage2PinDetect4,
        Stage2GlassDetect1,
        Stage2GlassDetect2,
        Stage2GlassDetect3,
        Stage2GlassDetect4,
        UvwDetPrbLeft,
        UvwDetPrbRight,
        AlignCamSafeSensor,
        GantrySafeSensor1,
        GantrySafeSensor2,
        FixPcbCylLockArrive,
        FixPcbCylUnlockArrive,
        CDAMain,
        CDADamping1,
        CDADamping2,
        CDAStage1Align,
        CDAStage2Align,
        CDAStage1Vaccum,
        CDAStage2Vaccum,
        CDAShutter,
        CDAReviewCamCooling,
        CDAIonizer,
        CDAZeroSys,
        DampingSignal1,//减震信号
        DampingSignal2,
        DampingSignal3,
        Stage1VacDetect,
        Stage2VacDetect,
        #endregion

        #region InputModule3
        CabDetectPrbLeft1F,
        CabDetectPrbLeft2F,
        CabDetectPrbLeft3F,
        CabDetectPrbLeft4F,
        CabDetectPrbLeft5F,
        CabDetectPrbLeft6F,
        CabDetectPrbLeft7F,
        CabDetectPrbLeft8F,
        CabDetectPrbRight1F,
        CabDetectPrbRight2F,
        CabDetectPrbRight3F,
        CabDetectPrbRight4F,
        CabDetectPrbRight5F,
        CabDetectPrbRight6F,
        CabDetectPrbRight7F,
        CabDetectPrbRight8F,
        PrbBufferDetectPrb1,
        PrbBufferDetectPrb2,
        PrbLoaderDetectPrb1,
        PrbLoaderDetectPrb2,
        SmokeAlarm1,
        SmokeAlarm2,
        EmgStop1,
        EmgStop2,
        EmgStop3,
        EmgStop4,
        EmgStop5,
        EmgStop6,
        SafeDoor1,
        SafeDoor2,
        SafeDoor3,
        SwitchButton,
        ManualEmgStop,
        EboxEmgStop,
        OPTableEmgStop,
        TempCtrlFeedback,
        OPTableTempCtrlFeedback,
        DetectAutoChgY1,
        DetectAutoChgY2,
        CabinetEmgStop1,
        CabinetEmgStop2,
        RobotEMO,
        RobotEMS,
        RobotArmSignal1,
        RobotArmSignal2,
        ManualButtonSignal,
        #endregion
    }
    public enum OutputName
    {
        #region OutputModule1 64bit

        Stage1Align1ClampOut,
        Stage1Align2ClampOut,
        Stage1Align3ClampOut,
        Stage1Align4ClampOut,
        Stage1Align5ClampOut,
        Stage1Align6ClampOut,
        Stage1Align7ClampOut,
        Stage1Align8ClampOut,
        Stage1Align1UnClampOut,
        Stage1Align2UnClampOut,
        Stage1Align3UnClampOut,
        Stage1Align4UnClampOut,
        Stage1Align5UnClampOut,
        Stage1Align6UnClampOut,
        Stage1Align7UnClampOut,
        Stage1Align8UnClampOut,

        Stage2Align1ClampOut,
        Stage2Align2ClampOut,
        Stage2Align3ClampOut,
        Stage2Align4ClampOut,
        Stage2Align5ClampOut,
        Stage2Align6ClampOut,
        Stage2Align7ClampOut,
        Stage2Align8ClampOut,
        Stage2Align1UnClampOut,
        Stage2Align2UnClampOut,
        Stage2Align3UnClampOut,
        Stage2Align4UnClampOut,
        Stage2Align5UnClampOut,
        Stage2Align6UnClampOut,
        Stage2Align7UnClampOut,
        Stage2Align8UnClampOut,

        Stage1Align1DownOut,
        Stage1Align2DownOut,
        Stage1Align3DownOut,
        Stage1Align4DownOut,
        Stage1Align5DownOut,
        Stage1Align6DownOut,
        Stage1Align7DownOut,
        Stage1Align8DownOut,
        Stage1Align1UpOut,
        Stage1Align2UpOut,
        Stage1Align3UpOut,
        Stage1Align4UpOut,
        Stage1Align5UpOut,
        Stage1Align6UpOut,
        Stage1Align7UpOut,
        Stage1Align8UpOut,

        Stage2Align1DownOut,
        Stage2Align2DownOut,
        Stage2Align3DownOut,
        Stage2Align4DownOut,
        Stage2Align5DownOut,
        Stage2Align6DownOut,
        Stage2Align7DownOut,
        Stage2Align8DownOut,
        Stage2Align1UpOut,
        Stage2Align2UpOut,
        Stage2Align3UpOut,
        Stage2Align4UpOut,
        Stage2Align5UpOut,
        Stage2Align6UpOut,
        Stage2Align7UpOut,
        Stage2Align8UpOut,
        #endregion

        #region OutputModule2
        Stage1Blow,
        Stage1Vacuum,
        Stage1IonizerPower1,
        Stage1IonizerPower2,
        Stage2Blow,
        Stage2Vacuum,
        Stage2IonizerPower1,
        Stage2IonizerPower2,
        DetectGantryIonizerPower,
        FixPcbCylLock,
        FixPcbCylUnlock,
        ShutterGateOpen,
        ShutterGateClose,
        DetectGantryIonizer,
        Stage1Ionizer,
        Stage2Ionizer,
        ShutterIonizer,
        UvwZeroSysLock,
        PrbLoaderZeroSysLock1,
        PrbLoaderZeroSysUnLock1,
        PrbLoaderZeroSysLock2,
        PrbLoaderZeroSysUnLock2,
        UvwZeroSysUnLock,
        RedLight,
        YellowLight,
        GreenLight,
        BlueLight,
        Buzzer,
        RedLight2,
        YellowLight2,
        GreenLight2,
        BlueLight2,
        Buzzer2,
        DoorUnLock1,
        DoorUnLock2,
        DoorUnLock3,
        ModeSwitchRelay,
        EqpFloodLight1,
        EqpFloodLight2,
        EqpFloodLight3,
        EqpFloodLight4,
        EqpFloodLight5,
        EqpFloodLight6,
        EqpFloodLight7,
        EqpFloodLight8,
        CabinetFloodLight1,
        CabinetFloodLight2,
        Tester1Power,
        Tester2Power,
        #endregion

        #region OutputModule3
        Stage1AxisTDrive,
        Stage2AxisTDrive,
        Stage1_YDrive,
        Stage2_YDrive,
        IRGty_Y1Drive,
        ProbeGantry_Y1Drive,
        Stage1_ZDrive,
        DetectGantryRev_XDrive,
        DetectGantryRev_ZDrive,
        DetectGantryIR_XDrive,
        DetectGantryIR_ZDrive,
        Stage2_ZDrive,
        ProbeGantryAlig_X1Drive,
        ProbeGantryAlig_X2Drive,
        ProbeGantryAlig_Z1Drive,
        ProbeGantryAlig_Z2Drive,
        UvwStage_YDrive,
        UvwStage_X1Drive,
        UvwStage_X2Drive,
        UvwStage_Z1Drive,
        UvwStage_Z2Drive,
        PcbFixAxisDrive,
        PrbAutoChg_YDrive,
        PrbAutoChg_ZDrive,
        PrbLoader_ZDrive,
        PrbBuffer_YDrive,
        PrbLoader_Y1Drive,
        PrbLoader_Y2Drive,
        CabGantry_Z1Drive,
        CabGantry_Z2Drive,
        SafePlcReset,
        UvwBrake1,
        UvwBrake2,
        /// <summary>
        /// 本机发送给机械手的Emo安全信号，和急停开关联动
        /// </summary>
        EQEMO,
        /// <summary>
        /// 本机发送给机械手的Ems安全信号
        /// </summary>
       // EQEMS,
        /// <summary>
        /// 本机发送给机械手的门禁信号
        /// </summary>
        Stage1ShutterSts,
        Stage2ShutterSts,
        /// <summary>
        /// 本机发送给机械手的Pin信号
        /// </summary>
        Stage1PinSts,
        Stage2PinSts,
        Stage1SensorPower,
        Stage2SensorPower,
        #endregion
    }
    public enum CylinderName
    {
        Stage1HorizontalCyl1,
        Stage1HorizontalCyl2,
        Stage1HorizontalCyl3,
        Stage1HorizontalCyl4,
        Stage1HorizontalCyl5,
        Stage1HorizontalCyl6,
        Stage1HorizontalCyl7,
        Stage1HorizontalCyl8,
        Stage1VerticalCyl1,
        Stage1VerticalCyl2,
        Stage1VerticalCyl3,
        Stage1VerticalCyl4,
        Stage1VerticalCyl5,
        Stage1VerticalCyl6,
        Stage1VerticalCyl7,
        Stage1VerticalCyl8,

        Stage2HorizontalCyl1,
        Stage2HorizontalCyl2,
        Stage2HorizontalCyl3,
        Stage2HorizontalCyl4,
        Stage2HorizontalCyl5,
        Stage2HorizontalCyl6,
        Stage2HorizontalCyl7,
        Stage2HorizontalCyl8,
        Stage2VerticalCyl1,
        Stage2VerticalCyl2,
        Stage2VerticalCyl3,
        Stage2VerticalCyl4,
        Stage2VerticalCyl5,
        Stage2VerticalCyl6,
        Stage2VerticalCyl7,
        Stage2VerticalCyl8,

        ShutterClyL,
        ShutterClyR,
        PcbFixCyl,
    }
    public enum VacuumName
    {
        Stage1Vacuum,
        Stage2Vacuum,
    }
    /// <summary>
    /// stage,ProbeGantry,DetectGantry交互指令枚举
    /// </summary>
    public enum StationCommand
    {
        /// <summary>
        /// 平台到达检测位置
        /// </summary>
        StgArrDetPos,
        /// <summary>
        /// Probe龙门到达初始位置
        /// </summary>
        ProbeGantryArrSafePos,
        /// <summary>
        /// 检测龙门到达初始位置
        /// </summary>
        IRGtyArrSafePos,

        /// <summary>
        /// 检测结束
        /// </summary>
        DetectEnd,
     
        /// <summary>
        /// 扫码
        /// </summary>
        ScanCode,
        /// <summary>
        /// 扫码完毕
        /// </summary>
        ScanCodeCom,
        /// <summary>
        /// Probe自动更换
        /// </summary>
        PrbAutoChg,
        /// <summary>
        /// ProbeCard更换完成
        /// </summary>
        PrbAutoChgCom,
        /// <summary>
        /// 自动运行
        /// </summary>
        AutoRun,
        /// <summary>
        /// Probe自动更换暂停
        /// </summary>
        PrbAutoChgPause,
        StageZHomeCom,
        PrbAutoChgHomeCom,
        PrbAutoChgYArriveSafePos,
        CabinetZHomeCom,
        StageArriveAutoChgPos,
        ProbeGantryArrChgPos,
        /// <summary>
        /// 单片检测完成，用于更新检测界面的endTime
        /// </summary>
        OneGlassDetectCom,
        DetectGantryArrived,
        DetectGantryMoving,
        AoiDetectReq,
        /// <summary>
        /// 直视Mura检测请求
        /// </summary>
        MuraDetectReq,
        /// <summary>
        /// 斜视Mura检测请求
        /// </summary>
        SquintMuraDetectReq,
        MultiAngleDetectReq,
        AoiDetectCom,
        /// <summary>
        /// 直视Mura检测完成
        /// </summary>
        MuraDetectCom,
        /// <summary>
        /// 斜视Mura检测完成
        /// </summary>
        SquintMuraDetectCom,
        MultiAngleDetectCom,
    }
}
