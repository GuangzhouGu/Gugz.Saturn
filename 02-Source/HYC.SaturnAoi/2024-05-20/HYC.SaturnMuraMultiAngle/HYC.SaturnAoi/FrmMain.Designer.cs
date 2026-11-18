
namespace HYC.SaturnAoi
{
    partial class FrmMain
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlViewHost = new System.Windows.Forms.Panel();
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
            // pnlViewHost
            // 
            this.pnlViewHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlViewHost.Location = new System.Drawing.Point(1, 61);
            this.pnlViewHost.Margin = new System.Windows.Forms.Padding(1);
            this.pnlViewHost.Name = "pnlViewHost";
            this.pnlViewHost.Size = new System.Drawing.Size(1918, 978);
            this.pnlViewHost.TabIndex = 3;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1920, 1080);
            this.Controls.Add(this.pnlViewHost);
            this.Name = "FrmMain";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.Controls.SetChildIndex(this.FORM_PNL_TITLE, 0);
            this.Controls.SetChildIndex(this.FORM_PNL_BOTTOM, 0);
            this.Controls.SetChildIndex(this.pnlViewHost, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlViewHost;
    }
}

