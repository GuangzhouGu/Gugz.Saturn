using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CIMBase;
using CIMBOE.Block;

namespace CIMBOE
{
    public class ReceivedJobReportBlock : ICIMData
    {
        public GlassDataA JobData { get; set; }
        public ushort[] ToArray()
        {
            return JobData.ToArray();
        }
    }
}
