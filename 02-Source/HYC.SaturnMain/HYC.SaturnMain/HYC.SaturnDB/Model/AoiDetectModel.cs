/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2023 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnDB.Model
// 文件名称    ：AoiDetectModel
// =================================
// 创 建 者    ：guguangzhou
// 创建日期    ：
// 功能描述    ：
// 使用说明    ：
//
//
// 创建标识：
//
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYC.SaturnDB.Model
{
    /// <summary>
    /// 单个Cell Aoi检测信息数据模型
    /// </summary>
    public class AoiDetectModel
    {
        /// <summary>
      /// 产品ID
      /// </summary>
        public string GlassID { get; set; }
        public int GlassNum { get; set; }
        public int ColumnNum { get; set; }
        public string CellID { get; set; }
        public string PCIndex { get; set; }
        public DateTime EQPInTime { get; set; }
        /// <summary>
        /// 检测模式，0：自动，1：手动
        /// </summary>
        public int TestMode { get; set; }

        /// <summary>
        /// 机种编号
        /// </summary>
        public string RecipeID { get; set; }

        /// <summary>
        /// 测试结果
        /// </summary>
        public string Result { get; set; }

        /// <summary>
        /// Code
        /// </summary>
        public string DetectCode { get; set; }
        /// <summary>
        /// 等级
        /// </summary>
        public string DetectGrade { get; set; }
        /// <summary>
        /// 不良Code文本
        /// </summary>
        public string ErrorText { get; set; }

        /// <summary>
        /// 图片的ROI区域
        /// </summary>
        public string ImgROI { get; set; }
        /// <summary>
        /// 图片保存路径
        /// </summary>
        public string BmpSavePath { get; set; }

        /// <summary>
        /// 测试开始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 测试结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// 测试耗时时间
        /// </summary>
        public string ProcessingTime { get; set; }
        /// <summary>
        /// 检测类型，Aoi/Mura
        /// </summary>
        public string DetectType { get; set; }
        public int BirghtDotDefectCount { get; set; }
        public int DarkDotDefectCount { get; set; }
        public int LineDefectCount { get; set; }
        public int Mura2DefectCount { get; set; }
        public int MuraDefectCount { get; set; }
        public int ReserveDefectCount { get; set; }
        public int TotalDefectCount { get; set; }
        public int XsycDefectCount { get; set; }
        /// <summary>
        /// 1:G(Good),3:N(NG)
        /// </summary>
        public string JobJudge { get; set; }
        /// <summary>
        /// 0:Inspect Skip or No Judge or No Grade
        /// 1:Y
        /// 2:SK
        /// 3:RW
        /// 4:RD
        /// 5:RL
        /// 6:RC
        /// 7:RG
        /// </summary>
        public string JobGrade { get; set; }
    }
}
