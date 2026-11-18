/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2021 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：CommonLib.Views
// 文件名称    ：ViewBase.cs
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
    public partial class ViewBase : Form
    {
        public ViewBase()
        {
            InitializeComponent();
            this.Size = new Size(1506, 824);
        }
        [Description("按钮名称")]
        public string ButtonName  { get; set; }
        [Description("是否需要登陆")]
        public bool IsNeedLogin { get; set; } = true;
        [Description("窗体编号，从0开始")]
        public int Index { get; set; }
        [Description("是否嵌入在主窗体里")]
        public bool IsInsertPanel { get; set; } = true;
        [Description("用户权限等级")]
        public UserLevel Level { get; set; }
    }
}
