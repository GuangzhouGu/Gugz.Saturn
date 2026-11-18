using HYC.HTPCBase.Extension.Recipe;
using HYC.HTPCBase.Extension.Views;
using HYC.HTPCBase.Motion;
using HYC.SaturnMain.Params;
using HYC.SaturnMain.Devices.ClientPC;
using HYC.SaturnMain.Devices.Motion;
using HYC.SaturnMain.Entities;
using System;

using System.Windows.Forms;
using HYC.HTPCBase.Extension.Entities;

namespace HYC.SaturnMain.SubForms
{
    public partial class FrmCamBasePoint : ParaViewBase
    {
        public FrmCamBasePoint()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 龙门方向
        /// </summary>
        double XDirBasePoint = 0;
        /// <summary>
        /// 相机方向
        /// </summary>
        double YDirBasePoint = 0;
        bool IsCalibrated = false;
        private void btnIRCamBasePoint_Click(object sender, EventArgs e)
        {
            try
            {
                string temp = cmxCamList.SelectedItem.ToString();
                if (string.IsNullOrEmpty(temp))
                {
                    MessageBox.Show("请选择需要标定的相机");
                    return ;
                }
                var camName = EnumHelper.GetEnumValue<CameraName>(temp);
                MotionManager motion = MotionManager.GetInstance();
                switch (camName)
                {
                    case CameraName.AoiCam1:
                    case CameraName.MuraCam1:
                    case CameraName.SquintMuraCam1:
                    case CameraName.MultiAngle1:
                        {
                            YDirBasePoint = motion.GetAxisActPos(StationEnum.DetectGantry, AxisName.DetectModule1_X) - Param.Panel.RightMarkOffsetY;
                            break;
                        }
                    case CameraName.AoiCam2:
                    case CameraName.MuraCam2:
                    case CameraName.SquintMuraCam2:
                    case CameraName.MultiAngle2:
                        {
                            YDirBasePoint = motion.GetAxisActPos(StationEnum.DetectGantry, AxisName.DetectModule2_X) + Param.Panel.RightMarkOffsetY2;
                            break;
                        }
                    default:
                        break;
                }
                XDirBasePoint = motion.GetAxisActPos(StationEnum.DetectGantry, AxisName.DetectGantry_Y1) + Param.Panel.RightMarkOffsetX;
                tbxXDirBasePoint.Text = XDirBasePoint.ToString();
                tbxYDirBasePoint.Text = YDirBasePoint.ToString();
                IsCalibrated = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FrmCamBasePoint_Load(object sender, EventArgs e)
        {
            cmxStage.SelectedIndex = 0;
            cmxCamList.SelectedIndex = 0;
        }

        public override bool Save()
        {
            try
            {
                if (IsCalibrated==false)
                {
                    MessageBox.Show("请先计算基准点");
                    return true;
                }
                var temp = cmxCamList.SelectedItem.ToString();
                if (string.IsNullOrEmpty(temp))
                {
                    MessageBox.Show("请选择需要标定的相机");
                    return false;
                }
                var camName = EnumHelper.GetEnumValue<CameraName>(temp);
                int stageIndex = cmxStage.SelectedIndex;
                switch (camName)
                {
                    case CameraName.AoiCam1:
                        {
                            if (stageIndex == 0)
                            {
                                Param.Motion.ST1_AoiCam1BasePoint_X = Math.Round(XDirBasePoint,4);
                                Param.Motion.ST1_AoiCam1BasePoint_Y = Math.Round(YDirBasePoint,4);
                            }
                            else
                            {
                                Param.Motion.ST2_AoiCam1BasePoint_X = Math.Round(XDirBasePoint, 4);
                                Param.Motion.ST2_AoiCam1BasePoint_Y = Math.Round(YDirBasePoint, 4);
                            }
                            break;
                        }

                    case CameraName.AoiCam2:
                        {
                            if (stageIndex == 0)
                            {
                                Param.Motion.ST1_AoiCam2BasePoint_X = Math.Round(XDirBasePoint, 4);
                                Param.Motion.ST1_AoiCam2BasePoint_Y = Math.Round(YDirBasePoint, 4);
                            }
                            else
                            {
                                Param.Motion.ST2_AoiCam2BasePoint_X = Math.Round(XDirBasePoint, 4);
                                Param.Motion.ST2_AoiCam2BasePoint_Y = Math.Round(YDirBasePoint, 4);
                            }
                            break;
                        }
                    case CameraName.MuraCam1:
                        {
                            if (stageIndex == 0)
                            {
                                Param.Motion.ST1_MuraCam1BasePoint_X = Math.Round(XDirBasePoint, 4);
                                Param.Motion.ST1_MuraCam1BasePoint_Y = Math.Round(YDirBasePoint, 4);
                            }
                            else
                            {
                                Param.Motion.ST2_MuraCam1BasePoint_X = Math.Round(XDirBasePoint, 4);
                                Param.Motion.ST2_MuraCam1BasePoint_Y = Math.Round(YDirBasePoint, 4);
                            }
                            break;
                        }

                    case CameraName.MuraCam2:
                        {
                            if (stageIndex == 0)
                            {
                                Param.Motion.ST1_MuraCam2BasePoint_X = Math.Round(XDirBasePoint, 4);
                                Param.Motion.ST1_MuraCam2BasePoint_Y = Math.Round(YDirBasePoint, 4);
                            }
                            else
                            {
                                Param.Motion.ST2_MuraCam2BasePoint_X = Math.Round(XDirBasePoint, 4);
                                Param.Motion.ST2_MuraCam2BasePoint_Y = Math.Round(YDirBasePoint, 4);
                            }
                            break;
                        }
                    case CameraName.SquintMuraCam1:
                        {
                            if (stageIndex == 0)
                            {
                                Param.Motion.ST1_SquintMuraCam1BasePoint_X = Math.Round(XDirBasePoint, 4);
                                Param.Motion.ST1_SquintMuraCam1BasePoint_Y = Math.Round(YDirBasePoint, 4);
                            }
                            else
                            {
                                Param.Motion.ST2_SquintMuraCam1BasePoint_X = Math.Round(XDirBasePoint, 4);
                                Param.Motion.ST2_SquintMuraCam1BasePoint_Y = Math.Round(YDirBasePoint, 4);
                            }
                            break;
                        }

                    case CameraName.SquintMuraCam2:
                        {
                            if (stageIndex == 0)
                            {
                                Param.Motion.ST1_SquintMuraCam2BasePoint_X = Math.Round(XDirBasePoint, 4);
                                Param.Motion.ST1_SquintMuraCam2BasePoint_Y = Math.Round(YDirBasePoint, 4);
                            }
                            else
                            {
                                Param.Motion.ST2_SquintMuraCam2BasePoint_X = Math.Round(XDirBasePoint, 4);
                                Param.Motion.ST2_SquintMuraCam2BasePoint_Y = Math.Round(YDirBasePoint, 4);
                            }
                            break;
                        }
                    case CameraName.MultiAngle1:
                        {
                            if (stageIndex == 0)
                            {
                                Param.Motion.ST1_MultiAngle1BasePoint_X = Math.Round(XDirBasePoint, 4);
                                Param.Motion.ST1_MultiAngle1BasePoint_Y = Math.Round(YDirBasePoint, 4);
                            }
                            else
                            {
                                Param.Motion.ST2_MultiAngle1BasePoint_X = Math.Round(XDirBasePoint, 4);
                                Param.Motion.ST2_MultiAngle1BasePoint_Y = Math.Round(YDirBasePoint, 4);
                            }
                            break;
                        }

                    case CameraName.MultiAngle2:
                        {
                            if (stageIndex == 0)
                            {
                                Param.Motion.ST1_MultiAngle2BasePoint_X = Math.Round(XDirBasePoint, 4);
                                Param.Motion.ST1_MultiAngle2BasePoint_Y = Math.Round(YDirBasePoint, 4);
                            }
                            else
                            {
                                Param.Motion.ST2_MultiAngle2BasePoint_X = Math.Round(XDirBasePoint, 4);
                                Param.Motion.ST2_MultiAngle2BasePoint_Y = Math.Round(YDirBasePoint, 4);
                            }
                            break;
                        }
                    default:
                        break;
                }
                return RecipeManager.CurRecipe.Para.SavePara(ParaType.Motion);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
    }
}
