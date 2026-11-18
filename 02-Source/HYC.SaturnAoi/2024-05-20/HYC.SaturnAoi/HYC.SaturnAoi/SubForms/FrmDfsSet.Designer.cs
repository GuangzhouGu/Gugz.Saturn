
namespace HYC.SaturnAoi.SubForms
{
    partial class FrmDfsSet
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
            this.formDFS1 = new DFS.SubForm.FormDFS();
            this.SuspendLayout();
            // 
            // formDFS1
            // 
            this.formDFS1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formDFS1.Location = new System.Drawing.Point(0, 0);
            this.formDFS1.Name = "formDFS1";
            this.formDFS1.Size = new System.Drawing.Size(763, 670);
            this.formDFS1.TabIndex = 0;
            // 
            // FrmDfsSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(763, 670);
            this.Controls.Add(this.formDFS1);
            this.Name = "FrmDfsSet";
            this.Text = "FrmDfsSet";
            this.ResumeLayout(false);

        }

        #endregion

        private DFS.SubForm.FormDFS formDFS1;
    }
}