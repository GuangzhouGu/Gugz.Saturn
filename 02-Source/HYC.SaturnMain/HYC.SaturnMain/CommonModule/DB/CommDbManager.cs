using HYC.HTAlarm;
using HYC.HTAlarm.DB.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonDB
{
   public class CommDbManager
    {
        static HTAlarmManager AlarmManager = HTAlarmManager.Instance;
        public static void Init(string conStr,string sqlType)
        {
            try
            {
                AlarmManager.Init(conStr, GetEnumValue<HYC.HTAlarm.DB.DAL.AlarmSQLType>(sqlType), "EQP");
                BaseDal.ConnectionString = conStr;
                BaseDal.SQLType = GetEnumValue<AlarmSQLType>(sqlType); ;
            }
            catch (Exception)
            {

            }
        }
        public static List<AlarmHistory> GetAlarmHistory(DateTime start, DateTime end)
        {
            return AlarmManager.GetAlarmHistory(start, end);
        }
        public static T GetEnumValue<T>(string value)
        {
            T result;
            Type enumType = typeof(T);
            result = (T)Enum.Parse(enumType, value);
            return result;
        }
    }
}
