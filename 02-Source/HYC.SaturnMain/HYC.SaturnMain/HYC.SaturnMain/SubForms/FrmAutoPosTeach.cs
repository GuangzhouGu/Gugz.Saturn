using HYC.HTPCBase.Args;
using HYC.HTPCBase.Config;
using HYC.HTPCBase.Motion;
using HYC.SaturnMain.Devices.Motion;
using HYC.SaturnMain.Entities;
using HYC.SaturnMain.Station;
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
    public partial class FrmAutoPosTeach : FormPopBase
    {
        public FrmAutoPosTeach()
        {
            InitializeComponent();
            this.Title = "相机移动";
            this.TopMost = true;
            this.Location = new Point(100,100);
        }
        MotionManager MotionManager = MotionManager.GetInstance();
        private void FrmAxisMove_Load(object sender, EventArgs e)
        {
            cmxCameraList.SelectedIndex = 0;
            cmxStageList.SelectedIndex = 0;
        }
        string CameraName = string.Empty;
        AxisInfo AxisInfo = null;
    

        private void cmxCameraList_SelectedIndexChanged(object sender, EventArgs e)
        {
            CameraName = cmxCameraList.SelectedItem.ToString();
        }
        private AxisName GetAxis(string dir,out double pos)
        {
            var ret = double.TryParse(tbxMoveDis.Text, out pos);
            AxisName axisName = AxisName.None;
            if (ret)
            {
                switch (CameraName)
                {
                    case "对位相机1":
                    case "Contact相机":
                        {
                            switch (dir)
                            {
                                case "Back":
                                case "Front":
                                    {
                                        axisName = AxisName.ProbeGantryAlig_X1;
                                        if (dir=="Back")
                                        {
                                            pos = -pos;
                                        }
                                        break;
                                    }
                                case "Left":
                                case "Right":
                                    {
                                        axisName = AxisName.ProbeGantry_Y1;
                                        if (dir == "Left")
                                        {
                                            pos = -pos;
                                        }
                                        if (IsGantryCanMove(axisName,pos)==false)
                                        {
                                            return AxisName.None;
                                        }
                                        break;
                                    }
                            }
                            break;
                        }
                    case "对位相机2":
                        {
                            switch (dir)
                            {
                                case "Back":
                                case "Front":
                                    {
                                        axisName = AxisName.ProbeGantryAlig_X2;
                                        if (dir == "Front")
                                        {
                                            pos = -pos;
                                        }
                                        break;
                                    }
                                case "Left":
                                case "Right":
                                    {
                                        axisName = AxisName.ProbeGantry_Y1;
                                        if (dir == "Left")
                                        {
                                            pos = -pos;
                                        }
                                        if (IsGantryCanMove(axisName, pos) == false)
                                        {
                                            return AxisName.None;
                                        }
                                        break;
                                    }
                            }
                            break;
                        }
                    case "Aoi相机1":
                    case "直视Mura相机1":
                    case "斜视Mura相机1":
                    case "多角度光谱仪1":
                        {
                            switch (dir)
                            {
                                case "Back":
                                case "Front":
                                    {
                                        axisName = AxisName.DetectModule1_X;
                                        if (dir == "Back")
                                        {
                                            pos = -pos;
                                        }
                                        break;
                                    }
                                case "Left":
                                case "Right":
                                    {
                                        axisName = AxisName.DetectGantry_Y1;
                                        if (dir == "Right")
                                        {
                                            pos = -pos;
                                        }
                                        if (IsGantryCanMove(axisName, pos) == false)
                                        {
                                            return AxisName.None;
                                        }
                                        break;
                                    }
                                case "Up":
                                case "Down":
                                    {
                                        axisName = AxisName.DetectModule1_Z1;
                                        if (CameraName== "多角度光谱仪1")
                                        {
                                            axisName = AxisName.DetectModule1_Z2;
                                        }
                                        if (dir == "Up")
                                        {
                                            pos = -pos;
                                        }
                                        break;
                                    }
                            }
                            break;
                        }
                    case "Aoi相机2":
                    case "直视Mura相机2":
                    case "斜视Mura相机2":
                    case "多角度光谱仪2":
                        {
                            switch (dir)
                            {
                                case "Back":
                                case "Front":
                                    {
                                        axisName = AxisName.DetectModule2_X;
                                        if (dir == "Front")
                                        {
                                            pos = -pos;
                                        }
                                        break;
                                    }
                                case "Left":
                                case "Right":
                                    {
                                        axisName = AxisName.DetectGantry_Y1;
                                        if (dir == "Right")
                                        {
                                            pos = -pos;
                                        }
                                        if (IsGantryCanMove(axisName, pos) == false)
                                        {
                                            return AxisName.None;
                                        }
                                        break;
                                    }
                                case "Up":
                                case "Down":
                                    {
                                        axisName = AxisName.DetectModule2_Z1;
                                        if (CameraName == "多角度光谱仪2")
                                        {
                                            axisName = AxisName.DetectModule2_Z2;
                                        }
                                        if (dir == "Up")
                                        {
                                            pos = -pos;
                                        }
                                        break;
                                    }
                            }
                            break;
                        }
                    case "平台1压接轴":
                        {
                            axisName = AxisName.Stage1_ContactZ;
                            if (dir=="Down")
                            {
                                pos = -pos;
                            }
                            if (Math.Abs(pos) > 10)
                            {
                                MessageBox.Show($"{axisName}轴单次移动不能超过10毫米");
                                return AxisName.None;
                            }
                            break;
                        }
                    case "平台2压接轴":
                        {
                            axisName = AxisName.Stage2_ContactZ;
                            if (dir == "Down")
                            {
                                pos = -pos;
                            }
                            if (Math.Abs(pos) > 10)
                            {
                                MessageBox.Show($"{axisName}轴单次移动不能超过10毫米");
                                return AxisName.None;
                            }
                            break;
                        }
                    case "平台1T轴":
                        {
                            axisName = AxisName.Stage1_T;
                            if (dir == "Rotate-")
                            {
                                pos = -pos;
                            }
                            if (Math.Abs(pos)>20)
                            {
                                MessageBox.Show($"{axisName}轴单次移动不能超过20度");
                                return AxisName.None;
                            }
                            break;
                        }
                    case "平台2T轴":
                        {
                            axisName = AxisName.Stage2_T;
                            if (dir == "Rotate-")
                            {
                                pos = -pos;
                            }
                            if (Math.Abs(pos) > 20)
                            {
                                MessageBox.Show($"{axisName}轴单次移动不能超过20度");
                                return AxisName.None;
                            }
                            break;
                        }
                }
            }
            else
            {
                MessageBox.Show("请输入正确的数字");
                return AxisName.None;
            }
            
            return axisName;
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            var axisName = GetAxis("Front", out double pos);
            if (CheckAxisName(axisName))
            {
                AxisInfo = AxisInfoHelper.GetAxisInfoByName(axisName);
                if (AxisInfo != null)
                {
                    MotionManager.MoveRel(AxisInfo.StationName, AxisInfo.AxisName, pos,false);
                }
                else
                {
                    MessageBox.Show("系统中没有选中轴");
                }
            }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            var axisName = GetAxis("Back",out double pos);
            if (CheckAxisName(axisName))
            {
                AxisInfo = AxisInfoHelper.GetAxisInfoByName(axisName);
                if (AxisInfo != null)
                {
                    MotionManager.MoveRel(AxisInfo.StationName, AxisInfo.AxisName, pos, false);
                }
                else
                {
                    MessageBox.Show("系统中没有选中轴");
                }
            }
 
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            var axisName = GetAxis("Left",out double pos);
            if (CheckAxisName(axisName))
            {
                AxisInfo = AxisInfoHelper.GetAxisInfoByName(axisName);
                if (AxisInfo != null)
                {
                    MotionManager.MoveRel(AxisInfo.StationName, AxisInfo.AxisName, pos, false);
                }
                else
                {
                    MessageBox.Show("系统中没有选中轴");
                }
            }
               
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            var axisName = GetAxis("Right",out double pos);
            if (CheckAxisName(axisName))
            {
                AxisInfo = AxisInfoHelper.GetAxisInfoByName(axisName);
                if (AxisInfo != null)
                {
                    MotionManager.MoveRel(AxisInfo.StationName, AxisInfo.AxisName, pos, false);
                }
                else
                {
                    MessageBox.Show("系统中没有选中轴");
                }
            }
        
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (AxisInfo != null)
            {
                MotionManager.Stop(AxisInfo.StationName, AxisInfo.AxisName);
            }

        }

        private void btnZUp_Click(object sender, EventArgs e)
        {
            var axisName = GetAxis("Up", out double pos);
            if (CheckAxisName(axisName,false))
            {
                AxisInfo = AxisInfoHelper.GetAxisInfoByName(axisName);
                if (AxisInfo != null)
                {
                    MotionManager.MoveRel(AxisInfo.StationName, AxisInfo.AxisName, pos, false);
                }
                else
                {
                    MessageBox.Show("系统中没有选中轴");
                }
            }
             
        }

        private void btnZDown_Click(object sender, EventArgs e)
        {
            var axisName = GetAxis("Down", out double pos);
            if (CheckAxisName(axisName, false))
            {
                AxisInfo = AxisInfoHelper.GetAxisInfoByName(axisName);
                if (AxisInfo != null)
                {
                    MotionManager.MoveRel(AxisInfo.StationName, AxisInfo.AxisName, pos, false);
                }
                else
                {
                    MessageBox.Show("系统中没有选中轴");
                }
            }

        }

        private void btnClockRotate_Click(object sender, EventArgs e)
        {
            var axisName = GetAxis("Rotate+", out double pos);
            if (CheckAxisName(axisName, false))
            {
                AxisInfo = AxisInfoHelper.GetAxisInfoByName(axisName);
                if (AxisInfo != null)
                {
                    MotionManager.MoveRel(AxisInfo.StationName, AxisInfo.AxisName, pos, false);
                }
                else
                {
                    MessageBox.Show("系统中没有选中轴");
                }
            }
              
        }

        private void btnReverseRotate_Click(object sender, EventArgs e)
        {
            var axisName = GetAxis("Rotate-", out double pos);
            if (CheckAxisName(axisName, false))
            {
                AxisInfo = AxisInfoHelper.GetAxisInfoByName(axisName);
                if (AxisInfo != null)
                {
                    MotionManager.MoveRel(AxisInfo.StationName, AxisInfo.AxisName, pos, false);
                }
                else
                {
                    MessageBox.Show("系统中没有选中轴");
                }
            }

        }
        private bool IsGantryCanMove(AxisName gantryName, double targetPos)
        {
            bool ret = false;
            MoveArgs args = new MoveArgs { Axis = AxisInfoHelper.GetAxisInfoByName(gantryName) };
            args.TargetPos = targetPos;
            StationEnum stationName = StationEnum.DetectGantry;
            if (gantryName == AxisName.ProbeGantry_Y1)
            {
                stationName = StationEnum.ProbeGantry;
            }
            else if (gantryName == AxisName.DetectGantry_Y1)
            {
                stationName = StationEnum.DetectGantry;
            }
            if (targetPos < MotionManager.GetAxisActPos(stationName, gantryName))
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

        private void btnAlignTest_Click(object sender, EventArgs e)
        {
            if (InterlockMonitor.Instance.IsContactZAtSafePos() == false)
            {
                MessageBox.Show("压头不在安全位置，禁止对位");
            }
            var stageIndex = cmxStageList.SelectedIndex;
            string glassID = DateTime.Now.ToLongTimeString();
            Task.Factory.StartNew(() => {
                Global.SignalProvider.StartAutoPos(stageIndex, glassID);
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

        private void btnSavePosInfo_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否保存对位点位","询问",MessageBoxButtons.OKCancel)==DialogResult.OK)
            {
                var stageIndex = cmxStageList.SelectedIndex;
                try
                {
                  
                    switch (stageIndex)
                    {
                        case 0:
                            {
                                AxisInfoHelper.GetPosInfoByName(AxisName.ProbeGantry_Y1, "Stg1ProbeGantryAlignPos").Pos = double.Parse(txtProbeGantryPos.Text);
                                AxisInfoHelper.GetPosInfoByName(AxisName.ProbeGantryAlig_X1, "Stg1AlignCamGrabPos_X1").Pos = double.Parse(txtAlignX1Pos.Text);
                                AxisInfoHelper.GetPosInfoByName(AxisName.ProbeGantryAlig_X2, "Stg1AlignCamGrabPos_X2").Pos = double.Parse(txtAlignX2Pos.Text);
                                AxisInfoHelper.GetPosInfoByName(AxisName.Stage1_T, "Neg180DegPos").Pos = double.Parse(txtStage1ContactPos.Text);
                                AxisInfoHelper.GetPosInfoByName(AxisName.Stage1_ContactZ, "AlignPos").Pos = double.Parse(txtStage1TPos.Text);
                                break;
                            }
                        case 1:
                            {
                                AxisInfoHelper.GetPosInfoByName(AxisName.ProbeGantry_Y1, "Stg2ProbeGantryAlignPos").Pos = double.Parse(txtProbeGantryPos.Text);
                                AxisInfoHelper.GetPosInfoByName(AxisName.ProbeGantryAlig_X1, "Stg2AlignCamGrabPos_X1").Pos = double.Parse(txtAlignX1Pos.Text);
                                AxisInfoHelper.GetPosInfoByName(AxisName.ProbeGantryAlig_X2, "Stg2AlignCamGrabPos_X2").Pos = double.Parse(txtAlignX2Pos.Text);
                                AxisInfoHelper.GetPosInfoByName(AxisName.Stage2_T, "Neg180DegPos").Pos = double.Parse(txtStage2ContactPos.Text);
                                AxisInfoHelper.GetPosInfoByName(AxisName.Stage2_ContactZ, "AlignPos").Pos = double.Parse(txtStage2TPos.Text);
                                break;
                            }
                    }
                   var ret= MotionConfig.Instance.SaveStationInfo();
                    MessageBox.Show($"保存点位{(ret?"完成":"失败")}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"保存点位失败，异常信息：{ex}");
                }
            }
   
        }

        private void btnReadPos_Click(object sender, EventArgs e)
        {
            txtProbeGantryPos.Text = MotionManager.GetAxisActPos(StationEnum.ProbeGantry, AxisName.ProbeGantry_Y1).ToString();
            txtAlignX1Pos.Text = MotionManager.GetAxisActPos(StationEnum.ProbeGantry, AxisName.ProbeGantryAlig_X1).ToString();
            txtAlignX2Pos.Text = MotionManager.GetAxisActPos(StationEnum.ProbeGantry, AxisName.ProbeGantryAlig_X2).ToString();
            var stageIndex = cmxStageList.SelectedIndex;
            switch (stageIndex)
            {
                case 0:
                    {
                        txtStage1ContactPos.Text = MotionManager.GetAxisActPos(StationEnum.DualStage, AxisName.Stage1_T).ToString();
                        txtStage1TPos.Text = MotionManager.GetAxisActPos(StationEnum.DualStage, AxisName.Stage1_ContactZ).ToString();
                        break;
                    }
                case 1:
                    {
                        txtStage2ContactPos.Text = MotionManager.GetAxisActPos(StationEnum.DualStage, AxisName.Stage2_T).ToString();
                        txtStage2TPos.Text = MotionManager.GetAxisActPos(StationEnum.DualStage, AxisName.Stage2_ContactZ).ToString();
                        break;
                    }
            }
        }
        private void LoadPos()
        {
            try
            {
                var stageIndex = cmxStageList.SelectedIndex;
                switch (stageIndex)
                {
                    case 0:
                        {
                            txtProbeGantryPos.Text = AxisInfoHelper.GetPosInfoByName(AxisName.ProbeGantry_Y1, "Stg1ProbeGantryAlignPos").Pos.ToString();
                            txtAlignX1Pos.Text = AxisInfoHelper.GetPosInfoByName(AxisName.ProbeGantryAlig_X1, "Stg1AlignCamGrabPos_X1").Pos.ToString();
                            txtAlignX2Pos.Text = AxisInfoHelper.GetPosInfoByName(AxisName.ProbeGantryAlig_X2, "Stg1AlignCamGrabPos_X2").Pos.ToString();
                            txtStage1TPos.Text = AxisInfoHelper.GetPosInfoByName(AxisName.Stage1_T, "Neg180DegPos").Pos.ToString();
                            txtStage1ContactPos.Text = AxisInfoHelper.GetPosInfoByName(AxisName.Stage1_ContactZ, "AlignPos").Pos.ToString();
                            break;
                        }
                    case 1:
                        {
                            txtProbeGantryPos.Text = AxisInfoHelper.GetPosInfoByName(AxisName.ProbeGantry_Y1, "Stg2ProbeGantryAlignPos").Pos.ToString();
                            txtAlignX1Pos.Text = AxisInfoHelper.GetPosInfoByName(AxisName.ProbeGantryAlig_X1, "Stg2AlignCamGrabPos_X1").Pos.ToString();
                            txtAlignX2Pos.Text = AxisInfoHelper.GetPosInfoByName(AxisName.ProbeGantryAlig_X2, "Stg2AlignCamGrabPos_X2").Pos.ToString();
                            txtStage2TPos.Text = AxisInfoHelper.GetPosInfoByName(AxisName.Stage2_T, "Neg180DegPos").Pos.ToString();
                            txtStage2ContactPos.Text = AxisInfoHelper.GetPosInfoByName(AxisName.Stage2_ContactZ, "AlignPos").Pos.ToString();
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"LoadPos方法执行失败，异常信息：{ex}");
            }
        }

        private void cmxStageList_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadPos();
        }
        /// <summary>
        /// 判断选中轴是否可以移动，上下左右按钮只能控制龙门轴和相机轴，Z上Z下和旋转只能控制平台轴
        /// </summary>
        /// <param name="axisName"></param>
        /// <param name="isCameraAxis"></param>
        /// <returns></returns>
        private bool CheckAxisName(AxisName axisName,bool isCameraAxis=true)
        {
            bool ret = false;
            if (isCameraAxis)
            {
                if (axisName != AxisName.Stage1_ContactZ && axisName != AxisName.Stage2_ContactZ && axisName != AxisName.Stage1_T && axisName != AxisName.Stage2_T)
                {
                    ret = true;
                }
            }
            else
            {
                if (axisName == AxisName.Stage1_ContactZ ||axisName == AxisName.Stage2_ContactZ || axisName == AxisName.Stage1_T||axisName == AxisName.Stage2_T)
                {
                    ret = true;
                }
            }
            return ret;
        }

        private void btnStage1AlignPos_Click(object sender, EventArgs e)
        {
            if (MotionManager.IsHomeOK(StationEnum.DualStage,AxisName.Stage1_ContactZ))
            {
                Task.Factory.StartNew(()=> {
                    var t=MotionManager.MoveAbsSync(StationEnum.DualStage, AxisName.Stage1_ContactZ, StagePosName.AlignPos, 30);
                    if (t.Result)
                    {
                        MessageBox.Show("平台1压接轴到达对位位置");
                    }
                });
            }
            else
            {
                MessageBox.Show("平台1压接轴原点丢失，禁止移动");
            }
        }

        private void btnStage2AlignPos_Click(object sender, EventArgs e)
        {
            if (MotionManager.IsHomeOK(StationEnum.DualStage, AxisName.Stage2_ContactZ))
            {
                Task.Factory.StartNew(() => {
                   var t= MotionManager.MoveAbsSync(StationEnum.DualStage, AxisName.Stage2_ContactZ, StagePosName.AlignPos, 30);
                    if (t.Result)
                    {
                        MessageBox.Show("平台2压接轴到达对位位置");
                    }
                });
            }
            else
            {
                MessageBox.Show("平台2压接轴原点丢失，禁止移动");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MoveArgs args = new MoveArgs();
            args.Axis = AxisInfoHelper.GetAxisInfoByName(AxisName.ProbeGantryAlig_X1);
            double pos = BasePointHelper.GetContactHeadPos(0, 0);
            args.TargetPos = pos;
            if (InterlockMonitor.Instance.CheckIsCanMove(args))
            {
                MotionManager.MoveAbs(StationEnum.ProbeGantry, AxisName.ProbeGantryAlig_X1, pos);
            }
        }
    }
}
