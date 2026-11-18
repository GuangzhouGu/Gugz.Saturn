/*----------------------------------------------------------------
// Copyright © 2021 HYC. All Rights Reserved
// 版权所有。
//
// =================================
// CLR版本     ：4.0.30319.42000
// 命名空间    ：CommonLib.Entities
// 文件名称    ：PlcDataPacket.cs
// =================================
// 创 建 者    ：gugz
// 创建日期    ：2022/1/7 12:56:00 
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

namespace CommonLib.Entities
{
    /// <summary>
    /// PLC数据包
    /// </summary>
    [Serializable]
    public class PlcDataPacket
    {
        /// <summary>
        /// PLC->PC 位数据
        /// </summary>
        public bool[] DataReadBits { get; set; }
        /// <summary>
        /// PLC->PC 字数据
        /// </summary>
        public ushort[] DataReadWords { get; set; }
        /// <summary>
        /// PC->PLC 位数据
        /// </summary>
        public bool[] DataWriteBits { get; set; }
        /// <summary>
        /// PC->PLC 字数据
        /// </summary>
        public bool[] DataWriteWords { get; set; }
    }
}
