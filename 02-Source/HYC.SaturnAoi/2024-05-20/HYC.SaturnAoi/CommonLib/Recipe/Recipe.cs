/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2021 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：PPITester.Entities
// 文件名称    ：Recipe.cs
// =================================
// 创 建 者    ：guguangzhou
// 创建日期    ：2021/01/29 09:00:00 
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
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using CommonLib.Entities;

namespace HYC.HTPCBase.Extension.Recipe
{
    [Serializable]
    public class Recipe
    {
        /// <summary>
        /// 机种名称
        /// </summary>
        public string RecipeID { get; set; }
        /// <summary>
         /// PPID
        /// </summary>
        public string PPID { get; set; }
        /// <summary>
        /// 机种编号
        /// </summary>
        public string RecipeNum { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; }
        [System.Xml.Serialization.XmlIgnore]
        public ParaManager Para { get; set; }
        [System.Xml.Serialization.XmlIgnore]
        public bool InitOK { get; set; }
        public Recipe()
        {
            RecipeID = string.Empty;
            PPID = string.Empty;
            RecipeNum = string.Empty;
            Para = new ParaManager();
        }
        public bool Load()
        {
            Para.RecipeName = RecipeID;
            InitOK = Para.LoadPara();
            return InitOK;
        }

    }
}
