using CommonLib.Entities;
using HYC.HTLog;
using HYC.SaturnAoi.Devices.Client;
using HYC.SaturnAoi.Entities;
using HYC.SaturnAoi.Params;
using HYC.SaturnDB;
using HYC.SaturnMain.Devices.MasterPC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HYC.SaturnAoi.Devices.MuraPC
{
    public class MuraPC
    {
        public static MuraPC Instance = null;
        public static DetectResultArgs DetectResultArgs = new DetectResultArgs();
        public string RecipeSyncResult { get; set; }
        static MuraPC()
        {
            Instance = new MuraPC();
        }
        public void TimeSync(int pcIndex)
        {
            try
            {
                MessageInfo info = new MessageInfo();
                info.Type = CommunicationType.Send;
                info.PCName = $"MuraPC";
                info.Command = Command.TimeSync;
                info.Message = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                MasterPCHelper.Instance.SendMessage(info);
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("MuraPC").Fatal(ex.ToString());
            }
        }
    
        public void UserSync(string user, int pcIndex)
        {
            try
            {
                MessageInfo info = new MessageInfo();
                info.Type = CommunicationType.Send;
                info.PCName = $"MuraPC";
                info.Command = Command.UserSync;
                info.Message = user;
                MasterPCHelper.Instance.SendMessage(info);
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("MuraPC").Fatal(ex.ToString());
            }
        }
        public void DetectReq(DetectReqArgs args)
        {
            try
            {
                MessageInfo info = new MessageInfo();
                info.Type = CommunicationType.Send;
                info.PCName = $"MuraPC";
                info.Command = Command.DetectReq;
                info.Datas = ClassHelper.ObjectToBytes(args);
                MasterPCHelper.Instance.SendMessage(info);
                if (Param.Comm.IsShieldMuraPC)
                {
                    DetectResultArgs = new DetectResultArgs();
                    Thread.Sleep(2000);
                    DetectResultArgs = new DetectResultArgs { DetectReqArgs = args, Result = "OK" };
                }
                else
                {
                    DetectResultArgs = new DetectResultArgs();
                }
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("MuraPC").Fatal(ex.ToString());
            }
        }
        public void PowerControlResult(string type, string result)
        {
            try
            {
                MessageInfo info = new MessageInfo();
                info.Type = CommunicationType.Send;
                info.PCName = $"MuraPC";
                info.Command = Command.PowerControlResult;
                info.Message = $"{type},{result}";
                MasterPCHelper.Instance.SendMessage(info);
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("MuraPC").Fatal(ex.ToString());
            }
        }
        public void SwitchPatternResult(int ptnNum, string result)
        {
            try
            {
                MessageInfo info = new MessageInfo();
                info.Type = CommunicationType.Send;
                info.PCName = $"MuraPC";
                info.Command = Command.SwitchPatternResult;
                info.Message = $"{ptnNum},{result}";
                MasterPCHelper.Instance.SendMessage(info);
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("MuraPC").Fatal(ex.ToString());
            }
        }
        public void RecipeSync(string recipe, string recipeOPType)
        {
            try
            {
                RecipeSyncResult = string.Empty;
                if (Param.Comm.IsShieldMuraPC)
                {
                    RecipeSyncResult = "OK";
                }
                MessageInfo info = new MessageInfo();
                info.Type = CommunicationType.Send;
                info.PCName = $"MuraPC";
                info.Command = Command.RecipeSync;
                info.Message = $"{recipe},{recipeOPType}"; ;
                MasterPCHelper.Instance.SendMessage(info);
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("AoiPC").Fatal(ex.ToString());
            }
        }
    }
}
