/*----------------------------------------------------------------
// Copyright © 2021 HYC. All Rights
// 版权所有。
//
// =================================
// CLR版本     ：4.0.30319.42000
// 命名空间    ：HYC.Mt2Aoi.Controls
// 文件名称    ：UcMilCamera.cs
// =================================
// 创 建 者    ：gugz
// 创建日期    ：2022/1/15 10:51:38 
// 功能描述    ：
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
using HYC.HTImage;
using HYC.StandardAoi.Algorithm;
using HYC.StandardAoi.Parameter;
using HYC.StandardAoi.UI;
using HYC.TPApiUnit.Devices.AoiCamera;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HYC.Mt2Aoi.Controls
{
    /// <summary>
    /// Mil相机控制
    /// </summary>
    public partial class UcMilCamera : UserControl
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public UcMilCamera()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void InitCtrl()
        {
            try
            {
                if (CaptureCardHelper.Instance!=null)
                {
                    this.cboCamera.Items.Clear();
                    for (int i = 0; i < CaptureCardHelper.Instance.CaptureCardCount; i++)
                    {
                        this.cboCamera.Items.Add("相机" + (i + 1));
                    }
                    this.cboCamera.SelectedIndex = 0;
                }
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// 拍照
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGrab_Click(object sender, EventArgs e)
        {
            ImageData data = null;
            if (cbxIsReadLocalImage.Checked)
            {
                var ofd = new OpenFileDialog();
                if (ofd.ShowDialog()==DialogResult.OK)
                {
                    var bitmap = Image.FromFile(ofd.FileName) as Bitmap;
                    data = ImageData.ConvertFromBitmap(bitmap,false);
                }
            }
            else
            {
                int index = this.cboCamera.SelectedIndex;
                data = CaptureCardHelper.Instance.GetCard(0).Grab2d(index);
            }
            this.pictureBox1.Image = data.ToBitmap();
        }

        /// <summary>
        /// 获取曝光时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetExposure_Click(object sender, EventArgs e)
        {
            int index = this.cboCamera.SelectedIndex;
            try
            {
                // 清除界面显示
                this.txtExposure.Text = string.Empty;
                // 获取
                var result = CaptureCardHelper.Instance.GetCard(0).GetExposureTime(index);
                this.txtExposure.Text = result.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 设置曝光时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetExposure_Click(object sender, EventArgs e)
        {
            int index = this.cboCamera.SelectedIndex;
            try
            {
                float value = Convert.ToSingle(this.txtExposure.Text);
                CaptureCardHelper.Instance.GetCard(0).SetExposureTime(index, value);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 获取相机增益
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetGain_Click(object sender, EventArgs e)
        {
            int index = this.cboCamera.SelectedIndex;
            try
            {
                // 清除界面显示
                this.txtGain.Text = string.Empty;
                // 获取
                double result = CaptureCardHelper.Instance.GetCard(0).GetGain(index);
                this.txtGain.Text = result.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 设置相机增益
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetGain_Click(object sender, EventArgs e)
        {
            int index = this.cboCamera.SelectedIndex;
            try
            {
                double value = Convert.ToDouble(this.txtGain.Text);
              var ret=  CaptureCardHelper.Instance.GetCard(0).SetGain(index, value);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

 
        /// <summary>
        /// 查看该枚产品所有画面灰度，生成到execl表中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void htbGray_Click(object sender, EventArgs e)
        {
            try
            {
                var isBlack = cbxIsBlack.Checked;
                Task.Factory.StartNew(()=> {
                    this.Invoke(new Action(()=> {
                        int[] aoi = new int[100];
                        var data =ImageData.ConvertFromBitmap( pictureBox1.Image as Bitmap);
                        new FormStandardAoi().GetManager().GrayTest(data.ImageBytes, data.Height, data.Width, isBlack,
                                                     out byte[] dataOut, out  aoi, out double gray);

                        txtGrayValue.Text =Math.Round(gray,3).ToString();
                        if (aoi!=null)
                        {
                            txtRoiRegion.Text = string.Join(",", aoi);
                        }

                    }));
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            //FolderBrowserDialog dialog = new FolderBrowserDialog();
            //dialog.Description = "请选择要测试的产品文件夹";
            //if (dialog.ShowDialog() == DialogResult.OK)
            //{
            //    try
            //    {
            //        var list = new string[] { };
            //        list = Directory.GetFiles(dialog.SelectedPath);
            //        string pictrueName = string.Empty;
            //        string grayNumber = string.Empty;

            //        Task.Factory.StartNew(() =>
            //        {
            //            for (int j = 0; j < list.Length; j++)
            //                {
            //                    bool isBlack = false;
            //                    int width = 0, height = 0;
            //                    var bitmap = (Bitmap)Image.FromFile(list[j]);

            //                    Match match = Regex.Match(list[j], @".+\\(.+)");
            //                    string fileName = match.Groups[1].Value;
            //                    if (fileName == "Black.bmp")
            //                    {
            //                        isBlack = true;
            //                    }
            //                    Bitmap bitmapA = BitmapConverter.Gray(bitmap);
            //                    byte[] data = BitmapConverter.BitmapToArray(bitmapA, out height, out width);

            //                    new FormStandardAoi().GetManager().GrayTest(data, height, width, isBlack,
            //                             out byte[] dataOut, out int[] aoi, out double gray);
            //                    pictrueName += fileName + ",";
            //                    grayNumber += gray + ",";
            //                }
            //            string folder=$"E:\\{DateTime.Now.ToString("yyyyMMdd")}_GRAY\\";
            //            if (!Directory.Exists(folder))
            //                Directory.CreateDirectory(folder);
            //            string fullPath = folder + "Gray.execl.csv";
            //            if (!File.Exists(fullPath))
            //            {
            //                File.WriteAllText(fullPath, pictrueName + "\r\n");
            //                File.AppendAllText(fullPath, grayNumber + "\r\n");
            //            }  
            //            else
            //                File.AppendAllText(fullPath, grayNumber + "\r\n");

            //            MessageBox.Show("生成成功:"+ fullPath);

            //        });

            //    }
            //    catch (Exception ex )
            //    {
            //        MessageBox.Show("异常错误："+ex.ToString());
            //    }
                
               
            //}
        }

        private void UcMilCamera_Load(object sender, EventArgs e)
        {
          
        }
        bool IsLiving = false;

        private void btnLive_Click(object sender, EventArgs e)
        {
            int index = cboCamera.SelectedIndex;
            if (index < 0) return;
            if (IsLiving==false)
            {
                CaptureCardHelper.Instance.GetCard(0).Live(index, true);
            }
            else
            {
                CaptureCardHelper.Instance.GetCard(0).Live(index, false);
            }
            IsLiving = !IsLiving;
        }

        private void UcMilCamera_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                InitCtrl();
            }
         
        }

        private void cboCamera_SelectedIndexChanged(object sender, EventArgs e)
        {
            var camera = CaptureCardHelper.Instance.GetCard(0);
            if (camera != null)
            {
                camera.OnImageReceived += Camera_OnImageReceived;
            }
        }

        private void Camera_OnImageReceived(int arg1, ImageData arg2)
        {
            pictureBox1.Image = arg2.ToBitmap();
        }

        private void btnSaveImg_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                if (string.IsNullOrEmpty(sfd.FileName) == false)
                {
                    pictureBox1.Image.Save(sfd.FileName + ".bmp");
                }
            }
        }
        Graphics Graphics = null;
        private void DrawCrossLine(PaintEventArgs e)
        {
            try
            {
                Graphics = e.Graphics;
                GrawLine();
            }
            catch (System.Exception)
            {

            }
        }

        private void GrawLine()
        {
            Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            Graphics.CompositingQuality = CompositingQuality.GammaCorrected;
            //画

            SizeF size = Graphics.VisibleClipBounds.Size;
            Pen pen = new Pen(Color.Lime, 0.5f);
            pen.DashStyle = DashStyle.Dot;
            Graphics.DrawLine(pen, new PointF(0, size.Height / 2), new PointF(size.Width, size.Height / 2));
            Graphics.DrawLine(pen, new PointF(size.Width / 2, 0), new PointF(size.Width / 2, size.Height));
        }

        private void imageBox1_Paint(object sender, PaintEventArgs e)
        {
            DrawCrossLine(e);
        }
    }
}
