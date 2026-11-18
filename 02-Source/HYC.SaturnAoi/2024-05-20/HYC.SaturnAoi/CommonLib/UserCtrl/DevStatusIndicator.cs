/*----------------------------------------------------------------
// Copyright (C) Suzhou HYC Technology Co.,LTD
// 版权所有。
//
// =================================
// CLR版本     ：4.0.30319.42000
// 命名空间    ：CommonLib.UserCtrl
// 文件名称    ：DevStatusIndicator.cs
// =================================
// 创 建 者    ：gugz
// 创建日期    ：2020/11/3 14:50:36 
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

namespace CommonLib.UserCtrl
{
    /// <summary>
    /// 单个组件的状态指示控件
    /// </summary>
    public partial class DevStatusIndicator : UserControl
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public DevStatusIndicator()
        {
            InitializeComponent();
        }



        /// <summary>
        /// 设备名称
        /// </summary>
        public string DevName
        {
            get { return this.lblName.Text; }
            set { this.lblName.Text = value; }
        }

        /// <summary>
        /// 设备状态
        /// </summary>
        public bool Status
        {    
            set
            {
                 this.lblSts.BackColor = value ? Color.Lime: Color.LightGray  ;           
            }
        }
    }
}
