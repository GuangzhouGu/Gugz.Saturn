/*----------------------------------------------------------------
// Copyright (C) Suzhou HYC Electronic Technology Co.,LTD
// 版权所有。
//
// =================================
// CLR版本     ：4.0.30319.42000
// 命名空间    ：HYC.AutoPosition.UI.Controls
// 文件名称    ：UcGroupConfig.cs
// =================================
// 创 建 者    ：yangkang
// 创建日期    ：2020/1/20 9:00:37 
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
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HYC.AutoPosition.UI.Controls
{
    /// <summary>
    /// 单组对位的设置项控件
    /// </summary>
    public partial class UcGroupConfig : UserControl
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public UcGroupConfig()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 单组对位的设置
        /// </summary>
        public GroupConfig Config
        {
            get
            {
                return GetGroupConfig();
            }
            set
            {
                SetGroupConfig(value);
            }
        }

        /// <summary>
        /// 从界面获取设置
        /// </summary>
        /// <returns>单组设置</returns>
        private GroupConfig GetGroupConfig()
        {
            GroupConfig cfg = new GroupConfig();
            cfg.GroupName = this.txtGroupName.Text;
            //相机
            if (this.chkCameraEnable1.Checked)
            {
                cfg.ListCamera.Add(new CameraConfig()
                {
                    MacAddr = this.txtMacAddr1.Text,
                    CameraType = this.txtCameraType1.Text,
                    RotateFlipType = this.chkRotate1.Checked ? RotateFlipType.Rotate90FlipNone : 0
                });
            }
            if (this.chkCameraEnable2.Checked)
            {
                cfg.ListCamera.Add(new CameraConfig()
                {
                    MacAddr = this.txtMacAddr2.Text,
                    CameraType = this.txtCameraType2.Text,
                    RotateFlipType = this.chkRotate2.Checked ? RotateFlipType.Rotate90FlipNone : 0
                });
            }
            //运动执行者类型
            cfg.MoveExecutorType = this.rbtnXYTPlatform.Checked ?
                MoveExecutorType.XYTPlatform : MoveExecutorType.SignalProvider;
            //判定阈值
            cfg.ThreXYT[0] = Convert.ToDouble(this.txtThreX.Text);
            cfg.ThreXYT[1] = Convert.ToDouble(this.txtThreY.Text);
            cfg.ThreXYT[2] = Convert.ToDouble(this.txtThreT.Text);
            //最大步距
            cfg.MaxMoveDistance[0] = Convert.ToDouble(this.txtMaxDisX.Text);
            cfg.MaxMoveDistance[1] = Convert.ToDouble(this.txtMaxDisY.Text);
            cfg.MaxMoveDistance[2] = Convert.ToDouble(this.txtMaxDisT.Text);
            //其他选项
            cfg.IsSwapXY = this.chkSwapXY.Checked;
            cfg.IsConfirmRequired = this.chkConfirmRequired.Checked;
            cfg.IsUseGrabLock = this.chkUseGrabLock.Checked;
            //目标工位
            string[] strs = this.txtTargetStages.Text.Split(new char[] { ' ' },
                StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < strs.Length; i++)
            {
                cfg.ListTargetStages.Add(Convert.ToInt32(strs[i]));
            }

            return cfg;
        }

        /// <summary>
        /// 更新设置到界面
        /// </summary>
        /// <param name="cfg">单组设置</param>
        private void SetGroupConfig(GroupConfig cfg)
        {
            this.txtGroupName.Text = cfg.GroupName;
            //相机
            if (cfg.ListCamera.Count >= 1)
            {
                this.chkCameraEnable1.Checked = true;
                this.txtMacAddr1.Text = cfg.ListCamera[0].MacAddr;
                this.txtCameraType1.Text = cfg.ListCamera[0].CameraType;
                this.chkRotate1.Checked = ((int)cfg.ListCamera[0].RotateFlipType) % 2 == 1;
            }
            else
            {
                this.chkCameraEnable1.Checked = false;
                this.txtMacAddr1.Text = string.Empty;
                this.txtCameraType1.Text = string.Empty;
                this.chkRotate1.Checked = false;
            }
            if (cfg.ListCamera.Count >= 2)
            {
                this.chkCameraEnable2.Checked = true;
                this.txtMacAddr2.Text = cfg.ListCamera[1].MacAddr;
                this.txtCameraType2.Text = cfg.ListCamera[1].CameraType;
                this.chkRotate2.Checked = ((int)cfg.ListCamera[1].RotateFlipType) % 2 == 1;
            }
            else
            {
                this.chkCameraEnable2.Checked = false;
                this.txtMacAddr2.Text = string.Empty;
                this.txtCameraType2.Text = string.Empty;
                this.chkRotate2.Checked = false;
            }
            //运动执行者类型
            this.rbtnXYTPlatform.Checked = cfg.MoveExecutorType == MoveExecutorType.XYTPlatform;
            this.rbtnSignalProvider.Checked = cfg.MoveExecutorType == MoveExecutorType.SignalProvider;
            //判定阈值
            this.txtThreX.Text = cfg.ThreXYT[0].ToString();
            this.txtThreY.Text = cfg.ThreXYT[1].ToString();
            this.txtThreT.Text = cfg.ThreXYT[2].ToString();
            //最大步距
            this.txtMaxDisX.Text = cfg.MaxMoveDistance[0].ToString();
            this.txtMaxDisY.Text = cfg.MaxMoveDistance[1].ToString();
            this.txtMaxDisT.Text = cfg.MaxMoveDistance[2].ToString();
            //其他选项
            this.chkSwapXY.Checked = cfg.IsSwapXY;
            this.chkConfirmRequired.Checked = cfg.IsConfirmRequired;
            this.chkUseGrabLock.Checked = cfg.IsUseGrabLock;
            //目标工位
            this.txtTargetStages.Text = string.Join(" ", cfg.ListTargetStages.ToArray());
        }

        /// <summary>
        /// 移除对位组
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        /// <summary>
        /// 相机1选中状态改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkCameraEnable1_CheckedChanged(object sender, EventArgs e)
        {
            this.txtMacAddr1.Enabled = this.chkCameraEnable1.Checked;
            this.txtCameraType1.Enabled = this.chkCameraEnable1.Checked;
            this.chkRotate1.Enabled = this.chkCameraEnable1.Checked;
        }

        /// <summary>
        /// 相机2选中状态改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkCameraEnable2_CheckedChanged(object sender, EventArgs e)
        {
            this.txtMacAddr2.Enabled = this.chkCameraEnable2.Checked;
            this.txtCameraType2.Enabled = this.chkCameraEnable2.Checked;
            this.chkRotate2.Enabled = this.chkCameraEnable2.Checked;
        }
    }
}
