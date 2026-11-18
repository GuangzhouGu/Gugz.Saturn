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

using CommonLib.Entities;
using HYC.SaturnAoi.Devices.MuraCamera;
using HYC.StandardAoi.Parameter;
using HYC.StandardAoi.UI;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HYC.Mt2Aoi.Controls
{
    /// <summary>
    /// Mil相机控制
    /// </summary>
    public partial class UcGigECamera : UserControl
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public UcGigECamera()
        {
            InitializeComponent();
        }
        CameraControl Camera = null;
        /// <summary>
        /// 初始化
        /// </summary>
        public void InitCtrl()
        {
            this.cboCamera.Items.Clear();
            if (CameraHelper.Instance!=null)
            {
                for (int i = 0; i < CameraHelper.Instance.CameraCount; i++)
                {
                    this.cboCamera.Items.Add("相机" + (i + 1));
                }
                this.cboCamera.SelectedIndex = 0;
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
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    var bitmap = Image.FromFile(ofd.FileName) as Bitmap;
                    data = ImageData.ConvertFromBitmap(bitmap, false);
                }
            }
            else
            {
                int index = this.cboCamera.SelectedIndex;
                CameraHelper.Instance.GetCamera(index).Grab(3000, out Bitmap img);
                data = ImageData.ConvertFromBitmap(img);
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
                float result = CameraHelper.Instance.GetCamera(index).GetExposureTime();
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
                CameraHelper.Instance.GetCamera(index).SetExposureTime(value);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 获取增益
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
               double result = CameraHelper.Instance.GetCamera(index).GetGain();
                this.txtGain.Text = result.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 设置增益
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetGain_Click(object sender, EventArgs e)
        {
            int index = this.cboCamera.SelectedIndex;
            try
            {
               double value = Convert.ToDouble(this.txtGain.Text);
                CameraHelper.Instance.GetCamera(index).SetGain(value); 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UcGigECamera_Load(object sender, EventArgs e)
        {
       
        }
   
        private void btnContinues_Click(object sender, EventArgs e)
        {
            Camera.Continues();
        }

        private void cboCamera_SelectedIndexChanged(object sender, EventArgs e)
        {
            Camera = CameraHelper.Instance.GetCamera(cboCamera.SelectedIndex);
            if (Camera != null)
            {
                Camera.OnImageReceived += Camera_OnImageReceived;
            }
        }

        private void Camera_OnImageReceived(Bitmap arg2)
        {
            pictureBox1.Image = arg2;
        }

        private void btnSaveImg_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            if (sfd.ShowDialog()==DialogResult.OK)
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

        private void btnGrayTtest_Click(object sender, EventArgs e)
        {
            try
            {
                var isBlack = cbxIsBlack.Checked;
                Task.Factory.StartNew(() => {
                    this.Invoke(new Action(() => {
                        int[] aoi = new int[100];
                        var data = ImageData.ConvertFromBitmap(this.pictureBox1.Image as Bitmap);
                        new FormStandardAoi().GetManager().GrayTest(data.ImageBytes, data.Height, data.Width, isBlack,
                                                     out byte[] dataOut, out aoi, out double gray);

                        txtGrayValue.Text =Math.Round( gray,3).ToString();
                        if (aoi != null)
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
        }

        private void UcGigECamera_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                if (ClassHelper.IsDesingMode() == false)
                {
                    InitCtrl();
                }
            }
        }
    }
}
