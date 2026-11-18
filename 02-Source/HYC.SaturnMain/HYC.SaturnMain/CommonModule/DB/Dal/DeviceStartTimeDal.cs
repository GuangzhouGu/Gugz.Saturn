using CommonDB.Model;
using HYC.HTLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonDB.Dal
{
    internal class DeviceStartTimeDal:BaseDal
    { 
        /// <summary>
      /// 获取指定ID的记录
      /// </summary>
      /// <param name="scanID"></param>
      /// <returns></returns>
        public DateTime? GetConnectTime(string deviceName)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("select * from DeviceStartTimeInfo ");
                sb.Append("where DeviceName=@DeviceName order by StartTime desc");
                DeviceStartTimeModel model = Db.Query<DeviceStartTimeModel>(sb.ToString(), new { DeviceName = deviceName }).FirstOrDefault();
                return model.StartTime;
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DB").Fatal(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 插入一条记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(string deviceName,DateTime time)
        {
            try
            {
                int ret = 0;
                if (Update(deviceName,time) <=0)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("insert into DeviceStartTimeInfo ");
                    sb.Append("(DeviceName,StartTime) ");
                    sb.Append("values(@DeviceName,@StartTime) ");
                    ret= Db.Execute(sb.ToString(), new { DeviceName =deviceName,StartTime=time});
                }
                return ret;
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DB").Fatal(ex.ToString());
                return 0;
            }
        }
        private int Update(string deviceName, DateTime time)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("update  DeviceStartTimeInfo ");
                sb.Append("set StartTime=@StartTime ");
                sb.Append("where DeviceName=@DeviceName ");
                return Db.Execute(sb.ToString(), new { DeviceName = deviceName, StartTime = time });
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DB").Fatal(ex.ToString());
                return 0;
            }
        }
    }
}
