/*----------------------------------------------------------------
// Copyright (C) Suzhou HYC Electronic Technology Co.,LTD
// 版权所有。
//
// =================================
// CLR版本     ：4.0.30319.42000
// 命名空间    ：HYC.AutoPosition.Entities
// 文件名称    ：ImageProducer.cs
// =================================
// 创 建 者    ：yangkang
// 创建日期    ：2020/1/2 14:29:57 
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
using HYC.AutoPosition.Parameter;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYC.AutoPosition.Interface
{
    /// <summary>
    /// 图像数据生产者
    /// </summary>
    public interface IImageProducer
    {
        /// <summary>
        /// 获取图像数据
        /// </summary>
        /// <returns>图形数据</returns>
        ImageData CreateImageData();
    }
}
