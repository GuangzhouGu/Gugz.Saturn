/*----------------------------------------------------------------
// Copyright (C) Suzhou HYC Electronic Technology Co.,LTD
// 版权所有。
//
// =================================
// CLR版本     ：4.0.30319.42000
// 命名空间    ：HYC.AutoPosition.Helper
// 文件名称    ：LanguageHelper.cs
// =================================
// 创 建 者    ：yangkang
// 创建日期    ：2020/7/11 16:42:45 
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
using HYC.HTFile.HTXML;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HYC.AutoPosition.Helper
{
    class LanguageHelper
    {
        /// <summary>
        /// 消息文本定义
        /// </summary>
        public static MsgDef Msg = new MsgDef();

        /// <summary>
        /// 修改语言
        /// </summary>
        /// <param name="lang">待设置语言</param>
        public static void SetLanguage(string lang)
        {
            if (lang != "zh-CN")
            {
                string folder = $"Language\\{lang}\\";
                string filename = folder + "HYC.AutoPosition.lang.xml";
                if (File.Exists(filename))
                {
                    Msg = HTXMLSerializer.EntityFromXMLFile<MsgDef>(filename);
                }
                else
                {
                    if (lang == "en-US")
                    {
                        Msg.SetToEnglish();
                    }
                    if (!Directory.Exists(folder))
                        Directory.CreateDirectory(folder);
                    HTXMLSerializer.EntityToXMLFile(Msg, filename);
                }
            }
            else
            {
                Msg = new MsgDef();
            }
        }
    }

    /// <summary>
    /// 消息文本，默认语言
    /// </summary>
    public class MsgDef
    {
        /// <summary>
        /// 自动对位开始
        /// </summary>
        public string AutoPosStarted = "工位{0}自动对位开始";
        /// <summary>
        /// 自动对位结束
        /// </summary>
        public string AutoPosCompleted = "工位{0}自动对位结束，结果{1}，耗时{2}";
        /// <summary>
        /// 自动对位异常
        /// </summary>
        public string AutoPosError = "工位{0}自动对位异常";

        /// <summary>
        /// 自动标定开始
        /// </summary>
        public string CalibrationStarted = "工位{0}自动标定开始";
        /// <summary>
        /// 自动标定结果
        /// </summary>
        public string CalibrationResult = "工位{0}自动标定结果：{1}";
        /// <summary>
        /// 自动标定结束
        /// </summary>
        public string CalibrationCompleted = "工位{0}自动标定结束，结果{1}，耗时{2}";
        /// <summary>
        /// 自动标定异常
        /// </summary>
        public string CalibrationError = "工位{0}自动标定异常";

        /// <summary>
        /// 调整前
        /// </summary>
        public string BeforeAdjust = "工位{0}第{1}次调整前，";
        /// <summary>
        /// 调整后
        /// </summary>
        public string AfterAdjust = "工位{0}第{1}次调整后，";
        /// <summary>
        /// 运动失败
        /// </summary>
        public string MoveFailed = "工位{0}第{1}次调整运动失败";
        /// <summary>
        /// Theta运动跳过
        /// </summary>
        public string ThetaMoveSkip = "工位{0}第{1}次调整Theta轴已到位，运动跳过";
        /// <summary>
        /// Theta运动失败
        /// </summary>
        public string ThetaMoveFailed = "工位{0}第{1}次调整Theta轴运动失败";
        /// <summary>
        /// XY运动失败
        /// </summary>
        public string XYMoveFailed = "工位{0}第{1}次调整XY轴运动失败";
        /// <summary>
        /// Theta调整后
        /// </summary>
        public string AfterThetaAdjust = "工位{0}第{1}次调整Theta后，";

        /// <summary>
        /// 获取图像失败
        /// </summary>
        public string GetImagesFailed = "工位{0}第{1}次调整获取图像失败";

        /// <summary>
        /// 运动距离超限制
        /// </summary>
        public string DistanceOverLimit = "工位{0}调整时运动距离超限制";

        /// <summary>
        /// 设置成英文
        /// </summary>
        public void SetToEnglish()
        {
            AutoPosStarted = "Station {0} AutoPos start";
            AutoPosCompleted = "Station {0} AutoPos completed, result {1}, time {2}";
            AutoPosError = "Station {0} AutoPos abnormally completed";

            CalibrationStarted = "Station {0} Calibration start";
            CalibrationResult = "Station {0} Calibration result: {1}";
            CalibrationCompleted = "Station {0} Calibration completed, result {1}, time {2}";
            CalibrationError = "Station {0} Calibration abnormally completed";

            BeforeAdjust = "Station {0} Times {1} before adjust, ";
            AfterAdjust = "Station {0} Times {1} after adjust, ";
            MoveFailed = "Station {0} Times {1} move failed";
            ThetaMoveSkip = "Station {0} Times {1} Theta is in position, move skiped";
            ThetaMoveFailed = "Station {0} Times {1} Theta move failed";
            XYMoveFailed = "Station {0} Times {1} XY move failed";
            AfterThetaAdjust = "Station {0} Times {1} after Theta adjust, ";

            GetImagesFailed = "Station {0} Times {1} get images failed";

            DistanceOverLimit = "Station {0} move distance is over limit";
        }
    }
}
