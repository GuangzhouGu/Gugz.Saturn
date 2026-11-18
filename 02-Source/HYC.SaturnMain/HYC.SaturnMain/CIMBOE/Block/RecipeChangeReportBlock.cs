using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CIMBase;
using HYC.HTTools;

namespace CIMBOE
{
    public class RecipeChangeReportBlock : ICIMData
    {
        /// <summary>
        /// 10个ushort长度,总长度20个字符
        /// </summary>
        public string RecipeID { get; set; }

        public ushort[] ToArray()
        {
            List<ushort> result = new List<ushort>();
            result.AddRange(HTValueConverter.AsciiStringToUshortArray(RecipeID, 10));

            return result.ToArray();
        }
    }
}
