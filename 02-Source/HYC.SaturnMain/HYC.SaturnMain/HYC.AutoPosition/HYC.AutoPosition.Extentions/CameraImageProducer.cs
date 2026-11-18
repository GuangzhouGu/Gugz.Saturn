/*----------------------------------------------------------------
// Copyright (C) Suzhou HYC Electronic Technology Co.,LTD
// 版权所有。
//
// =================================
// CLR版本     ：4.0.30319.42000
// 命名空间    ：HYC.AutoPosition.Extentions
// 文件名称    ：CameraImageProducer.cs
// =================================
// 创 建 者    ：yangkang
// 创建日期    ：2020/1/2 17:29:35 
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
using HYC.AutoPosition.Parameter;
using HYC.HTCamera.GigE;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HYC.AutoPosition.Extentions
{
    /// <summary>
    /// 从相机获取图像的图像数据生产者
    /// </summary>
    public class CameraImageProducer : IImageProducer
    {
        //相机
        private IHTCamera camera = null;
        //图像数据
        private ImageData imageData = null;
        //拍照事件
        private ManualResetEvent mreGrab = new ManualResetEvent(false);
        //相机拍照中
        private bool isGrabing = false;
        //拍照锁
        private static object lockObj = new object();
        //是否启用拍照锁
        private bool isUseGrabLock = false;
        //图像旋转与镜像
        private RotateFlipType rotateFlipType = 0;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="camera">相机</param>
        /// <param name="isUseGrabLock">拍照时是否加锁</param>
        /// <param name="rotateFlipType">是否旋转</param>
        public CameraImageProducer(IHTCamera camera, bool isUseGrabLock, RotateFlipType rotateFlipType)
        {
            this.camera = camera;
            this.isUseGrabLock = isUseGrabLock;
            this.rotateFlipType = rotateFlipType;
        }

        /// <summary>
        /// 接收到相机的图像数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Camera_ImageReceived(object sender, HTCameraEventArgs e)
        {
            if (!isGrabing) return;

            imageData = new ImageData
            {
                ImageBytes = e.Data,
                Width = e.Width,
                Height = e.Height
            };

            //如果需要翻转与镜像
            if (rotateFlipType != 0)
            {
                Bitmap bmp = imageData.ToBitmap();
                bmp.RotateFlip(rotateFlipType);
                imageData = new ImageData();
                imageData.FromBitmap(bmp);
            }

            mreGrab.Set();
        }

        /// <summary>
        /// 获取图像数据
        /// </summary>
        /// <returns>图形数据</returns>
        public ImageData CreateImageData()
        {
            try
            {
                if (isUseGrabLock)
                {
                    Monitor.Enter(lockObj);
                }
                isGrabing = true;
                this.camera.ImageReceived += Camera_ImageReceived;
                mreGrab.Reset();
                imageData = null;
                camera.SoftTrigger();
                mreGrab.WaitOne(1000);
                return imageData;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                isGrabing = false;
                this.camera.ImageReceived -= Camera_ImageReceived;
                if (isUseGrabLock)
                {
                    Monitor.Exit(lockObj);
                }
            }
        }
    }
}
