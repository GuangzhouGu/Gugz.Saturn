/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2023 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnAoi.Devices.PatternGenerator
// 文件名称    ：Class1
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
using HYC.HTCommunication.SocketWrapper;
using HYC.HTLog;
using HYC.SaturnAoi.Entities;
using NullFX.CRC;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HYC.SaturnAoi.Devices.Tester
{
    /// <summary>
    /// 使用协议封装的接口
    /// </summary>
    public class ELPTesterControl: ELPTesterBase
    {
        public event Action<string, string, Brush> OnShowMsg; //显示通信信息记录
        public event Action<string, int, bool> OnConnected;
        HTCOMMSocketClient Socket = new HTCOMMSocketClient();
        public ELPTesterInfo Info = null;
        ManualResetEvent Mre = new ManualResetEvent(false);
        string Msg = string.Empty;
        short TesterRepCmd = 0;
        TesterReqCmd CurCmd;
        byte[] ResultData = null;
        #region 通讯帧头帧尾
        const byte Head = 0x7F;
        const byte Tail = 0x7E; 
        #endregion
        //协议 ：Head(1byte)|Header(Command+ID+Length)|Data|CRC(2byte)|Tail(1byte)
        public ELPTesterControl(ELPTesterInfo info)
        {
            Info = info;
            Socket.ClientConnected += new EventHandler<SocketEventArgs>(ConnectedHandle);//server连接状态显示
            Socket.ClientDisconnected += new EventHandler<SocketEventArgs>(DisConnectedHandle);//server连接状态显示
            Socket.DataReceived += new EventHandler<SocketEventArgs>(ReceiveHandle);
            Connect();
         
        }
        /// <summary>
        /// 连接
        /// </summary>
        public override void Connect()
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
                            OnConnected?.Invoke($"{Info.TesterName}", Info.TesterNum, false);
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
            OnConnected?.Invoke($"{Info.TesterName}", Info.TesterNum, true);
        }
        private void DisConnectedHandle(object sender, SocketEventArgs e)
        {
            IsConnected = false;
            OnConnected?.Invoke($"{Info.TesterName}", Info.TesterNum, false);
        }

        public void ReceiveHandle(object sender, SocketEventArgs e)
        {
            try
            {
                string temp = Encoding.Default.GetString(e.Datas);
                LogManager.GetLogger("ELPTester").Info($"Received Data：{temp.Replace("\r\n", "")}");
                if (e.Datas.Length > 2)
                {
                    byte[] cmdByte = e.Datas.Skip(1).Take(2).ToArray();
                    short resCmd = BitConverter.ToInt16(cmdByte, 0);
                    byte[] res1 = e.Datas.Skip(5).Take(1).ToArray();
                    byte[] dataLengthByte = e.Datas.Skip(8).Take(2).ToArray();
                    short dataLength = BitConverter.ToInt16(dataLengthByte, 0);
                    if (dataLength>0)
                    {
                        ResultData = e.Datas.Skip(11).Take(dataLength).ToArray();
                    }
                    if (res1[0]==0x06&& TesterRepCmd == resCmd)
                    {
                        Mre.Set();
                    }
                    else if (res1[0]==0x15)
                    {
                        byte[] causeByte = e.Datas.Skip(6).Take(2).ToArray();
                        short cause = BitConverter.ToInt16(causeByte, 0);
                        string errMsg = $"{(ErrorMsg)cause}";
                        OnShowMsg($"ELPTester", $"ELPTester->{CurCmd}指令执行出错，错误信息：{errMsg}", Brushes.Red);
                    }
                 
                }
                else
                {
                    LogManager.GetLogger("ELPTester").Info("ELPTester->返回数据长度出错，数据长度小于3");
                }
            }
            catch (Exception ex)
            {
                OnShowMsg($"ELPTester", $"ELPTester->ReceiveHandle执行异常，异常信息：{ex.Message}", Brushes.Red);
            }
        }
        /// <summary>
        /// 获取检查机状态
        /// </summary>
        /// <returns></returns>
        public override string GetStatus()
        {
            try
            {
                string ret = string.Empty;
                List<byte> list = new List<byte>();
                list.Add(Head);//Head
                list.AddRange(BitConverter.GetBytes((short)TesterReqCmd.REQ_STATUS_CHECK));//CMD
                CurCmd = TesterReqCmd.REQ_STATUS_CHECK;
                TesterRepCmd = (short)TesterReqCmd.REQ_STATUS_CHECK + 0x1000;
                list.AddRange(BitConverter.GetBytes((short)0x0000));//ID，定值0x0000
                list.AddRange(BitConverter.GetBytes((short)0x0000));//Data Length
                byte[] crcByte = list.Skip(1).Take(list.Count() - 1).ToArray();
                var crcCode = CrcHelper.Crc16(crcByte, crcByte.Length);//CRC Check
                list.AddRange(BitConverter.GetBytes((short)crcCode));//CRC Code
                list.Add(Tail);//Tail
                Mre.Reset();
                if (Socket.Connected)
                {
                    Socket.Send(list.ToArray());
                }
                bool ret2 = Mre.WaitOne(3000);
                if (ret2)
                {
                    short status = ResultData[0];
                    if (status == 0x00)
                    {
                        ret = "IDLE";
                    }
                    else if (status == 0x01)
                    {
                        ret = "RUN";
                    }
                    else if (status == 0x02)
                    {
                        ret = "BUSY";
                    }
                    else if (status == 0x03)
                    {
                        ret = "WDG_RST";
                    }
                    short patternNum = ResultData[1];
                }
                return ret;
            }
            catch (Exception ex)
            {
                OnShowMsg($"ELPTester", $"ELPTester->GetStatus执行异常，异常信息：{ex.Message}", Brushes.Red);
                return "";
            }
        }
        /// <summary>
        /// 开电
        /// </summary>
        /// <returns></returns>
        public override bool PowerOn()
        {
            try
            {
                List<byte> list = new List<byte>();
                list.Add(Head);
                list.AddRange(BitConverter.GetBytes((short)TesterReqCmd.REQ_PTN_ON));
                CurCmd = TesterReqCmd.REQ_PTN_ON;
                TesterRepCmd = (short)TesterReqCmd.REQ_PTN_ON + 0x1000;
                list.AddRange(BitConverter.GetBytes((short)0x0000));
                list.AddRange(BitConverter.GetBytes((short)0x0000));
                byte[] crcByte = list.Skip(1).Take(list.Count() - 1).ToArray();
                var crcCode = CrcHelper.Crc16(crcByte, crcByte.Length);//CRC Check
                list.AddRange(BitConverter.GetBytes((short)crcCode));
                list.Add(Tail);
                Mre.Reset();
                if (Socket.Connected)
                {
                    Socket.Send(list.ToArray());
                }
                bool ret = Mre.WaitOne(3000);
                return ret;
            }
            catch (Exception ex)
            {
                OnShowMsg($"ELPTester", $"ELPTester->PowerOn执行异常，异常信息：{ex.Message}", Brushes.Red);
                return false;
            }
        }
        /// <summary>
        /// 关电
        /// </summary>
        /// <returns></returns>
        public override bool PowerOff()
        {
            try
            {
                List<byte> list = new List<byte>();
                list.Add(Head);
                list.AddRange(BitConverter.GetBytes((short)TesterReqCmd.REQ_PTN_OFF));
                CurCmd = TesterReqCmd.REQ_PTN_OFF;
                TesterRepCmd = (short)TesterReqCmd.REQ_PTN_OFF + 0x1000;
                list.AddRange(BitConverter.GetBytes((short)0x0000));
                list.AddRange(BitConverter.GetBytes((short)0x0000));
                byte[] crcByte = list.Skip(1).Take(list.Count() - 1).ToArray();
                var crcCode = CrcHelper.Crc16(crcByte, crcByte.Length);//CRC Check
                list.AddRange(BitConverter.GetBytes((short)crcCode));
                list.Add(Tail);
                Mre.Reset();
                if (Socket.Connected)
                {
                    Socket.Send(list.ToArray());
                }
                bool ret = Mre.WaitOne(3000);
                return ret;
            }
            catch (Exception ex)
            {
                OnShowMsg($"ELPTester", $"ELPTester->PowerOff执行异常，异常信息：{ex.Message}", Brushes.Red);
                return false;
            }
        }
        /// <summary>
        /// 序号切图
        /// </summary>
        /// <param name="patternNum"></param>
        /// <returns></returns>
        public override bool PatternDisplay(int patternNum)
        {
            try
            {
                List<byte> list = new List<byte>();
                list.Add(Head);//Head
                list.AddRange(BitConverter.GetBytes((short)TesterReqCmd.REQ_PTN_SEL));//CMD
                CurCmd = TesterReqCmd.REQ_PTN_SEL;
                TesterRepCmd = (short)TesterReqCmd.REQ_PTN_SEL + 0x1000;
                list.AddRange(BitConverter.GetBytes((short)0x0000));//ID，定值0x0000
                byte[] data = BitConverter.GetBytes(patternNum);
                list.AddRange(BitConverter.GetBytes((short)data.Length));//Data Length
                list.AddRange(data);//Data
                byte[] crcByte = list.Skip(1).Take(list.Count() - 1).ToArray();
                var crcCode = CrcHelper.Crc16(crcByte, crcByte.Length);//CRC Check
                list.AddRange(BitConverter.GetBytes((short)crcCode));//CRC Code
                list.Add(Tail);//Tail
                Mre.Reset();
                if (Socket.Connected)
                {
                    Socket.Send(list.ToArray());
                }
                bool ret = Mre.WaitOne(3000);
                patternNum = -1;
                if (ret && ResultData != null)
                {
                    patternNum = ResultData[0];
                    OnShowMsg($"ELPTester", $"ELPTester->当前画面序号：{patternNum}", Brushes.Green);
                }
                return ret;
            }
            catch (Exception ex)
            {
                OnShowMsg($"ELPTester", $"ELPTester->PatternDisplay执行异常，异常信息：{ex.Message}", Brushes.Red);
                return false;
            }
        }
        /// <summary>
        /// 获取当前画面序号
        /// </summary>
        /// <returns></returns>
        public override int GetPatternNum()
        {
            try
            {
                List<byte> list = new List<byte>();
                list.Add(Head);
                list.AddRange(BitConverter.GetBytes((short)TesterReqCmd.REQ_PTN_NUM));
                CurCmd = TesterReqCmd.REQ_PTN_NUM;
                TesterRepCmd = (short)TesterReqCmd.REQ_PTN_NUM + 0x1000;
                list.AddRange(BitConverter.GetBytes((short)0x0000));
                list.AddRange(BitConverter.GetBytes((short)0x0000));
                byte[] crcByte = list.Skip(1).Take(list.Count() - 1).ToArray();
                var crcCode = CrcHelper.Crc16(crcByte, crcByte.Length);//CRC Check
                list.AddRange(BitConverter.GetBytes((short)crcCode));
                list.Add(Tail);
                Mre.Reset();
                if (Socket.Connected)
                {
                    Socket.Send(list.ToArray());
                }
                bool ret2 = Mre.WaitOne(3000);
                short patternNum = -1;
                if (ret2 && ResultData != null)
                {
                    patternNum = ResultData[0];
                    OnShowMsg($"ELPTester", $"ELPTester->当前画面序号：{patternNum}", Brushes.Green);
                }
                return patternNum;
            }
            catch (Exception ex)
            {
                OnShowMsg($"ELPTester", $"ELPTester->GetPatternNum执行异常，异常信息：{ex.Message}", Brushes.Red);
                return -1;
            }
        }
        /// <summary>
        /// 获取当前画面名称
        /// </summary>
        /// <returns></returns>
        public override string GetPatternName()
        {
            try
            {
                List<byte> list = new List<byte>();
                list.Add(Head);
                list.AddRange(BitConverter.GetBytes((short)TesterReqCmd.REQ_PTN_ON_NAME));
                CurCmd = TesterReqCmd.REQ_PTN_ON_NAME;
                TesterRepCmd = (short)TesterReqCmd.REQ_PTN_ON_NAME + 0x1000;
                list.AddRange(BitConverter.GetBytes((short)0x0000));
                list.AddRange(BitConverter.GetBytes((short)0x0000));
                byte[] crcByte = list.Skip(1).Take(list.Count() - 1).ToArray();
                var crcCode = CrcHelper.Crc16(crcByte, crcByte.Length);//CRC Check
                list.AddRange(BitConverter.GetBytes((short)crcCode));
                list.Add(Tail);
                Mre.Reset();
                if (Socket.Connected)
                {
                    Socket.Send(list.ToArray());
                }
                bool ret = Mre.WaitOne(3000);
                string patternName = string.Empty; ;
                if (ret && ResultData != null)
                {
                    patternName = Encoding.Default.GetString(ResultData);
                    OnShowMsg($"ELPTester", $"ELPTester->当前画面名称：{patternName}", Brushes.Green);
                }
                return patternName;
            }
            catch (Exception ex)
            {
                OnShowMsg($"ELPTester", $"ELPTester->GetPatternName执行异常，异常信息：{ex.Message}", Brushes.Red);
                return "";
            }
        }
        /// <summary>
        /// 后切图切
        /// </summary>
        /// <returns></returns>
        public override bool PatternNext()
        {
            try
            {
                List<byte> list = new List<byte>();
                list.Add(Head);
                list.AddRange(BitConverter.GetBytes((short)TesterReqCmd.REQ_PTN_NEXT));
                CurCmd = TesterReqCmd.REQ_PTN_NEXT;
                TesterRepCmd = (short)TesterReqCmd.REQ_PTN_NEXT + 0x1000;
                list.AddRange(BitConverter.GetBytes((short)0x0000));
                list.AddRange(BitConverter.GetBytes((short)0x0000));
                byte[] crcByte = list.Skip(1).Take(list.Count() - 1).ToArray();
                var crcCode = CrcHelper.Crc16(crcByte, crcByte.Length);//CRC Check
                list.AddRange(BitConverter.GetBytes((short)crcCode));
                list.Add(Tail);
                Mre.Reset();
                if (Socket.Connected)
                {
                    Socket.Send(list.ToArray());
                }
                short patternNum = -1;
                bool ret = Mre.WaitOne(3000);
                if (ret)
                {
                    patternNum = ResultData[0];
                    OnShowMsg($"ELPTester", $"ELPTester->当前画面序号：{patternNum}", Brushes.Green);
                }
                return ret;
            }
            catch (Exception ex)
            {
                OnShowMsg($"ELPTester", $"ELPTester->PatternNext执行异常，异常信息：{ex.Message}", Brushes.Red);
                return false;
            }
        }
        /// <summary>
        /// 前切图
        /// </summary>
        /// <returns></returns>
        public override bool PatternPrevious()
        {
            try
            {
                List<byte> list = new List<byte>();
                list.Add(Head);
                list.AddRange(BitConverter.GetBytes((short)TesterReqCmd.REQ_PTN_PRE));
                CurCmd = TesterReqCmd.REQ_PTN_PRE;
                TesterRepCmd = (short)TesterReqCmd.REQ_PTN_PRE + 0x1000;
                list.AddRange(BitConverter.GetBytes((short)0x0000));
                list.AddRange(BitConverter.GetBytes((short)0x0000));
                byte[] crcByte = list.Skip(1).Take(list.Count() - 1).ToArray();
                var crcCode = CrcHelper.Crc16(crcByte, crcByte.Length);//CRC Check
                list.AddRange(BitConverter.GetBytes((short)crcCode));
                list.Add(Tail);
                Mre.Reset();
                if (Socket.Connected)
                {
                    Socket.Send(list.ToArray());
                }
                bool ret = Mre.WaitOne(3000);
                short patternNum = -1;
                if (ret)
                {
                    patternNum = ResultData[0];
                    OnShowMsg($"ELPTester", $"ELPTester->当前画面序号：{patternNum}", Brushes.Green);
                }
                return ret;
            }
            catch (Exception ex)
            {
                OnShowMsg($"ELPTester", $"ELPTester->PatternPrevious执行异常，异常信息：{ex.Message}", Brushes.Red);
                return false;
            }
        }
        public override bool DownLoadRecipe(int modelIndex,int ptnIndex,string recipeName)
        {
            try
            {
                List<byte> list = new List<byte>();
                list.Add(Head);
                list.AddRange(BitConverter.GetBytes((short)TesterReqCmd.REQ_RECIPE_INIT));
                CurCmd = TesterReqCmd.REQ_RECIPE_INIT;
                TesterRepCmd = (short)TesterReqCmd.REQ_RECIPE_INIT + 0x1000;
                list.AddRange(BitConverter.GetBytes((short)0x0000));
                var tempList = new List<byte>();
                byte[] data = BitConverter.GetBytes(modelIndex);
                tempList.AddRange(data);
                data = BitConverter.GetBytes(ptnIndex);
                tempList.AddRange(data);
                data = Encoding.Default.GetBytes(recipeName);
                tempList.AddRange(data);//Data
                list.AddRange(BitConverter.GetBytes((short)tempList.Count));//Data Length
                list.AddRange(tempList);
                byte[] crcByte = list.Skip(1).Take(list.Count() - 1).ToArray();
                var crcCode = CrcHelper.Crc16(crcByte, crcByte.Length);//CRC Check
                list.AddRange(BitConverter.GetBytes((short)crcCode));
                list.Add(Tail);
                Mre.Reset();
                if (Socket.Connected)
                {
                    Socket.Send(list.ToArray());
                }
                bool ret = Mre.WaitOne(3000);
                if (ret)
                {
                    OnShowMsg($"ELPTester", $"ELPTester->切换机种成功：{recipeName}", Brushes.Green);
                }
                return ret;
            }
            catch (Exception ex)
            {
                OnShowMsg($"ELPTester", $"ELPTester->DownLoadRecipe执行异常，异常信息：{ex.Message}", Brushes.Red);
                return false;
            }
        }
        public override string GetCurRecipe()
        {
            try
            {
                List<byte> list = new List<byte>();
                list.Add(Head);
                list.AddRange(BitConverter.GetBytes((short)TesterReqCmd.REQ_RCP_NAME));
                CurCmd = TesterReqCmd.REQ_RCP_NAME;
                TesterRepCmd = (short)TesterReqCmd.REQ_RCP_NAME + 0x1000;
                list.AddRange(BitConverter.GetBytes((short)0x0000));
                list.AddRange(BitConverter.GetBytes((short)0x0000));
                byte[] crcByte = list.Skip(1).Take(list.Count() - 1).ToArray();
                var crcCode = CrcHelper.Crc16(crcByte, crcByte.Length);//CRC Check
                list.AddRange(BitConverter.GetBytes((short)crcCode));
                list.Add(Tail);
                Mre.Reset();
                if (Socket.Connected)
                {
                    Socket.Send(list.ToArray());
                }
                bool ret = Mre.WaitOne(3000);
                string recipeName = string.Empty; ;
                if (ret && ResultData != null)
                {
                    recipeName = Encoding.Default.GetString(ResultData);
                    OnShowMsg($"ELPTester", $"ELPTester->当前机种名称：{recipeName}", Brushes.Green);
                }
                return recipeName;
            }
            catch (Exception ex)
            {
                OnShowMsg($"ELPTester", $"ELPTester->GetCurRecipe执行异常，异常信息：{ex.Message}", Brushes.Red);
                return string.Empty;
            }
        }
        public override bool VoltageSet(int ptnIndex, short rVoltage, short gVoltage, short bVoltage)
        {
            try
            {
                List<byte> list = new List<byte>();
                list.Add(Head);
                list.AddRange(BitConverter.GetBytes((short)TesterReqCmd.REQ_CH_VOLTAGE_SWEEP));
                CurCmd = TesterReqCmd.REQ_CH_VOLTAGE_SWEEP;
                TesterRepCmd = (short)TesterReqCmd.REQ_CH_VOLTAGE_SWEEP + 0x1000;
                list.AddRange(BitConverter.GetBytes((short)0x0000));
                var tempList = new List<byte>();
              
                tempList.Add((byte)ptnIndex);
                tempList.Add(3);
                tempList.Add(44);
                byte[]data = BitConverter.GetBytes(rVoltage);
                tempList.AddRange(data);

                tempList.Add(48);
                data = BitConverter.GetBytes(gVoltage);
                tempList.AddRange(data);

                tempList.Add(52);
                data = BitConverter.GetBytes(bVoltage);
                tempList.AddRange(data);

                list.AddRange(BitConverter.GetBytes((short)tempList.Count));//Data Length
                list.AddRange(tempList);
                byte[] crcByte = list.Skip(1).Take(list.Count() - 1).ToArray();
                var crcCode = CrcHelper.Crc16(crcByte, crcByte.Length);//CRC Check
                list.AddRange(BitConverter.GetBytes((short)crcCode));
                list.Add(Tail);
                Mre.Reset();
                if (Socket.Connected)
                {
                    Socket.Send(list.ToArray());
                }
                bool ret = Mre.WaitOne(3000);
                if (ret)
                {
                    OnShowMsg($"ELPTester", $"ELPTester->设置电压成功", Brushes.Green);
                }
                return ret;
            }
            catch (Exception ex)
            {
                OnShowMsg($"ELPTester", $"ELPTester->VoltageSet执行异常，异常信息：{ex.Message}", Brushes.Red);
                return false;
            }
        }
        public override string VoltageGet(int ptnIndex)
        {
            try
            {
                double[] voltages = new double[3];
                List<byte> list = new List<byte>();
                list.Add(Head);
                list.AddRange(BitConverter.GetBytes((short)TesterReqCmd.REQ_CH_VOLTAGE_READ));
                CurCmd = TesterReqCmd.REQ_CH_VOLTAGE_READ;
                TesterRepCmd = (short)TesterReqCmd.REQ_CH_VOLTAGE_READ + 0x1000;
                list.AddRange(BitConverter.GetBytes((short)0x0000));
                var tempList = new List<byte>();
           
                tempList.Add((byte)ptnIndex);
                tempList.Add(3);
                tempList.Add(44);
                tempList.Add(48);
                tempList.Add(52);
              

                list.AddRange(BitConverter.GetBytes((short)tempList.Count));//Data Length
                list.AddRange(tempList);
                byte[] crcByte = list.Skip(1).Take(list.Count() - 1).ToArray();
                var crcCode = CrcHelper.Crc16(crcByte, crcByte.Length);//CRC Check
                list.AddRange(BitConverter.GetBytes((short)crcCode));
                list.Add(Tail);
                Mre.Reset();
                if (Socket.Connected)
                {
                    Socket.Send(list.ToArray());
                }
                bool ret = Mre.WaitOne(3000);
                string value = string.Empty; ;
                if (ret && ResultData != null)
                {
                    voltages[0] = Math.Round(BitConverter.ToInt16(ResultData, 2)/1000.0f,3);
                    voltages[1] = Math.Round(BitConverter.ToInt16(ResultData, 5) / 1000.0f,3);
                    voltages[2] = Math.Round(BitConverter.ToInt16(ResultData, 8) / 1000.0f,3);
                    OnShowMsg($"ELPTester", $"ELPTester->读取电压成功，电压值：{string.Join(",", voltages)}", Brushes.Green);
                }
                return value;
            }
            catch (Exception ex)
            {
                OnShowMsg($"ELPTester", $"ELPTester->VoltageGet执行异常，异常信息：{ex.Message}", Brushes.Red);
                return string.Empty;
            }
        }
        private void Heartbeat()
        {
            byte[] datas = new byte[] { 0x5a, 0x5b, 0, 0, 0, 0, 0 };
            Socket.Send(datas);
        }

        public override void DisConnect()
        {
            Socket.Disconnect();
            Socket.Dispose();
        }
    }
    public enum TesterReqCmd
    {
        REQ_STATUS_CHECK = 0x1060,
        REQ_PTN_ON = 0x1063,
        REQ_PTN_OFF = 0x1064,
        REQ_PTN_NEXT = 0x1065,
        REQ_PTN_PRE = 0x1066,
        REQ_PTN_SEL = 0x1067,
        REQ_FW_VERSION = 0x1068,
        REQ_CH_VOLTAGE_SWEEP=0x1071,
        REQ_CH_VOLTAGE_READ=0x1072,
        REQ_RECIPE_INIT = 0x107A,
        REQ_PTN_NUM = 0x107B,
        REQ_RCP_NAME = 0x107C,
        REQ_PTN_ON_NAME = 0x1084,
    }
    public enum ErrorMsg
    {
        NoError = 0x0000,
        CommandError = 0x0E01,
        LengthMismatchError = 0x0E02,
        CRCError = 0x0E03,
        CommunicationTimeOverError = 0x0E04,
        CommunicationTimeOverError2 = 0x0E05,
        SequenceError = 0x0E11,
        SequenceError1 = 0x0E12,
        SequenceError2 = 0x0E13,
        SequenceError3 = 0x0E14,
        SequenceError4 = 0x0E15,
    }
}
