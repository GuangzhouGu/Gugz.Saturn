using System;
using System.IO;
using System.Drawing;
using FluentFTP;
using HYC.HTLog;
using System.Threading.Tasks;
using System.Threading;

namespace DFS
{
    public class DFSManager
    {
        public FtpClient Client = null;
        public event Action<string, int, bool> OnConnected;
        //消息显示事件
        public event Action<string, string, Brush> OnShowMsg;
        public static DFSManager Instance = null;
        static DFSManager()
        {
            Instance = new DFSManager();
        }
        public void Init()
        {
            DFSSettings.Load();
            Client = new FtpClient(DFSSettings.FTPHost, DFSSettings.FTPUser, DFSSettings.FTPPwd);
            Connect();
        }
        public void Connect()
        {
            bool LastSts = true;
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    try
                    {
                        bool sts = Client.IsConnected && DFSSettings.IsDFSEnabled;
                        if (sts != LastSts)
                        {
                            if (Client.IsConnected == false)
                            {
                                Client.Connect();
                                System.Threading.Thread.Sleep(10);
                            }
                            LastSts = sts;
                            OnConnected?.Invoke("DFS", 0, sts);
                        }
                    }
                    catch (Exception)
                    {

                    }
                    System.Threading.Thread.Sleep(500);
                }

            });
        }
        public bool IsConnected()
        {
            try
            {
                if (Client.IsConnected == false)
                {
                    Client.Connect();
                }
                return Client.IsConnected && DFSSettings.IsDFSEnabled;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UploadDataFile(string panelId, string localFileName)
        {
            try
            {
                //文件名
                string localPath = Path.GetDirectoryName(localFileName);
                DirectoryInfo dir = new DirectoryInfo(localPath);
                var files = dir.GetFiles();
                foreach (FileInfo file in files)
                {
                    //FTP文件夹路径
                    string remoteDir = DFSSettings.GetDataFileUploadDir(panelId);
                    string filename = file.Name;
                    localFileName = file.FullName;
                    if (file.Extension.ToLower() == ".xml")
                    {
                        remoteDir = DFSSettings.GeXmltDataFileUploadDir(panelId);
                    }
                    else if (file.Extension.ToLower() == ".dmy")
                    {
                        remoteDir = DFSSettings.GetDummyFileUploadDir();
                    }
                    string remoteFileName = remoteDir + filename;
                    //上传文件
                    if (Client.UploadFile(localFileName, remoteFileName, FtpExists.Overwrite, true))
                    {
                        OnShowMsg?.Invoke("DFS", $"上传文件成功：{filename}，远程路径：{remoteFileName}", Brushes.Green);
                     
                    }
                    else
                    {
                        OnShowMsg?.Invoke("DFS", $"上传文件失败：{filename}，远程路径：{remoteFileName}" , Brushes.Red);
                        return false;
                    }

                }
                return true;
            }
            catch (Exception ex)
            {
                OnShowMsg?.Invoke("DFS", "上传文件失败：" + ex.Message, Brushes.Red);
                return false;
            }
        }

        public bool UploadImage(string panelId, string localFileName, string detectType)
        {
            try
            {
                //FTP文件夹路径
                string localPath = Path.GetDirectoryName(localFileName);
                panelId = panelId.Split('_')[0];
                string remoteDir = DFSSettings.GetImageUploadDir(panelId) + $"{detectType}/";
                DirectoryInfo dir = new DirectoryInfo(localPath);
                var files = dir.GetFiles();
                foreach (FileInfo file in files)
                {
                    if (file.Extension.ToLower() == ".jpg" || file.Extension.ToLower() == ".png")
                    {
                        string filename = file.Name;
                        localFileName = file.FullName;
                        string remoteFileName = remoteDir + filename;
                        LogManager.GetLogger("DFS").Info("RemoteFile:" + remoteFileName + "。LocalFile:" + localFileName);

                        //上传文件
                        if (Client.UploadFile(localFileName, remoteFileName, FtpExists.Overwrite, true))
                        {
                            LogManager.GetLogger("DFS").Info("上传图片成功。" + "RemoteFile:" + remoteFileName + "。LocalFile:" + localFileName);
                            OnShowMsg?.Invoke("DFS", "上传图片成功。" + "RemoteFile:" + remoteFileName + "。LocalFile:" + localFileName, Brushes.Green);
                        }
                        else
                        {
                            LogManager.GetLogger("DFS").Info("上传图片失败。" + "RemoteFile:" + remoteFileName + "。LocalFile:" + localFileName);
                            OnShowMsg?.Invoke("DFS", "上传图片失败。" + "RemoteFile:" + remoteFileName + "。LocalFile:" + localFileName, Brushes.Red);
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("Error").Error(ex.Message + "\r\n" + ex.StackTrace);
                OnShowMsg?.Invoke("DFS", "上传图片失败：" + ex.Message, Brushes.Red);
                return false;
            }
        }
    }
}
