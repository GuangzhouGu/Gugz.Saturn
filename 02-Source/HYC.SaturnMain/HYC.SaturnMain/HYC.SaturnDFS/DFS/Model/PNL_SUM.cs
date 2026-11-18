using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DFS.Model
{
    public class PNL_SUM : DFSItemModel
    {
        public string FIELD_COUNT { get; set; }
      
        /// <summary>
        /// Mura缺陷数量
        /// </summary>
        public int DEFECT_QTY { get; set; }            //Mura Defect QTY
        public string REPEAT_DEFECT_QTY { get; set; } = "***";
        public int MASK_DEFECT_QTY { get; set; } 
        public int MURA_DEFECT_PNL { get; set; } 
        public int PARTICLE_QTY { get; set; } 
        public string FLOATING_PARTICLE_QTY { get; set; } = "***";
        public string S_DEFECT_QTY { get; set; } = "***";
        public string M_DEFECT_QTY { get; set; } = "***";
        public string L_DEFECT_QTY { get; set; } = "***";
        public string E_DEFECT_QTY { get; set; } = "***";
        public string U_DEFECT_QTY { get; set; } = "***";
        public string O_DEFECT_QTY { get; set; } = "***";

        protected override string GetHeaderStart()
        {
            return "PNL_SUM^";
        }

        protected override string GetContentStart()
        {
            return "PNL_SUM_DATA^";
        }
    }
}
