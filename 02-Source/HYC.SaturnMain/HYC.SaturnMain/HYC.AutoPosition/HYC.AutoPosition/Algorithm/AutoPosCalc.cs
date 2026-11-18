/*----------------------------------------------------------------
// Copyright (C) Suzhou HYC Electronic Technology Co.,LTD
// 版权所有。
//
// =================================
// CLR版本     ：4.0.30319.42000
// 命名空间    ：HYC.AutoPosition.Algorithm
// 文件名称    ：AutoPosCalc.cs
// =================================
// 创 建 者    ：yangkang
// 创建日期    ：2020/10/11 9:25:33 
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

namespace HYC.AutoPosition.Algorithm
{
    /// <summary>
    /// 具有XY和Theta的点
    /// </summary>
    public struct Point3f
    {
        /// <summary>
        /// 坐标X
        /// </summary>
        public float X;
        /// <summary>
        /// 坐标Y
        /// </summary>
        public float Y;
        /// <summary>
        /// 角度
        /// </summary>
        public float T;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="t"></param>
        public Point3f(float x, float y, float t)
        {
            this.X = x;
            this.Y = y;
            this.T = t;
        }

        /// <summary>
        /// 是否值全为0
        /// </summary>
        /// <returns></returns>
        public bool IsZero()
        {
            return X == 0 && Y == 0 && T == 0;
        }

        /// <summary>
        /// 转换为字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"X={X},Y={Y},T={T}";
        }
    }

    /// <summary>
    /// 自动对位结果计算
    /// </summary>
    public class AutoPosCalc
    {
        /// <summary>
        /// 计算运动值
        /// </summary>
        /// <param name="srcWidth">图像width</param>
        /// <param name="points">识别的坐标</param>
        /// <param name="para1">输入参数1</param>
        /// <param name="para2">输入参数2</param>
        /// <returns>xyt值</returns>
        public static double[] CalcMovePara(int srcWidth, Point3f[] points, float[] para1, float[] para2)
        {
            // 暂存参数值
            double markSizeMm = para1[0];           //Mark大小mm
            double markDisMm = para1[1];            //Mark距离mm
            double markSizePixel = para1[2];        //Mark大小pixel
            double markDisPixel = para1[3];         //Mark距离pixel
            double dstX1 = para1[4];                //左相机目标X(pixel)
            double dstY1 = para1[5];                //左相机目标Y(pixel)
            double dstT1 = para1[6];                //左相机目标T(角度)
            double dstX2 = para1[7];                //右相机目标X(pixel)
            double dstY2 = para1[8];                //右相机目标Y(pixel)
            double dstT2 = para1[9];                //右相机目标T(角度)
            double stepT = para1[10];               //角度步数
            double stepX = para1[11];               //X步数
            double stepY = para1[12];               //Y步数                

            double cameraCount = para2[0];          //相机数
            double rotateCenterX = para2[1];        //旋转中心X
            double rotateCenterY = para2[2];        //旋转中心Y

            double curX1 = points[0].X;             //识别的左相机坐标X(pixel)
            double curY1 = points[0].Y;             //识别的左相机坐标Y(pixel)
            double curT1 = points[0].T;             //识别的左相机坐标T(角度)
            double curX2 = points[1].X;             //识别的右相机坐标X(pixel)
            double curY2 = points[1].Y;             //识别的右相机坐标Y(pixel)
            double curT2 = points[1].T;             //识别的右相机坐标T(角度)

            // 计算旋转角度，以下为双相机计算方法
            // 求两相机的像素间距dCamera
            // ((W1 - x1) + dCamera + x2) ^ 2 + (y2 - y1) ^ 2 = dMark ^ 2
            double dMark = markSizePixel / markSizeMm * markDisMm;
            double dCamera = Math.Sqrt(Math.Pow(dMark, 2) - Math.Pow(curY2 - curY1, 2)) - (srcWidth - curX1 + curX2);
            // 求实际位置与与目标位置的夹角
            double k1 = (dstY2 - dstY1) / (srcWidth - dstX1 + dCamera + dstX2);
            double k2 = (curY2 - curY1) / (srcWidth - curX1 + dCamera + curX2);
            double rad = -Math.Atan((k2 - k1) / (1 + k1 * k2));
            double theta = rad / Math.PI * 180;
            // 如果是单相机，角度由左相机识别结果直接给出
            if (cameraCount == 1)
            {
                theta = -(curT1 - dstT1);
                rad = theta / 180 * Math.PI;
            }
            // 计算XY方向的移动距离，以左相机为基准
            double moveX = -(curX1 - dstX1);
            double moveY = -(curY1 - dstY1);
            // 如果设置了旋转中心，则需要计算旋转后的移动值
            if (rotateCenterX != 0 && rotateCenterY != 0)
            {
                double length = Math.Sqrt(Math.Pow(curY1 - rotateCenterY, 2) + Math.Pow(curX1 - rotateCenterX, 2));
                double theta2 = (curY1 - rotateCenterY) > 0 ? Math.Acos((curX1 - rotateCenterX) / length) : -Math.Acos((curX1 - rotateCenterX) / length);
                theta2 += rad;
                double newX = length * Math.Cos(theta2) + rotateCenterX;
                double newY = length * Math.Sin(theta2) + rotateCenterY;
                moveX = -(newX - dstX1);
                moveY = -(newY - dstY1);
            }
            // 将像素值换算为实际的距离
            double[] xyt = new double[12];
            xyt[0] = Math.Round(moveX / markSizePixel * markSizeMm * stepX, 3);
            xyt[1] = Math.Round(moveY / markSizePixel * markSizeMm * stepY, 3);
            xyt[2] = Math.Round(theta * stepT, 3);
            //left
            xyt[4] = curX1;
            xyt[5] = curY1;
            xyt[6] = curT1;
            //right
            xyt[8] = curX2;
            xyt[9] = curY2;
            xyt[10] = curT2;

            // 异常情况判断
            if (cameraCount == 1)
            {
                if (points[0].IsZero())
                {
                    xyt[0] = 0;
                    xyt[1] = 0;
                    xyt[2] = 400;
                }
            }
            else
            {
                if (points[0].IsZero() || points[1].IsZero())
                {
                    xyt[0] = 0;
                    xyt[1] = 0;
                    xyt[2] = 400;
                }
            }

            return xyt;
        }
    }
}
