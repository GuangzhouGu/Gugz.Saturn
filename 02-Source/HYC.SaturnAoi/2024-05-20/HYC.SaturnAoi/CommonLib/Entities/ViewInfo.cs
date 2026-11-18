/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2021 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：CommonLib.Entities
// 文件名称    ：ViewInfo.cs
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
using CommonLib.Views;
using HYC.WindowsControls.Buttons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Entities
{
    public class ViewInfo
    {
        public ViewBase View { get; set; }
        public HCButton2 Button { get; set; }
    }
}
