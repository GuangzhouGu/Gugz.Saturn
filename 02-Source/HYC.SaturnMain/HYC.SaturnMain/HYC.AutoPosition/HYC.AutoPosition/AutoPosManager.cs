/*----------------------------------------------------------------
// Copyright (C) Suzhou HYC Electronic Technology Co.,LTD
// 版权所有。
//
// =================================
// CLR版本     ：4.0.30319.42000
// 命名空间    ：HYC.AutoPosition
// 文件名称    ：Manager.cs
// =================================
// 创 建 者    ：yangkang
// 创建日期    ：2020/1/2 13:42:36 
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
using HYC.AutoPosition.Algorithm;
using HYC.AutoPosition.Class;
using HYC.AutoPosition.Events;
using HYC.AutoPosition.Helper;
using HYC.AutoPosition.Interface;
using HYC.AutoPosition.Parameter;
using HYC.AutoPosition.Recipes;
using HYC.HTLog;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYC.AutoPosition
{
    /// <summary>
    /// 自动对位管理类
    /// </summary>
    public class AutoPosManager
    {
        /// <summary>
        /// 机种管理类
        /// </summary>
        private RecipeManager recipeManager = new RecipeManager();

        /// <summary>
        /// 工位数
        /// </summary>
        public int StageCount { get; private set; } = 1;

        /// <summary>
        /// 当前机种
        /// </summary>
        public Recipe CurRecipe
        {
            get { return recipeManager.CurRecipe; }
            set { recipeManager.CurRecipe = value; }
        }

        /// <summary>
        /// 获取所有机种
        /// </summary>
        /// <returns></returns>
        public List<Recipe> GetRecipes()
        {
            recipeManager.LoadRecipeList();
            return recipeManager.GetRecipes();
        }

        /// <summary>
        /// 初始化参数
        /// </summary>
        /// <param name="recipeName">机种名</param>
        /// <param name="stageCount">工位数</param>
        public void Init(string recipeName, int stageCount)
        {
            this.StageCount = stageCount;

            //加载机种信息
            recipeManager.LoadRecipeList();
            //根据机种名称，获取机种
            Recipe recipe = recipeManager.GetRecipeByName(recipeName);
            //如果机种不存在，new一个默认的机种
            if (recipe == null)
            {
                recipe = new Recipe();
                recipe.RecipeID = "Default";
                recipe.Location = recipeManager.FilePath + "Default";
            }
                    
            //加载机种参数
            for (int i = 0; i < this.StageCount; i++)
            {
                recipe. ReadDataPos(i);
            }
            //更新算法参数
            //UpdateModuleInfo(recipe);

            //选定机种
            recipeManager.CurRecipe = recipe;
            recipeManager.IsRecipeSelected = true;
            //加载Camera信息

            recipe.AlignParameter = AlignParameter.Unserialize(recipe.Location + "\\AlignLimitParameter.xml");

        }
                
        /// <summary>
        /// 更新参数给算法
        /// </summary>
        /// <param name="recipe"></param>
        private void UpdateModuleInfo(Recipe recipe)
        {
            for (int i = 0; i < this.StageCount; i++)
            {
                DetectionAlgorithmPos.HT_OnUpdateModuleInfo(i, recipe.DataPos[i].AlgorithmData);
            }
        }

   
        /// <summary>
        /// 从旧机种创建新机种
        /// </summary>
        /// <param name="recipeIDOld">旧机种名</param>
        /// <param name="recipeIDNew">新机种名</param>
        public void CreateNewRecipeFrom(string recipeIDOld, string recipeIDNew)
        {
            //加载机种信息
            recipeManager.LoadRecipeList();
            //根据机种名称，获取机种
            Recipe repOld = recipeManager.GetRecipeByName(recipeIDOld);
            //如果机种不存在，new一个默认的机种
            if (repOld == null)
            {
                repOld = new Recipe();
                repOld.RecipeID = "Default";
                repOld.Location = recipeManager.FilePath + "Default";
            }

            //加载机种参数
            for (int i = 0; i < StageCount; i++)
            {
                repOld.ReadDataPos(i);
            }
            Recipe repNew = DeepCopyHelper.DeepCopyWithBinarySerializer(repOld);
            repNew.RecipeID = recipeIDNew;
            repNew.Location = recipeManager.FilePath + recipeIDNew;

            //复制文件
            GetFilesAndDirs(repOld.Location, repNew.Location);
            //加载机种信息
            recipeManager.LoadRecipeList();
            //加载相机参数
            //repOld.CameraInformations = CameraInformationArray.Unserialize(repOld.Location + "\\CameraInformation.xml");
            repOld.AlignParameter = AlignParameter.Unserialize(repOld.Location + "\\AlignLimitParameter.xml");

        }
        private void GetFilesAndDirs(string srcDir, string destDir)
        {
            if (Directory.Exists(destDir) == false)
            {
                Directory.CreateDirectory(destDir);
            }
            FileInfo fileInfo;
            string newPath;
            string[] files = Directory.GetFiles(srcDir);
            foreach (string path in files)          //遍历文件     
            {
                fileInfo = new FileInfo(path);
                newPath = Path.Combine(destDir, fileInfo.Name);
                File.Copy(path, newPath, true);
            }
            string[] dirs = Directory.GetDirectories(srcDir);
            foreach (string path in dirs)        //遍历文件夹
            {
                DirectoryInfo directory = new DirectoryInfo(path);
                string newDir = Path.Combine(destDir, directory.Name);
                GetFilesAndDirs(path + "\\", newDir + "\\");
            }
        }
        /// <summary>
        /// 删除机种
        /// </summary>
        /// <param name="recipeID">机种名</param>
        public void DeleteRecipe(string recipeID)
        {
            try
            {
                //加载机种信息
                recipeManager.LoadRecipeList();
                //根据机种名称，获取机种
                Recipe rep = recipeManager.GetRecipeByName(recipeID);
                //如果机种正在使用，则不能删除
                if (rep != null)
                {
                    if (recipeManager.IsRecipeSelected)
                    {
                        if (rep.RecipeID == recipeManager.CurRecipe.RecipeID)
                        {
                            throw new Exception("Recipe is in use!");
                        }
                    }
                }
                //删除机种文件夹
                if (Directory.Exists(rep.Location))
                    Directory.Delete(rep.Location, true);
                //加载机种信息
                recipeManager.LoadRecipeList();
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// 设置语言
        /// </summary>
        /// <param name="language"></param>
        public void SetLanguage(string language)
        {
            LanguageHelper.SetLanguage(language);
        }
            
        /// <summary>
        /// 创建对位工作流程
        /// </summary>
        /// <param name="para">对位流程参数</param>
        /// <returns>对位结果</returns>
        public async Task<WorkFlowResult> StartAutoPosAsync(WorkFlowPara para)
        {
            return await Task.Factory.StartNew(() =>
            {
                WorkFlow wf = new WorkFlow();
                wf.CurRecipe = CurRecipe;
                wf.ImageReceived += ImageReceived;
                wf.MessageShown += MessageShown;
                return wf.AutoPosDoWork(para);
            });
        }

        /// <summary>
        /// 创建对位工作流程
        /// </summary>
        /// <param name="para">对位流程参数</param>
        /// <returns>对位结果</returns>
        public WorkFlowResult StartManualPosAsync(WorkFlowPara para)
        {
            WorkFlow wf = new WorkFlow();
            wf.CurRecipe = CurRecipe;
            wf.ImageReceived += ImageReceived;
            wf.MessageShown += MessageShown;
            return wf.ManualPosDoWork(para);

        }

        /// <summary>
        /// 创建标定工作流程
        /// </summary>
        /// <param name="para">标定流程参数</param>
        /// <returns>标定结果</returns>
        public async Task<WorkFlowResult> StartCalibrationAsync(WorkFlowPara para)
        {
            return await Task.Factory.StartNew(() =>
            {
                WorkFlow wf = new WorkFlow();
                wf.CurRecipe = CurRecipe;
                wf.ImageReceived += ImageReceived;
                wf.MessageShown += MessageShown;
                return wf.CalibrationDoWork(para);
            });
        }

        private void MessageShown(object sender, ShowMessageArgs e)
        {
            LogManager.GetLogger("AutoPosManager").Info(e.Text);
        }

        /// <summary>
        /// 接收到图像数据
        /// </summary>
        public event EventHandler<ImageReceivedArgs> ImageReceived;
        /// <summary>
        /// 显示消息
        /// </summary>
       // public event EventHandler<ShowMessageArgs> MessageShown;
    }
}
