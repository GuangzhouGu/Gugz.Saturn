/*----------------------------------------------------------------
// Copyright (C) Suzhou HYC Technology Co.,LTD
// °æÈ¨ËùÓÐ¡£
//
// =================================
// CLR°æ±¾     £º4.0.30319.42000
// ÃüÃû¿Õ¼ä    £ºHYC.AutoPosition.Parameter
// ÎÄ¼þÃû³Æ    £ºCameraInformation.cs
// =================================
// ´´ ½¨ Õß    £ºmiaoliang
// ´´½¨ÈÕÆÚ    £º2020/12/2 15:11:54 
// ¹¦ÄÜÃèÊö    £ºCamera Information Parameter
// Ê¹ÓÃËµÃ÷    £º
//
//
// ´´½¨±êÊ¶£º
//
// ÐÞ¸Ä±êÊ¶£º
// ÐÞ¸ÄÃèÊö£º
//
// ÐÞ¸Ä±êÊ¶£º
// ÐÞ¸ÄÃèÊö£º
//----------------------------------------------------------------*/
using Emgu.CV;
using HYC.HTFile.HTXML;
using System;
using System.Drawing;

namespace HYC.AutoPosition.Parameter
{
    public class Point3D
    {
        private double[] values = new double[3];

        public double X
        {
            get
            {
                return values[0];
            }
            set
            {
                values[0] = value;
            }
        }

        public double Y
        {
            get
            {
                return values[1];
            }
            set
            {
                values[1] = value;
            }
        }

        public double Z
        {
            get
            {
                return values[2];
            }
            set
            {
                values[2] = value;
            }
        }
        public PointF PointF
        {
            get
            {
                return new PointF((float)values[0], (float)values[1]);
            }
        }
        public Point3D()
        {

        }
        public Point3D(double[] _values)
        {
            values[0] = _values[0];
            values[1] = _values[1];
            values[2] = _values[2];
        }
        public Point3D(Matrix<double> _values)
        {
            if (_values.Cols == 1)
            {
                for (int i = 0; i < _values.Rows && i < 3; i++)
                {
                    values[i] = _values[i, 0];
                }
            }
            if (_values.Rows == 1)
            {
                for (int i = 0; i < _values.Cols && i < 3; i++)
                {
                    values[i] = _values[0, i];
                }
            }
        }
        public Point3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public static Point3D operator +(Point3D p1, Point3D p2)
        {
            Point3D point3D = new Point3D();
            point3D.X = p1.X + p2.X;
            point3D.Y = p1.Y + p2.Y;
            point3D.Z = p1.Z + p2.Z;
            return point3D;
        }

        public static Point3D operator -(Point3D p1, Point3D p2)
        {
            Point3D point3D = new Point3D();
            point3D.X = p1.X - p2.X;
            point3D.Y = p1.Y - p2.Y;
            point3D.Z = p1.Z - p2.Z;
            return point3D;
        }
        public  Point3D Avg(Point3D p1, Point3D p2)
        {
            Point3D point3D = new Point3D();
            point3D.X = (p1.X + p2.X)/2;
            point3D.Y = (p1.Y + p2.Y)/2;
            point3D.Z = (p1.Z + p2.Z)/2;
            return point3D;
        }
        public double[] GetValues()
        {
            return values;
        }
    }
    public class Point5D
    {
        private double[] values = new double[5];

        public double this[int index]
        {
            get
            {
                return values[index];
            }
            set
            {
                values[index] = value;
            }
        }
    }
    /// <summary>
    /// Ïà»ú²ÎÊý
    /// </summary>
    [Serializable]
    public class CameraInformation
    {
        /// <summary>
        /// Camera Rotate CenterPoint
        /// </summary>
        public PointF CenterPoint { get; set; }
        /// <summary>
        /// Camera and stage theta
        /// </summary>
        public double Theta { get; set; }
        /// <summary>
        /// X Axis Resolution unit mm
        /// </summary>
        public double ResolutionX { get; set; }
        /// <summary>
        /// Y Axis Resolution unit mm
        /// </summary>
        public double ResolutionY { get; set; }
        /// <summary>
        /// X Axis Pixel
        /// </summary>
        public double PixelX{ get; set; }
        /// <summary>
        /// Y Axis Pixel
        /// </summary>
        public double PixelY { get; set; }
       
        /// <summary>
        /// X Axis FOV
        /// </summary>
        public double FOVX { get; set; }
        /// <summary>
        /// Y Axis FOV
        /// </summary>
        public double FOVY { get; set; }


