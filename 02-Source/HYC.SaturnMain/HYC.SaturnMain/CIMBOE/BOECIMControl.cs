using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CIMBase;
using CIMBase.CCLink;
using System.Threading;
using System.Threading.Tasks;
using HYC.HTFile;
using CIMBOE.Block;
using System.Drawing;
using HYC.HTLog;

namespace CIMBOE
{
    public class BOECIMControl
    {
        public  event Action<string,string, Brush> OnShowMsg;
        public  event Action<bool> OnConnected;
        public  event Action<CIMCommandNotifyArgs> CommandReceived;
        public event Action<CIMMode> OnCimModeChanged;
        public event Action<string, Brush> OnSendToCimMsg;
        public event Action<string, Brush> OnReceiveCimMsg;
        const int LinkSignalCount = 58;
        const int GlassDataLength = 84;
        const int FromBase = 16;
        public  bool IsCIMOn { get; set; }
        private bool[] UpToLocalSignalsOld = new bool[LinkSignalCount];     //Link信号：Up     =>  Local
        private bool[] LocalToUpSignalsOld = new bool[LinkSignalCount];   //Link信号：Local  =>  Up

        private bool[] DownToLocalSignalsOld = new bool[LinkSignalCount];  //Link信号：Down   =>  Local
        private bool[] LocalToDownSignalsOld = new bool[LinkSignalCount]; //Link信号：Local  =>  Down
        string LinkSignalFileName = AppDomain.CurrentDomain.BaseDirectory + "Config\\" + "LinkSignalAddrInfo.xml";
        public LinkSignalAddr LinkSignalAddr = null;
        private int[] TimeCount = new int[4]; //link信号读取计数
        public void Init()
        {
            try
            {
                LinkSignalAddr = HTXmlHelper.EntityFromXMLFile<LinkSignalAddr>(LinkSignalFileName);
                CCLinkHelper.CIMBaseLogUpLoad += CCLinkHelper_CIMBaseLogUpLoad;
                CCLinkHelper.CommandReceived += CCLinkHelper_CommandReceived;
                var result = CCLinkHelper.Init();//初始化并开始监控CIM的主动请求方法
                OnConnected?.Invoke(result);
               // CCLinkHelper.ccLink.SetBit(0x3100);//RunningModeNotification
                //LinkSignal
                Task.Factory.StartNew(UpToLocal);
                Task.Factory.StartNew(LocalToUp);
                Task.Factory.StartNew(DownToLocal);
                Task.Factory.StartNew(LocalToDown);
            }
            catch (Exception ex)
            {
                OnConnected?.Invoke(false);
                OnShowMsg?.Invoke("Cim", $"BOECIMControl->Init方法执行异常，异常信息：{ex}", Brushes.Red);
            }
        }

        private void CCLinkHelper_CIMBaseLogUpLoad(string obj)
        {
            LogManager.GetLogger("CCLinkHelper").Info(obj);
        }

        private  void CCLinkHelper_CommandReceived(CIMCommandNotifyArgs e, EventWaitHandle waitHandle)
        {
            try
            {
                waitHandle.Set();               
                OnReceiveCimMsg?.Invoke("CommandName:" + e.CommandName + ",CommandDatas:" +string.Join("?", e.ComandData),Brushes.Green);
                if (CommandReceived != null)
                {
                   // if (IsCIMOn)
                    {
                        CommandReceived(e);
                    }
                }
            }
            catch (Exception ex)
            {
                OnReceiveCimMsg?.Invoke($"BOECIMControl->CommandReceived方法执行异常，异常信息：{ex}", Brushes.Red);
            }
        }

        internal  void ShowMsg(string logName,string msg,bool isError=false)
        {
            OnSendToCimMsg?.Invoke( msg, isError?Brushes.Red:Brushes.Green);
        }

        public  void UInit()
        {
            try
            {
                CCLinkHelper.UInit();
                //设置地址为3100的数据为长On
                CCLinkHelper.ccLink.ResetBit(0x3100);
            }
            catch (Exception ex)
            {
                OnShowMsg?.Invoke("Cim",$"BOECIMControl->UInit方法执行异常，异常信息：{ex}",Brushes.Red);
            }
        }

