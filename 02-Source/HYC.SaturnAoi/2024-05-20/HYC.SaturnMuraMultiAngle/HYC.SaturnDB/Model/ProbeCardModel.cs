/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2020 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnDB.Model
// 文件名称    ：ProbeCardModel.cs
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYC.SaturnDB.Model
{
    public class ProbeCardModel
    {
        /// <summary>
        /// 机种名，必须唯一
        /// </summary>
        public string RecipeName { get; set; }
        /// <summary>
        /// 已压接次数
        /// </summary>
        public int ContactedCount { get; set; }
        /// <summary>
        /// RFID
        /// </summary>
        public string RFID { get; set; }
        /// <summary>
        /// 所在料仓层号
        /// </summary>
        public int CabinetRowNum { get; set; }
        /// <summary>
        /// ProbeCard状态，InUvw：使用中，InCabinet：在料仓中，OutEqp：不在设备中
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 安装到Uvw上的时间
        /// </summary>
        public DateTime? LoadInUvwTime { get; set; } = null;
        /// <summary>
        /// 从Uvw上卸载的时间
        /// </summary>
        public DateTime? UnLoadFromUvwTime { get; set; } = null;
        /// <summary>
        /// 进入料仓时间
        /// </summary>
        public DateTime? InCabinetTime { get; set; } = null;
        /// <summary>
        /// 出料仓时间
        /// </summary>
        public DateTime? OutCabinetTime { get; set; } = null;
        public int BlockCount { get; set; }
        public string ProbeCardName { get; set; }
    }
    public enum ProbeCardSts
    {
        /// <summary>
        /// 在Uvw使用中
        /// </summary>
        InUvw,
        /// <summary>
        /// 在料仓中
        /// </summary>
        InCabinet,
        /// <summary>
        /// 不在设备中
        /// </summary>
        OutEqp,
        /// <summary>
        /// 从Uvw中卸载
        /// </summary>
        OutUvw,
    }
}
