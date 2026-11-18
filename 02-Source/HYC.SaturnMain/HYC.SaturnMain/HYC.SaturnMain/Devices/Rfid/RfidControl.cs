/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2022 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnMain.Devices.Rfid
// 文件名称    ：RfidControl
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
using HYC.HTModbus;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;

namespace HYC.SaturnMain.Devices.Rfid
{
    public class RfidControl
    {
        public event Action<string, Brush> OnShowMessage;   //显示通信信息记录
        public event Action<string, int, bool> OnConnected;
        private bool SetResult = false;
        public bool IsConnected { get; set; }
        HTSocketInfo SocketInfo = null;
        ModbusMaster Master = null;
        public RfidControl(RfidInfo info)
        {
            SocketInfo = new HTSocketInfo { IP = info.IP, Port = info.Port };
            Master = ModbusMaster.CreateTCP(SocketInfo);
        }

    }
}
