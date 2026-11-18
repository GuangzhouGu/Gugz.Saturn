/*----------------------------------------------------------------
// Copyright © 2021 HYC. All Rights Reserved
// 版权所有。
//
// =================================
// CLR版本     ：4.0.30319.42000
// 命名空间    ：HYC.DbQuery.DAL
// 文件名称    ：CellStartTimeDal.cs
// =================================
// 创 建 者    ：gugz
// 创建日期    ：2022/3/5 12:59:10 
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

using CommonDB;
using CommonDB.Model;
using HYC.HTLog;
using System;
using System.Linq;
using System.Text;

namespace DBQuery.DAL
{
    public class CellStartTimeDal:BaseDal
    {
        /// <summary>
        /// 获取指定ID的记录
        /// </summary>
        /// <param name="scanID"></param>
        /// <returns></returns>
        public CellStartTimeModel GetByID(string panelID)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("select * from CellStartTimeInfo ");
                sb.Append("where PanelID=@PanelID order by StartTime desc");
                return Db.Query<CellStartTimeModel>(sb.ToString(), new { PanelID = panelID }).FirstOrDefault();
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
        public int Insert(CellStartTimeModel model)
        {
            try
            {
                int ret = Update(model);
                if (ret <= 0)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("insert into CellStartTimeInfo ");
                    sb.Append("(PanelID,StartTime) ");
                    sb.Append("values(@PanelID,@StartTime) ");
                    return Db.Execute(sb.ToString(), model);
                }
                else
                {
                    return ret;
                }
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DB").Fatal(ex.ToString());
                return 0;
            }
        }
        public int Update(CellStartTimeModel model)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("update CellStartTimeInfo ");
                sb.Append("set StartTime=@StartTime where ");
                sb.Append("PanelID=@PanelID ");
                return Db.Execute(sb.ToString(), model);
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DB").Fatal(ex.ToString());
                return 0;
            }
        }
        public int Delete(string jobID)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("delete from CellStartTimeInfo ");
                sb.Append("where PanelID=@PanelID ");
                return Db.Execute(sb.ToString(), new { PanelID = jobID });
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
                sb.Append("delete from CellStartTimeInfo ");
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
