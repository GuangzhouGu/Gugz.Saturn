using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CIMBase;
using HYC.HTTools;

namespace CIMBOE
{
    public class PPIDRecipeIDMapReportBlock : ICIMData
    {
        /// <summary>
        /// 长度10个ushort，20个字节
        /// </summary>
        public string PPID { get; set; }

        /// <summary>
        /// 长度10个ushort，20个字节
        /// </summary>
        public string RecipeID { get; set; }


        public ushort PPCINFO { get; set; }

        /// <summary>
        /// 长度10个ushort，20个字节
        /// </summary>
        public string LastPPID { get; set; }

        public ushort[] ToArray()
        {
            List<ushort> result = new List<ushort>();
            result.AddRange(HTValueConverter.AsciiStringToUshortArray(PPID, 10));
            result.AddRange(HTValueConverter.AsciiStringToUshortArray(RecipeID, 10));
            result.AddRange(new ushort[18]);//保留字符18个ushort长度
            result.Add(PPCINFO);
            result.AddRange(HTValueConverter.AsciiStringToUshortArray(LastPPID, 10));
            return result.ToArray();
         
        }
    }
}
