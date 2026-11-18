
namespace HYC.SaturnMain.UserControls
{
    partial class ScanerCtrl
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
            this.groupBox22 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label15 = new System.Windows.Forms.Label();
            this.lbxScanerMsg = new System.Windows.Forms.ListBox();
            this.btnClearScanerMsg = new HYC.WindowsControls.Buttons.HCButton2();
            this.cbxScanerList = new System.Windows.Forms.ComboBox();
            this.btnScan = new HYC.WindowsControls.Buttons.HCButton2();
            this.groupBox22.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox22
            // 
            this.groupBox22.Controls.Add(this.tableLayoutPanel1);
            this.groupBox22.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox22.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox22.Location = new System.Drawing.Point(0, 0);
            this.groupBox22.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox22.Name = "groupBox22";
            this.groupBox22.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox22.Size = new System.Drawing.Size(394, 372);
            this.groupBox22.TabIndex = 14;
            this.groupBox22.TabStop = false;
            this.groupBox22.Text = "扫码枪";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label15, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbxScanerMsg, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnClearScanerMsg, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.cbxScanerList, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnScan, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(4, 24);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(386, 343);
            this.tableLayoutPanel1.TabIndex = 42;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label15.ForeColor = System.Drawing.Color.Black;
            this.label15.Location = new System.Drawing.Point(3, 3);
            this.label15.Margin = new System.Windows.Forms.Padding(3);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(94, 24);
            this.label15.TabIndex = 36;
            this.label15.Text = "Scaner";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbxScanerMsg
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.lbxScanerMsg, 3);
            this.lbxScanerMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbxScanerMsg.FormattingEnabled = true;
            this.lbxScanerMsg.HorizontalScrollbar = true;
            this.lbxScanerMsg.ItemHeight = 20;
            this.lbxScanerMsg.Location = new System.Drawing.Point(3, 63);
            this.lbxScanerMsg.Name = "lbxScanerMsg";
            this.lbxScanerMsg.Size = new System.Drawing.Size(380, 277);
            this.lbxScanerMsg.TabIndex = 38;
            // 
            // btnClearScanerMsg
            // 
            this.btnClearScanerMsg.CustomerInformation = null;
            this.btnClearScanerMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClearScanerMsg.ForeColor = System.Drawing.Color.Black;
            this.btnClearScanerMsg.Location = new System.Drawing.Point(289, 33);
            this.btnClearScanerMsg.Name = "btnClearScanerMsg";
            this.btnClearScanerMsg.OriginalColor = System.Drawing.Color.White;
            this.btnClearScanerMsg.Size = new System.Drawing.Size(94, 24);
            this.btnClearScanerMsg.TabIndex = 41;
            this.btnClearScanerMsg.Text = "清除消息";
            this.btnClearScanerMsg.Click += new System.EventHandler(this.btnClearScanerMsg_Click);
            // 
            // cbxScanerList
            // 
            this.cbxScanerList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbxScanerList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxScanerList.FormattingEnabled = true;
            this.cbxScanerList.Location = new System.Drawing.Point(103, 3);
            this.cbxScanerList.Name = "cbxScanerList";
            this.cbxScanerList.Size = new System.Drawing.Size(180, 28);
            this.cbxScanerList.TabIndex = 40;
            // 
            // btnScan
            // 
            this.btnScan.CustomerInformation = null;
            this.btnScan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnScan.ForeColor = System.Drawing.Color.Black;
            this.btnScan.Location = new System.Drawing.Point(289, 3);
            this.btnScan.Name = "btnScan";
            this.btnScan.OriginalColor = System.Drawing.Color.White;
            this.btnScan.Size = new System.Drawing.Size(94, 24);
            this.btnScan.TabIndex = 41;
            this.btnScan.Text = "扫码";
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // ScanerCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox22);
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ScanerCtrl";
            this.Size = new System.Drawing.Size(394, 372);
            this.Load += new System.EventHandler(this.ScanerCtrl_Load);
            this.groupBox22.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox22;
        private WindowsControls.Buttons.HCButton2 btnClearScanerMsg;
        private WindowsControls.Buttons.HCButton2 btnScan;
        private System.Windows.Forms.ComboBox cbxScanerList;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ListBox lbxScanerMsg;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
