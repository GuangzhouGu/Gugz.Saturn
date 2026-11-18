/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2021 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：DB.DAL
// 文件名称    ：NGInfoDal.cs
// =================================
// 创 建 者    ：guguangzhou
// 创建日期    ：2021/01/29 09:00:00 
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
    public class OpenShortNGInfoDal
    {
        public List<OpenShortNGInfoModel> Get(string glassID,int glassNum,int colNum,int rowNum)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                if(glassID == "")
                {
                    sb.Append("select * from OpenShortNGInfo ");
                }
                else
                {
                    sb.Append("select * from NGInfo where GlassID=@GlassID and GlassNum=@GlassNum and ColumnNum=@ColumnNum and RowNum=@RowNum order by DetectTime desc");
                }
                var list = DBHelper.DB.Query<OpenShortNGInfoModel>(sb.ToString(),  new { GlassID = glassID, GlassNum = glassNum, ColumnNum = colNum, RowNum = rowNum });
                return list;
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DB").Fatal(ex.ToString());
                return null;
            }
        }
        public DataSet GetByID(string glassID)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                if (glassID == "")
                {
                    sb.Append("select * from OpenShortNGInfo ");
                }
                else
                {
                    sb.Append($"select * from OpenShortNGInfo where GlassID=@GlassID order by DetectTime desc");
                }
                var dataSet = DBHelper.DB.QueryDataSet(sb.ToString(), new { GlassID = glassID});
                return dataSet;
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DB").Fatal(ex.ToString());
                return null;
            }
        }
        public DataSet GetByTime(DateTime start, DateTime end)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"select * from OpenShortNGInfo where  DetectTime>@StartTime and DetectTime<@EndTime order by DetectTime desc");
                var dataSet = DBHelper.DB.QueryDataSet(sb.ToString(),new { StartTime =start,EndTime=end});
                return dataSet;
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DB").Fatal(ex.ToString());
                return null;
            }
        }
        public int Insert(OpenShortNGInfoModel model)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("insert into OpenShortNGInfo ");
                    sb.Append("(GlassID,GlassNum,CellID,ColumnNum,RowNum,NGNum,NGCode,NGType,DetectTime,Source,Measure,ShortType) ");
                    sb.Append("values(@GlassID,@GlassNum,@CellID,@ColumnNum,@RowNum,@NGNum,@NGCode,@NGType,@DetectTime,@Source,@Measure,@ShortType) ");
                    return DBHelper.DB.Execute(sb.ToString(),model);
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
                sb.Append("delete from OpenShortNGInfo ");
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
