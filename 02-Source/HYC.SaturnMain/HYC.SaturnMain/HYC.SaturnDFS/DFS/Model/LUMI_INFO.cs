/*----------------------------------------------------------------
// Copyright (C) Suzhou HYC Technology Co.,LTD
// 版权所有。
//
// =================================
// CLR版本     ：4.0.30319.42000
// 命名空间    ：DFS.Model
// 文件名称    ：LUMI_INFO.cs
// =================================
// 创 建 者    ：yangkang
// 创建日期    ：2021/1/15 17:09:31 
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
    public class LUMI_INFO : DFSItemModel
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string NO { get; set; }
        /// <summary>
        /// 亮度
        /// </summary>
        public string LV { get; set; }
        public string CIEX { get; set; }
        public string CIEY { get; set; }
        public string CIEU { get; set; }
        public string CIEV { get; set; }
        public string TC { get; set; }

        /// <summary>
        /// 获取标题行
        /// </summary>
        /// <returns></returns>
        public override string GetHeader()
        {
            return "LUMI_INFO^" + this.GetHeaderPart();
        }
    }
}
