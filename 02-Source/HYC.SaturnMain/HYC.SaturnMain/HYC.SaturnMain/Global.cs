/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2020 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnMain
// 文件名称    ：Global.cs
// =================================
// 创 建 者    ：guguangzhou
// 创建日期    ：2021/01/29 09:00:00 
// 功能描述    ：全局变量管理类
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


using HYC.HTPCBase.Extension.Recipe;
using HYC.HTFile.HTINI;
using HYC.HTLog.Core;
using HYC.HTLog.Logs;
using HYC.SaturnMain.Params;
using HYC.SaturnMain.Entities;
using HYC.SaturnMain.UVWStage;
using System;
using System.Collections.Generic;
using HYC.SaturnDB.Model;
using HYC.HTPCBase.Views;
using HYC.SaturnMain.Entities;
using HYC.MicroLedATDetect.AutoPos;

namespace HYC.SaturnMain
{
    class Global
    {
        /// <summary>
        /// Glass数量
        /// </summary>
        public const int GlassCount = 2;
        public static OpenShortNGInfoModel CurrentNGInfo = null;
        public static Dictionary<int, GlassModel> GlassDic = null;//存储Glass检测信息
        public static string[] GlassID = new string[2] { string.Empty,string.Empty};
        /// <summary>
        /// 当前检测Glass信息
        /// </summary>
        public static CurrentDetectInfo CurDetectInfo = null;
        public const int ErrorStep = -1000;//状态机错误步骤
        public static string ReceivedRecipeName = null;//要更换的ProbeCard机种名
        public static EqpSts EqpStatus = EqpSts.STOP;
        public static int InterfaceMode=-1;//和机械手交互方式，0：Load，1：Exchange
        public static string[] CellID = new string[] { "AA", "AB", "AC", "AD", "AE", "AF", "AG", "AH", "AJ", "AK", "AL", "AM", "AN", "AP", "AQ", "AR", "AS", "AT", "AU", "AV", "AW", "AX", "AY", "AZ" };
        public static AutoPosInfo AutoPosInfo = null;
        public static bool IsAutoPosOnly;
        public static AlignSignalProvider SignalProvider = new AlignSignalProvider();
        public static string GetCellID(int colNum, int cellNum)
        {
            return CellID[cellNum] + $"0{colNum + 1}";
        }
    }

  
    public enum PrbChgMode
    {
        /// <summary>
        /// 只安装
        /// </summary>
        Load,
        /// <summary>
        /// 只卸载
        /// </summary>
        Unload,
        /// <summary>
        /// 先卸载再安装
        /// </summary>
        UnloadLoad,
    }
    public enum Status
    {
        /// <summary>
        /// 初始值
        /// </summary>
        None=-1,
        /// <summary>
        /// 完成
        /// </summary>
        OK,
        /// <summary>
        /// 失败
        /// </summary>
        Fail,
    }
    public enum RunMode
    {
        /// <summary>
        /// 正常模式
        /// </summary>
        Normal,
       /// <summary>
       /// 千传模式
       /// </summary>
        ColdRun,
        /// <summary>
        /// 空跑模式，不检测，不压接
        /// </summary>
        NoDetect,
    }
    public enum DetectType
    {
        Aoi,
        /// <summary>
        /// 直视Mura
        /// </summary>
        Mura,
        /// <summary>
        /// 斜视Mura
        /// </summary>
        SquintMura,
        /// <summary>
        /// 多角度
        /// </summary>
        MultiAngle,
        OpenShort
    }
    public enum CameraName
    {
        /// <summary>
        /// 1号检测模组Aoi相机
        /// </summary>
        AoiCam1,
        /// <summary>
        /// 1号检测模组直视Mura
        /// </summary>
        MuraCam1,
        /// <summary>
        /// 1号检测模组斜视Mura
        /// </summary>
        SquintMuraCam1,
        /// <summary>
        /// 1号检测模组多角度光谱仪
        /// </summary>
        MultiAngle1,
        /// <summary>
        /// 2号检测模组Aoi相机
        /// </summary>
        AoiCam2,
        /// <summary>
        /// 2号检测模组直视Mura
        /// </summary>
        MuraCam2,
        /// <summary>
        /// 2号检测模组斜视Mura
        /// </summary>
        SquintMuraCam2,
        /// <summary>
        /// 2号检测模组多角度光谱仪
        /// </summary>
        MultiAngle2,
    }
    public enum DetectModule
    {
        /// <summary>
        /// 检测模组1
        /// </summary>
        DetectModule1,
        /// <summary>
        /// 检测模组2
        /// </summary>
        DetectModule2,
    }
}
