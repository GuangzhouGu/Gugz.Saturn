/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2023 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnDB.DAL
// 文件名称    ：OneCellDetectInfoDal
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYC.SaturnDB.DAL
{
    public class OneCellDetectInfoDal
    {
        /// <summary>
        /// 根据ID和时间查询记录
        /// </summary>
        /// <param name="GlassID"></param>
        /// <param name="StartTime"></param>
        /// <returns></returns>
        public OneCellDetectModel GetByIDAndTime(string glassID,string cellID, DateTime eqpInTime)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("select * from OneCellDetectInfo ");
                sb.Append("where GlassID=@GlassID and CellID=@CellID and EQPInTime=@EQPInTime ");
                return DBHelper.DB.Query<OneCellDetectModel>(sb.ToString(), new { GlassID = glassID,CellID=cellID, EQPInTime = eqpInTime }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DB").Fatal(ex.ToString());
                return null;
            }
        }
        /// <summary>
        /// 根据ID和时间查询记录
        /// </summary>
        /// <param name="GlassID"></param>
        /// <param name="StartTime"></param>
        /// <returns></returns>
        public List<OneCellDetectModel> GetByIDAndTime(string glassID, DateTime eqpInTime)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("select * from OneCellDetectInfo ");
                sb.Append("where GlassID=@GlassID and EQPInTime=@EQPInTime ");
                return DBHelper.DB.Query<OneCellDetectModel>(sb.ToString(), new { GlassID = glassID,EQPInTime = eqpInTime });
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
        public int Insert(OneCellDetectModel model)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Insert into OneCellDetectInfo ");
                sb.Append("(GlassID,CellID,EQPInTime,GlassNum,ColumnNum,CellNum) ");
                sb.Append("values(@GlassID,@CellID,@EQPInTime,@GlassNum,@ColumnNum,@CellNum) ");
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
        public int Update(OneCellDetectModel model)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("update OneCellDetectInfo ");
                sb.Append("set Result=@Result,PowerOnResult=@PowerOnResult,AoiResult=@AoiResult,MuraResult=@MuraResult,MultiAngleResult=@MultiAngleResult,StartTime=@StartTime,EndTime=@EndTime ");
                sb.Append("where GlassID=@GlassID and CellID=@CellID and EQPInTime=@EQPInTime ");
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
                sb.Append("delete from OneCellDetectInfo ");
                sb.Append("where StartTime<@StartTime ");
                return DBHelper.DB.Execute(sb.ToString(), new { StartTime = DateTime.Now.AddDays(-days) });
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DB").Fatal(ex.ToString());
                return 0;
            }
        }
    }
}
