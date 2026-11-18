/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2023 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：CIMBOE.Block
// 文件名称    ：PanelInformationRequestBlock
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
    public class PanelInformationRequestBlock : ICIMData
    {
        public ushort LotSequenceNumber { get; set; }
        public ushort SlotSequenceNumber { get; set; }
        public ushort UnitNo { get; set; }
        public ushort SubUnitSlotNo { get; set; }
        public ushort[] Reserved { get; set; } = new ushort[12];
        public ushort[] ToArray()
        {
            List<ushort> result = new List<ushort>();
            result.Add(LotSequenceNumber);
            result.Add(SlotSequenceNumber);
            result.Add(UnitNo);
            result.Add(SubUnitSlotNo);
            result.AddRange(Reserved);
            return result.ToArray();
        }
    }
    
}
