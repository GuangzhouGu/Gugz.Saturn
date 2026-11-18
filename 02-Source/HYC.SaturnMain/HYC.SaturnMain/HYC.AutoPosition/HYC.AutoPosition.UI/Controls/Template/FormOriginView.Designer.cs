namespace HYC.AutoPosition.UI.Controls
{
    partial class FormOriginView
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
            this.pnlOriginView = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // pnlOriginView
            // 
            this.pnlOriginView.Location = new System.Drawing.Point(0, 0);
            this.pnlOriginView.Name = "pnlOriginView";
            this.pnlOriginView.Size = new System.Drawing.Size(806, 576);
            this.pnlOriginView.TabIndex = 0;
            // 
            // FormOriginView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(835, 575);
            this.Controls.Add(this.pnlOriginView);
            this.Name = "FormOriginView";
            this.Text = "FormOriginView";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlOriginView;
    }
}