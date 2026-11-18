using CommonLib.Recipe;
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
    public partial class FormRecipeSelect : FormPopBase
    {
        public event Action<string> OnUpdateRecipe;
        public FormRecipeSelect()
        {
            InitializeComponent();
            Title = "机种选择";
        }

        private void FormRecipeSelect_Load(object sender, EventArgs e)
        {
            this.lstRecipes.Items.Clear();
            string[] temp = RecipeManager.GetRecipes().Select(p => p.RecipeID).ToArray();
            this.lstRecipes.Items.AddRange(temp);
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (this.lstRecipes.SelectedIndex < 0) return;
            string temp = this.lstRecipes.SelectedItem.ToString();
           var recipe = RecipeManager.GetRecipeByName(temp);
            bool ret = recipe.Load();
            if(ret)
            {
                RecipeManager.CurRecipe = recipe;
                OnUpdateRecipe?.Invoke(RecipeManager.CurRecipe.RecipeID);
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("选定机种参数加载失败，请确认Recipe文件夹中是否存在该机种文件夹");
            }
        }

        private void btnCacel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnRecipeManage_Click(object sender, EventArgs e)
        {
            FormRecipe FormRecipe = new FormRecipe();
            FormRecipe.ShowDialog();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            this.lstRecipes.Items.Clear();
            string[] temp = RecipeManager.GetRecipes().Select(p => p.RecipeID).ToArray();
            this.lstRecipes.Items.AddRange(temp);
        }
    }
}
