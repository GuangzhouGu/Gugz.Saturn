/*----------------------------------------------------------------
// Copyright (C)  Suzhou HYC Technology Co.,LTD
// 版权所有。
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：CommonLib.Entities
// 文件名称    ：ClassHelper.cs
// =================================
// 创 建 者    ：guguangzhou
// 创建日期    ：2021/01/29 09:00:00 
// 功能描述    ：枚举帮助类
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
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Entities
{
    public class ClassHelper
    {
        /// <summary>
        /// 返回基类所有的子类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<T> GetChildClass<T>()
        {
            try
            {
                List<T> childList = new List<T>();
                var types = Assembly.GetEntryAssembly().GetTypes();
                List<Type> typeList = types.ToList();
                var types2 = Assembly.GetAssembly(typeof(T)).GetTypes();
                typeList.AddRange(types2);
                try
                {
                    var types3 = Assembly.Load("HYC.HTPCBase").GetTypes();
                    typeList.AddRange(types3);
                }
                catch (Exception) { }
                typeList = typeList.GroupBy(m => m.FullName.ToLower().Trim()).Select(m => m.First()).ToList();
                foreach (var type in typeList)
                {
                    if (type != typeof(T) && typeof(T).IsAssignableFrom(type))
                    {
                        T obj = (T)Activator.CreateInstance(type);
                        if (obj != null)
                        {
                            childList.Add(obj);
                        }
                    }
                }
                return childList;
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("ClassHelper").Fatal(ex);
                return null;
            }
        }
        /// <summary>
        /// 对象转字节数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte[] ObjectToBytes<T>(T obj)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    // 序列化成流
                    bf.Serialize(ms, obj);
                    byte[] datas = ms.ToArray();
                    ms.Close();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("ClassHelper").Fatal(ex);
                return null;
            }
        }
        /// <summary>
        /// 字节数组转对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="datas"></param>
        /// <returns></returns>
        public static T BytesToObject<T>(byte[] datas)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    ms.Write(datas, 0, datas.Length);
                    ms.Seek(0, SeekOrigin.Begin);
                    // 反序列化成对象
                    BinaryFormatter bf = new BinaryFormatter();
                    T retval = (T)bf.Deserialize(ms);
                    ms.Close();
                    return retval;
                }
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("ClassHelper").Fatal(ex);
                return default(T);
            }
        }
        /// <summary>
        /// 判断当前窗体是否处于设计器模式，true：设计模式，false：非设计模式
        /// </summary>
        /// <returns></returns>
        public static bool IsDesingMode()
        {
            bool ReturnFlag = false;
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                ReturnFlag = true;
            else if (System.Diagnostics.Process.GetCurrentProcess().ProcessName == "devenv")
                ReturnFlag = true;
            return ReturnFlag;
        }
    }
}
