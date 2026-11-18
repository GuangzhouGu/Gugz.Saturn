/*----------------------------------------------------------------
// Copyright (C) Suzhou HYC Electronic Technology Co.,LTD
// 版权所有。
//
// =================================
// CLR版本     ：4.0.30319.42000
// 命名空间    ：HYC.AutoPosition.UI.Entities
// 文件名称    ：PosSignal.cs
// =================================
// 创 建 者    ：yangkang
// 创建日期    ：2020/1/20 11:21:43 
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
using HYC.AutoPosition.Extentions;
using HYC.AutoPosition.Interface;
using HYC.AutoPosition.Parameter;
using HYC.AutoPosition.UI.Helper;
using HYC.AutoPosition.UI.View;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HYC.AutoPosition.UI.Entities
{
    /// <summary>
    /// 信号提供者管理类
    /// </summary>
    static class PosSignal
    {
        //信号提供者
        private static ISignalProvider signalProvider = null;

        /// <summary>
        /// 外部传入ISignalProvider实例
        /// </summary>
        /// <param name="provider">ISignalProvider实例</param>
        public static void SetSignalProvider(ISignalProvider provider)
        {
            signalProvider = provider;
            signalProvider.AutoPosRequest += SignalProvider_AutoPosRequest;           
            signalProvider.CalibrationRequest += SignalProvider_CalibrationRequest;
            signalProvider.MoveRequest += SignalProvider_MoveRequest;
            signalProvider.SafetyChanged += SignalProvider_SafetyChanged;
        }

       

        /// <summary>
        /// 获取ISignalProvider实例
        /// </summary>
        /// <returns>ISignalProvider实例</returns>
        public static ISignalProvider GetSignalProvider()
        {
            return signalProvider;
        }
        
        /// <summary>
        /// 自动对位请求
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static async void SignalProvider_AutoPosRequest(object sender, RequestEventArgs e)
        {
            try
            {
                GroupConfig cfg = Global.Setting.ListGroup[e.GroupIndex];
                ShowMsg(string.Format(LanguageHelper.Msg.PosSignal_AutoPosRequestReceived,
                    cfg.GroupName, e.StageIndex + 1), Brushes.Green);

                // 创建图像数据生产者
                List<IImageProducer> producers = new List<IImageProducer>();
                for (int i = 0; i < cfg.ListCamera.Count; i++)
                {
                    var camera = PosCamera.GetCamera(cfg.ListCamera[i].MacAddr);
                    RotateFlipType rotateFlipType = cfg.ListCamera[i].RotateFlipType;
                    producers.Add(new CameraImageProducer(camera, cfg.IsUseGrabLock, rotateFlipType));
                }

                // 根据设置中的类型创建运动执行者
                IMoveExecutor executor = cfg.MoveExecutorType == MoveExecutorType.XYTPlatform ?
                    new XytMoveExecutor(PosPlatform.GetPlatform(cfg.GroupName)) :
                    (IMoveExecutor)signalProvider;


                //#region 测试
      
                //// 创建图像数据生产者
                //List<IImageProducer> lst = new List<IImageProducer>();
                //for (int i = 0; i < cfg.ListCamera.Count; i++)
                //{
                //    lst.Add(new FileImageProducer(@"D:\Execute\ReciewPC_AlignPerColumn\1.bmp"));
                //} 
                //#endregion

                // 初始化对位参数
                WorkFlowPara para = new WorkFlowPara();
                para.ImageProducers = producers.ToArray();
                para.MoveExecutor = executor;
                para.GroupIndex = e.GroupIndex;
                para.StageIndex = e.StageIndex;
                para.DetectMode = e.DetectMode;
                para.IsImgTestMode = false;
                para.IsManualMode = false;
                para.RetryLimitCount = Global.Setting.RetryLimitCount;
                para.MoveFlowType = Global.Setting.MoveFlowType;
                para.IsSwapXY = cfg.IsSwapXY;
                para.IsConfirmRequired = cfg.IsConfirmRequired;
                para.ThreXYT = cfg.ThreXYT;
                para.MaxMoveDistance = cfg.MaxMoveDistance;
                para.IsAlgorithmShield = Global.Setting.IsAlgorithmShield;
                para.IsOutputColorMode = Global.Setting.IsOutputColorMode;
                para.IsSingleCam = e.IsSingleCam;
                // 异步执行自动对位流程
                WorkFlowResult result = await Global.Manager.StartAutoPosAsync(para);
                signalProvider.AutoPosCompleted(e.GroupIndex, e.StageIndex, result.Result);
            }
            catch (Exception ex)
            {
                ShowMsg(LanguageHelper.Msg.PosSignal_ErrorToStartAutoPos + ex, Brushes.Red);
                signalProvider.AutoPosCompleted(e.GroupIndex, e.StageIndex, false);
            }
        }

        /// <summary>
        /// 自动标定请求
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static async void SignalProvider_CalibrationRequest(object sender, RequestEventArgs e)
        {
            try
            {
                GroupConfig cfg = Global.Setting.ListGroup[e.GroupIndex];
                ShowMsg(string.Format(LanguageHelper.Msg.PosSignal_CalibrationRequest,
                    cfg.GroupName, e.StageIndex + 1), Brushes.Green);

                // 创建图像数据生产者
                List<IImageProducer> producers = new List<IImageProducer>();
                for (int i = 0; i < cfg.ListCamera.Count; i++)
                {
                    var camera = PosCamera.GetCamera(cfg.ListCamera[i].MacAddr);
                    RotateFlipType rotateFlipType = cfg.ListCamera[i].RotateFlipType;
                    producers.Add(new CameraImageProducer(camera, cfg.IsUseGrabLock, rotateFlipType));
                }

                // 初始化对位参数
                WorkFlowPara para = new WorkFlowPara();
                para.ImageProducers = producers.ToArray();
                para.GroupIndex = e.GroupIndex;
                para.StageIndex = e.StageIndex;
                para.IsManualMode = false;
                para.IsOutputColorMode = Global.Setting.IsOutputColorMode;
                // 异步执行标定流程
                WorkFlowResult result = await Global.Manager.StartCalibrationAsync(para);
                signalProvider.CalibrationCompleted(e.GroupIndex, e.StageIndex, result.Result);
            }
            catch (Exception ex)
            {
                ShowMsg(LanguageHelper.Msg.PosSignal_ErrorToStartCalibration + ex, Brushes.Red);
                signalProvider.CalibrationCompleted(e.GroupIndex, e.StageIndex, false);
            }
        }

        /// <summary>
        /// 运动请求
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void SignalProvider_MoveRequest(object sender, MoveReqEventArgs e)
        {
            GroupConfig cfg = Global.Setting.ListGroup[e.GroupIndex];
            ShowMsg(string.Format(LanguageHelper.Msg.PosSignal_MoveRequestReceived,
                cfg.GroupName, e.MoveReqType), Brushes.Green);

            // 获取需要运动的平台
            var platform = PosPlatform.GetPlatform(cfg.GroupName);
            // 异步执行运动
            Task.Factory.StartNew(() =>
            {
                try
                {
                    bool result = false;
                    //请求类型为原点
                    if (e.MoveReqType == MoveReqType.Home)
                    {
                        if (platform.IsNeedHoming)
                            result = platform.AllHoming();
                        else
                            result = platform.MoveToHome();
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            //请求类型为用户位置
                            if (e.MoveReqType == MoveReqType.UserPos1 + i)
                            {
                                result = platform.MoveToUserPos(i);
                            }
                        }
                    }
                    signalProvider.MoveCompleted(e.GroupIndex, e.MoveReqType, result);
                   ShowMsg(string.Format(LanguageHelper.Msg.PosSignal_MoveCompleted,
                        cfg.GroupName, e.MoveReqType, result), Brushes.Green);
                }
                catch (Exception ex)
                {
                    signalProvider.MoveCompleted(e.GroupIndex, e.MoveReqType, false);
                    ShowMsg(LanguageHelper.Msg.PosSignal_MoveError + "\r\n" + ex, Brushes.Red);
                }
            });
        }

        /// <summary>
        /// 安全条件改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void SignalProvider_SafetyChanged(object sender, SafetyEventArgs e)
        {
            GroupConfig cfg = Global.Setting.ListGroup[e.GroupIndex];
            PosPlatform.UpdateSafetyFlag(cfg.GroupName, e.IsSaveToMove);
        }
        private static void ShowMsg(string msg,Brush bh)
        {
            LogManager.GetLogger("AutoPosManager").Info(msg);
        }
    }
}
