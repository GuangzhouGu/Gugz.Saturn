
namespace HYC.SaturnAoi.SubForms
{
    partial class FrmCheck
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
            this.htTabControl1 = new HYC.WindowsControls.HTTabControl();
            this.CaptureCard = new System.Windows.Forms.TabPage();
            this.ucMilCamera1 = new HYC.Mt2Aoi.Controls.UcMilCamera();
            this.GigeCamera = new System.Windows.Forms.TabPage();
            this.ucGigECamera1 = new HYC.Mt2Aoi.Controls.UcGigECamera();
            this.Tester = new System.Windows.Forms.TabPage();
            this.ucTester1 = new HYC.Mt2Aoi.Controls.UcTester();
            this.htTabControl1.SuspendLayout();
            this.CaptureCard.SuspendLayout();
            this.GigeCamera.SuspendLayout();
            this.Tester.SuspendLayout();
            this.SuspendLayout();
            // 
            // htTabControl1
            // 
            this.htTabControl1.Controls.Add(this.CaptureCard);
            this.htTabControl1.Controls.Add(this.GigeCamera);
            this.htTabControl1.Controls.Add(this.Tester);
            this.htTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.htTabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.htTabControl1.FillColor = System.Drawing.Color.White;
            this.htTabControl1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.htTabControl1.ItemSize = new System.Drawing.Size(150, 25);
            this.htTabControl1.Location = new System.Drawing.Point(0, 0);
            this.htTabControl1.Margin = new System.Windows.Forms.Padding(1);
            this.htTabControl1.MenuStyle = HYC.WindowsControls.HTMenuStyle.Custom;
            this.htTabControl1.Name = "htTabControl1";
            this.htTabControl1.SelectedIndex = 0;
            this.htTabControl1.Size = new System.Drawing.Size(1002, 633);
            this.htTabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.htTabControl1.Style = HYC.WindowsControls.HTStyle.Custom;
            this.htTabControl1.TabBackColor = System.Drawing.Color.Gainsboro;
            this.htTabControl1.TabIndex = 2;
            this.htTabControl1.TabSelectedColor = System.Drawing.Color.Silver;
            this.htTabControl1.TabSelectedForeColor = System.Drawing.Color.Black;
            this.htTabControl1.TabSelectedHighColor = System.Drawing.Color.Silver;
            this.htTabControl1.TabUnSelectedForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            // 
            // CaptureCard
            // 
            this.CaptureCard.BackColor = System.Drawing.Color.White;
            this.CaptureCard.Controls.Add(this.ucMilCamera1);
            this.CaptureCard.Location = new System.Drawing.Point(0, 25);
            this.CaptureCard.Name = "CaptureCard";
            this.CaptureCard.Size = new System.Drawing.Size(1002, 608);
            this.CaptureCard.TabIndex = 4;
            this.CaptureCard.Text = "采集卡";
            // 
            // ucMilCamera1
            // 
            this.ucMilCamera1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucMilCamera1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ucMilCamera1.Location = new System.Drawing.Point(0, 0);
            this.ucMilCamera1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ucMilCamera1.Name = "ucMilCamera1";
            this.ucMilCamera1.Size = new System.Drawing.Size(1002, 608);
            this.ucMilCamera1.TabIndex = 0;
            // 
            // GigeCamera
            // 
            this.GigeCamera.BackColor = System.Drawing.Color.White;
            this.GigeCamera.Controls.Add(this.ucGigECamera1);
            this.GigeCamera.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.GigeCamera.Location = new System.Drawing.Point(0, 25);
            this.GigeCamera.Name = "GigeCamera";
            this.GigeCamera.Size = new System.Drawing.Size(1002, 608);
            this.GigeCamera.TabIndex = 0;
            this.GigeCamera.Text = "相机";
            // 
            // ucGigECamera1
            // 
            this.ucGigECamera1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucGigECamera1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ucGigECamera1.Location = new System.Drawing.Point(0, 0);
            this.ucGigECamera1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ucGigECamera1.Name = "ucGigECamera1";
            this.ucGigECamera1.Size = new System.Drawing.Size(1002, 608);
            this.ucGigECamera1.TabIndex = 0;
            // 
            // Tester
            // 
            this.Tester.BackColor = System.Drawing.Color.White;
            this.Tester.Controls.Add(this.ucTester1);
            this.Tester.Location = new System.Drawing.Point(0, 25);
            this.Tester.Name = "Tester";
            this.Tester.Size = new System.Drawing.Size(1002, 608);
            this.Tester.TabIndex = 6;
            this.Tester.Text = "检查机";
            // 
            // ucTester1
            // 
            this.ucTester1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucTester1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ucTester1.Location = new System.Drawing.Point(0, 0);
            this.ucTester1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ucTester1.Name = "ucTester1";
            this.ucTester1.Size = new System.Drawing.Size(1002, 608);
            this.ucTester1.TabIndex = 0;
            // 
            // FrmCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1002, 633);
            this.Controls.Add(this.htTabControl1);
            this.Name = "FrmCheck";
            this.Text = "设备点检";
            this.Load += new System.EventHandler(this.FrmCheck_Load);
            this.htTabControl1.ResumeLayout(false);
            this.CaptureCard.ResumeLayout(false);
            this.GigeCamera.ResumeLayout(false);
            this.Tester.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private WindowsControls.HTTabControl htTabControl1;
        private System.Windows.Forms.TabPage CaptureCard;
        private System.Windows.Forms.TabPage GigeCamera;
        private Mt2Aoi.Controls.UcMilCamera ucMilCamera1;
        private Mt2Aoi.Controls.UcGigECamera ucGigECamera1;
        private System.Windows.Forms.TabPage Tester;
        private Mt2Aoi.Controls.UcTester ucTester1;

    }
}