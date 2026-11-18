using HYC.SaturnDB.DAL;
using HYC.SaturnDB.Model;
using HYC.HTPCBase.Alarm;
using HYC.HTPCBase.Entities;
using HYC.HTPCBase.Extension.Entities;
using HYC.HTPCBase.Extension.Recipe;
using HYC.HTPCBase.Extension.Views;
using HYC.HTPCBase.Motion;
using HYC.HTPCBase.Station;
using HYC.SaturnMain.CabinetInfos;
using HYC.SaturnMain.Devices.Rfid;
using HYC.SaturnMain.UserControls;
using HYC.WindowsControls;

using HYC.SaturnMain;
using HYC.SaturnMain.Params;
using HYC.SaturnMain.Devices.Motion;
using HYC.SaturnMain.Station;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using HYC.SaturnMain.Entities;

namespace HYC.SaturnMain.SubForms
{
    public partial class FrmCabinet : ViewBase
    {
        /// <summary>
        /// 选中的料仓层数，范围：0到7
        /// </summary>
        int SelectedRowNum = -1;
        /// <summary>
        /// 料仓和Uvw之间更换Probee流程状态，0：运行，1：暂停，-1：停止
        /// </summary>
        int ManualChgPrbMode = -1;
        /// <summary>
        /// 料仓和缓存平台之间更换Probe流程状态，0：运行，1：暂停，-1：停止
        /// </summary>
        int CabinetChgPrbMode = -1;
        /// <summary>
        /// 料仓和Uvw之间正在更换标志位，true:正在更换中，false:更换停止
        /// </summary>
        bool IsInChging = false;
        /// <summary>
        /// 料仓和缓存平台之间正在更换标志位，true:正在更换中，false:更换停止
        /// </summary>
        bool IsInLoading = false;
        /// <summary>
        /// false：料仓和Uvw之间更换Probe，true：料仓和缓存平台之间更换Probe
        /// </summary>
        bool ChangeMode = false;
        bool LoadFlg = false;
        CabinetCellCtrl[] CabinetCtrls = null;
        ProbeGantry ProbeStation = null;
        DualStage StageStation = null;
        PrbAutoChg AutoChgStation = null;
        Cabinet CabinetStation = null;
        MotionManager Motion = MotionManager.GetInstance();
        public FrmCabinet()
        {
            InitializeComponent();
            this.ButtonName = "料仓管理";
            this.IsNeedLogin = true;
            this.Level = UserLevel.Engineer;
            this.ButtonNameEn = "Cabinet\nManage";
            this.Index = 4;
            this.ButtonImage = Properties.Resources.Cabinet;
            this.IsCanSwitchInRunning = true;
            this.Level = UserLevel.Engineer;
            InitCtrl();
            InitManager.OnInitCom += InitManager_OnInitCom;
        }

        private void InitManager_OnInitCom()
        {
            LoadConfig();
        }

