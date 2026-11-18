/*----------------------------------------------------------------
// Copyright (C) Suzhou HYC Electronic Technology Co.,LTD
// 版权所有。
//
// =================================
// CLR版本     ：4.0.30319.42000
// 命名空间    ：HYC.AutoPosition.Parameter
// 文件名称    ：WorkFlowPara.cs
// =================================
// 创 建 者    ：yangkang
// 创建日期    ：2020/1/6 15:48:37 
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
using HYC.AutoPosition.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYC.AutoPosition.Parameter
{
    /// <summary>
    /// 工作流程参数
    /// </summary>
    public class WorkFlowPara
    {
        /// <summary>
        /// 图像生产者
        /// </summary>
        public IImageProducer[] ImageProducers { get; set; }
        /// <summary>
        /// 运动执行者
        /// </summary>
        public IMoveExecutor MoveExecutor { get; set; }
        /// <summary>
        /// 对位组序号
        /// </summary>
        public int GroupIndex { get; set; }
        /// <summary>
        /// 工位序号
        /// </summary>
        public int StageIndex { get; set; }
        /// <summary>
        /// 检测模式 0：Mark图片 1：目标位图片
        /// </summary>
        public int DetectMode { get; set; }
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
        /// 失败重试次数
        /// </summary>
        public int RetryLimitCount { get; set; } = 3;
        /// <summary>
        /// 运动模式
        /// </summary>
        public MoveFlowType MoveFlowType { get; set; }
        /// <summary>
        /// 是否交换XY
        /// </summary>
        public bool IsSwapXY { get; set; }
        /// <summary>
        /// 运动完成后需要确认
        /// </summary>
        public bool IsConfirmRequired { get; set; }
        /// <summary>
        /// XYT对位成功的判定阈值
        /// </summary>
        public double[] ThreXYT { get; set; }
        /// <summary>
        /// 是否算法屏蔽
        /// </summary>
        public bool IsAlgorithmShield { get; set; }
        /// <summary>
        /// 算法输出的为彩色图
        /// </summary>
        public bool IsOutputColorMode { get; set; }
        /// <summary>
        /// 最大移动步距
        /// </summary>
        public double[] MaxMoveDistance { get; set; }
        public bool IsSingleCam { get; set; }
    }
}
