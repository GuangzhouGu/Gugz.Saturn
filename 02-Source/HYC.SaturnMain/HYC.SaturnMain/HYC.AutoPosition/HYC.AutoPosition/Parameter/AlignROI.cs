/*----------------------------------------------------------------
// Copyright (C) Suzhou HYC Technology Co.,LTD
// 版权所有。
//
// =================================
// CLR版本     ：4.0.30319.42000
// 命名空间    ：HYC.AutoPosition.UI.Controls.Template
// 文件名称    ：AlignROI.cs
// =================================
// 创 建 者    ：miaoliang
// 创建日期    ：2020/11/30 16:23:41 
// 功能描述    ：对位ROI信息
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
using HYC.HTFile.HTXML;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYC.AutoPosition.Parameter
{

    /// <summary>
    /// 对位ROI信息
    /// </summary>
    [Serializable]
    public class AlignROI
    {
        /// <summary>
        /// 实际截取时针对ROI的区域
        /// </summary>
        public RectangleF MarkROI { get; set; }
        /// <summary>
        /// 实际ROI的需要寻找的区域
        /// </summary>
        public RectangleF AreaROI { get; set; }

        /// <summary>
        /// 对位时实际使用的中心
        /// </summary>
        public PointF CenterPoint { get; set; }

        /// <summary>
        /// ROI图片的位置
        /// </summary>
        public string ROIImageFilePath { get; set; }
    }

    /// <summary>
    /// 多组ROI集合
    /// </summary>
    [Serializable]
    public class AlignROIArray
    {
        /// <summary>
        /// 多组ROI
        /// </summary>
        public List<AlignROI> AlignROIs { get; set; }
        /// <summary>
        /// 截取ROI时，图片的大小
        /// </summary>
        public Size ImageSize { get; set; }

        /// <summary>
        /// 原始图片的位置
        /// </summary>
        public string ImageFilePath { get; set; }

        /// <summary>
        /// 初始化
        /// </summary>
        public AlignROIArray()
        {
            AlignROIs = new List<AlignROI>();
        }

        /// <summary>
        /// 序列化参数到文件中
        /// </summary>
        /// <param name="fileName"></param>
        public void Serialize(string fileName)
        {
            HTXMLSerializer.EntityToXMLFile(this, fileName);
        }

        /// <summary>
        /// 从文件中反序列化
        /// </summary>
        /// <param name="fileName"></param>
        public static AlignROIArray Unserialize(string fileName)
        {
            AlignROIArray alignROIArray = HTXMLSerializer.EntityFromXMLFile<AlignROIArray>(fileName);
            return alignROIArray;
        }

        public void Add(AlignROI alignROI)
        {
            AlignROIs.Add(alignROI);
        }

        /// <summary>
        /// 索引器
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public AlignROI this[int index]
        {
            get
            {
                if (index < 0 || index >= AlignROIs.Count)
                {
                    return null;
                 }
                return AlignROIs[index];
            }
            set
            {
                if (index <= 0 || index >= AlignROIs.Count)
                {
                    return;
                }
                AlignROIs[index] = value;
            }
        }
    }
}
