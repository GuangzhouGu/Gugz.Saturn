namespace HYC.SaturnMain.Devices.Cim
{
    partial class LinkSignalControl
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvUpToL = new System.Windows.Forms.DataGridView();
            this.lblUptoL = new System.Windows.Forms.Label();
            this.lblDownToL = new System.Windows.Forms.Label();
            this.dgvDownToL = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvLToDown = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.dgvLToUp = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.Num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SignalName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LinkSignalSts = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewImageColumn();
            this.ForceOutput = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewImageColumn();
            this.ForceOutput2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUpToL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDownToL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLToDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLToUp)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvUpToL
            // 
            this.dgvUpToL.AllowUserToAddRows = false;
            this.dgvUpToL.AllowUserToDeleteRows = false;
            this.dgvUpToL.AllowUserToResizeRows = false;
            this.dgvUpToL.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvUpToL.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvUpToL.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvUpToL.ColumnHeadersHeight = 25;
            this.dgvUpToL.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvUpToL.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Num,
            this.SignalName,
            this.LinkSignalSts});
            this.dgvUpToL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUpToL.Location = new System.Drawing.Point(3, 31);
            this.dgvUpToL.Name = "dgvUpToL";
            this.dgvUpToL.ReadOnly = true;
            this.dgvUpToL.RowHeadersVisible = false;
            this.dgvUpToL.RowTemplate.Height = 23;
            this.dgvUpToL.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvUpToL.Size = new System.Drawing.Size(269, 741);
            this.dgvUpToL.TabIndex = 38;
            // 
            // lblUptoL
            // 
            this.lblUptoL.BackColor = System.Drawing.Color.LightGray;
            this.lblUptoL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblUptoL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblUptoL.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblUptoL.ForeColor = System.Drawing.Color.Black;
            this.lblUptoL.Location = new System.Drawing.Point(2, 2);
            this.lblUptoL.Margin = new System.Windows.Forms.Padding(2);
            this.lblUptoL.Name = "lblUptoL";
            this.lblUptoL.Size = new System.Drawing.Size(271, 24);
            this.lblUptoL.TabIndex = 39;
            this.lblUptoL.Text = "上游To本机(本机下游)";
            this.lblUptoL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDownToL
            // 
            this.lblDownToL.BackColor = System.Drawing.Color.LightGray;
            this.lblDownToL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDownToL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDownToL.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDownToL.ForeColor = System.Drawing.Color.Black;
            this.lblDownToL.Location = new System.Drawing.Point(277, 2);
            this.lblDownToL.Margin = new System.Windows.Forms.Padding(2);
            this.lblDownToL.Name = "lblDownToL";
            this.lblDownToL.Size = new System.Drawing.Size(271, 24);
            this.lblDownToL.TabIndex = 41;
            this.lblDownToL.Text = "下游To本机(本机上游)";
            this.lblDownToL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvDownToL
            // 
            this.dgvDownToL.AllowUserToAddRows = false;
            this.dgvDownToL.AllowUserToDeleteRows = false;
            this.dgvDownToL.AllowUserToResizeRows = false;
            this.dgvDownToL.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDownToL.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDownToL.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDownToL.ColumnHeadersHeight = 25;
            this.dgvDownToL.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvDownToL.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3});
            this.dgvDownToL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDownToL.Location = new System.Drawing.Point(278, 31);
            this.dgvDownToL.Name = "dgvDownToL";
            this.dgvDownToL.ReadOnly = true;
            this.dgvDownToL.RowHeadersVisible = false;
            this.dgvDownToL.RowTemplate.Height = 23;
            this.dgvDownToL.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvDownToL.Size = new System.Drawing.Size(269, 741);
            this.dgvDownToL.TabIndex = 40;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.LightGray;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(552, 2);
            this.label2.Margin = new System.Windows.Forms.Padding(2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(271, 24);
            this.label2.TabIndex = 43;
            this.label2.Text = "本机To下游(本机上游)";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvLToDown
            // 
            this.dgvLToDown.AllowUserToAddRows = false;
            this.dgvLToDown.AllowUserToDeleteRows = false;
            this.dgvLToDown.AllowUserToResizeRows = false;
            this.dgvLToDown.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLToDown.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLToDown.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvLToDown.ColumnHeadersHeight = 25;
            this.dgvLToDown.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvLToDown.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.ForceOutput});
            this.dgvLToDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLToDown.Location = new System.Drawing.Point(553, 31);
            this.dgvLToDown.Name = "dgvLToDown";
            this.dgvLToDown.RowHeadersVisible = false;
            this.dgvLToDown.RowTemplate.Height = 23;
            this.dgvLToDown.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvLToDown.Size = new System.Drawing.Size(269, 741);
            this.dgvLToDown.TabIndex = 42;
            this.dgvLToDown.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLToDown_CellContentClick);
            this.dgvLToDown.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgvLToDown_CurrentCellDirtyStateChanged);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.LightGray;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(827, 2);
            this.label3.Margin = new System.Windows.Forms.Padding(2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(273, 24);
            this.label3.TabIndex = 45;
            this.label3.Text = "本机To上游(本机下游)";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvLToUp
            // 
            this.dgvLToUp.AllowUserToAddRows = false;
            this.dgvLToUp.AllowUserToDeleteRows = false;
            this.dgvLToUp.AllowUserToResizeRows = false;
            this.dgvLToUp.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLToUp.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLToUp.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvLToUp.ColumnHeadersHeight = 25;
            this.dgvLToUp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvLToUp.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8,
            this.dataGridViewTextBoxColumn9,
            this.ForceOutput2});
            this.dgvLToUp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLToUp.Location = new System.Drawing.Point(828, 31);
            this.dgvLToUp.Name = "dgvLToUp";
            this.dgvLToUp.RowHeadersVisible = false;
            this.dgvLToUp.RowTemplate.Height = 23;
            this.dgvLToUp.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvLToUp.Size = new System.Drawing.Size(271, 741);
            this.dgvLToUp.TabIndex = 44;
            this.dgvLToUp.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLToUp_CellContentClick);
            this.dgvLToUp.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgvLToUp_CurrentCellDirtyStateChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.lblUptoL, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dgvLToUp, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.dgvLToDown, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblDownToL, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.dgvDownToL, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.dgvUpToL, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1102, 775);
            this.tableLayoutPanel1.TabIndex = 46;
            // 
            // Num
            // 
            this.Num.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Num.FillWeight = 35.7868F;
            this.Num.HeaderText = "序号";
            this.Num.Name = "Num";
            this.Num.ReadOnly = true;
            this.Num.Width = 50;
            // 
            // SignalName
            // 
            this.SignalName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.SignalName.FillWeight = 228.4264F;
            this.SignalName.HeaderText = "LinkSignal名称";
            this.SignalName.MinimumWidth = 150;
            this.SignalName.Name = "SignalName";
            this.SignalName.ReadOnly = true;
            // 
            // LinkSignalSts
            // 
            this.LinkSignalSts.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.LinkSignalSts.FillWeight = 35.7868F;
            this.LinkSignalSts.HeaderText = "状态";
            this.LinkSignalSts.Image = global::HYC.SaturnMain.Properties.Resources.Gray2;
            this.LinkSignalSts.Name = "LinkSignalSts";
            this.LinkSignalSts.ReadOnly = true;
            this.LinkSignalSts.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.LinkSignalSts.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.LinkSignalSts.Width = 50;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn1.HeaderText = "序号";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 50;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.HeaderText = "LinkSignal名称";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 110;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn3.HeaderText = "状态";
            this.dataGridViewTextBoxColumn3.Image = global::HYC.SaturnMain.Properties.Resources.Gray2;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewTextBoxColumn3.Width = 50;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn4.HeaderText = "序号";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 50;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn5.FillWeight = 149.6599F;
            this.dataGridViewTextBoxColumn5.HeaderText = "LinkSignal名称";
            this.dataGridViewTextBoxColumn5.MinimumWidth = 120;
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn6.FillWeight = 50.34014F;
            this.dataGridViewTextBoxColumn6.HeaderText = "状态";
            this.dataGridViewTextBoxColumn6.Image = global::HYC.SaturnMain.Properties.Resources.Gray2;
            this.dataGridViewTextBoxColumn6.MinimumWidth = 40;
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewTextBoxColumn6.Width = 40;
            // 
            // ForceOutput
            // 
            this.ForceOutput.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ForceOutput.HeaderText = "强制";
            this.ForceOutput.MinimumWidth = 40;
            this.ForceOutput.Name = "ForceOutput";
            this.ForceOutput.Width = 40;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn7.HeaderText = "序号";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.Width = 50;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn8.HeaderText = "LinkSignal名称";
            this.dataGridViewTextBoxColumn8.MinimumWidth = 120;
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn9.HeaderText = "状态";
            this.dataGridViewTextBoxColumn9.Image = global::HYC.SaturnMain.Properties.Resources.Gray2;
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            this.dataGridViewTextBoxColumn9.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewTextBoxColumn9.Width = 40;
            // 
            // ForceOutput2
            // 
            this.ForceOutput2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ForceOutput2.HeaderText = "强制";
            this.ForceOutput2.MinimumWidth = 40;
            this.ForceOutput2.Name = "ForceOutput2";
            this.ForceOutput2.Width = 40;
            // 
            // LinkSignalControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "LinkSignalControl";
            this.Size = new System.Drawing.Size(1102, 775);
            this.Load += new System.EventHandler(this.LinkSignalControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUpToL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDownToL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLToDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLToUp)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvUpToL;
        private System.Windows.Forms.Label lblUptoL;
        private System.Windows.Forms.Label lblDownToL;
        private System.Windows.Forms.DataGridView dgvDownToL;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvLToDown;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgvLToUp;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Num;
        private System.Windows.Forms.DataGridViewTextBoxColumn SignalName;
        private System.Windows.Forms.DataGridViewImageColumn LinkSignalSts;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ForceOutput;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ForceOutput2;
    }
}
