using HYC.HTPCBase.Extension.Entities;
using HYC.HTPCBase.Extension.Recipe;
using HYC.SaturnAoi.Entities;
using HYC.SaturnDB.DAL;
using HYC.SaturnMain.Devices.ClientPC;
using HYC.SaturnMain.Devices.MasterPC;
using HYC.SaturnMain.Entities;
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
    public partial class PcCtrl : UserControl
    {
        public PcCtrl()
        {
            InitializeComponent();
        }
        string PcName = string.Empty;
        string Cmd = string.Empty;

        private void btn_send_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(Cmd))
                {
                    string msg = tbx_cmd.Text;
                    string command = cmx_comList.SelectedItem.ToString();
                    if (command == "DetectReq")
                    {
                        var startTime = DateTime.Now;
                        var glassID = "767Y430003B5A";
                        var strs = msg.Split(',');
                        DetectReqArgs detectReq = new DetectReqArgs();
                        detectReq.CellID = "AA01";
                        detectReq.DetectType = strs[0];
                        detectReq.GlassID = glassID;
                        detectReq.PCIndex = int.Parse(strs[1]);
                        detectReq.GlassNum = 0;
                        detectReq.ColumnNum = 0;
                        detectReq.CellNum = 0;
                       

                        new OneCellDetectInfoDal().Insert(new SaturnDB.Model.OneCellDetectModel { GlassID = glassID, CellID = detectReq.CellID = "AA01", EQPInTime = startTime, GlassNum = 0, CellNum = 0, ColumnNum = 0 });
                        new CellStartTimeDal().Insert(new SaturnDB.Model.CellStartTimeModel { PanelID = glassID, StartTime = startTime });
                        new TotalInfoDal().Insert(new SaturnDB.Model.TotalInfoModel { GlassID = glassID, EQPInTime = startTime, RecipeID = RecipeManager.CurRecipe.RecipeID, StartTime = startTime });
                        AoiPC.Instance.DetectReq(detectReq);
                    }
                    else
                    {
                        MessageInfo info = new MessageInfo { PCName = PcName, Command = EnumHelper.GetEnumValue<Command>(command), Message = msg };
                        MasterPCHelper.Instance.SendMessage(info);
                    }
               
                }
                else
                {
                    MessageInfo info = MessageInfo.GetInfo(Cmd);
                    MasterPCHelper.Instance.SendMessage(info);
                    Cmd = string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void btnClearClientMsg_Click(object sender, EventArgs e)
        {
            lbxClientMsg.Items.Clear();
        }

        private void cmx_clientList_SelectedIndexChanged(object sender, EventArgs e)
        {
            PcName = cmx_clientList.SelectedItem.ToString();
            Cmd = string.Empty;
        }
        private void ShowClientMsg(string msg)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(ShowClientMsg), msg);
            }
            else
            {
                this.lbxClientMsg.Items.Add(msg);
            }
        }

        private void PcCtrl_Load(object sender, EventArgs e)
        {
            if (ClassHelper.IsDesingMode() == false)
            {
                ProcessCmd.Instance.ShowClientMsg(ShowClientMsg);
                InitCmx();
            }

        }
        private void InitCmx()
        {
            string[] arrNames = Enum.GetNames(typeof(Command));
            cmx_comList.Items.Clear();
            cmx_comList.Items.AddRange(arrNames);
            string[] temp = RecipeManager.GetRecipes().Select(p => p.RecipeID).ToArray();
        }
    }
}
