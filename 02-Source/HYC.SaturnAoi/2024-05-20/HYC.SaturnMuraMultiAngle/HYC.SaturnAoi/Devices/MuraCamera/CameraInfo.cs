/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2021 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.Mt2Aoi.Devices
// 文件名称    ：CameraInfo.cs
// =================================
// 创 建 者    ：guguangzhou
// 创建日期    ：2022/01/08 09:00:00 
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

namespace HYC.SaturnAoi.Devices.MuraCamera
{
    public class CameraInfo
    {
        /// <summary>
        /// 厂家名称
        /// </summary>
        public string CameraVender { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string IpAddress { get; set; }
        /// <summary>
        /// Mac地址
        /// </summary>
        public string MacAddress { get; set; }
        /// <summary>
        /// 相机序号
        /// </summary>
        public int CameraNum { get; set; }
        /// <summary>
        /// 相机序号
        /// </summary>
        public string CameraName { get; set; }
        /// <summary>
        /// 是否是彩色相机
        /// </summary>
        public bool IsColor { get; set; }
    }
}