        #region --Report--
        public OperationResult ReceivedJobReport(ReceivedJobReportBlock[] jobDatas)
        {
            if (!IsCIMOn)
            {
                return OperationResult.OK;
            }
            ShowMsg("CimReport", "ReceivedJobReport");
            return CIMBase.CCLink.CCLinkHelper.Report("ReceivedJobReport", jobDatas);
        }

        public OperationResult SentOutJobReport(SentOutJobReportBlock[] jobDatas)
        {
            if (!IsCIMOn)
            {
                return OperationResult.OK;
            }
            ShowMsg("CimReport", "SentOutJobReport");
            return CIMBase.CCLink.CCLinkHelper.Report("SentOutJobReport", jobDatas);
        }
        public OperationResult SentOutJobReport2(SentOutJobReportBlock jobData)
        {
            if (!IsCIMOn)
            {
                return OperationResult.OK;
            }
            ShowMsg("CimReport", "SentOutJobReport2");
            return CIMBase.CCLink.CCLinkHelper.Report("SentOutJobReport2", jobData);
        }

        public OperationResult StoredJobReport(StoredJobDataBlock[] jobData)
        {
            if (!IsCIMOn)
            {
                return OperationResult.OK;
            }
            ShowMsg("CimReport", "StoredJobReport");
            return CIMBase.CCLink.CCLinkHelper.Report("StoredJobReport", jobData);
        }

        public OperationResult FetchedOutJobReport(FetchedOutJobDataBlock[] blocks, FetchedOutJobDataSubBlock subBlock)
        {
            if (!IsCIMOn)
            {
                return OperationResult.OK;
            }
            List<ICIMData> list = new List<ICIMData>();
            list.AddRange(blocks);
            list.Add(subBlock);
            ShowMsg("CimReport", "FetchedOutJobReport");
            return CIMBase.CCLink.CCLinkHelper.Report("FetchedOutJobReport", list.ToArray());
        }
      
        public OperationResult GlassProcessStartReport(ProcessStartBlock jobData)
        {
            if (!IsCIMOn)
            {
                return OperationResult.OK;
            }
            ShowMsg("CimReport", "GlassProcessStartReport");
            return CIMBase.CCLink.CCLinkHelper.Report("GlassProcessStartReport", jobData);
        }

        public OperationResult GlassProcessEndReport(ProcessEndBlock jobData)
        {
            if (!IsCIMOn)
            {
                return OperationResult.OK;
            }
            ShowMsg("CimReport", "GlassProcessEndReport");
            return CIMBase.CCLink.CCLinkHelper.Report("GlassProcessEndReport", jobData);
        }

        public OperationResult MachineStatusChangeReport(MachineStatusChangeReportBlock datablock)
        {
            if (!IsCIMOn)
            {
                return OperationResult.OK;
            }
            ShowMsg("CimReport", "MachineStatusChangeReport");
            return CIMBase.CCLink.CCLinkHelper.Report("MachineStatusChangeReport", datablock);

        }

        public OperationResult AlarmStateChangeReport(AlarmStatusReportBlock datablock)
        {
            if (!IsCIMOn)
            {
                return OperationResult.OK;
            }
            ShowMsg("CimReport", "AlarmStateChangeReport");
            return CIMBase.CCLink.CCLinkHelper.Report("AlarmStateChangeReport", datablock);
        }

        public JobDataRequestReplyBlock JobDataRequest(JobDataRequestBlock requestBlock)
        {
            JobDataRequestReplyBlock result = new JobDataRequestReplyBlock();
            List<ushort[]> data = new List<ushort[]>();
            ShowMsg("CimReport", "JobDataRequest");
            var requestReturn = CIMBase.CCLink.CCLinkHelper.Request("JobDataRequest", requestBlock, out data);
            if (requestReturn == OperationResult.OK && data != null && data.Count > 0)
            {
                result.LoadDataFormArrary(data[0]);
                return result;
            }
            return null;
        }

        public OperationResult JobDataChangeReport(JobDataChangeBlock block)
        {
            if (!IsCIMOn)
            {
                return OperationResult.OK;
            }
            ShowMsg("CimReport", "JobDataChangeReport");
            return CIMBase.CCLink.CCLinkHelper.Report("JobDataChangeReport", block);
        }

