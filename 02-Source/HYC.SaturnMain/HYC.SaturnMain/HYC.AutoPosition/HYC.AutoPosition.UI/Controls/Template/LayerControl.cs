/*----------------------------------------------------------------
// Copyright (C) Suzhou HYC Electronic Technology Co.,LTD
// 版权所有。
//
// =================================
// CLR版本     ：4.0.30319.42000
// 命名空间    ：CarrierMeasurement.SubForm
// 文件名称    ：LayerControl.cs
// =================================
// 创 建 者    ：miaoliang
// 创建日期    ：2019/5/4 14:20:36 
// 功能描述    ：自定义实现图层功能
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
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System;

namespace HYC.AutoPosition.UI.Controls
{
    public partial class LayerControl : UserControl
    {
        private Image image;
        private Graphics graphics;
        public LayerControl(int width, int height,string name)
        {
            this.Name = name;
            this.Width = width;
            this.Height = height;
            //建一个新的指定大小的位图
            image = new Bitmap(width, height);
            graphics = Graphics.FromImage(image);
            // 设置空间风格为:双缓冲|减少闪烁|用户绘制
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        /// <summary>
        /// 获取当前Image尺寸
        /// </summary>
        /// <returns></returns>
        public Size ImageSize()
        {
            return this.Image.Size;
        }
        // 重写OnPaint，每当绘画时发生
        protected override void OnPaint(PaintEventArgs e)
        {
            //做一张背景透明的位图当做绘制内容
            //若已有Image属性被指定，则画该图
            //由于e的绘制区域总是在变，这里不需要考虑绘制区域，系统会自己算，超出区域的不予以绘制
            //简单刷原理就是，每当系统要绘制时，先画背景，我们自行处理就需要计算应该画哪个区域（多数都是不规则的区域并集）
            //每当OnPaint时，绘制自己的Image图（若属性存在），或者是纯透明的位图以提供完全透明效果
           // var bitMap = new Bitmap(image);
           // bitMap.MakeTransparent(Color.White);
           // image = bitMap;
            //绘制模式指定
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.CompositingQuality = CompositingQuality.GammaCorrected;
            //透明色变换
            float[][] mtxItens = {
            new float[] {1,0,0,0,0},
            new float[] {0,1,0,0,0},
            new float[] {0,0,1,0,0},
            new float[] {0,0,0,1,0},
            new float[] {0,0,0,0,1}};
            ColorMatrix colorMatrix = new ColorMatrix(mtxItens);
            ImageAttributes imgAtb = new ImageAttributes();
            imgAtb.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            //画
            g.DrawImage(image, ClientRectangle, 0.0f, 0.0f, image.Width, image.Height, GraphicsUnit.Pixel, imgAtb);
        }
        //控件背景绘制
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            Graphics g = e.Graphics;
            //以下代码提取控件原来应有的背景（视觉上的位图，可能会包括其他空间的部分图形）到位图，进行变换后绘制
            if (Parent != null)
            {
                BackColor = Color.Transparent;
                //本控件在父控件的子集中的index
                int index = Parent.Controls.GetChildIndex(this);
                for (int i = Parent.Controls.Count - 1; i > index; i--)
                {
                    //对每一个父控件的可视子控件，进行绘制区域交集运算，得到应该绘制的区域
                    Control c = Parent.Controls[i];
                    //如果有交集且可见
                    if (c.Bounds.IntersectsWith(Bounds) && c.Visible)
                    {
                        //矩阵变换
                        Bitmap bmp = new Bitmap(c.Width, c.Height, g);
                        c.DrawToBitmap(bmp, c.ClientRectangle);
                        g.TranslateTransform(c.Left - Left, c.Top - Top);
                        //画图
                        g.DrawImageUnscaled(bmp, Point.Empty);
                        g.TranslateTransform(Left - c.Left, Top - c.Top);
                        bmp.Dispose();
                    }
                }
            }
            else
            {
                g.Clear(Parent.BackColor);
                g.FillRectangle(new SolidBrush(Color.FromArgb(255, Color.Transparent)), this.ClientRectangle);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="color"></param>
        public void Clear(Color color)
        {
            this.graphics.Clear(color);
            this.Invalidate();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public SizeF RealSize()
        {
            return this.graphics.VisibleClipBounds.Size;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pen"></param>
        /// <param name="rect"></param>
        public void DrawRectangle(Pen pen,Rectangle rect)
        {

            this.graphics.DrawRectangle(pen, rect);
            this.Invalidate();
        }


        /// <summary>
        /// 绘制带顶角的Rect
        /// </summary>
        /// <param name="pen"></param>
        /// <param name="rect"></param>
        public void DrawRectangleCorner(Pen pen, Rectangle rect)
        {
            this.graphics.DrawRectangle(pen, rect);
            this.Invalidate();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pen"></param>
        /// <param name="sp"></param>
        /// <param name="ep"></param>
        public void DrawLine(Pen pen, Point sp,Point ep)
        {
            //pen.DashStyle = DashStyle.Dash;
            this.graphics.DrawLine(pen,sp, ep);
            this.Invalidate();
        }

        /// <summary>
        /// 绘画十字线
        /// </summary>
        /// <param name="pen"></param>
        public void DrawCenterLine(Pen pen)
        {
            SizeF size = this.graphics.VisibleClipBounds.Size;
            pen.DashStyle = DashStyle.DashDotDot;
            this.graphics.DrawLine(pen, new PointF(0,size.Height/2), new PointF(size.Width, size.Height / 2));
            this.graphics.DrawLine(pen, new PointF(size.Width/2, 0), new PointF(size.Width/2, size.Height));
            this.Invalidate();
        }


        /// <summary>
        /// 绘画十字交叉线线
        /// </summary>
        /// <param name="pen"></param>
        /// <param name="centerPoint">中心点</param>
        public void DrawCrossLine(Pen pen,Point centerPoint)
        {
            SizeF size = this.graphics.VisibleClipBounds.Size;
            pen.DashStyle = DashStyle.DashDotDot;
            this.graphics.DrawLine(pen, new PointF(0, centerPoint.Y), new PointF(size.Width, centerPoint.Y));
            this.graphics.DrawLine(pen, new PointF(centerPoint.X, 0), new PointF(centerPoint.X, size.Height));
            this.Invalidate();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pen"></param>
        /// <param name="sp"></param>
        /// <param name="ep"></param>
        public void DrawLine(Pen pen, PointF sp, PointF ep)
        {
            this.graphics.DrawLine(pen, sp, ep);
            this.Invalidate();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="font"></param>
        /// <param name="brush"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void DrawString(string s, Font font, Brush brush, float x, float y)
        {
            this.graphics.DrawString( s,  font,  brush,  x,  y);
            this.Invalidate();
        }
        /// <summary>
        /// 
        /// </summary>
        //Image属性，每当赋值会引发Invalidate
        public Image Image
        {
            get
            {
                return image;
            }
            set
            {
                image = value;
                this.Invalidate();
            }
        }
    }
}
