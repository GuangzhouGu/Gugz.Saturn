/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2020 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnMain.Params
// 文件名称    ：CommunicationParamaters.cs
// =================================
// 创 建 者    ：guguangzhou
// 创建日期    ：2021/01/29 09:00:00 
// 功能描述    ：PC通讯参数
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
using HYC.SaturnMain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYC.SaturnMain.Params
{
    public class CommPara : ParaBase,IPara
    {
        [System.Xml.Serialization.XmlIgnore]
        [Browsable(false)]
        public bool IsShow { get; set; } = true;
        [System.Xml.Serialization.XmlIgnore]
        [Browsable(false)]
        public ParaViewBase ParaViewBase { get; set; } = null;

        [Category("通讯屏蔽"), Description("TesterPC屏蔽")]
        public bool TesterPCShield { get; set; }
        [Category("通讯屏蔽"), Description("AoiPC1屏蔽")]
        public bool AoiPC1Shield { get; set; }
        [Category("通讯屏蔽"), Description("AoiPC2屏蔽")]
        public bool AoiPC2Shield { get; set; }
        [Category("通讯屏蔽"), Description("DFS屏蔽")]
        public bool DFSShield { get; set; }
        [Category("通讯屏蔽"), Description("心跳屏蔽")]
        public bool HeartShield { get; set; }

        [Category("超时参数"), DisplayName("OneColumnTestTimeout"), Description("Tester单列检测超时时间，单位秒")]
        public double OneColumnTestTimeout { get; set; }
        [Category("超时参数"), DisplayName("OnePanelStartTestTimeout"), Description("Tester单个panel开始检测超时时间，单位秒")]
        public double OnePanelStartTestTimeout { get; set; }
        [Category("超时参数"), DisplayName("OnePanelStopTestTimeout"), Description("Tester单个panel结束检测超时时间，单位秒")]
        public double OnePanelStopTestTimeout { get; set; }
        [Category("超时参数"), DisplayName("AlignCam1GrabTimeout"), Description("Align相机1拍照超时时间，单位秒")]
        public double AlignCamGrabTimeout { get; set; }
 
        [Category("超时参数"), DisplayName("IRCam1GrabTimeout"), Description("IR相机1拍照超时时间，单位秒")]
        public double IRCamGrabTimeout { get; set; }

        [Category("超时参数"), DisplayName("CtcCamOpenTimeout"), Description("Contact相机打开超时时间，单位秒")]
        public double CtcCamOpenTimeout { get; set; }
        [Category("超时参数"), DisplayName("ContactCamStopTimeout"), Description("Contact相机关闭超时时间，单位秒")]
        public double ContactCamStopTimeout { get; set; }
        [Category("超时参数"), DisplayName("ReviewCamGrabTimeout"), Description("Review相机拍照超时时间，单位秒")]
        public double ReviewCamGrabTimeout { get; set; }
        [Category("超时参数"), DisplayName("PcdAutoChangeTimeout"), Description("ProbeCard自动更换超时时间，单位秒")]
        public double PcdAutoChangeTimeout { get; set; }
        [Category("超时参数"), DisplayName("WaitOnePanelDetectComTimeout"), Description("等待单个panel不良检测完毕超时时间，单位秒")]
        public double WaitOnePanelDetectComTimeout { get; set; }
        [Category("超时参数"), DisplayName("ContactAlignTimeout"), Description("Contact相机计算ProbeUnit压头偏移超时时间，单位秒")]
        public double ContactAlignTimeout { get; set; }
        [Category("配方参数"), DisplayName("RecipeID"), Description("发送给TesterPC的配方ID")]
        public int RecipeID { get; set; }
        [System.Xml.Serialization.XmlIgnore]
        [Browsable(false)]
        public string ParaName { get; set; } = "通讯参数";

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
