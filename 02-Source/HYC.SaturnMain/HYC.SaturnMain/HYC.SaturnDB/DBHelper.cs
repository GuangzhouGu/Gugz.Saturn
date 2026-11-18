using HYC.HTDB;
using HYC.HTLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace HYC.SaturnDB
{
    public class DBHelper
    {
        public static HTDBHelper DB = null;
        public static void Init(string connStr)
        {
            DB = HTDBHelper.CreateSQLServer();
            DB.ConnectionString = connStr;
        }
     
    }
}
