/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2022 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：TPAssembly.Devices.Camera
// 文件名称    ：CameraHelper.cs
// =================================
// 创 建 者    ：guguangzhou
// 创建日期    ：2021/05/08 09:00:00 
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
using HYC.HTFile.HTXML;
using HYC.StandardAoi.Extentions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HYC.SaturnAoi.Devices.MuraCamera
{
    public class CameraHelper:IDevice
    {
        //连接状态事件
        public event Action<string, int, bool> OnConnected;
        //消息提示
        public event Action<string, string, Brush> OnShowMsg;
        //继承了IDevice 的mura 相机实例
        public static CameraHelper Instance { get { return DeviceManager.Instance.Get("MuraCamera") as CameraHelper; } }
        // mura 相机配置信息存放地址
        private string CameraConfigPath= AppDomain.CurrentDomain.BaseDirectory + @"\Config\Hardware\CameraInfo.xml";
        //存放相机实例
        public Dictionary<int, CameraControl> CameraDic = null;
        //相机配置信息
        List<CameraInfo> CameraInfoList = null;
        public string DeviceName { get; set; } = "MuraCamera";
        //相机数目
        public int CameraCount { get { return CameraDic.Keys.Count; } }

        public bool IsInit { get; set; }
        // mura初始化
        public void Init()
        {
            try
            {
                CameraDic = new Dictionary<int, CameraControl>();
                CameraInfoList = GetCameraConfig();
                if (CameraInfoList != null && CameraInfoList.Count > 0)
                {
                    for (int i = 0; i < CameraInfoList.Count; i++)
                    {
                        CameraControl camera = new CameraControl(CameraInfoList[i]);
                        camera.OnConnected += OnConnected;
                        camera.OnShowMsg += OnShowMsg;
                        camera.Connect();
                        CameraDic.Add(i, camera);
                        Thread.Sleep(2000);
                    }
                }
            }
            catch (Exception ex)
            {
                OnShowMsg?.Invoke(DeviceName, $"CameraHelper->Init执行异常，异常信息：{ex}", Brushes.Red);
            }
        }
      
        /// <summary>
        /// 点检选择不同相机获取相机实例
        /// </summary>
        /// <param name="index">下拉选择相机号</param>
        /// <returns></returns>
        public CameraControl GetCamera(int index)
        {
            try
            {
                return CameraDic.Values?.Where(p => p.Info.CameraNum == index).First();
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// 根据索引获取不同的mura 相机实例
        /// </summary>
        /// <param name="cameraIndex"></param>
        /// <returns></returns>
        public CameraInfo GetCameraInfoByNum( int cameraIndex)
        {
            try
            {
                var camera = CameraInfoList.Where(p => p.CameraNum == cameraIndex).First();
                return camera;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// 释放mura 相机连接
        /// </summary>
        /// <returns></returns>
        public void Uninit()
        {
            CameraDic?.Values.Select(p => p.Uninit()).ToArray();
        }
        /// <summary>
        /// 读取相机配置
        /// </summary>
        /// <returns></returns>
        private List<CameraInfo> GetCameraConfig()
        {
            List<CameraInfo> result = HTXMLSerializer.EntityFromXMLFile<List<CameraInfo>>(CameraConfigPath);
            return result;
        }
        /// <summary>
        /// 读取相机配置
        /// </summary>
        /// <returns></returns>
        public bool SaveCameraConfig()
        {
             bool ret=HTXMLSerializer.EntityToXMLFile(CameraInfoList,CameraConfigPath);
            return ret;
        }

        public void InitRet(bool ret)
        {

        }
    }
}
