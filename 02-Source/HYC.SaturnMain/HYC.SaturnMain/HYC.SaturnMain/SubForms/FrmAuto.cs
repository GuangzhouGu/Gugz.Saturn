using HYC.HTPCBase.Extension.Entities;
using HYC.HTPCBase.Extension.Views;
using HYC.SaturnDB.DAL;
using HYC.SaturnDB.Model;
using HYC.HTLog;
using HYC.SaturnMain.Params;
using HYC.SaturnMain.Detections;
using HYC.SaturnMain.Devices.ClientPC;
using HYC.SaturnMain.Devices.Motion;
using HYC.SaturnMain.Entities;
using HYC.SaturnMain.Station;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HYC.SaturnMain.UserControls;
using HYC.HTFile.HTINI;
using HYC.SaturnMain.GlassCrds;
using HYC.HTPCBase.Extension.Recipe;
using HYC.SaturnMain;
using CIMBOE;
using BOECIM.Devices.CIM;
using CIMBOE.Block;

namespace HYC.SaturnMain.SubForms
{
    public partial class FrmAuto : ViewBase
    {
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }
        public FrmAuto()
        {
            InitializeComponent();
            this.IsNeedLogin = false;
            this.ButtonName = "检测界面";
            this.ButtonNameEn = "Auto\nDetect";
            this.Level = UserLevel.Engineer;
            this.Index = 0;
            this.ButtonImage = HYC.SaturnMain.Properties.Resources.Detect;
            CimManager.Instance.OnLinkSignalChanged += Instance_OnLinkSignalChanged;
        }
        GlassMapCtrl[] GlassMaps = null;
        private void FormAuto_Load(object sender, EventArgs e)
        {
            InitControls();
            RegisterEvent();
            Task.Factory.StartNew(()=> {
                LoadGlassInfo();
            });
        }
        private void RegisterEvent()
        {
            ProbeGantry.OnUpdateCellStatus += ReFreshPanelStatus;
            ProbeGantry.OnTriggerSignal += ProbeGantry_OnTriggerSignal;
            ProcessCmd.OnUpdatePanelStatus += ReFreshPanelStatus;
            DetectGantry.OnUpdateGlassID += DetectGantry_OnUpdateGlassID;
            DetectGantry.OnUpdateCellStatus += DetectGantry_OnUpdateCellStatus;
            ProbeCardHelper.OnUpdatePrbInfo += ProbeCardHelper_OnUpdatePrbInfo;
            UserCtrlManager.OnUpdateRecipe += RecipeManager_OnRecipeReport;
            CimManager.Instance.OnGetPanelStatus += CimHelper_OnGetCellStatus;
        }
        private PanelStatus[,] CimHelper_OnGetCellStatus(int glassNum)
        {
            return GlassMaps[glassNum].GetPanelStatuses();
        }
        private void DetectGantry_OnUpdateCellStatus(CurrentDetectInfo arg1, int cellNum, string result, DetectType detectType)
        {
            try
            {
                var oneCellSts = GlassMaps[arg1.GlassNum].GetOnePanelStatus(arg1.ColumnNum, cellNum);
                PanelStatusEnum panelStatusEnum = PanelStatusEnum.Wait;
                if (result == "NG")
                {
                    switch (detectType)
                    {
                        case DetectType.Aoi:
                            {
                                panelStatusEnum = PanelStatusEnum.AoiNG;
                                break;
                            }
                        case DetectType.Mura:
                            {
                                panelStatusEnum = PanelStatusEnum.MuraNG;
                                break;
                            }
                        case DetectType.SquintMura:
                            {
                                panelStatusEnum = PanelStatusEnum.SquintMuraNG;
                                break;
                            }
                        case DetectType.MultiAngle:
                            {
                                panelStatusEnum = PanelStatusEnum.MultiAngleNG;
                                break;
                            }
                        default:
                            break;
                    }
                    if (oneCellSts.NGList.Contains(panelStatusEnum) == false)
                    {
                        oneCellSts.NGList.Add(panelStatusEnum);
                    }
                    oneCellSts.Status = oneCellSts.NGList.Count > 0 ? PanelStatusEnum.NG : PanelStatusEnum.OK;
                    oneCellSts.Text = string.Empty;
                }
                else if (result == "Detecting")
                {
                    oneCellSts.Status =PanelStatusEnum.Detecting;
                    oneCellSts.Text = "Testing";
                }
              
                GlassMaps[arg1.GlassNum].RefreshPanelSts();
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("FrmAuto").Fatal(ex.ToString());
            }
        }

