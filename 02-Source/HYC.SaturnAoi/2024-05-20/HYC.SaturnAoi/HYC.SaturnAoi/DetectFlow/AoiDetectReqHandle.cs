/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2023 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnAoi.DetectFlow
// 文件名称    ：AoiDetectFlow
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
using CommonLib.Entities;
using HYC.SaturnAoi.Entities;
using HYC.SaturnAoi.Params;
using HYC.SaturnDB;
using HYC.SaturnDB.DAL;
using HYC.StandardAoi.Events;
using HYC.StandardAoi.Parameter;
using HYC.SaturnAoi.Devices.ClientPC;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYC.SaturnAoi.DetectFlow
{
    class AoiDetectReqHandle
    {
        public static AoiDetectReqHandle Instance = null;
        public static event Action<string, string, Brush> OnShowMsg;
        DetectReqArgs DetectReqArgs = null;
        static AoiDetectReqHandle()
        {
            Instance = new AoiDetectReqHandle();
        }
        public void AoiDetectReq(DetectReqArgs args)
        {
            DetectReqArgs = args;
            if ((Param.System.IsShieldAoiDetect&& args.DetectType=="Aoi")|| (Param.System.IsShieldMuraDetect && args.DetectType == "Mura"))
            {
                OnShowMsg?.Invoke("AoiDetectFlow",$"{args.DetectType}检测屏蔽",Brushes.Green);
                DetectResultReply(null);
            }
            else
            {
                DBManager.Insert(args);
                OnShowMsg?.Invoke("AoiDetectFlow", $"第{args.ColumnNum+1}列{args.CellID} {args.DetectType}检测开始", Brushes.Green);
                var reqArgs=new  StandardAoi.Interface.RequestEventArgs();
                if (args.DetectType=="SquintMura")
                {
                    reqArgs.PanelID = $"{args.GlassID}_{args.CellID}";
                    reqArgs.DetectionType = DetectionType.Mura;
                    reqArgs.Channel = 1;
                }
                else
                {
                    reqArgs.PanelID = $"{args.GlassID}_{args.CellID}";
                    reqArgs.DetectionType = EnumHelper.GetEnumValue<DetectionType>(args.DetectType);
                    reqArgs.Channel = 0;
                }
     
                Global.SignalProvider.OnDetectionRequest(reqArgs);
               
            }
        }
        public void DetectResultReply(DetectionCompletedArgs args)
        {
            DetectResultArgs detectResultArgs = new DetectResultArgs();
            detectResultArgs.DetectReqArgs = DetectReqArgs;
            if (args!=null)
            {
                detectResultArgs.Result = args.Result.Detail.TestResult == ResultType.OK ? "OK" : "NG";
                detectResultArgs.StartTime = args.Result.Detail.StartTime.ToString("yyyy-MM-dd HH:mm:ss");
                detectResultArgs.EndTime = args.Result.Detail.EndTime.ToString("yyyy-MM-dd HH:mm:ss");
            }
            else
            {
                detectResultArgs.Result = "OK";
                detectResultArgs.StartTime =DateTime.Now.AddSeconds(-8).ToString("yyyy-MM-dd HH:mm:ss");
                detectResultArgs.EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
            ClientHelper.Instance.SendDetectResult(detectResultArgs);
            DBManager.UpdateOneCellInfo(detectResultArgs);
            DetectReqArgs = null;
            
        }
      
    }
}
