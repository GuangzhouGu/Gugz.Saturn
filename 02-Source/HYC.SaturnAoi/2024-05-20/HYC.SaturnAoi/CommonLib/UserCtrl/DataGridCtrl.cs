/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2021 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：CommonLib.Entities
// 文件名称    ：ViewInfo.cs
// =================================
// 创 建 者    ：guguangzhou
// 创建日期    ：2021/11/08 09:00:00 
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
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using CommonLib.Entities;

namespace CommonLib.UserCtrl
{
    public partial class DataGridCtrl : UserControl
    {
        /// <summary>
        /// 文档保存路径
        /// </summary>
        [Description("文档保存路径")]
        public string FileName { get; set; } = "DataGridViewXml.xml";
        /// <summary>
        /// 标题头
        /// </summary>
        [Description("列标题")]
        public string[] ColumnHeaderStr { get; set; }
        /// <summary>
        /// 列类型
        /// </summary>
        [Description("列类型")]
        public CellTypeEnum[] CellTemplate { get; set; }
        /// <summary>
        /// 下拉框选项
        /// </summary>
        [Description("下拉框选项")]
        public string[] ComboxItems { get; set; }
        public DataGridCtrl()
        {
            InitializeComponent();
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.PowderBlue;
            dgv.RowsDefaultCellStyle.BackColor = Color.WhiteSmoke;
            dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }
        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            try
            {
                for (int i = 0; i < ColumnHeaderStr.Length; i++)
                {
                    DataGridViewColumn column = new DataGridViewColumn();
                    column.Name = ColumnHeaderStr[i];
                    column.HeaderText = ColumnHeaderStr[i];
                    DataGridViewCell cell = GetCellTemplate(CellTemplate[i]);
                    column.CellTemplate = cell;
                    dgv.Columns.Add(column);
                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }
        }


       /// <summary>
       /// 生成表格数据类型
       /// </summary>
       /// <param name="type"></param>
       /// <returns></returns>
        private DataGridViewCell GetCellTemplate(CellTypeEnum type)
        {
            try
            {
                DataGridViewCell ret = null;
                switch (type)
                {
                    case CellTypeEnum.TextBox:
                        ret = new DataGridViewTextBoxCell();
                        break;
                    case CellTypeEnum.CheckBox:
                        {
                            DataGridViewCheckBoxCell temp = new DataGridViewCheckBoxCell();
                            temp.TrueValue = true;
                            temp.FalseValue = false;
                            ret = temp;
                        }
                        break;
                    case CellTypeEnum.ComBox:
                        {
                            DataGridViewComboBoxCell temp = new DataGridViewComboBoxCell();
                            temp.Items.AddRange(ComboxItems);
                            ret = temp;
                        }
                        break;
                    default:
                        ret = new DataGridViewTextBoxCell();
                        break;
                }
                return ret;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// 添加行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_add_Click(object sender, EventArgs e)
        {
            try
            {
                int index = dgv.Rows.Add();
                dgv.Rows[index].Cells[0].Value = index;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 删除行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_delete_Click(object sender, EventArgs e)
        {
            try
            {
                int index = dgv.SelectedCells[0].RowIndex;
                dgv.Rows.RemoveAt(index);
                XDocument xDoc = XDocument.Load(FileName);
                XElement xeleRoot = xDoc.Root;
                XElement node = null;
                try
                {
                    XmlHelper.FindElement(xeleRoot, index.ToString(), ref node);
                }
                catch { }
                if (node != null)
                {
                    node.Remove();
                }
                xDoc.Save(FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 清楚表格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            dgv.Rows.Clear();
        }

        /// <summary>
        /// 移动表格
        /// </summary>
        /// <param name="dgv">数据元</param>
        /// <param name="isUp"></param>
        private void DgvMove(DataGridView dgv, bool isUp)
        {
            try
            {
                if (dgv.Rows.Count <= 0)
                {
                    MessageBox.Show("表格为空！");
                    return;
                }
                int rowNum = dgv.SelectedRows[0].Index;
                switch (isUp)
                {
                    case true:
                        {
                            if (rowNum == 0)
                            {
                                MessageBox.Show("选中单元格是第一行，无法上移！");
                                return;
                            }
                            List<string> list = new List<string>();
                            for (int i = 0; i < dgv.Columns.Count; i++)
                            {
                                list.Add(dgv.SelectedRows[0].Cells[i].Value.ToString());
                            }
                            for (int j = 0; j < dgv.Columns.Count; j++)
                            {
                                dgv.Rows[rowNum].Cells[j].Value = dgv.Rows[rowNum - 1].Cells[j].Value;
                                dgv.Rows[rowNum - 1].Cells[j].Value = list[j].ToString();
                            }
                            dgv.Rows[rowNum - 1].Selected = true;
                            dgv.Rows[rowNum].Selected = false;
                            break;
                        }
                    case false:
                        {
                            if (rowNum == dgv.Rows.Count - 1)
                            {
                                MessageBox.Show("选中单元格是最后一行，无法下移！");
                                return;
                            }
                            List<string> list = new List<string>();
                            for (int i = 0; i < dgv.Columns.Count; i++)
                            {
                                list.Add(dgv.SelectedRows[0].Cells[i].Value.ToString());
                            }
                            for (int j = 0; j < dgv.Columns.Count; j++)
                            {
                                dgv.Rows[rowNum].Cells[j].Value = dgv.Rows[rowNum + 1].Cells[j].Value;
                                dgv.Rows[rowNum + 1].Cells[j].Value = list[j].ToString();
                            }
                            dgv.Rows[rowNum + 1].Selected = true;
                            dgv.Rows[rowNum].Selected = false;
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 保存位置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_savePos_Click(object sender, EventArgs e)
        {


        }
        /// <summary>
        /// 向上移动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUp_Click(object sender, EventArgs e)
        {
            DgvMove(this.dgv, true);
        }
        /// <summary>
        /// 向下移动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDown_Click(object sender, EventArgs e)
        {
            DgvMove(this.dgv, false);
        }
        /// <summary>
        /// 表格初始化加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridCtrl_Load(object sender, EventArgs e)
        {
            Init();
        }
        /// <summary>
        ///  报错异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgv_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (dgv.Rows[e.RowIndex].IsNewRow) return;
        }
    }
    /// <summary>
    /// 表格数据类型
    /// </summary>
    public enum CellTypeEnum
    {
        TextBox,
        CheckBox,
        ComBox,
    }

}
