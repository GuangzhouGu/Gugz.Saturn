/*----------------------------------------------------------------
// Copyright (C) Suzhou HYC Electronic Technology Co.,LTD
// 版权所有。
//
// =================================
// CLR版本     ：4.0.30319.42000
// 命名空间    ：HYC.AutoPosition.UI.Entities
// 文件名称    ：PosCamera.cs
// =================================
// 创 建 者    ：yangkang
// 创建日期    ：2020/1/19 13:41:36 
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
using HYC.AutoPosition.Parameter;
using HYC.AutoPosition.UI.Helper;
using HYC.AutoPosition.UI.View;
using HYC.HTCamera.GigE;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HYC.AutoPosition.UI.Entities
{
    /// <summary>
    /// 显示图像的委托
    /// </summary>
    /// <param name="data">图像数据</param>
    public delegate void ShowLiveImage(ImageData data);

    /// <summary>
    /// 对位相机管理类
    /// </summary>
    public static class PosCamera
    {
        /// <summary>
        /// 相机数
        /// </summary>
        public static int CameraCount = 0;
        /// <summary>
        /// 相机的数组
        /// </summary>
        private static IHTCamera[] cameras = new IHTCamera[CameraCount];
        /// <summary>
        /// 图像数据
        /// </summary>
        private static ImageData[] imageDatas = new ImageData[CameraCount];

        //拍照事件
        private static ManualResetEvent[] mreGrab = new ManualResetEvent[CameraCount];
        //相机拍照中
        private static bool[] isGrabing = new bool[CameraCount];
        //相机实时预览中
        private static bool[] isLiving = new bool[CameraCount];
        //显示图像的委托集合，用于实时显示图像
        private static ShowLiveImage[] showLiveImageMethods = new ShowLiveImage[CameraCount];

        //图像旋转与镜像
        private static RotateFlipType[] rotateFlipTypes = new RotateFlipType[CameraCount];

        //暂存相机消息
        private static string[] Msgs = new string[CameraCount];

        /// <summary>
        /// 初始化
        /// </summary>
        public static void Init()
        {
            //变量初始化
            var lstCameraConfig = Global.Setting.GetCameraConfigs();
            CameraCount = lstCameraConfig.Count;    
            cameras = new IHTCamera[CameraCount];
            imageDatas = new ImageData[CameraCount];
            mreGrab = new ManualResetEvent[CameraCount];
            isGrabing = new bool[CameraCount];
            isLiving = new bool[CameraCount];
            showLiveImageMethods = new ShowLiveImage[CameraCount];
            rotateFlipTypes = new RotateFlipType[CameraCount];
            Msgs = new string[CameraCount];
            //依次初始化每个相机
            for (int i = 0; i < CameraCount; i++)
            {
                try
                {
                    mreGrab[i] = new ManualResetEvent(false);

                    var cfg = lstCameraConfig[i];
                    rotateFlipTypes[i] = cfg.RotateFlipType;
                    //创建相机的实例
                    cameras[i] = HTCameraFactory.CreateCamera(cfg.CameraType);
                    //设置相机的属性
                    cameras[i].CameraIndex = i;
                    cameras[i].Name = "";
                    cameras[i].MacAddr = cfg.MacAddr;
                    //监听事件
                    cameras[i].ImageReceived += Camera_ImageReceived;
                    cameras[i].ConnectionChanged += Camera_ConnectionChanged;
                    cameras[i].ShowMsg += PosCamera_ShowMsg;
                    //开始初始化
                    bool result = cameras[i].Init();

                    cameras[i].SetFloatValue("ExposureTimeAbs",cfg.ExposureTime * 1000);
                }
                    catch (Exception ex)
                {

                }
            }
            //启动重连线程
            Task.Factory.StartNew(ReInit);
        }

        /// <summary>
        /// 处理相机内部消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void PosCamera_ShowMsg(object sender, HTCameraShowMsgEventArgs e)
        {
            if (e.Text != Msgs[e.CameraIndex])
            {
                Msgs[e.CameraIndex] = e.Text;
                LogManager.GetLogger("Camera").Info("Camera:" + e.CameraIndex + "\r\n" + e.Text);
            }
        }

        /// <summary>
        /// 相机重连线程
        /// </summary>
        private static void ReInit()
        {
            while (true)
            {
                for (int i = 0; i < CameraCount; i++)
                {
                    try
                    {
                        if (!cameras[i].IsConnected)
                        {
                            cameras[i].Init();
                        }
                    }
                    catch (Exception)
                    { }
                }
                Thread.Sleep(1000);
            }
        }       

        /// <summary>
        /// 连接状态改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Camera_ConnectionChanged(object sender, HTCameraEventArgs e)
        {
            //if (e.IsConnected)
            //{
            //    ViewStatus.Instance.ShowMsg(string.Format(LanguageHelper.Msg.PosCamera_ConnectSuccess, e.CameraIndex + 1), Brushes.Green);
            //    ViewStatus.Instance.UpdateDeviceState("Camera" + (e.CameraIndex + 1), LanguageHelper.Msg.PosCamera_Name + (e.CameraIndex + 1), true);
            //    SetExposureTime(e.CameraIndex,Global.Setting.ExposureTime[e.CameraIndex]);
            //}
            //else
            //{
            //    ViewStatus.Instance.ShowMsg(string.Format(LanguageHelper.Msg.PosCamera_ConnectFailed, e.CameraIndex + 1), Brushes.Red);
            //    ViewStatus.Instance.UpdateDeviceState("Camera" + (e.CameraIndex + 1), LanguageHelper.Msg.PosCamera_Name + (e.CameraIndex + 1), false);
            //}
            //同步状态到SignalProvider
            var provider = PosSignal.GetSignalProvider();
            if (provider != null)
            {
                provider.UpdateCameraStatus(e.CameraIndex, e.IsConnected);
            }
        }

        /// <summary>
        /// 更新界面相机状态
        /// </summary>
        public static void UpdateCameraStatus()
        {
            for (int i = 0; i < CameraCount; i++)
            {
                bool status = cameras[i] != null ? cameras[i].IsConnected : false;
                
            }
        }

        /// <summary>
        /// 接收到图像数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Camera_ImageReceived(object sender, HTCameraEventArgs e)
        {
            if (!isGrabing[e.CameraIndex] && !isLiving[e.CameraIndex]) return;

            imageDatas[e.CameraIndex] = new ImageData
            {
                ImageBytes = e.Data,
                Width = e.Width,
                Height = e.Height
            };

            //如果需要翻转与镜像
            if (rotateFlipTypes[e.CameraIndex] != 0)
            {
                Bitmap bmp = imageDatas[e.CameraIndex].ToBitmap();
                bmp.RotateFlip(rotateFlipTypes[e.CameraIndex]);
                imageDatas[e.CameraIndex] = new ImageData();
                imageDatas[e.CameraIndex].FromBitmap(bmp);
            }

            mreGrab[e.CameraIndex].Set();
            //如果是实时模式
            if (isLiving[e.CameraIndex])
            {
                showLiveImageMethods[e.CameraIndex]?.Invoke(imageDatas[e.CameraIndex]);
            }
        }

        /// <summary>
        /// 拍照
        /// </summary>
        /// <param name="index">相机序号</param>
        /// <returns></returns>
        public static ImageData Grab(int index)
        {
            mreGrab[index].Reset();
            isGrabing[index] = true;
            imageDatas[index] = null;
            cameras[index].SoftTrigger();
            mreGrab[index].WaitOne(1000);
            isGrabing[index] = false;

            return imageDatas[index];
        }

        /// <summary>
        /// 实时
        /// </summary>
        /// <param name="index">相机序号</param>
        /// <param name="isSetToLive">是否设置成实时模式</param>
        /// <param name="method">显示图像的委托</param>
        public static void Live(int index, bool isSetToLive, ShowLiveImage method)
        {
            try
            {
                isLiving[index] = isSetToLive;
                showLiveImageMethods[index] = method;
                if (isSetToLive)
                {
                    //cameras[index].AcquisitionStop();
                    cameras[index]?.SetTriggerMode(TriggerMode.Continous);
                    //cameras[index].AcquisitionStart();
                }
                else
                {
                    //cameras[index].AcquisitionStop();
                    cameras[index]?.SetTriggerMode(TriggerMode.Software);
                    // cameras[index].AcquisitionStart();
                }
            }
            catch (Exception ex)
            {
                //ViewStatus.Instance.ShowMsg($"对位相机{index + 1  }Live失败，异常信息：{ex.Message}", Brushes.Red);
            }
        }

        /// <summary>
        /// 设置曝光时间
        /// </summary>
        /// <param name="index">相机序号</param>
        /// <param name="millisecond">曝光值，毫秒</param>
        /// <returns></returns>
        public static void SetExposureTime(int index, float millisecond)
        {
            try
            {
                bool ret = cameras[index].SetExposure(millisecond);
                if (ret == false)
                {
                    cameras[index].SetFloatValue("ExposureTimeAbs", millisecond * 1000);
                }
            }
            catch (Exception ex)
                    {
               // ViewStatus.Instance.ShowMsg($"对位相机{index+1  }设置曝光失败，异常信息：{ex.Message}",Brushes.Red);
            }
        }

       

        /// <summary>
        /// 获取指定Mac地址的相机
        /// </summary>
        /// <param name="macAddr">Mac地址</param>
        /// <returns></returns>
        public static IHTCamera GetCamera(string macAddr)
        {
            try
            {
                return cameras.Where(p => p.MacAddr.ToUpper() == macAddr.ToUpper()).FirstOrDefault();
            }
            catch (Exception ex)
            {
               // ViewStatus.Instance.ShowMsg($"获取对位相机{macAddr }失败，异常信息：{ex.Message}", Brushes.Red);
                return null;
            }
        }
        public static IHTCamera GetCamera(int camIndex)
        {
            try
            {
                return cameras.Where(p => p.CameraIndex==camIndex).FirstOrDefault();
            }
            catch (Exception ex)
            {
               // ViewStatus.Instance.ShowMsg($"获取对位相机{camIndex }失败，异常信息：{ex.Message}", Brushes.Red);
                return null;
            }
        }
        /// <summary>
        /// 获取指定Mac地址的相机的序号
        /// </summary>
        /// <param name="macAddr">Mac地址</param>
        /// <returns></returns>
        public static int GetCameraIndex(string macAddr)
        {
            int index = -1;
            for (int i = 0; i < cameras.Length; i++)
            {
                if (cameras[i] != null && cameras[i].MacAddr.ToUpper() == macAddr.ToUpper())
                {
                    index = i;
                    break;
                }
            }
            return index;
        }

        /// <summary>
        /// 断开连接，释放资源
        /// </summary>
        public static void UnInit()
        {
            try
            {
                for (int i = 0; i < CameraCount; i++)
                {
                    if (cameras != null)
                    {
                        //--------------------------//
                        ///Add By miaoliang
                        cameras[i].SoftTrigger(); 
                        bool ret =cameras[i].AcquisitionStop();
                        //--------------------------//
                        cameras[i].Dispose();

                    }
                }
            }
            catch (Exception ex)
            { }
        }
    }
}
