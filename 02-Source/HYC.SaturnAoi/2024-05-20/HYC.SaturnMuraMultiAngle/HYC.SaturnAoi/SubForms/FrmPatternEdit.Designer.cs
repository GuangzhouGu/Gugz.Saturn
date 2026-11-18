
namespace HYC.SaturnAoi.SubForms
{
    partial class FrmPatternEdit
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
            this.dgvPatternEdit = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_add = new HYC.WindowsControls.Buttons.HCButton2();
            this.btn_delete = new HYC.WindowsControls.Buttons.HCButton2();
            this.dataGridViewTextBoxColumn42 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn43 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn44 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPatternEdit)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvPatternEdit
            // 
            this.dgvPatternEdit.AllowUserToAddRows = false;
            this.dgvPatternEdit.AllowUserToDeleteRows = false;
            this.dgvPatternEdit.AllowUserToResizeColumns = false;
            this.dgvPatternEdit.AllowUserToResizeRows = false;
            this.dgvPatternEdit.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPatternEdit.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(244)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPatternEdit.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPatternEdit.ColumnHeadersHeight = 25;
            this.dgvPatternEdit.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvPatternEdit.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn42,
            this.dataGridViewTextBoxColumn43,
            this.dataGridViewTextBoxColumn44,
            this.Column1,
            this.Column2});
            this.tableLayoutPanel1.SetColumnSpan(this.dgvPatternEdit, 2);
            this.dgvPatternEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPatternEdit.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvPatternEdit.Location = new System.Drawing.Point(3, 3);
            this.dgvPatternEdit.Name = "dgvPatternEdit";
            this.dgvPatternEdit.RowHeadersVisible = false;
            this.dgvPatternEdit.RowTemplate.Height = 23;
            this.dgvPatternEdit.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvPatternEdit.Size = new System.Drawing.Size(1094, 658);
            this.dgvPatternEdit.TabIndex = 65;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.dgvPatternEdit, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btn_add, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btn_delete, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1100, 704);
            this.tableLayoutPanel1.TabIndex = 69;
            // 
            // btn_add
            // 
            this.btn_add.BackColor = System.Drawing.Color.Snow;
            this.btn_add.CustomerInformation = null;
            this.btn_add.Dock = System.Windows.Forms.DockStyle.Left;
            this.btn_add.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_add.ForeColor = System.Drawing.Color.Black;
            this.btn_add.Location = new System.Drawing.Point(5, 667);
            this.btn_add.Margin = new System.Windows.Forms.Padding(5, 3, 3, 5);
            this.btn_add.Name = "btn_add";
            this.btn_add.OriginalColor = System.Drawing.Color.White;
            this.btn_add.Size = new System.Drawing.Size(117, 32);
            this.btn_add.TabIndex = 66;
            this.btn_add.Text = "添加行";
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // btn_delete
            // 
            this.btn_delete.BackColor = System.Drawing.Color.Snow;
            this.btn_delete.CustomerInformation = null;
            this.btn_delete.Dock = System.Windows.Forms.DockStyle.Left;
            this.btn_delete.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_delete.ForeColor = System.Drawing.Color.Black;
            this.btn_delete.Location = new System.Drawing.Point(135, 667);
            this.btn_delete.Margin = new System.Windows.Forms.Padding(5, 3, 3, 5);
            this.btn_delete.Name = "btn_delete";
            this.btn_delete.OriginalColor = System.Drawing.Color.White;
            this.btn_delete.Size = new System.Drawing.Size(117, 32);
            this.btn_delete.TabIndex = 68;
            this.btn_delete.Text = "删除行";
            this.btn_delete.Click += new System.EventHandler(this.btn_delete_Click);
            // 
            // dataGridViewTextBoxColumn42
            // 
            this.dataGridViewTextBoxColumn42.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn42.HeaderText = "画面序号";
            this.dataGridViewTextBoxColumn42.MinimumWidth = 150;
            this.dataGridViewTextBoxColumn42.Name = "dataGridViewTextBoxColumn42";
            // 
            // dataGridViewTextBoxColumn43
            // 
            this.dataGridViewTextBoxColumn43.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn43.HeaderText = "画面名称";
            this.dataGridViewTextBoxColumn43.Name = "dataGridViewTextBoxColumn43";
            this.dataGridViewTextBoxColumn43.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn43.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn44
            // 
            this.dataGridViewTextBoxColumn44.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn44.HeaderText = "稳定时间(ms)";
            this.dataGridViewTextBoxColumn44.Name = "dataGridViewTextBoxColumn44";
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.HeaderText = "启用";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column2.HeaderText = "检查机控制";
            this.Column2.Items.AddRange(new object[] {
            "PowerOn",
            "SwitchPattern",
            "PowerOn+SwitchPattern",
            "NoControl"});
            this.Column2.Name = "Column2";
            // 
            // FrmPatternEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 704);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FrmPatternEdit";
            this.Text = "FrmMultiAngleDetectPatternEdit";
            this.Load += new System.EventHandler(this.FrmMultiAngleDetectPatternEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPatternEdit)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvPatternEdit;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private WindowsControls.Buttons.HCButton2 btn_delete;
        private WindowsControls.Buttons.HCButton2 btn_add;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn42;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn43;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn44;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column2;
    }
}