/*----------------------------------------------------------------
// Copyright (C)  Suzhou HYC Technology Co.,LTD
// 版权所有。
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：CommonLib.Entities
// 文件名称    ：XmlHelper.cs
// =================================
// 创 建 者    ：guguangzhou
// 创建日期    ：2021/11/03 09:00:00 
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace CommonLib.Entities
{
    public class XmlHelper
    {
        /// <summary>
        /// 查找单个节点
        /// </summary>
        /// <param name="xn"></param>
        /// <param name="strNodeName"></param>
        /// <param name="result"></param>
        public static void FindNode(XmlNode xn, string strNodeName, ref XmlNode result)
        {
            if (strNodeName == xn.Name)
            {
                result = xn;
                throw new Exception("FindNode");
            }
            else
            {
                if (xn.HasChildNodes)
                {
                    foreach (XmlNode node in xn.ChildNodes)
                    {
                        FindNode(node, strNodeName, ref result);
                    }
                }
            }
        }
        /// <summary>
        /// 查找单个元素
        /// </summary>
        /// <param name="xn"></param>
        /// <param name="strNodeName"></param>
        /// <param name="result"></param>
        public static void FindElement(XElement element, string strNodeName, ref XElement e)
        {
            if (strNodeName == element.Value)
            {
                e = element.Parent;
                throw new Exception("FindNode");
            }
            else
            {
                if (element.HasElements)
                {
                    foreach (XElement node in element.Elements())
                    {
                        FindElement(node, strNodeName, ref e);
                    }
                }
            }
        }
        /// <summary>
        /// 删除指定名称的节点
        /// </summary>
        public static void DeleteNode(XmlNode xnParent, string strNodeName)
        {
            XmlNode xn = null;
            try
            {
                FindNode(xnParent, strNodeName, ref xn);
            }
            catch (Exception)
            {
            }
            if (xn != null)
            {
                XmlNode parentNode = xn.ParentNode;
                parentNode.RemoveChild(xn);
            }
            return;
        }
    }
}
