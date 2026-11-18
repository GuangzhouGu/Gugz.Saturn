/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2020 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnMain.Params
// 文件名称    ：ThreeDParamaters.cs
// =================================
// 创 建 者    ：guguangzhou
// 创建日期    ：2021/01/29 09:00:00 
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
using HYC.HTPCBase.Extension.Entities;
using HYC.HTPCBase.Extension.Para;
using HYC.HTPCBase.Extension.Views;
using HYC.SaturnMain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYC.SaturnMain.Params
{
    public class ThreeDPara : ParaBase,IPara
    {
        [System.Xml.Serialization.XmlIgnore]
        [Browsable(false)]
        public bool IsShow { get; set; } = true;
        [System.Xml.Serialization.XmlIgnore]
        [Browsable(false)]
        public ParaViewBase ParaViewBase { get; set; }
        [System.Xml.Serialization.XmlIgnore]
        [Browsable(false)]
        public string ParaName { get; set; } = "3D参数";
        [Category("位置倍率设置"), DisplayName("ProbeGantryRate"), Description("Probe龙门位置倍率")]
        public int ProbeGantryRate { get; set; }
        [Category("位置倍率设置"), DisplayName("DetectGantryRate"), Description("Probe龙门位置倍率")]
        public int DetectGantryRate { get; set; }
        [Category("位置倍率设置"), DisplayName("Stage1YRate"), Description("平台1Y轴位置倍率")]
        public int Stage1YRate { get; set; }
        [Category("位置倍率设置"), DisplayName("Stage2YRate"), Description("平台2Y轴位置倍率")]
        public int Stage2YRate { get; set; }
        [Category("位置倍率设置"), DisplayName("Stage1TRate"), Description("平台1T轴位置倍率")]
        public int Stage1TRate { get; set; }
        [Category("位置倍率设置"), DisplayName("Stage2TRate"), Description("平台2T轴位置倍率")]
        public int Stage2TRate { get; set; }
        [Category("位置倍率设置"), DisplayName("AlignCamX1"), Description("对位相机1X轴位置倍率")]
        public int AlignCamX1 { get; set; }
        [Category("位置倍率设置"), DisplayName("AlignCamX2"), Description("对位相机2X轴位置倍率")]
        public int AlignCamX2 { get; set; }
        [Category("位置倍率设置"), DisplayName("AlignCamZ1"), Description("对位相机1Z轴位置倍率")]
        public int AlignCamZ1 { get; set; }
        [Category("位置倍率设置"), DisplayName("AlignCamZ2"), Description("对位相机2Z轴位置倍率")]
        public int AlignCamZ2 { get; set; }
        [Category("位置倍率设置"), DisplayName("IRCamX"), Description("红外相机X轴位置倍率")]
        public int IRCamX { get; set; }
        [Category("位置倍率设置"), DisplayName("IRCamZ"), Description("红外相机Z轴位置倍率")]
        public int IRCamZ { get; set; }
        [Category("位置倍率设置"), DisplayName("ReviewCamX"), Description("Review相机X轴位置倍率")]
        public int ReviewCamX { get; set; }
        [Category("位置倍率设置"), DisplayName("ReviewCamZ"), Description("Review相机Z轴位置倍率")]
        public int ReviewCamZ { get; set; }
        [Category("屏蔽设置"), DisplayName("Shield3D"), Description("屏蔽3D显示")]
        public bool Shield3D { get; set; }
        public IPara LoadPara()
        {
            return Load(this);
        }
        public bool SavePara()
        {
            return Save(this);
        }
    }
}
