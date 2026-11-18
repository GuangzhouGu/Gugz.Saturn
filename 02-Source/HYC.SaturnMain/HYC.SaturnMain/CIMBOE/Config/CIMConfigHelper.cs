using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using HYC.HTLog;
using System.Diagnostics;

namespace CIMBase
{
    /// <summary>
    /// 读取CIM配置信息的帮助类
    /// </summary>
    class CIMConfigHelper
    {
        /// <summary>
        /// 当前的配置信息
        /// </summary>
        private static CIMConfigEntity CurrentConfig { get; set; }
        /// <summary>
        /// 静态构造初始化一些东西
        /// </summary>
        static CIMConfigHelper()
        {
            string defaultCIMConfigPath = AppDomain.CurrentDomain.BaseDirectory + "\\Config\\"+"CIMConfigs.xml";
            CurrentConfig = LoadConfigFromFile(defaultCIMConfigPath);
        }
        /// <summary>
        /// 加载配置文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private static CIMConfigEntity LoadConfigFromFile(string fileName)
        {
            CIMConfigEntity result = new CIMConfigEntity();         //CIM的相关实例
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(fileName);

                var cclinkNode = doc.SelectSingleNode("/CIM/CCLinkCard");
                if (cclinkNode != null)
                {
                    CCLinkCardSetting cardSetting = new CCLinkCardSetting();
                    cardSetting.Channel = (short)GetPositiveValueFromXmlNode(cclinkNode.SelectSingleNode("Channel"));
                    cardSetting.Network = GetPositiveValueFromXmlNode(cclinkNode.SelectSingleNode("Network"));
                    cardSetting.Station = GetPositiveValueFromXmlNode(cclinkNode.SelectSingleNode("Station"));
                    result.CardSetting = cardSetting;
                }

                foreach (XmlNode node in doc.GetElementsByTagName("Operation"))
                {
                    try
                    {
                        CIMOperation cimop = new CIMOperation();

                        string strOpType = node.Attributes["Type"] == null ? "" : node.Attributes["Type"].Value;
                        CIMOperationType opType=CIMOperationType.Report;
                        if (Enum.TryParse(strOpType, out opType))
                        {
                            cimop.OperationType = opType; 
                        }
                        cimop.OperationName = node.Attributes["Name"]==null?"": node.Attributes["Name"].Value;
                        if (node.Attributes["Timeout"] != null)
                        {
                            int tempTimeOut = 0;
                            if (int.TryParse(node.Attributes["Timeout"].Value, out tempTimeOut))
                            {
                                cimop.TimeOut = tempTimeOut;

                            }
                        }
                       cimop.ReportNotifyAddress = ParseAddressFromConfigString(node.SelectSingleNode("ReportNotifyAddress"));
                       var dataAddressNodeList=  node.SelectNodes("ReportDataAddress");
                       if (dataAddressNodeList == null || dataAddressNodeList.Count == 0)
                       {
                           continue;
                       }

                       cimop.ReportDataAddress = new int[dataAddressNodeList.Count];
                       int currentIndex=0;
                       foreach(XmlNode  addressNode in dataAddressNodeList)
                       {
                           cimop.ReportDataAddress[currentIndex] = ParseAddressFromConfigString(addressNode);
                           currentIndex++;
                       }
                        cimop.ReplyAddress = ParseAddressFromConfigString(node.SelectSingleNode("ReplyAddress"));
                        //下面2个数据只有当是CIMOperationType.Request的时候才回去用
                        if (opType == CIMOperationType.Request)
                        {
                            var replyDataAddressNodes = node.SelectNodes("ReplyDataAddress");
                            var replyDataLengthNodes = node.SelectNodes("ReplyDataLength");
                            if (replyDataAddressNodes.Count != replyDataLengthNodes.Count)
                            {
                                continue;
                            }
                            cimop.ReplyDataAddress = new int[replyDataAddressNodes.Count];
                            cimop.ReplyDataLength = new int[replyDataAddressNodes.Count];
                            for (int i = 0; i < replyDataAddressNodes.Count; i++)
                            {
                                cimop.ReplyDataAddress[i] = ParseAddressFromConfigString(replyDataAddressNodes[i]);
                                cimop.ReplyDataLength[i] = ParseAddressFromConfigString(replyDataLengthNodes[i]);
                            }
                        }

                        result.Operations.Add(cimop);
                    }
                    catch (Exception ex)
                    {
                        LogManager.GetLogger("CIMConfigHelper").Info($"{new StackFrame(0).GetMethod().Name}执行异常，异常信息：{ex}");
                        continue;
                    }
                }

                foreach (XmlNode node in doc.GetElementsByTagName("Command"))
                {
                    try
                    {
                        CIMCommand cmd = new CIMCommand();
                        cmd.ACKAddress = ParseAddressFromConfigString(node.SelectSingleNode("ACKAddress"));
                        cmd.CommandDataAddress = ParseAddressFromConfigString(node.SelectSingleNode("CommandDataAddress"));
                        cmd.CommandDataLength= ParseAddressFromConfigString(node.SelectSingleNode("CommandDataLength"));
                        cmd.MonitorAddress = ParseAddressFromConfigString(node.SelectSingleNode("MonitorAddress"));
                        cmd.Name= node.Attributes["Name"] == null ? "" : node.Attributes["Name"].Value;

                        if (node.SelectSingleNode("ACKDataAddress") != null)
                        {
                            cmd.ACKDataAddress = ParseAddressFromConfigString(node.SelectSingleNode("ACKDataAddress"));
                        }
                        if (node.Attributes["MonitorInterval"] != null)
                        {
                            int tempMonitorInterval = 0;
                            if (int.TryParse(node.Attributes["MonitorInterval"].Value, out tempMonitorInterval))
                            {
                                cmd.MonitorInterval = tempMonitorInterval;
                            }
                        }
                        if (node.Attributes["Timeout"] != null)
                        {
                            int tempTimeOut = 0;
                            if (int.TryParse(node.Attributes["Timeout"].Value, out tempTimeOut))
                            {
                                cmd.Timeout = tempTimeOut;

                            }
                        }
                        var handlerNode = node.SelectSingleNode("Handler");
                        if (handlerNode != null)
                        {
                            try
                            {
                                var assemblyAttr = handlerNode.Attributes["AssemblyName"];
                                if (assemblyAttr != null)
                                {
                                    var assembly = System.Reflection.Assembly.Load(assemblyAttr.Value);
                                    cmd.Handler = (ICIMCommandHandler)assembly.CreateInstance(handlerNode.InnerText);
                                }
                            }
                            catch
                            {
                                cmd.Handler = null;
                            }
                        }
                        result.Commands.Add(cmd);
                    }
                    catch (Exception ex)
                    {
                        LogManager.GetLogger("CIMConfigHelper").Info($"{new StackFrame(0).GetMethod().Name}执行异常，异常信息：{ex}");
                        continue;
                    }
                }
            }
            catch(Exception ex)
            {
                LogManager.GetLogger("CIMConfigHelper").Info($"{new StackFrame(0).GetMethod().Name}执行异常，异常信息：{ex}");
                //Todo log some error message
            }
            return result;
        }

