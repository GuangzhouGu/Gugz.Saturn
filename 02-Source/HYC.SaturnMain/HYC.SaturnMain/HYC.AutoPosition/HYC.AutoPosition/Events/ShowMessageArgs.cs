/*----------------------------------------------------------------
// Copyright (C) Suzhou HYC Electronic Technology Co.,LTD
// 版权所有。
//
// =================================
// CLR版本     ：4.0.30319.42000
// 命名空间    ：HYC.AutoPosition.Events
// 文件名称    ：ShowMessageArgs.cs
// =================================
// 创 建 者    ：yangkang
// 创建日期    ：2020/2/19 13:08:53 
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYC.AutoPosition.Events
{
    /// <summary>
    /// 消息显示事件参数类
    /// </summary>
    public class ShowMessageArgs : EventArgs
    {
        /// <summary>
        /// 文本信息
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// 是否为异常信息
        /// </summary>
        public bool IsError { get; set; }
    }
}
