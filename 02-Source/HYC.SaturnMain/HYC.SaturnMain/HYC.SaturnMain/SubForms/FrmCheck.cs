using HYC.HTLog;
using HYC.HTPCBase.Motion;
using HYC.SaturnMain.Devices.ClientPC;
using HYC.SaturnMain.Devices.Motion;
using HYC.SaturnMain.Entities;
using HYC.SaturnMain.Station;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using HYC.SaturnMain.Params;
using HYC.SaturnMain.UVWStage;
using HYC.HTPCBase.Extension.Recipe;
using HYC.HTPCBase.Extension.Views;
using HYC.HTPCBase.Extension.Entities;
using HYC.HTPCBase.UserCtrl;
using HYC.HTPCBase.Station;


using HYC.HTPCBase.Param;
using HYC.SaturnMain;
using HYC.SaturnAoi.Entities;
using HYC.SaturnDB.DAL;
using CIMBOE.Block;


namespace HYC.SaturnMain.SubForms
{
    public partial class FrmCheck : ViewBase
    {
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }
        private bool InterlockEnable = true;
        public FrmCheck()
        {
            InitializeComponent();
            this.IsNeedLogin = true;
            this.ButtonName = "设备点检";
            this.ButtonNameEn = "Device\nCheck";
            this.Level = UserLevel.Engineer;
            this.Index = 3;
            this.ButtonImage = HYC.SaturnMain.Properties.Resources.DeviceCheck;
            this.IsCanSwitchInRunning = false;
        }
        private void Instance_OnSignalTrigger(IOInfo arg1, bool arg2)
        {
            if (arg1.IOName == Para.PCBase.ModeSwitchButton)
            {
                Motion.SetDigitalOutput(switchButtonCtrl6.SwitchName, false);
            }
        }
        MotionManager Motion = MotionManager.GetInstance();

        private void FormCheck_Load(object sender, EventArgs e)
        {
            RegisterEvents();
            CylinderCtrl.IsCanFront += CylinderCtrl_IsCanFront1;
        }
        private bool CylinderCtrl_IsCanFront1(string arg)
        {
            return IsInStopSts();
        }
     

        private void RegisterEvents()
        {
            switchButtonCtrl11.OnSwitchOutput += SwitchButtonCtrl11_OnSwitchOutput;
            switchButtonCtrl8.OnSwitchOutput += SwitchButtonCtrl8_OnSwitchOutput;
            CylinderCtrl.IsCanFront += CylinderCtrl_IsCanFront;
            zeroSysCtrl1.OnIsCanUnlock += ZeroSysCtrl1_OnIsCanUnlock;
            zeroSysCtrl2.OnIsCanUnlock += ZeroSysCtrl2_OnIsCanUnlock;
            zeroSysCtrl3.OnIsCanUnlock += ZeroSysCtrl2_OnIsCanUnlock;
            StationManager.Instance.OnSignalTrigger += Instance_OnSignalTrigger;
        }

        private bool ZeroSysCtrl2_OnIsCanUnlock()
        {
            return IsInStopSts();
        }

        private bool ZeroSysCtrl1_OnIsCanUnlock()
        {
            if (IsInStopSts() == false)
            {
                return false;
            }
            bool ret = true;
            if (Motion.IsArrive(StationEnum.PrbAutoChg, AxisName.PrbAutoChg_Z, PrbAutoChgPosName.PickFromUvwPos) == false &&
                Motion.GetDigitalInput(InputName.UvwDetPrbLeft) && Motion.GetDigitalInput(InputName.UvwDetPrbRight)&& InterlockEnable)
            {
                MessageBox.Show("Probe自动更换Z轴不在更换位置，禁止松开零点系统");
                ret = false;
            }
            if (Motion.GetDigitalInput(InputName.FixPcbCylUnlockArrive))
            {
                MessageBox.Show("Pcb固定气缸没有松开，禁止松开零点系统");
                ret = false;
            }
            return ret;
        }
        private bool CylinderCtrl_IsCanFront(string arg)
        {
            bool ret = true;
            if (arg== "ShutterClyL")
            {
                if (!Motion.GetDigitalInput(InputName.RobotDetect))
                {
                    MessageBox.Show("检测到机械手在设备中，禁止关闭门禁气缸");
                    ret = false;
                }
            }
            return ret;
        }

