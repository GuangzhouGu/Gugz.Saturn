/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2021 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnMain.Devices.Laser
// 文件名称    ：LaserHelper.cs
// =================================
// 创 建 者    ：guguangzhou
// 创建日期    ：2022/8/19 09:00:00 
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
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HYC.HTCommunication.SerialPortWrapper;
using HYC.HTFile.HTXML;
using CommonLib.Entities;
using HYC.HTPCBase.Extension.Device;
using HYC.HTPCBase.Extension.Entities;

namespace HYC.SaturnMain.Devices.Laser
{
    /// <summary>
    /// 激光位移传感器帮助类
    /// </summary>
    public class LaserHelper : IDevice, IDisposable
    {
        public event Action<string, int, bool> OnConnected;  //设备连接/断开消息
        public event Action<string, string, Brush> OnShowMsg;
        public static LaserHelper Instance { get { return DeviceManager.Instance.Get("LaserHelper") as LaserHelper; } set { } }
        HTCOMMSerialPort Port = null;
        private ManualResetEvent Mre = new ManualResetEvent(false);
        private string Msg = string.Empty;
        public int Index = 0;
        private string LaserConfigPath = AppDomain.CurrentDomain.BaseDirectory + @"\Config\HardWare\LaserInfo.xml";

        public string DeviceName { get; set; } = "LaserHelper";
        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    var info = GetInfo();
                    if (info != null && info.Count > 0)
                    {
                        HTSerialPortInfo _info = new HTSerialPortInfo { PortName = info[0].PortName, BaudRate = info[0].BaudRate, DataBits = 8, StopBits = StopBits.One, Parity = Parity.None };
                        Port = new HTCOMMSerialPort();
                        Port.DataReceived += DataReceivedHandle;
                        bool ret = Port.Init(_info);
                        OnConnected?.Invoke("Laser", Index, ret);
                    }
                    else
                    {
                        OnShowMsg?.Invoke("Laser", "配置文件为空", Brushes.Red);
                        OnConnected?.Invoke("Laser", Index, false);
                    }
                }
                catch (Exception ex)
                {
                    OnShowMsg?.Invoke("Laser", ex.Message, Brushes.Red);
                    OnConnected?.Invoke("Laser", Index, false);
                }
            });
        }
        /// <summary>
        /// 获取信息
        /// </summary>
        /// <returns></returns>
        private List<LaserInfo> GetInfo()
        {
            var result = HTXMLSerializer.EntityFromXMLFile<List<LaserInfo>>(LaserConfigPath);
            return result;
        }
        /// <summary>
        /// 打开镭射器
        /// </summary>
        public void Open()
        {
            if (Port.IsOpen == false)
            {
                Port.Open();
                OnConnected?.Invoke("Laser", Index, true);
            }
        }
        /// <summary>
        /// 关闭镭射器
        /// </summary>
        public void Close()
        {
            Port.Close();
            OnConnected?.Invoke("Laser", Index, false);
        }
        private void DataReceivedHandle(object sender, SerialPortEventArgs e)
        {
            string msg = Encoding.Default.GetString(e.Datas);
            string flag = msg.Substring(3, 1);
            string cmd = msg.Substring(4, 3);

            if (flag == "$")
            {
                Mre.Set();
                switch (cmd)
                {
                    case "RMD":
                        {
                            Msg = msg.Substring(7, 11);
                            OnShowMsg?.Invoke("Laser", $"获取测量值：{Msg}", Brushes.Blue);
                            break;
                        }
                    case "WZS":
                        {
                            OnShowMsg?.Invoke("Laser", $"清零指令发送完成", Brushes.Blue);
                            break;
                        }
                }
            }
            else
            {
                int errorCode = int.Parse(msg.Substring(4, 2));
                OnShowMsg?.Invoke("Laser", "指令:" + cmd + EnumHelper.GetEnumDesc((ErrorEnum)errorCode), Brushes.Red);
            }

        }

        public double? GetValue(int channel = 0)
        {
            double? value = null;
            int chn = channel == 0 ? 3 : 4;
            string cmd = $"%EE#RMD{chn}**\r";
            Send(cmd);
            Mre.Reset();
            bool ret = Mre.WaitOne(1000);
            if (ret)
            {
                value = Math.Round(double.Parse(Msg), 3);
            }
            return value;
        }

        public bool Zero(int channel = 0)
        {
            int chn = channel == 0 ? 3 : 4;
            string cmd = $"%EE#WZS{chn}00001**\r";
            Send(cmd);
            Mre.Reset();
            bool ret = Mre.WaitOne(1000);
            return ret;
        }

        public void UnInit()
        {
            Port.Close();
            Port.Dispose();
        }
        private void Send(string msg)
        {
            Msg = string.Empty;
            if (Port.IsOpen)
            {
                Port.Send(msg);
                OnShowMsg?.Invoke("Laser", $"发送指令：{msg}", Brushes.Green);
            }
            else
            {
                OnShowMsg?.Invoke("Laser", "串口未打开，重新打开串口", Brushes.Red);
                Port.Open();
            }

        }

        public void Uninit()
        {
            Port.Close();
        }

        public void Dispose()
        {
            Port.Dispose();
        }

        public void InitRet(bool ret)
        {
        }
    }
    public enum ErrorEnum
    {
        格式错误 = 01,
        指令信号错误 = 02,
        指令信号代码错误 = 03,
        代码部错误 = 04,
        数据错误 = 05,
        设定错误 = 07,
        BCC错误 = 08,
        控制流错误 = 10,
        通讯错误 = 11,
        执行错误 = 20,

    }
}
