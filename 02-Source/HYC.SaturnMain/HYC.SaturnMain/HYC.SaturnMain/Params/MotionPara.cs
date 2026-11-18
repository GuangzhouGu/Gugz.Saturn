/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2020 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnMain.Params
// 文件名称    ：MotionParamaters.cs
// =================================
// 创 建 者    ：guguangzhou
// 创建日期    ：2021/01/29 09:00:00 
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
using HYC.HTPCBase.Extension.Entities;
using HYC.HTPCBase.Extension.Para;
using HYC.HTPCBase.Extension.Views;
using HYC.SaturnMain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;

namespace HYC.SaturnMain.Params
{
    /// <summary>
    /// 运动参数
    /// </summary>
    public class MotionPara : ParaBase, IPara
    {
        [System.Xml.Serialization.XmlIgnore]
        [Browsable(false)]
        public bool IsShow { get; set; } = true;
        [System.Xml.Serialization.XmlIgnore]
        [Browsable(false)]
        public ParaViewBase ParaViewBase { get; set; }

        [System.Xml.Serialization.XmlIgnore]
        [Browsable(false)]
        public string ParaName { get; set; } = "运动参数";
        #region 超时时间设置

        [Category("超时时间设置"),Description("Uvw平台Z轴运动超时时间，单位秒")]
        public double UvwAxisZTimeout { get; set; }
        [Category("超时时间设置"),  Description("Uvw平台运动超时时间，单位秒")]
        public double UvwTimeout { get; set; }

        [Category("超时时间设置"),Description("Contact相机打开超时时间，单位秒")]
        public double CtcCamOpenTimeout { get; set; }

        [Category("超时时间设置"),  Description("料仓等待自动更换Probe完成超时时间，单位秒")]
        public double CabWaitAutoChgTimeout { get; set; }
        [Category("超时时间设置"), Description("Probe自动更换机构等待料仓到达指定位置超时时间，单位秒")]
        public double AutoChgWaitCabTimeout { get; set; }

        [Category("超时时间设置"), Description("等待料仓Z轴回零完成超时时间，单位秒")]
        public double WaitCabinetZHomeComTimeout { get; set; }
      
      
        #endregion

        #region 延时时间设置
        [Category("延时时间设置"),  Description("平台规整时底部吹气时间，单位秒")]
        public double StageBlowTime { get; set; }
        [Category("延时时间设置"),  Description("平台规整气缸规整时间，单位秒")]
        public double StageAlignTime { get; set; }
        [Category("延时时间设置"), Description("Probe龙门和检测龙门同时移动间隔时间，单位秒")]
        public double GantryIntervalTime { get; set; }

        [Category("延时时间设置"), Description("零点系统电磁阀吹气延时时间，单位秒")]
        public int PrbZeroSysTime { get; set; }
        [Category("延时时间设置"), Description("料仓检测Probe超时时间，单位秒")]
        public double CabDetPrbTimeout { get; set; }
        [Category("延时时间设置"), Description("Probe自动更换机构检测Probe超时时间，单位秒")]
        public double PrbAutoChgDetPrbTimeout { get; set; }
        [Category("延时时间设置"), Description("Uvw检测Probe超时时间，单位秒")]
        public double UvwDetPrbTimeout { get; set; }
        [Category("延时时间设置"), Description("Probe上料平台测Probe超时时间，单位秒")]
        public double PrbLoaderDetPrbTimeout { get; set; }
        [Category("延时时间设置"), Description("Probe缓存平台检测Probe超时时间，单位秒")]
        public double PrbBufferDetPrbTimeout { get; set; }
        #endregion

