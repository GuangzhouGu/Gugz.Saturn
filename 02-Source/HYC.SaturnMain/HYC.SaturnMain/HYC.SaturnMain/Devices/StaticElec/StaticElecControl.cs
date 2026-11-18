/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2022 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnMain.Devices.StaticElec
// 文件名称    ：StaticElecControl
// =================================
// 创 建 者    ：guguangzhou
// 创建日期    ：
// 功能描述    ：
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

using HYC.HTCommunication.SerialPortWrapper;
using HYC.HTPCBase.Extension.Entities;
using HYC.SaturnMain.Devices.EleStaticMes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Ports;
using System.Text;
using System.Threading;

namespace HYC.SaturnMain.Devices.StaticElec
{
    public class StaticElecControl
    {
        public event Action<string,string, Brush> OnShowMsg; //显示通信信息记录
        public event Action<string, int, bool> OnConnected;
        public bool IsConnected { get; set; }
        public StaticElecInfo Info = null;
        HTCOMMSerialPort Port = null;
        
        ManualResetEvent Mre = new ManualResetEvent(false);
        string Msg = string.Empty;
        public StaticElecControl(StaticElecInfo info)
        {
            Info = info;
        }
        public void Connect()
        {
            try
            {
                HTSerialPortInfo _info = new HTSerialPortInfo { PortName = Info.PortName, BaudRate = Info.BaudRate, DataBits = 8, StopBits = StopBits.One, Parity = Parity.None };
                Port = new HTCOMMSerialPort();
                Port.DataReceived += DataReceivedHandle;
                bool ret = Port.Init(_info);
                OnConnected?.Invoke($"StaticElec{(Info.Num + 1).ToString("00")}", Info.Num, ret);
            }
            catch (Exception)
            {
                OnConnected?.Invoke($"StaticElec{(Info.Num + 1).ToString("00")}", Info.Num, false);
            }
        }
        private void DataReceivedHandle(object sender, SerialPortEventArgs e)
        {
            try
            {
                string msg = Encoding.Default.GetString(e.Datas);
                string flag = msg.Substring(0, 2);
                if (flag == "M0")
                {
                    Mre.Set();
                    Msg = msg.Substring(7, 5);
                    OnShowMsg?.Invoke("StaticElec", $"获取测量值：{Msg}", Brushes.Blue);
                }
                else if (flag == "ER")
                {
                    int errorCode = int.Parse(msg.Substring(6, 2));
                    OnShowMsg?.Invoke("StaticElec", EnumHelper.GetEnumDesc((ErrorEnum)errorCode), Brushes.Red);
                }

            }
            catch (Exception ex)
            {

            }
        }
        public double? GetValue()
        {
            double? value = null;
            string cmd = $"M0\r\n";
            Send(cmd);
            Mre.Reset();
            bool ret = Mre.WaitOne(1000);
            if (ret)
            {
                value = Math.Round(double.Parse(Msg), 3);
            }
            return value;
        }
        private void Send(string msg)
        {
            Msg = string.Empty;
            if (Port.IsOpen)
            {
                Port.Send(msg);
                OnShowMsg?.Invoke("StaticElec", $"发送指令：{msg}", Brushes.Green);
            }
            else
            {
                OnShowMsg?.Invoke("StaticElec", "串口未打开，重新打开串口", Brushes.Red);
                Port.Open();
            }

        }
        public bool Uninit()
        {
            Port.Dispose();
            Port.Close();
            return true;
        }
    }
}
