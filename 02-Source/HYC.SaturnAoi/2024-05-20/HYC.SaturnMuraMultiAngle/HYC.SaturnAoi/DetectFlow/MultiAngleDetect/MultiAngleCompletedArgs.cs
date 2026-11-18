/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2023 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnAoi.DetectFlow.MultiAngleDetect
// 文件名称    ：MultiAngleResult
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
using HYC.SaturnAoi.Devices.MultiAngle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYC.SaturnAoi.DetectFlow.MultiAngleDetect
{
    public class MultiAngleCompletedArgs
    {
        public string GlassID { get; set; }
        public int GlassNum { get; set; }
        public int ColumnNum { get; set; }
        public string CellID { get; set; }
        public List<PatternDetectInfo> PatternInfoList { get; set; }
        public string Result { get; set; } = "OK";
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string ProcessingTime { get; set; }

    }
    public class PatternDetectInfo
    {
        public PatternInfo PatternInfo { get; set; }
        public MultiAngleValue MultiAngleValue { get; set; }
        public string Result { get; set; }
        public double ResultValue { get; set; }
    }
}
