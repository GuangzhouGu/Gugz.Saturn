/*----------------------------------------------------------------
// Copyright (C) Suzhou HYC Electronic Technology Co.,LTD
// 版权所有。
//
// =================================
// CLR版本     ：4.0.30319.42000
// 命名空间    ：HYC.AutoPosition.UI.Controls
// 文件名称    ：ImageViewBox.cs
// =================================
// 创 建 者    ：yangkang
// 创建日期    ：2020/1/2 14:10:44 
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
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace HYC.AutoPosition.UI.Controls
{
    /// <summary>
    /// 图像显示控件
    /// </summary>
    public partial class ImageViewBox : UserControl
    {
        private int cameraCount = 2;
        /// <summary>
        /// 一组对位中的相机数量
        /// </summary>
        public int CameraCount
        {
            get { return cameraCount; }
            set
            {
                if (cameraCount != value)
                {
                    cameraCount = value;
                    if (cameraCount == 1)
                        this.tbpnlMain.ColumnCount = 1;
                    else
                        this.tbpnlMain.ColumnCount = 3;
                }
            }
        }

        /// <summary>
        /// 获取控件中的图像
        /// </summary>
        public Image[] Images
        {
            get
            {
                return new Image[] { this.pictImg1.Image, this.pictImg2.Image };
            }
        }

        /// <summary>
        /// 组的名称
        /// </summary>
        public string GroupName
        {
            get { return this.lblGroupName.Text; }
            set
            {
                this.lblGroupName.Text = value;

                if (string.IsNullOrEmpty(this.lblGroupName.Text))
                {
                    this.chkLive.Enabled = false;
                    this.btnGrab.Enabled = false;
                }
                else
                {
                    this.chkLive.Enabled = true;
                    this.btnGrab.Enabled = true;
                }
            }
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        public ImageViewBox()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 显示图像
        /// </summary>
        /// <param name="imgs">图像数据数组</param>
        public void ShowImage(Image[] imgs)
        {
            //if (this.InvokeRequired)
            //{
            //    this.Invoke(new Action<Image[]>(ShowImage), imgs);
            //}
            //else
            {
                this.pictImg1.Image = null;
                this.pictImg2.Image = null;

                if (imgs != null)
                {
                    this.pictImg1.Image = imgs.Length > 0 ? imgs[0] : null;
                    this.pictImg2.Image = imgs.Length > 1 ? imgs[1] : null;
                }
            }
        }

        /// <summary>
        /// 显示图像
        /// </summary>
        /// <param name="index">序号</param>
        /// <param name="img">图像</param>
        public void ShowImage(int index, Image img)
        {
            if (this.InvokeRequired&&this.IsDisposed==false)
            {
                this.Invoke(new Action<int, Image>(ShowImage), index, img);
            }
            else
            {
                if (index == 0)
                {
                    this.pictImg1.Image = img;
                }
                else
                {
                    this.pictImg2.Image = img;
                }
            }
        }

        /// <summary>
        /// 显示XYT值
        /// </summary>
        /// <param name="xyt">xyt运动数据</param>
        public void ShowXYT(double[] xyt)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<double[]>(ShowXYT), xyt);
            }
            else
            {
                if (xyt != null)
                {
                    this.lblMoveX.Text = xyt[0].ToString();
                    this.lblMoveY.Text = xyt[1].ToString();
                    this.lblMoveT.Text = xyt[2].ToString();

                    if (xyt.Length > 4)
                    {
                        this.lblX1.Text = xyt[4].ToString();
                        this.lblY1.Text = xyt[5].ToString();
                        this.lblT1.Text = xyt[6].ToString();

                        this.lblX2.Text = xyt[8].ToString();
                        this.lblY2.Text = xyt[9].ToString();
                        this.lblT2.Text = xyt[10].ToString();
                    }
                }
            }
        }

        /// <summary>
        /// 拍照
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGrab_Click(object sender, EventArgs e)
        {
            GrabEventArgs args = new GrabEventArgs();
            args.GroupName = GroupName;
            GrabRequest?.Invoke(this, args);
        }

        /// <summary>
        /// 实时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkLive_CheckedChanged(object sender, EventArgs e)
        {
            //设置按钮状态
            this.btnGrab.Enabled = !this.chkLive.Checked;
            this.chkLive.BackColor = this.chkLive.Checked ? Color.SkyBlue : SystemColors.Control;

            LiveEventArgs args = new LiveEventArgs();
            args.GroupName = GroupName;
            args.IsSetToLive = this.chkLive.Checked;
            LiveRequest?.Invoke(this, args);
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
            pen.DashStyle = DashStyle.Solid;
            Graphics.DrawLine(pen, new PointF(0, size.Height / 2), new PointF(size.Width, size.Height / 2));
            Graphics.DrawLine(pen, new PointF(size.Width / 2, 0), new PointF(size.Width / 2, size.Height));
        }
        /// <summary>
        /// 拍照请求
        /// </summary>
        public event EventHandler<GrabEventArgs> GrabRequest;
        /// <summary>
        /// 实时请求
        /// </summary>
        public event EventHandler<LiveEventArgs> LiveRequest;

        private void pictImg1_Paint(object sender, PaintEventArgs e)
        {
            DrawCrossLine(e);
        }
    }
}
