/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2023 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnAoi.Entities
// 文件名称    ：DetectResultManager
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
using HYC.HTLog;
using HYC.HTPCBase.Extension.Recipe;
using HYC.SaturnAoi.DetectFlow;
using HYC.SaturnAoi.DetectFlow.MultiAngleDetect;
using HYC.SaturnDB;
using HYC.SaturnDB.DAL;
using HYC.SaturnDB.Model;
using HYC.StandardAoi.Events;
using HYC.StandardAoi.Parameter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYC.SaturnAoi.Entities
{
    public class DBManager
    {
        public static void Insert(DetectReqArgs args)
        {
            var startModel = new CellStartTimeDal().GetByID(args.GlassID);
            if (startModel==null)
            {
                LogManager.GetLogger("DBManager").Info($"Insert方法执行失败，CellStartTimeDal查询GlassID:{args.GlassID}失败");
                return;
            }
            switch (args.DetectType)
            {
                case "Aoi":
                case "Mura":
                case "SquintMura":
                    {
                        new AoiDetectInfoDal().Insert(new AoiDetectModel { GlassID = args.GlassID, CellID = args.CellID, EQPInTime = startModel.StartTime, ColumnNum = args.ColumnNum, GlassNum = args.GlassNum, DetectType = args.DetectType , RecipeID =RecipeManager.CurRecipe.RecipeID});
                        break;
                    }
                case "MultiAngle":
                    {
                        new MultiAngleDetectInfoDal().Insert(new MultiAngleDetectModel { GlassID = args.GlassID, CellID = args.CellID, EQPInTime = startModel.StartTime, ColumnNum = args.ColumnNum, GlassNum = args.GlassNum,RecipeID=RecipeManager.CurRecipe.RecipeID});
                        break;
                    }
                default:
                    break;
            }
        }
        public static void UpdateAoiResult(DetectionCompletedArgs args)
        {
            try
            {
                var result = args.Result;
                var strIDs = result.Detail.PanelID.Split('_');
                var glassID = strIDs[0];
                var cellID = strIDs[1];
                // 获取产品进入设备的时间
                var cellStartTime = new CellStartTimeDal().GetByID(glassID);
                if (cellStartTime == null)
                {
                    LogManager.GetLogger("DBManager").Info($"UpdateAoiResult方法执行失败，CellStartTimeDal查询GlassID:{glassID}失败");
                    return;
                }
                List<string> lst = new List<string>();
                List<ResultRow> lisError = result.Summary.Results;
                AoiDetectModel cellInfo = null;
                var dal = new AoiDetectInfoDal();
                try
                {
                    if (args.Channel==1)
                    {
                        cellInfo = dal.GetByIDAndTime(cellID, "SquintMura", cellStartTime.StartTime);//斜视Mura通道号使用1
                    }
                    else
                    {
                        cellInfo = dal.GetByIDAndTime(cellID, args.DetectionType.ToString(), cellStartTime.StartTime);
                    }
                }
                catch (Exception ex)
                {
                    LogManager.GetLogger("DetectResultManager").Info($"SaveResult执行异常，异常信息：{ex}");
                }
                if (cellInfo!=null)
                {
                    string Ngtext = string.Empty;
                    cellInfo.RecipeID = result.Detail.RecipeID;
                    cellInfo.StartTime = result.Detail.StartTime;
                    cellInfo.EndTime = result.Detail.EndTime;
                    cellInfo.ProcessingTime = result.Detail.ProcessingTime.ToString();
                    cellInfo.Result = result.Detail.TestResult == ResultType.OK ? "OK" : "NG";
                    if (result.Detail.ImgROI != null)
                    {
                        cellInfo.ImgROI = result.Detail.ImgROI.ToString();
                    }
                    cellInfo.BmpSavePath = result.Detail.ImgPath;
                    int defectNum = 0;
                    if (result.Detail.TestResult != ResultType.OK)
                    {
                        // 保存详细结果模型到数据库
                        foreach (var pattern in result.Detail.ListPattern)
                        {
                            foreach (var reason in pattern.ListReason)
                            {
                                Ngtext += $"{reason.DefectName}({pattern.PatternName}),";
                                AoiNGInfoModel defectInfo = new AoiNGInfoModel();
                                defectInfo.GlassID = cellInfo.GlassID;
                                defectInfo.GlassNum = cellInfo.GlassNum;
                                defectInfo.ColumnNum = cellInfo.ColumnNum;
                                defectInfo.PatternName = pattern.PatternName;
                                defectInfo.DefectNum = defectNum;
                                defectInfo.CellID = cellID;
                                defectInfo.DetectType = cellInfo.DetectType;
                                defectInfo.EQPInTime = cellStartTime.StartTime;
                                defectInfo.ErrorCode = reason.DefectCode.ToString();
                                defectInfo.ErrorCount = reason.DefectCount;
                                defectInfo.DefectName = reason.DefectName;
                                defectInfo.DefectType = reason.DefectType;
                                defectInfo.DefectParams = string.Join("-", reason.DefectParams.Select(p => p.ToString()));
                                defectInfo.DefectParaIndex = reason.DefectParaIndex;
                                defectInfo.DefectAreas = string.Join("-", reason.DefectAreas.Select(p => p.ToString()));
                                defectNum++;
                                new AoiNGInfoDal().Insert(defectInfo);
                            }
                        }
                    }
                    cellInfo.ErrorText = Ngtext;
                    dal.Update(cellInfo);
                }
                else
                {
                    LogManager.GetLogger("DBManager").Info("AoiDetectInfoDal类GetByIDAndTime返回值为空");
                }
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DBManager").Fatal(ex.ToString());
            }
        }
        public static void UpdateMultiAngleResult(MultiAngleCompletedArgs args)
        {
            try
            {
                var cellStartTime = new CellStartTimeDal().GetByID(args.GlassID);
                var dal = new MultiAngleDetectInfoDal();
                var cellModel = dal.GetByIDAndTime(args.GlassID, args.CellID, cellStartTime.StartTime);
                if (cellModel != null)
                {
                    cellModel.StartTime = args.StartTime;
                    cellModel.EndTime = args.EndTime;
                    cellModel.Result = args.Result;
                    cellModel.ProcessingTime = args.ProcessingTime;
                    dal.Update(cellModel);
                }
                else
                {
                    LogManager.GetLogger("DBManager").Info("MultiAngleDetectInfoDal类GetByIDAndTime返回值为空");
                }
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DBManager").Fatal(ex.ToString());
            }
        }
        public static void UpdateOneCellInfo(DetectResultArgs args)
        {
            try
            {
                var cellStartDal = new CellStartTimeDal();
                var oneInfoDal = new OneCellDetectInfoDal();
                var cellStartModel = cellStartDal.GetByID(args.DetectReqArgs.GlassID);
                var oneCellInfo = oneInfoDal.GetByIDAndTime(args.DetectReqArgs.GlassID, args.DetectReqArgs.CellID, cellStartModel.StartTime);
                if (oneCellInfo.StartTime == null && args.StartTime != null)
                {
                    oneCellInfo.StartTime = DateTime.Parse(args.StartTime);
                }
                if (oneCellInfo.EndTime == null && args.EndTime != null)
                {
                    oneCellInfo.EndTime = DateTime.Parse(args.EndTime);
                }
                switch (args.DetectReqArgs.DetectType)
                {
                    case "Aoi":
                        {
                            if (string.IsNullOrEmpty(oneCellInfo.AoiResult))
                            {
                                oneCellInfo.AoiResult = args.Result;
                            }
                            break;
                        }
                    case "Mura":
                        {
                            if (string.IsNullOrEmpty(oneCellInfo.MuraResult))
                            {
                                oneCellInfo.MuraResult = args.Result;
                            }
                            break;
                        }
                    case "SquintMura":
                        {
                            if (string.IsNullOrEmpty(oneCellInfo.SquintMuraResult))
                            {
                                oneCellInfo.SquintMuraResult = args.Result;
                            }
                            break;
                        }
                    case "MultiAngle":
                        {
                            if (string.IsNullOrEmpty(oneCellInfo.MultiAngleResult))
                            {
                                oneCellInfo.MultiAngleResult = args.Result;
                            }
                            break;
                        }
                    default:
                        break;
                }
                if (oneCellInfo.AoiResult != "NG" && oneCellInfo.MuraResult != "NG" && oneCellInfo.SquintMuraResult != "NG" && oneCellInfo.MultiAngleResult != "NG")
                {
                    oneCellInfo.Result = "OK";
                }
                else
                {
                    oneCellInfo.Result = "NG";
                }
                oneInfoDal.Update(oneCellInfo);
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DBManager").Fatal(ex.ToString());
            }
        }
    }
}