        public void Test()
        {
            //Test Check Angle
            PointF originPoint = new PointF(100, 100);
            Matrix<double> rotateMatrix = new Matrix<double>(new Size(3, 2));
            CvInvoke.GetRotationMatrix2D(originPoint, 0.2, 1, rotateMatrix);
            PointF theoryPoint = new PointF(100 + 100, 100);
            Matrix<double> theoryMatrix = GetMatrix(theoryPoint);
            Matrix<double> movedMatrix = rotateMatrix * theoryMatrix;
            PointF movedPoint = new PointF((float)movedMatrix[0, 0], (float)movedMatrix[1, 0]);
            double angle = CaculateCameraTheta(originPoint, movedPoint, 100, 0);

            ///¼ÆËãÐý×ªÖÐÐÄ Check CenterPoint
            Matrix<double> mat1 = new Matrix<double>(new Size(3, 2));
            Matrix<double> mat2 = new Matrix<double>(new Size(3, 2));
            PointF centerPoint = new PointF(10000, 10000);
            CvInvoke.GetRotationMatrix2D(centerPoint, 0.2, 1, mat1);
            CvInvoke.GetRotationMatrix2D(centerPoint, -0.2, 1, mat2);
            Matrix<double> originMatrix = GetMatrix(originPoint);

            Matrix<double> pt1 = mat1 * originMatrix;
            Matrix<double> pt2 = mat2 * originMatrix;

            PointF pt = CaculateRotatedCenterPoint(GetPoint(pt1), GetPoint(pt2), 0.2);
            PointF rotatedPoint = new PointF(GetPoint(pt1).X + 20, GetPoint(pt1).Y + 20);


            //Check CenterPoint
            movedMatrix = rotateMatrix * theoryMatrix;


            //Point3D point3D = CaculationXYT(cameraInformation, originPoint, movedPoint1, 0.2);
            int k = 0;
        }

        /// <summary>
        /// 计算Camera和运动平台之间的夹角
        /// </summary>
        /// <param name="originPoint">原始点位</param>
        /// <param name="movedPoint">移动后点位</param>
        /// <param name="deltaX">X偏移量</param>
        /// <param name="deltaY">Y偏移量</param>
        /// <returns></returns>
        public double CaculateCameraTheta(PointF originPoint, PointF movedPoint, double deltaX, double deltaY)
        {
            PointF movedPointTheory = new PointF((float)(originPoint.X + deltaX), (float)(originPoint.Y + deltaY));
            double angle = 180 * Math.Atan2(movedPoint.Y - originPoint.Y, movedPoint.X - originPoint.X) / Math.PI;
            double angle1 = 180 * Math.Atan2(movedPointTheory.Y - originPoint.Y, movedPointTheory.X - originPoint.X) / Math.PI;
            return angle - angle1;
        }

        /// <summary>
        /// ¼ÆËãÐý×ªÖÐÐÄ
        /// </summary>
        /// <param name="rotatedPoint1">µÚÒ»´ÎÐý×ªºóµÄµã</param>
        /// <param name="rotatedPoint2">µÚ¶þ´ÎÐý×ªºóµÄµã</param>
        /// <param name="angele">µÚÒ»´Îµ½µÚ¶þ´ÎÖ®¼äµÄ½Ç¶È²î</param>
        /// <returns></returns>
        public PointF CaculateRotatedCenterPoint(PointF rotatedPoint1, PointF rotatedPoint2, double angele)
        {
            double sn = Math.Sin(angele / 180 * Math.PI);
            double cs = Math.Cos(angele / 180 * Math.PI);
            double centerX = 0.5 * ((rotatedPoint2.Y - rotatedPoint1.Y) * sn + (rotatedPoint2.X + rotatedPoint1.X) * cs - (rotatedPoint2.X + rotatedPoint1.X)) / (cs - 1);
            double centerY = 0.5 * ((rotatedPoint2.Y + rotatedPoint1.Y) * cs - (rotatedPoint2.X - rotatedPoint1.X) * sn - (rotatedPoint2.Y + rotatedPoint1.Y)) / (cs - 1);
            return new PointF((float)centerX, (float)centerY);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="originPoint"></param>
        /// <param name="movedPoint"></param>
        /// <param name="movedTheta"></param>
        /// <returns></returns>
        public Point3D CaculationXYT(PointF originPoint, PointF movedPoint, double movedTheta)
        {
            
            PointF correctionPoint = CaculateRealPoint(originPoint, movedPoint);
            Matrix<double> rotatedMatrix = new Matrix<double>(new Size(3, 2));
            CvInvoke.GetRotationMatrix2D(this.CenterPoint, movedTheta, 1, rotatedMatrix);

            Matrix<double> rotatePointTheory = rotatedMatrix * GetMatrix(originPoint);
            double deltaX = correctionPoint.X - rotatePointTheory[0, 0];
            double deltaY = correctionPoint.Y - rotatePointTheory[1, 0];
            
            deltaX = deltaX * ResolutionX;
            deltaY = deltaY * ResolutionY;
            return new Point3D(deltaX, deltaY, movedTheta);
        }
        public Point3D CaculationXYT(PointF originPoint, PointF foundPoint)
        {
            double deltaX = (foundPoint.X- originPoint.X) * ResolutionX;
            double deltaY = (foundPoint.Y - originPoint.Y) * ResolutionY;
            return new Point3D(Math.Round(deltaX,3), Math.Round(deltaY,3), 0);
        }
        /// <summary>
        /// ÐÞÕýCameraºÍÆ½Ì¨µÄ¼Ð½Ç
        /// </summary>
        /// <param name="originPoint"></param>
        /// <param name="movedPoint"></param>
        /// <returns></returns>
        public PointF CaculateRealPoint(PointF originPoint, PointF movedPoint)
        {
            Matrix<double> rotatedMatrix = new Matrix<double>(new Size(3, 2));
            CvInvoke.GetRotationMatrix2D(originPoint, this.Theta, 1, rotatedMatrix);
            Matrix<double> movedMatrix = rotatedMatrix * GetMatrix(movedPoint);
            return GetPoint(movedMatrix);
        }

        private Matrix<double> GetMatrix(PointF pt)
        {
            Matrix<double> mat = new Matrix<double>(3, 1);
            mat[0, 0] = pt.X;
            mat[1, 0] = pt.Y;
            mat[2, 0] = 1;
            return mat;
        }

        private PointF GetPoint(Matrix<double> matrix)
        {
            PointF pt = new PointF((float)matrix[0, 0], (float)matrix[1, 0]);
            return pt;
        }
    }

