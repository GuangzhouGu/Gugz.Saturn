/*----------------------------------------------------------------
// Copyright (C) Suzhou HYC Electronic Technology Co.,LTD
// 版权所有。
//
// =================================
// CLR版本     ：4.0.30319.42000
// 命名空间    ：HYC.AutoPosition.Interface
// 文件名称    ：IMoveExecutor.cs
// =================================
// 创 建 者    ：yangkang
// 创建日期    ：2020/1/6 13:54:41 
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

namespace HYC.AutoPosition.Interface
{
    /// <summary>
    /// 运动执行者
    /// </summary>
    public interface IMoveExecutor
    {
        /// <summary>
        /// 移动
        /// </summary>
        /// <param name="groupIndex">对位组序号</param>
        /// <param name="xyt">移动值</param>
        /// <param name="reserve">预留</param>
        /// <returns>移动是否完成</returns>
        bool Move(int groupIndex, double[] xyt, int[] reserve);
    }
}
