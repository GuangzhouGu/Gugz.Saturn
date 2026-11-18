/*----------------------------------------------------------------
// Copyright (C) Suzhou HYC Electronic Technology Co.,LTD
// 版权所有。
//
// =================================
// CLR版本     ：4.0.30319.42000
// 命名空间    ：HYC.AutoPosition.UI.Entities
// 文件名称    ：PosPlatform.cs
// =================================
// 创 建 者    ：yangkang
// 创建日期    ：2020/1/20 11:11:54 
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
using HYC.AutoPosition.Interface;
using HYC.AutoPosition.UI.Helper;
using HYC.AutoPosition.UI.View;
using HYC.HTFile.HTXML;
using HYC.HTXYT;
using HYC.HTXYT.Base;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYC.AutoPosition.UI.Entities
{
    /// <summary>
    /// 对位平台管理类
    /// </summary>
    static class PosPlatform
    {
        // XYT平台管理类
        private static XYTManager manager = null;
        // 平台运动的安全条件
        private static Dictionary<string, bool> dictSafetyFlag = new Dictionary<string, bool>();
        /// <summary>
        /// 初始化
        /// </summary>
        public static void Init()
        {
            try
            {
                List<PlatInfo> lstPlatInfos = new List<PlatInfo>();
                //从本地文件加载平台设置
                List<PlatInfo> lstPlatInfosTmp = new List<PlatInfo>();
                string filename = @"Config\XYT.xml";
                if (File.Exists(filename))
                {
                    lstPlatInfosTmp = HTXMLSerializer.EntityFromXMLFile<List<PlatInfo>>(filename);
                }
                //筛选执行机构为对位平台的组
                var lstGroup = Global.Setting.ListGroup
                    .Where(p => p.MoveExecutorType == MoveExecutorType.XYTPlatform)
                    .ToList();
                for (int i = 0; i < lstGroup.Count; i++)
                {
                    if (lstPlatInfosTmp.Count > i)
                    {
                        //如果配置文件中存在项，直接添加
                        lstPlatInfos.Add(lstPlatInfosTmp[i]);
                        lstPlatInfos[i].PlatName = lstGroup[i].GroupName;
                    }
                    else
                    {
                        //如果配置文件中不存在项，新建后添加
                        PlatInfo info = new PlatInfo()
                        {
                            PlatName = lstGroup[i].GroupName,
                            AxisInfos = new List<AxisInfo>()
                            {
                                new AxisInfo(){ Address = 3 * i + 1, AxisType = AxisType.X },
                                new AxisInfo(){ Address = 3 * i + 2, AxisType = AxisType.Y },
                                new AxisInfo(){ Address = 3 * i + 3, AxisType = AxisType.T }
                            }
                        };
                        lstPlatInfos.Add(info);
                    }
                }
                //如果需要连接对位平台
                if (lstPlatInfos.Count > 0)
                {
                    //创建XYTManager的实例
                    manager = XYTFactory.CreateXYTManager(Global.Setting.XYTTypeName);
                    manager.StatusUpdate += Manager_StatusUpdate;
                    manager.SettingChanged += Manager_SettingChanged;
                    //初始化
                    manager.Init(lstPlatInfos, Global.Setting.XYTInitPara);

                }
            }
            catch (Exception ex)
            {
               // ViewStatus.Instance.ShowMsg(LanguageHelper.Msg.PosPlatform_InitError + ex.Message, Brushes.Red);
            }
        }

        /// <summary>
        /// 处理平台状态改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Manager_StatusUpdate(object sender, XYTEventArgs e)
        {
        
            //同步状态到SignalProvider
            var provider = PosSignal.GetSignalProvider();
            if (provider != null)
            {
                PlatformStatus status = new PlatformStatus();
                status.IsServoOn = e.Platform.IsServoOn();
                status.IsAtHome = e.Platform.IsAtHome();
                status.IsAlarm = e.Platform.IsAlarm();
                status.IsAtUserPos = new bool[3];
                for (int i = 0; i < 3; i++)
                {
                    status.IsAtUserPos[i] = e.Platform.IsAtUserPos(i);
                }
                provider.UpdatePlatformStatus(e.Platform.Index, status);
            }
        }

        /// <summary>
        /// 处理对位平台设置变化事件，将对位平台参数保存到文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Manager_SettingChanged(object sender, XYTSettingEventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"Config\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filename = path + "XYT.xml";
            HTXMLSerializer.EntityToXMLFile(e.ListPlatInfos, filename);
        }

        /// <summary>
        /// 获取指定名称的平台
        /// </summary>
        /// <param name="name">平台的名称标识</param>
        /// <returns></returns>
        public static XYTPlatform GetPlatform(string name)
        {
            return manager.GetPlatform(name);
        }

        /// <summary>
        /// 更新安全条件
        /// </summary>
        /// <param name="name">平台的名称标识</param>
        /// <param name="isSafeToMove">移动是否安全</param>
        public static void UpdateSafetyFlag(string name, bool isSafeToMove)
        {
            var platform = manager.GetPlatform(name);
            if (platform != null)
            {
                dictSafetyFlag[name] = isSafeToMove;
            }
        }

        /// <summary>
        /// 平台的移动是否安全
        /// </summary>
        /// <param name="name">平台的名称标识</param>
        /// <returns>移动是否安全</returns>
        public static bool IsSafeToMove(string name)
        {
            if (dictSafetyFlag.ContainsKey(name))
            {
                return dictSafetyFlag[name];
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 断开连接，释放资源
        /// </summary>
        public static void UnInit()
        {
            if (manager != null)
            {
                manager.UnInit();
            }
        }
    }
}
