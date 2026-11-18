namespace HYC.Mt2Aoi.Controls
{
    partial class UcMilCamera
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
            this.tbpnlCamera = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new HYC.WindowsControls.HTPanel();
            this.btnSaveImg = new HYC.WindowsControls.HTButton();
            this.label6 = new HYC.WindowsControls.HTLabel();
            this.label7 = new HYC.WindowsControls.HTLabel();
            this.htButton1 = new HYC.WindowsControls.HTButton();
            this.btnLive = new HYC.WindowsControls.HTButton();
            this.btnGrab = new HYC.WindowsControls.HTButton();
            this.cboCamera = new HYC.WindowsControls.HTComboBox();
            this.label1 = new HYC.WindowsControls.HTLabel();
            this.txtExposure = new HYC.WindowsControls.HTTextBox();
            this.txtGrayValue = new HYC.WindowsControls.HTTextBox();
            this.txtGain = new HYC.WindowsControls.HTTextBox();
            this.btnSetExposure = new HYC.WindowsControls.HTButton();
            this.htLabel1 = new HYC.WindowsControls.HTLabel();
            this.label16 = new HYC.WindowsControls.HTLabel();
            this.btnGetGain = new HYC.WindowsControls.HTButton();
            this.btnSetGain = new HYC.WindowsControls.HTButton();
            this.btnGetExposure = new HYC.WindowsControls.HTButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cbxIsReadLocalImage = new System.Windows.Forms.CheckBox();
            this.cbxIsBlack = new System.Windows.Forms.CheckBox();
            this.htLabel2 = new HYC.WindowsControls.HTLabel();
            this.txtRoiRegion = new HYC.WindowsControls.HTTextBox();
            this.tbpnlCamera.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tbpnlCamera
            // 
            this.tbpnlCamera.ColumnCount = 1;
            this.tbpnlCamera.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbpnlCamera.Controls.Add(this.panel1, 0, 0);
            this.tbpnlCamera.Controls.Add(this.pictureBox1, 0, 1);
            this.tbpnlCamera.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbpnlCamera.Location = new System.Drawing.Point(0, 0);
            this.tbpnlCamera.Name = "tbpnlCamera";
            this.tbpnlCamera.RowCount = 2;
            this.tbpnlCamera.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 127F));
            this.tbpnlCamera.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbpnlCamera.Size = new System.Drawing.Size(944, 615);
            this.tbpnlCamera.TabIndex = 140;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cbxIsBlack);
            this.panel1.Controls.Add(this.cbxIsReadLocalImage);
            this.panel1.Controls.Add(this.btnSaveImg);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.htButton1);
            this.panel1.Controls.Add(this.btnLive);
            this.panel1.Controls.Add(this.btnGrab);
            this.panel1.Controls.Add(this.cboCamera);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtExposure);
            this.panel1.Controls.Add(this.txtRoiRegion);
            this.panel1.Controls.Add(this.txtGrayValue);
            this.panel1.Controls.Add(this.txtGain);
            this.panel1.Controls.Add(this.htLabel2);
            this.panel1.Controls.Add(this.btnSetExposure);
            this.panel1.Controls.Add(this.htLabel1);
            this.panel1.Controls.Add(this.label16);
            this.panel1.Controls.Add(this.btnGetGain);
            this.panel1.Controls.Add(this.btnSetGain);
            this.panel1.Controls.Add(this.btnGetExposure);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(944, 127);
            this.panel1.TabIndex = 124;
            this.panel1.Text = null;
            // 
            // btnSaveImg
            // 
            this.btnSaveImg.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSaveImg.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.btnSaveImg.Location = new System.Drawing.Point(247, 49);
            this.btnSaveImg.Name = "btnSaveImg";
            this.btnSaveImg.Size = new System.Drawing.Size(100, 28);
            this.btnSaveImg.TabIndex = 149;
            this.btnSaveImg.Text = "保存照片";
            this.btnSaveImg.Click += new System.EventHandler(this.btnSaveImg_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.label6.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.label6.Location = new System.Drawing.Point(608, 18);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(28, 20);
            this.label6.TabIndex = 148;
            this.label6.Text = "ms";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.label7.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.label7.Location = new System.Drawing.Point(472, 17);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(40, 20);
            this.label7.TabIndex = 148;
            this.label7.Text = "曝光:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // htButton1
            // 
            this.htButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.htButton1.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.htButton1.Location = new System.Drawing.Point(362, 50);
            this.htButton1.Name = "htButton1";
            this.htButton1.Size = new System.Drawing.Size(100, 28);
            this.htButton1.TabIndex = 146;
            this.htButton1.Text = "灰度值测试";
            this.htButton1.Click += new System.EventHandler(this.htbGray_Click);
            // 
            // btnLive
            // 
            this.btnLive.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLive.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.btnLive.Location = new System.Drawing.Point(362, 15);
            this.btnLive.Name = "btnLive";
            this.btnLive.Size = new System.Drawing.Size(100, 28);
            this.btnLive.TabIndex = 146;
            this.btnLive.Text = "实时";
            this.btnLive.Click += new System.EventHandler(this.btnLive_Click);
            // 
            // btnGrab
            // 
            this.btnGrab.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGrab.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.btnGrab.Location = new System.Drawing.Point(247, 15);
            this.btnGrab.Name = "btnGrab";
            this.btnGrab.Size = new System.Drawing.Size(100, 28);
            this.btnGrab.TabIndex = 146;
            this.btnGrab.Text = "拍照";
            this.btnGrab.Click += new System.EventHandler(this.btnGrab_Click);
            // 
            // cboCamera
            // 
            this.cboCamera.DropDownStyle = HYC.WindowsControls.HTDropDownStyle.DropDownList;
            this.cboCamera.FillColor = System.Drawing.Color.White;
            this.cboCamera.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.cboCamera.Location = new System.Drawing.Point(81, 14);
            this.cboCamera.MinimumSize = new System.Drawing.Size(63, 0);
            this.cboCamera.Name = "cboCamera";
            this.cboCamera.Padding = new System.Windows.Forms.Padding(0, 0, 30, 0);
            this.cboCamera.Size = new System.Drawing.Size(150, 29);
            this.cboCamera.TabIndex = 145;
            this.cboCamera.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.cboCamera.SelectedIndexChanged += new System.EventHandler(this.cboCamera_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.label1.Location = new System.Drawing.Point(7, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 20);
            this.label1.TabIndex = 144;
            this.label1.Text = "相机选择:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtExposure
            // 
            this.txtExposure.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtExposure.FillColor = System.Drawing.Color.White;
            this.txtExposure.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.txtExposure.Location = new System.Drawing.Point(518, 18);
            this.txtExposure.Maximum = 2147483647D;
            this.txtExposure.Minimum = -2147483648D;
            this.txtExposure.Name = "txtExposure";
            this.txtExposure.Padding = new System.Windows.Forms.Padding(5);
            this.txtExposure.Size = new System.Drawing.Size(87, 26);
            this.txtExposure.TabIndex = 138;
            // 
            // txtGrayValue
            // 
            this.txtGrayValue.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtGrayValue.FillColor = System.Drawing.Color.White;
            this.txtGrayValue.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.txtGrayValue.Location = new System.Drawing.Point(518, 88);
            this.txtGrayValue.Maximum = 2147483647D;
            this.txtGrayValue.Minimum = -2147483648D;
            this.txtGrayValue.Name = "txtGrayValue";
            this.txtGrayValue.Padding = new System.Windows.Forms.Padding(5);
            this.txtGrayValue.ReadOnly = true;
            this.txtGrayValue.Size = new System.Drawing.Size(87, 26);
            this.txtGrayValue.TabIndex = 137;
            // 
            // txtGain
            // 
            this.txtGain.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtGain.FillColor = System.Drawing.Color.White;
            this.txtGain.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.txtGain.Location = new System.Drawing.Point(518, 53);
            this.txtGain.Maximum = 2147483647D;
            this.txtGain.Minimum = -2147483648D;
            this.txtGain.Name = "txtGain";
            this.txtGain.Padding = new System.Windows.Forms.Padding(5);
            this.txtGain.Size = new System.Drawing.Size(87, 26);
            this.txtGain.TabIndex = 137;
            // 
            // btnSetExposure
            // 
            this.btnSetExposure.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSetExposure.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.btnSetExposure.Location = new System.Drawing.Point(757, 17);
            this.btnSetExposure.Name = "btnSetExposure";
            this.btnSetExposure.Size = new System.Drawing.Size(100, 28);
            this.btnSetExposure.TabIndex = 122;
            this.btnSetExposure.Text = "设定";
            this.btnSetExposure.Click += new System.EventHandler(this.btnSetExposure_Click);
            // 
            // htLabel1
            // 
            this.htLabel1.AutoSize = true;
            this.htLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.htLabel1.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.htLabel1.Location = new System.Drawing.Point(458, 88);
            this.htLabel1.Name = "htLabel1";
            this.htLabel1.Size = new System.Drawing.Size(54, 20);
            this.htLabel1.TabIndex = 136;
            this.htLabel1.Text = "灰度值:";
            this.htLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.label16.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.label16.Location = new System.Drawing.Point(472, 53);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(40, 20);
            this.label16.TabIndex = 136;
            this.label16.Text = "增益:";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnGetGain
            // 
            this.btnGetGain.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGetGain.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.btnGetGain.Location = new System.Drawing.Point(642, 50);
            this.btnGetGain.Name = "btnGetGain";
            this.btnGetGain.Size = new System.Drawing.Size(100, 28);
            this.btnGetGain.TabIndex = 135;
            this.btnGetGain.Text = "读取";
            this.btnGetGain.Click += new System.EventHandler(this.btnGetGain_Click);
            // 
            // btnSetGain
            // 
            this.btnSetGain.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSetGain.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.btnSetGain.Location = new System.Drawing.Point(757, 50);
            this.btnSetGain.Name = "btnSetGain";
            this.btnSetGain.Size = new System.Drawing.Size(100, 28);
            this.btnSetGain.TabIndex = 134;
            this.btnSetGain.Text = "设定";
            this.btnSetGain.Click += new System.EventHandler(this.btnSetGain_Click);
            // 
            // btnGetExposure
            // 
            this.btnGetExposure.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGetExposure.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.btnGetExposure.Location = new System.Drawing.Point(642, 17);
            this.btnGetExposure.Name = "btnGetExposure";
            this.btnGetExposure.Size = new System.Drawing.Size(100, 28);
            this.btnGetExposure.TabIndex = 127;
            this.btnGetExposure.Text = "读取";
            this.btnGetExposure.Click += new System.EventHandler(this.btnGetExposure_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(1, 128);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(942, 486);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 125;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.imageBox1_Paint);
            // 
            // cbxIsReadLocalImage
            // 
            this.cbxIsReadLocalImage.AutoSize = true;
            this.cbxIsReadLocalImage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.cbxIsReadLocalImage.Location = new System.Drawing.Point(81, 55);
            this.cbxIsReadLocalImage.Name = "cbxIsReadLocalImage";
            this.cbxIsReadLocalImage.Size = new System.Drawing.Size(112, 24);
            this.cbxIsReadLocalImage.TabIndex = 150;
            this.cbxIsReadLocalImage.Text = "读取本地图片";
            this.cbxIsReadLocalImage.UseVisualStyleBackColor = false;
            // 
            // cbxIsBlack
            // 
            this.cbxIsBlack.AutoSize = true;
            this.cbxIsBlack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.cbxIsBlack.Location = new System.Drawing.Point(81, 84);
            this.cbxIsBlack.Name = "cbxIsBlack";
            this.cbxIsBlack.Size = new System.Drawing.Size(112, 24);
            this.cbxIsBlack.TabIndex = 150;
            this.cbxIsBlack.Text = "是否是黑画面";
            this.cbxIsBlack.UseVisualStyleBackColor = false;
            // 
            // htLabel2
            // 
            this.htLabel2.AutoSize = true;
            this.htLabel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.htLabel2.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.htLabel2.Location = new System.Drawing.Point(613, 88);
            this.htLabel2.Name = "htLabel2";
            this.htLabel2.Size = new System.Drawing.Size(64, 20);
            this.htLabel2.TabIndex = 136;
            this.htLabel2.Text = "ROI区域:";
            this.htLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtRoiRegion
            // 
            this.txtRoiRegion.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtRoiRegion.FillColor = System.Drawing.Color.White;
            this.txtRoiRegion.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.txtRoiRegion.Location = new System.Drawing.Point(683, 88);
            this.txtRoiRegion.Maximum = 2147483647D;
            this.txtRoiRegion.Minimum = -2147483648D;
            this.txtRoiRegion.Name = "txtRoiRegion";
            this.txtRoiRegion.Padding = new System.Windows.Forms.Padding(5);
            this.txtRoiRegion.ReadOnly = true;
            this.txtRoiRegion.Size = new System.Drawing.Size(245, 26);
            this.txtRoiRegion.TabIndex = 137;
            // 
            // UcMilCamera
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbpnlCamera);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "UcMilCamera";
            this.Size = new System.Drawing.Size(944, 615);
            this.Load += new System.EventHandler(this.UcMilCamera_Load);
            this.VisibleChanged += new System.EventHandler(this.UcMilCamera_VisibleChanged);
            this.tbpnlCamera.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.TableLayoutPanel tbpnlCamera;
        private HYC.WindowsControls.HTPanel panel1;
        private HYC.WindowsControls.HTTextBox txtExposure;
        private HYC.WindowsControls.HTTextBox txtGain;
        private HYC.WindowsControls.HTButton btnSetExposure;
        private HYC.WindowsControls.HTLabel label16;
        private HYC.WindowsControls.HTButton btnGetGain;
        private HYC.WindowsControls.HTButton btnSetGain;
        private HYC.WindowsControls.HTButton btnGetExposure;
        private HYC.WindowsControls.HTLabel label1;
        private HYC.WindowsControls.HTComboBox cboCamera;
        private HYC.WindowsControls.HTButton btnGrab;
        private HYC.WindowsControls.HTLabel label6;
        private HYC.WindowsControls.HTLabel label7;
        private WindowsControls.HTButton btnLive;
        private WindowsControls.HTButton btnSaveImg;
        private System.Windows.Forms.PictureBox pictureBox1;
        private WindowsControls.HTButton htButton1;
        private WindowsControls.HTTextBox txtGrayValue;
        private WindowsControls.HTLabel htLabel1;
        private System.Windows.Forms.CheckBox cbxIsReadLocalImage;
        private System.Windows.Forms.CheckBox cbxIsBlack;
        private WindowsControls.HTTextBox txtRoiRegion;
        private WindowsControls.HTLabel htLabel2;
    }
}
