/*----------------------------------------------------------------
// Copyright (C) Suzhou HYC Technology Co.,LTD
// 版权所有。
//
// =================================
// CLR版本     ：4.0.30319.42000
// 命名空间    ：HYC.AutoPosition.UI.Controls.Template
// 文件名称    ：SubMarkSetting.cs
// =================================
// 创 建 者    ：miaoliang
// 创建日期    ：2020/10/30 13:45:05 
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HYC.AutoPosition.UI.Controls
{
    partial class TemplateMatchingSetting
    {
        /// <summary>
        /// 设置TableLayoutPanel指定行的高度
        /// </summary>
        /// <param name="layout"></param>
        /// <param name="row"></param>
        /// <param name="height"></param>
        private void SetTlpRowHeight(TableLayoutPanel layout, int row, float height)
        {
            int count = layout.RowCount;
            if (row > count) return;

            for (int i = 0; i < count; i++)
            {
                if (i == row)
                {
                    layout.RowStyles[i].Height = height;
                    return;
                }
            }
        }

        /// <summary>
        /// 设置TableLayoutPanel指定行的宽度
        /// </summary>
        /// <param name="layout"></param>
        /// <param name="col"></param>
        /// <param name="width"></param>
        private void SetTlpColWidth(TableLayoutPanel layout, int col, float width)
        {
            try
            {
                int count = layout.ColumnCount;
                if (col > count) return;

                for (int i = 0; i < count; i++)
                {
                    if (i == col)
                    {
                        layout.ColumnStyles[i].Width = width;
                        return;
                    }
                }
            }
            catch { return; }
        }
        /// <summary>
        /// 获取Tablayout的行高
        /// </summary>
        /// <param name="layout"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        private float GetTlpRowHeight(TableLayoutPanel layout, int row)
        {
            float height = 0;
            int count = layout.RowCount;
            if (row > count) return 0;
            for (int i = 0; i < count; i++)
            {
                if (!i.Equals(row)) continue;
                height = layout.RowStyles[i].Height;
                SizeType type = layout.ColumnStyles[i].SizeType;
                if (type == SizeType.Percent)
                {
                    height = layout.ClientSize.Height * height / 100;
                }

                break;
            }
            return height;
        }

        /// <summary>
        /// 获取TableLayoutPanel指定列的宽度
        /// </summary>
        /// <param name="layout">TableLayoutPanel</param>
        /// <param name="col">行号</param>
        /// <returns>行宽</returns>
        public float GetTlpColWidth(TableLayoutPanel layout, int col)
        {
            float width = 0;
            try
            {
                int count = layout.ColumnCount;
                if (col > count) return 0;

                for (int i = 0; i <= count; i++)
                {
                    if (!i.Equals(col)) continue;
                    width = layout.ColumnStyles[i].Width;
                    SizeType type = layout.ColumnStyles[i].SizeType;
                    if (type == SizeType.Percent)
                    {
                        width = layout.ClientSize.Width * width / 100;
                    }

                    break;
                }
            }
            catch
            {
                return width;
            }
            return width;
        }
    }
}
