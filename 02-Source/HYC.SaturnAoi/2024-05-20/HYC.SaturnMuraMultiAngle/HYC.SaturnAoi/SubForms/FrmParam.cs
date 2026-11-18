using CommonLib.Para;
using HYC.HTPCBase.Extension.Recipe;
using HYC.HTPCBase.Extension.Views;
using HYC.StandardAoi.UI.View;
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
    public partial class FrmParam : ViewBaseEx
    {
        public FrmParam()
        {
            InitializeComponent();
            RecipeManager.OnRecipeChange += RecipeManager_OnRecipeChange;
        }

        private void RecipeManager_OnRecipeChange(RecipeChangeType arg1, Recipe arg2)
        {
            //InitTreeView();
        }

        string SelectedObjectNode = string.Empty;
        ParaViewBase LastView = null;
        bool IsFirstInit = true;
        public void InitTreeView()
        {
            try
            {
                treeView1.SuspendLayout();
                treeView1.Nodes.Clear();
                List<IPara> paras = RecipeManager.CurRecipe.Para.GetParas();
                for (int i = 0; i < paras.Count; i++)
                {
                    if (paras[i] != null)
                    {
                        if (paras[i].IsShow)
                        {
                            TreeNode tn = treeView1.Nodes.Add(paras[i].ParaName);
                            tn.Name = paras[i].GetType().Name;
                        }
                    }
                }
                treeView1.ExpandAll();
                treeView1.ResumeLayout();
                if (IsFirstInit)
                {
                    propertyGrid1.SelectedObject = null;
                    IsFirstInit = false;
                }
                else
                {
                    propertyGrid1.SelectedObject = SelectedObjectNode;
                    ShowPara(SelectedObjectNode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"InitTreeView异常，异常信息：{ex.ToString()}");
            }
        }
        /// <summary>
        /// 保存所有参数设值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                bool reuslt = SavePara();
                if (reuslt)
                {
                    MessageBox.Show("参数保存成功");
                }
                else
                {
                    MessageBox.Show("参数保存失败");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"参数保存异常，异常信息：{ex.ToString()}");
            }
        }
        private void ShowPara(string node)
        {
            try
            {
                IPara para = RecipeManager.CurRecipe.Para.Get(node);
                if (para.ParaViewBase != null)
                {
                    if (LastView != null && LastView != para.ParaViewBase)
                    {
                        LastView.Visible = false;
                    }
                    Invoke(new Action(() => {
                        btn_save.Visible = !para.ParaViewBase.IsHaveSaveBtn;
                        btn_load.Visible = !para.ParaViewBase.IsHaveSaveBtn;
                        propertyGrid1.Visible = false;
                        para.ParaViewBase.TopLevel = false;
                        para.ParaViewBase.Parent = panel1;
                        para.ParaViewBase.Dock = DockStyle.Fill;
                        para.ParaViewBase.Invoke(new Action(() => { para.ParaViewBase.Show(); }));
                        LastView = para.ParaViewBase;
                    }));
                  
                }
                else
                {
                    panel1.Controls.Remove(LastView);
                    propertyGrid1.SelectedObject = para;
                    btn_save.Visible = true;
                    btn_load.Visible = true;
                    propertyGrid1.Visible = true;
                    LastView = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool SavePara()
        {
            bool ret = false;
            var para = RecipeManager.CurRecipe.Para.Get(SelectedObjectNode);
            if (para.ParaViewBase != null && para.ParaViewBase.IsHaveSaveBtn == false)
            {
                ret = para.ParaViewBase.Save();
            }
            else
            {
                ret = RecipeManager.CurRecipe.Para.SavePara(SelectedObjectNode);
            }
            return ret;
        }
        private void btn_load_Click(object sender, EventArgs e)
        {
            RecipeManager.CurRecipe.Load();
            propertyGrid1.SelectedObject = RecipeManager.CurRecipe.Para.Get(SelectedObjectNode);
        }

        private void FormSetting_Load(object sender, EventArgs e)
        {
            InitTreeView();
        }
        /// <summary>
        /// 选择参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            SelectedObjectNode = e.Node.Name;
            ShowPara(SelectedObjectNode);
        }
    }
}
