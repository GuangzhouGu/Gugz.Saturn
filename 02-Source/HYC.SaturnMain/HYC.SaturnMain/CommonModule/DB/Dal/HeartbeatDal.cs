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
    public class HeartbeatDal:BaseDal
    {
        public int Insert(HeartbeatModel model)
        {
            try
            {
                new DeviceStartTimeDal().Insert(model.DeviceName,model.ConnectTime);
                StringBuilder sb = new StringBuilder();
                sb.Append("insert into HeartbeatInfo ");
                sb.Append("(DeviceName,ConnectTime,DeviceIP,DevicePort,ConnectStatus) ");
                sb.Append("values(@DeviceName,@ConnectTime,@DeviceIP,@DevicePort,@ConnectStatus)");
                return Db.Execute(sb.ToString(),model);
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DB").Fatal(ex.ToString());
                return 0;
            }
        }

        public int Delete(int days)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("delete from HeartbeatInfo ");
                sb.Append("where ConnectTime<@ConnectTime ");
                return Db.Execute(sb.ToString(), new { ConnectTime = DateTime.Now.AddDays(-days) });
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DB").Fatal(ex.ToString());
                return 0;
            }
        }

        public int Update(string deviceName,DateTime disconnectTime)
        {
            try
            {
                DateTime time=(DateTime)new DeviceStartTimeDal().GetConnectTime(deviceName);
                StringBuilder sb = new StringBuilder();
                sb.Append("update HeartbeatInfo ");
                sb.Append("set DisconnectTime=@DisconnectTime,ConnectStatus='0' ");
                sb.Append("where DeviceName=@DeviceName and ConnectTime=@ConnectTime  and ConnectStatus='1' ");
                return Db.ExecuteNonQuery(sb.ToString(), CommandType.Text,
                    new KeyValuePair<string, object>("@DeviceName", deviceName),
                     new KeyValuePair<string, object>("@ConnectTime", time),
                    new KeyValuePair<string, object>("@DisconnectTime", disconnectTime));
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DB").Fatal(ex.ToString());
                return 0;
            }
        }
    }
}
