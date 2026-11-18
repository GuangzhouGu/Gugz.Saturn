/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2021 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.Mt2Main.Devices.Power
// 文件名称    ：PowerControl.cs
// =================================
// 创 建 者    ：guguangzhou
// 创建日期    ：2021/05/10 09:00:00 
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
using HYC.HTPower;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HYC.SaturnMain.Devices.Power
{
    public class PowerControl
    {
        /// <summary>
        /// 连接状态事件
        /// </summary>
        public event Action<string, int, bool> OnConnected;
        /// <summary>
        /// //光源信息
        /// </summary>
        public PowerInfo Info { get; set; }
        /// <summary>
        /// //连接状态
        /// </summary>
        public bool IsConnected { get; set; }
        /// <summary>
        /// // 光源实例
        /// </summary>
        IHTPower Power = null;
        /// <summary>
        /// 连接类型 socket 或者 串口
        /// </summary>
        HTCommunicationInfo CommunicationInfo = null;
        public PowerControl(PowerInfo info)
        {
            Info = info;
            Power = HTPowerFactory.CreatePower(info.PowerVender);
            CommunicationInfo = new HTCommunicationInfo();
            CommunicationInfo.socketInfo.IP = info.IpAddress;
            CommunicationInfo.socketInfo.Port = info.Port;

        }
        /// <summary>
        /// 连接光源
        /// </summary>
        public void Connect()
        {
            var t = new Thread(() =>
            {
                while (true)
                {
                    if (!Power.GetConnect())
                    {
                        IsConnected = Power.Init(CommunicationInfo);
                        OnConnected?.Invoke($"Power{(Info.PowerNum + 1).ToString("00")}", Info.PowerNum, IsConnected);
                    }
                    Thread.Sleep(1000);
                }
            });
            t.IsBackground = true;
            t.Start();
        }
        /// <summary>
        /// 开启光源
        /// </summary>
        /// <param name="chanel"></param>
        public void PowerOn(HTPowerChannel chanel)
        {
            try
            {
                Power.PowerOn(chanel);
            }
            catch (Exception)
            {
            }
        }
        /// <summary>
        /// 关闭光源
        /// </summary>
        /// <param name="chanel"></param>
        public void PowerOff(HTPowerChannel chanel)
        {
            try
            {
                Power.PowerOff(chanel);
            }
            catch (Exception)
            {
            }
        }
        /// <summary>
        /// 获取电流
        /// </summary>
        /// <param name="chanel"></param>
        /// <returns></returns>
        public float BrightnessGet(HTPowerChannel chanel)
        {
            float result = 0;
            Power.BrightnessGet(chanel, ref result);
            return result;
        }
        /// <summary>
        /// 设置电流
        /// </summary>
        /// <param name="chanel"></param>
        /// <param name="value"></param>
        public void BrightnessSet(HTPowerChannel chanel, float value)
        {
            Power.BrightnessSet(chanel, value);
        }
        /// <summary>
        /// 释放光源连接
        /// </summary>
        /// <returns></returns>
        public bool Uninit()
        {
            Power.PowerOff(HTPowerChannel.POWER_CHANNEL_ALL);
            bool ret = Power.Uninit();
            return ret;
        }
    }
}
