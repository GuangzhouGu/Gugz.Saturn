using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CIMBase;
using HYC.HTTools;

namespace CIMBOE
{
    public class ProcessDataReportBlock : ICIMData
    {
        /// <summary>
        /// 10个ushort
        /// </summary>
        public string GlassID { get; set; }
        public ushort[] Reserved { get; set; } = new ushort[10];
        /// <summary>
        /// 3 ushort
        /// </summary>
        public DateTime LocalProcessStartTime { get; set; }
        /// <summary>
        /// 3 ushort
        /// </summary>
        public DateTime LocalProcessEndTime { get; set; }
        /// <summary>
        /// 1个ushort
        /// </summary>
        public ushort UnitNo { get; set; } = 0;
        /// <summary>
        /// 1个ushort
        /// </summary>
        public ushort SlotNo { get; set; } = 0;
        /// <summary>
        /// 1个ushort
        /// </summary>
        public ushort RecipeStepNo { get; set; }
        /// <summary>
        /// 1199个ushort
        /// </summary>
        public ushort[] DVData { get; set; } = new ushort[1199];


        public ushort[] ToArray()
        {
            List<ushort> result = new List<ushort>();
            result.AddRange(HTValueConverter.AsciiStringToUshortArray(GlassID,10));
            result.AddRange(Reserved);
            result.AddRange(HTValueConverter.DateTimeToUshortArray(LocalProcessStartTime));
            result.AddRange(HTValueConverter.DateTimeToUshortArray(LocalProcessEndTime));
            result.Add(UnitNo);
            result.Add(SlotNo);
            result.Add(RecipeStepNo);
            result.AddRange(DVData);
            return result.ToArray();
        }
    }
}
