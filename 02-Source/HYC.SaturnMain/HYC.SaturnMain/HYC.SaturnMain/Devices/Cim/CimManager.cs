using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CIMBOE;
using CIMBase;
using CIMBase.CCLink;
using HYC.HTPCBase.Extension.Recipe;
using CIMBOE.Block;
using HYC.HTTools;
using HYC.HTSystem;
using System.Drawing;
using HYC.SaturnDB.Model;
using HYC.SaturnDB.DAL;
using HYC.SaturnMain.Params;
using System.Diagnostics;
using HYC.HTPCBase.Views;
using HYC.HTPCBase.Alarm;
using HYC.HTAlarm.DB.Model;
using HYC.HTLog;
using HYC.SaturnMain.UserControls;
using HYC.SaturnMain;

namespace BOECIM.Devices.CIM
{
    public class CimManager
    {
        public event Action<bool> OnConnected;      //CIM状态变化，反馈给主窗体
        public event Action<string,string,Brush> OnShowMsg;      //CIM状态变化，反馈给主窗体
        public event LinkSignalEventHandler OnLinkSignalChanged;
        public event Action<CimMessageModel> OnReceiveCimCmd;
        public event Action<CIMMode> OnCimModeChanged;
        public  event Action<string, Brush> OnSendToCimMsg;
        public  event Action<string, Brush> OnReceiveCimMsg;
        public event Func<int, PanelStatus[,]> OnGetPanelStatus;
        public static CimManager Instance = null;
        public BOECIMControl CIMControl = null;
        private EqpSts LastSts = EqpSts.IDLE;
        static CimManager()
        {
            Instance = new CimManager();
        }
        public void Init()
        {
            Task.Factory.StartNew(()=> {
                CIMControl = new BOECIMControl();
                CIMControl.LinkSignalChanged += CIMControl_LinkSignalChanged;
                CIMControl.OnConnected += OnConnected;
                CIMControl.OnCimModeChanged += OnCimModeChanged;
                CIMControl.OnShowMsg += OnShowMsg;
                CIMControl.OnReceiveCimMsg += CIMControl_OnReceiveCimMsg;
                CIMControl.OnSendToCimMsg += CIMControl_OnSendToCimMsg;
                CIMControl.CommandReceived += CommandReceivedHandle;
                AlarmManager.Manager.AlarmRemoved += Manager_AlarmRemoved;
                CIMControl.Init();
             
            });
        }

        private void CIMControl_OnSendToCimMsg(string arg1, Brush arg2)
        {
            OnSendToCimMsg?.Invoke(arg1,arg2);
            LogManager.GetLogger("SendToCimMsg").Info(arg1);
        }

        private void CIMControl_OnReceiveCimMsg(string arg1, Brush arg2)
        {
            OnReceiveCimMsg?.Invoke(arg1, arg2);
            LogManager.GetLogger("ReceiveFromCimMsg").Info(arg1);
        }
        public bool IsCimOn()
        {
            return CIMControl.IsCIMOn;
        }
        private void Manager_AlarmRemoved(AlarmHistory obj)
        {
            Task.Factory.StartNew(()=> {
                AlarmStateChangeReport(obj.AlarmDetail, false);
            });
        }

        private void CIMControl_LinkSignalChanged(LinkSignalEventArgs e)
        {
            OnLinkSignalChanged?.Invoke(e);
        }
       
        private void CommandReceivedHandle(CIMCommandNotifyArgs obj)
        {
            try
            {
                switch (obj.CommandName)
                {
                    case "CIMModeChangeCommand":
                        {
                            CIM_Mode_Change_Command(obj);
                            break;
                        }
                    case "RecipeRegisterCheckCommand":
                        {
                            Recipe_Register_Check_Command(obj);
                            break;
                        }
                    case "RecipeParameterRequestCommand":
                        {
                            Recipe_Parameter_Request_Command(obj);
                            break;
                        }
                    case "CIMMessageSetCommand":
                        {
                            CIM_Message_Set_Command(obj);
                            break;
                        }
                    case "CIMMessageClearCommand":
                        {
                            CIM_Message_Clear_Command(obj);
                            break;
                        }
                    case "VCRModeChangeCommand":
                        {
                            VCR_Mode_Change_Command(obj);
                            break;
                        }
                    case "MachineModeChangeCommand":
                        {
                            Machine_Mode_Change_Command(obj);
                            break;
                        }
                    case "DummyGlassRequest":
                        {
                            Dummy_Glass_Request(obj);
                            break;
                        }
                    case "DateTimeSetCommand":
                        {
                            Date_Time_Set_Command(obj);
                            break;
                        }

                }
            }
            finally
            { }
        }
 
