/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2023 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnAoi.Devices.MultiAngle
// 文件名称    ：MultiAngleHelper
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
using CommonLib.Device;
using HYC.HTCommunication.SerialPortWrapper;
using HYC.HTFile.HTXML;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HYC.SaturnAoi.Devices.MultiAngle
{
    public class MultiAngleHelper : IDevice
    {
        public event Action<string, int, bool> OnConnected;  //设备连接/断开消息
        public event Action<string, string, Brush> OnShowMsg;
        public static MultiAngleHelper Instance { get { return DeviceManager.Instance.Get("MultiAngle") as MultiAngleHelper; } set { } }
     
        private string MultiAngleConfigPath = AppDomain.CurrentDomain.BaseDirectory + @"\Config\Hardware\MultiAngleInfo.xml";
        Dictionary<int, MultiAngleControl> MultiAngleDic = null;
        public string DeviceName { get; set; } = "MultiAngle";
        public bool IsInit { get ; set ; }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            try
            {
                MultiAngleDic = new Dictionary<int, MultiAngleControl>();
                var list = GetInfo();
                if (list != null && list.Count > 0)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        var control = new MultiAngleControl(list[i]);
                        control.OnConnected += OnConnected;
                        control.OnShowMsg += OnShowMsg;
                        control.Connect();
                        MultiAngleDic.Add(i, control);
                    }
                }
            }
            catch (Exception ex)
            {
                OnShowMsg?.Invoke("MultiAngleHelper", $"MultiAngleHelper->Init方法执行异常，异常信息：{ex}", Brushes.Red);
            }
        }
        /// <summary>
        /// 获取信息
        /// </summary>
        /// <returns></returns>
        private List<MultiAngleInfo> GetInfo()
        {
            var result = HTXMLSerializer.EntityFromXMLFile<List<MultiAngleInfo>>(MultiAngleConfigPath);
            return result;
        }
        public MultiAngleControl Get(int testerNum)
        {
            if (MultiAngleDic != null && MultiAngleDic.Values.Count > 0)
            {
                return MultiAngleDic[testerNum];
            }
            else
            {
                return null;
            }
        }

        public void Uninit()
        {
            MultiAngleDic.Values.Select(p=>p.Unint()).ToArray();
        }

        public void InitRet(bool ret)
        {
        }
    }
}
