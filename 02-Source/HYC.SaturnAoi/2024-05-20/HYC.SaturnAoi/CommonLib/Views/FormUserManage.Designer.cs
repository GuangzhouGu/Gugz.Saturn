namespace CommonLib.Views
{
    partial class FormUserManage
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnModifyPermission = new HYC.WindowsControls.Buttons.HCButton2();
            this.pnlPermission = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlPwd = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.txtNewPwd = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnModifyPwd = new HYC.WindowsControls.Buttons.HCButton2();
            this.txtOldPwd = new System.Windows.Forms.TextBox();
            this.pnlAdd = new System.Windows.Forms.Panel();
            this.rbtnUser = new System.Windows.Forms.RadioButton();
            this.rbtnEngineer = new System.Windows.Forms.RadioButton();
            this.txtNewUser = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnAddUser = new HYC.WindowsControls.Buttons.HCButton2();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPwdConfirm = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPwd = new System.Windows.Forms.TextBox();
            this.btnDeleteUser = new HYC.WindowsControls.Buttons.HCButton2();
            this.listViewUser = new System.Windows.Forms.ListView();
            this.columnName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnLevel = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1.SuspendLayout();
            this.pnlPwd.SuspendLayout();
            this.pnlAdd.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnModifyPermission);
            this.panel1.Controls.Add(this.pnlPermission);
            this.panel1.Location = new System.Drawing.Point(461, 410);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(397, 181);
            this.panel1.TabIndex = 59;
            // 
            // btnModifyPermission
            // 
            this.btnModifyPermission.CustomerInformation = null;
            this.btnModifyPermission.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnModifyPermission.ForeColor = System.Drawing.Color.Black;
            this.btnModifyPermission.Location = new System.Drawing.Point(254, 144);
            this.btnModifyPermission.Name = "btnModifyPermission";
            this.btnModifyPermission.OriginalColor = System.Drawing.Color.White;
            this.btnModifyPermission.Size = new System.Drawing.Size(100, 32);
            this.btnModifyPermission.TabIndex = 40;
            this.btnModifyPermission.Text = "修改权限";
            this.btnModifyPermission.Click += new System.EventHandler(this.btnModifyPermission_Click);
            // 
            // pnlPermission
            // 
            this.pnlPermission.AutoScroll = true;
            this.pnlPermission.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.pnlPermission.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pnlPermission.Location = new System.Drawing.Point(3, 0);
            this.pnlPermission.Name = "pnlPermission";
            this.pnlPermission.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.pnlPermission.Size = new System.Drawing.Size(218, 179);
            this.pnlPermission.TabIndex = 39;
            this.pnlPermission.WrapContents = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(3, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(164, 17);
            this.label1.TabIndex = 58;
            this.label1.Text = "从列表中选中项以进行编辑：";
            // 
            // pnlPwd
            // 
            this.pnlPwd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlPwd.Controls.Add(this.label7);
            this.pnlPwd.Controls.Add(this.txtNewPwd);
            this.pnlPwd.Controls.Add(this.label8);
            this.pnlPwd.Controls.Add(this.btnModifyPwd);
            this.pnlPwd.Controls.Add(this.txtOldPwd);
            this.pnlPwd.Location = new System.Drawing.Point(460, 286);
            this.pnlPwd.Name = "pnlPwd";
            this.pnlPwd.Size = new System.Drawing.Size(398, 118);
            this.pnlPwd.TabIndex = 57;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(44, 27);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 17);
            this.label7.TabIndex = 22;
            this.label7.Text = "原密码：";
            // 
            // txtNewPwd
            // 
            this.txtNewPwd.Location = new System.Drawing.Point(122, 71);
            this.txtNewPwd.Name = "txtNewPwd";
            this.txtNewPwd.PasswordChar = '*';
            this.txtNewPwd.Size = new System.Drawing.Size(100, 23);
            this.txtNewPwd.TabIndex = 25;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(44, 74);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 17);
            this.label8.TabIndex = 23;
            this.label8.Text = "新密码：";
            // 
            // btnModifyPwd
            // 
            this.btnModifyPwd.CustomerInformation = null;
            this.btnModifyPwd.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnModifyPwd.ForeColor = System.Drawing.Color.Black;
            this.btnModifyPwd.Location = new System.Drawing.Point(255, 62);
            this.btnModifyPwd.Name = "btnModifyPwd";
            this.btnModifyPwd.OriginalColor = System.Drawing.Color.White;
            this.btnModifyPwd.Size = new System.Drawing.Size(100, 32);
            this.btnModifyPwd.TabIndex = 1;
            this.btnModifyPwd.Text = "修改密码";
            this.btnModifyPwd.Click += new System.EventHandler(this.btnModifyPwd_Click);
            // 
            // txtOldPwd
            // 
            this.txtOldPwd.Location = new System.Drawing.Point(122, 24);
            this.txtOldPwd.Name = "txtOldPwd";
            this.txtOldPwd.PasswordChar = '*';
            this.txtOldPwd.Size = new System.Drawing.Size(100, 23);
            this.txtOldPwd.TabIndex = 24;
            // 
            // pnlAdd
            // 
            this.pnlAdd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlAdd.Controls.Add(this.rbtnUser);
            this.pnlAdd.Controls.Add(this.rbtnEngineer);
            this.pnlAdd.Controls.Add(this.txtNewUser);
            this.pnlAdd.Controls.Add(this.label4);
            this.pnlAdd.Controls.Add(this.btnAddUser);
            this.pnlAdd.Controls.Add(this.label3);
            this.pnlAdd.Controls.Add(this.txtPwdConfirm);
            this.pnlAdd.Controls.Add(this.label5);
            this.pnlAdd.Controls.Add(this.txtPwd);
            this.pnlAdd.Location = new System.Drawing.Point(461, 66);
            this.pnlAdd.Name = "pnlAdd";
            this.pnlAdd.Size = new System.Drawing.Size(397, 214);
            this.pnlAdd.TabIndex = 56;
            // 
            // rbtnUser
            // 
            this.rbtnUser.AutoSize = true;
            this.rbtnUser.Checked = true;
            this.rbtnUser.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rbtnUser.Location = new System.Drawing.Point(254, 25);
            this.rbtnUser.Name = "rbtnUser";
            this.rbtnUser.Size = new System.Drawing.Size(62, 21);
            this.rbtnUser.TabIndex = 35;
            this.rbtnUser.Text = "操作员";
            this.rbtnUser.UseVisualStyleBackColor = true;
            // 
            // rbtnEngineer
            // 
            this.rbtnEngineer.AutoSize = true;
            this.rbtnEngineer.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rbtnEngineer.Location = new System.Drawing.Point(254, 75);
            this.rbtnEngineer.Name = "rbtnEngineer";
            this.rbtnEngineer.Size = new System.Drawing.Size(62, 21);
            this.rbtnEngineer.TabIndex = 34;
            this.rbtnEngineer.Text = "工程师";
            this.rbtnEngineer.UseVisualStyleBackColor = true;
            // 
            // txtNewUser
            // 
            this.txtNewUser.Location = new System.Drawing.Point(121, 26);
            this.txtNewUser.Name = "txtNewUser";
            this.txtNewUser.Size = new System.Drawing.Size(100, 23);
            this.txtNewUser.TabIndex = 30;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(43, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 17);
            this.label4.TabIndex = 27;
            this.label4.Text = "用户名：";
            // 
            // btnAddUser
            // 
            this.btnAddUser.CustomerInformation = null;
            this.btnAddUser.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAddUser.ForeColor = System.Drawing.Color.Black;
            this.btnAddUser.Location = new System.Drawing.Point(254, 115);
            this.btnAddUser.Name = "btnAddUser";
            this.btnAddUser.OriginalColor = System.Drawing.Color.White;
            this.btnAddUser.Size = new System.Drawing.Size(100, 32);
            this.btnAddUser.TabIndex = 33;
            this.btnAddUser.Text = "添加用户";
            this.btnAddUser.Click += new System.EventHandler(this.btnAddUser_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(43, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 17);
            this.label3.TabIndex = 28;
            this.label3.Text = "密码：";
            // 
            // txtPwdConfirm
            // 
            this.txtPwdConfirm.Location = new System.Drawing.Point(121, 124);
            this.txtPwdConfirm.Name = "txtPwdConfirm";
            this.txtPwdConfirm.PasswordChar = '*';
            this.txtPwdConfirm.Size = new System.Drawing.Size(100, 23);
            this.txtPwdConfirm.TabIndex = 32;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(43, 127);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 17);
            this.label5.TabIndex = 29;
            this.label5.Text = "密码确认：";
            // 
            // txtPwd
            // 
            this.txtPwd.Location = new System.Drawing.Point(121, 74);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.PasswordChar = '*';
            this.txtPwd.Size = new System.Drawing.Size(100, 23);
            this.txtPwd.TabIndex = 31;
            // 
            // btnDeleteUser
            // 
            this.btnDeleteUser.CustomerInformation = null;
            this.btnDeleteUser.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDeleteUser.ForeColor = System.Drawing.Color.Black;
            this.btnDeleteUser.Location = new System.Drawing.Point(716, 613);
            this.btnDeleteUser.Name = "btnDeleteUser";
            this.btnDeleteUser.OriginalColor = System.Drawing.Color.White;
            this.btnDeleteUser.Size = new System.Drawing.Size(100, 32);
            this.btnDeleteUser.TabIndex = 55;
            this.btnDeleteUser.Text = "删除选中用户";
            this.btnDeleteUser.Click += new System.EventHandler(this.btnDeleteUser_Click);
            // 
            // listViewUser
            // 
            this.listViewUser.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnName,
            this.columnLevel});
            this.listViewUser.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listViewUser.FullRowSelect = true;
            this.listViewUser.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewUser.HideSelection = false;
            this.listViewUser.Location = new System.Drawing.Point(6, 66);
            this.listViewUser.MultiSelect = false;
            this.listViewUser.Name = "listViewUser";
            this.listViewUser.Size = new System.Drawing.Size(449, 525);
            this.listViewUser.TabIndex = 54;
            this.listViewUser.UseCompatibleStateImageBehavior = false;
            this.listViewUser.View = System.Windows.Forms.View.Details;
            this.listViewUser.SelectedIndexChanged += new System.EventHandler(this.listViewUser_SelectedIndexChanged);
            // 
            // columnName
            // 
            this.columnName.Text = "用户名";
            this.columnName.Width = 151;
            // 
            // columnLevel
            // 
            this.columnLevel.Text = "用户等级";
            this.columnLevel.Width = 124;
            // 
            // FormUserManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(880, 700);
            this.CloseFormTimeLag = 0;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pnlPwd);
            this.Controls.Add(this.pnlAdd);
            this.Controls.Add(this.btnDeleteUser);
            this.Controls.Add(this.listViewUser);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "FormUserManage";
            this.ShowFormTimelag = 200;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormUserManage";
            this.Load += new System.EventHandler(this.FormUserManage_Load);
            this.Controls.SetChildIndex(this.listViewUser, 0);
            this.Controls.SetChildIndex(this.btnDeleteUser, 0);
            this.Controls.SetChildIndex(this.pnlAdd, 0);
            this.Controls.SetChildIndex(this.pnlPwd, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.panel1.ResumeLayout(false);
            this.pnlPwd.ResumeLayout(false);
            this.pnlPwd.PerformLayout();
            this.pnlAdd.ResumeLayout(false);
            this.pnlAdd.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private HYC.WindowsControls.Buttons.HCButton2 btnModifyPermission;
        private System.Windows.Forms.FlowLayoutPanel pnlPermission;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlPwd;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtNewPwd;
        private System.Windows.Forms.Label label8;
        private HYC.WindowsControls.Buttons.HCButton2 btnModifyPwd;
        private System.Windows.Forms.TextBox txtOldPwd;
        private System.Windows.Forms.Panel pnlAdd;
        private System.Windows.Forms.RadioButton rbtnUser;
        private System.Windows.Forms.RadioButton rbtnEngineer;
        private System.Windows.Forms.TextBox txtNewUser;
        private System.Windows.Forms.Label label4;
        private HYC.WindowsControls.Buttons.HCButton2 btnAddUser;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPwdConfirm;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtPwd;
        private HYC.WindowsControls.Buttons.HCButton2 btnDeleteUser;
        private System.Windows.Forms.ListView listViewUser;
        private System.Windows.Forms.ColumnHeader columnName;
        private System.Windows.Forms.ColumnHeader columnLevel;
    }
}