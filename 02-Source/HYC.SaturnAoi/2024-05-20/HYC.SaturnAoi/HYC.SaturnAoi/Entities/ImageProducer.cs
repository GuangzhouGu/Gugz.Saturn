/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2023 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnAoi.Entities
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
using HYC.HTCamera.Card.Matrox;
using HYC.SaturnAoi.Devices.MuraCamera;
using HYC.SaturnAoi.Params;
using HYC.StandardAoi.Interface;
using HYC.StandardAoi.Parameter;
using HYC.TPApiUnit.Devices.AoiCamera;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYC.SaturnAoi.Entities
{
    internal class ImageProducer : IImageProducer
    {
        public int Channel { get; set; }
        public int CameraIndex { get; set; }
        public ImageProducer(int channel, int cameraIndex)
        {
            this.Channel = channel;
            this.CameraIndex = cameraIndex;
        }
      
        /// <summary>
        /// 获取图像数据
        /// </summary>
        /// <param name="pattName">画面名称</param>
        /// <returns>图形数据</returns>
        public ImageData CreateImageData(string pattName)
        {
            try
            {
                ImageData imageData = null;
                if (CameraIndex == 0)
                {
                    if (Param.System.IsUseLocalImage)
                    {
                        imageData = ImageData.ConvertFromFile($"E:\\TestImage\\Aoi\\{pattName}.bmp");
                    }
                    else
                    {
                        imageData= CaptureCardHelper.Instance.GetCard(CameraIndex).Grab2d(0);
                    }
                }
                else if (CameraIndex == 1|| CameraIndex == 2)
                {
                    if (Param.System.IsUseLocalImage)
                    {
                        imageData = ImageData.ConvertFromFile($"E:\\TestImage\\DirectMura\\{pattName}.bmp");
                    }
                    else
                    {
                        imageData = CameraHelper.Instance.GetCamera(CameraIndex - 1).Grab();
                    }

                }
            
                return imageData;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 设置曝光时间
        /// </summary>
        /// <param name="millisecond">曝光时间，毫秒</param>
        /// <returns>true:成功；false:失败</returns>
        public bool SetExposureTime(float millisecond)
        {
            bool ret = false;
            if (CameraIndex == 0)
            {
                ret = CaptureCardHelper.Instance.GetCard(CameraIndex).SetExposureTime(CameraIndex, millisecond);
            }
            else if (CameraIndex == 1)
            {
                ret = CameraHelper.Instance.GetCamera(CameraIndex-1).SetExposureTime(millisecond);
            }
            else if (CameraIndex == 2)
            {
                ret = CameraHelper.Instance.GetCamera(CameraIndex-1).SetExposureTime(millisecond);
            }
            
            return ret;
        }

        /// <summary>
        /// 设置增益
        /// </summary>
        /// <param name="value">增益值</param>
        /// <returns>true:成功；false:失败</returns>
        public bool SetGain(float value)
        {
            bool ret = false;
            if (CameraIndex == 0)
            {
                ret = CaptureCardHelper.Instance.GetCard(CameraIndex).SetGain(CameraIndex, value);
            }
            else if (CameraIndex == 1)
            {
                ret = CameraHelper.Instance.GetCamera(CameraIndex - 1).SetGain(value);
            }
            return ret;
        }

        public bool SetGamma(bool isEnabled, float value)
        {
            return true;
        }
    }
}