    /// <summary>
    /// Ïà»úÐÅÏ¢Êý×é£¬ÔÝÊ±½ö½öÖ§³ÖÁ½¸öÏà»ú
    /// </summary>
    [Serializable]
    public class CameraInformationArray {
        /// <summary>
        /// Camera Informations
        /// </summary>
        public CameraInformation[] CameraInformations { get; set; }
        /// <summary>
        /// Camera Calibration Parameter
        /// </summary>
        public CameraOption CameraOption { get; set; }

        /// <summary>
        /// 采用双相机时，需要测量两个相机的中心点与Stage平台的初始夹角。
        /// </summary>
        public double DefaultAngle { get; set; }
        /// <summary>
        /// 相机1和相机2使用同一个角度
        /// </summary>
        public bool UseSameAngle { get; set; }
        public CameraInformationArray()
        {
            CameraInformations = new CameraInformation[2];
            CameraInformations[0] = new CameraInformation();
            CameraInformations[1] = new CameraInformation();
            CameraOption = new CameraOption();
        }

        /// <summary>
        /// Caculate Real Angle
        /// </summary>
        /// <param name="pt1"></param>
        /// <param name="pt2"></param>
        /// <returns></returns>
        public double CaculateRealAngle(Point3D pt1,Point3D pt2)
        {
            double angle = Math.Atan2(pt1.Y - pt2.Y, pt1.X - pt2.X);
            angle = angle / Math.PI * 180;
            double defaultAngle = this.DefaultAngle;
            double deltaAngle = 0;
            if (angle * defaultAngle < 0)
            {
                if (angle < 0)
                {
                    deltaAngle = 360 + angle - defaultAngle;
                }
                else
                {
                    deltaAngle = 360 + defaultAngle - angle;
                }

            }
            else if (angle * defaultAngle > 0)
            {
                deltaAngle = angle - defaultAngle;
            }
            return deltaAngle;
        }
        /// <summary>
        /// ÐòÁÐ»¯²ÎÊýµ½ÎÄ¼þÖÐ
        /// </summary>
        /// <param name="fileName"></param>
        public void Serialize(string fileName)
        {
            HTXMLSerializer.EntityToXMLFile(this, fileName);
        }

        /// <summary>
        /// ´ÓÎÄ¼þÖÐ·´ÐòÁÐ»¯
        /// </summary>
        /// <param name="fileName"></param>
        public static CameraInformationArray Unserialize(string fileName)
        {
            try
            {
                CameraInformationArray cameraInformationArray = HTXMLSerializer.EntityFromXMLFile<CameraInformationArray>(fileName);
                return cameraInformationArray;
            }
            catch (Exception ex)
            {
                return new CameraInformationArray();
            }
        }

        public CameraInformation this[int index]
        {
            get
            {
                if (index < 0 || index >= CameraInformations.Length)
                {
                    return null;
                }
                return CameraInformations[index];
            }
            set
            {
                if (index <= 0 || index >= CameraInformations.Length)
                {
                    return;
                }
                CameraInformations[index] = value;
            }
        }
    }

    /// <summary>
    /// Camera Auto Calibration Value
    /// </summary>
    [Serializable]
    public class CameraOption
    {
        /// <summary>
        /// X Axis Offset mm
        /// </summary>
        public double DeltaX = 0.5;
        /// <summary>
        /// Y Axis Offset mm
        /// </summary>
        public double DeltaY = 0.5;
        /// <summary>
        /// Theta Axis Offset mm
        /// </summary>
        public double DeltaTheta = 0.2;
    }

}
