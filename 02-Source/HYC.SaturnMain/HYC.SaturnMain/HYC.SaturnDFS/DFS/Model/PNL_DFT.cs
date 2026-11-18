using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DFS.Model
{
    public class PNL_DFT : DFSItemModel
    {
        public string FIELD_COUNT { get; set; }
        public string PNL_ID { get; set; }
        public string DEFECT_NO { get; set; }
        public string AOI_DEFECT_SIZE { get; set; }    //1-999
        public string AOI_DEFECT_CODE { get; set; }
        public string PR_COOR_X { get; set; } = "***";
        public string PR_COOR_Y { get; set; } = "***";
        public string PR_TX { get; set; } = "***";
        public string PR_RX { get; set; } = "***";
        public string PNL_COOR_X { get; set; }
        public string PNL_COOR_Y { get; set; }
        public string DEFECT_LAYER { get; set; } = "***";
        public string DEFECT_SIZE_X { get; set; }
        public string DEFECT_SIZE_Y { get; set; }
        public string DEFECT_LENGTH { get; set; }
        public string DEFECT_AREA { get; set; }
        public string DEFECT_TYPE_1 { get; set; }
        public string DEFECT_TYPE_2 { get; set; }
        public string REPEAT_DEFECT { get; set; } = "***";
        public string MASK_DEFECT { get; set; } = "***";
        public string MURA_DEFECT { get; set; } = "***";
        public string IMAGE { get; set; } = "***";
        //暂存数据
        public Image ImageData = null;
        public string ImageName = string.Empty;

        protected override string GetHeaderStart()
        {
            return "PNL_DFT^";
        }

        protected override string GetContentStart()
        {
            return "PNL_DFT_DATA^";
        }
    }
}
