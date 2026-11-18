using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonDB.Model
{
    public class OpRecordModel
    {
        /// <summary>
        /// 当前用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OpTime { get; set; }
        /// <summary>
        /// 操作内容
        /// </summary>
        public string OpContent { get; set; }
       
    }
}
