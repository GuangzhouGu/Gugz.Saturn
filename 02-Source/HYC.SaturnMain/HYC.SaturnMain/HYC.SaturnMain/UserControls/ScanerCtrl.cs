using HYC.HTPCBase.Extension.Entities;
using HYC.SaturnMain.Devices.Scaner;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HYC.SaturnMain.UserControls
{
    public partial class ScanerCtrl : UserControl
    {
        public ScanerCtrl()
        {
            InitializeComponent();
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            var code = ScanerHelper.Instance.Scancode(0);
            lbxScanerMsg.Items.Add(code);
        }

        private void btnClearScanerMsg_Click(object sender, EventArgs e)
        {
            lbxScanerMsg.Items.Clear();
        }

        private void ScanerCtrl_Load(object sender, EventArgs e)
        {
            if (ClassHelper.IsDesingMode() == false)
            {
                InitCtrl();
            }
        }
        /// <summary>
        /// 初始化
        /// </summary>
        public void InitCtrl()
        {
            if (ScanerHelper.Instance!=null)
            {
                try
                {
                    for (int i = 0; i < ScanerHelper.Instance.ScanerCount; i++)
                    {
                        this.cbxScanerList.Items.Add("扫码枪" + (i + 1));
                    }
                    this.cbxScanerList.SelectedIndex = 0;
                }
                catch (Exception)
                {

                }
            }
        }
    }
}
