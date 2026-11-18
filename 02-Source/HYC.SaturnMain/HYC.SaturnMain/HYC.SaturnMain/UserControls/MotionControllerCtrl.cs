using HYC.HTPCBase.Motion;
using HYC.SaturnMain.Devices.Motion;
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
    public partial class MotionControllerCtrl : UserControl
    {
        public MotionControllerCtrl()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() => {
                MotionGlobal.MotionCards[0].InitCard();
            });
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() => {
                MotionManager.GetInstance().ResetController(0);
            });
        }

        private void btnRunBuffer_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbxBufferID.Text) == false)
            {
                var ret = short.TryParse(tbxBufferID.Text, out short bufferId);
                if (ret && bufferId >= 0)
                {
                    if (MessageBox.Show("执行当前Buffer有撞机风险，请确认是否执行") == DialogResult.OK)
                    {
                        MotionManager.GetInstance().RunBuffer(bufferId);
                    }
                }
            }
        }

        private void btnReadLog_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否解除料仓龙门") == DialogResult.OK)
            {
                MotionManager.GetInstance().ServoOff(StationEnum.Cabinet,AxisName.CabGantry_Z1);
                MotionManager.GetInstance().RunBuffer(1);
            }
            
        }
    }
}
