
namespace HYC.SaturnDFS
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
            this.messageCtrl1 = new HYC.HTPCBase.Extension.UserCtrl.MessageCtrl();
            this.statusCtrl1 = new HYC.HTPCBase.Extension.UserCtrl.StatusCtrl();
            this.formDFS1 = new DFS.SubForm.FormDFS();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.MsgBox = new HYC.WindowsControls.HCListBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // messageCtrl1
            // 
            this.messageCtrl1.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.messageCtrl1.Location = new System.Drawing.Point(882, 217);
            this.messageCtrl1.Margin = new System.Windows.Forms.Padding(0);
            this.messageCtrl1.Name = "messageCtrl1";
            this.messageCtrl1.Size = new System.Drawing.Size(282, 498);
            this.messageCtrl1.TabIndex = 0;
            // 
            // statusCtrl1
            // 
            this.statusCtrl1.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.statusCtrl1.Location = new System.Drawing.Point(882, 5);
            this.statusCtrl1.Margin = new System.Windows.Forms.Padding(0);
            this.statusCtrl1.Name = "statusCtrl1";
            this.statusCtrl1.Size = new System.Drawing.Size(282, 208);
            this.statusCtrl1.TabIndex = 1;
            // 
            // formDFS1
            // 
            this.formDFS1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.formDFS1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formDFS1.Location = new System.Drawing.Point(3, 3);
            this.formDFS1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.formDFS1.Name = "formDFS1";
            this.formDFS1.Size = new System.Drawing.Size(862, 671);
            this.formDFS1.TabIndex = 3;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl1.Location = new System.Drawing.Point(3, 5);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(876, 710);
            this.tabControl1.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.MsgBox);
            this.tabPage1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(868, 677);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "上传信息";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // MsgBox
            // 
            this.MsgBox.BackColor = System.Drawing.Color.White;
            this.MsgBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.MsgBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MsgBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.MsgBox.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.MsgBox.FormattingEnabled = true;
            this.MsgBox.HorizontalScrollbar = true;
            this.MsgBox.IsShowMillisecond = false;
            this.MsgBox.ItemHeight = 17;
            this.MsgBox.Location = new System.Drawing.Point(3, 3);
            this.MsgBox.Margin = new System.Windows.Forms.Padding(1);
            this.MsgBox.Name = "MsgBox";
            this.MsgBox.SelectionBackColor = System.Drawing.Color.SkyBlue;
            this.MsgBox.Size = new System.Drawing.Size(862, 671);
            this.MsgBox.TabIndex = 5;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.formDFS1);
            this.tabPage2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(868, 677);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "DFS设置";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(791, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(84, 28);
            this.button1.TabIndex = 5;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1169, 718);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusCtrl1);
            this.Controls.Add(this.messageCtrl1);
            this.Name = "FrmMain";
            this.Text = "DFS上传";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private HTPCBase.Extension.UserCtrl.MessageCtrl messageCtrl1;
        private HTPCBase.Extension.UserCtrl.StatusCtrl statusCtrl1;
        private DFS.SubForm.FormDFS formDFS1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private WindowsControls.HCListBox MsgBox;
        private System.Windows.Forms.Button button1;
    }
}

