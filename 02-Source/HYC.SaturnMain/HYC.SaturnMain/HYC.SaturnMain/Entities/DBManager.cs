/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2022 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnMain.Entities
// 文件名称    ：DBManager
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

using HYC.SaturnDB.DAL;
using HYC.SaturnDB.Model;
using HYC.HTLog;
using System;
using System.Threading.Tasks;
using HYC.HTPCBase.Extension.Recipe;
using HYC.SaturnDB;
using HYC.HTPCBase.Extension.Entities;
using System.Linq;

namespace HYC.SaturnMain.Entities
{
    public class DBManager
    {
        /// <summary>
        /// 插入单片Glass检测信息
        /// </summary>
        /// <param name="glassID"></param>
        /// <param name="glassNum"></param>
        public static void InsertTotalInfo(string glassID,int glassNum)
        {
            try
            {
                var cellStartModel = new CellStartTimeDal().GetByID(glassID);
                new TotalInfoDal().Insert(new TotalInfoModel { GlassID = glassID, EQPInTime = cellStartModel.StartTime, GlassNum = glassNum, RecipeID = RecipeManager.CurRecipe.RecipeID });
                Global.CurDetectInfo.GlassID = glassID;
                Global.CurDetectInfo.EQPInTime = cellStartModel.StartTime;
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DBManager").Fatal(ex.ToString());
            }
        }
        /// <summary>
        /// 更新单片Glass的检测时间
        /// </summary>
        /// <param name="info"></param>
        public static void UpdateTotalInfoTime(CurrentDetectInfo info)
        {
            try
            {
                var totalInfo = GetTotalInfo(info.GlassID);
                if (totalInfo!=null)
                {
                    if (totalInfo.StartTime == null && info.StartTime != null)
                    {
                        totalInfo.StartTime = info.StartTime;
                    }
                    if (totalInfo.EndTime == null && info.EndTime != null)
                    {
                        totalInfo.EndTime = info.EndTime;
                    }
                    new TotalInfoDal().Update(totalInfo);
                }
              
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DBManager").Fatal(ex.ToString());
            }
        }
        /// <summary>
        /// 更新单片Glass的检测结果
        /// </summary>
        /// <param name="info"></param>
        public static void UpdateTotalInfoResult(CurrentDetectInfo info)
        {
            try
            {
                var cellList=new OneCellDetectInfoDal().GetByIDAndTime(info.GlassID, info.EQPInTime);
                if (cellList.Count>0)
                {
                    var totalInfo = GetTotalInfo(info.GlassID);
                    totalInfo.OpenShortResult = cellList.Where(p => p.OpenShortResult != "OK").Count() > 0 ? "NG" : "OK";
                    totalInfo.AoiResult = cellList.Where(p => p.AoiResult != "OK").Count() > 0 ? "NG" : "OK";
                    totalInfo.MuraResult = cellList.Where(p => p.MuraResult != "OK").Count() > 0 ? "NG" : "OK";
                    totalInfo.MultiAngleResult = cellList.Where(p => p.MultiAngleResult != "OK").Count() > 0 ? "NG" : "OK";
                    if (totalInfo.OpenShortResult=="OK"&& totalInfo.AoiResult == "OK" && totalInfo.MuraResult == "OK" && totalInfo.MultiAngleResult == "OK" )
                    {
                        totalInfo.Result = "OK";
                        totalInfo.JobJudge = "1";
                        totalInfo.JobGrade = "0";
                    }
                    else
                    {
                        totalInfo.Result = "NG";
                        totalInfo.JobJudge = "3";
                        totalInfo.JobGrade = "0";
                    }
                    new TotalInfoDal().Update(totalInfo);
                }
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DBManager").Fatal(ex.ToString());
            }
        }
        /// <summary>
        /// 插入单个Cell检测信息
        /// </summary>
        /// <param name="info"></param>
        /// <param name="colNum"></param>
        /// <param name="cellNum"></param>
        public static void InsertOneCellInfo(CurrentDetectInfo info, int colNum, int cellNum)
        {
            try
            {
                var cellStartDal = new CellStartTimeDal();
                var cellStartModel = cellStartDal.GetByID(info.GlassID);
                var model = new OneCellDetectModel();
                model.GlassID = info.GlassID;
                model.GlassNum = info.GlassNum;
                model.ColumnNum = colNum;
                model.CellNum = cellNum;
                model.EQPInTime = cellStartModel.StartTime;
                model.StartTime = DateTime.Now;
                model.CellID = Global.GetCellID(colNum, cellNum);
                new OneCellDetectInfoDal().Insert(model);
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DBManager").Fatal(ex.ToString());
            }
        }
    
