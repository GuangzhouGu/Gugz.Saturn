using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CommonLib.UserCtrl
{
    public partial class StatusCtrl : UserControl
    {
        public StatusCtrl()
        {
            InitializeComponent();
        }
         /// <summary>
         /// 更新控件状态
         /// </summary>
         /// <param name="devName">空间名</param>
         /// <param name="sts">状态</param>
        public void UpdateDevStatus(string devName,bool sts)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string,bool>(UpdateDevStatus),devName,sts);
            }
            else
            {
                this.devStatusPanel1.UpdateDeviceState(devName,sts);
            }
        }
        /// <summary>
        /// 显示提示信息
        /// </summary>
        /// <param name="msg">文字消息</param>
        /// <param name="bh">文字提示颜色</param>
        public void ShowMsg(string msg,Brush bh)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string, Brush>(ShowMsg), msg, bh);
            }
            else
            {
                this.MsgBox.AddListBoxMsg(msg, bh);
            }
        }
    }
}
