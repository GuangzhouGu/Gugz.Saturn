using HYC.HTLog;
using HYC.HTPCBase.Extension.Recipe;
using HYC.SaturnAoi.Params;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HYC.SaturnAoi.Entities
{
    public class RecipeSyncManager
    {
        /// <summary>
        /// 从旧机种创建新机种
        /// </summary>
        /// <param name="recipeIDOld">旧机种名</param>
        /// <param name="recipeIDNew">新机种名</param>
        public static bool CreateNewRecipe(string recipeIDOld, string recipeIDNew)
        {
            try
            {
                string aoiPath = string.Empty,muraPath = string.Empty, systemPath = string.Empty;
                string aoiPath2 = string.Empty, muraPath2 = string.Empty, systemPath2 = string.Empty;
                Task.Factory.StartNew(()=> {
                     aoiPath = AppDomain.CurrentDomain.BaseDirectory + @"\Module\Aoi\";
                     muraPath = AppDomain.CurrentDomain.BaseDirectory + @"\Module\Mura\";
                     systemPath = AppDomain.CurrentDomain.BaseDirectory + @"\Config\Recipe\";
                    //复制文件
                    GetFilesAndDirs(aoiPath + recipeIDOld, aoiPath + recipeIDNew);
                    GetFilesAndDirs(muraPath + recipeIDOld, muraPath + recipeIDNew);
                    GetFilesAndDirs(systemPath + recipeIDOld, systemPath + recipeIDNew);
                });
                Task.Factory.StartNew(() => {
                     aoiPath2 = $"{Param.System.MuraPCExePath}Module\\Aoi\\";
                     muraPath2 = $"{Param.System.MuraPCExePath}Module\\Mura\\";
                     systemPath2 = $"{Param.System.MuraPCExePath}Config\\Recipe\\";
                    //复制文件
                    GetFilesAndDirs(aoiPath2 + recipeIDOld, aoiPath2 + recipeIDNew);
                    GetFilesAndDirs(muraPath2 + recipeIDOld, muraPath2 + recipeIDNew);
                    GetFilesAndDirs(systemPath2 + recipeIDOld, systemPath2 + recipeIDNew);
                });
                Thread.Sleep(3000);
                return true;
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("RecipeSyncManager").Info($"CreateNewRecipe方法执行失败，异常信息：{ex}");
                return false;
            }
        }
        //public static bool DeleteRecipe()
        //{

        //}
        private static void GetFilesAndDirs(string srcDir, string destDir)
        {
            try
            {
                if (Directory.Exists(destDir) == false)
                {
                    Directory.CreateDirectory(destDir);
                }
                FileInfo fileInfo;
                string newPath;
                string[] files = Directory.GetFiles(srcDir);
                foreach (string path in files)          //遍历文件     
                {
                    fileInfo = new FileInfo(path);
                    newPath = Path.Combine(destDir, fileInfo.Name);
                    File.Copy(path, newPath, true);
                }
                string[] dirs = Directory.GetDirectories(srcDir);
                foreach (string path in dirs)        //遍历文件夹
                {
                    DirectoryInfo directory = new DirectoryInfo(path);
                    string newDir = Path.Combine(destDir, directory.Name);
                    GetFilesAndDirs(path + "\\", newDir + "\\");
                }
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("RecipeSyncManager").Info($"GetFilesAndDirs方法执行失败，异常信息：{ex}");
            }
        }
    }
}
