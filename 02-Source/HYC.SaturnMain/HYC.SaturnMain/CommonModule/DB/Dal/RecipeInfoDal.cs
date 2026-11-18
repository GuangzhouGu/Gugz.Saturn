using CommonDB.Model;
using HYC.HTLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonDB.Dal
{
    public class RecipeInfoDal:BaseDal
    {
        public List<RecipeInfoModel> GetAll()
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("select * from RecipeInfo");
                DataSet ds = Db.ExecuteDataSet(sb.ToString(), CommandType.Text);
                List<RecipeInfoModel> lst = new List<RecipeInfoModel>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    RecipeInfoModel model = new RecipeInfoModel();
                    model.RecipeID = ds.Tables[0].Rows[i]["RecipeID"].ToString();
                    model.PPID = ds.Tables[0].Rows[i]["PPID"].ToString();
                    model.UpdateTime = (DateTime)ds.Tables[0].Rows[i]["UpdateTime"];
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

        public int Insert(RecipeInfoModel model)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("insert into RecipeInfo ");
                sb.Append("(RecipeID,PPID,UpdateTime) ");
                sb.Append("values(@RecipeID,@PPID,@Remark,@UpdateTime) ");
                return Db.ExecuteNonQuery(sb.ToString(), CommandType.Text,
                   new KeyValuePair<string, object>("@RecipeID", model.RecipeID),
                   new KeyValuePair<string, object>("@PPID", model.PPID),
                      new KeyValuePair<string, object>("@UpdateTime", model.UpdateTime)
                  );
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DB").Fatal(ex.ToString());
                return 0;
            }
        }

        public int Delete(RecipeInfoModel model)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("delete from RecipeInfo ");
                sb.Append("where RecipeID=@RecipeID ");
                return Db.ExecuteNonQuery(sb.ToString(), CommandType.Text,
                    new KeyValuePair<string, object>("@RecipeID", model.RecipeID));
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DB").Fatal(ex.ToString());
                return 0;
            }
        }

        public int Update(RecipeInfoModel model)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("update RecipeInfo ");
                sb.Append("set PPID=@PPID,UpdateTime=@UpdateTime ");
                sb.Append("where RecipeID=@RecipeID ");
                return Db.ExecuteNonQuery(sb.ToString(), CommandType.Text,
                    new KeyValuePair<string, object>("@PPID", model.PPID),
                    new KeyValuePair<string, object>("@RecipeID", model.RecipeID),
                    new KeyValuePair<string, object>("@UpdateTime", model.UpdateTime));
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DB").Fatal(ex.ToString());
                return 0;
            }
        }
        public int UpdateRecipeID(string oldRecipe, string newRecipe)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("update RecipeInfo ");
                sb.Append($"set RecipeID='{newRecipe}' ");
                sb.Append($"where RecipeID='{oldRecipe}'");
                return Db.Execute(sb.ToString());
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
    }
}