        #region CimCommand
        private void CIM_Mode_Change_Command(CIMCommandNotifyArgs obj)
        {
            try
            {
                if (obj.ComandData != null && obj.ComandData.Length > 0)
                {
                    var cimMode = (CIMMode)obj.ComandData[0];
                    CimModeSet(cimMode);
                    CCLinkHelper.ccLink.WriteData(obj.CommandReplyDataAddress, 1);//1:Accept,2:Already this mode
                    OnShowMsg?.Invoke("CimCommand", $"CommandName:{obj.CommandName},CIMMode:{cimMode}", Brushes.Green);
                }
            }
            catch (Exception ex)
            {
                OnShowMsg?.Invoke("CimCommand", $"CimManager->{new StackFrame(0).GetMethod().Name}执行异常，异常信息：{ex}", Brushes.Red);
            }
        }
        private void Recipe_Register_Check_Command(CIMCommandNotifyArgs obj)
        {
            try
            {
                if (obj.ComandData != null)
                {
                    ushort recipeID = obj.ComandData[0];
                    CCLinkHelper.ccLink.WriteData(obj.CommandReplyDataAddress, 1);
                    OnShowMsg?.Invoke("CimCommand", $"CommandName:{obj.CommandName}, Recipe_ID:{recipeID}", Brushes.Green);
                }
            }
            catch (Exception ex)
            {
                OnShowMsg?.Invoke("CimCommand", $"CimManager->{new StackFrame(0).GetMethod().Name}执行异常，异常信息：{ex}", Brushes.Green);
            }
        }
        private void Recipe_Parameter_Request_Command(CIMCommandNotifyArgs obj)
        {
            try
            {
                if (obj.ComandData != null)
                {
                    ushort recipeID = obj.ComandData[0];
                    ushort unitRecipeID = obj.ComandData[1];
                    ushort unitNo = obj.ComandData[2];
                    ushort slotNo = obj.ComandData[3];
                    CCLinkHelper.ccLink.WriteData(obj.CommandReplyDataAddress, 1);
                    OnShowMsg?.Invoke("CimCommand", $"CommandName:{obj.CommandName}, Recipe_ID:{recipeID}, Unit_Recipe_ID:{unitRecipeID}, Unit_No:{unitNo}, Slot_No:{slotNo}", Brushes.Green);
                }
            }
            catch (Exception ex)
            {
                OnShowMsg?.Invoke("CimCommand", $"CimManager->{new StackFrame(0).GetMethod().Name}执行异常，异常信息：{ex}", Brushes.Green);
            }
        }
        private void CIM_Message_Set_Command(CIMCommandNotifyArgs obj)
        {
            try
            {
                if (obj.ComandData != null && obj.ComandData.Length > 0)
                {
                    //更新数据库记录
                    var msg = HTValueConverter.UshortArrayToAsciiString(obj.ComandData, 0, 16);
                    ushort cIM_Message_ID = obj.ComandData[16];
                    ushort touch_Panel_No = obj.ComandData[17];
                    CimMessageModel model = new CimMessageModel();
                    model.MessageID = cIM_Message_ID;
                    model.ReceivedTime = DateTime.Now;
                    model.Message = msg;
                    model.IsSet = true;
                    CimMessageDal dal = new CimMessageDal();
                    var lst = dal.GetIsSet(true).Where(p => p.MessageID == cIM_Message_ID).ToList();
                    int rows = 0;
                    if (lst.Count > 0)
                    {
                        rows = 1;
                    }
                    else
                    {
                        rows = dal.Insert(model);
                    }
                    if (rows >= 1)
                    {
                        OnReceiveCimCmd?.Invoke(model);
                    }
                    OnShowMsg?.Invoke("CimCommand", $"CommandName:{obj.CommandName},Message_ID:{cIM_Message_ID}, Message:{msg}", Brushes.Green);
                }
            }
            catch (Exception ex)
            {
                OnShowMsg?.Invoke("CimCommand", $"CimManager->{new StackFrame(0).GetMethod().Name}执行异常，异常信息：{ex}", Brushes.Green);
            }
        }
        private void CIM_Message_Clear_Command(CIMCommandNotifyArgs obj)
        {
            try
            {
                if (obj.ComandData != null && obj.ComandData.Length > 0)
                {
                    ushort cIM_Message_ID = obj.ComandData[0];
                    ushort touch_Panel_No = obj.ComandData[1];
                    //更新数据库记录
                    CimMessageDal dal = new CimMessageDal();
                    var lst = dal.GetIsSet(true).Where(p => p.MessageID == cIM_Message_ID).ToList();
                    CimMessageModel model = new CimMessageModel();
                    model.ReceivedTime = DateTime.Now;
                    model.Message = "CIM_Message_Clear";
                    int rows = 0;
                    for (int i = 0; i < lst.Count; i++)
                    {
                        lst[i].IsSet = false;
                        rows += dal.Update(lst[i]);
                    }
                    OnShowMsg?.Invoke("CimCommand", $"CommandName:{obj.CommandName},Message_ID:{cIM_Message_ID}, Touch_Panel_No:{touch_Panel_No}", Brushes.Green);
                }
            }
            catch (Exception ex)
            {
                OnShowMsg?.Invoke("CimCommand", $"CimManager->{new StackFrame(0).GetMethod().Name}执行异常，异常信息：{ex}", Brushes.Green);
            }
        }
        private void VCR_Mode_Change_Command(CIMCommandNotifyArgs obj)
        {
            try
            {
                if (obj.ComandData != null && obj.ComandData.Length > 0)
                {
                    ushort vcrNo = obj.ComandData[0];//1~6
                    ushort enableMode = obj.ComandData[1];//1 : Enable,2 : Disable
                    ushort vcrOperationMode = obj.ComandData[2]; //1 : Key - In mode,2 : Skip mode
                    VCRModeChangeReport(enableMode, vcrOperationMode);
                    OnShowMsg?.Invoke("CimCommand", $"CommandName:{obj.CommandName},VCR_Enable_Mode:{enableMode}, VCR_Read_Fail_Operation_Mode:{vcrOperationMode}", Brushes.Green);
                }
            }
            catch (Exception ex)
            {
                OnShowMsg?.Invoke("CimCommand", $"CimManager->{new StackFrame(0).GetMethod().Name}执行异常，异常信息：{ex}", Brushes.Green);
            }
        }
        private void Machine_Mode_Change_Command(CIMCommandNotifyArgs obj)
        {
            try
            {
                if (obj.ComandData != null && obj.ComandData.Length > 0)
                {
                    ushort machineMode = obj.ComandData[0]; //0: No Machine Mode exist.1:  Normal Mode.2: Force Clean Out Mode。3:  Skip Mode.4: Cold Run Mode.5：TR Mode
                    CCLinkHelper.ccLink.WriteData(obj.CommandReplyDataAddress, 1);
                    OnShowMsg?.Invoke("CimCommand", $"CommandName:{obj.CommandName},Machine_Mode:{machineMode}", Brushes.Green);
                    if (machineMode==1)
                    {
                        Param.Control.RunMode = RunMode.Normal;
                    }
                    else if (machineMode == 4)
                    {
                        Param.Control.RunMode = RunMode.ColdRun;
                    }
                }
            }
            catch (Exception ex)
            {
                OnShowMsg?.Invoke("CimCommand", $"CimManager->{new StackFrame(0).GetMethod().Name}执行异常，异常信息：{ex}", Brushes.Green);
            }
        }
        private void Dummy_Glass_Request(CIMCommandNotifyArgs obj)
        {
            try
            {
                if (obj.ComandData != null && obj.ComandData.Length > 0)
                {
                    CCLinkHelper.ccLink.WriteData(obj.CommandReplyDataAddress, 1);
                    OnShowMsg?.Invoke("CimCommand", $"CommandName:{obj.CommandName}", Brushes.Green);
                }
            }
            catch (Exception ex)
            {
                OnShowMsg?.Invoke("CimCommand", $"CimManager->{new StackFrame(0).GetMethod().Name}执行异常，异常信息：{ex}", Brushes.Green);
            }
        }
        private void Date_Time_Set_Command(CIMCommandNotifyArgs obj)
        {
            try
            {
                if (obj.ComandData != null && obj.ComandData.Length > 0)
                {
                    //DateTime time = HTValueConverter.UshortArrayToDateTime(obj.ComandData.Take(3).ToArray(), 0);
                    var month = obj.ComandData[0].ToString("X4").Substring(0,2);
                    var year = obj.ComandData[0].ToString("X4").Substring(2, 2);
                    var hour = obj.ComandData[1].ToString("X4").Substring(0, 2);
                    var day = obj.ComandData[1].ToString("X4").Substring(2, 2);
                    var sec = obj.ComandData[2].ToString("X4").Substring(0, 2);
                    var min = obj.ComandData[2].ToString("X4").Substring(2, 2);
                    //HTDateTime.SetLocalDateTime(time);
                    //CCLinkHelper.ccLink.WriteData(obj.CommandReplyDataAddress, 1);
                }
            }
            catch (Exception ex)
            {
                OnShowMsg?.Invoke("CimCommand", $"CimManager->{new StackFrame(0).GetMethod().Name}执行异常，异常信息：{ex}", Brushes.Green);
            }
              
        }
        #endregion
        #region Report
        public void ReceivedJobReport(GlassDataA[] glassDatas)
        {
            try
            {
                ReceivedJobReportBlock block1 = new ReceivedJobReportBlock();
                block1.JobData = glassDatas[0];
                ReceivedJobReportBlock block2 = new ReceivedJobReportBlock();
                block2.JobData = glassDatas[1];
                CIMControl.ReceivedJobReport(new ReceivedJobReportBlock[] { block1,block2});
            }
            catch (Exception ex)
            {
                OnShowMsg?.Invoke("CimReport", $"CimManager->{new StackFrame(0).GetMethod().Name}执行异常，异常信息：{ex}", Brushes.Red);
            }
        }

