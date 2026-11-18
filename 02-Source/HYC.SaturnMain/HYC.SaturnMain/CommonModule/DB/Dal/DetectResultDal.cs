using CommonDB.Model;
using DBQuery.DAL;
using HYC.HTLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonDB.Dal
{
    public class DetectResultDal:BaseDal
    {
        public static event Action<DetectResultModel> OnTriggerCount;
        /// <summary>
       /// 根据ID查询记录
       /// </summary>
       /// <param name="panelId"></param>
       /// <param name="StartTime"></param>
       /// <returns></returns>
        public List<DetectResultModel> GetAll()
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("select * from DetectResultInfo ");
                return Db.Query<DetectResultModel>(sb.ToString());
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DB").Fatal(ex.ToString());
                return null;
            }
        }
        /// <summary>
        /// 根据ID查询记录
        /// </summary>
        /// <param name="panelId"></param>
        /// <param name="StartTime"></param>
        /// <returns></returns>
        public DetectResultModel GetByIDAndTime(string panelId, DateTime startTime)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("select * from DetectResultInfo ");
                sb.Append("where PanelID=@PanelID and StartTime=@StartTime ");
                return Db.Query<DetectResultModel>(sb.ToString(), new { PanelID = panelId, StartTime = startTime }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DB").Fatal(ex.ToString());
                return null;
            }
        }
        /// <summary>
        /// 获取指定时间间隔内的记录
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public List<DetectResultModel> GetByTimeRange(DateTime start, DateTime end)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("select * from DetectResultInfo ");
                sb.Append("where StartTime >@Start and StartTime <@End");
                var list= Db.Query<DetectResultModel>(sb.ToString(), new { Start = start, End = end });
                return list.Where(p => p.Result != null).ToList();
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DB").Fatal(ex.ToString());
                return null;
            }
        }
        /// <summary>
        /// 使用参数筛选结果表
        /// </summary>
        /// <param name="panelID">产品ID，用于相似性匹配</param>
        /// <param name="datetime1">起始时间</param>
        /// <param name="datetime2">结束时间</param>
        /// <param name="result">检测结果</param>
        /// <returns></returns>
        public List<DetectResultModel> GetList(string panelID, string starttime, string endtime,string result, string stationNo, string unitNo)
        {
            try
            {
                List<DetectResultModel> list = null;
                StringBuilder sb = new StringBuilder();
                sb.Append("select * from DetectResultInfo ");
                sb.Append("where PanelID like @PanelID and StartTime>@datetime1 and StartTime<@datetime2 ");
                if (result != "全部")
                {
                    sb.Append("and Result=@Result ");
                }
                if (stationNo != "全部")
                {
                    sb.Append("and StationNo=@StationNo ");
                }
                if (unitNo != "全部")
                {
                    sb.Append("and UnitNo=@UnitNo ");
                }
                sb.Append("order by StartTime");
                list = Db.Query<DetectResultModel>(sb.ToString(), new { PanelID = "%" + panelID + "%", datetime1 = starttime, datetime2 = endtime, Result = result, StationNo=stationNo, UnitNo= unitNo });
                return list.Where(p=>p.Result!=null).ToList();
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
        public int Insert(DetectResultModel model)
        {
            try
            {
                new CellStartTimeDal().Insert(new CellStartTimeModel { PanelID = model.PanelID, StartTime = model.StartTime });
                StringBuilder sb = new StringBuilder();
                sb.Append("Insert into DetectResultInfo ");
                sb.Append("(PanelID,StartTime,RecipeID,StationNo,UnitNo) ");
                sb.Append("values(@PanelID,@StartTime,@RecipeID,@StationNo,@UnitNo) ");
                return Db.Execute(sb.ToString(), model);
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DB").Fatal(ex.ToString());
                return 0;
            }
        }
     
        /// <summary>
        /// 更新数据库记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Update(DetectResultModel model)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("update DetectResultInfo ");
                sb.Append("set Result=@Result,EndTime=@EndTime ");
                sb.Append("where PanelID=@PanelID and StartTime=@StartTime ");
                OnTriggerCount?.Invoke(model);
                return Db.Execute(sb.ToString(), model);
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DB").Fatal(ex.ToString());
                return 0;
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
                sb.Append("delete from DetectResultInfo ");
                sb.Append("where StartTime<@StartTime ");
                return Db.Execute(sb.ToString(), new { StartTime = DateTime.Now.AddDays(-days) });
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DB").Fatal(ex.ToString());
                return 0;
            }
        }
    }
}
