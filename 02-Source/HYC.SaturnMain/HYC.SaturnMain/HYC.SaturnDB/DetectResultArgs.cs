/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2023 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnDB
// 文件名称    ：DetectResultArgs
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
using HYC.SaturnAoi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYC.SaturnDB
{
    [Serializable]
    public class DetectResultArgs
    {
        public DetectReqArgs DetectReqArgs { get; set; }
        public string Result { get; set; }
        [System.Xml.Serialization.XmlIgnore]
        public string StartTime { get; set; }
        [System.Xml.Serialization.XmlIgnore]
        public string EndTime { get; set; }

        public override string ToString()
        {
            return $"GlassID:{DetectReqArgs.GlassID},GlassNum:{DetectReqArgs.GlassNum},ColumnNum:{DetectReqArgs.ColumnNum},CellID:{DetectReqArgs.CellID},DetectType:{DetectReqArgs.DetectType},Result:{Result},StartTime:{StartTime},EndTime:{EndTime}";
        }
    }
}
