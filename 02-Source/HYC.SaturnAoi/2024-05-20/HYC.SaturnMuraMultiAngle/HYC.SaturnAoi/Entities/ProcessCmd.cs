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
using HYC.SaturnAoi.DetectFlow;
using HYC.SaturnAoi.Devices.Client;
using HYC.SaturnAoi.Devices.ClientPC;

using HYC.SaturnDB;
using HYC.SaturnMuraMultiAngle.Entities;
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
                                if (strs.Length > 1)
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
                                    if ( obj.DetectType == "SquintMura")
                                    {
                                        AoiDetectReqHandle.Instance.AoiDetectReq(obj);
                                    }
                                    else if (obj.DetectType == "MultiAngle")
                                    {
                                        MultiAngleDetectReqHandle.Instance.MultiAngleDetectReq(obj);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                OnShowMsg?.Invoke(LogName, $"DetectReq出错，异常信息：{ex}", System.Drawing.Brushes.Red);
                            }
                            break;
                        }
                    case Command.SwitchPatternResult:
                        {
                            if (!string.IsNullOrEmpty(info.Message))
                            {
                                string[] strs = info.Message.Split(',');
                                if (strs.Length==2)
                                {
                                    int ptnNum = int.Parse(strs[0]);
                                    OnShowMsg?.Invoke(LogName, $"收到画面:{ptnNum}切换{strs[1]}请求", System.Drawing.Brushes.Blue);
                                    PGHelper.Instance.SwitchPatternResult(ptnNum, strs[1]);
                                }
                              
                            }
                            else
                            {
                                OnShowMsg?.Invoke(LogName, "SwitchPatternResult出错，Message为空", System.Drawing.Brushes.Red);
                            }
                            break;
                        }
                    case Command.PowerControlResult:
                        {
                            if (!string.IsNullOrEmpty(info.Message))
                            {
                                string[] strs = info.Message.Split(',');
                                if (strs.Length == 2)
                                {
                                    var type = strs[0];
                                    var result = strs[1];
                                    OnShowMsg?.Invoke(LogName, $"收到检查机{type} {result}请求", System.Drawing.Brushes.Blue);
                                    PGHelper.Instance.PowerControlResult(type,result);
                                }

                            }
                            else
                            {
                                OnShowMsg?.Invoke(LogName, "SwitchPatternResult出错，Message为空", System.Drawing.Brushes.Red);
                            }
                            break;
                        }
                    case Command.DfsPara:
                        {
                            if (!string.IsNullOrEmpty(info.Message))
                            {
                                string[] strs = info.Message.Split(',');
                                if (strs.Length == 2)
                                {
                                    Global.StepID = strs[0];
                                    Global.EqpID = strs[1];
                                }
                                OnShowMsg?.Invoke(LogName, $"StepID：{strs[0]} ，EqpID：{strs[1]}", System.Drawing.Brushes.Blue);
                            }
                            else
                            {
                                OnShowMsg?.Invoke(LogName, "SwitchPatternResult出错，Message为空", System.Drawing.Brushes.Red);
                            }
                            break;
                        }
                }
            }
        }
    }
}
