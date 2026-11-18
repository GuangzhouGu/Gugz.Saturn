using CIMBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIMBOE.Block
{
    public class FetchedOutJobDataSubBlock : ICIMData
    {
        public ushort JobTypeOption { get; set; }
        public ushort Position { get; set; }
        public ushort UnitProcessingTime { get; set; }
        public ushort UnitNo{ get; set; }
        public ushort PortNo { get; set; }
        public ushort SlotNo { get; set; }
        public ushort[] Reserved { get; set; } = new ushort[74];
        public ushort[] ToArray()
        {
            List<ushort> temp = new List<ushort>();
            temp.Add(JobTypeOption);
            temp.Add(Position);
            temp.Add(UnitProcessingTime);
            temp.Add(UnitNo);
            temp.Add(PortNo);
            temp.Add(SlotNo);
            temp.AddRange(Reserved);
            return temp.ToArray();
        }
    }
}
