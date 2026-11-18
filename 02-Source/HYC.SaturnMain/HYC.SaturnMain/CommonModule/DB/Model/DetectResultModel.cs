/*----------------------------------------------------------------
// Copyright ? 2021 HYC. All Rights Reserved
// 版权所有。
//
// =================================
// CLR版本     ：4.0.30319.42000
// 命名空间    ：HYC.DbQuery.Model
// 文件名称    ：TotalInfo.cs
// =================================
// 创 建 者    ：gugz
// 创建日期    ：2022/3/5 12:59:10 
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

namespace CommonDB.Model
{
    /// <summary>
    /// 测试结果总表
    /// </summary>
    public class DetectResultModel
    {
        /// <summary>
        /// 产品ID
        /// </summary>
        public string PanelID { get; set; }
        /// <summary>
        /// 开始检测时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 结束检测时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 机种名
        /// </summary>
        public string RecipeID { get; set; }
        /// <summary>
        /// 单体序号，从0开始
        /// </summary>
        public int UnitNo { get; set; }
        /// <summary>
        /// 工位序号，从0开始
        /// </summary>
        public int StationNo { get; set; }
        /// <summary>
        /// 总结果，Pass/Fail
        /// </summary>
        public string Result { get; set; }

    }
}
