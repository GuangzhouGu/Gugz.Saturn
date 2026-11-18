/*----------------------------------------------------------------
// Copyright (C) Suzhou HYC Electronic Technology Co.,LTD
// 版权所有。
//
// =================================
// CLR版本     ：4.0.30319.42000
// 命名空间    ：HYC.AutoPosition.Interface
// 文件名称    ：ISignalProvider.cs
// =================================
// 创 建 者    ：yangkang
// 创建日期    ：2020/1/13 19:33:25 
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
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYC.AutoPosition.Interface
{
    /// <summary>
    /// 信号提供者
    /// </summary>
    public interface ISignalProvider : IMoveExecutor
    {
        /// <summary>
        /// 自动对位完成
        /// </summary>
        /// <param name="groupIndex">对位组序号</param>
        /// <param name="stageIndex">工位序号</param>
        /// <param name="result">对位结果</param>
        void AutoPosCompleted(int groupIndex, int stageIndex, bool result);
        /// <summary>
        /// 自动标定完成
        /// </summary>
        /// <param name="groupIndex">对位组序号</param>
        /// <param name="stageIndex">工位序号</param>
        /// <param name="result">标定结果</param>
        void CalibrationCompleted(int groupIndex, int stageIndex, bool result);
        /// <summary>
        /// 运动完成
        /// </summary>
        /// <param name="groupIndex">对位组序号</param>
        /// <param name="type">请求运动的类型</param>
        /// <param name="result">运动结果</param>
        void MoveCompleted(int groupIndex, MoveReqType type, bool result);
        /// <summary>
        /// 更新对位平台状态
        /// </summary>
        /// <param name="platIndex">平台序号</param>
        /// <param name="status">平台状态</param>
        void UpdatePlatformStatus(int platIndex, PlatformStatus status);
        /// <summary>
        /// 更新相机状态
        /// </summary>
        /// <param name="cameraIndex">相机序号</param>
        /// <param name="isConnected">连接状态</param>
        void UpdateCameraStatus(int cameraIndex, bool isConnected);
         
        /// <summary>
        /// 自动对位请求
        /// </summary>
        event EventHandler<RequestEventArgs> AutoPosRequest;


        /// <summary>
        /// 自动标定请求
        /// </summary>
        event EventHandler<RequestEventArgs> CalibrationRequest;
        /// <summary>
        /// 运动请求
        /// </summary>
        event EventHandler<MoveReqEventArgs> MoveRequest;
        /// <summary>
        /// 安全条件变化
        /// </summary>
        event EventHandler<SafetyEventArgs> SafetyChanged;
        event EventHandler<RecipeSyncArgs> OnRecipeSync;
    }

    /// <summary>
    /// 信号请求参数类
    /// </summary>
    public class RequestEventArgs : EventArgs
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
        /// 检测模式 0：Mark图片 1：目标位图片
        /// </summary>
        public int DetectMode { get; set; }
        public bool IsSingleCam { get; set; }
    }
    public class RequestByDataEventArgs : EventArgs
    {
        /// <summary>
        /// 信号请求参数类
        /// </summary>
        public RequestEventArgs RequestEventArgs { get; set; }
        /// <summary>
        ///  原始数据
        /// </summary>
        public byte[] RawData { get; set; }
        /// <summary>
        /// 老化后数据
        /// </summary>
        public byte[] AgingData { get; set; }
    }
    

    /// <summary>
    /// 运动请求参数类
    /// </summary>
    public class MoveReqEventArgs : EventArgs
    {
        /// <summary>
        /// 对位组序号
        /// </summary>
        public int GroupIndex { get; set; }
        /// <summary>
        /// 请求运动的类型
        /// </summary>
        public MoveReqType MoveReqType { get; set; }
    }

    /// <summary>
    /// 对位平台的安全条件改变的参数类
    /// </summary>
    public class SafetyEventArgs : EventArgs
    {
        /// <summary>
        /// 对位组序号
        /// </summary>
        public int GroupIndex { get; set; }
        /// <summary>
        /// 移动是否安全
        /// </summary>
        public bool IsSaveToMove { get; set; }
    }
    public class RecipeSyncArgs : EventArgs
    {
        public string RecipeID { get; set; }
    }
    /// <summary>
    /// 运动请求类型
    /// </summary>
    public enum MoveReqType
    {
        /// <summary>
        /// 原点
        /// </summary>
        Home,
        /// <summary>
        /// 用户位置1
        /// </summary>
        UserPos1,
        /// <summary>
        /// 用户位置2
        /// </summary>
        UserPos2,
        /// <summary>
        /// 用户位置3
        /// </summary>
        UserPos3
    }

    /// <summary>
    /// 同步给信号提供者的对位平台状态
    /// </summary>
    public class PlatformStatus
    {
        /// <summary>
        /// 是否使能
        /// </summary>
        public bool IsServoOn { get; set; }
        /// <summary>
        /// 是否在原点
        /// </summary>
        public bool IsAtHome { get; set; }
        /// <summary>
        /// 是否在用户设定位置
        /// </summary>
        public bool[] IsAtUserPos { get; set; }
        /// <summary>
        /// 是否有报警
        /// </summary>
        public bool IsAlarm { get; set; }
    }
}
