/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2021 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.Mt2Main.Devices.Power
// 文件名称    ：PowerInfo.cs
// =================================
// 创 建 者    ：guguangzhou
// 创建日期    ：2022/01/13 09:00:00 
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
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYC.SaturnAoi.Devices.Power
{
    public class PowerInfo
    {
        /// <summary>
        /// 厂家名称
        /// </summary>
        public string PowerVender { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string IpAddress { get; set; }
        /// <summary>
        /// 端口号
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 相机序号
        /// </summary>
        public int PowerNum { get; set; }
        /// <summary>
        /// 相机名称
        /// </summary>
        public string PowerName { get; set; }
        /// <summary>
        /// Com端口号
        /// </summary>
        public string PortName { get; set; }
        /// <summary>
        /// 比特率
        /// </summary>
        public int BaudRate { get; set; }
 
    }
}
