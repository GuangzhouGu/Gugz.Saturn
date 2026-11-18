namespace HYC.Mt2Aoi.Controls
{
    partial class UcGigECamera
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
            this.htPanel1 = new HYC.WindowsControls.HTPanel();
            this.btnSetGain = new HYC.WindowsControls.HTButton();
            this.btnGetGain = new HYC.WindowsControls.HTButton();
            this.txtGrayValue = new HYC.WindowsControls.HTTextBox();
            this.txtGain = new HYC.WindowsControls.HTTextBox();
            this.htLabel4 = new HYC.WindowsControls.HTLabel();
            this.htLabel5 = new HYC.WindowsControls.HTLabel();
            this.btnSetExposure = new HYC.WindowsControls.HTButton();
            this.btnGetExposure = new HYC.WindowsControls.HTButton();
            this.txtExposure = new HYC.WindowsControls.HTTextBox();
            this.btnGrayTtest = new HYC.WindowsControls.HTButton();
            this.btnContinues = new HYC.WindowsControls.HTButton();
            this.btnSaveImg = new HYC.WindowsControls.HTButton();
            this.btnGrab = new HYC.WindowsControls.HTButton();
            this.cboCamera = new HYC.WindowsControls.HTComboBox();
            this.htLabel3 = new HYC.WindowsControls.HTLabel();
            this.htLabel2 = new HYC.WindowsControls.HTLabel();
            this.htLabel1 = new HYC.WindowsControls.HTLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cbxIsBlack = new System.Windows.Forms.CheckBox();
            this.cbxIsReadLocalImage = new System.Windows.Forms.CheckBox();
            this.txtRoiRegion = new HYC.WindowsControls.HTTextBox();
            this.htLabel6 = new HYC.WindowsControls.HTLabel();
            this.tbpnlCamera.SuspendLayout();
            this.htPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tbpnlCamera
            // 
            this.tbpnlCamera.ColumnCount = 1;
            this.tbpnlCamera.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbpnlCamera.Controls.Add(this.htPanel1, 0, 0);
            this.tbpnlCamera.Controls.Add(this.pictureBox1, 0, 1);
            this.tbpnlCamera.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbpnlCamera.Location = new System.Drawing.Point(0, 0);
            this.tbpnlCamera.Name = "tbpnlCamera";
            this.tbpnlCamera.RowCount = 2;
            this.tbpnlCamera.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 133F));
            this.tbpnlCamera.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbpnlCamera.Size = new System.Drawing.Size(944, 615);
            this.tbpnlCamera.TabIndex = 140;
            // 
            // htPanel1
            // 
            this.htPanel1.Controls.Add(this.cbxIsBlack);
            this.htPanel1.Controls.Add(this.cbxIsReadLocalImage);
            this.htPanel1.Controls.Add(this.txtRoiRegion);
            this.htPanel1.Controls.Add(this.htLabel6);
            this.htPanel1.Controls.Add(this.btnSetGain);
            this.htPanel1.Controls.Add(this.btnGetGain);
            this.htPanel1.Controls.Add(this.txtGrayValue);
            this.htPanel1.Controls.Add(this.txtGain);
            this.htPanel1.Controls.Add(this.htLabel4);
            this.htPanel1.Controls.Add(this.htLabel5);
            this.htPanel1.Controls.Add(this.btnSetExposure);
            this.htPanel1.Controls.Add(this.btnGetExposure);
            this.htPanel1.Controls.Add(this.txtExposure);
            this.htPanel1.Controls.Add(this.btnGrayTtest);
            this.htPanel1.Controls.Add(this.btnContinues);
            this.htPanel1.Controls.Add(this.btnSaveImg);
            this.htPanel1.Controls.Add(this.btnGrab);
            this.htPanel1.Controls.Add(this.cboCamera);
            this.htPanel1.Controls.Add(this.htLabel3);
            this.htPanel1.Controls.Add(this.htLabel2);
            this.htPanel1.Controls.Add(this.htLabel1);
            this.htPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.htPanel1.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.htPanel1.Location = new System.Drawing.Point(0, 0);
            this.htPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.htPanel1.Name = "htPanel1";
            this.htPanel1.Size = new System.Drawing.Size(944, 133);
            this.htPanel1.TabIndex = 141;
            this.htPanel1.Text = null;
            // 
            // btnSetGain
            // 
            this.btnSetGain.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSetGain.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.btnSetGain.Location = new System.Drawing.Point(758, 56);
            this.btnSetGain.Name = "btnSetGain";
            this.btnSetGain.Size = new System.Drawing.Size(100, 28);
            this.btnSetGain.TabIndex = 143;
            this.btnSetGain.Text = "设定";
            this.btnSetGain.Click += new System.EventHandler(this.btnSetGain_Click);
            // 
            // btnGetGain
            // 
            this.btnGetGain.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGetGain.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.btnGetGain.Location = new System.Drawing.Point(642, 56);
            this.btnGetGain.Name = "btnGetGain";
            this.btnGetGain.Size = new System.Drawing.Size(100, 28);
            this.btnGetGain.TabIndex = 144;
            this.btnGetGain.Text = "读取";
            this.btnGetGain.Click += new System.EventHandler(this.btnGetGain_Click);
            // 
            // txtGrayValue
            // 
            this.txtGrayValue.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtGrayValue.FillColor = System.Drawing.Color.White;
            this.txtGrayValue.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.txtGrayValue.Location = new System.Drawing.Point(519, 98);
            this.txtGrayValue.Maximum = 2147483647D;
            this.txtGrayValue.Minimum = -2147483648D;
            this.txtGrayValue.Name = "txtGrayValue";
            this.txtGrayValue.Padding = new System.Windows.Forms.Padding(5);
            this.txtGrayValue.ReadOnly = true;
            this.txtGrayValue.Size = new System.Drawing.Size(87, 26);
            this.txtGrayValue.TabIndex = 147;
            // 
            // txtGain
            // 
            this.txtGain.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtGain.FillColor = System.Drawing.Color.White;
            this.txtGain.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.txtGain.Location = new System.Drawing.Point(519, 58);
            this.txtGain.Maximum = 2147483647D;
            this.txtGain.Minimum = -2147483648D;
            this.txtGain.Name = "txtGain";
            this.txtGain.Padding = new System.Windows.Forms.Padding(5);
            this.txtGain.Size = new System.Drawing.Size(87, 26);
            this.txtGain.TabIndex = 147;
            // 
            // htLabel4
            // 
            this.htLabel4.AutoSize = true;
            this.htLabel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.htLabel4.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.htLabel4.Location = new System.Drawing.Point(459, 98);
            this.htLabel4.Name = "htLabel4";
            this.htLabel4.Size = new System.Drawing.Size(54, 20);
            this.htLabel4.TabIndex = 146;
            this.htLabel4.Text = "灰度值:";
            this.htLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // htLabel5
            // 
            this.htLabel5.AutoSize = true;
            this.htLabel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.htLabel5.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.htLabel5.Location = new System.Drawing.Point(472, 58);
            this.htLabel5.Name = "htLabel5";
            this.htLabel5.Size = new System.Drawing.Size(40, 20);
            this.htLabel5.TabIndex = 146;
            this.htLabel5.Text = "增益:";
            this.htLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSetExposure
            // 
            this.btnSetExposure.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSetExposure.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.btnSetExposure.Location = new System.Drawing.Point(757, 17);
            this.btnSetExposure.Name = "btnSetExposure";
            this.btnSetExposure.Size = new System.Drawing.Size(100, 28);
            this.btnSetExposure.TabIndex = 128;
            this.btnSetExposure.Text = "设定";
            this.btnSetExposure.Click += new System.EventHandler(this.btnSetExposure_Click);
            // 
            // btnGetExposure
            // 
            this.btnGetExposure.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGetExposure.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.btnGetExposure.Location = new System.Drawing.Point(642, 17);
            this.btnGetExposure.Name = "btnGetExposure";
            this.btnGetExposure.Size = new System.Drawing.Size(100, 28);
            this.btnGetExposure.TabIndex = 129;
            this.btnGetExposure.Text = "读取";
            this.btnGetExposure.Click += new System.EventHandler(this.btnGetExposure_Click);
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
            this.txtExposure.TabIndex = 142;
            // 
            // btnGrayTtest
            // 
            this.btnGrayTtest.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGrayTtest.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.btnGrayTtest.Location = new System.Drawing.Point(362, 49);
            this.btnGrayTtest.Name = "btnGrayTtest";
            this.btnGrayTtest.Size = new System.Drawing.Size(100, 28);
            this.btnGrayTtest.TabIndex = 141;
            this.btnGrayTtest.Text = "灰度值测试";
            this.btnGrayTtest.Click += new System.EventHandler(this.btnGrayTtest_Click);
            // 
            // btnContinues
            // 
            this.btnContinues.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnContinues.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.btnContinues.Location = new System.Drawing.Point(362, 15);
            this.btnContinues.Name = "btnContinues";
            this.btnContinues.Size = new System.Drawing.Size(100, 28);
            this.btnContinues.TabIndex = 141;
            this.btnContinues.Text = "实时";
            this.btnContinues.Click += new System.EventHandler(this.btnContinues_Click);
            // 
            // btnSaveImg
            // 
            this.btnSaveImg.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSaveImg.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.btnSaveImg.Location = new System.Drawing.Point(247, 49);
            this.btnSaveImg.Name = "btnSaveImg";
            this.btnSaveImg.Size = new System.Drawing.Size(100, 28);
            this.btnSaveImg.TabIndex = 141;
            this.btnSaveImg.Text = "保存照片";
            this.btnSaveImg.Click += new System.EventHandler(this.btnSaveImg_Click);
            // 
            // btnGrab
            // 
            this.btnGrab.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGrab.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.btnGrab.Location = new System.Drawing.Point(247, 15);
            this.btnGrab.Name = "btnGrab";
            this.btnGrab.Size = new System.Drawing.Size(100, 28);
            this.btnGrab.TabIndex = 141;
            this.btnGrab.Text = "拍照";
            this.btnGrab.Click += new System.EventHandler(this.btnGrab_Click);
            // 
            // cboCamera
            // 
            this.cboCamera.DropDownStyle = HYC.WindowsControls.HTDropDownStyle.DropDownList;
            this.cboCamera.FillColor = System.Drawing.Color.White;
            this.cboCamera.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.cboCamera.Location = new System.Drawing.Point(91, 15);
            this.cboCamera.MinimumSize = new System.Drawing.Size(63, 0);
            this.cboCamera.Name = "cboCamera";
            this.cboCamera.Padding = new System.Windows.Forms.Padding(0, 0, 30, 0);
            this.cboCamera.Size = new System.Drawing.Size(140, 29);
            this.cboCamera.TabIndex = 140;
            this.cboCamera.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.cboCamera.SelectedIndexChanged += new System.EventHandler(this.cboCamera_SelectedIndexChanged);
            // 
            // htLabel3
            // 
            this.htLabel3.AutoSize = true;
            this.htLabel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.htLabel3.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.htLabel3.Location = new System.Drawing.Point(608, 18);
            this.htLabel3.Name = "htLabel3";
            this.htLabel3.Size = new System.Drawing.Size(28, 20);
            this.htLabel3.TabIndex = 139;
            this.htLabel3.Text = "ms";
            this.htLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // htLabel2
            // 
            this.htLabel2.AutoSize = true;
            this.htLabel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.htLabel2.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.htLabel2.Location = new System.Drawing.Point(472, 17);
            this.htLabel2.Name = "htLabel2";
            this.htLabel2.Size = new System.Drawing.Size(40, 20);
            this.htLabel2.TabIndex = 139;
            this.htLabel2.Text = "曝光:";
            this.htLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // htLabel1
            // 
            this.htLabel1.AutoSize = true;
            this.htLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.htLabel1.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.htLabel1.Location = new System.Drawing.Point(7, 17);
            this.htLabel1.Name = "htLabel1";
            this.htLabel1.Size = new System.Drawing.Size(68, 20);
            this.htLabel1.TabIndex = 139;
            this.htLabel1.Text = "相机选择:";
            this.htLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(1, 134);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(942, 480);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 142;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.imageBox1_Paint);
            // 
            // cbxIsBlack
            // 
            this.cbxIsBlack.AutoSize = true;
            this.cbxIsBlack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.cbxIsBlack.Location = new System.Drawing.Point(11, 86);
            this.cbxIsBlack.Name = "cbxIsBlack";
            this.cbxIsBlack.Size = new System.Drawing.Size(112, 24);
            this.cbxIsBlack.TabIndex = 153;
            this.cbxIsBlack.Text = "是否是黑画面";
            this.cbxIsBlack.UseVisualStyleBackColor = false;
            // 
            // cbxIsReadLocalImage
            // 
            this.cbxIsReadLocalImage.AutoSize = true;
            this.cbxIsReadLocalImage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.cbxIsReadLocalImage.Location = new System.Drawing.Point(11, 57);
            this.cbxIsReadLocalImage.Name = "cbxIsReadLocalImage";
            this.cbxIsReadLocalImage.Size = new System.Drawing.Size(112, 24);
            this.cbxIsReadLocalImage.TabIndex = 154;
            this.cbxIsReadLocalImage.Text = "读取本地图片";
            this.cbxIsReadLocalImage.UseVisualStyleBackColor = false;
            // 
            // txtRoiRegion
            // 
            this.txtRoiRegion.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtRoiRegion.FillColor = System.Drawing.Color.White;
            this.txtRoiRegion.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.txtRoiRegion.Location = new System.Drawing.Point(684, 98);
            this.txtRoiRegion.Maximum = 2147483647D;
            this.txtRoiRegion.Minimum = -2147483648D;
            this.txtRoiRegion.Name = "txtRoiRegion";
            this.txtRoiRegion.Padding = new System.Windows.Forms.Padding(5);
            this.txtRoiRegion.ReadOnly = true;
            this.txtRoiRegion.Size = new System.Drawing.Size(245, 26);
            this.txtRoiRegion.TabIndex = 152;
            // 
            // htLabel6
            // 
            this.htLabel6.AutoSize = true;
            this.htLabel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.htLabel6.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.htLabel6.Location = new System.Drawing.Point(614, 98);
            this.htLabel6.Name = "htLabel6";
            this.htLabel6.Size = new System.Drawing.Size(64, 20);
            this.htLabel6.TabIndex = 151;
            this.htLabel6.Text = "ROI区域:";
            this.htLabel6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // UcGigECamera
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbpnlCamera);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "UcGigECamera";
            this.Size = new System.Drawing.Size(944, 615);
            this.Load += new System.EventHandler(this.UcGigECamera_Load);
            this.VisibleChanged += new System.EventHandler(this.UcGigECamera_VisibleChanged);
            this.tbpnlCamera.ResumeLayout(false);
            this.htPanel1.ResumeLayout(false);
            this.htPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tbpnlCamera;
        private HYC.WindowsControls.HTLabel htLabel1;
        private HYC.WindowsControls.HTComboBox cboCamera;
        private HYC.WindowsControls.HTButton btnGrab;
        private HYC.WindowsControls.HTPanel htPanel1;
        private HYC.WindowsControls.HTLabel htLabel2;
        private HYC.WindowsControls.HTTextBox txtExposure;
        private HYC.WindowsControls.HTLabel htLabel3;
        private HYC.WindowsControls.HTButton btnSetExposure;
        private HYC.WindowsControls.HTButton btnGetExposure;
        private HYC.WindowsControls.HTButton btnSetGain;
        private HYC.WindowsControls.HTButton btnGetGain;
        private HYC.WindowsControls.HTTextBox txtGain;
        private HYC.WindowsControls.HTLabel htLabel5;
        private WindowsControls.HTButton btnContinues;
        private WindowsControls.HTButton btnSaveImg;
        private System.Windows.Forms.PictureBox pictureBox1;
        private WindowsControls.HTButton btnGrayTtest;
        private WindowsControls.HTTextBox txtGrayValue;
        private WindowsControls.HTLabel htLabel4;
        private System.Windows.Forms.CheckBox cbxIsBlack;
        private System.Windows.Forms.CheckBox cbxIsReadLocalImage;
        private WindowsControls.HTTextBox txtRoiRegion;
        private WindowsControls.HTLabel htLabel6;
    }
}
