
using HYC.HTLog;
using HYC.HTPCBase.Extension.Recipe;
using HYC.SaturnAoi.DetectFlow;
using HYC.SaturnAoi.Entities;
using HYC.SaturnAoi.Params;
using HYC.SaturnAoi.SubForms;
using HYC.SaturnMuraMultiAngle.Entities;
using HYC.StandardAoi.Parameter;
using HYC.StandardAoi.UI;
using HYC.WindowsForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        public static FrmMain Instance = null;
        static FrmMain()
        {
            Instance = new FrmMain();
        }
        public FormStandardAoi FrmStandardAoi = new FormStandardAoi();
        InitManager InitMan = new InitManager();
        public  void InitDevices()
        {
            InitMan.OnConnected += new Action<string, int, bool>((p, m, n) => { UpdateDeviceState(p, n); });
            InitMan.OnShowMsg += new Action<string, string, Brush>((p, m, n) => { ShowMsg(m, n); });
            ProcessCmd.OnShowMsg += new Action<string, string, Brush>((p, m, n) => { ShowMsg(m, n); });
            AoiDetectReqHandle.OnShowMsg += new Action<string, string, Brush>((p, m, n) => { ShowMsg(m, n); });
            MultiAngleDetectReqHandle.OnShowMsg += new Action<string, string, Brush>((p, m, n) => { ShowMsg(m, n); });
            ProcessCmd.OnExecuteCommand += ProcessCmd_OnExecuteCommand;
            InitMan.Init();
            //DFSManager.Instance.OnConnected += new Action<string, int, bool>((p, m, n) => { UpdateDeviceState(p, n); });
            //DFSManager.Instance.OnShowMsg += new Action<string, string, Brush>((p, m, n) => { ShowMsg(m, n); });
            //DFSManager.Instance.Init();
            //DFSEntity.Init();
            Global.PatternProducers.Add(new PatternProducer());
            FrmStandardAoi.SetSignalProvider(Global.SignalProvider);
            FrmStandardAoi.SetImageProducer(Global.ImageProducers);
            FrmStandardAoi.SetPatternProducers(Global.PatternProducers);
         
            string logPath = AppDomain.CurrentDomain.BaseDirectory + "Log";
            LogManager.SetLogPath(logPath);
            LogManager.DeleteOldLog(30);

        
        }

        private void ProcessCmd_OnExecuteCommand(Command arg1, object arg2)
        {
            switch (arg1)
            {              
                case Command.RecipeSync:
                    {
                        var strs = arg2.ToString().Split(',');
                        FrmStandardAoi.RecipeSelect(strs[0]);
                        RecipeManager.ShiftRecipe(strs[0]);
                    }
                    break;           
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
            FrmStandardAoi.DetectionTypeUI = DetectionTypeUI.Mura;
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
