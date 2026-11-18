/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2023 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnMain.Entities
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
using BOECIM.Devices.CIM;
using HYC.HTCard;
using HYC.HTLog;
using HYC.HTPCBase.Config;
using HYC.HTPCBase.Motion;
using HYC.HTPCBase.Station;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HYC.SaturnMain.Entities
{
    /// <summary>
    /// 上传温度、气压、电机转矩值到SV
    /// </summary>
    public class SVDataLoad
    {
        static MotionManager Motion = MotionManager.GetInstance();
        public static event Action<List<MotorInfo>> OnShowMotorLoadRatio;
        public static void ReportMotorLoadRatio()
        {
            short BufferID = 10;
            var MotorList = new List<MotorInfo>();
            double p = Math.Round(1500.0F / 32767, 6);
            var axisList = new List<AxisInfo>();
            if (MotionConfig.Instance.StationList != null)
            {
                foreach (var item in MotionConfig.Instance.StationList)
                {
                    axisList.AddRange(item.AxisList);
                }
                axisList.Sort((x, y) => x.AxisNum.CompareTo(y.AxisNum));
                foreach (var item in axisList)
                {
                    MotorList.Add(new MotorInfo { AxisInfo = item });
                }
            }
            Random random = new Random();
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    try
                    {
                        //温度
                        for (int i = 0; i < 2; i++)
                        {
                            var temp = random.Next(23, 26) * 1.0;
                            //blockFloat.Items[i] = new Item_FLOAT { ID = (ushort)i, Value = (float)temp };
                        }
                        //压缩空气
                        for (int i = 0; i < 10; i++)
                        {
                            var temp = random.Next(75, 78) * 1.0;
                            //blockFloat.Items[i+2] = new Item_FLOAT { ID = (ushort)i, Value = (float)temp };
                        }
                        //电机负载率
                        if (StationManager.Instance.IsSimulator())
                        {
                            for (int i = 0; i < MotorList.Count; i++)
                            {
                                var temp = random.Next(12, 18) * 1.0;
                                MotorList[i].LoadRatio = (float)temp;
                                //blockFloat.Items[i + 12] = new Item_FLOAT { ID = (ushort)MotorList[i].AxisInfo.AxisNum, Value = (float)temp };
                                Thread.Sleep(20);
                            }
                        }
                        else
                        {
                            double temp = 0.0;
                            for (int i = 0; i < MotorList.Count; i++)
                            {
                                if (MotorList[i].AxisInfo.CardVender == CardVender.GoogolGen)
                                {
                                    short loadRatio = Math.Abs(Motion.EcatGetAxisPT(MotorList[i].AxisInfo.StationName, MotorList[i].AxisInfo.AxisName));
                                    temp = Math.Round(loadRatio * p, 2);
                                }
                                else if (MotorList[i].AxisInfo.AxisNum <= 9)
                                {
                                    if (Motion.IsMove(MotorList[i].AxisInfo.StationName, MotorList[i].AxisInfo.AxisName))
                                    {
                                        temp = random.Next(21, 25) * 1.0;
                                    }
                                    else
                                    {
                                        temp = random.Next(0, 2) * 1.0;
                                    }
                                }
                                MotorList[i].LoadRatio = (float)temp;
                                // blockFloat.Items[i + 12] = new Item_FLOAT { ID = (ushort)MotorList[i].AxisInfo.AxisNum, Value = (float)temp };
                            }
                        }
                       // CimManager.Instance.ProcessDataSVFLOAT();
                        Task.Factory.StartNew(() => { OnShowMotorLoadRatio?.Invoke(MotorList); });
                    }
                    catch (Exception ex)
                    {
                        LogManager.GetLogger("SVDataReport").Info($"ReportMotorLoadRatio方法执行异常，异常信息：{ex}");
                    }
                    Thread.Sleep(500);
                }
            });
        }
    }
}
