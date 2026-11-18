using CIMBase.CCLink;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CIMBase
{
    class CIMCommandMonitor
    {
        /// <summary>
        /// 监控CIM 主发的Request，比如时间同步、OPCALL等
        /// </summary>
        /// <param name="cmd">CIM主动命令</param>
        /// <param name="card">CIM板卡信息</param>
        public CIMCommandMonitor(CIMCommand cmd, CCLinkCard card)
        {
            cclink = card;
            CIMCommand relatedCmd = cmd;
            this.MonitorInterval = cmd.MonitorInterval;
            this.MonitorAddress = cmd.MonitorAddress;
            MonitorBeats = new System.Timers.Timer();
            MonitorBeats.Interval = this.MonitorInterval;
            Thread t = new Thread(new ParameterizedThreadStart((a) =>
                 {
                     while (IsMonitorRunning)
                     {
                         //处理监控相关
                         bool flag = cclink.ReadBit(cmd.MonitorAddress);
                         if (flag)
                         {
                             IsInProcessing = true;
                             MonitorBeats.Stop();                       //计时器暂停
                             try
                             {
                                 ushort[] datas = null;
                                 if (cmd.CommandDataAddress >= 0)
                                 {
                                     datas = cclink.ReadMultiData(cmd.CommandDataAddress, cmd.CommandDataLength);
                                 }
                                 if (IsMonitorRunning)
                                 {
                                     new TaskFactory().StartNew(() =>
                                     {
                                         ManualResetEvent eventHandle = new ManualResetEvent(false);
                                         eventHandle.Reset();
                                         //触发事件
                                         CCLinkHelper.OnCommandReceived(new CIMCommandNotifyArgs { ComandData = datas, CommandName = relatedCmd.Name, IsHandlerConfiged = (relatedCmd.Handler != null), CommandReplyDataAddress = relatedCmd.ACKDataAddress }, eventHandle);
                                         if (relatedCmd.Handler != null)
                                         {
                                             relatedCmd.Handler.HandleData(datas);
                                         }
                                         if (!eventHandle.WaitOne(relatedCmd.Timeout))          //等待接受到数据给CIMBoeHelp的等待超时
                                         {
                                             CCLinkHelper.OnCommandWaitReplyTimeOut(new CIMEventArgs
                                             {
                                                 EventName = cmd.Name
                                             });
                                             return;
                                         }
                                     });
                                     //返回ACK to Master
                                     cclink.SetBit(relatedCmd.ACKAddress);
                                     int count = relatedCmd.Timeout / 20; //StaticInfo.TimeoutT2 / 20;
                                     int i = 0;
                                     for (i = 0; i < count; i++)
                                     {
                                         Thread.Sleep(20);
                                         flag = cclink.ReadBit(cmd.MonitorAddress);
                                         if (flag == false)
                                         {
                                             break;
                                         }
                                     }
                                     if (i == count)
                                     {
                                         //说明超时了上报事件
                                         CCLinkHelper.OnCommandWaitReplyTimeOut(new CIMEventArgs
                                         {
                                             EventName = cmd.Name
                                         });
                                     }
                                     cclink.ResetBit(relatedCmd.ACKAddress);
                                     Thread.Sleep(1000);
                                 }
                             }
                             catch
                             { }
                         }
                         Thread.Sleep(300);
                     }
                 }));
            t.IsBackground = true;
            t.Start();
            IsInProcessing = false;
        }

        private CCLinkCard cclink;
        public int MonitorInterval { get; private set; }
        public int MonitorAddress { get; private set; }
        private System.Timers.Timer MonitorBeats { get; set; }

        private bool IsInProcessing { get; set; }
        private bool IsMonitorRunning { get; set; }
        public void StartMonitor()
        {
            if (IsInProcessing)
            {
                return;
            }
            IsMonitorRunning = true;
        }
        public void StopMonitor()
        {
            IsMonitorRunning = false;
        }
    }
}
