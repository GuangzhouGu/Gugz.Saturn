
using CommonLib.Recipe;

using DBQuery.DAL;
using DbQuery.Model;
using HYC.WindowsForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CommonLib.Views
{
    public partial class FormRecipe : FormPopBase
    {
        public FormRecipe()
        {
            InitializeComponent();
            Title = "机种管理";
        }
        private string NewRecipeName = string.Empty;
        private void btnAdd_Click(object sender, EventArgs e)
        {
            FormRecipeAdd form = new FormRecipeAdd();
            if(form.ShowDialog()==DialogResult.OK)
            {
                NewRecipeName = form.StrRecipeID;
            }
            RecipeInfoDal dal = new RecipeInfoDal();
            List<RecipeInfoModel>list= dal.GetAll();
            if( list.Where(p=>p.RecipeID==NewRecipeName).ToArray().Length>0)
            {
                MessageBox.Show("该Recipe已经存在，请重新输入！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            RecipeInfoModel model = new RecipeInfoModel { RecipeID = NewRecipeName, PPID = "Test", Remark = "", UpdateTime = DateTime.Now };
            dal.Insert(model);
            RecipeManager.LoadRecipeList();
            this.lstRecipes.Items.Clear();
            this.lstRecipes.Items.AddRange(RecipeManager.GetRecipes().Select(p=>p.RecipeID).ToArray());
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.lstRecipes.SelectedIndex < 0)
            {
                MessageBox.Show("请选择需要删除的机种！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            RecipeInfoDal dal = new RecipeInfoDal();
            string name = this.lstRecipes.SelectedItem.ToString();
            RecipeInfoModel model = new RecipeInfoModel { RecipeID = name, PPID = "Test", Remark = "", UpdateTime = DateTime.Now };
            dal.Delete(model);
            this.lstRecipes.Items.RemoveAt(this.lstRecipes.SelectedIndex);
            RecipeManager.DeleteByName(name);
        }

        private void FormRecipe_Load(object sender, EventArgs e)
        {
            this.lstRecipes.Items.Clear();
            this.lstRecipes.Items.AddRange(RecipeManager.GetRecipes().Select(p => p.RecipeID).ToArray());
        }

        private void btnRename_Click(object sender, EventArgs e)
        {
            if (this.lstRecipes.SelectedIndex < 0)
            {
                MessageBox.Show("请选择需要重命名的机种！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string oldName = lstRecipes.SelectedItem.ToString();
            FormRecipeAdd form = new FormRecipeAdd();
            form.OldRecipeID = oldName;
            if (form.ShowDialog() == DialogResult.OK)
            {
                NewRecipeName = form.StrRecipeID;
                RenameRecipe(oldName, NewRecipeName);
            }
        }
        private void RenameRecipe(string oldName, string newName)
        {
            RecipeInfoDal dal = new RecipeInfoDal();
            dal.UpdateRecipeID(oldName, newName);
            RecipeManager.LoadRecipeList();
            this.lstRecipes.Items.Clear();
            this.lstRecipes.Items.AddRange(RecipeManager.GetRecipes().Select(p => p.RecipeID).ToArray());
        }
    }
}
