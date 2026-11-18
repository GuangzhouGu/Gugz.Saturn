using HYC.HTPCBase.Extension.Recipe;
using HYC.SaturnDB.DAL;
using HYC.SaturnDB.Model;
using HYC.SaturnMain.CabinetInfos;
using HYC.SaturnMain.Entities;
using HYC.WindowsForms;
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

namespace HYC.SaturnMain.SubForms
{
    public partial class FrmRecipeProbeConfig : Form
    {
        public FrmRecipeProbeConfig()
        {
            InitializeComponent();

        }

        private void FrmRecipeProbeConfig_Load(object sender, EventArgs e)
        {
            InitDgv(dgvProbeList);
            LoadRecipe();
            LoadProbeList();
        }
        private void LoadRecipe()
        {
            var recipeList = RecipeManager.GetRecipes().Select(p => p.RecipeID).ToArray();
            cmxRecipeList.Items.Clear();
            cmxRecipeList.Items.AddRange(recipeList);
            cmxRecipeList.SelectedIndex = 0;
        }
        private void InitDgv(DataGridView dgv)
        {
            dgv.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.White;
            dgv.RowsDefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
            dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            var dgvType = dgv.GetType();
            var properInfo = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            properInfo.SetValue(dgv, true, null);
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
         
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("微软雅黑", 10);
            foreach (var item in dgv.Columns)
            {
                DataGridViewColumn column = item as DataGridViewColumn;
                column.DefaultCellStyle.Font = new Font("微软雅黑", 9);
            }
        }
    
        private void LoadProbeList()
        {
            try
            {
                dgvProbeList.Rows.Clear();
                var probeList = new ProbeCardDal().GetList();
                if (probeList != null&&probeList.Count>0)
                {
                    for (int i = 0; i < probeList.Count(); i++)
                    {
                        var index = dgvProbeList.Rows.Add();
                        dgvProbeList.Rows[index].Cells[0].Value = probeList[index].RecipeName;
                        var recipe = RecipeManager.GetRecipeByName(probeList[index].RecipeName);
                        dgvProbeList.Rows[index].Cells[1].Value = recipe.PPID;
                        dgvProbeList.Rows[index].Cells[2].Value = probeList[index].ProbeCardName;
                        dgvProbeList.Rows[index].Cells[3].Value = probeList[index].RFID;
                        dgvProbeList.Rows[index].Cells[4].Value = probeList[index].BlockCount;
                        dgvProbeList.Rows[index].Cells[5].Value = probeList[index].ContactedCount;
                        dgvProbeList.Rows[index].Cells[6].Value = probeList[index].InCabinetTime;
                    }
                    var list2 = probeList.GroupBy(p => p.ProbeCardName.ToUpper()).Select(p => p.First()).ToList();
                    if (list2 != null)
                    {
                        cmxProbeList.Items.Clear();
                        cmxProbeList.Items.AddRange(list2.Select(p=>p.ProbeCardName).ToArray());
                        cmxProbeList.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cmxRecipeList.SelectedItem!=null)
            {
                try
                {
                    var dal = new ProbeCardDal();
                    var recipeName = cmxRecipeList.SelectedItem.ToString();
                    var list = dal.GetList().Select(p => p.RecipeName).ToList();
                    if (list.Contains(recipeName))
                    {
                        MessageBox.Show($"已添加机种{recipeName}，请重新选择机种");
                        return;
                    }
                    var probecardName = cmxProbeList.SelectedItem.ToString();
                   var probecardModel= ProbeCardHelper.GetProbeCardByProbeName(probecardName);
                    ProbeCardModel model = new ProbeCardModel();
                    model.ProbeCardName = probecardName;
                    model.RecipeName = recipeName;
                    model.RFID = probecardModel[0].RFID;
                    model.BlockCount = probecardModel[0].BlockCount;
                    model.ContactedCount = probecardModel[0].ContactedCount;
                    model.InCabinetTime = probecardModel[0].InCabinetTime;
                    model.Status = probecardModel[0].Status;
                    dal.Insert(model);
                    AddDataToDgv(model);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
        private void AddDataToDgv(ProbeCardModel mode)
        {
            var index = dgvProbeList.Rows.Add();
            var recipe = RecipeManager.GetRecipeByName(mode.RecipeName);
            dgvProbeList.Rows[index].Cells[0].Value = mode.RecipeName;
            dgvProbeList.Rows[index].Cells[1].Value = recipe.PPID;
            dgvProbeList.Rows[index].Cells[2].Value = mode.ProbeCardName;
            dgvProbeList.Rows[index].Cells[3].Value = mode.RFID;
            dgvProbeList.Rows[index].Cells[4].Value = mode.BlockCount;
            dgvProbeList.Rows[index].Cells[5].Value = mode.ContactedCount;
            dgvProbeList.Rows[index].Cells[6].Value = mode.InCabinetTime;
        }

        private void btnRename_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(tbxProbeName.Text) == false)
                {
                    var dal = new ProbeCardDal();
                    string oldProbeName = cmxProbeList.SelectedItem.ToString();
                    string newProbeName = tbxProbeName.Text;
                    var ret = dal.UpdateProbeName(oldProbeName, newProbeName);
                    if (ret > 0)
                    {
                        LoadProbeList();
                    }
                    var list = CabinetInfoHelper.CabinetInfoList.Where(p => p.ProbeName == oldProbeName).ToList();
                    if (list != null)
                    {
                        list.Select(p => p.ProbeName = newProbeName).ToArray();
                        CabinetInfoHelper.SaveCabinetInfo();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
          
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadRecipe();
            LoadProbeList();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
             
                var rowIndex = dgvProbeList.SelectedCells[0].RowIndex;
                var selectRecipeName = dgvProbeList.Rows[rowIndex].Cells[0].Value.ToString();
                new ProbeCardDal().DeleteByRecipeName(selectRecipeName);
                LoadProbeList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
    }
}
