
using HYC.SaturnDB.DAL;
using HYC.HTAlarm.DB.Model;
using HYC.HTFile.HTINI;
using HYC.HTPCBase.Alarm;
using HYC.HTPCBase.Entities;
using HYC.HTPCBase.Extension.Entities;
using HYC.HTPCBase.Extension.OpRecord;
using HYC.HTPCBase.Extension.Recipe;
using HYC.HTPCBase.Extension.UserCtrl;
using HYC.HTPCBase.Extension.Views;
using HYC.HTPCBase.Motion;
using HYC.HTPCBase.Station;
using HYC.HTPCBase.Views;
using HYC.SaturnMain.CabinetInfos;
using HYC.SaturnMain.Entities;
using HYC.SaturnMain.SubForms;
using PPI_Vision.SubForms;
using HYC.SaturnMain;

using HYC.SaturnMain.Params;
using HYC.SaturnMain.Devices.Cim;
using HYC.SaturnMain.Devices.ClientPC;
using HYC.SaturnMain.Devices.Motion;

using HYC.SaturnMain.Station;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using HYC.HTPCBase.Param;
using HYC.SaturnDB;
using BOECIM.Devices.CIM;
using CIMBOE;
using System.Globalization;

using System.Xml.Serialization;
using HYC.SaturnMain.Devices.MasterPC;
using HYC.HTLog;

namespace HYC.SaturnMain
{
    public partial class FrmMain : MotionViewMain
    {
        public FrmMain()
        {
            InitializeComponent();
            OnRegisterEvent += FrmMain_OnRegisterEvent;
        }
        public FrmContactCamera FrmContactCamera;
        HTINIHelper Ini = new HTINIHelper(AppDomain.CurrentDomain.BaseDirectory + "Config.INI");
        FrmPM FormPM = new FrmPM();
        MotionManager Motion = null;
        FrmPrbAutoChangeTip FrmPrbAutoChangeTip = null;
        FrmAutoPos FrmAutoPos = null;
        bool IsInitCom = false;
        bool IsLightOn = true;
        bool[] IsSave = new bool[10];
        private void FrmMain_OnRegisterEvent()
        {
            CimManager.Instance.OnLinkSignalChanged += CimManager_OnLinkSignalChanged;
            CimManager.Instance.OnReceiveCimCmd += Instance_OnReceiveCimCmd;
            AlarmManager.OnAlarm += AlarmManager_OnAlarm;
            InitManager.OnInitPara += InitManager_OnInitPara;
            LinkSignalFlow.Instance.RegisterEvent();
            InitManager.OnInitDevice += InitManager_OnInitDevice;
            ProcessCmd.OnShowMsg += ShowMsg;
            InitManager.OnInitCom += InitManager_OnInitCom;         
            StationManager.Instance.OnUpdateEQPSts += Instance_OnUpdateEQPSts;
            StationManager.Instance.OnSignalTrigger += Instance_OnSignalTrigger;
            StationManager.Instance.OnReset += Instance_OnReset;
            StationManager.Instance.OnIsCanRun += Instance_OnIsCanRun;
            ProbeGantry.OnUpdateXYTValue += UpdateXYTValue;
            DetectGantry.OnKeyInCodeRequest += DetectGantry_OnKeyInCodeRequest;
            RecipeManager.OnRecipeReport += RecipeManager_OnRecipeReport;
            DualStage.OnTriggerSignal += DualStage_OnTriggerSignal;
            Cabinet.OnTriggerSignal += DualStage_OnTriggerSignal;
            InterlockMonitor.OnShowMsg += InterlockMonitor_OnShowMsg;
            ProbeGantry.ShowContactCamera += ShowContactCamera;
            FormLogin.OnLogIn += FormLogin_OnLogIn;
            OnClosing += FrmMain_OnClosing;
          
            Global.SignalProvider.OnUpdateAlignCamSts += SignalProvider_OnUpdateAlignCamSts;
        }
        private void FormLogin_OnLogIn(HTPCBase.Extension.Entities.User obj, bool agr2)
        {
            Task.Factory.StartNew(() =>
            {
                if (agr2)
                {
                    Invoke(new Action(() => {
                        if (FormPM != null && FormPM.Visible)
                        {
                            FormPM.Close();
                        }
                    }));
                }
            });
        }
        private void FrmMain_OnClosing()
        {
            CimManager.Instance.UnInit();
            FrmAutoPos?.Unint();
        }

