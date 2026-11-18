/*----------------------------------------------------------------
// Copyright (C) Suzhou HYC Electronic Technology Co.,LTD
// 版权所有。
//
// =================================
// CLR版本     ：4.0.30319.42000
// 命名空间    ：HYC.AutoPosition.UI.Forms
// 文件名称    ：FormRecipeNew.cs
// =================================
// 创 建 者    ：yangkang
// 创建日期    ：2020/1/7 13:41:36 
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
using HYC.AutoPosition.UI.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HYC.AutoPosition.UI.Forms
{
    /// <summary>
    /// 新建机种窗口
    /// </summary>
    public partial class FormRecipeNew : Form
    {
        /// <summary>
        /// 机种ID
        /// </summary>
        public string RecipeID { get; set; } = string.Empty;

        /// <summary>
        /// 构造方法
        /// </summary>
        public FormRecipeNew()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;
        }

        /// <summary>
        /// 窗体启动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormRecipeNew_Load(object sender, EventArgs e)
        {
            this.txtRecipeID.Text = RecipeID;
        }

        /// <summary>
        /// 窗体激活事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormRecipeNameInput_Activated(object sender, EventArgs e)
        {
            this.txtRecipeID.Focus();
        }

        /// <summary>
        /// 文本框文本改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtRecipeID_TextChanged(object sender, EventArgs e)
        {
            //文本为空时，不显示提示信息
            if (string.IsNullOrEmpty(this.txtRecipeID.Text))
            {
                this.lblTipRecipeID.Visible = false;
                return;
            }

            //验证文本长度
            string str = this.txtRecipeID.Text;
            if (str.Length > 50)
            {
                this.lblTipRecipeID.Text = LanguageHelper.Msg.FormRecipeNew_RecipeIDLengthError;
                this.lblTipRecipeID.Visible = true;
                return;
            }

            //验证输入的机种名是否已存在
            var lst = Global.Manager.GetRecipes()
                .Where(p => p.RecipeID == str);
            if (lst.Count() > 0)
            {
                this.lblTipRecipeID.Text = LanguageHelper.Msg.FormRecipeNew_RecipeIDExists;
                this.lblTipRecipeID.Visible = true;
                return;
            }

            this.lblTipRecipeID.Visible = false;
        }

        /// <summary>
        /// 确定按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.lblTipRecipeID.Visible) return;
            if (string.IsNullOrEmpty(this.txtRecipeID.Text)) return;

            RecipeID = this.txtRecipeID.Text;
            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// 取消按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
