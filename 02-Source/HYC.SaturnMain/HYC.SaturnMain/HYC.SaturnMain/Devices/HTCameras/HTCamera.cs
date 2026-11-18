using HYC.HTCamera.GigE;
using HYC.HTImage;
using HYC.MicroLedATDetect.Devices.MonitorCamera;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HYC.MicroLedATDetect.Devices.HTCameras
{
    public class HTCamera
    {
        public event Action<string, Brush> OnShowMsg; //显示通信信息记录
        public event Action<string, int, bool> OnConnected;
        public Action< Bitmap> OnImageReceived;

        IHTCamera Camera = null;
        ManualResetEvent Mre = new ManualResetEvent(false);
        TriggerMode TriggerMode;
         Bitmap CurrentBmp = null;
        public Action<bool> OnCameraStop;
        HTCameraInfo Info = null;
        bool IsConnected = false;
        bool IsAutoConnect = true;
        public HTCamera(HTCameraInfo info)
        {
            Info = info;
            Camera = HTCameraFactory.CreateCamera(Info.Vendor);
            Camera.MacAddr = Info.MacAddr;
            Camera.ImageReceived += Camera_ImageReceived;
            Camera.ConnectionChanged += Camera_ConnectionChanged;
            //Camera.ShowMsg += Camera_ShowMsg;
        }

        /// <summary>
        /// 连接
        /// </summary>
        public void Init()
        {
            var t = new Thread(() => {
                while (true)
                {
                    if (IsAutoConnect)
                    {
                        if (!IsConnected)
                        {
                            try
                            {
                                bool ret=Camera.Init();
                                if (ret)
                                {
                                    SetExposure(Info.ExposureTime);
                                }
                               
                            }
                            catch (Exception ex)
                            {
                               // OnShowMsg?.Invoke($"HTCamera->Init方法执行异常，异常信息：{ex.Message}", Brushes.Red);
                                OnConnected?.Invoke(Info.CameraName, Info.CameraNum, false);
                            }
                        }
                    }
                    Thread.Sleep(5000);
                }
            });
            t.IsBackground = true;
            t.Start();
        }
        public void ManualInit()
        {
            try
            {
                if (IsAutoConnect)
                {
                    MessageBox.Show("请先断开相机");
                    return;
                }
                Camera = HTCameraFactory.CreateCamera(Info.Vendor);
                Camera.MacAddr = Info.MacAddr;
                Camera.ImageReceived += Camera_ImageReceived;
                Camera.ConnectionChanged += Camera_ConnectionChanged;
                Camera.ShowMsg += Camera_ShowMsg;
                Camera.Init();
            }
            catch (Exception ex)
            {
                OnShowMsg?.Invoke($"HTCamera->ManualInit方法执行异常，异常信息：{ex.Message}", Brushes.Red);
                OnConnected?.Invoke($"HTCamera{(Info.CameraNum + 1).ToString("00")}", Info.CameraNum, false);
            }
        }
        private void Camera_ShowMsg(object sender, HTCameraShowMsgEventArgs e)
        {
            OnShowMsg?.Invoke( e.Text, e.IsError ? Brushes.Red : Brushes.Green);
        }

        private void Camera_ConnectionChanged(object sender, HTCameraEventArgs e)
        {
            IsConnected = e.IsConnected;
            OnConnected?.Invoke($"HTCamera{e.CameraIndex+1}", e.CameraIndex, e.IsConnected);
        }
        object obj = new object();
        private void Camera_ImageReceived(object sender, HTCameraEventArgs e)
        {
            int width = e.Width;
            int height = e.Height;
            Bitmap bitmap = ArrayToBitmap(e.Data, e.Height, e.Width);
            if (TriggerMode == TriggerMode.Software)
            {
                CurrentBmp = (Bitmap)bitmap.Clone();
                Mre.Set();
            }
          OnImageReceived?.Invoke(bitmap);
        }

        public bool SetExposure(float millisecond)
        {
            Camera.SetFloatValue("ExposureTimeAbs", millisecond*1000);
            return true;
        }
        public double GetExposure()
        {
            double time = Camera.GetFloatValue("ExposureTimeAbs");
            return time;
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
                OnShowMsg?.Invoke($"Grab方法执行异常，异常信息：{ex.Message}", Brushes.Red);
                bmp = null;
                return false;
            }
        }

        public  bool Continues()
        {
            try
            {
                Camera.AcquisitionStart();
                TriggerMode = TriggerMode.Continous;
                 bool ret = Camera.SetTriggerMode(TriggerMode.Continous);
                if (ret == false)
                {
                    OnShowMsg?.Invoke($"SetTriggerMode(TriggerMode.Continous)返回值失败", Brushes.Red);
                }
                return ret;
            }
            catch (Exception ex)
            {
                OnShowMsg?.Invoke($"Continues方法执行异常，异常信息：{ex.Message}", Brushes.Red);
                return false;
            }
        }
        public  bool Stop()
        {
            try
            {
                bool ret = Camera.AcquisitionStop();
                ret = Camera.SetTriggerMode(TriggerMode.Software);
                //ret = Camera.AcquisitionStart();
                //OnCameraStop?.Invoke(true);
                return ret;
            }
            catch (Exception ex)
            {
                OnShowMsg?.Invoke($"Stop方法执行异常，异常信息：{ex.Message}", Brushes.Red);
                return false;
            }
        }
        private Bitmap ArrayToBitmap(byte[] data, int height, int width)
        {
            //return ConvertYUV422ToRGB(data, width, height);
            return BitmapConverter.ArrayToRgbBitmap(data,height,width);
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
        public bool UnInit()
        {
            try
            {
                IsAutoConnect = false;
                OnConnected?.Invoke($"HTCamera{(Info.CameraNum + 1).ToString("00")}", Info.CameraNum, false);
                IsConnected = false;
                Camera.AcquisitionStop();
                Camera.Dispose();
            }
            catch (Exception ex)
            {
                OnShowMsg?.Invoke($"UnInit方法执行异常，异常信息：{ex.Message}", Brushes.Red);
            }
            return true;
        }
    }
}
