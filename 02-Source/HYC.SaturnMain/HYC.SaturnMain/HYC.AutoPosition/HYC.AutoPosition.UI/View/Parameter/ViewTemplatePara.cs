/*----------------------------------------------------------------
// Copyright (C) Suzhou HYC Electronic Technology Co.,LTD
// 版权所有。
//
// =================================
// CLR版本     ：4.0.30319.42000
// 命名空间    ：HYC.AutoPosition.UI.View.Parameter
// 文件名称    ：ViewFlowPara.cs
// =================================
// 创 建 者    ：yangkang
// 创建日期    ：2020/10/9 10:24:03 
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
using HYC.AutoPosition.UI.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HYC.AutoPosition.UI.View.Parameter
{
    /// <summary>
    /// 流程参数编辑页面
    /// </summary>
    public partial class ViewTemplatePara : ViewParameterBase
    {
        // 对位组序号
        private int groupIndex = 0;
        // 目标工位序号
        private int stageIndex = 0;

        /// <summary>
        /// 构造方法
        /// </summary>
        public ViewTemplatePara()
        {
            InitializeComponent();
           // cboGroup.SelectedIndex = 1;
        }

        public override void UpdateView()
        {
            if (this.cboGroup.Items.Count != Global.Setting.ListGroup.Count)
            {
                this.cboGroup.Items.Clear();
                for (int i = 0; i < Global.Setting.ListGroup.Count; i++)
                {
                    GroupConfig cfg = Global.Setting.ListGroup[i];
                    this.cboGroup.Items.Add(cfg.GroupName);
                }
                this.cboGroup.SelectedIndex = 0;
            }
            this.markSetting.UpdateView();
        }

        private void cboGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboGroup.SelectedIndex < 0) return;

            groupIndex = this.cboGroup.SelectedIndex;
            GroupConfig cfg = Global.Setting.ListGroup[groupIndex];

            this.cboStage.Items.Clear();
            for (int i = 0; i < cfg.ListTargetStages.Count; i++)
            {
                this.cboStage.Items.Add(cfg.ListTargetStages[i]);
            }
            this.cboStage.SelectedIndex = 0;
            this.markSetting.CurrentGroupIndex = groupIndex;
            this.markSetting.GroupConfig = cfg;

        }

        private void cboStage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboStage.SelectedIndex < 0) return;

            stageIndex = Convert.ToInt32(this.cboStage.Text) - 1;
            this.markSetting.CurrentStageIndex = stageIndex;
            UpdateView();
        }
    }
}
