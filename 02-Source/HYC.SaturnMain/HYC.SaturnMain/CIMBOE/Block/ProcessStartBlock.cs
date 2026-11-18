/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2023 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：CIMBOE.Block
// 文件名称    ：ProcessStartBlock
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
    public class ProcessStartBlock:ICIMData
    {
        public ushort JobCodeLotNumber1 { get; set; }
        public ushort JobCodeSlotNumber1 { get; set; }
        public ushort JobCodeLotNumber2 { get; set; }
        public ushort JobCodeSlotNumber2 { get; set; }
        public ushort UnitNo { get; set; } = 0;
        public ushort SlotNo { get; set; } = 0;
        public ushort[] ToArray()
        {
            return new ushort[] { JobCodeLotNumber1, JobCodeSlotNumber1, JobCodeLotNumber2, JobCodeSlotNumber2, UnitNo, SlotNo };
        }
    }
}
