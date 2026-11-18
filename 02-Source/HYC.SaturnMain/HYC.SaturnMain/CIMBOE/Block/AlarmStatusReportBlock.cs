using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CIMBase;
using HYC.HTTools;

namespace CIMBOE
{
    /// <summary>
    /// 长度为45个ushort
    /// </summary>
    public class AlarmStatusReportBlock : ICIMData
    {
        /// <summary>
        /// 1个ushort
        /// 0: Alarm Clear
        ///1: Alarm Set
        /// </summary>
        public AlarmStatus AlarmState { get; set; }
        /// <summary>
        /// 1个ushort
        ///0: Machine Level Alarm
        ///1~15: Alarm Occurred Unit Number
        /// </summary>
        public ushort AlarmUnit { get; set; }
        /// <summary>
        /// 1个ushort
        /// </summary>
        public ushort AlarmID { get; set; }
        /// <summary>
        /// 1个ushort
        /// 0: Danger for human
        ///1: Machine error
        ///2: Parameter overflow caused process error
        ///3: Parameter overflow caused machine can't work
        ///4: Cannot recover trouble
        ///5: Machine status warning
        ///6: Process reached to predefined status
        ///7: CIM PC Caused
        /// </summary>
        public AlarmCode AlarmCode  { get; set; }
        /// <summary>
        /// 1个ushort
        /// 0 : Warning
        ///1 : Error
        /// </summary>
        public AlarmType AlarmType { get; set; }
        /// <summary>
        /// 1个ushort
        /// 0 : Using EQ Report Alarm Text
        ///1 : Using BC Alarm Text
        /// </summary>
        public AlarmTextUsingFlag AlarmTextUsingFlag  { get; set; }
        /// <summary>
        /// 长度35个字节
        /// </summary>
        public string AlarmText { get; set; }

        public ushort[] ToArray()
        {
            List<ushort> result = new List<ushort>();
            result.Add((ushort)AlarmState);
            result.Add(AlarmUnit);
            result.Add(AlarmID);
            result.Add((ushort)AlarmCode);
            result.Add((ushort)AlarmType);
            result.Add((ushort)AlarmTextUsingFlag);
            result.AddRange(HTValueConverter.AsciiStringToUshortArray(AlarmText, 35));
            return result.ToArray();
        }
    }

    public enum AlarmStatus
    { 
        AlarmClear,
        AlarmSet = 1,
    }

    public enum AlarmCode
    {
        DangerForHuman,
        MachineError,
        ParameterOverflowCauseProcessError,
        ParameterOverFlowCauseEquipmentCanNotWork,
        CanNotRecoverTrouble,
        MachineStatusWarning,
        ProcessReachedToPredefinedStatus,
        CIMPCCaused,
    }

    public enum AlarmType
    {
        Warning,
        Error,
    }
    public enum AlarmTextUsingFlag
    {
        UsingEQReportAlarmText,
        UsingBCAlarmText
    }
}
