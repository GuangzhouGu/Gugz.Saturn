/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2023 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnAoi.Devices.Tester
// 文件名称    ：ELPTesterHelper
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
using HYC.HTFile.HTXML;
using HYC.SaturnAoi.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HYC.SaturnAoi.Devices.Tester
{
    class ELPTesterHelper : IDevice
    {
        public string DeviceName { get ; set; } = "ELPTester";
        public bool IsInit { get; set; }
        public event Action<string, int, bool> OnConnected;
        public event Action<string, string, Brush> OnShowMsg;
        public static ELPTesterHelper Instance { get { return DeviceManager.Instance.Get("ELPTester") as ELPTesterHelper; } set { } }
        private string ConfigPath = AppDomain.CurrentDomain.BaseDirectory + @"\Config\Hardware\ELPTesterInfo.xml";
        Dictionary<int, ELPTesterBase> ELPTesterDic = null;
        public void Init()
        {
            try
            {
                ELPTesterDic = new Dictionary<int, ELPTesterBase>();
                List<ELPTesterInfo> list = GetConfig();
                if (list != null && list.Count > 0)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        ELPTesterBase tester = null;
                        if (list[i].CommunicationType==CommunicationType.Protocol)
                        {
                            tester = new ELPTesterControl(list[i]);
                        }
                        else
                        {
                            tester = new ELPTesterUseDll(list[i]);
                        }
                        tester.OnConnected += OnConnected;
                        tester.OnShowMsg += OnShowMsg;
                        ELPTesterDic.Add(i, tester);
                        Global.PatternProducers.Add(new PatternProducer(tester));
                        Thread.Sleep(100);
                    }
                }
            }
            catch (Exception ex)
            {
                OnShowMsg?.Invoke(DeviceName, $"ELPTesterHelper->Init执行异常，异常信息：{ex.Message}", Brushes.Red);
            }
        }
        public ELPTesterBase GetTester(int testerNum)
        {
            if (ELPTesterDic!=null&&ELPTesterDic.Values.Count>0)
            {
                return ELPTesterDic[testerNum];
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 读取配置信息
        /// </summary>
        /// <returns></returns>
        private List<ELPTesterInfo> GetConfig()
        {
            List<ELPTesterInfo> result = HTXMLSerializer.EntityFromXMLFile<List<ELPTesterInfo>>(ConfigPath);
            return result;
        }
        public void InitRet(bool ret)
        {
            
        }
        public int TesterCount { get { return ELPTesterDic.Values.Count; } }
        public void Uninit()
        {
            ELPTesterDic?.Values.Select(p => { p.DisConnect(); return true; }).ToArray();
        }
    }
}
