using CIMBase;
using HYC.HTTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CIMBOE
{
    public class RMSReportBlock : ICIMData
    {

        /// 10个ushort长度,总长度20个字符
        /// </summary>
        public string RecipeID { get; set; }

        public ushort RecipeCode { get; set; }
        /// <summary>
        /// 长度30个ushort 60个字节
        /// </summary>
        public string PreprocessingName { get; set; }
        /// <summary>
        /// 长度30个ushort 60个字节
        /// </summary>
        public string PostprocessingName { get; set; }
        /// <summary>
        /// 长度30个ushort 60个字节
        /// </summary>
        public string PGName { get; set; }

        public ushort[] ToArray()
        {
            try
            {
                List<ushort> result = new List<ushort>();
                result.AddRange(HTValueConverter.AsciiStringToUshortArray(RecipeID, 10));
                result.Add(RecipeCode);
                result.AddRange(HTValueConverter.AsciiStringToUshortArray(PGName, 30));
                result.AddRange(HTValueConverter.AsciiStringToUshortArray(PostprocessingName, 30));
                result.AddRange(HTValueConverter.AsciiStringToUshortArray(PreprocessingName, 30));
                return result.ToArray();
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