        #region 防撞位置设置
        [Category("防撞位置设置"),  Description("Probe龙门实际位置+检测龙门实际位置之和必须小于该位置，否则会发生碰撞，单位毫米")]
        public double GantryPreventCrushPos { get; set; }
        [Category("防撞位置设置"), Description("当检测龙门和Probe龙门实际距离小于安全位置时，IR相机和对位相机有相撞风险，单位毫米")]
        public double GantryPreventCrushPos2 { get; set; }
        [Category("防撞位置设置"),  Description("AlignCam1X轴实际位置+AlignCam2X轴实际位置之和必须小于该位置，否则会发生碰撞，单位毫米")]
        public double AlignCamXPreventCrushPos { get; set; }
        [Category("防撞位置设置"), Description("DetectModule1X轴实际位置+DetectModule2X轴实际位置之和必须小于该位置，否则会发生碰撞，单位毫米")]
        public double DetectModuleXPreventCrushPos { get; set; }
        [Category("防撞位置设置"), Description("Stage1Y轴实际位置+Stage2Y轴际位置之和必须小于该位置，否则会发生碰撞，单位毫米")]
        public double Stage1YStage2YPreventCrushPos { get; set; }
    
        [Category("防撞位置设置"), Description("当检测龙门和Probe龙门实际距离小于安全位置时，IRCamX轴实际位置-Align1CamX轴实际位置必须大于该设定值，不然会相撞，单位毫米")]
        public double AlignCam1AndIRCamPreventCrushPos { get; set; }
        [Category("防撞位置设置"),  Description("当检测龙门和Probe龙门实际距离小于安全位置时，IRCamX轴实际位置+Align2CamX轴实际位置必须小于该设定值，不然会相撞，单位毫米")]
        public double AlignCam2AndIRCamPreventCrushPos { get; set; }
        [Category("防撞位置设置"),  Description("Stage1T轴和Stage2T轴旋转时，Stage1Y轴和Stage2Y轴之和必须小于该值，否则旋转时会发生碰撞，单位毫米")]
        public double Stage1TStage2TPreventCrushPos { get; set; }
        [Category("防撞位置设置"), Description("水平规整气缸规整时，平台1和平台2位置之和必须小于该值")]
        public double AlignCylinderAlignPos { get; set; }
        [Category("防撞位置设置"), Description("自动更换Y轴安全位置，当Y轴实际位置小于此值时，料仓不能移动")]
        public double AutoChgAxisYSafePos { get; set; }
        [Category("防撞位置设置"), Description("Probe上料平台Y轴安全位置，当Y轴实际位置大于此值时，料仓不能移动")]
        public double ProbeLoaderAxisYSafePos { get; set; }
        [Category("防撞位置设置"), Description("自动更换Y轴安全位置2，当Y轴实际位置大于此值时，平台1T轴不能旋转")]
        public double AutoChgAxisYSafePos2 { get; set; }
        [Category("防撞位置设置"), Description("检测模组1X轴安全位置，当模组Z轴不在安全位置，并且检测模组位置大于此值，，两组龙门禁止移动到同一列，否则检测相机会和对位相机相撞")]
        public double DetectModule1XSafePos { get; set; } = 300;
        [Category("防撞位置设置"), Description("检测模组2X轴安全位置，当模组Z轴不在安全位置，并且检测模组位置大于此值，，两组龙门禁止移动到同一列，否则检测相机会和对位相机相撞")]
        public double DetectModule2XSafePos { get; set; } = 300;
        private double loaderZPreventCrushPos= 300;
         [Category("防撞位置设置"), Description("料仓上料Z轴避让距离，必须大于0，单位毫米")]
        public double LoaderZPreventCrushPos
        {
            get { return loaderZPreventCrushPos; }
            set
            {
                if (value < 0)
                {
                    MessageBox.Show("LoaderZPreventCrushPos只能大于等于0");
                }
                else
                {
                    loaderZPreventCrushPos = value;
                }
            }
        }
        #endregion

        #region 基准点设置
     
