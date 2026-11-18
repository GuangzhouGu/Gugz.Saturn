using HYC.HTLog;
using HYC.SaturnAoi;
using HYC.SaturnAoi.Devices.Client;
using HYC.SaturnAoi.Devices.ClientPC;
using HYC.SaturnAoi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HYC.SaturnMuraMultiAngle.Entities
{
    public class PGHelper
    {
        ManualResetEvent Mre = new ManualResetEvent(false);
        string SwitchResult = string.Empty;
        string PowerControlResultStr = string.Empty;
       public static PGHelper Instance = null;
        static PGHelper()
        {
            Instance = new PGHelper();
         
        }
        public bool PowerOn( )
        {
            try
            {
                PowerControlResultStr = string.Empty;
                MessageInfo info = new MessageInfo();
                info.Type = CommunicationType.Send;
                info.PCName = $"AoiPC";
                info.Command = Command.PowerControlReq;
                info.Message = $"-1,PowerOn";
                Mre.Reset();
                ClientHelper.Instance.SendMessage(info);
                var ret = Mre.WaitOne(3000);
                return ret && PowerControlResultStr == "OK";
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("PGHelper").Fatal(ex.ToString());
                return false;
            }
        }
        public bool PowerOff( )
        {
            try
            {
                PowerControlResultStr = string.Empty;
                MessageInfo info = new MessageInfo();
                info.Type = CommunicationType.Send;
                info.PCName = $"AoiPC";
                info.Command = Command.PowerControlReq;
                info.Message = $"-1,PowerOff";
                Mre.Reset();
                ClientHelper.Instance.SendMessage(info);
                var ret = Mre.WaitOne(3000);
                return ret && PowerControlResultStr == "OK";
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("PGHelper").Fatal(ex.ToString());
                return false;
            }
        }
        public void PowerControlResult(string type,string result)
        {
            try
            {
                PowerControlResultStr = result;
                Mre.Set();
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("PGHelper").Fatal(ex.ToString());
            }
        }
     
        public bool SwitchPattern(int patternNum)
        {
            try
            {
                SwitchResult = string.Empty;
                MessageInfo info = new MessageInfo();
                info.Type = CommunicationType.Send;
                info.PCName = $"AoiPC";
                info.Command = Command.SwitchPatternReq;
                info.Message = $"{patternNum}";
                Mre.Reset();
                ClientHelper.Instance.SendMessage(info);
                var ret = Mre.WaitOne(3000);
                return ret && SwitchResult == "OK";
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("PGHelper").Fatal(ex.ToString());
                return false;
            }
        }
        public void SwitchPatternResult(int patternNum, string result)
        {
            try
            {
                SwitchResult = result;
                Mre.Set();
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("PGHelper").Fatal(ex.ToString());
            }
        }
    }
}
