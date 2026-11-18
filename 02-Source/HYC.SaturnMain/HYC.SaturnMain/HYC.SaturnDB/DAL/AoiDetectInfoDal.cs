/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2023 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnDB.DAL
// 文件名称    ：AoiInfoDal
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
    public class AoiDetectInfoDal
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="glassId"></param>
        /// <param name="cellID"></param>
        /// <param name="detectType"></param>
        /// <returns></returns>
        public AoiDetectModel GetByID(string glassId, string cellID, string detectType)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("select * from AoiDetectInfo ");
                sb.Append("where GlassID=@GlassID and CellID=@CellID and DetectType=@DetectType");
                return DBHelper.DB.Query<AoiDetectModel>(sb.ToString(), new { GlassID = glassId, CellID = cellID, DetectType = detectType }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DB").Fatal(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="glassId"></param>
        /// <param name="cellID"></param>
        /// <param name="detectType"></param>
        /// <param name="eqpInTime"></param>
        /// <returns></returns>
        public AoiDetectModel GetByIDAndTime(string cellID, string detectType, DateTime eqpInTime)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("select * from AoiDetectInfo ");
                sb.Append("where CellID=@CellID and DetectType=@DetectType and EQPInTime=@EQPInTime");
                return DBHelper.DB.Query<AoiDetectModel>(sb.ToString(), new { CellID = cellID, DetectType = detectType, EQPInTime = eqpInTime }).FirstOrDefault(); ;
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DB").Fatal(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="glassId"></param>
        /// <param name="cellID"></param>
        /// <param name="detectType"></param>
        /// <param name="datetime1"></param>
        /// <param name="datetime2"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public DataSet GetDataSet(string glassId, string detectType, string datetime1, string datetime2, string result)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                List<KeyValuePair<string, object>> lstParams = new List<KeyValuePair<string, object>>();
                sb.Append("select * from AoiDetectInfo ");
                if (string.IsNullOrEmpty(glassId))
                {
                    sb.Append("where EQPInTime>@datetime1 and EQPInTime<@datetime2 and DetectType=@DetectType ");
                }
                else
                {
                    sb.Append("select * from AoiDetectInfo ");
                    sb.Append("where GlassID like @GlassID ");
                    sb.Append("and EQPInTime>@datetime1 and EQPInTime<@datetime2 and DetectType=@DetectType ");
                    lstParams.Add(new KeyValuePair<string, object>("@GlassID", "%" + glassId + "%"));
                }
                lstParams.Add(new KeyValuePair<string, object>("@datetime1", datetime1));
                lstParams.Add(new KeyValuePair<string, object>("@datetime2", datetime2));
                lstParams.Add(new KeyValuePair<string, object>("@DetectType", detectType));
               
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
        public int Insert(AoiDetectModel model)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("insert into AoiDetectInfo ");
                sb.Append("(GlassID,EQPInTime,GlassNum,ColumnNum,CellID,RecipeID,DetectType)");
                sb.Append("values(@GlassID,@EQPInTime,@GlassNum,@ColumnNum,@CellID,@RecipeID,@DetectType)");
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
        public int Insert2(AoiDetectModel model)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("insert into AoiDetectInfo ");
                sb.Append("(GlassID,EQPInTime,GlassNum,ColumnNum,CellID,RecipeID,DetectType,StartTime,EndTime,ProcessingTime,DetectCode,DetectGrade,PCIndex,BirghtDotDefectCount,DarkDotDefectCount,LineDefectCount,Mura2DefectCount,MuraDefectCount,ReserveDefectCount,TotalDefectCount,XsycDefectCount)");
                sb.Append("values(@GlassID,@EQPInTime,@GlassNum,@ColumnNum,@CellID,@RecipeID,@DetectType,@StartTime,@EndTime,@ProcessingTime,@DetectCode,@DetectGrade,@PCIndex,@BirghtDotDefectCount,@DarkDotDefectCount,@LineDefectCount,@Mura2DefectCount,@MuraDefectCount,@ReserveDefectCount,@TotalDefectCount,@XsycDefectCount)");
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
        public int Update(AoiDetectModel model)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("update AoiDetectInfo ");
                sb.Append("set StartTime=@StartTime,EndTime=@EndTime,");
                sb.Append("ProcessingTime=@ProcessingTime,Result=@Result,DetectCode=@DetectCode,DetectGrade=@DetectGrade,ErrorText=@ErrorText,");
                sb.Append("ImgROI=@ImgROI,BmpSavePath=@BmpSavePath ");
                sb.Append("where GlassID=@GlassID and EQPInTime=@EQPInTime and DetectType=@DetectType");
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
                sb.Append("delete from AoiDetectInfo ");
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
