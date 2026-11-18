using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HYC.SaturnMain.Entities;
using System.Threading;
using HYC.HTPCBase.Extension.Entities;
using System.Reflection;

using HYC.HTPCBase.Extension.UserCtrl;
using BOECIM.Devices.CIM;
using CIMBOE;

namespace HYC.SaturnMain.Devices.Cim
{
    public partial class LinkSignalControl : UserControl
    {
        public LinkSignalControl()
        {
            InitializeComponent();
            InitDgv();
            CimManager.Instance.OnLinkSignalChanged += Instance_LinkSignalChanged;
        }
        private void Instance_LinkSignalChanged(LinkSignalEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<LinkSignalEventArgs>(Instance_LinkSignalChanged),e);
            }
            else
            {
                switch (e.Index)
                {
                    case 0:
                        {
                            for (int i = 0; i < e.Signals.Length; i++)
                            {
                                dgvUpToL.Rows[i].Cells[2].Value = e.Signals[i] ? HYC.SaturnMain.Properties.Resources.Green2 : HYC.SaturnMain.Properties.Resources.Gray2; 
                            }
                            break;
                        }
                    case 1:
                        {
                            for (int i = 0; i < e.Signals.Length; i++)
                            {
                                dgvDownToL.Rows[i].Cells[2].Value = e.Signals[i] ? HYC.SaturnMain.Properties.Resources.Green2 : HYC.SaturnMain.Properties.Resources.Gray2;
                            }
                            break;
                        }
                    case 2:
                        {
                            for (int i = 0; i < e.Signals.Length; i++)
                            {
                                dgvLToDown.Rows[i].Cells[2].Value = e.Signals[i] ? HYC.SaturnMain.Properties.Resources.Green2 : HYC.SaturnMain.Properties.Resources.Gray2;
                            }
                            break;
                        }
                    case 3:
                        {
                            for (int i = 0; i < e.Signals.Length; i++)
                            {
                                dgvLToUp.Rows[i].Cells[2].Value = e.Signals[i] ? HYC.SaturnMain.Properties.Resources.Green2 : HYC.SaturnMain.Properties.Resources.Gray2;
                            }
                            break;
                        }
                    default:
                        break;
                }
            }
        }

        private void InitDgv()
        {
            try
            {
                dgvUpToL.AlternatingRowsDefaultCellStyle.BackColor = dgvLToDown.AlternatingRowsDefaultCellStyle.BackColor = dgvDownToL.AlternatingRowsDefaultCellStyle.BackColor = dgvLToUp.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.PowderBlue;
                dgvUpToL.RowsDefaultCellStyle.BackColor = dgvLToDown.RowsDefaultCellStyle.BackColor = dgvDownToL.RowsDefaultCellStyle.BackColor = dgvLToUp.RowsDefaultCellStyle.BackColor = System.Drawing.Color.WhiteSmoke;
                dgvUpToL.DefaultCellStyle.Alignment = dgvLToDown.DefaultCellStyle.Alignment = dgvDownToL.DefaultCellStyle.Alignment = dgvLToUp.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                List<string> upLinkSignals = EnumHelper.EnumToList<UpStreamSignal>();
                List<string> downLinkSignals = EnumHelper.EnumToList<DownStreamSignal>();

                var dgvType = dgvUpToL.GetType();
                var properInfo = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
                properInfo.SetValue(dgvUpToL, true, null);
                 dgvType = dgvLToDown.GetType();
                 properInfo = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
                properInfo.SetValue(dgvLToDown, true, null);
                 dgvType = dgvDownToL.GetType();
                 properInfo = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
                properInfo.SetValue(dgvDownToL, true, null);
                 dgvType = dgvLToUp.GetType();
                 properInfo = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
                properInfo.SetValue(dgvLToUp, true, null);

                for (int i = 0; i < upLinkSignals.Count; i++)
                {
                    dgvUpToL.Rows.Add();
                    dgvLToDown.Rows.Add();
                    dgvUpToL.Rows[i].Cells[0].Value = (i + 1).ToString();
                    dgvUpToL.Rows[i].Cells[1].Value = upLinkSignals[i];
                    dgvLToDown.Rows[i].Cells[0].Value = (i + 1).ToString();
                    dgvLToDown.Rows[i].Cells[1].Value = upLinkSignals[i];
                }
                for (int i = 0; i < downLinkSignals.Count; i++)
                {
                    dgvDownToL.Rows.Add();
                    dgvLToUp.Rows.Add();
                    dgvDownToL.Rows[i].Cells[0].Value = (i + 1).ToString();
                    dgvDownToL.Rows[i].Cells[1].Value = downLinkSignals[i];
                    dgvLToUp.Rows[i].Cells[0].Value = (i + 1).ToString();
                    dgvLToUp.Rows[i].Cells[1].Value = downLinkSignals[i];
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void LinkSignalControl_Load(object sender, EventArgs e)
        {

        }

        private void dgvLToDown_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 3)
                {
                    int rowIndex = e.RowIndex;
                    bool signal = bool.Parse((dgvLToDown.Rows[rowIndex].Cells[3].Value ?? false).ToString());
                    LinkSignalFlow.Instance.SendDownSignal((UpStreamSignal)rowIndex, signal);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvLToDown_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvLToDown.IsCurrentCellDirty)
            {
                dgvLToDown.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dgvLToUp_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvLToUp.IsCurrentCellDirty)
            {
                dgvLToUp.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dgvLToUp_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 3)
                {
                    int rowIndex = e.RowIndex;
                    bool signal = bool.Parse((dgvLToUp.Rows[rowIndex].Cells[3].Value ?? false).ToString());
                    LinkSignalFlow.Instance.SendUpSignal((DownStreamSignal)rowIndex, signal);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
