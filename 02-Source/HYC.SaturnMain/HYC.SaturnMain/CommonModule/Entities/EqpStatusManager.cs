using CommonDB.Dal;
using CommonDB.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonFunc.Entities
{
    public class EqpStatusManager
    {
        static EqpStatus LastSts = EqpStatus.None;
        static int LastStation = -1;
        public static void UpdateEqpSts(EqpStatus sts, int stationNo = -1)
        {
            var dal = new EqpHistoryDal();
            dal.Update(new EqpHistoryModel { EqpStatusType = (int)LastSts, StationNo = LastStation, CancelTime = DateTime.Now });
            LastSts = sts;
            dal.Insert(new EqpHistoryModel { EqpStatusType = (int)sts, InsertTime = DateTime.Now, StationNo = stationNo });
            LastStation = stationNo;
        }
        public static void EqpShutdown()
        {
            var dal = new EqpHistoryDal();
            dal.Update(new EqpHistoryModel { EqpStatusType = (int)LastSts, CancelTime = DateTime.Now });
        }
        public static EqpStatus GetLastSts()
        {
            return LastSts;
        }
        internal static DateTime GetEqpTime(DateTime startTime, DateTime endTime, EqpStatus sts, int stationNo = -1)
        {
            long second = new EqpHistoryDal().GetStatusTime(startTime, endTime, (int)sts, stationNo);
            string temp = ConvertToDateTime(second);
            return DateTime.Parse(temp);
        }
        private static string ConvertToDateTime(long second)
        {
            TimeSpan span = new TimeSpan(0, 0, (int)second);
            string time = span.ToString(@"hh\:mm\:ss");
            return time;
        }
    }
    public enum EqpStatus
    {
        Run,
        Pause,
        Standby,
        MediumAlarm,
        SeriousAlarm,
        Stop,
        None,
    }
}
