using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace DFS.Model
{
    public class PNL_INF : DFSItemModel
    {
        public int FIELD_COUNT { get; set; }
        /// <summary>
        /// 产品ID
        /// </summary>
        public string PNL_ID { get; set; }    //ABC12345
        /// <summary>
        /// G/N/P
        /// </summary>
        public string PNL_JUDGE { get; set; }
        /// <summary>
        /// S/A/T/N
        /// </summary>
        public string PNL_GRADE { get; set; }
        /// <summary>
        /// 产品X方向起始坐标
        /// </summary>
        public float PNL_X_POS { get; set; }  //70000UM
        /// <summary>
        /// 产品Y方向起始坐标
        /// </summary>
        public float PNL_Y_POS { get; set; }  //140000UM
        /// <summary>
        /// 产品X尺寸
        /// </summary>
        public float PNL_X_SIZE { get; set; }  //70000UM
        /// <summary>
        /// 产品Y尺寸
        /// </summary>
        public float PNL_Y_SIZE { get; set; }  //140000UM


        protected override string GetHeaderStart()
        {
            return "PNL_INF^";
        }

        protected override string GetContentStart()
        {
            return "PNL_INF_DATA^";
        }
    }
}
