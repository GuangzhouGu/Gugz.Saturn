/*----------------------------------------------------------------
// Copyright © 2021 HYC. All Rights Reserved
// 版权所有。
//
// =================================
// CLR版本     ：4.0.30319.42000
// 命名空间    ：CommonLib.Entities
// 文件名称    ：DetectResult.cs
// =================================
// 创 建 者    ：gugz
// 创建日期    ：2022/1/7 12:56:00 
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

namespace CommonLib.Entities
{
    [Serializable]
    public class DetectResult
    {
        /// <summary>
        /// 产品 id
        /// </summary>
        public string PanelID { get; set; }
        /// <summary>
        /// 3:OK,4:NG
        /// </summary>
        public ushort PanelStatus { get; set; }
        /// <summary>
        /// AOI 检测结果
        /// </summary>
        public ushort AoiStatus { get; set; }
        /// <summary>
        /// mura 检测结果
        /// </summary>
        public ushort MuraStatus { get; set; }
        /// <summary>
        /// 色度检测结果
        /// </summary>
        public ushort ChromaStatus { get; set; }
        /// <summary>
        ///  拍照结果 
        /// </summary>
        public ushort McrStatus { get; set; }
        /// <summary>
        /// 上传给PLC 的五个等级
        /// </summary>
        public string[] Grade { get; set; } = new string[5];
        /// <summary>
        /// 上传给PLC 的主code
        /// </summary>
        public string MainDetectCode { get; set; } = string.Empty;
        /// <summary>
        /// 上传给PLC 的五个等级code
        /// </summary>
        public string[] DefectCode { get; set; } = new string[5];
        /// <summary>
        /// 工位
        /// </summary>
        public int StationIndex { get; set; }
        /// <summary>
        /// 将上传给PLC 的数据显示
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string msg = $"PanelID:{PanelID},PanelStatus:{PanelStatus},AoiStatus:{AoiStatus},MuraStatus:{MuraStatus},ChromaStatus:{ChromaStatus},McrStatus:{McrStatus},Grade:{string.Join("-", Grade)},MainDetectCode:{MainDetectCode},DefectCode:{string.Join("-", DefectCode)},StationIndex:{StationIndex}";
            return msg;
        }
    }
}
