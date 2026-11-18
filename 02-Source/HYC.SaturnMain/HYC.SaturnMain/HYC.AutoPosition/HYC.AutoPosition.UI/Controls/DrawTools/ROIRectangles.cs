/*----------------------------------------------------------------
// Copyright (C) Suzhou HYC Technology Co.,LTD
// 版权所有。
//
// =================================
// CLR版本     ：4.0.30319.42000
// 命名空间    ：HYC.AutoPosition.UI.Controls.DrawTools
// 文件名称    ：ROIRectangles.cs
// =================================
// 创 建 者    ：miaoliang
// 创建日期    ：2020/11/10 12:00:27 
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
using HYC.HTFile.HTXML;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawTools
{
    /// <summary>
    /// ROI矩形
    /// </summary>
    [Serializable]
    public class ROIRectangle
    {
        /// <summary>
        /// 归一化的矩形尺寸
        /// </summary>
        public RectangleF NormalizationRectangle;
        
        /// <summary>
        /// ROI类型是Mark或者Area
        /// </summary>
        public int ROIType;
    }

    /// <summary>
    /// ROI列表
    /// </summary>
    public class ROIRectangles
    {
        public List<ROIRectangle> RoiRects { get; set; }
        public ROIRectangles()
        {
            RoiRects = new List<ROIRectangle>();
        }

        public void Serialize(string fileName)
        {
            HTXMLSerializer.EntityToXMLFile(this, fileName);
        }
        public void LoadFromGraphics(DrawArea drawArea)
        {
            RoiRects.Clear();
            foreach (DrawObject o in drawArea.GraphicsList.graphicsList)
            {
                if (o is DrawRectangle) {
                    DrawRectangle drawRectangle = (DrawRectangle)o;
                    RectangleF clientRect = drawArea.ClientRectangle;
                    ROIRectangle roiRect = new ROIRectangle();
                    roiRect.NormalizationRectangle.X = drawRectangle.Rectangle.X / clientRect.Width;
                    roiRect.NormalizationRectangle.Y = drawRectangle.Rectangle.Y / clientRect.Height;
                    roiRect.NormalizationRectangle.Width = drawRectangle.Rectangle.Width / clientRect.Width;
                    roiRect.NormalizationRectangle.Height = drawRectangle.Rectangle.Height / clientRect.Height;
                    roiRect.ROIType = drawRectangle.ROIType;
                    RoiRects.Add(roiRect);
                }
            }
        }

        public static void LoadToGraphics(DrawArea drawArea, string fileName)
        {
            try
            {

                ROIRectangles roiRectangles = HTXMLSerializer.EntityFromXMLFile<ROIRectangles>(fileName);
                RectangleF clientRect = drawArea.ClientRectangle;
                foreach (ROIRectangle roiRect in roiRectangles.RoiRects)
                {
                    int X = (int)(roiRect.NormalizationRectangle.X * clientRect.Width);
                    int Y = (int)(roiRect.NormalizationRectangle.Y * clientRect.Height);
                    int Width = (int)(roiRect.NormalizationRectangle.Width * clientRect.Width);
                    int Height = (int)(roiRect.NormalizationRectangle.Height * clientRect.Height);
                    DrawRectangle drawRectangle = new DrawRectangle(X, Y, Width, Height);
                    drawRectangle.ClientRectangle = clientRect;
                    drawRectangle.NormalizationRectangle = roiRect.NormalizationRectangle;
                    drawRectangle.ROIType = roiRect.ROIType;
                    AddNewObject(drawArea, drawRectangle);
                }
                return;
            }
            catch (Exception ex)
            {

            }
           
        }

        protected static void AddNewObject(DrawArea drawArea, DrawObject o)
        {
            drawArea.GraphicsList.UnselectAll();

            o.Selected = false;
            drawArea.GraphicsList.Add(o);

            drawArea.Capture = true;
            drawArea.Refresh();
            drawArea.SetDirty();
        }
    }
}
