using HYC.AutoPosition.Interface;
using HYC.HTPCBase.Alarm;
using HYC.SaturnMain.Alarm;
using HYC.SaturnMain.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HYC.MicroLedATDetect.AutoPos
{
    public class AlignSignalProvider : ISignalProvider
    {
        public event EventHandler<RequestEventArgs> AutoPosRequest;
        public event EventHandler<RequestEventArgs> CalibrationRequest;
        public event EventHandler<MoveReqEventArgs> MoveRequest;
        public event EventHandler<SafetyEventArgs> SafetyChanged;
        public event EventHandler<RecipeSyncArgs> OnRecipeSync;
        public event Action<string,bool> OnUpdateAlignCamSts;
        public static event Func<int,double[], int[],bool> OnStageAlign;
        public bool AutoPosResult = true;
       // public static double[] XYTValue = new double[3];
        public static double XValueSum;//X方向多次对位累计值
        ManualResetEvent Mre = new ManualResetEvent(false);
        public void StartAutoPos(int index,string glassID)
        {
            AutoPosResult = true;
            XValueSum = 0;
            RequestEventArgs arg = new RequestEventArgs();
            arg.GroupIndex = index;
            arg.StageIndex = index;
            arg.DetectMode = 0;
            Mre.Reset();
            AutoPosRequest?.Invoke(this, arg);
            bool ret=Mre.WaitOne((int)(Param.Align.AlignTimeout*1000));
            AutoPosResult &= ret;
        }
        public void AutoPosCompleted(int groupIndex, int stageIndex, bool result)
        {
            AutoPosResult &= result;
            Mre.Set();
        }

        public void CalibrationCompleted(int groupIndex, int stageIndex, bool result)
        {
        }

        public bool Move(int groupIndex, double[] xyt, int[] reserve)
        {
            if (xyt!=null)
            {
                if (Math.Abs(xyt[0]) >Param.Align.AlignRangeX)
                {
                    AlarmManager.CreateAlarm((int)MotionAlarmCode.XValueError);
                    return false;
                }
                if (Math.Abs(xyt[1]) > Param.Align.AlignRangeY)
                {
                    AlarmManager.CreateAlarm((int)MotionAlarmCode.YValueError);
                    return false;
                }
                if (Math.Abs(xyt[2]) > Param.Align.AlignRangeT)
                {
                    AlarmManager.CreateAlarm((int)MotionAlarmCode.TValueError);
                    return false;
                }
                if (Param.Align.IsInverseX[groupIndex])
                {
                    xyt[0] = -xyt[0];
                }
                if (Param.Align.IsInverseY[groupIndex])
                {
                    xyt[1] = -xyt[1];
                }
                if (Param.Align.IsInverseT[groupIndex])
                {
                    xyt[2] = -xyt[2];
                }
                bool ret=(bool)OnStageAlign?.Invoke(groupIndex,xyt, reserve);
                return ret;
            }
            else
            {
                return false;
            }
        }

        public void MoveCompleted(int groupIndex, MoveReqType type, bool result)
        {
        }

        public void UpdateCameraStatus(int cameraIndex, bool isConnected)
        {
            string camName = $"{(cameraIndex==0?"LeftAlignCam": "RightAlignCam")}";
            OnUpdateAlignCamSts?.Invoke(camName, isConnected);
        }

        public void UpdatePlatformStatus(int platIndex, PlatformStatus status)
        {
        }
    }
}
