
namespace HYC.SaturnAoi.Controls
{
    partial class UcMultiAngle
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
            this.btnTest = new HYC.WindowsControls.HTButton();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Lumi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.X = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Y = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.U = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.V = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cct = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Exposure = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Noise = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.htButton1 = new HYC.WindowsControls.HTButton();
            this.btnGetAngleSelect = new HYC.WindowsControls.HTButton();
            this.txtAngleSelect = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnTest
            // 
            this.btnTest.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTest.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.btnTest.Location = new System.Drawing.Point(7, 4);
            this.btnTest.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(100, 28);
            this.btnTest.TabIndex = 147;
            this.btnTest.Text = "数据测试";
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 17);
            this.label1.TabIndex = 149;
            this.label1.Text = "测试数据";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Lumi,
            this.X,
            this.Y,
            this.U,
            this.V,
            this.Cct,
            this.Exposure,
            this.Noise});
            this.dataGridView1.Location = new System.Drawing.Point(7, 130);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(803, 440);
            this.dataGridView1.TabIndex = 150;
            // 
            // Lumi
            // 
            this.Lumi.HeaderText = "Lumi";
            this.Lumi.Name = "Lumi";
            // 
            // X
            // 
            this.X.HeaderText = "X";
            this.X.Name = "X";
            // 
            // Y
            // 
            this.Y.HeaderText = "Y";
            this.Y.Name = "Y";
            // 
            // U
            // 
            this.U.HeaderText = "U";
            this.U.Name = "U";
            // 
            // V
            // 
            this.V.HeaderText = "V";
            this.V.Name = "V";
            // 
            // Cct
            // 
            this.Cct.HeaderText = "Cct";
            this.Cct.Name = "Cct";
            // 
            // Exposure
            // 
            this.Exposure.HeaderText = "Exposure";
            this.Exposure.Name = "Exposure";
            // 
            // Noise
            // 
            this.Noise.HeaderText = "Noise";
            this.Noise.Name = "Noise";
            // 
            // htButton1
            // 
            this.htButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.htButton1.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.htButton1.Location = new System.Drawing.Point(118, 4);
            this.htButton1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.htButton1.Name = "htButton1";
            this.htButton1.Size = new System.Drawing.Size(100, 28);
            this.htButton1.TabIndex = 147;
            this.htButton1.Text = "流程测试";
            this.htButton1.Click += new System.EventHandler(this.htButton1_Click);
            // 
            // btnGetAngleSelect
            // 
            this.btnGetAngleSelect.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGetAngleSelect.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.btnGetAngleSelect.Location = new System.Drawing.Point(229, 4);
            this.btnGetAngleSelect.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnGetAngleSelect.Name = "btnGetAngleSelect";
            this.btnGetAngleSelect.Size = new System.Drawing.Size(100, 28);
            this.btnGetAngleSelect.TabIndex = 147;
            this.btnGetAngleSelect.Text = "获取通道状态";
            this.btnGetAngleSelect.Click += new System.EventHandler(this.btnGetAngleSelect_Click);
            // 
            // txtAngleSelect
            // 
            this.txtAngleSelect.Location = new System.Drawing.Point(73, 52);
            this.txtAngleSelect.Name = "txtAngleSelect";
            this.txtAngleSelect.Size = new System.Drawing.Size(256, 23);
            this.txtAngleSelect.TabIndex = 151;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 17);
            this.label2.TabIndex = 149;
            this.label2.Text = "通道状态：";
            // 
            // UcMultiAngle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtAngleSelect);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnGetAngleSelect);
            this.Controls.Add(this.htButton1);
            this.Controls.Add(this.btnTest);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "UcMultiAngle";
            this.Size = new System.Drawing.Size(813, 576);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private WindowsControls.HTButton btnTest;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Lumi;
        private System.Windows.Forms.DataGridViewTextBoxColumn X;
        private System.Windows.Forms.DataGridViewTextBoxColumn Y;
        private System.Windows.Forms.DataGridViewTextBoxColumn U;
        private System.Windows.Forms.DataGridViewTextBoxColumn V;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cct;
        private System.Windows.Forms.DataGridViewTextBoxColumn Exposure;
        private System.Windows.Forms.DataGridViewTextBoxColumn Noise;
        private WindowsControls.HTButton htButton1;
        private WindowsControls.HTButton btnGetAngleSelect;
        private System.Windows.Forms.TextBox txtAngleSelect;
        private System.Windows.Forms.Label label2;
    }
}
