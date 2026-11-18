using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HYC.SaturnMain.Devices.EfuController
{
    public partial class EFUFans : UserControl
    {
        public EFUFans()
        {
            InitializeComponent();
   
        }
        public static event Func<int,int,bool> OnSetValue;
        public static event Action<int, string> OnShowmsg;
        private int efuNum = 0;
        /// <summary>
        /// 风扇序号，不能重复
        /// </summary>
        public int EfuNum
        {
            set
            {
                efuNum = value;
                lbl_index.Text = efuNum.ToString();
            }
            get { return efuNum; }
        }
        private void btn_set_Click(object sender, EventArgs e)
        {
            try
            {
                int setValue = int.Parse(tbx_setValue.Text);
               bool rtn= (bool)OnSetValue?.Invoke(EfuNum, setValue);
                if(rtn)
                {
                    OnShowmsg?.Invoke(EfuNum, "设置转速成功");
                }
                else
                {
                    OnShowmsg?.Invoke(EfuNum, "设置转速失败");
                }
            }
            catch (Exception ex)
            {
                OnShowmsg?.Invoke(EfuNum, "设置转速异常");
            }
        }
        public void UpdateValue(EfuValue info)
        {
            try
            {
                if (InvokeRequired)
                {
                    this.Invoke(new Action<EfuValue>(UpdateValue), info);
                }
                else
                {
                    lbl_actValue.Text = info.ActValue.ToString();
                    lbl_setValue.Text = info.SetValue.ToString();
                    lbl_sts.Text = info.FanStatus;
                    htAnalogMeter1.Value = info.ActValue;
                }

            }
            catch (Exception ex)
            {
            }
        }
    }
}
