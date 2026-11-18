using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CIMBase;
using CIMBOE.Block;

namespace CIMBOE
{
    public class JobDataRequestReplyBlock : ICIMData
    {
        public GlassDataA JobData { get; set; }
        public ReplyStatus Status { get; set; }
        public ushort[] Reserved { get; set; } = new ushort[11];
        public ushort[] ToArray()
        {
            List<ushort> result = new List<ushort>();
            result.Add((ushort)Status);
            result.AddRange(JobData.ToArray());
            return result.ToArray();
        }

        public void LoadDataFormArrary(ushort[] data)
        {
            JobData = new GlassDataA();
            JobData.FromArray(data.Take(84).ToArray());
            Status = (ReplyStatus)data[84];
            Reserved = data.Skip(85).Take(11).ToArray();
        }
    }

    public enum ReplyStatus
    {
        Exist = 1,
        NoExist=2,
    }
}
