/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2020 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnMain
// 文件名称    ：ControlParamaters.cs
// =================================
// 创 建 者    ：guguangzhou
// 创建日期    ：2021/01/25 09:00:00 
// 功能描述    ：软件控制参数类
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
    /// <summary>
    /// 控制参数
    /// </summary>
    public class ControlPara : ParaBase,IPara
    {
        [System.Xml.Serialization.XmlIgnore]
        [Browsable(false)]
        public bool IsShow { get; set; } = true;
        [System.Xml.Serialization.XmlIgnore]
        [Browsable(false)]
        public ParaViewBase ParaViewBase { get; set; } = null;
        [System.Xml.Serialization.XmlIgnore]
        [Browsable(false)]
        public string ParaName { get; set; } = "控制参数";
 
        [Category("功能屏蔽"),  Description("扫码枪屏蔽")]
        public bool ScanerShield { get; set; }       

        [Category("功能屏蔽"),  Description("InterLock屏蔽")]
        public bool InterLockShield { get; set; }
        [Category("功能屏蔽"), Description("是否启用Contact相机")]
        public bool IsUseContactCam { get; set; }
        [Category("功能屏蔽"), Description("是否屏蔽发送上游Glass结果给TesterPC")]
        public bool IsShieldSendUpstreamInfo { get; set; }
        //[Category("功能屏蔽"), Description("ProbeUnit压头到位自动压接屏蔽")]
        //public bool ProbeUnitAutoContactShield { get; set; }
        [Category("功能屏蔽"), Description("Probe自动更换屏蔽")]
        public bool ProbeAutoChangeShield { get; set; }
        [Category("功能屏蔽"), Description("配方自动更换屏蔽")]
        public bool RecipeAutoChangeShield { get; set; } = true;
        [Category("功能屏蔽"), Description("手动急停按钮安全模式启用")]
        public bool ManualEmgEnable { get; set; }
        [Category("功能屏蔽"), Description("是否更新PanelJudge，true:更新，false:不更新")]
        public bool UpdatePanelJudgeEnable { get; set; } = false;
        [Category("功能屏蔽"), Description("是否记录JobData到Log中")]
        public bool IsSaveJobDataToLog { get; set; } = false;
        [Category("Probe自动更换参数"),  Description("Probe更换模式")]
        public PrbChgMode PrbChgMode { get; set; }
        [Category("扫码设置"), Description("连续失败次数")]
        public int ScancodeFailCount { get; set; }
        [Category("扫码设置"), Description("如果连续扫码失败，使用从上游接收的条码，如果不使用，则手动输入条码")]
        public bool UseUpstreamCode { get; set; }
        [Category("设备运行模式"), Description("设备运行模式。Normal：正常运行模式。ColdRun：不检测，只传送Glass。NoDetect：不对位、不压接、不检测")]
        public RunMode RunMode { get; set; }
        [Category("系统设置"), Description("保存数据天数，单位：天")]
        public int DataSaveDays { get; set; }
        [Category("系统设置"), Description("Contact相机图片保存路径")]
        public string ContactCamImagesPath { get; set; } = "D:\\ContactImages";
        [Category("系统设置"), Description("PM界面显示的文本")]
        public string PMFormText { get; set; } = "设备维护中";
        [Category("系统设置"), Description("DFS上传程序路径")]
        public string DfsExePath { get; set; }
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
