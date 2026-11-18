/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2021 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：DB.DAL
// 文件名称    ：JobDataDal.cs
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
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using HYC.SaturnDB.Model;
using HYC.HTLog;

namespace HYC.SaturnDB.DAL
{
    public class JobDataDal
    {
        public JobDataModel Get(string glassID)
        {
            try
            {
                string strSql = "select * from JobDataInfo where GlassID='" + glassID + "' order by ReceiveTime desc";
                var list = DBHelper.DB.Query<JobDataModel>(strSql);
                return list[0];
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DB").Fatal(ex.ToString());
                return null;
            }
        }
        public JobDataModel GetLatest(int glassNum)
        {
            try
            {
                string strSql = $"select * from JobDataInfo where GlassNum='{glassNum}' order by ReceiveTime desc";
                var list = DBHelper.DB.Query<JobDataModel>(strSql);
                return list[0];
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DB").Fatal(ex.ToString());
                return null;
            }
        }
        public int Insert(JobDataModel model)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("insert into JobDataInfo ");
                sb.Append("(GlassID,JobData,ReceiveTime,GlassNum)");
                sb.Append("values(@GlassID,@JobData,@ReceiveTime,@GlassNum)");
                return DBHelper.DB.Execute(sb.ToString(), model);
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DB").Fatal(ex.ToString());
                return -1;
            }
        }
        public int Update(JobDataModel model)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("update JobDataInfo ");
                sb.Append("set SetOutTime=@SetOutTime,JobData=@JobData ");
                sb.Append("where GlassID=@GlassID");
                return DBHelper.DB.ExecuteNonQuery(sb.ToString(), CommandType.Text,
                    new KeyValuePair<string, object>("@GlassID", model.GlassID),
                    new KeyValuePair<string, object>("@SetOutTime", DateTime.Now),
                     new KeyValuePair<string, object>("@JobData", model.JobData)
                    );
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DB").Fatal(ex.ToString());
                return -1;
            }
        }
        public int Delete(string glassID,DateTime time)
        {
            try
            {
                string strSql = "delete  from JobDataInfo where GlassID='" + glassID + "' and ReceiveTime='"+time+"'";
                return DBHelper.DB.Execute(strSql);
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
                sb.Append("delete from JobDataInfo ");
                sb.Append("where ReceiveTime<@ReceiveTime ");
                return DBHelper.DB.Execute(sb.ToString(), new { ReceiveTime = DateTime.Now.AddDays(-days) });
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DB").Fatal(ex.ToString());
                return 0;
            }
        }
    }
}
