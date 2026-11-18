/*----------------------------------------------------------------
// Copyright (C) Suzhou HYC Electronic Technology Co.,LTD
// 版权所有。
//
// =================================
// CLR版本     ：4.0.30319.42000
// 命名空间    ：HYC.AutoPosition.UI
// 文件名称    ：FormAutoPos.cs
// =================================
// 创 建 者    ：yangkang
// 创建日期    ：2020/1/2 17:49:00 
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
using HYC.AutoPosition.Interface;
using HYC.AutoPosition.UI.Entities;
using HYC.AutoPosition.UI.Forms;
using HYC.AutoPosition.UI.Helper;
using HYC.AutoPosition.UI.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HYC.AutoPosition.UI
{
    /// <summary>
    /// 自动对位窗体
    /// </summary>
    public partial class FormAutoPos : Form
    {
        /// <summary>
        /// 状态显示页面
        /// </summary>
      //  private ViewStatus viewStatus = new ViewStatus();

        /// <summary>
        /// 界面分割控件
        /// </summary>
        private Splitter splitter = new Splitter();

        /// <summary>
        /// 已选中的机种
        /// </summary>
        public string SelectedRecipeID { get; set; }

        /// <summary>
        /// 构造方法
        /// </summary>
        public FormAutoPos()
        {
            InitializeComponent();
            //加载设置
            Global.LoadSettings();
       
        }
        bool IsInit = false;
        /// <summary>
        /// 窗体启动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormAutoPos_Load(object sender, EventArgs e)
        {
            LoadViews();
        }
        public void Init()
        {
            try
            {
                IsInit = true;
                //删除旧的日志
                LogManager.DeleteOldLog(15);
                //初始化硬件连接
                InitHardware();
            }
            catch (Exception ex)
            {
               // ShowMsg(ex.ToString(), Brushes.Red);
                LogManager.GetLogger("Error").Error(ex);
            }
          
        }
   
        /// <summary>
        /// 加载视图
        /// </summary>
        private void LoadViews()
        {
            //添加界面分隔控件
            splitter.Dock = DockStyle.Right;
            splitter.BackColor = tbpnlMain.BackColor;
            this.Controls.Add(splitter);
            //加载消息显示界面
            //viewStatus.FormBorderStyle = FormBorderStyle.None;
            //viewStatus.TopLevel = false;
            //viewStatus.Dock = DockStyle.Right;
            //this.Controls.Add(viewStatus);

            //加载继承了ViewBase的页面
            Assembly asm = Assembly.GetExecutingAssembly();
            Type[] types = asm.GetTypes();
            foreach (var item in types)
            {
                if (item != typeof(ViewBase) && typeof(ViewBase).IsAssignableFrom(item))
                {
                    ViewBase view = (ViewBase)asm.CreateInstance(item.FullName, true);
                    view.FormBorderStyle = FormBorderStyle.None;
                    view.TopLevel = false;
                    view.Dock = DockStyle.Fill;
                    pnlViewHost.Controls.Add(view);
                }
            }
            //显示消息栏
            //string str = Global.IniHelper.Read("UI", "IsExpanded", "0");
            //if (str == "1")
            //{
            //    this.btnExpandor.Text = ">";
            //    splitter.Show();
            //    viewStatus.Show();
            //}
            //else
            //{
            //    this.btnExpandor.Text = "<";
            //    splitter.Hide();
            //    viewStatus.Hide();
            //}
            //查找并显示主页面
            foreach (Control ctrl in pnlViewHost.Controls)
            {
                if (ctrl is ViewBase)
                {
                    ViewBase view = ctrl as ViewBase;
                    if (ctrl.Name == "ViewMain")
                    {
                        view.Show();
                        view.UpdateView();
                    }
                    else
                    {
                        view.Hide();
                    }
                }
            }
            //监听按钮事件
            foreach (Control ctrl in pnlNavigate.Controls)
            {
                if (ctrl is RadioButton)
                {
                    (ctrl as RadioButton).CheckedChanged += Navigate_CheckedChanged;
                }
            }
            //处理控件事件，重置用户登录超时
            foreach (Control ctrl in pnlViewHost.Controls)
            {
                if (ctrl is ViewBase)
                {
                    ViewBase view = ctrl as ViewBase;
                    if (view.IsNeedLogin)
                    {
                        RegisterControlEvent(view);
                    }
                }
            }
        }

        /// <summary>
        /// 注册控件事件
        /// </summary>
        /// <param name="ctrl"></param>
        private void RegisterControlEvent(Control ctrl)
        {
            ctrl.MouseEnter += Ctrl_ControlActive;
            ctrl.KeyPress += Ctrl_ControlActive;
            if (ctrl.Controls.Count > 0)
            {
                foreach (Control item in ctrl.Controls)
                {
                    RegisterControlEvent(item);
                }
            }
        }

        /// <summary>
        /// 控件活动时，重置用户超时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ctrl_ControlActive(object sender, EventArgs e)
        {
            Global.UserManager.ResetTimer();
        }

        /// <summary>
        /// 页面导航
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Navigate_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rbtn = sender as RadioButton;
            if (rbtn.Checked)
            {
                if (rbtn.Tag == null) return;
                // 查找需要显示的页面
                string tag = rbtn.Tag.ToString();
                foreach (Control ctrl in pnlViewHost.Controls)
                {
                    if (ctrl is ViewBase)
                    {
                        ViewBase view = ctrl as ViewBase;
                        if (ctrl.Name == tag)
                        {
                            //Global.UserManager.ResetTimer();
                            //// 判断是否需要显示用户登录页面
                            //if (view.IsNeedLogin)
                            //{
                            //    if (!Global.UserManager.IsLogin || Global.UserManager.CurUser.Level < UserLevel.Engineer)
                            //    {
                            //        viewLogin.Show();
                            //        viewLogin.UpdateView();
                            //    }
                            //}
                            //else
                            //{
                            //    // 隐藏用户登录页面
                            //    viewLogin.Hide();
                            //}
                            view.Show();
                            view.UpdateView();
                        }
                        else
                        {
                            view.Hide();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 初始化硬件连接
        /// </summary>
        private void InitHardware()
        {
            Task.Factory.StartNew(() =>
            {
                PosCamera.Init();
                //PosPlatform.Init();
            });
        }

        /// <summary>
        /// 释放硬件连接
        /// </summary>
        public void UnInitHardware()
        {
            PosCamera.UnInit();
         
        }

        /// <summary>
        /// 外部传入ISignalProvider实例
        /// </summary>
        /// <param name="provider">ISignalProvider实例</param>
        public void SetSignalProvider(ISignalProvider provider)
        {
            PosSignal.SetSignalProvider(provider);
            provider.OnRecipeSync += RecipeSync;
         }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void RecipeSync(object sender, RecipeSyncArgs e)
        {
            RecipeSelect(e.RecipeID);
        }
        /// <summary>
        /// 机种选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRecipeSelect_Click(object sender, EventArgs e)
        {
            FormRecipePick frm = new FormRecipePick();
            frm.RecipeList = Global.Manager.GetRecipes();
            frm.RecipeID = Global.Manager.CurRecipe?.RecipeID;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string recipeId = frm.RecipeID;
                    RecipeSelect(recipeId);
                }
                catch (Exception ex)
                {
                    LogManager.GetLogger("Error").Error("机种选择失败！" + ex.ToString());
                    MessageBox.Show("机种选择失败！" + ex.Message, "错误",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        /// <summary>
        /// 机种选择
        /// </summary>
        /// <param name="recipeId"></param>
        public void RecipeSelect(string recipeId)
        {       
            SelectedRecipeID = recipeId;
            var lstTargetStages = Global.Setting.ListGroup.SelectMany(p => p.ListTargetStages);
            int stageCount = lstTargetStages.Count() > 0 ? lstTargetStages.Max() : 0;
            Global.Manager.Init(SelectedRecipeID, stageCount);
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    this.lblRecipe.Text = SelectedRecipeID;
                }));
            }
            else
            {
                this.lblRecipe.Text = SelectedRecipeID;
            }
                    
            Global.IniHelper.Write("Recipe", "RecipeID", SelectedRecipeID);

            foreach (Control ctrl in pnlViewHost.Controls)
            {
                if (ctrl is ViewBase && ctrl.Visible)
                {
                    ViewBase view = ctrl as ViewBase;
                    view.UpdateView();
                }
            }
        }
        public void DeleteRecipe(string recipeName)
        {
            Global.Manager.DeleteRecipe(recipeName);
        }

        public void CopyRecipe(string oldRecipeName,string newRecipeName)
        {
            Global.Manager.CreateNewRecipeFrom(oldRecipeName, newRecipeName);
        }
        /// <summary>
        /// 用户注销
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogout_Click(object sender, EventArgs e)
        {
            Global.UserManager.Logout();
        }

  

        /// <summary>
        /// 显示登录的用户信息
        /// </summary>
        public void ShowUser()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(ShowUser));
            }
            else
            {
                if (Global.UserManager.IsLogin)
                {
                    this.lblUser.Text = Global.UserManager.CurUser.Name;
                    if (Global.UserManager.CurUser.Level == UserLevel.Developer)
                    {
                        this.lblUser.BackColor = Color.Red;
                        this.lblUser.ForeColor = Color.White;
                    }
                    else if (Global.UserManager.CurUser.Level == UserLevel.Engineer)
                    {
                        this.lblUser.BackColor = Color.LightBlue;
                        this.lblUser.ForeColor = Color.Black;
                    }
                    else
                    {
                        this.lblUser.BackColor = Color.WhiteSmoke;
                        this.lblUser.ForeColor = Color.Black;
                    }
                }
                else
                {
                    this.lblUser.Text = string.Empty;
                    this.lblUser.BackColor = Color.WhiteSmoke;
                    this.lblUser.ForeColor = Color.Black;
                }
            }
        }

        /// <summary>
        /// 展开状态显示栏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void btnExpandor_Click(object sender, EventArgs e)
        //{
        //    if (!viewStatus.Visible)
        //    {
        //        this.btnExpandor.Text = ">";
        //        splitter.Show();
        //        viewStatus.Show();
        //        Global.IniHelper.Write("UI", "IsExpanded", "1");
        //    }
        //    else
        //    {
        //        this.btnExpandor.Text = "<";
        //        splitter.Hide();
        //        viewStatus.Hide();
        //        Global.IniHelper.Write("UI", "IsExpanded", "0");
        //    }
        //}

      

        /// <summary>
        /// 获取Manager对象
        /// </summary>
        /// <returns></returns>
        public AutoPosManager GetManager()
        {
            return Global.Manager;
        }

        /// <summary>
        /// 获取用户管理类
        /// </summary>
        /// <returns></returns>
        public UserManager GetUserManager()
        {
            return Global.UserManager;
        }

        /// <summary>
        /// 设置语言
        /// </summary>
        /// <param name="language"></param>
        public void SetLanguage(string language)
        {
            //设置语言
            Global.Manager.SetLanguage(language);
            LanguageHelper.SetLanguage(language);
            //加载语言
            this.SuspendLayout();
            LanguageHelper.LoadLanguage(this);

            //LanguageHelper.LoadLanguage(viewStatus);
            PosCamera.UpdateCameraStatus();
            this.ResumeLayout();
            var dock = Dock;
            Dock = DockStyle.None;
            Dock = dock;
        }

        /// <summary>
        /// 注册相机实时图像
        /// </summary>
        /// <param name="index">相机序号</param>
        /// <param name="isSetToLive">是否设置成实时模式</param>
        /// <param name="method">显示图像的委托</param>
        public static void RegisterLiveImage(int index, bool isSetToLive, ShowLiveImage method)
        {
            PosCamera.Live(index, isSetToLive, method);
        }

    
        /// <summary>
        /// 视图类型枚举
        /// </summary>
        public enum ViewEnum
        {
            /// <summary>
            /// 对位平台页面
            /// </summary>
            ViewXYT,
            /// <summary>
            /// 机种管理页面
            /// </summary>
            ViewRecipeManage,
            /// <summary>
            /// 用户管理页面
            /// </summary>
            ViewUserManage
        }
    }
}
