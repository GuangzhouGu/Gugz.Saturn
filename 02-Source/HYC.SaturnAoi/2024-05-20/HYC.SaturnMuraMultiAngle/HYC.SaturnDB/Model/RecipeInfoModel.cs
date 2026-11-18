/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2020 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnDB.Model
// 文件名称    ：RecipeInfoModel.cs
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HYC.SaturnDB.Model
{
    public class RecipeInfoModel
    {
        public RecipeInfoModel()
        {
            RecipeID = string.Empty;
            PPID = string.Empty;
            Remark = string.Empty;
        }

        public string RecipeID { get; set; }
        public string PPID { get; set; }
        public string Remark { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
