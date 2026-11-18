/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2023 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：CIMBOE.Block
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIMBOE.Block
{
    public class ConvertHelper
    {
        public static string DecimalToBinary(int decimalNumber, int bitCount, bool isInverse = false)
        {
            StringBuilder sb = new StringBuilder();
            if (isInverse)
            {
                for (int i = bitCount - 1; i >= 0; i--)
                {
                    int ret = (decimalNumber & (1 << i)) != 0 ? 1 : 0;
                    sb.Append(ret);
                }
            }
            else
            {
                for (int i = 0; i < bitCount; i++)
                {
                    int ret = (decimalNumber & (1 << i)) != 0 ? 1 : 0;
                    sb.Append(ret);
                }
            }
            return sb.ToString();
        }
    }
    public interface IArrayAble
    {
        ushort[] ToArray();
        void FromArray(ushort[] datas);
    }
}
