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
using CommonFunc.Entities;
using System.Diagnostics;
using CommonModule.Entities;
using CommonModule.Controls;

namespace CommonFunc.Controls
{
    public partial class PerformanceMonitorCtrl : CtrlBase
    {
        public PerformanceMonitorCtrl()
        {
            InitializeComponent();
            Lbls = new Label[] {lblMtba,lblMtbf,lblJamRate,lblUph };
        }
        private DateTime StartTime = DateTime.Parse("00:00:00");
        private bool IsMonitor = false;
        PerformanceManager Manager = new PerformanceManager();
        private Label[] Lbls = null;
        double[] Performances = new double[4];
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
                            Read();
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                    Thread.Sleep(30 * 1000);
                }
            });
            thread.IsBackground = true;
            thread.Start();
        }
        private void Read()
        {
            Performances[0] = Manager.GetMtba(StartTime, DateTime.Now);
            Performances[1] = Manager.GetMtbf(StartTime, DateTime.Now);
            Performances[2] = Manager.GetJamRate(StartTime, DateTime.Now);
            Performances[3] = Manager.GetUph(StartTime, DateTime.Now);
            ShowValues();
        }
        private void ShowValues()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(ShowValues));
            }
            else
            {
                for (int i = 0; i < Performances.Length; i++)
                {
                    Lbls[i].Text = Performances[i].ToString();
                }
            }
        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            StartTime = dateTimePicker1.Value;
            if (!dateTimePicker1.Checked)
            {
                StartTime = DateTime.Parse("00:00:00");
            }
        }

        private void PerformanceMonitorCtrl_Load(object sender, EventArgs e)
        {
            if (CommonHelper.IsDesingMode()==false)
            {
                DelayMonitor();
            }
        }

        private void PerformanceMonitorCtrl_VisibleChanged(object sender, EventArgs e)
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
