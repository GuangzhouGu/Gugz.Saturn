/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2020 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnMain.Station
// 文件名称    ：ProbeGantry.cs
// =================================
// 创 建 者    ：guguangzhou
// 创建日期    ：2021/01/29 09:00:00 
// 功能描述    ：对位参数
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
using HYC.HTCard;
using HYC.HTLog;
using HYC.HTPCBase.Extension.Recipe;
using HYC.HTPCBase.Motion;
using HYC.MicroLedATDetect.AutoPos;
using HYC.SaturnMain;
using HYC.SaturnMain.Entities;
using HYC.SaturnMain.Params;
using HYC.SaturnMain.Devices.ClientPC;
using HYC.SaturnMain.Devices.Motion;

using HYC.SaturnMain.UVWStage;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HYC.SaturnMain.Station
{
    /// <summary>
    /// ProbeGantry分布类
    /// </summary>
    public partial class ProbeGantry
    {
        /// <summary>
        /// 压接结果标志位，0：成功，1：失败
        /// </summary>
        public static int ContactResult = -1;
        /// <summary>
        /// 停止压接结果标志位，0：成功，1：失败
        /// </summary>
        public static int StopContactResult = -1;
        /// <summary>
        /// Uvw对位结果标志位，0：成功，1：失败
        /// </summary>
        public static int UvwAutoPosResult = -1;
        /// <summary>
        /// Uvw运动结果标志位，0：成功，1：失败
        /// </summary>
        public static int IsUvwMoveDone = -1;
        /// <summary>
        /// 平台压接轴压接
        /// </summary>
        /// <param name="stageIndex">平台号</param>
        /// <returns>压接完成返回true，超时返回fasle</returns>
        public async Task<bool> StageContactSync(int stageIndex)
        {
            ContactResult = -1;
            StopContactResult = -1;
            if (Param.Control.RunMode==RunMode.NoDetect)
            {
                ContactResult = 0;
                return true;
            }
            double pos = stageIndex == 0 ?Param.Uvw.AxisZContactPosStage1 : Param.Uvw.AxisZContactPosStage2;
            AxisName axisName = stageIndex == 0 ? AxisName.Stage1_ContactZ : AxisName.Stage2_ContactZ;
            Motion.MoveAbs(StationEnum.DualStage, axisName, pos);
            return await Task.Run(() =>
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                while (true)
                {
                    if (sw.ElapsedMilliseconds > 10 * 1000)
                    {
                        ContactResult = 1;
                        return false;
                    }
                    else if (Motion.IsArrive(StationEnum.DualStage, axisName, pos))
                    {
                        ContactResult = 0;
                        ProbeCardHelper.UpdateContactCount(RecipeManager.CurRecipe.RecipeID);
                        return true;
                    }
                    Thread.Sleep(50);
                }
            });
        }

        /// <summary>
        /// 停止压接
        /// </summary>
        /// <param name="isAll">2个平台都停止压接</param>
        /// <param name="stageIndex">平台号</param>
        /// <returns>停止压接完成返回true，超时返回fasle</returns>
        public async Task<bool> StageStopContactSync(bool isAll,int stageIndex=0)
        {
            StopContactResult = -1;
            ContactResult = -1;
            AxisName axisName = stageIndex == 0 ? AxisName.Stage1_ContactZ : AxisName.Stage2_ContactZ;
            if (isAll)
            {
                Motion.MoveAbs(StationEnum.DualStage, AxisName.Stage1_ContactZ, StagePosName.SafePos);
                Motion.MoveAbs(StationEnum.DualStage, AxisName.Stage2_ContactZ, StagePosName.SafePos);
            }
           else
            {
                Motion.MoveAbs(StationEnum.DualStage, axisName, StagePosName.SafePos);
            }
            return await Task.Run(() =>
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                while (true)
                {
                    if (sw.ElapsedMilliseconds > 10 * 1000)
                    {
                        StopContactResult = 1;
                        return false;
                    }
                    else if (isAll)
                    {
                        if (Motion.IsArrive(StationEnum.DualStage, AxisName.Stage1_ContactZ, StagePosName.SafePos)&&
                        Motion.IsArrive(StationEnum.DualStage, AxisName.Stage2_ContactZ, StagePosName.SafePos))
                        {
                            StopContactResult = 0;
                            return true;
                        }
                    }
                    else if (Motion.IsArrive(StationEnum.DualStage, axisName, StagePosName.SafePos))
                    {
                        StopContactResult = 0;
                        return true;
                    }
                    Thread.Sleep(10);
                }
            });

        }
        /// <summary>
        /// 自动对位
        /// </summary>
        /// <param name="pos">对位值</param>
        /// <returns></returns>
        public void UvwAutoPos(double pos)
        {
            //Brake(false);
            Motion.MoveRel(StationEnum.ProbeGantry, AxisName.UvwStage_Y, Param.Align.IsInverseProbeDir?-pos: pos);
            LogManager.GetLogger("Uvw").Info($"ProbeGantry->Uvw_Y轴移动{pos}毫米");
            Thread.Sleep(100);
        }
        public bool IsAutoPosDone()
        {
            return Motion.IsArrive(StationEnum.ProbeGantry, AxisName.UvwStage_Y);
        }
        private async Task<bool> StageContactZToAlignPos(int stageIndex)
        {
            AxisName axisName = stageIndex == 0 ? AxisName.Stage1_ContactZ : AxisName.Stage2_ContactZ;
            Motion.MoveAbs(StationEnum.DualStage, axisName, StagePosName.AlignPos);
            return await Task.Run(() =>
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                while (true)
                {
                    if (sw.ElapsedMilliseconds > 10 * 1000)
                    {
                        return false;
                    }
                    else if (Motion.IsArrive(StationEnum.DualStage, axisName, StagePosName.AlignPos))
                    {
                        return true;
                    }
                    Thread.Sleep(10);
                }
            });
        }
        public async Task<bool> UvwOffsetSync(int glassNum,int columnNum,double waitTime = 20)
        {
            UvwAutoPosResult = -1;
            //double pos = 0;
            try
            {
                if (IsContactZAtSafePos()==false)
                {
                    LogManager.GetLogger("Uvw").Info($"ProbeGantry->压接轴不在安全位置，禁止移动Uvw");
                    UvwAutoPosResult = 1;
                    return false;
                }
                double offSet = Param.Align.GlassColumnOffsets[glassNum].ColumnOffsets[columnNum];
                if (Math.Abs(offSet)>0.1)
                {
                    LogManager.GetLogger("Uvw").Info($"ProbeGantry-Uvw单列补偿值过大，补偿值：{offSet}毫米");
                    //OnShowMsg?.Invoke("AutoRun", $"Uvw单列补偿值过大，补偿值：{offSet}毫米", Brushes.Red);
                    UvwAutoPosResult = 1;
                    return false;
                }
                //double yPos = glassNum == 0 ?Param.Uvw.AxisYPosStage1 :Param.Uvw.AxisYPosStage2;
                //pos = yPos + offSet + AlignSignalProvider.XYTValue[0];
                //Brake(false);
                Motion.MoveRel(StationEnum.ProbeGantry, AxisName.UvwStage_Y, offSet);
                Thread.Sleep(100);
                return await Task.Run(() =>
                {
                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    while (true)
                    {
                        if (sw.ElapsedMilliseconds > waitTime * 1000)
                        {
                            //Brake(true);
                            UvwAutoPosResult = 1;
                            return false;
                        }
                        else if (Motion.IsArrive(StationEnum.ProbeGantry, AxisName.UvwStage_Y))
                        {
                           // Brake(true);
                            UvwAutoPosResult = 0;
                            return true;
                        }
                        Thread.Sleep(50);
                    }
                });
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("Uvw").Info($"ProbeGantry->Uvw单列补偿失败，失败信息：{ex.ToString()}");

                UvwAutoPosResult = 1;
                return false;
            }
   
        }
        /// <summary>
        /// uvw平台移动，自动更换位置和自动检测位置
        /// </summary>
        /// <param name="posName"></param>
        /// <param name="stageIndex"></param>
        /// <param name="waitTime"></param>
        /// <returns></returns>
        public async Task<bool> UvwMoveAbsSync(ProbeGantryPosName posName, int stageIndex, bool isStopContact = true)
        {
            IsUvwMoveDone = -1;
            if (isStopContact)
            {
                await StageStopContactSync(true);
            }
            switch (posName)
            {
                case ProbeGantryPosName.DetectPos:
                    {
                        double yPos = stageIndex == 0 ? Param.Uvw.AxisYPosStage1 : Param.Uvw.AxisYPosStage2;
                        double x1Pos = stageIndex == 0 ? Param.Uvw.AxisX1PosStage1 : Param.Uvw.AxisX1PosStage2;
                        double x2Pos = stageIndex == 0 ? Param.Uvw.AxisX2PosStage1 : Param.Uvw.AxisX2PosStage2;
                        Motion.MoveAbs(StationEnum.ProbeGantry, AxisName.UvwStage_Y, yPos);
                        Motion.MoveAbs(StationEnum.ProbeGantry, AxisName.UvwStage_X1, x1Pos);
                        Motion.MoveAbs(StationEnum.ProbeGantry, AxisName.UvwStage_X2, x2Pos);
                        break;
                    }
                case ProbeGantryPosName.AutoChgPos:
                    {
                        Motion.MoveAbs(StationEnum.ProbeGantry, AxisName.UvwStage_Y, Param.Uvw.AxisYAutoChgPos);
                        Motion.MoveAbs(StationEnum.ProbeGantry, AxisName.UvwStage_X1, Param.Uvw.AxisX1AutoChgPos);
                        Motion.MoveAbs(StationEnum.ProbeGantry, AxisName.UvwStage_X2, Param.Uvw.AxisX2AutoChgPos);
                        break;
                    }
            }
            return await Task.Run(() =>
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                while (true)
                {
                    if (sw.ElapsedMilliseconds > 20 * 1000)
                    {
                        IsUvwMoveDone = 1;
                        return false;
                    }
                    bool ret = true;
                    switch (posName)
                    {
                        case ProbeGantryPosName.DetectPos:
                            {
                                double yPos = stageIndex == 0 ? Param.Uvw.AxisYPosStage1 : Param.Uvw.AxisYPosStage2;
                                double x1Pos = stageIndex == 0 ? Param.Uvw.AxisX1PosStage1 : Param.Uvw.AxisX1PosStage2;
                                double x2Pos = stageIndex == 0 ? Param.Uvw.AxisX2PosStage1 : Param.Uvw.AxisX2PosStage2;

                                ret &= Motion.IsArrive(StationEnum.ProbeGantry, AxisName.UvwStage_Y, yPos);
                                ret &= Motion.IsArrive(StationEnum.ProbeGantry, AxisName.UvwStage_X1, x1Pos);
                                ret &= Motion.IsArrive(StationEnum.ProbeGantry, AxisName.UvwStage_X2, x2Pos);
                                break;
                            }
                        case ProbeGantryPosName.AutoChgPos:
                            {
                                ret &= Motion.IsArrive(StationEnum.ProbeGantry, AxisName.UvwStage_Y, Param.Uvw.AxisYAutoChgPos);
                                ret &= Motion.IsArrive(StationEnum.ProbeGantry, AxisName.UvwStage_X1, Param.Uvw.AxisX1AutoChgPos);
                                ret &= Motion.IsArrive(StationEnum.ProbeGantry, AxisName.UvwStage_X2, Param.Uvw.AxisX2AutoChgPos);
                                break;
                            }
                    }
                    if (ret)
                    {
                        IsUvwMoveDone = 0;
                        return true;
                    }
                    Thread.Sleep(50);
                }
            });
        }
        /// <summary>
        /// 平台压接Z轴平台是否在安全位置
        /// </summary>
        /// <returns></returns>
        public bool IsContactZAtSafePos()
        {
            bool result = true;
            result &= Motion.GetAxisActPos(StationEnum.DualStage, AxisName.Stage1_ContactZ)<31;//平台压接轴位置小于31(对位位置是30)，视为安全位置
            result &= Motion.GetAxisActPos(StationEnum.DualStage, AxisName.Stage2_ContactZ) <31;
            return result;
        }
        public bool IsAtAutoChangePos()
        {
            bool ret = true;
            ret &= Motion.IsArrive(StationEnum.ProbeGantry, AxisName.UvwStage_Y,Param.Uvw.AxisYAutoChgPos);
            ret &= Motion.IsArrive(StationEnum.ProbeGantry, AxisName.UvwStage_X1,Param.Uvw.AxisX1AutoChgPos);
            ret &= Motion.IsArrive(StationEnum.ProbeGantry, AxisName.UvwStage_X2,Param.Uvw.AxisX2AutoChgPos);
            return ret;
        }
        /// <summary>
        /// uvw平台相对移动
        /// </summary>
        /// <param name="axisUPos"></param>
        /// <param name="AxisVPos"></param>
        /// <param name="axisWPos"></param>
        public void UvwMoveRel(double axisUPos, double AxisVPos, double axisWPos, double startAngle = 0, bool isOnlyAngle = false)
        {
            //Brake(false);
            double yPos = 0.0;
            double x1Pos = 0.0;
            double x2Pos = 0.0;
            ConvertUVWToXY(axisUPos, AxisVPos, axisWPos, out yPos, out x1Pos, out x2Pos, startAngle, isOnlyAngle);
            Motion.MoveRel(StationEnum.ProbeGantry, AxisName.UvwStage_Y, yPos);
            Motion.MoveRel(StationEnum.ProbeGantry, AxisName.UvwStage_X1, x1Pos);
            Motion.MoveRel(StationEnum.ProbeGantry, AxisName.UvwStage_X2, x2Pos);
        }
        /// <summary>
        /// ProbeUnitZ轴相对运动，用于手动控制
        /// </summary>
        /// <param name="pos">Z轴距离</param>
        /// <param name="angle">角度</param>
        public void AxisZMove(int stageIndex,double pos)
        {
            AxisName axisName = stageIndex == 0 ? AxisName.Stage1_ContactZ : AxisName.Stage2_ContactZ;
            Motion.MoveRel(StationEnum.DualStage, axisName, pos);
        }
        //public void Brake(bool onOff)
        //{
        //    Motion.SetDigitalOutput(OutputName.UvwBrake1, onOff);
        //    Motion.SetDigitalOutput(OutputName.UvwBrake2, onOff);
        //    Thread.Sleep(200);
        //}
        public void UvwStop()
        {
            Motion.Stop(StationEnum.ProbeGantry, AxisName.UvwStage_Y);
            Motion.Stop(StationEnum.ProbeGantry, AxisName.UvwStage_X1);
            Motion.Stop(StationEnum.ProbeGantry, AxisName.UvwStage_X2);
        }
        public static bool ConvertZAngleToZ1Z2(double dZ, double dAngle, out double dZ1, out double dZ2)
        {
            dZ1 = 0;
            dZ2 = 0;
            try
            {
                double dRadianAngle = dAngle * Math.PI / 180.0;
                dZ2 = ((-1) * Math.Tan(dRadianAngle) * (100 / 2.0)) + dZ;
                dZ1 = (Math.Tan(dRadianAngle) * (100 / 2.0)) + dZ;
                return true;
            }
            catch (System.Exception ex)
            {
                ex.Data.Clear();
            }
            return false;
        }
        //   -- UVW Stage Descriptions --

        //■-- X1 축(V축)
        //┌─────────┐
        //│ⓞ              ⓞ│■ Y축(U축)
        //│                  ││
        //│                  │ 
        //│          --------│----- Base Angle 0
        //│                  │
        //│                  │
        //│ⓞ              ⓞ│
        //└─────────┘
        //■-- X2 축(W축)

        //        -- Parameter Values -- 
        //R : cross roller bearing radius(mm)
        //θY = 45 : axis Y cross roller bearing angle(degree)
        //θX1 = 135 : axis X1 cross roller bearing angle(degree)
        //θX2 = 255 : axis X2 cross roller bearing angle(degree)

        //θO : origin angle => Home 0
        //θR : request angle
        //θC : current angle

        //dX1 : X1 relative distance(mm)
        //dX2 : X2 relative distance(mm)
        //dY : Y relative distance(mm)

        //dX1 = (R* cos(θR + θX1 + θO) - R* cos(θX1 + θO)) * (-1)   => axis V
        //dX2 = (R* cos(θR + θX2 + θO) - R* cos(θX2 + θO)) * (-1)   => axis W
        //dY = (R* sin(θR + θY + θO) - R* sin(θY + θO)) * (-1)      => axis U

        //*(-1) => motor direction
        public static bool ConvertUVWToXY(double dU, double dV, double dW, out double dY, out double dX1, out double dX2, double dStartAngle = 0, bool isOnlyAngle = false)
        {
            dY = 0;
            dX1 = 0;
            dX2 = 0;
            try
            {
                if (isOnlyAngle)
                {
                    //double Ux = 125, Uy = 125, Vx = -125, Vy = 125, Wx = -125, Wy = -125, at = 0, bt = 0;
                    double Ux = 673, Uy = 0, Vx = 0, Vy = 87.31, Wx = -673, Wy = 0, at = 0, bt = 0;
                    dX1 = 0 + dV + bt - Uy + (Ux - at) * Math.Sin(AngleToRad(dW + dStartAngle)) + (Uy - bt) * Math.Cos(AngleToRad(dW + dStartAngle)) - (0 + bt - Uy + (Ux - at) * Math.Sin(AngleToRad(dStartAngle)) + (Uy - bt) * Math.Cos(AngleToRad(dStartAngle)));
                    dY = Vx - at - 0 - dU - (Vx - at) * Math.Cos(AngleToRad(dW + dStartAngle)) + (Vy - bt) * Math.Sin(AngleToRad(dW + dStartAngle)) - (Vx - at - 0 - (Vx - at) * Math.Cos(AngleToRad(dStartAngle)) + (Vy - bt) * Math.Sin(AngleToRad(dStartAngle)));
                    dX2 = Wy - bt - 0 - dV - (Wx - at) * Math.Sin(AngleToRad(dW + dStartAngle)) - (Wy - bt) * Math.Cos(AngleToRad(dW + dStartAngle)) - (Wy - bt - 0 - (Wx - at) * Math.Sin(AngleToRad(dStartAngle)) - (Wy - bt) * Math.Cos(AngleToRad(dStartAngle)));
                    dX2 = -dX2;

                }
                else
                {
                    double dRadius = 50.0 * Math.Sqrt(2);
                    //弧度值   角度转弧度=angle*Math.PI / 180.0。弧度转角度=rad*180/Math.PI
                    double dThetaX1 = 225.0;
                    double dThetaX2 = 315.0;
                    double dThetaY = 135.0;

                    dX1 = ((dRadius * Math.Cos(AngleToRad(dW + dThetaX1 + dStartAngle))) - (dRadius * Math.Cos(AngleToRad(dThetaX1 + dStartAngle)))) * (-1);
                    dX2 = ((dRadius * Math.Cos(AngleToRad(dW + dThetaX2 + dStartAngle))) - (dRadius * Math.Cos(AngleToRad(dThetaX2 + dStartAngle)))) * (-1);
                    dY = ((dRadius * Math.Sin(AngleToRad(dW + dThetaY + dStartAngle))) - (dRadius * Math.Sin(AngleToRad(dThetaY + dStartAngle)))) * (-1);
                    //X,Y Offset
                    dY += dU;
                    dX1 += dV;
                    dX2 += dV;
                }
                return true;
            }
            catch (System.Exception ex)
            {
                ex.Data.Clear();
            }
            return false;
        }
        /// <summary>
        /// 角度值转弧度值
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        private static double AngleToRad(double angle)
        {
            double rad = 0.0;
            rad = angle * Math.PI / 180.0;
            return rad;
        }

    }

}
