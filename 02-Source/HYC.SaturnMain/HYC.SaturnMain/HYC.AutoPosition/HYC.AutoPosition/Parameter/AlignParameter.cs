/*----------------------------------------------------------------
// Copyright (C) Suzhou HYC Technology Co.,LTD
// 版权所有。
//
// =================================
// CLR版本     ：4.0.30319.42000
// 命名空间    ：HYC.AutoPosition.Parameter
// 文件名称    ：AlignParameter.cs
// =================================
// 创 建 者    ：miaoliang
// 创建日期    ：2020/12/2 16:01:20 
// 功能描述    ：对位参数
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
using HYC.HTFile.HTXML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYC.AutoPosition.Parameter
{
    /// <summary>
    /// 对位极限参数
    /// </summary>
    [Serializable]
    public class AlignLimit
    {
        public double X = 0.1;
        public double Y = 0.1;
        public double T = 0.1;
    }
    /// <summary>
    /// 对位系统参数
    /// </summary>
    [Serializable]
    public class AlignParameter
    {
        /// <summary>
        /// 匹配率设定 0 -- Left Camera  1 -- Right Camera
        /// </summary>
        public double[] MatchingRateLimitCamera = new double[2];
        /// <summary>
        /// 对位极限 0 -- OK限值， 1 -- Over Range 限值
        /// </summary>
        public AlignLimit[] AlignLimits;
        public AlignParameter()
        {
            AlignLimits = new AlignLimit[2];
            AlignLimits[0] = new AlignLimit();
            AlignLimits[1] = new AlignLimit();
        }

        public AlignLimit this[int index]
        {
            get
            {
                if (index < 0 || index >= AlignLimits.Length)
                {
                    return null;
                }
                return AlignLimits[index];
            }
            set
            {
                if (index <= 0 || index >= AlignLimits.Length)
                {
                    return;
                }
                AlignLimits[index] = value;
            }
        }
        /// <summary>
        /// 序列化参数到文件中
        /// </summary>
        /// <param name="fileName"></param>
        public void Serialize(string fileName)
        {
            HTXMLSerializer.EntityToXMLFile(this, fileName);
        }

        /// <summary>
        /// 从文件中反序列化
        /// </summary>
        /// <param name="fileName"></param>
        public static AlignParameter Unserialize(string fileName)
        {
            try
            {
                AlignParameter alignParameter = HTXMLSerializer.EntityFromXMLFile<AlignParameter>(fileName);
                return alignParameter;
            }
            catch (Exception ex)
            {
                return new AlignParameter();
            }
            
        }

    }

}
