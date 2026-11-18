/*----------------------------------------------------------------
// Copyright (C)  Suzhou HYC Technology Co.,LTD
// 版权所有。
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：E2000_Main.Devices.Scaner
// 文件名称    ：ScanerControl.cs
// =================================
// 创 建 者    ：guguangzhou
// 创建日期    ：2021/11/10 09:00:00 
// 功能描述    ：扫码枪接口
// 使用说明    ：
//
//
// 创建标识：
//
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;

using HYC.HTCommunication.SocketWrapper;
using System.Linq;
using HYC.HTLog;
using BOECIM.Devices.CIM;

namespace HYC.SaturnMain.Devices.Scaner
{
    public class ScanerControl
    {
        public event Action<string,string, Brush> OnShowMsg; //显示通信信息记录
        public event Action<string,int, bool> OnConnected;
        public bool IsConnected { get; set; }
        HTCOMMSocketClient Socket = new HTCOMMSocketClient();
        public ScanerInfo Info = null;
        ManualResetEvent Mre = new ManualResetEvent(false);
        string Msg = string.Empty;

        public ScanerControl(ScanerInfo info)
        {
            Info = info;
            Socket.ClientConnected += new EventHandler<SocketEventArgs>(ConnectedHandle);//server连接状态显示
            Socket.ClientDisconnected += new EventHandler<SocketEventArgs>(DisConnectedHandle);//server连接状态显示
            Socket.DataReceived += new EventHandler<SocketEventArgs>(ReceiveHandle);
        }
        /// <summary>
        /// 连接
        /// </summary>
        public void Connect()
        {
            var t = new Thread(() => {
                while (true)
                {
                    if (!IsConnected)
                    {
                        try
                        {
                            Socket.Connect(Info.IP, Info.Port);
                        }
                        catch (Exception ex)
                        {
                            OnConnected?.Invoke($"Scaner{(Info.ScanerNum + 1).ToString("00")}", Info.ScanerNum, false);
                        }
                    }
                    //else
                    //{
                    //    Heartbeat();
                    //}
                    Thread.Sleep(3000);
                }
            });
            t.IsBackground = true;
            t.Start();
        }
        private void ConnectedHandle(object sender, SocketEventArgs e)
        {
            IsConnected = true;
            OnConnected?.Invoke($"Scaner{(Info.ScanerNum+1).ToString("00")}",Info.ScanerNum, true);
           // CimManager.Instance.VCRStatusChangeReport1((ushort)Info.ScanerNum,true);
        }
        private void DisConnectedHandle(object sender, SocketEventArgs e)
        {
            IsConnected = false;
            OnConnected?.Invoke($"Scaner{(Info.ScanerNum + 1).ToString("00")}",Info.ScanerNum, false);
            //CimManager.Instance.VCRStatusChangeReport1((ushort)Info.ScanerNum, false);
        }

        public void ReceiveHandle(object sender, SocketEventArgs e)
        {
            try
            {
                string temp = Encoding.Default.GetString(e.Datas);
                LogManager.GetLogger("Scaner").Info($"接收到的扫码枪数据：{temp.Replace("\r\n", "")}");
                Msg = Encoding.Default.GetString(e.Datas).Replace("\r\n", "");
                Mre.Set();
            }
            catch (Exception ex)
            {
                OnShowMsg($"Scaner",$"ReceiveHandle执行异常，异常信息：{ex.Message}",Brushes.Red);
            }
        }
        public string Scancode()
        {
            string cmd = "+";
            string result = string.Empty;
            byte[] datas = Encoding.Default.GetBytes(cmd);
            Socket.Send(datas);
            Mre.Reset();
            bool ret = Mre.WaitOne(3000);
            if (ret)
            {
                if (Msg.Contains("Null") == false)
                {
                    OnShowMsg($"Scaner", $"扫码完成，条码：{Msg}", Brushes.Blue);
                    result= Msg;
                }
                else
                {
                    OnShowMsg($"Scaner", $"扫码失败，没有读取到条码", Brushes.Red);
                    result= "NoRead";
                }
            }
            else
            {
                OnShowMsg($"Scaner", $"扫码超时", Brushes.Red);
                result= "NoRead";
            }
            return result;
        }
   
        /// <summary>
        /// 断开
        /// </summary>
        public bool Uninit()
        {
            Socket.Disconnect();
            Socket.Dispose();
            return true;
        }
    }
}
