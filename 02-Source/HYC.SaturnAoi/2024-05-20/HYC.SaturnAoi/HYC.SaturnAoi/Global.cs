/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2023 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnAoi
// 文件名称    ：Global
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
using HYC.SaturnAoi.DetectFlow;
using HYC.SaturnAoi.Entities;
using HYC.StandardAoi.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYC.SaturnAoi
{
    class Global
    {
        public static SignalProvider SignalProvider = new SignalProvider();
        public static List<IImageProducer> ImageProducers = new List<IImageProducer>();
        //public static List<IPowerController> PowerControllers = new List<IPowerController>();
        public static List<IPatternProducer> PatternProducers = new List<IPatternProducer>();
    }
    public enum DetectType
    {
        Aoi,
        Mura,
        MultiAngle
    }
}
