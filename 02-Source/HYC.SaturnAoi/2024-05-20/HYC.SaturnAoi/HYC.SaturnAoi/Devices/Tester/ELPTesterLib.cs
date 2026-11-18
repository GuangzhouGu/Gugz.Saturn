/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2023 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnAoi.Devices.Tester
// 文件名称    ：ELPTesterLib
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
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HYC.SaturnAoi.Devices.Tester
{
    public class ELPTesterLib
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns>1：成功，0：失败</returns>
        [DllImport("AVI_DLLD.dll")]
        public static extern bool INITSOCK_DLL();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPGKind">4(302T)</param>
        /// <param name="nPGNum">为区分PG而设定的号码( 0 ~ 6)</param>
        /// <param name="nCommType">0 : SOCKET, 1 : SERIAL</param>
        /// <returns></returns>
        [DllImport("AVI_DLLD.dll")]
        public static extern bool INITIALIZE_DLL(int nPGKind, int nPGNum, int nCommType);
        /// <summary>
        /// 
        /// </summary>
        /// <returns>1：成功，0：失败</returns>
        [DllImport("AVI_DLLD.dll")]
        public static extern void DLL_FREE();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPGNum">在PG登录使用的PG区分号码</param>
        /// <param name="szIPAddress">IP 地址</param>
        /// <param name="nLen">IP 地址字符串长度</param>
        /// <param name="nPort">IP PORT 号码 (1000)使用</param>
        /// <returns></returns>
        [DllImport("AVI_DLLD.dll")]
        public static extern bool PG_CONNECT(int nPGNum, string szIPAddress, int nLen, int nPort);
        /// <summary>
        /// 
        /// </summary>
        /// <returns>1：成功，0：失败</returns>
        [DllImport("AVI_DLLD.dll")]
        public static extern bool PG_DISCONNECT(int nPGNum);
        /// <summary>
        /// 
        /// </summary>
        /// <returns>1：成功，0：失败</returns>
        [DllImport("AVI_DLLD.dll")]
        public static extern int PG_LAST_ERROR(int nPGNum);
        /// <summary>
        /// 
        /// </summary>
        /// <returns>1：成功，0：失败</returns>
        [DllImport("AVI_DLLD.dll")]
        public static extern bool GetZonePatternCount(int nPGNum, int nZone, int[] pPattCount);
        /// <summary>
        /// 
        /// </summary>
        /// <returns>1：成功，0：失败</returns>
        [DllImport("AVI_DLLD.dll")]
        public static extern bool PG_DOWNLOAD(int nPGNum, string FilePath, int nLen);
        /// <summary>
        /// 
        /// </summary>
        /// <returns>1：成功，0：失败</returns>
        [DllImport("AVI_DLLD.dll")]
        public static extern bool PGTurnOn(int nPGNum);
        /// <summary>
        /// 
        /// </summary>
        /// <returns>1：成功，0：失败</returns>
        [DllImport("AVI_DLLD.dll")]
        public static extern bool PGTurnOff(int nPGNum);
        /// <summary>
        /// 
        /// </summary>
        /// <returns>1：成功，0：失败</returns>
        [DllImport("AVI_DLLD.dll")]
        public static extern bool PGPatternNext(int nPGNum);
        /// <summary>
        /// 
        /// </summary>
        /// <returns>1：成功，0：失败</returns>
        [DllImport("AVI_DLLD.dll")]
        public static extern bool PGPatternPrev(int nPGNum);
      /// <summary>
      /// 切画面
      /// </summary>
      /// <param name="nPGNum"></param>
      /// <param name="nSel">画面序号</param>
      /// <returns></returns>
        [DllImport("AVI_DLLD.dll")]
        public static extern bool PGPatternSel(int nPGNum, int nSel);
        /// <summary>
        /// 
        /// </summary>
        /// <returns>1：成功，0：失败</returns>
        [DllImport("AVI_DLLD.dll")]
        public static extern bool PGGeteRecipeName(int nPGNum, string pRecipeName);
 
    }
}
