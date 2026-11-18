namespace HYC.SaturnMain.SubForms
{
    partial class FrmManualBarCode
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
            this.tbxBarcode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.lblTipMsg = new System.Windows.Forms.Label();
            this.cbxUseUpstreamGlassID = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // tbxBarcode
            // 
            this.tbxBarcode.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbxBarcode.Location = new System.Drawing.Point(180, 272);
            this.tbxBarcode.Name = "tbxBarcode";
            this.tbxBarcode.Size = new System.Drawing.Size(250, 29);
            this.tbxBarcode.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(122, 275);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 21);
            this.label1.TabIndex = 3;
            this.label1.Text = "条码：";
            // 
            // btnConfirm
            // 
            this.btnConfirm.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnConfirm.Location = new System.Drawing.Point(446, 268);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(113, 35);
            this.btnConfirm.TabIndex = 4;
            this.btnConfirm.Text = "确认";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // lblTipMsg
            // 
            this.lblTipMsg.Font = new System.Drawing.Font("微软雅黑", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTipMsg.ForeColor = System.Drawing.Color.OrangeRed;
            this.lblTipMsg.Location = new System.Drawing.Point(99, 67);
            this.lblTipMsg.Name = "lblTipMsg";
            this.lblTipMsg.Size = new System.Drawing.Size(498, 180);
            this.lblTipMsg.TabIndex = 3;
            this.lblTipMsg.Text = "扫码枪扫码失败,请输入产品条码";
            // 
            // cbxUseUpstreamGlassID
            // 
            this.cbxUseUpstreamGlassID.AutoSize = true;
            this.cbxUseUpstreamGlassID.Location = new System.Drawing.Point(446, 309);
            this.cbxUseUpstreamGlassID.Name = "cbxUseUpstreamGlassID";
            this.cbxUseUpstreamGlassID.Size = new System.Drawing.Size(117, 21);
            this.cbxUseUpstreamGlassID.TabIndex = 6;
            this.cbxUseUpstreamGlassID.Text = "使用CimGlassID";
            this.cbxUseUpstreamGlassID.UseVisualStyleBackColor = true;
            this.cbxUseUpstreamGlassID.CheckedChanged += new System.EventHandler(this.cbxUseUpstreamGlassID_CheckedChanged);
            // 
            // FrmManualBarCode
            // 
            this.AcceptButton = this.btnConfirm;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(703, 346);
            this.Controls.Add(this.cbxUseUpstreamGlassID);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.lblTipMsg);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbxBarcode);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmManualBarCode";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "手动输入条码";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmManualBarCode_FormClosing);
            this.Load += new System.EventHandler(this.FrmManualBarCode_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox tbxBarcode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Label lblTipMsg;
        private System.Windows.Forms.CheckBox cbxUseUpstreamGlassID;
    }
}