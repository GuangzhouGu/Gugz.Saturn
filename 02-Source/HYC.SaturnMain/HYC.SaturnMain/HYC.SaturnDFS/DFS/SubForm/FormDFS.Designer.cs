namespace DFS.SubForm
{
    partial class FormDFS
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
            this.label3 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtPwd = new System.Windows.Forms.TextBox();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.txtHost = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.txtImgPathUpload = new System.Windows.Forms.TextBox();
            this.txtStepID = new System.Windows.Forms.TextBox();
            this.txtFilePathUpload = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lstMsg = new HYC.WindowsControls.HCListBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtMode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtShop = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label15 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txtUNITID = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtEQPID = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbEnableDFS = new System.Windows.Forms.CheckBox();
            this.panel2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(3, 303);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "消息列表：";
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSave.Location = new System.Drawing.Point(550, 69);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(79, 30);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "保存设置";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtPwd
            // 
            this.txtPwd.Location = new System.Drawing.Point(400, 14);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.PasswordChar = '*';
            this.txtPwd.Size = new System.Drawing.Size(90, 23);
            this.txtPwd.TabIndex = 15;
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(254, 14);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(90, 23);
            this.txtUser.TabIndex = 14;
            // 
            // txtHost
            // 
            this.txtHost.Location = new System.Drawing.Point(49, 14);
            this.txtHost.Name = "txtHost";
            this.txtHost.Size = new System.Drawing.Size(150, 23);
            this.txtHost.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(353, 17);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 17);
            this.label7.TabIndex = 12;
            this.label7.Text = "密码：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(207, 17);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(44, 17);
            this.label8.TabIndex = 11;
            this.label8.Text = "用户：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 17);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(44, 17);
            this.label9.TabIndex = 10;
            this.label9.Text = "站点：";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(-1, 41);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(64, 17);
            this.label10.TabIndex = 16;
            this.label10.Text = "FTP设置：";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.txtHost);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.txtPwd);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.txtUser);
            this.panel2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panel2.Location = new System.Drawing.Point(2, 61);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(513, 46);
            this.panel2.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(9, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 17);
            this.label2.TabIndex = 13;
            this.label2.Text = "STEP ID:";
            // 
            // txtImgPathUpload
            // 
            this.txtImgPathUpload.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtImgPathUpload.Location = new System.Drawing.Point(90, 131);
            this.txtImgPathUpload.Name = "txtImgPathUpload";
            this.txtImgPathUpload.ReadOnly = true;
            this.txtImgPathUpload.Size = new System.Drawing.Size(523, 23);
            this.txtImgPathUpload.TabIndex = 20;
            // 
            // txtStepID
            // 
            this.txtStepID.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtStepID.Location = new System.Drawing.Point(90, 68);
            this.txtStepID.Name = "txtStepID";
            this.txtStepID.Size = new System.Drawing.Size(108, 23);
            this.txtStepID.TabIndex = 14;
            this.txtStepID.TextChanged += new System.EventHandler(this.txtStepID_TextChanged);
            // 
            // txtFilePathUpload
            // 
            this.txtFilePathUpload.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtFilePathUpload.Location = new System.Drawing.Point(90, 102);
            this.txtFilePathUpload.Name = "txtFilePathUpload";
            this.txtFilePathUpload.ReadOnly = true;
            this.txtFilePathUpload.Size = new System.Drawing.Size(523, 23);
            this.txtFilePathUpload.TabIndex = 19;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.Location = new System.Drawing.Point(9, 105);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(59, 17);
            this.label12.TabIndex = 15;
            this.label12.Text = "文件路径:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.Location = new System.Drawing.Point(9, 134);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(59, 17);
            this.label13.TabIndex = 18;
            this.label13.Text = "图片路径:";
            // 
            // lstMsg
            // 
            this.lstMsg.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lstMsg.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lstMsg.FormattingEnabled = true;
            this.lstMsg.HorizontalScrollbar = true;
            this.lstMsg.ItemHeight = 16;
            this.lstMsg.Location = new System.Drawing.Point(6, 323);
            this.lstMsg.Name = "lstMsg";
            this.lstMsg.SelectionBackColor = System.Drawing.Color.White;
            this.lstMsg.Size = new System.Drawing.Size(630, 148);
            this.lstMsg.TabIndex = 5;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.ForeColor = System.Drawing.Color.Gray;
            this.label11.Location = new System.Drawing.Point(212, 42);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(116, 17);
            this.label11.TabIndex = 26;
            this.label11.Text = "（online / offline）";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.ForeColor = System.Drawing.Color.Gray;
            this.label6.Location = new System.Drawing.Point(212, 14);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(206, 17);
            this.label6.TabIndex = 25;
            this.label6.Text = "（bp / even / eac / module / qa）";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(9, 42);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 17);
            this.label5.TabIndex = 23;
            this.label5.Text = "Mode:";
            // 
            // txtMode
            // 
            this.txtMode.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtMode.Location = new System.Drawing.Point(90, 39);
            this.txtMode.Name = "txtMode";
            this.txtMode.Size = new System.Drawing.Size(108, 23);
            this.txtMode.TabIndex = 24;
            this.txtMode.TextChanged += new System.EventHandler(this.txtMode_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(9, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 17);
            this.label1.TabIndex = 21;
            this.label1.Text = "Shop:";
            // 
            // txtShop
            // 
            this.txtShop.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtShop.Location = new System.Drawing.Point(90, 11);
            this.txtShop.Name = "txtShop";
            this.txtShop.ReadOnly = true;
            this.txtShop.Size = new System.Drawing.Size(108, 23);
            this.txtShop.TabIndex = 22;
            this.txtShop.TextChanged += new System.EventHandler(this.txtShop_TextChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl1.Location = new System.Drawing.Point(2, 113);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(638, 187);
            this.tabControl1.TabIndex = 18;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label15);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.label12);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label13);
            this.tabPage1.Controls.Add(this.txtMode);
            this.tabPage1.Controls.Add(this.txtFilePathUpload);
            this.tabPage1.Controls.Add(this.txtStepID);
            this.tabPage1.Controls.Add(this.txtShop);
            this.tabPage1.Controls.Add(this.txtImgPathUpload);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(630, 157);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "DFS路径设置";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label15.ForeColor = System.Drawing.Color.Gray;
            this.label15.Location = new System.Drawing.Point(404, 81);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(214, 17);
            this.label15.TabIndex = 27;
            this.label15.Text = "Panel ID示例：7F60154001A1AAA01";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txtUNITID);
            this.tabPage2.Controls.Add(this.label14);
            this.tabPage2.Controls.Add(this.txtEQPID);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(630, 157);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "其他参数设置";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // txtUNITID
            // 
            this.txtUNITID.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtUNITID.Location = new System.Drawing.Point(123, 47);
            this.txtUNITID.Name = "txtUNITID";
            this.txtUNITID.Size = new System.Drawing.Size(108, 23);
            this.txtUNITID.TabIndex = 18;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.Location = new System.Drawing.Point(13, 50);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(59, 17);
            this.label14.TabIndex = 17;
            this.label14.Text = "UNIT_ID:";
            // 
            // txtEQPID
            // 
            this.txtEQPID.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtEQPID.Location = new System.Drawing.Point(123, 18);
            this.txtEQPID.Name = "txtEQPID";
            this.txtEQPID.Size = new System.Drawing.Size(108, 23);
            this.txtEQPID.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(13, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 17);
            this.label4.TabIndex = 15;
            this.label4.Text = "EQP_ID:";
            // 
            // cbEnableDFS
            // 
            this.cbEnableDFS.AutoSize = true;
            this.cbEnableDFS.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbEnableDFS.Location = new System.Drawing.Point(2, 12);
            this.cbEnableDFS.Name = "cbEnableDFS";
            this.cbEnableDFS.Size = new System.Drawing.Size(73, 21);
            this.cbEnableDFS.TabIndex = 19;
            this.cbEnableDFS.Text = "启用DFS";
            this.cbEnableDFS.UseVisualStyleBackColor = true;
            // 
            // FormDFS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbEnableDFS);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lstMsg);
            this.Name = "FormDFS";
            this.Size = new System.Drawing.Size(647, 491);
            this.Load += new System.EventHandler(this.FormDFS_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private HYC.WindowsControls.HCListBox lstMsg;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtPwd;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.TextBox txtHost;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtImgPathUpload;
        private System.Windows.Forms.TextBox txtStepID;
        private System.Windows.Forms.TextBox txtFilePathUpload;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtShop;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtMode;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox txtEQPID;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtUNITID;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.CheckBox cbEnableDFS;
    }
}