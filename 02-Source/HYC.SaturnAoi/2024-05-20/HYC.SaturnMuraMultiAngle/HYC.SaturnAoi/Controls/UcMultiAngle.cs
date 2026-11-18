using HYC.SaturnAoi.DetectFlow;
using HYC.SaturnAoi.Devices.MultiAngle;
using HYC.SaturnAoi.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HYC.SaturnAoi.Controls
{
    public partial class UcMultiAngle : UserControl
    {
        public UcMultiAngle()
        {
            InitializeComponent();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            try
            {
                var value = MultiAngleHelper.Instance.Get(0).Grab();
                if (value!=null)
                {
                    this.dataGridView1.Rows.Clear();
                    for (int i = 0; i < value.Lumi.Length; i++)
                    {
                        int index=dataGridView1.Rows.Add();
                        dataGridView1.Rows[index].Cells["Lumi"].Value = value.Lumi[i].ToString();
                        dataGridView1.Rows[index].Cells["X"].Value = value.X[i].ToString();
                        dataGridView1.Rows[index].Cells["Y"].Value = value.Y[i].ToString();
                        dataGridView1.Rows[index].Cells["U"].Value = value.U[i].ToString();
                        dataGridView1.Rows[index].Cells["V"].Value = value.V[i].ToString();
                        dataGridView1.Rows[index].Cells["Cct"].Value = value.Cct[i].ToString();
                        dataGridView1.Rows[index].Cells["Exposure"].Value = value.Exposure[i].ToString();
                        dataGridView1.Rows[index].Cells["Noise"].Value = value.Noise[i].ToString();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void htButton1_Click(object sender, EventArgs e)
        {
            DetectReqArgs obj = new DetectReqArgs();
            obj.CellID = "AA01";
            obj.CellNum = 0;
            obj.ColumnNum = 0;
            obj.DetectType = "MultiAngle";
            obj.GlassID = DateTime.Now.ToString("yyyyMMdd");
            obj.GlassNum = 0;
            obj.PCIndex = 0;
            MultiAngleDetectReqHandle.Instance.MultiAngleDetectReq(obj);
        }

        private void btnGetAngleSelect_Click(object sender, EventArgs e)
        {
           var ret= MultiAngleHelper.Instance.Get(0).GetAngleSelect();
            txtAngleSelect.Text = ret;
        }
    }
}
