/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2020 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnMain.Param
// 文件名称    ：Para.cs
// =================================
// 创 建 者    ：guguangzhou
// 创建日期    ：2022/10/29 09:00:00 
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
using HYC.HTPCBase.Param;
using HYC.SaturnMain.UVWStage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYC.SaturnMain.Params
{
    public class Param
    {
        public static MotionPara Motion { get {return RecipeManager.GetPara(ParaType.Motion) as MotionPara; } set { } }
        public static AlignPara Align { get { return RecipeManager.GetPara(ParaType.Align) as AlignPara; } set { } }
        public static CommPara Comm { get { return RecipeManager.GetPara(ParaType.Comm) as CommPara; } set { } }
        public static PanelPara Panel { get { return RecipeManager.GetPara(ParaType.Panel) as PanelPara; } set { } }
        public static ControlPara Control { get { return RecipeManager.GetPara(ParaType.Control) as ControlPara; } set { } }
        public static CimPara Cim { get { return RecipeManager.GetPara(ParaType.Cim) as CimPara; } set { } }

        public static GlassMapPara GlassCrdInfo { get { return RecipeManager.GetPara(ParaType.GlassMapPara) as GlassMapPara; } set { } }
        public static ThreeDPara ThreeD { get { return RecipeManager.GetPara(ParaType.ThreeD) as ThreeDPara; } set { } }
        public static DetectPara Detect { get { return RecipeManager.GetPara(ParaType.Detect) as DetectPara; } set { } }
        public static UvwPosInfo Uvw { get { return RecipeManager.GetPara(ParaType.Uvw) as UvwPosInfo; } set { } }
    }

    public enum ParaType
    {
        Align,
        Cim,
        Motion,
        Comm,
        Panel,
        Control,
        Detect,
        SerialPort,
        ThreeD,
        GlassMapPara,
        Uvw,
        Cabinet,
        PCBase,
        None,
    }
}