        [Category("龙门方向基准点设置"), Description("平台1Aoi相机1X方向基准点，Aoi相机1中心对准平台1Glass右下角Cell的Mark时，检测龙门轴的位置，单位毫米")]
        public double ST1_AoiCam1BasePoint_X { get; set; }
        [Category("龙门方向基准点设置"), Description("平台1Aoi相机2X方向基准点，Aoi相机1中心对准平台1Glass右下角Cell的Mark时，检测龙门轴的位置，单位毫米")]
        public double ST1_AoiCam2BasePoint_X { get; set; }
        [Category("龙门方向基准点设置"), Description("平台1直视Mura相机1X方向基准点，直视Mura相机1中心对准平台1Glass右下角Cell的Mark时，检测龙门轴的位置，单位毫米")]
        public double ST1_MuraCam1BasePoint_X { get; set; }
        [Category("龙门方向基准点设置"), Description("平台1直视Mura相机2X方向基准点，直视Mura相机2中心对准平台1Glass右下角Cell的Mark时，检测龙门轴的位置，单位毫米")]
        public double ST1_MuraCam2BasePoint_X { get; set; }
        [Category("龙门方向基准点设置"), Description("平台1斜视Mura相机1X方向基准点，斜视Mura相机1中心对准平台1Glass右下角Cell的Mark时，检测龙门轴的位置，单位毫米")]
        public double ST1_SquintMuraCam1BasePoint_X { get; set; }
        [Category("龙门方向基准点设置"), Description("平台1斜视Mura相机2X方向基准点，斜视Mura相机2中心对准平台1Glass右下角Cell的Mark时，检测龙门轴的位置，单位毫米")]
        public double ST1_SquintMuraCam2BasePoint_X { get; set; }
        [Category("龙门方向基准点设置"), Description("平台1MultiAngle1X方向基准点，MultiAngle1中心对准平台1Glass右下角Cell的Mark时，检测龙门轴的位置，单位毫米")]
        public double ST1_MultiAngle1BasePoint_X { get; set; }
        [Category("龙门方向基准点设置"), Description("平台1MultiAngle2X方向基准点，MultiAngle2中心对准平台1Glass右下角Cell的Mark时，检测龙门轴的位置，单位毫米")]
        public double ST1_MultiAngle2BasePoint_X { get; set; }


        [Category("龙门方向基准点设置"), Description("平台2Aoi相机1X方向基准点，Aoi相机1中心对准平台2Glass右下角Cell的Mark时，检测龙门轴的位置，单位毫米")]
        public double ST2_AoiCam1BasePoint_X { get; set; }
        [Category("龙门方向基准点设置"), Description("平台2Aoi相机2X方向基准点，Aoi相机1中心对准平台2Glass右下角Cell的Mark时，检测龙门轴的位置，单位毫米")]
        public double ST2_AoiCam2BasePoint_X { get; set; }
        [Category("龙门方向基准点设置"), Description("平台2直视Mura相机1X方向基准点，直视Mura相机1中心对准平台2Glass右下角Cell的Mark时，检测龙门轴的位置，单位毫米")]
        public double ST2_MuraCam1BasePoint_X { get; set; }
        [Category("龙门方向基准点设置"), Description("平台2直视Mura相机2X方向基准点，直视Mura相机2中心对准平台2Glass右下角Cell的Mark时，检测龙门轴的位置，单位毫米")]
        public double ST2_MuraCam2BasePoint_X { get; set; }
        [Category("龙门方向基准点设置"), Description("平台2斜视Mura相机1X方向基准点，斜视Mura相机1中心对准平台2Glass右下角Cell的Mark时，检测龙门轴的位置，单位毫米")]
        public double ST2_SquintMuraCam1BasePoint_X { get; set; }
        [Category("龙门方向基准点设置"), Description("平台2斜视Mura相机2X方向基准点，斜视Mura相机2中心对准平台2Glass右下角Cell的Mark时，检测龙门轴的位置，单位毫米")]
        public double ST2_SquintMuraCam2BasePoint_X { get; set; }
        [Category("龙门方向基准点设置"), Description("平台2MultiAngle1X方向基准点，MultiAngle1中心对准平台2Glass右下角Cell的Mark时，检测龙门轴的位置，单位毫米")]
        public double ST2_MultiAngle1BasePoint_X { get; set; }
        [Category("龙门方向基准点设置"), Description("平台2MultiAngle2X方向基准点，MultiAngle2中心对准平台2Glass右下角Cell的Mark时，检测龙门轴的位置，单位毫米")]
        public double ST2_MultiAngle2BasePoint_X { get; set; }


