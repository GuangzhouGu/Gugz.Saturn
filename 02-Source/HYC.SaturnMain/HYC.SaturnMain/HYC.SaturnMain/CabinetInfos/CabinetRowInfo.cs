/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2020 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnMain.CabinetInfos
// 文件名称    ：CabinetRowInfo.cs
// =================================
// 创 建 者    ：guguangzhou
// 创建日期    ：2022/10/29 09:00:00 
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
using HYC.SaturnDB.DAL;
using HYC.SaturnDB.Model;
using HYC.SaturnMain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYC.SaturnMain.CabinetInfos
{
   public class CabinetRowInfo
    {
        public string RecipeName { get; set; } = string.Empty;
        /// <summary>
        ///Probe名称
        /// </summary>
        public string ProbeName { get; set; } = string.Empty;
        /// <summary>
        /// 料仓层号
        /// </summary>
        public int CabinetRowNum { get; set; } = -1;
        public string RFID { get; set; } = string.Empty;
 
        /// <summary>
        /// 当前料仓检测ProbeCard感应器名称
        /// </summary>
        public string PrbDetectSensorName { get; set; } = string.Empty;
        /// <summary>
        /// 当前料仓是否使用
        /// </summary>
        public bool IsUsed { get; set; }
    }
}
