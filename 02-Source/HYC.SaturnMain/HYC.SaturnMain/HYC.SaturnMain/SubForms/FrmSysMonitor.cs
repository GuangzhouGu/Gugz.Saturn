using HYC.HTCard;
using HYC.HTLog;
using HYC.HTPCBase.Config;
using HYC.HTPCBase.Extension.Views;
using HYC.HTPCBase.Motion;
using HYC.HTPCBase.Station;
using HYC.SaturnMain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HYC.SaturnMain.SubForms
{
    public partial class FrmSysMonitor : ViewBase
    {
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }
        bool IsShowed = false;
        public FrmSysMonitor()
        {
            InitializeComponent();
            this.Index = 6;
            this.ButtonName = "系统监控";
            this.ButtonNameEn = "System\nMonitor";
            this.IsNeedLogin = false;
            this.ButtonImage = HYC.SaturnMain.Properties.Resources.SafeMonitor;
            SVDataLoad.OnShowMotorLoadRatio += SVDataReport_OnShowMotorLoadRatio;
            InitDgv();
        }

        private void SVDataReport_OnShowMotorLoadRatio(List<MotorInfo> obj)
        {
            if (IsShowed)
            {
                try
                {
                    if (obj != null && obj.Count > 0)
                    {
                        for (int i = 0; i < obj.Count; i++)
                        {
                            ShowValue(i, obj[i].LoadRatio);
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        private void InitDgv()
        {
            var dgvType = dgvMotorLoadRatio.GetType();
            var properInfo = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            properInfo.SetValue(dgvMotorLoadRatio, true, null);
            ClearDgvBorderStyle(dgvMotorLoadRatio);
        }

        private void ClearDgvBorderStyle(DataGridView dgv)
        {
            dgv.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.White;
            dgv.RowsDefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
            dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.BorderStyle = BorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("微软雅黑", 10);
            foreach (var item in dgv.Columns)
            {
                DataGridViewColumn column = item as DataGridViewColumn;
                column.DefaultCellStyle.Font = new Font("微软雅黑", 9);
            }
        }
        private void FrmSafetyMonitor_Load(object sender, EventArgs e)
        {
            LoadAxisInfos();
            Thread.Sleep(200);
            IsShowed = true;
        }

        private void LoadAxisInfos()
        {
            List<AxisInfo> axisList = new List<AxisInfo>();
            if (MotionConfig.Instance.StationList != null)
            {
                foreach (var item in MotionConfig.Instance.StationList)
                {
                    axisList.AddRange(item.AxisList);
                }
                axisList.Sort((x,y)=>x.AxisNum.CompareTo(y.AxisNum));
            }
            foreach (var item in axisList)
            {
                int index = dgvMotorLoadRatio.Rows.Add();
                dgvMotorLoadRatio.Rows[index].Cells[0].Value = item.AxisNum;
                dgvMotorLoadRatio.Rows[index].Cells[1].Value = item.AxisName;
                dgvMotorLoadRatio.Rows[index].Cells[2].Value = item.AxisNameChn;
                dgvMotorLoadRatio.Rows[index].Cells[3].Value = item.CardNum;
                dgvMotorLoadRatio.Rows[index].Cells[4].Value = item.StationName;
                dgvMotorLoadRatio.Rows[index].Cells[5].Value = item.CardVender;
            }
           
        }

        private void ShowValue(int num,double value)
        {
            if (InvokeRequired)
            {
                dgvMotorLoadRatio.Invoke(new Action<int,double>(ShowValue),num,value);
            }
            else
            {
                dgvMotorLoadRatio.Rows[num].Cells[6].Value = $"{value}%";
            }
        }
    }
}
