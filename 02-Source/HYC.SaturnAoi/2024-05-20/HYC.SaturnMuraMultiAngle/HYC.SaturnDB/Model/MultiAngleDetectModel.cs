/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2023 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnDB.Model
// 文件名称    ：MultiAngleDetectInfoModel
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
    /// 单个Cell多角度检测信息数据模型
    /// </summary>
    public class MultiAngleDetectModel
    {
        /// <summary>
        /// 产品ID
        /// </summary>
        public string GlassID { get; set; }
        public int GlassNum { get; set; }
        public int ColumnNum { get; set; }
        public string CellID { get; set; }

        public DateTime EQPInTime { get; set; }
        /// <summary>
        /// 测试开始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 测试结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// 测试耗时时间
        /// </summary>
        public string ProcessingTime { get; set; }
        /// <summary>
        /// 机种编号
        /// </summary>
        public string RecipeID { get; set; }

        /// <summary>
        /// 测试结果
        /// </summary>
        public string Result { get; set; }
    }
}