        public void SentOutJobReport(GlassDataA[] glassDatas)
        {
            try
            {
                SentOutJobReportBlock block1 = new SentOutJobReportBlock();
                block1.JobData = glassDatas[0];
            
                SentOutJobReportBlock block2 = new SentOutJobReportBlock();
                block2.JobData = glassDatas[1];
                CIMControl.SentOutJobReport(new SentOutJobReportBlock[] { block1, block2 });
            }
            catch (Exception ex)
            {
                OnShowMsg?.Invoke("CimReport", $"CimManager->{new StackFrame(0).GetMethod().Name}执行异常，异常信息：{ex}", Brushes.Red);
            }
        }

        public void StoredInReport(GlassDataA[] glassDatas)
        {
            try
            {
                StoredJobDataBlock block = new StoredJobDataBlock();
                block.GlassID = glassDatas[0].Glass_ID;
                block.LotSequenceNo = glassDatas[0].LOT_Sequence_No;
                block.SlotSequenceNo = glassDatas[0].Slot_Sequence_No;
                StoredJobDataBlock block2 = new StoredJobDataBlock();
                block2.GlassID = glassDatas[1].Glass_ID;
                block2.LotSequenceNo = glassDatas[1].LOT_Sequence_No;
                block2.SlotSequenceNo = glassDatas[1].Slot_Sequence_No;
                CIMControl.StoredJobReport(new StoredJobDataBlock[] { block, block2 });
            }
            catch (Exception ex)
            {
                OnShowMsg?.Invoke("CimReport", $"CimManager->{new StackFrame(0).GetMethod().Name}执行异常，异常信息：{ex}", Brushes.Red);
            }
        }

