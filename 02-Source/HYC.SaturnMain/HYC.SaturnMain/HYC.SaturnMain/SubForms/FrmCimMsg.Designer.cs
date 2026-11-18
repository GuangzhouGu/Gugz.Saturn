
namespace HYC.SaturnMain.SubForms
{
    partial class FrmCimMsg
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
            this.dgvCImMsg = new System.Windows.Forms.DataGridView();
            this.No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReceiveTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CimMsg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Confirm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCImMsg)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvCImMsg
            // 
            this.dgvCImMsg.AllowUserToAddRows = false;
            this.dgvCImMsg.AllowUserToDeleteRows = false;
            this.dgvCImMsg.AllowUserToResizeRows = false;
            this.dgvCImMsg.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvCImMsg.BackgroundColor = System.Drawing.Color.White;
            this.dgvCImMsg.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvCImMsg.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvCImMsg.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCImMsg.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvCImMsg.ColumnHeadersHeight = 25;
            this.dgvCImMsg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvCImMsg.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.No,
            this.ID,
            this.ReceiveTime,
            this.CimMsg,
            this.Confirm});
            this.dgvCImMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCImMsg.Location = new System.Drawing.Point(1, 36);
            this.dgvCImMsg.Name = "dgvCImMsg";
            this.dgvCImMsg.RowHeadersVisible = false;
            this.dgvCImMsg.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.dgvCImMsg.RowTemplate.Height = 23;
            this.dgvCImMsg.Size = new System.Drawing.Size(740, 413);
            this.dgvCImMsg.TabIndex = 40;
            this.dgvCImMsg.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCImMsg_CellContentClick);
            // 
            // No
            // 
            this.No.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.No.FillWeight = 11.58143F;
            this.No.HeaderText = "No";
            this.No.MinimumWidth = 60;
            this.No.Name = "No";
            this.No.ReadOnly = true;
            this.No.Width = 80;
            // 
            // ID
            // 
            this.ID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ID.HeaderText = "ID";
            this.ID.MinimumWidth = 80;
            this.ID.Name = "ID";
            this.ID.Width = 80;
            // 
            // ReceiveTime
            // 
            this.ReceiveTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ReceiveTime.FillWeight = 25.59617F;
            this.ReceiveTime.HeaderText = "ReceiveTime";
            this.ReceiveTime.MinimumWidth = 130;
            this.ReceiveTime.Name = "ReceiveTime";
            this.ReceiveTime.ReadOnly = true;
            // 
            // CimMsg
            // 
            this.CimMsg.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.CimMsg.HeaderText = "CimMessage";
            this.CimMsg.MinimumWidth = 250;
            this.CimMsg.Name = "CimMsg";
            // 
            // Confirm
            // 
            this.Confirm.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Confirm.FillWeight = 24.96956F;
            this.Confirm.HeaderText = "Confirm";
            this.Confirm.MinimumWidth = 100;
            this.Confirm.Name = "Confirm";
            this.Confirm.ReadOnly = true;
            // 
            // FrmCimMsg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(742, 450);
            this.CloseFormTimeLag = 0;
            this.Controls.Add(this.dgvCImMsg);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "FrmCimMsg";
            this.ShowFormTimelag = 0;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmCimMsg";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmCimMsg_FormClosing);
            this.Controls.SetChildIndex(this.dgvCImMsg, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCImMsg)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvCImMsg;
        private System.Windows.Forms.DataGridViewTextBoxColumn No;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReceiveTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn CimMsg;
        private System.Windows.Forms.DataGridViewTextBoxColumn Confirm;
    }
}