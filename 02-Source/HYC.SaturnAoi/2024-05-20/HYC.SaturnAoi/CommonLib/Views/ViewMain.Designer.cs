namespace CommonLib.Views
{
    partial class ViewMain
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
            this.tlpButtons = new System.Windows.Forms.TableLayoutPanel();
            this.panel13 = new System.Windows.Forms.Panel();
            this.ViewPanel = new System.Windows.Forms.Panel();
            this.lblEqpStatus = new System.Windows.Forms.Label();
            this.statusCtrl1 = new CommonLib.UserCtrl.StatusCtrl();
            this.userManageCtrl1 = new CommonLib.UserCtrl.UserManageCtrl();
            this.SuspendLayout();
            // 
            // FORM_PNL_TITLE
            // 
            this.FORM_PNL_TITLE.Size = new System.Drawing.Size(1918, 60);
            // 
            // FORM_PNL_BOTTOM
            // 
            this.FORM_PNL_BOTTOM.Location = new System.Drawing.Point(1, 1039);
            this.FORM_PNL_BOTTOM.Size = new System.Drawing.Size(1918, 40);
            // 
            // tlpButtons
            // 
            this.tlpButtons.ColumnCount = 1;
            this.tlpButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpButtons.Location = new System.Drawing.Point(410, 65);
            this.tlpButtons.Name = "tlpButtons";
            this.tlpButtons.RowCount = 1;
            this.tlpButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpButtons.Size = new System.Drawing.Size(1506, 58);
            this.tlpButtons.TabIndex = 5;
            // 
            // panel13
            // 
            this.panel13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(95)))), ((int)(((byte)(137)))));
            this.panel13.Location = new System.Drawing.Point(1, 129);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(1920, 3);
            this.panel13.TabIndex = 670;
            // 
            // ViewPanel
            // 
            this.ViewPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.ViewPanel.Location = new System.Drawing.Point(410, 134);
            this.ViewPanel.Name = "ViewPanel";
            this.ViewPanel.Size = new System.Drawing.Size(1506, 824);
            this.ViewPanel.TabIndex = 671;
            // 
            // lblEqpStatus
            // 
            this.lblEqpStatus.BackColor = System.Drawing.Color.Tomato;
            this.lblEqpStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblEqpStatus.Font = new System.Drawing.Font("微软雅黑", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblEqpStatus.ForeColor = System.Drawing.Color.White;
            this.lblEqpStatus.Location = new System.Drawing.Point(5, 961);
            this.lblEqpStatus.Name = "lblEqpStatus";
            this.lblEqpStatus.Size = new System.Drawing.Size(402, 75);
            this.lblEqpStatus.TabIndex = 674;
            this.lblEqpStatus.Text = "STOP";
            this.lblEqpStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // statusCtrl1
            // 
            this.statusCtrl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(255)))));
            this.statusCtrl1.Location = new System.Drawing.Point(5, 134);
            this.statusCtrl1.Name = "statusCtrl1";
            this.statusCtrl1.Size = new System.Drawing.Size(402, 528);
            this.statusCtrl1.TabIndex = 4;
            // 
            // userManageCtrl1
            // 
            this.userManageCtrl1.Location = new System.Drawing.Point(5, 64);
            this.userManageCtrl1.Margin = new System.Windows.Forms.Padding(0);
            this.userManageCtrl1.Name = "userManageCtrl1";
            this.userManageCtrl1.Size = new System.Drawing.Size(402, 58);
            this.userManageCtrl1.TabIndex = 3;
            // 
            // ViewMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1920, 1080);
            this.Controls.Add(this.lblEqpStatus);
            this.Controls.Add(this.ViewPanel);
            this.Controls.Add(this.panel13);
            this.Controls.Add(this.tlpButtons);
            this.Controls.Add(this.statusCtrl1);
            this.Controls.Add(this.userManageCtrl1);
            this.Name = "ViewMain";
            this.Text = "ViewMain";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ViewMain_FormClosing);
            this.Controls.SetChildIndex(this.FORM_PNL_TITLE, 0);
            this.Controls.SetChildIndex(this.FORM_PNL_BOTTOM, 0);
            this.Controls.SetChildIndex(this.userManageCtrl1, 0);
            this.Controls.SetChildIndex(this.statusCtrl1, 0);
            this.Controls.SetChildIndex(this.tlpButtons, 0);
            this.Controls.SetChildIndex(this.panel13, 0);
            this.Controls.SetChildIndex(this.ViewPanel, 0);
            this.Controls.SetChildIndex(this.lblEqpStatus, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private UserCtrl.UserManageCtrl userManageCtrl1;
        private UserCtrl.StatusCtrl statusCtrl1;
        private System.Windows.Forms.TableLayoutPanel tlpButtons;
        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.Panel ViewPanel;
        private System.Windows.Forms.Label lblEqpStatus;
    }
}