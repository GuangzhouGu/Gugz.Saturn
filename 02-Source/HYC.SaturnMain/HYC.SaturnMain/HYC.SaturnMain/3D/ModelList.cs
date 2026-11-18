/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2020 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.0.30319.42000
// 命名空间    ：DirectXFormHost
// 文件名称    ：ModelList.cs
// =================================
// 创 建 者    ：yangkang
// 创建日期    ：2020/10/21 21:45:12 
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

namespace DirectXFormHost
{
    [Serializable]
    public class ModelList
    {
        public List<ModelNode> Nodes { get; set; } = new List<ModelNode>();
    }

    [Serializable]
    public class ModelNode
    {
        public ModelInfo ModelInfo { get; set; }
        public List<ModelNode> Children { get; set; } = new List<ModelNode>();
    }
}
