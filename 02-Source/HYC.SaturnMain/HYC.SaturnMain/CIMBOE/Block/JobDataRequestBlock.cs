using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CIMBase;
using HYC.HTTools;

namespace CIMBOE
{
    public class JobDataRequestBlock : ICIMData
    {
        public JobDataRequestType JobDataRequestType { get; set; }
        public string  RequestGlassID { get; set; }
        public ushort RequestGlassLotNumber { get; set; }
        public ushort RequestGlassSlotNumber { get; set; }
        public ushort UnitNo { get; set; }
        public ushort SubUnitSlotNo { get; set; }
        public ushort Reserved { get; set; }
        public ushort[] ToArray()
        {
            List<ushort> result = new List<ushort>();
            result.Add((ushort)JobDataRequestType);
            result.AddRange(HTValueConverter.AsciiStringToUshortArray(RequestGlassID, 10));
            result.Add(RequestGlassLotNumber);
            result.Add(RequestGlassSlotNumber);
            result.Add(UnitNo);
            result.Add(SubUnitSlotNo);
            result.Add(Reserved);
            return result.ToArray();
        }
    }
    public enum JobDataRequestType
    {
        GlassID=1,
        GlassCode,
        MaterialID,
        MaterialCode,
    }
}
