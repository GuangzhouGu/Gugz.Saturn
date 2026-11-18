using CommonLib.Entities;
using HYC.WindowsForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CommonLib.Views
{
    public partial class FormLogin :FormPopBase
    {
        public FormLogin()
        {
            InitializeComponent();
            Title = "登录";
        }
        public event Action< string,bool> OnShowUser;
        private void btn_confirm_Click(object sender, EventArgs e)
        {
            try
            {
                bool retValue = UserManager.Login(this.cboUser.Text, this.txtPwd.Text);
                if (retValue)
                {
                    OnShowUser?.Invoke(UserManager.CurUser.Name, true);
                    lblTip.Text = "";
                    txtPwd.Text = "";
                    this.DialogResult = DialogResult.OK;
                    this.Hide();
                }
                else
                {
                    OnShowUser?.Invoke("", false);
                    MessageBox.Show($"{UserManager.CurUser.Name.ToString()}登录失败，用户名或者密码错误!", "提示", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                this.lblTip.Text = ex.Message;
                this.lblTip.Visible = true;
            }
        }
        public void UpdateLoginInfo()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(UpdateLoginInfo));
            }
            else
            {
                UserManager.LoadUsers();
                var lst = UserManager.GetUsers().Select(p => p.Name).ToArray();
                lblTip.Text = "";
                txtPwd.Text = "";
                this.cboUser.Items.Clear();
                this.cboUser.Items.AddRange(lst);
            }
        }
        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            UserManager.LoadUsers();
            var lst = UserManager.GetUsers().Select(p => p.Name).ToArray();
            this.cboUser.Items.Clear();
            this.cboUser.Items.AddRange(lst);
            lblTip.Text = "";
        }
    }
}
