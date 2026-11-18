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
using CommonModule.Entities;
using CommonDB.Dal;
using HYC.HTFile.HTINI;

namespace CommonModule.Controls
{
    public partial class YieldMonitorCtrl : CtrlBase
    {
        public YieldMonitorCtrl()
        {
            InitializeComponent();
            Lbls = new Label[] {lblOkCount,lblNGCount,lblTotalCount,lblOkRate };
            DetectResultDal.OnTriggerCount += DetectResultDal_OnTriggerCount;
        }

        double[] Yield = new double[4];//ok,ng,total,okRate
        private Label[] Lbls= null;
        private void DetectResultDal_OnTriggerCount(CommonDB.Model.DetectResultModel obj)
        {
            if (obj.Result== "OK")
            {
                Yield[0]++;
            }
            else
            {
                Yield[1]++;
            }
            Yield[2]++;
            if (Yield[2]>0)
            {
                Yield[3] = Math.Round(Yield[0] * 100.0 / Yield[2], 2);
            }
            else
            {
                Yield[2] = 0;
            }
            ShowYield(Yield);
        }

        string StartTime =DateTime.Now.ToString("yyyy-MM-dd ") +"00:00:00";
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            UpdateYield();
        }
        private void UpdateYield()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(UpdateYield));
            }
            else
            {
                ClearYield();
                string date = this.dtpDate.Value.ToString("yyyy-MM-dd ");
                string time = this.dtpTime.Value.ToString("HH:mm:ss");

                if (!dtpTime.Checked)
                {
                    StartTime = date + "00:00:00";
                }
                else
                {
                    StartTime = date + time;
                }
                LoadYield();
            }
        }
        private void ShowYield(double[] yield)
        {
            if (base.InvokeRequired)
            {
                Invoke(new Action<double[]>(ShowYield), yield);
            }
            else
            {
                for (int i = 0; i < Lbls.Length; i++)
                {
                    if (i==3)
                    {
                        Lbls[i].Text = $"{yield[i]}%";
                    }
                    else
                    {
                        Lbls[i].Text = yield[i].ToString();
                    }
                }
            }
        }
        private void LoadYield()
        {
            Task.Factory.StartNew(()=> {
                var list = new DetectResultDal().GetByTimeRange(DateTime.Parse(StartTime), DateTime.Now);
                if (list!=null)
                {
                    foreach (var item in list)
                    {
                        if (item.Result == "OK")
                        {
                            Yield[0]++;
                        }
                        else
                        {
                            Yield[1]++;
                        }
                        Yield[2]++;
                    }
                    if (Yield[2] > 0)
                    {
                        Yield[3] = Math.Round(Yield[0] * 100.0 / Yield[2], 2);
                    }
                    else
                    {
                        Yield[2] = 0;
                    }
                }
                ShowYield(Yield);
            });

        }
        private void YieldCtrl_Load(object sender, EventArgs e)
        {
            if (CommonHelper.IsDesingMode() == false)
            {
                DelayLoadYield();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearYield();
        }
        private void ClearYield()
        {
            Lbls.Select(p => p.Text = "").ToArray();
            Yield = Yield.Select(p => p = 0).ToArray();
        }
        private async void DelayLoadYield()
        {
            await Task.Factory.StartNew(()=> { Thread.Sleep(3000); });
            LoadYield();
        }

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            UpdateYield();
        }
    }
}
