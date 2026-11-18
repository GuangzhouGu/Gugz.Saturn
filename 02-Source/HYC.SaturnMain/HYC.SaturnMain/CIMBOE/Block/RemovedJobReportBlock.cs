using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CIMBase;
using CIMBOE.Block;

namespace CIMBOE
{
    public class RemovedJobReportBlock:ICIMData
    {
       public GlassDataA JobData { get; set; }
       public RemovedJobReportFlog Flag { get; set; }

       public ushort[] ToArray()
       {
           List<ushort> data = new List<ushort>();
           data.AddRange(JobData.ToArray());
           data.Add((ushort)Flag);
           return data.ToArray();
       }
    }

   public enum RemovedJobReportFlog
   { 
       Take_Out=1,
       Scrap=2,
   }
}
