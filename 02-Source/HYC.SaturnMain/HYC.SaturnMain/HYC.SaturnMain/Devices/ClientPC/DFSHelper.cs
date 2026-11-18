using HYC.HTLog;
using HYC.SaturnMain.Devices.MasterPC;
using HYC.SaturnMain.Entities;
using HYC.SaturnMain.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYC.SaturnMain.Devices.ClientPC
{
    public class DFSHelper
    {
        public static void RecipeSync(string recipe)
        {
            try
            {
                MessageInfo info = new MessageInfo();
                info.Type = CommunicationType.Send;
                info.PCName = $"{ClientPCName.DFS}";
                info.Command = Command.RecipeSync;
                info.Message = recipe;
                MasterPCHelper.Instance.SendMessage(info);
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("AoiPC").Fatal(ex.ToString());
            }
        }
        public static void DfsUpload(string glassID)
        {
            if (Param.Comm.DFSShield)
            {
                return;
            }
            MessageInfo info = new MessageInfo();
            info.Type = CommunicationType.Send;
            info.PCName = $"{ClientPCName.DFS}";
            info.Command = Command.DfsUpload;
            info.Message = glassID;
            MasterPCHelper.Instance.SendMessage(info);
        }
        public static void UpdateStepID(string stepID)
        {
            if (Param.Comm.DFSShield)
            {
                return;
            }
            MessageInfo info = new MessageInfo();
            info.Type = CommunicationType.Send;
            info.PCName = $"{ClientPCName.DFS}";
            info.Command = Command.UpdateStepID;
            info.Message = stepID;
            MasterPCHelper.Instance.SendMessage(info);
        }
    }
}
