using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFS.Model
{
    public class GLS_INF : DFSItemModel
    {
        public string FIELD_COUNT { get; set; }
        /// <summary>
        /// LotID   ex:F67N420152
        /// </summary>
        public string LOT_ID { get; set; }

        /// <summary>
        /// Step ID  ex:T380OSN
        /// </summary>
        public string STEP_ID { get; set; }
        public string HGLS_ID { get; set; }
        public string Merge_HGLS_ID { get; set; } = "***";
        /// <summary>
        /// Slot ID  ex;2023
        /// </summary>
        public string SLOT_ID { get; set; }
        /// <summary>
        /// Glass Judgment ex:G/N
        /// </summary>
        public string HGLS_JUDGE { get; set; } 
        /// <summary>
        /// Glass Judgment ex:G/N
        /// </summary>
        public string HGLS_GRADE { get; set; }
        /// <summary>
        /// Product ID ex:BF068M49-TK0-7Q00 
        /// </summary>
        public string PRODUCT_ID { get; set; }
        /// <summary>
        /// Equipment ID   ex:7FXTS01
        /// </summary>
        public string EQUIPMENT_ID { get; set; }
        /// <summary>
        /// Recipe Name  ex:65B_IQC_SA_D_C
        /// </summary>
        public string RECIPE_NAME { get; set; }
        /// <summary>
        /// Automation status    ex:ONR/ONL/OFF
        /// </summary>
        public string AUTO_MODE { get; set; } = "***";
        /// <summary>
        /// Processing Time (Second)
        /// </summary>
        public string PROCESS_TIME { get; set; }
        /// <summary>
        /// Process start time (YYYYMMDDHHMMSS)  ex:20190311_090909
        /// </summary>
        public string START_TIME { get; set; }

        /// <summary>
        /// Process end time (YYYYMMDDHHMMSS)  ex:20190311_090909
        /// </summary>
        public string END_TIME { get; set; }
        /// <summary>
        /// System’s program version 
        /// </summary>
        public string PROGRAM { get; set; }
        public string TEST_MODE { get; set; } = "***";
        public string ALIGN_1_OFFSET { get; set; } = "***";
        public string ALIGN_2_OFFSET { get; set; } = "***";
        public string ILLUMINATION_FILTER { get; set; } = "***";
        public string CAMERA1_TEST_START_PNL { get; set; }
        public string CAMERA1_TEST_END_PNL { get; set; }
        public string CAMERA2_TEST_START_PNL { get; set; }
        public string CAMERA2_TEST_END_PNL { get; set; }
        public string CAMERA3_TEST_START_PNL { get; set; }
        public string CAMERA3_TEST_END_PNL { get; set; }
        public string CAMERA4_TEST_START_PNL { get; set; }
        public string CAMERA4_TEST_END_PNL { get; set; }
        public string CAMERA5_TEST_START_PNL { get; set; }
        public string CAMERA5_TEST_END_PNL { get; set; }
        public string CAMERA6_TEST_START_PNL { get; set; }
        public string CAMERA6_TEST_END_PNL { get; set; }

        protected override string GetHeaderStart()
        {
            return "GLS_INF^";
        }

        protected override string GetContentStart()
        {
            return "GLS_INF_DATA^";
        }
    }

}
