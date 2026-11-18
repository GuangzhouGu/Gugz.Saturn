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

using HYC.HTPCBase.Extension.Entities;
using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using HYC.MicroLedATDetect.Devices.HTCameras;

namespace HYC.Mt2Aoi.Controls
{
    /// <summary>
    /// Mil相机控制
    /// </summary>
    public partial class HTCameraCtrl : UserControl
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public HTCameraCtrl()
        {
            InitializeComponent();
        }
        MicroLedATDetect.Devices.HTCameras.HTCamera Camera = null;
        /// <summary>
        /// 初始化
        /// </summary>
        public void InitCtrl()
        {
            for (int i = 0; i < HTCameraHelper.Instance.CameraCount; i++)
            {
                this.cboCamera.Items.Add("相机" + (i + 1));
            }
            this.cboCamera.SelectedIndex = 0;
        }

        /// <summary>
        /// 拍照
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGrab_Click(object sender, EventArgs e)
        {
            int index = this.cboCamera.SelectedIndex;
            this.imageBox1.Image = null;
            this.btnGrab.Enabled = false;
            Task.Factory.StartNew(() =>
            {
                try
                {
                    // 拍照
                    var img=HTCameraHelper.Instance.Grab(index,1000);
                    // 清除界面显示
                    this.Invoke(new Action(() =>
                    {
                        this.imageBox1.Image = img;
                    }));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    this.Invoke(new Action(() =>
                    {
                        this.btnGrab.Enabled = true;
                    }));
                }
            });
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
                double result = HTCameraHelper.Instance.GetExposure(index);
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
                HTCameraHelper.Instance.SetExposure(index, value);
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
               // double result = CameraHelper.Instance.GetCameraByNum(index).GetGain();
               // this.txtGain.Text = result.ToString();
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
               //double value = Convert.ToDouble(this.txtGain.Text);
              //  CameraHelper.Instance.GetCameraByNum(index).SetGain(value); 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UcGigECamera_Load(object sender, EventArgs e)
        {
            if (ClassHelper.IsDesingMode()==false)
            {
                InitCtrl();
            }
        }

        private void btnContinues_Click(object sender, EventArgs e)
        {
            Camera.Continues();
        }
        private void ShowImage(Bitmap image)
        {
            if (this.Visible)
            {
                //if (InvokeRequired)
                //{
                //    imageBox1.Invoke(new Action<Bitmap>(ShowImage), image);
                //}
                //else
                {
                    imageBox1.Image = image;
                }
            }
          
        }

        private void cboCamera_SelectedIndexChanged(object sender, EventArgs e)
        {
            Camera = HTCameraHelper.Instance.GetCamera(cboCamera.SelectedIndex);
            if (Camera!=null)
            {
                Camera.OnImageReceived += ShowImage;
            }
        
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            Camera = HTCameraHelper.Instance.GetCamera(cboCamera.SelectedIndex);
            if (Camera!=null)
            {
                Camera.ManualInit();
            }
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            try
            {
                Camera = HTCameraHelper.Instance.GetCamera(cboCamera.SelectedIndex);
                if (Camera != null)
                {
                    Camera.UnInit();
                }
                else
                {
                    MessageBox.Show("Camera对象为空"); ;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString()); ;
            }
        }

        private void btnStopAcq_Click(object sender, EventArgs e)
        {
            Camera = HTCameraHelper.Instance.GetCamera(cboCamera.SelectedIndex);
            if (Camera != null)
            {
                Camera.Stop();
            }
            else
            {
                MessageBox.Show("Camera对象为空"); ;
            }
        }
    }
}
