namespace HYC.AutoPosition.UI.Controls
{
    partial class FormMarkModify
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
            this.groupBoxZoom = new System.Windows.Forms.GroupBox();
            this.checkBoxZoom3 = new System.Windows.Forms.CheckBox();
            this.checkBoxZoom2 = new System.Windows.Forms.CheckBox();
            this.checkBoxZoom1 = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnRight = new System.Windows.Forms.Button();
            this.btnLeft = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.cmbSpeed = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panelImageViewer = new System.Windows.Forms.Panel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBoxZoom.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxZoom
            // 
            this.groupBoxZoom.Controls.Add(this.checkBoxZoom3);
            this.groupBoxZoom.Controls.Add(this.checkBoxZoom2);
            this.groupBoxZoom.Controls.Add(this.checkBoxZoom1);
            this.groupBoxZoom.Location = new System.Drawing.Point(13, 13);
            this.groupBoxZoom.Name = "groupBoxZoom";
            this.groupBoxZoom.Size = new System.Drawing.Size(158, 53);
            this.groupBoxZoom.TabIndex = 0;
            this.groupBoxZoom.TabStop = false;
            this.groupBoxZoom.Text = "Zoom";
            // 
            // checkBoxZoom3
            // 
            this.checkBoxZoom3.AutoSize = true;
            this.checkBoxZoom3.Location = new System.Drawing.Point(109, 22);
            this.checkBoxZoom3.Name = "checkBoxZoom3";
            this.checkBoxZoom3.Size = new System.Drawing.Size(40, 21);
            this.checkBoxZoom3.TabIndex = 2;
            this.checkBoxZoom3.Tag = "2";
            this.checkBoxZoom3.Text = "x5";
            this.checkBoxZoom3.UseVisualStyleBackColor = true;
            this.checkBoxZoom3.Click += new System.EventHandler(this.checkBoxZoom_CheckedChanged);
            // 
            // checkBoxZoom2
            // 
            this.checkBoxZoom2.AutoSize = true;
            this.checkBoxZoom2.Location = new System.Drawing.Point(63, 22);
            this.checkBoxZoom2.Name = "checkBoxZoom2";
            this.checkBoxZoom2.Size = new System.Drawing.Size(40, 21);
            this.checkBoxZoom2.TabIndex = 1;
            this.checkBoxZoom2.Tag = "1";
            this.checkBoxZoom2.Text = "x2";
            this.checkBoxZoom2.UseVisualStyleBackColor = true;
            this.checkBoxZoom2.Click += new System.EventHandler(this.checkBoxZoom_CheckedChanged);
            // 
            // checkBoxZoom1
            // 
            this.checkBoxZoom1.AutoSize = true;
            this.checkBoxZoom1.Location = new System.Drawing.Point(17, 23);
            this.checkBoxZoom1.Name = "checkBoxZoom1";
            this.checkBoxZoom1.Size = new System.Drawing.Size(40, 21);
            this.checkBoxZoom1.TabIndex = 0;
            this.checkBoxZoom1.Tag = "0";
            this.checkBoxZoom1.Text = "x1";
            this.checkBoxZoom1.UseVisualStyleBackColor = true;
            this.checkBoxZoom1.Click += new System.EventHandler(this.checkBoxZoom_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnDown);
            this.groupBox1.Controls.Add(this.btnRight);
            this.groupBox1.Controls.Add(this.btnLeft);
            this.groupBox1.Controls.Add(this.btnUp);
            this.groupBox1.Controls.Add(this.cmbSpeed);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(13, 64);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(159, 158);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Target Point Move";
            // 
            // btnDown
            // 
            this.btnDown.Location = new System.Drawing.Point(58, 123);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(45, 29);
            this.btnDown.TabIndex = 5;
            this.btnDown.Tag = "2";
            this.btnDown.Text = "Down";
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnMove_Click);
            // 
            // btnRight
            // 
            this.btnRight.Location = new System.Drawing.Point(104, 89);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(49, 29);
            this.btnRight.TabIndex = 4;
            this.btnRight.Tag = "1";
            this.btnRight.Text = "Right";
            this.btnRight.UseVisualStyleBackColor = true;
            this.btnRight.Click += new System.EventHandler(this.btnMove_Click);
            // 
            // btnLeft
            // 
            this.btnLeft.Location = new System.Drawing.Point(6, 89);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(45, 29);
            this.btnLeft.TabIndex = 3;
            this.btnLeft.Tag = "3";
            this.btnLeft.Text = "Left";
            this.btnLeft.UseVisualStyleBackColor = true;
            this.btnLeft.Click += new System.EventHandler(this.btnMove_Click);
            // 
            // btnUp
            // 
            this.btnUp.Location = new System.Drawing.Point(58, 55);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(45, 29);
            this.btnUp.TabIndex = 2;
            this.btnUp.Tag = "0";
            this.btnUp.Text = "Up";
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnMove_Click);
            // 
            // cmbSpeed
            // 
            this.cmbSpeed.FormattingEnabled = true;
            this.cmbSpeed.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "5"});
            this.cmbSpeed.Location = new System.Drawing.Point(58, 23);
            this.cmbSpeed.Name = "cmbSpeed";
            this.cmbSpeed.Size = new System.Drawing.Size(91, 25);
            this.cmbSpeed.TabIndex = 1;
            this.cmbSpeed.SelectedIndexChanged += new System.EventHandler(this.cmbSpeed_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Speed";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(12, 232);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(53, 29);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(102, 232);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(60, 29);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // panelImageViewer
            // 
            this.panelImageViewer.Location = new System.Drawing.Point(178, 13);
            this.panelImageViewer.Name = "panelImageViewer";
            this.panelImageViewer.Size = new System.Drawing.Size(286, 250);
            this.panelImageViewer.TabIndex = 8;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 277);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(467, 22);
            this.statusStrip1.TabIndex = 9;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(37, 17);
            this.toolStripStatusLabel1.Text = "X:  Y:";
            // 
            // FormMarkModify
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(467, 299);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panelImageViewer);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBoxZoom);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FormMarkModify";
            this.Text = "Mark Modify";
            this.groupBoxZoom.ResumeLayout(false);
            this.groupBoxZoom.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxZoom;
        private System.Windows.Forms.CheckBox checkBoxZoom3;
        private System.Windows.Forms.CheckBox checkBoxZoom2;
        private System.Windows.Forms.CheckBox checkBoxZoom1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbSpeed;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnRight;
        private System.Windows.Forms.Button btnLeft;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel panelImageViewer;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}