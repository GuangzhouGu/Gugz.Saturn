/*----------------------------------------------------------------
// Copyright (C) Suzhou HYC Electronic Technology Co.,LTD
// 版权所有。
//
// =================================
// CLR版本     ：4.0.30319.42000
// 命名空间    ：HYC.AutoPosition.UI.Controls
// 文件名称    ：MessageListBox.cs
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
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HYC.AutoPosition.UI.Controls
{
    /// <summary>
    /// 消息显示列表框
    /// </summary>
    public partial class MessageListBox : ListBox
    {
        /// <summary>
        /// 选定项背景色
        /// </summary>
        [Description("选定项背景色"), Category("其他")]
        public Color SelectionBackColor { get; set; } = SystemColors.Highlight;

        /// <summary>
        /// 构造方法
        /// </summary>
        public MessageListBox()
        {
            InitializeComponent();
            this.DrawMode = DrawMode.OwnerDrawFixed;
            this.DoubleBuffered = true;
            this.ItemHeight = 16;
            this.HorizontalScrollbar = true;
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
            if (e.Index < 0) return;

            try
            {
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

                MessageItem item = (MessageItem)this.Items[e.Index];
                // 绘制文本
                e.Graphics.DrawString(item.Message, e.Font, item.Brush, e.Bounds, StringFormat.GenericDefault);
                // 绘制聚焦框
                e.DrawFocusRectangle();
            }
            catch (Exception)
            { }
        }

        /// <summary>
        /// 向列表中添加消息
        /// </summary>
        /// <param name="strMsg"></param>
        /// <param name="brush"></param>
        public void AddMessage(string strMsg, Brush brush)
        {
            try
            {
                if (this.Items.Count >= 100)
                {
                    this.Items.RemoveAt(this.Items.Count - 1);
                }
                this.Items.Insert(0, new MessageItem() { Message = strMsg, Brush = brush });
                //设置可以横向滚动的宽度
                Graphics g = this.CreateGraphics();
                int width = 0;
                foreach (var item in Items)
                {
                    int w = (int)g.MeasureString((item as MessageItem).Message, Font).Width;
                    if (w > width)
                    {
                        width = w;
                    }
                }
                this.HorizontalExtent = width;
            }
            catch (Exception)
            { }
        }

        /// <summary>
        /// 列表项
        /// </summary>
        public class MessageItem
        {
            /// <summary>
            /// 消息内容
            /// </summary>
            public string Message { get; set; }
            /// <summary>
            /// 颜色
            /// </summary>
            public Brush Brush { get; set; }
        }
    }
}
