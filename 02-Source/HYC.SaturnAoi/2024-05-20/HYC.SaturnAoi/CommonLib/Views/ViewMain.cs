/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2021 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：CommonLib.Views
// 文件名称    ：ViewMain.cs
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
using CommonLib.Entities;
using CommonLib.UserCtrl;
using HYC.HTLog;
using HYC.WindowsControls.Buttons;
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
    public partial class ViewMain : FormBase
    {
        public  event Action<string> OnUpdateRecipe;
        public event Action OnClosing;
        public ViewMain()
        {
            InitializeComponent();
            LoadForms();
            UserManageCtrl.OnUpdateRecipe += UserManageCtrl_OnUpdateRecipe;
        }

        private void UserManageCtrl_OnUpdateRecipe(string arg1)
        {
            OnUpdateRecipe?.Invoke(arg1);
        }

        List<ViewBase> ViewList = null;
        Dictionary<string, ViewInfo> ViewDic = new Dictionary<string, ViewInfo>();
       
        private void LoadForms()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(LoadForms));
            }
            else
            {
                try
                {
                    int columnIndex = 0;
                    ViewList = ClassHelper.GetChildClass<ViewBase>();
                    if (ViewList != null)
                    {
                        ViewList.Sort((x, y) => -y.Index.CompareTo(x.Index));
                        tlpButtons.Controls.Clear();
                        if (ViewList.Count < 14)
                        {
                            tlpButtons.ColumnCount = 14;
                        }
                        else
                        {
                            tlpButtons.ColumnCount = ViewList.Count;
                        }
                        for (int i = 0; i < tlpButtons.ColumnCount; i++)
                        {
                            tlpButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
                        }
                        foreach (var item in ViewList)
                        {
                            if (item.IsInsertPanel)
                            {
                                item.TopLevel = false;
                                item.Parent = ViewPanel;
                                item.Dock = DockStyle.Fill;
                            }
                            HCButton2 button = new HCButton2();
                            button.Name = $"btn{item.Name}";
                            button.Text = item.ButtonName;
                            button.OriginalColor = Color.White;
                            button.ActiveColor = Color.FromArgb(24, 121, 190);
                            button.Dock = DockStyle.Fill;
                            button.Click += Button_Click;
                            button.Font = new Font("微软雅黑", 14.25F);
                            tlpButtons.Controls.Add(button, columnIndex, 0);
                            columnIndex++;
                            ViewDic.Add(button.Name, new ViewInfo { Button = button, View = item });
                        }
                    }
                }
                catch (Exception ex)
                {
                    ShowMsg("Error", ex.Message, Brushes.Red);
                }
            }
          
        }
        public void EnableButtons(bool enable)
        {
            ViewDic.Values.Select(p=>p.Button.Enabled=enable).ToArray();
        }
        public void UpdateRecipe(string recipe)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(UpdateRecipe), recipe);
            }
            else
            {
           
                ShowMsg("Recipe", $"当前机种{recipe}", Brushes.Blue);
                userManageCtrl1.SetRecipe(recipe);
            }
        }
        public void UpdateDevStatus(string devName,bool sts)
        {
            try
            {
                if (InvokeRequired)
                {
                    Invoke(new Action<string, bool>(UpdateDevStatus), devName, sts);
                }
                else
                {
                    this.statusCtrl1.UpdateDevStatus(devName, sts);
                }
            }
            catch (Exception)
            {
            }
        }
        private bool CheckUserLevel(ViewBase view)
        {
            try
            {
                bool result = true;
                if (view.IsNeedLogin)
                {
                    if (UserManager.CurUser == null)
                    {
                        MessageBox.Show("请先登录", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        result = false;
                    }
                    else
                    {
                        result = UserManager.CurUser.Level >= view.Level;
                        if (!result)
                        {
                            MessageBox.Show("当前用户没有权限打开此界面", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                return result;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private void Button_Click(object sender, EventArgs e)
        {
            ViewInfo info = ViewDic[(sender as HCButton2).Name];
            Task.Factory.StartNew(() => { SwitchForm(info); });
        }
        public void SwitchForm(ViewInfo info)
        {
            try
            {
                if (InvokeRequired)
                {
                    Invoke(new Action<ViewInfo>(SwitchForm), info);
                }
                else
                {
                    if (CheckUserLevel(info.View))
                    {
                        foreach (KeyValuePair<string, ViewInfo> k in ViewDic)
                        {
                            k.Value.View.Visible = false;
                            k.Value.Button.OriginalColor = Color.White;
                            k.Value.Button.ForeColor = Color.Black;
                        }
                        info.Button.OriginalColor = Color.FromArgb(24, 121, 190);
                        info.Button.ForeColor = Color.White;
                        info.View.Visible = true;
                    }
                }
            }
            catch (Exception)
            {

            }
        }
        public ViewBase GetChildView(string viewName)
        {
            try
            {
                return ViewList?.Where(p => p.Name == viewName).First();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public ViewInfo GetViewInfo(string viewName)
        {
            return ViewDic.Values?.Where(p => p.View.Name == viewName).First();
        }
        public ViewInfo GetViewInfo(int viewIndex)
        {
            return ViewDic.Values?.Where(p => p.View.Index == viewIndex).First();
        }
        private Color GetColor(EqpSts sts)
        {
            Color clr = Color.Yellow;
            switch (sts)
            {
                case EqpSts.IDLE:
                    clr = Color.Yellow;
                    break;
                case EqpSts.PAUSE:
                    clr = Color.Yellow;
                    break;
                case EqpSts.RUN:
                    clr = Color.Lime;
                    break;
                case EqpSts.PM:
                    clr = Color.SkyBlue;
                    break;
                case EqpSts.STOP:
                    clr = Color.Tomato;
                    break;
                case EqpSts.ALARM:
                    clr = Color.Tomato;
                    break;
                default:
                    break;
            }
            return clr;
        }
        public void UpdateEqpsts(EqpSts sts)
        {
            try
            {
                if (InvokeRequired)
                {
                    Invoke(new Action<EqpSts>(UpdateEqpsts), sts);
                }
                else
                {
                    this.lblEqpStatus.Text = sts.ToString();
                    this.lblEqpStatus.BackColor = GetColor(sts);
                }
            }
            catch (Exception)
            {

            }
        }
        /// <summary>
        /// 显示消息
        /// </summary>
        /// <param name="logType">Log文件名</param>
        /// <param name="msg">消息体</param>
        /// <param name="colorBrush">消息颜色</param>
        public void ShowMsg(string logType, string msg, Brush colorBrush)
        {
            if (InvokeRequired && !statusCtrl1.IsDisposed)
            {
                statusCtrl1.Invoke(new Action<string, string, Brush>(ShowMsg), logType, msg, colorBrush);
            }
            else
            {
                statusCtrl1.ShowMsg(msg, colorBrush);
                LogManager.GetLogger(logType).Info(msg);
            }
        }

        private void ViewMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("是否关闭应用程序？", "询问", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                Uninit();
            }
            else
            {
                e.Cancel = true;
                return;
            }
        }
        /// <summary>
        /// 释放资源
        /// </summary>
        private void Uninit()
        {
            OnClosing?.Invoke();
            this.Dispose(true);
        }
    }
    public enum EqpSts
    {
        IDLE,
        PAUSE,
        RUN,
        PM,
        STOP,
        ALARM,
    }
}
