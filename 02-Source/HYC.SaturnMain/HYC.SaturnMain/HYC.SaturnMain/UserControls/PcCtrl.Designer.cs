
namespace HYC.SaturnMain.UserControls
{
    partial class PcCtrl
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
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.btnClearClientMsg = new HYC.WindowsControls.Buttons.HCButton2();
            this.label3 = new System.Windows.Forms.Label();
            this.cmx_clientList = new System.Windows.Forms.ComboBox();
            this.cmx_comList = new System.Windows.Forms.ComboBox();
            this.label37 = new System.Windows.Forms.Label();
            this.btn_send = new HYC.WindowsControls.Buttons.HCButton2();
            this.label4 = new System.Windows.Forms.Label();
            this.tbx_cmd = new System.Windows.Forms.TextBox();
            this.lbxClientMsg = new System.Windows.Forms.ListBox();
            this.groupBox8.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.tableLayoutPanel1);
            this.groupBox8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox8.Location = new System.Drawing.Point(0, 0);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(475, 509);
            this.groupBox8.TabIndex = 13;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "PC";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnClearClientMsg, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.cmx_clientList, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmx_comList, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label37, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.btn_send, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tbx_cmd, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.lbxClientMsg, 0, 5);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 22);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(469, 484);
            this.tableLayoutPanel1.TabIndex = 42;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(3, 3);
            this.label2.Margin = new System.Windows.Forms.Padding(3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 24);
            this.label2.TabIndex = 6;
            this.label2.Text = "从机";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnClearClientMsg
            // 
            this.btnClearClientMsg.CustomerInformation = null;
            this.btnClearClientMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClearClientMsg.ForeColor = System.Drawing.Color.Black;
            this.btnClearClientMsg.Location = new System.Drawing.Point(372, 63);
            this.btnClearClientMsg.Name = "btnClearClientMsg";
            this.btnClearClientMsg.OriginalColor = System.Drawing.Color.White;
            this.btnClearClientMsg.Size = new System.Drawing.Size(94, 24);
            this.btnClearClientMsg.TabIndex = 41;
            this.btnClearClientMsg.Text = "清除消息";
            this.btnClearClientMsg.Click += new System.EventHandler(this.btnClearClientMsg_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(3, 33);
            this.label3.Margin = new System.Windows.Forms.Padding(3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 24);
            this.label3.TabIndex = 6;
            this.label3.Text = "指令";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmx_clientList
            // 
            this.cmx_clientList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmx_clientList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmx_clientList.FormattingEnabled = true;
            this.cmx_clientList.Items.AddRange(new object[] {
            "AoiPC1",
            "AoiPC2",
            "TesterPC",
            "DFS"});
            this.cmx_clientList.Location = new System.Drawing.Point(103, 3);
            this.cmx_clientList.Name = "cmx_clientList";
            this.cmx_clientList.Size = new System.Drawing.Size(263, 28);
            this.cmx_clientList.TabIndex = 10;
            this.cmx_clientList.SelectedIndexChanged += new System.EventHandler(this.cmx_clientList_SelectedIndexChanged);
            // 
            // cmx_comList
            // 
            this.cmx_comList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmx_comList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmx_comList.FormattingEnabled = true;
            this.cmx_comList.Location = new System.Drawing.Point(103, 33);
            this.cmx_comList.Name = "cmx_comList";
            this.cmx_comList.Size = new System.Drawing.Size(263, 28);
            this.cmx_comList.TabIndex = 10;
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label37.ForeColor = System.Drawing.Color.Black;
            this.label37.Location = new System.Drawing.Point(3, 275);
            this.label37.Margin = new System.Windows.Forms.Padding(3);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(94, 24);
            this.label37.TabIndex = 6;
            this.label37.Text = "接收消息";
            // 
            // btn_send
            // 
            this.btn_send.CustomerInformation = null;
            this.btn_send.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_send.ForeColor = System.Drawing.Color.Black;
            this.btn_send.Location = new System.Drawing.Point(372, 3);
            this.btn_send.Name = "btn_send";
            this.btn_send.OriginalColor = System.Drawing.Color.White;
            this.btn_send.Size = new System.Drawing.Size(94, 24);
            this.btn_send.TabIndex = 11;
            this.btn_send.Text = "发送";
            this.btn_send.Click += new System.EventHandler(this.btn_send_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(3, 63);
            this.label4.Margin = new System.Windows.Forms.Padding(3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 24);
            this.label4.TabIndex = 6;
            this.label4.Text = "发送消息";
            // 
            // tbx_cmd
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.tbx_cmd, 3);
            this.tbx_cmd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbx_cmd.Location = new System.Drawing.Point(3, 93);
            this.tbx_cmd.Multiline = true;
            this.tbx_cmd.Name = "tbx_cmd";
            this.tbx_cmd.Size = new System.Drawing.Size(463, 176);
            this.tbx_cmd.TabIndex = 7;
            // 
            // lbxClientMsg
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.lbxClientMsg, 3);
            this.lbxClientMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbxClientMsg.FormattingEnabled = true;
            this.lbxClientMsg.HorizontalScrollbar = true;
            this.lbxClientMsg.ItemHeight = 20;
            this.lbxClientMsg.Location = new System.Drawing.Point(3, 305);
            this.lbxClientMsg.Name = "lbxClientMsg";
            this.lbxClientMsg.Size = new System.Drawing.Size(463, 176);
            this.lbxClientMsg.TabIndex = 9;
            // 
            // PcCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox8);
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "PcCtrl";
            this.Size = new System.Drawing.Size(475, 509);
            this.Load += new System.EventHandler(this.PcCtrl_Load);
            this.groupBox8.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox8;
        private WindowsControls.Buttons.HCButton2 btnClearClientMsg;
        private System.Windows.Forms.ComboBox cmx_clientList;
        private System.Windows.Forms.TextBox tbx_cmd;
        private System.Windows.Forms.ComboBox cmx_comList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lbxClientMsg;
        private WindowsControls.Buttons.HCButton2 btn_send;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
