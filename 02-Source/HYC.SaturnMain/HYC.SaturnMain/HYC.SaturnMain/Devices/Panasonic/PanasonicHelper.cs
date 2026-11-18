/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2022 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnMain.Devices.Panasonic
// 文件名称    ：PanasonicHelper
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
using HYC.HTFile.HTXML;
using HYC.HTModbus;
using HYC.HTPCBase.Extension.Device;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;

namespace HYC.SaturnMain.Devices.Panasonic
{

    public class PanasonicHelper 
    {
        public event Action<string, int, bool> OnConnected;
        public event Action<string, string, Brush> OnShowMsg;
        /// <summary>
        /// Modbus主站
        /// </summary>     
        private ModbusMaster master = null;
        public static PanasonicHelper Instance { get { return DeviceManager.Instance.Get("PanasonicHelper") as PanasonicHelper; } set { } }
        public string DeviceName { get; set; } = "PanasonicHelper";
        private readonly static object obj = new object();
        private string ConfigPath = AppDomain.CurrentDomain.BaseDirectory + @"\Config\HardWare\MotorInfo.xml";
        public void Init()
        {
            try
            {
                var configList = GetConfig();
                if (configList != null)
                {
                    HTSerialPortInfo info = new HTSerialPortInfo { PortName = configList[0].PortName, BaudRate = configList[0].BaudRate };
                    master = ModbusMaster.CreateRTU(info);
                    master.TimeOut = 5000;
                }
            }
            catch (Exception)
            {
                OnConnected?.Invoke("Motor", 0, false);
            }
        }
        private List<MotorInfo> GetConfig()
        {
            List<MotorInfo> result = HTXMLSerializer.EntityFromXMLFile<List<MotorInfo>>(ConfigPath);
            return result;
        }
        /// <summary>
        /// 初始化状态
        /// </summary>
        public bool IsInitOK
        {
            get
            {
                return master != null;
            }
        }


        /// <summary>
        ///  开始执行BlockMotion动作
        /// </summary>
        /// <param name="addr"></param>
        private void ActionExecutive(byte addr)
        {
            master.WriteSingleCoil(addr, 0x0120, true);
        }

        /// <summary>
        /// 回原点用
        /// </summary>
        /// <param name="addr">驱动器地址</param>
        /// <param name="para">回原点参数</param>
        /// <returns></returns>
        public bool Home(byte addr)
        {
            /// 设置block num
            master.WriteSingleRegister(addr, 0x4414, 00);
            /// 运动
            ActionExecutive(addr);
            return true;
        }

