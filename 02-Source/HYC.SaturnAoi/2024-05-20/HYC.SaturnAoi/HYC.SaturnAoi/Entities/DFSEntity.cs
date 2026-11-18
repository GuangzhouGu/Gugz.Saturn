using DFS;
using DFS.DataFile;
using DFS.Model;
using HYC.HTLog;
using HYC.SaturnAoi.Params;
using HYC.SaturnDB.DAL;
using HYC.SaturnDB.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HYC.SaturnAoi.Entities
{
   public class DFSEntity
    {
        public static DataFile_Saturn DataFileSaturn = null;
        public static event Action<string, string, Brush> OnShowMsg;
        /// <summary>
        /// 初始化，程序启动时调用一次
        /// </summary>
        public static void Init()
        {
            StartUpload();
        }
      
        #region 上传线程
        private static void StartUpload()
        {
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    DfsUploadDoWork();
                    Thread.Sleep(1000);
                }
            });
        }

        private static void DfsUploadDoWork()
        {
            try
            {
                if (DFSSettings.IsDFSEnabled == false) return;
                // if (DFSManager.Instance.IsConnected() == false) return;
                DfsUploadInfoDal dal = new DfsUploadInfoDal();
                var lst = dal.GetAll();
                if (lst != null)
                {
                    for (int i = 0; i < lst.Count; i++)
                    {
                        DfsUploadInfoModel model = lst[i];
                        if (model.PcIndex==Param.System.PcIndex)
                        {
                            if (string.IsNullOrEmpty(model.GlassID)) continue;
                            //开始上传文件
                            //FileInfo file = new FileInfo(model.FilePath);
                            if (Directory.Exists(model.FilePath))
                            {
                                bool result = DFSManager.Instance.UploadImage(model.GlassID, model.FilePath, model.DetectType);
                                if (result)
                                {
                                    //更新DB
                                    model.SendFlg = 0;
                                    dal.Update(model);
                                }
                                else
                                {
                                    //更新DB
                                    model.SendFlg = 2;
                                    dal.Update(model);
                                }
                            }
                            else
                            {
                                //更新DB
                                model.SendFlg = -1;
                                dal.Update(model);
                            }
                        }
                    
                    }
                }

            }
            catch (Exception ex)
            {
                //OnShowMsg?.Invoke("DFSEntity", $"DfsUploadDoWork方法执行失败，异常信息：{ex}", Brushes.Red);
                LogManager.GetLogger("DFSEntity").Info(ex.ToString());
            }
        }

        #endregion
    }
}
