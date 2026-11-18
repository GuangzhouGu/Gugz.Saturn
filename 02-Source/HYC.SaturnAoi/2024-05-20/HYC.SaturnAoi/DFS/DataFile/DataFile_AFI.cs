using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using DFS.Model;

namespace DFS.DataFile
{
    public class DataFile_AFI : DFSDataFile
    {
        public PNL_INF PNL_INF = new PNL_INF();
        public PNL_SUM PNL_SUM = new PNL_SUM();
        public List<PNL_PNT> ListPNT = new List<PNL_PNT>();
        public List<PNL_DFT> ListDFT = new List<PNL_DFT>();




        public override string GetContent()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(PNL_INF.GetHeader());
            sb.AppendLine(PNL_INF.GetContent());
            sb.AppendLine(PNL_SUM.GetHeader());
            sb.AppendLine(PNL_SUM.GetContent());
            sb.AppendLine(new PNL_PNT().GetHeader());
            for (int i = 0; i < ListPNT.Count; i++)
            {
                sb.AppendLine(ListPNT[i].GetContent());
            }
            sb.AppendLine(new PNL_DFT().GetHeader());
            for (int i = 0; i < ListDFT.Count; i++)
            {
                sb.AppendLine(ListDFT[i].GetContent());
            }
            return sb.ToString();
        }



        public override void ConvertFromFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                string[] lines = File.ReadAllLines(fileName);

                string[] pnlInfHeader = lines[0].Split(new char[] { '^' }, StringSplitOptions.RemoveEmptyEntries);
                string[] pnlInfContent = lines[1].Split(new char[] { '^' }, StringSplitOptions.RemoveEmptyEntries);
                GetReflectionValues(PNL_INF, pnlInfHeader, pnlInfContent);

                string[] pnlSumHeader = lines[2].Split(new char[] { '^' }, StringSplitOptions.RemoveEmptyEntries);
                string[] pnlSumContent = lines[3].Split(new char[] { '^' }, StringSplitOptions.RemoveEmptyEntries);
                GetReflectionValues(PNL_SUM, pnlSumHeader, pnlSumContent);

                ListPNT.Clear();
                var linesPNTHeader = lines.Where(p => p.StartsWith("PNL_PNT")).ToList();
                var linesPNTContent = lines.Where(p => p.StartsWith("PNL_PNT_DATA")).ToList();
                if (linesPNTHeader.Count > 0 && linesPNTContent.Count > 0)
                {
                    string[] pnlPntHeader = linesPNTHeader[0].Split(new char[] { '^' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < linesPNTContent.Count; i++)
                    {
                        string[] pnlPntContent = linesPNTContent[i].Split(new char[] { '^' }, StringSplitOptions.RemoveEmptyEntries);
                        var data = new PNL_PNT();
                        GetReflectionValues(data, pnlPntHeader, pnlPntContent);
                        ListPNT.Add(data);
                    }
                }

                ListDFT.Clear();
                var linesDFTHeader = lines.Where(p => p.StartsWith("PNL_DFT")).ToList();
                var linesDFTContent = lines.Where(p => p.StartsWith("PNL_DFT_DATA")).ToList();
                if (linesDFTHeader.Count > 0 && linesDFTContent.Count > 0)
                {
                    string[] pnlDftHeader = linesDFTHeader[0].Split(new char[] { '^' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < linesDFTContent.Count; i++)
                    {
                        string[] pnlDftContent = linesDFTContent[i].Split(new char[] { '^' }, StringSplitOptions.RemoveEmptyEntries);
                        var data = new PNL_DFT();
                        GetReflectionValues(data, pnlDftHeader, pnlDftContent);
                        ListDFT.Add(data);
                    }
                }
            }
        }

        public List<Image> GetImages(string imgPath)
        {
            List<Image> lstImages = new List<Image>();
            for (int i = 0; i < ListDFT.Count; i++)
            {
                try
                {
                    lstImages.Add(null);
                    string imgName = Path.Combine(imgPath, Path.GetFileName(ListDFT[i].IMAGE));
                    lstImages[i] = Image.FromFile(imgName);
                }
                catch (Exception)
                { }
            }
            return lstImages;
        }
    }
}
