
namespace HYC.SaturnMain.UserControls
{
    partial class GlassMapCtrl
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
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.resetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hideNumToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.x50ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.x100ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.x200ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.x500ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resetToolStripMenuItem,
            this.hideNumToolStripMenuItem,
            this.x50ToolStripMenuItem,
            this.x100ToolStripMenuItem,
            this.x200ToolStripMenuItem,
            this.x500ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(132, 136);
            // 
            // resetToolStripMenuItem
            // 
            this.resetToolStripMenuItem.Name = "resetToolStripMenuItem";
            this.resetToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.resetToolStripMenuItem.Text = "Reset";
            this.resetToolStripMenuItem.Click += new System.EventHandler(this.resetToolStripMenuItem_Click);
            // 
            // hideNumToolStripMenuItem
            // 
            this.hideNumToolStripMenuItem.Name = "hideNumToolStripMenuItem";
            this.hideNumToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.hideNumToolStripMenuItem.Text = "HideNum";
            this.hideNumToolStripMenuItem.Click += new System.EventHandler(this.hideNumToolStripMenuItem_Click);
            // 
            // x50ToolStripMenuItem
            // 
            this.x50ToolStripMenuItem.Name = "x50ToolStripMenuItem";
            this.x50ToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.x50ToolStripMenuItem.Text = "x50";
            this.x50ToolStripMenuItem.Click += new System.EventHandler(this.x50ToolStripMenuItem_Click);
            // 
            // x100ToolStripMenuItem
            // 
            this.x100ToolStripMenuItem.Name = "x100ToolStripMenuItem";
            this.x100ToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.x100ToolStripMenuItem.Text = "x100";
            this.x100ToolStripMenuItem.Click += new System.EventHandler(this.x100ToolStripMenuItem_Click);
            // 
            // x200ToolStripMenuItem
            // 
            this.x200ToolStripMenuItem.Name = "x200ToolStripMenuItem";
            this.x200ToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.x200ToolStripMenuItem.Text = "x200";
            this.x200ToolStripMenuItem.Click += new System.EventHandler(this.x200ToolStripMenuItem_Click);
            // 
            // x500ToolStripMenuItem
            // 
            this.x500ToolStripMenuItem.Name = "x500ToolStripMenuItem";
            this.x500ToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.x500ToolStripMenuItem.Text = "x500";
            this.x500ToolStripMenuItem.Click += new System.EventHandler(this.x500ToolStripMenuItem_Click);
            // 
            // GlassMapCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Name = "GlassMapCtrl";
            this.Size = new System.Drawing.Size(549, 645);
            this.Load += new System.EventHandler(this.GlassMap_Load);
            this.SizeChanged += new System.EventHandler(this.GlassMap_SizeChanged);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.GlassMap_Paint);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem resetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hideNumToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem x50ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem x100ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem x200ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem x500ToolStripMenuItem;
    }
}
