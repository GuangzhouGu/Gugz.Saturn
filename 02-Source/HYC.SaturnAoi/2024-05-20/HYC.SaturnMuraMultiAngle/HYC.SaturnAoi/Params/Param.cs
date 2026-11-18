/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2023 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnAoi.Params
// 文件名称    ：Param
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
using HYC.HTPCBase.Extension.Recipe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYC.SaturnAoi.Params
{
    class Param
    {
        public static CommPara Comm { get { return RecipeManager.GetPara(ParaType.Comm) as CommPara; } set { } }
        public static SystemPara System { get { return RecipeManager.GetPara(ParaType.System) as SystemPara; } set { } }
        public static MultiAngleDetectPattern MultiAngleDetectPattern { get { return RecipeManager.GetPara(ParaType.Pattern) as MultiAngleDetectPattern; } set { } }
    }
    public enum ParaType
    {
        Align,
        Cim,
        Motion,
        Comm,
        Panel,
        System,
        Detect,
        SerialPort,
        ThreeD,
        Glass,
        Uvw,
        Cabinet,
        PCBase,
        Pattern,
        None,
    }
}
