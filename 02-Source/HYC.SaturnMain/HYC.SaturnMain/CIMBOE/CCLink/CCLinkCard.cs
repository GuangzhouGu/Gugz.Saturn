using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIMBase.CCLink
{
    /// <summary>
    /// CCLine板卡类
    /// </summary>
    public class CCLinkCard
    {
        private short chan = 151; //Channel number of communication line
        private short mode = 0;   //Dummy
        private int path = 0;     //Opened line path pointer
        private int netno = 0;    //Network number
        private int stno = 255;   //Station number


        public short Init()
        {
           return  Mdfunc.mdOpen(chan, mode, ref path);
        }
        /// <summary>
        /// 初始化CCLine板卡并打开
        /// </summary>
        /// <param name="chan">通道</param>
        /// <param name="mode">模式</param>
        /// <param name="netno"></param>
        /// <param name="stno"></param>
        /// <returns></returns>
        public short Init(short chan, short mode, int netno, int stno)
        {
            this.chan = chan;
            this.mode = mode;
            this.netno = netno;
            this.stno = stno;
            try
            {
                return Mdfunc.mdOpen(chan, mode, ref path);
            }
            catch(Exception ex)
            {
                return -1;
            }
            return -1;
        }

        public int SetBit(int addr)
        {
            try
            {
                return Mdfunc.mdDevSetEx(path, netno, stno, (int)CCLinkDeviceType.LinkRelay, addr);
            }
            catch (Exception)
            {
                return -1;
            }
        }
        /// <summary>
        /// 重新bit
        /// </summary>
        /// <param name="addr"></param>
        /// <returns></returns>
        public int ResetBit(int addr)
        {
            try
            {
                return Mdfunc.mdDevRstEx(path, netno, stno, (int)CCLinkDeviceType.LinkRelay, addr);
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public int SetMultiBit(int addr, int length)
        {
            int ret = 0;
            for (int i = 0; i < length; i++)
            {
                ret = SetBit(addr + i);
                if (ret != 0)
                {
                    break;
                }
            }
            return ret;
        }

        public int ResetMultiBit(int addr, int length)
        {
            int ret = 0;
            for (int i = 0; i < length; i++)
            {
                ret = ResetBit(addr + i);
                if (ret != 0)
                {
                    break;
                }
            }
            return ret;
        }

        public int SetResetBit(int addr, bool value)
        {
            int ret = 0;
            if (value == true)
                ret = SetBit(addr);
            else
                ret = ResetBit(addr);
            return ret;
        }

        public int SetResetMultiBit(int addr, params bool[] values)
        {
            int ret = 0;
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] == true)
                {
                    ret = SetBit(addr + i);
                }
                else
                {
                    ret = ResetBit(addr + i);
                }

                if (ret != 0)
                {
                    break;
                }
            }
            return ret;
        }
        /// <summary>
        /// 向CCLine对应写入
        /// </summary>
        /// <param name="addr">写入地址</param>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public int WriteData(int addr, ushort data)
        {
            try
            {
                ushort[] tmpData = new ushort[1];
                int size = tmpData.Length * 2;
                tmpData[0] = data;
                return Mdfunc.mdSendEx(path, netno, stno, (int)CCLinkDeviceType.LinkRegister, addr, ref size, tmpData);
            }
            catch (Exception)
            {
                return -1;
            }
        }
        /// <summary>
        /// 向CCLINE写入数据以便交个下游设备
        /// </summary>
        /// <param name="addr">写入地址</param>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public int WriteData(int addr, ushort[] data)
        {
            try
            {
                int size = data.Length * 2;
                return Mdfunc.mdSendEx(path, netno, stno, (int)CCLinkDeviceType.LinkRegister, addr, ref size, data);
            }
            catch (Exception)
            {
                return -1;
            }
        }
        /// <summary>
        /// 获取某个偏移量地址的状态位（Bit）
        /// </summary>
        /// <param name="addr">偏移地址</param>
        /// <returns></returns>
        public bool ReadBit(int addr)
        {
            try
            {
                int size = 2;
                ushort[] data = new ushort[1];
                int tmp = addr % 16;
                Mdfunc.mdReceiveEx(path, netno, stno, (int)CCLinkDeviceType.LinkRelay, addr - tmp, ref size, data);
                bool tmpData = Convert.ToBoolean(data[0] & (0x01 << tmp));
                return tmpData;
            }
            catch (System.Exception ex)
            {}
            return false;
        }

        public bool[] ReadMultiBit(int addr, int len)
        {
            bool[] tmpData = new bool[len];
            try
            {
                for (int i = 0; i < len; i++)
                {
                    tmpData[i] = ReadBit(addr + i);
                }
            }
            catch (System.Exception ex)
            { }
            return tmpData;
        }

        public bool[] ReadMultiBit2(int addr, int len)
        {
            bool[] result = new bool[len];
            try
            {
                var size = (int)Math.Ceiling(1.0 * len / 8);
                ushort[] data = new ushort[(int)Math.Ceiling(1.0 * len / 16)];
                int tmp = addr % 16;
                Mdfunc.mdReceiveEx(path, netno, stno, (int)CCLinkDeviceType.LinkRelay, addr, ref size, data);
                for (int i = 0; i < len; i++)
                {
                    result[i] = Convert.ToBoolean(data[i / 16] & (0x01 << (i % 16)));
                }
            }
            catch (System.Exception ex)
            { }
            return result;
        }

        public ushort ReadData(int addr)
        {
            int size = 2;
            ushort[] data = new ushort[1];
            try
            {
                Mdfunc.mdReceiveEx(path, netno, stno, (int)CCLinkDeviceType.LinkRegister, addr, ref size, data);
            }
            catch (System.Exception ex)
            { }
            return data[0];
        }

        public ushort[] ReadMultiData(int addr, int len)
        {
            int size = 2 * len;
            ushort[] data = new ushort[len];
            try
            {
                Mdfunc.mdReceiveEx(path, netno, stno, (int)CCLinkDeviceType.LinkRegister, addr, ref size, data);
            }
            catch (System.Exception ex)
            { }
            return data;
        }
    }

    enum CCLinkDeviceType
    {
        LinkInput = 0x01,
        LinkOutput = 0x02,
        LinkSpecialRelay = 0x05,
        LinkSpecialRegister = 0x0E,
        LinkRelay = 0x17,
        LinkRegister = 0x18
    }

    enum CCLinkChannel
    {
        ControllerNetworkChan1 = 151,
        ControllerNetworkChan2 = 152,
        ControllerNetworkChan3 = 153,
        ControllerNetworkChan4 = 154
    }

    enum CCLinkMode
    {
        Online = 0x00,
        Offline = 0x02,
        StationToStationTest = 0x05,
        LoopTest = 0x06,
        SelfLoopbackTest = 0x07,
        HardwareTest = 0x09,
        BusInterfaceTest = 0x0E
    }
}
