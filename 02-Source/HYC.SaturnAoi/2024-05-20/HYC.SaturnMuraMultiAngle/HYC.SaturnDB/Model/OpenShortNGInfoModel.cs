/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2020 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnDB.Model
// 文件名称    ：OneNGModel.cs
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
    /// <summary>
    /// 单个Cell OpenShort不良信息数据模型
    /// </summary>
    public class OpenShortNGInfoModel
    {
        /// <summary>
        /// 条码
        /// </summary>
        public string GlassID { get; set; }
        /// <summary>
        /// 行序号
        /// </summary>
        public int GlassNum { get; set; }
        /// <summary>
        /// CellID
        /// </summary>
        public string CellID { get; set; }
       
        /// <summary>
        /// 行序号
        /// </summary>
        public int ColumnNum { get; set; }
        /// <summary>
        /// 行序号
        /// </summary>
        public int RowNum { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public int NGNum { get; set; }
        /// <summary>
        /// NGCode
        /// </summary>
        public string NGCode { get; set; }
        /// <summary>
        /// NGType,Open/Short
        /// </summary>
        public string NGType { get; set; }

        /// <summary>
        /// 开始测试时间
        /// </summary>
        public DateTime DetectTime { get; set; }
        /// <summary>
        ///Source通道号
        /// </summary>
        public string Source { get; set; }
        /// <summary>
        ///Measure通道号
        /// </summary>
        public string Measure { get; set; }
        /// <summary>
        ///Short类型
        /// </summary>
        public string ShortType { get; set; }
    }
    public enum NgType
    {
        Open,
        Short
    }
}
