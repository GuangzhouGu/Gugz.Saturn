/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2020 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnMain.Params
// 文件名称    ：AlignParamaters.cs
// =================================
// 创 建 者    ：guguangzhou
// 创建日期    ：2021/01/29 09:00:00 
// 功能描述    ：对位参数
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
using HYC.HTPCBase.Extension.Entities;
using HYC.HTPCBase.Extension.Para;
using HYC.HTPCBase.Extension.Views;
using HYC.SaturnMain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYC.SaturnMain.Params
{
    public class AlignPara : ParaBase, IPara
    {
        [System.Xml.Serialization.XmlIgnore]
        [Browsable(false)]
        public bool IsShow { get; set; } = true;
        [System.Xml.Serialization.XmlIgnore]
        [Browsable(false)]
        public ParaViewBase ParaViewBase { get; set; } = null;
        [System.Xml.Serialization.XmlIgnore]
        [Browsable(false)]
        public string ParaName { get; set; } = "对位参数";
        [Category("对位参数设置"), Description("X方向对位范围，单位毫米")]
        public double AlignRangeX { get; set; }
        [Category("对位参数设置"), Description("Y方向对位范围，单位毫米")]
        public double AlignRangeY { get; set; }
        [Category("对位参数设置"),  Description("T方向对位范围，单位度")]
        public double AlignRangeT { get; set; }
        [Category("对位参数设置"), Description("X方向取反")]
        public bool[] IsInverseX { get; set; } = new bool[2];
        [Category("对位参数设置"), Description("Y方向取反")]
        public bool[] IsInverseY { get; set; } = new bool[2];
        [Category("对位参数设置"), Description("T方向取反")]
        public bool[] IsInverseT { get; set; } = new bool[2];
        [Category("对位参数设置"),  Description("压头方向取反")]
        public bool IsInverseProbeDir { get; set; }
        [Category("对位参数设置"), Description("对位相机X2方向取反")]
        public bool AlignCamX2Dir { get; set; }
        [Category("对位参数设置"), Description("对位重试次数")]

        public int ReAlignCount { get; set; }
        [Category("对位参数设置"), Description("对位时是否移动压头")]
        public bool IsAlignProbe { get; set; }
        [Category("对位补偿值设置"), Description("X方向补偿值，重新示教点位后要清零，单位毫米")]
        public double[] OffsetX { get; set; }
        [Category("对位补偿值设置"),  Description("Y方向补偿值，重新示教点位后要清零，单位毫米")]
        public double[] OffsetY { get; set; }
        [Category("对位补偿值设置"),  Description("T方向补偿值，顺时针减，逆时针加。重新示教点位后要清零，单位度")]
        public double[] OffsetT { get; set; }

        [Category("对位补偿值设置"), Description("Glass每列偏移值")]
        [TypeConverter(typeof(CollectionCountConvert))]
        [Editor(typeof(ComplexCollectionEditor<ColumnOffset>), typeof(UITypeEditor))]
        public List<ColumnOffset> GlassColumnOffsets { get; set; } = new List<ColumnOffset>();
        [Category("对位补偿值设置"), Description("是否开启单列补偿功能")]
        public bool[] IsEnableOffset { get; set; }
        [Category("对位参数设置"), Description("对位屏蔽")]
        public bool IsAlignShield { get; set; }
        [Category("对位参数设置"), Description("对位超时时间")]
        public double AlignTimeout { get; set; } = 10;
        public IPara LoadPara()
        {
            return Load(this);
        }
        public bool SavePara()
        {
            return Save(this);
        }
    }
    public class ColumnOffset
    {
        [Category("ProbeCard偏移值"), Description("每列偏移值")]
        public List<double> ColumnOffsets { get; set; } = new List<double>();
    }

}
