/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2023 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.TPApiUnit.Devices.AoiCamera
// 文件名称    ：Class1
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
using CommonLib.Device;
using HYC.HTCamera.Card.Hik;
using HYC.SaturnAoi;
using HYC.SaturnAoi.Entities;
using HYC.StandardAoi.Parameter;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Xml.Linq;

namespace HYC.TPApiUnit.Devices.AoiCamera
{
    /// <summary>
    /// AOI相机管理
    /// </summary>
    public class CaptureCardControl
    {
        /// <summary>
        /// Mil相机管理对象
        /// </summary>
        private HikApplication HikApp =null;
        /// <summary>
        /// 总的相机数
        /// </summary>
        public  int CameraCount = 2;
        //相机实时预览中
        private bool IsLiving=false;
        //显示图像的委托集合，用于实时显示图像
        //连接状态事件
        public event Action<string, int, bool> OnConnected;
        public event Action< int, ImageData> OnImageReceived;
        //消息显示事件
        public event Action<string, string, Brush> OnShowMsg;
        private ManualResetEvent MreGrab = null;
        ImageData Image = null;
        CaptureCardInfo Info = null;
        public CaptureCardControl(CaptureCardInfo info)
        {
            Info = info;
            Image = new ImageData();
            MreGrab = new ManualResetEvent(false);
        }
        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            try
            {
                HikApp = new HikApplication();
                HikApp.Connect += HikApp_Connect1;
                HikApp.ImageReceived += HikApp_ImageReceived;
                HikApp.ShowMsg += HikApp_ShowMsg;
                HikApp.InitApplication(Info.FileName);
                var cam=GetCamera(0);
                Global.ImageProducers.Add(new ImageProducer(0,0));
            }
            catch (Exception ex)
            {
                OnShowMsg($"{Info.CaptureCardName}", $"采集卡连接异常，异常信息：{ex}", Brushes.Red);
            }
        }
        public HikCamera GetCamera(int cardIndex)
        {
            return HikApp.GetCamera(cardIndex, 0);
        }
        private void HikApp_Connect1(int arg1, bool arg2)
        {
            OnConnected?.Invoke($"{Info.CaptureCardName}", HikApp.CameraCount, arg2);
        }

        private void HikApp_ShowMsg(object sender, HikCameraShowMsgEventArgs e)
        {
            OnShowMsg($"{Info.CaptureCardName}", e.Text, e.IsError ? Brushes.Red : Brushes.Green);
        }

        private void HikApp_ImageReceived(object sender, HikCameraEventArgs e)
        {
            Image= new ImageData() { ImageBytes = e.Data, Width = e.Width, Height = e.Height };
            MreGrab.Set();
            if (IsLiving)
            {
                OnImageReceived?.Invoke(e.CardIndex, Image);
            }
        }

        /// <summary>
        /// 设置曝光时间
        /// </summary>
        /// <param name="cardIndex">采集卡序号</param>
        /// <param name="milliseconds">曝光时间</param>
        /// <returns>true:成功 false:失败</returns>
        public bool SetExposureTime(int cardIndex, float milliseconds)
        {
            HikCamera cam = HikApp.GetCamera(cardIndex, 0);
            if (cam != null)
            {
                bool ret = cam.SetExposureTime(milliseconds);
                return ret;
            }
            return false;
        }

        /// <summary>
        /// 获取曝光时间
        /// </summary>
        /// <param name="cardIndex">采集卡序号</param>
        /// <returns>曝光时间</returns>
        public float GetExposureTime(int cardIndex)
        {
            HikCamera cam = HikApp.GetCamera(cardIndex, 0);
            if (cam != null)
            {
                double time = 0;
                time = cam.GetExposureTime();
                return (float)time;
            }
            return 0;
        }

        /// <summary>
        /// 设置增益
        /// </summary>
        /// <param name="cardIndex">采集卡序号</param>
        /// <param name="gain">增益</param>
        /// <returns>true:成功 false:失败</returns>
        public bool SetGain(int cardIndex, double gain)
        {
            HikCamera cam = HikApp.GetCamera(cardIndex, 0);
            if (cam != null)
            {
                cam.SetFloatValue("Gain",gain);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 获取增益
        /// </summary>
        /// <param name="cardIndex">采集卡序号</param>
        /// <returns>增益</returns>
        public double GetGain(int cardIndex)
        {
            HikCamera cam = HikApp.GetCamera(cardIndex, 0);
            if (cam != null)
            {
                double gain = cam.GetFloatValue("Gain");
                return gain;
            }
            return 0;
        }

        /// <summary>
        /// 面阵相机拍照
        /// </summary>
        /// <param name="cardIndex">采集卡序号</param>
        /// <returns>图像数据</returns>
        public bool SoftTrigger(int cardIndex)
        {
            bool ret = false;
            HikCamera camera = HikApp.GetCamera(cardIndex, 0);
            if (camera != null)
            {
                ret=camera.SoftTrigger();
            }
            return ret;
        }
        /// <summary>
        /// 面阵相机拍照
        /// </summary>
        /// <param name="index"></param>
        /// <returns>图像数据</returns>
        public ImageData Grab2d(int cardIndex)
        {
            HikCamera camera = HikApp.GetCamera(cardIndex, 0);
            if (camera != null)
            {
                IsLiving = false;
                int width = camera.GetWidth();
                int height = camera.GetHeight();
                byte[] data = new byte[width * height];
                camera.Live(false);
                camera.Grab2d(data);
                return new ImageData()
                {
                    ImageBytes = data,
                    Width = width,
                    Height = height
                };
            }
            return null;
        }
        /// <summary>
        /// 等待
        /// </summary>
        /// <param name="index">CardIndex</param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public ImageData WaitGrab(int timeout = 10000)
        {
            Image = null;
            MreGrab.Reset();
            MreGrab.WaitOne(timeout);
            return Image;
        }

        /// <summary>
        /// 实时采集
        /// </summary>
        /// <param name="cardIndex">采集卡序号</param>
        /// <param name="isSetToLive"></param>
        public void Live(int cardIndex, bool isSetToLive)
        {
            IsLiving= isSetToLive;
            HikCamera camera = HikApp.GetCamera(cardIndex, 0);
            if (camera != null)
            {
                camera.Live(isSetToLive);
            }
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public bool Uninit()
        {
            try
            {
                for (int i = 0; i < CameraCount; i++)
                {
                    HikApp.GetCamera(i, 0)?.Live(false);
                    HikApp.GetCamera(i, 0)?.UninitCamera();
                }
                HikApp.FreeApplication();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }
    }
}
