using CommonModule.Controls;

namespace CommonFunc.Controls
{
    partial class PerformanceMonitorCtrl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PerformanceMonitorCtrl));
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.panelEx1 = new CommonModule.Controls.PanelEx();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label8 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblMtba = new System.Windows.Forms.Label();
            this.lblMtbf = new System.Windows.Forms.Label();
            this.lblJamRate = new System.Windows.Forms.Label();
            this.lblUph = new System.Windows.Forms.Label();
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
            this.tableLayoutPanel1.Controls.Add(this.label8, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.dateTimePicker1, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblMtba, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblMtbf, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblJamRate, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblUph, 1, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Checked = false;
            resources.ApplyResources(this.dateTimePicker1, "dateTimePicker1");
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.ShowCheckBox = true;
            this.dateTimePicker1.ShowUpDown = true;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
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
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // lblMtba
            // 
            this.lblMtba.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.lblMtba, "lblMtba");
            this.lblMtba.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.lblMtba.Name = "lblMtba";
            // 
            // lblMtbf
            // 
            this.lblMtbf.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.lblMtbf, "lblMtbf");
            this.lblMtbf.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.lblMtbf.Name = "lblMtbf";
            // 
            // lblJamRate
            // 
            this.lblJamRate.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.lblJamRate, "lblJamRate");
            this.lblJamRate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.lblJamRate.Name = "lblJamRate";
            // 
            // lblUph
            // 
            this.lblUph.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.lblUph, "lblUph");
            this.lblUph.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.lblUph.Name = "lblUph";
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
            // PerformanceMonitorCtrl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.panelEx2);
            this.Name = "PerformanceMonitorCtrl";
            this.Load += new System.EventHandler(this.PerformanceMonitorCtrl_Load);
            this.VisibleChanged += new System.EventHandler(this.PerformanceMonitorCtrl_VisibleChanged);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panelEx1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panelEx2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblMtba;
        private System.Windows.Forms.Label lblMtbf;
        private System.Windows.Forms.Label lblJamRate;
        private System.Windows.Forms.Label lblUph;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private PanelEx panelEx1;
        private PanelEx panelEx2;
    }
}
