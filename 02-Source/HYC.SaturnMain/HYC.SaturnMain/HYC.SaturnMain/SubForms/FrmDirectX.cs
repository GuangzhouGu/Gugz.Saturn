/*----------------------------------------------------------------
// Copyright (C) Suzhou HYC Electronic Technology Co.,LTD
// 版权所有。
//
// =================================
// CLR版本     ：4.0.30319.42000
// 命名空间    ：DirectXFormHost
// 文件名称    ：FormDirectX.cs
// =================================
// 创 建 者    ：yangkang
// 创建日期    ：2020/8/23 13:01:11 
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
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using HYC.HTFile.HTXML;
using HYC.SaturnMain.Station;
using HYC.SaturnMain.Entities;
using HYC.HTPCBase.Motion;
using HYC.SaturnMain.Params;
using HYC.HTPCBase.Extension.Views;
using HYC.HTPCBase.Config;

namespace DirectXFormHost
{
    public partial class FrmDirectX : ViewBase
    {
        private bool isHandleMsg = false; //正在处理windows msg，暂停渲染
        public ModelList ModelList { get; set; } = new ModelList();
        private AxisInfo AxisInfo = null;
        /// <summary>
        /// 构造方法
        /// </summary>
        public FrmDirectX()
        {
            InitializeComponent();
            this.Index = 5;
            this.ButtonName = "3D模拟";
            this.ButtonNameEn = "Abbr\nSimulator";
            this.IsNeedLogin = false;
            this.ButtonImage = HYC.SaturnMain.Properties.Resources._3D;
            if (File.Exists("ModelList.xml"))
            {
                ModelList = HTXMLSerializer.EntityFromXMLFile<ModelList>("ModelList.xml");
            }

        }

        private void FormDirectX_Load(object sender, EventArgs e)
        {
            // 初始化3D资源
            DirectXRender.RenderInitD3D(this.Handle);
            // 添加3D模型
            for (int i = 0; i < ModelList.Nodes.Count; i++)
            {
                var node = ModelList.Nodes[i];
                AddModels(0, node);
            }
            // 开始渲染
            Task.Factory.StartNew(Render);

            MonitorPos();
        }

        private void MonitorPos()
        {

            Task.Factory.StartNew(() => {
                while (true)
                {
                    try
                    {
                        if (!this.Visible)
                        {
                            Thread.Sleep(3000);
                            continue;
                        }
                        foreach (KeyValuePair<string, StationInfo> k in MotionConfig.Instance.StationDic)
                        {
                            //遍历轴字典
                            foreach (KeyValuePair<string, AxisInfo> m in k.Value.AxisInfoDic)
                            {
                                AxisInfo = m.Value;
                                if (AxisInfo != null)
                                {
                                    switch (AxisInfo.AxisName)
                                    {
                                        case "Stage1_Y":
                                            {
                                                MoveAbs("StageY2", -(float)AxisInfo.ActPos / Param.ThreeD.Stage2YRate);
                                                break;
                                            }
                                        case "Stage2_Y":
                                            {
                                                MoveAbs("StageY1",- (float)AxisInfo.ActPos / Param.ThreeD.Stage1YRate);
                                                break;
                                            }
                                        case "Stage1_T":
                                            {
                                                RotateAbs("StageT1", (float)AxisInfo.ActPos / Param.ThreeD.Stage1TRate);
                                                break;
                                            }
                                        case "Stage2_T":
                                            {
                                                RotateAbs("StageT2", (float)AxisInfo.ActPos / Param.ThreeD.Stage2TRate);
                                                break;
                                            }
                                        case "ProbeGantry_Y1":
                                            {
                                                MoveAbs("ProbeGantry", (float)AxisInfo.ActPos / Param.ThreeD.ProbeGantryRate);
                                                break;
                                            }
                                        case "ProbeGantryAlig_X1":
                                            {
                                                MoveAbs("AlignCamX1", (float)AxisInfo.ActPos / Param.ThreeD.AlignCamX1);
                                                break;
                                            }
                                        case "ProbeGantryAlig_X2":
                                            {
                                                MoveAbs("AlignCamX2", (float)AxisInfo.ActPos / Param.ThreeD.AlignCamX2);
                                                break;
                                            }
                                     
                                        case "DetectGantry_Y1":
                                            {
                                                MoveAbs("DetectGantry", (float)AxisInfo.ActPos / Param.ThreeD.DetectGantryRate);
                                                break;
                                            }
                                        case "DetectModule1_X":
                                            {
                                                MoveAbs("DetectModule1_X", (float)AxisInfo.ActPos / Param.ThreeD.ReviewCamX);
                                                break;
                                            }
                                        case "DetectModule2_X":
                                            {
                                                MoveAbs("DetectModule2_X", (float)AxisInfo.ActPos / Param.ThreeD.IRCamX);
                                                break;
                                            }
                                        case "DetectModule1_Z1":
                                            {
                                                MoveAbs("DetectModule1_Z1", -(float)AxisInfo.ActPos / Param.ThreeD.IRCamZ);
                                                break;
                                            }
                                        case "DetectModule2_Z1":
                                            {
                                                MoveAbs("DetectModule2_Z1", -(float)AxisInfo.ActPos / Param.ThreeD.ReviewCamZ);
                                                break;
                                            }
                                    }
                                }
                            }
                            Thread.Sleep(10);
                        }

                    }
                    catch (Exception ex)
                    {
                    }
                }
            });
        }
        /// <summary>
        /// 递归添加模型
        /// </summary>
        /// <param name="node"></param>
        private static void AddModels(int parentId, ModelNode node)
        {
            DirectXRender.RenderAddModel(parentId, node.ModelInfo);
            foreach (var item in node.Children)
            {
                AddModels(node.ModelInfo.ModelId, item);
            }
        }

        /// <summary>
        /// 渲染线程
        /// </summary>
        private void Render()
        {
            while (true)
            {
                try
                {
                    if (!this.Visible)
                    {
                        Thread.Sleep(100);
                        continue;
                    }
                    if (!isHandleMsg)
                    {
                        DirectXRender.RenderRun();
                    }
                }
                catch (Exception)
                { }
                Thread.Sleep(5);
            }
        }

        /// <summary>
        /// 处理Windows消息
        /// </summary>
        /// <param name="m"></param>
        protected override void DefWndProc(ref Message m)
        {
            isHandleMsg = true;
            try
            {
                DirectXRender.RenderMsgProc(m.HWnd, m.Msg, m.WParam, m.LParam);
            }
            catch (Exception)
            { }

            base.DefWndProc(ref m);
            isHandleMsg = false;
        }

        /// <summary>
        /// 鼠标进入获取焦点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormDirectX_MouseEnter(object sender, EventArgs e)
        {
            this.Focus();
        }
        /// <summary>
        /// 单个组件的移动
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pos"></param>
        public void MoveAbs(string name, float pos)
        {
            try
            {
                var node = GetModelNode(ModelList.Nodes, name);
                if (node == null) return;

                var info = node.ModelInfo;
                Tuple<int, int, int> direction = new Tuple<int, int, int>(0, 0, 0);
                switch (name)
                {
                    case "ProbeGantry":
                        direction = new Tuple<int, int, int>(0, 0, 1);
                        break;
                    case "AlignCamX1":
                        direction = new Tuple<int, int, int>(1, 0, 0);
                        break;
                    case "AlignCamX2":
                        direction = new Tuple<int, int, int>(-1, 0, 0);
                        break;
                    case "AlignCamZ1":
                        direction = new Tuple<int, int, int>(0, 1, 0);
                        break;
                    case "AlignCamZ2":
                        direction = new Tuple<int, int, int>(0, 1, 0);
                        break;
                    case "StageY1":
                        direction = new Tuple<int, int, int>(0, 0, 1);
                        break;
                    case "StageY2":
                        direction = new Tuple<int, int, int>(0, 0, -1);
                        break;
                    case "DetectGantry":
                        direction = new Tuple<int, int, int>(0, 0, -1);
                        break;
                    case "DetectModule1_X":
                        direction = new Tuple<int, int, int>(1, 0, 0);
                        break;
                    case "DetectModule2_X":
                        direction = new Tuple<int, int, int>(-1, 0, 0);
                        break;
                    case "DetectModule1_Z1":
                        direction = new Tuple<int, int, int>(0, 1, 0);
                        break;
                    case "DetectModule2_Z1":
                        direction = new Tuple<int, int, int>(0, 1, 0);
                        break;
                    default:
                        break;
                }

                if (direction.Item1 == 0 && direction.Item2 == 0 && direction.Item3 == 0)
                    return;

                float x = (float)(info.PositionX + direction.Item1 * pos);
                float y = (float)(info.PositionY + direction.Item2 * pos);
                float z = (float)(info.PositionZ + direction.Item3 * pos);
                DirectXRender.RenderSetPosition(info.ModelId, x, y, z);
            }
            catch (Exception ex)
            {

            }
        }
        /// <summary>
        /// 单个组件的旋转
        /// </summary>
        /// <param name="name"></param>
        /// <param name="degree">角度</param>
        private void RotateAbs(string name, float degree)
        {
            var node = GetModelNode(ModelList.Nodes, name);
            if (node == null) return;

            var info = node.ModelInfo;
            Tuple<int, int, int> direction = new Tuple<int, int, int>(0, 0, 0);
            switch (name)
            {
                case "StageT1":
                    direction = new Tuple<int, int, int>(0, -1, 0);
                    break;
                case "StageT2":
                    direction = new Tuple<int, int, int>(0, -1, 0);
                    break;
                default:
                    break;
            }

            if (direction.Item1 == 0 && direction.Item2 == 0 && direction.Item3 == 0)
                return;

            double rad = degree / 180 * Math.PI; //转换为弧度
            float x = (float)(info.RotationX + direction.Item1 * rad);
            float y = (float)(info.RotationY + direction.Item2 * rad);
            float z = (float)(info.RotationZ + direction.Item3 * rad);
            DirectXRender.RenderRotateCenter(info.ModelId, x, y, z);
        }
        /// <summary>
        /// 获取模型节点
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private ModelNode GetModelNode(List<ModelNode> nodes, string name)
        {
            foreach (ModelNode node in nodes)
            {
                if (node.ModelInfo.ModelName == name)
                {
                    return node;
                }
                var node2 = GetModelNode(node.Children, name);
                if (node2 != null)
                {
                    return node2;
                }
            }
            return null;
        }
    }
}
