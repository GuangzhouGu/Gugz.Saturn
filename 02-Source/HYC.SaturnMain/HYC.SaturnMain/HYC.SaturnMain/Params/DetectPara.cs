/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2020 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnMain.Params
// 文件名称    ：DetectPara.cs
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
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYC.SaturnMain.Params
{
    public class DetectPara : ParaBase, IPara
    {
        [System.Xml.Serialization.XmlIgnore]
        [Browsable(false)]
        public bool IsShow { get; set; } = true;
        [System.Xml.Serialization.XmlIgnore]
        [Browsable(false)]
        public ParaViewBase ParaViewBase { get; set; }
        [System.Xml.Serialization.XmlIgnore]
        [Browsable(false)]
        public string ParaName { get; set; } = "检测参数";

        [Category("功能屏蔽"),  Description("Glass检测屏蔽")]
        public bool[] GlassDetectShield { get; set; } = new bool[2];
        [Category("功能屏蔽"),  Description("单列检测屏蔽")]
        [TypeConverter(typeof(CollectionCountConvert))]
        [Editor(typeof(ComplexCollectionEditor<DetectShield>), typeof(UITypeEditor))]
        public List<DetectShield> ColDetectShield { get; set; } = new List<DetectShield>();
 
        [Category("功能屏蔽"), Description("OpenShort检测屏蔽")]
        public bool OpenShortDetectShield { get; set; }
        [Category("功能屏蔽"), Description("Aoi检测屏蔽")]
        public bool AoiDetectShield { get; set; }
        [Category("功能屏蔽"), Description("直视Mura检测屏蔽")]
        public bool MuraDetectShield { get; set; }
        [Category("功能屏蔽"), Description("斜视Mura检测屏蔽")]
        public bool SquintMuraDetectShield { get; set; }
        [Category("功能屏蔽"), Description("MulitiAngle检测屏蔽")]
        public bool MultiAngleDetectShield { get; set; }

        //[Category("检测参数"), Description("每个Cell最大不良检测数量(ShortNG)")]
        //public int MaxDetectNGCountPerCell { get; set; } = 5;
        //[Category("检测参数"), Description("每个Glass最大Panel检测数量(ShortNG)")]
        //public int MaxDetectPanelCountPerGlass { get; set; } = 105;
        //[Category("检测参数"), Description("每个Cell的不良数量大于该值时，此Cell不做IR检测(ShortNG)")]
        //public int ThresholdPerCell { get; set; }
        //[Category("检测参数"), Description("老化和IR相机拍照时间间隔")]
        //public double IRCamGrabInterval { get; set; }
        //[Category("检测参数"), Description("高低倍率IR拍照间隔时间")]
        //public double HighLowIRCamGrabInterval { get; set; }

        //[Category("检测参数"),  Description("X方向检测范围偏移值，单位毫米")]
        //public double DetectRangeOffsetX { get; set; }
        //[Category("检测参数"), Description("Y方向检测范围偏移值，单位毫米")]
        //public double DetectRangeOffsetY { get; set; }
        [Category("延时时间设置"), Description("一定时间内没有检测到Glass，设备切换到Idle状态，单位秒")]
        public double IdleDelayTime { get; set; }
        [Category("延时时间设置"), Description("压接完成后延时一段时间再发送检测命令，单位秒")]
        public double DetectDelayTime { get; set; }
        [Category("超时时间设置"), Description("检测超时时间，单位秒")]
        public double DetectTimeOut { get; set; } = 30;
        [Category("超时时间设置"), Description("Tester切换模式超时时间，单位秒")]
        public double TesterModeSwitchTimeOut { get; set; } = 5;
        [Category("超时时间设置"), Description("PG控制超时时间，单位秒")]
        public double PGPowerControlTimeOut { get; set; } = 5;
        public IPara LoadPara()
        {
            return Load(this);
        }
        public bool SavePara()
        {
            return Save(this);
        }

        public class DetectShield
        {
            [Category("检测屏蔽"), Description("OpenShort检测屏蔽数组")]
            public bool[] OpenShortDetectShield { get; set; } = new bool[5];
            [Category("检测屏蔽"), Description("Aoi检测屏蔽数组")]
            public bool[] AoiDetectShield { get; set; } = new bool[5];
            [Category("检测屏蔽"), Description("直视Mura检测屏蔽数组")]
            public bool[] MuraDetectShield { get; set; } = new bool[5];
            [Category("检测屏蔽"), Description("斜视Mura检测屏蔽数组")]
            public bool[] SquintMuraDetectShield { get; set; } = new bool[5];
            [Category("检测屏蔽"), Description("MultiAngle检测屏蔽数组")]
            public bool[] MultiAngleDetectShield { get; set; } = new bool[5];
        }
    }
}
