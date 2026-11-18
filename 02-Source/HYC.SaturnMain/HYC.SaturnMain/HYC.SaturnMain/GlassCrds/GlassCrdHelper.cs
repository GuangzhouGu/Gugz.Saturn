/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2020 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnMain.GlassCrds
// 文件名称    ：GlassCrdHelper.cs
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
using HYC.HTLog;
using HYC.HTPCBase.Extension.Recipe;
using HYC.SaturnMain.Params;
using HYC.SaturnMain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYC.SaturnMain.GlassCrds
{
    class GlassCrdHelper
    {
        /// <summary>
        /// 根据列号查询单列
        /// </summary>
        /// <param name="columnNum"></param>
        /// <returns></returns>
        public static OneColumnCrd GetOneColumnCrd(int columnNum)
        {
            try
            {
                OneColumnCrd result = null;
                var info = Param.GlassCrdInfo.ColumnCrdList.Where(p => p.ColumnNum == columnNum).ToArray();
                if (info.Length > 0)
                {
                    result = info[0];
                }
                return result;
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("GlassCrdHelper").Info($"GetOneColumnCrd方法执行异常，异常信息：{ex}");
                return null;
            }
        }
        public static OneCellCrd GetOneCellCrd(OneColumnCrd oneColumn, int rowNum)
        {
            try
            {
                var info = oneColumn.CellCrdList.Where(p => p.RowNum == rowNum).ToArray();
                if (info.Length > 0)
                {
                    return info[0];
                }
                return null;
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("GlassCrdHelper").Info($"GetOneCellCrd方法执行异常，异常信息：{ex}");
                return null;
            }
        }
        public static OneCellCrd GetOneCellCrd(int columnNum, int rowNum)
        {
            try
            {
                OneCellCrd result = null;
                var info = Param.GlassCrdInfo.ColumnCrdList.Where(p => p.ColumnNum == columnNum).First();
                result = info.CellCrdList.Where(p => p.RowNum == rowNum).First();
                return result;
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("GlassCrdHelper").Info($"GetOneCellCrd方法执行异常，异常信息：{ex}");
                return null;
            }
        }
        public static double[] GetOneCellCrd2(int columnNum, int rowNum)
        {
            try
            {
                OneCellCrd result = null;
                var info = Param.GlassCrdInfo.ColumnCrdList.Where(p => p.ColumnNum == columnNum).First();
                result = info.CellCrdList.Where(p => p.RowNum == rowNum).First();
                return new double[] { result.X, result.Y };
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("GlassCrdHelper").Info($"GetOneCellCrd2方法执行异常，异常信息：{ex}");
                return null;
            }
        }
    }
  
}
