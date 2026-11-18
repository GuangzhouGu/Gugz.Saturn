/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2023 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：CIMBOE.Block
// 文件名称    ：DummyGlassRequestReturnBlock
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
    public class DummyGlassRequestReturnBlock : ICIMData
    {
        /// <summary>
        /// 0: No Machine Mode exist
        /// 1: Accepted
        /// 2: Not Accepted(Machine have no this
        ///  PPID)
        /// 3: Not Accepted(Other error)
        /// </summary>
        public ushort DummyGlassRequestReturnCode { get; set; }

        public ushort[] Reserved { get; set; } = new ushort[15];
        public ushort[] ToArray()
        {
            List<ushort> result = new List<ushort>();
            result.Add(DummyGlassRequestReturnCode);
            result.AddRange(Reserved);
            return result.ToArray();
        }
        public void LoadDataFormArrary(ushort[] data)
        {
            DummyGlassRequestReturnCode = data[0];
            Reserved = data.Skip(1).Take(15).ToArray();
        }
    }
}
