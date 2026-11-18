using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CIMBase;
using HYC.HTTools;

namespace CIMBOE
{
    public class CurrentRecipeBlock : ICIMData
    {
        /// <summary>
        /// 1个ushort长度,总长度20个字符
        /// </summary>
        public ushort RecipeID { get; set; }
        /// <summary>
        /// 15个ushort长度,总长度20个字符
        /// </summary>
        public ushort[] Reserved { get; set; } = new ushort[15];
        public ushort[] ToArray()
        {
            List<ushort> result = new List<ushort>();
            result.Add(RecipeID);
            result.AddRange(Reserved);
            return result.ToArray();
        }
    }
}
