/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2023 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnAoi.Devices.Client
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
using CommonLib.Entities;
using HYC.HTPCBase.Extension.Entities;
using HYC.SaturnAoi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYC.SaturnAoi.Devices.Client
{
    public class MessageInfo
    {
        /// <summary>
        /// PC名称
        /// </summary>
        public string PCName { get; set; }
        /// <summary>
        /// 命令类型
        /// </summary>
        public Command Command { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public CommunicationType Type { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public byte[] Datas { get; set; }
        public override string ToString()
        {
            return Command.ToString() + "_" + Message;
        }
        public static MessageInfo GetInfo(string msg)
        {
            try
            {
                string[] temp = msg.Split('_');
                MessageInfo info = new MessageInfo();
                info.Command = EnumHelper.GetEnumValue<Command>(temp[0]);
                info.Message = temp[1];
                return info;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
