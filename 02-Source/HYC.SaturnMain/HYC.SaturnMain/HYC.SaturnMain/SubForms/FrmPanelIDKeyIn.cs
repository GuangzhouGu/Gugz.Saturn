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
    public partial class FrmManualBarCode : Form
    {
        public FrmManualBarCode()
        {
            InitializeComponent();
            Timer.Interval = 500;
            Timer.Tick += Timer_Tick;
            Timer.Start();
        }
        System.Windows.Forms.Timer Timer = new System.Windows.Forms.Timer();

        public string Barcode { get; set; }
        public string CimGlassID { get; set; }
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(tbxBarcode.Text))
            {
                MessageBox.Show("请输入条码！");
                this.DialogResult = DialogResult.No;
                return;
            }
            Barcode = tbxBarcode.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        int Blink = 0;
        private void Timer_Tick(object sender, EventArgs e)
        {
            Blink ^= 1;//异或
            lblTipMsg.ForeColor = Blink == 1 ? Color.White : Color.FromArgb(232, 115, 94);
            lblTipMsg.BackColor = Blink == 1 ? Color.FromArgb(232, 115, 94) : Color.White;
        }

        private void FrmManualBarCode_Load(object sender, EventArgs e)
        {
            tbxBarcode.Focus();
        }

        private void cbxUseUpstreamGlassID_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxUseUpstreamGlassID.Checked)
            {
                tbxBarcode.Text = CimGlassID;
            }
        }

        private void FrmManualBarCode_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (string.IsNullOrEmpty(Barcode))
            {
                MessageBox.Show("还未输入条码，禁止关闭");
                e.Cancel = true;
            }
        }
    }
}
