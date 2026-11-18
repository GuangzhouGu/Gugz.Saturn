/*----------------------------------------------------------------
// Copyright (C) Suzhou HYC Electronic Technology Co.,LTD
// 版权所有。
//
// =================================
// CLR版本     ：4.0.30319.42000
// 命名空间    ：HYC.AutoPosition.UI.Controls
// 文件名称    ：DevStatusPanel.cs
// =================================
// 创 建 者    ：yangkang
// 创建日期    ：2020/2/16 14:10:44 
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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HYC.AutoPosition.UI.Controls
{
    /// <summary>
    /// 所有组件的状态指示控件
    /// </summary>
    public class DevStatusPanel : Panel
    {
        //状态指示控件列表
        private List<DevStatusIndicator> lstIndicators = new List<DevStatusIndicator>();
        private object lockObj = new object();

        /// <summary>
        /// 构造方法
        /// </summary>
        public DevStatusPanel()
        {
            AutoScroll = true;
        }

        /// <summary>
        /// 更新状态显示
        /// </summary>
        /// <param name="ctrlName">控件名</param>
        /// <param name="devName">组件名</param>
        /// <param name="status">状态</param>
        public void UpdateDeviceState(string ctrlName, string devName, bool? status)
        {
            lock (lockObj)
            {
                var lst = lstIndicators.Where(p => p.Name == ctrlName).ToList();
                if (lst.Count > 0)
                {
                    //控件已添加，更新控件
                    lst[0].DevName = devName;
                    lst[0].Status = status;
                }
                else
                {
                    //控件未添加，新建控件
                    DevStatusIndicator item = new DevStatusIndicator();
                    item.Name = ctrlName;
                    item.DevName = devName;
                    item.Status = status;
                    item.Dock = DockStyle.Top;
                    lstIndicators.Add(item);
                    lstIndicators.Sort((x, y) => y.DevName.CompareTo(x.DevName));
                    //执行布局
                    this.SuspendLayout();
                    this.Controls.Clear();
                    this.Controls.AddRange(lstIndicators.ToArray());
                    this.ResumeLayout();
                }
            }
        }
    }
}
