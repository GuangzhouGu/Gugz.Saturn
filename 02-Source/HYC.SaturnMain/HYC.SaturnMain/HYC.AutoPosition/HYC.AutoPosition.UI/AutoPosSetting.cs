/*----------------------------------------------------------------
// Copyright (C) Suzhou HYC Electronic Technology Co.,LTD
// 版权所有。
//
// =================================
// CLR版本     ：4.0.30319.42000
// 命名空间    ：HYC.AutoPosition.UI
// 文件名称    ：PosSetting.cs
// =================================
// 创 建 者    ：yangkang
// 创建日期    ：2020/1/7 15:43:53 
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
using HYC.AutoPosition.Parameter;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HYC.AutoPosition.UI
{
    /// <summary>
    /// 自动对位设置类
    /// </summary>
    [Serializable]
    public class AutoPosSetting
    {
        /// <summary>
        /// 是否保存图片
        /// </summary>
        public bool IsSaveImage { get; set; }
        /// <summary>
        /// 是否仅保存NG图
        /// </summary>
        public bool IsSaveNgImageOnly { get; set; }
        /// <summary>
        /// 图片保存路径
        /// </summary>
        public string ImageSavePath { get; set; }

        /// <summary>
        /// 失败重试次数
        /// </summary>
        public int RetryLimitCount { get; set; } = 3;
        /// <summary>
        /// 运动模式
        /// </summary>
        public MoveFlowType MoveFlowType { get; set; }
        /// <summary>
        /// 是否算法屏蔽
        /// </summary>
        public bool IsAlgorithmShield { get; set; }
        /// <summary>
        /// 算法输出的为彩色图
        /// </summary>
        public bool IsOutputColorMode { get; set; }
        /// <summary>
        /// 对位平台类型
        /// </summary>
        public string XYTTypeName { get; set; } = "VirtualXYTManager";
        /// <summary>
        /// 对位平台初始化参数
        /// </summary>
        public string[] XYTInitPara { get; set; } = { };
        /// <summary>
        /// 相机曝光时间
        /// </summary>
        public float[] ExposureTime { get; set; } 

        /// <summary>
        /// 所有对位组
        /// </summary>
        public List<GroupConfig> ListGroup { get; set; } = new List<GroupConfig>();

        /// <summary>
        /// 获取所有相机配置
        /// </summary>
        /// <returns></returns>
        public List<CameraConfig> GetCameraConfigs()
        {
            List<CameraConfig> lst = new List<CameraConfig>();
            foreach (var item in ListGroup)
            {
                foreach (var cfg in item.ListCamera)
                {
                    // 使用了同一个Mac地址的相机是同一个相机
                    if (lst.Where(p => p.MacAddr == cfg.MacAddr).Count() <= 0)
                    {
                        lst.Add(cfg);
                    }
                }
            }
            return lst;
        }

        /// <summary>
        /// 找到目标平台对应的组
        /// </summary>
        /// <param name="stageIndex">目标平台</param>
        /// <returns></returns>
        public GroupConfig GetGroupConfig(int stageIndex)
        {
            return ListGroup.Where(p => p.ListTargetStages.Contains(stageIndex)).FirstOrDefault();
        }

        /// <summary>
        /// 找到所有平台对应的组的序号
        /// </summary>
        /// <param name="stageIndex">目标平台</param>
        /// <returns></returns>
        public int GetGroupIndex(int stageIndex)
        {
            int index = -1;
            for (int i = 0; i < ListGroup.Count; i++)
            {
                if (ListGroup[i].ListTargetStages.Contains(stageIndex))
                {
                    index = i;
                }
            }
            return index;
        }
    }

    /// <summary>
    /// 单组对位的设置类
    /// </summary>
    [Serializable]
    public class GroupConfig
    {
        /// <summary>
        /// 分组名称
        /// </summary>
        [XmlAttribute]
        public string GroupName { get; set; }
        /// <summary>
        /// 所有对位相机
        /// </summary>
        public List<CameraConfig> ListCamera { get; set; } = new List<CameraConfig>();
        /// <summary>
        /// 运动执行者类型
        /// </summary>
        public MoveExecutorType MoveExecutorType { get; set; }
        /// <summary>
        /// 是否交换XY
        /// </summary>
        public bool IsSwapXY { get; set; }
        /// <summary>
        /// 运动完成后需要确认
        /// </summary>
        public bool IsConfirmRequired { get; set; }
        /// <summary>
        /// 是否启用拍照锁
        /// </summary>
        public bool IsUseGrabLock { get; set; }
        /// <summary>
        /// XYT对位成功的判定阈值
        /// </summary>
        public double[] ThreXYT { get; set; } = new double[3];
        /// <summary>
        /// 最大移动步距
        /// </summary>
        public double[] MaxMoveDistance { get; set; } = new double[3];
        /// <summary>
        /// 目标工位序号集合
        /// </summary>
        public List<int> ListTargetStages { get; set; } = new List<int>();
     
    }

    /// <summary>
    /// 相机的设置类
    /// </summary>
    [Serializable]
    public class CameraConfig
    {
        /// <summary>
        /// 相机名称
        /// </summary>
        [XmlAttribute]
        public string Name { get; set; }
        /// <summary>
        /// 相机Mac地址
        /// </summary>
        [XmlAttribute]
        public string MacAddr { get; set; }
        /// <summary>
        /// 相机类型
        /// </summary>
        [XmlAttribute]
        public string CameraType { get; set; }
        /// <summary>
        /// 图像旋转与镜像
        /// </summary>
        [XmlAttribute]
        public RotateFlipType RotateFlipType { get; set; }
        /// <summary>
        /// 图像旋转与镜像
        /// </summary>
        [XmlAttribute]
        public float ExposureTime { get; set; } = 12f;
    }

    /// <summary>
    /// 运动执行者类型
    /// </summary>
    public enum MoveExecutorType
    {
        /// <summary>
        /// 运动执行者为对位平台
        /// </summary>
        XYTPlatform,
        /// <summary>
        /// 运动执行者为信号提供者
        /// </summary>
        SignalProvider
    }
}
