using CommonDB;
using CommonDB.Dal;
using HYC.HTAlarm;
using HYC.HTAlarm.DB.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonFunc.Entities
{
    internal  class PerformanceManager
    {
        /// <summary>
        /// MTBA=工作时间/工作时间中的辅助次数
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public double GetMtba(DateTime startTime,DateTime endTime)
        {
           var list= CommDbManager.GetAlarmHistory(startTime,endTime);//时间段内的产量
            var lightAlarmCount = list.Where(p=>p.AlarmDetail.AlarmLevel== AlarmLevel.LightAlarm).Count();//时间段内的中报警次数
            var runTime=EqpStatusManager.GetEqpTime(startTime, endTime,EqpStatus.Run);//时间段内的运行时间
            double hours = Math.Round(runTime.Hour + runTime.Minute / 60.0,2);//转换成小时
            if (lightAlarmCount > 0)
            {
                return Math.Round(hours / lightAlarmCount, 2);//时间除以中报警
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// MTBF=工作时间/工作时间中的故障次数
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public double GetMtbf(DateTime startTime, DateTime endTime)
        {
            var list = CommDbManager.GetAlarmHistory(startTime, endTime);//时间段内的产量
            var heavyAlarmCount = list.Where(p => p.AlarmDetail.AlarmLevel == AlarmLevel.HeavyAlarm).Count();//时间段内的重报警次数
            var runTime = EqpStatusManager.GetEqpTime(startTime, endTime, EqpStatus.Run);//时间段内的运行时间
            double hours = Math.Round(runTime.Hour + runTime.Minute / 60.0, 2);//转换成小时
            if (heavyAlarmCount > 0)
            {
                return Math.Round(hours / heavyAlarmCount, 2);//时间除以重报警
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// Jam Rate=1/平均两次卡料时间内的产出
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public double GetJamRate(DateTime startTime, DateTime endTime)
        {
            var jamCountList=new EqpHistoryDal().GetMediumAlarmTime(startTime,endTime, (int)EqpStatus.MediumAlarm);//查询时间段内的中报警次数
            var yieldList= new DetectResultDal().GetByTimeRange(startTime, endTime);//查询时间段内的产量
            if (jamCountList.Count>0)
            {
                return Math.Round(1.0 / (yieldList.Count / jamCountList.Count), 2);//1/（产量/中报警次数）
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// UPH=工作时间中的产出/工作时间
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public double GetUph(DateTime startTime, DateTime endTime)
        {
            var list = new DetectResultDal().GetByTimeRange(startTime, endTime);//读取时间段内产量
            var runTime = EqpStatusManager.GetEqpTime(startTime, endTime, EqpStatus.Run);//读取时间段内的运行时间
            double hours = Math.Round(runTime.Hour + runTime.Minute / 60.0, 2);//换算成小时
            if (list!=null&& hours>0)
            {
                return Math.Round(list.Count / hours, 2);//产量除以时间（小时）
            }
            else
            {
                return 0;
            }
        }
    }
}
