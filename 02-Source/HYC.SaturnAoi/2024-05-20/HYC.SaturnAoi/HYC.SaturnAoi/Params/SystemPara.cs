/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2020 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnAoi
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
using CommonLib.Para;
using HYC.HTPCBase.Extension.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYC.SaturnAoi.Params
{
    /// <summary>
    /// 控制参数
    /// </summary>
    public class SystemPara : ParaBase,IPara
    {
        [System.Xml.Serialization.XmlIgnore]
        [Browsable(false)]
        public bool IsShow { get; set; } = true;
        [System.Xml.Serialization.XmlIgnore]
        [Browsable(false)]
        public ParaViewBase ParaViewBase { get; set; } = null;
        [System.Xml.Serialization.XmlIgnore]
        [Browsable(false)]
        public string ParaName { get; set; } = "系统参数";

        [Category("功能屏蔽"), Description("Aoi检测屏蔽")]
        public bool IsShieldAoiDetect { get; set; }
        [Category("功能屏蔽"), Description("直视Mura检测屏蔽")]
        public bool IsShieldMuraDetect { get; set; }
        [Category("系统设置"), Description("检查机是否使用Dll通讯方式")]
        public bool IsTesterUseDllCommunication { get; set; }

        [Category("系统设置"), Description("MuraPC启动路径")]
        public string MuraPCExePath { get; set; }
        [Category("系统设置"), Description("ElpPG程序启动路径")]
        public string ElpPGExePath { get; set; }
        [Category("系统设置"), Description("开电重复次数")]
        public int PowerOnCount { get; set; } = 3;
        [Category("系统设置"), Description("PC索引")]
        public int PcIndex { get; set; }
        [Category("系统设置"), Description("使用本地图片")]
        public bool IsUseLocalImage { get; set; }
        [Category("系统设置"), Description("PG机种文件路径")]
        public string PGRecipeFilePath { get; set; }
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