        private void InitCtrl()
        {
            CabinetCtrls = new CabinetCellCtrl[8];
            for (int i = 0; i < 8; i++)
            {
                string temp = $"cabinetCellCtrl{i + 1}";
                CabinetCtrls[i] = Controls.Find(temp,true)[0] as CabinetCellCtrl;
            }
          
        }
        private void LoadConfig()
        {
            for (int i = 0; i < 8; i++)
            {
                var info=CabinetInfoHelper.GetCabinetInfo(i);
                if (info!=null)
                {
                    CabinetCtrls[i].LoadCabinetRowInfo(info);
                }
            }
        }
        CabinetRowInfo CabinetRowInfo = null;
        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (IsInChging)
            {
                MessageBox.Show("料仓和Uvw之间正在更换Probe，请等待");
                return;
            }
            if (IsCanRun() == false)
            {
                return;
            }
            CabinetRowInfo = null;
            if (SelectedRowNum==-1)
            {
                MessageBox.Show("请先选中要更换哪一层料仓");
                return;
            }
            if (IsInLoading)
            {
                MessageBox.Show("ProbeCard搬运模组正在运行中，请等待");
                return;
            }
            string leftDetPrbSensor = $"CabDetectPrbLeft{SelectedRowNum + 1}F";
            string rightDetPrbSensor = $"CabDetectPrbRight{SelectedRowNum + 1}F";
            if (Motion.GetDigitalInput(leftDetPrbSensor)|| Motion.GetDigitalInput(rightDetPrbSensor))
            {
                MessageBox.Show("所选料仓检测到Probe，请查看料仓是否有Probe");
                return;
            }
            if (!Motion.GetDigitalInput(InputName.PrbBufferDetectPrb1) && !Motion.GetDigitalInput(InputName.PrbBufferDetectPrb2))
            {
                MessageBox.Show("缓存平台没有检测到Probe，请查看缓存平台是否有Probe");
                return;
            }
            try
            {
                CabinetChgPrbMode = -1;
                ChangeMode = true;//料仓和缓存平台之间更换Probe流程状态
                string rfid = RfidHelper.Instance.Read();
                var info= CabinetInfoHelper.GetCabinetInfoByRFID(rfid);
                if (info!=null)
                {
                    CabinetRowInfo = CabinetInfoHelper.GetCabinetInfo(SelectedRowNum);
                    CabinetRowInfo.RFID = rfid;
                    CabinetRowInfo.ProbeName = info.ProbeName;
                    CabinetRowInfo.RecipeName = info.RecipeName;
                    CabinetRowInfo.IsUsed = true;
                    CabinetRowInfo.CabinetRowNum = SelectedRowNum;
                    if (MessageBox.Show($"将要把ProbeCard从缓存平台上放到第{SelectedRowNum + 1}层料仓中，请确认是否执行？", "Question", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        CabinetLoadPrb();
                    }
                }
                else
                {
                    FrmProbeCardEdit frm = new FrmProbeCardEdit(SelectedRowNum);
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        CabinetRowInfo = frm.Info;
                        if (MessageBox.Show($"将要把ProbeCard从缓存平台上放到第{SelectedRowNum + 1}层料仓中，请确认是否执行？", "Question", MessageBoxButtons.OKCancel) == DialogResult.OK)
                        {
                            CabinetLoadPrb(true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
         
        }
        private void btnUnload_Click(object sender, EventArgs e)
        {
            if (IsInChging)
            {
                MessageBox.Show("料仓和Uvw之间正在更换Probe，请等待");
                return;
            }
            if (IsCanRun() == false)
            {
                return;
            }
            if (SelectedRowNum == -1)
            {
                MessageBox.Show("请先选中要更换哪一层料仓");
                return;
            }
            if (IsInLoading)
            {
                MessageBox.Show("ProbeCard搬运模组正在运行中，请等待");
                return;
            }

            try
            {
                CabinetRowInfo = CabinetInfoHelper.GetCabinetInfo(SelectedRowNum);
                if (string.IsNullOrEmpty(CabinetRowInfo.ProbeName) && CabinetRowInfo.IsUsed == false)
                {
                    MessageBox.Show("选中料仓中没有配置ProbeCard信息，请查看配置");
                    return;
                }
                string leftDetPrbSensor = $"CabDetectPrbLeft{SelectedRowNum + 1}F";
                string rightDetPrbSensor = $"CabDetectPrbRight{SelectedRowNum + 1}F";
                if (!Motion.GetDigitalInput(leftDetPrbSensor) && !Motion.GetDigitalInput(rightDetPrbSensor))
                {
                    MessageBox.Show("所选料仓没有检测到Probe，请查看料仓是否有Probe");
                    return;
                }
                if (Motion.GetDigitalInput(InputName.PrbBufferDetectPrb1) || Motion.GetDigitalInput(InputName.PrbBufferDetectPrb2))
                {
                    MessageBox.Show("缓存平台上检测到Probe，请查看缓存平台是否有Probe");
                    return;
                }
                if (MessageBox.Show($"将要把ProbeCard从第{ SelectedRowNum + 1}层料仓放到缓存平台上，请确认是否执行？", "Question", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    ChangeMode = true;//料仓和缓存平台之间更换Probe流程状态
                    CabinetChgPrbMode = -1;
                    CabinetUnloadPrb();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void CabinetLoadPrb(bool isNew=false)
        {
            IsInLoading = true;
            LoadFlg = true;
            CabinetChgPrbRun(true);
            ShowMsg("FrmCabinet", $"把ProbeCard从缓存平台上放到第{SelectedRowNum + 1}层料仓中", Brushes.Blue);
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    if (CabinetChgPrbMode == 0)
                    {
                        CabinetStation.BufferToCabinetFlow(SelectedRowNum);
                        if (CabinetStation.LoadPrbStep == 0)
                        {
                            ShowMsg("FrmCabinet", $"缓存平台->缓存平台到料仓更换Probe完成", Brushes.Blue);
                            CabinetChgPrbMode = -1;
                            IsInLoading = false;
                            LoadFlg = false;
                            BufferToCabinetFlowCom(isNew);
                            break;
                        }
                    }
                    else if (CabinetChgPrbMode == -1)
                    {
                        IsInLoading = false;
                        if (CabinetStation.LoadPrbStep != 0)
                        {
                            ShowMsg("FrmCabinet", $"缓存平台->缓存平台到料仓更换更换Probe失败", Brushes.Red);
                        }
                        break;
                    }
                    Thread.Sleep(50);
                }
            });
        }
        private void CabinetUnloadPrb()
        {
            IsInLoading = true;
            LoadFlg = false;
            CabinetChgPrbRun(false);
            ShowMsg("FrmCabinet", $"把ProbeCard从第{ SelectedRowNum + 1}层料仓放到缓存平台上", Brushes.Blue);
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    if (CabinetChgPrbMode == 0)
                    {
                        CabinetStation.CabinetToBufferFlow(SelectedRowNum);
                        if (CabinetStation.UnloadPrbStep == 0)
                        {
                            ShowMsg("FrmCabinet", $"料仓->料仓到缓存平台更换Probe完成", Brushes.Blue);
                            CabinetChgPrbMode = -1;
                            IsInLoading = false;
                            CabinetToBufferFlowCom();
                            break;
                        }
                    }
                    else if (CabinetChgPrbMode == -1)
                    {
                        IsInLoading = false;
                        if (CabinetStation.UnloadPrbStep != 0)
                        {
                            ShowMsg("FrmCabinet", $"料仓->料仓到缓存平台更换Probe失败", Brushes.Red);
                        }
                        break;
                    }
                    Thread.Sleep(50);
                }
            });
        }
       
        private void FrmCabinet_Load(object sender, EventArgs e)
        {
       
             ProbeStation = StationManager.Instance.GetChild(StationEnum.ProbeGantry) as ProbeGantry;
             StageStation = StationManager.Instance.GetChild(StationEnum.DualStage) as DualStage;
             AutoChgStation = StationManager.Instance.GetChild(StationEnum.PrbAutoChg) as PrbAutoChg;
             CabinetStation = StationManager.Instance.GetChild(StationEnum.Cabinet) as Cabinet;
            AlarmManager.OnAlarm += AlarmManager_OnAlarm;
            StationManager.Instance.OnUpdateEQPSts += Instance_OnUpdateEQPSts;
           // Cabinet.OnBufferToCabinetFlowCom += Cabinet_OnBufferToCabinetFlowCom;
           // Cabinet.OnCabinetToBufferFlowCom += Cabinet_OnCabinetToBufferFlowCom;
        }

        private void CabinetToBufferFlowCom()
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action(CabinetToBufferFlowCom));
            }
            else
            {
                try
                {
                    new ProbeCardDal().UpdateStatusByProbeName(ProbeCardSts.OutEqp, CabinetRowInfo.ProbeName);
                    CabinetRowInfo.RecipeName = string.Empty;
                    CabinetRowInfo.ProbeName = string.Empty;
                    CabinetRowInfo.RFID = string.Empty;
                    CabinetRowInfo.IsUsed = false;
                    CabinetInfoHelper.SaveCabinetInfo();
                    CabinetCtrls[SelectedRowNum].UpdateInfo(null, true);
                }
                catch (Exception ex)
                {
                    ShowMsg("FrmCabinet", $"料仓->Cabinet_OnCabinetToBufferFlowCom执行失败，异常信息：{ex}", Brushes.Red);
                }
            }
     
        }

