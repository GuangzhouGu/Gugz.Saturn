/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2021 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：CommonLib.Device
// 文件名称    ：IDevice.cs
// =================================
// 创 建 者    ：guguangzhou
// 创建日期    ：2021/11/11 09:00:00 
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
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Device
{
    public interface IDevice
    {
        string DeviceName { get; set; }
        /// <summary>
        /// 初始化
        /// </summary>
        void Init();
        /// <summary>
        /// 反初始化
        /// </summary>
        void Uninit();
        /// <summary>
        /// 连接事件，第一个参数是硬件名称，第二个参数是硬件序号，第三个参数是连接状态
        /// </summary>
        event Action<string,int, bool> OnConnected;//连接状态事件
        /// <summary>
        /// 消息显示事件，第一个参数是硬件名称，第二个参数是消息体，第三个参数是消息颜色
        /// </summary>
        event Action<string, string, Brush> OnShowMsg;//信息交互事件
        /// <summary>
        ///初始化结果
        /// </summary>
        /// <param name="ret"></param>
        void InitRet(bool ret);
        /// <summary>
        /// 是否初始化
        /// </summary>
        bool IsInit { get; set; }

    }
}
