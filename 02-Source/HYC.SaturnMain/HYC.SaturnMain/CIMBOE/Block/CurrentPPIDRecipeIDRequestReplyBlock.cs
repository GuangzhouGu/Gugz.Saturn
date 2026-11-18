using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CIMBase;
using HYC.HTTools;

namespace CIMBOE
{
    public class CurrentPPIDRecipeIDRequestReplyBlock : ICIMData
    {
        public string PPID { get; set; }

        public string RecipeID { get; set; }

        public ushort[] ToArray()
        {
            List<ushort> result = new List<ushort>();
            result.AddRange(HTValueConverter.AsciiStringToUshortArray(PPID, 10));
            result.AddRange(HTValueConverter.AsciiStringToUshortArray(RecipeID, 10));
            return result.ToArray();
        }
    }
}
