/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2023 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnDB.Model
// 文件名称    ：AoiNGModel
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
    /// 单个Cell Aoi不良信息数据模型
    /// </summary>
    public class AoiNGInfoModel
    {
        /// <summary>
        /// 产品ID
        /// </summary>
        public string GlassID { get; set; }
        public int GlassNum { get; set; }
        public int ColumnNum { get; set; }
        public string CellID { get; set; }

        /// <summary>
        /// 产品进入设备时间
        /// </summary>
        public DateTime EQPInTime { get; set; }

        /// <summary>
        /// Pattern名称
        /// </summary>
        public string PatternName { get; set; }

        /// <summary>
        /// 缺陷序号
        /// </summary>
        public int DefectNum { get; set; }

        /// <summary>
        /// 不良Code
        /// 1-显示异常 20-亮点 21-暗点 3-线 4-斑 5-斑（其他）
        /// </summary>
        public string ErrorCode { get; set; }

        /// <summary>
        /// 缺陷等级
        /// </summary>
        public string DefectGrade { get; set; }

        /// <summary>
        /// 不良数量
        /// </summary>
        public int ErrorCount { get; set; }

        /// <summary>
        /// 检测类型，Aoi/Mura
        /// </summary>
        public string DetectType { get; set; }

        /// <summary>
        /// 缺陷_坐标X(像素)
        /// </summary>
        public int DefectSizeX { get; set; }

        /// <summary>
        /// 缺陷_坐标Y(像素)
        /// </summary>
        public int DefectSizeY { get; set; }

        /// <summary>
        /// 缺陷_坐标_X产品坐标
        /// </summary>
        public int CoordinateX { get; set; }

        /// <summary>
        /// 缺陷_坐标_Y产品坐标
        /// </summary>
        public int CoordinateY { get; set; }

        /// <summary>
        /// 缺陷区域
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// 缺陷面积
        /// </summary>
        public string Area { get; set; }

        /// <summary>
        /// 缺陷相关参数
        /// </summary>
        public string DefectParams { get; set; }
        public string DefectType { get; set; }
        public string DefectName { get; set; }
        public int DefectParaIndex { get; set; }
        public string DefectAreas { get; set; }
    }
}
