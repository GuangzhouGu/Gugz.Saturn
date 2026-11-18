/*----------------------------------------------------------------
// Copyright (C) Suzhou HYC Technology Co.,LTD
// 版权所有。
//
// =================================
// CLR版本     ：4.0.30319.42000
// 命名空间    ：HYC.AutoPosition.UI.Controls.Template
// 文件名称    ：SubMarkSetting.cs
// =================================
// 创 建 者    ：miaoliang
// 创建日期    ：2020/10/30 13:45:05 
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
    partial class TemplateMatchingSetting
    {
        /// <summary>
        /// 初始化Align 参数设置
        /// </summary>
        private void AlignParameterInitGUI()
        {
            txtMatchingRateLimitObjectCamera1.Text = curRecipe.AlignParameter.MatchingRateLimitCamera[0].ToString();
            txtMatchingRateLimitObjectCamera2.Text = curRecipe.AlignParameter.MatchingRateLimitCamera[1].ToString();
            txtAlignLimitOKX.Text = curRecipe.AlignParameter[0].X.ToString();
            txtAlignLimitOKY.Text = curRecipe.AlignParameter[0].Y.ToString();
            txtAlignLimitOKTheta.Text = curRecipe.AlignParameter[0].T.ToString();
            txtAlignLimitOverRangeX.Text = curRecipe.AlignParameter[1].X.ToString();
            txtAlignLimitOverRangeY.Text = curRecipe.AlignParameter[1].Y.ToString();
            txtAlignLimitOverRangeTheta.Text = curRecipe.AlignParameter[1].T.ToString();

        }
        /// <summary>
        /// Matching Rate Limit 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMatchingRateLimitSave_Click(object sender, EventArgs e)
        {
            double dValue = 0;
            if (ValidationValue(txtMatchingRateLimitObjectCamera1.Text, out dValue))
            {
                curRecipe.AlignParameter.MatchingRateLimitCamera[0] = dValue;
            }
            if (ValidationValue(txtMatchingRateLimitObjectCamera2.Text, out dValue))
            {
                curRecipe.AlignParameter.MatchingRateLimitCamera[1] = dValue;
            }
            curRecipe.AlignParameterSerialize();
        }
        private bool ValidationValue(string text, out double value)
        {
            bool ret = double.TryParse(text, out double value2);
            if (ret)
            {
                if (Math.Abs(value2) >= 1)
                {
                    MessageBox.Show("匹配率范围0.5到0.99，请重新输入");
                    value = 0.8;
                    return false;
                }
                else
                {
                    value = value2;
                    return true;
                }
            }
            else
            {
                MessageBox.Show("请输入正确的数字");
                value = 0.8;
                return false;
            }
        }
        /// <summary>
        /// 对位极限保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAlignLimitSave_Click(object sender, EventArgs e)
        {
            double dValue = 0;
            if (ValueIsValid(txtAlignLimitOKX.Text, out dValue))
            {
                curRecipe.AlignParameter[0].X = dValue;
            }
            if (ValueIsValid(txtAlignLimitOKY.Text, out dValue))
            {
                curRecipe.AlignParameter[0].Y = dValue;
            }
            if (ValueIsValid(txtAlignLimitOKTheta.Text, out dValue))
            {
                curRecipe.AlignParameter[0].T = dValue;
            }

            if (ValueIsValid(txtAlignLimitOverRangeX.Text, out dValue))
            {
                curRecipe.AlignParameter[1].X = dValue;
            }
            if (ValueIsValid(txtAlignLimitOverRangeY.Text, out dValue))
            {
                curRecipe.AlignParameter[1].Y = dValue;
            }
            if (ValueIsValid(txtAlignLimitOverRangeTheta.Text, out dValue))
            {
                curRecipe.AlignParameter[1].T = dValue;
            }
            curRecipe.AlignParameterSerialize();
        }

        /// <summary>
        /// 对位选项保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAlignOptionSave_Click(object sender, EventArgs e)
        {

        }
    }
}
