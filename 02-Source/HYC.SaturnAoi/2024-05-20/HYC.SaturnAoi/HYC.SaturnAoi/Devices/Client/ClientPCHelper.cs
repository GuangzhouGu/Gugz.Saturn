/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2020 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnAoi.Devices.Motion
// 文件名称    ：ClientPCHelper.cs
// =================================
// 创 建 者    ：guguangzhou
// 创建日期    ：2021/01/29 09:00:00 
// 功能描述    ：客户端功能类
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
using CommonLib.Device;
using CommonLib.Entities;
using HYC.HTCommunication.SocketWrapper;
using HYC.HTFile;
using HYC.HTLog;
using HYC.SaturnAoi.Devices.Client;
using HYC.SaturnAoi.Entities;
using HYC.SaturnAoi.Params;
using HYC.SaturnDB;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HYC.SaturnAoi.Devices.ClientPC
{
    /// <summary>
    /// 与EQPC通讯，做客户端
    /// </summary>
    public class ClientHelper : IDevice
    {
        public event Action<string, int, bool> OnConnected;
        public event Action<string, string, Brush> OnShowMsg;

        public static ClientHelper Instance { get { return DeviceManager.Instance.Get("Client") as ClientHelper; } }
        public bool IsInit { get; set; } = true;
        public string DeviceName { get; set; } = "Client";

        string ClientInfoConfigPath = AppDomain.CurrentDomain.BaseDirectory + @"\Config\Hardware\ClientInfo.xml";
        DateTime LastHeartBeatTime;
        bool IsConnected = false;
        HTCOMMSocketClient Socket = new HTCOMMSocketClient();
        ClientInfo Info = null;
        public void Init()
        {
            try
            {
                Info = GetClientInfoConfig();
                Socket.ClientConnected += new EventHandler<SocketEventArgs>(ServerConnectedHandle);//server连接状态显示
                Socket.ClientDisconnected += new EventHandler<SocketEventArgs>(ServerDisConnectedHandle);//server连接状态显示
                Socket.DataReceived += new EventHandler<SocketEventArgs>(ReceiveMessageHandle);
                Connect();
            }
            catch (Exception ex)
            {
                OnConnected?.Invoke(Info.MasterName, 0, false);
                OnShowMsg?.Invoke(DeviceName, $"ClientHelper->Init方法执行异常，异常信息：{ex.ToString()}", Brushes.Red);
            }
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
                            Socket.Connect(Info.MasterIP, Info.MasterPort);
                        }
                        catch (Exception ex)
                        {
                            OnConnected?.Invoke(Info.MasterName, 0, false);
                        }
                    }
                    else
                    {
                        //Heartbeat();
                    }
                    Thread.Sleep(3000);
                }
            });
            t.IsBackground = true;
            t.Start();
        }
        private ClientInfo GetClientInfoConfig()
        {
           ClientInfo result = HTXmlHelper.EntityFromXMLFile<ClientInfo>(ClientInfoConfigPath);
            return result;
        }
  
        private void ServerConnectedHandle(object sender, SocketEventArgs e)
        {
            LastHeartBeatTime = DateTime.Now;
            IsConnected = true;
            OnConnected?.Invoke(Info.MasterName, 0, true);
        }
        private void ServerDisConnectedHandle(object sender, SocketEventArgs e)
        {
            IsConnected = false;
            OnConnected?.Invoke(Info.MasterName, 0, false);
        }

        public void ReceiveMessageHandle(object sender, SocketEventArgs e)
        {
            if (e.Datas.Count() > 0)
            {
                LastHeartBeatTime = DateTime.Now;
                ProcessMsg(e.Datas);
            }
        }
        public void SendMessage(MessageInfo info)
        {
            try
            {
                byte[] datas = null;
                if (info.Datas != null)
                {
                    datas = PcMsgStruct.GetMsgBuffer((int)info.Command, "", info.Datas);
                }
                else
                {
                    datas = PcMsgStruct.GetMsgBuffer((int)info.Command, info.Message);
                }
                if (Socket.Connected)
                {
                    Socket.Send(datas);
                    LogManager.GetLogger(DeviceName).Info($"Command:{info.Command},Message:{info.Message},CommunicationType:{info.Type}");
                }
                else
                {
                    LogManager.GetLogger(DeviceName).Fatal("服务端连接失败，无法发送数据，" + $"Command：{info.Command},Message：{info.Message}");
                }
            }
            catch (Exception ex)
            {
                OnShowMsg?.Invoke(DeviceName,$"ClientHelper->SendMessage方法执行异常，异常信息：{ex}",Brushes.Red);
            }
        }
        /// <summary>
        /// 解析接收的消息，帧头2字节 +CRC长度4字节 + 命令4字节 +消息体长度4字节+N字节消息长度+帧尾2字节=16+N字节
        /// </summary>
        /// <param name="dataList"></param>
        /// <returns></returns>
        private void ProcessMsg(byte[] buffer)
        {
            try
            {
                if (buffer.Length < PcMsgStruct.MsgHeaderLength) return; //数据包长度校验
                if (buffer[0] != 0x00 || buffer[1] != 0x99) return;//消息头校验
                byte[] temp = new byte[4];
                Array.Copy(buffer, 10, temp, 0, 4);
                int msgLength = BitConverter.ToInt32(temp, 0);//获取消息体长度
                if (buffer[14 + msgLength] != 0x99 || buffer[15 + msgLength] != 0x00) return;//消息尾校验
                string msg = string.Empty;
                int command = 0;
                PcMsgStruct.UnpackData(buffer.Take(16 + msgLength).ToArray(), ref msg, ref command);//解析消息，消息长度16 + msgLength个字节
                if (!msg.Contains("Alive"))//如果是心跳，不做处理
                {
                    MessageInfo info = new MessageInfo { Command = (Command)command, Message = msg };
                    ProcessCmd.Instance.DealCommand(info);
                }
                byte[] datas = buffer.Skip(16 + msgLength).ToArray();
                ProcessMsg(datas);
            }
            catch (Exception ex)
            {
                LogManager.GetLogger(DeviceName).Fatal(ex.ToString());
            }
        }
      
        /// <summary>
        /// 心跳
        /// </summary>
        public void HeartBeat()
        {
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    try
                    {
                        if (Param.Comm.HeartShield)
                        {
                            Thread.Sleep(5000);
                            continue;
                        }
                        TimeSpan span = DateTime.Now.Subtract(LastHeartBeatTime);
                        if (Socket != null && Socket.Connected)
                        {
                            byte[] msg = PcMsgStruct.GetMsgBuffer((int)Command.HeartBeat, "Alive");
                            Socket.Send(msg);
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                    Thread.Sleep(500);
                }

            });
        }

      
        public void SendDetectResult(DetectResultArgs args)
        {
            try
            {
                MessageInfo info = new MessageInfo();
                info.Type = CommunicationType.Send;
                info.PCName = $"AoiPC{args.DetectReqArgs.PCIndex + 1}";
                info.Command = Command.DetectResult;
                info.Datas = ClassHelper.ObjectToBytes(args);
                info.Message = args.ToString();
                SendMessage(info);
            }
            catch (Exception ex)
            {
                LogManager.GetLogger(DeviceName).Fatal(ex.ToString());
            }
        }
        public void SendPowerControlResult(int pcIndex,string type,string result)
        {
            try
            {
                MessageInfo info = new MessageInfo();
                info.Type = CommunicationType.Send;
                info.PCName = $"AoiPC{pcIndex + 1}";
                info.Command = Command.PowerControlResult;
                info.Message =$"{pcIndex},{type},{result}";
                SendMessage(info);
            }
            catch (Exception ex)
            {
                LogManager.GetLogger(DeviceName).Fatal(ex.ToString());
            }
        }
        public void SendDfsParam(string stepID,string eqpID)
        {
            try
            {
                MessageInfo info = new MessageInfo();
                info.Type = CommunicationType.Send;
                info.PCName = $"AoiPC";
                info.Command = Command.DfsPara;
                info.Message = $"{stepID},{eqpID}";
                SendMessage(info);
            }
            catch (Exception ex)
            {
                LogManager.GetLogger(DeviceName).Fatal(ex.ToString());
            }
        }
        public void SendRecipeSyncResult(int pcIndex,bool ret)
        {
            try
            {
                MessageInfo info = new MessageInfo();
                info.Type = CommunicationType.Send;
                info.PCName = $"AoiPC{pcIndex + 1}";
                info.Command = Command.RecipeSyncAck;
                info.Message = $"{ info.PCName},{(ret?"OK":"NG")}";
                SendMessage(info);
            }
            catch (Exception ex)
            {
                LogManager.GetLogger(DeviceName).Fatal(ex.ToString());
            }
        }
        public void Uninit()
        {
            Socket.Disconnect();
            Socket.Dispose();
        }

        public void InitRet(bool ret)
        {
        }
    }


}