        /// <summary>
        /// 根据操作的名称找到操作的配置信息
        /// </summary>
        /// <param name="opName"></param>
        /// <returns></returns>
        public static CIMOperation FindOperationByName(string opName)
        {
            return CurrentConfig.Operations.Find(new Predicate<CIMOperation>((item) => { return item.OperationName.Trim() == opName.Trim(); }));
        }
        public static CCLinkCardSetting GetCardConfig()
        {
            return CurrentConfig.CardSetting;
        }

        public static List<CIMCommand> GetAllMonitorCommands()
        {
            return CurrentConfig.Commands;
        }

        /// <summary>
        /// 从配置文件中的地址字符串转化为Int类型的地址(一般用于16进制转出10进制)
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private static int ParseAddressFromConfigString(XmlNode node)
        {
            var address = node.InnerText.Trim();
            if (address.StartsWith("0x"))
            {
                return Convert.ToInt32(address, 16);
            }
            else
            {
                return Convert.ToInt32(address);
            }
        }
     
        /// <summary>
        /// 获取Node中的正数，当获取失败的时候返回-1
        /// </summary>
        /// <param name="node">xmlnode</param>
        /// <returns></returns>
        private static int GetPositiveValueFromXmlNode(XmlNode node)
        {
            if (node == null)
            {
                return -1;
            }
            int result = -1;
            if (int.TryParse(node.InnerText, out result))
            {
                if (result < 0)
                {
                    result = -1;
                }
            }
            return result;
        }
    }
}
