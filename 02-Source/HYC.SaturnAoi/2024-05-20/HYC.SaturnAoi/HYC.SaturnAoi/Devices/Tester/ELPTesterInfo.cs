/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2023 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnAoi.Devices.PatternGenerator
// 文件名称    ：PatternGeneratorInfo
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

namespace HYC.SaturnAoi.Devices.Tester
{
    public class ELPTesterInfo
    {
        public int TesterNum { get; set; }
        public string TesterName { get; set; }
        public string IP { get; set; }
        public int Port { get; set; }
        public CommunicationType CommunicationType { get; set; }
    }
    public enum CommunicationType
    {
        Protocol,
        Dll,
    }
}