        public void FetchedOutReport(GlassDataA[] glassDatas)
        {
            try
            {
                FetchedOutJobDataBlock block = new FetchedOutJobDataBlock();
                block.GlassID = glassDatas[0].Glass_ID;
                block.LotSequenceNo = glassDatas[0].LOT_Sequence_No;
                block.SlotSequenceNo = glassDatas[0].Slot_Sequence_No;

                FetchedOutJobDataBlock block2 = new FetchedOutJobDataBlock();
                block2.GlassID = glassDatas[1].Glass_ID;
                block2.LotSequenceNo = glassDatas[1].LOT_Sequence_No;
                block2.SlotSequenceNo = glassDatas[1].Slot_Sequence_No;

                FetchedOutJobDataSubBlock subBlock = new FetchedOutJobDataSubBlock();
                subBlock.JobTypeOption = 1;
                subBlock.Position = 1;
                subBlock.UnitProcessingTime = 300;
                subBlock.UnitNo = 1;
                subBlock.PortNo = 1;
                subBlock.SlotNo = 1;
                CIMControl.FetchedOutJobReport(new FetchedOutJobDataBlock[] { block, block2 }, subBlock);
            }
            catch (Exception ex)
            {
                OnShowMsg?.Invoke("CimReport", $"CimManager->{new StackFrame(0).GetMethod().Name}执行异常，异常信息：{ex}", Brushes.Red);
            }
        }
   
        public void ProcessStartReport(GlassDataA[] glassDatas)
        {
            try
            {
                ProcessStartBlock block1 = new ProcessStartBlock();
                block1.JobCodeLotNumber1 = glassDatas[0].LOT_Sequence_No;
                block1.JobCodeSlotNumber1 = glassDatas[0].Slot_Sequence_No;
                block1.JobCodeLotNumber2 = glassDatas[1].LOT_Sequence_No;
                block1.JobCodeSlotNumber2 = glassDatas[1].Slot_Sequence_No;
                block1.UnitNo = 1;
                block1.SlotNo = 1;
                CIMControl.GlassProcessStartReport(block1);
            }
            catch (Exception ex)
            {
                OnShowMsg?.Invoke("CimReport", $"CimManager->{new StackFrame(0).GetMethod().Name}执行异常，异常信息：{ex}", Brushes.Red);
            }
        }

