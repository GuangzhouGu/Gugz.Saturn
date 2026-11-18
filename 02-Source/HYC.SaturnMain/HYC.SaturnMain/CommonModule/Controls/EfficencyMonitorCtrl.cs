using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using CommonFunc.Entities;
using CommonModule.Entities;
using CommonModule.Controls;

namespace EqpRunHistory.Controls
{
    public partial class EfficencyMonitorCtrl : CtrlBase
    {
        public EfficencyMonitorCtrl()
        {
            InitializeComponent();
            LblTimes = new Label[6] { lblRuntime, lblPausetime,lblStandbyTime, lblMediumAlmtime, lblSeriousAlmtime, lblStoptime };
        }
        private bool IsMonitor = false;

        private static int Count = 6;

        private DateTime StartTime = DateTime.Parse("00:00:00");

        private DateTime[] Times = new DateTime[Count];

        private Label[] LblTimes = null;

        private void EfficencyMonitorCtrl_Load(object sender, EventArgs e)
        {
            if (CommonHelper.IsDesingMode()==false)
            {
                DelayMonitor();
            }
        }

        private void Monitor()
        {
            Thread thread = new Thread((ThreadStart)delegate
            {
                while (true)
                {
                    try
                    {
                        if (IsMonitor)
                        {
                            for (int i = 0; i < Count; i++)
                            {
                                Times[i] = EqpStatusManager.GetEqpTime(StartTime, DateTime.Now, (EqpStatus)i);
                            }
                            ShowTime(Times);
                        }
                    }
                    catch (Exception)
                    {
                    }
                    Thread.Sleep(500);
                }
            });
            thread.IsBackground = true;
            thread.Start();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            StartTime = dateTimePicker1.Value;
            if (!dateTimePicker1.Checked)
            {
                StartTime = DateTime.Parse("00:00:00");
            }
        }

        private void ShowTime(DateTime[] times)
        {
            if (base.InvokeRequired)
            {
                Invoke(new Action<DateTime[]>(ShowTime), times);
            }
            else
            {
                for (int i = 0; i < Count; i++)
                {
                    LblTimes[i].Text = times[i].ToString("HH:mm:ss");
                }
            }
           
        }

        private void EfficencyMonitorCtrl_VisibleChanged(object sender, EventArgs e)
        {
            IsMonitor = base.Visible;
        }
        private async void DelayMonitor()
        {
            await Task.Factory.StartNew(() => { Thread.Sleep(3000); });
            Monitor();
        }
      
    }
}
