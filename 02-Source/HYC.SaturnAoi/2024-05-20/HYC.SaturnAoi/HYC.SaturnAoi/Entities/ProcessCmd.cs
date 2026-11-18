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
using HYC.SaturnAoi.DetectFlow;
using HYC.SaturnAoi.Devices.Client;
using HYC.SaturnAoi.Devices.ClientPC;
using HYC.SaturnAoi.Devices.MuraPC;
using HYC.SaturnAoi.Devices.Tester;
using HYC.SaturnAoi.Params;
using HYC.SaturnDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
                    case Command.TimeSync:
                        {
                            if (!string.IsNullOrEmpty(info.Message))
                            {
                                DateTime time = DateTime.Parse(info.Message);
                                OnShowMsg?.Invoke(LogName, $"时间同步:{time}", System.Drawing.Brushes.Blue);
                                //HTDateTime.SetLocalDateTime(time);
                            }
                            else
                            {
                                OnShowMsg?.Invoke(LogName, "时间同步出错，Message为空", System.Drawing.Brushes.Red);
                            }
                            break;
                        }

                    case Command.UserSync:
                        {
                            if (!string.IsNullOrEmpty(info.Message))
                            {
                                OnShowMsg?.Invoke(LogName, $"User同步:{info.Message}", System.Drawing.Brushes.Blue);
                                OnExecuteCommand?.Invoke(Command.UserSync, info.Message);
                            }
                            else
                            {
                                OnShowMsg?.Invoke(LogName, "User同步出错，Message为空", System.Drawing.Brushes.Red);
                            }
                            break;
                        }
                    case Command.RecipeSync:
                        {
                            if (!string.IsNullOrEmpty(info.Message))
                            {
                                var strs = info.Message.Split(',');
                                if (strs.Length>1)
                                {
                                    OnShowMsg?.Invoke(LogName, $"Recipe同步:{strs[0]}", System.Drawing.Brushes.Blue);
                                    OnExecuteCommand?.Invoke(Command.RecipeSync, info.Message);
                                }
       
                            }
                            else
                            {
                                OnShowMsg?.Invoke(LogName, "Recipe同步出错，Message为空", System.Drawing.Brushes.Red);
                            }
                            break;
                        }
                    case Command.DetectReq:
                        {
                            try
                            {
                                var bytes = Encoding.Default.GetBytes(info.Message);
                                var obj = ClassHelper.BytesToObject<DetectReqArgs>(bytes);
                                if (obj!=null)
                                {
                                    OnShowMsg?.Invoke(LogName, $"收到{obj.CellID} {obj.DetectType}检测请求", System.Drawing.Brushes.Blue);
                                    if (obj.DetectType == "Aoi" || obj.DetectType == "Mura" )
                                    {
                                        AoiDetectReqHandle.Instance.AoiDetectReq(obj);
                                    }
                                    else if (obj.DetectType == "MultiAngle" || obj.DetectType == "SquintMura")
                                    {
                                        MuraPC.Instance.DetectReq(obj);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                OnShowMsg?.Invoke(LogName, $"DetectReq出错，异常信息：{ex}", System.Drawing.Brushes.Red);
                            }
                            break;
                        }
                    case Command.DetectResult:
                        {
                            var bytes = Encoding.Default.GetBytes(info.Message);
                            var obj = ClassHelper.BytesToObject<DetectResultArgs>(bytes);
                            if (obj != null)
                            {
                                OnShowMsg?.Invoke(LogName, $"收到MuraPC {obj.DetectReqArgs.CellID} {obj.DetectReqArgs.DetectType},检测结果:{obj.Result},StartTime:{obj.StartTime},EndTime:{obj.EndTime}", System.Drawing.Brushes.Blue);
                                ClientHelper.Instance.SendDetectResult(obj);
                            }
                            else
                            {
                                OnShowMsg?.Invoke(LogName, $"MuraPC检测结果转换失败，结果为空", System.Drawing.Brushes.Red);
                            }
                            break;
                        }
                    case Command.PowerControlReq:
                        {
                            Task.Factory.StartNew(()=> {
                                var strs = info.Message.Split(',');
                                if (strs.Length > 0)
                                {
                                    int pcIndex = int.Parse(strs[0]);
                                    var pcName = pcIndex == -1 ? "MuraPC" : "EQPC";
                                    bool ret = false;
                                    if (strs[1] == "PowerOn")
                                    {
                                        OnShowMsg?.Invoke(LogName, $"收到{pcName}开电请求", System.Drawing.Brushes.Blue);
                                        for (int i = 0; i < Param.System.PowerOnCount; i++)
                                        {
                                            ret = ELPTesterHelper.Instance.GetTester(0).PowerOn();
                                            OnShowMsg?.Invoke(LogName, $"第{i+1}次开电{(ret ? "OK" : "NG")}", System.Drawing.Brushes.Blue);
                                            if (ret)
                                            {
                                                break;
                                            }
                                            Thread.Sleep(500);
                                        }
                                        OnShowMsg?.Invoke(LogName, $"开电{(ret ? "OK" : "NG")}", ret? System.Drawing.Brushes.Blue:System.Drawing.Brushes.Red);

                                    }
                                    else if (strs[1] == "PowerOff")
                                    {
                                        OnShowMsg?.Invoke(LogName, $"收到{pcName}关电请求", System.Drawing.Brushes.Blue);
                                        ret = ELPTesterHelper.Instance.GetTester(0).PowerOff();
                                        OnShowMsg?.Invoke(LogName, $"关电{(ret ? "OK" : "NG")}", System.Drawing.Brushes.Blue);
                                    }
                                    if (pcName == "MuraPC")
                                    {
                                        MuraPC.Instance.PowerControlResult(strs[1], ret ? "OK" : "NG");
                                    }
                                    else
                                    {
                                        ClientHelper.Instance.SendPowerControlResult(pcIndex, strs[1], ret ? "OK" : "NG");
                                    }

                                }
                            });
         
                          
                            break;
                        }
                    case Command.SwitchPatternReq:
                        {
                            var strs = info.Message.Split(',');
                            if (string.IsNullOrEmpty(info.Message)==false)
                            {
                                int ptnNum = int.Parse(info.Message);
                                bool ret = false;
                                OnShowMsg?.Invoke(LogName, $"收到切画面请求，画面序号：{ptnNum}", System.Drawing.Brushes.Blue);
                                ret = ELPTesterHelper.Instance.GetTester(0).PatternDisplay(ptnNum);
                                MuraPC.Instance.SwitchPatternResult(ptnNum, ret ? "OK" : "NG");
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
