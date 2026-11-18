using HYC.SaturnDB.DAL;
using HYC.SaturnDB.Model;
using HYC.HTPCBase.Extension.Entities;
using HYC.SaturnMain.CabinetInfos;
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
using HYC.SaturnMain.Entities;
using HYC.HTPCBase.Extension.Recipe;

namespace HYC.SaturnMain.SubForms
{
    public partial class FrmUpdateProbeSts : FormPopBase
    {
        public FrmUpdateProbeSts()
        {
            InitializeComponent();
            this.Title = "手动更新Probe状态";
        }
        Action<CabinetRowInfo, bool> UpdateAction;
        CabinetRowInfo CabinetRowInfo = null;
        /// <summary>
        /// 操作类型，更新，插入，删除
        /// </summary>
        public string OpType { get; set; }
        public FrmUpdateProbeSts(Action<CabinetRowInfo,bool> action, CabinetRowInfo info) :this()
        {
            UpdateAction = action;
            CabinetRowInfo = info;
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string sts = cmxProbeSts.SelectedItem.ToString();
                var model = new ProbeCardDal().GetList(CabinetRowInfo.RecipeName)[0];
                switch (sts)
                {
                    case "InUvw":
                        {
                            model.LoadInUvwTime = DateTime.Now;
                            break;
                        }
                    case "InCabinet":
                        {
                            model.InCabinetTime = DateTime.Now;
                            break;
                        }
                    case "OutEqp":
                        {
                            model.OutCabinetTime = DateTime.Now;
                            break;
                        }
                }
                model.Status = sts;
                new ProbeCardDal().UpdateStatus(EnumHelper.GetEnumValue<ProbeCardSts>(sts), CabinetRowInfo.RecipeName);
                UpdateAction?.Invoke(CabinetRowInfo, false);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void FrmUpdateProbeSts_Load(object sender, EventArgs e)
        {
            try
            {
                switch (OpType)
                {
                    case "Update":
                        {
                            this.tabControl1.SelectedIndex = 0;
                            break;
                        }
                    case "Insert":
                        {
                            this.tabControl1.SelectedIndex = 1;

                            var recipeList = RecipeManager.GetRecipes().Select(p => p.RecipeID).ToArray();
                            cmxRecipeList.Items.Clear();
                            cmxRecipeList.Items.AddRange(recipeList);
                            break;
                        }
                  
                    default:
                        break;
                }             
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                var sts = cmxProbeSts2.SelectedItem.ToString();
              
                if (string.IsNullOrEmpty(tbxProbeName.Text))
                {
                    MessageBox.Show($"Probe名称为空，请输入Probe名称");
                    return;
                }
                var recipeName = cmxRecipeList.SelectedItem.ToString();
                var probeName = tbxProbeName.Text;
                if (CabinetInfoHelper.CheckProbeNameIsUsed(probeName))
                {
                    MessageBox.Show($"当前Probe名称已被使用，请重新输入Probe名称");
                    return;
                }
                if (CabinetInfoHelper.CheckRecipeNameIsUsed(recipeName))
                {
                    MessageBox.Show($"当前Recipe名称已被使用，请重新输入Recipe名称");
                    return;
                }
                var cabRowNum = CabinetRowInfo.CabinetRowNum;
                var model = new ProbeCardModel { RecipeName = recipeName,ProbeCardName=probeName, CabinetRowNum = cabRowNum + 1, RFID = tbxRFID.Text, Status = sts, InCabinetTime = DateTime.Now.AddMinutes(-30) ,LoadInUvwTime= DateTime.Now };
                new ProbeCardDal().Insert(model);
                CabinetRowInfo.ProbeName = tbxProbeName.Text;
                CabinetRowInfo.IsUsed = true;
                CabinetRowInfo.RecipeName = cmxRecipeList.SelectedItem.ToString();
                CabinetRowInfo.RFID = tbxRFID.Text;
                CabinetInfoHelper.SaveCabinetInfo();
                UpdateAction?.Invoke(CabinetRowInfo, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

     
    }
}
