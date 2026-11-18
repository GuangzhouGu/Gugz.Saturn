using HYC.HTPCBase.Extension.Recipe;
using HYC.HTPCBase.Extension.Views;
using HYC.SaturnMain.Params;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HYC.SaturnMain.SubForms
{
    public partial class FrmGlassCrds : ParaViewBase
    {
        public FrmGlassCrds()
        {
            InitializeComponent();
        }
        int rowNum = -1;
        public override bool Save()
        {
            bool ret = false;
            try
            {
                Param.GlassCrdInfo.ColumnCrdList = new List<OneColumnCrd>();
                for (int i = 0; i < Param.Panel.CellCountX; i++)
                {
                    OneColumnCrd oneColumn = new OneColumnCrd();
                    oneColumn.ColumnNum = i;//列号
                    for (int j = 0; j < Param.Panel.CellCountY; j++)
                    {
                        string temp = dgvGlassCrd.Rows[j].Cells[i + 1].Value.ToString();
                        if (!string.IsNullOrEmpty(temp))
                        {
                            if (temp.Contains(','))
                            {
                                OneCellCrd oneRow = new OneCellCrd();
                                double crdX = double.Parse(temp.Split(',')[0]);
                                double crdY = double.Parse(temp.Split(',')[1]);
                                oneRow.RowNum = j;
                                oneRow.X = crdX;
                                oneRow.Y = crdY;
                                oneColumn.CellCrdList.Add(oneRow);
                            }
                            else
                            {
                                MessageBox.Show($"请输入XY坐标，用逗号隔开");
                                return false;
                            }
                        }
                        else
                        {
                            MessageBox.Show($"单元格不能为空");
                            return false;
                        }
                    }
                    Param.GlassCrdInfo.ColumnCrdList.Add(oneColumn);
                }
                ret = RecipeManager.CurRecipe.Para.SavePara(ParaType.GlassMapPara);
             
            }
            catch (Exception ex)
            {
                MessageBox.Show($"保存坐标出错，错误信息:{ex.Message}");
            }
            return ret;
        }

        private void btnCalculateCrd_Click(object sender, EventArgs e)
        {
            CalCoordation();
        }
        private void CalCoordation()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(CalCoordation));
            }
            else
            {
                if (dgvGlassCrd.Rows.Count >= Param.Panel.CellCountY)
                {
                    return;
                }
                double crdX = 0;
                double crdY = 0;
                string[] sortStr = Param.Panel.PanelSort.Split('X');
                int[] sort = sortStr.Select(p => int.Parse(p)).ToArray();
                for (int i = 0; i < Param.Panel.CellCountY; i++)
                {
                    rowNum = dgvGlassCrd.Rows.Add();
                    dgvGlassCrd.Rows[rowNum].Cells[0].Value = i;
                    for (int j = 0; j < Param.Panel.CellCountX; j++)
                    {
                        crdX = Param.Panel.CellEdgeOffsetX + Param.Panel.CellSizeX * (j+1) + Param.Panel.CellIntervalX * j - Param.Panel.GlassSizeX / 2 - Param.Panel.CellSizeX / 2;
                        switch (sort.Length)
                        {
                            case 2:
                                {
                                    if (rowNum < sort[0])
                                    {
                                        crdY = Param.Panel.GlassSizeY / 2 - Param.Panel.CellEdgeOffsetY - Param.Panel.CellSizeY * rowNum - rowNum * Param.Panel.CellIntervalY - Param.Panel.CellSizeY / 2;
                                    }
                                    else if (sort[0] >= 9)
                                    {
                                        crdY = Param.Panel.GlassSizeY / 2 - Param.Panel.CellEdgeOffsetY - Param.Panel.CellSizeY * rowNum - (rowNum - 1) * Param.Panel.CellIntervalY - Param.Panel.CellIntervalY2 - Param.Panel.CellSizeY / 2;
                                    }

                                    break;
                                }
                          
                            case 3:
                                {
                                    if (rowNum < sort[0])
                                    {
                                        crdY = Param.Panel.GlassSizeY / 2 - Param.Panel.CellEdgeOffsetY - Param.Panel.CellSizeY * rowNum - rowNum * Param.Panel.CellIntervalY - Param.Panel.CellSizeY / 2;
                                    }
                                    else if (rowNum >= sort[0] && rowNum < sort[0]+ sort[1])
                                    {
                                        crdY = Param.Panel.GlassSizeY / 2 - Param.Panel.CellEdgeOffsetY - Param.Panel.CellSizeY * rowNum - (rowNum - 1) * Param.Panel.CellIntervalY - Param.Panel.CellIntervalY2 - Param.Panel.CellSizeY / 2;
                                    }
                                    else
                                    {
                                        crdY = Param.Panel.GlassSizeY / 2 - Param.Panel.CellEdgeOffsetY - Param.Panel.CellSizeY * rowNum - (rowNum - 2) * Param.Panel.CellIntervalY - Param.Panel.CellIntervalY2 * 2 - Param.Panel.CellSizeY / 2;
                                    }
                                    break;
                                }
                            case 4:
                                {
                                    break;
                                }
                        }
                        dgvGlassCrd.Rows[rowNum].Cells[j + 1].Value = $"{Math.Round(crdX, 4)},{Math.Round(crdY, 4)}";
                    }
                }
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            dgvGlassCrd.Rows.Clear();
        }

        private void FrmGlassMap_Load(object sender, EventArgs e)
        {
            InitCtrl();
           
        }
        private void InitCtrl()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(InitCtrl));
            }
            else
            {
                try
                {
                    dgvGlassCrd.AlternatingRowsDefaultCellStyle.BackColor = Color.PowderBlue;
                    dgvGlassCrd.RowsDefaultCellStyle.BackColor = Color.WhiteSmoke;
                    dgvGlassCrd.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvGlassCrd.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle { };
            
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        
        }

        private void dgvGlassCrd_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                CalCoordation();
            }
        }
    }
}
