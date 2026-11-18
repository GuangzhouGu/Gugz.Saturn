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
    public class PanelInformationReplyBlock:ICIMData
    {
        public ushort LotSequenceNumber { get; set; }
        public ushort SlotSequenceNumber { get; set; }
        public ushort UnitNo { get; set; }
        public ushort SubUnitSlotNo { get; set; }
        public ushort[] PanelJudge { get; set; } = new ushort[256];
        public ushort[] Reserved1 { get; set; } = new ushort[256];
        public ushort ReturnCode { get; set; }
        public ushort[] Reserved2 { get; set; } = new ushort[11];
        public ushort[] ToArray()
        {
            List<ushort> result = new List<ushort>();
            result.Add(LotSequenceNumber);
            result.Add(SlotSequenceNumber);
            result.Add(UnitNo);
            result.Add(SubUnitSlotNo);
            result.AddRange(PanelJudge);
            result.AddRange(Reserved1);
            result.Add(ReturnCode);
            result.AddRange(Reserved2);
            return result.ToArray();
        }
        public void LoadDataFormArrary(ushort[] data)
        {
            LotSequenceNumber = data[0];
            SlotSequenceNumber = data[1];
            UnitNo = data[2];
            SubUnitSlotNo = data[3];
            PanelJudge = data.Skip(4).Take(256).ToArray();
            Reserved1 = data.Skip(260).Take(256).ToArray();
            ReturnCode = data[516];
            Reserved2 = data.Skip(517).Take(256).ToArray();
        }
    }
}
