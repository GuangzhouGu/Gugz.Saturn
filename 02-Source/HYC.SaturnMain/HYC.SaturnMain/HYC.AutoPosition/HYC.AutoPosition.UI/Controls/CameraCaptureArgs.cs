using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYC.AutoPosition.UI.Controls
{
    /// <summary>
    /// 拍照事件参数类
    /// </summary>
    public class GrabEventArgs : EventArgs
    {
        /// <summary>
        /// 组的名称
        /// </summary>
        public string GroupName { get; set; }
    }
    /// <summary>
    /// 实时事件参数类
    /// </summary>
    public class LiveEventArgs : EventArgs
    {
        /// <summary>
        /// 组的名称
        /// </summary>
        public string GroupName { get; set; }
        /// <summary>
        /// 是否切换到Live模式
        /// </summary>
        public bool IsSetToLive { get; set; }
    }
}
