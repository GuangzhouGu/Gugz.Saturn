using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CIMBase;
using HYC.HTTools;

namespace CIMBOE
{
    public class MCRDataReportBlock : ICIMData
    {
       public DateTime CountStartTime { get; set; }

       public DateTime CountEndTime { get; set; }

       public int TriggerCount { get; set; }

       public int ReadOKCount { get; set; }

       public int PanelCount { get; set; }

       public int ACodeCount { get; set; }

       public int BCodeCount { get; set; }

       public int CCodeCount { get; set; }

       public int DCodeCount { get; set; }

       public int FCodeCount { get; set; }

       public ushort[] ToArray()
       {
           List<ushort> result = new List<ushort>();
           result.AddRange(HTValueConverter.DateTimeToUshortArray(CountStartTime));
           result.AddRange(HTValueConverter.DateTimeToUshortArray(CountEndTime));
           result.AddRange(HTValueConverter.IntToUshortArray(TriggerCount));
           result.AddRange(HTValueConverter.IntToUshortArray(ReadOKCount));
           result.AddRange(HTValueConverter.IntToUshortArray(PanelCount));
           result.AddRange(HTValueConverter.IntToUshortArray(ACodeCount));
           result.AddRange(HTValueConverter.IntToUshortArray(BCodeCount));
           result.AddRange(HTValueConverter.IntToUshortArray(CCodeCount));
           result.AddRange(HTValueConverter.IntToUshortArray(DCodeCount));
           result.AddRange(HTValueConverter.IntToUshortArray(FCodeCount));
           return result.ToArray();
       }
    }
}
