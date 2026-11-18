using HYC.SaturnDB.Model;
using HYC.HTLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYC.SaturnDB.DAL
{
    public class OpenShortInfoDal
    {
        public List<OpenShortDetectModel> Get(string glassID = "")
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                if (glassID == "")
                {
                    sb.Append("select * from OpenShortDetectInfo ");
                }
                else
                {
                    sb.Append("select * from OnePanelInfo where GlassID=@GlassID order by DetectTime desc");
                }
                return DBHelper.DB.Query<OpenShortDetectModel>(sb.ToString(), new { GlassID = glassID }); ;
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DB").Fatal(ex.ToString());
                return null;
            }
        }
        public DataSet GetDataSet(string glassID = "")
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                if (glassID == "")
                {
                    sb.Append("select * from OpenShortDetectInfo ");
                }
                else
                {
                    sb.Append($"select * from OpenShortDetectInfo where GlassID=@GlassID order by DetectTime desc");
                }
                return DBHelper.DB.QueryDataSet(sb.ToString(), new { GlassID =glassID}); 
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DB").Fatal(ex.ToString());
                return null;
            }
        }
        public List<OpenShortDetectModel> GetByTime(DateTime start,DateTime end)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("select * from OpenShortDetectInfo where DetectTime>@StartTime and DetectTime<@EndTime");
                return DBHelper.DB.Query<OpenShortDetectModel>(sb.ToString(), new { StartTime = start , EndTime =end}); ;
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DB").Fatal(ex.ToString());
                return null;
            }
        }
        public DataSet GetDataSetByTime(DateTime start, DateTime end)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"select * from OpenShortDetectInfo where DetectTime>@StartTime and DetectTime<@EndTime order by DetectTime desc");
                return DBHelper.DB.QueryDataSet(sb.ToString(),new { StartTime =start,EndTime=end}); 
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DB").Fatal(ex.ToString());
                return null;
            }
        }
        public int Insert(OpenShortDetectModel model)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("insert into OpenShortDetectInfo ");
                sb.Append("(GlassID,GlassNum,ColumnNum,RowNum,Result,NGType,TotalNGCount,OpenNGCount,ShortNGCount,DetectTime) ");
                sb.Append("values(@GlassID,@GlassNum,@ColumnNum,@RowNum,@Result,@NGType,@TotalNGCount,@OpenNGCount,@ShortNGCount,@DetectTime) ");
                return DBHelper.DB.Execute(sb.ToString(), model);
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DB").Fatal(ex.ToString());
                return -1;
            }
        }
        /// <summary>
        /// 删除指定天数之前的数据
        /// </summary>
        /// <param name="days"></param>
        /// <returns></returns>
        public int Delete(int days)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("delete from OpenShortDetectInfo ");
                sb.Append("where DetectTime<@DetectTime ");
                return DBHelper.DB.Execute(sb.ToString(), new { DetectTime = DateTime.Now.AddDays(-days) });
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DB").Fatal(ex.ToString());
                return 0;
            }
        }
    }
}
