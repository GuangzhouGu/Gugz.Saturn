using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CIMBase;

namespace CIMBOE
{
    public class JobJudgeChangeReportBlock : ICIMData
    {
        public ushort LotSequenceNumber { get; set; }
        public ushort SlotSequenceNumber { get; set; }
        public ushort UnitNo { get; set; }
        public ushort SubUnitSlotNo { get; set; }
        public JobJudgeResult JobJudgeResult { get; set; }
        public ushort[] Reserved { get; set; } = new ushort[11];
        public ushort[] ToArray()
        {
            List<ushort> result = new List<ushort>();
            result.Add(LotSequenceNumber);
            result.Add(SlotSequenceNumber);
            result.Add(UnitNo);
            result.Add(SubUnitSlotNo);
            result.Add((ushort)JobJudgeResult);
            result.AddRange(Reserved);
            return result.ToArray();
        }
    }
    public enum JobJudgeResult
    {
        NoJudgeResultExist,
        Good,
        NotGood,
        Pending,
        Rework,
        Repair,
        Scrap,
    }
}
