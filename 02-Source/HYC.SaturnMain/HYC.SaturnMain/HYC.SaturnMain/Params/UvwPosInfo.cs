/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2020 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnMain.UVWStage
// 文件名称    ：UvwPosInfo.cs
// =================================
// 创 建 者    ：guguangzhou
// 创建日期    ：2022/10/29 09:00:00 
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
using HYC.SaturnMain.Params;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYC.SaturnMain.UVWStage
{
    [Serializable]
    public class UvwPosInfo: ParaBase,IPara
    {
        [System.Xml.Serialization.XmlIgnore]
        [Browsable(false)]
        public bool IsShow { get; set; } = false;
        [System.Xml.Serialization.XmlIgnore]
        [Browsable(false)]
        public ParaViewBase ParaViewBase { get; set; }
        [System.Xml.Serialization.XmlIgnore]
        [Browsable(false)]
        public string ParaName { get; set; }
        /// <summary>
        /// 平台1ProbeUnit U轴压接时位置
        /// </summary>
        public double AxisYPosStage1 { get; set; }
        /// <summary>
        /// 平台1ProbeUnit V轴压接时位置
        /// </summary>
        public double AxisX1PosStage1 { get; set; }
        /// <summary>
        /// 平台1ProbeUnit W轴压接时位置
        /// </summary>
        public double AxisX2PosStage1 { get; set; }
        /// <summary>
        /// 平台2ProbeUnit U轴压接时位置
        /// </summary>
        public double AxisYPosStage2 { get; set; }
        /// <summary>
        /// 平台2ProbeUnit V轴压接时位置
        /// </summary>
        public double AxisX1PosStage2 { get; set; }
        /// <summary>
        /// 平台2ProbeUnit W轴压接时位置
        /// </summary>
        public double AxisX2PosStage2 { get; set; }
        /// <summary>
        /// 平台1压接Z轴压接时位置
        /// </summary>
        public double AxisZContactPosStage1 { get; set; }

        /// <summary>
        ///平台2压接Z轴压接时位置
        /// </summary>
        public double AxisZContactPosStage2 { get; set; }


        /// <summary>
        ///X1轴自动更换位置
        /// </summary>
        public double AxisX1AutoChgPos{ get; set; }
        /// <summary>
        /// X2轴自动更换位置
        /// </summary>
        public double AxisX2AutoChgPos { get; set; }
        /// <summary>
        ///Y轴自动更换位置
        /// </summary>
        public double AxisYAutoChgPos { get; set; }


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
