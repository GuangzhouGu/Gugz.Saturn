using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DFS.Model
{
    public class PNLINFO : DFSItemModel
    {
        public string PANEL_ID { get; set; }
        public string PRODUCT_ID { get; set; }
        /// <summary>
        /// 从BC获取
        /// </summary>
        public string STEP_ID { get; set; }
        public string EQUIPMENT_ID { get; set; }
        /// <summary>
        /// 从BC获取
        /// </summary>
        public string PPID { get; set; }
        /// <summary>
        /// 机种名
        /// </summary>
        public string RECIPE_NAME { get; set; }
        /// <summary>
        /// 载台 ID  
        /// A 11\12\13\14
        /// B 21\22\23\24
        /// C 31\32\33\34
        /// D 41\42\43\44
        /// </summary>
        public string STAGE_ID { get; set; }
        /// <summary>
        /// 无显上报NG，点亮上报OK
        /// </summary>
        public string CONTACT { get; set; }

        /// <summary>
        /// CTP程序名称
        /// </summary>
        public string CTP_NAME { get; set; }
        public string CTP_JUDGE { get; set; }
        public string CTP_CODE1 { get; set; }
        public string CTP_CODE2 { get; set; }
        public string CTP_CODE3 { get; set; }
        public string CTP_CODE4 { get; set; }
        public string CTP_CODE5 { get; set; }

        public string PG_NAME { get; set; }
        public string AOI_JUDGE { get; set; }
        public string AOI_CODE1 { get; set; }
        public string AOI_CODE2 { get; set; }
        public string AOI_CODE3 { get; set; }
        public string AOI_CODE4 { get; set; }
        public string AOI_CODE5 { get; set; }

        public string SVI_JUDGE { get; set; }
        public string SVI_CODE1 { get; set; }
        public string SVI_CODE2 { get; set; }
        public string SVI_CODE3 { get; set; }
        public string SVI_CODE4 { get; set; }
        public string SVI_CODE5 { get; set; }

        /// <summary>
        /// 复判平台
        /// </summary>
        public string MFT_STAGE { get; set; }
        /// <summary>
        /// 复判结果 OK/NG
        /// </summary>
        public string MFT_JUDGE { get; set; }
        public string MFT_CODE1 { get; set; }
        public string MFT_CODE2 { get; set; }
        public string MFT_CODE3 { get; set; }
        public string MFT_CODE4 { get; set; }
        public string MFT_CODE5 { get; set; }

        /// <summary>
        /// AOI+SVI 所有缺陷count数
        /// </summary>
        public string TOTAL_DEFECT { get; set; }
        /// <summary>
        /// AOI数量
        /// </summary>
        public string AOI_DEFECT_COUNT { get; set; }
        /// <summary>
        /// Mura数量
        /// </summary>
        public string SVI_DEFECT_COUNT { get; set; }

        public string CTP_TIME { get; set; }
        public string AOI_TIME { get; set; }
        public string SVI_TIME { get; set; }
        public string MFT_TIME { get; set; }

        /// <summary>
        /// 检测开始时间
        /// </summary>
        public string PS_TIME { get; set; }
        /// <summary>
        /// 检测结束时间
        /// </summary>
        public string PE_TIME { get; set; }

        /// <summary>
        /// 获取标题行
        /// </summary>
        /// <returns></returns>
        public override string GetHeader()
        {
            return "PNLINFO^" + GetHeaderPart();
        }
    }
}
