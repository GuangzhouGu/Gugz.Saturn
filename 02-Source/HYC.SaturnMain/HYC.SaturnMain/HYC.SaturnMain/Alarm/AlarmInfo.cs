/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2020 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnMain
// 文件名称    ：AlarmInfo.cs
// =================================
// 创 建 者    ：guguangzhou
// 创建日期    ：2021/01/29 09:00:00 
// 功能描述    ：报警信息
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
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYC.SaturnMain.Alarm
{
   
    public enum SaftyAlarmCode
    {
        /// <summary>
        ///  配电柜急停按钮被按下
        /// </summary>
        EboxEmgStopPressed,
        /// <summary>
        /// 操作台急停按钮被按下
        /// </summary>
        OPTableEmgStopPressed,
        /// <summary>
        /// Prb配电柜急停按钮被按下
        /// </summary>
        PrbBoxEmgStop,
        /// <summary>
        /// 设备左侧左急停按钮被按下
        /// </summary>
        CoverLeftLeftEmgStopPressed,
        /// <summary>
        /// 设备左侧右急停按钮被按下
        /// </summary>
        CoverLeftRightEmgStopPressed,
        /// <summary>
        /// 设备前方左急停按钮被按下
        /// </summary>
        CoverFrontLeftEmgStopPressed,
        /// <summary>
        /// 设备前方右急停按钮被按下
        /// </summary>
        CoverFrontRightEmgStopPressed,
        /// <summary>
        /// 设备右侧左急停按钮被按下
        /// </summary>
        CoverRightLeftEmgStopPressed,
        /// <summary>
        /// 设备右侧右急停按钮被按下
        /// </summary>
        CoverRightRightEmgStopPressed,
        /// <summary>
        /// 左侧安全门被打开
        /// </summary>
        LeftDoorOpened,
        /// <summary>
        /// 右侧安全门被打开
        /// </summary>
        RightDoorOpened,
        /// <summary>
        /// 前方安全门被打开
        /// </summary>
        FrontDoorOpened,
        /// <summary>
        /// 主压缩空气报警
        /// </summary>
        CDAMainAlarm,
        /// <summary>
        /// 气垫1压缩空气报警
        /// </summary>
        CDADamping1Alarm,
        /// <summary>
        /// 气垫2压缩空气报警
        /// </summary>
        CDADamping2Alarm,
        /// <summary>
        /// 平台1规整气缸压缩空气报警
        /// </summary>
        CDAStage1AlignAlarm,
        /// <summary>
        /// 平台2规整气缸压缩空气报警
        /// </summary>
        CDAStage2AlignAlarm,
        /// <summary>
        /// 平台1真空压缩空气报警
        /// </summary>
        CDAStage1VaccumAlarm,
        /// <summary>
        /// 平台2真空压缩空气报警
        /// </summary>
        CDAStage2VaccumAlarm,
        /// <summary>
        /// 门禁压缩空气报警
        /// </summary>
        CDAShutterAlarm,
        /// <summary>
        /// Review相机冷却压缩空气报警
        /// </summary>
        CDAReviewCamCoolingAlarm,
        /// <summary>
        /// 离子风扇压缩空气报警
        /// </summary>
        CDAIonizerAlarm,
        /// <summary>
        /// 零点系统压缩空气报警
        /// </summary>
        CDAZeroSysAlarm,
        /// <summary>
        /// 安全PLC报警
        /// </summary>
        SafetyPlcError
    }

    public enum PosErrorCode
    {
        /// <summary>
        /// Prb龙门和检测龙门位置小于安全位置
        /// </summary>
        ProbeGantryAndIRGtyPosError = 400,
        /// <summary>
        /// Align相机1X轴和Align相机2X轴位置小于安全位置
        /// </summary>
        AlignCamPosError,
        /// <summary>
        /// Stg1Y轴和Stg2Y轴位置小于安全位置
        /// </summary>
        StgPosError,
        /// <summary>
        /// 对位相机1和IR相机位置重叠，禁止移动龙门和相机
        /// </summary>
        Align1AndIRCamPosErr,
        /// <summary>
        /// Uvw轴不在初始位置，禁止移动平台Y轴，T轴和龙门轴
        /// </summary>
        UvwZIsNotAtSafePos,
        Stg1ZCantUpBecauseStg1HasGlass,
        Stg2ZCantUpBecauseStg2HasGlass,
        ProbeGtyCantMoveBecauseAlignCylUp,
        AlignCamNotAtSafePos,

        CabZNotAtSafepos,
        CabinetStopCuzLoaderYNotAtSafePos,
        CabinetStopCuzAutoChgYNotAtSafePos,
        GantryPosLimitDetect,
        ProbeGantryStopCuzPrbAutoChangeZNotAtSafePos,
        Stage1NotAtHorizontalPos,
        Stage2NotAtHorizontalPos,
        RobotArmSafeSignalOn,
        AutoChgYAtDangerPos,
        /// <summary>
        /// 对位相机2和IR相机位置重叠，禁止移动龙门和相机
        /// </summary>
        Align2AndIRCamPosErr,
        /// <summary>
        /// 平台1T轴禁止旋转因为机械手Robot1Signal置Off
        /// </summary>
        Stage1TCanTMoveBecauseRobot1SignalOff,
        /// <summary>
        /// 平台2T轴禁止旋转因为机械手Robot2Signal置Off
        /// </summary>
        Stage2TCanTMoveBecauseRobot2SignalOff,
        /// <summary>
        /// 平台1纵向规整气缸在上升位置，禁止移动Probe龙门和平台
        /// </summary>
        Stage1CylUp,
        /// <summary>
        /// 平台2纵向规整气缸在上升位置，禁止移动Probe龙门和平台
        /// </summary>
        Stage2CylUp,
        /// <summary>
        /// 2个检测模组X轴位置大于安全位置
        /// </summary>
        DetectModuleXPosErr,
    }
    public enum CimAlarmCode
    {
        /// <summary>
        /// 等待上游Sendable超时
        /// </summary>
        WaitUpStemSendAbleTimeout = 500,
        /// <summary>
        /// 等待下游JobTransferSignal超时
        /// </summary>
        WaitDnStemJobTranSigTimeout,
        /// <summary>
        /// 等待下游ReceiveComplete超时
        /// </summary>
        WaitDnStemRecComTimeout,
        /// <summary>
        /// 等待上游SendComplete超时
        /// </summary>
        WaitUpStemSendComTimeout,
        /// <summary>
        /// 等待上游JobTransferSignal超时
        /// </summary>
        WaitUpStemJobTranSigTimeout,
        /// <summary>
        /// 机械手在伸出位置，Shutter不能关闭
        /// </summary>
        RobotInStretchPos,
        /// <summary>
        /// 上游Inline信号没有置位
        /// </summary>
        UpStemNotInline,
        /// <summary>
        /// 下游Inline信号没有置位
        /// </summary>
        DownStemNotInline,
        /// <summary>
        /// 上游报警
        /// </summary>
        UpStemTrouble,
        /// <summary>
        /// 下游报警
        /// </summary>
        DownStemTrouble,
        JobDataCheckError,
        ReceiveCimRecipeTimeout,
        ShutterOpenTimeout,
        /// <summary>
        /// 等待下游ReceiveComplete置off超时
        /// </summary>
        WaitDnStemRecComOffTimeout,
        /// <summary>
        /// 等待上游SendComplete超时
        /// </summary>
        WaitUpStemSendComOffTimeout,
        /// <summary>
        /// 下游ReceiveAble没有关闭
        /// </summary>
        DownReceiveAbleNotOff,
        /// <summary>
        /// 下游ReceiveAble没有打开
        /// </summary>
        DownReceiveAbleNotOn,
        /// <summary>
        /// 等待下游初始化超时
        /// </summary>
        WaitDnInitAckOnTImeout,
        /// <summary>
        /// 等待上游初始化超时
        /// </summary>
        WaitUpInitAckOnTImeout,
        /// <summary>
        /// 下游初始化失败
        /// </summary>
        DnInitNack,
        /// <summary>
        /// 上游初始化失败
        /// </summary>
        UpInitNack,
        /// <summary>
        /// 等待上游SendStart信号Turn On超时
        /// </summary>
        WaitUpStemStartOnTimeout,
        /// <summary>
        /// 等待上游SendStart信号Turn On超时
        /// </summary>
        WaitUpJobTransferSignalOnTimeout,
        /// <summary>
        /// 机械手Emo安全信号Off
        /// </summary>
        RobotEmoOff,
        /// <summary>
        /// 机械手Ems安全信号Off
        /// </summary>
        RobotEmsOff,
        /// <summary>
        /// Pin没有上升到位
        /// </summary>
        StageNotReady,
        /// <summary>
        /// Shutter没有打开
        /// </summary>
        ShutterNotReady,
        /// <summary>
        /// Robot安全信号Off，禁止关闭Shutter气缸
        /// </summary>
        RobotArmSignalOff,
        /// <summary>
        /// 请求Cim校验条码失败
        /// </summary>
        JobIdValidationFail,

    }
  

    public enum MotionAlarmCode
    {
        DualStageHomeFail = 600,
        ProbeGantryStaHomeFail,
        DetectGantryStaHomeFail,
        PrbAutoChgStaHomeFail,
        CabinetStaHomeFail,
        /// <summary>
        /// ProbeGantry工位程序异常
        /// </summary>
        ProbeGantryStationException,
        DetectGantryStationException,
        DualStageStationException,
        ProbeChangeStationException,
        /// <summary>
        /// 平台1规整气缸上升超时
        /// </summary>
        Stg1AlignCylUpTimeout,
        /// <summary>
        /// 平台1规整气缸下降超时
        /// </summary>
        Stg1AlignCylDownTimeout,
        /// <summary>
        /// 平台1规整气缸夹紧超时
        /// </summary>
        Stg1AlignCylClampTimeout,
        /// <summary>
        /// 平台1规整气缸松开超时
        /// </summary>
        Stg1AlignCylUnClampTimeout,
        /// <summary>
        /// 平台1吸真空超时
        /// </summary>
        Stg1VacOnTimeout,
        /// <summary>
        /// 平台2吸真空超时
        /// </summary>
        Stg2VacOnTimeout,
        /// <summary>
        /// 平台1破真空超时
        /// </summary>
        Stg1VacOffTimeout,
        /// <summary>
        /// 平台2破真空超时
        /// </summary>
        Stg2VacOffTimeout,
        /// <summary>
        /// 平台2规整气缸上升超时
        /// </summary>
        Stg2AlignCylUpTimeout,
        /// <summary>
        /// 平台2规整气缸下降超时
        /// </summary>
        Stg2AlignCylDownTimeout,
        /// <summary>
        /// 平台2规整气缸夹紧超时
        /// </summary>
        Stg2AlignCylClampTimeout,
        /// <summary>
        /// 平台2规整气缸松开超时
        /// </summary>
        Stg2AlignCylUnClampTimeout,
        /// <summary>
        /// 对位超时
        /// </summary>
        AlignmentTimeout,

    
       /// <summary>
       /// Uvw平台运动超时
       /// </summary>
        UvwTimeout,

        /// <summary>
        /// Probe自动更换机构感应器未感应到Prb
        /// </summary>
        PrbAutoChgDetSenserFail,
        /// <summary>
        /// 料仓感应器未检测到Prb
        /// </summary>
        CabDetSenserFail,
        /// <summary>
        /// 料仓等待自当更换Prb完成超时
        /// </summary>
        CabWaitAutoChgTimeout,
        /// <summary>
        /// Uvw感应器未检测到Prb
        /// </summary>
        UvwDetSenserFail,
        /// <summary>
        /// PrbLoader未检测到Prb
        /// </summary>
        PrbLoaderDetSenserFail,
        /// <summary>
        /// 无法进行安装Prb操作，因为Uvw上检测到有ProbeCard
        /// </summary>
        CanNotLoadPrbCuzUvwDetectPrb,
        /// <summary>
        /// 无法进行卸载Prb操作，因为Uvw上没有检测到ProbeCard
        /// </summary>
        CanNotUnoadPrbCuzUvwDetectNoPrb,
        /// <summary>
        /// 无法进行安装Prb操作，因为料仓上没有检测到ProbeCard
        /// </summary>
        CanNotLoadPrbCuzCabinetDetectNoPrb,
        /// <summary>
        /// 无法进行卸载Prb操作，因为料仓上检测到有ProbeCard
        /// </summary>
        CanNotUnloadPrbCuzCabinetDetectPrb,
        /// <summary>
        /// 无法进行卸载Prb操作，因为当前在用的ProbeCard没有查询到机种名
        /// </summary>
        CanNotUnloadCuzCurProbeCardNoRecipeName,
        /// <summary>
        /// 缓存平台感应器未检测到Prb
        /// </summary>
        BufferDetSenserFail,
        /// <summary>
        /// 扫码失败
        /// </summary>
        ScancodeFail,
        /// <summary>
        /// 平台1Glass偏移，规整气缸无法规整
        /// </summary>
        Stage1GlassOffset,
        /// <summary>
        /// 平台2Glass偏移，规整气缸无法规整
        /// </summary>
        Stage2GlassOffset,
        /// <summary>
        /// Cabinet检测到Probe，无法执行Uvw->料仓流程，请检查料仓配置信息
        /// </summary>
        CabinetDetectProbe,
        /// <summary>
        /// Uvw检测到Probe，无法执行料仓->Uvw流程，请检查料仓配置信息
        /// </summary>
        UvwDetectProbe,
        /// <summary>
        /// Uvw平台Z轴不在安全位置，禁止移动龙门，请检查程序逻辑
        /// </summary>
        ProbeGantryMoveFail,
        /// <summary>
        /// Uvw平台Z轴不在安全位置，禁止移动平台，请检查程序逻辑
        /// </summary>
        StageMoveFail,
        /// <summary>
        /// PanelNG数量超过设定值
        /// </summary>
        PanelNGCountOverSetValue,
        /// <summary>
        /// BlockNG数量超过设定值
        /// </summary>
        BlockNGCountOverSetValue,
        /// <summary>
        /// YieldNG数量超过设定值
        /// </summary>
        YieldNGCountOverSetValue,
        /// <summary>
        /// X方向对位值超范围
        /// </summary>
        XValueError,
        /// <summary>
        ///Y方向对位值超范围
        /// </summary>
        YValueError,
        /// <summary>
        ///T方向对位值超范围
        /// </summary>
        TValueError,

        /// <summary>
        ///  Stage1_Y轴运动超时
        /// </summary>
        Stage1_YMoveTimeout,
        /// <summary>
        /// Stage2_Y轴运动超时
        /// </summary>
        Stage2_YMoveTimeout,
        /// <summary>
        ///  Stage1_T轴运动超时
        /// </summary>
        Stage1_TMoveTimeout,
        /// <summary>
        ///  Stage2_T轴运动超时
        /// </summary>
        Stage2_TMoveTimeout,
        /// <summary>
        /// Stage1_Z轴运动超时
        /// </summary>
        Stage1_ZMoveTimeout,
        /// <summary>
        /// Stage2_Z轴运动超时
        /// </summary>
        Stage2_ZMoveTimeout,
        /// <summary>
        /// Stage1_ContactZ轴运动超时
        /// </summary>
        Stage1_ContactZMoveTimeout,
        /// <summary>
        ///Stage2_ContactZ轴运动超时
        /// </summary>
        Stage2_ContactZMoveTimeout,
        /// <summary>
        /// ProbeGantry_Y1轴运动超时
        /// </summary>
        ProbeGantry_Y1MoveTimeout,
        /// <summary>
        /// ProbeGantryAlig_X1轴运动超时
        /// </summary>
        ProbeGantryAlig_X1MoveTimeout,
        /// <summary>
        /// ProbeGantryAlig_X2轴运动超时
        /// </summary>
        ProbeGantryAlig_X2MoveTimeout,
        /// <summary>
        /// UvwStage_Y轴运动超时
        /// </summary>
        UvwStage_YMoveTimeout,
        /// <summary>
        ///UvwStage_X1轴运动超时
        /// </summary>
        UvwStage_X1MoveTimeout,
        /// <summary>
        /// UvwStage_X2轴运动超时
        /// </summary>
        UvwStage_X2MoveTimeout,
        /// <summary>
        ///PcbFix气缸移动超时
        /// </summary>
        PcbFixCylMoveTimeout,
        /// <summary>
        /// DetectGantry_Y1轴运动超时
        /// </summary>
        DetectGantry_Y1MoveTimeout,
        /// <summary>
        ///DetectModule1_X轴运动超时
        /// </summary>
        DetectModule1_XMoveTimeout,
        /// <summary>
        /// DetectModule1_Z1轴运动超时
        /// </summary>
        DetectModule1_Z1MoveTimeout,
        /// <summary>
        /// DetectModule1_Z2轴运动超时
        /// </summary>
        DetectModule1_Z2MoveTimeout,
        /// <summary>
        /// DetectModule2_X轴运动超时
        /// </summary>
        DetectModule2_XMoveTimeout,
        /// <summary>
        /// DetectModule2_Z1轴运动超时
        /// </summary>
        DetectModule2_Z1MoveTimeout,
        /// <summary>
        ///  DetectModule2_Z2轴运动超时
        /// </summary>
        DetectModule2_Z2MoveTimeout,
        /// <summary>
        /// CabGantry_Z1轴运动超时
        /// </summary>
        CabGantry_Z1MoveTimeout,
        /// <summary>
        /// PrbLoader_Y1轴运动超时
        /// </summary>
        PrbLoader_Y1MoveTimeout,
        /// <summary>
        /// PrbLoader_Y2轴运动超时
        /// </summary>
        PrbLoader_Y2MoveTimeout,
        /// <summary>
        /// PrbLoader_Z轴运动超时
        /// </summary>
        PrbLoader_ZMoveTimeout,
        /// <summary>
        /// PrbBuffer_Y轴运动超时
        /// </summary>
        PrbBuffer_YMoveTimeout,
        /// <summary>
        ///PrbAutoChg_Y轴运动超时
        /// </summary>
        PrbAutoChg_YMoveTimeout,
        /// <summary>
        /// PrbAutoChg_Z轴运动超时
        /// </summary>
        PrbAutoChg_ZMoveTimeout,
        /// <summary>
        /// Stage压接超时
        /// </summary>
        StageContactTimeout,
        /// <summary>
        /// Stage停止压接超时
        /// </summary>
        StageStopContactTimeout,
        /// <summary>
        ///TesterPC单列检测超时
        /// </summary>
        OneColumnTestTimeout,
        /// <summary>
        /// Contact相机拍照失败
        /// </summary>
        ContactCamGrabFail,
        /// <summary>
        /// PC1检测超时
        /// </summary>
        PC1DetectTimeout,
        PC2DetectTimeout,
        /// <summary>
        /// PPID对应的机种不存在
        /// </summary>
        PPIDNotExist,
        /// <summary>
        /// 检测模组Z轴不在安全位置，禁止移动对位相机
        /// </summary>
        DetectModuleZNotSafe,
        Tester1ModeSwitchFail,
        Tester1ModeSwitchTimeout,
        Tester2ModeSwitchFail,
        Tester2ModeSwitchTimeout,
        PG1PowerOnFail,
        PG1PowerOnTimeout,
        PG2PowerOnFail,
        PG2PowerOnTimeout,
        PG1PowerOffFail,
        PG1PowerOffTimeout,
        PG2PowerOffFail,
        PG2PowerOffTimeout,
        /// <summary>
        /// 自动换型Y轴在料仓中，禁止回零
        /// </summary>
        AutoChgAxisYErrorPosCantHome,
        /// <summary>
        /// 自动更换手臂Sensor检测到Probe，禁止回零
        /// </summary>
        PrbAutoChgSensorDetectProbeCantHome,
        /// <summary>
        /// TesterPC掉线
        /// </summary>
        TesterPCOffline,
        /// <summary>
        /// AoiPC1掉线
        /// </summary>
        AoiPC1Offline,
        /// <summary>
        /// AoiPC2掉线
        /// </summary>
        AoiPC2Offline,
        /// <summary>
        /// DFS掉线
        /// </summary>
        DfsOffline,
        TesterPCShiftRecipeFail,
        AoiPC1ShiftRecipeFail,
        AoiPC2ShiftRecipeFail,
        ClientShiftRecipeTimeout,
    }
}
