/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2020 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnMain.Params
// 文件名称    ：CimParamaters.cs
// =================================
// 创 建 者    ：guguangzhou
// 创建日期    ：2021/01/29 09:00:00 
// 功能描述    ：Cim参数
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
using HYC.HTPCBase.Extension.Para;
using HYC.HTPCBase.Extension.Views;
using HYC.SaturnMain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HYC.SaturnMain.Params
{
    public class CimPara : ParaBase,IPara
    {
        [System.Xml.Serialization.XmlIgnore]
        [Browsable(false)]
        public bool IsShow { get; set; } = true;
        [System.Xml.Serialization.XmlIgnore]
        [Browsable(false)]
        public ParaViewBase ParaViewBase { get; set; } = null;
        [System.Xml.Serialization.XmlIgnore]
        [Browsable(false)]
        public string ParaName { get; set; } = "Cim参数";
        [Category("功能屏蔽"), Description("Cim屏蔽")]
        public bool CimShield { get; set; }
        [Category("模式设置"), Description("软件开启时切到CimON模式")]
        public bool IsCimON { get; set; }
        [Category("延时时间"), Description("本机LinkSignal延时时间，单位秒")]
        public double DelayTime { get; set; }

        [Category("超时时间"), Description("等待上游SendAble置on超时时间，单位秒")]
        public double WaitUpSendableOnTime { get; set; }
        [Category("超时时间"), Description("等待上游SendStart置on超时时间，单位秒")]
        public double WaitUpSendStartOnTime { get; set; }
        [Category("超时时间"), Description("等待上游JobTransferSignal置On超时时间，单位秒")]
        public double WaitUpJobTransferSignalOnTime { get; set; }
        [Category("超时时间"), Description("等待上游SendComplete置on超时时间，单位秒")]
        public double WaitUpSendCompleteOnTime { get; set; }
        [Category("超时时间"), Description("等待上游SendComplete置off超时时间，单位秒")]
        public double WaitUpSendCompleteOffTime { get; set; }


        [Category("超时时间"), Description("等待下游ReceiveComplete置on超时时间，单位秒")]
        public double WaitDownReceiveCompleteOnTime { get; set; }
        [Category("超时时间"), Description("等待下游ReceiveComplete置off超时时间，单位秒")]
        public double WaitDownReceiveCompleteOffTime { get; set; }
        [Category("超时时间"), Description("等待下游InitAck信号置on超时时间，单位秒")]
        public double WaitDownInitAckTime { get; set; }
 
        [Category("Cim配置"), Description("等待下游JobTransferSignal信号on超时时间，单位秒")]
        public double WaitDownJobTransferSignalOnTime { get; set; }
        [Category("超时时间"), Description("等待下游ReceiveAble置on超时时间，单位秒")]
        public double WaitDownReceiveAbleOnTime { get; set; }
        [Category("超时时间"), Description("等待下游ExchangeExecute超时时间，单位秒")]
        public double WaitDownExchangeExecuteTime { get; set; }
        public IPara LoadPara()
        {
            return Load(this);
        }
        public bool SavePara()
        {
            return Save(this);
        }
    }
}
