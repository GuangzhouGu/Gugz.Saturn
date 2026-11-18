using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonDB.Model
{
    public class HeartbeatModel
    {
        /// <summary>
        /// 外设名称
        /// </summary>
        public string DeviceName { get; set; }
        /// <summary>
        /// 外设IP
        /// </summary>
        public string DeviceIP { get; set; }
        /// <summary>
        /// 外设端口
        /// </summary>
        public string DevicePort { get; set; }
        /// <summary>
        /// 连接时间
        /// </summary>
        public DateTime ConnectTime { get; set; }
        /// <summary>
        /// 断开时间
        /// </summary>
        public DateTime? DisconnectTime { get; set; }
        /// <summary>
        /// 连接状态，0：离线，1：在线
        /// </summary>
        public int ConnectStatus { get; set; }
    }
}
