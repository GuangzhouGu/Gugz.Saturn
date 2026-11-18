
namespace HYC.SaturnMain.UserControls
{
    partial class PowerCtrl
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
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.cmxChannels = new System.Windows.Forms.ComboBox();
            this.btnClose = new HYC.WindowsControls.Buttons.HCButton2();
            this.cmxPower = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btnOpen = new HYC.WindowsControls.Buttons.HCButton2();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox4.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.tableLayoutPanel1);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(0, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(491, 486);
            this.groupBox4.TabIndex = 15;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "光源";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.Controls.Add(this.listBox2, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.cmxChannels, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnClose, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.cmxPower, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label10, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnOpen, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label8, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label9, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 22);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(485, 461);
            this.tableLayoutPanel1.TabIndex = 42;
            // 
            // listBox2
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.listBox2, 3);
            this.listBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox2.FormattingEnabled = true;
            this.listBox2.HorizontalScrollbar = true;
            this.listBox2.ItemHeight = 20;
            this.listBox2.Location = new System.Drawing.Point(3, 93);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(479, 365);
            this.listBox2.TabIndex = 38;
            // 
            // cmxChannels
            // 
            this.cmxChannels.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmxChannels.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmxChannels.FormattingEnabled = true;
            this.cmxChannels.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8"});
            this.cmxChannels.Location = new System.Drawing.Point(103, 34);
            this.cmxChannels.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmxChannels.Name = "cmxChannels";
            this.cmxChannels.Size = new System.Drawing.Size(279, 28);
            this.cmxChannels.TabIndex = 40;
            // 
            // btnClose
            // 
            this.btnClose.CustomerInformation = null;
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClose.ForeColor = System.Drawing.Color.Black;
            this.btnClose.Location = new System.Drawing.Point(388, 33);
            this.btnClose.Name = "btnClose";
            this.btnClose.OriginalColor = System.Drawing.Color.White;
            this.btnClose.Size = new System.Drawing.Size(94, 24);
            this.btnClose.TabIndex = 41;
            this.btnClose.Text = "关闭";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // cmxPower
            // 
            this.cmxPower.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmxPower.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmxPower.FormattingEnabled = true;
            this.cmxPower.Location = new System.Drawing.Point(103, 4);
            this.cmxPower.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmxPower.Name = "cmxPower";
            this.cmxPower.Size = new System.Drawing.Size(279, 28);
            this.cmxPower.TabIndex = 40;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(3, 63);
            this.label10.Margin = new System.Windows.Forms.Padding(3);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(94, 24);
            this.label10.TabIndex = 35;
            this.label10.Text = "接收消息";
            // 
            // btnOpen
            // 
            this.btnOpen.CustomerInformation = null;
            this.btnOpen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOpen.ForeColor = System.Drawing.Color.Black;
            this.btnOpen.Location = new System.Drawing.Point(388, 3);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.OriginalColor = System.Drawing.Color.White;
            this.btnOpen.Size = new System.Drawing.Size(94, 24);
            this.btnOpen.TabIndex = 41;
            this.btnOpen.Text = "打开";
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(3, 3);
            this.label8.Margin = new System.Windows.Forms.Padding(3);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(94, 24);
            this.label8.TabIndex = 36;
            this.label8.Text = "光源控制器";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(3, 33);
            this.label9.Margin = new System.Windows.Forms.Padding(3);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(94, 24);
            this.label9.TabIndex = 36;
            this.label9.Text = "通道";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PowerCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox4);
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "PowerCtrl";
            this.Size = new System.Drawing.Size(491, 486);
            this.Load += new System.EventHandler(this.PowerCtrl_Load);
            this.groupBox4.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox4;
        private WindowsControls.Buttons.HCButton2 btnClose;
        private WindowsControls.Buttons.HCButton2 btnOpen;
        private System.Windows.Forms.ComboBox cmxChannels;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmxPower;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
