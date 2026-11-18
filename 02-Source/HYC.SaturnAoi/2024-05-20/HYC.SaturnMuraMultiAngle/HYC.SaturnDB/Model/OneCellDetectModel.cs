/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2023 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnDB.Model
// 文件名称    ：OneCellDetectModel
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYC.SaturnDB.Model
{
    /// <summary>
    /// 单个Cell检测总信息，包含总结果，点灯结果，TOS结果，Aoi结果，Mura结果，MultiAngle结果
    /// </summary>
    public class OneCellDetectModel
    {
        /// <summary>
        /// 产品ID
        /// </summary>
        public string GlassID { get; set; }
        public string CellID { get; set; }
        public int GlassNum { get; set; }
        public int ColumnNum { get; set; }
        public int CellNum { get; set; }
        /// <summary>
        /// 产品进入设备时间
        /// </summary>
        public DateTime EQPInTime { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 总结果，OK/NG
        /// </summary>
        public string Result { get; set; }

        /// <summary>
        /// AOI测试结果，OK/NG
        /// </summary>
        public string AoiResult { get; set; }
        /// <summary>
        /// 直视Mura测试结果，OK/NG
        /// </summary>
        public string MuraResult { get; set; }
        /// <summary>
        /// 斜视Mura测试结果，OK/NG
        /// </summary>
        public string SquintMuraResult { get; set; }
        /// <summary>
        /// 多角度光谱仪检测结果，OK/NG
        /// </summary>
        public string MultiAngleResult { get; set; }
        /// <summary>
        /// TOS检测结果，OK/NG
        /// </summary>
        public string OpenShortResult { get; set; }
        /// <summary>
        /// 点灯结果，OK/NG
        /// </summary>
        public string PowerOnResult { get; set; }
        /// <summary>
        /// 1:G(Good),3:N(NG)
        /// </summary>
        public string JobJudge { get; set; }
        /// <summary>
        /// 0:Inspect Skip or No Judge or No Grade
        /// 1:Y
        /// 2:SK
        /// 3:RW
        /// 4:RD
        /// 5:RL
        /// 6:RC
        /// 7:RG
        /// </summary>
        public string JobGrade { get; set; }
    }
}
