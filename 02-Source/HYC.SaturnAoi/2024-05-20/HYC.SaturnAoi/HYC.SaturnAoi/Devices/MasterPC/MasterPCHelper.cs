/*----------------------------------------------------------------
// Copyright (C)  Suzhou HYC Technology Co.,LTD
// 版权所有。
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnMain.Devices.Motion
// 文件名称    ：MasterPCHelper.cs
// =================================
// 创 建 者    ：guguangzhou
// 创建日期    ：2021/01/29 09:00:00 
// 功能描述    ：服务端功能类
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
using HYC.HTCommunication.SocketWrapper;
using HYC.HTFile.HTXML;
using HYC.HTLog;
using HYC.HTPCBase.Extension.Recipe;
using HYC.SaturnMain.Devices.MasterPC;
using HYC.SaturnMain;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using CommonLib.Device;
using HYC.SaturnAoi.Devices.Client;
using HYC.SaturnAoi.Entities;
using CommonLib.Entities;
using HYC.SaturnAoi.Devices.MuraPC;
using HYC.SaturnAoi.Params;

namespace HYC.SaturnMain.Devices.MasterPC
{
    /// <summary>
    /// 与MuraPC通讯，做服务端
    /// </summary>
    public class MasterPCHelper : IDevice
    {
        public event Action<string, int, bool> OnConnected;
        public event Action<string, string, Brush> OnShowMsg;
        public event Action<MessageInfo> OnProcessCmd;
        HTCOMMSocketServer Server = null;
        ManualResetEvent Mre = new ManualResetEvent(false);
        string ReceiveMsg = string.Empty;
        public Dictionary<string, ManualResetEvent> ClientMreDic = new Dictionary<string, ManualResetEvent>();
        public static MasterPCHelper Instance { get { return DeviceManager.Instance.Get("MasterPC") as MasterPCHelper; } }
        public string DeviceName { get; set; } = "MasterPC";
        public bool IsInit { get ; set ; }

        ServerInfo Info = null;
        public Dictionary<string, ClientInfo> ClientDic = null;
        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            Task.Factory.StartNew(()=> {
                try
                {
                    string ConfigPath = AppDomain.CurrentDomain.BaseDirectory + @"Config\Hardware\ServerInfo.xml";
                    Info = HTXMLSerializer.EntityFromXMLFile<ServerInfo>(ConfigPath);
                    ClientDic = Info.ClientList.ToDictionary(p => p.IP);
                    Server = new HTCOMMSocketServer();
                    Server.Listen(Info.Port);
                    Server.ClientConnected += Server_ClientConnected;//丛机连接状态显示
                    Server.DataReceived += Server_DataReceived;//从机消息反馈
                    Server.ClientDisconnected += Server_ClientDisconnected;//丛机连接状态显示
                }
                catch (Exception ex)
                {
                    OnShowMsg?.Invoke(DeviceName, ex.ToString(), Brushes.Red);
                }
            });
   
        }
        private void Server_ClientDisconnected(object sender, SocketEventArgs e)
        {
            try
            {
                ClientInfo info = GetClientByIP(e.RemoteIP);
                info.Connected = false;
                if (info != null)
                {
                    OnConnected?.Invoke(info.Name, info.Index, false);
                }
            }
            catch (Exception ex)
            {
                LogManager.GetLogger(DeviceName).Fatal(ex.ToString());
            }
        }

        private void Server_DataReceived(object sender, SocketEventArgs e)
        {
            ClientInfo client = GetClientByIP(e.RemoteIP);
            try
            {
                if (client != null)
                {
                    client.LastHeartBeatTime = DateTime.Now;
                    ProcessMsg(e.Datas, client.Name);
                }
            }
            catch (Exception ex)
            {
                LogManager.GetLogger(DeviceName).Fatal(ex.ToString());
            }
        }

        private void Server_ClientConnected(object sender, SocketEventArgs e)
        {
            try
            {
                ClientInfo client = ClientDic[e.RemoteIP];
                client.Client = new HTSocketConnection(e.Socket);
                client.Connected = true;
                OnConnected?.Invoke(client.Name, client.Index, true);
                MuraPC.Instance.RecipeSync(RecipeManager.CurRecipe.RecipeID, "Shift");
            }
            catch (Exception ex)
            {
                LogManager.GetLogger(DeviceName).Fatal(ex.ToString());
            }
        }
        public bool GetClientConnectSts(string clientPCName)
        {
            try
            {
                var pc = ClientDic.Values.Where(p => p.Name == clientPCName).First();
                return pc.Connected;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 解析接收的消息，帧头2字节 +CRC长度4字节 + 命令4字节 +消息体长度4字节+N字节消息长度+帧尾2字节=16+N字节
        /// </summary>
        /// <param name="dataList"></param>
        /// <returns></returns>
        private void ProcessMsg(byte[] buffer, string clientName)
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
                    MessageInfo info = new MessageInfo { Type = CommunicationType.Receive, PCName = clientName, Command = (Command)command, Message = msg };
                    ProcessCmd.Instance.DealCommand(info);//压入接收队列
                }
                byte[] datas = buffer.Skip(16 + msgLength).ToArray();
                ProcessMsg(datas, clientName);
            }
            catch (Exception ex)
            {
                LogManager.GetLogger(DeviceName).Fatal(ex.ToString());
            }
        }
        /// <summary>
        /// 查询客户端by name
        /// </summary>
        /// <param name="ip">客户端ip</param>
        /// <returns></returns>
        public ClientInfo GetClientByIP(string ip)
        {
            try
            {
                if (ClientDic != null)
                {
                    foreach (KeyValuePair<string, ClientInfo> k in ClientDic)
                    {
                        if (k.Value.IP == ip)
                        {
                            return k.Value;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.GetLogger(DeviceName).Fatal(ex.ToString());
            }

            return null;
        }
        /// <summary>
        /// 查询客户端by name
        /// </summary>
        /// <param name="name">客户端名称</param>
        /// <returns></returns>
        public ClientInfo GetClientByName(string name)
        {
            try
            {
                if (ClientDic != null)
                {
                    foreach (KeyValuePair<string, ClientInfo> k in ClientDic)
                    {
                        if (k.Value.Name == name)
                        {
                            return k.Value;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.GetLogger(DeviceName).Fatal(ex.ToString());
            }

            return null;
        }
        public void SendMessage(MessageInfo info)
        {
            try
            {
                byte[] datas = null;
                if (info.Datas!=null)
                {
                    datas = PcMsgStruct.GetMsgBuffer((int)info.Command, "",info.Datas);
                }
                else
                {
                    datas = PcMsgStruct.GetMsgBuffer((int)info.Command, info.Message);
                }
                ClientInfo clientInfo = GetClientByName(info.PCName.ToString());
                if (clientInfo != null && clientInfo.Client != null&& clientInfo.Connected)
                {
                    Server?.Send(clientInfo.Client.RemoteIP, clientInfo.Client.RemotePort, datas);
                }
                LogManager.GetLogger(DeviceName).Info($"PCName:{info.PCName},CommunicationType:{info.Type},Command:{info.Command},Message:{info.Message}");
            }
            catch (Exception ex)
            {
                LogManager.GetLogger(DeviceName).Fatal(ex.ToString());
            }
        }

        public void SendAllClients(string msg, Command cmd)
        {
            try
            {
                foreach (var item in ClientDic)
                {
                    byte[] temp = PcMsgStruct.GetMsgBuffer((int)cmd, msg);
                    Server.Send(item.Value.Client.RemoteIP, item.Value.Client.RemotePort, temp);
                }
            }
            catch (Exception ex)
            {
                LogManager.GetLogger(DeviceName).Fatal(ex.ToString());
            }
        }
  
        public void Uninit()
        {
            Server?.Dispose();
        }

        public void InitRet(bool ret)
        {
        }
    }
    public class ClientInfo
    {
        /// <summary>
        /// 序号
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// Socket
        /// </summary>
        [XmlIgnore]
        public HTSocketConnection Client { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        public string IP { get; set; }
        /// <summary>
        /// 连接状态
        /// </summary>
        [XmlIgnore]
        public bool Connected { get; set; }
        [XmlIgnore]
        public DateTime LastHeartBeatTime { get; set; }
    }
}
