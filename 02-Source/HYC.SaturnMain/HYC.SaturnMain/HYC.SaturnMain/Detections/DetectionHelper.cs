/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2020 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnMain.Params
// 文件名称    ：DetectionHelper.cs
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
using HYC.SaturnDB.Model;
using HYC.HTLog;
using HYC.SaturnMain;
using HYC.SaturnMain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYC.SaturnMain.Detections
{
    class DetectionHelper
    {
     
        /// <summary>
        /// 根据列号查询单列
        /// </summary>
        /// <param name="columnNum"></param>
        /// <returns></returns>
        public static OneColumnModel GetOneColumnModel(int glassNum, int columnNum)
        {
            try
            {
                var info = Global.GlassDic.Values.Where(p => p.GlassNum== glassNum).ToArray();
                if(info.Length>0)
                {
                    var info2 = info[0].ColumnList.Where(p => p.ColumnNum == columnNum).ToArray();
                    if(info2.Length>0)
                    {
                        return info2[0];
                    }
                  
                }
                return null;
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DetectionHelper").Fatal($"GetOneColumnModel方法执行异常，异常信息：{ex.ToString()}");
                return null;
            }
        }
        /// <summary>
        /// 根据列号删除单列
        /// </summary>
        /// <param name="columnNum"></param>
        /// <returns></returns>
        public static void DeleteOneColumnModel(int glassNum, int columnNum)
        {
            try
            {
                var info = Global.GlassDic.Values.Where(p => p.GlassNum == glassNum).FirstOrDefault();
                if(info!=null)
                {
                    var info2 = info.ColumnList.Where(p => p.ColumnNum == columnNum).FirstOrDefault();
                    if (info2!=null)
                    {
                        info.ColumnList.Remove(info2);
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DetectionHelper").Fatal($"DeleteOneColumnModel方法执行异常，异常信息：{ex.ToString()}");
            }
        }
        /// <summary>
        /// 根据行号列号查询单个panel
        /// </summary>
        /// <param name="rowNum"></param>
        /// <returns></returns>
        public static OpenShortDetectModel GetOnePanelModel(int glassNum, int columnNum, int rowNum)
        {
            try
            {
                var info = Global.GlassDic.Values.Where(p => p.GlassNum == glassNum).ToArray();
                if(info.Length>0)
                {
                    var info2 = info[0].ColumnList.Where(p => p.ColumnNum == columnNum).ToArray();
                    if (info2.Length>0)
                    {
                        var info3 = info2[0].PanelList.Where(p => p.RowNum == rowNum).ToArray();
                        if (info3.Length>0)
                        {
                            return info3[0];
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DetectionHelper").Fatal($"GetOnePanelModel方法执行异常，异常信息：{ex.ToString()}");
                return null;
            }
        }
        public static OpenShortDetectModel GetOnePanelModel(OneColumnModel oneColumn, int rowNum)
        {
            try
            {
                var info = oneColumn.PanelList.Where(p => p.RowNum == rowNum).ToArray();
                if (info.Length > 0)
                {
                    return info[0];
                }
                return null;
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DetectionHelper").Fatal($"GetOnePanelModel2方法执行异常，异常信息：{ex.ToString()}");
                return null;
            }
        }

        public static bool GetGlassResult(int glassNum )
        {
            return Global.GlassDic[glassNum].ColumnList.Where(p => p.Result == "NG").ToList().Count > 0;
        }

    }
}
