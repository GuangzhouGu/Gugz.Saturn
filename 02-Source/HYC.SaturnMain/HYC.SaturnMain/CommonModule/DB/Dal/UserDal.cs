using CommonDB.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonDB.Dal
{
    public class UserDal:BaseDal
    {
        public List<UserModel> GetAll()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select * from UserInfo ");
            DataSet ds = Db.ExecuteDataSet(sb.ToString(), CommandType.Text);
            List<UserModel> lst = new List<UserModel>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                UserModel model = new UserModel();
                model.UserName = ds.Tables[0].Rows[i]["UserName"].ToString();
                model.Password = ds.Tables[0].Rows[i]["Password"].ToString();
                model.UserLevel = Convert.ToInt32(ds.Tables[0].Rows[i]["UserLevel"].ToString());
                model.UserID = Convert.ToInt32(ds.Tables[0].Rows[i]["UserID"].ToString());
                lst.Add(model);
            }
            return lst;
        }

        public int Insert(UserModel model)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("insert into UserInfo ");
            sb.Append("(ID,UserName,Password,UserLevel,UserID) ");
            sb.Append("values(newid(),@UserName,@Password,@UserLevel,@UserID) ");
            return Db.ExecuteNonQuery(sb.ToString(), CommandType.Text,
                new KeyValuePair<string, object>("@UserName", model.UserName),
                new KeyValuePair<string, object>("@Password", model.Password),
                new KeyValuePair<string, object>("@UserLevel", model.UserLevel),
                new KeyValuePair<string, object>("@UserID", model.UserID));
        }

        public int Delete(UserModel model)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("delete from UserInfo ");
            sb.Append("where UserName=@UserName ");
            return Db.ExecuteNonQuery(sb.ToString(), CommandType.Text,
                new KeyValuePair<string, object>("@UserName", model.UserName));
        }

        public int DeleteAll()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("delete from UserInfo ");
            return Db.ExecuteNonQuery(sb.ToString(), CommandType.Text);
        }

        public int Update(UserModel model)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("update UserInfo ");
            sb.Append("set Password=@Password,UserLevel=@UserLevel,UserID=@UserID ");
            sb.Append("where UserName=@UserName ");
            return Db.ExecuteNonQuery(sb.ToString(), CommandType.Text,
                new KeyValuePair<string, object>("@UserName", model.UserName),
                new KeyValuePair<string, object>("@Password", model.Password),
                new KeyValuePair<string, object>("@UserLevel", model.UserLevel),
                new KeyValuePair<string, object>("@UserID", model.UserID));
        }
    }
}
