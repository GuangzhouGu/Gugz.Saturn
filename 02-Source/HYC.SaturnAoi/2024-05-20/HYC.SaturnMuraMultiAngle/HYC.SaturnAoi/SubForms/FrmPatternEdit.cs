using HYC.HTPCBase.Extension.Recipe;
using HYC.HTPCBase.Extension.Views;
using HYC.SaturnAoi.DetectFlow.MultiAngleDetect;
using HYC.SaturnAoi.Params;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HYC.SaturnAoi.SubForms
{
    public partial class FrmPatternEdit : ParaViewBase
    {
        public static FrmPatternEdit Instance = null;
        public FrmPatternEdit()
        {
            InitializeComponent();
            IsHaveSaveBtn = false;
        }
        static FrmPatternEdit()
        {
            Instance = new FrmPatternEdit();
        }
        private void FrmMultiAngleDetectPatternEdit_Load(object sender, EventArgs e)
        {
            SetDgvFormat(dgvPatternEdit);
            LoadInfo();
        }
        private void  LoadInfo()
        {
            Invoke(new Action(()=> {
                if (Param.MultiAngleDetectPattern.PatternList != null && Param.MultiAngleDetectPattern.PatternList.Count > 0)
                {
                    for (int i = 0; i < Param.MultiAngleDetectPattern.PatternList.Count; i++)
                    {
                        int rowIndex = dgvPatternEdit.Rows.Add();
                        dgvPatternEdit.Rows[rowIndex].Cells[0].Value = Param.MultiAngleDetectPattern.PatternList[i].PatternNum;
                        dgvPatternEdit.Rows[rowIndex].Cells[1].Value = Param.MultiAngleDetectPattern.PatternList[i].PatternName;
                        dgvPatternEdit.Rows[rowIndex].Cells[2].Value = Param.MultiAngleDetectPattern.PatternList[i].WaitTime;
                        dgvPatternEdit.Rows[rowIndex].Cells[3].Value = Param.MultiAngleDetectPattern.PatternList[i].IsUse;
                        dgvPatternEdit.Rows[rowIndex].Cells[4].Value = Param.MultiAngleDetectPattern.PatternList[i].ControlType;
                    }
                }
            }));
           
        }
        private void SetDgvFormat(DataGridView dgv)
        {
            Invoke(new Action(()=> {
                dgv.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.PowderBlue;
                dgv.RowsDefaultCellStyle.BackColor = System.Drawing.Color.WhiteSmoke;
                dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }));

        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            int index = dgvPatternEdit.Rows.Add();
            dgvPatternEdit.Rows[index].Cells[2].Value = 200;
            dgvPatternEdit.Rows[index].Cells[3].Value = true;
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (dgvPatternEdit.Rows.Count>0)
            {
                int index = dgvPatternEdit.SelectedCells[0].RowIndex;
                dgvPatternEdit.Rows.RemoveAt(index);
            }
        }
        public override bool Save()
        {
            try
            {
                Param.MultiAngleDetectPattern.PatternList = new List<PatternInfo>();
                for (int i = 0; i < dgvPatternEdit.Rows.Count; i++)
                {
                    PatternInfo info = new PatternInfo();
                    info.PatternNum = int.Parse(dgvPatternEdit.Rows[i].Cells[0].Value.ToString());
                    info.PatternName = dgvPatternEdit.Rows[i].Cells[1].Value.ToString();
                    info.WaitTime = int.Parse(dgvPatternEdit.Rows[i].Cells[2].Value.ToString());
                    info.IsUse = bool.Parse(dgvPatternEdit.Rows[i].Cells[3].Value.ToString());
                    info.ControlType = dgvPatternEdit.Rows[i].Cells[4].Value.ToString();
                    Param.MultiAngleDetectPattern.PatternList.Add(info);
                }
                return RecipeManager.CurRecipe.Para.SavePara("MultiAngleDetectPattern");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"保存画面信息异常，异常信息：{ex}");
                return false;
            }
        }
    }
}
