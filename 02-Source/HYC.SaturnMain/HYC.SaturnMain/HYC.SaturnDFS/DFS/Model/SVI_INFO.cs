/*----------------------------------------------------------------
// Copyright (C) Suzhou HYC Technology Co.,LTD
// 版权所有。
//
// =================================
// CLR版本     ：4.0.30319.42000
// 命名空间    ：DFS.Model
// 文件名称    ：SVI_INFO.cs
// =================================
// 创 建 者    ：yangkang
// 创建日期    ：2021/1/15 10:25:20 
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

namespace DFS.Model
{
    public class SVI_INFO : DFSItemModel
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string NO { get; set; }
        /// <summary>
        /// 缺陷CODE
        /// </summary>
        public string DEFCODE { get; set; }
        /// <summary>
        /// 缺陷画面名称
        /// </summary>
        public string PATTERN_NAME { get; set; }
        /// <summary>
        /// 缺陷坐标X1（单位：pixel）
        /// </summary>
        public string DATA_PIX { get; set; }
        /// <summary>
        /// 缺陷坐标Y1（单位：pixel）
        /// </summary>
        public string GATE_PIX { get; set; }
        /// <summary>
        /// 缺陷面积
        /// </summary>
        public string SIZE { get; set; }
        /// <summary>
        /// 缺陷宽度
        /// </summary>
        public string WIDTH { get; set; }
        /// <summary>
        /// 缺陷高度
        /// </summary>
        public string HEIGHT { get; set; }
        /// <summary>
        /// 逻辑名称，AOI提供
        /// </summary>
        public string LOGIC { get; set; }
        /// <summary>
        /// 缺陷面积
        /// </summary>
        public string SIZE1 { get; set; }
        /// <summary>
        /// 缺陷宽度
        /// </summary>
        public string WIDTH1 { get; set; }
        /// <summary>
        /// 缺陷高度
        /// </summary>
        public string HEIGHT1 { get; set; }
        /// <summary>
        /// 缺陷强度
        /// </summary>
        public string STRENGTH { get; set; }

        /// <summary>
        /// 获取标题行
        /// </summary>
        /// <returns></returns>
        public override string GetHeader()
        {
            return "SVI_INFO^" + GetHeaderPart();
        }
    }
}
