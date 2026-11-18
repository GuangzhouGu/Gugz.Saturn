namespace HYC.AutoPosition.UI.View
{
    partial class ViewParameter
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
            this.pnlViewHost = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // pnlViewHost
            // 
            this.pnlViewHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlViewHost.Location = new System.Drawing.Point(0, 0);
            this.pnlViewHost.Margin = new System.Windows.Forms.Padding(0);
            this.pnlViewHost.Name = "pnlViewHost";
            this.pnlViewHost.Size = new System.Drawing.Size(1272, 661);
            this.pnlViewHost.TabIndex = 1;
            // 
            // ViewParameter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1272, 661);
            this.Controls.Add(this.pnlViewHost);
            this.Name = "ViewParameter";
            this.Text = "ViewParameter";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pnlViewHost;
    }
}