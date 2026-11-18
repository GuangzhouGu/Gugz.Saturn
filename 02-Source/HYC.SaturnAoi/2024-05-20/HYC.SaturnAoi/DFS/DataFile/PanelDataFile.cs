using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using DFS.Model;

namespace DFS.DataFile
{
    public class PanelDataFile : DFSDataFile
    {
        public PNLINFO PNLINFO = new PNLINFO();
        public List<PG_INFO> lstPG_INFO = new List<PG_INFO>();
        public List<AOI_INFO> lstAOI_INFO = new List<AOI_INFO>();
        public List<SVI_INFO> lstSVI_INFO = new List<SVI_INFO>();
        public List<MFT_INFO> lstMFT_INFO = new List<MFT_INFO>();
        public List<LUMI_INFO> lstLUMI_INFO = new List<LUMI_INFO>();

        /// <summary>
        /// 获取内容
        /// </summary>
        /// <returns></returns>
        public override string GetContent()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(PNLINFO.GetHeader());
            sb.AppendLine(PNLINFO.GetContent());

            sb.AppendLine(new PG_INFO().GetHeader());
            foreach (var item in lstPG_INFO)
            {
                sb.AppendLine(item.GetContent());
            }

            sb.AppendLine(new AOI_INFO().GetHeader());
            foreach (var item in lstAOI_INFO)
            {
                sb.AppendLine(item.GetContent());
            }

            sb.AppendLine(new SVI_INFO().GetHeader());
            foreach (var item in lstSVI_INFO)
            {
                sb.AppendLine(item.GetContent());
            }

            sb.AppendLine(new MFT_INFO().GetHeader());
            foreach (var item in lstMFT_INFO)
            {
                sb.AppendLine(item.GetContent());
            }

            sb.AppendLine(new LUMI_INFO().GetHeader());
            foreach (var item in lstLUMI_INFO)
            {
                sb.AppendLine(item.GetContent());
            }

            return sb.ToString();
        }

        /// <summary>
        /// 从文件转换为DataFile
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static PanelDataFile ConvertFromFile(string fileName)
        {
            PanelDataFile dataFile = new PanelDataFile();
            if (File.Exists(fileName))
            {
                string[] lines = File.ReadAllLines(fileName);

                string[] pnlInfoHeader = lines[0].Split(new char[] { '^' });
                string[] pnlInfoContent = lines[1].Split(new char[] { '^' });
                GetReflectionValues(dataFile.PNLINFO, pnlInfoHeader, pnlInfoContent);

                string strHeader = string.Empty;
                string strHeaderType = string.Empty;
                for (int i = 0; i < lines.Length; i++)
                {
                    string[] strs = lines[i].Split(new char[] { '^' });
                    if (!string.IsNullOrEmpty(strs[0]))
                    {
                        strHeader = lines[i];
                        strHeaderType = strs[0];
                        continue;
                    }

                    if (strHeaderType == "PG_INFO")
                    {
                        PG_INFO model = new PG_INFO();
                        GetReflectionValues(model, strHeader.Split(new char[] { '^' }), strs);
                        dataFile.lstPG_INFO.Add(model);
                    }
                    else if (strHeaderType == "AOI_INFO")
                    {
                        AOI_INFO model = new AOI_INFO();
                        GetReflectionValues(model, strHeader.Split(new char[] { '^' }), strs);
                        dataFile.lstAOI_INFO.Add(model);
                    }
                    else if (strHeaderType == "SVI_INFO")
                    {
                        SVI_INFO model = new SVI_INFO();
                        GetReflectionValues(model, strHeader.Split(new char[] { '^' }), strs);
                        dataFile.lstSVI_INFO.Add(model);
                    }
                    else if (strHeaderType == "MFT_INFO")
                    {
                        MFT_INFO model = new MFT_INFO();
                        GetReflectionValues(model, strHeader.Split(new char[] { '^' }), strs);
                        dataFile.lstMFT_INFO.Add(model);
                    }
                    else if (strHeaderType == "LUMI_INFO")
                    {
                        LUMI_INFO model = new LUMI_INFO();
                        GetReflectionValues(model, strHeader.Split(new char[] { '^' }), strs);
                        dataFile.lstLUMI_INFO.Add(model);
                    }
                }
            }
            return dataFile;
        }
    }
}