        private void RecipeManager_OnRecipeReport(string arg1)
        {
            InitControls();
            Task.Factory.StartNew(() => {
                LoadGlassInfo();
            });
        }

        private void ProbeCardHelper_OnUpdatePrbInfo(ProbeCardEventArgs obj)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<ProbeCardEventArgs>(ProbeCardHelper_OnUpdatePrbInfo), obj);
            }
            else
            {
                var probeCard = ProbeCardHelper.GetProbeCard(RecipeManager.CurRecipe.RecipeID);
                if (obj.MsgType == "UpdateCount")
                {
                    if (probeCard != null)
                    {
                        this.lbl_contactCount.Text = probeCard.ContactedCount.ToString();
                    }
                }
            }
        }
        string LastGlassID = string.Empty;
        private void Instance_OnLinkSignalChanged(LinkSignalEventArgs e)
        {
            if (e.Glass_Datas != null && e.Glass_Datas.Length > 0)
            {
                if (e.Glass_Datas[0].Glass_ID != LastGlassID)
                {
                    this.Invoke(new Action(() => {
                        try
                        {
                            //Task.Factory.StartNew(() => { GetUpstreamPanelSts(e.Glass_Datas[0], 0); GetUpstreamPanelSts(e.Glass_Datas[1], 1); });
                            lbl_GlassID1.Text = e.Glass_Datas[0].Glass_ID;
                            lbl_GlassID2.Text = e.Glass_Datas[1].Glass_ID;
                            lbl_PPID1.Text = e.Glass_Datas[0].PPID[1].ToString();
                            lbl_PPID2.Text = e.Glass_Datas[1].PPID[1].ToString();
                            lbl_LotID1.Text = e.Glass_Datas[0].Lot_ID;
                            lbl_LotID2.Text = e.Glass_Datas[1].Lot_ID;
                            lbl_LotSequence1.Text = e.Glass_Datas[0].LOT_Sequence_No.ToString();
                            lbl_LotSequence2.Text = e.Glass_Datas[1].LOT_Sequence_No.ToString();
                            lbl_SlotLotSequence1.Text = e.Glass_Datas[0].Slot_Sequence_No.ToString();
                            lbl_SlotLotSequence2.Text = e.Glass_Datas[1].Slot_Sequence_No.ToString();
                            lbl_OperID1.Text = e.Glass_Datas[0].OPER_ID;
                            lbl_OperID2.Text = e.Glass_Datas[1].OPER_ID;
                            lbl_ProdID1.Text = e.Glass_Datas[0].Prod_ID;
                            lbl_ProdID2.Text = e.Glass_Datas[1].Prod_ID;
                            lbl_RotateDir1.Text = e.Glass_Datas[0].Glass_Angle.ToString();
                            lbl_RotateDir2.Text = e.Glass_Datas[1].Glass_Angle.ToString();
                            lbl_ContactHeight1.Text = e.Glass_Datas[0].Glass_Thickness_Code.ToString();
                            lbl_ContactHeight2.Text = e.Glass_Datas[1].Glass_Thickness_Code.ToString();
                            lblCstID1.Text = e.Glass_Datas[0].Option_Value;
                            lblCstID2.Text = e.Glass_Datas[1].Option_Value;

                            DFSHelper.UpdateStepID(e.Glass_Datas[0].Prod_ID);
                            AoiPC.Instance.UpdateStepID(e.Glass_Datas[0].Prod_ID, 0);
                            AoiPC.Instance.UpdateStepID(e.Glass_Datas[0].Prod_ID, 1);
                        }
                        catch (Exception ex)
                        {
                            LogManager.GetLogger("FrmAuto").Fatal(ex.ToString());
                        }
                    }));
                    LastGlassID = e.Glass_Datas[0].Glass_ID;
                
                }
            }
        }
        private void GetUpstreamPanelSts(GlassDataA jobDatas,int glassNum)
        {
            try
            {
                var block1 = CimManager.Instance.PanelDataInformationDownloadRequest(jobDatas);
                if (block1 != null)
                {
                    if (block1.ReturnCode == 1)
                    {
                        StringBuilder sb = new StringBuilder();                      
                        int rowNum = 0, colNum = 0,totalCount=0;
                        for (int i = 0; i < 37; i++)
                        {
                             //int类型转成二进制字符转                                                                                             //not used    Panel3      Panel2      Panel1
                            var binaryStr = Entities.ConvertHelper.DecimalToBinary(block1.PanelJudge[i],15,true);//   0           0 1 0 1 0   0 0 0 0 1   0 0 0 0 0
                            string[] panels = new string[3];
                            for (int j = 2; j >= 0; j--)
                            {
                                panels[2 - j] = binaryStr.Substring(j * 5, 5);
                                PanelStatusEnum panelSts = PanelStatusEnum.Wait;
                                if (string.IsNullOrEmpty(panels[2-j])==false)
                                {
                                    int panelJobJudge = Convert.ToInt32(panels[2 - j], 2);
                                    if (panelJobJudge == 14)
                                    {
                                        panelSts = PanelStatusEnum.UpstreamNG;
                                    }
                                }
                                else
                                {
                                    LogManager.GetLogger("FrmAuto").Info($"panels[2 - j]为空,PanelID:{jobDatas.Glass_ID}");
                                }
                                var oneCellSts = GlassMaps[glassNum].GetOnePanelStatus(colNum, rowNum);
                                if (panelSts.ToString().Contains("NG")&& !oneCellSts.NGList.Contains(panelSts))
                                {
                                    oneCellSts.NGList.Add(panelSts);
                                }
                                rowNum++;
                                totalCount++;
                                if (rowNum == Param.Panel.CellCountY)
                                {
                                    colNum++; rowNum = 0;
                                    if (colNum == Param.Panel.CellCountX )
                                    {
                                        break;
                                    }
                                }
                            }
                            if (Param.Panel.CellCountX * Param.Panel.CellCountY== totalCount)
                            {
                                break;
                            }
                        }
                       var str= GetPanelSts(GlassMaps[glassNum].GetPanelStatuses());
                        LogManager.GetLogger("PanelJudgeData").Info($"从CIM接收的PanelJudge结果:\n{str},\nPanelID:{jobDatas.Glass_ID}");
                        GlassMaps[glassNum].RefreshPanelSts();
                        if (Param.Control.IsShieldSendUpstreamInfo == false)
                        {
                            TesterPC.Instance.UpstreamGlassInfo(jobDatas.Glass_ID,glassNum, GlassMaps[glassNum].GetPanelStatuses());
                        }
                    }
                    else
                    {
                        ShowMsg("FrmAuto",$"PanelJudgeDataDownloadRequest返回结果中ReturnCode NG", Brushes.Red);
                    }
                 
                }
                else
                {
                    ShowMsg("FrmAuto", "PanelJudgeDataDownloadRequest返回结果为空",Brushes.Red);
                }
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("FrmAuto").Fatal(ex.ToString());
            }

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
                            string cellID = Global.GetCellID(i,j);
                            if (panelStatus[i, j].Status == PanelStatusEnum.UpstreamNG)
                            {
                                sb.Append($"{cellID}NG-");
                            }
                            else
                            {
                                sb.Append($"{cellID}OK-");
                            }
                        }
                        string temp = sb.ToString();
                        result += temp.TrimEnd('-') + "\n";
                    }
                    result = result.TrimEnd('\n');
                }
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("FrmAuto").Info($"GetPanelSts执行异常，异常信息：{ex}");
            }
            return result;
        }
      
        private void ProbeGantry_OnTriggerSignal(int obj)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<int>(ProbeGantry_OnTriggerSignal), obj);
            }
            else
            {
                if (obj == (int)StationCommand.OneGlassDetectCom)
                {
                    if (Global.CurDetectInfo.GlassNum == 0)
                    {
                        lbl_EndTime1.Text = DateTime.Now.ToLongTimeString();
                    }
                    else
                    {
                        lbl_EndTime2.Text = DateTime.Now.ToLongTimeString();
                    }
                }
            }
         
        }

        private void DetectGantry_OnUpdateGlassID(int arg1, string arg2)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<int,string>(DetectGantry_OnUpdateGlassID),arg1,arg2);
            }
            else
            {
                if (arg1 == 0)
                {
                    lbl_StartTime1.Text = DateTime.Now.ToLongTimeString(); ;
                    lbl_ScanerID1.Text = arg2;
                }
                else
                {
                    lbl_StartTime2.Text = DateTime.Now.ToLongTimeString();
                    lbl_ScanerID2.Text = arg2;
                }
            }
        }

        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControls()
        {
            try
            {
                panelGlass2.Controls.Clear();
                panelGlass1.Controls.Clear();
                GlassMaps = new GlassMapCtrl[2];
                GlassMapCtrl glass1 = new GlassMapCtrl();
                glass1.Parent = panelGlass1;
                glass1.Dock = DockStyle.Fill;
                GlassMapCtrl glass2 = new GlassMapCtrl();
                glass2.Parent = panelGlass2;
                glass2.Dock = DockStyle.Fill;
                GlassMaps[0] = glass1;
                GlassMaps[1] = glass2;          
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("FrmAuto").Fatal(ex.ToString()); 
            }
        }
        private void LoadGlassInfo()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(LoadGlassInfo));
            }
            else
            {
                var probeCard = ProbeCardHelper.GetProbeCard(RecipeManager.CurRecipe.RecipeID);
                if (probeCard!=null)
                {
                    lbl_probeCardName.Text = probeCard.ProbeCardName;
                    lbl_equipDate.Text = probeCard.LoadInUvwTime?.ToString("yyyy-MM-dd HH:mm:ss");
                    lbl_contactCount.Text = probeCard.ContactedCount.ToString();
                }

            }
        }
       
        /// <summary>
        /// 刷新产品状态
        /// </summary>
        public void ReFreshPanelStatus(bool init = false)
        {
            try
            {
                if (InvokeRequired)
                {
                    this.Invoke(new Action<bool>(ReFreshPanelStatus), init);
                }
                else
                {
                    if (init)
                    {
                        for (int m = 0; m < 2; m++)
                        {
                            for (int i = 0; i < Param.Panel.CellCountX; i++)
                            {
                                for (int j = 0; j < Param.Panel.CellCountY; j++)
                                {
                                    var oneCell = GlassMaps[m].GetOnePanelStatus(i, j);
                                    oneCell.NGList = new List<PanelStatusEnum>();
                                    oneCell.Status = PanelStatusEnum.Wait;
                                    oneCell.Text = string.Empty;
                                }
                            }
                            GlassMaps[m].RefreshPanelSts();
                        }
                       
                        lbl_StartTime1.Text = "";
                        lbl_ScanerID1.Text = "";
                        lbl_EndTime1.Text = "";
                        lbl_StartTime2.Text = "";
                        lbl_ScanerID2.Text = "";
                        lbl_EndTime2.Text = "";
                    }
                    else
                    {
                        OneColumnModel oneColumn = DetectionHelper.GetOneColumnModel(Global.CurDetectInfo.GlassNum,Global.CurDetectInfo.ColumnNum);
                        if (oneColumn != null)
                        {
                            if (oneColumn.DetectSts==1)
                            {
                                for (int i = 0; i < Param.Panel.CellCountY; i++)
                                {
                                    var oneCell = GlassMaps[Global.CurDetectInfo.GlassNum].GetOnePanelStatus(Global.CurDetectInfo.ColumnNum, i);
                                    oneCell.Status = PanelStatusEnum.Detecting;
                                }
                            }
                            else
                            {
                                for (int i = 0; i < Param.Panel.CellCountY; i++)
                                {
                                    OpenShortDetectModel onePanel = DetectionHelper.GetOnePanelModel(Global.CurDetectInfo.GlassNum,Global.CurDetectInfo.ColumnNum, i);
                                    if (onePanel != null)
                                    {
                                        var oneCell=GlassMaps[Global.CurDetectInfo.GlassNum].GetOnePanelStatus(Global.CurDetectInfo.ColumnNum, i);
                                        if (onePanel.Result == "NG")
                                        {
                                            var ngType = onePanel.NGType == "Open" ? PanelStatusEnum.OpenNG : PanelStatusEnum.ShortNG;
                                            if (oneCell.NGList.Contains(ngType) ==false)
                                            {
                                                oneCell.NGList.Add(ngType);
                                            }
                                        }
                                        oneCell.Status = oneCell.NGList.Count > 0 ? PanelStatusEnum.NG : PanelStatusEnum.OK;
                                    }
                                }
                            }
                          
                        }
                        GlassMaps[Global.CurDetectInfo.GlassNum].RefreshPanelSts();
                    }                
                }
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("FrmAuto").Fatal(ex.ToString());
            }

        }

        private void label3_Click(object sender, EventArgs e)
        {
           
            //for (int i = 0; i < Param.Panel.CellCountX; i++)
            //{
            //    for (int j = 0; j < Param.Panel.CellCountY; j++)
            //    {
            //        var temp = new CurrentDetectInfo { GlassNum = 0, ColumnNum = i };
            //        string result = "NG";
            //        for (int m = 0; m < 4; m++)
            //        {
            //            DetectType detectType = (DetectType)(m% 4);
            //            if (m==3)
            //            {
            //                result = "OK";
            //            }
            //            DetectGantry_OnUpdateCellStatus(temp, j, result, detectType);
            //        }                 
            //    }
            //}
           
        }
    }
}
