using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFS.Model
{
    public class GLS_SUM:DFSItemModel
    {
        public int FIELD_COUNT { get; set; }
        public int INPUT_PNL_QTY { get; set; }
        public int PNL_JUDGE_G_QTY { get; set; }
        public int HGlass_DEFECT_TDQTY { get; set; }
        public int S_DEFECT_TDQTY { get; set; }
        public int M_DEFECT_TDQTY { get; set; }
        public int L_DEFECT_TDQTY { get; set; }
        public int E_DEFECT_TDQTY { get; set; }
        public int U_DEFECT_TDQTY { get; set; }
        public int O_DEFECT_TDQTY { get; set; }
        public int Black_DEFECT_TDQTY { get; set; }
        public int White_DEFECT_TDQTY { get; set; }
        public int AA_DEFECT_TDQTY { get; set; }
        public string Top_DEFECT_TDQTY { get; set; }="***";
        public string Bottom_DEFECT_TDQTY { get; set; } = "***";
        public string Left_DEFECT_TDQTY { get; set; } = "***";
        public string Right_DEFECT_TDQTY { get; set; } = "***";
        public string HGlass_DEFECT_CDQTY { get; set; } = "***";
        public string S_DEFECT_CDQTY { get; set; } = "***";
        public string M_DEFECT_CDQTY { get; set; } = "***";
        public string L_DEFECT_CDQTY { get; set; } = "***";
        public string E_DEFECT_CDQTY { get; set; } = "***";
        public string U_DEFECT_CDQTY { get; set; } = "***";
        public string O_DEFECT_CDQTY { get; set; } = "***";
        public string Black_DEFECT_CDQTY { get; set; } = "***";
        public string White_DEFECT_CDQTY { get; set; } = "***";
        public string AA_DEFECT_CDQTY { get; set; } = "***";
        public string Top_DEFECT_CDQTY { get; set; } = "***";
        public string Bottom_DEFECT_CDQTY { get; set; } = "***";
        public string Left_DEFECT_CDQTY { get; set; } = "***";
        public string Right_DEFECT_CDQTY { get; set; } = "***";
        public string REPEAT_DEFECT_PNL_QTY { get; set; } = "***";
        public string MASK_DEFECT_PNL_QTY { get; set; } = "***";
        public string MURA_DEFECT_PNL_QTY { get; set; } = "***";

        protected override string GetHeaderStart()
        {
            return "GLS_SUM^";
        }

        protected override string GetContentStart()
        {
            return "GLS_SUM_DATA^";
        }
    }
}
