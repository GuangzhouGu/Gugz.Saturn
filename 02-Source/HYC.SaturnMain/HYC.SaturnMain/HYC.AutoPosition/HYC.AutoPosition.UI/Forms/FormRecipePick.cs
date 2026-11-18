/*----------------------------------------------------------------
// Copyright (C) Suzhou HYC Electronic Technology Co.,LTD
// 版权所有。
//
// =================================
// CLR版本     ：4.0.30319.42000
// 命名空间    ：HYC.AutoPosition.UI.Forms
// 文件名称    ：FormRecipePick.cs
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
using HYC.AutoPosition.Recipes;
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
    /// 机种选择窗口
    /// </summary>
    public partial class FormRecipePick : Form
    {
        /// <summary>
        /// 选中的RecipeID
        /// </summary>
        public string RecipeID { get; set; }
        /// <summary>
        /// 机种列表
        /// </summary>
        public List<Recipe> RecipeList { get; set; }

        /// <summary>
        /// 构造方法
        /// </summary>
        public FormRecipePick()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;
        }

        /// <summary>
        /// 窗体启动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormRecipePick_Load(object sender, EventArgs e)
        {
            //将所有机种添加到列表
            this.lstRecipes.Items.Clear();
            this.lstRecipes.Items.AddRange(RecipeList.ToArray());
            //突出显示已经选中的机种
            if (!string.IsNullOrEmpty(RecipeID))
            {
                for (int i = 0; i < this.lstRecipes.Items.Count; i++)
                {
                    var rep = (Recipe)this.lstRecipes.Items[i];
                    if (rep.RecipeID == RecipeID)
                    {
                        this.lstRecipes.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 确定按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.lstRecipes.SelectedIndex < 0) return;

            var rep = (Recipe)this.lstRecipes.SelectedItem;
            RecipeID = rep.RecipeID;
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
