namespace HYC.SaturnMain.SubForms
{
    partial class FrmPrbAutoChangeTip
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTipMsg = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTipMsg
            // 
            this.lblTipMsg.BackColor = System.Drawing.Color.White;
            this.lblTipMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTipMsg.Font = new System.Drawing.Font("微软雅黑", 60F);
            this.lblTipMsg.ForeColor = System.Drawing.Color.OrangeRed;
            this.lblTipMsg.Location = new System.Drawing.Point(1, 36);
            this.lblTipMsg.Margin = new System.Windows.Forms.Padding(0);
            this.lblTipMsg.Name = "lblTipMsg";
            this.lblTipMsg.Size = new System.Drawing.Size(805, 526);
            this.lblTipMsg.TabIndex = 1;
            this.lblTipMsg.Text = "自动更换Probe中，请勿进行其他操作！";
            this.lblTipMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmPrbAutoChangeTip
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(807, 563);
            this.CloseFormTimeLag = 0;
            this.Controls.Add(this.lblTipMsg);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "FrmPrbAutoChangeTip";
            this.ShowFormTimelag = 0;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmPrbAutoChangeTip";
            this.Controls.SetChildIndex(this.lblTipMsg, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTipMsg;
    }
}