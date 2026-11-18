/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2021 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：TPAssembly.Config
// 文件名称    ：ConfigBase.cs
// =================================
// 创 建 者    ：guguangzhou
// 创建日期    ：2021/01/29 09:00:00 
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
using CommonLib.Entities;
using CommonLib.Para;
using HYC.HTFile.HTXML;
using HYC.HTLog;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace CommonLib.Entities
{
    public class ParaManager
    {
        public List<IPara> ParaList = new List<IPara>();
        public string RecipeName = string.Empty;
        public string FilePath = AppDomain.CurrentDomain.BaseDirectory + @"Config\Recipe\";
        public static event Action<string, string, Brush> OnShowMsg;
        public static event Action<string> OnParaChange;
        public bool LoadPara()
        {
            bool result = true;
            try
            {
                ParaList = ClassHelper.GetChildClass<IPara>();
                ParaBase.FilePath = Path.Combine(FilePath, RecipeName);
                for (int i = 0; i < ParaList.Count; i++)
                {
                    ParaList[i] = ParaList[i].LoadPara();
                }
            }
            catch (Exception ex)
            {
                OnShowMsg?.Invoke("ParaManager", $"ParaManager->LoadPara方法执行异常，异常信息：{ex.ToString()}", Brushes.Red);
                result = false;
            }
            return result;
        }
        public bool SavePara<T>(T type)
        {
            try
            {
                bool? result = true;
                ParaBase.FilePath = Path.Combine(FilePath, RecipeName);
                if (type.ToString() != "None")
                {
                    var para = Get(type);
                    result &= para.SavePara();
                }
                else
                {
                    bool[] temp = ParaList.Select(p => p.SavePara()).ToArray();
                    result = !temp.Contains(false);
                }
                if (result != null && (bool)result)
                {
                    try
                    {
                        OnParaChange?.Invoke(type.ToString());
                    }
                    catch (Exception ex)
                    {
                        LogManager.GetLogger("ParaManager").Info($"OnParaChange事件执行异常，异常信息：{ex}");
                    }
                }
                return result ?? false;
            }
            catch (Exception ex)
            {
                OnShowMsg?.Invoke("ParaManager", $"ParaManager->SavePara方法执行异常，异常信息：{ex.ToString()}", Brushes.Red);
                return false;
            }
        }
        private static object LockObj = new object();
        public IPara Get<T>(T type)
        {
            try
            {
                //Monitor.Enter(LockObj);
                IPara para = null;
                foreach (var item in ParaList.ToArray())
                {
                    if (item.GetType().Name.Contains(type.ToString()))
                    {
                        para = item;
                        break;
                    }
                }
                return para;
            }
            catch (Exception ex)
            {
                OnShowMsg?.Invoke("ParaManager", $"ParaManager->Get方法执行异常，异常信息：{ex.ToString()}", Brushes.Red);
                return null;
            }
            finally
            {
                //Monitor.Exit(LockObj);
            }

        }
        public List<IPara> GetParas()
        {
            try
            {
                return ParaList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
