
namespace HYC.SaturnMain.SubForms
{
    partial class FrmAutoPos
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
            this.pnlHost = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // pnlHost
            // 
            this.pnlHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHost.Location = new System.Drawing.Point(0, 0);
            this.pnlHost.Margin = new System.Windows.Forms.Padding(1);
            this.pnlHost.Name = "pnlHost";
            this.pnlHost.Size = new System.Drawing.Size(800, 450);
            this.pnlHost.TabIndex = 1;
            // 
            // FrmAutoPos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pnlHost);
            this.Name = "FrmAutoPos";
            this.Text = "FrmAutoPos";
            this.Load += new System.EventHandler(this.FrmAutoPos_Load);
            this.Controls.SetChildIndex(this.pnlHost, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHost;
    }
}