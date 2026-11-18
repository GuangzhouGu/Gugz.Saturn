/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2021 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：TPAssembly.Devices.Plc
// 文件名称    ：PlcUserControl.cs
// =================================
// 创 建 者    ：guguangzhou
// 创建日期    ：2021/05/10 09:00:00 
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
using System.Text.RegularExpressions;

namespace TPAssembly.Devices.Plc
{
    public partial class PlcSignalCtrl : UserControl
    {
        public static event Action<string, bool> SetOutput;
        public PlcSignalCtrl()
        {
            InitializeComponent();
            InitDgv();
        }
        /// <summary>
        ///  初始化信号表格
        /// </summary>
        private void InitDgv()
        {
            dgv_InputInfo.AlternatingRowsDefaultCellStyle.BackColor = dgv_OutputInfo.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
            dgv_InputInfo.RowsDefaultCellStyle.BackColor = dgv_OutputInfo.RowsDefaultCellStyle.BackColor = System.Drawing.Color.White;
            dgv_InputInfo.DefaultCellStyle.Alignment = dgv_OutputInfo.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

        }
        /// <summary>
        /// 信号变化显示
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="input"></param>
        /// <param name="output"></param>
        public void LoadInfo<T1, T2>(T1 input, T2 output)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<T1, T2>(LoadInfo), input, output);
            }
            else
            {
                try
                {
                    dgv_InputInfo.Rows.Clear();
                    string[] inputNames = Enum.GetNames(typeof(T1));
                    for (int i = 0; i < inputNames.Length - 1; i++)
                    {
                        int rowNum = dgv_InputInfo.Rows.Add();
                        dgv_InputInfo.Rows[rowNum].Cells[0].Value = inputNames[rowNum];
                    }
                    dgv_OutputInfo.Rows.Clear();
                    string[] outputNames = Enum.GetNames(typeof(T2));
                    for (int i = 0; i < outputNames.Length - 1; i++)
                    {
                        int rowNum = dgv_OutputInfo.Rows.Add();
                        dgv_OutputInfo.Rows[rowNum].Cells[0].Value = outputNames[rowNum];
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

        }
        /// <summary>
        /// 输出信号测试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgv_OutputInfo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgv_OutputInfo.Columns[e.ColumnIndex].Name == "Output")
                {
                    string outPutName = dgv_OutputInfo.Rows[e.RowIndex].Cells[0].Value.ToString();
                    bool value = Convert.ToBoolean(dgv_OutputInfo.Rows[e.RowIndex].Cells[1].EditedFormattedValue);
                    Task.Factory.StartNew(() => { SetOutput?.Invoke(outPutName, value); });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        /// <summary>
        /// 更新信号变化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputName"></param>
        /// <param name="value"></param>
        public void UpdateInput<T>(T inputName, bool value)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<T, bool>(UpdateInput), inputName, value);
            }
            else
            {
                try
                {
                    if (dgv_InputInfo.Rows.Count > 0)
                    {
                        Regex r = new Regex(inputName.ToString());
                        Match match = null;
                        for (int i = 0; i < dgv_InputInfo.Rows.Count; i++)
                        {
                            match = r.Match(dgv_InputInfo.Rows[i].Cells[0].Value.ToString());//正则表达式寻找相同的值
                            if (match.Success)
                            {
                                dgv_InputInfo.Rows[i].Cells[1].Value = value? CommonLib.Properties.Resources.Green2: CommonLib.Properties.Resources.Gray2;
                                break;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
