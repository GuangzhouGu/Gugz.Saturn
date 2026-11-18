/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2021 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.Mt2Aoi.Devices
// 文件名称    ：CameraControl.cs
// =================================
// 创 建 者    ：guguangzhou
// 创建日期    ：2022/01/08 09:00:00 
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
using HYC.HTCamera.GigE;
using HYC.HTImage;
using HYC.SaturnAoi.Entities;
using HYC.StandardAoi.Extentions;
using HYC.StandardAoi.Parameter;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HYC.SaturnAoi.Devices.MuraCamera
{
    public class CameraControl
    {   //连接状态事件
        public event Action<string, int, bool> OnConnected;
        //消息提示事件
        public event Action<string, string, Brush> OnShowMsg;
        //显示图像事件
        public Action<Bitmap> OnImageReceived;
        //mura 相机实例
        IHTCamera Camera = null;
        //相机信息
        public CameraInfo Info { get; set; }
        //连接状态
        public bool IsConnected { get; set; }
        //拍照等待信号
        ManualResetEvent Mre = null;
        //图像
        //ImageData ImageDatas = null;
        Bitmap CurrentBmp = null;
        TriggerMode TriggerMode;
        bool IsGrabing = false;//拍照
        bool IsLiving = false;//实时
        public int StationNum { get; set; }
        public CameraControl(CameraInfo info)
        {
            try
            {
                Mre = new ManualResetEvent(false);
                Camera = HTCameraFactory.CreateCamera(info.CameraVender);
                Info = info;
                Camera.MacAddr = info.MacAddress;
                Camera.Name = info.CameraName;
                Camera.CameraIndex = info.CameraNum;
                Camera.IsAutoConnectMode = false;
                Camera.ConnectionChanged += ConnectHandle;
                Camera.ShowMsg += ShowMsgHandle;
                Camera.ImageReceived += ImageReceivedHandle;
                Global.ImageProducers.Add(new ImageProducer(info.CameraNum, info.CameraNum+1));
            }
            catch (Exception ex)
            {
                OnShowMsg?.Invoke($"MuraCamera", $"CameraControl->{info.CameraName}连接异常，异常信息：{ex.ToString()}",Brushes.Red);
                OnConnected?.Invoke(info.CameraName, Info.CameraNum,false);
            }
        }
        /// <summary>
        /// 连接相机
        /// </summary>
        public  void Connect()
        {
            var t = new Thread(() => {
                while (true)
                {
                    if (Camera!=null&&Camera.IsConnected==false)
                    {
                        Camera.Init();
                    }
                    Thread.Sleep(3000);
                }
            });
            t.IsBackground = true;
            t.Start();
        }
        private void ConnectHandle(object sender, HTCameraEventArgs e)
        {
            OnConnected?.Invoke($"{Info.CameraName}", e.CameraIndex, e.IsConnected);
            IsConnected = e.IsConnected;
        }
        private void ShowMsgHandle(object sender, HTCameraShowMsgEventArgs e)
        {
            //OnShowMsg?.Invoke($"MuraCamera{(e.CameraIndex + 1).ToString("00")}", e.Text,Brushes.Green);
        }
        private void ImageReceivedHandle(object sender, HTCameraEventArgs e)
        {
            Bitmap bitmap = null;
            if (Info.IsColor)
            {
                bitmap = ArrayToBitmap(e.Data, e.Height, e.Width);
            }
            else
            {
                bitmap = new ImageData
                {
                    ImageBytes = e.Data,
                    Width = e.Width,
                    Height = e.Height
                }.ToBitmap();
            }
            if (TriggerMode == TriggerMode.Software)
            {
                CurrentBmp = (Bitmap)bitmap.Clone();
                Mre.Set();
            }
            Mre.Set();
            OnImageReceived?.Invoke(bitmap);
        
        }
        private Bitmap ArrayToBitmap(byte[] data, int height, int width)
        {
            return BitmapConverter.ArrayToRgbBitmap(data, height, width);
        }
        private Bitmap ConvertYUV422ToRGB(byte[] data, int width, int height)
        {
            TimeSpan ts1 = new TimeSpan(DateTime.Now.Ticks);
            byte[] RGBData = new byte[height * width * 3];
            Bitmap colorBitmap = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            ParallelLoopResult result = Parallel.For(0, height, new ParallelOptions() { MaxDegreeOfParallelism = 40 }, i =>
            {
                byte[] rowData = new byte[width * 2];
                Array.Copy(data, i * width * 2, rowData, 0, width * 2);
                for (int index = 0, outIndex = 0; index < rowData.Length; index += 4)
                {
                    int U = rowData[index];
                    int Y1 = rowData[index + 1];
                    int V = rowData[index + 2];
                    int Y2 = rowData[index + 3];
                    int Cb = U;
                    int Cr = V;
                    int R1 = (int)(Y1 + 1.402 * (Cr - 128));
                    int G1 = (int)(Y1 - 0.344 * (Cb - 128) - 0.714 * (Cr - 128));
                    int B1 = (int)(Y1 + 1.772 * (Cb - 128));

                    int R2 = (int)(Y2 + 1.402 * (Cr - 128));
                    int G2 = (int)(Y2 - 0.344 * (Cb - 128) - 0.714 * (Cr - 128));
                    int B2 = (int)(Y2 + 1.772 * (Cb - 128));

                    RGBData[i * width * 3 + outIndex++] = (byte)B1;
                    RGBData[i * width * 3 + outIndex++] = (byte)G1;
                    RGBData[i * width * 3 + outIndex++] = (byte)R1;
                    RGBData[i * width * 3 + outIndex++] = (byte)B2;
                    RGBData[i * width * 3 + outIndex++] = (byte)G1;
                    RGBData[i * width * 3 + outIndex++] = (byte)R2;
                }
            });
            TimeSpan ts2 = new TimeSpan(DateTime.Now.Ticks);
            TimeSpan ts3 = ts1.Subtract(ts2).Duration();
            string costTime = ts3.TotalMilliseconds.ToString();
            bool ret = result.IsCompleted;
            BitmapData bitmapData = colorBitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, colorBitmap.PixelFormat);
            Marshal.Copy(RGBData, 0, bitmapData.Scan0, RGBData.Length);
            colorBitmap.UnlockBits(bitmapData);
            return colorBitmap;
        }
        public ImageData Grab()
        {
            Grab(3000,out Bitmap imageData);
            return ImageData.ConvertFromBitmap(imageData,Info.IsColor);
        }
        public bool Grab(int timeout, out Bitmap bmp)
        {
            try
            {
                Camera.AcquisitionStart();
                Camera.SetTriggerMode(TriggerMode.Software);
                TriggerMode = TriggerMode.Software;
                bool ret = false;
                Mre.Reset();
                ret = Camera.SoftTrigger();
                ret = Mre.WaitOne(timeout);
                if (ret)
                {
                    bmp = (Bitmap)CurrentBmp.Clone();
                }
                else
                {
                    bmp = null;
                }
                return ret;
            }
            catch (Exception ex)
            {
                OnShowMsg?.Invoke("CameraControl", $"Grab方法执行异常，异常信息：{ex.Message}", Brushes.Red);
                bmp = null;
                return false;
            }
        }
        public bool Continues()
        {
            try
            {
                IsLiving = true;
                TriggerMode = TriggerMode.Continous;
                bool ret = Camera.SetTriggerMode(TriggerMode.Continous);
                return ret;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool Stop()
        {
            try
            {
                IsLiving = false;
                bool ret = Camera.SetTriggerMode(TriggerMode.Software);
                return ret;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public  bool SetExposureTime(float value,bool isSave=false)
        {
            bool ret=Camera.SetExposure(value);
            if (isSave)
            {
                CameraInfo info = CameraHelper.Instance.GetCameraInfoByNum(Info.CameraNum);
               // info.ExposureTime = value;
                //CameraHelper.Instance.SaveCameraConfig();
            }
            return ret;
        }
        public  float GetExposureTime()
        {
            float value = 0;
            Camera.GetExposure(ref value);
            return value;
        }
        public double GetGain()
        {
            double value =Camera.GetFloatValue("Gain");
            return value;
        }
        public bool SetGain(double value)
        {
             Camera.SetFloatValue("Gain", value);
            return true;
        }
        public  bool Uninit()
        {
            Camera?.AcquisitionStop();
            Camera?.Dispose();
            return true;
        }
      
    }
}
