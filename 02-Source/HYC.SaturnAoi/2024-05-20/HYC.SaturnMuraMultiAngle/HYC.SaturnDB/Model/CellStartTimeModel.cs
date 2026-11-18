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
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYC.SaturnDB.Model
{
    public class CellStartTimeModel
    {   
        /// <summary>
        /// 扫码枪扫到的ID
        /// </summary>
        [Key]
        public string PanelID { get; set; }

        /// <summary>
        /// 扫描到ID的时间
        /// </summary>
        public DateTime StartTime { get; set; }
    }
}
