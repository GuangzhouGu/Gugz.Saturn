/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2023 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnAoi.Entities
// 文件名称    ：InitManager
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
using CommonLib.Device;
using CommonLib.Entities;
using HYC.HTFile;
using HYC.HTFile.HTINI;
using HYC.HTPCBase.Extension.Recipe;
using HYC.SaturnDB;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYC.SaturnAoi.Entities
{
    public class InitManager
    {
        internal event Action<string, string, Brush> OnShowMsg;//消息提示
        internal event Action<string> OnUpdateRecipe;//更新机种
        internal event Action<string, int, bool> OnConnected;//连接状态
        public event Func<bool> OnInitPara;//不继承IPara初始化参数
        public event Func<bool> OnInitDevice;//不继承IDevice初始化硬件
        HTIniHelper Ini = new HTIniHelper(AppDomain.CurrentDomain.BaseDirectory + "Config.INI");//读取配置文件
        /// <summary>
        /// 初始化所用硬件
        /// </summary>
        /// <returns></returns>
        public void Init()
        {
            try
            {
                OnShowMsg?.Invoke("初始化", "初始化开始", Brushes.Blue);
                LoadPara();
                OnShowMsg?.Invoke("初始化", "初始化硬件", Brushes.Green);
                DeviceManager.Instance.OnConnected += OnConnected;
                DeviceManager.Instance.OnShowMsg += OnShowMsg;
                DeviceManager.Instance.Init();
                OnInitDevice?.Invoke();
                OnShowMsg?.Invoke("初始化", "初始化完成", Brushes.Blue);
            }
            catch (Exception ex)
            {
                OnShowMsg?.Invoke("初始化", $"InitManager->Init执行异常，异常信息：{ex}", Brushes.Red);
            }
        }
        /// <summary>
        /// 加载所有参数设置参数
        /// </summary>
        private void LoadPara()
        {
            try
            {
                ParaManager.OnShowMsg += OnShowMsg;
                OnShowMsg?.Invoke("初始化", "初始化配方参数", Brushes.Green);
                string recipeName = Ini.Read("Recipe", "RecipeID", "");
                string connectStr = Ini.Read("DB", "ConnectionString", "");
                DBHelper.Init(connectStr);
                RecipeManager.LoadRecipeList();
                Recipe recipe = RecipeManager.GetRecipeByName(recipeName);
                RecipeManager.CurRecipe = recipe;
                OnUpdateRecipe?.Invoke(recipe.RecipeID);
                OnConnected?.Invoke("Paramaters", 0, recipe.InitOK);
                OnInitPara?.Invoke();
                if (!recipe.InitOK)
                {
                    OnShowMsg?.Invoke("初始化", "配方参数加载失败", Brushes.Red);
                }
            }
            catch (Exception ex)
            {
                OnShowMsg?.Invoke("初始化", $"InitManager->LoadPara执行异常，异常信息：{ex}", Brushes.Red);
            }
        }
        /// <summary>
        /// 机种切换更换参数
        /// </summary>
        /// <param name="recipeName"></param>
        /// <returns></returns>
        public bool RecipeSelect(string recipeName)
        {
            Recipe recipe = RecipeManager.GetRecipeByName(recipeName);
            RecipeManager.CurRecipe = recipe;
            OnUpdateRecipe?.Invoke(recipe.RecipeID);
            Ini.Write("Recipe", "RecipeID", RecipeManager.CurRecipe.RecipeID);
            return recipe != null && recipe.InitOK;
        }
        /// <summary>
        /// 释放所有硬件连接
        /// </summary>
        public void Uninit()
        {
            try
            {
                Ini.Write("Recipe", "RecipeID", RecipeManager.CurRecipe.RecipeID);
                DeviceManager.Instance.Uninit();
            }
            catch (Exception ex)
            {
                OnShowMsg?.Invoke("初始化", $"InitManager->Uninit执行异常，异常信息：{ex}", Brushes.Red);
            }
        }
    }
}
