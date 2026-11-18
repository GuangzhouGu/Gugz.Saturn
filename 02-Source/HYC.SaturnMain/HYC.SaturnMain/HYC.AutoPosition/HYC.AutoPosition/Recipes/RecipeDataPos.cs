/*----------------------------------------------------------------
// Copyright (C) Suzhou HYC Electronic Technology Co.,LTD
// 版权所有。
//
// =================================
// CLR版本     ：4.0.30319.42000
// 命名空间    ：HYC.AutoPosition.Recipes
// 文件名称    ：RecipeDataPos.cs
// =================================
// 创 建 者    ：yangkang
// 创建日期    ：2019/12/30 15:42:44 
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
using HYC.AutoPosition.Helper;
using HYC.AutoPosition.Parameter;
using HYC.HTFile.HTXML;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYC.AutoPosition.Recipes
{
    /// <summary>
    /// 对位机种数据
    /// </summary>
    [Serializable]
    public class RecipeDataPos
    {
        private const int paraCount = 50;       
        /// <summary>
        /// 第几组对位
        /// </summary>
        public int index = 0;
        /// <summary>
        /// 对位参数
        /// </summary>
        public AlgorithmParaDetail AlgorithmData = new AlgorithmParaDetail();
        /// <summary>
        /// 流程参数
        /// </summary>      
        public FlowParaDetail FlowPara = new FlowParaDetail();

        /// <summary>
        /// 初始化对位参数
        /// </summary>
        /// <param name="index">第几组对位</param>
        public void InitData(int index)
        {
            this.index = index;
            AlgorithmData = new AlgorithmParaDetail();
            AlgorithmData.param1 = new float[paraCount];        
            AlgorithmData.param2 = new float[paraCount];
            AlgorithmData.param3 = new float[paraCount];
            AlgorithmData.param4 = new float[paraCount];
            AlgorithmData.param5 = new float[paraCount];
            AlgorithmData.param6 = new float[paraCount];
            AlgorithmData.param7 = new float[paraCount];
            AlgorithmData.param8 = new float[paraCount];
        }

        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="strLocation">机种目录</param>
        public void ReadData(string strLocation)
        {
            string strFile = string.Empty;
            this.FlowPara.CameraInformations = CameraInformationArray.Unserialize(Path.Combine(strLocation, (index + 1).ToString(), "CameraInformation.xml"));
            //Template Matching Algorithm
            strFile = Path.Combine(strLocation, (index + 1).ToString(), "LeftRoi.bmp");
            if (File.Exists(strFile))
            {
                Bitmap bmp = new Bitmap(strFile);
                //Changed by miaoliang,
                // Because  FlowPara.TemplateImgs[0] = bmp;This is shallow copy,the file will be locked.
                // we cant register a new roi bmp;
                FlowPara.TemplateImgs[0] = DeepCopyHelper.DeepCopyWithBinarySerializer<Bitmap>(bmp); //(Bitmap)Image.FromFile();
                bmp.Dispose();
            }
            strFile = Path.Combine(strLocation, (index + 1).ToString(), "LeftROI.xml");
            if (File.Exists(strFile))
            {
                AlignROIArray alignROIArray = AlignROIArray.Unserialize(strFile);
                FlowPara.ROI[0] = alignROIArray.AlignROIs[0].MarkROI;
                FlowPara.ImageSize[0] = alignROIArray.ImageSize;
            }
               
            strFile = Path.Combine(strLocation, (index + 1).ToString(), "RightRoi.bmp");
            if (File.Exists(strFile))
            {
                //Changed by miaoliang,
                // Because  FlowPara.TemplateImgs[0] = bmp;This is shallow copy,the file will be locked.
                // we cant register a new roi bmp;
                Bitmap bmp = new Bitmap(strFile);
                FlowPara.TemplateImgs[1] = DeepCopyHelper.DeepCopyWithBinarySerializer<Bitmap>(bmp); //(Bitmap)Image.FromFile();
                bmp.Dispose();
            }
            strFile = Path.Combine(strLocation, (index + 1).ToString(), "RightROI.xml");
            if (File.Exists(strFile))
            {
                AlignROIArray alignROIArray = AlignROIArray.Unserialize(strFile);
                FlowPara.ROI[1] = alignROIArray.AlignROIs[0].MarkROI;
                FlowPara.ImageSize[1] = alignROIArray.ImageSize;
            }
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="strLocation">机种目录</param>
        public void SaveData(string strLocation)
        {
            if (Directory.Exists(strLocation) == false) Directory.CreateDirectory(strLocation);
            string strFile = string.Empty;
            //序列化到文件
           // HTXMLSerializer.EntityToXMLFile(AlgorithmData, strFile);
            this.FlowPara.CameraInformations.Serialize(Path.Combine(strLocation, (index + 1).ToString(), "CameraInformation.xml"));
            if (FlowPara.TemplateImgs[0] != null)
            {
                strFile = Path.Combine(strLocation, (index + 1).ToString(), "LeftRoi.bmp");
                //FlowPara.TemplateImgs[0].Save(strFile, ImageFormat.Bmp);
            }
            if (FlowPara.TemplateImgs[1] != null)
            {
                strFile = Path.Combine(strLocation, (index + 1).ToString(), "RightRoi.bmp");
                //FlowPara.TemplateImgs[1].Save(strFile, ImageFormat.Bmp);
            }
        }
    }
}
