/*----------------------------------------------------------------
// Copyright © 2021 HYC. All Rights Reserved
// 版权所有。
//
// =================================
// CLR版本     ：4.0.30319.42000
// 命名空间    ：HistoryQuery
// 文件名称    ：UcHistory.cs
// =================================
// 创 建 者    ：lihang
// 创建日期    ：2021/4/20 17:52:17 
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

using CommonDB.Dal;
using CommonModule.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace CommonModule.Controls
{
    /// <summary>
    /// 历史查询自定义控件
    /// </summary>
    public partial class YieldQueryCtrl : CtrlBase
    {
        /// <summary>
        /// 控件静态示例
        /// </summary>
        public static YieldQueryCtrl Instance;

        /// <summary>
        /// 查询线程
        /// </summary>
        private Thread SearchThread;
        /// <summary>
        /// 构造方法
        /// </summary>
        public YieldQueryCtrl()
        {
            InitializeComponent();
            // 设置控件的默认值
            this.cboResult.SelectedIndex = 0;
            this.cboStation.SelectedIndex = 0;
            this.cboUnit.SelectedIndex = 0;
            this.dtPickerTime1.Value = DateTime.Now.AddMinutes(-60);
          
            // 设置表格属性
            this.dataGridViewUnit1.CountPerPage = 50;
            this.dataGridViewUnit1.View.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewUnit1.View.RowsDefaultCellStyle.BackColor = Color.FromArgb(0xF2, 0xF2, 0xF2);
            this.dataGridViewUnit1.View.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(0xE7, 0xE7, 0xE7);
            this.dataGridViewUnit1.View.AllowUserToAddRows = false;
           
        }

        /// <summary>
        /// 查询按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            dataGridViewUnit1.DataSource = null;
            SearchThread = new Thread(Query);
            SearchThread.Start();
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
                QueryTotalInfo(datetime1, datetime2);
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
                string panelid = string.Empty;
                string result = string.Empty;
                string stationNo = string.Empty, unitNo= string.Empty;

                this.Invoke(new Action(() =>
                {
                    tbxOKRate.Text = "0%";
                    panelid = this.txtPanelID.Text;
                    result = this.cboResult.Text;
                    stationNo = cboStation.Text;
                    unitNo = cboUnit.Text;
                }));
                // 查询
                DetectResultDal dal = new DetectResultDal();
                var list = dal.GetList(panelid, datetime1, datetime2,result, stationNo, unitNo);
              
                this.Invoke(new Action(() =>
                {
                    if (list.Count>0)
                    {
                        int okCount = list.Where(p => p.Result == "Pass").Count();
                        tbxOKRate.Text = Math.Round(100.0*okCount / list.Count,2).ToString()+"%";
                       this.dataGridViewUnit1.DataSource = ListToDataSet(list).Tables[0];
                        this.dataGridViewUnit1.Columns["StartTime"].DefaultCellStyle.Format = "yyyy/MM/dd HH:mm:ss";
                        for (int i = 0; i < dataGridViewUnit1.Columns.Count; i++)
                        {
                            this.dataGridViewUnit1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        }
                    }
                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        /// <summary>
        /// List转DataSet
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        private  DataSet ListToDataSet<T>(List<T> list)
        {
            if (list.Count == 0) return new DataSet();
            var properties = list[0].GetType().GetProperties();
            var cols = properties.Select(p => new DataColumn(p.Name));
            var dt = new DataTable();
            dt.Columns.AddRange(cols.ToArray());
            list.ForEach(x => dt.Rows.Add(properties.Select(p => p.GetValue(x)).ToArray()));
            return new DataSet { Tables = { dt } };
        }
      
    }
}
