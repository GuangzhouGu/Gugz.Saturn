using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYC.MicroLedATDetect.Devices.MonitorCamera
{
    public class HTCameraInfo
    {
        public int CameraNum { get; set; }
        public string CameraName { get; set; }
        public string MacAddr { get; set; }
        public string Vendor { get; set; }
        public float ExposureTime { get; set; }
    }
}
