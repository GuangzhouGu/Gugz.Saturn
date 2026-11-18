/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2022 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnMain.Params
// 文件名称    ：CamBasePointPara
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
    class CamBasePointPara : ParaBase, IPara
    {
        [System.Xml.Serialization.XmlIgnore]
        [Browsable(false)]
        public bool IsShow { get; set; } = true;
        [System.Xml.Serialization.XmlIgnore]
        [Browsable(false)]
        public ParaViewBase ParaViewBase { get; set; } = new FrmCamBasePoint();
        [System.Xml.Serialization.XmlIgnore]
        [Browsable(false)]
        public string ParaName { get; set; } = "相机基准点设置";
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
