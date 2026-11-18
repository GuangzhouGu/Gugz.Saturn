/*----------------------------------------------------------------
// Copyright (C) Suzhou HYC Electronic Technology Co.,LTD
// 版权所有。
//
// =================================
// CLR版本     ：4.0.30319.42000
// 命名空间    ：HYC.AutoPosition.Parameter
// 文件名称    ：WorkFlowResult.cs
// =================================
// 创 建 者    ：yangkang
// 创建日期    ：2020/1/6 19:05:24 
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

namespace HYC.AutoPosition.Parameter
{
    /// <summary>
    /// 工作流程结果
    /// </summary>
    public class WorkFlowResult
    {
        /// <summary>
        /// 是否对位成功
        /// </summary>
        public bool Result { get; set; }
        /// <summary>
        /// 对位耗时
        /// </summary>
        public int ProcessingTime { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMsg { get; set; }

        /// <summary>
        /// 转换成字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Result:{0}\r\nProcessingTime:{1}\r\nErrorMsg:{2}",
                Result, ProcessingTime, ErrorMsg);
        }
    }
}
