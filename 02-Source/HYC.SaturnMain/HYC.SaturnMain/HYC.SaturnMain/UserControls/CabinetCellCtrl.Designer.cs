
namespace HYC.SaturnMain.UserControls
{
    partial class CabinetCellCtrl
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
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblRowNum = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblProbeName = new System.Windows.Forms.Label();
            this.lblRfid = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.pbxPrb = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblContactCount = new System.Windows.Forms.Label();
            this.pbxIsUsed = new System.Windows.Forms.PictureBox();
            this.lblPrbSts = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.更新Probbe状态ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.插入Probe状态ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除Probe状态ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblRecipeName = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxPrb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxIsUsed)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.lblRowNum, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label4, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblProbeName, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblRfid, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.label6, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblRecipeName, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.pbxPrb, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label5, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblContactCount, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.pbxIsUsed, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblPrbSts, 4, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 31.78295F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35.65892F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1124, 129);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lblRowNum
            // 
            this.lblRowNum.AutoSize = true;
            this.lblRowNum.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblRowNum.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRowNum.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblRowNum.Location = new System.Drawing.Point(1, 1);
            this.lblRowNum.Margin = new System.Windows.Forms.Padding(1);
            this.lblRowNum.Name = "lblRowNum";
            this.lblRowNum.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.tableLayoutPanel1.SetRowSpan(this.lblRowNum, 3);
            this.lblRowNum.Size = new System.Drawing.Size(98, 127);
            this.lblRowNum.TabIndex = 0;
            this.lblRowNum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.Control;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(101, 1);
            this.label2.Margin = new System.Windows.Forms.Padding(1);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.label2.Size = new System.Drawing.Size(108, 40);
            this.label2.TabIndex = 0;
            this.label2.Text = "ProbeName";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.Control;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(101, 83);
            this.label4.Margin = new System.Windows.Forms.Padding(1);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.label4.Size = new System.Drawing.Size(108, 45);
            this.label4.TabIndex = 0;
            this.label4.Text = "RFID";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblProbeName
            // 
            this.lblProbeName.BackColor = System.Drawing.SystemColors.Control;
            this.lblProbeName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblProbeName.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblProbeName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.lblProbeName.Location = new System.Drawing.Point(211, 1);
            this.lblProbeName.Margin = new System.Windows.Forms.Padding(1);
            this.lblProbeName.Name = "lblProbeName";
            this.lblProbeName.Size = new System.Drawing.Size(148, 40);
            this.lblProbeName.TabIndex = 0;
            this.lblProbeName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRfid
            // 
            this.lblRfid.BackColor = System.Drawing.SystemColors.Control;
            this.lblRfid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRfid.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblRfid.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.lblRfid.Location = new System.Drawing.Point(211, 83);
            this.lblRfid.Margin = new System.Windows.Forms.Padding(1);
            this.lblRfid.Name = "lblRfid";
            this.lblRfid.Size = new System.Drawing.Size(148, 45);
            this.lblRfid.TabIndex = 0;
            this.lblRfid.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.SystemColors.Control;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(101, 43);
            this.label6.Margin = new System.Windows.Forms.Padding(1);
            this.label6.Name = "label6";
            this.label6.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.label6.Size = new System.Drawing.Size(108, 38);
            this.label6.TabIndex = 0;
            this.label6.Text = "RecipeName";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pbxPrb
            // 
            this.pbxPrb.BackColor = System.Drawing.Color.White;
            this.pbxPrb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbxPrb.Location = new System.Drawing.Point(621, 1);
            this.pbxPrb.Margin = new System.Windows.Forms.Padding(1);
            this.pbxPrb.Name = "pbxPrb";
            this.tableLayoutPanel1.SetRowSpan(this.pbxPrb, 3);
            this.pbxPrb.Size = new System.Drawing.Size(502, 127);
            this.pbxPrb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbxPrb.TabIndex = 2;
            this.pbxPrb.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.Control;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(361, 1);
            this.label3.Margin = new System.Windows.Forms.Padding(1);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.label3.Size = new System.Drawing.Size(108, 40);
            this.label3.TabIndex = 0;
            this.label3.Text = "已压接次数";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.SystemColors.Control;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(361, 43);
            this.label5.Margin = new System.Windows.Forms.Padding(1);
            this.label5.Name = "label5";
            this.label5.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.label5.Size = new System.Drawing.Size(108, 38);
            this.label5.TabIndex = 0;
            this.label5.Text = "是否被使用";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(361, 83);
            this.label1.Margin = new System.Windows.Forms.Padding(1);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.label1.Size = new System.Drawing.Size(108, 45);
            this.label1.TabIndex = 0;
            this.label1.Text = "Probe状态";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblContactCount
            // 
            this.lblContactCount.BackColor = System.Drawing.SystemColors.Control;
            this.lblContactCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblContactCount.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblContactCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.lblContactCount.Location = new System.Drawing.Point(471, 1);
            this.lblContactCount.Margin = new System.Windows.Forms.Padding(1);
            this.lblContactCount.Name = "lblContactCount";
            this.lblContactCount.Size = new System.Drawing.Size(148, 40);
            this.lblContactCount.TabIndex = 0;
            this.lblContactCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pbxIsUsed
            // 
            this.pbxIsUsed.BackColor = System.Drawing.SystemColors.Control;
            this.pbxIsUsed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbxIsUsed.Image = global::HYC.SaturnMain.Properties.Resources.Gray2;
            this.pbxIsUsed.Location = new System.Drawing.Point(470, 42);
            this.pbxIsUsed.Margin = new System.Windows.Forms.Padding(0);
            this.pbxIsUsed.Name = "pbxIsUsed";
            this.pbxIsUsed.Size = new System.Drawing.Size(150, 40);
            this.pbxIsUsed.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbxIsUsed.TabIndex = 3;
            this.pbxIsUsed.TabStop = false;
            // 
            // lblPrbSts
            // 
            this.lblPrbSts.BackColor = System.Drawing.SystemColors.Control;
            this.lblPrbSts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPrbSts.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPrbSts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.lblPrbSts.Location = new System.Drawing.Point(471, 83);
            this.lblPrbSts.Margin = new System.Windows.Forms.Padding(1);
            this.lblPrbSts.Name = "lblPrbSts";
            this.lblPrbSts.Size = new System.Drawing.Size(148, 45);
            this.lblPrbSts.TabIndex = 0;
            this.lblPrbSts.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.更新Probbe状态ToolStripMenuItem,
            this.插入Probe状态ToolStripMenuItem,
            this.删除Probe状态ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(168, 70);
            // 
            // 更新Probbe状态ToolStripMenuItem
            // 
            this.更新Probbe状态ToolStripMenuItem.Name = "更新Probbe状态ToolStripMenuItem";
            this.更新Probbe状态ToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.更新Probbe状态ToolStripMenuItem.Text = "更新Probbe状态";
            this.更新Probbe状态ToolStripMenuItem.Click += new System.EventHandler(this.更新Probbe状态ToolStripMenuItem_Click);
            // 
            // 插入Probe状态ToolStripMenuItem
            // 
            this.插入Probe状态ToolStripMenuItem.Name = "插入Probe状态ToolStripMenuItem";
            this.插入Probe状态ToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.插入Probe状态ToolStripMenuItem.Text = "插入Probe状态";
            this.插入Probe状态ToolStripMenuItem.Click += new System.EventHandler(this.插入Probe状态ToolStripMenuItem_Click);
            // 
            // 删除Probe状态ToolStripMenuItem
            // 
            this.删除Probe状态ToolStripMenuItem.Name = "删除Probe状态ToolStripMenuItem";
            this.删除Probe状态ToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.删除Probe状态ToolStripMenuItem.Text = "删除Probe状态";
            this.删除Probe状态ToolStripMenuItem.Click += new System.EventHandler(this.删除Probe状态ToolStripMenuItem_Click);
            // 
            // lblRecipeName
            // 
            this.lblRecipeName.BackColor = System.Drawing.SystemColors.Control;
            this.lblRecipeName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRecipeName.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblRecipeName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.lblRecipeName.Location = new System.Drawing.Point(211, 43);
            this.lblRecipeName.Margin = new System.Windows.Forms.Padding(1);
            this.lblRecipeName.Name = "lblRecipeName";
            this.lblRecipeName.Size = new System.Drawing.Size(148, 38);
            this.lblRecipeName.TabIndex = 0;
            this.lblRecipeName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CabinetCellCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "CabinetCellCtrl";
            this.Size = new System.Drawing.Size(1124, 129);
            this.Load += new System.EventHandler(this.CabinetCellCtrl_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxPrb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxIsUsed)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblProbeName;
        private System.Windows.Forms.Label lblRfid;
        private System.Windows.Forms.Label lblRowNum;
        private System.Windows.Forms.PictureBox pbxPrb;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblContactCount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblPrbSts;
        private System.Windows.Forms.PictureBox pbxIsUsed;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 更新Probbe状态ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 插入Probe状态ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除Probe状态ToolStripMenuItem;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblRecipeName;
    }
}
