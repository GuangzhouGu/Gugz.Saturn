using DFS;
using HYC.HTLog;
using HYC.HTPCBase.Extension.Recipe;
using HYC.SaturnAoi.DetectFlow;
using HYC.SaturnAoi.Devices.ClientPC;
using HYC.SaturnAoi.Devices.MuraPC;
using HYC.SaturnAoi.Devices.Tester;
using HYC.SaturnAoi.Entities;
using HYC.SaturnAoi.Params;
using HYC.StandardAoi.Parameter;
using HYC.StandardAoi.UI;
using HYC.WindowsForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HYC.SaturnAoi
{
    public partial class FrmMain : FormBase
    {
        public FrmMain()
        {
            InitializeComponent();
        }
        FormStandardAoi FrmStandardAoi = new FormStandardAoi();
        InitManager InitMan = new InitManager();
        public  void InitDevices()
        {
            InitMan.OnConnected += new Action<string, int, bool>((p, m, n) => { UpdateDeviceState(p, n); });
            InitMan.OnShowMsg += new Action<string, string, Brush>((p, m, n) => { ShowMsg(m, n); });
            ProcessCmd.OnShowMsg += new Action<string, string, Brush>((p, m, n) => { ShowMsg(m, n); });
            AoiDetectReqHandle.OnShowMsg += new Action<string, string, Brush>((p, m, n) => { ShowMsg(m, n); });
            ProcessCmd.OnExecuteCommand += ProcessCmd_OnExecuteCommand;
            InitMan.OnUpdateRecipe += InitMan_OnUpdateRecipe;
            InitMan.Init();
            DFSManager.Instance.OnConnected += new Action<string, int, bool>((p, m, n) => { UpdateDeviceState(p, n); });
            DFSManager.Instance.OnShowMsg+= new Action<string, string, Brush>((p, m, n) => { ShowMsg(m, n); });
            DFSManager.Instance.Init();
            DFSEntity.Init();
            RecipeManager.OnRecipeChange += RecipeManager_OnRecipeReport;
            //FrmStandardAoi.SetDefaultView("FrmDetectDeail");
            StartExe();
            FrmStandardAoi.SetSignalProvider(Global.SignalProvider);
            FrmStandardAoi.SetImageProducer(Global.ImageProducers);
            FrmStandardAoi.SetPatternProducers(Global.PatternProducers);
        
            string logPath = AppDomain.CurrentDomain.BaseDirectory + "Log";
            LogManager.SetLogPath(logPath);
            LogManager.DeleteOldLog(30);
            ClientHelper.Instance.SendDfsParam(DFSSettings.StepID,DFSSettings.EQPID);
        }

        private void RecipeManager_OnRecipeReport(HTPCBase.Extension.Recipe.RecipeChangeType arg1, Recipe arg2)
        {
            ELPTesterHelper.Instance.GetTester(0).DownLoadRecipe(0, 0, Param.System.PGRecipeFilePath);
        }

        private void StartExe()
        {
            try
            {
                if (string.IsNullOrEmpty(Param.System.MuraPCExePath+ "HYC.SaturnMuraMultiAngle.exe") == false)
                {
                    Process.Start(Param.System.MuraPCExePath+ "HYC.SaturnMuraMultiAngle.exe");
                }
                if (Param.System.IsTesterUseDllCommunication&& string.IsNullOrEmpty(Param.System.ElpPGExePath ) == false)
                {
                    Process.Start(Param.System.ElpPGExePath);
                }
            }
            catch (Exception ex)
            {
                ShowMsg($"启动HYC.SaturnMuraMultiAngle.exe程序异常，异常信息：{ex}", Brushes.Red);
            }
        }
        private void ProcessCmd_OnExecuteCommand(Command arg1, object arg2)
        {
            try
            {
                var strs = arg2.ToString().Split(',');
                bool ret = true;
                switch (arg1)
                {
                    case Command.RecipeSync:
                        {
                            if (strs[1] == "Shift")
                            {
                                MuraPC.Instance.RecipeSync(strs[0], strs[1]);
                                FrmStandardAoi.RecipeSelect(strs[0]);
                                ret = RecipeManager.ShiftRecipe(strs[0]);
                               
                            }
                            else if (strs[1] == "Add")
                            {
                                ret = RecipeSyncManager.CreateNewRecipe(RecipeManager.CurRecipe.RecipeID, strs[0]);
                                RecipeManager.LoadRecipeList();
                            }
                            else if (strs[1] == "Delete")
                            {
                                ret = RecipeSyncManager.CreateNewRecipe(RecipeManager.CurRecipe.RecipeID, strs[0]);
                                RecipeManager.LoadRecipeList();
                            }
                            ClientHelper.Instance.SendRecipeSyncResult(Param.System.PcIndex, ret);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                ShowMsg($"启动HYC.SaturnMuraMultiAngle.exe程序异常，异常信息：{ex}", Brushes.Red);
            }

        }

        /// <summary>
        /// 显示组件连接状态
        /// </summary>
        /// <param name="devName"></param>
        /// <param name="status"></param>
        public void UpdateDeviceState(string devName, bool? status)
        {
            FrmStandardAoi.UpdateDeviceState(devName, status);
        }
        private void InitMan_OnUpdateRecipe(string obj)
        {
            try
            {
               
            }
            catch (Exception ex)
            {
                ShowMsg($"InitMan_OnUpdateRecipe执行异常，异常信息：{ex.ToString()}", Brushes.Red);
            }
        }

        public void ShowMsg(string strValue, Brush brush)
        {
            if (strValue.Contains("机种名同步:"))
            {
                string recipeID = strValue.Split(':')[1];
                if (!string.IsNullOrEmpty(recipeID))
                {
                    if (recipeID != RecipeManager.CurRecipe.RecipeID)
                    {
                        InitMan.RecipeSelect(recipeID);
                        FrmStandardAoi.RecipeSelect(recipeID);
                    }
                }
            }
            FrmStandardAoi.ShowMsg(strValue, brush);
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            FrmStandardAoi.DetectionTypeUI = DetectionTypeUI.AoiMura;
            FrmStandardAoi.FormBorderStyle = FormBorderStyle.None;
            FrmStandardAoi.TopLevel = false;
            FrmStandardAoi.Dock = DockStyle.Fill;
            this.pnlViewHost.Controls.Add(FrmStandardAoi);
            FrmStandardAoi.Show();
            InitDevices();
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("是否退出应用程序？","询问",MessageBoxButtons.OKCancel)!=DialogResult.OK)
            {
                e.Cancel = true;
                return;
            }
            InitMan.Uninit();
        }
    }
}
