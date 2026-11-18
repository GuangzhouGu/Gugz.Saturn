using HYC.HTLog;
using HYC.SaturnAoi.DetectFlow.MultiAngleDetect;
using HYC.SaturnAoi.Devices.MultiAngle;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HYC.SaturnAoi.Controls
{
    public partial class UcMultiAngleDetectDetail : UserControl
    {
        public UcMultiAngleDetectDetail()
        {
            InitializeComponent();
        }
        [Description("画面名称")]
        public string PatternName
        {
            get { return label1.Text; }
            set { label1.Text=value; }
        }
        public void ShowDetectDetail(PatternDetectInfo patternDetectInfo,string cellID)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<PatternDetectInfo,string>(ShowDetectDetail), patternDetectInfo, cellID);
            }
            else
            {
                try
                {
                    this.dgvDetail.Rows.Clear();
                    if (patternDetectInfo != null)
                    {
                        if (patternDetectInfo.MultiAngleValue != null)
                        {
                            for (int i = 0; i < patternDetectInfo.MultiAngleValue.Lumi.Length; i++)
                            {
                                int index = dgvDetail.Rows.Add();
                                dgvDetail.Rows[index].Cells["Lumi"].Value = patternDetectInfo.MultiAngleValue.Lumi[i].ToString();
                                dgvDetail.Rows[index].Cells["X"].Value = patternDetectInfo.MultiAngleValue.X[i].ToString();
                                dgvDetail.Rows[index].Cells["Y"].Value = patternDetectInfo.MultiAngleValue.Y[i].ToString();
                                dgvDetail.Rows[index].Cells["U"].Value = patternDetectInfo.MultiAngleValue.U[i].ToString();
                                dgvDetail.Rows[index].Cells["V"].Value = patternDetectInfo.MultiAngleValue.V[i].ToString();
                                dgvDetail.Rows[index].Cells["Cct"].Value = patternDetectInfo.MultiAngleValue.Cct[i].ToString();
                                dgvDetail.Rows[index].Cells["Exposure"].Value = patternDetectInfo.MultiAngleValue.Exposure[i].ToString();
                                dgvDetail.Rows[index].Cells["Noise"].Value = patternDetectInfo.MultiAngleValue.Noise[i].ToString();
                            }
                            lblResult.Text = patternDetectInfo.Result;
                            lblResult.BackColor = patternDetectInfo.Result == "OK" ? Color.Lime : Color.Tomato;
                            lblCellID.Text = cellID;
                        }
                    }
                    else
                    {
                        lblResult.Text = "None";
                        lblResult.BackColor = Color.Gold;
                        lblCellID.Text = string.Empty;
                    }
                }
                catch (Exception ex)
                {
                    LogManager.GetLogger("UcMultiAngleDetectDetail").Info(ex.ToString());
                }
            }

          
        }
        private void InitDgv(DataGridView dgv)
        {
            dgv.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.White;
            dgv.RowsDefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
            dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            var dgvType = dgv.GetType();
            var properInfo = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            properInfo.SetValue(dgv, true, null);
            ClearDgvBorderStyle(dgv);
        }

        private void ClearDgvBorderStyle(DataGridView dgv)
        {
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.BorderStyle = BorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("微软雅黑", 11);
            foreach (var item in dgv.Columns)
            {
                DataGridViewColumn column = item as DataGridViewColumn;
                column.DefaultCellStyle.Font = new Font("微软雅黑", 10);
            }
        }

        private void UcMultiAngleDetectDetail_Load(object sender, EventArgs e)
        {
            InitDgv(this.dgvDetail);
        }
    }
}
