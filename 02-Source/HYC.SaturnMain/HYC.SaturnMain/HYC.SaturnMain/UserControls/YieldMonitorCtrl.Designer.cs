
namespace HYC.SaturnMain.UserControls
{
    partial class YieldMonitorCtrl
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblOkCount = new System.Windows.Forms.Label();
            this.lblNGCount = new System.Windows.Forms.Label();
            this.lblTotalCount = new System.Windows.Forms.Label();
            this.lblOkRate = new System.Windows.Forms.Label();
            this.dtpTime = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.panelEx1 = new HYC.HTPCBase.Extension.UserCtrl.PanelEx();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panelEx2 = new HYC.HTPCBase.Extension.UserCtrl.PanelEx();
            this.tableLayoutPanel1.SuspendLayout();
            this.panelEx1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panelEx2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label4, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label5, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblOkCount, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblNGCount, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblTotalCount, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblOkRate, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.dtpTime, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnClear, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.dtpDate, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(2, 2);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.00001F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(318, 144);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(215)))), ((int)(((byte)(247)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(1, 1);
            this.label2.Margin = new System.Windows.Forms.Padding(1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 45);
            this.label2.TabIndex = 1;
            this.label2.Text = "OK";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(215)))), ((int)(((byte)(247)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(80, 1);
            this.label3.Margin = new System.Windows.Forms.Padding(1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 45);
            this.label3.TabIndex = 1;
            this.label3.Text = "NG";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(215)))), ((int)(((byte)(247)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(159, 1);
            this.label4.Margin = new System.Windows.Forms.Padding(1);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 45);
            this.label4.TabIndex = 1;
            this.label4.Text = "总计";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(215)))), ((int)(((byte)(247)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(238, 1);
            this.label5.Margin = new System.Windows.Forms.Padding(1);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 45);
            this.label5.TabIndex = 1;
            this.label5.Text = "良率";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblOkCount
            // 
            this.lblOkCount.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblOkCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblOkCount.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblOkCount.ForeColor = System.Drawing.Color.Green;
            this.lblOkCount.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblOkCount.Location = new System.Drawing.Point(1, 48);
            this.lblOkCount.Margin = new System.Windows.Forms.Padding(1);
            this.lblOkCount.Name = "lblOkCount";
            this.lblOkCount.Size = new System.Drawing.Size(77, 45);
            this.lblOkCount.TabIndex = 1;
            this.lblOkCount.Text = "0";
            this.lblOkCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNGCount
            // 
            this.lblNGCount.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblNGCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNGCount.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblNGCount.ForeColor = System.Drawing.Color.OrangeRed;
            this.lblNGCount.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblNGCount.Location = new System.Drawing.Point(80, 48);
            this.lblNGCount.Margin = new System.Windows.Forms.Padding(1);
            this.lblNGCount.Name = "lblNGCount";
            this.lblNGCount.Size = new System.Drawing.Size(77, 45);
            this.lblNGCount.TabIndex = 1;
            this.lblNGCount.Text = "0";
            this.lblNGCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTotalCount
            // 
            this.lblTotalCount.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblTotalCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalCount.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTotalCount.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblTotalCount.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblTotalCount.Location = new System.Drawing.Point(159, 48);
            this.lblTotalCount.Margin = new System.Windows.Forms.Padding(1);
            this.lblTotalCount.Name = "lblTotalCount";
            this.lblTotalCount.Size = new System.Drawing.Size(77, 45);
            this.lblTotalCount.TabIndex = 1;
            this.lblTotalCount.Text = "0";
            this.lblTotalCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblOkRate
            // 
            this.lblOkRate.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblOkRate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblOkRate.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblOkRate.ForeColor = System.Drawing.Color.LimeGreen;
            this.lblOkRate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblOkRate.Location = new System.Drawing.Point(238, 48);
            this.lblOkRate.Margin = new System.Windows.Forms.Padding(1);
            this.lblOkRate.Name = "lblOkRate";
            this.lblOkRate.Size = new System.Drawing.Size(79, 45);
            this.lblOkRate.TabIndex = 1;
            this.lblOkRate.Text = "0";
            this.lblOkRate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtpTime
            // 
            this.dtpTime.Checked = false;
            this.tableLayoutPanel1.SetColumnSpan(this.dtpTime, 2);
            this.dtpTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpTime.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.dtpTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpTime.Location = new System.Drawing.Point(80, 119);
            this.dtpTime.Margin = new System.Windows.Forms.Padding(1);
            this.dtpTime.Name = "dtpTime";
            this.dtpTime.ShowCheckBox = true;
            this.dtpTime.ShowUpDown = true;
            this.dtpTime.Size = new System.Drawing.Size(156, 23);
            this.dtpTime.TabIndex = 2;
            this.dtpTime.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(215)))), ((int)(((byte)(247)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label6.Location = new System.Drawing.Point(1, 95);
            this.label6.Margin = new System.Windows.Forms.Padding(1);
            this.label6.Name = "label6";
            this.tableLayoutPanel1.SetRowSpan(this.label6, 2);
            this.label6.Size = new System.Drawing.Size(77, 48);
            this.label6.TabIndex = 1;
            this.label6.Text = "时间选择";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.SystemColors.Control;
            this.btnClear.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClear.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnClear.Location = new System.Drawing.Point(238, 95);
            this.btnClear.Margin = new System.Windows.Forms.Padding(1);
            this.btnClear.Name = "btnClear";
            this.tableLayoutPanel1.SetRowSpan(this.btnClear, 2);
            this.btnClear.Size = new System.Drawing.Size(79, 48);
            this.btnClear.TabIndex = 3;
            this.btnClear.Text = "清除";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // dtpDate
            // 
            this.dtpDate.Checked = false;
            this.tableLayoutPanel1.SetColumnSpan(this.dtpDate, 2);
            this.dtpDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpDate.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.dtpDate.Location = new System.Drawing.Point(80, 95);
            this.dtpDate.Margin = new System.Windows.Forms.Padding(1);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(156, 23);
            this.dtpDate.TabIndex = 2;
            this.dtpDate.ValueChanged += new System.EventHandler(this.dtpDate_ValueChanged);
            // 
            // panelEx1
            // 
            this.panelEx1.BackColor = System.Drawing.Color.White;
            this.panelEx1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(151)))), ((int)(((byte)(151)))));
            this.panelEx1.BorderSize = 1;
            this.panelEx1.Controls.Add(this.label1);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(65)))), ((int)(((byte)(97)))));
            this.panelEx1.Location = new System.Drawing.Point(1, 1);
            this.panelEx1.Margin = new System.Windows.Forms.Padding(1);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Padding = new System.Windows.Forms.Padding(3);
            this.panelEx1.Size = new System.Drawing.Size(322, 28);
            this.panelEx1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(65)))), ((int)(((byte)(97)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Margin = new System.Windows.Forms.Padding(3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(316, 22);
            this.label1.TabIndex = 1;
            this.label1.Text = "Yield Info";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.panelEx2, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.panelEx1, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(324, 180);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // panelEx2
            // 
            this.panelEx2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(151)))), ((int)(((byte)(151)))));
            this.panelEx2.BorderSize = 1;
            this.panelEx2.Controls.Add(this.tableLayoutPanel1);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx2.FillColor = System.Drawing.SystemColors.Control;
            this.panelEx2.Location = new System.Drawing.Point(1, 31);
            this.panelEx2.Margin = new System.Windows.Forms.Padding(1);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Padding = new System.Windows.Forms.Padding(2);
            this.panelEx2.Size = new System.Drawing.Size(322, 148);
            this.panelEx2.TabIndex = 0;
            // 
            // YieldMonitorCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel2);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "YieldMonitorCtrl";
            this.Size = new System.Drawing.Size(324, 180);
            this.Load += new System.EventHandler(this.YieldCtrl_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panelEx1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panelEx2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblOkCount;
        private System.Windows.Forms.Label lblNGCount;
        private System.Windows.Forms.Label lblTotalCount;
        private System.Windows.Forms.Label lblOkRate;
        private System.Windows.Forms.DateTimePicker dtpTime;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private HTPCBase.Extension.UserCtrl.PanelEx panelEx1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private HTPCBase.Extension.UserCtrl.PanelEx panelEx2;
        private System.Windows.Forms.Label label1;
    }
}
