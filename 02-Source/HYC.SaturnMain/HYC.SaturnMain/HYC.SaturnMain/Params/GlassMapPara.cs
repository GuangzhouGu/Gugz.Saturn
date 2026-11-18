/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2022 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnMain.Params
// 文件名称    ：GlassMapPara
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
using HYC.HTPCBase.Extension.Para;
using HYC.HTPCBase.Extension.Views;
using HYC.SaturnMain.SubForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace HYC.SaturnMain.Params
{
    public class GlassMapPara:ParaBase,IPara
    {
        [System.Xml.Serialization.XmlIgnore]
        [Browsable(false)]
        public bool IsShow { get; set; } = true;
        [System.Xml.Serialization.XmlIgnore]
        [Browsable(false)]
        public ParaViewBase ParaViewBase { get; set; } = new FrmGlassCrds();
        [System.Xml.Serialization.XmlIgnore]
        [Browsable(false)]
        public string ParaName { get; set; } = "Glass坐标参数";
        public List<OneColumnCrd> ColumnCrdList = null;
        public IPara LoadPara()
        {
            return Load(this);
        }
        public bool SavePara()
        {
            return Save(this);
        }
    }
    public class OneColumnCrd
    {
        public int ColumnNum { get; set; }
        public List<OneCellCrd> CellCrdList = null;
        public OneColumnCrd()
        {
            CellCrdList = new List<OneCellCrd>();
        }
    }
    /// <summary>
    /// 每个Cell基准点坐标
    /// </summary>
    public class OneCellCrd
    {
        public int RowNum { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public override string ToString()
        {
            return $"{X},{Y}";
        }
    }
}