        public OperationResult ProcessDataReport(ProcessDataReportBlock datablock)
        {
            if (!IsCIMOn)
            {
                return OperationResult.OK;
            }
            ShowMsg("CimReport", "ProcessDataReport");
            return CIMBase.CCLink.CCLinkHelper.Report("ProcessDataReport", datablock);
        }
        public OperationResult ProcessDataReport2(ProcessDataReportBlock datablock)
        {
            if (!IsCIMOn)
            {
                return OperationResult.OK;
            }
            ShowMsg("CimReport", "ProcessDataReport2");
            return CIMBase.CCLink.CCLinkHelper.Report("ProcessDataReport2", datablock);
        }
        public OperationResult CurrentRecipeIDChangeReport(CurrentRecipeBlock datablock)
        {
            if (!IsCIMOn)
            {
                return OperationResult.OK;
            }
            ShowMsg("CimReport", "CurrentRecipeIDChangeReport");
            return CIMBase.CCLink.CCLinkHelper.Report("CurrentRecipeIDChangeReport", datablock);
        }

        public OperationResult RecipeListChangeReport(MachineRecipeBlock datablock)
        {
            if (!IsCIMOn)
            {
                return OperationResult.OK;
            }
            ShowMsg("CimReport", "RecipeListChangeReport");
            return CIMBase.CCLink.CCLinkHelper.Report("RecipeListChangeReport", datablock);
        }

        public OperationResult RecipeParameterDataChangeReport(RecipeParameterDataBlock datablock)
        {
            if (!IsCIMOn)
            {
                return OperationResult.OK;
            }
            ShowMsg("CimReport", "RecipeParameterDataChangeReport");
            return CIMBase.CCLink.CCLinkHelper.Report("RecipeParameterDataChangeReport", datablock);
        }

        public OperationResult CIMMessageConfirmReport(CimMessageConfirmBlock datablock)
        {
            if (!IsCIMOn)
            {
                return OperationResult.OK;
            }
            ShowMsg("CimReport", "CIMMessageConfirmReport");
            return CIMBase.CCLink.CCLinkHelper.Report("CIMMessageConfirmReport", datablock);
        }

        public OperationResult VCREventReport(VCREventBlock block)
        {
            if (!IsCIMOn)
            {
                return OperationResult.OK;
            }
            ShowMsg("CimReport", "VCREventReport");
            return CIMBase.CCLink.CCLinkHelper.Report("VCREventReport", block);
        }

        public OperationResult VCRModeChangeReport(VCRModeBlock block)
        {
            if (!IsCIMOn)
            {
                return OperationResult.OK;
            }
            ShowMsg("CimReport", "VCRModeChangeReport");
            return CIMBase.CCLink.CCLinkHelper.Report("VCRModeChangeReport", block);
        }

        public PanelInformationReplyBlock PanelDataInformationDownloadRequest(PanelInformationRequestBlock block)
        {
            if (!IsCIMOn)
            {
                return null;
            }
            ShowMsg("CimReport", "PanelDataInformationDownloadRequest");
            PanelInformationReplyBlock replyBlock = new PanelInformationReplyBlock();
            var requestReturn = CIMBase.CCLink.CCLinkHelper.Request("PanelDataInformationDownloadRequest", block, out List<ushort[]> datas);
            if (requestReturn == OperationResult.OK && datas != null && datas.Count > 0)
            {
                replyBlock.LoadDataFormArrary(datas[0]);
                return replyBlock;
            }
            return null;
        }

        public OperationResult PanelDataUpdateReport(PanelInformationUpdateBlock block)
        {
            if (!IsCIMOn)
            {
                return OperationResult.OK;
            }
            ShowMsg("CimReport", "PanelDataUpdateReport");
            return CIMBase.CCLink.CCLinkHelper.Report("PanelDataUpdateReport", new ICIMData[] { block });
        }

        public OperationResult JobJudgeResultReport(JobJudgeChangeReportBlock datablock)
        {
            if (!IsCIMOn)
            {
                return OperationResult.OK;
            }
            ShowMsg("CimReport", "JobJudgeResultReport");
            return CIMBase.CCLink.CCLinkHelper.Report("JobJudgeResultReport", datablock);
        }

