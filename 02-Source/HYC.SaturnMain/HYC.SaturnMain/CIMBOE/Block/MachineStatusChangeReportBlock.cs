using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CIMBase;

namespace CIMBOE
{
    public class MachineStatusChangeReportBlock : ICIMData
    {
       public MachineStatus MainMachineStatus { get; set; }
        public MachineStatusReasonCode ReasonCode { get; set; }
        public ushort[] UnitStatus { get; set; } = new ushort[30];
        public ushort[] UnitStatusReasonCode { get; set; } = new ushort[30];
        public ushort[] SubUnitStatus { get; set; } = new ushort[9];
        public ushort[] SubUnitStatusReasonCode { get; set; } = new ushort[9];
        public ushort[] ToArray()
       {
           List<ushort> result = new List<ushort>();
           result.Add((ushort)MainMachineStatus);
           result.Add((ushort)ReasonCode);
           result.AddRange(UnitStatus);
            result.AddRange(UnitStatusReasonCode);
            result.AddRange(SubUnitStatus);
            result.AddRange(SubUnitStatusReasonCode);
            return result.ToArray();
       }
    }

    public enum MachineStatus
    {
        PM=1,
        /// <summary>
        /// Stop
        /// </summary>
        BM=2,
        Pause=3,
        Idle=4,
        Run=5,
    }

    public enum MachineStatusReasonCode
    {
        Default=0,
        MC_DIL=1001,
        MC_ALC=1002,
        MC_CWP=1003,
        MC_TFL=1004,
        MC_ACF=1005,
        MC_COF=1006,
        MC_FPC=1007,
        MC_UV=1008,
        MC_OCA=1009,
        MC_CG=1010,
        MC_CF=1011,
        MC_TSP=1012,
        MC_PF=1013,
        MC_SCF=1014,
        ID_MT=2001,
        ID_OP=2002,
        ID_P_Y=2003,
        ID_P_N=2004
    }
}
