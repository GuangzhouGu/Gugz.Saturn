namespace HYC.Mt2Aoi.Controls
{
    partial class HTCameraCtrl
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
            this.btnConnect = new HYC.WindowsControls.HTButton();
            this.btnDisconnect = new HYC.WindowsControls.HTButton();
            this.btnSetGain = new HYC.WindowsControls.HTButton();
            this.btnGetGain = new HYC.WindowsControls.HTButton();
            this.txtGain = new HYC.WindowsControls.HTTextBox();
            this.htLabel5 = new HYC.WindowsControls.HTLabel();
            this.btnSetExposure = new HYC.WindowsControls.HTButton();
            this.btnGetExposure = new HYC.WindowsControls.HTButton();
            this.txtExposure = new HYC.WindowsControls.HTTextBox();
            this.btnContinues = new HYC.WindowsControls.HTButton();
            this.btnGrab = new HYC.WindowsControls.HTButton();
            this.cboCamera = new HYC.WindowsControls.HTComboBox();
            this.htLabel3 = new HYC.WindowsControls.HTLabel();
            this.htLabel2 = new HYC.WindowsControls.HTLabel();
            this.htLabel1 = new HYC.WindowsControls.HTLabel();
            this.imageBox1 = new HYC.WindowsControls.HTImageBox();
            this.btnStopAcq = new HYC.WindowsControls.HTButton();
            this.tbpnlCamera.SuspendLayout();
            this.htPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbpnlCamera
            // 
            this.tbpnlCamera.ColumnCount = 1;
            this.tbpnlCamera.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbpnlCamera.Controls.Add(this.htPanel1, 0, 0);
            this.tbpnlCamera.Controls.Add(this.imageBox1, 0, 1);
            this.tbpnlCamera.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbpnlCamera.Location = new System.Drawing.Point(0, 0);
            this.tbpnlCamera.Name = "tbpnlCamera";
            this.tbpnlCamera.RowCount = 2;
            this.tbpnlCamera.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 113F));
            this.tbpnlCamera.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbpnlCamera.Size = new System.Drawing.Size(944, 615);
            this.tbpnlCamera.TabIndex = 140;
            // 
            // htPanel1
            // 
            this.htPanel1.Controls.Add(this.btnConnect);
            this.htPanel1.Controls.Add(this.btnDisconnect);
            this.htPanel1.Controls.Add(this.btnSetGain);
            this.htPanel1.Controls.Add(this.btnGetGain);
            this.htPanel1.Controls.Add(this.txtGain);
            this.htPanel1.Controls.Add(this.htLabel5);
            this.htPanel1.Controls.Add(this.btnSetExposure);
            this.htPanel1.Controls.Add(this.btnGetExposure);
            this.htPanel1.Controls.Add(this.txtExposure);
            this.htPanel1.Controls.Add(this.btnStopAcq);
            this.htPanel1.Controls.Add(this.btnContinues);
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
            this.htPanel1.Size = new System.Drawing.Size(944, 113);
            this.htPanel1.TabIndex = 141;
            this.htPanel1.Text = null;
            // 
            // btnConnect
            // 
            this.btnConnect.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConnect.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.btnConnect.Location = new System.Drawing.Point(641, 12);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(100, 32);
            this.btnConnect.Style = HYC.WindowsControls.HTStyle.Blue;
            this.btnConnect.TabIndex = 143;
            this.btnConnect.Text = "连接";
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDisconnect.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.btnDisconnect.Location = new System.Drawing.Point(757, 12);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(100, 32);
            this.btnDisconnect.Style = HYC.WindowsControls.HTStyle.Blue;
            this.btnDisconnect.TabIndex = 143;
            this.btnDisconnect.Text = "断开连接";
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // btnSetGain
            // 
            this.btnSetGain.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSetGain.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.btnSetGain.Location = new System.Drawing.Point(757, 62);
            this.btnSetGain.Name = "btnSetGain";
            this.btnSetGain.Size = new System.Drawing.Size(100, 32);
            this.btnSetGain.Style = HYC.WindowsControls.HTStyle.Blue;
            this.btnSetGain.TabIndex = 143;
            this.btnSetGain.Text = "设定";
            this.btnSetGain.Click += new System.EventHandler(this.btnSetGain_Click);
            // 
            // btnGetGain
            // 
            this.btnGetGain.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGetGain.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.btnGetGain.Location = new System.Drawing.Point(641, 62);
            this.btnGetGain.Name = "btnGetGain";
            this.btnGetGain.Size = new System.Drawing.Size(100, 32);
            this.btnGetGain.Style = HYC.WindowsControls.HTStyle.Blue;
            this.btnGetGain.TabIndex = 144;
            this.btnGetGain.Text = "读取";
            this.btnGetGain.Click += new System.EventHandler(this.btnGetGain_Click);
            // 
            // txtGain
            // 
            this.txtGain.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtGain.FillColor = System.Drawing.Color.White;
            this.txtGain.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.txtGain.Location = new System.Drawing.Point(538, 65);
            this.txtGain.Maximum = 2147483647D;
            this.txtGain.Minimum = -2147483648D;
            this.txtGain.Name = "txtGain";
            this.txtGain.Padding = new System.Windows.Forms.Padding(5);
            this.txtGain.Size = new System.Drawing.Size(76, 26);
            this.txtGain.TabIndex = 147;
            // 
            // htLabel5
            // 
            this.htLabel5.AutoSize = true;
            this.htLabel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.htLabel5.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.htLabel5.Location = new System.Drawing.Point(472, 68);
            this.htLabel5.Name = "htLabel5";
            this.htLabel5.Size = new System.Drawing.Size(51, 20);
            this.htLabel5.TabIndex = 146;
            this.htLabel5.Text = "增益：";
            this.htLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSetExposure
            // 
            this.btnSetExposure.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSetExposure.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.btnSetExposure.Location = new System.Drawing.Point(362, 62);
            this.btnSetExposure.Name = "btnSetExposure";
            this.btnSetExposure.Size = new System.Drawing.Size(100, 32);
            this.btnSetExposure.Style = HYC.WindowsControls.HTStyle.Blue;
            this.btnSetExposure.TabIndex = 128;
            this.btnSetExposure.Text = "设定";
            this.btnSetExposure.Click += new System.EventHandler(this.btnSetExposure_Click);
            // 
            // btnGetExposure
            // 
            this.btnGetExposure.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGetExposure.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.btnGetExposure.Location = new System.Drawing.Point(247, 62);
            this.btnGetExposure.Name = "btnGetExposure";
            this.btnGetExposure.Size = new System.Drawing.Size(100, 32);
            this.btnGetExposure.Style = HYC.WindowsControls.HTStyle.Blue;
            this.btnGetExposure.TabIndex = 129;
            this.btnGetExposure.Text = "读取";
            this.btnGetExposure.Click += new System.EventHandler(this.btnGetExposure_Click);
            // 
            // txtExposure
            // 
            this.txtExposure.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtExposure.FillColor = System.Drawing.Color.White;
            this.txtExposure.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.txtExposure.Location = new System.Drawing.Point(92, 65);
            this.txtExposure.Maximum = 2147483647D;
            this.txtExposure.Minimum = -2147483648D;
            this.txtExposure.Name = "txtExposure";
            this.txtExposure.Padding = new System.Windows.Forms.Padding(5);
            this.txtExposure.Size = new System.Drawing.Size(76, 26);
            this.txtExposure.TabIndex = 142;
            // 
            // btnContinues
            // 
            this.btnContinues.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnContinues.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.btnContinues.Location = new System.Drawing.Point(362, 15);
            this.btnContinues.Name = "btnContinues";
            this.btnContinues.Size = new System.Drawing.Size(100, 28);
            this.btnContinues.Style = HYC.WindowsControls.HTStyle.Blue;
            this.btnContinues.TabIndex = 141;
            this.btnContinues.Text = "实时";
            this.btnContinues.Click += new System.EventHandler(this.btnContinues_Click);
            // 
            // btnGrab
            // 
            this.btnGrab.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGrab.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.btnGrab.Location = new System.Drawing.Point(247, 15);
            this.btnGrab.Name = "btnGrab";
            this.btnGrab.Size = new System.Drawing.Size(100, 28);
            this.btnGrab.Style = HYC.WindowsControls.HTStyle.Blue;
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
            this.htLabel3.Location = new System.Drawing.Point(182, 68);
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
            this.htLabel2.Location = new System.Drawing.Point(18, 68);
            this.htLabel2.Name = "htLabel2";
            this.htLabel2.Size = new System.Drawing.Size(51, 20);
            this.htLabel2.TabIndex = 139;
            this.htLabel2.Text = "曝光：";
            this.htLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // htLabel1
            // 
            this.htLabel1.AutoSize = true;
            this.htLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.htLabel1.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.htLabel1.Location = new System.Drawing.Point(18, 19);
            this.htLabel1.Name = "htLabel1";
            this.htLabel1.Size = new System.Drawing.Size(79, 20);
            this.htLabel1.TabIndex = 139;
            this.htLabel1.Text = "相机选择：";
            this.htLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // imageBox1
            // 
            this.imageBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.imageBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageBox1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.imageBox1.Image = null;
            this.imageBox1.Location = new System.Drawing.Point(3, 117);
            this.imageBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.imageBox1.Name = "imageBox1";
            this.imageBox1.Size = new System.Drawing.Size(938, 494);
            this.imageBox1.TabIndex = 142;
            // 
            // btnStopAcq
            // 
            this.btnStopAcq.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStopAcq.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.btnStopAcq.Location = new System.Drawing.Point(476, 16);
            this.btnStopAcq.Name = "btnStopAcq";
            this.btnStopAcq.Size = new System.Drawing.Size(100, 28);
            this.btnStopAcq.Style = HYC.WindowsControls.HTStyle.Blue;
            this.btnStopAcq.TabIndex = 141;
            this.btnStopAcq.Text = "停止采集";
            this.btnStopAcq.Click += new System.EventHandler(this.btnStopAcq_Click);
            // 
            // HTCameraCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbpnlCamera);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "HTCameraCtrl";
            this.Size = new System.Drawing.Size(944, 615);
            this.Load += new System.EventHandler(this.UcGigECamera_Load);
            this.tbpnlCamera.ResumeLayout(false);
            this.htPanel1.ResumeLayout(false);
            this.htPanel1.PerformLayout();
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
        private WindowsControls.HTImageBox imageBox1;
        private WindowsControls.HTButton btnContinues;
        private WindowsControls.HTButton btnDisconnect;
        private WindowsControls.HTButton btnConnect;
        private WindowsControls.HTButton btnStopAcq;
    }
}
