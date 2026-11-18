using HYC.AutoPosition.UI;
using HYC.HTPCBase.Extension.Entities;
using HYC.HTPCBase.Extension.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HYC.SaturnMain.SubForms
{
    public partial class FrmAutoPos :ViewBase
    {
        public FormAutoPos FormAutoPos = null;
        public FrmAutoPos()
        {
            InitializeComponent();
            this.IsNeedLogin = false;
            this.ButtonName = "对位监控";
            this.ButtonNameEn = "AutoPos\nMonitor";
            this.Index = 1;
            this.ButtonImage = HYC.SaturnMain.Properties.Resources.AutoPos;


            UserCtrlManager.OnUpdateRecipe += RecipeManager_OnUpdateRecipe;
            UserCtrlManager.OnDeleteRecipe += UserCtrlManager_OnDeleteRecipe;
            UserCtrlManager.OnCopyRecipe += UserCtrlManager_OnCopyRecipe;
            FormAutoPos = new FormAutoPos();
            FormAutoPos.FormBorderStyle = FormBorderStyle.None;
            FormAutoPos.TopLevel = false;
            FormAutoPos.Dock = DockStyle.Fill;
            FormAutoPos.Size = new Size(1400, 800);
            //设置信号提供者
            FormAutoPos.SetSignalProvider(Global.SignalProvider);
        }
      

        private void UserCtrlManager_OnCopyRecipe(string arg1, string arg2)
        {
            try
            {
                FormAutoPos.CopyRecipe(arg1, arg2);
            }
            catch (Exception ex)
            {
                ShowMsg("FrmAutoPos", $"UserCtrlManager_OnCopyRecipe执行异常，异常信息：{ex}", Brushes.Red); ;
            }
        }

        private void UserCtrlManager_OnDeleteRecipe(string obj)
        {
            try
            {
                FormAutoPos.DeleteRecipe(obj);
            }
            catch (Exception ex)
            {
                ShowMsg("FrmAutoPos", $"UserCtrlManager_OnDeleteRecipe执行异常，异常信息：{ex}", Brushes.Red); ;
            }
        }

        private void RecipeManager_OnUpdateRecipe(string arg2)
        {
            try
            {
                FormAutoPos.RecipeSelect(arg2);
            }
            catch (Exception ex)
            {
                ShowMsg("FrmAutoPos", $"RecipeManager_OnUpdateRecipe执行异常，异常信息：{ex}", Brushes.Red); ;
            }
        }
        public void Unint()
        {
            try
            {
                FormAutoPos.UnInitHardware();
            }
            catch (Exception ex)
            {

            }
        }



        private void FrmAutoPos_Load(object sender, EventArgs e)
        {
            try
            {
                FormAutoPos = new FormAutoPos();
                FormAutoPos.FormBorderStyle = FormBorderStyle.None;
                FormAutoPos.TopLevel = false;
                FormAutoPos.Dock = DockStyle.Fill;
                FormAutoPos.Size = new Size(1400, 800);
 
                this.pnlHost.Controls.Add(FormAutoPos);
                //显示窗体
                FormAutoPos.Show();
            
            }
            catch (Exception ex)
            {
                ShowMsg("FrmAutoPos", $"FrmAutoPos构造函数执行异常，异常信息：{ex}", Brushes.Red); ;
            }
        }
    }
}
