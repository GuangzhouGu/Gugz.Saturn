using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CIMBase.CCLink
{
    /// <summary>
    /// CIM的基础类-Helper
    /// </summary>
    public class CCLinkHelper
    {
        #region --变量--
        /// <summary>
        /// 当前正在运行的操作
        /// </summary>
        private static List<string> currentRunningOperation;

        /// <summary>
        /// 操作CCLink板卡的类
        /// </summary>
        public static CCLinkCard ccLink;
        /// <summary>
        /// 保存现有CIM Master的命令请求
        /// </summary>
        private static Dictionary<int, CIMCommandMonitor> commandMonitor;
        #endregion

        #region --事件--
        /// <summary>
        /// Log上报事件
        /// </summary>
        public static event Action<string> CIMBaseLogUpLoad;
        /// <summary>
        /// 上报至上一层类
        /// </summary>
        /// <param name="msg"></param>
        public static void OnLogUpLoad(string msg)
        {
            CIMBaseLogUpLoad?.Invoke(msg);
        }
        /// <summary>
        /// 发生错误的时候上报事情
        /// </summary>
        public static event CimErrorHandler Error;
        /// <summary>
        /// 主动向CIM服务器上报数据发送的时候上报事件
        /// </summary>
        public static event CimEventHandler ReportSend;
        /// <summary>
        /// 上报事件等待回复的时候发生超时的时候上报事件
        /// </summary>
        public static event CimEventHandler OperationWaitReplyTimeOut;
        /// <summary>
        /// 处理命令的时候等待Master回复发生超时的时候上报的事件
        /// </summary>
        public static event CimEventHandler CommandWaitReplyTimeOut;
        /// <summary>
        /// 当收到CIM Master的Command的时候出发
        /// </summary>
        public static event CIMCommandNotifyHandler CommandReceived;
        /// <summary>
        /// 上传至主界面：更新UporDown的Link状态位
        /// </summary>
        public static event CIMUpOrDownLinkStatus CimUpOrDownLinkStatu;
        /// <summary>
        /// 监控地址接受到数据
        /// </summary>
        /// <param name="args">数据体</param>
        /// <param name="waitHandle">事件</param>
        internal static void OnCommandReceived(CIMCommandNotifyArgs args, EventWaitHandle waitHandle)
        {
            if (CommandReceived != null)
            {
                CommandReceived(args, waitHandle);
            }
            waitHandle.Set();
        }
        /// <summary>
        /// 错误
        /// </summary>
        /// <param name="args"></param>
        internal static void OnError(CIMErrorEventArgs args)
        {
            if (Error != null)
            {
                Error(args);
            }
        }

        /// <summary>
        /// 等待应答超时
        /// </summary>
        /// <param name="args"></param>
        internal static void OnCommandWaitReplyTimeOut(CIMEventArgs args)
        {
            if (CommandWaitReplyTimeOut != null)
            {
                CommandWaitReplyTimeOut(args);
            }
        }
     
        #endregion

        #region --初始化--
        /// <summary>
        /// 静态构造初始化内部变量
        /// </summary>
        static CCLinkHelper()
        {
            currentRunningOperation = new List<string>();
            commandMonitor = new Dictionary<int, CIMCommandMonitor>();
            ccLink = new CCLinkCard();
        }

        /// <summary>
        /// CCLine连接初始化
        /// </summary>
        /// <returns></returns>
        public static bool Init()                                                     
        {
            try
            {
                OnLogUpLoad("CCLinkCard Init!");
                CCLinkCardSetting setting = CIMConfigHelper.GetCardConfig();                   //CIM的配置文件对象：端口信息、operator、command  
                var result = ccLink.Init(setting.Channel, 0, setting.Network, setting.Station);//打开CCLine板卡
                OnLogUpLoad("CCLinkCard Init Result:" + result);
                if (result ==0)
                {
                    UInit();//停止监控
                    foreach (CIMCommand command in CIMConfigHelper.GetAllMonitorCommands())
                    {
                        if (!commandMonitor.ContainsKey(command.MonitorAddress))                //命令的监控地址
                        {
                            CIMCommandMonitor monitor = new CIMCommandMonitor(command, ccLink); //每个命令单独设置监控
                            commandMonitor.Add(command.MonitorAddress, monitor);//没有对象再重新加载并监控
                            monitor.StartMonitor();//监控状态开启
                            OnLogUpLoad("CCLinkCard CIMCommandMonitor:" + monitor.MonitorAddress + " starts!");
                        }
                    }
                }
                else
                {
                    OnLogUpLoad("CCLinkCard Init Fail!");
                }
                return result == 0;
            }
            catch (Exception ex)
            {
                OnLogUpLoad($"CCLinkCard Init Exception，Exception Detail:{ex.Message}!");
                return false;
            }
        }
        /// <summary>
        /// 去初始化
        /// </summary>
        public static void UInit()
        {
            foreach (KeyValuePair<int, CIMCommandMonitor> kv in commandMonitor)
            {
                kv.Value.StopMonitor();
            }
            commandMonitor = new Dictionary<int, CIMCommandMonitor>();
        }
        #endregion

        #region --Report and Request--
        public static OperationResult Report(string operationName, ICIMData[] data)
        {
            List<ushort[]> realData = new List<ushort[]>();
            foreach (ICIMData cim in data)
            {
                if (cim == null)
                {
                    realData.Add(new ushort[1]);
                }
                else
                {
                    realData.Add(cim.ToArray());
                }
            }
            return Report(operationName, realData);
        }
        public static OperationResult Report(string operationName, ICIMData data)
        {
            return Report(operationName, data.ToArray());
        }
        public static OperationResult Report(string operationName, ushort data)
        {
            return Report(operationName, new ushort[] { data });
        }
        public static OperationResult Report(string operationName, ushort[] data)
        {
            return Report(operationName, new List<ushort[]> { data });
        }
        /// <summary>
        /// 上报数据
        /// </summary>
        /// <param name="operationName">CIMConfig中对应的操作名</param>
        /// <param name="data">上报的数据</param>
        /// <returns>上报结果</returns>
        public static OperationResult Report(string operationName, List<ushort[]> data)
        {
            OnLogUpLoad("Operation:" + operationName + " starts!");//向BOEmanage类中的LOG上传数据
            lock (currentRunningOperation)//锁住当前命令表
            {
                //if (currentRunningOperation.Contains(operationName))
                //{
                    //OnLogUpLoad("Operation:" + operationName + " is already in running!");
                    //OnLogUpLoad("Operation:" + operationName + " Result:" + OperationResult.Running.ToString());
                    //return OperationResult.Running;
                //}
                //else
                {
                    currentRunningOperation.Add(operationName);
                }
            }
            try
            {
                var opConfig = CIMConfigHelper.FindOperationByName(operationName);//向XML表查询相关CIM信息
                if (opConfig == null)
                {
                    OnLogUpLoad("Operation:" + operationName + " can not find config!");
                    OnLogUpLoad("Operation:" + operationName + " Result:" + OperationResult.NotConfiged.ToString());
                    return OperationResult.NotConfiged;
                }
                if (opConfig.ReportDataAddress.Length != data.Count)
                {
                    OnLogUpLoad("Operation:" + operationName + " data address length and data counter is not matched!");
                    //地址个数和传入的参数数据个数不同
                    OnLogUpLoad("Operation:" + operationName + " Result:" + OperationResult.ParameterCountNotMatched.ToString());
                    return OperationResult.ParameterCountNotMatched;
                }
                for (int i = 0; i < data.Count; i++)
                {
                    OnLogUpLoad("Operation:" + operationName + " write data.Address:" + opConfig.ReportDataAddress[i] + " Data:" + string.Join(",", data[i]));
                    //写数据
                    ccLink.WriteData(opConfig.ReportDataAddress[i], data[i]);
                }
                OnLogUpLoad("Operation:" + operationName + " SetBit:" + opConfig.ReportNotifyAddress);
                //设置标识位
                ccLink.SetBit(opConfig.ReportNotifyAddress);
                //上报事件
                if (ReportSend != null)//有发生者无调用者
                {
                    CIMEventArgs e = new CIMEventArgs();
                    e.EventName = operationName;
                    e.Data = data;
                    ReportSend(e);
                }
                //等待Ack
                bool result = false;
                int count = opConfig.TimeOut / 20;
                for (int i = 0; i < count; i++)
                {
                    Thread.Sleep(20);
                    bool flag = ccLink.ReadBit(opConfig.ReplyAddress);
                    if (flag)
                    {
                        result = true;
                        break;
                    }
                }
                OnLogUpLoad("Operation:" + operationName + " ResetBit:" + opConfig.ReportNotifyAddress);
                ccLink.ResetBit(opConfig.ReportNotifyAddress);
                if (!result)
                {
                    OnLogUpLoad("Operation:" + operationName + " Wait timeout Address:" + opConfig.ReplyAddress);
                    if (OperationWaitReplyTimeOut != null)
                    {
                        new TaskFactory().StartNew(new Action(() =>
                        {
                            OperationWaitReplyTimeOut(new CIMEventArgs { EventName = operationName });
                        }
                         ));
                    }
                    OnLogUpLoad("Operation:" + operationName + " Result:" + OperationResult.TimeOut.ToString());
                    return OperationResult.TimeOut;
                }
                OnLogUpLoad("Operation:" + operationName + " Result:" + OperationResult.OK.ToString());
                return OperationResult.OK;
            }
            catch (Exception ex)
            {
                if (Error != null)
                {
                    Error(new CIMErrorEventArgs { EventName = operationName, ErrorMessage = ex.Message });
                }
                OnLogUpLoad("Operation:" + operationName + " Exception:" + ex.Message);
                OnLogUpLoad("Operation:" + operationName + " Result:" + OperationResult.Error.ToString());
                return OperationResult.Error;
            }
            finally
            {
                currentRunningOperation.Remove(operationName);//命令表中移除该命令，表示该命令已经完成
            }
        }
        /// <summary>
        /// 根据Panle的信息向CIMPC获取Panel数据
        /// </summary>
        /// <param name="operationName">命令名称</param>
        /// <param name="data">数据</param>
        /// <param name="replyData">应答数据</param>
        /// <returns></returns>
        public static OperationResult Request(string operationName, ICIMData data, out List<ushort[]> replyData)
        {
            return Request(operationName, new List<ushort[]> { data.ToArray() }, out replyData);
        }
        /// <summary>
        /// 主动请求数据
        /// </summary>
        /// <param name="operationName">请求的操作名</param>
        /// <param name="parameterData">请求的数据</param>
        /// <param name="replyData">请求返回的数据</param>
        /// <returns></returns>
        public static OperationResult Request(string operationName, List<ushort[]> parameterData, out List<ushort[]> replyData)
        {
            OnLogUpLoad("Operation:" + operationName + " Request Start!");
            var reportResult = Report(operationName, parameterData);//这里只反馈数据交互是否正常，不关心数据
            replyData = new List<ushort[]>();
            if (reportResult == OperationResult.OK)//根据交互状态再处理数据
            {
                var opConfig = CIMConfigHelper.FindOperationByName(operationName);
                if (opConfig.ReplyDataAddress != null && opConfig.ReplyDataAddress.Length > 0) ;
                int resultDataLength = opConfig.ReplyDataAddress.Length;
                OnLogUpLoad("Operation:" + operationName + " ReplyDataAddress:" + resultDataLength);
                for (int i = 0; i < resultDataLength; i++)
                {
                    try
                    {
                        ushort[] tmpData = ccLink.ReadMultiData(opConfig.ReplyDataAddress[i], opConfig.ReplyDataLength[i]);
                        replyData.Add(tmpData);
                    }
                    catch (Exception ex)
                    {
                        OnLogUpLoad("Operation:" + operationName + " GetReplyData error:" + ex.Message);
                        replyData.Add(null);
                    }
                }
            }
            OnLogUpLoad("Operation:" + operationName + " Request result:" + reportResult.ToString());
            return reportResult;
        }
        public static ushort[] ReadMultiData(int addr,int length)
        {
            return ccLink.ReadMultiData(addr, length);
        }
        public static int  WirteMulutiData(int addr,ushort[] data)
        {
            return ccLink.WriteData(addr, data);
        }
        #endregion
    }
    /// <summary>
    /// report or rquest的操作格式
    /// </summary>
    public enum OperationResult
    {
        OK,
        Running,
        NotConfiged,
        Error,
        TimeOut,
        ParameterCountNotMatched,
    }
}
