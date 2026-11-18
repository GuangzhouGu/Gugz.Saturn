/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2023 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnAoi.Entities
// 文件名称    ：FtpHelper
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
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HYC.SaturnAoi.Entities
{
    class FTPHelper
    {
        //ftp 实例
        private FtpWebRequest reqFTP;
        //ftp 信息
        private FtpInfo Info = null;
        public static FTPHelper Instance = null;
        static FTPHelper()
        {
            Instance = new FTPHelper();
        }
        public void Init(FtpInfo info)
        {
            Info = info;
        }
        #region 连接
        /// <summary>
        /// 连接
        /// </summary>
        /// <param name="path"></param>
        private void Connect(string path)//连接ftp
        {
            //if (Info != null)
            //{
            //    Info.Password = PassWord;
            //    Info.UserName = UserName;
            //}
            // 根据uri创建FtpWebRequest对象
            if (reqFTP != null)
            {
                try
                {
                    reqFTP.Abort();
                }
                catch { }
            }
            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(path));
            // 指定数据传输类型
            reqFTP.UseBinary = true;
            // ftp用户名和密码
            reqFTP.Credentials = new NetworkCredential(Info.UserName, Info.Password);
        }
        #endregion

        #region 获取文件列表
        /// <summary>
        /// 获取文件列表
        /// </summary>
        /// <param name="path"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        private string[] GetFileList(string path, string method)//上面的代码示例了如何从ftp服务器上获得文件列表
        {
            StringBuilder result = new StringBuilder();
            try
            {
                Connect(path);
                reqFTP.Method = method;
                using (WebResponse response = reqFTP.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.UTF8))
                    {
                        string content = reader.ReadToEnd();
                        if (!string.IsNullOrEmpty(content))
                        {
                            return content.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                        }
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("Dfs").Fatal($"GetFileList方法执行异常，异常信息：{ex.ToString()}");
                return null;
            }
        }
        private string[] GetFileList(string path)//上面的代码示例了如何从ftp服务器上获得文件列表
        {
            return GetFileList("ftp://" + Info.ServerIP + "/" + path, WebRequestMethods.Ftp.ListDirectory);
        }
        private string[] GetFileList()//上面的代码示例了如何从ftp服务器上获得文件列表
        {
            return GetFileList("ftp://" + Info.ServerIP + "/", WebRequestMethods.Ftp.ListDirectory);
        }
        #endregion

        #region 上传文件
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="filename">上传文件的全路径</param>
        /// <param name="dirName">文件夹名，根目录请填写"",不存在则自动创建</param>
        /// <param name="isGuidName">是否使用GUID文件名，默认为true</param>
        /// <returns></returns>
        public string Upload(string filename, string dirName, bool isGuidName) //上面的代码实现了从ftp服务器上载文件的功能
        {
            try
            {
                LogManager.GetLogger("FTPupload").Info("上传开始");
                bool isGuid = true;
                string savaName = "";
                isGuid = isGuidName;
                dirName = dirName.Trim('/').TrimEnd('/');
                string path = Info.ServerIP.Replace("\\", "/");
                FileInfo fileInf = new FileInfo(filename);
                if (isGuid)
                {
                    string[] temp = fileInf.Name.Split('.');
                    savaName = Guid.NewGuid() + "." + temp[temp.Length - 1];
                }
                else
                {
                    savaName = fileInf.Name;
                }
                MakeMultipleDir(dirName);
                savaName = dirName == "" ? savaName : dirName + "/" + savaName;
                string uri = "ftp://" + path + "/" + savaName;
                Connect(uri);//连接         
                             // 默认为true，连接不会被关闭
                             // 在一个命令之后被执行
                reqFTP.KeepAlive = false;
                // 指定执行什么命令
                reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
                // 上传文件时通知服务器文件的大小
                reqFTP.ContentLength = fileInf.Length;
                // 缓冲大小设置为kb 
                int buffLength = 2048;
                byte[] buff = new byte[buffLength];
                int contentLen;
                // 打开一个文件流(System.IO.FileStream) 去读上传的文件
                FileStream fs = fileInf.OpenRead();
                // 把上传的文件写入流
                Stream strm = reqFTP.GetRequestStream();
                // 每次读文件流的kb
                contentLen = fs.Read(buff, 0, buffLength);
                // 流内容没有结束
                while (contentLen != 0)
                {
                    // 把内容从file stream 写入upload stream 
                    strm.Write(buff, 0, contentLen);
                    contentLen = fs.Read(buff, 0, buffLength);
                }
                // 关闭两个流
                strm.Close();
                fs.Close();
                //errorinfo = "完成";
                LogManager.GetLogger("FTPupload").Info("上传完成");
                return "/" + savaName;

            }
            catch (Exception ex)
            {
                LogManager.GetLogger("FTPHelper").Error(ex);
                return "";
            }
        }

        /// <summary>
        /// 直接传数据流的方式
        /// </summary>
        /// <param name="streamRead">数据流</param>
        /// <param name="dirName">ftp路径</param>
        /// <param name="fileName">文件名称</param>
        /// <returns></returns>
        private string UpdateStream(Stream streamRead, string dirName, string fileName)
        {
            try
            {
                string savaName = "";
                dirName = dirName.Trim('/').TrimEnd('/');
                string path = Info.ServerIP.Replace("\\", "/");
                MakeMultipleDir(dirName);
                savaName = dirName == "" ? savaName : dirName + "/" + fileName;
                string uri = "ftp://" + path + "/" + savaName;
                Connect(uri);//连接         
                             // 默认为true，连接不会被关闭
                             // 在一个命令之后被执行
                reqFTP.KeepAlive = false;
                // 指定执行什么命令
                reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
                // 上传文件时通知服务器文件的大小  
                reqFTP.ContentLength = streamRead.Length;
                // 缓冲大小设置为5kb  
                int buffLength = 2048;
                byte[] buff = new byte[buffLength];
                int contentLen;
                Stream streamWrite = reqFTP.GetRequestStream();
                streamRead.Flush();
                streamRead.Position = 0;
                contentLen = streamRead.Read(buff, 0, buffLength);
                while (contentLen != 0)
                {
                    streamWrite.Write(buff, 0, contentLen);
                    contentLen = streamRead.Read(buff, 0, buffLength);
                }
                // 关闭两个流  
                streamWrite.Close();

                return "/" + savaName;
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("FTPHelper").Error(ex);
                return "";
            }
        }


        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="img">图片</param>
        /// <param name="dirName">ftp地址</param>
        /// <param name="fileName">文件名称</param>
        /// <param name="imgFormat">图片格式</param>
        /// <returns></returns>
        public string UploadImg(Image img, string dirName, string fileName, ImageFormat imgFormat)
        {
            //获取图片流
            try
            {
                MemoryStream streamRead = new MemoryStream();
                img.Save(streamRead, imgFormat);
                return UpdateStream(streamRead, dirName, fileName);
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("FTPHelper").Error(ex);
                return "";
            }
        }
        #endregion

        #region 删除文件
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="fileName">文件的相对路径</param>
        public bool DeleteFileName(string fileName)
        {
            try
            {
                //FileInfo fileInf = new FileInfo(fileName);
                string uri = "ftp://" + Info.ServerIP + "/" + fileName;
                Connect(uri);//连接         
                // 默认为true，连接不会被关闭
                // 在一个命令之后被执行
                reqFTP.KeepAlive = false;
                // 指定执行什么命令
                reqFTP.Method = WebRequestMethods.Ftp.DeleteFile;
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                response.Close();
                return true;
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("FTPHelper").Error(ex);
                return false;
            }
        }
        #endregion

        #region 在ftp上创建目录
        private void MakeMultipleDir(string dirName)
        {
            try
            {
                if (dirName != "")
                {
                    string[] dirs = dirName.Trim('/').TrimEnd('/').Split('/');
                    if (dirs.Length > 0)
                    {
                        string oldpath = "";   //文件夹完整路径
                        string nowdir = "";    //当前文件夹名称
                        bool b = true;         //是否存在该文件夹
                        for (int i = 0; i < dirs.Length; i++)
                        {
                            b = true;
                            string[] list = GetFileList(oldpath.TrimEnd('/'));
                            if (list != null)
                            {
                                for (int z = 0; z < list.Length; z++)
                                {
                                    if (i > 0)
                                        nowdir = dirs[i - 1] + "/" + dirs[i];
                                    else
                                        nowdir = dirs[i];
                                    if (list[z] == nowdir)
                                    {
                                        b = false;
                                        break;
                                    }
                                }
                            }
                            oldpath += dirs[i] + "/";
                            if (b)
                                MakeDir(oldpath.TrimEnd('/'));
                        }
                    }
                    else
                    {
                        MakeDir(dirName);
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("FTPHelper").Error(ex);
            }
        }

        /// <summary>
        /// 在ftp上创建目录
        /// </summary>
        /// <param name="dirName"></param>
        private void MakeDir(string dirName)
        {
            try
            {
                string uri = "ftp://" + Info.ServerIP + "/" + dirName;
                Connect(uri);//连接      
                reqFTP.Method = WebRequestMethods.Ftp.MakeDirectory;
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                response.Close();
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("FTPHelper").Error(ex);
                // MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region 删除ftp上目录
        /// <summary>
        /// 删除ftp上目录
        /// </summary>
        /// <param name="dirName"></param>
        public void DelDir(string dirName)
        {
            try
            {
                string uri = "ftp://" + Info.ServerIP + "/" + dirName;
                Connect(uri);//连接      
                reqFTP.Method = WebRequestMethods.Ftp.RemoveDirectory;
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                response.Close();
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("FTPHelper").Error(ex);
                // MessageBox.Show(ex.Message);
            }
        }
        #endregion



        #region 重命名对象
        /// <summary>
        /// 重命名对象
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="newFilename"></param>
        public void Rename(string filePath, string newFilename)
        {
            try
            {
                string uri = "ftp://" + Info.ServerIP + "/" + filePath;
                Connect(uri);//连接
                reqFTP.Method = WebRequestMethods.Ftp.Rename;
                reqFTP.RenameTo = newFilename;
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                response.Close();
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("FTPHelper").Error(ex);
                // MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region 获得文件明细
        /// <summary>
        /// 获得文件明细
        /// </summary>
        /// <returns></returns>
        public string[] GetFilesDetailList()
        {
            return GetFileList("ftp://" + Info.ServerIP + "/", WebRequestMethods.Ftp.ListDirectoryDetails);
        }
        /// <summary>
        /// 获得文件明细
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string[] GetFilesDetailList(string path)
        {
            path = path.Replace("\\", "/");
            return GetFileList("ftp://" + Info.ServerIP + "/" + path, WebRequestMethods.Ftp.ListDirectoryDetails);
        }
        #endregion
    }
    public class FtpInfo
    {
        /// <summary>
        /// ftp服务端ip
        /// </summary>
        public string ServerIP { get; set; }
        /// <summary>
        /// ftp登录用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// ftp登录密码
        /// </summary>
        public string Password { get; set; }

    }
}
