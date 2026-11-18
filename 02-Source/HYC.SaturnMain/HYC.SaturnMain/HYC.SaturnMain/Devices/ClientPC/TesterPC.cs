/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2020 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnMain
// 文件名称    ：TesterPC.cs
// =================================
// 创 建 者    ：guguangzhou
// 创建日期    ：2021/01/29 09:00:00 
// 功能描述    ：TesterPC相关指令
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
using HYC.HTLog;
using HYC.SaturnMain.Params;
using HYC.SaturnMain.Detections;
using HYC.SaturnMain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HYC.SaturnMain.Devices.MasterPC;
using HYC.SaturnDB.Model;
using HYC.SaturnMain.UserControls;
using HYC.SaturnMain;
using CIMBOE.Block;
using HYC.HTPCBase.Extension.Recipe;

namespace HYC.SaturnMain.Devices.ClientPC
{
    public class TesterPC
    {
        public static TesterPC Instance = null;
        public static bool TesterPCOnline = false;// TesterPC在线信号 
        public static bool OneColumnTestCom = false;//单列Panel测试完成状态
        public static bool OnePanelStartTestCom = false;//单个Panel测试完成状态
        public static bool OnePanelStopTestCom = false;//单个Panel测试完成状态
        public static bool TesterPCIsAlarm = false;// TesterPC报警状态 
        public static bool TesterManualDetect = false;
        public static string TesterModeSwitchResult1 = string.Empty;
        public static string TesterModeSwitchResult2 = string.Empty;
        public static int TesterManualDetectGlassNum;
        public static int TesterManualDetectColumnNum;
        public static string RecipeSyncResult = string.Empty;

        //模拟测试
        private double[,] SimulatorCrds = new double[,] { { -308, 677.896 }, { -151, 465.345 }, { 5.578, 35.2 }, { 165.632, -324.785 }, { 319.12, -536.087 } };
        static TesterPC()
        {
            Instance = new TesterPC();
        }
    
