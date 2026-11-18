namespace TPAssembly.Devices.Plc
{
    partial class PlcSignalCtrl
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgv_InputInfo = new System.Windows.Forms.DataGridView();
            this.dgv_OutputInfo = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Output = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.InputNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsInputed = new System.Windows.Forms.DataGridViewImageColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_InputInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_OutputInfo)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv_InputInfo
            // 
            this.dgv_InputInfo.AllowUserToAddRows = false;
            this.dgv_InputInfo.AllowUserToDeleteRows = false;
            this.dgv_InputInfo.AllowUserToResizeRows = false;
            this.dgv_InputInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dgv_InputInfo.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(244)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_InputInfo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_InputInfo.ColumnHeadersHeight = 25;
            this.dgv_InputInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgv_InputInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.InputNum,
            this.IsInputed});
            this.dgv_InputInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_InputInfo.Location = new System.Drawing.Point(3, 30);
            this.dgv_InputInfo.Margin = new System.Windows.Forms.Padding(3, 0, 3, 4);
            this.dgv_InputInfo.Name = "dgv_InputInfo";
            this.dgv_InputInfo.RowHeadersVisible = false;
            this.dgv_InputInfo.RowTemplate.Height = 23;
            this.dgv_InputInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgv_InputInfo.Size = new System.Drawing.Size(321, 1047);
            this.dgv_InputInfo.TabIndex = 38;
            // 
            // dgv_OutputInfo
            // 
            this.dgv_OutputInfo.AllowUserToAddRows = false;
            this.dgv_OutputInfo.AllowUserToDeleteRows = false;
            this.dgv_OutputInfo.AllowUserToResizeRows = false;
            this.dgv_OutputInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dgv_OutputInfo.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(244)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_OutputInfo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_OutputInfo.ColumnHeadersHeight = 25;
            this.dgv_OutputInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgv_OutputInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.Output});
            this.dgv_OutputInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_OutputInfo.Location = new System.Drawing.Point(330, 30);
            this.dgv_OutputInfo.Margin = new System.Windows.Forms.Padding(3, 0, 3, 4);
            this.dgv_OutputInfo.Name = "dgv_OutputInfo";
            this.dgv_OutputInfo.RowHeadersVisible = false;
            this.dgv_OutputInfo.RowTemplate.Height = 23;
            this.dgv_OutputInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgv_OutputInfo.Size = new System.Drawing.Size(321, 1047);
            this.dgv_OutputInfo.TabIndex = 38;
            this.dgv_OutputInfo.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_OutputInfo_CellContentClick);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn1.HeaderText = "输出名称";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // Output
            // 
            this.Output.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Output.HeaderText = "输出";
            this.Output.Name = "Output";
            this.Output.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Output.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Output.Width = 57;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.dgv_InputInfo, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.dgv_OutputInfo, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(654, 1081);
            this.tableLayoutPanel1.TabIndex = 39;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(151)))), ((int)(((byte)(234)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 4);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(321, 26);
            this.label1.TabIndex = 39;
            this.label1.Text = "输入信号";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(151)))), ((int)(((byte)(234)))));
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(330, 4);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(321, 26);
            this.label2.TabIndex = 39;
            this.label2.Text = "输出信号";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // InputNum
            // 
            this.InputNum.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.InputNum.HeaderText = "输入名称";
            this.InputNum.Name = "InputNum";
            this.InputNum.ReadOnly = true;
            // 
            // IsInputed
            // 
            this.IsInputed.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.IsInputed.HeaderText = "有无信号";
            this.IsInputed.Image = global::CommonLib.Properties.Resources.Gray2;
            this.IsInputed.Name = "IsInputed";
            this.IsInputed.ReadOnly = true;
            this.IsInputed.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.IsInputed.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.IsInputed.Width = 81;
            // 
            // PlcSignalCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "PlcSignalCtrl";
            this.Size = new System.Drawing.Size(654, 1081);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_InputInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_OutputInfo)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dgv_InputInfo;
        private System.Windows.Forms.DataGridView dgv_OutputInfo;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Output;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn InputNum;
        private System.Windows.Forms.DataGridViewImageColumn IsInputed;
    }
}
