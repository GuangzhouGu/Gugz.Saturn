using HYC.HTPCBase.Extension.Recipe;
using HYC.SaturnDB.DAL;
using HYC.SaturnDB.Model;
using HYC.SaturnMain.CabinetInfos;
using HYC.SaturnMain.Devices.Rfid;
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

namespace HYC.SaturnMain.SubForms
{
    public partial class FrmProbeCardEdit : FormPopBase
    {
        public FrmProbeCardEdit()
        {
            InitializeComponent();
            this.Title = "ProbeCard编辑";
        }
        int RowNum = -1;
        public FrmProbeCardEdit(int rowNum):this()
        {
            RowNum = rowNum;
        }
        public CabinetRowInfo Info = null;
        public int BlockCount { get; set; }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                Info = null;
                if (cmxRecipeList.SelectedItem==null)
                {
                    MessageBox.Show($"请选择RecipeName");
                    return;
                }
                if (string.IsNullOrEmpty(txtProbeHeadCount.Text))
                {
                    MessageBox.Show($"请输入压头数量");
                    return;
                }
                string probeName = tbxProbeName.Text;
                if (string.IsNullOrEmpty(probeName))
                {
                    MessageBox.Show($"Probe名称为空，请输入Probe名称");
                    return;
                }
                string recipeName = cmxRecipeList.SelectedItem.ToString();
                if (CabinetInfoHelper.CheckProbeNameIsUsed(probeName))
                {
                    MessageBox.Show($"当前Probe名称已被使用，请重新输入Probe名称");
                    return;
                }
                string rfid = tbxRFID.Text;
                var rfidRead = RfidHelper.Instance.Read();
                if (string.IsNullOrEmpty(rfid))
                {
                    if (string.IsNullOrEmpty(rfidRead))
                    {
                        MessageBox.Show($"Probe的RFID标签为空，请输入RFID");
                        return;
                    }
                }
                if (CabinetInfoHelper.CheckCabinetIsUsed(RowNum) == false)
                {
                    Info = CabinetInfoHelper.GetCabinetInfo(RowNum);
                    Info.ProbeName = probeName;
                    Info.RecipeName = recipeName;
                    Info.CabinetRowNum = RowNum;
                    BlockCount = int.Parse(txtProbeHeadCount.Text);
                    if (string.IsNullOrEmpty(rfidRead))
                    {
                        RfidHelper.Instance.Write(rfid);
                        Info.RFID = rfid;
                    }
                    else
                    {
                        Info.RFID = rfidRead;
                    }
                    var ret=new ProbeCardDal().UpdateRowNum(probeName, RowNum + 1);
                    if (ret<1)
                    {
                        var model = new ProbeCardModel { ProbeCardName = Info.ProbeName, RecipeName = Info.RecipeName, CabinetRowNum = Info.CabinetRowNum + 1, RFID = Info.RFID, Status = ProbeCardSts.InCabinet.ToString(), InCabinetTime = DateTime.Now, BlockCount = BlockCount };
                        new ProbeCardDal().Insert(model);
                    }
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show($"料仓第{RowNum + 1}行已经被使用，请重新选择");
                    this.DialogResult = DialogResult.Cancel;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void FrmProbeCardEdit_Load(object sender, EventArgs e)
        {
            try
            {
                var recipeList = RecipeManager.GetRecipes().Select(p => p.RecipeID).ToArray();
                cmxRecipeList.Items.Clear();
                cmxRecipeList.Items.AddRange(recipeList);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
