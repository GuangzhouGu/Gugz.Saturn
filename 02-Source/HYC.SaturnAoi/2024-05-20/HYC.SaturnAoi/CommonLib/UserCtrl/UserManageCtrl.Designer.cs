namespace CommonLib.UserCtrl
{
    partial class UserManageCtrl
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lbl_CurrentUser = new System.Windows.Forms.Label();
            this.lbl_recipe = new System.Windows.Forms.Label();
            this.btnLogin = new HYC.WindowsControls.Buttons.HCButton2();
            this.btnRecipe = new HYC.WindowsControls.Buttons.HCButton2();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.36585F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 64.63415F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 73F));
            this.tableLayoutPanel1.Controls.Add(this.label8, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbl_CurrentUser, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbl_recipe, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnLogin, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnRecipe, 2, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(402, 58);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(121)))), ((int)(((byte)(190)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(2, 31);
            this.label8.Margin = new System.Windows.Forms.Padding(2);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(112, 25);
            this.label8.TabIndex = 677;
            this.label8.Text = "机种";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(121)))), ((int)(((byte)(190)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(2, 2);
            this.label7.Margin = new System.Windows.Forms.Padding(2);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(112, 25);
            this.label7.TabIndex = 676;
            this.label7.Text = "用户";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_CurrentUser
            // 
            this.lbl_CurrentUser.BackColor = System.Drawing.Color.White;
            this.lbl_CurrentUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_CurrentUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_CurrentUser.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_CurrentUser.Location = new System.Drawing.Point(118, 2);
            this.lbl_CurrentUser.Margin = new System.Windows.Forms.Padding(2);
            this.lbl_CurrentUser.Name = "lbl_CurrentUser";
            this.lbl_CurrentUser.Size = new System.Drawing.Size(208, 25);
            this.lbl_CurrentUser.TabIndex = 678;
            this.lbl_CurrentUser.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_recipe
            // 
            this.lbl_recipe.BackColor = System.Drawing.Color.White;
            this.lbl_recipe.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_recipe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_recipe.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_recipe.Location = new System.Drawing.Point(118, 31);
            this.lbl_recipe.Margin = new System.Windows.Forms.Padding(2);
            this.lbl_recipe.Name = "lbl_recipe";
            this.lbl_recipe.Size = new System.Drawing.Size(208, 25);
            this.lbl_recipe.TabIndex = 679;
            this.lbl_recipe.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnLogin
            // 
            this.btnLogin.CustomerInformation = null;
            this.btnLogin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnLogin.ForeColor = System.Drawing.Color.Black;
            this.btnLogin.Location = new System.Drawing.Point(330, 2);
            this.btnLogin.Margin = new System.Windows.Forms.Padding(2);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.OriginalColor = System.Drawing.Color.White;
            this.btnLogin.Size = new System.Drawing.Size(70, 25);
            this.btnLogin.TabIndex = 683;
            this.btnLogin.Text = "未登录";
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // btnRecipe
            // 
            this.btnRecipe.CustomerInformation = null;
            this.btnRecipe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRecipe.ForeColor = System.Drawing.Color.Black;
            this.btnRecipe.Location = new System.Drawing.Point(330, 31);
            this.btnRecipe.Margin = new System.Windows.Forms.Padding(2);
            this.btnRecipe.Name = "btnRecipe";
            this.btnRecipe.OriginalColor = System.Drawing.Color.White;
            this.btnRecipe.Size = new System.Drawing.Size(70, 25);
            this.btnRecipe.TabIndex = 684;
            this.btnRecipe.Text = "机种选择";
            this.btnRecipe.Click += new System.EventHandler(this.btnRecipe_Click);
            // 
            // UserManageCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "UserManageCtrl";
            this.Size = new System.Drawing.Size(402, 58);
            this.Load += new System.EventHandler(this.UserManageCtrl_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lbl_CurrentUser;
        private System.Windows.Forms.Label lbl_recipe;
        private HYC.WindowsControls.Buttons.HCButton2 btnLogin;
        private HYC.WindowsControls.Buttons.HCButton2 btnRecipe;
    }
}