        /// <summary>
        /// 更新单个Cell检测结果
        /// </summary>
        /// <param name="model"></param>
        /// <param name="detectType"></param>
        public static void UpdateOneCellResult(DetectResultArgs args)
        {
            try
            {
                var cellStartDal = new CellStartTimeDal();
                var oneInfoDal = new OneCellDetectInfoDal();
                var cellStartModel = cellStartDal.GetByID(args.DetectReqArgs.GlassID);
                if (cellStartModel == null)
                {
                    LogManager.GetLogger("DBManager").Info("UpdateOneCellResult执行失败，CellStartTimeDal.GetByID方法返回值为空");
                    return;
                }
                var oneCellInfo = oneInfoDal.GetByIDAndTime(args.DetectReqArgs.GlassID, args.DetectReqArgs.CellID, cellStartModel.StartTime);
                if (oneCellInfo == null)
                {
                    LogManager.GetLogger("DBManager").Info("UpdateOneCellResult执行失败，OneCellDetectInfoDal.GetByIDAndTime方法返回值为空");
                    return;
                }
                oneCellInfo.EndTime = DateTime.Now;
                DetectType detectType = EnumHelper.GetEnumValue<DetectType>(args.DetectReqArgs.DetectType);
                switch (detectType)
                {
                    case DetectType.Aoi:
                        {
                            oneCellInfo.AoiResult = args.Result;
                            break;
                        }
                    case DetectType.Mura:
                        {
                            oneCellInfo.MuraResult = args.Result;
                            break;
                        }
                    case DetectType.MultiAngle:
                        {
                            oneCellInfo.MultiAngleResult = args.Result;
                            break;
                        }
                    case DetectType.OpenShort:
                        {
                            oneCellInfo.OpenShortResult = args.Result;
                            break;
                        }
                    default:
                        break;
                }

                if (oneCellInfo.AoiResult != "NG" && oneCellInfo.MuraResult != "NG" && oneCellInfo.MultiAngleResult != "NG" && oneCellInfo.OpenShortResult != "NG")
                {
                    oneCellInfo.Result = "OK";
                    oneCellInfo.JobJudge = "1";
                    oneCellInfo.JobGrade = "0";
                }
                else
                {
                    oneCellInfo.Result = "NG";
                    oneCellInfo.JobJudge = "3";
                    oneCellInfo.JobGrade = "0";
                }
                oneInfoDal.Update(oneCellInfo);
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DBManager").Info($"UpdateOneCellResult执行异常，异常信息：{ex}");
            }
        }
 
        public static void DeleteOldDatas(int days)
        {
            if (days == 0)
            {
                days = 90;
            }
            Task.Factory.StartNew(() =>
            {
                try
                {
                    new CellStartTimeDal().Delete(days);
                    new CimMessageDal().Delete(days);
                    new JobDataDal().Delete(days);
                    new OpenShortNGInfoDal().Delete(days);
                    new OpenShortInfoDal().Delete(days);
                    new AoiDetectInfoDal().Delete(days);
                    new AoiNGInfoDal().Delete(days);
                    new TotalInfoDal().Delete(days);
                    new MultiAngleDetectInfoDal().Delete(days);
                    new OneCellDetectInfoDal().Delete(days);
                    new DfsUploadInfoDal().Delete(days);
                }
                catch (Exception ex)
                {
                    LogManager.GetLogger("DBManager").Fatal(ex.ToString());
                }
            });
        }
        public static TotalInfoModel GetTotalInfo(string glassID)
        {
            var totalInfoDal = new TotalInfoDal();
            var cellStartModel = new CellStartTimeDal().GetByID(glassID);
            if (cellStartModel!=null)
            {
                var totalInfo = totalInfoDal.GetByIDAndTime(glassID, cellStartModel.StartTime);
                return totalInfo;
            }
            else
            {
                return null;
            }
         
        }
    }
}
