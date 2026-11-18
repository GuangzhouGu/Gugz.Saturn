/*----------------------------------------------------------------
// Copyright (C) Suzhou HYC Electronic Technology Co.,LTD
// 版权所有。
//
// =================================
// CLR版本     ：4.0.30319.42000
// 命名空间    ：HYC.AutoPosition.UI.View
// 文件名称    ：ViewBase.cs
// =================================
// 创 建 者    ：yangkang
// 创建日期    ：2020/1/2 17:58:57 
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
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace HYC.AutoPosition.UI.View
{
    /// <summary>
    /// 页面视图基类，定义虚方法，设置常用属性
    /// </summary>
    public partial class ViewBase : Form
    {
        /// <summary>
        /// 页面是否需要登录
        /// </summary>
        public bool IsNeedLogin { get; set; }

        /// <summary>
        /// 构造方法
        /// </summary>
        public ViewBase()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 更新视图显示
        /// </summary>
        public virtual void UpdateView() { }
    }
}
