/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2023 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnAoi.Devices.Tester
// 文件名称    ：ELPTesterBase
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
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYC.SaturnAoi.Devices.Tester
{
    public abstract class ELPTesterBase
    {
        public bool IsConnected { get; set; }
        public event Action<string, int, bool> OnConnected;
        public event Action<string, string, Brush> OnShowMsg;
        public abstract void Connect();
        public abstract string GetStatus();
        public abstract bool PowerOn();
        public abstract bool PowerOff();
        public abstract bool PatternDisplay(int patternNum);
        public abstract int GetPatternNum();
        public abstract string GetPatternName();
        public abstract bool PatternNext();
        public abstract bool PatternPrevious();
        public abstract bool DownLoadRecipe(int modelIndex, int ptnIndex, string recipeName);
        public abstract string GetCurRecipe();
        public abstract bool VoltageSet(int ptnIndex, short rVoltage, short gVoltage, short bVoltage);
        public abstract string VoltageGet(int ptnIndex);
        public abstract void DisConnect();
    }
}
