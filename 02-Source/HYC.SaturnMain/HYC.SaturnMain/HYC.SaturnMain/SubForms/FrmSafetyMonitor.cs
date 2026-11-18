using HYC.HTPCBase.Extension.Views;
using HYC.HTPCBase.Station;
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

namespace PPITester.SubForms
{
    public partial class FrmSafetyMonitor : ViewBase
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
        public FrmSafetyMonitor()
        {
            InitializeComponent();
            this.Index = 5;
            this.ButtonName = "安全监控";
            this.ButtonNameEn = "Safety\nMonitor";
            this.IsNeedLogin = false;
            this.ButtonImage = HYC.SaturnMain.Properties.Resources.SafeMonitor;
        }

        private void Instance_OnSignalTrigger(HYC.HTPCBase.Motion.IOInfo arg1, bool arg2)
        {

        }
        private void FrmSafetyMonitor_Load(object sender, EventArgs e)
        {
            StationManager.Instance.OnSignalTrigger += Instance_OnSignalTrigger;
        }
    }
}
