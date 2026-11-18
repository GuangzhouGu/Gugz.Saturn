using HYC.SaturnDB.DAL;
using HYC.SaturnDB.Model;
using HYC.HTFile.HTINI;
using HYC.HTLog;
using HYC.HTPCBase.Extension.Recipe;
using HYC.HTPCBase.Extension.Views;
using HYC.HTSystem;
using HYC.SaturnMain.Devices.Cim;
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
using HYC.HTAlarm.DB.Model;
using System.Threading;
using HYC.HTPCBase.Extension.Entities;
using CIMBase.CCLink;

namespace HYC.SaturnMain.SubForms
{
    public partial class FrmLinkSignal : ViewBase
    {
        public FrmLinkSignal()
        {
            InitializeComponent();
            this.ButtonName = "Cim通讯";
            this.ButtonNameEn = "Cim\nCommunication";
            this.IsNeedLogin = false;
            this.Index = 2;
            this.ButtonImage = Properties.Resources.Cim;
            RegisterEvents();
        }
        public void RegisterEvents()
        {
            CimManager.Instance.OnReceiveCimMsg += Instance_OnReceiveCimMsg;
            CimManager.Instance.OnSendToCimMsg += Instance_OnSendToCimMsg;
            LinkSignalFlow.Instance.OnShowMsg += Instance_OnShowMsg;
        }

