using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CommonLib.Entities
{
    public class DataTableHelper
    {
        /// <summary>
        /// 数据导出至CSV
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="strFilePath"></param>
        public static bool DataTableToCsv(DataTable dt, string strFilePath)
        {
            using (StreamWriter sw = new StreamWriter(strFilePath, false, Encoding.UTF8))
            {
                string strHeader = string.Empty;
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    strHeader += dt.Columns[i].Caption + ",";
                }
                sw.WriteLine(strHeader.Substring(0, strHeader.Length - 1));

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string strContent = string.Empty;
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        string tmp = dt.Rows[i][j].ToString().TrimEnd();
                        if (tmp.Contains(","))
                            strContent += "\"'" + tmp + "\"',";
                        else
                            strContent += tmp + ",";
                    }
                    sw.WriteLine(strContent.Substring(0, strContent.Length - 1));
                }
                return true;
            }
        }
        /// <summary>
        /// 将 DataGridView 数据源转换成DataTable
        /// </summary>
        /// <param name="dgvCount"></param>
        /// <returns></returns>
        public static DataTable GetDataSource(DataGridView dgvCount)
        {
            DataTable dt = new DataTable();
            for (int colu = 0; colu < dgvCount.Columns.Count; colu++)
            {
                dt.Columns.Add(dgvCount.Columns[colu].Name);
            }
            dt.TableName = DateTime.Now.ToString("yyyyMMdd");

            for (int row = 0; row < dgvCount.Rows.Count; row++)
            {
                DataRow dr = dt.NewRow();

                for (int colu = 0; colu < dgvCount.Columns.Count; colu++)
                {
                    string cell1 = dgvCount.Rows[row].Cells[0].Value?.ToString();
                    string cell2 = dgvCount.Rows[row].Cells[1].Value?.ToString();
                    //不保存空数据
                    if (!string.IsNullOrEmpty(cell2))
                    {
                        object obj = dgvCount.Rows[row].Cells[colu].Value;
                        string value = obj == null ? string.Empty : obj.ToString();
                        dr[colu] = value;
                    }
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }
    }


}
