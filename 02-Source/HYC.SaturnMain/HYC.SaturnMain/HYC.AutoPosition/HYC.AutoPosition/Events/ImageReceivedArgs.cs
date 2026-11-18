/*----------------------------------------------------------------
// Copyright (C) Suzhou HYC Electronic Technology Co.,LTD
// 版权所有。
//
// =================================
// CLR版本     ：4.0.30319.42000
// 命名空间    ：HYC.AutoPosition.Events
// 文件名称    ：ImageReceivedArgs.cs
// =================================
// 创 建 者    ：yangkang
// 创建日期    ：2020/1/2 15:49:33 
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYC.AutoPosition.Events
{
    /// <summary>
    /// 对位图像生成事件类
    /// </summary>
    public class ImageReceivedArgs: EventArgs
    {
        /// <summary>
        /// 对位组序号
        /// </summary>
        public int GroupIndex { get; set; }
        /// <summary>
        /// 工位序号
        /// </summary>
        public int StageIndex { get; set; }
        /// <summary>
        /// 第几次处理
        /// </summary>
        public int ProcessIndex { get; set; }
        /// <summary>
        /// 图片类型
        /// </summary>
        public ImageType ImageType { get; set; }
        /// <summary>
        /// 图片数据
        /// </summary>
        public ImageData[] ImageData { get; set; }
        /// <summary>
        /// 图片测试模式只计算，不运动
        /// </summary>
        public bool IsImgTestMode { get; set; }
        /// <summary>
        /// 拍照测试模式只计算，不运动，保存拍照图像
        /// </summary>
        public bool IsGrabTestMode { get; set; }
        /// <summary>
        /// 手动模式
        /// </summary>
        public bool IsManualMode { get; set; }
        /// <summary>
        /// 运动数据
        /// </summary>
        public double[] XYT { get; set; }
        /// <summary>
        /// 算法备用输出值
        /// </summary>
        public int[] Reserve { get; set; }
        /// <summary>
        /// 图像处理NG
        /// </summary>
        public bool IsProcessedNG { get; set; }
    }

    /// <summary>
    /// 图片类型
    /// </summary>
    public enum ImageType
    {
        /// <summary>
        /// 原始图
        /// </summary>
        Original,
        /// <summary>
        /// 处理图
        /// </summary>
        Processed
    }
}
