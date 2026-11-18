using HYC.HTCommunication.SocketWrapper;
using HYC.HTFile.HTXML;
using HYC.HTLog;
using HYC.SaturnAoi.Devices.Client;
using HYC.SaturnAoi.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HYC.SaturnDFS.Devices
{
    public class MasterPC
    {
        HTCOMMSocketClient Socket = new HTCOMMSocketClient();
        public event Action<string, string, Brush> OnShowMsg; //显示通信信息记录
        public event Action<string, int, bool> OnConnected;
        MasterInfo Info = null;
        private string ConfigPath = AppDomain.CurrentDomain.BaseDirectory + @"\Config\Hardware\MasterInfo.xml";
        public bool IsConnected { get; set; }
        ManualResetEvent Mre = new ManualResetEvent(false);
        string Msg = string.Empty;
        public static MasterPC Instance = null;
        static MasterPC()
        {
            Instance = new MasterPC();
        }

        public void Init()
        {
            Info=HTXMLSerializer.EntityFromXMLFile<MasterInfo>(ConfigPath);
            Socket.ClientConnected += new EventHandler<SocketEventArgs>(ConnectedHandle);//server连接状态显示
            Socket.ClientDisconnected += new EventHandler<SocketEventArgs>(DisConnectedHandle);//server连接状态显示
            Socket.DataReceived += new EventHandler<SocketEventArgs>(ReceiveHandle);
            Connect();
        }
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
                            OnConnected?.Invoke(Info.Name, 0, false);
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
            OnConnected?.Invoke(Info.Name, 0, true);
        }
        private void DisConnectedHandle(object sender, SocketEventArgs e)
        {
            IsConnected = false;
            OnConnected?.Invoke(Info.Name,0, false);
        }

        public void ReceiveHandle(object sender, SocketEventArgs e)
        {
            try
            {
                string temp = Encoding.Default.GetString(e.Datas);
                LogManager.GetLogger("Master").Info($"接收到的数据：{temp.Replace("\r\n", "")}");
                if (e.Datas.Count() > 0)
                {
                    //LastHeartBeatTime = DateTime.Now;
                    ProcessMsg(e.Datas);
                }
            }
            catch (Exception ex)
            {
                OnShowMsg($"Master", $"ReceiveHandle执行异常，异常信息：{ex.Message}", Brushes.Red);
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
                LogManager.GetLogger(Info.Name).Fatal(ex.ToString());
            }
        }

    }
}
