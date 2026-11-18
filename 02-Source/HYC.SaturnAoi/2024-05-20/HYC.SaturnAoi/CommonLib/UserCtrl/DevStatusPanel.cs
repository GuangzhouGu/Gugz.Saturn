/*----------------------------------------------------------------
// Copyright (C) Suzhou HYC Technology Co.,LTD
// 版权所有。
//
// =================================
// CLR版本     ：4.0.30319.42000
// 命名空间    ：CommonLib.UserCtrl
// 文件名称    ：DevStatusIndicator.cs
// =================================
// 创 建 者    ：gugz
// 创建日期    ：2020/11/3 14:50:36 
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

namespace CommonLib.UserCtrl
{
    /// <summary>
    /// 所有组件的状态指示控件
    /// </summary>
    public class DevStatusPanel : Panel
    {
        //状态列表
        private List<StatusItem> lstStatus = new List<StatusItem>();
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
        public void UpdateDeviceState(string devName, bool status)
        {
            lock (lockObj)
            {
                var lst = lstStatus.Where(p => p.DevName == devName).ToList();
                if (lst.Count > 0)
                {
                    // 控件已添加，更新控件
                    foreach (Control ctrl in this.Controls)
                    {
                        DevStatusIndicator indicator = ctrl as DevStatusIndicator;
                        if (indicator.DevName == devName)
                        {
                            indicator.Status = status;
                            break;
                        }
                    }
                    lst[0].DevName = devName;
                    lst[0].Status = status;
                }
                else
                {
                    // 控件未添加，新建控件
                    DevStatusIndicator item = new DevStatusIndicator();
                    item.DevName = devName;
                    item.Status = status;
                    item.Dock = DockStyle.Top;
                    
                    // 排序
                    lstStatus.Add(new StatusItem() { DevName = devName, Status = status });
                    lstStatus.Sort((x, y) => y.DevName.CompareTo(x.DevName));

                    this.SuspendLayout();
                    this.Controls.Add(item);
                    int index = 0;
                    foreach (Control ctrl in this.Controls)
                    {
                        DevStatusIndicator indicator = ctrl as DevStatusIndicator;
                        indicator.DevName = lstStatus[index].DevName;
                        indicator.Status = lstStatus[index].Status;
                        index++;
                    }
                    this.ResumeLayout();
                }
            }
        }

        /// <summary>
        /// 状态项目
        /// </summary>
        public class StatusItem
        {
   
            /// <summary>
            /// 显示名称
            /// </summary>
            public string DevName { get; set; }
            /// <summary>
            /// 状态
            /// </summary>
            public bool Status { get; set; }
        }
    }
}
