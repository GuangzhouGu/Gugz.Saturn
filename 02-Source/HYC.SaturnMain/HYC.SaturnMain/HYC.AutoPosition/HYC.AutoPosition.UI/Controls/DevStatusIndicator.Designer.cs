namespace HYC.AutoPosition.UI.Controls
{
    partial class DevStatusIndicator
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tbpnlMain = new System.Windows.Forms.TableLayoutPanel();
            this.pnlSplit = new System.Windows.Forms.Panel();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.lblName = new System.Windows.Forms.Label();
            this.pnlStatus = new System.Windows.Forms.Panel();
            this.pnlSplitH = new System.Windows.Forms.Panel();
            this.tbpnlMain.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            this.pnlRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbpnlMain
            // 
            this.tbpnlMain.ColumnCount = 3;
            this.tbpnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbpnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 1F));
            this.tbpnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tbpnlMain.Controls.Add(this.pnlSplit, 1, 0);
            this.tbpnlMain.Controls.Add(this.pnlLeft, 0, 0);
            this.tbpnlMain.Controls.Add(this.pnlRight, 2, 0);
            this.tbpnlMain.Controls.Add(this.pnlSplitH, 0, 1);
            this.tbpnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbpnlMain.Location = new System.Drawing.Point(0, 0);
            this.tbpnlMain.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbpnlMain.Name = "tbpnlMain";
            this.tbpnlMain.RowCount = 2;
            this.tbpnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbpnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 1F));
            this.tbpnlMain.Size = new System.Drawing.Size(283, 28);
            this.tbpnlMain.TabIndex = 0;
            // 
            // pnlSplit
            // 
            this.pnlSplit.BackColor = System.Drawing.Color.Gray;
            this.pnlSplit.Location = new System.Drawing.Point(212, 0);
            this.pnlSplit.Margin = new System.Windows.Forms.Padding(0);
            this.pnlSplit.Name = "pnlSplit";
            this.pnlSplit.Size = new System.Drawing.Size(1, 27);
            this.pnlSplit.TabIndex = 0;
            // 
            // pnlLeft
            // 
            this.pnlLeft.Controls.Add(this.lblName);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlLeft.Margin = new System.Windows.Forms.Padding(0);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(212, 27);
            this.pnlLeft.TabIndex = 1;
            // 
            // pnlRight
            // 
            this.pnlRight.Controls.Add(this.pnlStatus);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Location = new System.Drawing.Point(213, 0);
            this.pnlRight.Margin = new System.Windows.Forms.Padding(0);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(70, 27);
            this.pnlRight.TabIndex = 2;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(4, 5);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(43, 17);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Name";
            // 
            // pnlStatus
            // 
            this.pnlStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlStatus.Location = new System.Drawing.Point(20, 6);
            this.pnlStatus.Name = "pnlStatus";
            this.pnlStatus.Size = new System.Drawing.Size(30, 15);
            this.pnlStatus.TabIndex = 0;
            // 
            // pnlSplitH
            // 
            this.pnlSplitH.BackColor = System.Drawing.Color.Gray;
            this.tbpnlMain.SetColumnSpan(this.pnlSplitH, 3);
            this.pnlSplitH.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSplitH.Location = new System.Drawing.Point(0, 27);
            this.pnlSplitH.Margin = new System.Windows.Forms.Padding(0);
            this.pnlSplitH.Name = "pnlSplitH";
            this.pnlSplitH.Size = new System.Drawing.Size(283, 1);
            this.pnlSplitH.TabIndex = 3;
            // 
            // DevStatusIndicator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbpnlMain);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "DevStatusIndicator";
            this.Size = new System.Drawing.Size(283, 28);
            this.tbpnlMain.ResumeLayout(false);
            this.pnlLeft.ResumeLayout(false);
            this.pnlLeft.PerformLayout();
            this.pnlRight.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tbpnlMain;
        private System.Windows.Forms.Panel pnlSplit;
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Panel pnlStatus;
        private System.Windows.Forms.Panel pnlSplitH;
    }
}
