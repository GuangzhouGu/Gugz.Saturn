/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2023 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnDB.DAL
// 文件名称    ：AoiNGInfoDal
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
    public class AoiNGInfoDal
    {
        /// <summary>
        /// 根据ID查询记录
        /// </summary>
        /// <param name="panelId"></param>
        /// <returns></returns>
        public List<AoiNGInfoModel>GetByID(string glassId, string cellID, string detectType)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("select * from AoiNGInfo ");
                sb.Append("where GlassID=@GlassID and CellID=@CellID and DetectType=@DetectType ");
                return DBHelper.DB.Query<AoiNGInfoModel>(sb.ToString(), new { GlassID = glassId, CellID = cellID, DetectType = detectType });
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
        /// <param name="stepId">检测步骤</param>
        /// <param name="pcname">检测主机</param>
        /// <param name="result">检测结果</param>
        /// <returns></returns>
        public DataSet GetDataSet(string glassId, string cellID, string detectType)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("select * from AoiNGInfo ");
                sb.Append("where GlassID=@GlassID and CellID=@CellID and DetectType=@DetectType ");
                List<KeyValuePair<string, object>> lstParams = new List<KeyValuePair<string, object>>();
                lstParams.Add(new KeyValuePair<string, object>("@GlassID", "%" + glassId + "%"));
                lstParams.Add(new KeyValuePair<string, object>("@CellID", cellID));
                lstParams.Add(new KeyValuePair<string, object>("@DetectType", detectType));
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
        public int Insert(AoiNGInfoModel model)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("insert into AoiNGInfo ");
                sb.Append("(GlassID,GlassNum,ColumnNum,CellID,EQPInTime,PatternName,DefectNum,ErrorCode,DefectGrade,ErrorCount,DetectType,Region,Area,DefectParams,DefectType) ");
                sb.Append("values(@GlassID,@GlassNum,@ColumnNum,@CellID,@EQPInTime,@PatternName,@DefectNum,@ErrorCode,@DefectGrade,@ErrorCount,@DetectType,@Region,@Area,@DefectParams,@DefectType)");
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
                sb.Append("delete from AoiNGInfo ");
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
