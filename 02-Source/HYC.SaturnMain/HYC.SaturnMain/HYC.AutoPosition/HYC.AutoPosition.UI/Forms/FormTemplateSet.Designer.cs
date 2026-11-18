namespace HYC.AutoPosition.UI.Forms
{
    partial class FormTemplateSet
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
            this.pictImage = new System.Windows.Forms.PictureBox();
            this.btnSetAsLeft = new System.Windows.Forms.Button();
            this.btnSetAsRight = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictImage)).BeginInit();
            this.SuspendLayout();
            // 
            // pictImage
            // 
            this.pictImage.BackColor = System.Drawing.Color.LightGray;
            this.pictImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictImage.Location = new System.Drawing.Point(108, 12);
            this.pictImage.Name = "pictImage";
            this.pictImage.Size = new System.Drawing.Size(218, 223);
            this.pictImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictImage.TabIndex = 0;
            this.pictImage.TabStop = false;
            // 
            // btnSetAsLeft
            // 
            this.btnSetAsLeft.Location = new System.Drawing.Point(21, 269);
            this.btnSetAsLeft.Name = "btnSetAsLeft";
            this.btnSetAsLeft.Size = new System.Drawing.Size(117, 30);
            this.btnSetAsLeft.TabIndex = 1;
            this.btnSetAsLeft.Text = "设为左相机模板";
            this.btnSetAsLeft.UseVisualStyleBackColor = true;
            this.btnSetAsLeft.Click += new System.EventHandler(this.btnSetAsLeft_Click);
            // 
            // btnSetAsRight
            // 
            this.btnSetAsRight.Location = new System.Drawing.Point(158, 269);
            this.btnSetAsRight.Name = "btnSetAsRight";
            this.btnSetAsRight.Size = new System.Drawing.Size(117, 30);
            this.btnSetAsRight.TabIndex = 2;
            this.btnSetAsRight.Text = "设为右相机模板";
            this.btnSetAsRight.UseVisualStyleBackColor = true;
            this.btnSetAsRight.Click += new System.EventHandler(this.btnSetAsRight_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(295, 269);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(117, 30);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // FormTemplateSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 323);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSetAsRight);
            this.Controls.Add(this.btnSetAsLeft);
            this.Controls.Add(this.pictImage);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormTemplateSet";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "模板设置";
            this.Load += new System.EventHandler(this.FormTemplateSet_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictImage;
        private System.Windows.Forms.Button btnSetAsLeft;
        private System.Windows.Forms.Button btnSetAsRight;
        private System.Windows.Forms.Button btnClose;
    }
}