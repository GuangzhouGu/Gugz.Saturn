/*----------------------------------------------------------------
// Copyright (C) Suzhou HYC Technology Co.,LTD
// 版权所有。
//
// =================================
// CLR版本     ：4.0.30319.42000
// 命名空间    ：DFS.Model
// 文件名称    ：CTP_INFO.cs
// =================================
// 创 建 者    ：yangkang
// 创建日期    ：2021/1/15 10:39:00 
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
    public class CTP_INFO : DFSItemModel
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string NO { get; set; }

        /// <summary>
        /// 获取标题行
        /// </summary>
        /// <returns></returns>
        public override string GetHeader()
        {
            return "CTP_INFO^" + this.GetHeaderPart();
        }
    }
}
