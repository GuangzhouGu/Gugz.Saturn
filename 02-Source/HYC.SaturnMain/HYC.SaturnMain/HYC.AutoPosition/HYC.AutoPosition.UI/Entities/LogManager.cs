/*----------------------------------------------------------------
// Copyright (C) Suzhou HYC Electronic Technology Co.,LTD
// 版权所有。
//
// =================================
// CLR版本     ：4.0.30319.42000
// 命名空间    ：HYC.AutoPosition.UI.Entities
// 文件名称    ：LogManager.cs
// =================================
// 创 建 者    ：yangkang
// 创建日期    ：2020/4/9 9:44:16 
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
using HYC.HTLog.Core;
using HYC.HTLog.Logs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYC.AutoPosition.UI.Entities
{
    /// <summary>
    /// 日志管理类
    /// </summary>
    static class LogManager
    {
        // 所有Log对象的集合
        private static Dictionary<string, Logger> dictLogger = new Dictionary<string, Logger>();
        private static object lockObj = new object();
        // Log记录默认根目录
        private static string strPath = @"Log\AutoPos\";

        /// <summary>
        /// 获取指定的Log对象
        /// </summary>
        /// <param name="name">文件夹名称</param>
        /// <returns>日志对象</returns>
        public static Logger GetLogger(string name)
        {
            lock (lockObj)
            {
                if (dictLogger.ContainsKey(name))
                {
                    return dictLogger[name];
                }
                else
                {
                    Logger logger = new Logger(strPath + name);
                    dictLogger.Add(name, logger);
                    return logger;
                }
            }
        }

        /// <summary>
        /// 以最后修改时间为依据，删除超过指定天数之前的Log
        /// </summary>
        /// <param name="days">要保留的Log的天数</param>
        public static void DeleteOldLog(int days)
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    DirectoryInfo info = new DirectoryInfo(strPath);
                    DeleteLog(days, info);
                }
                catch (Exception)
                { }
            });
        }

        /// <summary>
        /// 删除日志
        /// </summary>
        /// <param name="days"></param>
        /// <param name="info"></param>
        private static void DeleteLog(int days, DirectoryInfo info)
        {
            DirectoryInfo[] folders = info.GetDirectories();
            foreach (DirectoryInfo folder in folders)
            {
                DeleteLog(days, folder);
            }

            FileInfo[] files = info.GetFiles();
            foreach (FileInfo file in files)
            {
                if (file.Extension.ToLower().Equals(".log"))
                {
                    if (file.LastWriteTime < DateTime.Now.AddDays(-days))
                    {
                        file.Delete();
                    }
                }
            }
        }
    }

    /// <summary>
    /// 日志对象
    /// </summary>
    class Logger
    {
        private ILogNet log = null;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="folder"></param>
        public Logger(string folder)
        {
            log = new LogManagerDateTime(folder, GenerateMode.ByEveryDay);
        }

        /// <summary>
        /// 记录Info类型的消息
        /// </summary>
        /// <param name="message"></param>
        public void Info(string message)
        {
            log.WriteInfo(message);
        }

        /// <summary>
        /// 记录Error类型的消息
        /// </summary>
        /// <param name="message"></param>
        public void Error(string message)
        {
            log.WriteError(message);
        }

        /// <summary>
        /// 记录Error类型的异常
        /// </summary>
        /// <param name="ex"></param>
        public void Error(Exception ex)
        {
            log.WriteError(ex.ToString());
        }
    }
}
