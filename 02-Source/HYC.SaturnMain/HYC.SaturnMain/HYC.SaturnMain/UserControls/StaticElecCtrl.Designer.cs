
namespace HYC.SaturnMain.UserControls
{
    partial class StaticElecCtrl
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
            this.groupBox23 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label19 = new System.Windows.Forms.Label();
            this.lbxStaticElecMsg = new System.Windows.Forms.ListBox();
            this.btnStaticElecClearMsg = new HYC.WindowsControls.Buttons.HCButton2();
            this.cmxStaticElec = new System.Windows.Forms.ComboBox();
            this.btnStaticElecRead = new HYC.WindowsControls.Buttons.HCButton2();
            this.groupBox23.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox23
            // 
            this.groupBox23.Controls.Add(this.tableLayoutPanel1);
            this.groupBox23.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox23.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox23.Location = new System.Drawing.Point(0, 0);
            this.groupBox23.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox23.Name = "groupBox23";
            this.groupBox23.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox23.Size = new System.Drawing.Size(377, 369);
            this.groupBox23.TabIndex = 14;
            this.groupBox23.TabStop = false;
            this.groupBox23.Text = "StaticElec";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label19, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbxStaticElecMsg, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnStaticElecClearMsg, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.cmxStaticElec, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnStaticElecRead, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(4, 24);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(369, 340);
            this.tableLayoutPanel1.TabIndex = 42;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label19.ForeColor = System.Drawing.Color.Black;
            this.label19.Location = new System.Drawing.Point(3, 3);
            this.label19.Margin = new System.Windows.Forms.Padding(3);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(94, 24);
            this.label19.TabIndex = 36;
            this.label19.Text = "StaticElec";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbxStaticElecMsg
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.lbxStaticElecMsg, 3);
            this.lbxStaticElecMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbxStaticElecMsg.FormattingEnabled = true;
            this.lbxStaticElecMsg.HorizontalScrollbar = true;
            this.lbxStaticElecMsg.ItemHeight = 20;
            this.lbxStaticElecMsg.Location = new System.Drawing.Point(3, 63);
            this.lbxStaticElecMsg.Name = "lbxStaticElecMsg";
            this.lbxStaticElecMsg.Size = new System.Drawing.Size(363, 274);
            this.lbxStaticElecMsg.TabIndex = 38;
            // 
            // btnStaticElecClearMsg
            // 
            this.btnStaticElecClearMsg.CustomerInformation = null;
            this.btnStaticElecClearMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnStaticElecClearMsg.ForeColor = System.Drawing.Color.Black;
            this.btnStaticElecClearMsg.Location = new System.Drawing.Point(272, 33);
            this.btnStaticElecClearMsg.Name = "btnStaticElecClearMsg";
            this.btnStaticElecClearMsg.OriginalColor = System.Drawing.Color.White;
            this.btnStaticElecClearMsg.Size = new System.Drawing.Size(94, 24);
            this.btnStaticElecClearMsg.TabIndex = 41;
            this.btnStaticElecClearMsg.Text = "清除消息";
            this.btnStaticElecClearMsg.Click += new System.EventHandler(this.btnStaticElecClearMsg_Click);
            // 
            // cmxStaticElec
            // 
            this.cmxStaticElec.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmxStaticElec.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmxStaticElec.FormattingEnabled = true;
            this.cmxStaticElec.Location = new System.Drawing.Point(103, 3);
            this.cmxStaticElec.Name = "cmxStaticElec";
            this.cmxStaticElec.Size = new System.Drawing.Size(163, 28);
            this.cmxStaticElec.TabIndex = 40;
            // 
            // btnStaticElecRead
            // 
            this.btnStaticElecRead.CustomerInformation = null;
            this.btnStaticElecRead.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnStaticElecRead.ForeColor = System.Drawing.Color.Black;
            this.btnStaticElecRead.Location = new System.Drawing.Point(272, 3);
            this.btnStaticElecRead.Name = "btnStaticElecRead";
            this.btnStaticElecRead.OriginalColor = System.Drawing.Color.White;
            this.btnStaticElecRead.Size = new System.Drawing.Size(94, 24);
            this.btnStaticElecRead.TabIndex = 41;
            this.btnStaticElecRead.Text = "读取";
            this.btnStaticElecRead.Click += new System.EventHandler(this.btnStaticElecRead_Click);
            // 
            // StaticElecCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox23);
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "StaticElecCtrl";
            this.Size = new System.Drawing.Size(377, 369);
            this.Load += new System.EventHandler(this.StaticElecCtrl_Load);
            this.groupBox23.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox23;
        private WindowsControls.Buttons.HCButton2 btnStaticElecClearMsg;
        private WindowsControls.Buttons.HCButton2 btnStaticElecRead;
        private System.Windows.Forms.ComboBox cmxStaticElec;
        private System.Windows.Forms.ListBox lbxStaticElecMsg;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
