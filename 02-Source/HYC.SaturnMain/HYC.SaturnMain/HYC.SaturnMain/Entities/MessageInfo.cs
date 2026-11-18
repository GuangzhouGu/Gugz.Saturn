// Copyright (C)  Suzhou HYC Technology Co.,LTD
// 版权所有。
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnMain.Entities
// 文件名称    ：MessageInfo.cs
// =================================
// 创 建 者    ：guguangzhou
// 创建日期    ：2021/01/29 09:00:00 
// 功能描述    ：枚举帮助类
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
using HYC.HTPCBase.Extension.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYC.SaturnMain.Entities
{
    public class MessageInfo
    {
        /// <summary>
        /// 命令类型
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
        public byte[] Datas { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public CommunicationType Type { get; set; }
        public override string ToString()
        {
            return PCName.ToString() + "_" + Command.ToString() + "_" + Message + "_" + Type.ToString();
        }
        public static MessageInfo GetInfo(string msg)
        {
            try
            {
                string[] temp = msg.Split('_');
                MessageInfo info = new MessageInfo();
                info.PCName = temp[0];
                info.Command = EnumHelper.GetEnumValue<Command>(temp[1]);
                info.Message = temp[2];
                info.Type = EnumHelper.GetEnumValue<CommunicationType>(temp[3]);
                return info;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
