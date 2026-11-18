/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2023 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnAoi.Devices.MultiAngle
// 文件名称    ：MultiAngleControl
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
using HYC.HTLog;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HYC.SaturnAoi.Devices.MultiAngle
{
    public class MultiAngleControl
    {
        //连接状态事件
        public event Action<string, int, bool> OnConnected;
        //消息显示事件
        public event Action<string, string, Brush> OnShowMsg;

        MultiAngleInfo Info = null;
        HTCOMMSerialPort Port = null;
        ManualResetEvent Mre = new ManualResetEvent(false);
        MultiAngleValue MultiAngleValue = null;
        string RevMsg = string.Empty;
        string CurCmd = string.Empty;
        public MultiAngleControl(MultiAngleInfo info)
        {
            Info = info;
        }
        public void Connect()
        {
            try
            {
                var serialPortInfo = new HTSerialPortInfo { PortName = Info.PortName, BaudRate = Info.BaudRate, DataBits = 8, StopBits = StopBits.One, Parity = Parity.None };
                Port = new HTCOMMSerialPort();
                Port.DataReceived += DataReceivedHandle;
                bool ret = Port.Init(serialPortInfo);
                OnConnected?.Invoke(Info.MultiAngleName, Info.MultiAngleNum, ret);
            }
            catch (Exception ex)
            {
                OnConnected?.Invoke(Info.MultiAngleName, Info.MultiAngleNum, false);
                OnShowMsg?.Invoke(Info.MultiAngleName,$"MultiAngleControl->Connect方法执行异常，异常信息：{ex}",Brushes.Red);
            }
        }
        private void DataReceivedHandle(object sender, SerialPortEventArgs e)
        {
            string msg = Encoding.Default.GetString(e.Datas);
            RevMsg = msg;
            if (string.IsNullOrEmpty(msg) || msg == ">>\r\n")
            {
                OnShowMsg?.Invoke(Info.MultiAngleName, $"MultiAngleControl->Received Datas为空", Brushes.Red);
                return;
            }
            string[] str = msg.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (str == null || str.Length <1)
            {
                OnShowMsg?.Invoke(Info.MultiAngleName, $"MultiAngleControl->Received Datas长度异常，Length:{str.Length }", Brushes.Red);
                return;
            }
            else
            {
                try
                {
                    switch (CurCmd)
                    {
                        case ":meas:Yuv":
                            {
                                MultiAngleValue = new MultiAngleValue();
                                for (int i = 0; i < 6; i++)
                                {
                                    MultiAngleValue.Lumi[i] = Convert.ToDouble(str[i * 8]);
                                    MultiAngleValue.X[i] = Convert.ToDouble(str[i * 8 + 1]);
                                    MultiAngleValue.Y[i] = Convert.ToDouble(str[i * 8 + 2]);
                                    MultiAngleValue.U[i] = Convert.ToDouble(str[i * 8 + 3]);
                                    MultiAngleValue.V[i] = Convert.ToDouble(str[i * 8 + 4]);
                                    MultiAngleValue.Cct[i] = Convert.ToDouble(str[i * 8 + 5]);
                                    MultiAngleValue.Exposure[i] = Convert.ToDouble(str[i * 8 + 6]); // 是否过曝
                                    MultiAngleValue.Noise[i] = Convert.ToDouble(str[i * 8 + 7]);
                                }
                                break;
                            }
                        default:
                            break;
                    }
                 
                }
                catch (Exception ex)
                {
                    MultiAngleValue = null;
                    OnShowMsg?.Invoke(Info.MultiAngleName, $"MultiAngleControl->DataReceivedHandle方法执行异常，异常信息：{ex}", Brushes.Red);
                }
                finally
                {
                  
                }
            }
            Mre.Set();
        }
        /// <summary>
        /// 拍照点检
        /// </summary>
        /// <returns></returns>
        public MultiAngleValue Grab(int timeOut=3*1000)
        {
            try
            {
                CurCmd = ":meas:Yuv";
                MultiAngleValue = null;
                Mre.Reset();
                Port.Send(Encoding.ASCII.GetBytes(":meas:Yuv" + '\n'));
                if (Mre.WaitOne(timeOut))
                {
                    return MultiAngleValue;
                }
                else
                {
                    OnShowMsg?.Invoke(Info.MultiAngleName, $"MultiAngleControl->GrabCheckTest方法超时", Brushes.Red);
                }
                return null;
            }
            catch (Exception ex)
            {
                OnShowMsg?.Invoke(Info.MultiAngleName, $"MultiAngleControl->GrabCheckTest方法执行异常，异常信息：{ex}", Brushes.Red);
                return null;
            }
        }
        public string GetAngleSelect(int timeOut = 3 * 1000)
        {
            CurCmd = ":ANGLE:SELECTED?";
            RevMsg = string.Empty;
            Mre.Reset();
            Port.Send(Encoding.ASCII.GetBytes(":ANGLE:SELECTED?" + '\n'));
            if (Mre.WaitOne(timeOut))
            {
                return RevMsg;
            }
            else
            {
                OnShowMsg?.Invoke(Info.MultiAngleName, $"MultiAngleControl->GetAngleSelect方法超时", Brushes.Red);
                return null;
            }
        }
  
        /// <summary>
        /// 关闭串口连接
        /// </summary>
        public bool Unint()
        {
            bool ret = Port.Close();
            return ret;
        }
    }
    public class MultiAngleValue
    {
        public double[] Lumi { get; set; }
        public double[] X { get; set; }
        public double[] Y { get; set; }
        public double[] U { get; set; }
        public double[] V { get; set; }
        public double[] Cct { get; set; }
        public double[] Exposure { get; set; }
        public double[] Noise { get; set; }
        public MultiAngleValue()
        {
            Lumi = new double[6];
            X = new double[6];
            Y = new double[6];
            U = new double[6];
            V = new double[6];
            Cct = new double[6];
            Exposure = new double[6];
            Noise = new double[6];
        }
    }
}
