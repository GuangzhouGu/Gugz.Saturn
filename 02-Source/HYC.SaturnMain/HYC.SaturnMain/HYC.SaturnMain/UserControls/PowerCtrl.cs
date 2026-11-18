using HYC.HTPCBase.Extension.Entities;
using HYC.HTPower;
using HYC.SaturnMain.Devices.Power;
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
    public partial class PowerCtrl : UserControl
    {
        public PowerCtrl()
        {
            InitializeComponent();
        }

        private void PowerCtrl_Load(object sender, EventArgs e)
        {
            if (ClassHelper.IsDesingMode() == false)
            {
                InitCtrl();
            }
        }
        public void InitCtrl()
        {
            if (PowerHelper.Instance != null)
            {
                try
                {
                    for (int i = 0; i < PowerHelper.Instance.PowerCount; i++)
                    {
                        this.cmxPower.Items.Add("光源控制器" + (i + 1));
                    }
                    this.cmxPower.SelectedIndex = 0;
                    this.cmxChannels.SelectedIndex = 0;
                }
                catch (Exception)
                {

                }
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            int powerIndex = this.cmxPower.SelectedIndex;
            int channel = this.cmxChannels.SelectedIndex;
            PowerHelper.Instance.GetPowerByNum(powerIndex).PowerOn((HTPowerChannel)channel);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            int powerIndex = this.cmxPower.SelectedIndex;
            int channel = this.cmxChannels.SelectedIndex;
            PowerHelper.Instance.GetPowerByNum(powerIndex).PowerOff((HTPowerChannel)channel);
        }
    }
}
