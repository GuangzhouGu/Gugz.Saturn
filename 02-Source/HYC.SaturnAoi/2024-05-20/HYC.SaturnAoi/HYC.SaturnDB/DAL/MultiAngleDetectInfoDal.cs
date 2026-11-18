/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2023 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnDB.DAL
// 文件名称    ：MultiAngleInfoDal
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
   public class MultiAngleDetectInfoDal
    {
        public MultiAngleDetectModel GetByIDAndTime(string glassID,string cellID,  DateTime eqpInTime)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("select * from MultiAngleDetectInfo ");
                sb.Append("where GlassID=@GlassID and CellID=@CellID and EQPInTime=@EQPInTime");
                return DBHelper.DB.Query<MultiAngleDetectModel>(sb.ToString(), new { CellID = cellID, GlassID = glassID, EQPInTime = eqpInTime }).FirstOrDefault();
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
        public int Insert(MultiAngleDetectModel model)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("insert into MultiAngleDetectInfo ");
                sb.Append("(GlassID,CellID,EQPInTime,RecipeID,GlassNum,ColumnNum)");
                sb.Append("values(@GlassID,@CellID,@EQPInTime,@RecipeID,@GlassNum,@ColumnNum)");
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
        public int Update(MultiAngleDetectModel model)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("update MultiAngleDetectInfo ");
                sb.Append("set StartTime=@StartTime,EndTime=@EndTime,ProcessingTime=@ProcessingTime,Result=@Result ");
                sb.Append("where GlassID=@GlassID and EQPInTime=@EQPInTime");
                return DBHelper.DB.Execute(sb.ToString(), model);
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DB").Fatal(ex.ToString());
                return 0;
            }
        }
        public DataSet GetDataSet(string glassId, string datetime1, string datetime2, string result)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                List<KeyValuePair<string, object>> lstParams = new List<KeyValuePair<string, object>>();
                if (string.IsNullOrEmpty(glassId))
                {
                    sb.Append("select * from MultiAngleDetectInfo ");
                    sb.Append("where EQPInTime>@datetime1 and EQPInTime<@datetime2 ");
                }
                else
                {
                    sb.Append("select * from MultiAngleDetectInfo ");
                    sb.Append("where GlassID like @GlassID ");
                    sb.Append("and EQPInTime>@datetime1 and EQPInTime<@datetime2 ");
                    lstParams.Add(new KeyValuePair<string, object>("@GlassID", "%" + glassId + "%"));
                }
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
        /// 删除指定天数之前的数据
        /// </summary>
        /// <param name="days"></param>
        /// <returns></returns>
        public int Delete(int days)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("delete from MultiAngleDetectInfo ");
                sb.Append("where EQPInTime<@EQPInTime ");
                return DBHelper.DB.Execute(sb.ToString(), new { EQPInTime = DateTime.Now.AddDays(-days) });
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DB").Fatal(ex.ToString());
                return 0;
            }
        }
      
    }
}
