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
using HYC.HTPower;
using HYC.StandardAoi.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HYC.SaturnAoi.Entities
{
    /// <summary>
    /// 光源控制器
    /// </summary>
    public class PowerController : IPowerController
    {
        /// <summary>
        /// 侧光源
        /// </summary>
        private IHTPower Sidepower = null;
    

        private static object lockObj = new object();

        /// <summary>
        /// 添加光源控制
        /// </summary>
        /// <param name="sidepower"></param>
        /// <param name="backpower"></param>
        /// <param name="cepower"></param>
        public PowerController(IHTPower sidepower, IHTPower backpower, IHTPower cepower)
        {
            this.Sidepower = sidepower;
        }

        /// <summary>
        /// 关闭背光
        /// </summary>
        /// <returns></returns>
        public bool PowerOffBack()
        {
            try
            {          
                return true;
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("Error").Error(ex);
                return false;
            }
          
        }

        /// <summary>
        /// 关闭侧光
        /// </summary>
        /// <returns></returns>
        public bool PowerOffSide()
        {
            try
            {
                Monitor.Enter(lockObj);
                bool res = Sidepower.PowerOff(HTPowerChannel.POWER_CHANNEL_2);
                if (!res)
                {
                    Thread.Sleep(50);
                    Sidepower.PowerOff(HTPowerChannel.POWER_CHANNEL_2);
                }
                res = Sidepower.PowerOff(HTPowerChannel.POWER_CHANNEL_3);
                if (!res)
                {
                    Thread.Sleep(50);
                    Sidepower.PowerOff(HTPowerChannel.POWER_CHANNEL_3);
                }
                return true;
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("Error").Error(ex);
                return false;
            }
            finally
            {
                Monitor.Exit(lockObj);
            }
        }

        /// <summary>
        /// 打开背光
        /// 控制背光源pc0,2,4
        /// </summary>
        /// <returns></returns>
        public bool PowerOnBack()
        {
            try
            {
                return true;
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("Error").Error(ex);
                return false;
            }
            finally
            {
                // Monitor.Exit(lockObj);
            }
        }

        /// <summary>
        /// 打开侧光
        /// </summary>
        /// <returns></returns>
        public bool PowerOnSide()
        {
            try
            {
                Monitor.Enter(lockObj);
                bool res = Sidepower.PowerOn(HTPowerChannel.POWER_CHANNEL_2);
                if (!res)
                {
                    Thread.Sleep(50);
                    Sidepower.PowerOn(HTPowerChannel.POWER_CHANNEL_2);
                }
                return true;
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("Error").Error(ex);
                return false;
            }
            finally
            {
                Monitor.Exit(lockObj);
            }
        }
    }
}
