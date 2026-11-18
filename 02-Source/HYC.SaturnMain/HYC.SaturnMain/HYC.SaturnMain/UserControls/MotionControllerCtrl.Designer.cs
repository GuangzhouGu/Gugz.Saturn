
namespace HYC.SaturnMain.UserControls
{
    partial class MotionControllerCtrl
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.btnRunBuffer = new HYC.WindowsControls.Buttons.HCButton2();
            this.label6 = new System.Windows.Forms.Label();
            this.tbxBufferID = new System.Windows.Forms.TextBox();
            this.hcButton21 = new HYC.WindowsControls.Buttons.HCButton2();
            this.btnReset = new HYC.WindowsControls.Buttons.HCButton2();
            this.btnDisableGantry = new HYC.WindowsControls.Buttons.HCButton2();
            this.btnConnect = new HYC.WindowsControls.Buttons.HCButton2();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tableLayoutPanel1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(411, 387);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "MotionController";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnRunBuffer, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.tbxBufferID, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.hcButton21, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.btnReset, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnDisableGantry, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnConnect, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.listBox1, 0, 5);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 22);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(405, 362);
            this.tableLayoutPanel1.TabIndex = 47;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 30);
            this.label5.TabIndex = 45;
            this.label5.Text = "Buffer";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnRunBuffer
            // 
            this.btnRunBuffer.CustomerInformation = null;
            this.btnRunBuffer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRunBuffer.ForeColor = System.Drawing.Color.Black;
            this.btnRunBuffer.Location = new System.Drawing.Point(308, 123);
            this.btnRunBuffer.Name = "btnRunBuffer";
            this.btnRunBuffer.OriginalColor = System.Drawing.Color.White;
            this.btnRunBuffer.Size = new System.Drawing.Size(94, 24);
            this.btnRunBuffer.TabIndex = 42;
            this.btnRunBuffer.Text = "执行Buffer";
            this.btnRunBuffer.Click += new System.EventHandler(this.btnRunBuffer_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(3, 120);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(94, 30);
            this.label6.TabIndex = 35;
            this.label6.Text = "接收消息";
            // 
            // tbxBufferID
            // 
            this.tbxBufferID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbxBufferID.Location = new System.Drawing.Point(103, 3);
            this.tbxBufferID.Name = "tbxBufferID";
            this.tbxBufferID.Size = new System.Drawing.Size(199, 26);
            this.tbxBufferID.TabIndex = 46;
            // 
            // hcButton21
            // 
            this.hcButton21.CustomerInformation = null;
            this.hcButton21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hcButton21.ForeColor = System.Drawing.Color.Black;
            this.hcButton21.Location = new System.Drawing.Point(308, 93);
            this.hcButton21.Name = "hcButton21";
            this.hcButton21.OriginalColor = System.Drawing.Color.White;
            this.hcButton21.Size = new System.Drawing.Size(94, 24);
            this.hcButton21.TabIndex = 41;
            this.hcButton21.Text = "清除消息";
            // 
            // btnReset
            // 
            this.btnReset.CustomerInformation = null;
            this.btnReset.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnReset.ForeColor = System.Drawing.Color.Black;
            this.btnReset.Location = new System.Drawing.Point(308, 33);
            this.btnReset.Name = "btnReset";
            this.btnReset.OriginalColor = System.Drawing.Color.White;
            this.btnReset.Size = new System.Drawing.Size(94, 24);
            this.btnReset.TabIndex = 44;
            this.btnReset.Text = "重启";
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnDisableGantry
            // 
            this.btnDisableGantry.CustomerInformation = null;
            this.btnDisableGantry.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDisableGantry.ForeColor = System.Drawing.Color.Black;
            this.btnDisableGantry.Location = new System.Drawing.Point(308, 63);
            this.btnDisableGantry.Name = "btnDisableGantry";
            this.btnDisableGantry.OriginalColor = System.Drawing.Color.White;
            this.btnDisableGantry.Size = new System.Drawing.Size(94, 24);
            this.btnDisableGantry.TabIndex = 41;
            this.btnDisableGantry.Text = "解除料仓龙门";
            this.btnDisableGantry.Click += new System.EventHandler(this.btnReadLog_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.CustomerInformation = null;
            this.btnConnect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnConnect.ForeColor = System.Drawing.Color.Black;
            this.btnConnect.Location = new System.Drawing.Point(308, 3);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.OriginalColor = System.Drawing.Color.White;
            this.btnConnect.Size = new System.Drawing.Size(94, 24);
            this.btnConnect.TabIndex = 42;
            this.btnConnect.Text = "连接";
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // listBox1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.listBox1, 3);
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.HorizontalScrollbar = true;
            this.listBox1.ItemHeight = 20;
            this.listBox1.Location = new System.Drawing.Point(3, 154);
            this.listBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(399, 204);
            this.listBox1.TabIndex = 38;
            // 
            // MotionControllerCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MotionControllerCtrl";
            this.Size = new System.Drawing.Size(411, 387);
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label5;
        private WindowsControls.Buttons.HCButton2 btnRunBuffer;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbxBufferID;
        private WindowsControls.Buttons.HCButton2 hcButton21;
        private WindowsControls.Buttons.HCButton2 btnReset;
        private WindowsControls.Buttons.HCButton2 btnDisableGantry;
        private WindowsControls.Buttons.HCButton2 btnConnect;
        private System.Windows.Forms.ListBox listBox1;
    }
}
