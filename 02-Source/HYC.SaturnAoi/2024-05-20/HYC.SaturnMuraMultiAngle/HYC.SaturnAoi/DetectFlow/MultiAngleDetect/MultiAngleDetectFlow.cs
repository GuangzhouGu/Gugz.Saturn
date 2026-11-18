/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2023 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnAoi.DetectFlow.MultiAngleDetect
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
using HYC.HTLog;
using HYC.SaturnAoi.Devices.ClientPC;
using HYC.SaturnAoi.Devices.MultiAngle;

using HYC.SaturnAoi.Entities;
using HYC.SaturnAoi.Params;
using HYC.SaturnMuraMultiAngle.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HYC.SaturnAoi.DetectFlow.MultiAngleDetect
{
    public class MultiAngleDetectFlow
    {
        public event Action<string, string, Brush> OnShowMsg;
        public event Action<MultiAngleCompletedArgs> OnDetectComplete;
        public static event Action<PatternDetectInfo, string> OnShowDetectValue;
        MultiAngleCompletedArgs MultiAngleResult = null;
        PatternProducer PatternProducer = null;
        public void MultiAngleThread(DetectReqArgs multiAngleReqArgs)
        {
            try
            {
                MultiAngleResult = new MultiAngleCompletedArgs();
                MultiAngleResult.StartTime = DateTime.Now;
                MultiAngleResult.GlassID = multiAngleReqArgs.GlassID;
                MultiAngleResult.GlassNum = multiAngleReqArgs.GlassNum;
                MultiAngleResult.CellID = multiAngleReqArgs.CellID;
                MultiAngleResult.ColumnNum = multiAngleReqArgs.ColumnNum;
                MultiAngleResult.PatternInfoList = new List<PatternDetectInfo>();
                if (Param.MultiAngleDetectPattern.PatternList != null && Param.MultiAngleDetectPattern.PatternList.Count > 0)
                {
                    PatternProducer = new PatternProducer();
                    OnShowDetectValue?.Invoke(null, null);
                    for (int i = 0; i < Param.MultiAngleDetectPattern.PatternList.Count; i++)
                    {
                        var pattern = Param.MultiAngleDetectPattern.PatternList[i];
                        bool ret = true;
                        OnShowMsg?.Invoke("MultiAngleDetectFlow", $"当前检测画面名称：{pattern.PatternName},画面序号：{pattern.PatternNum},画面稳定时间：{pattern.WaitTime},是否启用：{pattern.IsUse}，控制类型：{pattern.ControlType}", Brushes.Green);
                        if (pattern.IsUse)
                        {
                            switch (pattern.ControlType)
                            {
                                case "PowerOn":
                                    {
                                        ret = PatternProducer.PowerOn();
                                    }
                                    break;
                                case "SwitchPattern":
                                    {
                                        ret = PatternProducer.CreatePattern(pattern.PatternNum);
                                    }
                                    break;
                                case "PowerOn+SwitchPattern":
                                    {
                                        ret = PatternProducer.PowerOn();
                                        Thread.Sleep(1000);
                                        ret &= PatternProducer.CreatePattern(pattern.PatternNum);
                                    }
                                    break;
                                case "NoControl":
                                    break;

                            }
                            if (ret)
                            {
                                Thread.Sleep(pattern.WaitTime);
                                OnShowMsg?.Invoke("MultiAngleDetectFlow", $"光谱仪开始测试", Brushes.Green);
                                MultiAngleValue model = null;
                                string strRet = string.Empty;
                                for (int j = 0; j < Param.System.MultiAngleReDetectCount; j++)
                                {
                                    OnShowMsg?.Invoke("MultiAngleDetectFlow", $"光谱仪第{j+1}次测试", Brushes.Green);
                                    model = MultiAngleHelper.Instance.Get(0).Grab();
                                    strRet = model == null ? "数据为空" : "数据不为空";
                                    if (model!=null)
                                    {
                                        break;
                                    }
                                    Thread.Sleep(500);
                                }
                                OnShowMsg?.Invoke("MultiAngleDetectFlow", $"光谱仪测试完成，{strRet}", model == null ? Brushes.Red:Brushes.Green);
                                ResultJudge(model, pattern.PatternName);
                            }
                            else
                            {
                                OnShowMsg?.Invoke("MultiAngleDetectFlow", $"检查机切画面{pattern.PatternName}失败", Brushes.Red);
                            }
                        }
                    }
                    SaveCsv(MultiAngleResult);
                    DetectComplete();
                    OnShowMsg?.Invoke("MultiAngleDetectFlow", $"第{multiAngleReqArgs.ColumnNum + 1}列{multiAngleReqArgs.CellID} {multiAngleReqArgs.DetectType}检测结束", Brushes.Green);
                }
                else
                {
                    OnShowMsg?.Invoke("MultiAngleDetectFlow", $"MultiAngle检测画面列表为空", Brushes.Red);
                }
            }
            catch (Exception ex)
            {
                MultiAngleResult.EndTime = DateTime.Now;
                MultiAngleResult.ProcessingTime = MultiAngleResult.EndTime.Subtract(MultiAngleResult.StartTime).TotalSeconds.ToString();
                OnDetectComplete?.Invoke(MultiAngleResult);
                OnShowMsg?.Invoke("MultiAngleDetectFlow", $"MultiAngleThread执行异常，异常信息：{ex}", Brushes.Red);
                LogManager.GetLogger("MultiAngleDetectFlow").Info(ex.ToString());
            }
        }
        private void ResultJudge(MultiAngleValue value, string ptnName)
        {
            try
            {
                bool result = true;
                double resultVal = 0;
                if (value == null)
                {
                    result = false;
                }
                else
                {
                    try
                    {
                        // JNCD measure
                        double u1 = value.U[0];
                        double v1 = value.V[0];
                        double u2 = value.U[3];
                        double v2 = value.V[3];
                        double delta = Math.Sqrt(Math.Pow((u2 - u1), 2) + Math.Pow((v2 - v1), 2));
                        resultVal = delta / Param.System.JNCD;
                        if (resultVal < Param.System.MultiAngleStndard)
                        {
                            result &= true;
                        }
                        else
                        {
                            result &= false;
                            //break;
                        }
                    }
                    catch (Exception ex)
                    {
                        LogManager.GetLogger("MultiAngleDetectFlow").Info($"ResultJudge执行异常，异常信息：{ex}");
                    }
                }

                var ptnDetectInfo = new PatternDetectInfo { PatternInfo = new PatternInfo { PatternName = ptnName }, MultiAngleValue = value, Result = result ? "OK" : "NG", ResultValue = resultVal };
                OnShowDetectValue?.Invoke(ptnDetectInfo, MultiAngleResult.CellID);
                MultiAngleResult.PatternInfoList.Add(ptnDetectInfo);

            }
            catch (Exception ex)
            {
                OnShowMsg?.Invoke("MultiAngleDetectFlow", $"ResultJudge执行异常，异常信息：{ex}", Brushes.Red);
            }
        }
       
        private void SaveCsv(MultiAngleCompletedArgs value)
        {
            StreamWriter sw = null;
            try
            {
                string path = @"D:\MultiAngleData\" + DateTime.Now.ToString("yyyy-MM-dd");
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                string fileName = path + $@"\{MultiAngleResult.GlassID}.csv";

                if (!File.Exists(fileName))
                {
                    sw = new StreamWriter(fileName, true);
                    sw.WriteLine("CellID,Pattern,结果,Delta,Angle(角度),Luminance(亮度),X(1931色度x),Y(1931色度y),U(1976色度u),V(1976色度v),CCT(色温),Exposure(0:曝光正常;1:过曝),Noise(0:信号正常;1:杂音)");
                }
                else
                {
                    sw = new StreamWriter(fileName, true);
                }

                if (value.PatternInfoList != null && value.PatternInfoList.Count > 0)
                {
                    foreach (var item in value.PatternInfoList)
                    {
                        sw.Write($"{MultiAngleResult.CellID},{item.PatternInfo.PatternName},{item.Result},{item.ResultValue},");
                        double angle = 0;
                        if (item.MultiAngleValue != null)
                        {
                            for (int i = 0; i < 6; i++)
                            {
                                angle = i * 15;
                                if (i == 0)
                                {
                                    sw.WriteLine($"{angle},{item.MultiAngleValue.Lumi[i]},{item.MultiAngleValue.X[i]},{item.MultiAngleValue.Y[i]},{item.MultiAngleValue.U[i]},{item.MultiAngleValue.V[i]},{item.MultiAngleValue.Cct[i]},{item.MultiAngleValue.Exposure[i]},{item.MultiAngleValue.Noise[i]}");
                                }
                                else
                                {
                                    sw.WriteLine($",,,,{angle},{item.MultiAngleValue.Lumi[i]},{item.MultiAngleValue.X[i]},{item.MultiAngleValue.Y[i]},{item.MultiAngleValue.U[i]},{item.MultiAngleValue.V[i]},{item.MultiAngleValue.Cct[i]},{item.MultiAngleValue.Exposure[i]},{item.MultiAngleValue.Noise[i]}");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                OnShowMsg?.Invoke("MultiAngleDetectFlow", $"SaveCsv执行异常，异常信息：{ex}", Brushes.Red);
            }
            finally
            {
                sw.Close();
                sw.Dispose();
            }
        }
        private void DetectComplete()
        {
            try
            {
                //客户不要求判断光谱仪结果
                //MultiAngleResult.Result = MultiAngleResult.PatternInfoList.Where(p => p.Result == "NG").Count() > 0 ? "NG" : "OK";
                MultiAngleResult.Result = "OK";
                MultiAngleResult.EndTime = DateTime.Now;
                MultiAngleResult.ProcessingTime = MultiAngleResult.EndTime.Subtract(MultiAngleResult.StartTime).TotalSeconds.ToString();
                OnDetectComplete?.Invoke(MultiAngleResult);
            }
            catch (Exception ex)
            {
                OnShowMsg?.Invoke("MultiAngleDetectFlow", $"DetectComplete执行异常，异常信息：{ex}", Brushes.Red);
            }
        }
    }
}
