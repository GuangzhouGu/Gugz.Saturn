/*----------------------------------------------------------------
// Copyright (C) Suzhou HYC Electronic Technology Co.,LTD
// 版权所有。
//
// =================================
// CLR版本     ：4.0.30319.42000
// 命名空间    ：HYC.AutoPosition.Parameter
// 文件名称    ：MoveType.cs
// =================================
// 创 建 者    ：yangkang
// 创建日期    ：2020/1/2 16:19:23 
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYC.AutoPosition.Parameter
{
    /// <summary>
    /// 对位流程中的平台运动类型
    /// </summary>
    public enum MoveFlowType
    {
        /// <summary>
        /// 所有轴一起运动
        /// </summary>
        [Description("所有轴一起运动")]
        OnceAllAxis,
        /// <summary>
        /// 先走T，再走XY
        /// </summary>
        [Description("先走T，再走XY")]
        ThetaFirst
    }
}
