/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2022 HYC. All Rights Reserved.
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
using HYC.SaturnDB.Model;
using HYC.HTLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace HYC.SaturnDB.DAL
{
    public class CimMessageDal
    {
        public List<CimMessageModel> GetIsSet(bool isSet)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("select * from CimMessage where IsSet=@IsSet ");
                DataSet ds = DBHelper.DB.ExecuteDataSet(sb.ToString(), CommandType.Text,
                    new KeyValuePair<string, object>("@IsSet", isSet));
                List<CimMessageModel> lst = new List<CimMessageModel>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    CimMessageModel model = new CimMessageModel();
                    model.MessageID = Convert.ToInt32(ds.Tables[0].Rows[i]["MessageID"]);
                    model.ReceivedTime = (DateTime)ds.Tables[0].Rows[i]["ReceivedTime"];
                    model.Message = ds.Tables[0].Rows[i]["Message"].ToString();
                    model.IsSet = Convert.ToBoolean(ds.Tables[0].Rows[i]["IsSet"]);
                    lst.Add(model);
                }
                return lst;
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DB").Fatal(ex.ToString());
                return null;
          
            }
        }

        public List<CimMessageModel> GetTop20()
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("select * from CimMessage order by ReceivedTime desc limit 20");
                DataSet ds = DBHelper.DB.ExecuteDataSet(sb.ToString(), CommandType.Text);
                List<CimMessageModel> lst = new List<CimMessageModel>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    CimMessageModel model = new CimMessageModel();
                    model.MessageID = Convert.ToInt32(ds.Tables[0].Rows[i]["MessageID"]);
                    model.ReceivedTime = (DateTime)ds.Tables[0].Rows[i]["ReceivedTime"];
                    model.Message = ds.Tables[0].Rows[i]["Message"].ToString();
                    model.IsSet = Convert.ToBoolean(ds.Tables[0].Rows[i]["IsSet"]);
                    lst.Add(model);
                }
                return lst;
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DB").Fatal(ex.ToString());
                return null;
            }
        }

        public int Insert(CimMessageModel model)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("insert into CimMessage ");
                sb.Append("(MessageID,ReceivedTime,Message,IsSet) ");
                sb.Append("values(@MessageID,@ReceivedTime,@Message,@IsSet) ");
                return DBHelper.DB.ExecuteNonQuery(sb.ToString(), CommandType.Text,
                    new KeyValuePair<string, object>("@MessageID", model.MessageID),
                    new KeyValuePair<string, object>("@ReceivedTime", model.ReceivedTime),
                    new KeyValuePair<string, object>("@Message", model.Message),
                    new KeyValuePair<string, object>("@IsSet", model.IsSet));
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DB").Fatal(ex.ToString());
                return -1;
            }
        }

        public int Update(CimMessageModel model)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("update CimMessage ");
                sb.Append("set Message=@Message,IsSet=@IsSet ");
                sb.Append("where MessageID=@MessageID and ReceivedTime=@ReceivedTime ");
                return DBHelper.DB.ExecuteNonQuery(sb.ToString(), CommandType.Text,
                    new KeyValuePair<string, object>("@MessageID", model.MessageID),
                    new KeyValuePair<string, object>("@ReceivedTime", model.ReceivedTime),
                    new KeyValuePair<string, object>("@Message", model.Message),
                    new KeyValuePair<string, object>("@IsSet", model.IsSet));
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DB").Fatal(ex.ToString());
                return -1;
            }
        }

        public int Delete(string messageid)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("delete from CimMessage ");
            sb.Append("where MessageID=@MessageID  ");
            return DBHelper.DB.ExecuteNonQuery(sb.ToString(), CommandType.Text,
                new KeyValuePair<string, object>("@MessageID", messageid));
        }

        public int Delete(int days)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("delete from CimMessage ");
            sb.Append("where ReceivedTime<@ReceivedTime ");
            return DBHelper.DB.ExecuteNonQuery(sb.ToString(), CommandType.Text,
                new KeyValuePair<string, object>("@ReceivedTime", DateTime.Now.Date.AddDays(-days)));
        }
    }
}
