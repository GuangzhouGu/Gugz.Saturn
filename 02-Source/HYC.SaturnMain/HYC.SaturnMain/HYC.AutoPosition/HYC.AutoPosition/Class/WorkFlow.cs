/*----------------------------------------------------------------
// Copyright (C) Suzhou HYC Electronic Technology Co.,LTD
// 版权所有。
//
// =================================
// CLR版本     ：4.0.30319.42000
// 命名空间    ：HYC.AutoPosition.Class
// 文件名称    ：WorkFlow.cs
// =================================
// 创 建 者    ：yangkang
// 创建日期    ：2019/12/30 13:30:12 
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
using HYC.AutoPosition.Algorithm;
using HYC.AutoPosition.Events;
using HYC.AutoPosition.Parameter;
using HYC.AutoPosition.Recipes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using HYC.AutoPosition.Helper;

using HYC.AutoPosition.Interface;
using HYC.AutoPosition.ImageProcess;

namespace HYC.AutoPosition.Class
{
    /// <summary>
    /// 工作流程
    /// </summary>
    class WorkFlow
    {
        /// <summary>
        /// 工作流程参数
        /// </summary>
        private WorkFlowPara para = null;
        /// <summary>
        /// 当前机种
        /// </summary>
        public Recipe CurRecipe { get; set; }
        /// <summary>
        /// 重试时，保存的拍照确认的值
        /// </summary>
        private double[] reworkXyt = null;
        private int[] reworkReserve = null;

        public WorkFlowResult AutoPosDoWork(WorkFlowPara para)
        {
            try
            {
                this.para = para;
                //记录开始时间
                DateTime startTime = DateTime.Now;
                ShowMessage(string.Format(LanguageHelper.Msg.AutoPosStarted, para.StageIndex + 1), false);
                //开始流程
                bool result = false;
                for (int i = 0; i <= para.RetryLimitCount; i++)
                {
                    result = ProcessOnce(i,para.IsSingleCam);
                    //对位成功、测试模式、目标位检测模式，不执行重试
                    if (result || para.IsImgTestMode || para.IsGrabTestMode || para.DetectMode != 0)
                    {
                        break;
                    }
                }
                //获取耗时
                int processingTime = (int)(DateTime.Now - startTime).TotalMilliseconds;
                ShowMessage(string.Format(LanguageHelper.Msg.AutoPosCompleted, para.StageIndex + 1, result, processingTime), false);
                //流程结束
                return new WorkFlowResult()
                {
                    Result = result,
                    ProcessingTime = processingTime
                };
            }
            catch (Exception ex)
            {
                ShowMessage(string.Format(LanguageHelper.Msg.AutoPosError, para.StageIndex + 1), true);
                return new WorkFlowResult()
                {
                    Result = false,
                    ErrorMsg = "Auto Pos Abnormally Completed!\r\n" + ex
                };
            }
        }
        public WorkFlowResult ManualPosDoWork(WorkFlowPara para)
        {
            try
            {        
                this.para = para;
                //记录开始时间
                DateTime startTime = DateTime.Now;
                ShowMessage(string.Format(LanguageHelper.Msg.AutoPosStarted, para.StageIndex + 1), false);
                //开始流程
                bool result = false;
                for (int i = 0; i <= para.RetryLimitCount; i++)
                {
                    result = ProcessOnce(i, para.IsSingleCam);
                    //对位成功、测试模式、目标位检测模式，不执行重试
                    if (result || para.IsImgTestMode || para.IsGrabTestMode || para.DetectMode != 0)
                    {
                        break;
                    }
                }
                //获取耗时
                int processingTime = (int)(DateTime.Now - startTime).TotalMilliseconds;
                ShowMessage(string.Format(LanguageHelper.Msg.AutoPosCompleted, para.StageIndex + 1, result, processingTime), false);
                //流程结束
                return new WorkFlowResult()
                {
                    Result = result,
                    ProcessingTime = processingTime
                };
            }
            catch (Exception ex)
            {
                ShowMessage(string.Format(LanguageHelper.Msg.AutoPosError, para.StageIndex + 1), true);
                return new WorkFlowResult()
                {
                    Result = false,
                    ErrorMsg = "Auto Pos Abnormally Completed!\r\n" + ex
                };
            }
        }

        /// <summary>
        /// 开始执行标定流程
        /// </summary>
        /// <param name="para">标定参数</param>
        /// <returns>标定结果</returns>
        public WorkFlowResult CalibrationDoWork(WorkFlowPara para)
        {
            try
            {
                this.para = para;
                //记录开始时间
                DateTime startTime = DateTime.Now;
                ShowMessage(string.Format(LanguageHelper.Msg.CalibrationStarted, para.StageIndex + 1), false);
                //更新算法参数
                var data = CurRecipe.DataPos[para.StageIndex];
                DetectionAlgorithmPos.HT_OnUpdateModuleInfo(para.StageIndex, data.AlgorithmData);
                //开始流程
                bool result = true;
                //拍照，调用算法，获取运动参数
                int[] reserve = new int[1024];
                double[] xyt =  GetMovePara(0, reserve);
                //异常值判断
                if (xyt == null) result = false;
                if (xyt[0] == 0 && xyt[1] == 0 && xyt[2] == 400) result = false;
                ShowMessage(string.Format(LanguageHelper.Msg.CalibrationResult, para.StageIndex + 1, string.Join(",", xyt)), false);
                //更新算法目标值
                if (result)
                {
                    data.AlgorithmData.param1[4] = (float)Math.Round(xyt[4], 2);
                    data.AlgorithmData.param1[5] = (float)Math.Round(xyt[5], 2);
                    data.AlgorithmData.param1[6] = (float)Math.Round(xyt[6], 2);
                    if (para.ImageProducers.Length > 1)
                    {
                        data.AlgorithmData.param1[7] = (float)Math.Round(xyt[8], 2);
                        data.AlgorithmData.param1[8] = (float)Math.Round(xyt[9], 2);
                        data.AlgorithmData.param1[9] = (float)Math.Round(xyt[10], 2);
                    }
                    CurRecipe.SaveDataPos(para.StageIndex);
                    DetectionAlgorithmPos.HT_OnUpdateModuleInfo(para.StageIndex, data.AlgorithmData);
                }
                //获取耗时
                int processingTime = (int)(DateTime.Now - startTime).TotalMilliseconds;
                ShowMessage(string.Format(LanguageHelper.Msg.CalibrationCompleted, para.StageIndex + 1, result, processingTime), false);
                //流程结束
                return new WorkFlowResult()
                {
                    Result = result,
                    ProcessingTime = processingTime
                };
            }
            catch (Exception ex)
            {
                ShowMessage(string.Format(LanguageHelper.Msg.CalibrationError, para.StageIndex + 1), true);
                return new WorkFlowResult()
                {
                    Result = false,
                    ErrorMsg = "Calibration Abnormally Completed!\r\n" + ex
                };
            }
        }

        /// <summary>
        /// 单次对位流程
        /// </summary>
        /// <param name="processIndex">第几次处理</param>
        /// <returns>是否对位成功</returns>
        private bool ProcessOnce(int processIndex, bool isSingleCam = false)
        {
            //拍照，调用算法，获取运动参数
            double[] xyt = null;
            int[] reserve = new int[1024];
            reserve[0] = 3;
            if (reworkXyt != null)
            {
                // 使用拍照确认时获得的值，减少拍照次数
                xyt = new double[reworkXyt.Length];
                reworkXyt.CopyTo(xyt, 0);
                reserve = new int[reworkReserve.Length];
                reworkReserve.CopyTo(reserve, 0);
                reworkXyt = null;
                ShowMessage(string.Format(LanguageHelper.Msg.BeforeAdjust, para.StageIndex + 1, processIndex) +
                    (xyt == null ? "xyt=null" : $"X[{xyt[0]}], Y[{xyt[1]}], T[{xyt[2]}]"), false);
            }
            else
            {
                xyt = GetMovePara(processIndex, reserve, isSingleCam);
                ShowMessage(string.Format(LanguageHelper.Msg.BeforeAdjust, para.StageIndex + 1, processIndex) +
                    (xyt == null ? "xyt=null" : $"X[{xyt[0]}], Y[{xyt[1]}], T[{xyt[2]}]"), false);
                //异常值直接返回
                if (xyt == null) return false;
                if (xyt[0] == 0 && xyt[1] == 0 && xyt[2] == 400) return false;
            }

            //测试模式，或目标位检测模式，直接返回
            if (para.IsImgTestMode || para.IsGrabTestMode || para.DetectMode != 0) return true;
            //移动
            if (para.MoveFlowType == MoveFlowType.OnceAllAxis)
            {
            
                bool result = Move(MoveType.All, xyt, reserve);
                if (!result)
                {
                    ShowMessage(string.Format(LanguageHelper.Msg.MoveFailed, para.StageIndex + 1, processIndex), true);
                    return false;
                }
            }
            else if (para.MoveFlowType == MoveFlowType.ThetaFirst)
            {
                //先移动Theta
                if (para.IsConfirmRequired && Math.Abs(xyt[2]) <= para.ThreXYT[2])
                {
                    ShowMessage(string.Format(LanguageHelper.Msg.ThetaMoveSkip, para.StageIndex + 1, processIndex), false);
                }
                else
                {
                    bool result = Move(MoveType.Theta, xyt, reserve);
                    if (!result)
                    {
                        ShowMessage(string.Format(LanguageHelper.Msg.ThetaMoveFailed, para.StageIndex + 1, processIndex), true);
                        return false;
                    }
                    //调用算法，获取XY移动值
                    xyt = GetMovePara(processIndex, reserve);
                    ShowMessage(string.Format(LanguageHelper.Msg.AfterThetaAdjust, para.StageIndex + 1, processIndex) +
                        (xyt == null ? "xyt=null" : $"X[{xyt[0]}], Y[{xyt[1]}], T[{xyt[2]}]"), false);
                    //异常值直接返回
                    if (xyt == null) return false;
                    if (xyt[0] == 0 && xyt[1] == 0 && xyt[2] == 400) return false;
                }
                //移动XY
                bool result2 = Move(MoveType.XY, xyt, reserve);
                if (!result2)
                {
                    ShowMessage(string.Format(LanguageHelper.Msg.XYMoveFailed, para.StageIndex + 1, processIndex), true);
                    return false;
                }
            }
            //再次拍照确认
            if (para.IsConfirmRequired)
            {
                xyt = GetMovePara(processIndex, reserve);
                ShowMessage(string.Format(LanguageHelper.Msg.AfterAdjust, para.StageIndex + 1, processIndex) +
                    (xyt == null ? "xyt=null" : $"X[{xyt[0]}], Y[{xyt[1]}], T[{xyt[2]}]"), false);
                //异常值直接返回
                if (xyt == null) return false;
                if (xyt[0] == 0 && xyt[1] == 0 && xyt[2] == 400) return false;
                //结果比对
                if (Math.Abs(xyt[0]) > para.ThreXYT[0] ||
                    Math.Abs(xyt[1]) > para.ThreXYT[1] ||
                    Math.Abs(xyt[2]) > para.ThreXYT[2])
                {
                    reworkXyt = new double[xyt.Length];
                    xyt.CopyTo(reworkXyt, 0);
                    reworkReserve = new int[reserve.Length];
                    reserve.CopyTo(reworkReserve, 0);
                    return false;
                }
            }
            return true;
        }
     
        /// <summary>
        /// 调用算法，获取运动参数
        /// </summary>
        /// <param name="processIndex">第几次处理</param>
        /// <param name="reserve">预留</param>
        /// <returns>算法反馈的xyt运动数据</returns>
        private double[] GetMovePara(int processIndex, int[] reserve,bool isSingleCam = false)
        {
            double[] xyt = new double[3];
            //拍照，传递图片给算法
            ImageData[] dataIn = new ImageData[para.ImageProducers.Length];
            Task<bool>[] tasks = new Task<bool>[para.ImageProducers.Length];
            for (int i = 0; i < para.ImageProducers.Length; i++)
            {
                int index = i;
                tasks[index] = Task.Factory.StartNew(() =>
                {
                    try
                    {
                        dataIn[index] = para.ImageProducers[index].CreateImageData();
                        return dataIn[index] != null ? true : false;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                });
            }
            Task.WaitAll(tasks);
            //图像获取失败
            if (tasks.Select(p => p.Result).Contains(false))
            {
                ShowMessage(string.Format(LanguageHelper.Msg.GetImagesFailed, para.StageIndex + 1, processIndex), true);
                return null;
            }
            //触发事件
            ImageReceived?.Invoke(this, new ImageReceivedArgs()
            {
                GroupIndex = para.GroupIndex,
                StageIndex = para.StageIndex,
                ProcessIndex = processIndex,
                ImageType = ImageType.Original,
                ImageData = dataIn,
                IsImgTestMode = para.IsImgTestMode,
                IsGrabTestMode = para.IsGrabTestMode,
                IsManualMode = para.IsManualMode
            });
            //初始化结果图
            ImageData[] dataOut = new ImageData[para.ImageProducers.Length];
            for (int i = 0; i < para.ImageProducers.Length; i++)
            {
                dataOut[i] = new ImageData();
                dataOut[i].ImageBytes = new byte[dataIn[i].ImageBytes.Length * 3];
                dataOut[i].Width = dataIn[i].Width;
                dataOut[i].Height = dataIn[i].Height;
            }
            //获取Mark位置
            var recipeData = CurRecipe.DataPos[para.StageIndex];
                
            Point3D deltaXYT = new Point3D();
            Point3D[] xytWithoutAngle = new Point3D[para.ImageProducers.Length];
            Point3D[] foundPoints = new Point3D[para.ImageProducers.Length];
            Point3D[] foundWordPoints = new Point3D[para.ImageProducers.Length];
            Point3D[] originPoints = new Point3D[para.ImageProducers.Length];
            CameraInformationArray cameraInformations = recipeData.FlowPara.CameraInformations;
            tasks = new Task<bool>[para.ImageProducers.Length];
            //
            for (int i = 0; i < para.ImageProducers.Length; i++)
            {
                CameraInformation cameraInfo = cameraInformations[i];
                int index = i;
                if (!para.IsAlgorithmShield)
                {
                    tasks[index] = Task.Factory.StartNew(() =>
                    {
                        return true;
                    });
                        
                    ImageData dataTemp = new ImageData();
                    dataTemp.FromBitmap(recipeData.FlowPara.TemplateImgs[index]);

                    int[] roi = new int[] { 0, 0, dataIn[index].Width, dataIn[index].Height };
                    int[] matchRect = new int[4];
                    float[] location = new float[3];

                    HTImageProcessWrapper imageProc = new HTImageProcessWrapper();
                    imageProc.UpdateParameter(CurRecipe.AlignParameter.MatchingRateLimitCamera[index]);
                    imageProc.InitTemplate(recipeData.FlowPara.TemplateImgs[index]);                  
                    double rowCheck, colCheck, angleCheck, score;
                    imageProc.Match(dataIn[index].ToBitmap(), out rowCheck, out colCheck, out angleCheck, out score);
                    if (angleCheck == -400)
                    {
                        return new double[] { 0, 0, 400 };
                    }
                    foundPoints[index] = new Point3D(colCheck, rowCheck, angleCheck);
                    originPoints[index] = new Point3D(recipeData.FlowPara.ROI[index].X + recipeData.FlowPara.ROI[index].Width / 2, recipeData.FlowPara.ROI[index].Y + recipeData.FlowPara.ROI[index].Height / 2, 0);
                    foundWordPoints[index] = new Point3D();
                    foundWordPoints[index].X = foundPoints[index].X - cameraInfo.CenterPoint.X;
                    foundWordPoints[index].Y = foundPoints[index].Y - cameraInfo.CenterPoint.Y;
                    xytWithoutAngle[index] = cameraInformations[index].CaculationXYT(foundPoints[index].PointF, originPoints[index].PointF);

                    int[] rect = new int[4];
                    rect[0] = (int)colCheck - dataTemp.Width / 2;
                    rect[1] = (int)rowCheck - dataTemp.Height / 2;
                    rect[2] = dataTemp.Width;
                    rect[3] = dataTemp.Height;
                    ImageProc.drawRectangle(dataIn[index].ImageBytes, dataIn[index].Width, dataIn[index].Height, rect, dataOut[index].ImageBytes);
                }
            }

            Task.WaitAll(tasks);
           
            if (isSingleCam==false)//是否把相机之间的角度，以及相机和平台之间的角度考虑在内
            {
                double angle = recipeData.FlowPara.CameraInformations.CaculateRealAngle(foundWordPoints[0], foundWordPoints[1]);
                Point3D args1 = cameraInformations[0].CaculationXYT(originPoints[0].PointF, foundPoints[0].PointF, angle);
                Point3D args2 = cameraInformations[1].CaculationXYT(originPoints[1].PointF, foundPoints[1].PointF, angle);
                deltaXYT.X = Math.Round((args1.X + args2.X) / 2, 5);
                deltaXYT.Y = Math.Round((args1.Y + args2.Y) / 2, 5);
                deltaXYT.Z = Math.Round((args1.Z + args2.Z) / 2, 5);
                xyt = deltaXYT.GetValues();
            }
            else
            {
                if (para.ImageProducers.Length == 2)
                {
                    deltaXYT.X = Math.Round((xytWithoutAngle[0].X + xytWithoutAngle[1].X) / 2, 5);
                    deltaXYT.Y = Math.Round((xytWithoutAngle[0].Y + xytWithoutAngle[1].Y) / 2, 5);
                    deltaXYT.Z = Math.Round((xytWithoutAngle[0].Z + xytWithoutAngle[1].Z) / 2, 5);
                    xyt = deltaXYT.GetValues();
                }
                else if (para.ImageProducers.Length == 1)
                {
                    xyt = xytWithoutAngle[0].GetValues();
                }
            }
            if (para.IsSwapXY)
            {
                double tmp = xyt[0];
                xyt[0] = xyt[1];
                xyt[1] = tmp;
            }
            //触发事件
            bool isProcessedNG = xyt[0] == 0 && xyt[1] == 0 && xyt[2] == 400;
            ImageReceived?.Invoke(this, new ImageReceivedArgs()
            {
                GroupIndex = para.GroupIndex,
                StageIndex = para.StageIndex,
                ProcessIndex = processIndex,
                ImageType = ImageType.Processed,
                ImageData = dataOut,
                IsImgTestMode = para.IsImgTestMode,
                IsGrabTestMode = para.IsGrabTestMode,
                IsManualMode = para.IsManualMode,
                XYT = xyt,
                Reserve = reserve,
                IsProcessedNG = isProcessedNG
            });
            return xyt;
        }

        /// <summary>
        /// 执行运动
        /// </summary>
        /// <param name="type">移动类型</param>
        /// <param name="xyt">移动距离</param>
        /// <param name="reserve">预留</param>
        /// <returns>运动是否成功</returns>
        public bool Move(MoveType type, double[] xyt, int[] reserve)
        {
            // 检查移动值
            if (!CheckMoveDistance(xyt, type))
            {
                ShowMessage(string.Format(LanguageHelper.Msg.DistanceOverLimit, para.StageIndex + 1), true);
                return false;
            }

            bool result = false;
            if (type == MoveType.All)
                result = para.MoveExecutor.Move(para.GroupIndex, xyt, reserve);
            else if (type == MoveType.X)
                result = para.MoveExecutor.Move(para.GroupIndex, new double[] { xyt[0], 0, 0 }, reserve);
            else if (type == MoveType.Y)
                result = para.MoveExecutor.Move(para.GroupIndex, new double[] { 0, xyt[1], 0 }, reserve);
            else if (type == MoveType.Theta)
                result = para.MoveExecutor.Move(para.GroupIndex, new double[] { 0, 0, xyt[2] }, reserve);
            else if (type == MoveType.XY)
                result = para.MoveExecutor.Move(para.GroupIndex, new double[] { xyt[0], xyt[1], 0 }, reserve);

            return result;
        }

        /// <summary>
        /// 检查移动步距
        /// </summary>
        /// <param name="xyt">移动距离</param>
        /// <param name="type">移动类型</param>
        /// <returns>移动距离是否在设定范围内</returns>
        public bool CheckMoveDistance(double[] xyt, MoveType type)
        {
            // 未设置最大步距
            if (para.MaxMoveDistance == null) return true;

            bool result = true;
            if (type == MoveType.All)
            {
                if ((para.MaxMoveDistance[0] > 0 && Math.Abs(xyt[0]) > para.MaxMoveDistance[0]) ||
                    (para.MaxMoveDistance[1] > 0 && Math.Abs(xyt[1]) > para.MaxMoveDistance[1]) ||
                    (para.MaxMoveDistance[2] > 0 && Math.Abs(xyt[2]) > para.MaxMoveDistance[2]))
                {
                    result = false;
                }
            }
            else if (type == MoveType.X)
            {
                if (para.MaxMoveDistance[0] > 0 && Math.Abs(xyt[0]) > para.MaxMoveDistance[0])
                {
                    result = false;
                }
            }
            else if (type == MoveType.Y)
            {
                if (para.MaxMoveDistance[1] > 0 && Math.Abs(xyt[1]) > para.MaxMoveDistance[1])
                {
                    result = false;
                }
            }
            else if (type == MoveType.Theta)
            {
                if (para.MaxMoveDistance[2] > 0 && Math.Abs(xyt[2]) > para.MaxMoveDistance[2])
                {
                    result = false;
                }
            }
            else if (type == MoveType.XY)
            {
                if ((para.MaxMoveDistance[0] > 0 && Math.Abs(xyt[0]) > para.MaxMoveDistance[0]) ||
                    (para.MaxMoveDistance[1] > 0 && Math.Abs(xyt[1]) > para.MaxMoveDistance[1]))
                {
                    result = false;
                }
            }
            return result;
        }

        /// <summary>
        /// 显示消息
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="isError">是否为异常信息</param>
        private void ShowMessage(string text, bool isError)
        {
            MessageShown?.Invoke(this, new ShowMessageArgs() { Text = text, IsError = isError });
        }

        /// <summary>
        /// 接收到图像数据
        /// </summary>
        public event EventHandler<ImageReceivedArgs> ImageReceived;
        /// <summary>
        /// 显示消息
        /// </summary>
        public event EventHandler<ShowMessageArgs> MessageShown;
    }
}
