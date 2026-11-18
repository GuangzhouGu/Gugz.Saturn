using CommonDB.Model;
using HYC.HTLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonDB.Dal
{
    public class OpRecordDal:BaseDal
    {
        public int Insert(OpRecordModel model)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("insert into OpRecordInfo ");
                sb.Append("(UserName,UserID,OpTime,OpContent) ");
                sb.Append("values(@UserName,@UserID,@OpTime,@OpContent)");
                return Db.Execute(sb.ToString(), model);
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
                sb.Append("delete from OpRecordInfo ");
                sb.Append("where OpTime<@OpTime ");
                return Db.Execute(sb.ToString(), new { OpTime = DateTime.Now.AddDays(-days) });
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DB").Fatal(ex.ToString());
                return 0;
            }
        }
        public List<OpRecordModel> GetByTime(DateTime start, DateTime end)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("select * from OpRecordInfo ");
                sb.Append("where OpTime >@Start and OpTime <@End");
                return Db.Query<OpRecordModel>(sb.ToString(), new { Start = start, End = end });
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DB").Fatal(ex.ToString());
                return null;
            }
        }


    }
}