        [Category("相机方向基准点设置"), Description("平台1Aoi相机1Y方向基准点，Aoi相机1中心对准平台1Glass右下角Cell的Mark时，DetectModule1_X轴的位置，单位毫米")]
        public double ST1_AoiCam1BasePoint_Y { get; set; }
        [Category("相机方向基准点设置"), Description("平台1Aoi相机2Y方向基准点，Aoi相机1中心对准平台1Glass右下角Cell的Mark时，DetectModule1_X轴的位置，单位毫米")]
        public double ST1_AoiCam2BasePoint_Y { get; set; }
        [Category("相机方向基准点设置"), Description("平台1直视Mura相机1Y方向基准点，直视Mura相机1中心对准平台1Glass右下角Cell的Mark时，DetectModule1_X轴的位置，单位毫米")]
        public double ST1_MuraCam1BasePoint_Y { get; set; }
        [Category("相机方向基准点设置"), Description("平台1直视Mura相机2Y方向基准点，直视Mura相机2中心对准平台1Glass右下角Cell的Mark时，DetectModule2_X轴的位置，单位毫米")]
        public double ST1_MuraCam2BasePoint_Y { get; set; }
        [Category("相机方向基准点设置"), Description("平台1斜视Mura相机1Y方向基准点，斜视Mura相机1中心对准平台1Glass右下角Cell的Mark时，DetectModule1_X轴的位置，单位毫米")]
        public double ST1_SquintMuraCam1BasePoint_Y { get; set; }
        [Category("相机方向基准点设置"), Description("平台1斜视Mura相机2Y方向基准点，斜视Mura相机2中心对准平台1Glass右下角Cell的Mark时，DetectModule2_X轴的位置，单位毫米")]
        public double ST1_SquintMuraCam2BasePoint_Y { get; set; }
        [Category("相机方向基准点设置"), Description("平台1MultiAngle1Y方向基准点，MultiAngle1中心对准平台1Glass右下角Cell的Mark时，DetectModule2_X轴的位置，单位毫米")]
        public double ST1_MultiAngle1BasePoint_Y { get; set; }
        [Category("相机方向基准点设置"), Description("平台1MultiAngle2Y方向基准点，MultiAngle2中心对准平台1Glass右下角Cell的Mark时，DetectModule2_X轴的位置，单位毫米")]
        public double ST1_MultiAngle2BasePoint_Y { get; set; }


        [Category("相机方向基准点设置"), Description("平台2Aoi相机1Y方向基准点，Aoi相机1中心对准平台2Glass右下角Cell的Mark时，DetectModule1_X轴的位置，单位毫米")]
        public double ST2_AoiCam1BasePoint_Y { get; set; }
        [Category("相机方向基准点设置"), Description("平台2Aoi相机2Y方向基准点，Aoi相机1中心对准平台2Glass右下角Cell的Mark时，DetectModule1_X轴的位置，单位毫米")]
        public double ST2_AoiCam2BasePoint_Y { get; set; }
        [Category("相机方向基准点设置"), Description("平台2直视Mura相机1Y方向基准点，直视Mura相机1中心对准平台2Glass右下角Cell的Mark时，DetectModule1_X轴的位置，单位毫米")]
        public double ST2_MuraCam1BasePoint_Y { get; set; }
        [Category("相机方向基准点设置"), Description("平台2直视Mura相机2Y方向基准点，直视Mura相机2中心对准平台2Glass右下角Cell的Mark时，DetectModule2_X轴的位置，单位毫米")]
        public double ST2_MuraCam2BasePoint_Y { get; set; }
        [Category("相机方向基准点设置"), Description("平台2斜视Mura相机1Y方向基准点，斜视Mura相机1中心对准平台2Glass右下角Cell的Mark时，DetectModule1_X轴的位置，单位毫米")]
        public double ST2_SquintMuraCam1BasePoint_Y { get; set; }
        [Category("相机方向基准点设置"), Description("平台2斜视Mura相机2Y方向基准点，斜视Mura相机2中心对准平台2Glass右下角Cell的Mark时，DetectModule2_X轴的位置，单位毫米")]
        public double ST2_SquintMuraCam2BasePoint_Y { get; set; }
        [Category("相机方向基准点设置"), Description("平台2MultiAngle1Y方向基准点，MultiAngle1中心对准平台2Glass右下角Cell的Mark时，DetectModule2_X轴的位置，单位毫米")]
        public double ST2_MultiAngle1BasePoint_Y { get; set; }
        [Category("相机方向基准点设置"), Description("平台2MultiAngle2Y方向基准点，MultiAngle2中心对准平台2Glass右下角Cell的Mark时，DetectModule2_X轴的位置，单位毫米")]
        public double ST2_MultiAngle2BasePoint_Y { get; set; }
        #endregion
        private int contactCamCheckCellNum=5;

