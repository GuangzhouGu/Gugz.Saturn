/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2023 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnDB.DAL
// 文件名称    ：Class1
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
    public class CellStartTimeDal
    {
        /// <summary>
        /// 获取指定ID的记录
        /// </summary>
        /// <param name="scanID"></param>
        /// <returns></returns>
        public CellStartTimeModel GetByID(string glassID)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("select * from CellStartTimeInfo ");
                sb.Append("where PanelID=@PanelID order by StartTime desc");
                return DBHelper.DB.Query<CellStartTimeModel>(sb.ToString(), new { PanelID = glassID}).FirstOrDefault();
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
                StringBuilder sb = new StringBuilder();
                sb.Append("insert into CellStartTimeInfo ");
                sb.Append("(PanelID,StartTime) ");
                sb.Append("values(@PanelID,@StartTime) ");
                return DBHelper.DB.Execute(sb.ToString(), model);
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DB").Fatal(ex.ToString());
                return 0;
            }
        }
        public int Update(string oldGlassID,string newGlassID)
        {
            var model=GetByID(oldGlassID);
            StringBuilder sb = new StringBuilder();
            sb.Append("update CellStartTimeInfo ");
            sb.Append($"set PanelID='{newGlassID}' where PanelID='{oldGlassID}' and StartTime='{model.StartTime}'");
            return DBHelper.DB.Execute(sb.ToString());
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