        public void ProcessEndReport(GlassDataA[] glassDatas)
        {
            try
            {
                ProcessEndBlock block1 = new ProcessEndBlock();
                block1.JobCodeLotNumber1 = glassDatas[0].LOT_Sequence_No;
                block1.JobCodeSlotNumber1 = glassDatas[0].Slot_Sequence_No;
                block1.JobCodeLotNumber2 = glassDatas[1].LOT_Sequence_No;
                block1.JobCodeSlotNumber2 = glassDatas[1].Slot_Sequence_No;
                block1.UnitNo = 1;
                block1.SlotNo = 1;
                CIMControl.GlassProcessEndReport(block1);
            }
            catch (Exception ex)
            {
                OnShowMsg?.Invoke("CimReport", $"CimManager->{new StackFrame(0).GetMethod().Name}执行异常，异常信息：{ex}", Brushes.Red);
            }
        }

        public void MachineStatusChangeReport(EqpSts status)
        {
            try
            {
                if (status!=LastSts)
                {
                    MachineStatusChangeReportBlock block = new MachineStatusChangeReportBlock();
                    switch (status)
                    {
                        case EqpSts.RUN:
                            block.MainMachineStatus = MachineStatus.Run;
                            block.ReasonCode = MachineStatusReasonCode.Default;
                            break;
                        case EqpSts.IDLE:
                            block.MainMachineStatus = MachineStatus.Idle;
                            block.ReasonCode = MachineStatusReasonCode.Default;
                            break;
                        case EqpSts.PAUSE:
                            block.MainMachineStatus = MachineStatus.Pause;
                            block.ReasonCode = MachineStatusReasonCode.Default;
                            break;
                        case EqpSts.STOP:
                            block.MainMachineStatus = MachineStatus.BM;
                            block.ReasonCode = MachineStatusReasonCode.Default;
                            break;
                        case EqpSts.ALARM:
                            block.MainMachineStatus = MachineStatus.BM;
                            block.ReasonCode = MachineStatusReasonCode.Default;
                            break;
                        case EqpSts.EMG:
                            block.MainMachineStatus = MachineStatus.BM;
                            block.ReasonCode = MachineStatusReasonCode.Default;
                            break;
                        case EqpSts.PM:
                            block.MainMachineStatus = MachineStatus.PM;
                            block.ReasonCode = MachineStatusReasonCode.Default;
                            break;
                        default:
                            break;
                    }
                    LastSts = status;
                    CIMControl.MachineStatusChangeReport(block);
                }
  
            }
            catch (Exception ex)
            {
                OnShowMsg?.Invoke("CimReport", $"CimManager->{new StackFrame(0).GetMethod().Name}执行异常，异常信息：{ex}", Brushes.Red);
            }
        }

        public void AlarmStateChangeReport(AlarmDetail detail, bool isCreate)
        {
            try
            {
                AlarmStatusReportBlock block = new AlarmStatusReportBlock();
                switch (detail.AlarmType)
                {
                    case "急停报警":
                        {
                            block.AlarmCode = AlarmCode.DangerForHuman;
                            break;
                        }
                    case "安全门报警":
                        {
                            block.AlarmCode = AlarmCode.MachineStatusWarning;
                            break;
                        }
                    case "压缩空气报警":
                        {
                            block.AlarmCode = AlarmCode.MachineStatusWarning;
                            break;
                        }
                    case "伺服报警":
                        {
                            block.AlarmCode = AlarmCode.MachineStatusWarning;
                            break;
                        }
                    case "Interlock报警":
                        {
                            block.AlarmCode = AlarmCode.MachineStatusWarning;
                            break;
                        }
                    case "LinkSignal交互报警":
                        {
                            block.AlarmCode = AlarmCode.MachineStatusWarning;
                            break;
                        }
                    case "自动流程报警":
                        {
                            block.AlarmCode = AlarmCode.MachineError;
                            break;
                        }
                }
                block.AlarmID = ushort.Parse(detail.AlarmId);
                block.AlarmState = isCreate ? AlarmStatus.AlarmSet : AlarmStatus.AlarmClear;
                block.AlarmUnit = 0;
                block.AlarmText = detail.DescriptionCN;
                if (detail.AlarmLevel == AlarmLevel.HeavyAlarm)
                {
                    block.AlarmType = AlarmType.Error;
                }
                else if (detail.AlarmLevel == AlarmLevel.LightAlarm)
                {
                    block.AlarmType = AlarmType.Warning;
                }
                CIMControl.AlarmStateChangeReport(block);
            }
            catch (Exception ex)
            {
                OnShowMsg?.Invoke("CimReport", $"CimManager->{new StackFrame(0).GetMethod().Name}执行异常，异常信息：{ex}", Brushes.Red);
            }
        }

