namespace HYC.AutoPosition.UI.View.Parameter
{
    partial class ViewTemplatePara
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label8 = new System.Windows.Forms.Label();
            this.cboGroup = new System.Windows.Forms.ComboBox();
            this.tbpnlMain = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.cboStage = new System.Windows.Forms.ComboBox();
            this.markSetting = new HYC.AutoPosition.UI.Controls.TemplateMatchingSetting();
            this.tbpnlMain.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label8.Location = new System.Drawing.Point(8, 12);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(32, 17);
            this.label8.TabIndex = 3;
            this.label8.Text = "组：";
            // 
            // cboGroup
            // 
            this.cboGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGroup.FormattingEnabled = true;
            this.cboGroup.Location = new System.Drawing.Point(46, 6);
            this.cboGroup.Name = "cboGroup";
            this.cboGroup.Size = new System.Drawing.Size(112, 25);
            this.cboGroup.TabIndex = 2;
            this.cboGroup.SelectedIndexChanged += new System.EventHandler(this.cboGroup_SelectedIndexChanged);
            // 
            // tbpnlMain
            // 
            this.tbpnlMain.ColumnCount = 1;
            this.tbpnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbpnlMain.Controls.Add(this.panel1, 0, 0);
            this.tbpnlMain.Controls.Add(this.markSetting, 0, 1);
            this.tbpnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbpnlMain.Location = new System.Drawing.Point(0, 0);
            this.tbpnlMain.Name = "tbpnlMain";
            this.tbpnlMain.RowCount = 2;
            this.tbpnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tbpnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbpnlMain.Size = new System.Drawing.Size(1346, 932);
            this.tbpnlMain.TabIndex = 128;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cboStage);
            this.panel1.Controls.Add(this.cboGroup);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1346, 40);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(178, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "工位：";
            // 
            // cboStage
            // 
            this.cboStage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStage.FormattingEnabled = true;
            this.cboStage.Location = new System.Drawing.Point(228, 6);
            this.cboStage.Name = "cboStage";
            this.cboStage.Size = new System.Drawing.Size(81, 25);
            this.cboStage.TabIndex = 4;
            this.cboStage.SelectedIndexChanged += new System.EventHandler(this.cboStage_SelectedIndexChanged);
            // 
            // markSetting
            // 
            this.markSetting.AutoScroll = true;
            this.markSetting.BackColor = System.Drawing.Color.WhiteSmoke;
            this.markSetting.CurrentGroupIndex = 0;
            this.markSetting.CurrentStageIndex = 0;
            this.markSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.markSetting.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.markSetting.Location = new System.Drawing.Point(3, 44);
            this.markSetting.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.markSetting.Name = "markSetting";
            this.markSetting.Size = new System.Drawing.Size(1340, 884);
            this.markSetting.TabIndex = 1;
            // 
            // ViewTemplatePara
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1346, 932);
            this.Controls.Add(this.tbpnlMain);
            this.Name = "ViewTemplatePara";
            this.Text = "ViewFlowPara";
            this.tbpnlMain.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cboGroup;
        private System.Windows.Forms.TableLayoutPanel tbpnlMain;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cboStage;
        private System.Windows.Forms.Label label1;
        private Controls.TemplateMatchingSetting markSetting;
    }
}