        /// <summary>
        /// 用预先设置速度和加减速度，走绝对运动，使用时需要预先设置BlockMotion参数
        /// </summary>
        /// <param name="addr">驱动器地址<</param>
        /// <param name="position">位置:单位脉冲</param>
        /// <param name="timeOut">运动超时时间</param>
        /// <returns></returns>
        public bool MoveAbs(byte addr, int count, int timeOut = 5000)
        {
            try
            {
                /// 写步距
                WriteBlockData(addr, 0x480A, count);
                /// 设置block num
                master.WriteSingleRegister(addr, 0x4414, 02);
                /// 运动
                ActionExecutive(addr);
                Thread.Sleep(100);
                return WaitMoveDone(addr, timeOut);
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        ///  设置回原点参数
        /// </summary>
        /// <param name="addr">驱动器地址：0到N</param>
        /// <param name="homeCheckType">原点检测方式</param>
        /// <param name="homeDir">回原点方向</param>

        /// <summary>
        /// 设置速度block
        /// </summary>
        /// <param name="addr">驱动器地址：0到N</param>
        /// <param name="blockNum"> block速度地址：0到15</param>
        /// <param name="velValue">电机旋转速度，0到3000，单位是r/min</param>
        public void SetBlockVel(byte addr, int blockNum, int velValue)
        {
            int blockAddr = 0x4600 + blockNum;
            WriteBlockData(addr, (ushort)blockAddr, velValue);
        }
        /// <summary>
        /// 设置加速度block
        /// </summary>
        /// <param name="addr">驱动器地址：0到N</param>
        /// <param name="blockNum"> block速度地址：0到15</param>
        /// <param name="accValue">电机旋转加速度，0到3000，单位是：ms/r/min，一般300到500即可</param>
        public void SetBlockAccVel(byte addr, int blockNum, int accValue)
        {
            int blockAddr = 0x4610 + blockNum;
            WriteBlockData(addr, (ushort)blockAddr, accValue);
        }
        /// <summary>
        /// 设置减速度block
        /// </summary>
        /// <param name="addr">驱动器地址：0到N</param>
        /// <param name="blockNum"> block速度地址：0到15</param>
        /// <param name="decValue">电机旋转减速度，0到3000，单位是：ms/r/min，一般300到500即可</param>
        public void SetBlockDecVel(byte addr, int blockNum, int decValue)
        {
            int blockAddr = 0x4620 + blockNum;
            WriteBlockData(addr, (ushort)blockAddr, decValue);
        }


        private void WriteBlockData(byte addr, ushort blockAddr, int value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            ushort[] data = new ushort[2];
            for (int i = 0; i < 2; i++)
            {
                data[i] = BitConverter.ToUInt16(bytes, i * 2);
            }
            master.WriteMultipleRegisters(addr, blockAddr, data);
        }
        /// <summary>
        /// 轴使能ON
        /// </summary>
        /// <param name="addr">驱动器地址</param>
        /// <returns></returns>
        public bool ServoOn(byte addr)
        {

            master.WriteSingleCoil(addr, 0x0060, true);
            return true;
        }

        /// <summary>
        /// 轴使能OFF
        /// </summary>
        /// <param name="addr">驱动器地址</param>
        /// <returns></returns>
        public bool ServoOff(byte addr)
        {

            master.WriteSingleCoil(addr, 0x0060, false);
            return true;
        }
        /// <summary>
        ///  报警清除
        /// </summary>
        /// <param name="addr"></param>
        /// <returns></returns>
        public bool AlarmReset(byte addr)
        {
            master.WriteSingleCoil(addr, 0x0061, false);
            Thread.Sleep(200);
            master.WriteSingleCoil(addr, 0x0061, true);
            return true;
        }
        /// <summary>
        ///  获取是否报警
        /// </summary>
        /// <param name="addr"></param>
        /// <returns></returns>
        public bool GetAlarmStatus(byte addr)
        {
            lock (obj)
            {
                return master.ReadCoils(addr, 0x00A1, 1)[0];
            }
        }
        /// <summary>
        ///  定位完成信号
        /// </summary>
        /// <param name="addr"></param>
        /// <returns>false：未完成，true： 完成</returns>
        private bool WaitMoveDone(byte addr, int TimeOut)
        {
            DateTime start = DateTime.Now;
            while ((DateTime.Now.Ticks - start.Ticks) / 10000 < TimeOut)
            {
                bool[] values = master.ReadCoils(addr, 0x00A0, 3);
                if (values[2])
                {
                    return true;
                }
                Thread.Sleep(150);
            }
            return false;
        }
        public bool AxisIsEnable(byte addr)
        {
            lock (obj)
            {
                bool[] val = master.ReadCoils(addr, 0x0020, 1);
                return val[0];
            }
        }

        public int GetAxisPosition(byte addr)
        {
            lock (obj)
            {
                ushort[] data = master.ReadHoldingRegisters(addr, 0x600F, 2);
                List<byte> bytelist = new List<byte>();
                for (int i = 0; i < data.Length; i++)
                {
                    bytelist.AddRange(BitConverter.GetBytes(data[i]));
                }
                return BitConverter.ToInt32(bytelist.ToArray(), 0);
            }
        }

        public double GetAxisAbsPosition(byte addr)
        {
            try
            {
                ushort[] data = master.ReadHoldingRegisters(addr, 0x4202, 2);
                ushort[] data2 = master.ReadHoldingRegisters(addr, 0x4204, 1);
                List<byte> bytelist = new List<byte>();
                for (int i = 0; i < data.Length; i++)
                {
                    bytelist.AddRange(BitConverter.GetBytes(data[i]));
                }
                return (BitConverter.ToInt32(bytelist.ToArray(), 0) + data2[0] * 8388608) / 1677721.6;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int GetAxisErrorPosition(byte addr)
        {
            ushort[] data = master.ReadHoldingRegisters(addr, 0x609C, 2);
            List<byte> bytelist = new List<byte>();
            for (int i = 0; i < data.Length; i++)
            {
                bytelist.AddRange(BitConverter.GetBytes(data[i]));
            }
            return BitConverter.ToInt32(bytelist.ToArray(), 0);
        }
        public string GetAlarmCode(byte addr)
        {
            ushort[] data = master.ReadHoldingRegisters(addr, 0x4001, 1);
            List<byte> bytelist = new List<byte>();
            for (int i = 0; i < data.Length; i++)
            {
                bytelist.AddRange(BitConverter.GetBytes(data[i]));
            }
            return $"{bytelist[1]}.{bytelist[0]}";
        }

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用



        public void Uninit()
        {
            if (master != null)
            {
                master.Dispose();
            }
        }

        public void InitRet(bool ret)
        {
        }
        #endregion
    }

}
