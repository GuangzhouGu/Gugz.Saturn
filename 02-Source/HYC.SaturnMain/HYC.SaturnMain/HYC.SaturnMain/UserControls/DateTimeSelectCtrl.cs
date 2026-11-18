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
    public partial class DateTimeSelectCtrl : UserControl
    {
        public DateTimeSelectCtrl()
        {
            InitializeComponent();
        }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        private void dtpDateStart_ValueChanged(object sender, EventArgs e)
        {
            UpdateTime();
        }

        private void dtpTimeStart_ValueChanged(object sender, EventArgs e)
        {
            UpdateTime();
        }
        private void UpdateTime(bool isStart=true)
        {
            if (isStart)
            {
                StartTime = DateTime.Parse(dtpDateStart.Value.ToString("yyyy-MM-dd") + " " + dtpTimeStart.Value.ToString("HH:mm:ss"));
            }
            else
            {
                EndTime = DateTime.Parse(dtpDateEnd.Value.ToString("yyyy-MM-dd") + " " + dtpTimeEnd.Value.ToString("HH:mm:ss"));
            }
          
        }

        private void dtpDateEnd_ValueChanged(object sender, EventArgs e)
        {
            UpdateTime(false);
        }

        private void dtpTimeEnd_ValueChanged(object sender, EventArgs e)
        {
            UpdateTime(false);
        }
    }
}
