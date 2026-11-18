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
using HYC.SaturnAoi.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HYC.SaturnAoi.Devices.Power
{
    public class PowerControl
    {
        /// <summary>
        /// 连接状态事件
        /// </summary>
        public event Action<string, int, bool> OnConnected;
        /// <summary>
        /// 消息显示
        /// </summary>
        public event Action<string, string, Brush> OnShowMsg;
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
            try
            {
                Info = info;
                Power = HTPowerFactory.CreatePower(info.PowerVender);
                CommunicationInfo = new HTCommunicationInfo();
                CommunicationInfo.SocketInfo = new HTCommunication.SocketWrapper.HTSocketInfo { IP = info.IpAddress, Port = info.Port };
                Connect();
                Global.PowerControllers.Add(new PowerController(Power, null, null));
            }
            catch (Exception ex)
            {
                OnShowMsg?.Invoke(info.PowerName,$"PowerControl->构造函数异常，异常信息：{ex}",Brushes.Red);
            }
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
                    try
                    {
                        if (!Power.GetConnect())
                        {
                            IsConnected = Power.Init(CommunicationInfo);
                            OnConnected?.Invoke($"{Info.PowerName}", Info.PowerNum, IsConnected);
                        }
                    }
                    catch (Exception ex)
                    {
                        OnShowMsg?.Invoke(Info.PowerName, $"PowerControl->Connect异常，异常信息：{ex}", Brushes.Red);
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
            catch (Exception ex)
            {
                OnShowMsg?.Invoke(Info.PowerName, $"PowerControl->PowerOn异常，异常信息：{ex}", Brushes.Red);
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
            catch (Exception ex)
            {
                OnShowMsg?.Invoke(Info.PowerName, $"PowerControl->PowerOff异常，异常信息：{ex}", Brushes.Red);
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
