/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2022 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnMain.Devices.Rfid
// 文件名称    ：RfidHelper
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
using HYC.HTFile.HTXML;
using HYC.HTModbus;
using HYC.HTPCBase.Extension.Device;
using HYC.HTTools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace HYC.SaturnMain.Devices.Rfid
{
    public class RfidHelper : IDevice
    {
        public string DeviceName { get; set; } = "RfidHelper";
        public static RfidHelper Instance { get { return DeviceManager.Instance.Get("RfidHelper") as RfidHelper; } set { } }
        public event Action<string, int, bool> OnConnected;
        public event Action<string, string, Brush> OnShowMsg;
        private string ConfigPath = AppDomain.CurrentDomain.BaseDirectory + @"\Config\HardWare\RfidInfo.xml";
        public Dictionary<int, ModbusMaster> RfidDic = null;
        public int RfidCount { get { return RfidDic.Count; } }
        public void Init()
        {
            Task.Factory.StartNew(()=> {
                try
                {
                    List<RfidInfo> list = GetConfig();
                    RfidDic = new Dictionary<int, ModbusMaster>();
                    if (list != null && list.Count > 0)
                    {
                        for (int i = 0; i < list.Count; i++)
                        {
                            HTSocketInfo info = new HTSocketInfo { IP = list[i].IP, Port = list[i].Port };
                            ModbusMaster master = ModbusMaster.CreateTCP(info);
                            OnConnected?.Invoke($"Rfid{(i + 1).ToString("00")}", i, master != null);
                            RfidDic.Add(i, master);
                        }
                    }
                }
                catch (Exception ex)
                {
                    OnShowMsg?.Invoke(DeviceName, ex.Message, Brushes.Red);
                }
            });

        }
        public string Read(int masterNum = 0)
        {
            try
            {
                var temp = RfidDic[masterNum].ReadHoldingRegisters(255, 0, 8);
                string value = HTValueConverter.UshortArrayToAsciiString(temp, 0, temp.Length);
                return value;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public void Write(string value, int masterNum = 0)
        {
            try
            {
                int length = value.Length / 2;
                var array = HTValueConverter.AsciiStringToUshortArray(value, length);
                if (array==null||array.Length==0)
                {
                    array = new ushort[8];
                }
                RfidDic[masterNum].WriteMultipleRegisters(255, 0, array);
            }
            catch (Exception ex)
            {

            }
        }
    
        /// <summary>
        /// 读取配置信息
        /// </summary>
        /// <returns></returns>
        private List<RfidInfo> GetConfig()
        {
            List<RfidInfo> result = HTXMLSerializer.EntityFromXMLFile<List<RfidInfo>>(ConfigPath);
            return result;
        }
        public void InitRet(bool ret)
        {
        }

        public void Uninit()
        {
            RfidDic?.Values.Select(p => { p.Dispose(); return true; }).ToArray();
        }
    }
}
