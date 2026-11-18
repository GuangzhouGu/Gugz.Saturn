/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2023 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.TPApiUnit.Devices.AoiCamera
// 文件名称    ：CaptureCardHelper
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
using HYC.HTFile;
using HYC.HTFile.HTXML;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;

namespace HYC.TPApiUnit.Devices.AoiCamera
{
    public class CaptureCardHelper : IDevice
    {
        /// <summary>
        /// 当前aoi相机实例
        /// </summary>
        public static CaptureCardHelper Instance { get { return DeviceManager.Instance.Get("CaptureCard") as CaptureCardHelper; } set { } }
        public event Action<string, int, bool> OnConnected;
        //消息显示事件
        public event Action<string, string, Brush> OnShowMsg;
        public string DeviceName { get; set; } = "CaptureCard";
        public bool IsInit { get ; set; }
        public Dictionary<int, CaptureCardControl> CaptureCardDic = null;
        private string CaptureCardConfigPath = AppDomain.CurrentDomain.BaseDirectory + @"\Config\Hardware\CaptureCardInfo.xml";
        public void Init()
        {
            try
            {
                CaptureCardDic = new Dictionary<int, CaptureCardControl>();
                var list = GetCaptureCardConfig();
                if (list != null && list.Count > 0)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        var card = new CaptureCardControl(list[i]);
                        card.OnConnected += OnConnected;
                        card.OnShowMsg += OnShowMsg;
                        card.Init();
                        CaptureCardDic.Add(i, card);
                    }
                }
            }
            catch (Exception ex)
            {
                OnShowMsg?.Invoke("CaptureCardHelper", $"CaptureCardHelper->Init方法执行异常，异常信息：{ex.ToString()}", Brushes.Red);
            }
        }
        private List<CaptureCardInfo> GetCaptureCardConfig()
        {
            List<CaptureCardInfo> result = HTXmlHelper.EntityFromXMLFile<List<CaptureCardInfo>>(CaptureCardConfigPath);
            return result;
        }
        public CaptureCardControl GetCard(int index)
        {
            if (CaptureCardDic.Keys.Contains(0))
            {
                return CaptureCardDic[0];
            }
            else
            {
                return null;
            }
        }
        public int CaptureCardCount { get { return CaptureCardDic.Values.Count; } }
        public void Uninit()
        {
            CaptureCardDic.Values.Select(p => p.Uninit()).ToArray();
        }

        public void InitRet(bool ret)
        {

        }
    }
}
