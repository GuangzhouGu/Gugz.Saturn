/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2020 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnMain
// 文件名称    ：ProcessCmd.cs
// =================================
// 创 建 者    ：guguangzhou
// 创建日期    ：2021/01/29 09:00:00 
// 功能描述    ：通讯指令处理类
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
using HYC.SaturnDB.DAL;
using HYC.SaturnDB.Model;
using HYC.HTLog;
using HYC.HTPCBase.Alarm;
using HYC.HTPCBase.Motion;
using HYC.HTPCBase.Station;
using HYC.SaturnMain;
using HYC.SaturnMain.Params;
using HYC.SaturnMain.Detections;
using HYC.SaturnMain.Devices.ClientPC;
using HYC.SaturnMain.Devices.Motion;
using HYC.SaturnMain.Station;
using HYC.SaturnMain.SubForms;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HYC.HTPCBase.Extension.Entities;
using HYC.SaturnDB;
using HYC.SaturnMain.Entities;

namespace HYC.SaturnMain.Entities
{
    /// <summary>
    /// 指令处理类
    /// </summary>
    class ProcessCmd
    {
        public static event Action<string, string, System.Drawing.Brush> OnShowMsg;
        public static event Action<bool> OnUpdatePanelStatus;
        public static ProcessCmd Instance = null;
        public static Action<Command, object> OnCommand;
        private Action<string> ShowClinetMsgDelegate;
        private string LogName = "Communication";
        static ProcessCmd()
        {
            Instance = new ProcessCmd();
        }
        /// <summary>
        /// 处理接收的指令队列
        /// </summary>
        public void DealCommand(MessageInfo info)
        {
            try
            {
                if (info.Command!= Command.DetectResult)
                {
                    LogManager.GetLogger(LogName).Info($"PCName:{info.PCName},CommunicationType:{info.Type},Command:{info.Command},Message:{info.Message}");
                    ShowClinetMsgDelegate?.Invoke($"PCName:{info.PCName},Command:{info.Command},Message:{info.Message}");
                }
                switch (info.Command)
                {
                    case Command.RecipeSyncAck:
                        {
                            var strs = info.Message.Split(',');
                            if (strs.Length>1)
                            {
                                var pcName = strs[0];
                                var result = strs[1];
                                switch (pcName)
                                {
                                    case "TesterPC":
                                        {
                                            TesterPC.RecipeSyncResult = result;
                                            break;
                                        }
                                    case "AoiPC1":
                                        {
                                            AoiPC.PC1RecipeSyncResult = result;
                                            break;
                                        }
                                    case "AoiPC2":
                                        {
                                            AoiPC.PC2RecipeSyncResult = result;
                                            break;
                                        }
                                    default:
                                        break;
                                }
                                OnShowMsg?.Invoke(LogName, $"{pcName}切换机种完成，结果：{result}", System.Drawing.Brushes.Green);
                            }
                            else
                            {
                                OnShowMsg?.Invoke(LogName, $"机种切换结果指令异常，结果为空", System.Drawing.Brushes.Red);
                            }
                            break;
                        }
                    #region AoiPC指令回复
                    case Command.DetectResult:
                        {
                            var bytes = Encoding.Default.GetBytes(info.Message);
                            var obj = ClassHelper.BytesToObject<DetectResultArgs>(bytes);
                            if (obj != null)
                            {
                                OnShowMsg?.Invoke(LogName, $"收到AoiPC{obj.DetectReqArgs.PCIndex+1},{obj.DetectReqArgs.CellID} {obj.DetectReqArgs.DetectType},检测结果:{obj.Result},StartTime:{obj.StartTime},EndTime:{obj.EndTime}", System.Drawing.Brushes.Blue);
                                if (obj.DetectReqArgs.PCIndex==0)
                                {
                                    AoiPC.PC1DetectResultArgs = obj;
                                }
                                else if (obj.DetectReqArgs.PCIndex == 1)
                                {
                                    AoiPC.PC2DetectResultArgs = obj;
                                }
                            }
                            else
                            {
                                OnShowMsg?.Invoke(LogName, $"AoiPC检测结果转换失败，结果为空", System.Drawing.Brushes.Red);
                            }
                            break;
                        }
                    case Command.PowerControlResult:
                        {
                            string[] strs = info.Message.Split(',');//0/1,PowerON/PowerOff,OK/NG
                            OnShowMsg?.Invoke(LogName, $"AoiPC->PG{strs[0]}{strs[1]}完成，结果：{strs[2]}", System.Drawing.Brushes.Blue);
                            if (strs[0]=="0")
                            {
                                AoiPC.PC1PowerControlResult = strs[2];
                            }
                            else
                            {
                                AoiPC.PC2PowerControlResult = strs[2];
                            }                          
                            break;
                        }
                    #endregion
                    #region TesterPC指令回复
                    case Command.OneColumnTestCom:
                        {
                            OnShowMsg?.Invoke(LogName, "TesterPC->单列Panel测试完成", System.Drawing.Brushes.Blue);
                            GetTesterResult(info.Message);
                            break;
                        }
                   
                    case Command.RequestGlassID:
                        {
                            OnShowMsg?.Invoke(LogName, $"TesterPC->Tester请求GlassID", System.Drawing.Brushes.Blue);
                            if (string.IsNullOrEmpty(Global.CurDetectInfo.GlassID))
                            {
                                Global.CurDetectInfo.GlassID = DateTime.Now.ToString("yyyyMMddHHffmm");
                            }
                            break;
                        }
                    case Command.NGCountAlarm:
                        {
                            OnShowMsg?.Invoke(LogName, $"TesterPC->收到TesterPC NG数量过多报警", System.Drawing.Brushes.Red);
                            OnCommand?.Invoke(Command.NGCountAlarm, info.Message);
                            break;
                        }
                    case Command.TesterModeSwitchResult:
                        {
                            string[] result = info.Message.Split(',');//OK/NG
                            if (result.Length>0)
                            {
                                OnShowMsg?.Invoke(LogName, $"TesterPC->Tester{result[0]}模式切换完成，结果：{result[1]}", System.Drawing.Brushes.Blue);
                                if (result[0] == "0")
                                {
                                    TesterPC.TesterModeSwitchResult1 = result[1];
                                }
                                else
                                {
                                    TesterPC.TesterModeSwitchResult2 = result[1];
                                }
                            }
                            break;
                        }
                    case Command.TesterPowerControl:
                        {
                            string[] strs = info.Message.Split(',');//open/close，0/1
                            OnShowMsg?.Invoke(LogName, $"TesterPC->TesterPC请求{strs[0]} {strs[1]}号检查机", System.Drawing.Brushes.Blue);
                            TesterPowerControl(strs[0], strs[1]);
                            break;
                        }
                        #endregion
                }
            }
            catch (Exception ex)
            {
                LogManager.GetLogger(LogName).Fatal(ex.ToString());
            }
        }
       
