/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2021 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.Mt2Main.Devices.Power
// 文件名称    ：PowerHelper.cs
// =================================
// 创 建 者    ：guguangzhou
// 创建日期    ：2021/05/08 09:00:00 
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
using CommonLib.Device;
using HYC.HTFile.HTXML;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYC.SaturnAoi.Devices.Power
{
    public class PowerHelper:IDevice
    {
        /// <summary>
        /// 设备连接/断开消息
        /// </summary>
        public event Action<string, int, bool> OnConnected;  
        /// <summary>
        /// 消息显示
        /// </summary>
        public event Action<string, string, Brush> OnShowMsg;
        /// <summary>
        /// 硬件名字
        /// </summary>
        public string DeviceName { get; set; } = "Power";
        /// <summary>
        /// 当前光源硬件实例
        /// </summary>
        public static PowerHelper Instance { get { return DeviceManager.Instance.Get("Power") as PowerHelper; } set { } }
        /// <summary>
        /// 光源配置信息存放
        /// </summary>
        private string PowerConfigPath = AppDomain.CurrentDomain.BaseDirectory + @"Config\Hardware\PowerInfo.xml";
        public Dictionary<int, PowerControl> PowerDic = null;
         List<PowerInfo> PowerInfoList = null;
        /// <summary>
        /// 光源数量
        /// </summary>
        public int PowerCount { get { return PowerInfoList.Count; } }
        /// <summary>
        /// 是否初始化完成
        /// </summary>
        public bool IsInit { get; set; } = true;
        static PowerHelper()
        {
            Instance = new PowerHelper();
        }
        /// <summary>
        /// 初始化光源连接
        /// </summary>
        /// <returns></returns>
        public void Init()
        {
            try
            {
                PowerDic = new Dictionary<int, PowerControl>();
                PowerInfoList = GetPowerConfig();
                if (PowerInfoList != null && PowerInfoList.Count > 0)
                {
                    for (int i = 0; i < PowerInfoList.Count; i++)
                    {
                        PowerControl Power = new PowerControl(PowerInfoList[i]);
                        Power.OnConnected += OnConnected;
                        Power.OnShowMsg += OnShowMsg;
                        PowerDic.Add(i, Power);
                    }
                }
            }
            catch (Exception ex)
            {
                OnShowMsg?.Invoke(DeviceName,$"PowerHelper->Init执行异常，异常信息：{ex}", Brushes.Red);
            }
        }
        /// <summary>
        /// 获取当前光源实例
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public PowerControl GetPowerByName(string name)
        {
            return PowerDic.Values?.Where(p => p.Info.PowerName == name).First();
        }
        /// <summary>
        /// 获取当前光源实例
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public PowerControl GetPowerByNum(int index)
        {
            try
            {
                return PowerDic.Values?.Where(p => p.Info.PowerNum == index).First();
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// 释放光源实例
        /// </summary>
        /// <returns></returns>
        public void Uninit()
        {
            PowerDic?.Values.Select(p => p.Uninit()).ToArray();
        }
        /// <summary>
        /// 读取相机配置
        /// </summary>
        /// <returns></returns>
        private List<PowerInfo> GetPowerConfig()
        {
            List<PowerInfo> result = HTXMLSerializer.EntityFromXMLFile<List<PowerInfo>>(PowerConfigPath);
            return result;
        }

        public void InitRet(bool ret)
        {
        }


    }
}
