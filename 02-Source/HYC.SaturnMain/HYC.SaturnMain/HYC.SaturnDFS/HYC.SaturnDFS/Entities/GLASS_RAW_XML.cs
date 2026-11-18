using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace HYC.SaturnDFS.Entities
{
    [XmlRoot("GLASS")]
    public class GLASS_RAW_XML
    {

        public GLASS_RAW_XML()
        {
            GLASS_INF = new GLASS_INF_RAW_XML();
            PNL_INF = new List<PANEL_RAW_XML>();
        }
        //private GLASS_RAW_XML gLASS_RAW_XML;
        //public GLASS_RAW_XML GetInstance()
        //{
        //    if (gLASS_RAW_XML==null)
        //    {
        //        gLASS_RAW_XML = new GLASS_RAW_XML();
        //    }
        //    return gLASS_RAW_XML;
        //}
        public GLASS_INF_RAW_XML GLASS_INF { get; set; }
        public List<PANEL_RAW_XML> PNL_INF { get; set; }

        /// <summary>
        /// 实体转XML
        /// </summary>
        /// <returns></returns>
        public string EntityToString()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("", "");
                XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
                xmlWriterSettings.Indent = true;//要求缩进
                xmlWriterSettings.OmitXmlDeclaration = true;
                using (XmlWriter xmlWriter = XmlWriter.Create(ms, xmlWriterSettings))
                {
                    xmlWriter.WriteStartDocument();
                    #region GLASS
                    xmlWriter.WriteStartElement("GLASS");
                    #region GLASS_INF
                    xmlWriter.WriteStartElement("GLASS_INF");
                    Type gi = this.GLASS_INF.GetType();
                    foreach (System.Reflection.PropertyInfo p in gi.GetProperties())
                    {
                        xmlWriter.WriteStartElement(p.Name);
                        xmlWriter.WriteString((string)p.GetValue(this.GLASS_INF));
                        xmlWriter.WriteEndElement();
                    }

                    xmlWriter.WriteEndElement();//结束GLASS_INF节点
                    #endregion
                    #region PNL_INF
                    xmlWriter.WriteStartElement("PNL_INF");
                    for (int i = 0; i < this.PNL_INF.Count; i++)
                    {
                        xmlWriter.WriteStartElement("PANEL");
                        xmlWriter.WriteStartElement("PANEL_ID");
                        xmlWriter.WriteString(this.PNL_INF[i].PANEL_ID);
                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteStartElement("ITME");
                        xmlWriter.WriteString(this.PNL_INF[i].ITME);
                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteStartElement("INDEX");
                        xmlWriter.WriteString(this.PNL_INF[i].INDEX);
                        xmlWriter.WriteEndElement();
                        for (int j = 0; j < this.PNL_INF[i].RAWs.Count; j++)
                        {
                            xmlWriter.WriteStartElement($"RAW_{j + 1}");
                            xmlWriter.WriteString(this.PNL_INF[i].RAWs[j]);
                            xmlWriter.WriteEndElement();
                        }
                        xmlWriter.WriteEndElement();
                    }
                    xmlWriter.WriteEndElement();
                    #endregion
                    #endregion
                    xmlWriter.WriteEndDocument();
                }
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }
    }
    [XmlRoot("GLASS_INF")]
    public class GLASS_INF_RAW_XML
    {
        public string LOT_ID { get; set; } = "***";
        public string STEP_ID { get; set; } = "***";
        public string OPERATOR_ID { get; set; } = "***";
        public string SLOT_ID { get; set; } = "***";
        public string PRODUCT_ID { get; set; } = "***";
        public string RECIPE_NAME { get; set; } = "***";
        public string EQUIPMENT_ID { get; set; } = "***";
        public string START_TIME { get; set; } = "***";
        public string PROGRAM { get; set; } = "***";
        public string GLS_ID { get; set; } = "***";
    }
    [XmlType("PANEL")]
    public class PANEL_RAW_XML
    {

        public string PANEL_ID { get; set; } = "***";
        public string ITME { get; set; } = "***";
        public string INDEX { get; set; } = "***";
        public List<string> RAWs { get; set; }
    }
}
