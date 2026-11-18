/*----------------------------------------------------------------
// Copyright (C) Suzhou HYC Electronic Technology Co.,LTD
// 版权所有。
//
// =================================
// CLR版本     ：4.0.30319.42000
// 命名空间    ：HYC.AutoPosition.Recipes
// 文件名称    ：Recipe.cs
// =================================
// 创 建 者    ：yangkang
// 创建日期    ：2019/12/30 16:12:22 
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
using HYC.AutoPosition.Parameter;
using System;

namespace HYC.AutoPosition.Recipes
{
    /// <summary>
    /// 机种类
    /// </summary>
    [Serializable]
    public class Recipe: RecipeBase
    {
   
        /// <summary>
        /// Recipe文件夹路径
        /// </summary>
        public string Location { get; set; } = string.Empty;


        /// <summary>
        /// 对位极限 参数
        /// </summary>
        public AlignParameter AlignParameter;
        /// <summary>
        /// 机种更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; } = DateTime.Now;

        #region DataPos
        /// <summary>
        /// 对位机种数据
        /// </summary>
        public RecipeDataPos[] DataPos = new RecipeDataPos[100];

        

        /// <summary>
        /// 读取对位机种数据
        /// </summary>
        /// <param name="index"></param>
        public void ReadDataPos(int index)
        {
            // 对位
            DataPos[index] = new RecipeDataPos();
            DataPos[index].InitData(index);
            DataPos[index].ReadData(Location);
            

        }

        /// <summary>
        /// 保存对位机种数据
        /// </summary>
        /// <param name="index"></param>
        public void SaveDataPos(int index)
        {
            if (DataPos[index] == null)
            {
                DataPos[index] = new RecipeDataPos();
                DataPos[index].InitData(index);
            }
            DataPos[index].SaveData(Location);
        }
        #endregion

        #region camera information
        /// <summary>
        /// 相机参数序列化
        /// </summary>
        public void AlignParameterSerialize()
        {
            AlignParameter.Serialize(this.Location + "\\AlignLimitParameter.xml");
        }
        #endregion      
    }
    [Serializable]
    public class RecipeBase
    {
        /// <summary>
        /// 机种名称
        /// </summary>
        public string RecipeID { get; set; } = string.Empty;
        /// <summary>
        /// PPID
        /// </summary>
        public string PPID { get; set; } = string.Empty;
        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; }
    }
    }
