/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2020 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnMain
// 文件名称    ：EfuHelper.cs
// =================================
// 创 建 者    ：guguangzhou
// 创建日期    ：2021/01/29 09:00:00 
// 功能描述    ：Efu功能类
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HYC.HTCommunication.SerialPortWrapper;
using HYC.SaturnMain.Params;
using System.Threading;
using HYC.HTLog;
using System.Drawing;
using HYC.HTPCBase.Extension.Device;
using HYC.HTFile.HTXML;

namespace HYC.SaturnMain.Devices.EfuController
{
    class EfuHelper
    {
        public event Action<string, int, bool> OnConnected;
        public event Action<string, string, Brush> OnShowMsg;
        private string ConfigPath = AppDomain.CurrentDomain.BaseDirectory + @"\Config\HardWare\EfuInfo.xml";
        public static EfuHelper Instance { get { return DeviceManager.Instance.Get("EfuHelper") as EfuHelper; } set { } }
        public string DeviceName { get ; set; } = "EfuHelper";
        public Dictionary<int, EfuControl> EfuDic = null;
      
        public void Init()
        {
            try
            {
                EfuDic = new Dictionary<int, EfuControl>();
                EfuInfo info = HTXMLSerializer.EntityFromXMLFile<EfuInfo>(ConfigPath);
                if (info != null )
                {
                    EfuControl efu = new EfuControl(info);
                    efu.OnConnected += OnConnected;
                    efu.Connect();
                    EfuDic.Add(info.EfuNum, efu);
                }
            }
            catch (Exception ex)
            {
                OnShowMsg?.Invoke("Hardware",$"初始化Efu异常，异常信息：{ex.Message}",Brushes.Red);
            }
        }
      
        public EfuControl Get(int index)
        {
            try
            {
                return EfuDic[index];
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void Uninit()
        {
            EfuDic?.Values.Select(p => p.Uninit()).ToArray();
        }

        public void InitRet(bool ret)
        {
        }
    }
}
