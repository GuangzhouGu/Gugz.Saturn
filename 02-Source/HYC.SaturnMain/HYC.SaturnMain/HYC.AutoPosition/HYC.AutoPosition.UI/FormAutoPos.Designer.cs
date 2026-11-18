namespace HYC.AutoPosition.UI
{
    partial class FormAutoPos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAutoPos));
            this.tbpnlMain = new System.Windows.Forms.TableLayoutPanel();
            this.pnlViewHost = new System.Windows.Forms.Panel();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.btnLogout = new System.Windows.Forms.Button();
            this.lblUser = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnExpandor = new System.Windows.Forms.Button();
            this.btnRecipeSelect = new System.Windows.Forms.Button();
            this.lblRecipe = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlNavigate = new System.Windows.Forms.Panel();
            this.rbtnSetting = new HYC.AutoPosition.UI.Controls.NavigateButton();
            this.rbtnParameter = new HYC.AutoPosition.UI.Controls.NavigateButton();
            this.rbtnManual = new HYC.AutoPosition.UI.Controls.NavigateButton();
            this.rbtnMain = new HYC.AutoPosition.UI.Controls.NavigateButton();
            this.tbpnlMain.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlNavigate.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbpnlMain
            // 
            this.tbpnlMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            resources.ApplyResources(this.tbpnlMain, "tbpnlMain");
            this.tbpnlMain.Controls.Add(this.pnlViewHost, 1, 1);
            this.tbpnlMain.Controls.Add(this.pnlTop, 0, 0);
            this.tbpnlMain.Controls.Add(this.pnlNavigate, 0, 1);
            this.tbpnlMain.Name = "tbpnlMain";
            // 
            // pnlViewHost
            // 
            resources.ApplyResources(this.pnlViewHost, "pnlViewHost");
            this.pnlViewHost.BackColor = System.Drawing.Color.White;
            this.pnlViewHost.Name = "pnlViewHost";
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.Transparent;
            this.tbpnlMain.SetColumnSpan(this.pnlTop, 2);
            this.pnlTop.Controls.Add(this.btnLogout);
            this.pnlTop.Controls.Add(this.lblUser);
            this.pnlTop.Controls.Add(this.label3);
            this.pnlTop.Controls.Add(this.panel1);
            this.pnlTop.Controls.Add(this.btnRecipeSelect);
            this.pnlTop.Controls.Add(this.lblRecipe);
            this.pnlTop.Controls.Add(this.label1);
            resources.ApplyResources(this.pnlTop, "pnlTop");
            this.pnlTop.Name = "pnlTop";
            // 
            // btnLogout
            // 
            resources.ApplyResources(this.btnLogout, "btnLogout");
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // lblUser
            // 
            this.lblUser.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.lblUser, "lblUser");
            this.lblUser.Name = "lblUser";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Name = "label3";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnExpandor);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // btnExpandor
            // 
            this.btnExpandor.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnExpandor.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.btnExpandor, "btnExpandor");
            this.btnExpandor.Name = "btnExpandor";
            this.btnExpandor.UseVisualStyleBackColor = false;
          //  this.btnExpandor.Click += new System.EventHandler(this.btnExpandor_Click);
            // 
            // btnRecipeSelect
            // 
            resources.ApplyResources(this.btnRecipeSelect, "btnRecipeSelect");
            this.btnRecipeSelect.Name = "btnRecipeSelect";
            this.btnRecipeSelect.UseVisualStyleBackColor = true;
            this.btnRecipeSelect.Click += new System.EventHandler(this.btnRecipeSelect_Click);
            // 
            // lblRecipe
            // 
            this.lblRecipe.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblRecipe.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.lblRecipe, "lblRecipe");
            this.lblRecipe.Name = "lblRecipe";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Name = "label1";
            // 
            // pnlNavigate
            // 
            this.pnlNavigate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.pnlNavigate.Controls.Add(this.rbtnSetting);
            this.pnlNavigate.Controls.Add(this.rbtnParameter);
            this.pnlNavigate.Controls.Add(this.rbtnManual);
            this.pnlNavigate.Controls.Add(this.rbtnMain);
            resources.ApplyResources(this.pnlNavigate, "pnlNavigate");
            this.pnlNavigate.Name = "pnlNavigate";
            // 
            // rbtnSetting
            // 
            resources.ApplyResources(this.rbtnSetting, "rbtnSetting");
            this.rbtnSetting.BackColor = System.Drawing.Color.Transparent;
            this.rbtnSetting.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.rbtnSetting.FlatAppearance.BorderSize = 0;
            this.rbtnSetting.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.rbtnSetting.ForeColor = System.Drawing.Color.Black;
            this.rbtnSetting.Image = global::HYC.AutoPosition.UI.Properties.Resources.setting;
            this.rbtnSetting.Name = "rbtnSetting";
            this.rbtnSetting.Tag = "ViewSetting";
            this.rbtnSetting.UseVisualStyleBackColor = false;
            // 
            // rbtnParameter
            // 
            resources.ApplyResources(this.rbtnParameter, "rbtnParameter");
            this.rbtnParameter.BackColor = System.Drawing.Color.Transparent;
            this.rbtnParameter.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.rbtnParameter.FlatAppearance.BorderSize = 0;
            this.rbtnParameter.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.rbtnParameter.ForeColor = System.Drawing.Color.Black;
            this.rbtnParameter.Image = global::HYC.AutoPosition.UI.Properties.Resources.filter;
            this.rbtnParameter.Name = "rbtnParameter";
            this.rbtnParameter.Tag = "ViewParameter";
            this.rbtnParameter.UseVisualStyleBackColor = false;
            // 
            // rbtnManual
            // 
            resources.ApplyResources(this.rbtnManual, "rbtnManual");
            this.rbtnManual.BackColor = System.Drawing.Color.Transparent;
            this.rbtnManual.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.rbtnManual.FlatAppearance.BorderSize = 0;
            this.rbtnManual.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.rbtnManual.ForeColor = System.Drawing.Color.Black;
            this.rbtnManual.Image = global::HYC.AutoPosition.UI.Properties.Resources.edit;
            this.rbtnManual.Name = "rbtnManual";
            this.rbtnManual.Tag = "ViewManual";
            this.rbtnManual.UseVisualStyleBackColor = false;
            // 
            // rbtnMain
            // 
            resources.ApplyResources(this.rbtnMain, "rbtnMain");
            this.rbtnMain.BackColor = System.Drawing.Color.Transparent;
            this.rbtnMain.Checked = true;
            this.rbtnMain.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.rbtnMain.FlatAppearance.BorderSize = 0;
            this.rbtnMain.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.rbtnMain.ForeColor = System.Drawing.Color.Black;
            this.rbtnMain.Name = "rbtnMain";
            this.rbtnMain.TabStop = true;
            this.rbtnMain.Tag = "ViewMain";
            this.rbtnMain.UseVisualStyleBackColor = false;
            // 
            // FormAutoPos
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.Controls.Add(this.tbpnlMain);
            this.Name = "FormAutoPos";
            this.Load += new System.EventHandler(this.FormAutoPos_Load);
            this.tbpnlMain.ResumeLayout(false);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.pnlNavigate.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tbpnlMain;
        private System.Windows.Forms.Panel pnlViewHost;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Panel pnlNavigate;
        private Controls.NavigateButton rbtnParameter;
        private Controls.NavigateButton rbtnManual;
        private Controls.NavigateButton rbtnMain;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblRecipe;
        private System.Windows.Forms.Button btnRecipeSelect;
        private Controls.NavigateButton rbtnSetting;
        private System.Windows.Forms.Button btnExpandor;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Label label3;
    }
}