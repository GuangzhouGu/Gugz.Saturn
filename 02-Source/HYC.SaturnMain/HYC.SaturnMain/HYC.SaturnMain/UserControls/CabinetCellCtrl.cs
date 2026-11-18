using HYC.HTPCBase.Motion;
using HYC.SaturnMain.CabinetInfos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using HYC.HTPCBase.Extension.Entities;
using HYC.HTPCBase.Extension.UserCtrl;
using HYC.SaturnMain.Entities;
using HYC.SaturnMain.SubForms;
using HYC.SaturnDB.Model;
using HYC.SaturnDB.DAL;

namespace HYC.SaturnMain.UserControls
{
    public partial class CabinetCellCtrl : UcBase
    {
        public CabinetCellCtrl()
        {
            InitializeComponent();
        }
        public int CabinetRowNum { get; set; } = -1;
        MotionManager Motion = MotionManager.GetInstance();
  
        private void CabinetCellCtrl_Load(object sender, EventArgs e)
        {
            ProbeCardHelper.OnUpdatePrbInfo += ProbeCardHelper_OnUpdatePrbInfo;
        }

        private void ProbeCardHelper_OnUpdatePrbInfo(ProbeCardEventArgs obj)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<ProbeCardEventArgs>(ProbeCardHelper_OnUpdatePrbInfo), obj);
            }
            else
            {
                var recipeName = obj.MsgValue;
                var probeCard = ProbeCardHelper.GetProbeCard(recipeName);
                if (probeCard.CabinetRowNum-1==CabinetRowNum)
                {
                    if (obj.MsgType == "UpdateCount")
                    {
                        if (probeCard != null)
                        {
                            this.lblContactCount.Text = probeCard.ContactedCount.ToString();
                        }
                    }
                    else
                    {
                        if (probeCard != null)
                        {
                            this.pbxPrb.Image = probeCard.Status == "InCabinet" ? Properties.Resources.ProbeCard : null;
                            this.lblPrbSts.Text = probeCard.Status.ToUpper();
                        }
                    }
                }
              
            }
        }

        public void UpdateInfo(CabinetRowInfo info,bool isOutEqp=false)
        {       
            if (InvokeRequired)
            {
                Invoke(new Action<CabinetRowInfo, bool>(UpdateInfo),info, isOutEqp);
            }
            else
            {
                if (isOutEqp)
                {
                    this.lblProbeName.Text = "";
                    this.lblRecipeName.Text = "";
                    this.lblRfid.Text = "";
                    this.pbxIsUsed.Image = Properties.Resources.Gray2;
                    this.lblContactCount.Text = "";
                    this.pbxPrb.Image = null;
                    lblPrbSts.Text = "";
                }
                else
                {
                    this.lblRowNum.Text = (info.CabinetRowNum + 1).ToString();
                    this.lblProbeName.Text = info.ProbeName;
                    this.lblRecipeName.Text = info.RecipeName;
                    this.lblRfid.Text = info.RFID;
                    this.pbxIsUsed.Image  = info.IsUsed? Properties.Resources.Green2: Properties.Resources.Gray2;
                    var probeCard = ProbeCardHelper.GetProbeCard(info.RecipeName);
                    if (probeCard != null)
                    {
                        this.lblContactCount.Text = probeCard.ContactedCount.ToString();
                        lblPrbSts.Text = probeCard.Status.ToUpper();
                        bool ret = Motion.GetDigitalInput(info.PrbDetectSensorName);
                        this.pbxPrb.Image =probeCard.Status == "InCabinet" ? Properties.Resources.ProbeCard : null;
                    }
                }
            }
          
        }
        public void LoadCabinetRowInfo(CabinetRowInfo info)
        {
            CabinetRowNum = info.CabinetRowNum;
            if (InvokeRequired)
            {
                Invoke(new Action<CabinetRowInfo>(LoadCabinetRowInfo), info);
            }
            else
            {
                this.lblRowNum.Text = (info.CabinetRowNum + 1).ToString();
                this.lblProbeName.Text = info.ProbeName;
                this.lblRecipeName.Text = info.RecipeName;
                this.lblRfid.Text = info.RFID;
                this.pbxIsUsed.Image = info.IsUsed ? Properties.Resources.Green2 : Properties.Resources.Gray2;
                var probeCard = ProbeCardHelper.GetProbeCard(info.RecipeName);
                if (probeCard != null)
                {
                    this.lblContactCount.Text = probeCard.ContactedCount.ToString();
                    lblPrbSts.Text = probeCard.Status.ToUpper();
                    bool ret = Motion.GetDigitalInput(info.PrbDetectSensorName);
                    this.pbxPrb.Image = probeCard.Status == "InCabinet" ? Properties.Resources.ProbeCard : null;
                }
            }
        }

        private void 更新Probbe状态ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var info = CabinetInfoHelper.GetCabinetInfo(CabinetRowNum);
            FrmUpdateProbeSts frm = new FrmUpdateProbeSts(UpdateInfo, info);
            frm.OpType = "Update";
            frm.ShowDialog();
        }

        private void 插入Probe状态ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var info = CabinetInfoHelper.GetCabinetInfo(CabinetRowNum);
            FrmUpdateProbeSts frm = new FrmUpdateProbeSts(UpdateInfo, info);
            frm.OpType = "Insert";
            frm.ShowDialog();
        }

        private void 删除Probe状态ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var info = CabinetInfoHelper.GetCabinetInfo(CabinetRowNum);
            ProbeCardHelper.Delete(info.ProbeName);
            info.RFID = string.Empty;
            info.ProbeName = string.Empty;
            info.RecipeName = string.Empty;
            info.IsUsed = false;
            CabinetInfoHelper.SaveCabinetInfo();
            UpdateInfo(info, true);
        }
    }
}
