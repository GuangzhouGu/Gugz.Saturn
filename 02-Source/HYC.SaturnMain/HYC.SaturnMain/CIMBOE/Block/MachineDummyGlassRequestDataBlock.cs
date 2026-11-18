/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2023 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：CIMBOE.Block
// 文件名称    ：MachineDummyGlassRequestDataBlock
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
    public class MachineDummyGlassRequestDataBlock : ICIMData
    {
        public ushort DummyGlassCount { get; set; }
        /// <summary>
        /// 15个ushort长度,总长度20个字符
        /// </summary>
        public ushort UnitNo { get; set; }
        public ushort SlotNo { get; set; }
        /// <summary>
        /// 1: Dummy Type
        /// 2: Dummy Glass PPID
        /// </summary>
        public ushort DummyRequestOption { get; set; }
        /// <summary>
        /// 1: Normal
        /// 2: Bare Dummy
        /// 3:Tooling Dummy
        /// 4:Offset Dummy
        /// 5:PI Dummy
        /// 6:Key Dummy
        /// 7:TFE CVD Dummy
        /// 8:TFE Inkjet Dummy
        /// 9~ : Reserved
        /// </summary>
        public ushort DummyType { get; set; }
        public ushort DummyGlassPPID { get; set; }
        public ushort[] Reserved { get; set; } = new ushort[10];
        public ushort[] ToArray()
        {
            List<ushort> result = new List<ushort>();
            result.Add(DummyGlassCount);
            result.Add(UnitNo);
            result.Add(SlotNo);
            result.Add(DummyRequestOption);
            result.Add(DummyType);
            result.Add(DummyGlassPPID);
            result.AddRange(Reserved);
            return result.ToArray();
        }
    }
}