        /// <summary>
        /// 解析testerPC返回的结果
        /// </summary>
        /// <param name="value"></param>
        public void GetTesterResult(string value)
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    //Panel0OK,Panel1NG-3-Open_0001*Tx*Rx_0002*Tx*Rx*IRSHTTTP-Short_0003*Tx*Rx*IRSHTTTP,Panel2OK,Panel3NG-2-Open_0004*Tx*Rx_0005*Tx*Rx-Short,Panel4OK
                    if (!string.IsNullOrEmpty(value.Replace("\0", "")))
                    {
                        string[] temp = value.Split(',');
                        if (temp.Length== Param.Panel.CellCountY)
                        {
                            OneColumnModel oneColumn = DetectionHelper.GetOneColumnModel(Global.CurDetectInfo.GlassNum,Global.CurDetectInfo.ColumnNum);
                            if (oneColumn != null)
                            {
                                oneColumn.DetectSts = 0;
                                for (int i = 0; i < Param.Panel.CellCountY; i++)
                                {
                                    OpenShortDetectModel onePanel = DetectionHelper.GetOnePanelModel(oneColumn, i);
                                    onePanel.Result = temp[i].Contains("OK") ? "OK" :"NG";
                                    if (onePanel.Result == "NG")
                                    {
                                        string[] ngList = temp[i].Split('-');
                                        if (ngList.Length==4)
                                        {
                                            onePanel.TotalNGCount = int.Parse(ngList[1]);//open不良数量
                                            string[] openNg = ngList[2].Split('_');
                                            string[] shortNg = ngList[3].Split('_');
                                            if (openNg.Length>1)
                                            {
                                                onePanel.NGType = NgType.Open.ToString();
                                                onePanel.OpenNGCount = openNg.Length-1;//open不良数量
                                                for (int j = 0; j < onePanel.OpenNGCount; j++)
                                                {
                                                    OpenShortNGInfoModel ngInfo = new OpenShortNGInfoModel();
                                                    ngInfo.NGNum = j;
                                                    ngInfo.NGType = NgType.Open.ToString();
                                                    string[] chanel = openNg[j + 1].Split('*');
                                                    ngInfo.NGCode = chanel[0];
                                                    ngInfo.Source = chanel[1];
                                                    ngInfo.Measure = chanel[2];
                                                    ngInfo.GlassID = Global.CurDetectInfo.GlassID;
                                                    ngInfo.ColumnNum = oneColumn.ColumnNum;
                                                    ngInfo.GlassNum = Global.CurDetectInfo.GlassNum;
                                                    ngInfo.RowNum = onePanel.RowNum;
                                                    ngInfo.DetectTime = onePanel.DetectTime;
                                                    ngInfo.CellID = Global.GetCellID(ngInfo.ColumnNum, ngInfo.RowNum); 
                                                    onePanel.OpenNGInfoList.Add(ngInfo);
                                                    new OpenShortNGInfoDal().Insert(ngInfo);
                                                }
                                            }
                                            if (shortNg.Length > 1)
                                            {
                                                if (onePanel.NGType != NgType.Open.ToString())
                                                {
                                                    onePanel.NGType = NgType.Short.ToString();
                                                }
                                                onePanel.ShortNGCount = shortNg.Length - 1;//short不良数量
                                                for (int j = 0; j < onePanel.ShortNGCount; j++)
                                                {
                                                    OpenShortNGInfoModel ngInfo = new OpenShortNGInfoModel();
                                                    ngInfo.NGNum = j;
                                                    ngInfo.NGType = NgType.Short.ToString();
                                                    string[] chanel = shortNg[j + 1].Split('*');
                                                    ngInfo.NGCode = chanel[0];
                                                    ngInfo.Source = chanel[1];
                                                    ngInfo.Measure = chanel[2];
                                                    ngInfo.ShortType = chanel[3];
                                                    ngInfo.GlassID = Global.CurDetectInfo.GlassID;
                                                    ngInfo.ColumnNum = oneColumn.ColumnNum;
                                                    ngInfo.GlassNum = Global.CurDetectInfo.GlassNum;
                                                    ngInfo.RowNum = onePanel.RowNum;
                                                    ngInfo.DetectTime = onePanel.DetectTime;
                                                    ngInfo.CellID = Global.GetCellID(ngInfo.ColumnNum, ngInfo.RowNum);
                                                    onePanel.ShortNGInfoList.Add(ngInfo);
                                                    new OpenShortNGInfoDal().Insert(ngInfo);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            OnShowMsg?.Invoke(LogName, $"TesterPC->TesterPC返回的不良信息長度出錯", System.Drawing.Brushes.Red);
                                        }
                                    }
                                    new OpenShortInfoDal().Insert(onePanel);
                                    DetectResultArgs args = new DetectResultArgs();
                                    args.DetectReqArgs = new SaturnAoi.Entities.DetectReqArgs
                                    { 
                                        GlassID=Global.CurDetectInfo.GlassID,
                                        GlassNum=Global.CurDetectInfo.GlassNum,
                                        ColumnNum=Global.CurDetectInfo.ColumnNum,
                                        CellID=Global.GetCellID(Global.CurDetectInfo.ColumnNum,i),
                                        CellNum=i ,
                                        DetectType="OpenShort"
                                    };
                                    args.Result = onePanel.Result;
                                    DBManager.UpdateOneCellResult(args);
                                }
                                oneColumn.Result = oneColumn.PanelList.Where(p => p.Result == "NG"&&p.ShortNGCount>0).ToList().Count > 0 ? "NG" : "OK";
                                OnUpdatePanelStatus?.Invoke(false);
                                TesterPC.OneColumnTestCom = true;
                            }
                        }
                        else
                        {
                            OnShowMsg?.Invoke(LogName, $"TesterPC->TesterPC返回單列检测结果數量：{temp.Length}，和Recipe配置的數量不一致", System.Drawing.Brushes.Red);
                        }
                    }
                }
                catch (Exception ex)
                {
                    OnShowMsg?.Invoke(LogName, $"TesterPC->TesterPC返回检测结果出错，错误信息：{ex.Message}", System.Drawing.Brushes.Red);
                    LogManager.GetLogger(LogName).Fatal(ex.ToString());
                }
            });
        }      
        private void TesterPowerControl(string value,string testerNum)
        {
            bool value2 = value== "Close";
            string name = $"Tester{testerNum + 1}Power";
            MotionManager.GetInstance().SetDigitalOutput(name,value2);
        }
     
        public void ShowClientMsg(Action<string> action)
        {
            ShowClinetMsgDelegate = action;
        }
    }
}
