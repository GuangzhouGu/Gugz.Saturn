/*----------------------------------------------------------------
// Copyright (C) Suzhou HYC Electronic Technology Co.,LTD
// 版权所有。
//
// =================================
// CLR版本     ：4.0.30319.42000
// 命名空间    ：HYC.AutoPosition.UI.View
// 文件名称    ：ViewMain.cs
// =================================
// 创 建 者    ：yangkang
// 创建日期    ：2020/1/2 18:06:47 
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
using HYC.AutoPosition.UI.Controls;
using HYC.AutoPosition.UI.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HYC.AutoPosition.UI.View
{
    /// <summary>
    /// 主视图，对位监控
    /// </summary>
    public partial class ViewMain : ViewBase
    {
        // 图像显示控件列表
        private List<ImageViewBox> lst = new List<ImageViewBox>();

        /// <summary>
        /// 构造方法
        /// </summary>
        public ViewMain()
        {
            InitializeComponent();
            Global.Manager.ImageReceived += Manager_ImageReceived;
        }

        /// <summary>
        /// 更新界面
        /// </summary>
        public override void UpdateView()
        {
            if (lst.Count != Global.Setting.ListGroup.Count)
            {
                //ImageViewBox数量不符合要求，重新布局
                ControlsLayout();
            }
            else
            {
                //ImageViewBox数量符合要求，更新控件属性
                for (int i = 0; i < Global.Setting.ListGroup.Count; i++)
                {
                    lst[i].GroupName = Global.Setting.ListGroup[i].GroupName;
                    lst[i].CameraCount = Global.Setting.ListGroup[i].ListCamera.Count;
                }
            }
        }

        /// <summary>
        /// 控件布局
        /// </summary>
        private void ControlsLayout()
        {
            lst.Clear();
            this.Controls.Clear();
            //设置属性
            for (int i = 0; i < Global.Setting.ListGroup.Count; i++)
            {
                lst.Add(new ImageViewBox()
                {
                    Dock = DockStyle.Fill,
                    BorderStyle = BorderStyle.FixedSingle
                });
            }
            for (int i = 0; i < Global.Setting.ListGroup.Count; i++)
            {
                lst[i].GroupName = Global.Setting.ListGroup[i].GroupName;
                lst[i].CameraCount = Global.Setting.ListGroup[i].ListCamera.Count;
            }
            //监听事件
            foreach (var viewBox in lst)
            {
                viewBox.GrabRequest += ViewBox_GrabRequest;
                viewBox.LiveRequest += ViewBox_LiveRequest;
            }
            //设置布局
            if (Global.Setting.ListGroup.Count <= 4)
            {
                // 添加布局控件
                TableLayoutPanel tbpnl = new TableLayoutPanel();
                tbpnl.Dock = DockStyle.Fill;
                tbpnl.RowCount = 2;
                tbpnl.ColumnCount = 2;
                tbpnl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
                tbpnl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
                tbpnl.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
                tbpnl.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
                this.Controls.Add(tbpnl);
                // 添加显示控件
                if (Global.Setting.ListGroup.Count == 1)
                {
                    tbpnl.RowCount = 1;
                    tbpnl.ColumnCount = 1;
                    tbpnl.Controls.Add(lst[0], 0, 0);
                }
                else if (Global.Setting.ListGroup.Count == 2)
                {
                    tbpnl.RowCount = 2;
                    tbpnl.ColumnCount = 1;
                    tbpnl.Controls.Add(lst[0], 0, 0);
                    tbpnl.Controls.Add(lst[1], 0, 1);
                }
                else if (Global.Setting.ListGroup.Count == 3)
                {
                    tbpnl.RowCount = 2;
                    tbpnl.ColumnCount = 2;
                    tbpnl.Controls.Add(lst[0], 0, 0);
                    tbpnl.Controls.Add(lst[1], 1, 0);
                    tbpnl.Controls.Add(lst[2], 0, 1);
                }
                else if (Global.Setting.ListGroup.Count == 4)
                {
                    tbpnl.RowCount = 2;
                    tbpnl.ColumnCount = 2;
                    tbpnl.Controls.Add(lst[0], 0, 0);
                    tbpnl.Controls.Add(lst[1], 1, 0);
                    tbpnl.Controls.Add(lst[2], 0, 1);
                    tbpnl.Controls.Add(lst[3], 1, 1);
                }
            }
            else
            {
                TabControl tab = new TabControl();
                tab.Dock = DockStyle.Fill;
                this.Controls.Add(tab);

                int pageCount = Global.Setting.ListGroup.Count / 4 +
                    ((Global.Setting.ListGroup.Count % 4) > 0 ? 1 : 0);
                for (int i = 0; i < pageCount; i++)
                {
                    // 添加布局控件
                    TabPage page = new TabPage();
                    page.Text = "Groups" + (i + 1);
                    page.BackColor = Color.White;
                    tab.TabPages.Add(page);
                    TableLayoutPanel tbpnl = new TableLayoutPanel();
                    tbpnl.Dock = DockStyle.Fill;
                    tbpnl.RowCount = 2;
                    tbpnl.ColumnCount = 2;
                    tbpnl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
                    tbpnl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
                    tbpnl.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
                    tbpnl.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
                    page.Controls.Add(tbpnl);
                    // 添加显示控件
                    if (i != pageCount - 1)
                    {
                        tbpnl.Controls.Add(lst[4 * i + 0], 0, 0);
                        tbpnl.Controls.Add(lst[4 * i + 1], 1, 0);
                        tbpnl.Controls.Add(lst[4 * i + 2], 0, 1);
                        tbpnl.Controls.Add(lst[4 * i + 3], 1, 1);
                    }
                    else
                    {
                        if (Global.Setting.ListGroup.Count % 4 == 1)
                        {
                            tbpnl.Controls.Add(lst[4 * i + 0], 0, 0);
                        }
                        else if (Global.Setting.ListGroup.Count % 4 == 2)
                        {
                            tbpnl.Controls.Add(lst[4 * i + 0], 0, 0);
                            tbpnl.Controls.Add(lst[4 * i + 1], 0, 1);
                        }
                        else if (Global.Setting.ListGroup.Count % 4 == 3)
                        {
                            tbpnl.Controls.Add(lst[4 * i + 0], 0, 0);
                            tbpnl.Controls.Add(lst[4 * i + 1], 1, 0);
                            tbpnl.Controls.Add(lst[4 * i + 2], 0, 1);
                        }
                        else if (Global.Setting.ListGroup.Count % 4 == 0)
                        {
                            tbpnl.Controls.Add(lst[4 * i + 0], 0, 0);
                            tbpnl.Controls.Add(lst[4 * i + 1], 1, 0);
                            tbpnl.Controls.Add(lst[4 * i + 2], 0, 1);
                            tbpnl.Controls.Add(lst[4 * i + 3], 1, 1);
                        }
                    }

                }
            }
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
        /// 接收到对位图片及XYT参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Manager_ImageReceived(object sender, ImageReceivedArgs e)
        {
            GroupConfig cfg = Global.Setting.ListGroup[e.GroupIndex];
            //更新界面显示
            if (!e.IsImgTestMode && !e.IsGrabTestMode && !e.IsManualMode)
            {
                lst[e.GroupIndex].ShowImage(e.ImageData.Select(p => p.ToBitmap()).ToArray());
                this.Invoke(new Action(() =>
                {
                 
                    lst[e.GroupIndex].ShowXYT(e.XYT);
                }));
            }
            //如果需要保存图片
            if (Global.Setting.IsSaveImage && !e.IsImgTestMode)
            {
                //保存所有图片
                if (!Global.Setting.IsSaveNgImageOnly)
                {
                    //创建文件夹
                    string path = Path.Combine(Global.Setting.ImageSavePath,
                        DateTime.Now.ToString("yyyyMMdd"), cfg.GroupName, (e.StageIndex + 1).ToString());
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    //保存图片
                    string[] filenames = new string[2];
                    string time = DateTime.Now.ToString("HHmmss");
                    filenames[0] = Path.Combine(path, $"{time}_{e.ProcessIndex}_{e.ImageType}_Left.bmp");
                    filenames[1] = Path.Combine(path, $"{time}_{e.ProcessIndex}_{e.ImageType}_Right.bmp");
                    Task.Factory.StartNew(() =>
                    {
                        Bitmap[] bitmaps = e.ImageData.Select(p => p.ToBitmap()).ToArray();
                        for (int i = 0; i < bitmaps.Length; i++)
                        {
                                    bitmaps[i].Save(filenames[i], ImageFormat.Bmp);
                        }
                    });
                }

                        //保存处理NG的图片
                if (e.IsProcessedNG)
                {
                    //创建文件夹
                    string path = Path.Combine(Global.Setting.ImageSavePath, "NG",
                        DateTime.Now.ToString("yyyyMMdd"), cfg.GroupName, (e.StageIndex + 1).ToString());
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    //保存图片
                    string[] filenames = new string[2];
                    string time = DateTime.Now.ToString("HHmmss");
                    filenames[0] = Path.Combine(path, $"{time}_{e.ProcessIndex}_{e.ImageType}_Left.bmp");
                    filenames[1] = Path.Combine(path, $"{time}_{e.ProcessIndex}_{e.ImageType}_Right.bmp");
                    Task.Factory.StartNew(() =>
                    {
                        Bitmap[] bitmaps = e.ImageData.Select(p => p.ToBitmap()).ToArray();
                        for (int i = 0; i < bitmaps.Length; i++)
                        {
                            bitmaps[i].Save(filenames[i], ImageFormat.Bmp);
                        }
                    });
                }
            }
        }
    }
}
