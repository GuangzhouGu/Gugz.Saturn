/*----------------------------------------------------------------
// Copyright (C) Suzhou HYC Technology Co.,LTD
// °æÈ¨ËùÓÐ¡£
//
// =================================
// CLR°æ±¾     £º4.0.30319.42000
// ÃüÃû¿Õ¼ä    £ºHYC.AutoPosition.UI.Controls.Template
// ÎÄ¼þÃû³Æ    £ºSubMarkSetting.cs
// =================================
// ´´ ½¨ Õß    £ºmiaoliang
// ´´½¨ÈÕÆÚ    £º2020/10/30 13:45:05 
// ¹¦ÄÜÃèÊö    £º
// Ê¹ÓÃËµÃ÷    £º
//
//
// ´´½¨±êÊ¶£º
//
// ÐÞ¸Ä±êÊ¶£º
// ÐÞ¸ÄÃèÊö£º
//
// ÐÞ¸Ä±êÊ¶£º
// ÐÞ¸ÄÃèÊö£º
//----------------------------------------------------------------*/
using DrawTools;
using Emgu.CV;
using Emgu.CV.CvEnum;
using HYC.AutoPosition.Extentions;
using HYC.AutoPosition.Interface;
using HYC.AutoPosition.Parameter;
using HYC.AutoPosition.UI.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HYC.AutoPosition.UI.Controls
{
    partial class TemplateMatchingSetting
    {

        private void CameraInformationInitGUI()
        {
           CameraInformationArray cameraInformations = curRecipe.DataPos[CurrentStageIndex].FlowPara.CameraInformations;
            if (cameraInformations.UseSameAngle)
            {
                cameraInformations[0].Theta = cameraInformations[1].Theta;
            }
          
            txtPixelXCamera1.Text = cameraInformations[0].PixelX.ToString();
            txtPixelYCamera1.Text = cameraInformations[0].PixelY.ToString();
            txtResolutionXCamera1.Text = cameraInformations[0].ResolutionX.ToString();
            txtResolutionYCamera1.Text = cameraInformations[0].ResolutionY.ToString();
            txtFOVXCamera1.Text = cameraInformations[0].FOVX.ToString();
            txtFOVYCamera1.Text = cameraInformations[0].FOVY.ToString();
            txtCamera1CenterPointX.Text = cameraInformations[0].CenterPoint.X.ToString();
            txtCamera1CenterPointY.Text = cameraInformations[0].CenterPoint.Y.ToString();
            txtCamera1Theta.Text = cameraInformations[0].Theta.ToString();

            txtPixelXCamera2.Text = cameraInformations[1].PixelX.ToString();
            txtPixelYCamera2.Text = cameraInformations[1].PixelY.ToString();
            txtResolutionXCamera2.Text = cameraInformations[1].ResolutionX.ToString();
            txtResolutionYCamera2.Text = cameraInformations[1].ResolutionY.ToString();
            txtFOVXCamera2.Text = cameraInformations[1].FOVX.ToString();
            txtFOVYCamera2.Text = cameraInformations[1].FOVY.ToString();
            txtCamera2CenterPointX.Text = cameraInformations[1].CenterPoint.X.ToString();
            txtCamera2CenterPointY.Text = cameraInformations[1].CenterPoint.Y.ToString();
            txtCamera2Theta.Text = cameraInformations[1].Theta.ToString();

            txtCameraStageTheta.Text = cameraInformations.DefaultAngle.ToString();

            txtCalibrationOptionX.Text = cameraInformations.CameraOption.DeltaX.ToString();
            txtCalibrationOptionY.Text = cameraInformations.CameraOption.DeltaY.ToString();
            txtCalibrationOptionTheta.Text = cameraInformations.CameraOption.DeltaTheta.ToString();
            this.checkBox1.Checked = cameraInformations.UseSameAngle;
        }
        /// <summary>2
        /// ±£´æCamera²ÎÊý
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCameraInfoSave_Click(object sender, EventArgs e)
        {
            double dValue = 0;
            CameraInformationArray cameraInformations = curRecipe.DataPos[CurrentStageIndex].FlowPara.CameraInformations;
            //Camera 1
            if (ValueIsValid(txtPixelXCamera1.Text, out dValue))
            {
                cameraInformations[0].PixelX = dValue;
            }
            if (ValueIsValid(txtPixelYCamera1.Text, out dValue))
            {
                cameraInformations[0].PixelY = dValue;
            }
            if (ValueIsValid(txtResolutionXCamera1.Text, out dValue))
            {
                cameraInformations[0].ResolutionX = dValue;
            }
            if (ValueIsValid(txtResolutionYCamera1.Text, out dValue))
            {
                cameraInformations[0].ResolutionY = dValue;
            }
            if (ValueIsValid(txtFOVXCamera1.Text, out dValue))
            {
                cameraInformations[0].FOVX = dValue;
            }
            if (ValueIsValid(txtFOVYCamera1.Text, out dValue))
            {
                cameraInformations[0].FOVY = dValue;
            }
            double x, y;
            ValueIsValid(txtCamera1CenterPointX.Text, out x);
            ValueIsValid(txtCamera1CenterPointY.Text, out y);
            cameraInformations[0].CenterPoint = new PointF((float)x, (float)y);
            if (ValueIsValid(txtCamera1Theta.Text, out dValue))
            {
                cameraInformations[0].Theta = dValue;
            }
            //Camera 2
            if (ValueIsValid(txtPixelXCamera2.Text, out dValue))
            {
                cameraInformations[1].PixelX = dValue;
            }
            if (ValueIsValid(txtPixelYCamera2.Text, out dValue))
            {
                cameraInformations[1].PixelY = dValue;
            }
            if (ValueIsValid(txtResolutionXCamera2.Text, out dValue))
            {
                cameraInformations[1].ResolutionX = dValue;
            }
            if (ValueIsValid(txtResolutionYCamera2.Text, out dValue))
            {
                cameraInformations[1].ResolutionY = dValue;
            }
            if (ValueIsValid(txtFOVXCamera2.Text, out dValue))
            {
                cameraInformations[1].FOVX = dValue;
            }
            if (ValueIsValid(txtFOVYCamera2.Text, out dValue))
            {
                cameraInformations[1].FOVY = dValue;
            }
            ValueIsValid(txtCamera2CenterPointX.Text, out x);
            ValueIsValid(txtCamera2CenterPointY.Text, out y);
            cameraInformations[1].CenterPoint = new PointF((float)x, (float)y);
            if (ValueIsValid(txtCamera2Theta.Text, out dValue))
            {
                cameraInformations[1].Theta = dValue;
            }
            if (ValueIsValid(txtCameraStageTheta.Text, out dValue))
            {
                cameraInformations.DefaultAngle = dValue;
            }
            cameraInformations.UseSameAngle = this.checkBox1.Checked;
            curRecipe.SaveDataPos(CurrentStageIndex);
        }
        /// <summary>2
        /// ±£´æCamera²ÎÊý
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCalOptionSave_Click(object sender, EventArgs e)
        {
            double dValue = 0;
            CameraInformationArray cameraInformations = curRecipe.DataPos[CurrentStageIndex].FlowPara.CameraInformations;
            if (ValueIsValid(txtCalibrationOptionX.Text, out dValue))
            {
                cameraInformations.CameraOption.DeltaX = dValue;
            }
            if (ValueIsValid(txtCalibrationOptionY.Text, out dValue))
            {
                cameraInformations.CameraOption.DeltaY = dValue;
            }
            if (ValueIsValid(txtCalibrationOptionTheta.Text, out dValue))
            {
                cameraInformations.CameraOption.DeltaTheta = dValue;
            }
            curRecipe.SaveDataPos(CurrentStageIndex);
        }


        /// <summary>
        /// Calibration
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAutoCalibration_Click(object sender, EventArgs e)
        {
            Task.Factory.StartNew(()=> {
                this.Invoke(new Action(()=> {
                    //If Camera in continus mode,this will be cause crush.
                    DisabledCameraContinus();
                    //
                    DialogResult result = MessageBox.Show($"Are you sure to start Auto Calibration.It will lost pre-value.", "Align", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                    if (result == DialogResult.Cancel)
                    {
                        return;
                    }
                    CameraInformationArray cameraInformations = curRecipe.DataPos[CurrentStageIndex].FlowPara.CameraInformations;
                    var cfg = Global.Setting.ListGroup[CurrentGroupIndex];
                    if (cfg != null)
                    {
                        //
                        IMoveExecutor executor = cfg.MoveExecutorType == MoveExecutorType.XYTPlatform ?
                          new XytMoveExecutor(PosPlatform.GetPlatform(cfg.GroupName)) :
                          (IMoveExecutor)PosSignal.GetSignalProvider();
                        result = MessageBox.Show("Please Check Stage and Camera Can be Homing to Zero Pos", "Align", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                        if (result != DialogResult.OK)
                        {
                            return;
                        }
                        bool ret = AutoCalibrationProcess(0, executor);//LeftCam Calibration FLow
                        if (ret == false)
                        {
                            MessageBox.Show("Auto Calibration Fail", "Align", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                       // CameraInformationInitGUI();
                        ret = AutoCalibrationProcess(1, executor);//RightCam Stage2 Calibration FLow
                        if (ret == false)
                        {
                            MessageBox.Show("Auto Calibration Fail", "Align", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                        CameraInformationInitGUI();
                        PointF leftOP, leftMP, rightOP, rightMP;
                        double angle = CaculateAngleByTwoCamera(out leftOP, out leftMP, out rightOP, out rightMP);
                        cameraInformations.DefaultAngle = angle;
                        txtCameraStageTheta.Text = angle.ToString();
                        curRecipe.SaveDataPos(CurrentStageIndex);
                        MessageBox.Show("Auto Calibration Finish", "Align", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }));
            });
           
        }

        private double CaculateAngleByTwoCamera(out PointF leftOrignPoint, out PointF leftMovePoint, out PointF rightOrignPoint, out PointF righMovedPoint)
        {
            CameraInformationArray cameraInformations = curRecipe.DataPos[CurrentStageIndex].FlowPara.CameraInformations;

            Bitmap templateImage = curRecipe.DataPos[CurrentStageIndex].FlowPara.TemplateImgs[0];
            Bitmap srcImage = Grab(CameraSource.Camera1);
            drawToolControl1.SetBackgroundImage(srcImage);
            double[] leftWorldPoint = new double[3];
            double[] leftPointOffset = TestFind(templateImage, srcImage, CameraSource.Camera1, ref leftWorldPoint);
            leftOrignPoint = new PointF((float)(leftWorldPoint[0] + leftPointOffset[0]), (float)(leftWorldPoint[1] + leftPointOffset[1]));
            leftMovePoint = new PointF((float)(leftWorldPoint[0]), (float)(leftWorldPoint[1]));

            templateImage = curRecipe.DataPos[CurrentStageIndex].FlowPara.TemplateImgs[1];
            srcImage = Grab(CameraSource.Camera2);
            drawToolControl2.SetBackgroundImage(srcImage);
            double[] rightWorldPoint = new double[3];
            double[] rightPointOffset = TestFind(templateImage, srcImage, CameraSource.Camera2, ref rightWorldPoint);
            rightOrignPoint = new PointF((float)(rightWorldPoint[0] + rightPointOffset[0]), (float)(rightWorldPoint[1] + rightPointOffset[1]));
            righMovedPoint = new PointF((float)(rightWorldPoint[0]), (float)(rightWorldPoint[1]));



            rightWorldPoint[0] = rightWorldPoint[0] - cameraInformations[1].CenterPoint.X;
            rightWorldPoint[1] = rightWorldPoint[1] - cameraInformations[1].CenterPoint.Y;

            leftWorldPoint[0] = leftWorldPoint[0] - cameraInformations[0].CenterPoint.X;
            leftWorldPoint[1] = leftWorldPoint[1] - cameraInformations[0].CenterPoint.Y;

            double deltaAngle = Math.Atan2(leftWorldPoint[1] - rightWorldPoint[1], leftWorldPoint[0] - rightWorldPoint[0]);
            deltaAngle = deltaAngle / Math.PI * 180;
            return deltaAngle;
        }
        /// <summary>
        /// ×Ô¶¯±ê¶¨Á÷³Ì ÒÆ¶¯X -deltaX ºó 2*deltaX  ÒÆ¶¯Y  ÏÈ-deltaY ºó2*deltaY
        /// </summary>
        /// <param name="dir">Left or Right Cam</param>
        /// <param name="executor">MoveExecutor</param>
        private bool AutoCalibrationProcess(int dir, IMoveExecutor executor)
        {
            CameraInformationArray cameraInformations = curRecipe.DataPos[CurrentStageIndex].FlowPara.CameraInformations;

            int[] res = new int[3] { dir + 1, 0, 0 };
            string dirString = dir == 0 ? "\\Left" : "\\Right";
            CameraSource cameraSource = dir == 0 ? CameraSource.Camera1 : CameraSource.Camera2;
            double[] xyt = new double[3];
            DrawToolControl drawTool = dir == 0 ? drawToolControl1 : drawToolControl2;
            Point3D originPoint = new Point3D();
            Point3D foundPoint = new Point3D();
            Point3D offsetPoint = new Point3D();
            double[] foundOriginPoint = new double[3];
            double[] foundMovedPoint = new double[3];
            Point3D delta = new Point3D(cameraInformations.CameraOption.DeltaX, cameraInformations.CameraOption.DeltaY, cameraInformations.CameraOption.DeltaTheta);
            Bitmap templateImage = new Bitmap(curRecipe.Location + "\\" + (int)(CurrentStageIndex + 1) + dirString + "Roi.bmp");
            CameraInformation calibration = new CameraInformation();
            //0 Step Caculate Zero Postion Origin Point
            Bitmap srcImage = Grab(cameraSource);
            drawTool.SetBackgroundImage(srcImage);

            double[] originOffset = TestFind(templateImage, srcImage, cameraSource, ref foundOriginPoint);
            originPoint = new Point3D(originOffset);
            cameraInformations[dir].PixelX = srcImage.Width;
            cameraInformations[dir].PixelY = srcImage.Height;
            //1 Step Caculate Axis Resolution
            xyt[0] = -delta.X;
            bool ret = false;
            foundPoint = MoveAndProcess(dir, executor, templateImage, xyt, ref foundMovedPoint,ref  ret);
            if (ret==false)
            {
                return false;
            }
            if (foundPoint == null || foundPoint.Z == 400)
            {
                goto FailEnd;
            }

            offsetPoint = foundPoint - originPoint;
            double resolutionX1 = Math.Abs(delta.X) / Math.Sqrt(offsetPoint.X * offsetPoint.X + offsetPoint.Y * offsetPoint.Y);
            xyt[0] = 2 * delta.X;
            foundPoint = MoveAndProcess(dir, executor, templateImage, xyt, ref foundOriginPoint, ref ret);
            if (ret == false)
            {
                return false;
            }
            if (foundPoint == null || foundPoint.Z == 400)
            {
                goto FailEnd;
            }
            offsetPoint = foundPoint - originPoint;
            double resolutionX2 = Math.Abs(delta.X) / Math.Sqrt(offsetPoint.X * offsetPoint.X + offsetPoint.Y * offsetPoint.Y);
            cameraInformations[dir].ResolutionX = ((resolutionX1 + resolutionX2) / 2);
            double PixelOffset = delta.X / cameraInformations[dir].ResolutionX;
            //2 Step Caculate Camera and Stage Angle
            double angle = calibration.CaculateCameraTheta(originPoint.PointF, foundPoint.PointF, PixelOffset, 0);
            cameraInformations[dir].Theta = angle;

            //回原点
            xyt[0] = -delta.X;
            ret= executor.Move(CurrentGroupIndex, xyt, res);
            if (ret==false)
            {
                return ret;
            }
            xyt[0] = 0;
            srcImage = Grab(cameraSource);
            drawTool.SetBackgroundImage(srcImage);
            //3 Step Caculate Axis Resolution
            xyt[1] = -delta.Y;
            foundPoint = MoveAndProcess(dir, executor, templateImage, xyt, ref foundMovedPoint,ref ret);
            if (ret == false)
            {
                return false;
            }
            if (foundPoint == null || foundPoint.Z == 400)
            {
                goto FailEnd;
            }
            offsetPoint = foundPoint - originPoint;
            double resolutionY1 = Math.Abs(delta.Y) / Math.Sqrt(offsetPoint.X * offsetPoint.X + offsetPoint.Y * offsetPoint.Y);
            xyt[1] = 2 * delta.Y;
                
            foundPoint = MoveAndProcess(dir, executor, templateImage, xyt, ref foundMovedPoint,ref ret);
            if (ret == false)
            {
                return false;
            }
            if (foundPoint == null || foundPoint.Z == 400)
            {
                goto FailEnd;
            }
            offsetPoint = foundPoint - originPoint;
            double resolutionY2 = Math.Abs(delta.Y) / Math.Sqrt(offsetPoint.X * offsetPoint.X + offsetPoint.Y * offsetPoint.Y);
            cameraInformations[dir].ResolutionY = (resolutionY1 + resolutionY2) / 2;
            xyt[1] = -delta.Y;

            ret=executor.Move(CurrentGroupIndex, xyt, res);
            if (ret == false)
            {
                return false;
            }
            xyt[1] = 0;
            srcImage = Grab(cameraSource);
            drawTool.SetBackgroundImage(srcImage);
            //5 Step Caculate Rotate CenterPoint
            xyt[2] = -delta.Z;
            foundPoint = MoveAndProcess(dir, executor, templateImage, xyt, ref foundMovedPoint,ref ret);
            if (ret == false)
            {
                return false;
            }
            if (foundPoint == null || foundPoint.Z == 400)
            {
                goto FailEnd;
            }
            Point3D rotatedPoint1 = new Point3D(foundMovedPoint);
            Bitmap temp = drawTool.GetBackgroundImage();
            xyt[2] = 2 * delta.Z;
            foundPoint = MoveAndProcess(dir, executor, templateImage, xyt, ref foundMovedPoint, ref ret);
            if (ret == false)
            {
                return false;
            }
            if (foundPoint == null || foundPoint.Z == 400)
            {
                goto FailEnd;
            }
            Point3D rotatedPoint2 = new Point3D(foundMovedPoint);
            PointF centerPoint = calibration.CaculateRotatedCenterPoint(rotatedPoint1.PointF, rotatedPoint2.PointF, -2 * cameraInformations.CameraOption.DeltaTheta);
            cameraInformations[dir].CenterPoint = centerPoint;
            xyt[2] = -delta.Z;
            ret=executor.Move(CurrentGroupIndex, xyt, res);
            if (ret == false)
            {
                return false;
            }
            xyt[2] = 0;
            srcImage = Grab(cameraSource);
            drawTool.SetBackgroundImage(srcImage);
            return true;
        FailEnd:
            MessageBox.Show("Find Target Fail,Finish Auto Calibration", "Align", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }

        private Point3D MoveAndProcess(int dir, IMoveExecutor executor, Bitmap templateImage, double[] xyt, ref double[] findPoint,ref bool ret)
        {
            CameraSource cameraSource = dir == 0 ? CameraSource.Camera1 : CameraSource.Camera2;
            int[] res = new int[3] { dir + 1, 0, 0 };
            ret=executor.Move(CurrentGroupIndex, xyt, res);
            if (ret==false)
            {
                return null;
            }
            //Camera Grab
            Bitmap srcImage = Grab(cameraSource);
            DrawToolControl drawTool = dir == 0 ? drawToolControl1 : drawToolControl2;
            drawTool.SetBackgroundImage(srcImage);
     
            double[] _offset = TestFind(templateImage, srcImage, cameraSource, ref findPoint);
            return new Point3D(_offset);
        }
        /// <summary>
        /// ½á¹û±£´æ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnResultSave_Click(object sender, EventArgs e)
        {

        }

        private void btnCameraMove_Click(object sender, EventArgs e)
        {
            var cfg = Global.Setting.ListGroup[CurrentGroupIndex];
            if (cfg != null)
            {
                // ¸ù¾ÝÉèÖÃÖÐµÄÀàÐÍ´´½¨ÔË¶¯Ö´ÐÐÕß
                IMoveExecutor executor = cfg.MoveExecutorType == MoveExecutorType.XYTPlatform ?
                  new XytMoveExecutor(PosPlatform.GetPlatform(cfg.GroupName)) :
                  (IMoveExecutor)PosSignal.GetSignalProvider();
                double[] xyt = new double[3] { 0, 0, 0 };
                int[] res = new int[3] { 0, 0, 0 };
                Double.TryParse(txtCameraMoveX.Text, out xyt[0]);
                Double.TryParse(txtCameraMoveY.Text, out xyt[1]);
                Double.TryParse(txtCameraMoveT.Text, out xyt[2]);
                Int32.TryParse(txtCameraIndex.Text, out res[0]);
                executor.Move(CurrentGroupIndex, xyt, res);
            }
        }

        private void btnCameraMoveHome_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Please Check Stage and Camera Can be Homing to Zero Pos", "Align", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (result == DialogResult.OK)
            {
                CameraMoveHome();
            }
        }

        private void CameraMoveHome()
        {
            var cfg = Global.Setting.ListGroup[CurrentGroupIndex];
            if (cfg != null)
            {

                IMoveExecutor executor = cfg.MoveExecutorType == MoveExecutorType.XYTPlatform ?
                  new XytMoveExecutor(PosPlatform.GetPlatform(cfg.GroupName)) :
                  (IMoveExecutor)PosSignal.GetSignalProvider();
                double[] xyt = new double[3] { 0, 0, 0 };
                int[] res = new int[3] { 0, 1, 0 };
                Double.TryParse(txtCameraMoveX.Text, out xyt[0]);
                Double.TryParse(txtCameraMoveY.Text, out xyt[1]);
                Double.TryParse(txtCameraMoveT.Text, out xyt[2]);
                executor.Move(CurrentGroupIndex, xyt, res);
            }
        }
        /// <summary>
        /// È·ÈÏÊý¾Ý¸ñÊ½ÊÇ·ñ·ûºÏÒªÇó
        /// </summary>
        /// <param name="value"></param>
        /// <param name="dValue"></param>
        /// <returns></returns>
        private bool ValueIsValid(string value, out double dValue)
        {
            bool ret = Double.TryParse(value, out dValue);
            if (ret == false)
            {
                MessageBox.Show("Please Check Input Value");
            }
            return ret;
        }
    }
}
