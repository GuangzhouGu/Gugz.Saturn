using HYC.HTPCBase.Extension.Views;
using HYC.SaturnDB.DAL;
using HYC.SaturnDB.Model;
using HYC.HTLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using HYC.SaturnMain.Entities;
using HYC.SaturnMain.Params;
using HYC.WindowsControls;

namespace HYC.SaturnMain.SubForms
{
    public partial class FrmQuery : ViewBase
    {
        public FrmQuery()
        {
            InitializeComponent();
            this.Index = 7;
            this.ButtonName = "数据查询";
            this.ButtonNameEn = "Query";
            this.IsNeedLogin = false;
            this.ButtonImage = HYC.SaturnMain.Properties.Resources.Query;
            InitDgvBorderStyle(dgvProbeInfo);

        }
        int SelectedRowNum = -1;
      
        private void InitDgvBorderStyle(HTDataGridViewUnit dgv)
        {
            this.dgvProbeInfo.View.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProbeInfo.View.RowsDefaultCellStyle.BackColor = Color.FromArgb(0xF2, 0xF2, 0xF2);
            this.dgvProbeInfo.View.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(0xE7, 0xE7, 0xE7);
            this.dgvProbeInfo.View.AllowUserToAddRows = false;
        }
        private void btn_query_Click(object sender, EventArgs e)
        {
            try
            {
                dgv_detectInfo.DataSource = null;
                Task.Factory.StartNew(() => {
                    Invoke(new Action(() => {
                        OpenShortInfoDal dal = new OpenShortInfoDal();
                        OpenShortNGInfoDal dal3 = new OpenShortNGInfoDal();
                        DateTime startTime = DateTime.Parse(dtpDateStart.Value.ToString("yyyy-MM-dd") + " " + dtpTimeStart.Value.ToString("HH:mm:ss"));
                        DateTime endTime = DateTime.Now;
                        if (rbtCellInfo.Checked)
                        {
                            DataSet list = null;
                            DataSet temp = null;
                            if (cbxByTime.Checked)
                            {
                                //if (!cbxNow.Checked)
                                //{
                                //    endTime = DateTime.Parse(dtpDateEnd.Value.ToString("yyyy-MM-dd") + " " + dtpTimeEnd.Value.ToString("HH:mm:ss"));
                                //    TimeSpan span = endTime - startTime;
                                //    if (span.Days>5)
                                //    {
                                //        MessageBox.Show("查询日期跨度不能大于5天");
                                //        return;
                                //    }
                                //}
                                list = dal.GetDataSetByTime(startTime, endTime);
                            }
                            else if (cbxByID.Checked)
                            {
                                string id = tbxID.Text;
                                if (string.IsNullOrEmpty(id))
                                {
                                    MessageBox.Show("请输入条码");
                                    return;
                                }
                                list = dal.GetDataSet(id);
                            }
                            if (cmxGlassSelect.SelectedItem.ToString() != "全部")
                            {
                                int glassNum = cmxGlassSelect.SelectedIndex - 1;
                                if (list.Tables.Count > 0)
                                {
                                    DataRow[] dataRow = list.Tables[0].Select($"GlassNum = {glassNum}", "GlassNum", DataViewRowState.CurrentRows);
                                    temp = new DataSet(); ;
                                    temp.Merge(dataRow);
                                    list = temp;
                                }
                            }
                            if (cmxColNumSelect.SelectedItem.ToString() != "全部")
                            {
                                int colNum = cmxColNumSelect.SelectedIndex - 1;
                                if (list.Tables.Count > 0)
                                {
                                    DataRow[] dataRow = list.Tables[0].Select($"ColumnNum = {colNum}", "ColumnNum", DataViewRowState.CurrentRows);
                                    temp = new DataSet(); ;
                                    temp.Merge(dataRow);
                                    list = temp;
                                }
                            }
                            if (cmxRowNumSelect.SelectedItem.ToString() != "全部")
                            {
                                int rowNum = cmxRowNumSelect.SelectedIndex - 1;
                                if (list.Tables.Count > 0)
                                {
                                    DataRow[] dataRow = list.Tables[0].Select($"RowNum = {rowNum}", "RowNum", DataViewRowState.CurrentRows);
                                    temp = new DataSet(); ;
                                    temp.Merge(dataRow);
                                    list = temp;
                                }
                            }
                            if (list != null && list.Tables.Count > 0)
                            {
                                dgv_detectInfo.DataSource = list.Tables[0];
                            }
                        }
                        else if (rbtNGInfo.Checked)
                        {
                            DataSet list = null;
                            DataSet temp =null;
                            if (cbxByTime.Checked)
                            {
                                //if (!cbxNow.Checked)
                                //{
                                //    endTime = DateTime.Parse(dtpDateEnd.Value.ToString("yyyy-MM-dd") + " " + dtpTimeEnd.Value.ToString("HH:mm:ss"));
                                //}
                                //TimeSpan span = endTime - startTime;
                                //if (span.Days > 5)
                                //{
                                //    MessageBox.Show("查询日期跨度不能大于5天");
                                //    return;
                                //}
                                list = dal3.GetByTime(startTime, endTime);
                            }
                            else if (cbxByID.Checked)
                            {
                                string id = tbxID.Text;
                                if (string.IsNullOrEmpty(id))
                                {
                                    MessageBox.Show("请输入条码");
                                    return;
                                }
                                list = dal3.GetByID(id);
                            }
                            if (cmxGlassSelect.SelectedItem.ToString() != "全部")
                            {
                                int glassNum = cmxGlassSelect.SelectedIndex - 1;
                                if (list.Tables.Count>0)
                                {
                                    DataRow[] dataRow = list.Tables[0].Select($"GlassNum = {glassNum}", "GlassNum", DataViewRowState.CurrentRows);
                                    temp = new DataSet();
                                    temp.Merge(dataRow);
                                    list = temp;
                                }
                            }
                            if (cmxColNumSelect.SelectedItem.ToString() != "全部")
                            {
                                int colNum = cmxColNumSelect.SelectedIndex - 1;
                                if (list.Tables.Count > 0)
                                {
                                    DataRow[] dataRow = list.Tables[0].Select($"ColumnNum = {colNum}", "ColumnNum", DataViewRowState.CurrentRows);
                                    temp = new DataSet();
                                    temp.Merge(dataRow);
                                    list = temp;
                                }
                            }
                            if (cmxRowNumSelect.SelectedItem.ToString() != "全部")
                            {
                                int rowNum = cmxRowNumSelect.SelectedIndex - 1;
                                if (list.Tables.Count > 0)
                                {
                                    DataRow[] dataRow = list.Tables[0].Select($"RowNum = {rowNum}", "RowNum", DataViewRowState.CurrentRows);
                                    temp = new DataSet();
                                    temp.Merge(dataRow);
                                    list = temp;
                                }
                            
                            }
                            if (cmxNgTypeSelect.SelectedItem.ToString() != "全部")
                            {
                                string ngType = cmxNgTypeSelect.SelectedItem.ToString();
                                if (list.Tables.Count > 0)
                                {
                                    DataRow[] dataRow = list.Tables[0].Select($"NGType = '{ngType}'", "NGType", DataViewRowState.CurrentRows);
                                    temp = new DataSet();
                                    temp.Merge(dataRow);
                                    list = temp;
                                }

                            }
                            if (list != null&&list.Tables.Count>0)
                            {
                                dgv_detectInfo.DataSource = list.Tables[0];
                            }
                        }
                    }));
                });

            }
            catch (Exception ex)
            {
                LogManager.GetLogger("Form").Fatal(ex.ToString());
            }
        }

