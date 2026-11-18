/*----------------------------------------------------------------
// Copyright (C) Suzhou HYC Technology Co.,LTD
// 版权所有。
//
// =================================
// CLR版本     ：4.0.30319.42000
// 命名空间    ：HYC.Alignment
// 文件名称    ：TemplateMatching.cs
// =================================
// 创 建 者    ：miaoliang
// 创建日期    ：2020/11/9 15:25:00 
// 功能描述    ：模板匹配，使用Halcon实现
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
using HalconDotNet;
using System;

namespace HYC.AutoPosition.ImageProcess
{
    public class TemplateMatching
    {
        public double MinScore = 0.8;
        private HTuple modelID = new HTuple();
        public void CreateShapeModel(HObject templateImage,int contrast = 30,int mincontrast =10)
        {
            HOperatorSet.CreateShapeModel(templateImage, "auto", new HTuple(new double[1] { -Math.PI }), new HTuple(new double[1] { Math.PI }), "auto", "none", "use_polarity", 27, 10, out modelID);
        }

        public void Search(HObject srcImage,out HTuple RowCheck, out HTuple ColumnCheck, out HTuple AngleCheck, out HTuple Score)
        {
            HOperatorSet.FindShapeModel(srcImage, modelID, new HTuple(new double[1] { -Math.PI }), new HTuple(new double[1] { Math.PI }), MinScore, 1, 0.5, "least_squares", 0, 0.7, out RowCheck, out ColumnCheck, out AngleCheck, out Score);
        }

        public void Search(HObject srcImage, HObject templateImage, out HTuple RowCheck, out HTuple ColumnCheck, out HTuple AngleCheck, out HTuple Score)
        {
            HOperatorSet.CreateShapeModel(templateImage, "auto", new HTuple(new double[1] { -Math.PI }), new HTuple(new double[1] { Math.PI }), "auto", "none", "use_polarity", 27, 10, out modelID);
            HOperatorSet.FindShapeModel(srcImage, modelID, new HTuple(new double[1] { -Math.PI }), new HTuple(new double[1] { Math.PI }), MinScore, 1, 0.5, "least_squares", 0, 0.7, out RowCheck, out ColumnCheck, out AngleCheck, out Score);
        }
    }
}
