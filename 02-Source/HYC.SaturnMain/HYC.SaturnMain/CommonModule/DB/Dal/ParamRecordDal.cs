using CommonDB.Model;
using HYC.HTLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonDB.Dal
{
    public class ParamRecordDal:BaseDal
    {
        public int Insert(ParamRecordModel model)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("insert into ParamRecordInfo ");
                sb.Append("(UserName,UserID,OpTime,SourceParam,ModifiedParam,ParaName,ParaCategory) ");
                sb.Append("values(@UserName,@UserID,@OpTime,@SourceParam,@ModifiedParam,@ParaName,@ParaCategory)");
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
                sb.Append("delete from ParamRecordInfo ");
                sb.Append("where OpTime<@OpTime ");
                return Db.Execute(sb.ToString(), new { OpTime = DateTime.Now.AddDays(-days) });
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DB").Fatal(ex.ToString());
                return 0;
            }
        }
        public List<ParamRecordModel> GetByTime(DateTime start, DateTime end)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("select * from ParamRecordInfo ");
                sb.Append("where OpTime >@Start and OpTime <@End");
                return Db.Query<ParamRecordModel>(sb.ToString(), new { Start = start, End = end });
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DB").Fatal(ex.ToString());
                return null;
            }
        }
    }
}
