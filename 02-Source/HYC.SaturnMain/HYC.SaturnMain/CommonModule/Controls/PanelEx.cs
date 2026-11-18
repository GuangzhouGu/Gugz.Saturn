using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CommonModule.Controls
{
    public partial class PanelEx : Panel
    {
        public PanelEx()
        {
            InitializeComponent();
        }

        private Color _BorderColor = Color.FromArgb(151, 151, 151);

        [Browsable(true), Description("边框颜色"), Category("自定义分组")]
        public Color BorderColor
        {
            get { return _BorderColor; }
            set
            {
                _BorderColor = value;
                this.Invalidate();
            }
        }
        private Color fillColor = SystemColors.Control;

        [Browsable(true), Description("填充颜色"), Category("自定义分组")]
        public Color FillColor
        {
            get { return fillColor; }
            set
            {
                fillColor = value;
                this.Invalidate();
            }
        }
        private int _BorderSize = 1;

        [Browsable(true), Description("边框粗细"), Category("自定义分组")]
        public int BorderSize
        {
            get { return _BorderSize; }
            set
            {
                _BorderSize = value;
                this.Invalidate();
            }
        }
        private int roundCorner = 4;
        [Category("HYCControls"), DefaultValue(4), Description("按钮框的圆角属性")]
        public int CornerRadius
        {
            get
            {
                return this.roundCorner;
            }
            set
            {
                this.roundCorner = value;
                base.Invalidate();
            }
        }
        /// <summary>
        /// 重写OnPaint方法
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            GraphicsPath graphicsPath = new GraphicsPath();
            graphicsPath.AddLine(this.roundCorner, 0, base.Width - this.roundCorner - 1, 0);
            graphicsPath.AddArc(base.Width - this.roundCorner * 2 - 1, 0, this.roundCorner * 2, this.roundCorner * 2, 270f, 90f);
            graphicsPath.AddLine(base.Width - 1, this.roundCorner, base.Width - 1, base.Height - this.roundCorner - 1);
            graphicsPath.AddArc(base.Width - this.roundCorner * 2 - 1, base.Height - this.roundCorner * 2 - 1, this.roundCorner * 2, this.roundCorner * 2, 0f, 90f);
            graphicsPath.AddLine(base.Width - this.roundCorner - 1, base.Height - 1, this.roundCorner, base.Height - 1);
            graphicsPath.AddArc(0, base.Height - this.roundCorner * 2 - 1, this.roundCorner * 2, this.roundCorner * 2, 90f, 90f);
            graphicsPath.AddLine(0, base.Height - this.roundCorner - 1, 0, this.roundCorner);
            graphicsPath.AddArc(0, 0, this.roundCorner * 2, this.roundCorner * 2, 180f, 90f);
            Pen pen = new Pen(BorderColor);
            Brush brush = new SolidBrush(FillColor);
            e.Graphics.FillPath(brush, graphicsPath);
            e.Graphics.DrawPath(pen, graphicsPath);
            pen.Dispose();
            graphicsPath.Dispose();
            base.OnPaint(e);
            //ControlPaint.DrawBorder(e.Graphics,
            //               this.ClientRectangle,
            //               this._BorderColor,
            //               this._BorderSize,
            //               ButtonBorderStyle.Solid,
            //               this._BorderColor,
            //               this._BorderSize,
            //               ButtonBorderStyle.Solid,
            //              this._BorderColor,
            //               this._BorderSize,
            //               ButtonBorderStyle.Solid,
            //               this._BorderColor,
            //               this._BorderSize,
            //               ButtonBorderStyle.Solid);
        }

    }
}
