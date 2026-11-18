/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2023 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnDB.DAL
// 文件名称    ：TotalInfoDal
// =================================
// 创 建 者    ：guguangzhou
// 创建日期    ：
// 功能描述    ：
// 使用说明    ：
//
//
// 创建标识：
//
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using HYC.HTLog;
using HYC.SaturnDB.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYC.SaturnDB.DAL
{
    public class TotalInfoDal
    {
        public static event Action<TotalInfoModel> OnTriggerCount;
        /// <summary>
        /// 根据ID查询记录
        /// </summary>
        /// <param name="GlassID"></param>
        /// <param name="eqpInTime"></param>
        /// <returns></returns>
        public TotalInfoModel GetByIDAndTime(string glassID, DateTime eqpInTime)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("select * from TotalInfo ");
                sb.Append("where GlassID=@GlassID and EQPInTime=@EQPInTime ");
                return DBHelper.DB.Query<TotalInfoModel>(sb.ToString(), new { GlassID = glassID, EQPInTime = eqpInTime }).FirstOrDefault();
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
        public List<TotalInfoModel> GetByTimeRange(DateTime start, DateTime end)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("select * from TotalInfo ");
                sb.Append("where EQPInTime >@Start and EQPInTime <@End");
                return DBHelper.DB.Query<TotalInfoModel>(sb.ToString(), new { Start = start, End = end });
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
        /// <param name="GlassID">产品ID，用于相似性匹配</param>
        /// <param name="datetime1">起始时间</param>
        /// <param name="datetime2">结束时间</param>
        /// <param name="result">检测结果</param>
        /// <returns></returns>
        public DataSet GetDataSet(string glassID, string datetime1, string datetime2, string result)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("select * from TotalInfo ");
                sb.Append("where GlassID like @GlassID ");
                sb.Append("and EQPInTime>@datetime1 and EQPInTime<@datetime2 ");
                List<KeyValuePair<string, object>> lstParams = new List<KeyValuePair<string, object>>();
                lstParams.Add(new KeyValuePair<string, object>("@GlassID", "%" + glassID + "%"));
                lstParams.Add(new KeyValuePair<string, object>("@datetime1", datetime1));
                lstParams.Add(new KeyValuePair<string, object>("@datetime2", datetime2));

                if (result != "Total")
                {
                    sb.Append("and Result=@Result ");
                    lstParams.Add(new KeyValuePair<string, object>("@Result", result));
                }

                sb.Append("order by EQPInTime ");

                DataSet ds = DBHelper.DB.ExecuteDataSet(sb.ToString(), CommandType.Text, lstParams.ToArray());
                return ds;
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
        public int Insert(TotalInfoModel model)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Insert into TotalInfo ");
                sb.Append("(GlassID,EQPInTime,GlassNum,RecipeID) ");
                sb.Append("values(@GlassID,@EQPInTime,@GlassNum,@RecipeID) ");
                return DBHelper.DB.Execute(sb.ToString(), model);
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DB").Fatal(ex.ToString());
                return 0;
            }
        }
        /// <summary>
        /// 测试用
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert2(TotalInfoModel model)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Insert into TotalInfo ");
                sb.Append("(GlassID,EQPInTime,GlassNum,RecipeID,StartTime,EndTime,JobJudge,JobGrade) ");
                sb.Append("values(@GlassID,@EQPInTime,@GlassNum,@RecipeID,@StartTime,@EndTime,@JobJudge,@JobGrade) ");
                return DBHelper.DB.Execute(sb.ToString(), model);
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
        public int Update(TotalInfoModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.Result)==false)
                {
                    OnTriggerCount?.Invoke(model);
                }
                StringBuilder sb = new StringBuilder();
                sb.Append("update TotalInfo ");
                sb.Append("set Result=@Result,AoiResult=@AoiResult,MuraResult=@MuraResult,MultiAngleResult=@MultiAngleResult,StartTime=@StartTime,EndTime=@EndTime,EQPOutTime=@EQPOutTime ");
                sb.Append("where GlassID=@GlassID and EQPInTime=@EQPInTime ");
                return DBHelper.DB.Execute(sb.ToString(), model);
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
                sb.Append("delete from TotalInfo ");
                sb.Append("where EQPInTime<@EQPInTime ");
                return DBHelper.DB.Execute(sb.ToString(), new { EQPInTime = DateTime.Now.AddDays(-days) });
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DB").Fatal(ex.ToString());
                return 0;
            }
        }

        /// <summary>
        /// 插入一条记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int InsertTest(TotalInfoModel model)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Insert into TotalInfo ");
                sb.Append("(GlassID,EQPInTime,GlassNum,RecipeID,Result,StartTime,EndTime) ");
                sb.Append("values(@GlassID,@EQPInTime,@GlassNum,@RecipeID,@Result,@StartTime,@EndTime) ");
                return DBHelper.DB.Execute(sb.ToString(), model);
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DB").Fatal(ex.ToString());
                return 0;
            }
        }
    }
}
