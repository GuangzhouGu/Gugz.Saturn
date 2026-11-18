/*----------------------------------------------------------------
// Copyright (C) Suzhou HYC Electronic Technology Co.,LTD
// 版权所有。
//
// =================================
// CLR版本     ：4.0.30319.42000
// 命名空间    ：HYC.AutoPosition.Algorithm
// 文件名称    ：ImageProc.cs
// =================================
// 创 建 者    ：yangkang
// 创建日期    ：2020/10/11 9:11:55 
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
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HYC.AutoPosition.Algorithm
{
    /// <summary>
    /// 图像处理
    /// </summary>
    public class ImageProc
    {
        /// <summary>
        /// 模板匹配
        /// </summary>
        /// <param name="srcData">输入图像数据</param>
        /// <param name="srcWidth">输入图像width</param>
        /// <param name="srcHeight">输入图像height</param>
        /// <param name="templateData">模板图像数据</param>
        /// <param name="templateWidth">模板图像width</param>
        /// <param name="templateHeight">模板图像height</param>
        /// <param name="roi">ROI区域</param>
        /// <param name="matchRect">匹配结果</param>
        /// <returns>模板匹配是否成功</returns>
        [DllImport("ImageProc.dll")]
        public static extern int matchTemplate(byte[] srcData, int srcWidth, int srcHeight,
            byte[] templateData, int templateWidth, int templateHeight, int[] roi, int[] matchRect);

        /// <summary>
        /// 查找连通域，输出中心坐标
        /// </summary>
        /// <param name="srcData">输入图像数据</param>
        /// <param name="srcWidth">输入图像width</param>
        /// <param name="srcHeight">输入图像height</param>
        /// <param name="roi">ROI区域</param>
        /// <param name="location">连通域中心坐标</param>
        /// <returns>处理是否成功</returns>
        [DllImport("ImageProc.dll")]
        public static extern int findCenter(byte[] srcData, int srcWidth, int srcHeight,
            int[] roi, float[] location);

        /// <summary>
        /// 找两条线，计算线的交点
        /// </summary>
        /// <param name="srcData">输入图像数据</param>
        /// <param name="srcWidth">输入图像width</param>
        /// <param name="srcHeight">输入图像height</param>
        /// <param name="roi">ROI区域</param>
        /// <param name="location">线的交点及角度</param>
        /// <returns>处理是否成功</returns>
        [DllImport("ImageProc.dll")]
        public static extern int findLineCross(byte[] srcData, int srcWidth, int srcHeight,
            int[] roi, float[] location);

        /// <summary>
        /// 绘制矩形
        /// </summary>
        /// <param name="srcData">输入图像数据</param>
        /// <param name="srcWidth">输入图像width</param>
        /// <param name="srcHeight">输入图像height</param>
        /// <param name="rect">绘制区域</param>
        /// <param name="dstData">输出图像数据</param>
        [DllImport("ImageProc.dll")]
        public static extern void drawRectangle(byte[] srcData, int srcWidth, int srcHeight,
            int[] rect, byte[] dstData);

        /// <summary>
        /// 绘制十字线
        /// </summary>
        /// <param name="srcData">输入图像数据</param>
        /// <param name="srcWidth">输入图像width</param>
        /// <param name="srcHeight">输入图像height</param>
        /// <param name="x">坐标X</param>
        /// <param name="y">坐标Y</param>
        /// <param name="t">十字线的角度(-90~90)</param>
        /// <param name="dstData">输出图像数据</param>
        [DllImport("ImageProc.dll")]
        public static extern void drawCross(byte[] srcData, int srcWidth, int srcHeight,
            float x, float y, float t, byte[] dstData);

        /// <summary>
        /// 灰度图转彩色图，或彩色图转灰度图
        /// </summary>
        /// <param name="srcData"></param>
        /// <param name="srcWidth"></param>
        /// <param name="srcHeight"></param>
        /// <param name="dstData"></param>
        /// <param name="toColor"></param>
        [DllImport("ImageProc.dll")]
        public static extern void cvtColor(byte[] srcData, int srcWidth, int srcHeight,
            byte[] dstData, bool toColor);

        /// <summary>
        /// 图像旋转
        /// </summary>
        /// <param name="srcData">输入图像数据</param>
        /// <param name="srcWidth">输入图像width</param>
        /// <param name="srcHeight">输入图像height</param>
        /// <param name="rotateCode">旋转方式</param>
        /// <param name="dstData">输出图像数据</param>
        /// <param name="dstWidth">输出图像width</param>
        /// <param name="dstHeight">输出图像height</param>
        [DllImport("ImageProc.dll")]
        public static extern void rotate(byte[] srcData, int srcWidth, int srcHeight, RotateFlags rotateCode,
            byte[] dstData, out int dstWidth, out int dstHeight);

        /// <summary>
        /// 图像翻转
        /// </summary>
        /// <param name="srcData">输入图像数据</param>
        /// <param name="srcWidth">输入图像width</param>
        /// <param name="srcHeight">输入图像height</param>
        /// <param name="flipCode">翻转方式</param>
        /// <param name="dstData">输出图像数据</param>
        /// <param name="dstWidth">输出图像width</param>
        /// <param name="dstHeight">输出图像height</param>
        [DllImport("ImageProc.dll")]
        public static extern void flip(byte[] srcData, int srcWidth, int srcHeight, FlipFlags flipCode,
            byte[] dstData, out int dstWidth, out int dstHeight);

        /// <summary>
        /// 图像旋转标识
        /// </summary>
        public enum RotateFlags
        {
            /// <summary>
            /// Rotate 90 degrees clockwise
            /// </summary>
            ROTATE_90_CLOCKWISE = 0, 
            /// <summary>
            /// Rotate 180 degrees clockwise
            /// </summary>
            ROTATE_180 = 1, 
            /// <summary>
            /// Rotate 270 degrees clockwise
            /// </summary>
            ROTATE_90_COUNTERCLOCKWISE = 2, 
        }

        /// <summary>
        /// 图像翻转标识
        /// </summary>
        public enum FlipFlags
        {
            /// <summary>
            /// 沿X轴翻转
            /// </summary>
            FLIP_X = 0,
            /// <summary>
            /// 沿Y轴翻转
            /// </summary>
            FLIP_Y = 1,
            /// <summary>
            /// 沿X轴和Y轴翻转
            /// </summary>
            FLIP_XY = -1
        }
    }
}
