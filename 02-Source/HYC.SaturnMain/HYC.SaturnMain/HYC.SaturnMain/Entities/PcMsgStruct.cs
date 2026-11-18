/*----------------------------------------------------------------
// Copyright (C)  Suzhou HYC Technology Co.,LTD
// 版权所有。
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnMain.Entities
// 文件名称    ：SocketDataDeal.cs
// =================================
// 创 建 者    ：guguangzhou
// 创建日期    ：2021/01/29 09:00:00 
// 功能描述    ：通讯协议功能类
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
using HYC.HTLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HYC.SaturnMain.Entities
{
    /// <summary>
    /// 自定义网络协议，处理粘包和拆包问题
    /// </summary>
    public class PcMsgStruct
    {
        private static int Crccode = 65433;
        public static int MsgHeaderLength = 14;//消息头长度
        /// <summary>
        /// 封包（帧头2字节 +CRC长度4字节 + 命令4字节 +消息体长度4字节+  消息体N字节+帧尾2字节）
        /// </summary>
        /// <param name="command">命令</param>
        /// <param name="messageBody">消息内容（string数组）</param>
        /// <returns>返回构建完整的数据包</returns>
        public static byte[] GetMsgBuffer(int command, string messageBody,byte[]datas=null)
        {
            try
            {
                //消息头各个分类数据转换为字节数组非字符型数据需先转换为网络序  HostToNetworkOrder:主机序转网络序）
                byte[] stxByte = new byte[] { 0x00, 0x99 };//帧头，2bytes
                byte[] etxByte = new byte[] { 0x99, 0x00 };//帧尾，2bytes
                byte[] crccodeByte = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(Crccode));//CRC校验，4bytes
                byte[] commandByte = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(command));//主命令，4bytes
                byte[] messageBodyByte = null;
                if (datas==null)
                {
                   messageBodyByte = Encoding.Default.GetBytes(messageBody);//消息体，N bytes
                }
                else
                {
                    messageBodyByte = datas;
                }
                byte[] messageBodyLengthByte = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(messageBodyByte.Length));//消息体长度，4bytes
                                                                                                                           //定义总数据包（帧头2字节 +CRC长度4字节 + 主命令4字节 +消息体长度4字节+  消息体+帧尾2字节）
                byte[] totalByte = new byte[16 + messageBodyByte.Length];
                stxByte.CopyTo(totalByte, 0);
                crccodeByte = InversionArray(crccodeByte);
                crccodeByte.CopyTo(totalByte, 2);
                commandByte = InversionArray(commandByte);
                commandByte.CopyTo(totalByte, 6);
                messageBodyLengthByte = InversionArray(messageBodyLengthByte);
                messageBodyLengthByte.CopyTo(totalByte, 10);
                messageBodyByte.CopyTo(totalByte, 14);
                etxByte.CopyTo(totalByte, 14 + messageBodyByte.Length);
                return totalByte;
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("PcMsgStruct").Info($"{new StackFrame(0).GetMethod().Name}执行异常，异常信息：{ex}");
                return null;
            }
        }
    
        /// <summary>
        /// 高低位转换
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        private static byte[] InversionArray(byte[]array)
        {
            for (int i = 0; i < array.Length / 2; i++)
            {               
                byte temp;
                temp = array[i];
                array[i] = array[array.Length - i - 1];
                array[array.Length - i - 1] = temp;
            }
            return array;
        }
        /// <summary>
        /// 解析消息包
        /// </summary>
        /// <param name="Head">消息头</param>
        /// <param name="Body">消息体</param>
        public static string UnpackData(byte[] message,ref string msg,ref int command)
        {
            try
            {
                byte[] bytes = new byte[2];
                Array.Copy(message, 0, bytes, 0, 2);//stx
                short stx = BitConverter.ToInt16(bytes, 0);
                bytes = new byte[4];
                Array.Copy(message, 2, bytes, 0, 4);//crc
                int crcCode = BitConverter.ToInt32(bytes, 0);
                bytes = new byte[4];
                Array.Copy(message, 6, bytes, 0, 4);//command
                 command = BitConverter.ToInt32(bytes, 0);
                bytes = new byte[4];
                Array.Copy(message, 10, bytes, 0, 4);//message length
                int msgLength = BitConverter.ToInt32(bytes, 0);
                bytes = new byte[msgLength];
                Array.Copy(message, 14, bytes, 0, msgLength);//message
                msg = Encoding.Default.GetString(bytes);
                return msg;
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("PcMsgStruct").Info($"{new StackFrame(0).GetMethod().Name}执行异常，异常信息：{ex}");
                return null;
            }
        }
    }
    public enum ClientPCName
    {
        AoiPC1,
        AoiPC2,
        TesterPC,
        DFS,
    }
    public enum Command
    {
        /// <summary>
        /// 心跳
        /// </summary>
        HeartBeat,
        /// <summary>
        /// 时间同步
        /// </summary>
        TimeSync,
        UserSync,
        /// <summary>
        /// 机种操作(新增，切换，删除)
        /// </summary>
        RecipeSync,
        Start,
        Stop,
        Pause,
        DfsUpload,
        UpdateStepID,
        /// <summary>
        /// 机种操作结果回复
        /// </summary>
        RecipeSyncAck,
        /// <summary>
        /// 检测请求
        /// </summary>
        DetectReq = 100,
        DetectResult,
        PowerControlReq,
        PowerControlResult,
        SwitchPatternReq,
        SwitchPatternResult,
        DfsPara,
        /// <summary>
        /// 单列Tester测试
        /// </summary>
        OneColumnTest = 200,
        OneColumnTestCom,
        LoadTesterRecipe,
        NotifyProcessId,
        NotifyGlassId,
        RequestGlassID,
        RequestGlassIDAck,
        OneGlassTestEnd,
        UpstreamGlassInfo,
        NGCountAlarm,
        TesterModeSwitch,
        TesterModeSwitchResult,
        TesterPowerControl,
        OneCellDetectEnd,
    }
    public enum CommunicationType
    {
        Send,
        Receive
    }

}
