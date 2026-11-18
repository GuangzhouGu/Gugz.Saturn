/*----------------------------------------------------------------
// Copyright (C) Suzhou HYC Electronic Technology Co.,LTD
// 版权所有。
//
// =================================
// CLR版本     ：4.0.30319.42000
// 命名空间    ：HYC.StandardAoi.UI.View
// 文件名称    ：ViewParameter.cs
// =================================
// 创 建 者    ：yangkang
// 创建日期    ：2020/3/3 17:31:32 
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
using HYC.AutoPosition.UI.View.Parameter;
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
    /// 参数显示与设置视图
    /// </summary>
    public partial class ViewParameter : ViewBase
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public ViewParameter()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            IsNeedLogin = false;
            // 加载视图
            LoadViews();
        }

        /// <summary>
        /// 更新视图显示
        /// </summary>
        public override void UpdateView()
        {
            foreach (Control ctrl in pnlViewHost.Controls)
            {
                if (ctrl is ViewParameterBase && ctrl.Visible)
                {
                    ViewParameterBase view = ctrl as ViewParameterBase;
                    view.UpdateView();
                }
            }
        }

        /// <summary>
        /// 加载视图
        /// </summary>
        private void LoadViews()
        {
            //加载继承了ViewParameterBase的页面
            Assembly asm = Assembly.GetExecutingAssembly();
            Type[] types = asm.GetTypes();
            foreach (var item in types)
            {
                if (item != typeof(ViewParameterBase) && typeof(ViewParameterBase).IsAssignableFrom(item))
                {
                    ViewParameterBase view = (ViewParameterBase)asm.CreateInstance(item.FullName, true);
                    view.FormBorderStyle = FormBorderStyle.None;
                    view.TopLevel = false;
                    view.Dock = DockStyle.Fill;
                    pnlViewHost.Controls.Add(view);
                }
            }
            //查找并显示主页面
            foreach (Control ctrl in pnlViewHost.Controls)
            {
                if (ctrl is ViewParameterBase)
                {
                    ViewParameterBase view = ctrl as ViewParameterBase;
                    if (ctrl.Name == "ViewTemplatePara")
                    {
                        view.Show();
                    }
                    else
                    {
                        view.Hide();
                    }
                }
            }
          
        }

   
    }
}
