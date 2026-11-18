/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2021 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：CommonLib.Device
// 文件名称    ：DeviceManager.cs
// =================================
// 创 建 者    ：guguangzhou
// 创建日期    ：2021/11/11 09:00:00 
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
using CommonLib.Entities;
using HYC.HTFile.HTINI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Device
{
    public class DeviceManager
    {
        /// <summary>
        /// 硬件连接消息提示
        /// </summary>
        public event Action<string, string, Brush> OnShowMsg;
        /// <summary>
        /// 连接状态改变
        /// </summary>
        public event Action<string, int, bool> OnConnected;
        /// <summary>
        /// 实例自身
        /// </summary>
        public static DeviceManager Instance = null;
        /// <summary>
        /// 所有继承IDevice的硬件
        /// </summary>
        List<IDevice> DeviceList = null;
        static DeviceManager()
        {
            Instance = new DeviceManager();
        }
        /// <summary>
        /// 初始化继承了IDevice所有硬件
        /// </summary>
        /// <returns></returns>
        public bool Init()
        {
            try
            {
                bool ret = true;
                DeviceList = ClassHelper.GetChildClass<IDevice>();
                DeviceList?.Select(p =>
                {
                    p.OnConnected += new Action<string, int, bool>((m, n, o) => { OnConnected(m, n, o); });
                    p.OnShowMsg += OnShowMsg;
                    p.Init();
                    p.InitRet(true);
                    return true;
                }).ToArray();
                return ret;
            }
            catch (Exception ex)
            {
                OnShowMsg?.Invoke("DeviceManager", $"硬件初始化异常，异常信息：{ex.ToString() }", Brushes.Red);
                return false;
            }
        }
        /// <summary>
        /// 获取当前硬件实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="deviceName"></param>
        /// <returns></returns>
        public IDevice Get<T>(T deviceName)
        {
            try
            {
                IDevice device = DeviceList.Where(p => p.DeviceName == deviceName.ToString()).FirstOrDefault();
                return device;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// 释放所有硬件连接
        /// </summary>
        public void Uninit()
        {
            DeviceList?.Select(p =>
            {
                p.Uninit();
                return true;
            }).ToArray();
        }
    }
}
