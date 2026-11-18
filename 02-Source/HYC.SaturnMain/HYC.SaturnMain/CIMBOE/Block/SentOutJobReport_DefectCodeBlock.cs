using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CIMBase;
using HYC.HTTools;

namespace CIMBOE
{
    public class SentOutJobReport_DefectCodeBlock : ICIMData
    {
       /// <summary>
       /// 长度为5，总字节数为10
       /// </summary>
       public string Code1 { get; set; }
       /// <summary>
       /// 长度为5，总字节数为10
       /// </summary>
       public string Code2 { get; set; }
       /// <summary>
       /// 长度为5，总字节数为10
       /// </summary>
       public string Code3 { get; set; }
       /// <summary>
       /// 长度为5，总字节数为10
       /// </summary>
       public string Code4 { get; set; }
       /// <summary>
       /// 长度为5，总字节数为10
       /// </summary>
       public string Code5 { get; set; }
       /// <summary>
       /// 长度为5，总字节数为10
       /// </summary>
       public string Code6 { get; set; }
       /// <summary>
       /// 长度为5，总字节数为10
       /// </summary>
       public string Code7 { get; set; }
       /// <summary>
       /// 长度为5，总字节数为10
       /// </summary>
       public string Code8 { get; set; }
       /// <summary>
       /// 长度为5，总字节数为10
       /// </summary>
       public string Code9 { get; set; }
       /// <summary>
       /// 长度为5，总字节数为10
       /// </summary>
       public string Code10 { get; set; }

       public ushort[] ToArray()
       {
           List<ushort> result = new List<ushort>();
           result.AddRange(HTValueConverter.AsciiStringToUshortArray(Code1, 5));
           result.AddRange(HTValueConverter.AsciiStringToUshortArray(Code2, 5));
           result.AddRange(HTValueConverter.AsciiStringToUshortArray(Code3, 5));
           result.AddRange(HTValueConverter.AsciiStringToUshortArray(Code4, 5));
           result.AddRange(HTValueConverter.AsciiStringToUshortArray(Code5, 5));
           result.AddRange(HTValueConverter.AsciiStringToUshortArray(Code6, 5));
           result.AddRange(HTValueConverter.AsciiStringToUshortArray(Code7, 5));
           result.AddRange(HTValueConverter.AsciiStringToUshortArray(Code8, 5));
           result.AddRange(HTValueConverter.AsciiStringToUshortArray(Code9, 5));
           result.AddRange(HTValueConverter.AsciiStringToUshortArray(Code10, 5));
           return result.ToArray();
       }
    }
}
