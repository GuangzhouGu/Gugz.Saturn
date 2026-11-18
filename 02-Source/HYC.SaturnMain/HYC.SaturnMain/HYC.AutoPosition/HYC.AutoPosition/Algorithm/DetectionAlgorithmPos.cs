/*----------------------------------------------------------------
// Copyright (C) Suzhou HYC Electronic Technology Co.,LTD
// 版权所有。
//
// =================================
// CLR版本     ：4.0.30319.42000
// 命名空间    ：HYC.AutoPosition.Algorithm
// 文件名称    ：DetectionAlgorithmPos.cs
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
using HYC.AutoPosition.Parameter;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HYC.AutoPosition.Algorithm
{
    /// <summary>
    /// 对位算法参数结构体
    /// </summary>
    [Serializable]
    public struct AlgorithmParaDetail
    {
        /// <summary>
        /// 参数1
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
        public float[] param1;
        /// <summary>
        /// 参数2
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
        public float[] param2;
        /// <summary>
        /// 参数3
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
        public float[] param3;
        /// <summary>
        /// 参数4
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
        public float[] param4;
        /// <summary>
        /// 参数5
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
        public float[] param5;
        /// <summary>
        /// 参数6
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
        public float[] param6;
        /// <summary>
        /// 参数7
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
        public float[] param7;
        /// <summary>
        /// 参数8
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
        public float[] param8;
    }
            
    /// <summary>
    /// 对位算法
    /// </summary>
   public class DetectionAlgorithmPos
    {
        /// <summary>
        /// 更新参数
        /// </summary>
        /// <param name="nMember">检测通道</param>
        /// <param name="para">算法参数</param>
        /// <returns></returns>
        [DllImport("AutoAlignment.dll", EntryPoint = "HT_OnUpdateModuleInfo", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool HT_OnUpdateModuleInfo(int nMember, AlgorithmParaDetail para);

        /// <summary>
        /// 处理图片
        /// </summary>
        /// <param name="nMember">检测通道</param>
        /// <param name="dataIn">图像数据</param>
        /// <param name="nWidth">图像宽</param>
        /// <param name="nHeight">图像高</param>
        /// <param name="nIndex">第几张图片</param>
        /// <param name="nErr">错误代码</param>
        /// <returns></returns>
        [DllImport("AutoAlignment.dll", EntryPoint = "HT_OnHandleImage", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool HT_OnHandleImage(int nMember, byte[] dataIn, int nWidth, int nHeight, int nIndex, ref int nErr);

        /// <summary>
        /// 获取结果
        /// </summary>
        /// <param name="nMember">检测通道</param>
        /// <param name="dataOut">输出的图像数据</param>
        /// <param name="xyt">输出的xyt结果</param>
        /// <param name="reserve">保留</param>
        /// <returns></returns>
        [DllImport("AutoAlignment.dll", EntryPoint = "HT_OnUpdateImage", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool HT_OnUpdateImage(int nMember, byte[] dataOut, double[] xyt, int[] reserve);
    }

    /// <summary>
    /// 对位相机参数
    /// </summary>
    [Serializable]
    public class AlignCameraParameter
    {
        /// <summary>
        /// Camera 对应的旋转中心
        /// </summary>
        public PointF CenterPoint { get; set; }
        /// <summary>
        /// Camera 与Stage之间的夹角
        /// </summary>
        public double Theta { get; set; }

        /// <summary>
        /// 截取ROI的原图的大小
        /// </summary>
        public Size ImageSize;

        /// <summary>
        /// ROI区域设置
        /// </summary>
        public RectangleF ROI { get; set; }

        /// <summary>
        /// 归一化后的ROI区域
        /// </summary>
        public RectangleF NROI { get; set; }

        /// <summary>
        /// 模板图像
        /// </summary>
        public Bitmap TemplateImgs;
    }
    /// <summary>
    /// 流程参数
    /// </summary>
    [Serializable]
    public class FlowParaDetail
    {
        public CameraInformationArray CameraInformations { get; set; }
        /// <summary>
        /// 截取ROI的原图的大小
        /// </summary>
        public Size[] ImageSize = new Size[2];

        /// <summary>
        /// ROI区域设置
        /// </summary>
        public RectangleF[] ROI { get; set; } = new RectangleF[2];

        /// <summary>
        /// 归一化后的ROI区域
        /// </summary>
        public RectangleF[] NROI { get; set; } = new RectangleF[2];

        /// <summary>
        /// 模板图像
        /// </summary>
        public Bitmap[] TemplateImgs = new Bitmap[2];

        public FlowParaDetail()
        {
           
        }
    }
}
