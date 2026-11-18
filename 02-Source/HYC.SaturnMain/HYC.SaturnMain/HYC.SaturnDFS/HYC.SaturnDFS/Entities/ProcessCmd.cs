/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2023 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnAoi.Entities
// 文件名称    ：Class1
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
using CommonLib.Entities;
using DFS;
using HYC.SaturnAoi.Devices.Client;
using HYC.SaturnDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYC.SaturnAoi.Entities
{
    /// <summary>
    /// 指令处理类
    /// </summary>
    class ProcessCmd
    {
        public static event Action<string, string, System.Drawing.Brush> OnShowMsg;
        public static event Action<Command, object> OnExecuteCommand;
        public static ProcessCmd Instance = null;
        const string LogName = "Communication";
        static ProcessCmd()
        {
            Instance = new ProcessCmd();
        }
        public void DealCommand(MessageInfo info)
        {
            if (info != null)
            {
                switch (info.Command)
                {
                    case Command.RecipeSync:
                        {
                            if (!string.IsNullOrEmpty(info.Message))
                            {
                                OnShowMsg?.Invoke(LogName, $"Recipe同步:{info.Message}", System.Drawing.Brushes.Blue);
                                OnExecuteCommand?.Invoke(Command.RecipeSync, info.Message);
                            }
                            else
                            {
                                OnShowMsg?.Invoke(LogName, "Recipe同步出错，Message为空", System.Drawing.Brushes.Red);
                            }
                            break;
                        }
                    case Command.DfsUpload:
                        {
                            if (!string.IsNullOrEmpty(info.Message))
                            {
                                OnShowMsg?.Invoke(LogName, $"DFS上传:{info.Message}", System.Drawing.Brushes.Blue);
                                OnExecuteCommand?.Invoke(Command.DfsUpload, info.Message);
                            }
                            else
                            {
                                OnShowMsg?.Invoke(LogName, "DFS上传，Message为空", System.Drawing.Brushes.Red);
                            }
                            break;
                        }
                    case Command.UpdateStepID:
                        {
                            if (string.IsNullOrEmpty(info.Message))
                            {
                                DFSSettings.SyncStepID(info.Message);
                                OnShowMsg?.Invoke(LogName, $"同步StepID:{info.Message}", System.Drawing.Brushes.Blue);
                            }
                            break;
                        }

                }
            }
        }
    }
}
