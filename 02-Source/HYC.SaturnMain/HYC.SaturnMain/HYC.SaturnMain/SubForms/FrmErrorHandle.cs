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
    public partial class FrmErrorHandle : FormPopBase
    {
        public FrmErrorHandle()
        {
            InitializeComponent();
            this.Title = "料仓更换Probe流程错误处理";
            this.TopMost = true;
        }
        public int Mode = -1;

        private void btnContinue_Click(object sender, EventArgs e)
        {
            Mode = 0;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            Mode = 2;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
