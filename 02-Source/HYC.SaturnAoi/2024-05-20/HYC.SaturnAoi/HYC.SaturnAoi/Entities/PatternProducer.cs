/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2023 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnAoi.Entities
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
using HYC.SaturnAoi.Devices.Tester;
using HYC.StandardAoi.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYC.SaturnAoi.Entities
{
    /// <summary>
    /// 画面生产者
    /// </summary>
    public class PatternProducer : IPatternProducer
    {
        private ELPTesterBase Tester = null;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="Tester"></param>
        /// <param name="index"></param>
        public PatternProducer(ELPTesterBase Tester)
        {
            this.Tester = Tester;
        }

        /// <summary>
        /// 切换画面 RGB方式
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public bool CreatePattern(int r, int g, int b)
        {
            try
            {
                return true;
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("PatternProducer").Fatal(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 切换画面 序号方式
        /// </summary>
        /// <param name="patternIndex"></param>
        /// <returns></returns>
        public bool CreatePattern(int patternIndex)
        {
            try
            {
               bool  ret = Tester.PatternDisplay(patternIndex);
                return ret;
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("PatternProducer").Fatal(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 切换画面 画面名称
        /// </summary>
        /// <param name="patternName"></param>
        /// <returns></returns>
        public bool CreatePattern(string patternName)
        {
            try
            {
                return true;
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("PatternProducer").Fatal(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 关电
        /// </summary>
        /// <returns></returns>
        public bool PowerOff()
        {
            try
            {
                return Tester.PowerOff();
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 开电
        /// </summary>
        /// <returns></returns>
        public bool PowerOn()
        {
            try
            {
                return Tester.PowerOn();
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("PatternProducer").Fatal(ex.ToString());
                return false;
            }
        }
    }
}
