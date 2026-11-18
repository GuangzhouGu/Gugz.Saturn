/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2020 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnMain
// 文件名称    ：EfuHelper.cs
// =================================
// 创 建 者    ：guguangzhou
// 创建日期    ：2021/01/29 09:00:00 
// 功能描述    ：Efu功能类
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HYC.HTCommunication.SerialPortWrapper;
using System.Threading;
using HYC.HTLog;
using System.Drawing;

namespace HYC.SaturnMain.Devices.EfuController
{
    class EfuControl
    {
        public event Action<string,Brush> OnShowMessage;   //显示通信信息记录
        public event Action<string, int, bool> OnConnected;
        HTCOMMSerialPort Sp = null;
        ManualResetEvent Mre = new ManualResetEvent(false);
        private byte Start_ICU_ID = 0, End_ICU_ID = 0;
        private int IcuCount = 0;
        private bool SetOrRead = false;//读取还是设定，true读取，false设定
        private string[,] ReadResult = null;
        private bool SetResult = false;
        public bool IsConnected { get; set; }
        EfuInfo Info = null;
        public  EfuControl(EfuInfo info)
        {
            Info = info;
            Sp = new HTCOMMSerialPort();
            Sp.DataReceived += DataReceived;
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
                            bool ret = Sp.Init(new HTSerialPortInfo { PortName = Info.PortName, BaudRate = Info.BaudRate, DataBits = 8, StopBits = 0, Parity = 0 });
                            OnConnected?.Invoke($"Efu{(Info.EfuNum+1).ToString("00")}", Info.EfuNum, ret);
                        }
                        catch (Exception)
                        {
                            OnConnected?.Invoke($"Efu{(Info.EfuNum+1).ToString("00")}", Info.EfuNum, false);
                        }
                    }
                    Thread.Sleep(1000);
                }
            });
            t.IsBackground = true;
            t.Start();
        }
        /// <summary>
        /// LV32_ID : 1 ~ 32 LV32 ID 根据LV32后的码盘设定, PLC获取MCU发送值为十六进制值（或者ID 为1时从0X81开始）.
        ///Start ICU_ID : 1 ~ 32 ICU ID   根据EFU的ID设定值，同样为16进制值（或者ID 为1时从0X81开始）
        ///End ICU_ID : 1 ~ 32 ICU ID 同Start ICU_ID
        ///以上 STX MODE1、MODE2、 DPU ID、 EXT都为固定值
        ///Check sum= Mode1+…+ENDICU_ID 取结果的低八位。
        /// </summary>读取实际值，报警状态，设定值
        /// <param name="LV32_ID"></param>
        /// <param name="Start_ICU_ID"></param>
        /// <param name="End_ICU_ID"></param>
        public string[,] GetProcessValue(byte lv32_ID, byte start_ICU_ID  , byte end_ICU_ID,int count)
        {
            try
            {
                SetOrRead = true;
                Start_ICU_ID = start_ICU_ID;
                End_ICU_ID = end_ICU_ID;
                IcuCount = count;
                byte STX = 0x02;
                byte MODE1 = 0x8A;
                byte MODE2 = 0x87;
                byte DPU_ID= 0x9F;
                byte ETX = 0x03;
                List<byte> tempList = new List<byte>();
                tempList.Add(STX);
                tempList.Add(MODE1);
                tempList.Add(MODE2);
                tempList.Add(lv32_ID);
                tempList.Add(DPU_ID);
                tempList.Add(start_ICU_ID);
                tempList.Add(end_ICU_ID);
                int temp = MODE1 + MODE2 + lv32_ID + DPU_ID +start_ICU_ID + end_ICU_ID;
                byte checkSum = BitConverter.GetBytes(temp)[0];
                tempList.Add(checkSum);
                tempList.Add(ETX);
                if (!Sp.IsOpen)
                {
                    Sp.Open();
                }
                Mre.Reset();
                Sp.Send(tempList.ToArray());
                bool rtn=Mre.WaitOne();
                if(rtn)
                {
                    OnShowMessage?.Invoke("读取转速成功",Brushes.Green);
                    return ReadResult;
                }
               else
                {
                    OnShowMessage?.Invoke("读取转速超时", Brushes.Red);
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("Efu").Fatal(ex.ToString());
                return null;
            }
        }
        Random rd = new Random();
        public string[,] GetProcessValueSmilator(byte lv32_ID, byte start_ICU_ID, byte end_ICU_ID, int count)
        {
            string[,] temp =new string[12,4];
          
            for (int i = 0; i < 12; i++)
            {
                int temp1 = rd.Next(140,145);
                temp[i,0] =$"{i+1}";
                temp[i, 1] = $"{temp1}";
                temp[i, 2] = $"{0x80}";
                temp[i,3] = $"{temp1}";
            }
            return temp;
        }
        /// <summary>
        /// 调速
        /// </summary>
        /// <param name="lv32_ID"></param>
        /// <param name="start_ICU_ID"></param>
        /// <param name="end_ICU_ID"></param>
        /// <param name="svValue">设定值</param>
        /// <param name="count"></param>
        public bool SetProcessValue(byte lv32_ID, byte start_ICU_ID, byte end_ICU_ID,byte svValue, int count)
        {
            try
            {
                SetOrRead = false;
                Start_ICU_ID = start_ICU_ID;
                End_ICU_ID = end_ICU_ID;
                IcuCount = count;
                byte STX = 0x02;
                byte MODE1 = 0x8A;
                byte MODE2 = 0x87;
                byte DPU_ID = 0x9F;
                byte ETX = 0x03;
                List<byte> tempList = new List<byte>();
                tempList.Add(STX);
                tempList.Add(MODE1);
                tempList.Add(MODE2);
                tempList.Add(lv32_ID);
                tempList.Add(DPU_ID);
                tempList.Add(start_ICU_ID);
                tempList.Add(end_ICU_ID);
                tempList.Add(svValue);
                int temp = MODE1 + MODE2 + lv32_ID + DPU_ID + start_ICU_ID + end_ICU_ID+ svValue;
                byte checkSum = BitConverter.GetBytes(temp)[0]; 
                tempList.Add(checkSum);
                tempList.Add(ETX);
                if (!Sp.IsOpen)
                {
                    Sp.Open();
                }
                Mre.Reset();
                Sp.Send(tempList.ToArray());
                bool rtn=Mre.WaitOne(3000);
                if(!rtn)
                {
                    OnShowMessage?.Invoke("设定转速超时", Brushes.Red);
                    return false;
                }
                else
                {
                    OnShowMessage?.Invoke("设定转速成功", Brushes.Green);
                    return SetResult;
                }
               
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("Efu").Fatal(ex.ToString());
                return false;
            }
        }
        private void DataReceived(object sender, SerialPortEventArgs e)
        {
            try
            {
                byte[] datas = e.Datas;
                if(SetOrRead)
                {
                    if (datas != null && datas.Length == (7 + IcuCount * 4))
                    {
                        DealMsg(datas);
                        Mre.Set();
                    }
                }
                else
                {
                    if (datas != null && datas.Length == 10)
                    {
                        byte[] temp = datas.Skip(7).ToArray();
                        SetResult = temp[0] == 0xb9;
                        Mre.Set();
                    }
                }
             
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("Efu").Fatal(ex.ToString());
            }
        }
        private void DealMsg(byte[]datas)
        {
            try
            {
                ReadResult = new string[IcuCount,4];
                byte[] temp = datas.Skip(5).ToArray();
                for (int i = 0; i < IcuCount; i++)
                {
                    byte[] temp1 = temp.Skip(4 * i).Take(4).ToArray();
                    for (int j = 0; j < 4; j++)
                    {
                        ReadResult[i, j] = temp1[j].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                OnShowMessage?.Invoke("读取转速失败", Brushes.Red);
                LogManager.GetLogger("Efu").Fatal(ex.ToString());
            }
        }

        public bool Uninit()
        {
            bool ret=Sp.Close();
            Sp.Dispose();
            return ret;
        }
    }
    public class EfuValue
    {
        /// <summary>
        /// 序号
        /// </summary>
        public int Num { get; set; }
        /// <summary>
        /// 实际转速值
        /// </summary>
        public int ActValue { get; set; }
        /// <summary>
        /// 设定转速值
        /// </summary>
        public int SetValue { get; set; }
        /// <summary>
        /// 风扇状态
        /// 
        /// </summary>
        public string FanStatus { get; set; }

    }

    public enum EfuStatus
    {
        正常=0x80,
        供电故障 = 0x84,
        电流过载Alarm = 0xA0,
        电机Alarm = 0xC0,
        通讯Alarm = 0x00,    
    }
}
