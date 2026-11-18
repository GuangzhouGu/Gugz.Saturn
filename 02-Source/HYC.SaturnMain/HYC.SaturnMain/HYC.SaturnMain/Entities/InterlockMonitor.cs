/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2020 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnMain
// 文件名称    ：StationBase.cs
// =================================
// 创 建 者    ：guguangzhou
// 创建日期    ：2021/01/29 09:00:00 
// 功能描述    ：工位基类
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
using HYC.SaturnMain.Devices.Motion;
using HYC.SaturnMain.Entities;
using HYC.SaturnMain.SubForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;
using System.Drawing;
using HYC.SaturnMain.Devices.ClientPC;
using System.Windows.Forms;
using HYC.SaturnMain.Devices.Cim;
using HYC.HTPCBase.Motion;
using HYC.HTLog;
using HYC.SaturnMain.Params;
using HYC.HTPCBase.Extension.Entities;
using HYC.HTPCBase.Alarm;
using HYC.HTPCBase.Station;
using HYC.HTPCBase.UserCtrl;
using HYC.HTPCBase.Args;
using HYC.HTPCBase.Extension.Recipe;
using HYC.SaturnMain.Alarm;

namespace HYC.SaturnMain.Station
{
    /// <summary>
    /// Interlock监控
    /// </summary>
    public class InterlockMonitor
    {
        MotionManager MotionManager = MotionManager.GetInstance();
        private bool[] IsTriggered = new bool[5];
        public static bool IsSafeDoorOpen = false;
        public static event Action<string, Brush> OnShowMsg;

        #region 私有变量
        ProbeGantry Probe = null;
        bool StageAxisYPosErr = false;
        bool AlignCamAxisXPosErr = false;
        bool GantryPosErr = false;
        bool GantryPosErr2 = false;
        MotionPara MotionPara = null;
        ControlPara ControlPara = null;
        #endregion
        public static InterlockMonitor Instance = null;
        private InterlockMonitor()
        {

        }
        static InterlockMonitor()
        {
            Instance = new InterlockMonitor();
        }
        public void StartMonitor()
        {
            MotionPara = RecipeManager.GetPara(ParaType.Motion) as MotionPara;
            ControlPara = RecipeManager.GetPara(ParaType.Control) as ControlPara;
            Probe = StationManager.Instance.GetChild(StationEnum.ProbeGantry) as ProbeGantry;
            MonitorAxisSafePos();
            MotionTeachCtrl.OnCheckIsCanMove += CheckIsCanMove;
            MotionTeachCtrl.OnCheckIsCanHome += CheckIsCanHome;
            StationManager.Instance.OnReset += Instance_OnReset;
            ParaManager.OnParaChange += ParaManager_OnParaChange;
        }

        private void ParaManager_OnParaChange(string obj)
        {
            switch (obj)
            {
                case "MotionPara":
                    {
                        MotionPara = RecipeManager.GetPara(ParaType.Motion) as MotionPara;
                        break;
                    }
                case "ControlPara":
                    {
                        ControlPara = RecipeManager.GetPara(ParaType.Control) as ControlPara;
                        break;
                    }
                default:
                    break;
            }

        }

        private void Instance_OnReset()
        {
            IsTriggered = new bool[5];
        }