        public OperationResult MachineModeChangeReport(MachineModeBlock block)
        {
            if (!IsCIMOn)
            {
                return OperationResult.OK;
            }
            ShowMsg("CimReport", "MachineModeChangeReport:" + block.MachineMode.ToString());
            return CIMBase.CCLink.CCLinkHelper.Report("MachineModeChangeReport", block);
        }

        public DummyGlassRequestReturnBlock MachineDummyGlassRequest(MachineDummyGlassRequestDataBlock block)
        {
            if (!IsCIMOn)
            {
                return null;
            }
            ShowMsg("CimReport", "MachineDummyGlassRequest" );
            var requestReturn= CIMBase.CCLink.CCLinkHelper.Request("MachineDummyGlassRequest", block, out List<ushort[]> datas);
            DummyGlassRequestReturnBlock replyBlock = new DummyGlassRequestReturnBlock();
            if (requestReturn == OperationResult.OK && datas != null && datas.Count > 0)
            {
                replyBlock.LoadDataFormArrary(datas[0]);
                return replyBlock;
            }
            return null;
        }

        public OperationResult CIMModeChangeReport(CIMMode mode)
        {
            //var result = CIMBase.CCLink.CCLinkHelper.Report("CIMModeChangeReport", (ushort)mode);//1.on 2.off
           // if (result == OperationResult.OK )
            {
                OnCimModeChanged?.Invoke(mode);//上报至主界面的CIM --on的状态
                IsCIMOn = mode == CIMMode.CIMON;
                if (IsCIMOn)
                {
                    CCLinkHelper.ccLink.SetBit(0xA01);
                    CCLinkHelper.ccLink.SetBit(0xA05);
                }
                else
                {
                    CCLinkHelper.ccLink.ResetBit(0xA01);
                }
                
            }
            ShowMsg("CimReport", "CIMModeChangeReport:" + mode.ToString());
            return OperationResult.OK;
        }

    
        public  OperationResult RemovedJobReport(RemovedJobReportBlock datablock)
        {
            if (!IsCIMOn)
            {
                return OperationResult.OK;
            }
            ShowMsg("CimReport", "RemovedJobReport");
            return CIMBase.CCLink.CCLinkHelper.Report("RemovedJobReport", datablock);
        }
     
