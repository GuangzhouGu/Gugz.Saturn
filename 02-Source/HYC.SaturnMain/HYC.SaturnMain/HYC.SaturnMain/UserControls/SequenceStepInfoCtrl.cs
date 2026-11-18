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

namespace HYC.SaturnMain.UserControls
{
    public partial class SequenceStepInfoCtrl : UserControl
    {
        public SequenceStepInfoCtrl()
        {
            InitializeComponent();
            SequenceStepManager.OnUpdateSequenceStepTime += SequenceStepManager_OnUpdateSequenceStepTime;
        }

        private void SequenceStepManager_OnUpdateSequenceStepTime(SequenceName arg1, int stageNum, double time)
        {
            try
            {
                if (InvokeRequired)
                {
                    Invoke(new Action<SequenceName, int, double>(SequenceStepManager_OnUpdateSequenceStepTime), arg1, stageNum, time);
                }
                else
                {
                    switch (arg1)
                    {
                        case SequenceName.PinDown:
                            lblPinDownTime.Text = time.ToString();
                            break;
                        case SequenceName.GlassCenter_Load:
                            lblGlassCenterTime.Text = time.ToString();
                            break;
                        case SequenceName.StageToRotatePos:
                            lblStageRotateTime.Text = time.ToString();
                            break;
                        case SequenceName.StageRotate_Load:
                            lblStageThetaTime.Text = time.ToString();
                            break;
                        case SequenceName.StageToDetectPos:
                            lblStageDetectPosTime.Text = time.ToString();
                            break;
                        case SequenceName.LoadTotal:
                            lblLoadTotalTime.Text = time.ToString();
                            break;

                        case SequenceName.ProbeToAlignPos:
                            if (stageNum == 0)
                            {
                                lblStage1AlignTime.Text = time.ToString();
                            }
                            else
                            {
                                lblStage2AlignTime.Text = time.ToString();
                            }
                            break;
                        case SequenceName.GlassAlign:
                            if (stageNum == 0)
                            {
                                lblStage1GlassAlignTime.Text = time.ToString();
                            }
                            else
                            {
                                lblStage2GlassAlignTime.Text = time.ToString();
                            }
                            break;
                        case SequenceName.ContactDown:
                            if (stageNum == 0)
                            {
                                lblStage1ContactDownTime.Text = time.ToString();
                            }
                            else
                            {
                                lblStage2ContactDownTime.Text = time.ToString();
                            }
                            break;
                        case SequenceName.OSTest:
                            if (stageNum == 0)
                            {
                                lblStage1OSTestTime.Text = time.ToString();
                            }
                            else
                            {
                                lblStage2OSTestTime.Text = time.ToString();
                            }
                            break;
                        case SequenceName.ContactUp:
                            if (stageNum == 0)
                            {
                                lblStage1ContactUpTime.Text = time.ToString();
                            }
                            else
                            {
                                lblStage2ContactUpTime.Text = time.ToString();
                            }
                            break;
                        case SequenceName.ProbeTotal:
                            if (stageNum == 0)
                            {
                                lblStage1ProbingTotalTIme.Text = time.ToString();
                            }
                            else
                            {
                                lblStage2ProbingTotalTIme.Text = time.ToString();
                            }
                            break;
                        case SequenceName.IRTestPos:
                            if (stageNum == 0)
                            {
                                lblStage1IRTestPosTime.Text = time.ToString();
                            }
                            else
                            {
                                lblStage2IRTestPosTime.Text = time.ToString();
                            }
                            break;
                        case SequenceName.IRTest:
                            if (stageNum == 0)
                            {
                                lblStage1IRTestTime.Text = time.ToString();
                            }
                            else
                            {
                                lblStage2IRTestTime.Text = time.ToString();
                            }
                            break;
                        case SequenceName.IRTestTotal:
                            if (stageNum == 0)
                            {
                                lblStage1IRTestTotalTime.Text = time.ToString();
                            }
                            else
                            {
                                lblStage2IRTestTotalTime.Text = time.ToString();
                            }
                            break;
                        case SequenceName.StageToReadyPos:
                            lblStageRdyTime.Text = time.ToString();
                            break;
                        case SequenceName.StageToUnloadPos:
                            lblStageUnloadPosTime.Text = time.ToString();
                            break;
                        case SequenceName.StageRotate_Unload:
                            lblStageThetaPosTime.Text = time.ToString();
                            break;
                        case SequenceName.GlassCenter_Unload:
                            lblGlassCenterTime2.Text = time.ToString();
                            break;
                        case SequenceName.PinUp:
                            lblPinUpTime.Text = time.ToString();
                            break;
                        case SequenceName.ShutterOpen:
                            lblShutterTime.Text = time.ToString();
                            break;
                        case SequenceName.UnloadTotal:
                            lblUnLoadTotalTime.Text = time.ToString();
                            break;
                        case SequenceName.OneCycle:
                            lblTotalTime.Text = time.ToString();
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
