using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CIMBase;

namespace CIMBOE
{
    public class AutoRecipeModeChangeReportBlock : ICIMData
    {
        public AutoRecipeModeChangeReportStatus Status { get; set; }



        public ushort[] ToArray()
        {
            return new ushort[] { (ushort)Status };
        }
    }

    public enum AutoRecipeModeChangeReportStatus
    { 
        Enable=1,
        Disable=2
    }
}
