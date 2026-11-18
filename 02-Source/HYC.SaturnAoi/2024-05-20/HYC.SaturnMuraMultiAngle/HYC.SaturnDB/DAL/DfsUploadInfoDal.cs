using HYC.HTLog;
using HYC.SaturnDB.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace HYC.SaturnDB.DAL
{
    public class DfsUploadInfoDal
    {
        public List<DfsUploadInfoModel> GetAll(string GlassID)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("select * from DfsUploadInfo ");
                sb.Append("where GlassID=@GlassID ");
                sb.Append("order by StartTime desc ");
                DataSet ds = DBHelper.DB.ExecuteDataSet(sb.ToString(), CommandType.Text,
                    new List<KeyValuePair<string, object>>() { new KeyValuePair<string, object>("@GlassID", GlassID) }.ToArray());

                List<DfsUploadInfoModel> lst = new List<DfsUploadInfoModel>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DfsUploadInfoModel model = new DfsUploadInfoModel();
                    model.GlassID = ds.Tables[0].Rows[i]["GlassID"].ToString();
                    model.StartTime = (DateTime)ds.Tables[0].Rows[i]["StartTime"];
                    model.PcIndex = ds.Tables[0].Rows[i]["PcIndex"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[i]["PcIndex"]) : 0;
                    model.FilePath = ds.Tables[0].Rows[i]["FilePath"].ToString();
                    model.SendFlg = Convert.ToInt32(ds.Tables[0].Rows[i]["SendFlg"]);
                    model.DetectType = ds.Tables[0].Rows[i]["DetectType"].ToString();
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

        public List<DfsUploadInfoModel> GetAll(int sendFlg = 1)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("select * from DfsUploadInfo ");
                sb.Append("where SendFlg=@SendFlg ");
                DataSet ds = DBHelper.DB.ExecuteDataSet(sb.ToString(), CommandType.Text, new List<KeyValuePair<string, object>>() { new KeyValuePair<string, object>("@SendFlg", sendFlg) }.ToArray());

                List<DfsUploadInfoModel> lst = new List<DfsUploadInfoModel>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DfsUploadInfoModel model = new DfsUploadInfoModel();
                    model.GlassID = ds.Tables[0].Rows[i]["GlassID"].ToString();
                    model.StartTime = (DateTime)(ds.Tables[0].Rows[i]["StartTime"]);
                    model.PcIndex = ds.Tables[0].Rows[i]["PcIndex"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[i]["PcIndex"]) : 0;
                    model.FilePath = ds.Tables[0].Rows[i]["FilePath"].ToString();
                    model.SendFlg = Convert.ToInt32(ds.Tables[0].Rows[i]["SendFlg"]);
                    model.DetectType = ds.Tables[0].Rows[i]["DetectType"].ToString();
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

        public int Insert(DfsUploadInfoModel model)
        {
            try
            {
                List<KeyValuePair<string, object>> lstParams = new List<KeyValuePair<string, object>>();
                StringBuilder sb = new StringBuilder();
                sb.Append("insert into DfsUploadInfo ");
                sb.Append("(GlassID,StartTime,PcIndex,FilePath,SendFlg,DetectType) ");
                sb.Append("values(@GlassID,@StartTime,@PcIndex,@FilePath,@SendFlg,@DetectType) ");
                lstParams.Add(new KeyValuePair<string, object>("@GlassID", model.GlassID));
                lstParams.Add(new KeyValuePair<string, object>("@StartTime", model.StartTime));
                lstParams.Add(new KeyValuePair<string, object>("@PcIndex", model.PcIndex));
                lstParams.Add(new KeyValuePair<string, object>("@FilePath", model.FilePath));
                lstParams.Add(new KeyValuePair<string, object>("@SendFlg", model.SendFlg));
                lstParams.Add(new KeyValuePair<string, object>("@DetectType", model.DetectType));
                return DBHelper.DB.ExecuteNonQuery(sb.ToString(), CommandType.Text, lstParams.ToArray());
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DB").Fatal(ex.ToString());
                return 0;
            }
        }

        public int Update(DfsUploadInfoModel model)
        {
            try
            {
                List<KeyValuePair<string, object>> lstParams = new List<KeyValuePair<string, object>>();
                StringBuilder sb = new StringBuilder();
                sb.Append("update DfsUploadInfo ");
                sb.Append("set PcIndex=@PcIndex,FilePath=@FilePath,SendFlg=@SendFlg ");
                sb.Append("where GlassID=@GlassID and StartTime=@StartTime ");
                lstParams.Add(new KeyValuePair<string, object>("@GlassID", model.GlassID));
                lstParams.Add(new KeyValuePair<string, object>("@StartTime", model.StartTime));
                lstParams.Add(new KeyValuePair<string, object>("@PcIndex", model.PcIndex));
                lstParams.Add(new KeyValuePair<string, object>("@FilePath", model.FilePath));
                lstParams.Add(new KeyValuePair<string, object>("@SendFlg", model.SendFlg));
                lstParams.Add(new KeyValuePair<string, object>("@DetectType", model.DetectType));
                return DBHelper.DB.ExecuteNonQuery(sb.ToString(), CommandType.Text, lstParams.ToArray());
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DB").Fatal(ex.ToString());
                return 0;
            }
        }

        public int Delete(int days)
        {
            try
            {
                List<KeyValuePair<string, object>> lstParams = new List<KeyValuePair<string, object>>();
                StringBuilder sb = new StringBuilder();
                sb.Append("delete from DfsUploadInfo ");
                sb.Append("where StartTime<@StartTime ");
                lstParams.Add(new KeyValuePair<string, object>("@StartTime", DateTime.Now.Date.AddDays(-days)));
                return DBHelper.DB.ExecuteNonQuery(sb.ToString(), CommandType.Text, lstParams.ToArray());
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DB").Fatal(ex.ToString());
                return 0;
            }
        }
    }
}
