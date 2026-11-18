/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2021 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：CommonLib.UserCtrl
// 文件名称    ：UserManageCtrl.cs
// =================================
// 创 建 者    ：guguangzhou
// 创建日期    ：2021/11/08 09:00:00 
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
using CommonLib.Entities;
using CommonLib.Views;
using CommonLib.Recipe;

namespace CommonLib.UserCtrl
{
    public partial class UserManageCtrl : UserControl
    {
        public UserManageCtrl()
        {
            InitializeComponent();
            FrmLogin.OnShowUser += SetCurrentUser;
        }
        FormLogin FrmLogin = new FormLogin();
        public static event Action<string> OnUpdateRecipe;
        /// <summary>
        /// 显示当前用户
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="status"></param>
        public void SetCurrentUser(string userName, bool status)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string, bool>(SetCurrentUser), userName, status);
            }
            else
            {
                if (status)
                {
                    this.lbl_CurrentUser.Text = userName;
                    btnLogin.Text = "登录";
                    lbl_CurrentUser.BackColor = Color.FromArgb(223, 244, 245);
                }
            }
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (lbl_CurrentUser.Text != "")
            {
                lbl_CurrentUser.Text = "";
                lbl_CurrentUser.BackColor = Color.White;
                UserManager.CurUser = null;
                btnLogin.Text = "未登录";
            }
            else
            {
                if (FrmLogin == null || FrmLogin.IsDisposed)
                {
                    FrmLogin = new FormLogin();
                }
                FrmLogin.ShowDialog();
            }
        }

        private void btnRecipe_Click(object sender, EventArgs e)
        {
            UserControl button = (UserControl)sender;
            FormRecipeSelect recipeSelect = new FormRecipeSelect();
            if (recipeSelect.ShowDialog() == DialogResult.OK)
            {
                this.lbl_recipe.Text = RecipeManager.CurRecipe.RecipeID;
                OnUpdateRecipe?.Invoke(RecipeManager.CurRecipe.RecipeID);
            }
        }
        public void SetRecipe(string recipeName)
        {
            this.lbl_recipe.Text = recipeName;
        }

        private void UserManageCtrl_Load(object sender, EventArgs e)
        {

        }
    }
}
