/*----------------------------------------------------------------
// Copyright © 2021 HYC. All Rights
// 版权所有。
//
// =================================
// CLR版本     ：4.0.30319.42000
// 命名空间    ：HYC.Mt2Aoi.Controls
// 文件名称    ：UcTester.cs
// =================================
// 创 建 者    ：gugz
// 创建日期    ：2022/1/15 10:51:38 
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
using CommonLib.Entities;
using HYC.SaturnAoi.Devices.Tester;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HYC.Mt2Aoi.Controls
{
    /// <summary>
    /// 检查机控制
    /// </summary>
    public partial class UcTester : UserControl
    {
        public static event Action<string, Brush> OnShowMsg;
        /// <summary>
        /// 构造方法
        /// </summary>
        public UcTester()
        {
            InitializeComponent();

        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void InitCtrl()
        {
            if (ELPTesterHelper.Instance != null)
            {
                for (int i = 0; i < ELPTesterHelper.Instance.TesterCount; i++)
                {
                    this.cboTester.Items.Add("检查机" + (i + 1));
                }
                this.cboTester.SelectedIndex = 0;
            }

        }

        /// <summary>
        /// 开电
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTesterPowerOn_Click(object sender, EventArgs e)
        {
            this.txtResult.Text = "";
            int index = this.cboTester.SelectedIndex;
            SetBtnEnable(false);
            Task.Factory.StartNew(() =>
            {
                try
                {
                    bool ret = ELPTesterHelper.Instance.GetTester(index).PowerOn();
                    this.Invoke(new Action(() =>
                    {
                        this.txtResult.Text = ret ? "OK" : "NG";
                    }));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    SetBtnEnable(true);
                }
            });
        }

        /// <summary>
        /// 关电
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTesterPowerOff_Click(object sender, EventArgs e)
        {
            this.txtResult.Text = "";
            int index = this.cboTester.SelectedIndex;
            SetBtnEnable(false);
            Task.Factory.StartNew(() =>
            {
                try
                {
                    bool ret = ELPTesterHelper.Instance.GetTester(index).PowerOff();
                    this.Invoke(new Action(() =>
                    {
                        this.txtResult.Text = ret ? "OK" : "NG";
                    }));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    SetBtnEnable(true);
                }
            });
        }

        /// <summary>
        /// 画面上切
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTesterPrePattern_Click(object sender, EventArgs e)
        {
            this.txtResult.Text = "";
            int index = this.cboTester.SelectedIndex;
            SetBtnEnable(false);
            Task.Factory.StartNew(() =>
            {
                try
                {
                    bool ret = ELPTesterHelper.Instance.GetTester(index).PatternPrevious();
                    this.Invoke(new Action(() =>
                    {
                        this.txtResult.Text = ret ? "OK" : "NG";
                    }));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    SetBtnEnable(true);
                }
            });
        }

        /// <summary>
        /// 画面下切
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTesterNextPattern_Click(object sender, EventArgs e)
        {
            this.txtResult.Text = "";
            int index = this.cboTester.SelectedIndex;
            SetBtnEnable(false);
            Task.Factory.StartNew(() =>
            {
                try
                {
                    bool ret = ELPTesterHelper.Instance.GetTester(index).PatternNext();
                    this.Invoke(new Action(() =>
                    {
                        this.txtResult.Text = ret ? "OK" : "NG";
                    }));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    SetBtnEnable(true);
                }
            });
        }

        /// <summary>
        /// 按名称跳转画面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNameStep_Click(object sender, EventArgs e)
        {
            this.txtResult.Text = "";
            int index = this.cboTester.SelectedIndex;
            string name = this.txtPattName.Text;
            SetBtnEnable(false);
            Task.Factory.StartNew(() =>
            {
                try
                {
                    //bool ret = ELPTesterHelper.Instance.GetTester(index).PatternDisplay(name);
                    //this.Invoke(new Action(() =>
                    //{
                    //    this.txtResult.Text = ret ? "OK" : "NG";
                    //}));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    SetBtnEnable(true);
                }
            });
        }
        /// <summary>
        /// RGB切图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRGBStep_Click(object sender, EventArgs e)
        {
            this.txtResult.Text = "";
            int index = this.cboTester.SelectedIndex;

            SetBtnEnable(false);
            Task.Factory.StartNew(() =>
            {
                try
                {
                    //bool ret = ELPTesterHelper.Instance.GetTester(index).PatternDisplay(uint.Parse(num_R.Value.ToString()), uint.Parse(num_G.Value.ToString()), uint.Parse(num_B.Value.ToString()));
                    //this.Invoke(new Action(() =>
                    //{
                    //    this.txtResult.Text = ret ? "OK" : "NG";
                    //}));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    SetBtnEnable(true);
                }
            });
        }
        /// <summary>
        /// 设置按钮的启用状态
        /// </summary>
        /// <param name="isEnabled"></param>
        private void SetBtnEnable(bool isEnabled)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<bool>(SetBtnEnable), isEnabled);
            }
            else
            {
                this.btnTesterPowerOn.Enabled = isEnabled;
                this.btnTesterPowerOff.Enabled = isEnabled;
                this.btnTesterPrePattern.Enabled = isEnabled;
                this.btnTesterNextPattern.Enabled = isEnabled;

            }
        }
        /// <summary>
        /// 初始化检查机及对应检查机脚本
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UcTester_Load(object sender, EventArgs e)
        {
            if (ClassHelper.IsDesingMode() == false)
            {
                InitCtrl();
            }

        }
  
        /// <summary>
        /// 检查机开电
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPowerOnSts_Click(object sender, EventArgs e)
        {
            this.txtResult.Text = "";
            int index = this.cboTester.SelectedIndex;
            string name = this.txtPattName.Text;
            SetBtnEnable(false);
            Task.Factory.StartNew(() =>
            {
                try
                {
                    var ret = ELPTesterHelper.Instance.GetTester(index).GetStatus();
                    this.Invoke(new Action(() =>
                    {
                        this.txtResult.Text = ret;
                    }));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    SetBtnEnable(true);
                }
            });
        }
        /// <summary>
        /// 按照序号切换画面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSwitchByNo_Click(object sender, EventArgs e)
        {
            this.txtResult.Text = "";
            int index = this.cboTester.SelectedIndex;
            int no = int.Parse(this.tbxPatternNo.Text);
            SetBtnEnable(false);
            Task.Factory.StartNew(() =>
            {
                try
                {
                    bool ret = false;
                    ret = ELPTesterHelper.Instance.GetTester(index).PatternDisplay(no);
                    this.Invoke(new Action(() =>
                    {
                        this.txtResult.Text = ret ? "OK" : "NG";
                    }));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    SetBtnEnable(true);
                }
            });
        }

        private void btnShiftRecipe_Click(object sender, EventArgs e)
        {
            int index = this.cboTester.SelectedIndex;
            var recipeName = txtRecipeName.Text;
            if (string.IsNullOrEmpty(recipeName))
            {
                MessageBox.Show("机种名称为空，请输入机种名");
            }
            ELPTesterHelper.Instance.GetTester(index).DownLoadRecipe(1,1,recipeName);
        }

        private void btnGetCurRecipe_Click(object sender, EventArgs e)
        {
            txtRecipeName.Text = string.Empty;
            int index = this.cboTester.SelectedIndex;
            var recipeName = ELPTesterHelper.Instance.GetTester(index).GetCurRecipe();
            txtRecipeName.Text = recipeName;
        }

        private void btnGetCurPatternName_Click(object sender, EventArgs e)
        {
            txtPattName.Text = string.Empty;
            int index = this.cboTester.SelectedIndex;
            var patternName = ELPTesterHelper.Instance.GetTester(index).GetPatternName();
            txtPattName.Text = patternName;
        }

        private void btnReadVoltage_Click(object sender, EventArgs e)
        {
            try
            {
                int ptnIndex = int.Parse(txtPattName.Text);
                int index = this.cboTester.SelectedIndex;
                var value = ELPTesterHelper.Instance.GetTester(index).VoltageGet((byte)ptnIndex);
                txtVoltage.Text = value;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnSetVoltage_Click(object sender, EventArgs e)
        {
            int ptnIndex = int.Parse(txtPattName.Text);
            int index = this.cboTester.SelectedIndex;
            var value = ELPTesterHelper.Instance.GetTester(index).VoltageSet((byte)ptnIndex,3000,2000,3000);
        }
    }
}
