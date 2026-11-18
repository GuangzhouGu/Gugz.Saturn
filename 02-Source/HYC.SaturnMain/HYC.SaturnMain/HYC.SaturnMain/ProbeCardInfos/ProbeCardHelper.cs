/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2020 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnMain.Entities
// 文件名称    ：ProbeCardHelper.cs
// =================================
// 创 建 者    ：guguangzhou
// 创建日期    ：2021/01/29 09:00:00 
// 功能描述    ：对位参数
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
using HYC.SaturnDB.DAL;
using HYC.SaturnDB.Model;
using HYC.HTLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HYC.HTPCBase.Extension.Recipe;

namespace HYC.SaturnMain.Entities
{
    public class ProbeCardHelper
    {
        public static event Action<ProbeCardEventArgs> OnUpdatePrbInfo;
        public static ProbeCardModel GetProbeCard(string  recipeName)
        {
            try
            {
                var list = new ProbeCardDal().GetList().Where(p => p.Status != ProbeCardSts.OutEqp.ToString()).ToList();
                return list.Where(p => p.RecipeName == recipeName).FirstOrDefault();
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("ProbeCardHelper").Fatal(ex.ToString());
                return null;
            }
        }
        public static List<ProbeCardModel> GetProbeCardByProbeName(string probeName)
        {
            try
            {
                var list = new ProbeCardDal().GetList().Where(p => p.ProbeCardName == probeName).ToList();
                return list;
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("ProbeCardHelper").Fatal(ex.ToString());
                return null;
            }
        }
        public static void UpdateContactCount(string recipeName)
        {
            try
            {
                new ProbeCardDal().UpdateContactCount(recipeName);
                OnUpdatePrbInfo?.Invoke(new ProbeCardEventArgs { MsgType = "UpdateCount",MsgValue= recipeName });
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("ProbeCardHelper").Fatal(ex.ToString());
            }
        }
        public static void UpdateStatus(ProbeCardSts sts, string recipeName)
        {
            try
            {

                new ProbeCardDal().UpdateStatus(sts, recipeName);
                if (sts == ProbeCardSts.InUvw)
                {
                    OnUpdatePrbInfo?.Invoke(new ProbeCardEventArgs { MsgType = sts.ToString(), MsgValue = recipeName });
                }
                else if (sts == ProbeCardSts.OutUvw)//从Uvw平台上卸载下来就等于进料仓
                {
                    OnUpdatePrbInfo?.Invoke(new ProbeCardEventArgs { MsgType = ProbeCardSts.InCabinet.ToString(), MsgValue = recipeName });
                }
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("ProbeCardHelper").Fatal(ex.ToString());
            }
        }
        public static void Delete(string probeName)
        {
            new ProbeCardDal().DeleteByProbeName(probeName);
        }
    }
    public class ProbeCardEventArgs
    {
        /// <summary>
        /// 消息类别，0：刷新probeCard状态，1：刷新probeCard压接次数
        /// </summary>
        public string MsgType { get; set; } = string.Empty;
        public string MsgValue { get; set; } = string.Empty;
    }
}
