using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CIMBase.CCLink
{
    /// <summary>
    /// CCLink Card Functions
    /// </summary>
    class Mdfunc
    {
        /// <summary>
        /// 打开板卡
        /// </summary>
        /// <param name="chan">Channel number of communication line</param>
        /// <param name="mode">Dummy</param>
        /// <param name="path">Opend line path pointer</param>
        /// <returns>Return value</returns>
        [DllImport("MdFunc32.dll")]
        public static extern short mdOpen(short chan, short mode, ref int path);
        /// <summary>
        /// 关闭板卡
        /// </summary>
        /// <param name="path">Path of channel</param>
        /// <returns>Return value</returns>
        [DllImport("MdFunc32.dll")]
        public static extern short mdClose(int path);
        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="path">Path of channel</param>
        /// <param name="netno">Network number</param>
        /// <param name="stno">Station number</param>
        /// <param name="devtyp">Device type</param>
        /// <param name="devno">Start device number</param>
        /// <param name="size">Written byte size</param>
        /// <param name="data">written data</param>
        /// <returns>Return value</returns>
        [DllImport("MdFunc32.dll")]
        public static extern int mdSendEx(int path, int netno, int stno, int devtyp, int devno, ref int size, ushort[] data);
        /// <summary>
        /// 接收数据
        /// </summary>
        /// <param name="path">Path of channel</param>
        /// <param name="netno">Network number</param>
        /// <param name="stno">Station number</param>
        /// <param name="devtyp">Device type</param>
        /// <param name="devno">Start device number</param>
        /// <param name="size">Read byte size</param>
        /// <param name="data">Read data</param>
        /// <returns>Return value</returns>
        [DllImport("MdFunc32.dll")]
        public static extern int mdReceiveEx(int path, int netno, int stno, int devtyp, int devno, ref int size, ushort[] data);
        /// <summary>
        /// 读取当前的模式
        /// </summary>
        /// <param name="path">Path of channel</param>
        /// <param name="mode">Mode</param>
        /// <returns>Return value</returns>
        [DllImport("MdFunc32.dll")]
        public static extern short mdBdModRead(int path, ref short mode);
        /// <summary>
        /// 设置模式
        /// </summary>
        /// <param name="path">Path of channel</param>
        /// <param name="mode">Mode</param>
        /// <returns>Return value</returns>
        [DllImport("MdFunc32.dll")]
        public static extern short mdBdModSet(int path, short mode);
        /// <summary>
        /// 重置板卡
        /// </summary>
        /// <param name="path">Path of channel</param>
        /// <returns>Return value</returns>
        [DllImport("MdFunc32.dll")]
        public static extern short mdBdRst(int path);
        /// <summary>
        /// 刷新可编程控制器地址表
        /// </summary>
        /// <param name="path">Path of channel</param>
        /// <returns>Return value</returns>
        [DllImport("MdFunc32.dll")]
        public static extern short mdInit(int path);
        /// <summary>
        /// 读取板卡信息（Fixed value, Software version, Software Model Name, Hardware Model Name）
        /// </summary>
        /// <param name="path">Path of channel</param>
        /// <param name="buf">Read data</param>
        /// <returns>Return value</returns>
        [DllImport("MdFunc32.dll")]
        public static extern short mdBdVerRead(int path, short[] buf);
        /// <summary>
        /// 读取板卡状态（station number setting, board number setting, board identification, and I/O address setting information）
        /// </summary>
        /// <param name="path">Path of channel</param>
        /// <param name="buf">Read data</param>
        /// <returns>Return value</returns>
        [DllImport("MdFunc32.dll")]
        public static extern short mdBdSwRead(int path, short[] buf);
        /// <summary>
        /// 设置指定的bit设备为ON
        /// </summary>
        /// <param name="path">Path of channel</param>
        /// <param name="netno">Network number</param>
        /// <param name="stno">Station number</param>
        /// <param name="devtyp">Device type</param>
        /// <param name="devno">Start device number</param>
        /// <returns>Return value</returns>
        [DllImport("MdFunc32.dll")]
        public static extern int mdDevSetEx(int path, int netno, int stno, int devtyp, int devno);
        /// <summary>
        /// 重置指定的bit设备为OFF
        /// </summary>
        /// <param name="path">Path of channel</param>
        /// <param name="netno">Network number</param>
        /// <param name="stno">Station number</param>
        /// <param name="devtyp">Device type</param>
        /// <param name="devno">Start device number</param>
        /// <returns>Return value</returns>
        [DllImport("MdFunc32.dll")]
        public static extern int mdDevRstEx(int path, int netno, int stno, int devtyp, int devno);
        /// <summary>
        /// 写数据到目标站点的缓冲区（只能是CC-Link IE Field Network远程站点）
        /// </summary>
        /// <param name="path">Path of channel</param>
        /// <param name="netno">Network number</param>
        /// <param name="stno">Station number</param>
        /// <param name="offset">Start address</param>
        /// <param name="size">Written byte size</param>
        /// <param name="data">Written data</param>
        /// <returns>Return value</returns>
        [DllImport("MdFunc32.dll")]
        public static extern int mdRemBufWriteEx(int path, int netno, int stno, int offset, ref int size, short[] data);
        /// <summary>
        /// 从目标站点的缓冲区读数据（只能是CC-Link IE Field Network远程站点）
        /// </summary>
        /// <param name="path">Path of channel</param>
        /// <param name="netno">Network number</param>
        /// <param name="stno">Station number</param>
        /// <param name="offset">Start address</param>
        /// <param name="size">Written byte size</param>
        /// <param name="data">Written data</param>
        /// <returns>Return value</returns>
        [DllImport("MdFunc32.dll")]
        public static extern int mdRemBufReadEx(int path, int netno, int stno, int offset, ref int size, short[] data);
        /// <summary>
        /// 读取CPU名称代码
        /// </summary>
        /// <param name="path">Path of channel</param>
        /// <param name="stno">Station number</param>
        /// <param name="buf">Read data</param>
        /// <returns>Return value</returns>
        [DllImport("MdFunc32.dll")]
        public static extern short mdTypeRead(int path, short stno, ref short buf);
        /// <summary>
        /// 远程控制CPU运行、停止或暂停
        /// </summary>
        /// <param name="path">Path of channel</param>
        /// <param name="stno">Station number</param>
        /// <param name="buf">Command code</param>
        /// <returns>Return value</returns>
        [DllImport("MdFunc32.dll")]
        public static extern short mdControl(int path, short stno, short buf);
        /// <summary>
        /// 向随机指定的设备写入数据
        /// </summary>
        /// <param name="path">Path of channel</param>
        /// <param name="netno">Network number</param>
        /// <param name="stno">Station number</param>
        /// <param name="dev">Randomly-specified device</param>
        /// <param name="buf">Written data</param>
        /// <param name="bufsize">Dummy</param>
        /// <returns>Return value</returns>
        [DllImport("MdFunc32.dll")]
        public static extern int mdRandWEx(int path, int netno, int stno, int[] dev, ushort[] buf, int bufsize);
        /// <summary>
        /// 从随机指定的设备读取数据
        /// </summary>
        /// <param name="path">Path of channel</param>
        /// <param name="netno">Network number</param>
        /// <param name="stno">Station number</param>
        /// <param name="dev">Randomly-specified device</param>
        /// <param name="buf">Written data</param>
        /// <param name="bufsize">Dummy</param>
        /// <returns>Return value</returns>
        [DllImport("MdFunc32.dll")]
        public static extern int mdRandREx(int path, int netno, int stno, int[] dev, ushort[] buf, int bufsize);
        /// <summary>
        /// 等待事件发生，直至超时
        /// </summary>
        /// <param name="path">Path of channel</param>
        /// <param name="eventno">Waiting event number</param>
        /// <param name="timeout">Timeout value</param>
        /// <param name="signaledno">Driven event number</param>
        /// <param name="details">Event detail information</param>
        /// <returns>Return value</returns>
        [DllImport("MdFunc32.dll")]
        public static extern short mdWaitBdEvent(int path, short[] eventno, int timeout, ref short signaledno, short[] details);
        /// <summary>
        /// 读取板卡的LED信息
        /// </summary>
        /// <param name="path">Path of channel</param>
        /// <param name="buf">Read data</param>
        /// <returns>Return value</returns>
        [DllImport("MdFunc32.dll")]
        public static extern short mdBdLedRead(int path, short[] buf);
        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="path">Path of channel</param>
        /// <param name="stno">Station number</param>
        /// <param name="devtyp">Device type</param>
        /// <param name="devno">Start device number</param>
        /// <param name="size">Written byte size</param>
        /// <param name="data">Written data</param>
        /// <returns>Return value</returns>
        [DllImport("MdFunc32.dll")]
        public static extern short mdSend(int path, short stno, short devtyp, short devno, ref short size, ushort[] data);
        /// <summary>
        /// 接收数据
        /// </summary>
        /// <param name="path">Path of channel</param>
        /// <param name="stno">Station number</param>
        /// <param name="devtyp">Device type</param>
        /// <param name="devno">Start device number</param>
        /// <param name="size">Read byte size</param>
        /// <param name="data">Read data</param>
        /// <returns>Return value</returns>
        [DllImport("MdFunc32.dll")]
        public static extern short mdReceive(int path, short stno, short devtyp, short devno, ref short size, ushort[] data);
        /// <summary>
        /// 向随机指定的设备写入数据
        /// </summary>
        /// <param name="path">Path of channel</param>
        /// <param name="stno">Station number</param>
        /// <param name="dev">Randomly-specified device</param>
        /// <param name="buf">Written data</param>
        /// <param name="bufsize">Dummy</param>
        /// <returns>Return value</returns>
        [DllImport("MdFunc32.dll")]
        public static extern short mdRandW(int path, short stno, short[] dev, ushort[] buf, short bufsize);
        /// <summary>
        /// 从随机指定的设备读取数据
        /// </summary>
        /// <param name="path">Path of channel</param>
        /// <param name="stno">Station number</param>
        /// <param name="dev">Randomly-specified device</param>
        /// <param name="buf">Read data</param>
        /// <param name="bufsize">Number of bytes of read data</param>
        /// <returns>Return value</returns>
        [DllImport("MdFunc32.dll")]
        public static extern short mdRandR(int path, short stno, short[] dev, ushort[] buf, short bufsize);
        /// <summary>
        /// 设置指定的bit设备为ON
        /// </summary>
        /// <param name="path">Path of channel</param>
        /// <param name="stno">Station number</param>
        /// <param name="devtyp">Device type</param>
        /// <param name="devno">Start device number</param>
        /// <returns>Return value</returns>
        [DllImport("MdFunc32.dll")]
        public static extern short mdDevSet(int path, short stno, short devtyp, short devno);
        /// <summary>
        /// 重置指定的bit设备为OFF
        /// </summary>
        /// <param name="path">Path of channel</param>
        /// <param name="stno">Station number</param>
        /// <param name="devtyp">Device type</param>
        /// <param name="devno">Start device number</param>
        /// <returns>Return value</returns>
        [DllImport("MdFunc32.dll")]
        public static extern short mdDevRst(int path, short stno, short devtyp, short devno);
    }
}