        private void SwitchButtonCtrl8_OnSwitchOutput(string arg1, bool arg2)
        {
            Motion.SetDigitalOutput("CabinetFloodLight2", arg2);
        }
        private void SwitchButtonCtrl11_OnSwitchOutput(string arg1, bool arg2)
        {
            Motion.SetDigitalOutput("EqpFloodLight2", arg2);
            Motion.SetDigitalOutput("EqpFloodLight3", arg2);
            Motion.SetDigitalOutput("EqpFloodLight4", arg2);
            Motion.SetDigitalOutput("EqpFloodLight5", arg2);
            Motion.SetDigitalOutput("EqpFloodLight6", arg2);
            Motion.SetDigitalOutput("EqpFloodLight7", arg2);
            Motion.SetDigitalOutput("EqpFloodLight8", arg2);
        }
        private void AnalysisData(string[,] datas)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string[,]>(AnalysisData), datas);
            }
            else
            {
                int count = datas.GetLength(1);
                for (int i = 0; i < count; i++)
                {
                    byte index = (byte)(byte.Parse(datas[i, 0]) - 0x80);
                    byte actValue = byte.Parse(datas[i, 1]);
                    byte sts = byte.Parse(datas[i, 2]);
                    byte setValue = byte.Parse(datas[i, 3]);
                    string temp = $"EFU：{index}，实际值：{actValue * 10}，报警内容：{sts}，设定值：{setValue * 10}";
                    //lbxEfuMsg.Items.Add(temp);

                }
            }
        }

        private bool IsInStopSts()
        {
            bool ret = false;
            var sts = StationManager.Instance.GetCurrentEQPSts();
            if (sts == HYC.HTPCBase.Views.EqpSts.STOP)
            {
                ret = true;
            }
            else
            {
                MessageBox.Show("设备不在停止状态，禁止操作");
            }
            return ret;
        }

        private void btnStartTest_Click(object sender, EventArgs e)
        {
            var startTime = DateTime.Now;
            int temp = new Random().Next(1000,9999);
            GlassID = $"767Y43{temp}B5A";
            new CellStartTimeDal().Insert(new SaturnDB.Model.CellStartTimeModel { PanelID=GlassID,StartTime= startTime });
            new TotalInfoDal().Insert2(new SaturnDB.Model.TotalInfoModel { GlassID = GlassID , EQPInTime = startTime ,RecipeID=RecipeManager.CurRecipe.RecipeID,StartTime=DateTime.Now,EndTime=DateTime.Now.AddMinutes(30),JobJudge="G",JobGrade="S"});

            GlassDataA jobData1 = new GlassDataA();
            jobData1.Glass_ID = GlassID;
            jobData1.Lot_ID = "F67N420152";
            jobData1.OPER_ID = "HYC";
            jobData1.Prod_ID = "BF068M49-TK0-7Q00";
            new JobDataDal().Insert(new SaturnDB.Model.JobDataModel { JobData = jobData1.ToString2(), GlassID = GlassID , GlassNum =0, ReceiveTime =DateTime.Now, SetOutTime = DateTime.Now .AddMinutes(30)});

            var dal = new AoiDetectInfoDal();
            Random random = new Random(0);
            for (int i = 0; i < Param.Panel.CellCountX; i++)
            {
                for (int j = 0; j < Param.Panel.CellCountY; j++)
                {
                    var model = new SaturnDB.Model.AoiDetectModel {DetectType="Aoi", GlassID = GlassID, GlassNum = 0, ColumnNum = i, CellID = Global.GetCellID(i, j), PCIndex = "1", EQPInTime = startTime, RecipeID = RecipeManager.CurRecipe.RecipeID, Result = (j % 2 == 0) ? "OK" : "NG", DetectCode = random.Next(1000, 1200).ToString(), StartTime = DateTime.Now, EndTime = DateTime.Now.AddMinutes(1), ProcessingTime = "60", BirghtDotDefectCount = 2, DarkDotDefectCount = 2, LineDefectCount = 3, Mura2DefectCount = 1, MuraDefectCount = 0, ReserveDefectCount = 2, TotalDefectCount = 2, XsycDefectCount = 1 };
                    dal.Insert2(model) ;
                    if (model.Result=="NG")
                    {
                        new AoiNGInfoDal().Insert(new SaturnDB.Model.AoiNGInfoModel {DetectType="Aoi",PatternName="Blue", GlassID= GlassID, GlassNum=0, ColumnNum=i, CellID=model.CellID, EQPInTime=model.EQPInTime, DefectNum=0, DefectSizeX=1, DefectSizeY=2, CoordinateX=3, CoordinateY=4 , ErrorCode = "1000" , DefectType ="Line"});
                    }
                }
            }
          
        }

     
        private void interlock解除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InterlockEnable = false;
            Task.Factory.StartNew(() =>
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                while (true)
                {
                    if (sw.ElapsedMilliseconds > 15 * 1000)
                    {
                        InterlockEnable = true;
                        break;
                    }
                    Thread.Sleep(10);
                }

            });
        }

        string GlassID = "";
        private void hcButton21_Click(object sender, EventArgs e)
        {
            try
            {
                //DFSEntity.Init();
                //DFSEntity.SaveDFSData(GlassID);
                //DFSEntity.SaveDFSDataFile();
                DFSHelper.DfsUpload(GlassID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void hcButton22_Click(object sender, EventArgs e)
        {
          
        }
    }
}


