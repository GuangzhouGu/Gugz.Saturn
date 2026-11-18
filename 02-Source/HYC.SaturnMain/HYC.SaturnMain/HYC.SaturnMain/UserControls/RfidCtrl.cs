using HYC.HTPCBase.Extension.Entities;
using HYC.SaturnMain.Devices.Rfid;
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
    public partial class RfidCtrl : UserControl
    {
        public RfidCtrl()
        {
            InitializeComponent();
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            var temp = RfidHelper.Instance.Read();
            if (temp != null)
            {
                lbxRfidMsg.Items.Add(temp);
            }
        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            var value = tbxWriteValue.Text;
            RfidHelper.Instance.Write(value);
        }

        private void btnClearRfidMsg_Click(object sender, EventArgs e)
        {
            lbxRfidMsg.Items.Clear();
        }
        /// <summary>
        /// 初始化
        /// </summary>
        public void InitCtrl()
        {
            if (RfidHelper.Instance != null)
            {
                try
                {
                    for (int i = 0; i < RfidHelper.Instance.RfidCount; i++)
                    {
                        this.cmxRfid.Items.Add("RFID读写器" + (i + 1));
                    }
                    this.cmxRfid.SelectedIndex = 0;
                }
                catch (Exception)
                {

                }
            }
        }

        private void RfidCtrl_Load(object sender, EventArgs e)
        {
            if (ClassHelper.IsDesingMode() == false)
            {
                InitCtrl();
            }
        }
    }
}
