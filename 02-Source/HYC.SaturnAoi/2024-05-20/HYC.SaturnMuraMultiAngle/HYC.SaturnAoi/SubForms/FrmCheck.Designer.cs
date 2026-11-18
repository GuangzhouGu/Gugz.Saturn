
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
            this.GigeCamera = new System.Windows.Forms.TabPage();
            this.MultiAngle = new System.Windows.Forms.TabPage();
            this.ucGigECamera1 = new HYC.Mt2Aoi.Controls.UcGigECamera();
            this.ucMultiAngle1 = new HYC.SaturnAoi.Controls.UcMultiAngle();
            this.htTabControl1.SuspendLayout();
            this.GigeCamera.SuspendLayout();
            this.MultiAngle.SuspendLayout();
            this.SuspendLayout();
            // 
            // htTabControl1
            // 
            this.htTabControl1.Controls.Add(this.GigeCamera);
            this.htTabControl1.Controls.Add(this.MultiAngle);
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
            // MultiAngle
            // 
            this.MultiAngle.BackColor = System.Drawing.Color.White;
            this.MultiAngle.Controls.Add(this.ucMultiAngle1);
            this.MultiAngle.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MultiAngle.Location = new System.Drawing.Point(0, 25);
            this.MultiAngle.Name = "MultiAngle";
            this.MultiAngle.Size = new System.Drawing.Size(1002, 608);
            this.MultiAngle.TabIndex = 3;
            this.MultiAngle.Text = "光谱仪";
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
            // ucMultiAngle1
            // 
            this.ucMultiAngle1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucMultiAngle1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ucMultiAngle1.Location = new System.Drawing.Point(0, 0);
            this.ucMultiAngle1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ucMultiAngle1.Name = "ucMultiAngle1";
            this.ucMultiAngle1.Size = new System.Drawing.Size(1002, 608);
            this.ucMultiAngle1.TabIndex = 0;
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
            this.GigeCamera.ResumeLayout(false);
            this.MultiAngle.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private WindowsControls.HTTabControl htTabControl1;
        private System.Windows.Forms.TabPage GigeCamera;
        private System.Windows.Forms.TabPage MultiAngle;
        private Mt2Aoi.Controls.UcGigECamera ucGigECamera1;
        private Controls.UcMultiAngle ucMultiAngle1;
    }
}