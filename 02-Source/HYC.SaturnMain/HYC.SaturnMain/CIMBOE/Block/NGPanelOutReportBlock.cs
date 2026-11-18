using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CIMBase;
using HYC.HTTools;

namespace CIMBOE
{
    public class NGPanelOutReportBlock : ICIMData
    {
       /// <summary>
       /// 长度10个ushort，20个字节
       /// </summary>
       public string LotID { get; set; }

       /// <summary>
       /// 长度10个ushort，20个字节
       /// </summary>
       public string PanelID { get; set; }

       /// <summary>
       /// 长度20个ushort，40个字节
       /// </summary>
       public string NGText { get; set; }


       public ushort[] ToArray()
       {
           List<ushort> result = new List<ushort>();
           result.AddRange(HTValueConverter.AsciiStringToUshortArray(LotID, 10));
           result.AddRange(HTValueConverter.AsciiStringToUshortArray(PanelID, 10));
           result.AddRange(HTValueConverter.AsciiStringToUshortArray(NGText, 20));
           return result.ToArray();
       }
    }
}
