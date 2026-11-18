/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2023 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnDB.Model
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYC.SaturnDB.Model
{
    public class DfsUploadInfoModel
    {
        public DfsUploadInfoModel()
        {
            GlassID = string.Empty;
            FilePath = string.Empty;
        }

        public string GlassID { get; set; }
        public DateTime StartTime { get; set; }
        public int PcIndex { get; set; }
        public string FilePath { get; set; }
        /// <summary>
        /// -1-本地文件丢失 0-上传成功 1-等待上传 2-上传失败
        /// </summary>
        public int SendFlg { get; set; }
        public string DetectType { get; set; }
    }
}
