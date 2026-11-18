/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2020 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnMain
// 文件名称    ：ReviewPC.cs
// =================================
// 创 建 者    ：guguangzhou
// 创建日期    ：2021/01/29 09:00:00 
// 功能描述    ：ReviewPC相关指令
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
using HYC.HTPCBase.Extension.Device;
using HYC.HTLog;
using HYC.SaturnMain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HYC.SaturnMain.Devices.MasterPC;

using HYC.HTPCBase.Extension.Entities;
using HYC.SaturnAoi.Entities;
using HYC.SaturnDB;
using HYC.SaturnMain.Params;
using System.Threading;
using HYC.HTPCBase.Extension.Recipe;

namespace HYC.SaturnMain.Devices.ClientPC
{
    public class AoiPC 
    {
        public static AoiPC Instance = null;
        public static DetectResultArgs PC1DetectResultArgs=new DetectResultArgs();
        public static DetectResultArgs PC2DetectResultArgs = new DetectResultArgs();
        public static string PC1PowerControlResult = string.Empty;
        public static string PC2PowerControlResult = string.Empty;
        public static string PC1RecipeSyncResult = string.Empty;
        public static string PC2RecipeSyncResult = string.Empty;
        static AoiPC()
        {
            Instance = new AoiPC();
        }
        public  void TimeSync(int pcIndex)
        {
            try
            {
                MessageInfo info = new MessageInfo();
                info.Type = CommunicationType.Send;
                info.PCName = $"{(ClientPCName)pcIndex}";
                info.Command = Command.TimeSync;
                info.Message = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
               MasterPCHelper.Instance.SendMessage(info);
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("AoiPC").Fatal(ex.ToString());
            }
        }
        public  void RecipeSync(string recipe,int pcIndex, RecipeChangeType recipeOPType)
        {
            try
            {
                if (pcIndex == 0)
                {
                    PC1RecipeSyncResult = string.Empty;
                    if (Param.Comm.AoiPC1Shield)
                    {
                        PC1RecipeSyncResult = "OK";
                    }
                }
                else
                {
                    PC2RecipeSyncResult = string.Empty;
                    if (Param.Comm.AoiPC2Shield)
                    {
                        PC2RecipeSyncResult = "OK";
                    }
                }
                MessageInfo info = new MessageInfo();
                info.Type = CommunicationType.Send;
                info.PCName = $"{(ClientPCName)pcIndex}";
                info.Command = Command.RecipeSync;
                info.Message = $"{recipe},{recipeOPType}"; ;
               MasterPCHelper.Instance.SendMessage(info);
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("AoiPC").Fatal(ex.ToString());
            }
        }
        public  void UserSync(string user, int pcIndex)
        {
            try
            {
                MessageInfo info = new MessageInfo();
                info.Type = CommunicationType.Send;
                info.PCName = $"{(ClientPCName)pcIndex}";
                info.Command = Command.UserSync;
                info.Message = user;
               MasterPCHelper.Instance.SendMessage(info);
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("AoiPC").Fatal(ex.ToString());
            }
        }
        public void DetectReq(DetectReqArgs args)
        {
            try
            {
                MessageInfo info = new MessageInfo();
                info.Type = CommunicationType.Send;
                info.PCName = $"{(ClientPCName)args.PCIndex}";
                info.Command = Command.DetectReq;
                info.Datas = ClassHelper.ObjectToBytes(args);
                MasterPCHelper.Instance.SendMessage(info);
                if (args.PCIndex== 0)
                {
                    PC1DetectResultArgs = new DetectResultArgs();
                    if (Param.Comm.AoiPC1Shield)
                    {
                        Thread.Sleep(2000);
                        PC1DetectResultArgs.DetectReqArgs = args;
                        PC1DetectResultArgs.Result = "NG";
                    }

                }
                else
                {
                    PC2DetectResultArgs = new DetectResultArgs();
                    if (Param.Comm.AoiPC2Shield)
                    {
                        Thread.Sleep(2000);
                        PC2DetectResultArgs.DetectReqArgs = args;
                        PC2DetectResultArgs.Result = "NG";
                    }

                }
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("AoiPC").Fatal(ex.ToString());
            }
        }
        public void PowerControlReq(bool isPowerOn,int pcIndex)
        {
            try
            {
                if (pcIndex == 0)
                {
                    PC1PowerControlResult = string.Empty;
                    if (Param.Comm.AoiPC1Shield)
                    {
                        PC1PowerControlResult = "OK";
                    }
                }
                else
                {
                    PC2PowerControlResult = string.Empty;
                    if (Param.Comm.AoiPC2Shield)
                    {
                        PC2PowerControlResult = "OK";
                    }
                }
                MessageInfo info = new MessageInfo();
                info.Type = CommunicationType.Send;
                info.PCName = $"{(ClientPCName)pcIndex}";
                info.Command = Command.PowerControlReq;
                info.Message = $"{pcIndex},{(isPowerOn?"PowerOn":"PowerOff")}";
                MasterPCHelper.Instance.SendMessage(info);
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("AoiPC").Fatal(ex.ToString());
            }
        }
        public void SwitchPatternReq(int pcIndex,int ptnNum)
        {
            try
            {
                MessageInfo info = new MessageInfo();
                info.Type = CommunicationType.Send;
                info.PCName = $"{(ClientPCName)pcIndex}";
                info.Command = Command.SwitchPatternReq;
                info.Message = $"{ptnNum}";
                MasterPCHelper.Instance.SendMessage(info);
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("AoiPC").Fatal(ex.ToString());
            }
        }
        public  void UpdateStepID(string stepID,int pcIndex)
        {
            MessageInfo info = new MessageInfo();
            info.Type = CommunicationType.Send;
            info.PCName = $"{(ClientPCName)pcIndex}";
            info.Command = Command.UpdateStepID;
            info.Message = stepID;
            MasterPCHelper.Instance.SendMessage(info);
        }
    }
    
}
