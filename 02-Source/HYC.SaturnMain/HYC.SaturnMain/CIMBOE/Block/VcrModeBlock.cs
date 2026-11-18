/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2023 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：CIMBOE.Block
// 文件名称    ：VcrModeBlock
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
using CIMBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIMBOE.Block
{
    public class VCRModeBlock : ICIMData
    {
        public ushort VCR_Enable_Mode { get; set; }//1 : Enable,2 : Disable
        public ushort VCR_Read_Fail_Operation_Mode { get; set; }//1 : Key - In mode,2 : Skip mode
        public ushort[] ToArray()
        {
            return new ushort[] { VCR_Enable_Mode, VCR_Read_Fail_Operation_Mode };
        }
    }
}
