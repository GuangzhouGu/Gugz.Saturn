using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CIMBase;
using CIMBOE.Block;

namespace CIMBOE
{
    public class VCRValidationRequestReplyBlock : ICIMData
    {

        public ReplyStatus Status { get; set; }

        public GlassDataA JobData { get; set; }



        public ushort[] ToArray()
        {
            List<ushort> result = new List<ushort>();
            result.Add((ushort)Status);
            result.AddRange(JobData.ToArray());
            return result.ToArray();
        }

        public void LoadDataFormArrary(ushort[] data)
        {
            Status = (ReplyStatus)data[0];
            GlassDataA tmp = new GlassDataA();
            tmp.FromArray(data.Skip(1).Take(data.Length - 1).ToArray());
            JobData = tmp;
        }
    }
}
