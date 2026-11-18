using HYC.HTDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonDB
{
    /// <summary>
    /// 数据库查询基类
    /// </summary>
    public class BaseDal
    {
        /// <summary>
        /// 默认的连接字符串
        /// </summary>
        public static string ConnectionString = "";
        /// <summary>
        /// 数据库类型，默认为MySQL
        /// </summary>
        public static AlarmSQLType SQLType = AlarmSQLType.MySQL;
        /// <summary>
        /// 数据库访问对象
        /// </summary>
        protected HTDBHelper Db = null;

        /// <summary>
        /// 构造方法
        /// </summary>
        public BaseDal()
        {
            switch (SQLType)
            {
                case AlarmSQLType.MySQL:
                    Db = HTDBHelper.CreateMySQL();
                    break;
                case AlarmSQLType.SQLServer:
                    Db = HTDBHelper.CreateSQLServer();
                    break;
                case AlarmSQLType.SQLite:
                    Db = HTDBHelper.CreateSQLite();
                    break;
                default:
                    break;
            }
            Db.ConnectionString = ConnectionString;
            Db.Log += Log;
        }

        /// <summary>
        /// 日志输出
        /// </summary>
        public static event Action<string> Log;
    }

    /// <summary>
    /// 数据库类型
    /// </summary>
    public enum AlarmSQLType
    {
        /// <summary>
        /// MySQL
        /// </summary>
        MySQL,
        /// <summary>
        /// SQLServer
        /// </summary>
        SQLServer,
        /// <summary>
        /// SQLite
        /// </summary>
        SQLite
    }
}
