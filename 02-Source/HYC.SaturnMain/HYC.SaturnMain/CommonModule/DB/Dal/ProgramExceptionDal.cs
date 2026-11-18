using CommonDB.Model;
using HYC.HTLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonDB.Dal
{
    public class ProgramExceptionDal:BaseDal
    {
        public int Insert(ProgramExceptionModel model)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("insert into ProgramExceptionInfo ");
                sb.Append("(ClassName,MethodName,ExceptionMessage,ExceptionTime,StackTrace) ");
                sb.Append("values(@ClassName,@MethodName,@ExceptionMessage,@ExceptionTime,@StackTrace)");
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
                sb.Append("delete from ProgramExceptionInfo ");
                sb.Append("where ExceptionTime<@ExceptionTime ");
                return Db.Execute(sb.ToString(), new { ExceptionTime = DateTime.Now.AddDays(-days)});
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DB").Fatal(ex.ToString());
                return 0;
            }
        }
        public List<ProgramExceptionModel> GetByTime(DateTime start, DateTime end)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("select * from ProgramExceptionInfo ");
                sb.Append("where ExceptionTime >@Start and ExceptionTime <@End");
                return Db.Query<ProgramExceptionModel>(sb.ToString(), new { Start = start, End = end });
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DB").Fatal(ex.ToString());
                return null;
            }
        }
    }
}