        public JobDataRequestReplyBlock JobDataRequest(string glassID, GlassDataA glassData)
        {
            try
            {
                JobDataRequestBlock block = new JobDataRequestBlock();
                block.JobDataRequestType = JobDataRequestType.GlassCode;
                block.RequestGlassID = glassID;
                block.RequestGlassLotNumber = glassData.LOT_Sequence_No;
                block.RequestGlassSlotNumber = glassData.Slot_Sequence_No;
                block.SubUnitSlotNo = 0;
                block.UnitNo = 0;
                var replyBlock = CIMControl.JobDataRequest(block);
                return replyBlock;
            }
            catch (Exception ex)
            {
                OnShowMsg?.Invoke("CimReport", $"CimManager->{new StackFrame(0).GetMethod().Name}执行异常，异常信息：{ex}", Brushes.Red);
                return null;
            }
        }

        public void JobDataChangeReport(GlassDataA glassData)
        {
            JobDataChangeBlock block = new JobDataChangeBlock();
            block.JobData = glassData;
             CIMControl.JobDataChangeReport(block);
        }

        public void ProcessDataReport(GlassDataA glassDatas,int glassNum)
        {
            try
            {
                var model=new CellStartTimeDal().GetByID(glassDatas.Glass_ID);
                var resultModel = new TotalInfoDal().GetByIDAndTime(model.PanelID,model.StartTime);
                ProcessDataReportBlock block1 = new ProcessDataReportBlock();
                block1.GlassID = glassDatas.Glass_ID;
                block1.LocalProcessStartTime = (DateTime)resultModel.StartTime;
                block1.LocalProcessEndTime = (DateTime)resultModel.EndTime;
                block1.SlotNo = glassDatas.Slot_Sequence_No;
                block1.UnitNo = 1;
                block1.RecipeStepNo = 1;
                //临时测试
                for (int i = 0; i < 10; i++)
                {
                    block1.DVData[i] = (ushort)i;
                }
           
                if (glassNum==0)
                {
                    CIMControl.ProcessDataReport(block1);
                }
                else
                {
                    CIMControl.ProcessDataReport(block1);
                }
            }
            catch (Exception ex)
            {
                OnShowMsg?.Invoke("CimReport", $"CimManager->{new StackFrame(0).GetMethod().Name}执行异常，异常信息：{ex}", Brushes.Red);
            }
        }

        public void CurrentRecipeIDChangeReport(Recipe recipe)
        {
            try
            {
                CurrentRecipeBlock block = new CurrentRecipeBlock();
                block.RecipeID = ushort.Parse(recipe.PPID);
                CIMControl.CurrentRecipeIDChangeReport(block);
            }
            catch (Exception ex)
            {
                OnShowMsg?.Invoke("CimReport", $"CimManager->{new StackFrame(0).GetMethod().Name}执行异常，异常信息：{ex}", Brushes.Red);
            }
        }

        public void RecipeParameterDataChangeReport(Recipe recipe)
        {
            try
            {
                RecipeParameterDataBlock block = new RecipeParameterDataBlock();
                block.RecipeID = ushort.Parse(recipe.PPID);
                CIMControl.RecipeParameterDataChangeReport(block);
            }
            catch (Exception ex)
            {
                OnShowMsg?.Invoke("CimReport", $"CimManager->{new StackFrame(0).GetMethod().Name}执行异常，异常信息：{ex}", Brushes.Red);
            }
        }
        public void RecipeListChangeReport(List<Recipe> recipeList)
        {
            try
            {
                MachineRecipeBlock block = new MachineRecipeBlock();
                block.MachineRecipeList = recipeList.Select(p => ushort.Parse(p.PPID)).ToArray();
                CIMControl.RecipeListChangeReport(block);
            }
            catch (Exception ex)
            {
                OnShowMsg?.Invoke("CimReport", $"CimManager->{new StackFrame(0).GetMethod().Name}执行异常，异常信息：{ex}", Brushes.Red);
            }
        }

        public void CimMessageConfirmReport(ushort msgID)
        {
            try
            {
                CimMessageConfirmBlock block = new CimMessageConfirmBlock();
                block.CIM_Message_ID = msgID;
                block.Touch_Panel_No = 1;
                CIMControl.CIMMessageConfirmReport(block);
            }
            catch (Exception ex)
            {
                OnShowMsg?.Invoke("CimReport", $"CimManager->{new StackFrame(0).GetMethod().Name}执行异常，异常信息：{ex}", Brushes.Red);
            }
        }

