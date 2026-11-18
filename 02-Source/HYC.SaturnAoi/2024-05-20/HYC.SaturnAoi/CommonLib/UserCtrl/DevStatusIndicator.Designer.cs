namespace CommonLib.UserCtrl
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
            this.lblSts = new System.Windows.Forms.Label();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.lblName = new System.Windows.Forms.Label();
            this.pnlSplitH = new System.Windows.Forms.Panel();
            this.tbpnlMain.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbpnlMain
            // 
            this.tbpnlMain.ColumnCount = 2;
            this.tbpnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 77.0318F));
            this.tbpnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.9682F));
            this.tbpnlMain.Controls.Add(this.lblSts, 1, 0);
            this.tbpnlMain.Controls.Add(this.pnlLeft, 0, 0);
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
            // lblSts
            // 
            this.lblSts.BackColor = System.Drawing.Color.LightGray;
            this.lblSts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSts.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSts.Location = new System.Drawing.Point(244, 6);
            this.lblSts.Margin = new System.Windows.Forms.Padding(27, 6, 27, 6);
            this.lblSts.Name = "lblSts";
            this.lblSts.Size = new System.Drawing.Size(12, 15);
            this.lblSts.TabIndex = 5;
            this.lblSts.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlLeft
            // 
            this.pnlLeft.Controls.Add(this.lblName);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlLeft.Margin = new System.Windows.Forms.Padding(0);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(217, 27);
            this.pnlLeft.TabIndex = 1;
            // 
            // lblName
            // 
            this.lblName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(244)))), ((int)(((byte)(245)))));
            this.lblName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblName.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblName.Location = new System.Drawing.Point(0, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(217, 27);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Name";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlSplitH
            // 
            this.pnlSplitH.BackColor = System.Drawing.Color.Gray;
            this.tbpnlMain.SetColumnSpan(this.pnlSplitH, 2);
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
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tbpnlMain;
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Panel pnlSplitH;
        private System.Windows.Forms.Label lblSts;
    }
}
