/*----------------------------------------------------------------
// Copyright (C) Suzhou HYC Electronic Technology Co.,LTD
// 版权所有。
//
// =================================
// CLR版本     ：4.0.30319.42000
// 命名空间    ：HYC.AutoPosition.Extentions
// 文件名称    ：XytMoveExecutor.cs
// =================================
// 创 建 者    ：yangkang
// 创建日期    ：2020/1/6 17:57:32 
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
using HYC.AutoPosition.Interface;
using HYC.HTXYT.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYC.AutoPosition.Extentions
{
    /// <summary>
    /// 使用XYT对位平台的运动执行者
    /// </summary>
    public class XytMoveExecutor : IMoveExecutor
    {
        //XYT平台
        private XYTPlatform platform = null;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="platform">XYT平台类</param>
        public XytMoveExecutor(XYTPlatform platform)
        {
            this.platform = platform;
        }

        /// <summary>
        /// 移动
        /// </summary>
        /// <param name="groupIndex">对位组序号</param>
        /// <param name="xyt">移动值</param>
        /// <param name="reserve">预留</param>
        /// <returns>移动是否完成</returns>
        public bool Move(int groupIndex, double[] xyt, int[] reserve)
        {
            PlatMovePara para = new PlatMovePara();
            para.Distance = xyt.Select(p => (float)p).ToArray();
            return platform.MoveRel(para);
        }
    }
}
