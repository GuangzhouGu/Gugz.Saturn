using HYC.HTCamera.GigE;
using HYC.HTFile.HTXML;
using HYC.HTLog;
using HYC.HTPCBase.Extension.Device;
using HYC.MicroLedATDetect.Devices.HTCameras;
using HYC.MicroLedATDetect.Devices.MonitorCamera;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;


public class HTCameraHelper : IDevice
{
    public static HTCameraHelper Instance { get { return DeviceManager.Instance.Get("HTCamera") as HTCameraHelper; } }
    public string DeviceName { get; set; } = "HTCamera";

    public event Action<string, int, bool> OnConnected;
    public event Action<string, string, Brush> OnShowMsg;
    public List<HTCamera> Cameras=new List<HTCamera>();
    List<HTCameraInfo> Infos = null;
    public int CameraCount = 0;
    public void Init()
    {
        try
        {
            string ConfigPath = AppDomain.CurrentDomain.BaseDirectory + @"Config\Hardware\HTCameraInfo.xml";
            Infos = HTXMLSerializer.EntityFromXMLFile<List<HTCameraInfo>>(ConfigPath);
            if (Infos != null)
            {
                CameraCount = Infos.Count;
                for (int i = 0; i < Infos.Count; i++)
                {
                    HTCamera camera = new HTCamera(Infos[i]);
                    camera.OnConnected += OnConnected;
                    camera.OnShowMsg += ShowMsg;
                    camera.Init();
                    Cameras.Add(camera);
                    Thread.Sleep(200);
                }
            }
        }
        catch (Exception ex)
        {
            OnShowMsg?.Invoke(DeviceName, ex.Message, Brushes.Red);
        }
    }
    public HTCamera GetCamera(int index)
    {
        try
        {
            if (Cameras.Count > 0)
            {
                return Cameras[index];
            }
            else
            {
                return null;
            }
        }
        catch (Exception ex)
        {
            ShowMsg($"GetCamera异常，异常信息：{ex.Message}", Brushes.Red);
            return null;
        }
    }
    private void ShowMsg(string msg,Brush bh)
    {
        OnShowMsg?.Invoke("HTCamera",msg,bh);
    }
    public  bool SetExposure(int num,float millisecond)
    {
        try
        {
            return Cameras[num].SetExposure(millisecond);
        }
        catch (Exception ex)
        {
            ShowMsg($"SetExposure异常，异常信息：{ex.Message}",Brushes.Red);
            return false;
        }
    }
    public double GetExposure(int num)
    {
        return Cameras[num].GetExposure()/1000;
    }

    public Bitmap Grab(int num,int timeout)
    {
        if (Cameras.Count > 0)
        {
            Bitmap bitMap = null;
            Cameras[num].Grab(timeout, out bitMap);
            return bitMap;
        }
        else
        {
            return null;
        }
      
    }
    public bool Continues(int num)
    {
        try
        {
            if (Cameras.Count > 0)
            {
                return Cameras[num].Continues();
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    public bool Stop(int num)
    {
        if (Cameras.Count > 0)
        {
            return Cameras[num].Stop();
        }
        else
        {
            return false;
        }
    }
    public void InitRet(bool ret)
    {
    }

    public void Uninit()
    {
        if (Cameras!=null)
        {
            Cameras.Select(p => { p.Stop(); p.UnInit();return true; }).ToArray();
        }
    }
}