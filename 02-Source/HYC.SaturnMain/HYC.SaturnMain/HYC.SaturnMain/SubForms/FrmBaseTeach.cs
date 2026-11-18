using HYC.HTLog;
using HYC.HTPCBase.Args;
using HYC.HTPCBase.Config;
using HYC.HTPCBase.Entities;
using HYC.HTPCBase.Extension.Entities;
using HYC.HTPCBase.Extension.Recipe;
using HYC.HTPCBase.Extension.Views;
using HYC.HTPCBase.Motion;
using HYC.HTPCBase.Station;
using HYC.SaturnMain.CabinetInfos;
using HYC.SaturnMain.GlassCrds;
using HYC.SaturnMain.Params;
using HYC.WindowsControls;


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
using HYC.SaturnAoi.Entities;
using HYC.SaturnDB.DAL;

namespace HYC.SaturnMain.SubForms
{
    public partial class FrmBaseTeach : ViewBase
    {
        public FrmBaseTeach()
        {
            InitializeComponent();
            this.IsNeedLogin = true;
            this.ButtonName = "相机示教";
            this.ButtonNameEn = "Camera\nTeach";
            this.Level = UserLevel.Engineer;
            this.Index = 9;
            this.ButtonImage = Properties.Resources.AutoPos;
            this.IsCanSwitchInRunning = true;
            UserCtrlManager.OnUpdateRecipe += UserCtrlManager_OnUpdateRecipe;
        }
        double startAngle = 0;
        int StageIndex = 0; int ColumnIndex = 0;
        string PosName = string.Empty;
        Dictionary<double, PictureBox> ProbeGtyPosPbxDic = null;
        Dictionary<double, PictureBox> DetectGtyPosPbxDic = null;
        private void UserCtrlManager_OnUpdateRecipe(string obj)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(UserCtrlManager_OnUpdateRecipe), obj);
            }
            else
            {
                cbxStageIndex.SelectedIndex = StageIndex;
                LoadUvwPosInfo();
            }
        }

        ProbeGantry Probe = null;
        DualStage Stage = null;
        MotionManager Motion = MotionManager.GetInstance();

        System.Windows.Forms.Timer Timer = new System.Windows.Forms.Timer();
        private void FrmAlign_Load(object sender, EventArgs e)
        {
            Probe = StationManager.Instance.GetChild("ProbeGantry") as ProbeGantry;
            Stage = StationManager.Instance.GetChild("DualStage") as DualStage;
            InitCtrl();
            this.timer1.Enabled = true;
        }

        private void tabPos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabPos.SelectedIndex==0)
            {
                btnContact.Enabled = true;
                btnStopContact.Enabled = true;
            }
            else
            {
                btnContact.Enabled = false;
                btnStopContact.Enabled = false;
                LoadUvwAutoChgPos();
            }
        }
        private void LoadUvwAutoChgPos()
        {
            dgvAutoChangePos.Rows.Clear();
            dgvAutoChangePos.Rows.Add();
            dgvAutoChangePos.Rows[0].Cells[0].Value =Param.Uvw.AxisYAutoChgPos;
            dgvAutoChangePos.Rows[0].Cells[1].Value =Param.Uvw.AxisX1AutoChgPos;
            dgvAutoChangePos.Rows[0].Cells[2].Value =Param.Uvw.AxisX2AutoChgPos;
        }
        private void btnUPos_Click(object sender, EventArgs e)
        {
            if (CheckIsCanMove())
            {
                double pos = double.Parse(tbxDisU.Text);
                Probe.UvwMoveRel(pos, 0, 0);
            }
        }

        private void btnUNeg_Click(object sender, EventArgs e)
        {
            if (CheckIsCanMove())
            {
                double pos = double.Parse(tbxDisU.Text);
                Probe.UvwMoveRel(-pos, 0, 0);
            }
        }

        private void btnVPos_Click(object sender, EventArgs e)
        {
            if (CheckIsCanMove())
            {
                double pos = double.Parse(tbxDisV.Text);
                Probe.UvwMoveRel(0, pos, 0);
            }
        }

        private void btnVNeg_Click(object sender, EventArgs e)
        {
            if (CheckIsCanMove())
            {
                double pos = double.Parse(tbxDisV.Text);
                Probe.UvwMoveRel(0, -pos, 0);
            }
        }

        private void btnWPos_Click(object sender, EventArgs e)
        {
            if (CheckIsCanMove())
            {
                double pos = double.Parse(tbxDisW.Text);
                UvwMoveAngle(-pos);
            }
        }

        private void btnWNeg_Click(object sender, EventArgs e)
        {
            if (CheckIsCanMove())
            {
                double pos = double.Parse(tbxDisW.Text);
                UvwMoveAngle(pos);
            }
        }

        private void btnZPos_Click(object sender, EventArgs e)
        {
            double pos = double.Parse(tbxDisZ.Text);
            Probe.AxisZMove(StageIndex,pos*2.5);
        }

        private void btnZNeg_Click(object sender, EventArgs e)
        {
            double pos = double.Parse(tbxDisZ.Text);
            Probe.AxisZMove(StageIndex, -pos * 2.5);
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            if (IsInStopSts() == false) { return; }
            startAngle = 0;
            tbxAxisWCurAngle.Text = startAngle.ToString();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            Probe.UvwStop();
        }
        private bool CheckIsCanMove(bool isAxisUVW=true)
        {
            if (IsInStopSts() == false) { return false; }
            if (isAxisUVW&&(Motion.GetDigitalOutput(OutputName.UvwBrake1) || Motion.GetDigitalOutput(OutputName.UvwBrake2)))
            {
                MessageBox.Show("Uvw刹车未松开，禁止移动Uvw平台！");
                return false;
            }
            if (cbxProbeHeadInterlock.Checked)
            {
                if (Probe.IsContactZAtSafePos())
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("压头在下压位置，禁止移动！");
                    return false;
                }
            }
            else
            {
                return true;
            }
        }
        private void UvwMoveAngle(double detAngle)
        {
            Probe.UvwMoveRel(0, 0, detAngle, startAngle, true);
            startAngle += detAngle;
            tbxAxisWCurAngle.Text = startAngle.ToString();
        }

        private void LoadUvwPosInfo()
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action(LoadUvwPosInfo));
            }
            else
            {
                try
                {
                    int stageIndex = cbxStageIndex.SelectedIndex;
                    dgvUvwPos.Rows.Clear();
                    if (Param.Uvw != null)
                    {
                        int index = dgvUvwPos.Rows.Add();
                        dgvUvwPos.Rows[index].Cells[0].Value = stageIndex == 0 ?Param.Uvw.AxisYPosStage1 :Param.Uvw.AxisYPosStage2;
                        dgvUvwPos.Rows[index].Cells[1].Value = stageIndex == 0 ?Param.Uvw.AxisX1PosStage1 :Param.Uvw.AxisX1PosStage2;
                        dgvUvwPos.Rows[index].Cells[2].Value = stageIndex == 0 ?Param.Uvw.AxisX2PosStage1 :Param.Uvw.AxisX2PosStage2;
                        dgvUvwPos.Rows[index].Cells[3].Value = stageIndex == 0 ?Param.Uvw.AxisZContactPosStage1 : Param.Uvw.AxisZContactPosStage2;
                        dgvUvwPos.Rows[index].Cells[4].Value =Math.Round( (stageIndex == 0 ?Param.Uvw.AxisZContactPosStage1 : Param.Uvw.AxisZContactPosStage2) /2.5,3);
                    }
                }
                catch (Exception ex)
                {
                    LogManager.GetLogger("Form").Fatal(ex.ToString());
                }
            }
        }

        private void btnReadPos_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsInStopSts() == false) { return; }
                var sts = StationManager.Instance.GetCurrentEQPSts();
                if (sts != HTPCBase.Views.EqpSts.STOP)
                {
                  
                    MessageBox.Show("设备不在停止状态，禁止读取点位");
                }
                //基准位置读取
                if (tabPos.SelectedIndex==0)
                {
                    if (cbxStageIndex.SelectedItem == null)
                    {
                        MessageBox.Show("请先选择平台号");
                        return;
                    }
                    if (dgvUvwPos.Rows.Count < 0)
                    {
                        MessageBox.Show("请先添加行");
                        return;
                    }
                    int stageIndex = cbxStageIndex.SelectedIndex;
                    double pos = 0;
                    if (cbxReadAxisYPos.Checked)
                    {
                        pos = AxisInfoHelper.GetAxisInfoByName(AxisName.UvwStage_Y).ActPos;

                        dgvUvwPos.Rows[0].Cells[0].Value = pos;
                    }
                    if (cbxReadAxisX1Pos.Checked)
                    {
                        pos = AxisInfoHelper.GetAxisInfoByName(AxisName.UvwStage_X1).ActPos;
                        dgvUvwPos.Rows[0].Cells[1].Value = pos;
                    }
                    if (cbxReadAxisX2Pos.Checked)
                    {
                        pos = AxisInfoHelper.GetAxisInfoByName(AxisName.UvwStage_X2).ActPos;
                        dgvUvwPos.Rows[0].Cells[2].Value = pos;
                    }
                    if (cbxReadAxisZ1Pos.Checked)
                    {
                        if (StageIndex==0)
                        {
                            pos = AxisInfoHelper.GetAxisInfoByName(AxisName.Stage1_ContactZ).ActPos;
                        }
                        else
                        {
                            pos = AxisInfoHelper.GetAxisInfoByName(AxisName.Stage2_ContactZ).ActPos;
                        }
                       
                        dgvUvwPos.Rows[0].Cells[3].Value = pos;
                        dgvUvwPos.Rows[0].Cells[4].Value =Math.Round(pos/2.5,3);
                    }
                }
                else//自动更换位置读取
                {
                    if (dgvAutoChangePos.Rows.Count < 0)
                    {
                        MessageBox.Show("请先添加行");
                        return;
                    }
                    double pos = 0;
                    if (cbxReadAxisYPos.Checked)
                    {
                        pos = AxisInfoHelper.GetAxisInfoByName(AxisName.UvwStage_Y).ActPos;
                        dgvAutoChangePos.Rows[0].Cells[0].Value = pos;
                    }
                    if (cbxReadAxisX1Pos.Checked)
                    {
                        pos = AxisInfoHelper.GetAxisInfoByName(AxisName.UvwStage_X1).ActPos;
                        dgvAutoChangePos.Rows[0].Cells[1].Value = pos;
                    }
                    if (cbxReadAxisX2Pos.Checked)
                    {
                        pos = AxisInfoHelper.GetAxisInfoByName(AxisName.UvwStage_X2).ActPos;
                        dgvAutoChangePos.Rows[0].Cells[2].Value = pos;
                    }
                  
                }
            
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("Form").Fatal(ex.ToString());
            }
        }

        private void btnContact_Click(object sender, EventArgs e)
        {
            if (IsInStopSts() == false) { return; }
            var sts = StationManager.Instance.GetCurrentEQPSts();
            if (sts ==HTPCBase.Views.EqpSts.STOP)
            {
                int stageIndex = cbxStageIndex.SelectedIndex;
                Task.Factory.StartNew(()=> {
                    Task<bool> t = Probe.StageContactSync(stageIndex);
                    if (t.Result == false)
                    {
                        MessageBox.Show("压头下压超时");
                    }
                });
            }
            else
            {
                MessageBox.Show("设备不在停止状态，禁止手动控制压头");
            }
        }

        private void btnStopContact_Click(object sender, EventArgs e)
        {
            AoiPC.Instance.PowerControlReq(false, 0);
            AoiPC.Instance.PowerControlReq(false, 1);
            var sts = StationManager.Instance.GetCurrentEQPSts();
            if (sts == HTPCBase.Views.EqpSts.STOP)
            {
                int stageIndex = cbxStageIndex.SelectedIndex;
                Task.Factory.StartNew(() => {
                    Task<bool> t = Probe.StageStopContactSync(false,stageIndex);
                    if (t.Result == false)
                    {
                        MessageBox.Show("压头抬升超时");
                    }
                });
            }
            else
            {
                MessageBox.Show("设备不在停止状态，禁止手动控制压头");
            }
        }

        private void hcButton25_Click(object sender, EventArgs e)
        {
            bool result = false;
            try
            {
                if (tabPos.SelectedIndex==0)
                {
                    if (CheckDgvCellsIsEmpty(dgvUvwPos))
                    {
                        MessageBox.Show("有单元格为空，请赋值");
                        return;
                    }
                    int stageIndex = cbxStageIndex.SelectedIndex;
                    if (dgvUvwPos.Rows.Count>0)
                    {
                        if (stageIndex == 0)
                        {
                           Param.Uvw.AxisYPosStage1 = double.Parse(dgvUvwPos.Rows[0].Cells[0].Value.ToString());
                           Param.Uvw.AxisX1PosStage1 = double.Parse(dgvUvwPos.Rows[0].Cells[1].Value.ToString());
                           Param.Uvw.AxisX2PosStage1 = double.Parse(dgvUvwPos.Rows[0].Cells[2].Value.ToString());
                           Param.Uvw.AxisZContactPosStage1 = double.Parse(dgvUvwPos.Rows[0].Cells[3].Value.ToString());
                            dgvUvwPos.Rows[0].Cells[4].Value = Math.Round(double.Parse(dgvUvwPos.Rows[0].Cells[3].Value.ToString()) / 2.5, 3);
                        }
                        else
                        {
                           Param.Uvw.AxisYPosStage2 = double.Parse(dgvUvwPos.Rows[0].Cells[0].Value.ToString());
                           Param.Uvw.AxisX1PosStage2 = double.Parse(dgvUvwPos.Rows[0].Cells[1].Value.ToString());
                           Param.Uvw.AxisX2PosStage2 = double.Parse(dgvUvwPos.Rows[0].Cells[2].Value.ToString());
                           Param.Uvw.AxisZContactPosStage2 = double.Parse(dgvUvwPos.Rows[0].Cells[3].Value.ToString());
                            dgvUvwPos.Rows[0].Cells[4].Value = Math.Round(double.Parse(dgvUvwPos.Rows[0].Cells[3].Value.ToString()) / 2.5, 3);
                        }
                    }
                    result = RecipeManager.CurRecipe.Para.SavePara(ParaType.Uvw);
                    if (result)
                    {
                        MessageBox.Show("保存完成");
                    }
                    else
                    {
                        MessageBox.Show("保存失败");
                    }
                }
                else
                {
                    if (dgvAutoChangePos.Rows.Count>0)
                    {
                       Param.Uvw.AxisYAutoChgPos = double.Parse(dgvAutoChangePos.Rows[0].Cells[0].Value.ToString());
                       Param.Uvw.AxisX1AutoChgPos = double.Parse(dgvAutoChangePos.Rows[0].Cells[1].Value.ToString());
                       Param.Uvw.AxisX2AutoChgPos = double.Parse(dgvAutoChangePos.Rows[0].Cells[2].Value.ToString());
                    }
                    result = RecipeManager.CurRecipe.Para.SavePara(ParaType.Uvw);
                    if (result)
                    {
                        MessageBox.Show("保存完成");
                    }
                    else
                    {
                        MessageBox.Show("保存失败");
                    }
                }
             
            }
            catch (Exception ex)
            {
                MessageBox.Show($"保存异常，异常信息：{ex.Message}");
            }

        }
        private bool CheckDgvCellsIsEmpty(DataGridView dgv)
        {
            bool result = false;
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                for (int j = 0; j < dgv.Columns.Count; j++)
                {
                    if (dgv.Rows[i].Cells[j].Value == null)
                    {
                        result = true;
                    }
                }
            }
            return result;
        }

        private void cbxStageIndex_SelectedIndexChanged(object sender, EventArgs e)
        {
            StageIndex = cbxStageIndex.SelectedIndex;
            dgvUvwPos.Rows.Clear();
            Task.Factory.StartNew(() => {
                LoadUvwPosInfo();
            });
        }
     
        private void InitCtrl()
        {
            dgvUvwPos.DefaultCellStyle.Alignment = dgvAutoChangePos.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            string[] temp = RecipeManager.GetRecipes().Select(p => p.RecipeID).ToArray();
            cbxStageIndex.SelectedIndex = 0;
            cmxPos.SelectedIndex = 0;
            cmxCamList.SelectedIndex = 0;
            Timer.Interval = 500;
            Timer.Tick += Timer_Tick;
            Timer.Start();
            cmxColumn.Items.Clear();
            for (int i = 0; i < Param.Panel.CellCountX; i++)
            {
                cmxColumn.Items.Add(i+1);
            }
            cmxColumn.SelectedIndex = 0;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
           tbxZ1ActPos.Text= Math.Round(Motion.GetAxisActPos(StationEnum.DualStage,AxisName.Stage1_ContactZ) /2.5,3).ToString();
            tbxZ2ActPos.Text = Math.Round(Motion.GetAxisActPos(StationEnum.DualStage, AxisName.Stage2_ContactZ) / 2.5, 3).ToString();
        }


        private void cmxPos_SelectedIndexChanged(object sender, EventArgs e)
        {
            PosName = cmxPos.SelectedItem.ToString();
        }


        private void btnAlignCamX_Click(object sender, EventArgs e)
        {
            if (Motion.GetAxisActPos(StationEnum.ProbeGantry, AxisName.ProbeGantry_Y1) + Motion.GetAxisActPos(StationEnum.DetectGantry, AxisName.DetectGantry_Y1) >= Param.Motion.GantryPreventCrushPos2)
            {
                if (Probe.IsDetectModuleZSafe() == false)
                {
                    MessageBox.Show("检测相机Z轴不在安全位置，禁止移动对位相机");
                    return;
                }
            }
          
            if (IsInStopSts() == false) { return; }
            AxisInfo axis1 = AxisInfoHelper.GetAxisInfoByName(AxisName.DetectGantry_Y1.ToString());
            AxisInfo axis2 = AxisInfoHelper.GetAxisInfoByName(AxisName.ProbeGantry_Y1.ToString());
            if (axis1.ActPos + axis2.ActPos > Param.Motion.GantryPreventCrushPos2)
            {
                MessageBox.Show("检测龙门和Probe龙门距离小于安全位置，不能对位相机，请先移开检测龙门！");
                return;
            }
            Task.Factory.StartNew(() =>
            {
                switch (PosName)
                {
                    case "初始位":
                        {
                             Motion.MoveAbsSync(StationEnum.ProbeGantry, AxisName.ProbeGantryAlig_X2, ProbeGantryPosName.SafePos, 30);
                            Motion.MoveAbsSync(StationEnum.ProbeGantry, AxisName.ProbeGantryAlig_X1, ProbeGantryPosName.SafePos, 30);
                            break;
                        }
                    case "拍照位":
                        {
                            MoveArgs args1 = new MoveArgs();
                            args1.Axis = AxisInfoHelper.GetAxisInfoByName(AxisName.ProbeGantryAlig_X1);
                            args1.TargetPos = AxisInfoHelper.GetPosInfoByName(AxisName.ProbeGantryAlig_X1, StageIndex == 0 ? ProbeGantryPosName.Stg1AlignCamGrabPos_X1 : ProbeGantryPosName.Stg2AlignCamGrabPos_X1).Pos;
                            MoveArgs args2 = new MoveArgs();
                            args2.Axis = AxisInfoHelper.GetAxisInfoByName(AxisName.ProbeGantryAlig_X2);
                            args2.TargetPos = AxisInfoHelper.GetPosInfoByName(AxisName.ProbeGantryAlig_X2, StageIndex == 0 ? ProbeGantryPosName.Stg1AlignCamGrabPos_X2 : ProbeGantryPosName.Stg2AlignCamGrabPos_X2).Pos;
                            if (InterlockMonitor.Instance.CheckIsCanMove(args1))
                            {
                                if (InterlockMonitor.Instance.CheckIsCanMove(args2))
                                {
                                     Motion.MoveAbsSync(StationEnum.ProbeGantry, AxisName.ProbeGantryAlig_X1, StageIndex == 0 ? ProbeGantryPosName.Stg1AlignCamGrabPos_X1 : ProbeGantryPosName.Stg2AlignCamGrabPos_X1, 30);
                                    Motion.MoveAbsSync(StationEnum.ProbeGantry, AxisName.ProbeGantryAlig_X2, StageIndex == 0 ? ProbeGantryPosName.Stg1AlignCamGrabPos_X2 : ProbeGantryPosName.Stg2AlignCamGrabPos_X2, 30);
                                }
                            }
                            break;
                        }
                }
            });
        }

        private void btnStageT_Click(object sender, EventArgs e)
        {
            if (IsInStopSts() == false) { return; }
            if (!Probe.IsContactZAtSafePos())
            {
                MessageBox.Show("压头不在安全位置，禁止移动平台");
                return;
            }
            if (!InterlockMonitor.Instance.CheckIsCanMove(new HTPCBase.Args.MoveArgs { Axis = AxisInfoHelper.GetAxisInfoByName("Stage1_Y") }))
            {
                return;
            }
            Task.Factory.StartNew(() => {

                switch (PosName)
                {
                    case "初始位":
                        {
                            Task<bool> t1 = Motion.MoveAbsSync(StationEnum.DualStage, AxisName.Stage1_Y, StagePosName.RotatePos, 60);
                            Task<bool> t2 = Motion.MoveAbsSync(StationEnum.DualStage, AxisName.Stage2_Y, StagePosName.RotatePos, 60);
                            if (t1.Result && t2.Result)
                            {
                                t1 = Motion.MoveAbsSync(StationEnum.DualStage, AxisName.Stage1_T, StagePosName.ZeroDegPos, 60);
                                t2 = Motion.MoveAbsSync(StationEnum.DualStage, AxisName.Stage2_T, StagePosName.ZeroDegPos, 60);
                            }
              
                            break;
                        }
                    case "拍照位":
                        {
                            StageToDetectPos();                         
                            break;
                        }
                }
            });
        }

        private void btnContactCamX_Click(object sender, EventArgs e)
        {
            if (IsInStopSts() == false) { return; }
            MoveArgs args = new MoveArgs();
            args.Axis = AxisInfoHelper.GetAxisInfoByName(AxisName.ProbeGantryAlig_X1);
            var rowIndex = cmxPanelNum.SelectedIndex;
            double pos = BasePointHelper.GetContactHeadPos(StageIndex, rowIndex);
            args.TargetPos = pos;
            if (InterlockMonitor.Instance.CheckIsCanMove(args))
            {
                Motion.MoveAbs(StationEnum.ProbeGantry, AxisName.ProbeGantryAlig_X1, pos);
            }
        }

        private void btnAnotherHeadPos_Click(object sender, EventArgs e)
        {
            if (IsInStopSts() == false) { return; }
            Motion.MoveRel(StationEnum.ProbeGantry, AxisName.ProbeGantryAlig_X1, Param.Panel.RxTxOffset, false);
        }

        private void btnCameraMove_Click(object sender, EventArgs e)
        {
            try
            {
                int camIndex = cmxCamList.SelectedIndex;
                int cellIndex = cmxPanelNum.SelectedIndex;
                if (camIndex<4)//检测模组1相机
                {
                    if (cellIndex>4)//Cell1到Cell5
                    {
                        MessageBox.Show("检测模组1只能检测Cell1到Cell5");
                        return;
                    }
                }
                else//检测模组2相机
                {
                    if (cellIndex <5)//Cell6到Cell9
                    {
                        MessageBox.Show("检测模组2只能检测Cell6到Cell9");
                        return;
                    }
                }

                {
                    if (Motion.IsArrive(StationEnum.ProbeGantry, AxisName.ProbeGantryAlig_X1,ProbeGantryPosName.SafePos)==false|| Motion.IsArrive(StationEnum.ProbeGantry, AxisName.ProbeGantryAlig_X2, ProbeGantryPosName.SafePos) == false)
                    {
                        MessageBox.Show($"对位相机不在安全位置，禁止移动{(CameraName)camIndex}相机到Cell正中心");
                        return;
                    }
                }
                if (IsInStopSts() == false) { return; }          
                Task.Factory.StartNew(() => {
                    CamMoveToCellCenter(cellIndex, (CameraName)camIndex);
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CamMoveToCellCenter(int cellIndex, CameraName cameraName)
        {
            if (ColumnIndex == -1)
            {
                MessageBox.Show("请选择第几列");
            }
            double detectGantryBasePointX = BasePointHelper.GetDetectStaBasePt(cameraName , StageIndex);
            double camBasePointY = BasePointHelper.GetDetectStaBasePt(cameraName , StageIndex,false);
            OneCellCrd OneCell = GlassCrdHelper.GetOneCellCrd(ColumnIndex, cellIndex);
            if (IsCanMove(AxisName.DetectGantry_Y1, detectGantryBasePointX + OneCell.X))
            {
                Motion.MoveAbsSync(StationEnum.DetectGantry, AxisName.DetectGantry_Y1, detectGantryBasePointX + OneCell.X, 60, true);              
                switch (cameraName)
                {
                    case CameraName.AoiCam1:
                    case CameraName.MuraCam1:
                    case CameraName.SquintMuraCam1:
                    case CameraName.MultiAngle1:
                        if (IsCanMove(AxisName.DetectModule1_X, camBasePointY - OneCell.Y))
                        {
                            Motion.MoveAbsSync(StationEnum.DetectGantry, AxisName.DetectModule1_X, camBasePointY - OneCell.Y, 60);
                            if (cameraName == CameraName.MuraCam1)
                            {
                                Motion.MoveAbsSync(StationEnum.DetectGantry, AxisName.DetectModule1_Rotate, DetectGtyPosName.DirectMuraGrabPos, 60);
                            }
                            else if (cameraName == CameraName.SquintMuraCam1)
                            {
                                Motion.MoveAbsSync(StationEnum.DetectGantry, AxisName.DetectModule1_Rotate, DetectGtyPosName.SquintMuraGrabPos, 60);
                            }
                        }
                        else
                        {
                            MessageBox.Show("检测模组1X轴目标位置会和检测模组2X轴相撞，禁止移动");
                        }
                        //LogManager.GetLogger("FrmBaseTeach").Info($"camBasePointY:{camBasePointY}, OneCell.Y:{ OneCell.Y},TotalDis:{camBasePointY - OneCell.Y}");
                        break;
                    case CameraName.AoiCam2:
                    case CameraName.MuraCam2:
                    case CameraName.SquintMuraCam2:
                    case CameraName.MultiAngle2:
                        if (IsCanMove(AxisName.DetectModule2_X, camBasePointY + OneCell.Y))
                        {
                            Motion.MoveAbsSync(StationEnum.DetectGantry, AxisName.DetectModule2_X, camBasePointY + OneCell.Y, 60);
                            if (cameraName == CameraName.MuraCam2)
                            {
                                Motion.MoveAbsSync(StationEnum.DetectGantry, AxisName.DetectModule2_Rotate, DetectGtyPosName.DirectMuraGrabPos, 60);
                            }
                            else if (cameraName == CameraName.SquintMuraCam2)
                            {
                                Motion.MoveAbsSync(StationEnum.DetectGantry, AxisName.DetectModule2_Rotate, DetectGtyPosName.SquintMuraGrabPos, 60);
                            }
                        }
                        else
                        {
                            MessageBox.Show("检测模组2X轴目标位置会和检测模组1X轴相撞，禁止移动");
                        }
                       // LogManager.GetLogger("FrmBaseTeach").Info($"camBasePointY:{camBasePointY}, OneCell.Y:{ OneCell.Y},TotalDis:{camBasePointY + OneCell.Y}");
                        break;
                    default:
                        break;
                }
                CamZMove(cameraName);
            }
        }
        private void CamZMove(CameraName cameraName)
        {
            switch (cameraName)
            {
                case CameraName.AoiCam1:
                    Motion.MoveAbsSync(StationEnum.DetectGantry, AxisName.DetectModule1_Z1, StageIndex == 0 ? DetectGtyPosName.Stg1AoiCam1GrabPos : DetectGtyPosName.Stg2AoiCam1GrabPos, 60);
                    break;
                case CameraName.MuraCam1:
                case CameraName.SquintMuraCam1:
                    Motion.MoveAbsSync(StationEnum.DetectGantry, AxisName.DetectModule1_Z1, StageIndex == 0 ? DetectGtyPosName.Stg1MuraCam1GrabPos : DetectGtyPosName.Stg2MuraCam1GrabPos, 60);
                    break;
                case CameraName.MultiAngle1:
                    Motion.MoveAbsSync(StationEnum.DetectGantry, AxisName.DetectModule1_Z2, StageIndex == 0 ? DetectGtyPosName.Stg1MultiAngle1GrabPos : DetectGtyPosName.Stg2MultiAngle1GrabPos, 60);
                    break;
                case CameraName.AoiCam2:
                    Motion.MoveAbsSync(StationEnum.DetectGantry, AxisName.DetectModule2_Z1, StageIndex == 0 ? DetectGtyPosName.Stg1AoiCam2GrabPos : DetectGtyPosName.Stg2AoiCam2GrabPos, 60);
                    break;
                case CameraName.MuraCam2:
                case CameraName.SquintMuraCam2:
                    Motion.MoveAbsSync(StationEnum.DetectGantry, AxisName.DetectModule2_Z1, StageIndex == 0 ? DetectGtyPosName.Stg1MuraCam2GrabPos : DetectGtyPosName.Stg2MuraCam2GrabPos, 60);
                    break;
                case CameraName.MultiAngle2:
                     Motion.MoveAbsSync(StationEnum.DetectGantry, AxisName.DetectModule2_Z2, StageIndex == 0 ? DetectGtyPosName.Stg1MultiAngle2GrabPos : DetectGtyPosName.Stg2MultiAngle2GrabPos, 60);
                    break;
                default:
                    break;
            }
        }
        private void CamMoveToTargetCrd(CameraName cameraName)
        {
            double detectGantryBasePointX = BasePointHelper.GetDetectStaBasePt(cameraName, StageIndex);
            double camBasePointY = BasePointHelper.GetDetectStaBasePt(cameraName, StageIndex,false);
            double reviewCrdX, reviewCrdY;
            bool ret = double.TryParse(tbxXCrd.Text, out reviewCrdX);
            ret &= double.TryParse(tbxYCrd.Text, out reviewCrdY);
         
            if (ret)
            {
                if (IsCanMove(AxisName.DetectGantry_Y1, detectGantryBasePointX + reviewCrdX))
                {
                     Motion.MoveAbsSync(StationEnum.DetectGantry, AxisName.DetectGantry_Y1, detectGantryBasePointX + reviewCrdX, 60, true);
                    switch (cameraName)
                    {
                        case CameraName.AoiCam1:
                        case CameraName.MuraCam1:
                        case CameraName.SquintMuraCam1:
                        case CameraName.MultiAngle1:
                            if (IsCanMove(AxisName.DetectModule1_X, camBasePointY + reviewCrdY))
                            {
                                Motion.MoveAbsSync(StationEnum.DetectGantry, AxisName.DetectModule1_X, camBasePointY + reviewCrdY, 60);
                                if (cameraName == CameraName.MuraCam1)
                                {
                                    Motion.MoveAbsSync(StationEnum.DetectGantry, AxisName.DetectModule1_Rotate, DetectGtyPosName.DirectMuraGrabPos, 60);
                                }
                                else if (cameraName == CameraName.SquintMuraCam1)
                                {
                                    Motion.MoveAbsSync(StationEnum.DetectGantry, AxisName.DetectModule1_Rotate, DetectGtyPosName.SquintMuraGrabPos, 60);
                                }
                            }
                            else
                            {
                                MessageBox.Show("检测模组1X轴目标位置会和检测模组2X轴相撞，禁止移动");
                            }
                        
                            break;
                        case CameraName.AoiCam2:
                        case CameraName.MuraCam2:
                        case CameraName.SquintMuraCam2:
                        case CameraName.MultiAngle2:
                            if (IsCanMove(AxisName.DetectModule2_X, camBasePointY - reviewCrdY))
                            {
                                Motion.MoveAbsSync(StationEnum.DetectGantry, AxisName.DetectModule2_X, camBasePointY - reviewCrdY, 60);
                                if (cameraName == CameraName.MuraCam2)
                                {
                                    Motion.MoveAbsSync(StationEnum.DetectGantry, AxisName.DetectModule2_Rotate, DetectGtyPosName.DirectMuraGrabPos, 60);
                                }
                                else if (cameraName == CameraName.SquintMuraCam2)
                                {
                                    Motion.MoveAbsSync(StationEnum.DetectGantry, AxisName.DetectModule2_Rotate, DetectGtyPosName.SquintMuraGrabPos, 60);
                                }
                            }
                            else
                            {
                                MessageBox.Show("检测模组2X轴目标位置会和检测模组1X轴相撞，禁止移动");
                            }
                
                            break;
                        default:
                            break;
                    }
                   
                    CamZMove(cameraName);                 
                }              
            }
            else
            {
                MessageBox.Show("输入坐标有误，请重新输入！", "Error");
            }

        }
        private void CamMoveToMark(CameraName cameraName)
        {
            double detectGantryBasePointX = BasePointHelper.GetDetectStaBasePt(cameraName, StageIndex);
            double camBasePointY = BasePointHelper.GetDetectStaBasePt(cameraName, StageIndex, false);

            if (IsCanMove(AxisName.DetectGantry_Y1, detectGantryBasePointX + Param.Panel.RightMarkOffsetX))
            {
                Motion.MoveAbsSync(StationEnum.DetectGantry, AxisName.DetectGantry_Y1, detectGantryBasePointX - Param.Panel.RightMarkOffsetX, 60, true);

                switch (cameraName)
                {
                    case CameraName.AoiCam1:
                    case CameraName.MuraCam1:
                    case CameraName.SquintMuraCam1:
                    case CameraName.MultiAngle1:
                        Motion.MoveAbsSync(StationEnum.DetectGantry, AxisName.DetectModule1_X, camBasePointY + Param.Panel.RightMarkOffsetY, 60);
                        if (cameraName == CameraName.MuraCam1)
                        {
                            Motion.MoveAbsSync(StationEnum.DetectGantry, AxisName.DetectModule1_Rotate, DetectGtyPosName.DirectMuraGrabPos, 60);
                        }
                        else if (cameraName == CameraName.SquintMuraCam1)
                        {
                            Motion.MoveAbsSync(StationEnum.DetectGantry, AxisName.DetectModule1_Rotate, DetectGtyPosName.SquintMuraGrabPos, 60);
                        }
                        break;
                    case CameraName.AoiCam2:
                    case CameraName.MuraCam2:
                    case CameraName.SquintMuraCam2:
                    case CameraName.MultiAngle2:
                        Motion.MoveAbsSync(StationEnum.DetectGantry, AxisName.DetectModule2_X, camBasePointY - Param.Panel.RightMarkOffsetY2, 60);
                        if (cameraName == CameraName.MuraCam2)
                        {
                            Motion.MoveAbsSync(StationEnum.DetectGantry, AxisName.DetectModule2_Rotate, DetectGtyPosName.DirectMuraGrabPos, 60);
                        }
                        else if (cameraName == CameraName.SquintMuraCam2)
                        {
                            Motion.MoveAbsSync(StationEnum.DetectGantry, AxisName.DetectModule2_Rotate, DetectGtyPosName.SquintMuraGrabPos, 60);
                        }
                        break;
                    default:
                        break;
                }
                CamZMove(cameraName);
            }
        }
        private bool IsCanMove(AxisName axisName,double targetPos)
        {
            bool ret = false;
            MoveArgs args = new MoveArgs { Axis = AxisInfoHelper.GetAxisInfoByName(axisName) };
            args.TargetPos = targetPos;
            StationEnum stationName=StationEnum.DetectGantry;
            if (axisName == AxisName.ProbeGantry_Y1)
            {
                stationName = StationEnum.ProbeGantry;
            }
            else if (axisName == AxisName.DetectGantry_Y1|| axisName==AxisName.DetectModule1_X || axisName == AxisName.DetectModule2_X)
            {
                stationName = StationEnum.DetectGantry;
            }
            if (targetPos < Motion.GetAxisActPos(stationName, axisName))
            {
                args.Dir = -1;
            }
            else
            {
                args.Dir = 1;
            }
            ret = InterlockMonitor.Instance.CheckIsCanMove(args);
            return ret;
        }
        //int SelectColumn = -2,GlassNum=  -1;
        private void btnDetectGantryMove_Click(object sender, EventArgs e)
        {
            if (IsInStopSts() == false) { return; }
            double targetPos = 0;
            if (rbtHomePos.Checked)
            {
                targetPos = 0;
            }         
            else if (rbtColumnPos.Checked)
            {
                targetPos = BasePointHelper.GetGantryDetectBasePoint(GantryName.Detect, StageIndex, ColumnIndex);
            }
            if (IsCanMove(AxisName.DetectGantry_Y1, targetPos) ==false)
            {
                return;
            }
            tbxDetectGtyPrfPos.Text = targetPos.ToString("0.000");
            Task.Factory.StartNew(()=> {
                Task<bool> t = Motion.MoveAbsSync(StationEnum.DetectGantry, AxisName.DetectGantry_Y1, targetPos, 60, true);
                if (t.Result==false)
                {
                    MessageBox.Show("检测龙门移动失败");
                }
            });
       
        }

        private void btnProbeGantryMove_Click(object sender, EventArgs e)
        {
            if (IsInStopSts() == false) { return; }
            double targetPos = 0;
            if (rbtHomePos.Checked)
            {
                targetPos = 0;
            }
            else if (rbtMarkPos.Checked)
            {
                targetPos = AxisInfoHelper.GetPosInfoByName(AxisName.ProbeGantry_Y1, StageIndex == 0 ? ProbeGantryPosName.Stg1ProbeGantryAlignPos : ProbeGantryPosName.Stg2ProbeGantryAlignPos).Pos;
            }
            else if (rbtColumnPos.Checked)
            {
                targetPos = BasePointHelper.GetGantryDetectBasePoint(GantryName.Probe, StageIndex, ColumnIndex);
            }
         
            if (IsCanMove(AxisName.ProbeGantry_Y1, targetPos) == false)
            {
                return;
            }
            tbxProbeGtyPrfPos.Text = targetPos.ToString("0.000");
            Task.Factory.StartNew(() => {
                Task<bool> t = Probe.StageStopContactSync(true);
                if (t.Result == false)
                {
                    MessageBox.Show("压头抬升失败");
                    return;
                }
                t = Motion.MoveAbsSync(StationEnum.ProbeGantry, AxisName.ProbeGantry_Y1, targetPos, 60);
                if (t.Result == false)
                {
                    MessageBox.Show("Probe龙门移动失败");
                }
            });
        }
    
        private void btnAutoAlign_Click(object sender, EventArgs e)
        {
            if (IsInStopSts() == false) { return; }
            Task.Factory.StartNew(() => {
                CylinderAlignStep = 1;
                while (CylinderAlignStep>0)
                {
                    AutoAlign(StageIndex);
                    Thread.Sleep(50);
                }
                if (CylinderAlignStep==0)
                {
                    MessageBox.Show("规整完成");
                }
            });
        }
        int CylinderAlignStep = -1;
        Delay Delay = new Delay();
        private void AutoAlign(int stageIndex)
        {
            switch (CylinderAlignStep)
            {
                case 1:
                    {
                        //气缸松开
                        for (int i = 0; i < 8; i++)
                        {
                            string temp  = $"Stage{stageIndex+1}HorizontalCyl{i + 1}";
                            Motion.Cylinder(temp, true);
                        }
                        CylinderAlignStep = 10;
                        break;
                    }
                case 10:
                    {
                        //判断气缸是否松开到位
                        if (Stage.IsClyUnClamp(stageIndex))
                        {
                            //气缸上升
                            for (int i = 0; i < 8; i++)
                            {
                                string temp = $"Stage{stageIndex + 1}VerticalCyl{i + 1}";
                                Motion.Cylinder(temp, true);
                            }
                            CylinderAlignStep = 20;
                        }
                        break;
                    }
                case 20:
                    {
                        //判断气缸是否上升到位
                        if (Stage.IsClyUp(stageIndex))
                        {
                            //气缸加紧
                            for (int i = 0; i < 8; i++)
                            {
                                string temp = $"Stage{stageIndex + 1}HorizontalCyl{i + 1}";
                                Motion.Cylinder(temp, false);
                            }
                            CylinderAlignStep = 30;
                        }
                        break;
                    }
                case 30:
                    {
                        //判断气缸是否夹紧到位
                        if (Stage.IsClyClamp(stageIndex))
                        {
                            Delay.Start(2);//延时2秒
                            CylinderAlignStep = 40;
                        }
                        break;
                    }
                case 40:
                    {
                        if (Delay.TimeUp())
                        {
                            //气缸松开
                            for (int i = 0; i < 8; i++)
                            {
                                string temp = $"Stage{stageIndex + 1}HorizontalCyl{i + 1}";
                                Motion.Cylinder(temp, true);
                            }
                            CylinderAlignStep = 50;
                        }
                        break;
                    }
                        
                case 50:
                    {
                        //判断是否松开
                        if (Stage.IsClyUnClamp(stageIndex))
                        {
                            //气缸下降
                            for (int i = 0; i < 8; i++)
                            {
                                string temp = $"Stage{stageIndex + 1}VerticalCyl{i + 1}";
                                Motion.Cylinder(temp, false);
                            }
                            CylinderAlignStep = 60;
                        }
                        break;
                    }
                case 60:
                    {
                        //判断是否下降到位
                        if (Stage.IsClyDown(stageIndex))
                        {
                            //气缸夹紧
                            for (int i = 0; i < 8; i++)
                            {
                                string temp = $"Stage{stageIndex + 1}HorizontalCyl{i + 1}";
                                Motion.Cylinder(temp, false);
                            }
                            CylinderAlignStep =70;
                        }
                        break;
                    }
                case 70:
                    {
                        //判断是否夹紧
                        if (Stage.IsClyClamp(stageIndex))
                        {
                            CylinderAlignStep = 0;
                        }
                        break;
                    }
            }
       
        }

        private void btnAlignStop_Click(object sender, EventArgs e)
        {
            CylinderAlignStep = -1;
        }


        private void btnMoveToAutoChangePos_Click(object sender, EventArgs e)
        {
            if (IsInStopSts() == false) { return; }
            Task.Factory.StartNew(() => {
                ProbeGantry prb = StationManager.Instance.GetChild("ProbeGantry") as ProbeGantry;
                var t = prb.UvwMoveAbsSync(ProbeGantryPosName.AutoChgPos, StageIndex);
                if (t.Result)
                {
                    MessageBox.Show("Uvw移动移动到自动更换Probe位置完成");
                }
            });
        }

        private void btnMoveToContactPos_Click(object sender, EventArgs e)
        {
            if (IsInStopSts() == false) { return; }
            int stageIndex = cbxStageIndex.SelectedIndex;
            Task.Factory.StartNew(() => {
                ProbeGantry prb = StationManager.Instance.GetChild("ProbeGantry") as ProbeGantry;
             
                var t = prb.UvwMoveAbsSync(ProbeGantryPosName.DetectPos, stageIndex);
                if (t.Result)
                {
                    MessageBox.Show("Uvw移动移动到压接位置完成");
                }
            });
        }

        private void btnAlignTest_Click(object sender, EventArgs e)
        {
            if (IsCanRun()==false)
            {
                return;
            }
            string str = $"请确认是否执行平台{StageIndex+1}第{ColumnIndex+1}列自动对位";
            if (MessageBox.Show(str, "询问", MessageBoxButtons.OKCancel)==DialogResult.OK)
            {
                Global.AutoPosInfo = new Entities.AutoPosInfo { GlassNum=StageIndex,ColumnNum=ColumnIndex};
                Global.IsAutoPosOnly = true;
                StationManager.Instance.Start();
            }
        }
        private bool IsCanRun()
        {
            if (IsInStopSts() == false) { return false; }
            bool ret = true;
            if (MotionGlobal.IsHomeOK == false)
            {
                MessageBox.Show("设备原点丢失，请重新回零");
                ret = false;
            }
            return ret;
        }

        private void cmxColumn_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmxPanelNum.Items.Clear();
            int colNum = cmxColumn.SelectedIndex;
            for (int i = 0; i < Param.Panel.CellCountY; i++)
            {
                cmxPanelNum.Items.Add(Global.GetCellID(colNum,i));
            }
            cmxPanelNum.SelectedIndex = 0;
            ColumnIndex = cmxColumn.SelectedIndex;
        }

        private void btnIRMoveToCrdPos_Click(object sender, EventArgs e)
        {
            if (IsInStopSts() == false) { return; }
            int camIndex = cmxCamList.SelectedIndex;
            CamMoveToTargetCrd((CameraName)camIndex);
        }

        private void btnBrakeOff_Click(object sender, EventArgs e)
        {
            if (Motion.GetDigitalOutput(OutputName.UvwBrake1)|| Motion.GetDigitalOutput(OutputName.UvwBrake2))
            {
                btnBrakeOff.Text = "刹车抱紧";
                Motion.SetDigitalOutput(OutputName.UvwBrake1,false);
                Motion.SetDigitalOutput(OutputName.UvwBrake2, false);
            }
            else
            {
                btnBrakeOff.Text = "刹车松开";
                Motion.SetDigitalOutput(OutputName.UvwBrake1, true);
                Motion.SetDigitalOutput(OutputName.UvwBrake2, true);
            }
        }

        private void btnMoveToBasePos_Click(object sender, EventArgs e)
        {
            if (IsInStopSts() == false) { return; }
            int stageIndex = cbxStageIndex.SelectedIndex;
            Task.Factory.StartNew(()=> {
              var t=  Probe.UvwMoveAbsSync(ProbeGantryPosName.DetectPos, stageIndex);
                if (t.Result)
                {
                    var stage1T = AxisInfoHelper.GetAxisInfoByName(AxisName.Stage1_T);
                    var stage2T = AxisInfoHelper.GetAxisInfoByName(AxisName.Stage2_T);
                    if (stage1T .ActPos> stage1T.PosInfoDic["Neg180DegPos"].Pos-1&& stage1T.ActPos < stage1T.PosInfoDic["Neg180DegPos"].Pos + 1)
                    {
                        if (stage2T.ActPos > stage2T.PosInfoDic["Neg180DegPos"].Pos - 1 && stage2T.ActPos < stage2T.PosInfoDic["Neg180DegPos"].Pos + 1)
                        {
                            var t1=Motion.MoveAbsSync(StationEnum.DualStage,AxisName.Stage1_T, "Neg180DegPos",10);
                            var t2 = Motion.MoveAbsSync(StationEnum.DualStage, AxisName.Stage2_T, "Neg180DegPos", 10);
                            if (t1.Result&&t2.Result)
                            {
                                t1=Motion.MoveAbsSync(StationEnum.DualStage, AxisName.Stage1_Y, "DetectingPos", 10);
                               t2= Motion.MoveAbsSync(StationEnum.DualStage, AxisName.Stage2_Y, "DetectingPos", 10);
                                if (t1.Result && t2.Result)
                                {
                                    MessageBox.Show("Uvw和平台回基准位完成");
                                }
                                else
                                {
                                    MessageBox.Show("Uvw和平台回基准位失败");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Uvw和平台回基准位失败");
                            }
                        }
                        else
                        {
                            StageToDetectPos();
                        }
                    }
                    else
                    {
                        StageToDetectPos();
                    }
                }
                else
                {
                    MessageBox.Show("Uvw和平台回基准位失败");
                }
            });
        }
        private void StageToDetectPos()
        {
            Task<bool> t1 = Motion.MoveAbsSync(StationEnum.DualStage, AxisName.Stage1_Y, StagePosName.RotatePos, 60);
            Task<bool> t2 = Motion.MoveAbsSync(StationEnum.DualStage, AxisName.Stage2_Y, StagePosName.RotatePos, 60);
            if (t1.Result && t2.Result)
            {
                t1 = Motion.MoveAbsSync(StationEnum.DualStage, AxisName.Stage1_T, StagePosName.Neg180DegPos, 60);
                t2 = Motion.MoveAbsSync(StationEnum.DualStage, AxisName.Stage2_T, StagePosName.Neg180DegPos, 60);
            }
            if (t1.Result && t2.Result)
            {
                t1 = Motion.MoveAbsSync(StationEnum.DualStage, AxisName.Stage1_Y, StagePosName.DetectingPos, 60);
                t2 = Motion.MoveAbsSync(StationEnum.DualStage, AxisName.Stage2_Y, StagePosName.DetectingPos, 60);
            }
        }

  
        private void pbxGlass2Column1_MouseDoubleClick(object sender, EventArgs e)
        {
            PictureBox pbx = sender as PictureBox;
            pbx.Image = Properties.Resources.col;
        }

        private void cbxProbeHeadInterlock_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxProbeHeadInterlock.Checked == false)
            {
                Task.Factory.StartNew(() => {
                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    while (true)
                    {
                        if (sw.ElapsedMilliseconds > 15 * 1000)
                        {
                            cbxProbeHeadInterlock.Invoke(new Action(() => { cbxProbeHeadInterlock.Checked = true; }));
                            break;
                        }
                        Thread.Sleep(10);
                    }

                });
            }
        }

        int[] LastColNum = new int[] { -1, -1 },LastGlassNum=new int[] { -1,-1};

        private void btnManunalAlign_Click(object sender, EventArgs e)
        {
            if (InterlockMonitor.Instance.IsContactZAtSafePos()==false)
            {
                MessageBox.Show("压头不在安全位置，禁止对位");
            }
            string glassID = DateTime.Now.ToLongTimeString();
            Task.Factory.StartNew(()=> {
                Global.SignalProvider.StartAutoPos(StageIndex, glassID);
                if (Global.SignalProvider.AutoPosResult)
                {
                    MessageBox.Show("对位完成");
                }
                else
                {
                    MessageBox.Show("对位失败");
                }
            });
           
        }

        private void btnDetect_Click(object sender, EventArgs e)
        {
            int detectIndex = cmxCamList.SelectedIndex;
            int pcIndex = -1;
            int cellIndex = cmxPanelNum.SelectedIndex;
            DetectType detectType = DetectType.Aoi;
            switch (detectIndex)
            {
                case 0:
                    {
                        pcIndex = 0;
                        detectType = DetectType.Aoi;
                        break;
                    }
                case 1:
                    {
                        pcIndex = 0;
                        detectType = DetectType.Mura;
                        break;
                    }
                case 2:
                    {
                        pcIndex = 0;
                        detectType = DetectType.SquintMura;
                        break;
                    }
                case 3:
                    {
                        pcIndex = 0;
                        detectType = DetectType.MultiAngle;
                        break;
                    }
                case 4:
                    {
                        pcIndex = 1;
                        detectType = DetectType.Aoi;
                        break;
                    }
                case 5:
                    {
                        pcIndex = 1;
                        detectType = DetectType.Mura;
                        break;
                    }
                case 6:
                    {
                        pcIndex = 1;
                        detectType = DetectType.SquintMura;
                        break;
                    }
                case 7:
                    {
                        pcIndex = 1;
                        detectType = DetectType.MultiAngle;
                        break;
                    }
            }
            var startTime = DateTime.Now;
            string glassID = startTime.ToString("yyyyMMddHHmmss");
            DetectReqArgs args = new DetectReqArgs();
            args.DetectType = detectType.ToString();
            args.GlassID = glassID;
            args.GlassNum = StageIndex;
            args.ColumnNum = ColumnIndex;
            args.PCIndex = pcIndex;//0：AoiPC1，1：AoiPC2
            args.CellNum = cellIndex;
            args.CellID = Global.GetCellID(args.ColumnNum, cellIndex);

            new OneCellDetectInfoDal().Insert(new SaturnDB.Model.OneCellDetectModel { GlassID = glassID, CellID = args.CellID , EQPInTime = startTime, GlassNum = args.GlassNum, CellNum = args.CellNum, ColumnNum = args.ColumnNum });
            new CellStartTimeDal().Insert(new SaturnDB.Model.CellStartTimeModel { PanelID = glassID, StartTime = startTime });
            new TotalInfoDal().Insert(new SaturnDB.Model.TotalInfoModel { GlassID = glassID, EQPInTime = startTime, RecipeID = RecipeManager.CurRecipe.RecipeID, StartTime = startTime });
            AoiPC.Instance.PowerControlReq(false, pcIndex);
            Thread.Sleep(1000);
            AoiPC.Instance.PowerControlReq(true, pcIndex);
            Task.Factory.StartNew(() => {
                try
                {
                    TesterPC.Instance.TesterModeSwitch("Cell", pcIndex, cellIndex);
                    Thread.Sleep(1000);
                    AoiPC.Instance.DetectReq(args);//0：AoiPC1，1：AoiPC2
                }
                catch (Exception ex)
                {

                }
            });
        }
        double ProbeGtyActPos=0, DetectGtyActPos = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (this.Visible)
                {
                    ProbeGtyActPos = Motion.GetAxisActPos(StationEnum.ProbeGantry, AxisName.ProbeGantry_Y1);
                    DetectGtyActPos = Motion.GetAxisActPos(StationEnum.DetectGantry, AxisName.DetectGantry_Y1);
                    tbxProbeGtyActPos.Text = ProbeGtyActPos.ToString("0.000");
                    tbxDetectGtyActPos.Text = DetectGtyActPos.ToString("0.000");
                    if (ProbeGtyPosPbxDic.Keys.Contains(Math.Round(ProbeGtyActPos, 0)))
                    {
                        foreach (var item in ProbeGtyPosPbxDic)
                        {
                            if (item.Value.Tag.ToString() != "DetectGantry"|| item.Value.Tag.ToString() == "ProbeGantry")
                            {
                                item.Value.Image = Properties.Resources.col;
                            }
                        }
                        //if (ProbeGtyPosPbxDic[Math.Round(ProbeGtyActPos, 0)].Tag.ToString() == "DetectGantry")
                        //{
                        //    ProbeGtyPosPbxDic[Math.Round(ProbeGtyActPos, 0)].Image = Properties.Resources.DetectProbeGantry;
                        //    ProbeGtyPosPbxDic[Math.Round(ProbeGtyActPos, 0)].Tag = "DetectProbeGantry";
                        //}
                        //else
                        {
                            ProbeGtyPosPbxDic[Math.Round(ProbeGtyActPos, 0)].Image = Properties.Resources.ProbeGantry;
                            ProbeGtyPosPbxDic[Math.Round(ProbeGtyActPos, 0)].Tag = "ProbeGantry";
                        }
                    }
                    if (DetectGtyPosPbxDic.Keys.Contains(Math.Round(DetectGtyActPos, 0)))
                    {
                        foreach (var item in DetectGtyPosPbxDic)
                        {
                            if (item.Value.Tag.ToString() != "ProbeGantry" || item.Value.Tag.ToString() == "DetectGantry")
                            {
                                item.Value.Image = Properties.Resources.col;
                            }
                        }
                        DetectGtyPosPbxDic[Math.Round(DetectGtyActPos, 0)].Image = Properties.Resources.DetectGantry;
                        DetectGtyPosPbxDic[Math.Round(DetectGtyActPos, 0)].Tag = "DetectGantry";
                    }
                }
             
            }
            catch (Exception ex)
            {

            }
        }

        private void FrmBaseTeach_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                InitDic();
            }
            this.timer1.Enabled = this.Visible;
        }

        private void btnCamMoveToMarkPos_Click(object sender, EventArgs e)
        {
            if (IsInStopSts() == false) { return; }
            int camIndex = cmxCamList.SelectedIndex;
            CamMoveToMark((CameraName)camIndex);
        }

        private void btnPowerOn_Click(object sender, EventArgs e)
        {
            int pcIndex = -1;
            int detectIndex = cmxCamList.SelectedIndex;
            pcIndex = detectIndex < 4 ? 0 : 1;
            int cellIndex = cmxPanelNum.SelectedIndex;
            TesterPC.Instance.TesterModeSwitch("Cell", pcIndex, cellIndex);
            Thread.Sleep(1000);
            AoiPC.Instance.PowerControlReq(true, pcIndex);
        }

        private void btnPowerOff_Click(object sender, EventArgs e)
        {
            int pcIndex = -1;
            int detectIndex = cmxCamList.SelectedIndex;
            pcIndex = detectIndex < 4 ? 0 : 1;
            AoiPC.Instance.PowerControlReq(false, pcIndex);
        }

        private void btnNextSwitch_Click(object sender, EventArgs e)
        {
            if (cmxPatternList.SelectedItem==null)
            {
                MessageBox.Show("请选择画面");
                return;
            }
            int pcIndex = -1;
            int detectIndex = cmxCamList.SelectedIndex;
            pcIndex = detectIndex < 4 ? 0 : 1;
            int ptnNum = cmxPatternList.SelectedIndex + 1;
            AoiPC.Instance.SwitchPatternReq(pcIndex, ptnNum);
        }

        private bool IsInStopSts()
        {
            bool ret = false;
            var sts = StationManager.Instance.GetCurrentEQPSts();
            if (sts == HTPCBase.Views.EqpSts.STOP)
            {
                ret = true;
            }
            else
            {
                MessageBox.Show("设备不在停止状态，禁止操作");
            }
            return ret;
        }
        private void InitDic()
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action(InitDic));
            }
            else
            {
                try
                {
                    string pbxName = string.Empty;
                    ProbeGtyPosPbxDic = new Dictionary<double, PictureBox>();
                    DetectGtyPosPbxDic = new Dictionary<double, PictureBox>();
                    ProbeGtyPosPbxDic.Add(0, pbxHomePos1);
                    DetectGtyPosPbxDic.Add(0, pbxHomePos2);

                    var pos = AxisInfoHelper.GetPosInfoByName(AxisName.ProbeGantry_Y1, ProbeGantryPosName.Stg1ProbeGantryAlignPos).Pos;
                    ProbeGtyPosPbxDic.Add(Math.Round(pos,0), pbxMarkPos1);
                    pos = AxisInfoHelper.GetPosInfoByName(AxisName.ProbeGantry_Y1, ProbeGantryPosName.Stg2ProbeGantryAlignPos).Pos;
                    ProbeGtyPosPbxDic.Add(Math.Round(pos,0), pbxMarkPos2);

                    for (int i = Param.Panel.CellCountX - 1; i >= 0; i--)
                    {
                        pos = BasePointHelper.GetGantryDetectBasePoint(GantryName.Probe, 0, i);
                        pbxName = $"pbx1_{i + 1}";
                        PictureBox pbx = Controls.Find(pbxName, true)[0] as PictureBox;
                        ProbeGtyPosPbxDic.Add(Math.Round(pos,0), pbx);

                        pos = BasePointHelper.GetGantryDetectBasePoint(GantryName.Detect, 0, i);                
                        DetectGtyPosPbxDic.Add(Math.Round(pos, 0), pbx);

                        pos = BasePointHelper.GetGantryDetectBasePoint(GantryName.Probe, 1, i);
                        pbxName = $"pbx2_{i + 1}";
                        pbx = Controls.Find(pbxName, true)[0] as PictureBox;
                        ProbeGtyPosPbxDic.Add(Math.Round(pos,0), pbx);

                        pos = BasePointHelper.GetGantryDetectBasePoint(GantryName.Detect, 1, i);
                        DetectGtyPosPbxDic.Add(Math.Round(pos,0), pbx);
                    }
                }
                catch (Exception ex)
                {

                }
              
            }
  
        }
    }
}
