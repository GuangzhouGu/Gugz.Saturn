/*----------------------------------------------------------------
// Copyright (C)  Suzhou HYC Technology Co.,LTD
// 版权所有。
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：E2000_Main.Devices.Scaner
// 文件名称    ：ScanerHelper.cs
// =================================
// 创 建 者    ：guguangzhou
// 创建日期    ：2021/11/10 09:00:00 
// 功能描述    ：扫码枪帮助类
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
using HYC.HTPCBase.Extension.Device;
using HYC.HTFile.HTINI;
using HYC.HTFile.HTXML;
using HYC.HTLog;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace HYC.SaturnMain.Devices.Scaner
{
    public class ScanerHelper:IDevice
    {
        public static ScanerHelper Instance{ get { return DeviceManager.Instance.Get("ScanerHelper") as ScanerHelper; } set { } }
        public  int BarcodeCount = 1;
        public event Action<string, int, bool> OnConnected;  //设备连接/断开消息
        public event Action<string, string, Brush> OnShowMsg;
        private string ConfigPath = AppDomain.CurrentDomain.BaseDirectory + @"\Config\HardWare\ScanerInfo.xml";
        public string DeviceName { get; set; } = "ScanerHelper";
        public Dictionary<ScanerEnum, ScanerControl> ScanerDic = null;
        public int ScanerCount { get { return ScanerDic.Count; } }
        /// <summary>
        /// 初始化扫码枪
        /// </summary>
        /// <returns></returns>
        public void Init()
        {
            try
            {
                ScanerDic = new Dictionary<ScanerEnum, ScanerControl>();
                List<ScanerInfo> list = GetConfig();
                if (list != null && list.Count > 0)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        ScanerControl scaner = new ScanerControl(list[i]);
                        scaner.OnConnected += OnConnected;
                        scaner.OnShowMsg += OnShowMsg;
                        scaner.Connect();
                        ScanerDic.Add((ScanerEnum)i, scaner);
                    }
                }
            }
            catch (Exception ex)
            {
                OnShowMsg?.Invoke("Scaner", $"扫码枪初始化异常，异常信息：{ex.Message}", Brushes.Red);
            }
        }
        /// <summary>
        /// 读取配置信息
        /// </summary>
        /// <returns></returns>
        private List<ScanerInfo> GetConfig()
        {
            List<ScanerInfo> result = HTXMLSerializer.EntityFromXMLFile<List<ScanerInfo>>(ConfigPath);
            return result;
        }
        public void Uninit()
        {
            ScanerDic?.Values.Select(p => p.Uninit()).ToArray();
        }
        public string Scancode(int index)
        {
            try
            {
                var scaner = ScanerDic.Values.Where(p => p.Info.ScanerNum == index).First();
                string ret = scaner?.Scancode().Replace("\r\n","");
                return ret;
            }
            catch (Exception ex)
            {
                OnShowMsg?.Invoke("Scaner", $"Scancode执行异常，异常信息：{ex.Message}", Brushes.Red);
                return "NoRead";
            }
        }
     
   
        public void InitRet(bool ret)
        {
        }
    }
  
    public enum ScanerEnum
    {
        Scaner1,
        Scaner2,
    }
}
