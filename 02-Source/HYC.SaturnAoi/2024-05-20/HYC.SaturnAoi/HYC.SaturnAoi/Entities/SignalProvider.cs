/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2023 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnAoi.Entities
// 文件名称    ：Class1
// =================================
// 创 建 者    ：guguangzhou
// 创建日期    ：
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
using HYC.SaturnAoi.Entities;
using HYC.StandardAoi.Events;
using HYC.StandardAoi.Interface;
using HYC.StandardAoi.Parameter;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYC.SaturnAoi.DetectFlow
{
    public class SignalProvider : ISignalProvider
    {
        public static SignalProvider Instance = null;
        static SignalProvider()
        {
            Instance = new SignalProvider();
        }
        /// <summary>
        /// 消息显示
        /// </summary>
        public event Action<string, Brush> OnShowMsg;

        /// <summary>
        /// 已准备好获取图片，用于外观检等需要外部配合的情况
        /// </summary>
        /// <param name="type">检测类型</param>
        /// <param name="channel">检测通道</param>
        /// <param name="patternIndex"></param>
        public void GetImagesReady(DetectionType type, int channel, int patternIndex)
        {

        }

        /// <summary>
        /// 单个Pattern的图像获取完成
        /// </summary>
        /// <param name="type">检测类型</param>
        /// <param name="channel">检测通道</param>
        /// <param name="patternIndex"></param>
        public void GetImagesCompleted(DetectionType type, int channel, int patternIndex)
        {

        }

        /// <summary>
        /// 所有图像获取完成
        /// </summary>
        /// <param name="type">检测类型</param>
        /// <param name="channel">检测通道</param>
        public void GetImagesEnded(DetectionType type, int channel)
        {

        }

        /// <summary>
        /// 检测完成
        /// </summary>
        /// <param name="type">检测类型</param>
        /// <param name="channel">检测通道</param>
        /// <param name="result">结果</param>
        public void DetectionCompleted(DetectionType type, int channel, ResultType result)
        {
          
        }
      
        /// <summary>
        /// 触发检测
        /// </summary>
        /// <param name="args"></param>
        public void OnDetectionRequest(RequestEventArgs args)
        {
            DetectionRequest?.Invoke(this, args);
        }

        public void GetImagesReady(GetImagesReadyArgs args)
        {
        }

        public void GetImagesCompleted(GetImagesCompletedArgs args)
        {
        }

        public void GetImagesEnded(GetImagesEndedArgs args)
        {
        }

        public void DetectionCompleted(DetectionCompletedArgs args)
        {
            DBManager.UpdateAoiResult(args);
            AoiDetectReqHandle.Instance.DetectResultReply(args);
            ImageSaveHelper.SavePartialImage(args);
        }

        /// <summary>
        /// 开始检测请求
        /// </summary>
        public event EventHandler<RequestEventArgs> DetectionRequest;
    }
}
