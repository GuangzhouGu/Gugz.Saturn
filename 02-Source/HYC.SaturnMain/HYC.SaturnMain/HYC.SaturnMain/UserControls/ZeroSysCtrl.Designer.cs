namespace HYC.HTPCBase.UserCtrl
{
    partial class ZeroSysCtrl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ZeroSysCtrl));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnZeroUnlock = new HYC.WindowsControls.Buttons.HCButton2();
            this.btnZeroLock = new HYC.WindowsControls.Buttons.HCButton2();
            this.pbxUnlockSts = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pbxLockSts = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lblName = new System.Windows.Forms.Label();
            this.panelEx1 = new HYC.HTPCBase.Extension.UserCtrl.PanelEx();
            this.panelEx2 = new HYC.HTPCBase.Extension.UserCtrl.PanelEx();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxUnlockSts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxLockSts)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.panelEx1.SuspendLayout();
            this.panelEx2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.btnZeroUnlock, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnZeroLock, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.pbxUnlockSts, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.pbxLockSts, 1, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // btnZeroUnlock
            // 
            this.btnZeroUnlock.CustomerInformation = null;
            resources.ApplyResources(this.btnZeroUnlock, "btnZeroUnlock");
            this.btnZeroUnlock.Name = "btnZeroUnlock";
            this.btnZeroUnlock.OriginalColor = System.Drawing.Color.White;
            this.btnZeroUnlock.Click += new System.EventHandler(this.btnZeroUnlock_Click);
            // 
            // btnZeroLock
            // 
            this.btnZeroLock.CustomerInformation = null;
            resources.ApplyResources(this.btnZeroLock, "btnZeroLock");
            this.btnZeroLock.Name = "btnZeroLock";
            this.btnZeroLock.OriginalColor = System.Drawing.Color.White;
            this.btnZeroLock.Click += new System.EventHandler(this.btnZeroLock_Click);
            // 
            // pbxUnlockSts
            // 
            this.pbxUnlockSts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(244)))), ((int)(((byte)(247)))));
            this.pbxUnlockSts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.pbxUnlockSts, "pbxUnlockSts");
            this.pbxUnlockSts.Image = global::HYC.SaturnMain.Properties.Resources.Gray2;
            this.pbxUnlockSts.Name = "pbxUnlockSts";
            this.pbxUnlockSts.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutPanel1.SetColumnSpan(this.pictureBox1, 2);
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // pbxLockSts
            // 
            this.pbxLockSts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(244)))), ((int)(((byte)(247)))));
            this.pbxLockSts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.pbxLockSts, "pbxLockSts");
            this.pbxLockSts.Image = global::HYC.SaturnMain.Properties.Resources.Gray2;
            this.pbxLockSts.Name = "pbxLockSts";
            this.pbxLockSts.TabStop = false;
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.Controls.Add(this.lblName, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.panelEx1, 0, 1);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // lblName
            // 
            this.lblName.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.lblName, "lblName");
            this.lblName.Name = "lblName";
            // 
            // panelEx1
            // 
            this.panelEx1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panelEx1.BorderSize = 1;
            this.panelEx1.Controls.Add(this.tableLayoutPanel1);
            resources.ApplyResources(this.panelEx1, "panelEx1");
            this.panelEx1.FillColor = System.Drawing.SystemColors.Control;
            this.panelEx1.Name = "panelEx1";
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
            // ZeroSysCtrl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelEx2);
            this.Name = "ZeroSysCtrl";
            this.Load += new System.EventHandler(this.ZeroSysCtrl_Load);
            this.VisibleChanged += new System.EventHandler(this.ZeroSysCtrl_VisibleChanged);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbxUnlockSts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxLockSts)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panelEx1.ResumeLayout(false);
            this.panelEx2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox pbxUnlockSts;
        private WindowsControls.Buttons.HCButton2 btnZeroLock;
        private WindowsControls.Buttons.HCButton2 btnZeroUnlock;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label lblName;
        private Extension.UserCtrl.PanelEx panelEx1;
        private System.Windows.Forms.PictureBox pbxLockSts;
        private Extension.UserCtrl.PanelEx panelEx2;
    }
}