        private void Instance_OnShowMsg(string arg1, string arg2, Brush arg3)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string,string,Brush>(Instance_OnShowMsg),arg1,arg2,arg3);
            }else
            {
                lbxInterfaceMsg.AddListBoxMsg(arg2, arg3);
                LogManager.GetLogger(arg1).Info(arg2);
            }
        }

        private void Instance_OnSendToCimMsg(string arg1, Brush arg2)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string, Brush>(Instance_OnSendToCimMsg), arg1, arg2);
            }
            else
            {
                lbxEqToCimMsg.AddListBoxMsg(arg1, arg2);
            }
        }

        private void Instance_OnReceiveCimMsg(string arg1, Brush arg2)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string, Brush>(Instance_OnReceiveCimMsg), arg1, arg2);
            }
            else
            {
                lbxCimToEqMsg.AddListBoxMsg(arg1, arg2);
            }
        }

        private void btnClearEqToCimMsg_Click(object sender, EventArgs e)
        {
            lbxEqToCimMsg.Items.Clear();
        }

        private void btnClearCimToEqMsg_Click(object sender, EventArgs e)
        {
            lbxCimToEqMsg.Items.Clear();
        }

        private void FrmCim_Load(object sender, EventArgs e)
        {
           
        }
    
        private void btnInterfaceMsgClear_Click(object sender, EventArgs e)
        {
            lbxInterfaceMsg.Items.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CimManager.Instance.ReceivedJobReport(GetGlassData());

        }
        private GlassDataA[]GetGlassData()
        {
            GlassDataA glassData1 = new GlassDataA();
            glassData1.Glass_ID = "AHalf";
            glassData1.Prod_ID = "qwerty";
            glassData1.Option_Value = "sdgsfg";
            glassData1.Lot_ID= DateTime.Now.ToLongTimeString();
            glassData1.LOT_Sequence_No = 1000;
            glassData1.Slot_Sequence_No = 2000;

            GlassDataA glassData2 = new GlassDataA();
            glassData2.Glass_ID = "BHalf";
            glassData2.Prod_ID = "qwerty";
            glassData2.Option_Value = "sdgsfg";
            glassData2.Lot_ID = DateTime.Now.ToLongTimeString();
            glassData2.LOT_Sequence_No = 1001;
            glassData2.Slot_Sequence_No = 2001;
            return new GlassDataA[] { glassData1,glassData2};
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CimManager.Instance.StoredInReport(GetGlassData());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CimManager.Instance.ProcessStartReport(GetGlassData());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            CimManager.Instance.ProcessEndReport(GetGlassData());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var glassDatas = GetGlassData();
            DateTime time1 = DateTime.Now;
            new CellStartTimeDal().Insert(new CellStartTimeModel { PanelID= glassDatas[0].Glass_ID ,StartTime=time1});
            new TotalInfoDal().InsertTest(new TotalInfoModel { GlassNum=0, GlassID =glassDatas[0].Glass_ID,RecipeID=RecipeManager.CurRecipe.RecipeID,Result="OK",EQPInTime=time1,StartTime= time1.AddMinutes(1), EndTime=time1.AddMinutes(3)});

            CimManager.Instance.ProcessDataReport(glassDatas[0],0);

            DateTime time2 = DateTime.Now;
            new CellStartTimeDal().Insert(new CellStartTimeModel { PanelID = glassDatas[1].Glass_ID, StartTime = time2 });
            new TotalInfoDal().InsertTest(new TotalInfoModel { GlassNum = 1, GlassID = glassDatas[1].Glass_ID, RecipeID = RecipeManager.CurRecipe.RecipeID, Result = "NG", EQPInTime = time2, StartTime = time2.AddMinutes(1), EndTime = time2.AddMinutes(3) });
            CimManager.Instance.ProcessDataReport(glassDatas[1], 1);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            CimManager.Instance.SentOutJobReport(GetGlassData());
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var glassDatas = GetGlassData();
            DateTime time1 = DateTime.Now;
            new CellStartTimeDal().Insert(new CellStartTimeModel { PanelID = glassDatas[0].Glass_ID, StartTime = time1 });
            new TotalInfoDal().InsertTest(new TotalInfoModel { GlassNum = 0, GlassID = glassDatas[0].Glass_ID, RecipeID = RecipeManager.CurRecipe.RecipeID, Result = "OK", EQPInTime = time1, StartTime = time1.AddMinutes(1), EndTime = time1.AddMinutes(3) });
            CimManager.Instance.JobJudgeResultReport(glassDatas[0]);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            CimManager.Instance.FetchedOutReport(GetGlassData());
        }

        private void button9_Click(object sender, EventArgs e)
        {
            var sts = comboBox1.SelectedItem.ToString();
            var machineSts = EnumHelper.GetEnumValue<HTPCBase.Views.EqpSts>(sts);
            CimManager.Instance.MachineStatusChangeReport(machineSts);
          
        }

        private void button14_Click(object sender, EventArgs e)
        {
            var model = new AlarmDetail();
            model.AlarmId = "0001";
            model.AlarmLevel = AlarmLevel.LightAlarm;
            model.Description = "Axis1 Servo Lost";
            model.DescriptionCN = "1号轴伺服使能丢失";
            model.AlarmType = "伺服报警";
            CimManager.Instance.AlarmStateChangeReport(model,false);
        }


        private void button12_Click(object sender, EventArgs e)
        {
            var glassDatas = GetGlassData();
            CimManager.Instance.VCREventReport(glassDatas[0].Glass_ID,"qqqq", glassDatas[0],true,0);
            CimManager.Instance.VCREventReport(glassDatas[1].Glass_ID, "www", glassDatas[1], true, 1);
        }


        private void button17_Click(object sender, EventArgs e)
        {
            var glassDatas = GetGlassData();
            var block=CimManager.Instance.PanelDataInformationDownloadRequest(glassDatas[0]);
        }

        private void btnJobDataRequest_Click(object sender, EventArgs e)
        {
            var glassDatas = GetGlassData();
            var block = CimManager.Instance.JobDataRequest("",glassDatas[1]);
        }

        private void btnJobDataChangeReport_Click(object sender, EventArgs e)
        {
            var glassDatas = GetGlassData();
            CimManager.Instance.JobDataChangeReport(glassDatas[0]);
        }

        private void btnCurrentRecipeIDChangeReport_Click(object sender, EventArgs e)
        {
            CimManager.Instance.CurrentRecipeIDChangeReport(RecipeManager.CurRecipe);
        }

        private void btnRecipeParameterDataChangeReport_Click(object sender, EventArgs e)
        {
            CimManager.Instance.RecipeParameterDataChangeReport(RecipeManager.CurRecipe);
        }

        private void btnRecipeListChangeReport_Click(object sender, EventArgs e)
        {
            var list = RecipeManager.GetRecipes();
            CimManager.Instance.RecipeListChangeReport(list);
        }

        private void btnCimMessageConfirmReport_Click(object sender, EventArgs e)
        {
            CimManager.Instance.CimMessageConfirmReport(1);
        }

        private void btnVCRModeChangeReport_Click(object sender, EventArgs e)
        {
            CimManager.Instance.VCRModeChangeReport(1, 1);
        }

        private void btnPanelDataUpdateReport_Click(object sender, EventArgs e)
        {
            var glassDatas = GetGlassData();
            CimManager.Instance.PanelDataUpdateReport(glassDatas[0].Glass_ID, 0);
        }

        private void btnMachineModeChangeReport_Click(object sender, EventArgs e)
        {
            CimManager.Instance.MachineModeChangeReport(1);
        }

        private void btnMachineDummyGlassRequest_Click(object sender, EventArgs e)
        {
            CimManager.Instance.MachineDummyGlassRequest();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                int addr = Convert.ToInt32(textBox1.Text, 16);
                CCLinkHelper.ccLink.SetBit(addr);
            }
            catch (Exception ex)
            {

            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                int addr = Convert.ToInt32(textBox1.Text, 16);
                CCLinkHelper.ccLink.ResetBit(addr);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
