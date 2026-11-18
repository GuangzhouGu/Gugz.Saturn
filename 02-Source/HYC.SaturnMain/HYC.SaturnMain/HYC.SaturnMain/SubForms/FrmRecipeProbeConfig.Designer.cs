
namespace HYC.SaturnMain.SubForms
{
    partial class FrmRecipeProbeConfig
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnDelete = new HYC.WindowsControls.Buttons.HCButton2();
            this.btnAdd = new HYC.WindowsControls.Buttons.HCButton2();
            this.label4 = new System.Windows.Forms.Label();
            this.cmxRecipeList = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dgvProbeList = new System.Windows.Forms.DataGridView();
            this.InputNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PPID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmxProbeList = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnLoad = new HYC.WindowsControls.Buttons.HCButton2();
            this.btnRename = new HYC.WindowsControls.Buttons.HCButton2();
            this.tbxProbeName = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProbeList)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnDelete
            // 
            this.btnDelete.CustomerInformation = null;
            this.btnDelete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDelete.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDelete.ForeColor = System.Drawing.Color.Black;
            this.btnDelete.Location = new System.Drawing.Point(503, 2);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.OriginalColor = System.Drawing.Color.White;
            this.btnDelete.Size = new System.Drawing.Size(96, 26);
            this.btnDelete.TabIndex = 67;
            this.btnDelete.Text = "删除";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.CustomerInformation = null;
            this.btnAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAdd.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAdd.ForeColor = System.Drawing.Color.Black;
            this.btnAdd.Location = new System.Drawing.Point(403, 2);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.OriginalColor = System.Drawing.Color.White;
            this.btnAdd.Size = new System.Drawing.Size(94, 26);
            this.btnAdd.TabIndex = 69;
            this.btnAdd.Text = "新增";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(3, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 30);
            this.label4.TabIndex = 70;
            this.label4.Text = "ProbeName：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmxRecipeList
            // 
            this.cmxRecipeList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmxRecipeList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmxRecipeList.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmxRecipeList.FormattingEnabled = true;
            this.cmxRecipeList.Location = new System.Drawing.Point(103, 2);
            this.cmxRecipeList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmxRecipeList.Name = "cmxRecipeList";
            this.cmxRecipeList.Size = new System.Drawing.Size(170, 28);
            this.cmxRecipeList.TabIndex = 72;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(3, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(94, 30);
            this.label7.TabIndex = 71;
            this.label7.Text = "RecipeName：";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dgvProbeList
            // 
            this.dgvProbeList.AllowUserToAddRows = false;
            this.dgvProbeList.AllowUserToDeleteRows = false;
            this.dgvProbeList.AllowUserToResizeRows = false;
            this.dgvProbeList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvProbeList.BackgroundColor = System.Drawing.Color.White;
            this.dgvProbeList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvProbeList.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvProbeList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvProbeList.ColumnHeadersHeight = 25;
            this.dgvProbeList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvProbeList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.InputNum,
            this.PPID,
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5});
            this.tableLayoutPanel1.SetColumnSpan(this.dgvProbeList, 6);
            this.dgvProbeList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvProbeList.Location = new System.Drawing.Point(3, 62);
            this.dgvProbeList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvProbeList.Name = "dgvProbeList";
            this.dgvProbeList.RowHeadersVisible = false;
            this.dgvProbeList.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.dgvProbeList.RowTemplate.Height = 23;
            this.dgvProbeList.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.dgvProbeList.Size = new System.Drawing.Size(940, 352);
            this.dgvProbeList.TabIndex = 74;
            // 
            // InputNum
            // 
            this.InputNum.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.InputNum.FillWeight = 22.84264F;
            this.InputNum.HeaderText = "机种名";
            this.InputNum.MinimumWidth = 150;
            this.InputNum.Name = "InputNum";
            this.InputNum.ReadOnly = true;
            // 
            // PPID
            // 
            this.PPID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.PPID.HeaderText = "PPID";
            this.PPID.MinimumWidth = 100;
            this.PPID.Name = "PPID";
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column1.HeaderText = "Probe名称";
            this.Column1.MinimumWidth = 150;
            this.Column1.Name = "Column1";
            this.Column1.Width = 150;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column2.HeaderText = "RFID";
            this.Column2.MinimumWidth = 100;
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column3.HeaderText = "Block数量";
            this.Column3.Name = "Column3";
            this.Column3.Width = 89;
            // 
            // Column4
            // 
            this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column4.HeaderText = "已压接次数";
            this.Column4.Name = "Column4";
            this.Column4.Width = 93;
            // 
            // Column5
            // 
            this.Column5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column5.HeaderText = "启用时间";
            this.Column5.MinimumWidth = 200;
            this.Column5.Name = "Column5";
            // 
            // cmxProbeList
            // 
            this.cmxProbeList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmxProbeList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmxProbeList.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmxProbeList.FormattingEnabled = true;
            this.cmxProbeList.Location = new System.Drawing.Point(103, 32);
            this.cmxProbeList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmxProbeList.Name = "cmxProbeList";
            this.cmxProbeList.Size = new System.Drawing.Size(170, 28);
            this.cmxProbeList.TabIndex = 72;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 176F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 124F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 344F));
            this.tableLayoutPanel1.Controls.Add(this.btnLoad, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnRename, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dgvProbeList, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnDelete, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmxProbeList, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnAdd, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmxRecipeList, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbxProbeName, 2, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(946, 416);
            this.tableLayoutPanel1.TabIndex = 75;
            // 
            // btnLoad
            // 
            this.btnLoad.CustomerInformation = null;
            this.btnLoad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnLoad.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnLoad.ForeColor = System.Drawing.Color.Black;
            this.btnLoad.Location = new System.Drawing.Point(503, 32);
            this.btnLoad.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.OriginalColor = System.Drawing.Color.White;
            this.btnLoad.Size = new System.Drawing.Size(96, 26);
            this.btnLoad.TabIndex = 77;
            this.btnLoad.Text = "重新加载";
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnRename
            // 
            this.btnRename.CustomerInformation = null;
            this.btnRename.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRename.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRename.ForeColor = System.Drawing.Color.Black;
            this.btnRename.Location = new System.Drawing.Point(403, 32);
            this.btnRename.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnRename.Name = "btnRename";
            this.btnRename.OriginalColor = System.Drawing.Color.White;
            this.btnRename.Size = new System.Drawing.Size(94, 26);
            this.btnRename.TabIndex = 76;
            this.btnRename.Text = "重命名";
            this.btnRename.Click += new System.EventHandler(this.btnRename_Click);
            // 
            // tbxProbeName
            // 
            this.tbxProbeName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbxProbeName.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbxProbeName.Location = new System.Drawing.Point(279, 33);
            this.tbxProbeName.Name = "tbxProbeName";
            this.tbxProbeName.Size = new System.Drawing.Size(118, 26);
            this.tbxProbeName.TabIndex = 75;
            // 
            // FrmRecipeProbeConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(946, 416);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FrmRecipeProbeConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Probe编辑";
            this.Load += new System.EventHandler(this.FrmRecipeProbeConfig_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProbeList)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private WindowsControls.Buttons.HCButton2 btnDelete;
        private WindowsControls.Buttons.HCButton2 btnAdd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmxRecipeList;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView dgvProbeList;
        private System.Windows.Forms.ComboBox cmxProbeList;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private WindowsControls.Buttons.HCButton2 btnRename;
        private System.Windows.Forms.TextBox tbxProbeName;
        private WindowsControls.Buttons.HCButton2 btnLoad;
        private System.Windows.Forms.DataGridViewTextBoxColumn InputNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn PPID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
    }
}