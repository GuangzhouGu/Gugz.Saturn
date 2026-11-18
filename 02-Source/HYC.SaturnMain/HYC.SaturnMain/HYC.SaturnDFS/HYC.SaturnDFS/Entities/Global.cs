using HYC.SaturnDFS.Param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYC.SaturnDFS.Entities
{
    public class Global
    {
        public static string[] CellID = new string[] { "AA", "AB", "AC", "AD", "AE", "AF", "AG", "AH", "AJ", "AK", "AL", "AM", "AN", "AP", "AQ", "AR", "AS", "AT", "AU", "AV", "AW", "AX", "AY", "AZ" };
        public static PanelPara PanelPara = null;
        public static string CurRecipeID = string.Empty;
        public static string GetCellID(int colNum, int cellNum)
        {
            return CellID[cellNum] + $"0{colNum + 1}";
        }

    }
}
