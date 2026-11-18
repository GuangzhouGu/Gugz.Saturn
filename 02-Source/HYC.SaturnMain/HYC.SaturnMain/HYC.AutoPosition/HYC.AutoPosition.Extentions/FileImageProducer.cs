/*----------------------------------------------------------------
// Copyright (C) Suzhou HYC Electronic Technology Co.,LTD
// 版权所有。
//
// =================================
// CLR版本     ：4.0.30319.42000
// 命名空间    ：HYC.AutoPosition.Extentions
// 文件名称    ：FileImageProducer.cs
// =================================
// 创 建 者    ：yangkang
// 创建日期    ：2020/1/2 17:29:35 
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
using HYC.AutoPosition.Parameter;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYC.AutoPosition.Extentions
{
    /// <summary>
    /// 从文件获取图像的图像数据生产者
    /// </summary>
    public class FileImageProducer : IImageProducer
    {
        //文件名
        private string fullFileName;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="fullFileName">图像文件名</param>
        public FileImageProducer(string fullFileName)
        {
            this.fullFileName = fullFileName;
        }
                
        /// <summary>
        /// 获取图像数据
        /// </summary>
        /// <returns>图形数据</returns>
        public ImageData CreateImageData()
        {
            Bitmap bitmap = (Bitmap)Image.FromFile(fullFileName);
            ImageData imageData = new ImageData();
            imageData.FromBitmap(bitmap);
            return imageData;
        }
    }
}
