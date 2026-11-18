using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIMBase
{
    /// <summary>
    /// CIM 配置文件对应的实体类
    /// </summary>
    class CIMConfigEntity
    {
        public CIMConfigEntity()
        {
            CardSetting = new CCLinkCardSetting();
            Operations = new List<CIMOperation>();
            Commands = new List<CIMCommand>();
        }
        public CCLinkCardSetting CardSetting { get; set; }
        public List<CIMOperation> Operations { get; set; }
        public List<CIMCommand> Commands { get; set; }
    }

    public  enum CIMOperationType
    { 
       Report,
       Request
    }
    /// <summary>
    /// CIM操作
    /// </summary>
    class CIMOperation
    {
        /// <summary>
        /// 操作名
        /// </summary>
        public string OperationName { get; set; }
        /// <summary>
        /// 操作类型（分为Report和Request）
        /// </summary>
        public CIMOperationType OperationType { get; set; }
        /// <summary>
        /// 事件通知地址
        /// </summary>
        public int ReportNotifyAddress { get; set; }
        /// <summary>
        /// 事件数据
        /// </summary>
        public int[] ReportDataAddress { get; set; }
        /// <summary>
        /// 事件回复地址
        /// </summary>
        public int ReplyAddress { get; set; }

        /// <summary>
        /// 回复的数据地址列表(只有当时request的时候才有用)
        /// </summary>
        public int[] ReplyDataAddress { get; set; }
        /// <summary>
        /// 回复的数据长度(只有当时request的时候才有用)
        /// </summary>
        public int[] ReplyDataLength { get; set; }


        private int timeOut = 4000;
        public int TimeOut {
            get
            {
                return timeOut;
            }
            set
            {
                if (value > 0)
                {
                    timeOut = value;
                }
            }
        }
    }

    /// <summary>
    /// CIM命令的配置
    /// </summary>
    class CIMCommand
    {
        public string Name { get; set; }

        public int MonitorAddress { get; set; }

        public int CommandDataAddress { get; set; }

        public int CommandDataLength { get; set; }

        public int ACKAddress { get; set; }

        public int ACKDataAddress { get; set; }//需要写入数据

        public ICIMCommandHandler Handler { get; set; }

        private int monitorInterval = 20;
        /// <summary>
        /// 监控定时
        /// </summary>
        public int MonitorInterval
        {
            get
            {
                return monitorInterval;
            }
            set
            {
                if (value >= 20)
                {
                    monitorInterval = value;
                }
            }
        }

        private int timeout = 2000;
        public int Timeout
        {
            get { return timeout; }
            set
            {
                if (value > 1000)
                {
                    timeout = value;
                }
            }
        }
    }

    /// <summary>
    /// CCLinkCard的配置信息
    /// </summary>
    class CCLinkCardSetting
    {
        private short channel = 151;
        public short Channel
        {
            get { return channel; }
            set {
                if (value > 0)
                {
                    channel = value;
                }
            }
        }
        private int network = 0;
        public int Network {
            get { return network; }
            set {
                if (value > 0)
                {
                    network = value;
                }
            }
         }
        private int station = 255;
        public int Station {
            get { return station; }
            set {
                if (value > 0)
                {
                    station = value;
                }
            }
         }
    }


}
