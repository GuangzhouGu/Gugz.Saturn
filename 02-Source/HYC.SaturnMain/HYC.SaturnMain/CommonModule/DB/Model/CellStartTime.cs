/*----------------------------------------------------------------
// Copyright © 2021 HYC. All Rights Reserved
// 版权所有。
//
// =================================
// CLR版本     ：4.0.30319.42000
// 命名空间    ：HYC.DbQuery.Model
// 文件名称    ：CellStartTime.cs
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
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonDB.Model
{
    /// <summary>
    /// 记录产品进入设备的时间，ID重复时不覆盖
    /// </summary>
    [Table("CellStartTime")]
    public class CellStartTimeModel
    {
        /// <summary>
        /// 扫码枪扫到的ID
        /// </summary>
        public string PanelID { get; set; }
        /// <summary>
        /// 扫描到ID的时间
        /// </summary>
        public DateTime StartTime { get; set; }

    }
}
