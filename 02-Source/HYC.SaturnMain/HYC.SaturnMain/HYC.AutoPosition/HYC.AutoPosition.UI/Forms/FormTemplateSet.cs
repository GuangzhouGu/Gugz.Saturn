/*----------------------------------------------------------------
// Copyright (C) Suzhou HYC Electronic Technology Co.,LTD
// 版权所有。
//
// =================================
// CLR版本     ：4.0.30319.42000
// 命名空间    ：HYC.AutoPosition.UI.Forms
// 文件名称    ：FormTemplateSet.cs
// =================================
// 创 建 者    ：yangkang
// 创建日期    ：2020/10/11 9:50:37 
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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HYC.AutoPosition.UI.Forms
{
    public partial class FormTemplateSet : Form
    {
        /// <summary>
        /// 图像
        /// </summary>
        public Image Image { get; set; }
        /// <summary>
        /// 选择
        /// </summary>
        public int Option { get; set; }

        /// <summary>
        /// 构造方法
        /// </summary>
        public FormTemplateSet()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormTemplateSet_Load(object sender, EventArgs e)
        {
            this.pictImage.Image = Image;
        }

        /// <summary>
        /// 设置为左模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetAsLeft_Click(object sender, EventArgs e)
        {
            Option = 1;
            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// 设置为右模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetAsRight_Click(object sender, EventArgs e)
        {
            Option = 2;
            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
