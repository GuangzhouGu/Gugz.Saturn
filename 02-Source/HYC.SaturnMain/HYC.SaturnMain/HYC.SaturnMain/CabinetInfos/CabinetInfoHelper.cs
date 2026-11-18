/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2020 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnMain.CabinetInfos
// 文件名称    ：CabinetInfoHelper.cs
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
using HYC.HTFile.HTXML;
using HYC.HTLog;
using HYC.HTPCBase.Extension.Recipe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYC.SaturnMain.CabinetInfos
{
    public class CabinetInfoHelper
    {
        private static string FileName = @"Config\CabinetInfo.xml";
        public static List<CabinetRowInfo> CabinetInfoList { get; set; }
        public static bool SaveCabinetInfo()
        {
            try
            {
                bool ret = HTFile.HTXmlHelper.EntityToXMLFile(CabinetInfoList, FileName);
                return ret;
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("CabinetInfoHelper").Fatal($"SaveCabinetInfo方法执行异常，异常信息：{ex.ToString()}");
                return false;
            }
        }
        public static bool ReadCabinetInfo()
        {
            try
            {
                CabinetInfoList = HTFile.HTXmlHelper.EntityFromXMLFile<List<CabinetRowInfo>>(FileName);
                return CabinetInfoList != null;
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("CabinetInfoHelper").Fatal($"ReadCabinetInfo方法执行异常，异常信息：{ex.ToString()}");
                return false;
            }
        }
  
        public static CabinetRowInfo GetCabinetInfo(int rowNum)
        {
            try
            {
                return CabinetInfoList.Where(p => p.CabinetRowNum == rowNum).FirstOrDefault();
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("CabinetInfoHelper").Fatal($"GetCabinetInfo方法执行异常，异常信息：{ex.ToString()}");
                return null;
            }
        }
        public static CabinetRowInfo GetCabinetInfoByRFID(string rfid)
        {
            try
            {
                return CabinetInfoList.Where(p => p.RFID == rfid).FirstOrDefault();
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("CabinetInfoHelper").Fatal($"GetCabinetInfo方法执行异常，异常信息：{ex.ToString()}");
                return null;
            }
        }
   
   
        /// <summary>
        /// 判断当前料仓层号是否被使用
        /// </summary>
        /// <param name="rowNum"></param>
        /// <returns></returns>
        public static bool CheckCabinetIsUsed(int rowNum)
        {
            if (CabinetInfoList != null)
            {
                bool ret = CabinetInfoList.Where(p => p.CabinetRowNum == rowNum).FirstOrDefault().IsUsed;
                return ret;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 判断当前Probe名称是否被使用
        /// </summary>
        /// <param name="probeName"></param>
        /// <returns></returns>
        public static bool CheckProbeNameIsUsed(string probeName)
        {
            if (CabinetInfoList != null)
            {
                var count = CabinetInfoList.Where(p => p.ProbeName == probeName).Count();
                return count > 0;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 判断当前机种名称是否被使用
        /// </summary>
        /// <param name="probeName"></param>
        /// <returns></returns>
        public static bool CheckRecipeNameIsUsed(string recipeName)
        {
            if (CabinetInfoList != null)
            {
                var count = CabinetInfoList.Where(p => p.RecipeName == recipeName).Count();
                return count > 0;
            }
            else
            {
                return false;
            }
        }
        public static int GetRowNum(string recipeName)
        {
            try
            {
                return CabinetInfoList.Where(p => p.RecipeName == recipeName).FirstOrDefault().CabinetRowNum;
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("CabinetInfoHelper").Fatal($"GetRowNum方法执行异常，异常信息：{ex.ToString()}");
                return -1;
            }
        }
    }
}