        private void InterlockMonitor_OnShowMsg(string arg1, Brush arg2)
        {
            ShowMsg("Interlock",arg1,arg2);
        }

        private void DualStage_OnTriggerSignal(int obj)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<int>(DualStage_OnTriggerSignal), obj);
            }
            else
            {
                try
                {
                    switch (obj)
                    {
                        case 1000:
                            {
                                if (FrmPrbAutoChangeTip == null || FrmPrbAutoChangeTip.IsDisposed)
                                {
                                    FrmPrbAutoChangeTip = new FrmPrbAutoChangeTip();
                                }
                                FrmPrbAutoChangeTip.Show();
                                break;
                            }
                        case 1001:
                            {
                                if (FrmPrbAutoChangeTip != null)
                                {
                                    FrmPrbAutoChangeTip.Close();
                                }
                                break;
                            }
                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {
                    ShowMsg("FrmMain", $"DualStage_OnTriggerSignal执行异常，异常信息：{ex}", Brushes.Red);
                }
            }
        }

        private void RecipeManager_OnRecipeReport(RecipeChangeType arg1, Recipe arg2)
        {
            try
            {
                DFSHelper.RecipeSync(arg2.RecipeID);
                switch (arg1)
                {
                    case RecipeChangeType.Shift:
                        {
                            Task.Factory.StartNew(() =>
                            {
                                CimManager.Instance.CurrentRecipeIDChangeReport(arg2);
                            });
                            AoiPC.Instance.RecipeSync(arg2.RecipeID, 0, arg1);
                            AoiPC.Instance.RecipeSync(arg2.RecipeID, 1, arg1);
                            TesterPC.Instance.RecipeSync(arg2.PPID, RecipeChangeType.Shift);
                            LoadParaToMainVIew();
                            break;
                        }
                    case RecipeChangeType.Add:
                        {
                            Task.Factory.StartNew(() =>
                            {
                                var recipeList = RecipeManager.GetRecipes();
                                CimManager.Instance.RecipeListChangeReport(recipeList);
                                AoiPC.Instance.RecipeSync(arg2.RecipeID, 0, arg1);
                                AoiPC.Instance.RecipeSync(arg2.RecipeID, 1, arg1);
                            });
                            break;
                        }
                    case RecipeChangeType.Delete:
                        {
                            Task.Factory.StartNew(() =>
                            {
                                var recipeList = RecipeManager.GetRecipes();
                                CimManager.Instance.RecipeListChangeReport(recipeList);
                                //AoiPC.Instance.RecipeSync(arg2.RecipeID, 0, arg1);
                                //AoiPC.Instance.RecipeSync(arg2.RecipeID, 1, arg1);
                            });
                            break;
                        }
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                ShowMsg("FrmMain", $"RecipeManager_OnRecipeReport执行失败，异常信息：{ex.Message}", Brushes.Red);
                LogManager.GetLogger("FrmMain").Info(ex.ToString());
            }
        }

        FrmCimMsg FrmCimMsg = null;
        private void Instance_OnReceiveCimCmd(HYC.SaturnDB.Model.CimMessageModel obj)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<HYC.SaturnDB.Model.CimMessageModel>(Instance_OnReceiveCimCmd),obj);
            }
            else
            {
                if (FrmCimMsg == null || FrmCimMsg.IsDisposed)
                {
                    FrmCimMsg = new FrmCimMsg();
                }
                FrmCimMsg.ShowCimMessage(obj);
            }
    
        }

        private bool Instance_OnIsCanRun()
        {
            bool ret = true;
            try
            {
                if (Global.EqpStatus != EqpSts.PAUSE)
                {
                    if (InterlockMonitor.Instance.IsContactZAtSafePos() == false)
                    {
                        MessageBox.Show("压头不在安全位置，禁止启动！");
                        ret = false;
                    }
                    if (Motion.GetDigitalInput(InputName.PrbAutoChgDetLeft)|| Motion.GetDigitalInput(InputName.PrbAutoChgDetRight))
                    {
                        MessageBox.Show("自动换型手臂检测到Probe，禁止启动,！");
                        ret = false;
                    }
                    if (Motion.GetDigitalInput(InputName.PrbLoaderDetectPrb1) || Motion.GetDigitalInput(InputName.PrbLoaderDetectPrb2))
                    {
                        MessageBox.Show("Probe上料手臂检测到Probe，禁止启动,！");
                        ret = false;
                    }
                }
                 if (Param.Control.InterLockShield)
                {
                    MessageBox.Show("Interlock被屏蔽，禁止自动运行！");
                    ret = false;
                }
                 if (!Motion.GetDigitalInput(InputName.RobotDetect))
                {
                    MessageBox.Show("机械手在设备中，禁止自动运行！");
                    ret = false;
                }
                if (MasterPCHelper.Instance.GetClientConnectSts("TesterPC")==false&&Param.Comm.TesterPCShield==false)
                {
                    ShowMsg("FrmMain", $"TesterPC程序没有启动，禁止自动运行", Brushes.Red);
                    ret = false;
                }
                 if (MasterPCHelper.Instance.GetClientConnectSts("DFS") == false && Param.Comm.TesterPCShield == false)
                {
                    ShowMsg("FrmMain", $"DFS程序没有启动，禁止自动运行", Brushes.Red);
                    ret = false;
                }
                 if (MasterPCHelper.Instance.GetClientConnectSts("AoiPC1") == false && Param.Comm.AoiPC1Shield == false)
                {
                    ShowMsg("FrmMain", $"AoiPC1程序没有启动，禁止自动运行", Brushes.Red);
                    ret = false;
                }
                 if (MasterPCHelper.Instance.GetClientConnectSts("AoiPC2") == false && Param.Comm.AoiPC2Shield == false)
                {
                    ShowMsg("FrmMain", $"AoiPC2程序没有启动，禁止自动运行", Brushes.Red);
                    ret = false;
                }
            }
            catch (Exception ex)
            {
                ret = false;
                MessageBox.Show($"OnIsCanRun执行异常，异常信息：{ex}"); 
            }
            return ret;
        }

        private void Instance_OnReset()
        {
            Motion.SetDigitalOutput(OutputName.EQEMO, true);
            InterlockMonitor.IsSafeDoorOpen = false;
            CimManager.Instance.MachineStatusChangeReport(EqpSts.IDLE);
        }

        private bool InitManager_OnInitDevice()
        {         
            CimManager.Instance.OnCimModeChanged += Instance_OnShowCimSts;
            CimManager.Instance.OnConnected += m => { DeviceStsManager.UpdateDevStatus("Cim", m); };
            CimManager.Instance.OnShowMsg += ShowMsg;
            CimManager.Instance.Init();
            return true;
        }

        private void CimManager_OnLinkSignalChanged(LinkSignalEventArgs e)
        {
            LinkSignalFlow.Instance.LinkSignalChanged(e);
        }
    
        private void Instance_OnSignalTrigger(IOInfo arg1, bool arg2)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<IOInfo,bool>(Instance_OnSignalTrigger), arg1, arg2);
            }
            else
            {
                if (arg1.IOName.Contains("EmgStop")&& !arg2)
                {
                    Motion.SetDigitalOutput(OutputName.EQEMO, false);
                }
               else if (arg1.IOName == Para.PCBase.ModeSwitchButton )
                {
                    if (!arg2)
                    {
                        if (FormPM == null || FormPM.IsDisposed)
                        {
                            FormPM = new FrmPM();
                        }
                        FormPM.Show();
                    }
                 
                }
                else if (arg1.IOName.Contains("SafeDoor") && !arg2)
                {
                    InterlockMonitor.IsSafeDoorOpen = true;
                }
            }
        }

        private void UpdateXYTValue(int stageIndex, double[] xytValue)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<int, double[]>(UpdateXYTValue), stageIndex, xytValue);
            }
            else
            {
                if (stageIndex == 0)
                {
                    lblXValue.Text = xytValue[0].ToString();
                    lblYValue.Text = xytValue[1].ToString();
                    lblTValue.Text = xytValue[2].ToString();
                }
                else
                {
                    lbl_xValue2.Text = xytValue[0].ToString();
                    lbl_yValue2.Text = xytValue[1].ToString();
                    lbl_tValue2.Text = xytValue[2].ToString();
                }
            }
        }
    
      
        private void AlarmManager_OnAlarm(int stationNo,HTAlarm.DB.Model.AlarmDetail detail)
        {
            Task.Factory.StartNew(() =>
            {
                if (detail.AlarmLevel == HTAlarm.DB.Model.AlarmLevel.HeavyAlarm)
                {
                    LinkSignalFlow.Instance.TurnOffInlineSignal(true);                 
                }
                if (MotionGlobal.IsHomeOK)
                {
                    CimManager.Instance.AlarmStateChangeReport(detail,true);
                }
                  
            });
        }

        private void Instance_OnShowCimSts(CIMMode mode)
        {
            if (InvokeRequired)
            {
                lblCimStatus.Invoke(new Action<CIMMode>(Instance_OnShowCimSts), mode);
            }
            else
            {
                lblCimStatus.Text = mode==CIMMode.CIMON ? "CimOn" : "CimOff";
                lblCimStatus.BackColor = mode == CIMMode.CIMON ? Color.FromArgb(83, 175, 80) : Color.FromArgb(232, 115, 94);
                panelEx12.FillColor = mode == CIMMode.CIMON ? Color.FromArgb(83, 175, 80) : Color.FromArgb(232, 115, 94);
                Motion.SetDigitalOutput(OutputName.BlueLight,mode==CIMMode.CIMON);
                Motion.SetDigitalOutput(OutputName.BlueLight2, mode == CIMMode.CIMON);
            }
        }

        private void InitManager_OnInitCom()
        {
            try
            {
                FrmAutoPos = GetChildView("FrmAutoPos") as FrmAutoPos;
                if (FrmAutoPos != null)
                {
                    FrmAutoPos.FormAutoPos.Init();
                }
                Motion = MotionManager.GetInstance();
       
                Output();
                InterlockMonitor.Instance.StartMonitor();
                //SVDataLoad.ReportMotorLoadRatio();
                DBManager.DeleteOldDatas(Param.Control.DataSaveDays);

                MonitorProbeHeadSts();
                CimManager.Instance.CimModeSet(Param.Cim.IsCimON?CIMMode.CIMON:CIMMode.CIMOFF);
                StartDfsExe();
                RecipeManager.RecipeFrmExpandDelegate = ProbeEditFrmShow;
                IsInitCom = true;
            }
            catch (Exception ex)
            {
                ShowMsg("FrmMain", $"InitManager_OnInitCom执行异常，异常信息：{ex}", Brushes.Red);
            }
        }
        private void ProbeEditFrmShow()
        {
            FrmRecipeProbeConfig frmRecipeProbeConfig = new FrmRecipeProbeConfig();
            frmRecipeProbeConfig.Show();
        }
        private void StartDfsExe()
        {
            try
            {
                if (string.IsNullOrEmpty(Param.Control.DfsExePath) == false)
                {
                    Process.Start(Param.Control.DfsExePath);
                }
            }
            catch (Exception ex)
            {
                ShowMsg("FrmMain", $"启动Dfs上传程序异常，异常信息：{ex}", Brushes.Red);
            }
        }
        private void DetectGantry_OnKeyInCodeRequest(string cimGlassID)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(DetectGantry_OnKeyInCodeRequest), cimGlassID);
            }
            else
            {
                FrmManualBarCode formManualBarCode = new FrmManualBarCode();
                formManualBarCode.CimGlassID = cimGlassID;
                if (formManualBarCode.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    DetectGantry.KeyInCode = formManualBarCode.Barcode;
                }
            }
        }

    
        private void Output()
        {
            Task.Factory.StartNew(() => {
                Motion.SetDigitalOutput(OutputName.EQEMO, true);
                //打开照明灯
                for (int i = 0; i < 8; i++)
                {
                    string name = $"EqpFloodLight{i + 1}";
                    Motion.SetDigitalOutput(name, true);
                    if (i < 2)
                    {
                        name = $"CabinetFloodLight{i + 1}";
                        Motion.SetDigitalOutput(name, true);
                    }
                }
                IsLightOn = true;
                //check stage is have glass
                if (IsDetectGlass(0))
                {
                    Motion.Vacuum("Stage1Vacuum", true);
                }
                if (IsDetectGlass(1))
                {
                    Motion.Vacuum("Stage2Vacuum", true);
                }
                Motion.SetDigitalOutput(OutputName.UvwBrake1, false);
                Motion.SetDigitalOutput(OutputName.UvwBrake2, false);

                IonizerPowerControl();
            });
        }
        private void IonizerPowerControl()
        {
            Motion.SetDigitalOutput(OutputName.Stage1IonizerPower1, true);
            Motion.SetDigitalOutput(OutputName.Stage1IonizerPower2, true);
            Motion.SetDigitalOutput(OutputName.Stage2IonizerPower1, true);
            Motion.SetDigitalOutput(OutputName.Stage2IonizerPower2, true);
            Motion.SetDigitalOutput(OutputName.DetectGantryIonizerPower, true);
            Motion.SetDigitalOutput(OutputName.Stage1SensorPower, true);
            Motion.SetDigitalOutput(OutputName.Stage2SensorPower, true);
        }
        private bool IsDetectGlass(int glassNum)
        {
            string inputName = string.Empty;
            bool ret = true;
            for (int i = 0; i < 4; i++)
            {
                inputName = $"Stage{glassNum + 1}GlassDetect{i + 1}";
                ret &= Motion.GetDigitalInput(inputName);
                Thread.Sleep(10);
            }
            return ret;
        }
        private void Instance_OnUpdateEQPSts(EqpSts obj)
        {
            try
            {
                Global.EqpStatus = obj;
                switch (obj)
                {
                    case EqpSts.RUN:
                        {
                            var view = GetViewInfo(0);
                            SwitchForm(view);
                            break;
                        }
                    case EqpSts.PAUSE:
                        {
                            TesterPC.Instance.TesterPCPause();
                        }
                        break;
                    case EqpSts.STOP:
                        {
                            Global.IsAutoPosOnly = false;
                            Global.AutoPosInfo = null;
                            MotionManager.GetInstance().StopBuffer();
                            if (MotionGlobal.IsHomeOK)
                            {
                                TesterPC.Instance.TesterPCStop();
                                LinkSignalFlow.Instance.TurnOffInlineSignal();
                            }
                            break;
                        }
                    case EqpSts.EMG:
                        {
                            Global.IsAutoPosOnly = false;
                            Global.AutoPosInfo = null;
                            Motion.SetDigitalOutput(OutputName.EQEMO, false);
                            MotionManager.GetInstance().StopBuffer();
                            if (MotionGlobal.IsHomeOK)
                            {
                                TesterPC.Instance.TesterPCStop();
                                LinkSignalFlow.Instance.TurnOffInlineSignal();
                            }
                            break;
                        }
                }
                if (MotionGlobal.IsHomeOK)
                {
                    Task.Factory.StartNew(() =>
                    {
                        CimManager.Instance.MachineStatusChangeReport(obj);
                    });
                }
            }
            catch (Exception ex)
            {
                ShowMsg("FrmMain", $"Instance_OnUpdateEQPSts执行异常，异常信息：{ex}", Brushes.Red);
            }
        }
        private bool InitManager_OnInitPara()
        {
            string connectStr = Ini.Read("DB", "ConnectionString", "");
            string sqlType = Ini.Read("DB", "SqlType", "");
            DBHelper.Init(connectStr);
            LoadParaToMainVIew();
            return CabinetInfoHelper.ReadCabinetInfo();
        }

        private void lblCimStatus_Click(object sender, EventArgs e)
        {
            if (CimManager.Instance.IsCimOn())
            {
                CimManager.Instance.CimModeSet(CIMMode.CIMOFF);
            }
            else
            {
                CimManager.Instance.CimModeSet(CIMMode.CIMON);
            }
        }
        public void ShowContactCamera(bool visible,bool iLive=false)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<bool,bool>(ShowContactCamera), visible, iLive);
            }
            else
            {
                try
                {
                    if (visible)
                    {
                        if (FrmContactCamera == null || FrmContactCamera.IsDisposed)
                        {
                            FrmContactCamera = new FrmContactCamera();
                            var cam = HTCameraHelper.Instance.GetCamera(0);
                            if (cam != null)
                            {
                                cam.OnImageReceived += FrmContactCamera.ContactmageReceived;
                            }
                            else
                            {
                                ShowMsg("FrmMain", $"Camera对象为空", Brushes.Red);
                            }
                        }
                        FrmContactCamera.Show();
                        if (iLive)
                        {
                            HTCameraHelper.Instance.Continues(0);
                        }
                        else
                        {
                            HTCameraHelper.Instance.Stop(0);
                        }
                    }
                    else
                    {
                         HTCameraHelper.Instance.Stop(0);
                        if (FrmContactCamera != null)
                        {
                            FrmContactCamera.Hide();
                        }
                    }
                }
                catch (Exception ex)
                {
                    ShowMsg("FrmMain", $"ShowContactCamera执行异常，异常信息：{ex}", Brushes.Red);
                }
            }

        }

        private void LoadParaToMainVIew()
        {
            this.Invoke(new Action(() =>
            {
                cbxTOSEnable.Checked = !Param.Detect.OpenShortDetectShield;
                cbxCimEnable.Checked = !Param.Cim.CimShield;
                cmxModes.Text = Param.Control.RunMode.ToString();
                cbxAoiEnable.Checked = !Param.Detect.AoiDetectShield;
                cbxMuraEnable.Checked = !Param.Detect.MuraDetectShield;
                cbxSquintMuraEnable.Checked = !Param.Detect.SquintMuraDetectShield;
                cbxMultiAngleEnable.Checked = !Param.Detect.MultiAngleDetectShield;
             
                lblRecipeName.Text = RecipeManager.CurRecipe.PPID+":"+ RecipeManager.CurRecipe.Reserved;
                lblPPID.Text = RecipeManager.CurRecipe.PPID;
            }));
        }

        private void cmxModes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsInitCom)
            {
                string runMode = cmxModes.SelectedItem.ToString();
                Param.Control.RunMode = EnumHelper.GetEnumValue<RunMode>(runMode);
                bool ret = RecipeManager.CurRecipe.Para.SavePara(ParaType.Control);
                OpRecordHelper.Record(new EqInfo { ViewName = "主界面", OperationType = OperationType.EqOperation, ButtonName = $"运行模式：{Param.Control.RunMode }" });
                ShowMsg("FrmMain",$"当前设备运行模式：{runMode}",Brushes.Blue);
            }
        }

        private void btnLight_Click(object sender, EventArgs e)
        {
            if (IsLightOn)
            {
                for (int i = 0; i < 8; i++)
                {
                    string name = $"EqpFloodLight{i + 1}";
                    Motion.SetDigitalOutput(name, false);
                    if (i < 2)
                    {
                        name = $"CabinetFloodLight{i + 1}";
                        Motion.SetDigitalOutput(name, false);
                    }
                }
            }
            else
            {
                for (int i = 0; i < 8; i++)
                {
                    string name = $"EqpFloodLight{i + 1}";
                    Motion.SetDigitalOutput(name, true);
                    if (i < 2)
                    {
                        name = $"CabinetFloodLight{i + 1}";
                        Motion.SetDigitalOutput(name, true);
                    }
                }
            }
            IsLightOn = !IsLightOn;
            btnLight.Text = IsLightOn ? "关灯" : "开灯";
        }

        private void cbxIREnable_ValueChanged(object sender, bool value)
        {
            if (IsSave[0] && IsInitCom)
            {
                Param.Detect.AoiDetectShield = !cbxAoiEnable.Checked;
                bool ret = RecipeManager.CurRecipe.Para.SavePara(ParaType.Detect);
                OpRecordHelper.Record(new EqInfo { ViewName = "主界面", OperationType = OperationType.EqOperation, ButtonName = $"AOI检测：{(Param.Detect.AoiDetectShield ? "屏蔽":"开启")}" });
            }
        }
        private void cbxReviewEnable_ValueChanged(object sender, bool value)
        {
            if (IsSave[1] && IsInitCom)
            {
                Param.Detect.MuraDetectShield = !cbxMuraEnable.Checked;
                bool ret = RecipeManager.CurRecipe.Para.SavePara(ParaType.Detect);
                OpRecordHelper.Record(new EqInfo { ViewName = "主界面", OperationType = OperationType.EqOperation, ButtonName = $"直视Mura检测：{(Param.Detect.MuraDetectShield ? "屏蔽" : "开启")}" });
            }
        }

        private void cbxScanerEnable_ValueChanged(object sender, bool value)
        {
            if (IsSave[2] && IsInitCom)
            {
                Param.Detect.OpenShortDetectShield = !cbxTOSEnable.Checked;
                bool ret = RecipeManager.CurRecipe.Para.SavePara(ParaType.Detect);
                OpRecordHelper.Record(new EqInfo { ViewName = "主界面", OperationType = OperationType.EqOperation, ButtonName = $"TOS检测：{(Param.Control.ScanerShield ? "屏蔽" : "开启")}" });
            }
        }

        private void cbxCimEnable_ValueChanged(object sender, bool value)
        {
            if (IsSave[3] && IsInitCom)
            {
                Param.Cim.CimShield = !cbxCimEnable.Checked;
                bool ret = RecipeManager.CurRecipe.Para.SavePara(ParaType.Cim);
                OpRecordHelper.Record(new EqInfo { ViewName = "主界面", OperationType = OperationType.EqOperation, ButtonName = $"Cim：{(Param.Cim.CimShield ? "屏蔽" : "开启")}" });
            }
        }

        private void btnProbeUp_Click(object sender, EventArgs e)
        {
            AoiPC.Instance.PowerControlReq(false, 0);
            AoiPC.Instance.PowerControlReq(false, 1);
            OpRecordHelper.Record(new EqInfo { ViewName = "主界面", OperationType = OperationType.EqOperation, ButtonName = "压头抬升按钮" });
            if (StationManager.Instance.IsInRunning())
            {
                MessageBox.Show("自动运行中，禁止手动控制压头");
                return;
            }
            if (StationManager.Instance.IsInHoming())
            {
                MessageBox.Show("回零过程中，禁止手动控制压头");
                return;
            }
            if (Motion.IsHomeOK(StationEnum.DualStage,AxisName.Stage1_ContactZ) ==false|| Motion.IsHomeOK(StationEnum.DualStage, AxisName.Stage2_ContactZ) == false)
            {
                MessageBox.Show("平台压接轴没有回零");
                return;
            }
            ProbeGantry ProbeGantry = StationManager.Instance.GetChild("ProbeGantry") as ProbeGantry;
            Task.Factory.StartNew(()=> {
                var t=ProbeGantry.StageStopContactSync(true);
                if (t.Result)
                {
                    MessageBox.Show("压头抬升完成");
                }
            });
           
        }
      
        private void cbxIREnable_Click(object sender, EventArgs e)
        {
            IsSave[0] = false;
            string str = !cbxAoiEnable.Checked ? "启用" : "屏蔽";
            if (MessageBox.Show($"是否{str}AOI检测", "询问", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                Task.Factory.StartNew(()=> {
                    cbxAoiEnable.Invoke(new Action(()=> {
                        cbxAoiEnable.Checked = !Param.Detect.AoiDetectShield;
                    }));  });
            }
            else
            {
                IsSave[0] = true;
            }
        }

        private void cbxReviewEnable_Click(object sender, EventArgs e)
        {
            IsSave[1] = false;
            string str = !cbxMuraEnable.Checked ? "启用" : "屏蔽";
            if (MessageBox.Show($"是否{str}直视Mura检测", "询问", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                Task.Factory.StartNew(() => {
                    cbxMuraEnable.Invoke(new Action(() => {
                        cbxMuraEnable.Checked = !Param.Detect.MuraDetectShield;
                    }));
                });
            }
            else
            {
                IsSave[1] = true;
            }
        }

        private void cbxScanerEnable_Click(object sender, EventArgs e)
        {
            IsSave[2] = false;
            string str = !cbxTOSEnable.Checked ? "启用" : "屏蔽";
            if (MessageBox.Show($"是否{str}TOS检测", "询问", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                Task.Factory.StartNew(() => {
                    cbxTOSEnable.Invoke(new Action(() => {
                        cbxTOSEnable.Checked = !Param.Detect.OpenShortDetectShield;
                    }));
                });
            }
            else
            {
                IsSave[2] = true;
            }
        }

        private void cbxCimEnable_Click(object sender, EventArgs e)
        {
            IsSave[3] = false;
            string str = !cbxCimEnable.Checked ? "启用" : "屏蔽";
            if (MessageBox.Show($"是否{str}Cim", "询问", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                Task.Factory.StartNew(() => {
                    cbxCimEnable.Invoke(new Action(() => {
                        cbxCimEnable.Checked = !Param.Cim.CimShield;
                    }));});
            }
            else
            {
                IsSave[3] = true;
            }
        }
        private void MonitorProbeHeadSts()
        {
            var t = new Thread(()=> {
                while (true)
                {
                    try
                    {
                        var ret1 = Motion.GetAxisActPos(StationEnum.DualStage, AxisName.Stage1_ContactZ) < 1&&Motion.IsHomeOK(StationEnum.DualStage, AxisName.Stage1_ContactZ);
                        var ret2 = Motion.GetAxisActPos(StationEnum.DualStage, AxisName.Stage2_ContactZ) < 1 && Motion.IsHomeOK(StationEnum.DualStage, AxisName.Stage2_ContactZ);
                        lblStage1ContactZ.Invoke(new Action(() => { lblStage1ContactZ.BackColor = ret1 ? Color.Lime : Color.OrangeRed; }));
                        lblStage2ContactZ.Invoke(new Action(() => { lblStage2ContactZ.BackColor = ret2 ? Color.Lime : Color.OrangeRed; }));
                    }
                    catch (Exception)
                    {
                    }
                    Thread.Sleep(500);
                }
            });
            t.IsBackground = true;
            t.Start();
        }

        private void btnContactClose_Click(object sender, EventArgs e)
        {
            ShowContactCamera(false,true);
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            ShowContactCamera(true,true);
        }

        private void cbxMultiAngleEnable_ValueChanged(object sender, bool value)
        {
            if (IsSave[4] && IsInitCom)
            {
                Param.Detect.MultiAngleDetectShield = !cbxMultiAngleEnable.Checked;
                bool ret = RecipeManager.CurRecipe.Para.SavePara(ParaType.Detect);
                OpRecordHelper.Record(new EqInfo { ViewName = "主界面", OperationType = OperationType.EqOperation, ButtonName = $"多角度检测：{(Param.Detect.MultiAngleDetectShield ? "屏蔽" : "开启")}" });
            }
        }

        private void cbxMultiAngleEnable_Click(object sender, EventArgs e)
        {
            IsSave[4] = false;
            string str = !cbxMultiAngleEnable.Checked ? "启用" : "屏蔽";
            if (MessageBox.Show($"是否{str}多角度检测", "询问", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                Task.Factory.StartNew(() => {
                    cbxMultiAngleEnable.Invoke(new Action(() => {
                        cbxMultiAngleEnable.Checked = !Param.Detect.MultiAngleDetectShield;
                    }));
                });
            }
            else
            {
                IsSave[4] = true;
            }
        }

        private void SignalProvider_OnUpdateAlignCamSts(string arg1, bool arg2)
        {
            DeviceStsManager.UpdateDevStatus(arg1, arg2);
        }

        private void cbxSquintMuraEnable_Click(object sender, EventArgs e)
        {
            IsSave[5] = false;
            string str = !cbxSquintMuraEnable.Checked ? "启用" : "屏蔽";
            if (MessageBox.Show($"是否{str}斜视检测", "询问", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                Task.Factory.StartNew(() => {
                    cbxSquintMuraEnable.Invoke(new Action(() => {
                        cbxSquintMuraEnable.Checked = !Param.Detect.SquintMuraDetectShield;
                    }));
                });
            }
            else
            {
                IsSave[5] = true;
            }
        }

        private void cbxSquintMuraEnable_ValueChanged(object sender, bool value)
        {
            if (IsSave[5] && IsInitCom)
            {
                Param.Detect.SquintMuraDetectShield = !cbxSquintMuraEnable.Checked;
                bool ret = RecipeManager.CurRecipe.Para.SavePara(ParaType.Detect);
                OpRecordHelper.Record(new EqInfo { ViewName = "主界面", OperationType = OperationType.EqOperation, ButtonName = $"斜视Mura检测：{(Param.Detect.SquintMuraDetectShield ? "屏蔽" : "开启")}" });
            }
        }

        private void btnAxisMove_Click(object sender, EventArgs e)
        {
            FrmAutoPosTeach frmAxisMove = new FrmAutoPosTeach();
            frmAxisMove.Show();
        }

        private void btnHomingStop_Click(object sender, EventArgs e)
        {
            StationManager.Instance.EcatStopHoming();
        }

    
    }
}
