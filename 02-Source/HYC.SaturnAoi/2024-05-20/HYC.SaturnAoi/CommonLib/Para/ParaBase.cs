/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2021 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ： TPAssembly.Config
// 文件名称    ：ParaBase.cs
// =================================
// 创 建 者    ：guguangzhou
// 创建日期    ：2021/5/8 09:00:00 
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

using HYC.HTFile;
using HYC.HTFile.HTXML;
using HYC.HTLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CommonLib.Para
{
    public class ParaBase
    {
        public static string FilePath = string.Empty;
        public T Load<T>(T para)
        {
            try
            {
                string fileName = para.ToString().Split('.').Last() + ".xml";
                string fullName = Path.Combine(FilePath, fileName);
                para = HTXMLSerializer.EntityFromXMLFile<T>(fullName);
                return para;
            }
            catch (Exception ex)
            {
                return (T)Activator.CreateInstance(para.GetType());
            }
        }
        public bool Save<T>(T para)
        {
            try
            {
                string fileName = para.ToString().Split('.').Last() + ".xml";
                string fullName = Path.Combine(FilePath, fileName);
                bool ret = HTXmlHelper.EntityToXMLFile(para, fullName);
                return ret;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }

}
