using CommonDB.Model;
using HYC.HTLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonDB.Dal
{
    public class EqpHistoryDal : BaseDal
    {
        public int Insert(EqpHistoryModel model)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("insert into EqpRunHistory ");
                sb.Append("(EqpStatusType,InsertTime,StationNo) ");
                sb.Append("values(@EqpStatusType,@InsertTime,@StationNo) ");
                return Db.Execute(sb.ToString(), model);
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DB").Fatal(ex.ToString());
                return 0;
            }
        }
        public int Update(EqpHistoryModel model)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("update EqpRunHistory ");
                sb.Append("set CancelTime=@CancelTime ");
                sb.Append("where EqpStatusType=@EqpStatusType and StationNo=@StationNo and CancelTime =@CancelTime ");
                return Db.Execute(sb.ToString(), model);
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DB").Fatal(ex.ToString());
                return 0;
            }
        }
        public long GetStatusTime(DateTime startTime, DateTime endTime, int type, int stationNo = -1)
        {
            object[] value = new object[4];
            try
            {
                string methodName = BaseDal.SQLType == AlarmSQLType.MySQL ? "TIMESTAMPDIFF" : "datediff";
                string sql1 = $"select sum({methodName}(second, InsertTime, CancelTime)) from EqpRunHistory where EqpStatusType=@EqpStatusType and InsertTime>=@StartTime and CancelTime<=@EndTime";
                string sql2 = $"select sum({methodName}(second, @StartTime, CancelTime)) from EqpRunHistory where EqpStatusType=@EqpStatusType and InsertTime<=@StartTime and CancelTime>=@StartTime and CancelTime<=@EndTime";
                string sql3 = $"select sum({methodName}(second, InsertTime, @EndTime)) from EqpRunHistory where EqpStatusType=@EqpStatusType and InsertTime>=@StartTime and InsertTime<=@EndTime and (CancelTime>=@EndTime or CancelTime is null)";
                string sql4 = $"select sum({methodName}(second, @StartTime, @EndTime)) from EqpRunHistory where EqpStatusType=@EqpStatusType and InsertTime<=@StartTime and (CancelTime>=@EndTime or CancelTime is null)";
                if (stationNo >= 0)
                {
                    sql1 += " and StationNo=@StationNo";
                    sql2 += " and StationNo=@StationNo";
                    sql3 += " and StationNo=@StationNo";
                    sql4 += " and StationNo=@StationNo";
                }
                List<KeyValuePair<string, object>> lstParams = new List<KeyValuePair<string, object>>();
                lstParams.Add(new KeyValuePair<string, object>("@EqpStatusType", type));
                lstParams.Add(new KeyValuePair<string, object>("@StartTime", startTime));
                lstParams.Add(new KeyValuePair<string, object>("@EndTime", endTime));
                lstParams.Add(new KeyValuePair<string, object>("@StationNo", stationNo));

                value[0] = Db.ExecuteScalar(sql1, System.Data.CommandType.Text,
                  lstParams.ToArray());
                value[1] = Db.ExecuteScalar(sql2, System.Data.CommandType.Text,
            lstParams.ToArray());
                value[2] = Db.ExecuteScalar(sql3, System.Data.CommandType.Text,
               lstParams.ToArray());
                value[3] = Db.ExecuteScalar(sql4, System.Data.CommandType.Text,
               lstParams.ToArray());

                long[] longValue = new long[4];
                for (int i = 0; i < longValue.Length; i++)
                {
                    long.TryParse(value[i].ToString(), out longValue[i]);
                }
                return longValue.Sum();
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DB").Fatal(ex.ToString());
                return 0;
            }
        }
     
        public List<EqpHistoryModel> GetMediumAlarmTime(DateTime startTime, DateTime endTime, int type, int stationNo = -1)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select * from EqpRunHistory ");
            sb.Append("where InsertTime >@Start and InsertTime <@End and EqpStatusType=@Type order by InsertTime desc");
            return Db.Query<EqpHistoryModel>(sb.ToString(), new { Type = type, Start = startTime, End = endTime, StationNo = stationNo });
        }
        public int Delete(int days)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("delete from EqpRunHistory ");
            sb.Append("where InsertTime<@InsertTime ");
            return Db.ExecuteNonQuery(sb.ToString(), CommandType.Text,
                new KeyValuePair<string, object>("@InsertTime", DateTime.Now.Date.AddDays(-days)));
        }
    }
}
