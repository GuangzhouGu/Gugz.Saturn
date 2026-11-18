/*----------------------------------------------------------------
// Copyright (C) Suzhou HYC Electronic Technology Co.,LTD
// 版权所有。
//
// =================================
// CLR版本     ：4.0.30319.42000
// 命名空间    ：HYC.AutoPosition.UI.Controls
// 文件名称    ：RecipeListBox.cs
// =================================
// 创 建 者    ：yangkang
// 创建日期    ：2020/2/17 13:07:25 
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
using HYC.AutoPosition.Recipes;
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
    /// 机种显示列表框
    /// </summary>
    public partial class RecipeListBox : ListBox
    {
        /// <summary>
        /// 选定项背景色
        /// </summary>
        [Description("选定项背景色"), Category("其他")]
        public Color SelectionBackColor { get; set; } = Color.LightBlue;

        /// <summary>
        /// 构造方法
        /// </summary>
        public RecipeListBox()
        {
            InitializeComponent();
            this.DrawMode = DrawMode.OwnerDrawFixed;
            this.DoubleBuffered = true;
            this.ItemHeight = 32;
        }

        /// <summary>
        /// 重绘
        /// </summary>
        /// <param name="pe"></param>
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        /// <summary>
        /// 重绘列表项
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            base.OnDrawItem(e);

            if (e.Index < 0) return;
            if (this.Items.Count <= 0) return;
            // 为每一项绘制背景
            if ((e.State & DrawItemState.Selected) > 0)
            {
                //选中时，填充选中色
                e.Graphics.FillRectangle(new SolidBrush(SelectionBackColor), e.Bounds);
            }
            else
            {
                //未选中时，填充背景色
                e.DrawBackground();
            }

            Rectangle rect = e.Bounds;
            Graphics g = e.Graphics;

            var rep = (Recipe)this.Items[e.Index];
            g.DrawString(rep.RecipeID, new Font("微软雅黑", 12f, FontStyle.Bold), Brushes.Black, rect.Left, rect.Top + 5);
            //g.DrawString("PPID: " + rep.PPID, new Font("微软雅黑", 9f), Brushes.Black, rect.Left, rect.Top + 18);
            //g.DrawString("Remark: " + rep.Remark, new Font("微软雅黑", 9f), Brushes.Black, rect.Left + 150, rect.Top + 18);

            // 绘制聚焦框
            e.DrawFocusRectangle();
        }
    }
}
