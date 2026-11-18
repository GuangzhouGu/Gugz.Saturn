using HYC.HTPCBase.Extension.Entities;
using HYC.SaturnMain.Devices.StaticElec;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HYC.SaturnMain.UserControls
{
    public partial class StaticElecCtrl : UserControl
    {
        public StaticElecCtrl()
        {
            InitializeComponent();
        }

        private void btnStaticElecRead_Click(object sender, EventArgs e)
        {
            int index = cmxStaticElec.SelectedIndex;
            var value = StaticElecHelper.Instance.GetValue(index);
            if (value != null)
            {
                lbxStaticElecMsg.Items.Add((double)value);
            }
        }
        /// <summary>
        /// 初始化
        /// </summary>
        public void InitCtrl()
        {
            if (StaticElecHelper.Instance != null)
            {
                try
                {
                    for (int i = 0; i < StaticElecHelper.Instance.StaticElecCount; i++)
                    {
                        this.cmxStaticElec.Items.Add("静电测量仪" + (i + 1));
                    }
                    this.cmxStaticElec.SelectedIndex = 0;
                }
                catch (Exception)
                {

                }
            }
        }

        private void StaticElecCtrl_Load(object sender, EventArgs e)
        {
            if (ClassHelper.IsDesingMode() == false)
            {
                InitCtrl();
            }
        }

        private void btnStaticElecClearMsg_Click(object sender, EventArgs e)
        {
            lbxStaticElecMsg.Items.Clear();
        }
    }
}
