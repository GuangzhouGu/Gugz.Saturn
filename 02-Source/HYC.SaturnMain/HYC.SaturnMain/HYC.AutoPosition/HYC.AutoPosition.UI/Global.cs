/*----------------------------------------------------------------
// Copyright (C) Suzhou HYC Electronic Technology Co.,LTD
// 版权所有。
//
// =================================
// CLR版本     ：4.0.30319.42000
// 命名空间    ：HYC.AutoPosition.UI
// 文件名称    ：Global.cs
// =================================
// 创 建 者    ：yangkang
// 创建日期    ：2020/1/6 17:30:18 
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
using HYC.AutoPosition.UI.Entities;
using HYC.HTFile.HTINI;
using HYC.HTFile.HTXML;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYC.AutoPosition.UI
{
    /// <summary>
    /// 全局类
    /// </summary>
    public class Global
    {
        /// <summary>
        /// 自动对位管理类
        /// </summary>
        public static AutoPosManager Manager { get; set; } = new AutoPosManager();
        /// <summary>
        /// 自动对位设置类
        /// </summary>
        public static AutoPosSetting Setting { get; set; } = new AutoPosSetting();
                
        /// <summary>
        /// 用户管理
        /// </summary>
        public static UserManager UserManager { get; set; } = new UserManager();

        /// <summary>
        /// ini文件读写帮助类
        /// </summary>
        public static HTINIHelper IniHelper = new HTINIHelper(@"AutoPos\AutoPosConfig.cfg");

        /// <summary>
        /// 加载设置
        /// </summary>
        public static void LoadSettings()
        {
            string strPath = AppDomain.CurrentDomain.BaseDirectory + "AutoPos";
            string strFileName = Path.Combine(strPath, "AutoPosSetting.xml");
            if (File.Exists(strFileName))
            {
                //反序列化加载
                Setting = HTXMLSerializer.EntityFromXMLFile<AutoPosSetting>(strFileName);
            }
        }

        /// <summary>
        /// 保存设置
        /// </summary>
        public static void SaveSettings()
        {
            string strPath = AppDomain.CurrentDomain.BaseDirectory + "AutoPos";
            string strFileName = Path.Combine(strPath, "AutoPosSetting.xml");
            //如果文件不存在，则先创建文件夹
            if (!Directory.Exists(strPath))
            {
                Directory.CreateDirectory(strPath);
            }
            //序列化到文件
            HTXMLSerializer.EntityToXMLFile(Setting, strFileName);
        }
    }
}
