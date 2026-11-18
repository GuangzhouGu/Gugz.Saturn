/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2023 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnAoi.DetectFlow
// 文件名称    ：MultiAngleDetectFlow
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
using HYC.SaturnAoi.Devices.MultiAngle;
using HYC.SaturnAoi.Entities;
using HYC.SaturnAoi.Params;
using HYC.SaturnDB;
using HYC.SaturnDB.DAL;
using HYC.SaturnDB.Model;
using HYC.SaturnAoi.Devices.ClientPC;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Threading;
using HYC.SaturnAoi.DetectFlow.MultiAngleDetect;

namespace HYC.SaturnAoi.DetectFlow
{
    public class MultiAngleDetectReqHandle
    {
        public static MultiAngleDetectReqHandle Instance = null;
        public static event Action<string, string, Brush> OnShowMsg;
        DetectReqArgs DetectReqArgs=null;
     
        static MultiAngleDetectReqHandle()
        {
            Instance = new MultiAngleDetectReqHandle();
        }
        public  async void MultiAngleDetectReq(DetectReqArgs args)
        {
            DetectReqArgs = args;
            await Task.Factory.StartNew(()=> {
                if (Param.System.IsShieldMultiAngleDetect)
                {
                    OnShowMsg?.Invoke("MultiAngleDetectFlow", $"{args.DetectType}检测屏蔽", Brushes.Green);
                    DetectResultReply(null);
                }
                else
                {
                    MultiAngleDetectFlow flow = new MultiAngleDetectFlow();
                    flow.OnDetectComplete -= DetectResultReply;
                    flow.OnShowMsg -= OnShowMsg;
                    flow.OnDetectComplete += DetectResultReply;
                    flow.OnShowMsg += OnShowMsg;
                    DBManager.Insert(args);
                    OnShowMsg?.Invoke("MultiAngleDetectFlow", $"第{args.ColumnNum + 1}列{args.CellID} {args.DetectType}检测开始", Brushes.Green);
                    flow.MultiAngleThread(args);
                }
            });
        }

        public void DetectResultReply(MultiAngleCompletedArgs result)
        {
            DetectResultArgs args = new DetectResultArgs();
            args.DetectReqArgs = DetectReqArgs;
            if (result==null)
            {
                args.Result = "OK";
            }
            else
            {
                DBManager.UpdateMultiAngleResult(result);
                args.Result = result.Result;
            }
            args.StartTime = result.StartTime.ToString("yyyy-MM-dd HH:mm:ss"); ;
            args.EndTime = result.EndTime.ToString("yyyy-MM-dd HH:mm:ss"); ;
            ClientHelper.Instance.SendDetectResult(args);
        }
     
    }

}
