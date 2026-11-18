/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2023 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：CIMBOE
// 文件名称    ：LinkSignalAddr
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIMBOE
{
    public class LinkSignalAddr
    {
        /// <summary>
        /// 上游LinkSignal信号给本机的地址
        /// </summary>
        public string UpToLocalAddr { get; set; }
        /// <summary>
        /// 下游LinkSignal信号给本机的地址
        /// </summary>
        public string DownToLocalAddr { get; set; }
        /// <summary>
        /// 本机LinkSignal信号给上游的地址
        /// </summary>
        public string LocalToUpAddr { get; set; }
        /// <summary>
        /// 本机LinkSignal信号给下游的地址
        /// </summary>
        public string LocalToDownAddr { get; set; }
        public string UpToLocalWordAddr1 { get; set; }
        public string UpToLocalWordAddr2 { get; set; }
        public string LocalToDownWordAddr1 { get; set; }
        public string LocalToDownWordAddr2 { get; set; }
    }
}