        #endregion
        //上游->本机
        public void UpToLocal()
        {
            while (true)
            {
                try
                {
                    bool[] signals = CCLinkHelper.ccLink.ReadMultiBit2(Convert.ToInt32(LinkSignalAddr.UpToLocalAddr, FromBase), LinkSignalCount);
                    if (signals != null && signals.Length > 0)
                    {
                        if (IsSignalChanged(UpToLocalSignalsOld, signals))
                        {
                            LinkSignalEventArgs e = new LinkSignalEventArgs();
                            e.Index = 0;
                            e.Signals = signals;
                            if (signals[14])//Sendable
                            {
                                GlassDataA glassData1 = new GlassDataA(), glassData2 = new GlassDataA();
                                ushort[] datas1 = CCLinkHelper.ReadMultiData(Convert.ToInt32(LinkSignalAddr.UpToLocalWordAddr1, FromBase), GlassDataLength);
                                glassData1.FromArray(datas1);
                                ushort[] datas2 = CCLinkHelper.ReadMultiData(Convert.ToInt32(LinkSignalAddr.UpToLocalWordAddr2, FromBase), GlassDataLength);
                                glassData2.FromArray(datas2);
                                e.Glass_Datas = new GlassDataA[] { glassData1, glassData2 };
                            }
                            OnLinkSignalChanged(e);
                        }
                        UpToLocalSignalsOld = signals;
                        //TimeCount[3]++;
                        //if (TimeCount[3] == 100)
                        //    TimeCount[3] = 0;
                    }
                    Thread.Sleep(50);
                }
                catch (Exception ex)
                {
                    Thread.Sleep(1000);
                }
            }
        }
        //下游->本机
        public void DownToLocal()
        {
            while (true)
            {
                try
                {
                    bool[] signals = CIMBase.CCLink.CCLinkHelper.ccLink.ReadMultiBit2(Convert.ToInt32(LinkSignalAddr.DownToLocalAddr, FromBase), LinkSignalCount);
                    if (signals != null && signals.Length > 0)
                    {
                        if (IsSignalChanged(DownToLocalSignalsOld, signals))
                        {
                            LinkSignalEventArgs e = new LinkSignalEventArgs();
                            e.Index = 1;
                            e.Signals = signals;
                            e.Glass_Datas = null;
                            OnLinkSignalChanged(e);
                        }
                        DownToLocalSignalsOld = signals;
                        //TimeCount[2]++;
                        //if (TimeCount[2] == 100)
                        //    TimeCount[2] = 0;
                    }
                    Thread.Sleep(50);
                }
                catch (Exception ex)
                {
                    Thread.Sleep(1000);
                }
            }
        }
        //本机->下游
        public void LocalToDown()
        {
            while (true)
            {
                try
                {
                    bool[] signals = CIMBase.CCLink.CCLinkHelper.ccLink.ReadMultiBit2(Convert.ToInt32(LinkSignalAddr.LocalToDownAddr, FromBase), LinkSignalCount);
                    if (signals != null && signals.Length > 0)
                    {
                        if (IsSignalChanged(LocalToDownSignalsOld, signals))
                        {
                            LinkSignalEventArgs e = new LinkSignalEventArgs();
                            e.Index = 2;
                            e.Signals = signals;
                            e.Glass_Datas = null;
                            OnLinkSignalChanged(e);
                        }
                        LocalToDownSignalsOld = signals;
                        //TimeCount[1]++;
                        //if (TimeCount[1] == 100)
                        //    TimeCount[1] = 0;
                    }
                    Thread.Sleep(50);
                }
                catch (Exception ex)
                {
                    Thread.Sleep(1000);
                }
            }
        }
        //本机->上游
        public void LocalToUp()
        {
            while (true)
            {
                try
                {
                    bool[] signals = CIMBase.CCLink.CCLinkHelper.ccLink.ReadMultiBit2(Convert.ToInt32(LinkSignalAddr.LocalToUpAddr, FromBase), LinkSignalCount);
                    if (signals != null && signals.Length > 0)
                    {
                        if (IsSignalChanged(LocalToUpSignalsOld, signals))
                        {
                            LinkSignalEventArgs e = new LinkSignalEventArgs();
                            e.Index = 3;
                            e.Signals = signals;
                            e.Glass_Datas = null;
                            OnLinkSignalChanged(e);
                        }
                        LocalToUpSignalsOld = signals;
                        //TimeCount[0]++;
                        //if (TimeCount[0] == 100)
                        //    TimeCount[0] = 0;
                    }
                    Thread.Sleep(50);
                }
                catch (Exception ex)
                {
                    Thread.Sleep(1000);
                }
            }
        }
        //信号是否发生变化
        private bool IsSignalChanged(bool[] signalsOld, bool[] signalsNew)
        {
            bool result = false;
            if (signalsOld == null || signalsNew == null) return true;

            for (int i = 0; i < signalsOld.Length; i++)
            {
                if (signalsOld[i] != signalsNew[i])
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
        public event LinkSignalEventHandler LinkSignalChanged;
        private void OnLinkSignalChanged(LinkSignalEventArgs e)
        {
            LinkSignalChanged?.Invoke(e);
        }
        public void  WriteGlassDataToDown(GlassDataA[] glassDatas)
        {
            try
            {
                if (glassDatas[1]!=null)
                {
                    CCLinkHelper.WirteMulutiData(Convert.ToInt32(LinkSignalAddr.LocalToDownWordAddr1, FromBase), glassDatas[1].ToArray());
                }
                if (glassDatas[0] != null)
                {
                    CCLinkHelper.WirteMulutiData(Convert.ToInt32(LinkSignalAddr.LocalToDownWordAddr2, FromBase), glassDatas[0].ToArray());
                }
              
            }
            catch (Exception)
            {

            }
        }
        public void WriteLocalToUp(bool[] values)
        {
            CCLinkHelper.ccLink.SetResetMultiBit(Convert.ToInt32(LinkSignalAddr.LocalToUpAddr, FromBase), values);
        }
        public void WriteLocalToDown(bool[] values)
        {
            CCLinkHelper.ccLink.SetResetMultiBit(Convert.ToInt32(LinkSignalAddr.LocalToDownAddr, FromBase), values);
        }
    }

    public enum CIMMode
    { 
        CIMON=1,
        CIMOFF=2
    }

    public enum MachineAutoMode
    {
        AUTO=1,
        MANUAL=2
    }
}
