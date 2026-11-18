/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2023 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnAoi.DetectFlow.MultiAngleDetect
// 文件名称    ：PatternInfo
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYC.SaturnAoi.DetectFlow.MultiAngleDetect
{
    public class PatternInfo
    {
        /// <summary>
        ///画面序号
        /// </summary>
        public int PatternNum { get; set; }
        /// <summary>
        ///画面名称
        /// </summary>
        public string PatternName { get; set; }
        /// <summary>
        ///稳定时间
        /// </summary>
        public int WaitTime { get; set; }
        public bool IsUse { get; set; }
        public string ControlType { get; set; }

    }
    public enum ControlType
    {
        PowerOn,
        SwitchPattern,
        PowerOnSwitchPattern,
        NoControl
    }
}