        [Category("Contact相机监测Cell序号"),Description("自动压接时Contact相机监测的Cell序号")]
        public int ContactCamCheckCellNum
        { 
            get{ return contactCamCheckCellNum; }
            set
            {
                if (value < 0||value>21)
                {
                    MessageBox.Show("ContactCamCheckCellNum必须大于等于0并且小于22");
                }
                else
                {
                    contactCamCheckCellNum = value;
                }
            }
        }


        private double detectGantryOffset=200;
        [Category("偏移值"), Description("Contact相机区压头上方拍照时，检测龙门避让偏移值，单位毫米")]
        public double DetectGantryOffset
        {
            get { return detectGantryOffset; }
            set
            {
                if (value < 0)
                {
                    MessageBox.Show("该值只能大于等于0");
                }
                else
                {
                    detectGantryOffset = value;
                }
            }
        }

        [Category("偏移值"), Description("相机移动到Cell中心位置时龙门方向(X方向)的偏移值，单位毫米")]
        [TypeConverter(typeof(CollectionCountConvert))]
        [Editor(typeof(ComplexCollectionEditor<CamDetectOffset>), typeof(UITypeEditor))]
        public List<CamDetectOffset> CellCenterOffsetX { get; set; } = new List<CamDetectOffset>();
        /// <summary>
        /// 相机移动到Cell中心位置时相机方向(Y方向)的偏移值
        /// </summary>
        [Category("偏移值"), Description("相机移动到Cell中心位置时相机方向(Y方向)的偏移值，单位毫米")]
        [TypeConverter(typeof(CollectionCountConvert))]
        [Editor(typeof(ComplexCollectionEditor<CamDetectOffset>), typeof(UITypeEditor))]
        public List<CamDetectOffset> CellCenterOffsetY { get; set; } = new List<CamDetectOffset>();

        [Category("反向间隙补偿"), Description("平台1T轴反向间隙补偿值")]
        public double Stage1TBacklashValue { get; set; }
        [Category("反向间隙补偿"), Description("平台2T轴反向间隙补偿值")]
        public double Stage2TBacklashValue { get; set; }
        [Category("反向间隙补偿"), Description("平台1T轴反向间隙补偿值")]
        public BacklashDir Stage1TBacklashDir { get; set; }
        [Category("反向间隙补偿"), Description("平台2T轴反向间隙补偿值")]
        public BacklashDir Stage2TBacklashDir { get; set; }
        [Category("反向间隙补偿"), Description("是否启用反向间隙补偿")]
        public bool IsEnableBacklash { get; set; }
        [Category("二维误差补偿"), Description("是否启用二维误差补偿")]
        public bool Enable2DCompensation { get; set; }

        public IPara LoadPara()
        {
            return Load(this);
        }
        public bool SavePara()
        {
            return Save(this);
        }
    }
    public enum BacklashDir
    {
        Postive,
        Negtive
    }

    public class CamDetectOffset
    {
        /// <summary>
        /// 检测模组移动到Cell正上方时，相机方向偏移值，0：一号检测模组，1：二号检测模组
        /// </summary>
        [Category("相机偏移"), Description("检测模组移动到Cell正上方时，相机方向偏移值")]
        public double[] CamOffset { get; set; }
   
    }
}
