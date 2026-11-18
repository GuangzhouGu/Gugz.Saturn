/*----------------------------------------------------------------
// Copyright (C) Suzhou HYC Technology Co.,LTD
// 版权所有。
//
// =================================
// CLR版本     ：4.0.30319.42000
// 命名空间    ：DFS.Model
// 文件名称    ：PG_INFO.cs
// =================================
// 创 建 者    ：yangkang
// 创建日期    ：2021/1/15 9:51:08 
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
    public class PG_INFO : DFSItemModel
    {
        public string NO { get; set; }
        public string PATTERN_NAME { get; set; }
        public string V_IOVCC { get; set; }
        public string I_IOVCC { get; set; }
        public string V_VCC { get; set; }
        public string I_VCC { get; set; }
        public string V_AVDD { get; set; }
        public string I_AVDD { get; set; }
        public string V_PVDD { get; set; }
        public string I_PVDD { get; set; }
        public string V_PVEE { get; set; }
        public string I_PVEE { get; set; }
        public string V_DVDD { get; set; }
        public string I_DVDD { get; set; }

        /// <summary>
        /// 获取标题行
        /// </summary>
        /// <returns></returns>
        public override string GetHeader()
        {
            return "PG_INFO^" + GetHeaderPart();
        }
    }
}