        private void BufferToCabinetFlowCom(bool isNew)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<bool>(BufferToCabinetFlowCom),isNew);
            }
            else
            {
                try
                {      
                    CabinetRowInfo.IsUsed = true;
                    CabinetInfoHelper.SaveCabinetInfo();
                    CabinetCtrls[SelectedRowNum].UpdateInfo(CabinetRowInfo);
                }
                catch (Exception ex)
                {
                    ShowMsg("FrmCabinet", $"料仓->Cabinet_OnBufferToCabinetFlowCom执行失败，异常信息：{ex}", Brushes.Red);
                }
            }
    
        }

        private void Instance_OnUpdateEQPSts(HTPCBase.Views.EqpSts obj)
        {
            if (obj == HTPCBase.Views.EqpSts.PAUSE)
            {
                if (ChangeMode)
                {
                    CabinetChgPrbMode = 1;
                }
                else
                {
                    ManualChgPrbMode = 1;
                }
            }
            else if (obj == HTPCBase.Views.EqpSts.STOP)
            {
                IsInChging = false;
                IsInLoading = false;
                ManualChgPrbMode = -1;
                CabinetChgPrbMode = -1;
            }
        }

        private void AlarmManager_OnAlarm(int arg1, HTAlarm.DB.Model.AlarmDetail arg2)
        {
            if (arg2.AlarmLevel==HTAlarm.DB.Model.AlarmLevel.LightAlarm)
            {
                if (ChangeMode)
                {
                    CabinetChgPrbMode = 1;
                }
                else
                {
                    ManualChgPrbMode = 1;
                }
            }
            else if (arg2.AlarmLevel == HTAlarm.DB.Model.AlarmLevel.HeavyAlarm)
            {
                IsInChging = false;
                IsInLoading = false;
                ManualChgPrbMode = -1;
                CabinetChgPrbMode = -1;
            }
        }

        private void rbtRow1_ValueChanged(object sender, bool value)
        {
            var rbt = sender as HTRadioButton;
            SelectedRowNum = int.Parse(rbt.Tag.ToString());
        }
        private bool IsCanRun()
        {
            bool ret = true;
            if (StationManager.Instance.IsInHoming())
            {
                MessageBox.Show("设备正在回零中，无法取放ProbeCard");
                ret = false;
            }
           else if (StationManager.Instance.IsInRunning())
            {
                MessageBox.Show("设备正在运行中，无法取放ProbeCard");
                ret = false;
            }
            else if (MotionGlobal.IsHomeOK == false)
            {
                MessageBox.Show("设备原点丢失，请先回零");
                ret = false;
            }
            return ret;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            CabinetInfoHelper.ReadCabinetInfo();
            for (int i = 0; i < 8; i++)
            {
                var info = CabinetInfoHelper.GetCabinetInfo(i);
                if (info != null)
                {
                    CabinetCtrls[i].UpdateInfo(info);
                }
            }
        }

        private void btnCabinetToUvw_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($"将要把ProbeCard从第{ SelectedRowNum + 1}层料仓安装到Uvw上，请确认是否执行？", "Question", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                ShowMsg("FrmCabinet", $"把ProbeCard从第{ SelectedRowNum + 1}层料仓安装到Uvw上", Brushes.Blue);
                PrbAutoChg(PrbChgMode.Load);
            }
        }
      
        private void btnUvwToCabinet_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($"将要把ProbeCard从Uvw上卸载到料仓中，请确认是否执行？", "Question", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                ShowMsg("FrmCabinet", $"把ProbeCard从Uvw上卸载到料仓中", Brushes.Blue);
                PrbAutoChg(PrbChgMode.Unload);
            }
        }

        private void btnUvwToCabinetToUvw_Click(object sender, EventArgs e)
        {
          
            if (MessageBox.Show($"将要把ProbeCard从Uvw上卸载到料仓中，再从料仓中把ProbeCard安装到Uvw上，请确认是否执行？", "Question", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                ShowMsg("FrmCabinet", $"把ProbeCard从Uvw上卸载到料仓中，再从料仓中把ProbeCard安装到Uvw上", Brushes.Blue);
                PrbAutoChg(PrbChgMode.UnloadLoad);
            }
        }

        private  void PrbAutoChg(PrbChgMode mode)
        {
            try
            {
                if (IsInLoading)
                {
                    MessageBox.Show("料仓和缓存平台之间正在更换Probe，请等待");
                    return;
                }
                if (IsInChging)
                {
                    MessageBox.Show("正在进行自动更换，禁止重复启动");
                    return;
                }
                if (mode == PrbChgMode.Load || mode == PrbChgMode.UnloadLoad)
                {
                    if (SelectedRowNum == -1)
                    {
                        MessageBox.Show("请先选中要更换哪一层料仓");
                        return;
                    }
                    CabinetRowInfo = CabinetInfoHelper.GetCabinetInfo(SelectedRowNum);
                    if (string.IsNullOrEmpty(CabinetRowInfo.ProbeName) || CabinetRowInfo.IsUsed == false)
                    {
                        MessageBox.Show("所选料仓为空，没有配置信息");
                        return;
                    }
                    Global.ReceivedRecipeName = CabinetRowInfo.RecipeName;
                }
                if (IsCanRun() == false)
                {
                    return;
                }
                Param.Control.PrbChgMode = mode;
                ChangeMode = false;//料仓和Uvw平台之间更换Probe流程状态
                PrbAutoChgFlow();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"PrbAutoChg方法执行异常，异常信息：{ex.ToString()}");
            }
        }

        private void PrbAutoChgFlow()
        {
            IsInChging = true;
            AutoChangeRun();
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    if (ManualChgPrbMode == 0)
                    {
                        ProbeStation.PrbAutoChgFlow();
                        StageStation.PrbAutoChgFlow();
                        AutoChgStation.PrbAutoChgFlow();
                        CabinetStation.PrbAutoChgFlow();
                        if (ProbeStation.PrbAutoChgStep == 0 && StageStation.PrbAutoChgStep == 0 && AutoChgStation.PrbAutoChgStep == 0 && CabinetStation.PrbAutoChgStep == 0)
                        {
                            ShowMsg("FrmCabinet", $"ProbeCard更换完成", Brushes.Blue);
                            ManualChgPrbMode = -1;
                            IsInChging = false;
                            break;
                        }
                    }
                    else if (ManualChgPrbMode==-1)
                    {
                        IsInChging = false;
                        break;
                    }
                    Thread.Sleep(50);
                }
            });
        }
        private void btnPause_Click(object sender, EventArgs e)
        {
            if (ChangeMode)
            {
                 CabinetChgPrbMode = 1;
            }
            else
            {
                ManualChgPrbMode = 1;
            }
        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            if (ChangeMode)
            {
                CabinetChgPrbRun(LoadFlg);
            }
            else
            {
                AutoChangeRun();
            }
           
        }
        private void AutoChangeRun()
        {
            if (ManualChgPrbMode == -1)
            {
                ProbeStation.PrbAutoChgStep = 1;
                StageStation.PrbAutoChgStep = 1;
                AutoChgStation.PrbAutoChgStep = 1;
                CabinetStation.PrbAutoChgStep = 1;
            }
            ManualChgPrbMode = 0;
        }
        private void CabinetChgPrbRun(bool isLoad)
        {
            if (CabinetChgPrbMode == -1)
            {
                if (isLoad)
                {
                    CabinetStation.LoadPrbStep = 1;
                }
                else
                {
                    CabinetStation.UnloadPrbStep = 1;
                }
            }
            CabinetChgPrbMode = 0;
        }
        private void btnStop_Click(object sender, EventArgs e)
        {
            IsInChging = false;
            ManualChgPrbMode = -1;
            IsInLoading = false;
            CabinetChgPrbMode = -1;
            StationManager.Instance.Stop();
        }

        private void btnBufferStageOut_Click(object sender, EventArgs e)
        {
            if (IsCanRun()&& MessageBox.Show("是否将缓存平台移出？", "询问", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                var y1Pos = Motion.GetAxisActPos(StationEnum.Cabinet, AxisName.PrbLoader_Y1);
                var y2Pos = Motion.GetAxisActPos(StationEnum.Cabinet, AxisName.PrbLoader_Y2);
                if (y1Pos>3||y2Pos > 3)//如果Y1和Y2轴不在安全位置，禁止移动上料平台Z轴
                {
                    MessageBox.Show("上料平台Y轴不在安全位置，禁止移动缓存平台");
                    return;
                }
                Task.Factory.StartNew(() =>
                {
                    var posInfo = AxisInfoHelper.GetPosInfoByName(AxisName.PrbLoader_Z, CabinetPosName.SafePos);
                    var destPos = posInfo.Pos - Param.Motion.LoaderZPreventCrushPos;
                    var t = Motion.MoveAbsSync(StationEnum.Cabinet, AxisName.PrbLoader_Z, destPos, 60);
                    if (t.Result)
                    {
                        t = Motion.MoveAbsSync(StationEnum.Cabinet, AxisName.PrbBuffer_Y, CabinetPosName.PrbBuffStgUnldPos, 60);
                        if (t.Result)
                        {
                            MessageBox.Show("缓存平台到达放置Probe位置");
                        }
                        else
                        {
                            MessageBox.Show("缓存平台移动到放置Probe位置超时");
                        }
                    }
                    else
                    {
                        MessageBox.Show("上料平台Z轴移动到安全位置超时");
                    }
                });

            }
        }
    }
}
