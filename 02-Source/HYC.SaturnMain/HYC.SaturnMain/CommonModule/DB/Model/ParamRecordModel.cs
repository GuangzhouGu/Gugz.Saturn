using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonDB.Model
{
    public class ParamRecordModel
    {
        public string RecipeName { get; set; }
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
        /// 原参数
        /// </summary>
        public string SourceParam { get; set; }
        /// <summary>
        /// 更改后参数
        /// </summary>
        public string ModifiedParam { get; set; }
        /// <summary>
        /// 参数名
        /// </summary>
        public string ParaName { get; set; }
        /// <summary>
        /// 参数类别
        /// </summary>
        public string ParaCategory { get; set; }
    }
}