        #region TesterPC
        public  void TimeSync()
        {
            MessageInfo info = new MessageInfo();
            info.Type = CommunicationType.Send;
            info.PCName = ClientPCName.TesterPC.ToString();
            info.Command = Command.TimeSync;
            info.Message = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            MasterPCHelper.Instance.SendMessage(info);
        }
        public  void RecipeSync(string recipe, RecipeChangeType recipeOPType)
        {
            RecipeSyncResult = string.Empty;
            if (Param.Comm.TesterPCShield)
            {
                RecipeSyncResult = "OK";
            }
            MessageInfo info = new MessageInfo();
            info.Type = CommunicationType.Send;
            info.PCName = ClientPCName.TesterPC.ToString();
            info.Command = Command.RecipeSync;
            info.Message = $"{recipe},{recipeOPType}";
            MasterPCHelper.Instance.SendMessage(info);
        }
        public  void UserSync(string user)
        {
            MessageInfo info = new MessageInfo();
            info.Type = CommunicationType.Send;
            info.PCName = ClientPCName.TesterPC.ToString();
            info.Command = Command.UserSync;
            info.Message = user;
            MasterPCHelper.Instance.SendMessage(info);
        }
        public void OneColumnTestStart(int columnNum)
        {
            try
            {
                OneColumnTestCom = false;
                if (Param.Control.RunMode==RunMode.NoDetect)
                {
                    Thread.Sleep(1000);
                    OneColumnTestCom = true;
                    return;
                }
                if (Param.Comm.TesterPCShield)
                {
                    Thread.Sleep(1000);
                    OneColumnTestCom = true;
                    return;
                }
                MessageInfo info = new MessageInfo();
                info.Type = CommunicationType.Send;
                info.PCName = ClientPCName.TesterPC.ToString();
                info.Command = Command.OneColumnTest;
                info.Message = $"{columnNum}";
                MasterPCHelper.Instance.SendMessage(info);


                #region 测试
                //Thread.Sleep(1000);
                //string value = string.Empty;
                //switch (columnNum)
                //{
                //    case 0:
                //        {
                //            value = $"Panel0NG-3-Open_0001*5*6_0002*5*6-Short_0003*308*677.896*IRSHTTTP,Panel1OK,Panel2OK,Panel3OK,Panel4NG-2-Open_0004*5*6_0005*5*6-Short,Panel5OK,Panel6OK,Panel7OK,Panel8OK,Panel9OK,Panel10OK,Panel11OK,Panel12OK,Panel13OK,Panel14OK,Panel15OK,Panel16OK,Panel17OK,Panel18OK,Panel19OK";
                //            break;
                //        }
                //    case 1:
                //        {
                //            value = $"Panel0OK,Panel1OK,Panel2OK,Panel3OK,Panel4OK,Panel5NG-1-Open-Short_0001*308*677.896*IRSHTTTP,Panel6OK,Panel7OK,Panel8OK,Panel9OK,Panel10OK,Panel11OK,Panel12OK,Panel13OK,Panel14OK,Panel15OK,Panel16OK,Panel17OK,Panel18OK,Panel19OK";
                //            break;
                //        }
                //    case 2:
                //        {
                //            value = $"Panel0OK,Panel1OK,Panel2OK,Panel3OK,Panel4NG-2-Open_0001*5*6_0002*5*6-Short,Panel5OK,Panel6OK,Panel7OK,Panel8NG-1-Open-Short_0003*308*677.896*IRSHTTTP,Panel9OK,Panel10OK,Panel11OK,Panel12OK,Panel13OK,Panel14OK,Panel15OK,Panel16OK,Panel17OK,Panel18OK,Panel19OK";
                //            break;
                //        }
                //    case 3:
                //        {
                //            value = $"Panel0OK,Panel1OK,Panel2OK,Panel3OK,Panel4OK,Panel5OK,Panel6OK,Panel7OK,Panel8OK,Panel9NG-2-Open_0001*5*6_0002*5*6-Short,Panel10OK,Panel11OK,Panel12OK,Panel13OK,Panel14OK,Panel15OK,Panel16OK,Panel17OK,Panel18OK,Panel19NG-1-Open-Short_0003*308*677.896*IRSHTTTP";
                //            break;
                //        }
                //    case 4:
                //        {
                //            value = $"Panel0NG-3-Open_0001*5*6_0002*5*6-Short_0003*308*677.896*IRSHTTTP,Panel1OK,Panel2OK,Panel3OK,Panel4OK,Panel5OK,Panel6OK,Panel7OK,Panel8OK,Panel9OK,Panel10OK,Panel11OK,Panel12NG-1-Open-Short_0004*308*677.896*IRSHTTTP,Panel13OK,Panel14OK,Panel15OK,Panel16OK,Panel17OK,Panel18OK,Panel19OK";
                //            break;
                //        }
                //    default:
                //        break;
                //}
                //ProcessCmd.Instance.GetTesterResult(value);
                //OneColumnTestCom = true;
                #endregion
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("TesterPC").Fatal(ex.ToString());
            }
        }

   
        public void NotifyProcessId(string processID)
        {
            try
            {
                if (!Param.Comm.TesterPCShield)
                {
                    MessageInfo info = new MessageInfo();
                    info.Type = CommunicationType.Send;
                    info.PCName = ClientPCName.TesterPC.ToString();
                    info.Command = Command.NotifyProcessId;
                    info.Message = $"{processID}";
                    MasterPCHelper.Instance.SendMessage(info);
                }
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("TesterPC").Fatal(ex.ToString());
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="upStreamID">Cim给的ID</param>
        /// <param name="scanerID">扫码枪ID</param>
        /// <param name="jobData"></param>
        public void NotifyGlassId(string upStreamID, string scanerID, GlassDataA jobData)
        {
            try
            {
                if (Param.Control.RunMode == RunMode.NoDetect)
                {
                    Thread.Sleep(1000);
                    return;
                }
                if (!Param.Comm.TesterPCShield)
                {
                    MessageInfo info = new MessageInfo();
                    info.Type = CommunicationType.Send;
                    info.PCName = ClientPCName.TesterPC.ToString();
                    info.Command = Command.NotifyGlassId;
                    info.Message = $"{upStreamID},{jobData.Lot_ID},{jobData.Slot_Sequence_No},{jobData.Option_Value},{jobData.Prod_ID},{scanerID}";
                    MasterPCHelper.Instance.SendMessage(info);
                }
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("TesterPC").Fatal(ex.ToString());
            }
        }
        public void TesterPCPause()
        {
            MessageInfo info = new MessageInfo();
            info.Type = CommunicationType.Send;
            info.PCName = ClientPCName.TesterPC.ToString();
            info.Command = Command.Pause;
            info.Message = "";
            MasterPCHelper.Instance.SendMessage(info);
        }
        public void TesterPCStart()
        {
            if (Param.Control.RunMode == RunMode.NoDetect)
            {
                return;
            }
            MessageInfo info = new MessageInfo();
            info.Type = CommunicationType.Send;
            info.PCName = ClientPCName.TesterPC.ToString();
            info.Command = Command.Start;
            info.Message = "";
            MasterPCHelper.Instance.SendMessage(info);
        }
        public void TesterPCStop()
        {
            MessageInfo info = new MessageInfo();
            info.Type = CommunicationType.Send;
            info.PCName = ClientPCName.TesterPC.ToString();
            info.Command = Command.Stop;
            info.Message = "";
           MasterPCHelper.Instance.SendMessage(info);
        }
        public void DetectEnd()
        {
            MessageInfo info = new MessageInfo();
            info.Type = CommunicationType.Send;
            info.PCName = ClientPCName.TesterPC.ToString();
            info.Command = Command.OneGlassTestEnd;
            info.Message = "";
            MasterPCHelper.Instance.SendMessage(info);
        }
        public void UpstreamGlassInfo(string glassID,int glassNum,PanelStatus[,] panelStatus)
        {
            MessageInfo info = new MessageInfo();
            info.Type = CommunicationType.Send;
            info.PCName = ClientPCName.TesterPC.ToString();
            info.Command = Command.UpstreamGlassInfo;
            string panelsStatus = GetPanelSts(panelStatus);
            info.Message = $"{glassID},{glassNum},{panelsStatus}";
            MasterPCHelper.Instance.SendMessage(info);
        }
        /// <summary>
        /// Tester模式切换
        /// </summary>
        /// <param name="mode">TOS/Cell</param>
        /// <param name="pgNum">PG序号：0-1</param>
        ///  <param name="cellNum">Cell序号：0-11</param>
        public void TesterModeSwitch(string mode,int pgNum=-1,int cellNum=-1)
        {

            if (pgNum == 0)
            {
                TesterModeSwitchResult1 = string.Empty;
                if (Param.Comm.TesterPCShield)
                {
                    TesterModeSwitchResult1 = "OK";
                    return;
                }
            }
            else if (pgNum == 1)
            {
                TesterModeSwitchResult2 = string.Empty;
                if (Param.Comm.TesterPCShield)
                {
                    TesterModeSwitchResult2 = "OK";
                    return;
                }
            }
            MessageInfo info = new MessageInfo();
            info.Type = CommunicationType.Send;
            info.PCName = ClientPCName.TesterPC.ToString();
            info.Command = Command.TesterModeSwitch;
            info.Message = $"{mode},{pgNum},{cellNum}";
            MasterPCHelper.Instance.SendMessage(info);
        }
       public void OneCellDetectEnd(int pgNum = -1, int cellNum = -1)
        {
            MessageInfo info = new MessageInfo();
            info.Type = CommunicationType.Send;
            info.PCName = ClientPCName.TesterPC.ToString();
            info.Command = Command.OneCellDetectEnd;
            info.Message = $"{pgNum},{cellNum}";
            MasterPCHelper.Instance.SendMessage(info);
        }
        private string GetPanelSts(PanelStatus[,] panelStatus)
        {
            string result = string.Empty;
            try
            {
                if (panelStatus != null)
                {
                    for (int i = 0; i < panelStatus.GetLength(0); i++)
                    {
                        StringBuilder sb = new StringBuilder();
                        for (int j = 0; j < panelStatus.GetLength(1); j++)
                        {
                            if (panelStatus[i, j].Status == PanelStatusEnum.UpstreamNG)
                            {
                                sb.Append("NG-");
                            }
                            else
                            {
                                sb.Append("OK-");
                            }
                        }
                        string temp = sb.ToString();
                        result += temp.TrimEnd('-') + ",";
                    }
                    result = result.TrimEnd(',');
                }
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("TesterPC").Info($"GetPanelSts执行异常，异常信息：{ex}");
            }
            return result;
        }
        #endregion
    }


}
