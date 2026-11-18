/*----------------------------------------------------------------
// Copyright (C) Suzhou HYC Technology Co.,LTD
// 版权所有。
//
// =================================
// CLR版本     ：4.0.30319.42000
// 命名空间    ：HYC.AutoPosition.UI.Controls
// 文件名称    ：MarkSetting.cs
// =================================
// 创 建 者    ：miaoliang
// 创建日期    ：2020/10/7 10:43:04 
// 功能描述    ：
// 使用说明    ：模板匹配定位中Mark的设定
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
using DrawTools;
using Emgu.CV;
using Emgu.CV.Structure;
using HYC.AutoPosition.Extentions;
using HYC.AutoPosition.ImageProcess;
using HYC.AutoPosition.Interface;
using HYC.AutoPosition.Parameter;
using HYC.AutoPosition.Recipes;
using HYC.AutoPosition.UI.Entities;
using HYC.HTCamera.GigE;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HYC.AutoPosition.UI.Controls
{
    public partial class TemplateMatchingSetting : UserControl
    {
        /// <summary>
        /// 当前选择Group
        /// </summary>
        public int CurrentGroupIndex { get; set; }
        /// <summary>
        /// 当前选择GroupConfig
        /// </summary>
        public GroupConfig GroupConfig { get; set; }
        /// <summary>
        /// 当前选择Stage
        /// </summary>
        public int CurrentStageIndex { get; set; }

        private bool bZoomIn = false;
        private DrawToolControl[] drawToolControls = new DrawToolControl[4];
        
        private Recipe curRecipe;
        public TemplateMatchingSetting()
        {
            InitializeComponent();
            drawToolControls[0] = drawToolControl1;
            drawToolControls[1] = drawToolControl2;
            drawToolControls[2] = drawToolControl3;
            drawToolControls[3] = drawToolControl4;

            drawToolControl1.EnableToolStrip(false);
            drawToolControl2.EnableToolStrip(false);
            drawToolControl3.EnableDraw(false);
            drawToolControl4.EnableDraw(false);
            foreach (DrawToolControl ctrl in drawToolControls)
            {
                ctrl.OnDoubleClickEvent(new EventHandler(drawLayer_DoubleClick));
            }
            InitGUI();

        }

        public void InitGUI()
        {
            selectCameraSource = CameraSource.Camera1;
            selectMarkAreaType = MarkAreaType.Mark;
            selectSystemModeType = SystemModeType.Test;
            radioButtonTest.Checked = true;
            radioButtonCameraModeGrab.Checked = true;
            radioButtonCamera1.Checked = true;
            btnCenterCrossLine.BackColor = Color.SkyBlue;

        }

        public void UpdateView()
        {
            this.picObjectImage1.Image = null;
            this.picObjectImage2.Image = null;
            curRecipe = Global.Manager.CurRecipe;
            CameraInformationInitGUI();
            AlignParameterInitGUI();
            var dataPos = curRecipe.DataPos[CurrentStageIndex];
            this.picObjectImage1.Image = dataPos.FlowPara.TemplateImgs[0];
            this.picObjectImage2.Image = dataPos.FlowPara.TemplateImgs[1];

            //drawToolControl3.SetBackgroundImage(new Bitmap("D:\\Mark\\1013\\050450506\\left.bmp"));
            //drawToolControl4.SetBackgroundImage(new Bitmap("D:\\Mark\\1013\\050450506\\right.bmp"));
            //drawToolControl1.SetBackgroundImage(new Bitmap("D:\\Mark\\1013\\050805746\\left.bmp"));
            //drawToolControl2.SetBackgroundImage(new Bitmap("D:\\Mark\\1013\\050805746\\right.bmp"));
        }

        /// <summary>
        /// 变更大小
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void drawLayer_DoubleClick(object sender, EventArgs e)
        {
            selectMarkAreaType = MarkAreaType.None;
            int index = findLayerIndex(sender);
            float percent = 50;
            if (bZoomIn == true)
            {
                SetTlpRowHeight(tableLayoutPanelImageViewer, 0, percent);
                SetTlpColWidth(tableLayoutPanelImageViewer, 0, percent);
                SetTlpRowHeight(tableLayoutPanelImageViewer, 1, percent);
                SetTlpColWidth(tableLayoutPanelImageViewer, 1, percent);
                bZoomIn = false;
                goto End;
            }

            int col = index % 2;
            int row = index / 2;
            switch (index)
            {
                case 0:
                    {
                        SetTlpRowHeight(tableLayoutPanelImageViewer, 0, percent * 2);
                        SetTlpColWidth(tableLayoutPanelImageViewer, 0, percent * 2);
                        SetTlpRowHeight(tableLayoutPanelImageViewer, 1, 0);
                        SetTlpColWidth(tableLayoutPanelImageViewer, 1, 0);
                        bZoomIn = true;
                    }
                    break;
                case 1:
                    {
                        SetTlpRowHeight(tableLayoutPanelImageViewer, 0, percent * 2);
                        SetTlpColWidth(tableLayoutPanelImageViewer, 1, percent * 2);
                        SetTlpRowHeight(tableLayoutPanelImageViewer, 1, 0);
                        SetTlpColWidth(tableLayoutPanelImageViewer, 0, 0);
                        bZoomIn = true;
                    }
                    break;
                case 2:
                    {
                        SetTlpRowHeight(tableLayoutPanelImageViewer, 1, percent * 2);
                        SetTlpColWidth(tableLayoutPanelImageViewer, 0, percent * 2);
                        SetTlpRowHeight(tableLayoutPanelImageViewer, 0, 0);
                        SetTlpColWidth(tableLayoutPanelImageViewer, 1, 0);
                        bZoomIn = true;
                    }
                    break;
                case 3:
                    {
                        SetTlpRowHeight(tableLayoutPanelImageViewer, 1, percent * 2);
                        SetTlpColWidth(tableLayoutPanelImageViewer, 1, percent * 2);
                        SetTlpRowHeight(tableLayoutPanelImageViewer, 0, 0);
                        SetTlpColWidth(tableLayoutPanelImageViewer, 0, 0);
                        bZoomIn = true;
                    }
                    break;
            }
        End:
            this.tableLayoutPanelImageViewer.Invalidate();
        }

        private int findLayerIndex(object sender)
        {
            DrawArea drawArea = (DrawArea)sender;
            for (int i = 0; i < 4; i++)
            {
                if (drawArea.Parent == drawToolControls[i])
                {
                    return i;
                }
            }
            return -1;
        }
       
        /// <summary>
        /// 显示十字中心线
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private bool bDrawCenterCross = true;
        private void btnCenterCrossLine_Click(object sender, EventArgs e)
        {
            bDrawCenterCross = !bDrawCenterCross;
            if (bDrawCenterCross == false)
            {
                btnCenterCrossLine.BackColor = Color.White;
            }
            else
            {
                btnCenterCrossLine.BackColor = Color.SkyBlue;
            }
            drawToolControl1.EnableCenterCross(bDrawCenterCross);
            drawToolControl2.EnableCenterCross(bDrawCenterCross);
            drawToolControl3.EnableCenterCross(bDrawCenterCross);
            drawToolControl4.EnableCenterCross(bDrawCenterCross);
        }

        private void btnSaveCapture_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "选择存储目录"; ;//左上角提示
            string path = string.Empty;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                path = dialog.SelectedPath;
                drawToolControl1.SaveCapture(path + "\\CaptureTargetLeft.bmp");
                drawToolControl2.SaveCapture(path + "\\CaptureTargetRight.bmp");
                drawToolControl3.SaveCapture(path + "\\CaptureObjectLeft.bmp");
                drawToolControl4.SaveCapture(path + "\\CaptureObjectRight.bmp");
            }

        }

        private void btnSaveOrigin_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "选择存储目录"; ;//左上角提示
            string path = string.Empty;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                path = dialog.SelectedPath;
                drawToolControl1.SaveOrigin(path + "\\OriginTargetLeft.bmp");
                drawToolControl2.SaveOrigin(path + "\\OriginTargetRight.bmp");
                drawToolControl3.SaveOrigin(path + "\\OriginObjectLeft.bmp");
                drawToolControl4.SaveOrigin(path + "\\OriginObjectRight.bmp");
            }
        }

        private void btnClearViewer_Click(object sender, EventArgs e)
        {
            drawToolControl1.SetBackgroundImage(null);
            drawToolControl2.SetBackgroundImage(null);
            drawToolControl3.SetBackgroundImage(null);
            drawToolControl4.SetBackgroundImage(null);
        }

        private void chkResult_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void chkRuler_CheckedChanged(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 显示原始图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOrignView_Click(object sender, EventArgs e)
        {
            Bitmap srcImage = getDrawToolControl(selectCameraSource, selectSystemModeType).GetBackgroundImage();

            if (srcImage == null)
            {
                return;
            }
            FormOriginView originViewForm = new FormOriginView();
            
            originViewForm.InitSize(srcImage.Size);
            originViewForm.Image = srcImage;
            originViewForm.ShowDialog();
        }

        private DrawToolControl getDrawToolControl(CameraSource cameraSource, SystemModeType systemModeType)
        {
            if (systemModeType == SystemModeType.Test)
            {
                if (selectCameraSource == CameraSource.Camera1)
                {
                    return drawToolControl1;
                }
                else
                {
                    return drawToolControl2;
                }
            }
            else
            {
                if (selectCameraSource == CameraSource.Camera1)
                {
                    return drawToolControl3;
                }
                else
                {
                    return drawToolControl4;
                }
            }
        }

        /// <summary>
        /// 采集图片
        /// </summary>
        /// <param name="cameraSource"></param>
        /// <returns></returns>
        private Bitmap Grab(CameraSource cameraSource)
        {
            try
            {
                Bitmap img = null;
                if (cbxOffLine.Checked)
                {
                    OpenFileDialog ofd = new OpenFileDialog();
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        img = Image.FromFile(ofd.FileName) as Bitmap;
                    }
                }
                else
                {
                    var cfg = Global.Setting.ListGroup[CurrentGroupIndex];
                    int i = cameraSource == CameraSource.Camera1 ? 0 : 1;
                    int camIndex = PosCamera.GetCameraIndex(cfg.ListCamera[i].MacAddr);
                    var data = PosCamera.Grab(camIndex);
                    img = data.ToBitmap();
                }
                return img;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 测试找Mark
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTestFind_Click(object sender, EventArgs e)
        {
            DisabledCameraContinus();
            textBox1.Text = "";
            Bitmap templateImage = curRecipe.DataPos[CurrentStageIndex].FlowPara.TemplateImgs[selectCameraSource == CameraSource.Camera1?0:1];
            Bitmap srcImage = Grab(selectCameraSource);
            if (srcImage==null)
            {
                return;
            }
            if (selectCameraSource == CameraSource.Camera1)
            {
                drawToolControl1.SetBackgroundImage(srcImage);
            }
            else {
                drawToolControl2.SetBackgroundImage(srcImage);
            }
            
            double[] foundPoint = new double[3];
            TestFind(templateImage, srcImage, selectCameraSource, ref foundPoint);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTestFindAll_Click(object sender, EventArgs e)
        {
            try
            {
                DisabledCameraContinus();
                CameraInformationArray cameraInformations = curRecipe.DataPos[CurrentStageIndex].FlowPara.CameraInformations;

                textBox1.Text = "";
                PointF leftOP, leftMP, rightOP, rightMP;
                double angle = CaculateAngleByTwoCamera(out leftOP, out leftMP, out rightOP, out rightMP);
                double defaultAngle = cameraInformations.DefaultAngle;
                double deltaAngle = 0;
                if (angle * defaultAngle < 0)
                {
                    if (angle < 0)
                    {
                        deltaAngle = 360 + angle - defaultAngle;
                    }
                    else
                    {
                        deltaAngle = 360 + defaultAngle - angle;
                    }

                }
                else if (angle * defaultAngle > 0)
                {
                    deltaAngle = angle - defaultAngle;
                }

                Point3D args1 = cameraInformations[0].CaculationXYT(leftOP, leftMP, deltaAngle);
                Point3D args2 = cameraInformations[1].CaculationXYT(rightOP, rightMP, deltaAngle);
                textBox1.Text = string.Format("1 X:{0} Y:{1} Theta:{2} \r\n", args1.X, args1.Y, args1.Z) + textBox1.Text;
                textBox1.Text = string.Format("2 X:{0} Y:{1} Theta:{2} \r\n", args2.X, args2.Y, args2.Z) + textBox1.Text;
                textBox1.Text = string.Format("Total X:{0} Y:{1} Theta:{2} \r\n", (args1.X + args2.X) / 2, (args1.Y + args2.Y) / 2, args2.Z) + textBox1.Text;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void DisabledCameraContinus()
        {
            radioButtonCameraModeLive.Checked = false;
            radioButtonCameraModeGrab.Checked = true;
            radioButtonCameraModeLive.BackColor = radioButtonCameraModeLive.Checked ? Color.SkyBlue : SystemColors.Control;
            CameraLive(this.radioButtonCameraModeLive.Checked, selectSystemModeType == SystemModeType.Test ? 0 : 2);
        }
        private void TestFind(string templateFile, string srcFile, CameraSource cameraSource)
        {
            Bitmap templateImage = new Bitmap(templateFile);
            Bitmap srcImage = new Bitmap(srcFile);
            double[] foundPoint = new double[3];
            TestFind(templateImage, srcImage, cameraSource, ref foundPoint);
        }

        /// <summary>
        /// 图片对位
        /// </summary>
        /// <param name="templateImage"></param>
        /// <param name="srcImage"></param>
        /// <param name="cameraSource"></param>
        private double[] TestFind(Bitmap templateImage, Bitmap srcImage, CameraSource cameraSource, ref double[] foundPoint)
        {
            Image img = srcImage;
            HTImageProcessWrapper imageProc = new HTImageProcessWrapper();
            imageProc.InitTemplate(templateImage);
            double rowCheck, colCheck, angleCheck, score;

            imageProc.Match(srcImage, out rowCheck, out colCheck, out angleCheck, out score);
            if (angleCheck == -400)
            {
                MessageBox.Show("Find Target Fail!");
                return new double[3] { 0,0,400};
            }
            string fileName = cameraSource == CameraSource.Camera1 ? "\\LeftROI.xml" : "\\RightROI.xml";
            string filePath = curRecipe.Location + "\\" + (int)(CurrentStageIndex + 1) + fileName;
            AlignROIArray alignROIArray = AlignROIArray.Unserialize(filePath);
            AlignROI alignROI = alignROIArray[0];
                    
            double xOffset = Math.Round(alignROI.CenterPoint.X - colCheck,1);
            double yOffset = Math.Round(alignROI.CenterPoint.Y - rowCheck,1);
            Rectangle roiRect = new Rectangle((int)colCheck - templateImage.Width / 2, (int)rowCheck - templateImage.Height / 2, templateImage.Width, templateImage.Height);
            Bitmap bmpCrop = srcImage.Clone(roiRect, templateImage.PixelFormat);
            textBox1.Text = string.Format("X:{0} Y:{1} Theta:{2} Score:{3}\r\n", xOffset, yOffset, angleCheck, score)+ textBox1.Text;
            PictureBox pic = cameraSource == CameraSource.Camera1 ? picTargetImage1 : picTargetImage2;
            pic.Image = bmpCrop;
            foundPoint[0] = colCheck;
            foundPoint[1] = rowCheck;
            foundPoint[2] = angleCheck;

            return new double[3] { xOffset,yOffset, angleCheck }; 
        }

        public static Image<Gray, byte> ImagePan(Image<Gray, byte> src, double s, double v,double angle)
        {
            
            double[,] m = new double[,] { { Math.Cos(angle), -Math.Sin(angle), s }, { Math.Sin(angle), Math.Cos(angle), v } };
            Matrix<double> matrix = new Matrix<double>(m);
            CvInvoke.WarpAffine(src, src, matrix, src.Size);
            return src;
        }
        private void btnRegisterr_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectCameraSource == CameraSource.Camera1)
                {
                    SaveROIInfo(drawToolControl3, CameraSource.Camera1);
                }
                else
                {
                    SaveROIInfo(drawToolControl4, CameraSource.Camera2);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 保存ROI以及Template信息
        /// </summary>
        /// <param name="drawToolControl"></param>
        /// <param name="cameraSource"></param>
        private void SaveROIInfo(DrawToolControl drawToolControl, CameraSource cameraSource)
        {
            string dir = cameraSource == CameraSource.Camera1 ? "Left" : "Right";
            string Path = curRecipe.Location + "\\" + (int)(CurrentStageIndex+1);
            if (!Directory.Exists(Path))
            {
                Directory.CreateDirectory(Path);
            }

            List<ROIRectangle> lstROIRects = drawToolControl.GetROIs();
            ROIRectangle markROI = null;
            foreach (ROIRectangle roi in lstROIRects)
            {
                if (roi.ROIType == 1)
                {
                    markROI = roi;
                    break;
                }
            }
            if (markROI == null)
            {
                MessageBox.Show("Please Select a ROI Region");
                return;
            }

            Bitmap bmpImage = drawToolControl.GetBackgroundImage();
            RectangleF roiRect = new RectangleF(markROI.NormalizationRectangle.X * bmpImage.Width, markROI.NormalizationRectangle.Y * bmpImage.Height, markROI.NormalizationRectangle.Width * bmpImage.Width, markROI.NormalizationRectangle.Height * bmpImage.Height);
            Bitmap bmpCrop = bmpImage.Clone(roiRect, bmpImage.PixelFormat);
            Console.WriteLine(string.Format("Center:{0} {1}", roiRect.X + roiRect.Width / 2, roiRect.Y + roiRect.Height / 2));

            //重新调整Mark
            FormMarkModify formMarkModify = new FormMarkModify();
            formMarkModify.StartPosition = FormStartPosition.CenterParent;
            formMarkModify.Image = bmpCrop;
            if (formMarkModify.ShowDialog() == DialogResult.OK)
            {
                Point point = formMarkModify.GetOffset();
                PictureBox pic = cameraSource == CameraSource.Camera1 ? picObjectImage1 : picObjectImage2;
                roiRect.Offset(point);
                bmpCrop = bmpImage.Clone(roiRect, bmpImage.PixelFormat);
                markROI.NormalizationRectangle.X = (roiRect.X / bmpImage.Width);
                markROI.NormalizationRectangle.Y = (roiRect.Y / bmpImage.Height);
                bmpCrop = bmpImage.Clone(roiRect, bmpImage.PixelFormat);
                pic.Image = bmpCrop;
                
                //保存UI显示的ROI位置
                drawToolControl.Save(Path + "\\" + dir + "ROIUI.xml");

                //保存Align ROI的信息
                string rawImagePath = curRecipe.Location + "\\" + (CurrentStageIndex+1).ToString() + "\\" + dir + ".bmp";
                string roiImagePath = curRecipe.Location + "\\" + (CurrentStageIndex + 1).ToString() + "\\" + dir + "Roi.bmp";
                AlignROIArray alignROIArray = new AlignROIArray();
                AlignROI alignROI = new AlignROI();
                alignROI.AreaROI = new RectangleF(0,0,bmpImage.Width, bmpImage.Height);
                alignROI.MarkROI = roiRect;
                alignROI.CenterPoint = new PointF(roiRect.X + roiRect.Width / 2, roiRect.Y + roiRect.Height / 2);
                alignROI.ROIImageFilePath = roiImagePath;
                alignROIArray.Add(alignROI);
                alignROIArray.ImageFilePath = rawImagePath;
                alignROIArray.ImageSize = new Size(bmpImage.Width, bmpImage.Height);
                alignROIArray.Serialize(Path + "\\" + dir + "ROI.xml");
                bmpCrop.Save(roiImagePath);
                bmpImage.Save(rawImagePath);

                Global.Manager.CurRecipe.DataPos[CurrentStageIndex].FlowPara.ROI[cameraSource == CameraSource.Camera1 ? 0 : 1] = roiRect;
                Global.Manager.CurRecipe.DataPos[CurrentStageIndex].FlowPara.TemplateImgs[cameraSource == CameraSource.Camera1 ? 0 : 1] = bmpCrop;
            }
        }

        /// <summary>
        /// 测试对位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTestAlignment_Click(object sender, EventArgs e)
        {
            DisabledCameraContinus();
            CameraInformationArray cameraInformations = curRecipe.DataPos[CurrentStageIndex].FlowPara.CameraInformations;

            textBox1.Text = "";
            PointF leftOP, leftMP, rightOP, rightMP;
            double angle = CaculateAngleByTwoCamera(out leftOP, out leftMP, out rightOP, out rightMP);
            double defaultAngle = cameraInformations.DefaultAngle;
            double deltaAngle = 0;
            if (angle * defaultAngle < 0)
            {
                if (angle < 0)
                {
                    deltaAngle = 360 + angle - defaultAngle;
                }
                else
                {
                    deltaAngle = 360 + defaultAngle - angle;
                }

            }
            else if (angle * defaultAngle > 0)
            {
                deltaAngle = angle - defaultAngle;
            }

            Point3D args1 = cameraInformations[0].CaculationXYT(leftOP, leftMP, deltaAngle);
            Point3D args2 = cameraInformations[1].CaculationXYT(rightOP, rightMP, deltaAngle);
            textBox1.Text = string.Format("1 X:{0} Y:{1} Theta:{2} \r\n", args1.X, args1.Y, args1.Z) + textBox1.Text;
            textBox1.Text = string.Format("2 X:{0} Y:{1} Theta:{2} \r\n", args2.X, args2.Y, args2.Z) + textBox1.Text;
            textBox1.Text = string.Format("Total X:{0} Y:{1} Theta:{2} \r\n", (args1.X + args2.X) / 2, (args1.Y + args2.Y) / 2, args2.Z) + textBox1.Text;

            var cfg = Global.Setting.ListGroup[CurrentGroupIndex];
            if (cfg != null)
            {

                IMoveExecutor executor = cfg.MoveExecutorType == MoveExecutorType.XYTPlatform ?
                  new XytMoveExecutor(PosPlatform.GetPlatform(cfg.GroupName)) :
                  (IMoveExecutor)PosSignal.GetSignalProvider();
                double x = (args1.X + args2.X) / 2;
                double y = (args1.Y + args2.Y) / 2;
                double[] xyt = new double[3] { x, y, deltaAngle };
                int[] res = new int[3] { 3, 0, 0 };
                
                executor.Move(CurrentGroupIndex, xyt, res);
            }
        }

      
        /// <summary>
        /// 拍照请求
        /// </summary>
        public event EventHandler<GrabEventArgs> GrabRequest;
        /// <summary>
        /// 实时请求
        /// </summary>
        public event EventHandler<LiveEventArgs> LiveRequest;

        private ShowLiveImage method;
        private void chkLive_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void CameraLive(bool isSetLive,int startIndex)
        {
            if (GroupConfig != null)
            {
                //获取Mac地址
                var lstMacAddr = GroupConfig.ListCamera.Select(p => p.MacAddr).ToList();
                for (int i = 0; i < lstMacAddr.Count; i++)
                {
                    int index = i;
                    //获取相机的序号
                    int cameraIndex = PosCamera.GetCameraIndex(lstMacAddr[i]);
                    if (cameraIndex < 0) continue;
                    if (cbxOffLine.Checked&&isSetLive)
                    {
                        OpenFileDialog ofd = new OpenFileDialog();
                        Bitmap img = null;
                        if (ofd.ShowDialog() == DialogResult.OK)
                        {
                            img = Image.FromFile(ofd.FileName) as Bitmap;
                        }
                        drawToolControls[index + startIndex].SetBackgroundImage(img);
                    }
                    else
                    {
                        //显示图像的委托
                        method = new ShowLiveImage(data =>
                        {
                            Bitmap img = data?.ToBitmap();
                            //显示图像
                            drawToolControls[index + startIndex].SetBackgroundImage(img);
                        });
                        //切换Live模式
                        PosCamera.Live(cameraIndex, isSetLive, method);
                    }
                }
            }
        }
        private void btnGrab_Click(object sender, EventArgs e)
        {
            //查找配置信息
            if (GroupConfig != null)
            {
                //获取Mac地址
                var lstMacAddr = GroupConfig.ListCamera.Select(p => p.MacAddr).ToList();
                for (int i = 0; i < lstMacAddr.Count; i++)
                {
                    int index = i;
                   
                        //获取相机的序号
                        int cameraIndex = PosCamera.GetCameraIndex(lstMacAddr[index]);
                        if (cameraIndex < 0) return;
                        //拍照
                        Bitmap img =null;
                        if (cbxOffLine.Checked)
                        {

                            OpenFileDialog ofd = new OpenFileDialog();
                            if (ofd.ShowDialog()==DialogResult.OK)
                            {
                                img = Image.FromFile(ofd.FileName) as Bitmap;
                            }
                        }
                        else
                        {
                            var data = PosCamera.Grab(cameraIndex);
                            img = data?.ToBitmap();
                        }
                        //显示图像
                        drawToolControls[index].SetBackgroundImage(img);
       
                }
            }
        }

        private enum CameraModeType
        {
            None = 0,
            Live,
            Grab,
        }
        private CameraModeType selectCameraModeType = CameraModeType.None;

        private enum SystemModeType
        {
            None = 0,
            Setting,
            Test,
        }
        private SystemModeType selectSystemModeType = SystemModeType.None;
        private enum MarkAreaType
        {
            None = 0,
            Mark,
            Area,
        }
        private MarkAreaType selectMarkAreaType = MarkAreaType.None;


        private enum CameraSource
        {
            None,
            Camera1,
            Camera2,
        }
        private CameraSource selectCameraSource = CameraSource.None;
        private void radioButtonMode_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton btn = (RadioButton)sender;
            btn.BackColor = btn.Checked ? Color.SkyBlue : SystemColors.Control;
            bool value = btn.Checked;
            if (btn == radioButtonSetting)
            {
                selectSystemModeType = SystemModeType.Setting;
            }
            else {
                selectSystemModeType = SystemModeType.Test;
            }
            if (selectSystemModeType == SystemModeType.Setting)
            {
                radioButtonMark.Enabled = true;
                radioButtonArea.Enabled = true;
                radioButtonMark.Checked = true;
            }
            else {
                radioButtonMark.Checked = false;
                radioButtonArea.Checked = false;
                radioButtonMark.Enabled = false;
                radioButtonArea.Enabled = false;
            }
            if (value)
            {
                CameraLive(this.radioButtonCameraModeLive.Checked, selectSystemModeType == SystemModeType.Test ? 0 : 2);
            }
         
        }

        private void radioButtonCameraMode_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton btn = (RadioButton)sender;
            bool value = btn.Checked;
            btn.BackColor = btn.Checked ? Color.SkyBlue : SystemColors.Control;
            if (value)
            {
                CameraLive(this.radioButtonCameraModeLive.Checked, selectSystemModeType == SystemModeType.Test ? 0 : 2);
            }
        }

        private void radioButtonCamera_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton btn = (RadioButton)sender;
            bool value = btn.Checked;
            if (value == true)
            {
                selectCameraSource = btn == radioButtonCamera1 ? CameraSource.Camera1 : CameraSource.Camera2;
            }
            btn.BackColor = btn.Checked ? Color.SkyBlue : SystemColors.Control;
        }

       
        private void radioButtonMark_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton btn = (RadioButton)sender;
            bool value = btn.Checked;
            if (value == true)
            {
                selectMarkAreaType = btn == radioButtonMark ? MarkAreaType.Mark : MarkAreaType.Area;
            }
            btn.BackColor = btn.Checked ? Color.SkyBlue : SystemColors.Control;
            this.drawToolControl3.EnableDraw(true);
            this.drawToolControl4.EnableDraw(true);
            this.drawToolControl3.ActiveRoiType = (int)selectMarkAreaType;
            this.drawToolControl4.ActiveRoiType = (int)selectMarkAreaType;
        }

     
    }
}
