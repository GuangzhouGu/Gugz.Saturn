using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace CIMBase
{
    public delegate void CimEventHandler(CIMEventArgs e);
    public delegate void CimErrorHandler(CIMErrorEventArgs e);
    public delegate void CIMCommandNotifyHandler(CIMCommandNotifyArgs e,EventWaitHandle waitHandle);
    public delegate void CIMUpStreamToDownStreamDataHandler(CIMDataArgs e);
    public delegate void CIMUpOrDownLinkStatus(CIMUpOrDownLinkStatusArgs e);                        //20180415增加的CIM上下游屏蔽交互功能
    /// <summary>
    /// 主要是否需要屏蔽与上下游的交互--用于单体测试
    /// </summary>
    public class CIMUpOrDownLinkStatusArgs
    {
        public int iUpOrDown { get; set; }
        public bool LinkeStatusFlag { get; set; }
    }
    public class CIMEventArgs
    {
        public string EventName { get; set; }

        public List<ushort[]> Data { get; set; }
    }
    public class CIMErrorEventArgs
    {
        public string EventName { get; set; }

        public string ErrorMessage { get; set; }
    }
    public class CIMCommandNotifyArgs
    {
        public string CommandName { get; set; }
        public ushort[] ComandData { get; set; }
        public bool IsHandlerConfiged { get; set; }
        /// <summary>
        ///CIM主动命令回复数据地址
        /// </summary>
        public int CommandReplyDataAddress { get; set; }
    }

    public class CIMDataArgs
    {
        public ushort[] Data { get; set; }
    }

    public enum LinkSignalType
    {
        ShipIn,         //入料
        ShipOut         //出料
    }
    public enum ScanerIndexType
    {
        A,
        B
    }
}
