/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2023 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnMain.Entities
// 文件名称    ：Class1
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
using HYC.SaturnDB.Model;
using HYC.HTLog;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using HYC.SaturnMain.Params;

namespace HYC.SaturnMain.Entities
{
    public class ContactCamManager
    {
        public static bool Grab(CurrentDetectInfo info,int colNum)
        {
            bool ret = false;
            try
            {
                Bitmap image = HTCameraHelper.Instance.Grab(0, 1000);
                if (image != null)
                {
                    string dir = GetDirectory(info.GlassID, info.GlassNum);
                    string name = $"{info.GlassID}_{(info.GlassNum == 0 ? "A" : "B")}_Column{colNum + 1}_{DateTime.Now.ToString("yyyyMMddHHmmss")}";
                    string pathname = dir + "\\" + $"{name}.jpeg";
                    image.Save(pathname, System.Drawing.Imaging.ImageFormat.Jpeg);
                    ret = true;
                }
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("ContactCamManager").Info($"Grab方法执行异常，异常信息：{ex}");
            }
            return ret;
        }
        private static string GetDirectory(string glassID, int glassNum)
        {
            string dir = Path.Combine(Param.Control.ContactCamImagesPath, DateTime.Now.ToString("yyyyMMdd"), "Glass" + (glassNum + 1).ToString(), glassID, "ContactCam");
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            return dir;
        }

    }
}
