/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2023 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnAoi.Entities
// 文件名称    ：Class1
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
using HYC.SaturnAoi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYC.SaturnAoi.Entities
{
    [Serializable]
    public class DetectReqArgs
    {
        public int GlassNum { get; set; }
        public string GlassID { get; set; }
        public int ColumnNum { get; set; }
        public int CellNum { get; set; }
        public string CellID { get; set; }
        public string DetectType { get; set; }
        public int PCIndex { get; set; }

    }
}
