using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYC.SaturnMain.Entities
{
    public class SequenceStepManager
    {
        static Stopwatch[] Sw = new Stopwatch[(int)SequenceName.Count];
        public static event Action<SequenceName, int, double> OnUpdateSequenceStepTime;
        public static void StartWatch(SequenceName sequenceName,int stageNum=-1,int subIndex=0)
        {
            if (Sw[(int)sequenceName]==null)
            {
                Sw[(int)sequenceName] = new Stopwatch();
            }
            Sw[(int)sequenceName].Restart();
        }
        public static void StopWatch(SequenceName sequenceName, int stageNum = -1, int subIndex = 0)
        {
            if (Sw[(int)sequenceName]!=null)
            {
                Sw[(int)sequenceName].Stop();
                double sec = Sw[(int)sequenceName].Elapsed.TotalSeconds;
                Task.Factory.StartNew(() => {
                    OnUpdateSequenceStepTime?.Invoke(sequenceName, stageNum, Math.Round(sec,3));
                });
            }
        }
    }
    public enum SequenceName
    {
        PinDown,
        GlassCenter_Load,
        StageToRotatePos,
        StageRotate_Load,
        StageToDetectPos,
        LoadTotal,

        ProbeToAlignPos,
        GlassAlign,
        ContactDown,
        OSTest,
        ContactUp,
        ProbeTotal,

        IRTestPos,
        IRTest,
        IRTestTotal,

        StageToReadyPos,
        StageToUnloadPos,
        StageRotate_Unload,
        GlassCenter_Unload,
        PinUp,
        ShutterOpen,
        UnloadTotal,

        OneCycle,
        Count,
    }
}
