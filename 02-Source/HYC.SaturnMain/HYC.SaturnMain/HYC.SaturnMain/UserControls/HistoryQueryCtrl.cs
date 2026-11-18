/*----------------------------------------------------------------
// Copyright © 2023 HYC. All Rights Reserved
// 版权所有。
//
// =================================
// CLR版本     ：4.0.30319.42000
// 命名空间    ：HistoryQuery
// 文件名称    ：UcHistory.cs
// =================================
// 创 建 者    ：gugz
// 创建日期    ：2023/11/10 17:52:17 
// 功能描述    ：
// 使用说明    ：
//
//
// 创建标识：
//
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using HYC.SaturnDB.DAL;
using System;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace HYC.SaturnMain.UserControls
{
    /// <summary>
    /// 历史查询自定义控件
    /// </summary>
    public partial class HistoryQueryCtrl : UserControl
    {
        /// <summary>
        /// 控件静态示例
        /// </summary>
        public static HistoryQueryCtrl Instance;

        /// <summary>
        /// 查询线程
        /// </summary>
        private Thread searchThread;
        /// <summary>
        /// 查询类型
        /// </summary>
        private string QueryType ="Total";
        /// <summary>
        /// 构造方法
        /// </summary>
        public HistoryQueryCtrl()
        {
            InitializeComponent();
            // 设置控件的默认值
            this.dtPickerTime1.Value = DateTime.Now.AddMinutes(-60);
            // 设置表格属性
            this.dataGridViewUnit1.CountPerPage = 50;
            this.dataGridViewUnit1.View.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewUnit1.View.RowsDefaultCellStyle.BackColor = Color.FromArgb(0xF2, 0xF2, 0xF2);
            this.dataGridViewUnit1.View.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(0xE7, 0xE7, 0xE7);
            this.dataGridViewUnit1.View.AllowUserToAddRows = false;
            this.dataGridViewUnit1.View.CellMouseClick += View_CellMouseClick;
            cmxCellIDs.Items.AddRange(Global.CellID);
        }

        /// <summary>
        /// 更新视图
        /// </summary>
        public void UpdateView()
        {
            if (this.ckbNow1.Checked)
            {
                this.dtPickerDate2.Value = DateTime.Now;
                this.dtPickerTime2.Value = DateTime.Now;
            }
        }

        /// <summary>
        /// 处理表格的单元格点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex < 0) return;

            var dgv = this.dataGridViewUnit1.View;
            if (dgv.Columns[e.ColumnIndex].HeaderText == "BmpSavePath")
            {
                if (!string.IsNullOrEmpty(dgv.SelectedCells[0].Value.ToString()))
                {
                    Point p1 = MousePosition;
                    Point p2 = PointToScreen(new Point());

                    ContextMenu menu = new ContextMenu();
                    menu.MenuItems.Add("查看图片");
                    //menu.MenuItems[0].Click += Menu_Click;
                    menu.Show(this, new Point(p1.X - p2.X, p1.Y - p2.Y));
                }
            }
        }

        /// <summary>
        /// 查询按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            searchThread = new Thread(Query);
            searchThread.Start();
        }
        /// <summary>
        /// 查询线程
        /// </summary>
        private void Query()
        {
            try
            {
                // 获取时间范围
                string date1 = this.dtPickerDate1.Value.ToString("yyyy-MM-dd ");
                string time1 = this.dtPickerTime1.Value.ToString("HH:mm:ss");
                string date2 = this.dtPickerDate2.Value.ToString("yyyy-MM-dd ");
                string time2 = this.dtPickerTime2.Value.ToString("HH:mm:ss");
                string datetime1 = date1 + time1;
                string datetime2 = date2 + time2;
                if (this.ckbNow1.Checked)
                {
                    datetime2 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }
                QueryType = cmxQueryType.SelectedItem.ToString();
                // 根据查询类型，执行不同的查询方法
                switch (QueryType)
                {
                    case "Total":
                        QueryTotalInfo(datetime1, datetime2);
                        break;
                    case "NGDetail":
                        QueryDefectiveInfo(datetime1, datetime2);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        /// <summary>
        /// 查询TotalInfo表
        /// </summary>
        /// <param name="datetime1"></param>
        /// <param name="datetime2"></param>
        private void QueryTotalInfo(string datetime1, string datetime2)
        {
            try
            {
                // 获取其他参数
                string glassId = string.Empty;
                string cellId = string.Empty;
                string result = string.Empty;
                string detectType = string.Empty;

                this.Invoke(new Action(() =>
                {
                    glassId = this.txtPanelID.Text;
                    result = this.cboResult.Text;

                    detectType = cmxDetectType.SelectedItem.ToString();
                }));
                DataSet ds = null;
                switch (detectType)
                {
                    case "Total":
                        {
                            TotalInfoDal dal = new TotalInfoDal();
                             ds = dal.GetDataSet(glassId, datetime1, datetime2, result);
                            break;
                        }
                    case "Aoi":
                    case "Mura":
                    case "SquintMura":
                        {
                            AoiDetectInfoDal dal = new AoiDetectInfoDal();
                            ds = dal.GetDataSet(glassId, detectType, datetime1, datetime2, result);
                            break;
                        }
                    case "MultiAngle":
                        {
                            MultiAngleDetectInfoDal dal = new MultiAngleDetectInfoDal();
                            ds = dal.GetDataSet(glassId, datetime1, datetime2, result);
                            break;
                        }
                    default:
                        break;
                }
              

                // 设置单元格的格式
                this.Invoke(new Action(() =>
                {
                    this.dataGridViewUnit1.DataSource = ds.Tables[0];
                    //this.dataGridViewUnit1.Columns["PanelID"].FillWeight = 150;
                    this.dataGridViewUnit1.Columns["EQPInTime"].DefaultCellStyle.Format = "yyyy/MM/dd HH:mm:ss";

                    for (int i = 0; i < dataGridViewUnit1.Columns.Count; i++)
                    {
                        this.dataGridViewUnit1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }
                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        /// <summary>
        /// 查询不良明细
        /// </summary>
        /// <param name="datetime1"></param>
        /// <param name="datetime2"></param>
        private void QueryDefectiveInfo(string datetime1, string datetime2)
        {
            try
            {
                // 获取其他参数
                string glassId = string.Empty;
                string cellId = string.Empty;
                string detectType = string.Empty;
                if (string.IsNullOrEmpty(this.txtPanelID.Text))
                {
                    MessageBox.Show("条码不能为空");
                    return;
                }
                this.Invoke(new Action(() =>
                {
                    cellId = cmxCellIDs.SelectedItem.ToString();
                    glassId = this.txtPanelID.Text;
                    detectType = cmxDetectType.SelectedItem.ToString();
                }));
                DataSet ds =null;
                // 查询
                switch (detectType)
                {
                    case "Aoi":
                    case "Mura":
                        {
                            AoiNGInfoDal dal = new AoiNGInfoDal();
                            dal.GetDataSet(glassId, cellId,detectType);
                            break;
                        }
                    case "MultiAngle":
                        {
                            break;
                        }
                    default:
                        break;
                }
             
                // 设置单元格的格式
                this.Invoke(new Action(() =>
                {
                    this.dataGridViewUnit1.DataSource = ds.Tables[0];
                    this.dataGridViewUnit1.Columns["EQPInTime"].DefaultCellStyle.Format = "yyyy/MM/dd HH:mm:ss";

                    for (int i = 0; i < dataGridViewUnit1.Columns.Count; i++)
                    {
                        this.dataGridViewUnit1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }
                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 切换查询类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboSearchType_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

   
        /// <summary>
        /// 结束时间选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ckbNow1_ValueChanged(object sender, bool value)
        {
            if (this.ckbNow1.Checked)
            {
                this.dtPickerDate2.Enabled = false;
                this.dtPickerTime2.Enabled = false;
            }
            else
            {
                this.dtPickerDate2.Enabled = true;
                this.dtPickerTime2.Enabled = true;
            }
        }

        private void HistoryQueryCtrl_Load(object sender, EventArgs e)
        {
            cboResult.SelectedIndex = 0;
            cmxDetectType.SelectedIndex = 0;
            cmxCellIDs.SelectedIndex = 0;
            cmxQueryType.SelectedIndex = 0;
        }
    }
}
