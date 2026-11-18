using HYC.HTFile.HTINI;
using HYC.HTLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DFS.SubForm
{
    public partial class FormDFS : UserControl
    {
        public static FormDFS Instance = null;
        public FormDFS()
        {
            InitializeComponent();
            Instance = this;
        }

        private void FormDFS_Load(object sender, EventArgs e)
        {
            DFSSettings.UpdateView += DFSSettings_UpdateView;
            DFSSettings.Load();
            UpdateView();
        }

        private void DFSSettings_UpdateView()
        {
            UpdateView();
        }

        public void UpdateView()
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action(UpdateView));
            }
            else
            {
                this.txtHost.Text = DFSSettings.FTPHost;
                this.txtUser.Text = DFSSettings.FTPUser;
                this.txtPwd.Text = DFSSettings.FTPPwd;

                this.txtShop.Text = DFSSettings.Shop;
                this.txtMode.Text = DFSSettings.UploadMode;
                this.txtStepID.Text = DFSSettings.StepID;
                this.txtEQPID.Text = DFSSettings.EQPID;
                this.txtUNITID.Text = DFSSettings.UNITID;

                cbEnableDFS.Checked = DFSSettings.IsDFSEnabled;
            }

        }

        private void FormDFS_FormClosing(object sender, FormClosingEventArgs e)
        {
            Instance = null;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DFSSettings.FTPHost = this.txtHost.Text;
                DFSSettings.FTPUser = this.txtUser.Text;
                DFSSettings.FTPPwd = this.txtPwd.Text;

                DFSSettings.Shop = this.txtShop.Text;
                DFSSettings.StepID = this.txtStepID.Text;
                DFSSettings.EQPID = this.txtEQPID.Text;
                DFSSettings.UNITID = this.txtUNITID.Text;
                DFSSettings.UploadMode = this.txtMode.Text;
                DFSSettings.IsDFSEnabled = cbEnableDFS.Checked;
                DFSSettings.Save();

            }
            catch (Exception ex)
            {
                ShowMsg("保存出错！\r\n" + ex.Message, Brushes.Red);
                LogManager.GetLogger("Error").Error(ex.Message + "\r\n" + ex.StackTrace);
            }
        }

        public void ShowMsg(string str, Brush brush)
        {
            try
            {
                if (this.lstMsg.InvokeRequired)
                {
                    this.lstMsg.Invoke(new Action<string, Brush>(ShowMsg), str, brush);
                }
                else
                {
                    this.lstMsg.AddListBoxMsg(DateTime.Now.ToString("HH:mm:ss(fff)") + ": " + str, brush);
                }
            }
            catch (Exception)
            { }
        }

        private void txtStepID_TextChanged(object sender, EventArgs e)
        {
            this.txtFilePathUpload.Text = DFSSettings.GetDataFileUploadDir("7F60154001A1AAA01",txtShop.Text ,txtMode.Text, txtStepID.Text);
            this.txtImgPathUpload.Text = DFSSettings.GetImageUploadDir("7F60154001A1AAA01", txtShop.Text, txtMode.Text, txtStepID.Text);
        }

        private void txtShop_TextChanged(object sender, EventArgs e)
        {
            this.txtFilePathUpload.Text = DFSSettings.GetDataFileUploadDir("7F60154001A1AAA01", txtShop.Text, txtMode.Text, txtStepID.Text);
            this.txtImgPathUpload.Text = DFSSettings.GetImageUploadDir("7F60154001A1AAA01", txtShop.Text, txtMode.Text, txtStepID.Text);
        }

        private void txtMode_TextChanged(object sender, EventArgs e)
        {
            this.txtFilePathUpload.Text = DFSSettings.GetDataFileUploadDir("7F60154001A1AAA01", txtShop.Text, txtMode.Text, txtStepID.Text);
            this.txtImgPathUpload.Text = DFSSettings.GetImageUploadDir("7F60154001A1AAA01", txtShop.Text, txtMode.Text, txtStepID.Text);
        }
    }
}
