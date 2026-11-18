using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HYC.HTPCBase.MotionInfo;
using HYC.HTPCBase.Motion;
using HYC.HTPCBase.Entities;
using HYC.HTPCBase.Extension.Entities;
using HYC.HTPCBase.Extension.UserCtrl;
using System.Threading;

namespace HYC.HTPCBase.UserCtrl
{
    public partial class ZeroSysCtrl : UcBase
    {
        public ZeroSysCtrl()
        {
            InitializeComponent();
        }
        public event Func<bool> OnIsCanUnlock;
        public event Func<bool> OnIsCanLock;
        /// <summary>
        /// 零点系统闭锁输出名称
        /// </summary>
        [Description("零点系统闭锁输出名称")]
        public string ZeroSysLock { get; set; }
        /// <summary>
        /// 零点系统解锁输出名称
        /// </summary>
        [Description("零点系统解锁输出名称")]
        public string ZeroSysUnLock { get; set; }

        private string zeroSysName = string.Empty;
        /// <summary>
        /// 零点系统名称
        /// </summary>
        [Description("零点系统中文名称")]
        public string ZeroSysName
        {
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    zeroSysName = value;
                    this.lblName.Text = value;
                }
            }
            get
            {
                return zeroSysName;
            }
        }
        private void GetZeroSysSts()
        {
            try
            {
                if (!string.IsNullOrEmpty(ZeroSysLock)&&!string.IsNullOrEmpty(ZeroSysUnLock))
                {
                    bool lockSts = IOInfoHelper.GetOutputValueByName(ZeroSysLock);
                    bool unlockSts = IOInfoHelper.GetOutputValueByName(ZeroSysUnLock);
                    UpdateOutput(unlockSts, lockSts);
                }           
            }
            catch (Exception)
            {
            }
        }
     
        private void UpdateOutput(bool unLockSts, bool lockSts)
        {
            try
            {
                if (InvokeRequired && !this.IsDisposed)
                {
                    Invoke(new Action<bool, bool>(UpdateOutput), unLockSts, lockSts);
                }
                else
                {
                    pbxUnlockSts.Image = unLockSts ? HYC.SaturnMain.Properties.Resources.Green2 : HYC.SaturnMain.Properties.Resources.Gray2;
                    pbxLockSts.Image = lockSts ? HYC.SaturnMain.Properties.Resources.Green2 : HYC.SaturnMain.Properties.Resources.Gray2;
                    if (unLockSts&&!lockSts)
                    {
                        pictureBox1.Image = HYC.SaturnMain.Properties.Resources.unLock;
                    }
                    else if (!unLockSts && lockSts)
                    {
                        pictureBox1.Image = HYC.SaturnMain.Properties.Resources._lock;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void ZeroSysCtrl_Load(object sender, EventArgs e)
        {
    
        }

        private void btnZeroUnlock_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否松开零点系统？","Question",MessageBoxButtons.OKCancel)==DialogResult.OK)
            {
                if (OnIsCanUnlock!=null)
                {
                    bool ret = OnIsCanUnlock.Invoke();
                    if (ret)
                    {
                        MotionManager.GetInstance().SetDigitalOutput(ZeroSysLock, false);
                        MotionManager.GetInstance().SetDigitalOutput(ZeroSysUnLock, true);
                        Thread.Sleep(50);
                        GetZeroSysSts();
                    }
                }
                else
                {
                    MotionManager.GetInstance().SetDigitalOutput(ZeroSysLock, false);
                    MotionManager.GetInstance().SetDigitalOutput(ZeroSysUnLock, true);
                    Thread.Sleep(50);
                    GetZeroSysSts();
                }
            }
        }

        private void btnZeroLock_Click(object sender, EventArgs e)
        {
            MotionManager.GetInstance().SetDigitalOutput(ZeroSysUnLock, false);
            MotionManager.GetInstance().SetDigitalOutput(ZeroSysLock, true);
            Thread.Sleep(50);
            GetZeroSysSts();
        }

        private void ZeroSysCtrl_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                bool value = CheckFrmMode.IsDesingMode();
                if (value == false)
                {
                    GetZeroSysSts();
                }
            }
        }
    }
}
