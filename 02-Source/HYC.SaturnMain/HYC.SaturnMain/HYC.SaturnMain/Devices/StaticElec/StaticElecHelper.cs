/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2022 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnMain.Devices.StaticElec
// 文件名称    ：StaticElecHelper
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
using HYC.HTPCBase.Extension.Device;
using HYC.HTPCBase.Extension.Entities;
using HYC.SaturnMain.Devices.EleStaticMes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HYC.SaturnMain.Devices.StaticElec
{
    public class StaticElecHelper : IDevice
    {
        public event Action<string, int, bool> OnConnected;  //设备连接/断开消息
        public event Action<string, string, Brush> OnShowMsg;
        public static StaticElecHelper Instance { get { return DeviceManager.Instance.Get("StaticElecHelper") as StaticElecHelper; } set { } }
        private string StaticElecConfigPath = AppDomain.CurrentDomain.BaseDirectory + @"\Config\HardWare\StaticElecInfo.xml";
        public Dictionary<int, StaticElecControl> StaticElecDic = new Dictionary<int, StaticElecControl>();
        public string DeviceName { get; set; } = "StaticElecHelper";
        public int StaticElecCount { get { return StaticElecDic.Count; } }
        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            Task.Factory.StartNew(()=> {
                try
                {
                    var list = GetInfo();
                    if (list != null && list.Count > 0)
                    {
                        for (int i = 0; i < list.Count; i++)
                        {
                            StaticElecControl staticElec = new StaticElecControl(list[i]);
                            staticElec.OnConnected += OnConnected;
                            staticElec.Connect();
                            StaticElecDic.Add(i, staticElec);
                        }
                    }
                    else
                    {
                        OnShowMsg?.Invoke("StaticElec", "配置文件为空", Brushes.Red);
                    }
                }
                catch (Exception ex)
                {
                    OnShowMsg?.Invoke("StaticElec", ex.Message, Brushes.Red);
                }
            });
       
        }
        /// <summary>
        /// 获取信息
        /// </summary>
        /// <returns></returns>
        private List<StaticElecInfo> GetInfo()
        {
            var result = HTXMLSerializer.EntityFromXMLFile<List<StaticElecInfo>>(StaticElecConfigPath);
            return result;
        }
        public double? GetValue(int index)
        {
            try
            {
                var scaner = StaticElecDic.Values.Where(p => p.Info.Num == index).First();
                return scaner?.GetValue();
            }
            catch (Exception ex)
            {
                OnShowMsg?.Invoke("Hardware", $"Scancode执行异常，异常信息：{ex.Message}", Brushes.Red);
                return null;
            }
        }
        public void Uninit()
        {
            StaticElecDic?.Values.Select(p => p.Uninit()).ToArray();
        }

        public void InitRet(bool ret)
        {
        }
    }
    public enum ErrorEnum
    {
        不正确的指令错误 = 00,
        数据长度错误 = 20,
        参数错误 = 21,
        参数错误2 = 22,
        通信错误 = 29,
        ID编号错误 = 65,
        扩展线错误 = 66,
        写入控制错误 = 67,
    }
}
