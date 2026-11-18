/*----------------------------------------------------------------
// Copyright (C) Suzhou HYC Electronic Technology Co.,LTD
// 版权所有。
//
// =================================
// CLR版本     ：4.0.30319.42000
// 命名空间    ：HYC.AutoPosition.UI.View
// 文件名称    ：ViewSetting.cs
// =================================
// 创 建 者    ：yangkang
// 创建日期    ：2020/1/7 15:32:34 
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
using HYC.AutoPosition.Parameter;
using HYC.AutoPosition.UI.Controls;
using HYC.AutoPosition.UI.Helper;
using HYC.HTXYT;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HYC.AutoPosition.UI.View
{
    /// <summary>
    /// 设置页面
    /// </summary>
    public partial class ViewSetting : ViewBase
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public ViewSetting()
        {
            InitializeComponent();
            IsNeedLogin = true;
            this.cboXYTTypeName.Items.AddRange(XYTFactory.GetInstances().Select(p => p.Name).ToArray());
        }
        /// <summary>
        /// 更新界面显示
        /// </summary>
        public override void UpdateView()
        {
            this.chkSaveImage.Checked = Global.Setting.IsSaveImage;
            this.chkSaveNgImageOnly.Checked = Global.Setting.IsSaveNgImageOnly;
            this.txtImageSavePath.Text = Global.Setting.ImageSavePath;

            this.txtRetryLimitCount.Text = Global.Setting.RetryLimitCount.ToString();
            this.cboMoveFlowType.SelectedIndex = (int)Global.Setting.MoveFlowType;

            this.chkAlgorithmShield.Checked = Global.Setting.IsAlgorithmShield;
            this.chkOutputColorMode.Checked = Global.Setting.IsOutputColorMode;

            this.cboXYTTypeName.Text = Global.Setting.XYTTypeName;
            this.txtXYTInitPara.Text = string.Join(";", Global.Setting.XYTInitPara);

            if (this.pnlGroupConfig.Controls.Count != Global.Setting.ListGroup.Count)
            {
                this.pnlGroupConfig.Controls.Clear();
                for (int i = 0; i < Global.Setting.ListGroup.Count; i++)
                {
                    UcGroupConfig ctrl = new UcGroupConfig();
                    ctrl.Config = Global.Setting.ListGroup[i];
                    this.pnlGroupConfig.Controls.Add(ctrl);
                }
            }
        }
        /// <summary>
        /// 保存设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SaveData();
                Global.SaveSettings();
                this.lblTip.Visible = false;
            }
            catch (Exception ex)
            {
                this.lblTip.Text = LanguageHelper.Msg.ViewSetting_SaveError + ex.Message;
                this.lblTip.Visible = true;
            }
        }
        /// <summary>
        /// 从界面获取设置
        /// </summary>
        private void SaveData()
        {
            Global.Setting.IsSaveImage = this.chkSaveImage.Checked;
            Global.Setting.IsSaveNgImageOnly = this.chkSaveNgImageOnly.Checked;
            Global.Setting.ImageSavePath = this.txtImageSavePath.Text;

            Global.Setting.RetryLimitCount = Convert.ToInt32(this.txtRetryLimitCount.Text);
            Global.Setting.MoveFlowType = (MoveFlowType)this.cboMoveFlowType.SelectedIndex;

            Global.Setting.IsAlgorithmShield = this.chkAlgorithmShield.Checked;
            Global.Setting.IsOutputColorMode = this.chkOutputColorMode.Checked;

            Global.Setting.XYTTypeName = this.cboXYTTypeName.Text;
            Global.Setting.XYTInitPara = this.txtXYTInitPara.Text.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            Global.Setting.ListGroup.Clear();
            foreach (Control ctrl in this.pnlGroupConfig.Controls)
            {
                if (ctrl is UcGroupConfig)
                {
                    var cfg = (ctrl as UcGroupConfig).Config;
                    if (Global.Setting.ListGroup.Where(p => p.GroupName == cfg.GroupName).Count() <= 0)
                        Global.Setting.ListGroup.Add(cfg);
                    else
                        throw new Exception(LanguageHelper.Msg.ViewSetting_GroupNameError);
                }
            }
        }
        /// <summary>
        /// 选择图片保存路径
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.SelectedPath = this.txtImageSavePath.Text;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                this.txtImageSavePath.Text = dlg.SelectedPath;
            }
        }
        /// <summary>
        /// 添加对位组
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddGroup_Click(object sender, EventArgs e)
        {
            UcGroupConfig ctrl = new UcGroupConfig();
            this.pnlGroupConfig.Controls.Add(ctrl);
        }
    }
}
