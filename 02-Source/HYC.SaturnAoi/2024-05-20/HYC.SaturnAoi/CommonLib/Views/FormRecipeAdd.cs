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
    public partial class FormRecipeAdd : FormPopBase
    {
        public FormRecipeAdd()
        {
            InitializeComponent();
            Title = "机种名";
        }
        public string StrRecipeID = string.Empty;
        public string OldRecipeID = string.Empty;
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("请输入机种名");
                this.DialogResult = DialogResult.No;
                return;
            }
            StrRecipeID = this.textBox1.Text;
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCacel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
