/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2023 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：CIMBOE.Block
// 文件名称    ：MachineModeBlock
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
using CIMBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIMBOE.Block
{
    public class MachineModeBlock : ICIMData
    {
        /// <summary>
        /// 1个ushort长度,总长度20个字符
        /// 0: No Machine Mode exist
        ///1: Normal Mode
        ///2: Recovery(Force Clean Out)  Mode
        ///3: Skip Mode
        ///4: Cold Run Mode
        ///5: TR Mode
        ///6: PECVD Only Mode(For PECVD)
        ///7: Manual Mode(For Furnace)
        /// </summary>
        public ushort MachineMode { get; set; }
        /// <summary>
        /// 15个ushort长度,总长度20个字符
        /// </summary>
        public ushort[] Reserved { get; set; } = new ushort[15];
        public ushort[] ToArray()
        {
            List<ushort> result = new List<ushort>();
            result.Add(MachineMode);
            result.AddRange(Reserved);
            return result.ToArray();
        }
    }
}
