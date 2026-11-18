/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2020 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ： HYC.SaturnMain.Devices.Scaner
// 文件名称    ：ScanerInfo.cs
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

namespace HYC.SaturnMain.Devices.Scaner
{
    public class ScanerInfo
    {
        /// <summary>
        /// 厂家名称
        /// </summary>
        public string ScanerVender { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string IP { get; set; }
        /// <summary>
        /// 端口号
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int ScanerNum { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string ScanerName { get; set; }


    }
}
