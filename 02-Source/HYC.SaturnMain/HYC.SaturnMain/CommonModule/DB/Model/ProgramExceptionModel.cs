using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonDB.Model
{
    public class ProgramExceptionModel
    {
        /// <summary>
        /// 类名
        /// </summary>
        public string ClassName { get; set; }
        /// <summary>
        /// 方法名
        /// </summary>
        public string MethodName { get; set; }
        /// <summary>
        /// 异常信息
        /// </summary>
        public string ExceptionMessage { get; set; }
        /// <summary>
        /// 异常位置
        /// </summary>
        public string StackTrace { get; set; }
        /// <summary>
        /// 异常时间
        /// </summary>
        public DateTime ExceptionTime { get; set; }
    }
}
