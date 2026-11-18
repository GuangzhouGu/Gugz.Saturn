/*----------------------------------------------------------------
// Copyright (C) Suzhou HYC Electronic Technology Co.,LTD
// 版权所有。
//
// =================================
// CLR版本     ：4.0.30319.42000
// 命名空间    ：HYC.AutoPosition.Parameter
// 文件名称    ：ImageData.cs
// =================================
// 创 建 者    ：yangkang
// 创建日期    ：2020/1/2 14:44:00 
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
using HYC.HTImage;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYC.AutoPosition.Parameter
{
    /// <summary>
    /// 图像数据封装
    /// </summary>
    public class ImageData
    {
        /// <summary>
        /// 图像字节数组
        /// </summary>
        public byte[] ImageBytes { get; set; }
        /// <summary>
        /// 图像的Width
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// 图像的Height
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// 将字节数组转换为Bitmap对象
        /// </summary>
        /// <returns></returns>
        public Bitmap ToBitmap()
        {
            if (ImageBytes.Length == Width * Height)
                return BitmapConverter.ArrayToBitmap(ImageBytes, Height, Width);
            else
                return BitmapConverter.ArrayTo32RgbBitmap(ImageBytes, Height, Width, true);
        }

        /// <summary>
        /// 将Bitmap对象转换为字节数组
        /// </summary>
        /// <param name="bitmap"></param>
        public void FromBitmap(Bitmap bitmap)
        {
            ImageBytes = BitmapConverter.BitmapToArray(bitmap, out int height, out int width);
            Width = width;
            Height = height;
        }
    }
}
