namespace CommonModule.Controls
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(YieldMonitorCtrl));
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.panelEx1 = new CommonModule.Controls.PanelEx();
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
            this.panelEx2 = new CommonModule.Controls.PanelEx();
            this.tableLayoutPanel2.SuspendLayout();
            this.panelEx1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panelEx2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.panelEx1, 0, 1);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // panelEx1
            // 
            this.panelEx1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(151)))), ((int)(((byte)(151)))));
            this.panelEx1.BorderSize = 1;
            this.panelEx1.Controls.Add(this.tableLayoutPanel1);
            resources.ApplyResources(this.panelEx1, "panelEx1");
            this.panelEx1.FillColor = System.Drawing.SystemColors.Control;
            this.panelEx1.Name = "panelEx1";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
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
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // lblOkCount
            // 
            this.lblOkCount.BackColor = System.Drawing.Color.WhiteSmoke;
            resources.ApplyResources(this.lblOkCount, "lblOkCount");
            this.lblOkCount.ForeColor = System.Drawing.Color.Green;
            this.lblOkCount.Name = "lblOkCount";
            // 
            // lblNGCount
            // 
            this.lblNGCount.BackColor = System.Drawing.Color.WhiteSmoke;
            resources.ApplyResources(this.lblNGCount, "lblNGCount");
            this.lblNGCount.ForeColor = System.Drawing.Color.OrangeRed;
            this.lblNGCount.Name = "lblNGCount";
            // 
            // lblTotalCount
            // 
            this.lblTotalCount.BackColor = System.Drawing.Color.WhiteSmoke;
            resources.ApplyResources(this.lblTotalCount, "lblTotalCount");
            this.lblTotalCount.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblTotalCount.Name = "lblTotalCount";
            // 
            // lblOkRate
            // 
            this.lblOkRate.BackColor = System.Drawing.Color.WhiteSmoke;
            resources.ApplyResources(this.lblOkRate, "lblOkRate");
            this.lblOkRate.ForeColor = System.Drawing.Color.LimeGreen;
            this.lblOkRate.Name = "lblOkRate";
            // 
            // dtpTime
            // 
            this.dtpTime.Checked = false;
            this.tableLayoutPanel1.SetColumnSpan(this.dtpTime, 2);
            resources.ApplyResources(this.dtpTime, "dtpTime");
            this.dtpTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpTime.Name = "dtpTime";
            this.dtpTime.ShowCheckBox = true;
            this.dtpTime.ShowUpDown = true;
            this.dtpTime.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            this.tableLayoutPanel1.SetRowSpan(this.label6, 2);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.btnClear, "btnClear");
            this.btnClear.FlatAppearance.BorderSize = 0;
            this.btnClear.Name = "btnClear";
            this.tableLayoutPanel1.SetRowSpan(this.btnClear, 2);
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // dtpDate
            // 
            this.dtpDate.Checked = false;
            this.tableLayoutPanel1.SetColumnSpan(this.dtpDate, 2);
            resources.ApplyResources(this.dtpDate, "dtpDate");
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.ValueChanged += new System.EventHandler(this.dtpDate_ValueChanged);
            // 
            // panelEx2
            // 
            this.panelEx2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(151)))), ((int)(((byte)(151)))));
            this.panelEx2.BorderSize = 1;
            this.panelEx2.Controls.Add(this.tableLayoutPanel2);
            this.panelEx2.CornerRadius = 5;
            resources.ApplyResources(this.panelEx2, "panelEx2");
            this.panelEx2.FillColor = System.Drawing.SystemColors.Control;
            this.panelEx2.Name = "panelEx2";
            // 
            // YieldMonitorCtrl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.panelEx2);
            this.Name = "YieldMonitorCtrl";
            this.Load += new System.EventHandler(this.YieldCtrl_Load);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panelEx1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panelEx2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
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
        private CommonModule.Controls.PanelEx panelEx1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private PanelEx panelEx2;
        private System.Windows.Forms.DateTimePicker dtpDate;
    }
}
