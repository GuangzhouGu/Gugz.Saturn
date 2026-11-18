using HYC.SaturnDB.DAL;
using HYC.SaturnDB.Model;
using HYC.HTLog;
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
using CIMBOE.Block;
using BOECIM.Devices.CIM;

namespace HYC.SaturnMain.SubForms
{
    public partial class FrmCimMsg : FormPopBase
    {
        public FrmCimMsg()
        {
            InitializeComponent();
            this.Title = "Cim消息";
            this.TopMost = true;
        }
        public void ShowCimMessage(CimMessageModel model)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<CimMessageModel>(ShowCimMessage), model);
            }
            else
            {
                if (this.Visible == false)
                {
                    this.Show();
                }
                try
                {
                    CimMessageDal dal = new CimMessageDal();
                    var lst = dal.GetIsSet(true).OrderBy(p => p.ReceivedTime).ToList();
                    for (int i = 0; i < lst.Count; i++)
                    {
                        this.dgvCImMsg.Rows.Add();
                        this.dgvCImMsg.Rows[i].Cells["No"].Value = i + 1;
                        this.dgvCImMsg.Rows[i].Cells["ID"].Value = lst[i].MessageID;
                        this.dgvCImMsg.Rows[i].Cells["ReceiveTime"].Value = lst[i].ReceivedTime;
                        this.dgvCImMsg.Rows[i].Cells["CimMsg"].Value = lst[i].Message;
                        //DataGridViewButtonCell btn_confirm = new DataGridViewButtonCell();
                        //btn_confirm.Value = "Confirm";
                        this.dgvCImMsg.Rows[i].Cells["Confirm"].Value = "Confirm";
                    }
                }
                catch (Exception ex)
                {
                    LogManager.GetLogger("Error").Error(ex);
                }
            }
           
        }

        private void dgvCImMsg_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int columnindex = e.ColumnIndex;
                int rowindex = e.RowIndex;
                if (columnindex == 4)//Confirm按钮
                {
                    string strMsgID = (this.dgvCImMsg.Rows[rowindex].Cells["ID"].Value??"").ToString();
                    if (strMsgID != "")
                    {
                        ushort msgID = Convert.ToUInt16(strMsgID);
                        //上报CIM
                        //反馈
                        CimManager.Instance.CimMessageConfirmReport(msgID);
                        //更新数据库记录
                        CimMessageDal dal = new CimMessageDal();
                        var lst = dal.GetIsSet(true).Where(p => p.MessageID == msgID).ToList();
                        if (lst!=null)
                        {
                            for (int i = 0; i < lst.Count; i++)
                            {
                                lst[i].IsSet = false;
                                dal.Update(lst[i]);
                            }
                        }
                        dgvCImMsg.Rows.RemoveAt(rowindex);
                    }
                    if (dgvCImMsg.Rows.Count == 0)
                    {
                        this.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void FrmCimMsg_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Visible = false;
        }
    }
}
