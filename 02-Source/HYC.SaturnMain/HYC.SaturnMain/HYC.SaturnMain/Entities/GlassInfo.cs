// Copyright (C)  Suzhou HYC Technology Co.,LTD
// 版权所有。
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：PPITester.Entities
// 文件名称    ：GlassInfo.cs
// =================================
// 创 建 者    ：guguangzhou
// 创建日期    ：2021/01/29 09:00:00 
// 功能描述    ：枚举帮助类
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

namespace PPITester.Entities
{

    public  class GlassInfo
    {
        /// <summary>
        /// 玻璃序号,0和1
        /// </summary>
        public int GlassNum { get; set; }

        /// <summary>
        /// 开始测试时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 结束测试时间
        /// </summary>
        public DateTime EndTime { get; set; }
        public string GlassID { get; set; }
        /// <summary>
        /// 所有列的集合
        /// </summary>
        public  List<OneColumnInfo> ColumnList { get; set; }
        public GlassInfo()
        {
            ColumnList = new List<OneColumnInfo>();
        }
      
    }
    /// <summary>
    /// 单列panel信息
    /// </summary>
    public class OneColumnInfo
    {
        /// <summary>
        /// 列序号，0到4
        /// </summary>
        public int ColumnNum { get; set; }
        /// <summary>
        /// 测试状态，-1：等待检测，0：检测完成，1：检测中
        /// </summary>
        public int DetectSts { get; set; }
        /// <summary>
        /// 一列的Panel
        /// </summary>
        public  List<OnePanelInfo> PanelList  { get; set; }
        /// <summary>
        /// 单列检测结果
        /// </summary>
        public string Result { get; set; }
        public OneColumnInfo()
        {
            PanelList = new List<OnePanelInfo>();
        }
        public override string ToString()
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                if (PanelList != null && PanelList.Count > 0)
                {
                    for (int i = 0; i < PanelList.Count; i++)
                    {
                        OnePanelInfo info = PanelList[i];
                        sb.Append(info.ToString());
                    }
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
    }
    /// <summary>
    /// 单个panel的信息
    /// </summary>
    public class OnePanelInfo
    {
        /// <summary>
        /// HLineNG信息列表，水平线不良
        /// </summary>
        public List<PanelNGInfo> HLineNGInfoList { get; set; }
        /// <summary>
        /// VLineNG信息列表，垂直线不良
        /// </summary>
        public List<PanelNGInfo> VLineNGInfoList { get; set; }
        /// <summary>
        /// AreaNG信息列表，点不良
        /// </summary>
        public List<PanelNGInfo> AreaNGInfoList { get; set; }
        /// <summary>
        /// 总NG信息列表
        /// </summary>
        public List<PanelNGInfo> TotalNGInfoList { get ; set; }
        /// <summary>
        /// 行序号
        /// </summary>
        public int RowNum { get; set; }
        /// <summary>
        /// 在整个玻璃中的序号
        /// </summary>
        public int TotalNum { get; set; }
        /// <summary>
        /// 不良数量
        /// </summary>
        public int TotalNGCount { get; set; } 
        /// <summary>
        ///结果
        /// </summary>
        public string Result { get; set; }

        public OnePanelInfo()
        {
            HLineNGInfoList = new List<PanelNGInfo>();
            VLineNGInfoList = new List<PanelNGInfo>();
            AreaNGInfoList = new List<PanelNGInfo>();
            TotalNGInfoList = new List<PanelNGInfo>();
            Result = string.Empty;
        }
        public override string ToString()
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("["+RowNum + ",");
                sb.Append(Result + ",");
                sb.Append(TotalNGCount + ",");
                if (TotalNGInfoList.Count > 0)
                {
                    for (int i = 0; i < TotalNGInfoList.Count; i++)
                    {
                        PanelNGInfo info = TotalNGInfoList[i];
                        sb.Append(info.ToString());
                    }

                }
                sb.Append("]");
                return sb.ToString();
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
    /// <summary>
    /// 单个panel的信息
    /// </summary>
    public class PanelNGInfo
    {

        /// <summary>
        /// 序号
        /// </summary>
        public int NGNum { get; set; }
        /// <summary>
        /// NGCode
        /// </summary>
        public string NGCode { get; set; }
        /// <summary>
        /// NGType
        /// </summary>
        public string NGType { get; set; }
        /// <summary>
        /// IR1给的X方向不良点坐标
        /// </summary>
        public double CoordinationXByIR1 { get; set; }
        /// <summary>
        /// IR1给的Y方向不良点坐标
        /// </summary>
        public double CoordinationYByIR1 { get; set; }
        /// <summary>
        /// IR2给的X方向不良点坐标
        /// </summary>
        public double CoordinationXByIR2 { get; set; }
        /// <summary>
        /// IR2给的Y方向不良点坐标
        /// </summary>
        public double CoordinationYByIR2 { get; set; }
        /// <summary>
        /// Area不良X方向不良点坐标
        /// </summary>
        public double AreaCrdXByTester { get; set; }
        /// <summary>
        ///Area不良Y方向不良点坐标
        /// </summary>
        public double AreaCrdYByTester { get; set; }
        /// <summary>
        /// Line不良X方向起始坐标
        /// </summary>
        public double LineNGStartCrdXByTester { get; set; }
        /// <summary>
        ///  Line不良X方向起始坐标
        /// </summary>
        public double LineNGStartCrdYByTester { get; set; }
        /// <summary>
        ///  Line不良X方向终点坐标
        /// </summary>
        public double LineNGEndCrdXByTester { get; set; }
        /// <summary>
        ///  Line不良Y方向终点坐标
        /// </summary>
        public double LineNGEndCrdYByTester { get; set; }
        public override string ToString()
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("("+NGType+",");
                sb.Append(NGCode + ",");
                sb.Append(NGNum + ",");
                if (NGType == "HLine" || NGType == "VLine")
                {
                    sb.Append(LineNGStartCrdXByTester + ",");
                    sb.Append(LineNGStartCrdYByTester + ",");
                    sb.Append(LineNGEndCrdXByTester + ",");
                    sb.Append(LineNGEndCrdYByTester + ",");
                }
                else
                {
                    sb.Append(AreaCrdXByTester + ",");
                    sb.Append(AreaCrdYByTester + ",");
                }
                return sb.ToString().TrimEnd(',')+")";
            }
            catch (Exception)
            {
                return string.Empty;
            }
        
        }
    }

}
