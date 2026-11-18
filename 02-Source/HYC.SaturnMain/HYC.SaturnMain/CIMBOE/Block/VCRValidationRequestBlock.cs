using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CIMBase;
using HYC.HTTools;

namespace CIMBOE
{
    public class VCRValidationRequestBlock : ICIMData
    {
        public ushort VCRNo { get; set; }

        //10个ushort,20个字节
        public string OriginalPanelID { get; set; }

        //10个ushort，20个字节
        public string VCRReadPanelID { get; set; }


        public VCRValidationRequestFlag Flag { get; set; }

        public VCRValidationRequestObject Object { get; set; }

        public VCRValidationRequestVCRType VCRType { get; set; }


        public ushort[] ToArray()
        {
            List<ushort> result = new List<ushort>();
            result.Add(VCRNo);
            result.AddRange(HTValueConverter.AsciiStringToUshortArray(OriginalPanelID, 10));
            result.AddRange(HTValueConverter.AsciiStringToUshortArray(VCRReadPanelID, 10));
            result.Add((ushort)Flag);
            result.Add((ushort)Object);
            result.Add((ushort)VCRType);
            return result.ToArray();
        }
    }

    public enum VCRValidationRequestFlag
    {
        Mismatch = 1,
        KeyIn = 2,
        ByPass = 3
    }

    public enum VCRValidationRequestObject
    {
        RequestToCIMPC = 1,
        RequestToMES = 2
    }

    public enum VCRValidationRequestVCRType
    {
        StationaryVCR = 1,
        HandTypeVCR = 2,
    }
}
