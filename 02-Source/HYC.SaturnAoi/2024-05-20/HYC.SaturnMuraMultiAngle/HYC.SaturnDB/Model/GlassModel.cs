/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2022 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnDB.Model
// 文件名称    ：Class1
// =================================
// 创 建 者    ：guguangzhou
// 创建日期    ：
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

namespace HYC.SaturnDB.Model
{
    public class GlassModel
    {
        /// <summary>
        /// 玻璃序号,0和1
        /// </summary>
        public int GlassNum { get; set; }
  
        /// 条码
        /// </summary>
        public string GlassID { get; set; }
        /// <summary>
        /// 所有列的集合
        /// </summary>
        public List<OneColumnModel> ColumnList { get; set; }
        public GlassModel()
        {
            ColumnList = new List<OneColumnModel>();
        }
    }
    public class OneColumnModel
    {
        /// <summary>
        /// 条码
        /// </summary>
        public string GlassID { get; set; }
        /// <summary>
        /// 列序号，第一块玻璃0到4
        /// </summary>
        public int ColumnNum { get; set; }
        /// <summary>
        /// 测试状态，-1：等待检测，0：检测完成，1：检测中
        /// </summary>
        public int DetectSts { get; set; }
        /// <summary>
        /// 单列结果
        /// </summary>
        public string Result { get; set; }
        /// <summary>
        /// 一列的Panel
        /// </summary>
        public List<OpenShortDetectModel> PanelList { get; set; }
        public OneColumnModel()
        {
            PanelList = new List<OpenShortDetectModel>();
        }

    }
}
