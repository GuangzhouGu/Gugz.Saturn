using HYC.HTFile.HTINI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DFS
{
    public class DFSSettings
    {
        //FTP相关信息
        public static string FTPHost = string.Empty;
        public static string FTPUser = string.Empty;
        public static string FTPPwd = string.Empty;

        //上传路径相关信息
        public static string Shop = "module";
        public static string UploadMode = "online"; //自动上传-online 手动上传-offline
        public static string StepID = string.Empty;//AMI M25000N    AFI M64000N
        //其他参数设置
        public static string EQPID = "TOS";
        public static string UNITID = "AOI1";
        static HTINIHelper IniHelper= new HTINIHelper(AppDomain.CurrentDomain.BaseDirectory + @"Config\DFSConfig.cfg");
        public static bool IsDFSEnabled = true; //是否启用DFS
        public static event Action UpdateView;
        public static void Load()
        {
            FTPHost = IniHelper.Read("FTP", "FTPHost", "ftp://10.141.70.243/");
            FTPUser = IniHelper.Read("FTP", "FTPUser", "modftp");
            FTPPwd = IniHelper.Read("FTP", "FTPPwd", "modftp");

            Shop = IniHelper.Read("DFS", "Shop", "module");
            StepID = IniHelper.Read("DFS", "StepID", "T380OSN");
            EQPID = IniHelper.Read("DFS", "EQPID", "TOS");
            UNITID = IniHelper.Read("DFS", "UNITID", "AOI1");
            UploadMode = IniHelper.Read("DFS", "Mode", "offline");
            IsDFSEnabled = Convert.ToBoolean(IniHelper.Read("DFS", "IsDFSEnabled", "false"));
        }

        public static void Save()
        {
            IniHelper.Write("FTP", "FTPHost", FTPHost);
            IniHelper.Write("FTP", "FTPUser", FTPUser);
            IniHelper.Write("FTP", "FTPPwd", FTPPwd);
            IniHelper.Write("DFS", "Shop", Shop);
            IniHelper.Write("DFS", "StepID", StepID);
            IniHelper.Write("DFS", "EQPID", EQPID);
            IniHelper.Write("DFS", "UNITID", UNITID);
            IniHelper.Write("DFS", "Mode", UploadMode);
            IniHelper.Write("DFS", "IsDFSEnabled", IsDFSEnabled.ToString());
        }
        public static void SyncStepID(string stepID)
        {
            if (StepID!=stepID)
            {
                StepID = stepID;
                IniHelper.Write("DFS", "StepID", StepID);
                UpdateView?.Invoke();
            }
        }
        /// <summary>
        /// 获取文件上传路径
        /// </summary>
        /// <param name="panelId"></param>
        /// <returns></returns>
        public static string GetDataFileUploadDir(string panelId)
        {
            return GetDataFileUploadDir(panelId,Shop, UploadMode, StepID);
        }

        public static string GetDataFileUploadDir(string panelId, string shop, string mode, string stepId)
        {
            string remoteDir = mode + "/" + "data" + "/" + stepId +"/";
            remoteDir += panelId.Substring(0, 4) + "/" + panelId.Substring(4, 2) + "/" +
                panelId.Substring(6, 4) + "/" + panelId.Substring(10, 3) ;
            if (remoteDir.EndsWith("/") == false) remoteDir += "/";
            return remoteDir;     //.ToUpper();//要转换成大写，否则会失败
        }
        public static string GeXmltDataFileUploadDir(string panelId)
        {
            return GetXmlDataFileUploadDir(panelId, Shop, UploadMode, StepID);
        }

        public static string GetXmlDataFileUploadDir(string panelId, string shop, string mode, string stepId)
        {
            string remoteDir = mode + "/" + "data" + "/" + stepId + "/";
            remoteDir += panelId.Substring(0, 4) + "/" + panelId.Substring(4, 2) + "/" +
                panelId.Substring(6, 4) + "/" + panelId.Substring(10, 3) + "/RAW/";
            if (remoteDir.EndsWith("/") == false) remoteDir += "/";
            return remoteDir;     //.ToUpper();//要转换成大写，否则会失败
        }
        public static string GetImageUploadDir(string panelId)
        {
            return GetImageUploadDir(panelId, Shop, UploadMode, StepID);
        }

        public static string GetImageUploadDir(string panelId, string shopId, string mode, string stepId)
        {
            string remoteDir = mode + "/" + "image" + "/" + stepId + "/";
            remoteDir += panelId.Substring(0, 4) + "/" + panelId.Substring(4, 2) + "/" +
                panelId.Substring(6, 4) + "/" + panelId.Substring(10, 3) ;
            if (remoteDir.EndsWith("/") == false) remoteDir += "/";
            return remoteDir;    //.ToUpper();  //大写
        }

        public static string GetDummyFileUploadDir()
        {
            return GetDummyFileUploadDir(Shop, UploadMode, StepID);
        }

        public static string GetDummyFileUploadDir(string shopId, string mode, string stepId)
        {
            string remoteDir = mode + "/" + "data" + "/" + stepId + "/" + "dummy" + "/";
            if (remoteDir.EndsWith("/") == false) remoteDir += "/";
            return remoteDir;      //.ToUpper();
        }

        public static string GetDataFileName(string glassID)
        {
            return glassID + "_" + StepID + "_" + EQPID + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".hjl";
        }
        public static string GetXmlFileName(string lotID, string glassID,string jogJudge,string jobGrade,string endTime,string startTime)
        {
            return jogJudge + "_" +lotID + "_" + glassID + "_" + StepID + "_" + EQPID + "_" + endTime + "_" + jobGrade + "_" + startTime+".xml";
        }
        public static string GetXmlFileName2(string glassID, string firstCellID,string lastCellID)
        {
            return  glassID + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + lastCellID + "_" + firstCellID + ".xml";
        }
    }
}