        public void VCREventReport(string upstreamId, string scanerId, GlassDataA glassData, bool result, int glassNum)
        {
            try
            {
                VCREventBlock block = new VCREventBlock();
                block.GlassID = upstreamId;
                block.LotNumber = glassData.LOT_Sequence_No;
                block.SlotNumber = glassData.Slot_Sequence_No;
                block.VcrReadID = scanerId;
                block.UnitNo = 0;
                block.VCRNo = 1;
                block.VcrResult = (ushort)(result ? 0 : 1);
                CIMControl.VCREventReport(block);
            }
            catch (Exception ex)
            {
                OnShowMsg?.Invoke("CimReport", $"CimManager->{new StackFrame(0).GetMethod().Name}执行异常，异常信息：{ex}", Brushes.Red);
            }
        }
        public void VCRModeChangeReport(ushort enableMode, ushort readFailOperationMode)
        {
            try
            {
                VCRModeBlock block = new VCRModeBlock();
                block.VCR_Enable_Mode = enableMode;//1 : Enable,2 : Disable
                block.VCR_Read_Fail_Operation_Mode = readFailOperationMode; //1 : Key - In mode,2 : Skip mode
                CIMControl.VCRModeChangeReport(block);
            }
            catch (Exception ex)
            {
                OnShowMsg?.Invoke("CimReport", $"CimManager->{new StackFrame(0).GetMethod().Name}执行异常，异常信息：{ex}", Brushes.Red);
            }
        }

        public PanelInformationReplyBlock PanelDataInformationDownloadRequest(GlassDataA glassData)
        {
            try
            {
                PanelInformationRequestBlock block = new PanelInformationRequestBlock();
                block.LotSequenceNumber = glassData.LOT_Sequence_No;
                block.SlotSequenceNumber = glassData.Slot_Sequence_No;
                block.SubUnitSlotNo = 0;
                block.UnitNo = 0;
                return CIMControl.PanelDataInformationDownloadRequest(block);
            }
            catch (Exception ex)
            {
                OnShowMsg?.Invoke("CimReport", $"CimManager->{new StackFrame(0).GetMethod().Name}执行异常，异常信息：{ex}", Brushes.Red);
                return null;
            }
        }

        public void PanelDataUpdateReport(string glassID, int glassNum)
        {
            try
            {
                var model = new JobDataDal().Get(glassID);
                GlassDataA jobData = new GlassDataA();
                jobData.FromString(model.JobData);
                var cellStartTimeDal = new CellStartTimeDal().GetByID(glassID);
                var list = new OneCellDetectInfoDal().GetByIDAndTime(glassID, cellStartTimeDal.StartTime);
                var panelSts = OnGetPanelStatus(glassNum);
                PanelInformationUpdateBlock block = new PanelInformationUpdateBlock();
                block.SlotSequenceNumber = jobData.Slot_Sequence_No;
                block.LotSequenceNumber = jobData.LOT_Sequence_No;
                block.SubUnitSlotNo = 0;
                if (list != null)
                {
                    int count = 0, panelJudgeDataCount = 0;
                    string onePanelJudgeData = string.Empty;
                    string[] tempArray = new string[3];
                    string tempStr = string.Empty;
                    for (int i = 0; i < Param.Panel.CellCountX; i++)
                    {
                        StringBuilder sb = new StringBuilder();
                        for (int j = 0; j < Param.Panel.CellCountY; j++)
                        {
                            var panel = list.Where(p => p.CellNum == j && p.ColumnNum == i).FirstOrDefault();
                            string temp = panel == null ? "OK" : panel.Result;
                            if (panelSts != null && panelSts[i, j] != null)
                            {
                                if (panelSts[i, j].Status == PanelStatusEnum.UpstreamNG)
                                {
                                    temp = "NG";
                                }
                            }
                            string cellID = $"{Global.CellID[j]}0{i + 1}";
                            sb.Append($"{cellID}{temp}-");
                            int result = temp == "NG" ? 14 : 7;
                            var binaryValue = ConvertHelper.DecimalToBinary(result, 5, true);
                            tempArray[count] = binaryValue;
                            count++;
                            if (count == 3)
                            {
                                onePanelJudgeData = "0" + tempArray[2] + tempArray[1] + tempArray[0];
                                block.PanelJudge[panelJudgeDataCount] = Convert.ToUInt16(onePanelJudgeData, 2);
                                onePanelJudgeData = string.Empty;
                                panelJudgeDataCount++;
                                count = 0;
                            }
                        }
                        tempStr += sb.ToString().TrimEnd('-') + "\n";
                    }
                    tempStr = tempStr.TrimEnd('\n');
                    LogManager.GetLogger("PanelJudgeData").Info($"发送给CIM的PanelJudge结果:\n{tempStr},\nPanelID:{glassID}");
                    if (Param.Control.UpdatePanelJudgeEnable)
                    {
                        CIMControl.PanelDataUpdateReport(block);
                    }
                }
                else
                {
                    OnShowMsg?.Invoke("CimHelper", $"CimHelper->PanelDataUpdate方法获取PanelInfo失败，List为空", Brushes.Red);
                }
            }
            catch (Exception ex)
            {
                OnShowMsg?.Invoke("CimReport", $"CimManager->{new StackFrame(0).GetMethod().Name}执行异常，异常信息：{ex}", Brushes.Red);
            }
        }