        public bool CheckIsCanMove(MoveArgs e)
        {
            if (IsSafeDoorOpen && !MotionManager.GetDigitalInput(InputName.ManualButtonSignal) && ControlPara.ManualEmgEnable)
            {
                MessageBox.Show($"安全门开启，手持急停按钮没有在安全状态，禁止移动{e.Axis.AxisName}轴");
                return false;
            }
            if (UserManager.CurUser.Level < UserLevel.Developer && (StationManager.Instance.IsInRunning() || StationManager.Instance.IsInHoming()))
            {
                MessageBox.Show("设备正在运行中，禁止移动伺服");
                return false;
            }
            bool result = true;
            AxisName axis = HYC.HTPCBase.Extension.Entities.EnumHelper.GetEnumValue<AxisName>(e.Axis.AxisName);
            if (axis == AxisName.ProbeGantry_Y2 || axis == AxisName.DetectGantry_Y2 || axis == AxisName.CabGantry_Z2)
            {
                MessageBox.Show("龙门从轴禁止移动，请选择龙门主轴");
                return false;
            }
            if (e.IsUnion)
            {
                switch (e.UnionName)
                {
                    case "PrbLoaderUnion":
                        {
                            var ret = MotionManager.IsArrive(StationEnum.Cabinet, AxisName.PrbLoader_Z, CabinetPosName.LoadZBeforeToCabPos);
                            if (ret == false && e.Dir == 1)
                            {
                                MessageBox.Show($"Probe上料平台Z轴不在进入料仓位置，禁止执行{e.UnionName}插补");
                                result = false;
                            }
                            break;
                        }
                    default:
                        break;
                }
            }
            else
            {
                switch (axis)
                {
                    case AxisName.Stage1_T:
                    case AxisName.Stage2_T:
                        {
                            if (!IsContactZAtSafePos())
                            {
                                MessageBox.Show($"ProbeCard压头在下压位置，禁止移动{axis}轴");
                                result = false;
                                break;
                            }
                            AxisInfo stage1Y = AxisInfoHelper.GetAxisInfoByName(AxisName.Stage1_Y.ToString());
                            AxisInfo stage2Y = AxisInfoHelper.GetAxisInfoByName(AxisName.Stage2_Y.ToString());
                            if (stage1Y.ActPos + stage2Y.ActPos > MotionPara.Stage1TStage2TPreventCrushPos)
                            {
                                MessageBox.Show("平台1和平台2距离太近，禁止T轴旋转");
                                result = false;
                                break;
                            }
                            if (!IsStageAxisZDown(0) && axis == AxisName.Stage1_T)
                            {
                                MessageBox.Show("平台1Z轴不在安全位置，禁止T轴旋转");
                                result = false;
                                break;
                            }
                            if (!IsStageAxisZDown(1) && axis == AxisName.Stage2_T)
                            {
                                MessageBox.Show("平台2Z轴不在安全位置，禁止T轴旋转");
                                result = false;
                                break;
                            }
                            if (!MotionManager.GetDigitalInput(InputName.RobotDetect))
                            {
                                MessageBox.Show("上游机械手在危险位置，禁止T轴旋转");
                                result = false;
                                break;
                            }
                            if (!MotionManager.GetDigitalInput(InputName.RobotArmSignal1) || !MotionManager.GetDigitalInput(InputName.RobotArmSignal2))
                            {
                                if (LinkSignalFlow.Instance.IsInInterfaceFlow)
                                {
                                    MessageBox.Show("上游Robot安全信号Off，禁止T轴旋转");
                                    result = false;
                                    break;
                                }
                            }
                            if (axis == AxisName.Stage1_T && MotionManager.GetAxisActPos(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Y) > MotionPara.AutoChgAxisYSafePos2)
                            {
                                MessageBox.Show("自动更换Y轴不在安全位置，禁止平台1T轴旋转");
                                result = false;
                                break;
                            }
                            if (axis == AxisName.Stage1_T && IsClyDown(0) == false)
                            {
                                MessageBox.Show($"平台1纵向规整气缸不在原点位置，禁止旋转平台1T轴！");
                                result = false;
                                break;
                            }
                            if (axis == AxisName.Stage2_T && IsClyDown(1) == false)
                            {
                                MessageBox.Show($"平台2纵向规整气缸不在原点位置，禁止旋转平台2T轴！");
                                result = false;
                                break;
                            }
                            break;
                        }
                    case AxisName.Stage1_Y:
                    case AxisName.Stage2_Y:
                        {
                            if (!IsContactZAtSafePos())
                            {
                                MessageBox.Show($"ProbeCard压头在下压位置，禁止移动{axis}轴");
                                result = false;
                                break;
                            }
                            var posT1 = MotionManager.GetAxisActPos(StationEnum.DualStage, AxisName.Stage1_T);
                            if ((Math.Abs(posT1) > 10 && Math.Abs(posT1) < 170) || Math.Abs(posT1) > 190)
                            {
                                MessageBox.Show($"平台1T轴不在0度或者180度，禁止移动{axis}轴！");
                                result = false;
                                break;
                            }
                            var posT2 = MotionManager.GetAxisActPos(StationEnum.DualStage, AxisName.Stage2_T);
                            if ((Math.Abs(posT2) > 10 && Math.Abs(posT2) < 170) || Math.Abs(posT2) > 190)
                            {
                                MessageBox.Show($"平台2T轴不在0度或者180度，禁止移动{axis}轴！");
                                result = false;
                                break;
                            }

                            if (axis == AxisName.Stage1_Y)
                            {
                                if (e.Dir == 1 && e.TargetPos + MotionManager.GetAxisActPos(StationEnum.DualStage, AxisName.Stage2_Y) > MotionPara.Stage1YStage2YPreventCrushPos)
                                {
                                    MessageBox.Show($"平台1Y轴目标位置会和平台2相撞，禁止移动平台1Y轴！");
                                    result = false;
                                    break;
                                }
                                if (e.Dir == -1 && MotionManager.GetAxisActPos(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Y) > MotionPara.AutoChgAxisYSafePos2)
                                {
                                    MessageBox.Show($"自动更换Y轴不在安全位置，禁止移动平台1Y轴！");
                                    result = false;
                                    break;
                                }
                                if (IsClyDown(0) == false)
                                {
                                    MessageBox.Show($"平台1纵向规整气缸不在原点位置，禁止移动平台1Y轴！");
                                    result = false;
                                    break;
                                }
                                if (!IsStageAxisZDown(0))
                                {
                                    MessageBox.Show($"平台1Z轴不在安全位置，禁止移动{axis}轴！");
                                    result = false;
                                    break;
                                }
                            }
                            if (axis == AxisName.Stage2_Y)
                            {
                                if (e.Dir == 1 && e.TargetPos + MotionManager.GetAxisActPos(StationEnum.DualStage, AxisName.Stage1_Y) > MotionPara.Stage1YStage2YPreventCrushPos)
                                {
                                    MessageBox.Show($"平台2Y轴目标位置会和平台1相撞，禁止移动平台2Y轴！");
                                    result = false;
                                    break;
                                }
                                if (IsClyDown(1) == false)
                                {
                                    MessageBox.Show($"平台2纵向规整气缸不在原点位置，禁止移动平台2Y轴！");
                                    result = false;
                                    break;
                                }
                                if (!IsStageAxisZDown(1))
                                {
                                    MessageBox.Show($"平台2Z轴不在安全位置，禁止移动{axis}轴！");
                                    result = false;
                                    break;
                                }
                            }
                            break;
                        }
                    case AxisName.Stage1_Z:
                        {
                            if (IsDetectGlass(0) && (MotionManager.GetDigitalInput(InputName.Stage1VacDetect) || MotionManager.GetDigitalOutput(OutputName.Stage1Vacuum)))
                            {
                                MessageBox.Show($"平台1检测到玻璃，请先关闭真空");
                                result = false;
                            }
                            break;
                        }
                    case AxisName.Stage2_Z:
                        {
                            if (IsDetectGlass(1) && (MotionManager.GetDigitalInput(InputName.Stage2VacDetect) || MotionManager.GetDigitalOutput(OutputName.Stage2Vacuum)))
                            {
                                MessageBox.Show($"平台2检测到玻璃，请先关闭真空");
                                result = false;
                            }
                            var ret = MotionManager.IsArrive(StationEnum.DetectGantry,AxisName.DetectModule1_X,DetectGtyPosName.SafePos);
                            if (ret==false)
                            {
                                MessageBox.Show($"1号检测模组X轴不在起始位置，禁止移动{axis}轴");
                                result = false;
                            }
                            break;
                        }
                    case AxisName.UvwStage_X1:
                    case AxisName.UvwStage_X2:
                    case AxisName.UvwStage_Y:
                        {
                            if (!IsContactZAtSafePos())
                            {
                                MessageBox.Show($"ProbeCard压头在下压位置，禁止移动{axis}轴");
                                result = false;
                                break;
                            }
                            if (axis == AxisName.UvwStage_X1 || axis == AxisName.UvwStage_X2 || axis == AxisName.UvwStage_Y)
                            {
                                var posInfo = AxisInfoHelper.GetPosInfoByName(AxisName.PrbAutoChg_Z, "SafePos");
                                if (MotionManager.GetAxisActPos(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Z) > posInfo.Pos + 2)
                                {
                                    MessageBox.Show($"Probe自动更换模组Z轴高于安全位置，禁止移动{axis}轴！");
                                    result = false;
                                    break;
                                }
                            }
                            if (MotionManager.GetDigitalOutput(OutputName.UvwBrake1) || MotionManager.GetDigitalOutput(OutputName.UvwBrake2))
                            {
                                MessageBox.Show($"Uvw刹车未松开，禁止移动{axis}轴！");
                                result = false;
                            }
                            break;
                        }
                    case AxisName.CabGantry_Z1:
                        {
                            if (MotionManager.IsHomeOK(StationEnum.Cabinet, AxisName.CabGantry_Z1) == false)
                            {
                                MessageBox.Show("料仓龙门没有回零，不能移动料仓龙门轴");
                                result = false;
                                break;
                            }
                            if (!MotionManager.GetDigitalInput(InputName.DetectAutoChgY1) || !MotionManager.GetDigitalInput(InputName.DetectAutoChgY2) || MotionManager.GetAxisActPos(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Y) < MotionPara.AutoChgAxisYSafePos)
                            {
                                MessageBox.Show("Probe自动更换Y轴不在安全位置，禁止移动料仓！");
                                result = false;
                                break;
                            }
                            if (MotionManager.GetAxisActPos(StationEnum.Cabinet, AxisName.PrbLoader_Y1) > MotionPara.ProbeLoaderAxisYSafePos)
                            {
                                MessageBox.Show("Probe上料平台Y轴不在安全位置，禁止移动料仓！");
                                result = false;
                            }
                            break;
                        }
                    case AxisName.PrbAutoChg_Y:
                        {
                            var posT1 = MotionManager.GetAxisActPos(StationEnum.DualStage, AxisName.Stage1_T);
                            if ((Math.Abs(posT1) > 5 && Math.Abs(posT1) < 175) || Math.Abs(posT1) > 185)
                            {
                                MessageBox.Show($"平台1T轴不在0度或者180度，禁止移动{axis}轴！");
                                result = false;
                                break;
                            }
                            var posInfo = AxisInfoHelper.GetPosInfoByName(AxisName.PrbAutoChg_Z, "SafePos");
                            if (MotionManager.GetAxisActPos(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Z) > posInfo.Pos + 2)
                            {
                                MessageBox.Show($"Probe自动更换Z轴高于安全位置，禁止移动{axis}轴！");
                                result = false;
                                break;
                            }
                            if (!IsContactZAtSafePos())
                            {
                                MessageBox.Show($"ProbeCard压头不在安全位置，禁止移动{axis}轴");
                                result = false;
                                break;
                            }
                            break;
                        }
                    case AxisName.PrbAutoChg_Z:
                        {
                            if (Probe.IsAtAutoChangePos() == false && e.Dir == 1)
                            {
                                MessageBox.Show($"Uvw不在自动更换Probe位置，禁止上升{axis}轴！");
                                result = false;
                            }
                            bool temp = MotionManager.GetDigitalOutput(OutputName.UvwZeroSysUnLock);
                            if (temp == false && e.Dir == 1 && (MotionManager.GetDigitalInput(InputName.PrbAutoChgDetLeft) || MotionManager.GetDigitalInput(InputName.PrbAutoChgDetRight)))
                            {
                                MessageBox.Show($"Uvw零点系统没有打开并且自动更换模组上有Probe，禁止上升{axis}轴！");
                                result = false;
                            }

                            break;
                        }
                    case AxisName.ProbeGantry_Y1:
                    case AxisName.DetectGantry_Y1:
                        {
                            if (axis == AxisName.ProbeGantry_Y1)
                            {
                                if (MotionManager.IsHomeOK(StationEnum.ProbeGantry, axis) == false)
                                {
                                    MessageBox.Show($"龙门没有回零，禁止移动{axis}轴");
                                    result = false;
                                    break;
                                }
                                if (e.Dir == 1 && (e.TargetPos + MotionManager.GetAxisActPos(StationEnum.DetectGantry, AxisName.DetectGantry_Y1) >= MotionPara.GantryPreventCrushPos))
                                {
                                    MessageBox.Show($"Probe龙门目标位置会与检测龙门相撞，禁止移动Probe龙门轴");
                                    result = false;
                                    break;
                                }
                                if (e.Dir == 1 && (e.TargetPos + MotionManager.GetAxisActPos(StationEnum.DetectGantry, AxisName.DetectGantry_Y1) >= MotionPara.GantryPreventCrushPos2) && IsAlignAndIRCamDanger2())
                                {
                                    MessageBox.Show($"IR相机和对位相机位置重叠，Probe龙门目标位置会导致IR相机和对位相机相撞，禁止移动Probe龙门轴");
                                    result = false;
                                    break;
                                }
                                if (!IsContactZAtSafePos())
                                {
                                    MessageBox.Show($"ProbeCard压头不在安全位置，禁止移动Probe龙门轴");
                                    result = false;
                                    break;
                                }
                                var posInfo = AxisInfoHelper.GetPosInfoByName(AxisName.PrbAutoChg_Z, "SafePos");
                                if (MotionManager.GetAxisActPos(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Z) > posInfo.Pos + 2)
                                {
                                    MessageBox.Show($"Probe自动更换模组Z轴高于安全位置，禁止移动Probe龙门轴！");
                                    result = false;
                                    break;
                                }
                                if (IsClyDown(0) == false && (ControlPara.InterLockShield == false))
                                {
                                    MessageBox.Show($"平台1纵向规整气缸不在原点位置，禁止移动Probe龙门轴！");
                                    result = false;
                                    break;
                                }
                                if (IsClyDown(1) == false && (ControlPara.InterLockShield == false))
                                {
                                    MessageBox.Show($"平台2纵向规整气缸不在原点位置，禁止移动Probe龙门轴！");
                                    result = false;
                                    break;
                                }
                                if (!IsStageAxisZDown(0) && (ControlPara.InterLockShield == false))
                                {
                                    MessageBox.Show($"平台1Z轴不在安全位置，禁止移动{axis}轴！");
                                    result = false;
                                    break;
                                }
                                if (!IsStageAxisZDown(1) && (ControlPara.InterLockShield == false))
                                {
                                    MessageBox.Show($"平台2Z轴不在安全位置，禁止移动{axis}轴！");
                                    result = false;
                                    break;
                                }
                                if (Probe.IsDetectModuleZSafe() == false&& e.Dir == 1)
                                {
                                    MessageBox.Show($"检测模组Z轴不在安全位置，禁止移动{axis}轴！");
                                    result = false;
                                    break;
                                }
                            }
                            if (axis == AxisName.DetectGantry_Y1)
                            {
                                if (MotionManager.IsHomeOK(StationEnum.DetectGantry, axis) == false)
                                {
                                    MessageBox.Show($"龙门没有回零，禁止移动{axis}轴");
                                    result = false;
                                    break;
                                }
                                if (e.Dir == 1 && (e.TargetPos + MotionManager.GetAxisActPos(StationEnum.ProbeGantry, AxisName.ProbeGantry_Y1) >= MotionPara.GantryPreventCrushPos))
                                {
                                    MessageBox.Show($"检测龙门目标位置会与Probe龙门相撞，禁止移动检测龙门轴");
                                    result = false;
                                    break;
                                }
                                if (e.Dir == 1 && (e.TargetPos + MotionManager.GetAxisActPos(StationEnum.ProbeGantry, AxisName.ProbeGantry_Y1) >= MotionPara.GantryPreventCrushPos2) && IsAlignAndIRCamDanger2())
                                {
                                    MessageBox.Show($"IR相机和对位相机位置重叠，检测龙门目标位置会导致IR相机和对位相机相撞，禁止移动检测龙门轴");
                                    result = false;
                                    break;
                                }
                                if (e.Dir==1&& (MotionManager.IsArrive(StationEnum.ProbeGantry,AxisName.ProbeGantryAlig_X1,ProbeGantryPosName.SafePos)==false|| MotionManager.IsArrive(StationEnum.ProbeGantry, AxisName.ProbeGantryAlig_X2, ProbeGantryPosName.SafePos) == false))
                                {
                                    MessageBox.Show($"对位相机X轴不在安全位置，禁止移动检测龙门轴");
                                    result = false;
                                    break;
                                }
                            }

                            if (IsGantryDanger() && e.Dir == 1)
                            {
                                MessageBox.Show($"Probe龙门和检测龙门距离太近，禁止移动{axis}轴！");
                                result = false;
                                break;
                            }
                            if (!IsStageAxisZDown(0) && (ControlPara.InterLockShield == false))
                            {
                                MessageBox.Show($"平台1Z轴不在安全位置，禁止移动{axis}轴！");
                                result = false;
                                break;
                            }
                            if (!IsStageAxisZDown(1) && (ControlPara.InterLockShield == false))
                            {
                                MessageBox.Show($"平台2Z轴不在安全位置，禁止移动{axis}轴！");
                                result = false;
                                break;
                            }
                            break;
                        }
                    case AxisName.DetectModule1_X:
                    case AxisName.DetectModule2_X:
                        {
                            var posZ1 = MotionManager.GetAxisActPos(StationEnum.DualStage, AxisName.Stage1_Z);
                            var posZ2 = MotionManager.GetAxisActPos(StationEnum.DualStage, AxisName.Stage2_Z);
                            if (Math.Abs(posZ1) > 5)
                            {
                                MessageBox.Show($"平台1Z轴不在安全位置，禁止移动{axis}轴！");
                                result = false;
                                break;
                            }
                            if (Math.Abs(posZ2) > 5)
                            {
                                MessageBox.Show($"平台2Z轴不在安全位置，禁止移动{axis}轴！");
                                result = false;
                                break;
                            }
                            if (axis == AxisName.DetectModule1_X)
                            {
                                if (e.Dir == 1 && e.TargetPos + MotionManager.GetAxisActPos(StationEnum.DetectGantry, AxisName.DetectModule2_X) > MotionPara.DetectModuleXPreventCrushPos)
                                {
                                    MessageBox.Show($"检测模组1X轴目标位置会和检测模组2X相撞，禁止移动检测模组1X轴！");
                                    result = false;
                                    break;
                                }
                                if (IsGantryDanger())
                                {
                                    if (MotionManager.IsArrive(StationEnum.DetectGantry, AxisName.DetectModule1_Z1, "SafePos") == false || MotionManager.IsArrive(StationEnum.DetectGantry, AxisName.DetectModule1_Z2, "SafePos") == false)
                                    {
                                        MessageBox.Show($"检测模组1Z轴不在安全位置，并且检测龙门和压接龙门靠的太近，禁止{axis}轴，否则会和1号对位相机相撞");
                                        result = false;
                                    }
                              
                                }
                            }
                            if (axis == AxisName.DetectModule2_X)
                            {
                                if (e.Dir == 1 && e.TargetPos + MotionManager.GetAxisActPos(StationEnum.DetectGantry, AxisName.DetectModule1_X) > MotionPara.DetectModuleXPreventCrushPos)
                                {
                                    MessageBox.Show($"检测模组2X轴目标位置会和检测模组1X相撞，禁止移动检测模组2X轴！");
                                    result = false;
                                    break;
                                }
                                if (IsGantryDanger())
                                {
                                    if (MotionManager.IsArrive(StationEnum.DetectGantry, AxisName.DetectModule2_Z1, "SafePos") == false || MotionManager.IsArrive(StationEnum.DetectGantry, AxisName.DetectModule2_Z2, "SafePos") == false)
                                    {
                                        MessageBox.Show($"检测模组2Z轴不在安全位置，并且检测龙门和压接龙门靠的太近，禁止{axis}轴，否则会和2号对位相机相撞");
                                        result = false;
                                    }

                                }
                            }
                            break;
                        }
                    case AxisName.PrbLoader_Y1:
                    case AxisName.PrbLoader_Y2:
                        {
                            if (MotionManager.GetDigitalInput(InputName.PrbLoaderDetectPrb1) || MotionManager.GetDigitalInput(InputName.PrbLoaderDetectPrb2))
                            {
                                MessageBox.Show("Probe上料臂上检测到Probe，不能单独移动Probe上料臂，请选择PrbLoaderUnion插补移动");
                                result = false;
                            }
                            var ret = MotionManager.IsArrive(StationEnum.Cabinet, AxisName.PrbLoader_Z, CabinetPosName.LoadZBeforeToCabPos);
                            if (ret == false && e.Dir == 1)
                            {
                                MessageBox.Show($"Probe上料平台Z轴不在进入料仓位置，禁止{axis}轴");
                                result = false;
                            }
                            break;
                        }
                    case AxisName.ProbeGantryAlig_X1:
                        {
                            if (IsGantryDanger())
                            {
                                if (MotionManager.IsArrive(StationEnum.DetectGantry,AxisName.DetectModule1_Z1,"SafePos")==false|| MotionManager.IsArrive(StationEnum.DetectGantry, AxisName.DetectModule1_Z2, "SafePos") == false)
                                {
                                    MessageBox.Show($"检测模组1Z轴不在安全位置，并且检测龙门和压接龙门靠的太近，禁止{axis}轴");
                                    result = false;
                                }
                                if (Math.Abs(e.TargetPos - MotionManager.GetAxisActPos(StationEnum.DetectGantry, AxisName.DetectModule1_X)) <= MotionPara.AlignCam1AndIRCamPreventCrushPos)
                                {
                                    MessageBox.Show($"Probe龙门和检测龙门距离太近，IR相机目标位置会与对位相机相撞，禁止移动{axis}轴");
                                    result = false;
                                    break;
                                }
                            }
                            break;
                        }
                    case AxisName.ProbeGantryAlig_X2:
                        {
                            if (IsGantryDanger())
                            {
                                if (MotionManager.IsArrive(StationEnum.DetectGantry, AxisName.DetectModule2_Z1, "SafePos") == false || MotionManager.IsArrive(StationEnum.DetectGantry, AxisName.DetectModule2_Z2, "SafePos") == false)
                                {
                                    MessageBox.Show($"检测模组2Z轴不在安全位置，并且检测龙门和压接龙门靠的太近，禁止{axis}轴");
                                    result = false;
                                }
                                if (e.TargetPos + MotionManager.GetAxisActPos(StationEnum.DetectGantry, AxisName.DetectModule1_X) >= MotionPara.AlignCam2AndIRCamPreventCrushPos)
                                {
                                    MessageBox.Show($"Probe龙门和检测龙门距离太近，IR相机目标位置会与对位相机相撞，禁止移动{axis}轴");
                                    result = false;
                                    break;
                                }
                            }
                            break;
                        }
                
                    default:
                        {
                          
                            break;
                        }
                }
            }
            return result;
        }
        public bool IsContactZAtSafePos()
        {
            bool ret = true;
            if (!Probe.IsContactZAtSafePos())
            {
                ret = false;
            }
            return ret;
        }
        public bool IsAlignAndIRCamDanger()
        {
            AxisInfo ProbeGantry = AxisInfoHelper.GetAxisInfoByName(AxisName.ProbeGantry_Y1.ToString());
            AxisInfo DetectGantry = AxisInfoHelper.GetAxisInfoByName(AxisName.DetectGantry_Y1.ToString());
            AxisInfo alignCamX1 = AxisInfoHelper.GetAxisInfoByName(AxisName.ProbeGantryAlig_X1.ToString());
            AxisInfo alignCamX2 = AxisInfoHelper.GetAxisInfoByName(AxisName.ProbeGantryAlig_X2.ToString());
            AxisInfo irCamX = AxisInfoHelper.GetAxisInfoByName(AxisName.DetectModule1_X.ToString());
            bool flag = ProbeGantry.ActPos + DetectGantry.ActPos >= MotionPara.GantryPreventCrushPos2;
            bool flag2 = Math.Abs(alignCamX1.ActPos - irCamX.ActPos) <= MotionPara.AlignCam1AndIRCamPreventCrushPos;
            bool flag3 = alignCamX2.ActPos + irCamX.ActPos >= MotionPara.AlignCam2AndIRCamPreventCrushPos;
            return flag && (flag2 || flag3);
        }
        public bool IsAlignAndIRCamDanger2()
        {
            AxisInfo alignCamX1 = AxisInfoHelper.GetAxisInfoByName(AxisName.ProbeGantryAlig_X1.ToString());
            AxisInfo alignCamX2 = AxisInfoHelper.GetAxisInfoByName(AxisName.ProbeGantryAlig_X2.ToString());
            AxisInfo irCamX = AxisInfoHelper.GetAxisInfoByName(AxisName.DetectModule1_X.ToString());
            bool flag2 = Math.Abs(alignCamX1.ActPos - irCamX.ActPos) <= MotionPara.AlignCam1AndIRCamPreventCrushPos;
            bool flag3 = alignCamX2.ActPos + irCamX.ActPos >= MotionPara.AlignCam2AndIRCamPreventCrushPos;
            return flag2 || flag3;
        }
        public bool IsGantryDanger(bool bit = true)
        {
            AxisInfo ProbeGantry = AxisInfoHelper.GetAxisInfoByName(AxisName.ProbeGantry_Y1.ToString());
            AxisInfo DetectGantry = AxisInfoHelper.GetAxisInfoByName(AxisName.DetectGantry_Y1.ToString());
            bool flag = ProbeGantry.ActPos + DetectGantry.ActPos >= (bit ? MotionPara.GantryPreventCrushPos : MotionPara.GantryPreventCrushPos2);
            return flag;
        }
        private bool CheckIsCanHome(MoveArgs arg)
        {
            bool ret = true;
            AxisName axis = HYC.HTPCBase.Extension.Entities.EnumHelper.GetEnumValue<AxisName>(arg.Axis.AxisName);
            if (axis == AxisName.PrbLoader_Y1 && (MotionManager.GetDigitalInput(InputName.PrbLoaderDetectPrb1) || MotionManager.GetDigitalInput(InputName.PrbLoaderDetectPrb2)))
            {
                MessageBox.Show("Probe上料臂上检测到Probe，不能单独回零Probe上料臂");
                ret= false;
            }
            if (axis == AxisName.PrbLoader_Y2 && MotionManager.GetDigitalInput(InputName.PrbLoaderDetectPrb1) || MotionManager.GetDigitalInput(InputName.PrbLoaderDetectPrb2))
            {
                MessageBox.Show("Probe上料臂上检测到Probe，不能单独回零Probe上料臂");
                ret= false;
            }
            if (axis == AxisName.PrbAutoChg_Y)
            {
                if (MotionManager.GetDigitalInput(InputName.PrbAutoChgDetLeft) || MotionManager.GetDigitalInput(InputName.PrbAutoChgDetRight))
                {
                    MessageBox.Show("自动更换手臂上检测到Probe，禁止回零自动更换手臂Y轴");
                    ret= false;
                }
                if (AxisInfoHelper.GetAxisInfoByName(AxisName.CabGantry_Z1).IsHomeOK==false)
                {
                    MessageBox.Show("料仓龙门原点丢失，禁止回零自动更换手臂Y轴");
                    ret = false;
                }
                if (MotionManager.IsArrive(StationEnum.Cabinet,AxisName.CabGantry_Z1,CabinetPosName.HomeSafePos)==false)
                {
                    MessageBox.Show("料仓龙门没有在回零等待位置，禁止回零自动更换手臂Y轴");
                    ret = false;
                }
            }
            return ret;
        }
        private void MonitorAxisSafePos()
        {
            //平台Y轴Interlock
            Thread t1 = new Thread(() =>
            {
                while (true)
                {
                    try
                    {
                        if (ControlPara.InterLockShield == false)
                        {
                            if (MotionManager.IsMove(StationEnum.DualStage, AxisName.Stage1_Y))
                            {
                                ProbeHeadInterlock(AxisName.Stage1_Y,0);
                            }
                            if (MotionManager.IsMove(StationEnum.DualStage, AxisName.Stage2_Y))
                            {
                                ProbeHeadInterlock(AxisName.Stage2_Y,1);
                            }
                        }
                        else
                        {
                            Thread.Sleep(2000);
                        }
                    }
                    catch (Exception ex)
                    {
                        OnShowMsg?.Invoke($"Interlock线程1执行异常，异常信息：{ex.ToString()}", Brushes.Red);
                        StationManager.Instance.Stop();
                        Thread.Sleep(2000);
                    }
                    Thread.Sleep(5);
                }
            });
            t1.IsBackground = true;
            t1.Priority = ThreadPriority.Highest;
            t1.Start();

            //Probe龙门轴Interlock
            Thread t2 = new Thread(() =>
            {
                while (true)
                {
                    try
                    {
                        if (ControlPara.InterLockShield == false)
                        {
                            if (Filter(StationEnum.ProbeGantry, AxisName.ProbeGantry_Y1))
                            {
                                ProbeHeadInterlock(AxisName.ProbeGantry_Y1);
                            }
                        }
                        else
                        {
                            Thread.Sleep(2000);
                        }
                    }
                    catch (Exception ex)
                    {
                        OnShowMsg?.Invoke($"Interlock线程2执行异常，异常信息：{ex.ToString()}", Brushes.Red);
                        StationManager.Instance.Stop();
                        Thread.Sleep(2000);
                    }
                    Thread.Sleep(5);
                }
            });
            t2.IsBackground = true;
            t2.Start();

            //平台T轴Interlock
            Thread t8 = new Thread(() =>
            {
                while (true)
                {
                    try
                    {
                        if (ControlPara.InterLockShield == false)
                        {
                            if (MotionManager.IsMove(StationEnum.DualStage, AxisName.Stage1_T))
                            {
                                ProbeHeadInterlock(AxisName.Stage1_T,0);
                            }
                            if (MotionManager.IsMove(StationEnum.DualStage, AxisName.Stage2_T))
                            {
                                ProbeHeadInterlock(AxisName.Stage2_T,1);
                            }
                        }
                        else
                        {
                            Thread.Sleep(2000);
                        }
                    }
                    catch (Exception ex)
                    {
                        OnShowMsg?.Invoke($"Interlock线程8执行异常，异常信息：{ex.ToString()}", Brushes.Red);
                        StationManager.Instance.Stop();
                        Thread.Sleep(2000);
                    }
                    Thread.Sleep(5);
                }
            });
            t8.IsBackground = true;
            t8.Start();

            //对位相机和平台Interlock
            Thread t3 = new Thread(() =>
            {
                while (true)
                {
                    try
                    {
                        if (ControlPara.InterLockShield == false)
                        {
                            StageAxisYPosErr = Math.Abs(MotionManager.GetAxisActPos(StationEnum.DualStage, AxisName.Stage1_Y) + MotionManager.GetAxisActPos(StationEnum.DualStage, AxisName.Stage2_Y)) > MotionPara.Stage1YStage2YPreventCrushPos;
                            AlignCamAxisXPosErr = Math.Abs(MotionManager.GetAxisActPos(StationEnum.ProbeGantry, AxisName.ProbeGantryAlig_X1) + MotionManager.GetAxisActPos(StationEnum.ProbeGantry, AxisName.ProbeGantryAlig_X2)) > MotionPara.AlignCamXPreventCrushPos;
                            //防止两个对位相机相撞
                            if (AlignCamAxisXPosErr && (MotionManager.IsMove(StationEnum.ProbeGantry, AxisName.ProbeGantryAlig_X1) || MotionManager.IsMove(StationEnum.ProbeGantry, AxisName.ProbeGantryAlig_X2)))
                            {
                                if (!StationManager.Instance.IsInHoming())
                                {
                                    MotionManager.Stop(StationEnum.ProbeGantry, AxisName.ProbeGantryAlig_X1);
                                    MotionManager.Stop(StationEnum.ProbeGantry, AxisName.ProbeGantryAlig_X2);
                                    AlarmManager.CreateAlarm((int)PosErrorCode.AlignCamPosError);
                                }
                            }
                            //防止两个平台相撞
                            if (StageAxisYPosErr && (MotionManager.IsMove(StationEnum.DualStage, AxisName.Stage1_Y) || MotionManager.IsMove(StationEnum.DualStage, AxisName.Stage2_Y)))
                            {
                                if (!StationManager.Instance.IsInHoming())
                                {
                                    MotionManager.Stop(StationEnum.DualStage, AxisName.Stage1_Y);
                                    MotionManager.Stop(StationEnum.DualStage, AxisName.Stage2_Y);
                                    AlarmManager.CreateAlarm((int)PosErrorCode.StgPosError);
                                }
                            }
                            //当平台不在0度或者180度时，禁止平台Y轴移动
                            var posT1 = MotionManager.GetAxisActPos(StationEnum.DualStage, AxisName.Stage1_T);
                            var posT2 = MotionManager.GetAxisActPos(StationEnum.DualStage, AxisName.Stage2_T);
                            bool axis1TFlag = (Math.Abs(posT1) > 5 && Math.Abs(posT1) < 175) || Math.Abs(posT1) > 185;
                            bool axis2TFlag = (Math.Abs(posT2) > 5 && Math.Abs(posT2) < 175) || Math.Abs(posT2) > 185;
                            if (axis1TFlag && (MotionManager.IsMove(StationEnum.DualStage, AxisName.Stage1_Y) || MotionManager.IsMove(StationEnum.DualStage, AxisName.Stage2_Y)))
                            {
                                MotionManager.Stop(StationEnum.DualStage, AxisName.Stage1_Y);
                                MotionManager.Stop(StationEnum.DualStage, AxisName.Stage2_Y);
                                AlarmManager.CreateAlarm((int)PosErrorCode.Stage1NotAtHorizontalPos);
                            }

                            if (axis2TFlag && (MotionManager.IsMove(StationEnum.DualStage, AxisName.Stage1_Y) || MotionManager.IsMove(StationEnum.DualStage, AxisName.Stage2_Y)))
                            {
                                MotionManager.Stop(StationEnum.DualStage, AxisName.Stage1_Y);
                                MotionManager.Stop(StationEnum.DualStage, AxisName.Stage2_Y);
                                AlarmManager.CreateAlarm((int)PosErrorCode.Stage2NotAtHorizontalPos);
                            }
                            if (axis1TFlag && MotionManager.IsMove(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Y) & !StationManager.Instance.IsInHoming())
                            {
                                AlarmManager.CreateAlarm((int)PosErrorCode.Stage1NotAtHorizontalPos);
                                MotionManager.Stop(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Y);
                            }

                        }
                        else
                        {
                            Thread.Sleep(2000);
                        }
                    }
                    catch (Exception ex)
                    {
                        OnShowMsg?.Invoke($"Interlock线程3执行异常，异常信息：{ex.ToString()}", Brushes.Red);
                        StationManager.Instance.Stop();
                        Thread.Sleep(2000);
                    }
                    Thread.Sleep(5);
                }

            });
            t3.IsBackground = true;
            t3.Start();

            //对位相机1和红外相机Interlock
            Thread t4 = new Thread(() =>
            {
                while (true)
                {
                    try
                    {
                        if (ControlPara.InterLockShield == false)
                        {
                            GantryPosErr2 = Math.Abs(MotionManager.GetAxisActPos(StationEnum.ProbeGantry, AxisName.ProbeGantry_Y1) + MotionManager.GetAxisActPos(StationEnum.DetectGantry, AxisName.DetectGantry_Y1)) > MotionPara.GantryPreventCrushPos2;
                            //防止对位相机1和红外相机相撞
                            if (GantryPosErr2 && (Math.Abs(MotionManager.GetAxisActPos(StationEnum.ProbeGantry, AxisName.ProbeGantryAlig_X1) - MotionManager.GetAxisActPos(StationEnum.DetectGantry, AxisName.DetectModule1_X)) < MotionPara.AlignCam1AndIRCamPreventCrushPos))
                            {
                                if (IsTriggered[0] == false)
                                {
                                    MotionManager.Stop(StationEnum.ProbeGantry, AxisName.ProbeGantryAlig_X1);
                                    MotionManager.Stop(StationEnum.DetectGantry, AxisName.DetectModule1_X);
                                    AlarmManager.CreateAlarm((int)PosErrorCode.Align1AndIRCamPosErr);
                                    IsTriggered[0] = true;
                                }
                            }
                            else
                            {
                                if (IsTriggered[0])
                                {
                                    IsTriggered[0] = false;
                                }
                            }
                            //防止对位相机2和红外相机相撞
                            if (GantryPosErr2 && ((MotionManager.GetAxisActPos(StationEnum.ProbeGantry, AxisName.ProbeGantryAlig_X2) + MotionManager.GetAxisActPos(StationEnum.DetectGantry, AxisName.DetectModule1_X)) > MotionPara.AlignCam2AndIRCamPreventCrushPos))
                            {
                                if (IsTriggered[1] == false)
                                {
                                    MotionManager.Stop(StationEnum.ProbeGantry, AxisName.ProbeGantryAlig_X2);
                                    MotionManager.Stop(StationEnum.DetectGantry, AxisName.DetectModule1_X);
                                    AlarmManager.CreateAlarm((int)PosErrorCode.Align2AndIRCamPosErr);
                                    IsTriggered[1] = true;
                                }
                            }
                            else
                            {
                                if (IsTriggered[1])
                                {
                                    IsTriggered[1] = false;
                                }
                            }
                        }
                        else
                        {
                            Thread.Sleep(2000);
                        }
                    }
                    catch (Exception ex)
                    {
                        OnShowMsg?.Invoke($"Interlock线程4执行异常，异常信息：{ex.ToString()}", Brushes.Red);
                        StationManager.Instance.Stop();
                        Thread.Sleep(2000);
                    }
                    Thread.Sleep(5);
                }

            });
            t4.IsBackground = true;
            t4.Start();

            //检测龙门和Probe龙门防止相撞Interlock
            Thread t5 = new Thread(() =>
            {
                while (true)
                {
                    try
                    {
                        if (ControlPara.InterLockShield == false)
                        {
                            GantryPosErr = Math.Abs(MotionManager.GetAxisActPos(StationEnum.ProbeGantry, AxisName.ProbeGantry_Y1) + MotionManager.GetAxisActPos(StationEnum.DetectGantry, AxisName.DetectGantry_Y1)) > MotionPara.GantryPreventCrushPos;
                            //防止两个龙门相撞
                            if (GantryPosErr)
                            {
                                if (IsTriggered[2] == false)
                                {
                                    MotionManager.Stop(StationEnum.ProbeGantry, AxisName.ProbeGantry_Y1);
                                    MotionManager.Stop(StationEnum.DetectGantry, AxisName.DetectGantry_Y1);
                                    AlarmManager.CreateAlarm((int)PosErrorCode.ProbeGantryAndIRGtyPosError);
                                    IsTriggered[2] = true;
                                }
                            }
                            else
                            {
                                if (IsTriggered[2])
                                {
                                    IsTriggered[2] = false;
                                }
                            }
                        
                        }
                        else
                        {
                            Thread.Sleep(2000);
                        }
                    }
                    catch (Exception ex)
                    {
                        OnShowMsg?.Invoke($"Interlock线程5执行异常，异常信息：{ex.ToString()}", Brushes.Red);
                        StationManager.Instance.Stop();
                        Thread.Sleep(2000);
                    }
                    Thread.Sleep(5);
                }
            });
            t5.IsBackground = true;
            t5.Start();

            //料仓Interlock
            Thread t6 = new Thread(() =>
            {
                while (true)
                {
                    try
                    {
                        if (ControlPara.InterLockShield == false)
                        {
                            //防止料仓和probe上料平台Y轴相撞
                            if ((MotionManager.GetAxisActPos(StationEnum.Cabinet, AxisName.PrbLoader_Y1) > MotionPara.ProbeLoaderAxisYSafePos) && MotionManager.IsMove(StationEnum.Cabinet, AxisName.CabGantry_Z1))
                            {
                                MotionManager.Stop(StationEnum.Cabinet, AxisName.CabGantry_Z1);
                                AlarmManager.CreateAlarm((int)PosErrorCode.CabinetStopCuzLoaderYNotAtSafePos);
                            }
                            //防止料仓和自动更换Y轴相撞
                            if ((!MotionManager.GetDigitalInput(InputName.DetectAutoChgY1) || !MotionManager.GetDigitalInput(InputName.DetectAutoChgY2)) && MotionManager.IsMove(StationEnum.Cabinet, AxisName.CabGantry_Z1))
                            {
                                MotionManager.Stop(StationEnum.Cabinet, AxisName.CabGantry_Z1);
                                AlarmManager.CreateAlarm((int)PosErrorCode.CabinetStopCuzAutoChgYNotAtSafePos);
                            }
                            //防止Probe龙门和自动更换Z轴相撞
                            if (MotionManager.GetAxisActPos(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Z) > 5 && Filter(StationEnum.ProbeGantry, AxisName.ProbeGantry_Y1))
                            {
                                MotionManager.Stop(StationEnum.ProbeGantry, AxisName.ProbeGantry_Y1);
                                AlarmManager.CreateAlarm((int)PosErrorCode.ProbeGantryStopCuzPrbAutoChangeZNotAtSafePos);
                            }
                            //防止平台1和自动更换Y轴相撞
                            if (MotionManager.GetAxisActPos(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Y) > MotionPara.AutoChgAxisYSafePos2 && MotionManager.IsMove(StationEnum.DualStage, AxisName.Stage1_T))
                            {
                                MotionManager.Stop(StationEnum.DualStage, AxisName.Stage1_T);
                                AlarmManager.CreateAlarm((int)PosErrorCode.AutoChgYAtDangerPos);
                            }
                        }
                        else
                        {
                            Thread.Sleep(2000);
                        }
                    }
                    catch (Exception ex)
                    {
                        OnShowMsg?.Invoke($"Interlock线程6执行异常，异常信息：{ex.ToString()}", Brushes.Red);
                        StationManager.Instance.Stop();
                        Thread.Sleep(2000);
                    }
                    Thread.Sleep(5);
                }

            });
            t6.IsBackground = true;
            t6.Start();

            //Probe上料平台Y轴Interlock
            Thread t7 = new Thread(() =>
            {
                while (true)
                {
                    try
                    {
                        if (ControlPara.InterLockShield == false)
                        {
                            if (!StationManager.Instance.IsInHoming())
                            {
                                //Probe上料平台检测到Probe时，禁止单轴移动Probe上料平台Y轴
                                if (MotionManager.GetDigitalInput(InputName.PrbLoaderDetectPrb1) || MotionManager.GetDigitalInput(InputName.PrbLoaderDetectPrb2))
                                {
                                    if (MotionManager.IsServoOn(StationEnum.Cabinet, AxisName.PrbLoader_Y1) == false ||
                                    MotionManager.IsAlarm(StationEnum.Cabinet, AxisName.PrbLoader_Y1) ||
                                    MotionManager.IsPosLimit(StationEnum.Cabinet, AxisName.PrbLoader_Y1) ||
                                    MotionManager.IsNegLimit(StationEnum.Cabinet, AxisName.PrbLoader_Y1))
                                    {
                                        MotionManager.Stop(StationEnum.Cabinet, AxisName.PrbLoader_Y2);
                                    }
                                    if (MotionManager.IsServoOn(StationEnum.Cabinet, AxisName.PrbLoader_Y2) == false || MotionManager.IsAlarm(StationEnum.Cabinet, AxisName.PrbLoader_Y2) || MotionManager.IsPosLimit(StationEnum.Cabinet, AxisName.PrbLoader_Y2) || MotionManager.IsNegLimit(StationEnum.Cabinet, AxisName.PrbLoader_Y2))
                                    {
                                        MotionManager.Stop(StationEnum.Cabinet, AxisName.PrbLoader_Y1);
                                    }
                                }
                            }
                            if (IsSafeDoorOpen && !MotionManager.GetDigitalInput(InputName.ManualButtonSignal) && ControlPara.ManualEmgEnable)
                            {
                                if (IsTriggered[4] == false)
                                {
                                    IsTriggered[4] = true;
                                    OnShowMsg?.Invoke("安全门开启，手持急停按钮没有在安全状态，禁止移动轴", Brushes.Red);
                                    StationManager.Instance.Stop();
                                }
                            }
                            else
                            {
                                if (IsTriggered[4])
                                {
                                    IsTriggered[4] = false;
                                }
                            }
                        }
                        else
                        {
                            Thread.Sleep(2000);
                        }
                    }
                    catch (Exception ex)
                    {
                        OnShowMsg?.Invoke($"Interlock线程7执行异常，异常信息：{ex.ToString()}", Brushes.Red);
                        StationManager.Instance.Stop();
                        Thread.Sleep(2000);
                    }
                    Thread.Sleep(5);
                }
            });
            t7.IsBackground = true;
            t7.Start();

            //防止2个检测模组X轴相撞
            Thread t9 = new Thread(() =>
            {
                while (true)
                {
                    try
                    {
                        if (ControlPara.InterLockShield == false)
                        {
                            bool detectModuleXErr = Math.Abs(MotionManager.GetAxisActPos(StationEnum.DetectGantry, AxisName.DetectModule1_X) + MotionManager.GetAxisActPos(StationEnum.DetectGantry, AxisName.DetectModule2_X)) > MotionPara.DetectModuleXPreventCrushPos;
                            if (detectModuleXErr && (MotionManager.IsMove(StationEnum.DetectGantry, AxisName.DetectModule1_X) || MotionManager.IsMove(StationEnum.DetectGantry, AxisName.DetectModule2_X)))
                            {
                                if (!StationManager.Instance.IsInHoming())
                                {
                                    MotionManager.Stop(StationEnum.DetectGantry, AxisName.DetectModule1_X);
                                    MotionManager.Stop(StationEnum.DetectGantry, AxisName.DetectModule2_X);
                                    AlarmManager.CreateAlarm((int)PosErrorCode.DetectModuleXPosErr);
                                }
                            }
                        }
                        else
                        {
                            Thread.Sleep(2000);
                        }
                    }
                    catch (Exception ex)
                    {
                        OnShowMsg?.Invoke($"Interlock线程9执行异常，异常信息：{ex.ToString()}", Brushes.Red);
                        StationManager.Instance.Stop();
                        Thread.Sleep(2000);
                    }
                    Thread.Sleep(5);
                }
            });
            t9.IsBackground = true;
            t9.Start();
        }
        /// <summary>
        /// 消除信号抖动
        /// </summary>
        /// <param name="stationName"></param>
        /// <param name="axisName"></param>
        /// <returns></returns>
        private bool Filter(StationEnum stationName, AxisName axisName)
        {
            bool ret = false;
            var pos1 = MotionManager.GetAxisActPos(stationName, axisName);
            Thread.Sleep(2);
            var pos2 = MotionManager.GetAxisActPos(stationName, axisName);
            ret = Math.Abs(pos1 - pos2) > 0.05;
            return ret;
        }
        private void ProbeHeadInterlock(AxisName axisName,int stageIndex=-1)
        {
            if (IsContactZAtSafePos() == false)//如果压头不在安全位置，禁止移动龙门轴，平台轴
            {
                StationManager.Instance.Stop();
                OnShowMsg?.Invoke($"平台压接轴不在安全位置，禁止移动{axisName}", Brushes.Red);
            }
            else if (IsStageAxisZDown(0) == false&& (stageIndex==0|| stageIndex == -1))
            {
                StationManager.Instance.Stop();
                OnShowMsg?.Invoke($"平台1Z轴不在安全位置，禁止移动{axisName}", Brushes.Red);
            }
            else if (IsStageAxisZDown(1) == false && (stageIndex == 1 || stageIndex == -1))
            {
                StationManager.Instance.Stop();
                OnShowMsg?.Invoke($"平台2Z轴不在安全位置，禁止移动{axisName}", Brushes.Red);
            }
            else if (IsClyDown(0) == false && (stageIndex == 0 || stageIndex == -1))
            {
                StationManager.Instance.Stop();
                OnShowMsg?.Invoke($"平台1规整气缸不在安全位置，禁止移动{axisName}", Brushes.Red);
            }
            else if (IsClyDown(1) == false && (stageIndex == 1 || stageIndex == -1))
            {
                StationManager.Instance.Stop();
                OnShowMsg?.Invoke($"平台2规整气缸不在安全位置，禁止移动{axisName}", Brushes.Red);
            }
        }
        private bool IsDetectGlass(int stageIndex)
        {
            string inputName = string.Empty;
            bool ret = true;
            for (int i = 0; i < 4; i++)
            {
                inputName = $"Stage{stageIndex + 1}GlassDetect{i + 1}";
                ret &= MotionManager.GetDigitalInput(inputName);
                Thread.Sleep(10);
            }
            return ret;
        }
        /// <summary>
        /// 上下规整气缸是否全部在原点
        /// </summary>
        /// <returns></returns>
        public bool IsClyDown(int stageIndex)
        {
            string clyName = string.Empty;
            bool ret = true;
            for (int i = 0; i < 8; i++)
            {
                clyName = $"Stage{stageIndex + 1}VerticalCyl{i + 1}";
                ret &= MotionManager.IsCylinderArrive(clyName, false);
                Thread.Sleep(1);
            }
            return ret;
        }
        private bool IsStageAxisZDown(int stageIndex)
        {
            bool ret = false;
            AxisName axis = stageIndex == 0 ? AxisName.Stage1_Z : AxisName.Stage2_Z;
            if (MotionManager.IsHomeOK(StationEnum.DualStage,axis)==false)
            {
                return false;
            }
            ret = MotionManager.GetAxisActPos(StationEnum.DualStage, axis) < 3;
            return ret;
        }
    }

}
