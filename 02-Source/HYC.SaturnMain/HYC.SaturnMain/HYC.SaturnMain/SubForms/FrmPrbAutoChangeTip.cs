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

namespace HYC.SaturnMain.SubForms
{
    public partial class FrmPrbAutoChangeTip : FormPopBase
    {
        public FrmPrbAutoChangeTip()
        {
            InitializeComponent();
            this.Title = "Probe自动更换";
            Timer.Interval = 500;
            Timer.Tick += Timer_Tick;
            Timer.Start();
            this.TopMost = true;
        }
        System.Windows.Forms.Timer Timer = new System.Windows.Forms.Timer();
        int Blink = 0;
        private void Timer_Tick(object sender, EventArgs e)
        {
            Blink ^= 1;//异或
            lblTipMsg.ForeColor = Blink == 1 ? Color.White : Color.FromArgb(232, 115, 94);
            lblTipMsg.BackColor = Blink == 1 ? Color.FromArgb(232, 115, 94) : Color.White;
        }
    }
}
