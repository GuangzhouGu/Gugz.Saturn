using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CIMBase;
using HYC.HTTools;

namespace CIMBOE
{
    public class VCREventBlock : ICIMData
    {
        /// <summary>
        /// 长度10个ushort，20个字节
        /// </summary>
        public string GlassID { get; set; }
        /// <summary>
        /// 长度1个ushort
        /// </summary>
        public ushort LotNumber { get; set; }
        /// <summary>
        /// 长度1个ushort
        /// </summary>
        public ushort SlotNumber { get; set; }
        /// <summary>
        /// 长度10个ushort，20个字节
        /// </summary>
        public string VcrReadID { get; set; }
        /// <summary>
        /// 长度1个ushort
        /// </summary>
        public ushort UnitNo { get; set; }
        /// <summary>
        /// 长度1个ushort
        /// </summary>
        public ushort Reserved1 { get; set; }
        /// <summary>
        /// 长度1个ushort
        /// </summary>
        public ushort VCRNo { get; set; }
        /// <summary>
        /// 长度1个ushort
        /// 1: VCR Reading OK & Miss Match With Job Data Job ID
        ///2: VCR Reading Fail & Key In & Match With Job Data Job ID
        ///3: VCR Reading Fail & Key In & Miss Match With Job Data Job ID
        ///4: VCR Reading Fail & Pass
        /// </summary>
        public ushort VcrResult { get; set; }

        public VCRReadResultReportFlag Flag { get; set; }
        /// <summary>
        /// 长度5个ushort
        /// </summary>
        public ushort[] Reserved2 { get; set; } = new ushort[5];
        public ushort[] ToArray()
        {
            List<ushort> result = new List<ushort>();
            result.AddRange(HTValueConverter.AsciiStringToUshortArray(GlassID, 10));
            result.Add(LotNumber);
            result.Add(SlotNumber);
            result.AddRange(HTValueConverter.AsciiStringToUshortArray(VcrReadID, 10));
            result.Add(UnitNo);
            result.Add(Reserved1);
            result.Add(VCRNo);
            result.Add(VcrResult);
            result.Add((ushort)Flag);
            result.AddRange(Reserved2);
            return result.ToArray();
        }
    }

    public enum VCRReadResultReportFlag
    {
        Normal,
        RejectFlag ,
    }
}
