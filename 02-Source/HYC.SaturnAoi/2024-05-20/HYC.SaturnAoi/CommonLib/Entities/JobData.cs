using HYC.HTTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Entities
{
    /// <summary>
    /// CIM下发的产品数据
    /// </summary>
    public class JobData
    {
        public string PanelID = string.Empty;
        public ushort PanelType = 0;
        public string WorkOrder = string.Empty;
        public string ProductID = string.Empty;
        public string PPID = string.Empty;
        public string JobJudge = string.Empty;
        public string JobGrade = string.Empty;
        public string RiskFlag = string.Empty;
        public ushort[] Reserve = new ushort[10];

        /// <summary>
        /// 数据转换为ushort数组
        /// </summary>
        /// <returns></returns>
        public ushort[] ToArray()
        {
            List<ushort> datas = new List<ushort>();
            datas.AddRange(HTValueConverter.AsciiStringToUshortArray(PanelID, 10));
            datas.Add(PanelType);
            datas.AddRange(HTValueConverter.AsciiStringToUshortArray(WorkOrder, 10));
            datas.AddRange(HTValueConverter.AsciiStringToUshortArray(ProductID, 10));
            datas.AddRange(HTValueConverter.AsciiStringToUshortArray(PPID, 20));
            datas.AddRange(HTValueConverter.AsciiStringToUshortArray(JobJudge, 1));
            datas.AddRange(HTValueConverter.AsciiStringToUshortArray(JobGrade, 1));
            datas.AddRange(HTValueConverter.AsciiStringToUshortArray(RiskFlag, 1));
            datas.AddRange(Reserve);
            return datas.ToArray();
        }

        /// <summary>
        /// 从ushort数组转换数据
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="startIndex"></param>
        public void FromArray(ushort[] datas, int startIndex)
        {
            PanelID = HTValueConverter.UshortArrayToAsciiString(datas, startIndex, 20);
            //PanelType = datas[startIndex + 10];
            //WorkOrder = HTValueConverter.UshortArrayToAsciiString(datas, startIndex + 11, 10);
            //ProductID = HTValueConverter.UshortArrayToAsciiString(datas, startIndex + 21, 10);
            //PPID = HTValueConverter.UshortArrayToAsciiString(datas, startIndex + 31, 20);
            //JobJudge = HTValueConverter.UshortArrayToAsciiString(datas, startIndex + 51, 1);
            //JobGrade = HTValueConverter.UshortArrayToAsciiString(datas, startIndex + 52, 1);
            //RiskFlag = HTValueConverter.UshortArrayToAsciiString(datas, startIndex + 53, 1);
        }

        /// <summary>
        /// 转换为文本
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("PanelID:{0},PanelType:{1},WorkOrder:{2},ProductID:{3},PPID:{4},JobJudge:{5},JobGrade:{6},RiskFlag:{7}",
                PanelID, PanelType, WorkOrder, ProductID, PPID, JobJudge, JobGrade, RiskFlag);
        }
    }
}
