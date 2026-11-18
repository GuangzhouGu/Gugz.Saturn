using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CIMBase;
using HYC.HTTools;

namespace CIMBOE
{
    public class SentOutJobReport_PanelMaterialUsedReportBlock : ICIMData
    {
        /// <summary>
        /// 长度为10，字节总数为20
        /// </summary>
        public string PanelID { get; set; }
        public ushort MaterialType1 { get; set; }
        /// <summary>
        /// 长度为10，字节总数为20
        /// </summary>
        public string MaterialID1 { get; set; }
        /// <summary>
        /// 长度为10，字节总数为20
        /// </summary>
        public string MaterialLotID1 { get; set; }
        /// <summary>
        /// 长度为5，字节总数为10
        /// </summary>
        public string MaterialVendorID1 { get; set; }
        public ushort MaterialType2 { get; set; }
        /// <summary>
        /// 长度为10，字节总数为20
        /// </summary>
        public string MaterialID2 { get; set; }
        /// <summary>
        /// 长度为10，字节总数为20
        /// </summary>
        public string MaterialLotID2 { get; set; }
        /// <summary>
        /// 长度为5，字节总数为10
        /// </summary>
        public string MaterialVendorID2 { get; set; }
        public ushort MaterialType3 { get; set; }
        /// <summary>
        /// 长度为10，字节总数为20
        /// </summary>
        public string MaterialID3 { get; set; }
        /// <summary>
        /// 长度为10，字节总数为20
        /// </summary>
        public string MaterialLotID3 { get; set; }
        /// <summary>
        /// 长度为5，字节总数为10
        /// </summary>
        public string MaterialVendorID3 { get; set; }
        public ushort MaterialType4 { get; set; }
        /// <summary>
        /// 长度为10，字节总数为20
        /// </summary>
        public string MaterialID4 { get; set; }
        /// <summary>
        /// 长度为10，字节总数为20
        /// </summary>
        public string MaterialLotID4 { get; set; }
        /// <summary>
        /// 长度为5，字节总数为10
        /// </summary>
        public string MaterialVendorID4 { get; set; }

        public ushort MaterialQTY1 { get; set; }
        public ushort MaterialQTY2 { get; set; }
        public ushort MaterialQTY3 { get; set; }
        public ushort MaterialQTY4 { get; set; }



        public ushort[] ToArray()
        {
            List<ushort> result = new List<ushort>();
            result.AddRange(HTValueConverter.AsciiStringToUshortArray(PanelID, 10));
            result.Add(MaterialType1);
            result.AddRange(HTValueConverter.AsciiStringToUshortArray(MaterialID1,10));
            result.AddRange(HTValueConverter.AsciiStringToUshortArray(MaterialLotID1, 10));
            result.AddRange(HTValueConverter.AsciiStringToUshortArray(MaterialVendorID1, 5));

            result.Add(MaterialType2);
            result.AddRange(HTValueConverter.AsciiStringToUshortArray(MaterialID2, 10));
            result.AddRange(HTValueConverter.AsciiStringToUshortArray(MaterialLotID2, 10));
            result.AddRange(HTValueConverter.AsciiStringToUshortArray(MaterialVendorID2, 5));

            result.Add(MaterialType3);
            result.AddRange(HTValueConverter.AsciiStringToUshortArray(MaterialID3, 10));
            result.AddRange(HTValueConverter.AsciiStringToUshortArray(MaterialLotID3, 10));
            result.AddRange(HTValueConverter.AsciiStringToUshortArray(MaterialVendorID3, 5));

            result.Add(MaterialType4);
            result.AddRange(HTValueConverter.AsciiStringToUshortArray(MaterialID4, 10));
            result.AddRange(HTValueConverter.AsciiStringToUshortArray(MaterialLotID4, 10));
            result.AddRange(HTValueConverter.AsciiStringToUshortArray(MaterialVendorID4, 5));

            result.Add(MaterialQTY1);
            result.Add(MaterialQTY2);
            result.Add(MaterialQTY3);
            result.Add(MaterialQTY4);
            return result.ToArray();

        }
    }
}
