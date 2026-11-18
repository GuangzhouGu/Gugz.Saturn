/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2020 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnMain.Entities
// 文件名称    ：BasePointHelper.cs
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
using HYC.HTPCBase.Motion;
using HYC.MicroLedATDetect.AutoPos;
using HYC.SaturnMain;
using HYC.SaturnMain.Params;
using HYC.SaturnMain.Devices.ClientPC;
using HYC.SaturnMain.Devices.Motion;
using HYC.SaturnMain.Station;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYC.SaturnMain.Entities
{
    class BasePointHelper
    {

        public static BasePointHelper Instance = null;
        static BasePointHelper()
        {
            Instance = new BasePointHelper();
        }

        /// <summary>
        /// 获取相机在Glass正中心时，相机X轴和检测龙门的基准位置
        /// </summary>
        /// <param name="cameraName">相机名称</param>
        /// <param name="stageIndex">平台编号</param>
        /// <param name="isGantryDir">是否是龙门方向(X方向)，true:龙门方向(X)，false：相机方向(Y)</param>
        /// <returns></returns>
        public static double GetDetectStaBasePt(CameraName cameraName, int stageIndex,bool isGantryDir=true)
        {
            double pos = 0;
            switch (cameraName)
            {
                case CameraName.AoiCam1:
                    {
                        if (isGantryDir)
                        {
                            pos = (stageIndex == 0 ? Param.Motion.ST1_AoiCam1BasePoint_X : Param.Motion.ST2_AoiCam1BasePoint_X) +  Param.Motion.CellCenterOffsetX[stageIndex].CamOffset[0];
                        }
                        else
                        {
                            pos = (stageIndex == 0 ? Param.Motion.ST1_AoiCam1BasePoint_Y : Param.Motion.ST2_AoiCam1BasePoint_Y) + AlignSignalProvider.XValueSum+Param.Motion.CellCenterOffsetY[stageIndex].CamOffset[0];//相机Y方向基准位置需要加上X方向的对位偏移值
                        }
                        break;
                    }
                case CameraName.AoiCam2:
                    {
                        if (isGantryDir)
                        {
                            pos = (stageIndex == 0 ? Param.Motion.ST1_AoiCam2BasePoint_X : Param.Motion.ST2_AoiCam2BasePoint_X) + Param.Motion.CellCenterOffsetX[stageIndex].CamOffset[1];
                        }
                        else
                        {
                            pos = (stageIndex == 0 ? Param.Motion.ST1_AoiCam2BasePoint_Y : Param.Motion.ST2_AoiCam2BasePoint_Y) + AlignSignalProvider.XValueSum + Param.Motion.CellCenterOffsetY[stageIndex].CamOffset[1];
                        }
                       
                        break;
                    }
                case CameraName.MuraCam1:
                    {
                        if (isGantryDir)
                        {
                            pos = (stageIndex == 0 ? Param.Motion.ST1_MuraCam1BasePoint_X : Param.Motion.ST2_MuraCam1BasePoint_X) + Param.Motion.CellCenterOffsetX[stageIndex].CamOffset[0];
                        }
                        else
                        {
                            pos = (stageIndex == 0 ? Param.Motion.ST1_MuraCam1BasePoint_Y : Param.Motion.ST2_MuraCam1BasePoint_Y) + AlignSignalProvider.XValueSum + Param.Motion.CellCenterOffsetY[stageIndex].CamOffset[0];
                        }
                        
                        break;
                    }
                case CameraName.MuraCam2:
                    {
                        if (isGantryDir)
                        {
                            pos = (stageIndex == 0 ? Param.Motion.ST1_MuraCam2BasePoint_X : Param.Motion.ST2_MuraCam2BasePoint_X) + Param.Motion.CellCenterOffsetX[stageIndex].CamOffset[1];
                        }
                        else
                        {
                            pos = (stageIndex == 0 ? Param.Motion.ST1_MuraCam2BasePoint_Y : Param.Motion.ST2_MuraCam2BasePoint_Y) + AlignSignalProvider.XValueSum + Param.Motion.CellCenterOffsetY[stageIndex].CamOffset[1];
                        }
                      
                        break;
                    }
                case CameraName.SquintMuraCam1:
                    {
                        if (isGantryDir)
                        {
                            pos = (stageIndex == 0 ? Param.Motion.ST1_SquintMuraCam1BasePoint_X : Param.Motion.ST2_SquintMuraCam1BasePoint_X) + Param.Motion.CellCenterOffsetX[stageIndex].CamOffset[0];
                        }
                        else
                        {
                            pos = (stageIndex == 0 ? Param.Motion.ST1_SquintMuraCam1BasePoint_Y : Param.Motion.ST2_SquintMuraCam1BasePoint_Y) + AlignSignalProvider.XValueSum + Param.Motion.CellCenterOffsetY[stageIndex].CamOffset[0];
                        }

                        break;
                    }
                case CameraName.SquintMuraCam2:
                    {
                        if (isGantryDir)
                        {
                            pos = (stageIndex == 0 ? Param.Motion.ST1_SquintMuraCam2BasePoint_X : Param.Motion.ST2_SquintMuraCam2BasePoint_X) + Param.Motion.CellCenterOffsetX[stageIndex].CamOffset[1];
                        }
                        else
                        {
                            pos = (stageIndex == 0 ? Param.Motion.ST1_SquintMuraCam2BasePoint_Y : Param.Motion.ST2_SquintMuraCam2BasePoint_Y) + AlignSignalProvider.XValueSum + Param.Motion.CellCenterOffsetY[stageIndex].CamOffset[1];
                        }

                        break;
                    }
                case CameraName.MultiAngle1:
                    {
                        if (isGantryDir)
                        {
                            pos = stageIndex == 0 ? Param.Motion.ST1_MultiAngle1BasePoint_X : Param.Motion.ST2_MultiAngle1BasePoint_X;
                        }
                        else
                        {
                            pos = (stageIndex == 0 ? Param.Motion.ST1_MultiAngle1BasePoint_Y : Param.Motion.ST2_MultiAngle1BasePoint_Y) + AlignSignalProvider.XValueSum + Param.Motion.CellCenterOffsetY[stageIndex].CamOffset[0];
                        }
                       
                        break;
                    }
                case CameraName.MultiAngle2:
                    {
                        if (isGantryDir)
                        {
                            pos = stageIndex == 0 ? Param.Motion.ST1_MultiAngle2BasePoint_X : Param.Motion.ST2_MultiAngle2BasePoint_X;
                        }
                        else
                        {
                            pos = (stageIndex == 0 ? Param.Motion.ST1_MultiAngle2BasePoint_Y : Param.Motion.ST2_MultiAngle2BasePoint_Y) + AlignSignalProvider.XValueSum + Param.Motion.CellCenterOffsetY[stageIndex].CamOffset[1];
                        }
                       
                        break;
                    }
            }
            return pos;
        }
        /// <summary>
        /// 获取龙门在每列的压接位置
        /// </summary>
        /// <param name="name"></param>
        /// <param name="stageIndex"></param>
        /// <param name="columnIndex"></param>
        /// <returns></returns>
        public static double GetGantryDetectBasePoint(GantryName name, int stageIndex, int columnIndex = 0)
        {
            double pos = 0;
            switch (name)
            {
                case GantryName.Probe:
                    {
                        if (stageIndex == 0)
                        {
                            pos = AxisInfoHelper.GetPosInfoByName(AxisName.ProbeGantry_Y1, ProbeGantryPosName.Stg1ProbeGantryAlignPos).Pos;
                        }
                        else
                        {
                            pos = AxisInfoHelper.GetPosInfoByName(AxisName.ProbeGantry_Y1, ProbeGantryPosName.Stg2ProbeGantryAlignPos).Pos;
                        }
                        pos += Param.Panel.CellDistanceX * (Param.Panel.CellCountX-1 - columnIndex) + Param.Panel.MarkPadDis;
                        break;
                    }
                case GantryName.Detect:
                    {
                        if (stageIndex == 0)
                        {
                            pos = AxisInfoHelper.GetPosInfoByName(AxisName.DetectGantry_Y1, DetectGtyPosName.Stg1GantryDetectPos).Pos;
                        }
                        else
                        {
                            pos = AxisInfoHelper.GetPosInfoByName(AxisName.DetectGantry_Y1, DetectGtyPosName.Stg2GantryDetectPos).Pos;
                        }
                        pos -= Param.Panel.CellDistanceX * (Param.Panel.CellCountX - 1 - columnIndex);
                        break;
                    }
            }
            return pos;
        }
        public static double GetContactHeadPos(int stageIndex, int contactHeadIndex)
        {
            double pos = 0;
            if (stageIndex == 0)
            {
                pos = AxisInfoHelper.GetPosInfoByName(AxisName.ProbeGantryAlig_X1, ProbeGantryPosName.Stg1ContactCamMonitorStartPos).Pos + AlignSignalProvider.XValueSum;
            }
            else
            {
                pos = AxisInfoHelper.GetPosInfoByName(AxisName.ProbeGantryAlig_X1, ProbeGantryPosName.Stg2ContactCamMonitorStartPos).Pos + AlignSignalProvider.XValueSum;
            }
            string[] sortStr = Param.Panel.PanelSort.Split('X');
            int[] sort = sortStr.Select(p => int.Parse(p)).ToArray();
            switch (sort.Length)
            {
                case 2:
                    {
                       // contactHeadIndex = Param.Panel.CellCountY - contactHeadIndex - 1;
                        if (contactHeadIndex < sort[0])
                        {
                            pos += Param.Panel.CellDistanceY * contactHeadIndex;
                        }
                        else if (contactHeadIndex >= sort[0])
                        {
                            pos += Param.Panel.CellDistanceY * contactHeadIndex + Param.Panel.CellIntervalY2 - Param.Panel.CellIntervalY;
                        }
                        break;
                    }
                case 3:
                    {
                       //contactHeadIndex = Param.Panel.CellCountY - contactHeadIndex - 1;
                        if (contactHeadIndex < sort[0])
                        {
                            pos += Param.Panel.CellDistanceY * contactHeadIndex;
                        }
                        else if (contactHeadIndex >= sort[0] && contactHeadIndex < sort[0] + sort[1])
                        {
                            if (Param.Panel.CellIntervalY2 != Param.Panel.CellIntervalY)
                            {
                                pos += Param.Panel.CellDistanceY * contactHeadIndex + Param.Panel.CellIntervalY2 - Param.Panel.CellIntervalY;
                            }
                            else
                            {
                                pos += Param.Panel.CellDistanceY * contactHeadIndex;
                            }
                        }
                        else
                        {
                            if (Param.Panel.CellIntervalY2 != Param.Panel.CellIntervalY)
                            {
                                pos += Param.Panel.CellDistanceY * contactHeadIndex + (Param.Panel.CellIntervalY2 - Param.Panel.CellIntervalY) * 2;
                            }
                            else
                            {
                                pos += Param.Panel.CellDistanceY * contactHeadIndex;
                            }
                        }
                        break;
                    }
                case 4:
                    {

                        break;
                    }

            }

            return pos;
        }

    }

    enum GantryName
    {
        Probe,
        Detect,
    }
}
