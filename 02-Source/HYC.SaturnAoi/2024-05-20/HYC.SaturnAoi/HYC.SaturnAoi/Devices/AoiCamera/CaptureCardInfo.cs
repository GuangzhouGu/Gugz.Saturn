/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2023 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.TPApiUnit.Devices.AoiCamera
// 文件名称    ：CaptureCardInfo
// =================================
// 创 建 者    ：guguangzhou
// 创建日期    ：
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

namespace HYC.TPApiUnit.Devices.AoiCamera
{
    public class CaptureCardInfo
    {
        public int CaptureCardIndex { get; set; }
        public string FileName { get; set; }
        public int CameraCount { get; set; }
        public string CaptureCardName { get; set; }
    }
}
