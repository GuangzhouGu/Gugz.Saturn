namespace HYC.SaturnMain.SubForms
{
    partial class FrmCamBasePoint
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
            this.btnIRCamBasePoint = new HYC.WindowsControls.Buttons.HCButton2();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbxYDirBasePoint = new System.Windows.Forms.TextBox();
            this.tbxXDirBasePoint = new System.Windows.Forms.TextBox();
            this.cmxCamList = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmxStage = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnIRCamBasePoint
            // 
            this.btnIRCamBasePoint.CustomerInformation = null;
            this.btnIRCamBasePoint.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnIRCamBasePoint.ForeColor = System.Drawing.Color.Black;
            this.btnIRCamBasePoint.Location = new System.Drawing.Point(264, 144);
            this.btnIRCamBasePoint.Name = "btnIRCamBasePoint";
            this.btnIRCamBasePoint.OriginalColor = System.Drawing.Color.White;
            this.btnIRCamBasePoint.Size = new System.Drawing.Size(99, 31);
            this.btnIRCamBasePoint.TabIndex = 136;
            this.btnIRCamBasePoint.Text = "计算";
            this.btnIRCamBasePoint.Click += new System.EventHandler(this.btnIRCamBasePoint_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(10, 149);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 20);
            this.label2.TabIndex = 133;
            this.label2.Text = "Y方向基准点";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(10, 103);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 20);
            this.label1.TabIndex = 134;
            this.label1.Text = "X方向基准点";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(11, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 20);
            this.label5.TabIndex = 135;
            this.label5.Text = "相机";
            // 
            // tbxYDirBasePoint
            // 
            this.tbxYDirBasePoint.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbxYDirBasePoint.Location = new System.Drawing.Point(98, 146);
            this.tbxYDirBasePoint.Name = "tbxYDirBasePoint";
            this.tbxYDirBasePoint.ReadOnly = true;
            this.tbxYDirBasePoint.Size = new System.Drawing.Size(145, 26);
            this.tbxYDirBasePoint.TabIndex = 130;
            // 
            // tbxXDirBasePoint
            // 
            this.tbxXDirBasePoint.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbxXDirBasePoint.Location = new System.Drawing.Point(98, 100);
            this.tbxXDirBasePoint.Name = "tbxXDirBasePoint";
            this.tbxXDirBasePoint.ReadOnly = true;
            this.tbxXDirBasePoint.Size = new System.Drawing.Size(145, 26);
            this.tbxXDirBasePoint.TabIndex = 131;
            // 
            // cmxCamList
            // 
            this.cmxCamList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmxCamList.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmxCamList.FormattingEnabled = true;
            this.cmxCamList.Items.AddRange(new object[] {
            "",
            "AoiCam1",
            "MuraCam1",
            "SquintMuraCam1",
            "MultiAngle1",
            "AoiCam2",
            "MuraCam2",
            "SquintMuraCam2",
            "MultiAngle2"});
            this.cmxCamList.Location = new System.Drawing.Point(99, 15);
            this.cmxCamList.Name = "cmxCamList";
            this.cmxCamList.Size = new System.Drawing.Size(145, 28);
            this.cmxCamList.TabIndex = 132;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(10, 188);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(558, 45);
            this.label3.TabIndex = 137;
            this.label3.Text = "Tip：相机基准点指的是相机的中心在Glass正中心时，X方向和Y方向的实际坐标。计算相机基准点时，先把相机移动到GlassMark中心位置，然后点击计算";
            // 
            // cmxStage
            // 
            this.cmxStage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmxStage.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmxStage.FormattingEnabled = true;
            this.cmxStage.Items.AddRange(new object[] {
            "平台1",
            "平台2"});
            this.cmxStage.Location = new System.Drawing.Point(99, 55);
            this.cmxStage.Name = "cmxStage";
            this.cmxStage.Size = new System.Drawing.Size(145, 28);
            this.cmxStage.TabIndex = 132;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(11, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 20);
            this.label4.TabIndex = 135;
            this.label4.Text = "平台";
            // 
            // FrmCamBasePoint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 401);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnIRCamBasePoint);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbxYDirBasePoint);
            this.Controls.Add(this.tbxXDirBasePoint);
            this.Controls.Add(this.cmxStage);
            this.Controls.Add(this.cmxCamList);
            this.Name = "FrmCamBasePoint";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormCamBasePoint";
            this.Load += new System.EventHandler(this.FrmCamBasePoint_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private HYC.WindowsControls.Buttons.HCButton2 btnIRCamBasePoint;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbxYDirBasePoint;
        private System.Windows.Forms.TextBox tbxXDirBasePoint;
        private System.Windows.Forms.ComboBox cmxCamList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmxStage;
        private System.Windows.Forms.Label label4;
    }
}