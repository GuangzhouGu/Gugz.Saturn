/*----------------------------------------------------------------
// Copyright (C) Suzhou HYC Electronic Technology Co.,LTD
// 版权所有。
//
// =================================
// CLR版本     ：4.0.30319.42000
// 命名空间    ：HYC.AutoPosition.Recipes
// 文件名称    ：RecipeManager.cs
// =================================
// 创 建 者    ：yangkang
// 创建日期    ：2019/12/30 16:14:56 
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
using HYC.HTFile.HTXML;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYC.AutoPosition.Recipes
{
    /// <summary>
    /// 机种管理类
    /// </summary>
    class RecipeManager
    {
        /// <summary>
        /// 机种列表
        /// </summary>
        private List<Recipe> lstRecipe = new List<Recipe>();
        /// <summary>
        /// 当前机种
        /// </summary>
        public Recipe CurRecipe = null;
        /// <summary>
        /// 机种文件路径
        /// </summary>
        public string FilePath = AppDomain.CurrentDomain.BaseDirectory + @"AutoPos\";
        /// <summary>
        /// 机种是否已选择
        /// </summary>
        public bool IsRecipeSelected = false;
        string RecipeInfoPath = AppDomain.CurrentDomain.BaseDirectory + @"AutoPos\RecipeInfo.xml";
        /// <summary>
        /// 加载机种列表
        /// </summary>
        public void LoadRecipeList()
        {
            lstRecipe.Clear();

            //判断文件夹是否存在，不存在则创建
            DirectoryInfo folderRoot = new DirectoryInfo(FilePath);
            if (folderRoot.Exists == false)
                Directory.CreateDirectory(FilePath);        
            //获取子文件夹，每一个文件夹代表一个机种
            DirectoryInfo[] folderID = folderRoot.GetDirectories();
            foreach (var item in folderID)
            {
                Recipe recipe = new Recipe();
                recipe.RecipeID = item.Name;
                recipe.Location = item.FullName;

                lstRecipe.Add(recipe);
            }
            //机种排序
            lstRecipe.Sort((x, y) => x.RecipeID.CompareTo(y.RecipeID));
            var recipeInfoList = HTXMLSerializer.EntityFromXMLFile<List<RecipeBase>>(RecipeInfoPath);
            if (recipeInfoList != null)
            {
                foreach (var item in recipeInfoList)
                {
                    var ret = lstRecipe.Where(p => p.RecipeID == item.RecipeID).ToArray();
                    if (ret.Length > 0)
                    {
                        ret[0].PPID = item.PPID;
                        ret[0].CreateTime = item.CreateTime;
                    }
                }
            }
        }

        /// <summary>
        /// 获取所有机种
        /// </summary>
        /// <returns>机种列表</returns>
        public List<Recipe> GetRecipes()
        {
            return lstRecipe;
        }

        /// <summary>
        /// 根据机种名称获取机种
        /// </summary>
        /// <param name="name">机种名</param>
        /// <returns>机种实例</returns>
        public Recipe GetRecipeByName(string name)
        {
            var reps = lstRecipe.Where(p => p.RecipeID == name).ToList();
            if (reps.Count > 0)
            {
                return reps[0];
            }
            else
            {
                return null;
            }
        }
    }
}
