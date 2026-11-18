using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DFS.Model;
using System.IO;
using System.Xml.Serialization;

namespace DFS.DataFile
{
    public class DataFile_Saturn : DFSDataFile
    {
        /// <summary>
        /// Glass信息
        /// </summary>
        public GLS_INF GLS_INF { get; set; } = new GLS_INF();
        /// <summary>
        /// Glass检测信息
        /// </summary>
        public GLS_SUM GLS_SUM { get; set; } = new GLS_SUM();
        /// <summary>
        /// Cell信息
        /// </summary>
        public List<PNL_INF> List_PNL_INF { get; set; } = new List<PNL_INF>(); 
        /// <summary>
        /// Cell检测信息
        /// </summary>
        public List<PNL_SUM> List_PNL_SUM { get; set; } = new  List<PNL_SUM>();
        /// <summary>
        /// Cell 画质不良信息
        /// </summary>
        public List<PNL_DFT> List_PNL_DFT { get; set; } = new List<PNL_DFT>();
        /// <summary>
        /// Cell 光学不良信息
        /// </summary>
      //  public List<PNL_PNT> List_PNL_PNT = new List<PNL_PNT>();
        public DataFile_Saturn()
        {
            GLS_INF = new GLS_INF();
            GLS_SUM = new GLS_SUM();
            List_PNL_INF = new List<PNL_INF>();
            List_PNL_SUM = new List<PNL_SUM>();
            List_PNL_DFT = new List<PNL_DFT>();
            //List_PNL_PNT = new List<PNL_PNT>();
        }
        public override string GetContent()
        {
            StringBuilder sb = new StringBuilder();
            ///GLS
            sb.AppendLine(GLS_INF.GetHeader());
            sb.AppendLine(GLS_INF.GetContent());
            sb.AppendLine(GLS_SUM.GetHeader());
            sb.AppendLine(GLS_SUM.GetContent());
            /// INF
         
            if(List_PNL_INF.Count!=List_PNL_SUM.Count)
            {
                return "List_PNL_INF 与 List_PNL_SUM 长度不匹配";
            }
            for (int i = 0; i < List_PNL_INF.Count; i++)
            {
                sb.AppendLine(List_PNL_INF[i].GetHeader());
                sb.AppendLine(List_PNL_INF[i].GetContent());
                sb.AppendLine(List_PNL_SUM[i].GetHeader());
                sb.AppendLine(List_PNL_SUM[i].GetContent());
            }
            ///  DFT
            foreach (PNL_DFT item in List_PNL_DFT)
            {
                sb.AppendLine(item.GetHeader());
                sb.AppendLine(item.GetContent());
            }
            ///  PNT
            //foreach (PNL_PNT item in List_PNL_PNT)
            //{
            //    sb.AppendLine(item.GetHeader());
            //    sb.AppendLine(item.GetContent());
            //}
            return sb.ToString();
          
        }


        public override void ConvertFromFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                string[] lines = File.ReadAllLines(fileName);

                string[] pnlInfHeader = lines[0].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                string[] pnlInfContent = lines[1].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                GetReflectionValues(GLS_INF, pnlInfHeader, pnlInfContent);

                string[] pnlSumHeader = lines[2].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                string[] pnlSumContent = lines[3].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                GetReflectionValues(GLS_SUM, pnlSumHeader, pnlSumContent);

                List_PNL_INF.Clear();
                var linespPNLINFHeader = lines.Where(p => p.StartsWith("PNL_INF")).ToList();
                var linesPNLINFContent = lines.Where(p => p.StartsWith("PNL_INF_DATA")).ToList();
                if (linespPNLINFHeader.Count > 0 && linesPNLINFContent.Count > 0)
                {
                    string[] pnlDftHeader = linespPNLINFHeader[0].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < linesPNLINFContent.Count; i++)
                    {
                        string[] pnlDftContent = linesPNLINFContent[i].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        var data = new PNL_INF();
                        GetReflectionValues(data, pnlDftHeader, pnlDftContent);
                        List_PNL_INF.Add(data);
                    }
                }
                List_PNL_SUM.Clear();
                var linesPNLINFSUMHeader = lines.Where(p => p.StartsWith("PNL_SUM")).ToList();
                var linesPNLINFSUMContent = lines.Where(p => p.StartsWith("PNL_SUM_DATA")).ToList();
                if(linesPNLINFSUMHeader.Count>0 && linesPNLINFSUMContent.Count>0)
                {
                    string[] header = linesPNLINFSUMHeader[0].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < linesPNLINFSUMContent.Count; i++)
                    {
                        string[] content = linesPNLINFSUMContent[i].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        var data = new PNL_SUM();
                        GetReflectionValues(data, header, content);
                        List_PNL_SUM.Add(data); 
                    }
                }

                List_PNL_DFT.Clear();
                var linesPNLDFTHeader = lines.Where(p => p.StartsWith("PNL_DFT")).ToList();
                var linesPNLDFTContent = lines.Where(p => p.StartsWith("PNL_DFT_DATA")).ToList();
                if (linesPNLDFTHeader.Count > 0 && linesPNLDFTContent.Count > 0)
                {
                    string[] header = linesPNLDFTHeader[0].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < linesPNLDFTContent.Count; i++)
                    {
                        string[] content = linesPNLDFTContent[i].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        var data = new PNL_DFT();
                        GetReflectionValues(data, header, content);
                        List_PNL_DFT.Add(data);
                    }
                }

                //List_PNL_PNT.Clear();
                //var linesPNLPNTHeader = lines.Where(p => p.StartsWith("PNL_PNT")).ToList();
                //var linesPNLPNTContent = lines.Where(p => p.StartsWith("PNL_PNT_DATA")).ToList();
                //if (linesPNLPNTHeader.Count > 0 && linesPNLPNTContent.Count > 0)
                //{
                //    string[] header = linesPNLPNTHeader[0].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                //    for (int i = 0; i < linesPNLPNTContent.Count; i++)
                //    {
                //        string[] content = linesPNLPNTContent[i].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                //        var data = new PNL_PNT();
                //        GetReflectionValues(data, header, content);
                //        List_PNL_PNT.Add(data);
                //    }
                //}
            }
        }

      
    }
    public class GLASS
    {
        public GLS_INF GLASS_INF { get; set; } = new GLS_INF();
        [XmlArrayItem("PANEL")]
        public List<PNL_INF> PNL_INF { get; set; }
    }
    [XmlRoot("GLASS")]
    public class GLASS2
    {
        public GLS_INF GLASS_INF { get; set; } = new GLS_INF();
        [XmlArrayItem("PANEL")]
        public List<PNL_DFT> PNL_INF { get; set; }
    }
}
