/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2020 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnDB.DAL
// 文件名称    ：ProbeCardDal.cs
// =================================
// 创 建 者    ：guguangzhou
// 创建日期    ：2022/10/29 09:00:00 
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace HYC.SaturnDB.DAL
{
    public class ProbeCardDal
    {
        public int Insert(ProbeCardModel model)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("insert into ProbeCardInfo ");
                sb.Append("(RecipeName,RFID,CabinetRowNum,Status,ContactedCount,InCabinetTime,BlockCount,ProbeCardName) ");
                sb.Append("values(@RecipeName,@RFID,@CabinetRowNum,@Status,@ContactedCount,@InCabinetTime,@BlockCount,@ProbeCardName)");
                return DBHelper.DB.Execute(sb.ToString(), model);
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DB").Fatal(ex.ToString());
                return 0;
            }
        }
        public int UpdateContactCount(string recipeName)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("update ProbeCardInfo ");
                sb.Append($"set ContactedCount=ContactedCount+1 ");
                sb.Append($"where RecipeName=@RecipeName");
                return DBHelper.DB.Execute(sb.ToString(), new { RecipeName = recipeName });
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DB").Fatal(ex.ToString());
                return 0;
            }
        }
        /// <summary>
        /// 更新ProbeCard状态
        /// </summary>
        /// <param name="time"></param>
        /// <param name="timeType"></param>
        /// <returns></returns>
        public int UpdateStatus(ProbeCardSts sts,string recipeName)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("update ProbeCardInfo ");
                if (sts == ProbeCardSts.InUvw)
                {
                    sb.Append($"set LoadInUvwTime=GetDate(),Status='{ProbeCardSts.InUvw}' ");
                }
                else if (sts == ProbeCardSts.OutUvw||sts==ProbeCardSts.InCabinet)//注意，从Uvw上卸载下来，状态要切换成InCabinet
                {
                    sb.Append($"set UnLoadFromUvwTime=GetDate(),Status='{ProbeCardSts.InCabinet}' ");
                }
                else if (sts == ProbeCardSts.OutEqp)
                {
                    sb.Append($"set OutCabinetTime=GetDate(),Status='{ProbeCardSts.OutEqp}' ");
                }
                sb.Append($"where RecipeName=@RecipeName");
                return DBHelper.DB.Execute(sb.ToString(),new { RecipeName= recipeName });
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DB").Fatal(ex.ToString());
                return 0;
            }
        }
        public int UpdateStatusByProbeName(ProbeCardSts sts, string probeName)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("update ProbeCardInfo ");
                if (sts == ProbeCardSts.InUvw)
                {
                    sb.Append($"set LoadInUvwTime=GetDate(),Status='{ProbeCardSts.InUvw}' ");
                }
                else if (sts == ProbeCardSts.OutUvw || sts == ProbeCardSts.InCabinet)//注意，从Uvw上卸载下来，状态要切换成InCabinet
                {
                    sb.Append($"set UnLoadFromUvwTime=GetDate(),Status='{ProbeCardSts.InCabinet}' ");
                }
                else if (sts == ProbeCardSts.OutEqp)
                {
                    sb.Append($"set OutCabinetTime=GetDate(),Status='{ProbeCardSts.OutEqp}' ");
                }
                sb.Append($"where ProbeCardName=@ProbeCardName");
                return DBHelper.DB.Execute(sb.ToString(), new { ProbeCardName = probeName });
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DB").Fatal(ex.ToString());
                return 0;
            }
        }
        public int UpdateProbeName(string oldProbeName,string newProbeName)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("update ProbeCardInfo ");
                sb.Append($"set ProbeCardName='{newProbeName}' ");
                sb.Append($"where ProbeCardName=@ProbeCardName");
                return DBHelper.DB.Execute(sb.ToString(), new { ProbeCardName = oldProbeName });
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DB").Fatal(ex.ToString());
                return 0;
            }
        }
        public int UpdateRowNum(string recipeName,int rowNum)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("update ProbeCardInfo ");
                sb.Append($"set CabinetRowNum='{rowNum}' ");
                sb.Append($"where RecipeName=@RecipeName");
                return DBHelper.DB.Execute(sb.ToString(), new { RecipeName = recipeName });
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DB").Fatal(ex.ToString());
                return 0;
            }
        }
        /// <summary>
        /// 根据ID查询记录
        /// </summary>
        /// <param name="panelId"></param>
        /// <param name="StartTime"></param>
        /// <returns></returns>
        public List<ProbeCardModel> GetList(string recipeName = "")
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                if (string.IsNullOrEmpty(recipeName))
                {
                    sb.Append("select * from ProbeCardInfo ");
                }
                else
                {
                    sb.Append($"select * from ProbeCardInfo where RecipeName='{recipeName}'");
                }
                return DBHelper.DB.Query<ProbeCardModel>(sb.ToString());
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DB").Fatal(ex.ToString());
                return null;
            }
        }
        public DataSet GetAll()
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("select * from ProbeCardInfo ");
                return DBHelper.DB.ExecuteDataSet(sb.ToString());
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DB").Fatal(ex.ToString());
                return null;
            }
        }
      
        public void DeleteByProbeName(string probeName)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                 sb.Append("delete  from ProbeCardInfo where ProbeCardName=@ProbeCardName ");
                 DBHelper.DB.Execute(sb.ToString(),new { ProbeCardName= probeName });
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DB").Fatal(ex.ToString());
                
            }
        }
        public void DeleteByRecipeName(string recipeName)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("delete  from ProbeCardInfo where RecipeName=@RecipeName ");
                DBHelper.DB.Execute(sb.ToString(), new { RecipeName = recipeName });
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DB").Fatal(ex.ToString());

            }
        }
    }
}
