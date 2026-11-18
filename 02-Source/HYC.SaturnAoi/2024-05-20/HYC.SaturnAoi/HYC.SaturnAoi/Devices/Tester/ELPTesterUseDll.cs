/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2023 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnAoi.Devices.Tester
// 文件名称    ：ELPTesterDll
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
using HYC.SaturnAoi.Params;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HYC.SaturnAoi.Devices.Tester
{
    /// <summary>
    /// 使用动态链接库接口
    /// </summary>
    public class ELPTesterUseDll : ELPTesterBase
    {
        public event Action<string, int, bool> OnConnected;
        public event Action<string, string, Brush> OnShowMsg;
        public ELPTesterInfo Info = null;
        public ELPTesterUseDll(ELPTesterInfo info)
        {
            Info = info;
            Connect();
        }
        public override void Connect()
        {
            var t = new Thread(() => {
                while (true)
                {
                    if (!IsConnected)
                    {
                        try
                        {
                            var ret = ELPTesterLib.INITSOCK_DLL();
                            ret = ELPTesterLib.INITIALIZE_DLL(4, 0, 0);
                            IsConnected = ELPTesterLib.PG_CONNECT(0, Info.IP, Info.IP.Length, Info.Port);
                            if (IsConnected)
                            {
                                OnConnected?.Invoke($"{Info.TesterName}", Info.TesterNum, true);
                                ret = DownLoadRecipe(0, 0, Param.System.PGRecipeFilePath);
                                OnShowMsg($"ELPTester", $"加载机种文件{Param.System.PGRecipeFilePath}{(ret?"成功":"失败")}", ret? Brushes.Green:Brushes.Red);
                            }
                        }
                        catch (Exception ex)
                        {
                            OnConnected?.Invoke($"{Info.TesterName}", Info.TesterNum, false);
                            OnShowMsg($"ELPTester", $"ELPTester->Connect执行异常，异常信息：{ex.Message}", Brushes.Red);
                        }
                    }
                    Thread.Sleep(3000);
                }
            });
            t.IsBackground = true;
            t.Start();
        
        }

        public override void DisConnect()
        {
            try
            {
                ELPTesterLib.PG_DISCONNECT(0);
                ELPTesterLib.DLL_FREE();
            }
            catch (Exception ex)
            {
                OnShowMsg($"ELPTester", $"ELPTester->DisConnect执行异常，异常信息：{ex.Message}", Brushes.Red);
            }
        }

        public override bool DownLoadRecipe(int pgIndex, int ptnIndex, string recipeName)
        {
            var ret = ELPTesterLib.INITSOCK_DLL();
            ret = ELPTesterLib.INITIALIZE_DLL(4, 0, 0);
            ret = ELPTesterLib.PG_CONNECT(0, Info.IP, Info.IP.Length, Info.Port);
            ret = ELPTesterLib.PG_DOWNLOAD(0, recipeName, recipeName.Length);
            ELPTesterLib.PG_DISCONNECT(0);
            ELPTesterLib.DLL_FREE();
            return ret;
        }

        public override string GetCurRecipe()
        {
            string curRecipeName = string.Empty;
            ELPTesterLib.PGGeteRecipeName(0, curRecipeName);
            return curRecipeName;
        }

        public override string GetPatternName()
        {
            throw new NotImplementedException();
        }

        public override int GetPatternNum()
        {
            throw new NotImplementedException();
        }

        public override string GetStatus()
        {
            throw new NotImplementedException();
        }

        public override bool PatternDisplay(int patternNum)
        {
            return ELPTesterLib.PGPatternSel(0, patternNum);
        }

        public override bool PatternNext()
        {
            return ELPTesterLib.PGPatternNext(0);
        }

        public override bool PatternPrevious()
        {
            return ELPTesterLib.PGPatternPrev(0);
        }

        public override bool PowerOff()
        {
            return ELPTesterLib.PGTurnOff(0);
        }

        public override bool PowerOn()
        {
            return ELPTesterLib.PGTurnOn(0);
        }

        public override string VoltageGet(int ptnIndex)
        {
            throw new NotImplementedException();
        }

        public override bool VoltageSet(int ptnIndex, short rVoltage, short gVoltage, short bVoltage)
        {
            throw new NotImplementedException();
        }
    }
}
