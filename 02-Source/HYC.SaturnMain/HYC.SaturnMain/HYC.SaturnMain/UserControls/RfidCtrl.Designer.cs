
namespace HYC.SaturnMain.UserControls
{
    partial class RfidCtrl
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
            this.groupBox18 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label45 = new System.Windows.Forms.Label();
            this.lbxRfidMsg = new System.Windows.Forms.ListBox();
            this.label42 = new System.Windows.Forms.Label();
            this.btnWrite = new HYC.WindowsControls.Buttons.HCButton2();
            this.tbxWriteValue = new System.Windows.Forms.TextBox();
            this.btnRead = new HYC.WindowsControls.Buttons.HCButton2();
            this.cmxRfid = new System.Windows.Forms.ComboBox();
            this.btnClearRfidMsg = new HYC.WindowsControls.Buttons.HCButton2();
            this.label44 = new System.Windows.Forms.Label();
            this.groupBox18.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox18
            // 
            this.groupBox18.Controls.Add(this.tableLayoutPanel1);
            this.groupBox18.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox18.Location = new System.Drawing.Point(0, 0);
            this.groupBox18.Name = "groupBox18";
            this.groupBox18.Size = new System.Drawing.Size(380, 365);
            this.groupBox18.TabIndex = 14;
            this.groupBox18.TabStop = false;
            this.groupBox18.Text = "Rfid";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label45, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbxRfidMsg, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label42, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnWrite, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbxWriteValue, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnRead, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmxRfid, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnClearRfidMsg, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.label44, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 22);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(374, 340);
            this.tableLayoutPanel1.TabIndex = 44;
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label45.ForeColor = System.Drawing.Color.Black;
            this.label45.Location = new System.Drawing.Point(3, 3);
            this.label45.Margin = new System.Windows.Forms.Padding(3);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(94, 24);
            this.label45.TabIndex = 36;
            this.label45.Text = "Rfid";
            this.label45.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbxRfidMsg
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.lbxRfidMsg, 3);
            this.lbxRfidMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbxRfidMsg.FormattingEnabled = true;
            this.lbxRfidMsg.HorizontalScrollbar = true;
            this.lbxRfidMsg.ItemHeight = 20;
            this.lbxRfidMsg.Location = new System.Drawing.Point(3, 93);
            this.lbxRfidMsg.Name = "lbxRfidMsg";
            this.lbxRfidMsg.Size = new System.Drawing.Size(368, 244);
            this.lbxRfidMsg.TabIndex = 38;
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label42.ForeColor = System.Drawing.Color.Black;
            this.label42.Location = new System.Drawing.Point(3, 33);
            this.label42.Margin = new System.Windows.Forms.Padding(3);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(94, 24);
            this.label42.TabIndex = 33;
            this.label42.Text = "写入值";
            this.label42.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnWrite
            // 
            this.btnWrite.CustomerInformation = null;
            this.btnWrite.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnWrite.ForeColor = System.Drawing.Color.Black;
            this.btnWrite.Location = new System.Drawing.Point(277, 33);
            this.btnWrite.Name = "btnWrite";
            this.btnWrite.OriginalColor = System.Drawing.Color.White;
            this.btnWrite.Size = new System.Drawing.Size(94, 24);
            this.btnWrite.TabIndex = 41;
            this.btnWrite.Text = "写入";
            this.btnWrite.Click += new System.EventHandler(this.btnWrite_Click);
            // 
            // tbxWriteValue
            // 
            this.tbxWriteValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbxWriteValue.Location = new System.Drawing.Point(103, 33);
            this.tbxWriteValue.Name = "tbxWriteValue";
            this.tbxWriteValue.Size = new System.Drawing.Size(168, 26);
            this.tbxWriteValue.TabIndex = 43;
            this.tbxWriteValue.Text = "hyc";
            // 
            // btnRead
            // 
            this.btnRead.CustomerInformation = null;
            this.btnRead.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRead.ForeColor = System.Drawing.Color.Black;
            this.btnRead.Location = new System.Drawing.Point(277, 3);
            this.btnRead.Name = "btnRead";
            this.btnRead.OriginalColor = System.Drawing.Color.White;
            this.btnRead.Size = new System.Drawing.Size(94, 24);
            this.btnRead.TabIndex = 41;
            this.btnRead.Text = "读取";
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // cmxRfid
            // 
            this.cmxRfid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmxRfid.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmxRfid.FormattingEnabled = true;
            this.cmxRfid.Location = new System.Drawing.Point(103, 4);
            this.cmxRfid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmxRfid.Name = "cmxRfid";
            this.cmxRfid.Size = new System.Drawing.Size(168, 28);
            this.cmxRfid.TabIndex = 40;
            // 
            // btnClearRfidMsg
            // 
            this.btnClearRfidMsg.CustomerInformation = null;
            this.btnClearRfidMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClearRfidMsg.ForeColor = System.Drawing.Color.Black;
            this.btnClearRfidMsg.Location = new System.Drawing.Point(277, 63);
            this.btnClearRfidMsg.Name = "btnClearRfidMsg";
            this.btnClearRfidMsg.OriginalColor = System.Drawing.Color.White;
            this.btnClearRfidMsg.Size = new System.Drawing.Size(94, 24);
            this.btnClearRfidMsg.TabIndex = 41;
            this.btnClearRfidMsg.Text = "清除消息";
            this.btnClearRfidMsg.Click += new System.EventHandler(this.btnClearRfidMsg_Click);
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label44.ForeColor = System.Drawing.Color.Black;
            this.label44.Location = new System.Drawing.Point(3, 63);
            this.label44.Margin = new System.Windows.Forms.Padding(3);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(94, 24);
            this.label44.TabIndex = 35;
            this.label44.Text = "接收消息";
            // 
            // RfidCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox18);
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "RfidCtrl";
            this.Size = new System.Drawing.Size(380, 365);
            this.Load += new System.EventHandler(this.RfidCtrl_Load);
            this.groupBox18.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox18;
        private WindowsControls.Buttons.HCButton2 btnClearRfidMsg;
        private System.Windows.Forms.TextBox tbxWriteValue;
        private WindowsControls.Buttons.HCButton2 btnWrite;
        private WindowsControls.Buttons.HCButton2 btnRead;
        private System.Windows.Forms.ComboBox cmxRfid;
        private System.Windows.Forms.ListBox lbxRfidMsg;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