        public void JobJudgeResultReport(GlassDataA glassDatas)
        {
            try
            {
                CellStartTimeDal cellStartTimeDal = new CellStartTimeDal();
                var model1 = cellStartTimeDal.GetByID(glassDatas.Glass_ID);
                TotalInfoDal dal = new TotalInfoDal();
                var glass1Info = dal.GetByIDAndTime(model1.PanelID, model1.StartTime);

                JobJudgeChangeReportBlock block1 = new JobJudgeChangeReportBlock();
                block1.LotSequenceNumber = glassDatas.LOT_Sequence_No;
                block1.SlotSequenceNumber = glassDatas.Slot_Sequence_No;
                block1.UnitNo = 0;
                block1.SubUnitSlotNo = 0;
                block1.JobJudgeResult = glass1Info.Result == "OK" ? JobJudgeResult.Good : JobJudgeResult.NotGood;
                CIMControl.JobJudgeResultReport(block1);
            }
            catch (Exception ex)
            {
                OnShowMsg?.Invoke("CimReport", $"CimManager->{new StackFrame(0).GetMethod().Name}执行异常，异常信息：{ex}", Brushes.Red);
            }
        }

        public void MachineModeChangeReport(ushort machineMode)
        {
            try
            {
                MachineModeBlock block1 = new MachineModeBlock();
                block1.MachineMode = machineMode;
                CIMControl.MachineModeChangeReport(block1);
            }
            catch (Exception ex)
            {
                OnShowMsg?.Invoke("CimReport", $"CimManager->{new StackFrame(0).GetMethod().Name}执行异常，异常信息：{ex}", Brushes.Red);
            }
        }

        public void MachineDummyGlassRequest()
        {
            MachineDummyGlassRequestDataBlock block = new MachineDummyGlassRequestDataBlock();
            block.DummyType = 1;
            block.DummyGlassCount = 1;
            block.DummyRequestOption = 1;
            CIMControl.MachineDummyGlassRequest(block);
        }
        /// <summary>
        /// 向CIM请求 VCRValidation，CIM返回 1 OK， 2 NOT EXIST， 和 JobData
        /// </summary>
        /// <param name="requestBlock"></param>
        /// <returns></returns>
        public bool VCRValidationRequest(string glassID, GlassDataA glassData, out GlassDataA glassDataOut)
        {
            try
            {
                var replyBlock = JobDataRequest(glassID, glassData);
                glassDataOut = replyBlock.JobData;
                return replyBlock.Status == ReplyStatus.Exist;
            }
            catch (Exception ex)
            {
                glassDataOut = null;
                OnShowMsg?.Invoke("CimReport", $"CimManager->{new StackFrame(0).GetMethod().Name}执行异常，异常信息：{ex}", Brushes.Red);
                return false;
            }
        }
      
        public void WriteJobData(string[] glassID)
        {
            try
            {
                GlassDataA jobData1 = new GlassDataA();
                if (string.IsNullOrEmpty(glassID[0]) == false)
                {
                    string temp1 = new JobDataDal().Get(glassID[0]).JobData;
                    jobData1.FromString(temp1);
                }
                GlassDataA jobData2 = new GlassDataA();
                if (string.IsNullOrEmpty(glassID[1]) == false)
                {
                    string temp2 = new JobDataDal().Get(glassID[1]).JobData;
                    jobData2.FromString(temp2);
                }
                CIMControl.WriteGlassDataToDown(new GlassDataA[] { jobData1, jobData2 });
            }
            catch (Exception ex)
            {
                OnShowMsg?.Invoke("Cim", $"CimManager->{new StackFrame(0).GetMethod().Name}执行异常，异常信息：{ex}", Brushes.Red);
            }
        }
        public void JobDataClear()
        {

        }
        public void CimModeSet(CIMMode mode)
        {
            CIMControl.CIMModeChangeReport(mode);
        }
     
        public void ProcessDataSVFLOAT()
        {

        }
        #endregion
        public void WriteLocalToUp(bool[]values)
        {
            CIMControl.WriteLocalToUp(values);
        }
        public void WriteLocalToDown(bool[] values)
        {
            CIMControl.WriteLocalToDown(values);
        }
        public void UnInit()
        {
            CIMControl?.UInit();
        }
    }
}