        private void FormQuery_Load(object sender, EventArgs e)
        {
            InitCtrl();
            cbxByTime.Checked = true;
        }
        private void InitCtrl()
        {
            cmxGlassSelect.SelectedIndex = 0;
            cmxNgTypeSelect.SelectedIndex = 0;
            for (int i = -1; i < Param.Panel.CellCountX; i++)
            {
                if (i==-1)
                {
                    cmxColNumSelect.Items.Add("全部");
                }
                else
                {
                    cmxColNumSelect.Items.Add(i);
                }
            }
            for (int i = -1; i < Param.Panel.CellCountY; i++)
            {
                if (i == -1)
                {
                    cmxRowNumSelect.Items.Add("全部");
                }
                else
                {
                    cmxRowNumSelect.Items.Add(i);
                }
            }
            cmxColNumSelect.SelectedIndex = 0;
            cmxRowNumSelect.SelectedIndex = 0;
            cmxSort.SelectedIndex = 0;
        }
    
        private void GetProbeCardInfos()
        {
            try
            {
                var dataSet = new ProbeCardDal().GetAll();
                if (dataSet != null)
                {
                    dgvProbeInfo.DataSource = dataSet.Tables[0];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            GetProbeCardInfos();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectedRowNum = e.RowIndex;
        }

        private void htTabControlMenu1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (htTabControlMenu1.SelectedIndex==2)
            {
                GetProbeCardInfos();
            }
        }

    }
}
