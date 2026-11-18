using HYC.HTPCBase.Extension.Entities;
using HYC.SaturnDB.DAL;
using HYC.SaturnDB.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HYC.SaturnMain.UserControls
{
    public partial class YieldMonitorCtrl : UserControl
    {
        public YieldMonitorCtrl()
        {
            InitializeComponent();
            Lbls = new Label[] { lblOkCount, lblNGCount, lblTotalCount, lblOkRate };
            TotalInfoDal.OnTriggerCount += DetectResultDal_OnTriggerCount;
        }

        double[] Yield = new double[4];//ok,ng,total,okRate
        private Label[] Lbls = null;
        private void DetectResultDal_OnTriggerCount(TotalInfoModel obj)
        {
            if (obj.Result == "OK")
            {
                Yield[0]++;
            }
            else
            {
                Yield[1]++;
            }
            Yield[2]++;
            if (Yield[2] > 0)
            {
                Yield[3] = Math.Round(Yield[0] * 100.0 / Yield[2], 2);
            }
            else
            {
                Yield[2] = 0;
            }
            ShowYield(Yield);
        }

        string StartTime = DateTime.Now.ToString("yyyy-MM-dd ") + "00:00:00";
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
                    if (i == 3)
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
            Task.Factory.StartNew(() => {
                var list = new TotalInfoDal().GetByTimeRange(DateTime.Parse(StartTime), DateTime.Now);
                if (list != null)
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
            if (ClassHelper.IsDesingMode() == false)
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
            await Task.Factory.StartNew(() => { Thread.Sleep(3000); });
            LoadYield();
        }

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            UpdateYield();
        }
    }
}
