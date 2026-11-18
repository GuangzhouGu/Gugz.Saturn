using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HYC.AutoPosition.UI.Controls
{
    /// <summary>
    /// 导航按钮
    /// </summary>
    public partial class NavigateButton : RadioButton
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public NavigateButton()
        {
            InitializeComponent();
            this.AutoSize = false;
            this.Appearance = Appearance.Button;
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
        }

        /// <summary>
        /// 重绘
        /// </summary>
        /// <param name="pe"></param>
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            //选中状态下，添加指示条
            if (Checked)
            {
                Brush brush = new SolidBrush(Color.FromArgb(0, 120, 215));
                Rectangle rect = new Rectangle(0, 3, 3, this.Height - 6);
                pe.Graphics.FillRectangle(brush, rect);
            }
        }
    }
}
