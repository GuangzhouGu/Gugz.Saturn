/*----------------------------------------------------------------
// Copyright (C)  Suzhou HYC Technology Co.,LTD
// 版权所有。
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：PPITester.Entities
// 文件名称    ：RecipeManager.cs
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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HYC.HTLog;

using CommonLib.Para;

namespace HYC.HTPCBase.Extension.Recipe
{
    public class RecipeManager
    {
        public static List<Recipe> lstRecipe = new List<Recipe>();
        public static Recipe CurRecipe { get; set; }
        public static string FilePath = AppDomain.CurrentDomain.BaseDirectory + @"Config\Recipe";
        public static string RecipeInfoPath = AppDomain.CurrentDomain.BaseDirectory + @"Config\RecipeInfo.xml";
        public static event Action<RecipeChangeType, Recipe> OnRecipeChange;
        public static void LoadRecipeList()
        {
            lstRecipe.Clear();
            try
            {
                DirectoryInfo dir = new DirectoryInfo(FilePath);
                DirectoryInfo[] folders = dir.GetDirectories();
                foreach (var item in folders)
                {
                    Recipe recipe = new Recipe { RecipeID = item.Name };
                    recipe.Load();
                    lstRecipe.Add(recipe);
                }
                var recipeInfoList = HTFile.HTXmlHelper.EntityFromXMLFile<List<Recipe>>(RecipeInfoPath);
                if (recipeInfoList != null)
                {
                    foreach (var item in recipeInfoList)
                    {
                        var ret = lstRecipe.Where(p => p.RecipeID == item.RecipeID).ToArray();
                        if (ret.Length > 0)
                        {
                            ret[0].PPID = item.PPID;
                            ret[0].RecipeNum = item.RecipeNum;
                            ret[0].CreateTime = item.CreateTime;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("RecipeManager").Fatal($"LoadRecipeList方法执行异常，异常信息：{ex.ToString()}");
            }

            lstRecipe.Sort((x, y) => x.RecipeID.CompareTo(y.RecipeID));
        }

        public static List<Recipe> GetRecipes()
        {
            LoadRecipeList();
            return lstRecipe;
        }
        public static bool ShiftRecipe(string recipeName)
        {
            CurRecipe = GetRecipeByName(recipeName);
            OnRecipeChange?.Invoke(RecipeChangeType.Delete, CurRecipe);
            LogManager.GetLogger("RecipeManager").Info($"机种：{recipeName}切换{(CurRecipe.InitOK?"成功":"失败")}");
            return CurRecipe.InitOK;
        }
        public static void DeleteByName(string recipeName)
        {
            var recipe = GetRecipeByName(recipeName);
            if (recipe!=null)
            {
                lstRecipe.Remove(recipe);
                SaveRecipeInfo();
                OnRecipeChange?.Invoke(RecipeChangeType.Delete, recipe);
            }
    
        }
        public static void AddRecipe(string recipeName, string ppid,string num)
        {
            LoadRecipeList();
            var recipe = GetRecipeByName(recipeName);
            recipe.PPID = ppid;
            recipe.RecipeNum = num;
            recipe.CreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            SaveRecipeInfo();
            OnRecipeChange?.Invoke(RecipeChangeType.Add, recipe);
        }
        public static bool CheckRecipe(string recipeNum)
        {
           return lstRecipe.Where(p => p.RecipeNum == recipeNum).Count() > 0;
        }
        public static Recipe GetRecipeByName(string name)
        {
            try
            {
                if (CurRecipe != null && name == CurRecipe.RecipeID)
                {
                    return CurRecipe;
                }
                var reps = lstRecipe.Where(p => p.RecipeID == name).ToList();
                if (reps.Count > 0)
                {
                   bool ret= reps[0].Load();
                    if (ret)
                    {
                        return reps[0];
                    }
                    else
                    {
                        LogManager.GetLogger("RecipeManager").Fatal($"{name}机种加载失败");
                        return null;
                    }
                }
                else
                {
                    LogManager.GetLogger("RecipeManager").Fatal($"找不到{name}机种");
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("RecipeManager").Fatal($"GetRecipeByName方法执行异常，异常信息：{ex.ToString()}");
                return null;
            }
        }
        public static Recipe GetRecipeByPPID(string name)
        {
            try
            {
                if (CurRecipe != null && name == CurRecipe.PPID)
                {
                    return CurRecipe;
                }
                var reps = lstRecipe.Where(p => p.PPID == name).ToList();
                if (reps.Count > 0)
                {
                    reps[0].Load();
                    return reps[0];
                }
                else
                {
                    LogManager.GetLogger("RecipeManager").Fatal($"找不到PPID为{name}的机种");
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("RecipeManager").Fatal($"GetRecipeByName方法执行异常，异常信息：{ex.ToString()}");
                return null;
            }
        }
        public static Recipe GetRecipeByNumber(string number)
        {
            try
            {
                if (CurRecipe != null && number == CurRecipe.RecipeNum)
                {
                    return CurRecipe;
                }
                var reps = lstRecipe.Where(p => p.RecipeNum == number).ToList();
                if (reps.Count > 0)
                {
                    return reps[0];
                }
                else
                {
                    LogManager.GetLogger("RecipeManager").Fatal($"找不到RecipeNumber为{number}的机种");
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("RecipeManager").Fatal($"GetRecipeByNumber方法执行异常，异常信息：{ex.ToString()}");
                return null;
            }
        }
        public static IPara GetPara<T>(T type)
        {
            return CurRecipe.Para.Get(type);
        }
        public static bool SaveRecipeInfo()
        {
            if (File.Exists(RecipeInfoPath))
            {
                return HTFile.HTXmlHelper.EntityToXMLFile(lstRecipe, RecipeInfoPath);
            }
            else
            {
                return false;
            }
           
        }
    
    }
    public enum RecipeChangeType
    {
        Shift,
        Add,
        Delete
    }
}
