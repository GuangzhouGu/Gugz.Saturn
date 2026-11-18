/*----------------------------------------------------------------
// Copyright (C) Suzhou HYC Electronic Technology Co.,LTD
// 版权所有。
//
// =================================
// CLR版本     ：4.0.30319.42000
// 命名空间    ：HYC.AutoPosition.UI.View
// 文件名称    ：ViewManual.cs
// =================================
// 创 建 者    ：yangkang
// 创建日期    ：2020/1/2 19:24:55 
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
using HYC.AutoPosition.Events;
using HYC.AutoPosition.Extentions;
using HYC.AutoPosition.Interface;
using HYC.AutoPosition.Parameter;
using HYC.AutoPosition.UI.Controls;
using HYC.AutoPosition.UI.Entities;
using HYC.AutoPosition.UI.Helper;
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

namespace HYC.AutoPosition.UI.View
{
    /// <summary>
    /// 手动视图，图片检测及拍照测试
    /// </summary>
    public partial class ViewManual : ViewBase
    {
        // 对位组序号
        private int groupIndex = 0;
        // 目标工位序号
        private int stageIndex = 0;

        /// <summary>
        /// 构造方法
        /// </summary>
        public ViewManual()
        {
            InitializeComponent();
            Global.Manager.ImageReceived += Manager_ImageReceived;
            imageViewBox.GrabRequest += ViewBox_GrabRequest;
            imageViewBox.LiveRequest += ViewBox_LiveRequest;
        }

        /// <summary>
        /// 更新视图
        /// </summary>
        public override void UpdateView()
        {
            //更新ComboBox显示
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
        }

        /// <summary>
        /// 选择的组变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboGroup.SelectedIndex < 0) return;

            groupIndex = this.cboGroup.SelectedIndex;
            GroupConfig cfg = Global.Setting.ListGroup[groupIndex];

            this.imageViewBox.GroupName = cfg.GroupName;

            this.cboStage.Items.Clear();
            for (int i = 0; i < cfg.ListTargetStages.Count; i++)
            {
                this.cboStage.Items.Add(cfg.ListTargetStages[i]);
            }
            this.cboStage.SelectedIndex = 0;
        }

        /// <summary>
        /// 选择的目标工位变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboStage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboStage.SelectedIndex < 0) return;

            stageIndex = Convert.ToInt32(this.cboStage.Text) - 1;
        }

        /// <summary>
        /// 接收到图片数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Manager_ImageReceived(object sender, ImageReceivedArgs e)
        {
            if (!e.IsImgTestMode && !e.IsGrabTestMode && !e.IsManualMode) return;

            this.Invoke(new Action(() =>
            {
                imageViewBox.ShowImage(e.ImageData.Select(p => p.ToBitmap()).ToArray());
                imageViewBox.ShowXYT(e.XYT);
            }));
        }

        /// <summary>
        /// 拍照
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewBox_GrabRequest(object sender, GrabEventArgs e)
        {
            //查找配置信息
            var cfg = Global.Setting.ListGroup.Where(p => p.GroupName == e.GroupName).FirstOrDefault();
            if (cfg != null)
            {
                //获取Mac地址
                var lstMacAddr = cfg.ListCamera.Select(p => p.MacAddr).ToList();
                for (int i = 0; i < lstMacAddr.Count; i++)
                {
                    int index = i;
                    Task.Factory.StartNew(() =>
                    {
                        //获取相机的序号
                        int cameraIndex = PosCamera.GetCameraIndex(lstMacAddr[index]);
                        if (cameraIndex < 0) return;
                        //拍照
                        var data = PosCamera.Grab(cameraIndex);
                        Image img = data?.ToBitmap();
                        //显示图像
                        (sender as ImageViewBox).ShowImage(index, img);
                    });
                }
            }
        }

        /// <summary>
        /// 实时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewBox_LiveRequest(object sender, LiveEventArgs e)
        {
            //查找配置信息
            var cfg = Global.Setting.ListGroup.Where(p => p.GroupName == e.GroupName).FirstOrDefault();
            if (cfg != null)
            {
                //获取Mac地址
                var lstMacAddr = cfg.ListCamera.Select(p => p.MacAddr).ToList();
                for (int i = 0; i < lstMacAddr.Count; i++)
                {
                    int index = i;
                    //获取相机的序号
                    int cameraIndex = PosCamera.GetCameraIndex(lstMacAddr[i]);
                    if (cameraIndex < 0) continue;
                    //显示图像的委托
                    ShowLiveImage method = new ShowLiveImage(data =>
                    {
                        Image img = data?.ToBitmap();
                        //显示图像
                        (sender as ImageViewBox).ShowImage(index, img);
                    });
                    //切换Live模式
                    PosCamera.Live(cameraIndex, e.IsSetToLive, method);
                }
            }
        }

        /// <summary>
        /// 显示提示文本
        /// </summary>
        /// <param name="strMsg"></param>
        public void ShowMsg(string strMsg)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<string>(ShowMsg), strMsg);
            }
            else
            {
                this.txtMsg.Clear();
                this.txtMsg.Text = strMsg;
            }
        }

        /// <summary>
        /// Mark检测（单组）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnDetectMark_Click(object sender, EventArgs e)
        {
            await SingleImageTest(0);
        }

        /// <summary>
        /// 目标位检测（单组）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnDetectTarget_Click(object sender, EventArgs e)
        {
            await SingleImageTest(0);
        }

        /// <summary>
        /// 拍照测试--Mark
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnGrabTestMark_Click(object sender, EventArgs e)
        {
            await GrabTest(0);
        }

        /// <summary>
        /// 拍照测试--目标位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnGrabTestTarget_Click(object sender, EventArgs e)
        {
            await GrabTest(1);
        }

        /// <summary>
        /// 手动对位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnManualPos_Click(object sender, EventArgs e)
        {
            await ManualPos();
        }

        /// <summary>
        /// 手动标定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnManualCalibration_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show(LanguageHelper.Msg.ViewManual_ManualCalibrationTip,
                LanguageHelper.Msg.StrTip,
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dr != DialogResult.OK) return;

            await ManualCalibration();
        }

        /// <summary>
        /// 获取曝光时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetExposureTime1_Click(object sender, EventArgs e)
        {               
            try
            {
                this.txtExposureTime1.Text = string.Empty;

                var cfg = Global.Setting.ListGroup[groupIndex];
                if (cfg != null)
                {
                    if (cfg.ListCamera.Count > 0)
                    {
                        //获取指定的相机
                        var camera = PosCamera.GetCamera(cfg.ListCamera[0].MacAddr);

                        //获取曝光
                        float millisecond = 0;
                        bool result = camera.GetExposure(ref millisecond);
                        if (result)
                        {
                            this.txtExposureTime1.Text = millisecond.ToString();
                        }
                        else {
                            //--------------------------//
                            //Add By miaoliang
                            //Some Camera not support GetExposure Function,so use raw function
                            millisecond = (float)camera.GetFloatValue("ExposureTimeAbs");
                            this.txtExposureTime1.Text = (millisecond/1000).ToString();
                            //--------------------------//
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMsg(ex.ToString());
            }
        }

        /// <summary>
        /// 设定曝光时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetExposureTime1_Click(object sender, EventArgs e)
        {
            try
            {
                var cfg = Global.Setting.ListGroup[groupIndex];
                if (cfg != null)
                {
                    if (cfg.ListCamera.Count > 0)
                    {
                        //获取指定的相机
                        var camera = PosCamera.GetCamera(cfg.ListCamera[0].MacAddr);
                        //设置曝光
                        float millisecond = Convert.ToSingle(this.txtExposureTime1.Text);

                        bool result = camera.SetExposure(millisecond);
                        if (!result)
                        {
                            cfg.ListCamera[0].ExposureTime = millisecond;
                            //--------------------------//
                            //Add By miaoliang
                            //Some Camera not support GetExposure Function,so use raw function
                            camera.SetFloatValue("ExposureTimeAbs", millisecond * 1000);
                            //--------------------------//
                        }
                        Global.Setting.ExposureTime[0] = millisecond;
                        Global.SaveSettings();
                        //保存设置
                        camera.UserSetSave();
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMsg(ex.ToString());
            }
        }

        /// <summary>
        /// 获取曝光时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetExposureTime2_Click(object sender, EventArgs e)
        {
            try
            {
                this.txtExposureTime2.Text = string.Empty;

                var cfg = Global.Setting.ListGroup[groupIndex];
                if (cfg != null)
                {
                    if (cfg.ListCamera.Count > 1)
                    {
                        //获取指定的相机
                        var camera = PosCamera.GetCamera(cfg.ListCamera[1].MacAddr);
                        //获取曝光
                        float millisecond = 0;
                        bool result = camera.GetExposure(ref millisecond);
                        if (result)
                        {
                            cfg.ListCamera[1].ExposureTime = millisecond;
                            this.txtExposureTime2.Text = millisecond.ToString();
                        }
                        else
                        {
                            //Some Camera not support GetExposure Function,so use raw function
                            millisecond = (float)camera.GetFloatValue("ExposureTimeAbs");
                            this.txtExposureTime2.Text = (millisecond / 1000).ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMsg(ex.ToString());
            }
        }

        /// <summary>
        /// 设定曝光时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetExposureTime2_Click(object sender, EventArgs e)
        {
            try
            {
                var cfg = Global.Setting.ListGroup[groupIndex];
                if (cfg != null)
                {
                    if (cfg.ListCamera.Count > 1)
                    {
                        //获取指定的相机
                        var camera = PosCamera.GetCamera(cfg.ListCamera[1].MacAddr);
                        //设置曝光
                        float millisecond = Convert.ToSingle(this.txtExposureTime2.Text);
                        bool result = camera.SetExposure(millisecond);
                        if (!result)
                        {
                            //Some Camera not support GetExposure Function,so use raw function
                            camera.SetFloatValue("ExposureTimeAbs", millisecond * 1000);
                        }
                        Global.Setting.ExposureTime[1] = millisecond;
                        Global.SaveSettings();
                        //保存设置
                        camera.UserSetSave();
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMsg(ex.ToString());
            }
        }

        /// <summary>
        /// 单组图片检测
        /// </summary>
        /// <param name="detectMode">检测模式 0：Mark图片 1：目标位图片</param>
        /// <returns></returns>
        private async Task SingleImageTest(int detectMode)
        {
            try
            {
                var cfg = Global.Setting.ListGroup[groupIndex];
                if (cfg != null)
                {
                    // 选择文件
                    OpenFileDialog dlg = new OpenFileDialog();
                    dlg.Filter = LanguageHelper.Msg.ViewManual_OpenFileFilter;
                    dlg.Multiselect = true;
                    DialogResult dr = dlg.ShowDialog();
                    if (dr != DialogResult.OK) return;

                    // 创建图像数据生产者
                    List<IImageProducer> lst = new List<IImageProducer>();
                    for (int i = 0; i < dlg.FileNames.Length; i++)
                    {
                        lst.Add(new FileImageProducer(dlg.FileNames[i]));                        
                    }

                    // 初始化对位参数
                    WorkFlowPara para = new WorkFlowPara();
                    para.ImageProducers = lst.ToArray();
                    para.GroupIndex = groupIndex;
                    para.StageIndex = stageIndex;
                    para.DetectMode = detectMode;
                    para.IsImgTestMode = true;
                    para.IsSwapXY = cfg.IsSwapXY;
                    para.IsOutputColorMode = Global.Setting.IsOutputColorMode;
                    //异步执行对位
                    WorkFlowResult result = Global.Manager.StartManualPosAsync(para);
                    ShowMsg(result.ToString());
                }
            }
            catch (Exception ex)
            {
                ShowMsg(ex.ToString());
            }
        }

        /// <summary>
        /// 拍照测试
        /// </summary>
        /// <param name="detectMode">检测模式 0：Mark图片 1：目标位图片</param>
        /// <returns></returns>
        private async Task GrabTest(int detectMode)
        {
            try
            {
                var cfg = Global.Setting.ListGroup[groupIndex];
                if (cfg != null)
                {
                    // 创建图像数据生产者
                    List<IImageProducer> producers = new List<IImageProducer>();
                    for (int i = 0; i < cfg.ListCamera.Count; i++)
                    {
                        var camera = PosCamera.GetCamera(cfg.ListCamera[i].MacAddr);        
                        RotateFlipType rotateFlipType = cfg.ListCamera[i].RotateFlipType;
                        producers.Add(new CameraImageProducer(camera, cfg.IsUseGrabLock, rotateFlipType));
                    }

                    //初始化参数
                    WorkFlowPara para = new WorkFlowPara();
                    para.ImageProducers = producers.ToArray();
                    para.GroupIndex = groupIndex;
                    para.StageIndex = stageIndex;
                    para.DetectMode = detectMode;
                    para.IsImgTestMode = false;
                    para.IsGrabTestMode = true;
                    para.IsManualMode = false;
                    para.RetryLimitCount = Global.Setting.RetryLimitCount;
                    para.MoveFlowType = Global.Setting.MoveFlowType;
                    para.IsSwapXY = cfg.IsSwapXY;
                    para.IsConfirmRequired = cfg.IsConfirmRequired;
                    para.ThreXYT = cfg.ThreXYT;
                    para.IsAlgorithmShield = Global.Setting.IsAlgorithmShield;
                    para.IsOutputColorMode = Global.Setting.IsOutputColorMode;
                    //异步执行对位
                    WorkFlowResult result = await Global.Manager.StartAutoPosAsync(para);
                    ShowMsg(result.ToString());
                }
            }
            catch (Exception ex)
            {
                ShowMsg(ex.ToString());
            }
        }

        /// <summary>
        /// 手动对位
        /// </summary>
        /// <returns></returns>
        private async Task ManualPos()
        {
            try
            {
                var cfg = Global.Setting.ListGroup[groupIndex];
                if (cfg != null)
                {
                    if (!PosPlatform.IsSafeToMove(cfg.GroupName))
                    {
                        ShowMsg(LanguageHelper.Msg.ViewManual_SafetyConditionOff);
                        return;
                    }

                    // 创建图像数据生产者
                    List<IImageProducer> producers = new List<IImageProducer>();
                    for (int i = 0; i < cfg.ListCamera.Count; i++)
                    {
                        var camera = PosCamera.GetCamera(cfg.ListCamera[i].MacAddr);
                        RotateFlipType rotateFlipType = cfg.ListCamera[i].RotateFlipType;
                        producers.Add(new CameraImageProducer(camera, cfg.IsUseGrabLock, rotateFlipType));
                    }

                    // 根据设置中的类型创建运动执行者
                    IMoveExecutor executor = cfg.MoveExecutorType == MoveExecutorType.XYTPlatform ?
                        new XytMoveExecutor(PosPlatform.GetPlatform(cfg.GroupName)) :
                        (IMoveExecutor)PosSignal.GetSignalProvider();

                    // 初始化对位参数
                    WorkFlowPara para = new WorkFlowPara();
                    para.ImageProducers = producers.ToArray();
                    para.MoveExecutor = executor;
                    para.GroupIndex = groupIndex;
                    para.StageIndex = stageIndex;
                    para.DetectMode = 0;
                    para.IsImgTestMode = false;
                    para.IsGrabTestMode = false;
                    para.IsManualMode = true;
                    para.RetryLimitCount = Global.Setting.RetryLimitCount;
                    para.MoveFlowType = Global.Setting.MoveFlowType;
                    para.IsSwapXY = cfg.IsSwapXY;
                    para.IsConfirmRequired = cfg.IsConfirmRequired;
                    para.ThreXYT = cfg.ThreXYT;
                    para.MaxMoveDistance = cfg.MaxMoveDistance;
                    para.IsAlgorithmShield = Global.Setting.IsAlgorithmShield;
                    para.IsOutputColorMode = Global.Setting.IsOutputColorMode;
                    // 异步执行自动对位流程
                    WorkFlowResult result = await Global.Manager.StartAutoPosAsync(para);
                    ShowMsg(result.ToString());
                }
            }
            catch (Exception ex)
            {
                ShowMsg(ex.ToString());
            }
        }

        /// <summary>
        /// 手动标定
        /// </summary>
        /// <returns></returns>
        private async Task ManualCalibration()
        {
            try
            {
                var cfg = Global.Setting.ListGroup[groupIndex];
                if (cfg != null)
                {
                    // 创建图像数据生产者
                    List<IImageProducer> producers = new List<IImageProducer>();
                    for (int i = 0; i < cfg.ListCamera.Count; i++)
                    {
                        var camera = PosCamera.GetCamera(cfg.ListCamera[i].MacAddr);
                        RotateFlipType rotateFlipType = cfg.ListCamera[i].RotateFlipType;
                        producers.Add(new CameraImageProducer(camera, cfg.IsUseGrabLock, rotateFlipType));
                    }

                    // 初始化对位参数
                    WorkFlowPara para = new WorkFlowPara();
                    para.ImageProducers = producers.ToArray();
                    para.GroupIndex = groupIndex;
                    para.StageIndex = stageIndex;
                    para.IsManualMode = true;
                    para.IsOutputColorMode = Global.Setting.IsOutputColorMode;
                    // 异步执行标定流程
                    WorkFlowResult result = await Global.Manager.StartCalibrationAsync(para);
                    ShowMsg(result.ToString());
                }
            }
            catch (Exception ex)
            {
                ShowMsg(ex.ToString());
            }
        }
    }
}
