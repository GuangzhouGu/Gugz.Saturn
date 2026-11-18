namespace CommonLib.UserCtrl
{
    partial class DataGridCtrl
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
            this.dgv = new System.Windows.Forms.DataGridView();
            this.btn_add = new HYC.WindowsControls.Buttons.HCButton2();
            this.btn_delete = new HYC.WindowsControls.Buttons.HCButton2();
            this.btnClear = new HYC.WindowsControls.Buttons.HCButton2();
            this.btn_savePos = new HYC.WindowsControls.Buttons.HCButton2();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnUp = new HYC.WindowsControls.Buttons.HCButton2();
            this.btnDown = new HYC.WindowsControls.Buttons.HCButton2();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AllowUserToResizeRows = false;
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgv.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(244)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv.ColumnHeadersHeight = 25;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Location = new System.Drawing.Point(3, 3);
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersVisible = false;
            this.tableLayoutPanel1.SetRowSpan(this.dgv, 7);
            this.dgv.RowTemplate.Height = 23;
            this.dgv.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgv.Size = new System.Drawing.Size(366, 372);
            this.dgv.TabIndex = 41;
            this.dgv.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgv_DataError);
            // 
            // btn_add
            // 
            this.btn_add.BackColor = System.Drawing.Color.Snow;
            this.btn_add.CustomerInformation = null;
            this.btn_add.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_add.ForeColor = System.Drawing.Color.Black;
            this.btn_add.Location = new System.Drawing.Point(375, 3);
            this.btn_add.Name = "btn_add";
            this.btn_add.OriginalColor = System.Drawing.Color.White;
            this.btn_add.Size = new System.Drawing.Size(80, 30);
            this.btn_add.TabIndex = 45;
            this.btn_add.Text = "添加行";
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // btn_delete
            // 
            this.btn_delete.BackColor = System.Drawing.Color.Snow;
            this.btn_delete.CustomerInformation = null;
            this.btn_delete.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_delete.ForeColor = System.Drawing.Color.Black;
            this.btn_delete.Location = new System.Drawing.Point(375, 48);
            this.btn_delete.Name = "btn_delete";
            this.btn_delete.OriginalColor = System.Drawing.Color.White;
            this.btn_delete.Size = new System.Drawing.Size(80, 30);
            this.btn_delete.TabIndex = 48;
            this.btn_delete.Text = "删除行";
            this.btn_delete.Click += new System.EventHandler(this.btn_delete_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.Snow;
            this.btnClear.CustomerInformation = null;
            this.btnClear.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClear.ForeColor = System.Drawing.Color.Black;
            this.btnClear.Location = new System.Drawing.Point(375, 183);
            this.btnClear.Name = "btnClear";
            this.btnClear.OriginalColor = System.Drawing.Color.White;
            this.btnClear.Size = new System.Drawing.Size(80, 30);
            this.btnClear.TabIndex = 46;
            this.btnClear.Text = "清空";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btn_savePos
            // 
            this.btn_savePos.BackColor = System.Drawing.Color.Snow;
            this.btn_savePos.CustomerInformation = null;
            this.btn_savePos.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_savePos.ForeColor = System.Drawing.Color.Black;
            this.btn_savePos.Location = new System.Drawing.Point(375, 228);
            this.btn_savePos.Name = "btn_savePos";
            this.btn_savePos.OriginalColor = System.Drawing.Color.White;
            this.btn_savePos.Size = new System.Drawing.Size(80, 30);
            this.btn_savePos.TabIndex = 47;
            this.btn_savePos.Text = "保存";
            this.btn_savePos.Click += new System.EventHandler(this.btn_savePos_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80.91106F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.08894F));
            this.tableLayoutPanel1.Controls.Add(this.btnUp, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.dgv, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btn_delete, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.btn_add, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btn_savePos, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.btnClear, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.btnDown, 1, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 28F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(461, 378);
            this.tableLayoutPanel1.TabIndex = 49;
            // 
            // btnUp
            // 
            this.btnUp.BackColor = System.Drawing.Color.Snow;
            this.btnUp.CustomerInformation = null;
            this.btnUp.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnUp.ForeColor = System.Drawing.Color.Black;
            this.btnUp.Location = new System.Drawing.Point(375, 93);
            this.btnUp.Name = "btnUp";
            this.btnUp.OriginalColor = System.Drawing.Color.White;
            this.btnUp.Size = new System.Drawing.Size(80, 30);
            this.btnUp.TabIndex = 49;
            this.btnUp.Text = "上移";
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnDown
            // 
            this.btnDown.BackColor = System.Drawing.Color.Snow;
            this.btnDown.CustomerInformation = null;
            this.btnDown.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDown.ForeColor = System.Drawing.Color.Black;
            this.btnDown.Location = new System.Drawing.Point(375, 138);
            this.btnDown.Name = "btnDown";
            this.btnDown.OriginalColor = System.Drawing.Color.White;
            this.btnDown.Size = new System.Drawing.Size(80, 30);
            this.btnDown.TabIndex = 49;
            this.btnDown.Text = "下移";
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // DataGridCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "DataGridCtrl";
            this.Size = new System.Drawing.Size(461, 378);
            this.Load += new System.EventHandler(this.DataGridCtrl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private HYC.WindowsControls.Buttons.HCButton2 btn_savePos;
        private HYC.WindowsControls.Buttons.HCButton2 btnClear;
        private HYC.WindowsControls.Buttons.HCButton2 btn_delete;
        private HYC.WindowsControls.Buttons.HCButton2 btn_add;
        private HYC.WindowsControls.Buttons.HCButton2 btnUp;
        private HYC.WindowsControls.Buttons.HCButton2 btnDown;
    }
}
