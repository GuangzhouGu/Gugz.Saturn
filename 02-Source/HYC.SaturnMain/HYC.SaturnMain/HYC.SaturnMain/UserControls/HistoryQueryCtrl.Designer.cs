
namespace HYC.SaturnMain.UserControls
{
    partial class HistoryQueryCtrl
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
            this.tbpnlMain = new System.Windows.Forms.TableLayoutPanel();
            this.dataGridViewUnit1 = new HYC.WindowsControls.HTDataGridViewUnit();
            this.tbpnlTop = new System.Windows.Forms.TableLayoutPanel();
            this.btnQuery = new HYC.WindowsControls.HTButton();
            this.panel2 = new HYC.WindowsControls.HTPanel();
            this.cboResult = new HYC.WindowsControls.HTComboBox();
            this.txtPanelID = new HYC.WindowsControls.HTTextBox();
            this.cmxQueryType = new HYC.WindowsControls.HTComboBox();
            this.htLabel3 = new HYC.WindowsControls.HTLabel();
            this.cmxCellIDs = new HYC.WindowsControls.HTComboBox();
            this.htLabel2 = new HYC.WindowsControls.HTLabel();
            this.cmxDetectType = new HYC.WindowsControls.HTComboBox();
            this.htLabel1 = new HYC.WindowsControls.HTLabel();
            this.label4 = new HYC.WindowsControls.HTLabel();
            this.label6 = new HYC.WindowsControls.HTLabel();
            this.panel1 = new HYC.WindowsControls.HTPanel();
            this.dtPickerDate2 = new System.Windows.Forms.DateTimePicker();
            this.dtPickerDate1 = new System.Windows.Forms.DateTimePicker();
            this.label1 = new HYC.WindowsControls.HTLabel();
            this.ckbNow1 = new HYC.WindowsControls.HTCheckBox();
            this.dtPickerTime2 = new System.Windows.Forms.DateTimePicker();
            this.dtPickerTime1 = new System.Windows.Forms.DateTimePicker();
            this.label2 = new HYC.WindowsControls.HTLabel();
            this.tbpnlMain.SuspendLayout();
            this.tbpnlTop.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbpnlMain
            // 
            this.tbpnlMain.ColumnCount = 1;
            this.tbpnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbpnlMain.Controls.Add(this.dataGridViewUnit1, 0, 1);
            this.tbpnlMain.Controls.Add(this.tbpnlTop, 0, 0);
            this.tbpnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbpnlMain.Location = new System.Drawing.Point(0, 0);
            this.tbpnlMain.Name = "tbpnlMain";
            this.tbpnlMain.RowCount = 2;
            this.tbpnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 85F));
            this.tbpnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbpnlMain.Size = new System.Drawing.Size(1068, 705);
            this.tbpnlMain.TabIndex = 0;
            // 
            // dataGridViewUnit1
            // 
            this.dataGridViewUnit1.AutoScroll = true;
            this.dataGridViewUnit1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dataGridViewUnit1.CountPerPage = 50;
            this.dataGridViewUnit1.DataSource = null;
            this.dataGridViewUnit1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewUnit1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataGridViewUnit1.Location = new System.Drawing.Point(3, 89);
            this.dataGridViewUnit1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dataGridViewUnit1.Name = "dataGridViewUnit1";
            this.dataGridViewUnit1.Size = new System.Drawing.Size(1062, 612);
            this.dataGridViewUnit1.TabIndex = 15;
            // 
            // tbpnlTop
            // 
            this.tbpnlTop.ColumnCount = 3;
            this.tbpnlTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tbpnlTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbpnlTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tbpnlTop.Controls.Add(this.btnQuery, 2, 0);
            this.tbpnlTop.Controls.Add(this.panel2, 0, 0);
            this.tbpnlTop.Controls.Add(this.panel1, 0, 0);
            this.tbpnlTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbpnlTop.Location = new System.Drawing.Point(0, 0);
            this.tbpnlTop.Margin = new System.Windows.Forms.Padding(0);
            this.tbpnlTop.Name = "tbpnlTop";
            this.tbpnlTop.RowCount = 1;
            this.tbpnlTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbpnlTop.Size = new System.Drawing.Size(1068, 85);
            this.tbpnlTop.TabIndex = 16;
            // 
            // btnQuery
            // 
            this.btnQuery.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnQuery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnQuery.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnQuery.Location = new System.Drawing.Point(971, 3);
            this.btnQuery.Margin = new System.Windows.Forms.Padding(3, 3, 3, 45);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(94, 37);
            this.btnQuery.Style = HYC.WindowsControls.HTStyle.Blue;
            this.btnQuery.TabIndex = 1;
            this.btnQuery.Text = "查询";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cboResult);
            this.panel2.Controls.Add(this.txtPanelID);
            this.panel2.Controls.Add(this.cmxQueryType);
            this.panel2.Controls.Add(this.htLabel3);
            this.panel2.Controls.Add(this.cmxCellIDs);
            this.panel2.Controls.Add(this.htLabel2);
            this.panel2.Controls.Add(this.cmxDetectType);
            this.panel2.Controls.Add(this.htLabel1);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.panel2.Location = new System.Drawing.Point(360, 4);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(605, 77);
            this.panel2.TabIndex = 14;
            this.panel2.Text = null;
            // 
            // cboResult
            // 
            this.cboResult.DropDownStyle = HYC.WindowsControls.HTDropDownStyle.DropDownList;
            this.cboResult.FillColor = System.Drawing.Color.White;
            this.cboResult.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboResult.FormattingEnabled = true;
            this.cboResult.Items.AddRange(new object[] {
            "Total",
            "OK",
            "NG"});
            this.cboResult.Location = new System.Drawing.Point(65, 42);
            this.cboResult.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboResult.MinimumSize = new System.Drawing.Size(63, 0);
            this.cboResult.Name = "cboResult";
            this.cboResult.Padding = new System.Windows.Forms.Padding(0, 0, 30, 0);
            this.cboResult.Size = new System.Drawing.Size(104, 25);
            this.cboResult.TabIndex = 9;
            this.cboResult.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPanelID
            // 
            this.txtPanelID.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtPanelID.FillColor = System.Drawing.Color.White;
            this.txtPanelID.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPanelID.Location = new System.Drawing.Point(65, 12);
            this.txtPanelID.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtPanelID.Maximum = 2147483647D;
            this.txtPanelID.Minimum = -2147483648D;
            this.txtPanelID.Name = "txtPanelID";
            this.txtPanelID.Padding = new System.Windows.Forms.Padding(5);
            this.txtPanelID.Size = new System.Drawing.Size(175, 23);
            this.txtPanelID.TabIndex = 5;
            // 
            // cmxQueryType
            // 
            this.cmxQueryType.DropDownStyle = HYC.WindowsControls.HTDropDownStyle.DropDownList;
            this.cmxQueryType.FillColor = System.Drawing.Color.White;
            this.cmxQueryType.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmxQueryType.FormattingEnabled = true;
            this.cmxQueryType.Items.AddRange(new object[] {
            "Total",
            "NGDetail"});
            this.cmxQueryType.Location = new System.Drawing.Point(447, 42);
            this.cmxQueryType.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmxQueryType.MinimumSize = new System.Drawing.Size(63, 0);
            this.cmxQueryType.Name = "cmxQueryType";
            this.cmxQueryType.Padding = new System.Windows.Forms.Padding(0, 0, 30, 0);
            this.cmxQueryType.Size = new System.Drawing.Size(120, 25);
            this.cmxQueryType.TabIndex = 13;
            this.cmxQueryType.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // htLabel3
            // 
            this.htLabel3.AutoSize = true;
            this.htLabel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.htLabel3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.htLabel3.Location = new System.Drawing.Point(396, 15);
            this.htLabel3.Name = "htLabel3";
            this.htLabel3.Size = new System.Drawing.Size(45, 17);
            this.htLabel3.TabIndex = 12;
            this.htLabel3.Text = "CellID:";
            this.htLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmxCellIDs
            // 
            this.cmxCellIDs.DropDownStyle = HYC.WindowsControls.HTDropDownStyle.DropDownList;
            this.cmxCellIDs.FillColor = System.Drawing.Color.White;
            this.cmxCellIDs.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmxCellIDs.FormattingEnabled = true;
            this.cmxCellIDs.Location = new System.Drawing.Point(447, 9);
            this.cmxCellIDs.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmxCellIDs.MinimumSize = new System.Drawing.Size(63, 0);
            this.cmxCellIDs.Name = "cmxCellIDs";
            this.cmxCellIDs.Padding = new System.Windows.Forms.Padding(0, 0, 30, 0);
            this.cmxCellIDs.Size = new System.Drawing.Size(120, 25);
            this.cmxCellIDs.TabIndex = 13;
            this.cmxCellIDs.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // htLabel2
            // 
            this.htLabel2.AutoSize = true;
            this.htLabel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.htLabel2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.htLabel2.Location = new System.Drawing.Point(382, 47);
            this.htLabel2.Name = "htLabel2";
            this.htLabel2.Size = new System.Drawing.Size(59, 17);
            this.htLabel2.TabIndex = 12;
            this.htLabel2.Text = "查询类型:";
            this.htLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmxDetectType
            // 
            this.cmxDetectType.DropDownStyle = HYC.WindowsControls.HTDropDownStyle.DropDownList;
            this.cmxDetectType.FillColor = System.Drawing.Color.White;
            this.cmxDetectType.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmxDetectType.FormattingEnabled = true;
            this.cmxDetectType.Items.AddRange(new object[] {
            "Total",
            "Aoi",
            "Mura",
            "SquintMura",
            "MultiAngle",
            "OpenShort"});
            this.cmxDetectType.Location = new System.Drawing.Point(237, 42);
            this.cmxDetectType.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmxDetectType.MinimumSize = new System.Drawing.Size(63, 0);
            this.cmxDetectType.Name = "cmxDetectType";
            this.cmxDetectType.Padding = new System.Windows.Forms.Padding(0, 0, 30, 0);
            this.cmxDetectType.Size = new System.Drawing.Size(120, 25);
            this.cmxDetectType.TabIndex = 13;
            this.cmxDetectType.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmxDetectType.SelectedIndexChanged += new System.EventHandler(this.cboSearchType_SelectedIndexChanged);
            // 
            // htLabel1
            // 
            this.htLabel1.AutoSize = true;
            this.htLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.htLabel1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.htLabel1.Location = new System.Drawing.Point(172, 46);
            this.htLabel1.Name = "htLabel1";
            this.htLabel1.Size = new System.Drawing.Size(59, 17);
            this.htLabel1.TabIndex = 12;
            this.htLabel1.Text = "检测类型:";
            this.htLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.label4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(4, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 17);
            this.label4.TabIndex = 8;
            this.label4.Text = "检测结果:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.label6.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(4, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 17);
            this.label6.TabIndex = 4;
            this.label6.Text = "二维码：";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dtPickerDate2);
            this.panel1.Controls.Add(this.dtPickerDate1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.ckbNow1);
            this.panel1.Controls.Add(this.dtPickerTime2);
            this.panel1.Controls.Add(this.dtPickerTime1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.panel1.Location = new System.Drawing.Point(3, 4);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(351, 77);
            this.panel1.TabIndex = 12;
            this.panel1.Text = null;
            // 
            // dtPickerDate2
            // 
            this.dtPickerDate2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtPickerDate2.Location = new System.Drawing.Point(56, 42);
            this.dtPickerDate2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtPickerDate2.Name = "dtPickerDate2";
            this.dtPickerDate2.Size = new System.Drawing.Size(132, 23);
            this.dtPickerDate2.TabIndex = 3;
            // 
            // dtPickerDate1
            // 
            this.dtPickerDate1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtPickerDate1.Location = new System.Drawing.Point(56, 11);
            this.dtPickerDate1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtPickerDate1.Name = "dtPickerDate1";
            this.dtPickerDate1.Size = new System.Drawing.Size(132, 23);
            this.dtPickerDate1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(6, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "起始：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ckbNow1
            // 
            this.ckbNow1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.ckbNow1.Checked = true;
            this.ckbNow1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ckbNow1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ckbNow1.Location = new System.Drawing.Point(286, 44);
            this.ckbNow1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ckbNow1.Name = "ckbNow1";
            this.ckbNow1.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.ckbNow1.Size = new System.Drawing.Size(51, 21);
            this.ckbNow1.Style = HYC.WindowsControls.HTStyle.Blue;
            this.ckbNow1.TabIndex = 9;
            this.ckbNow1.Text = "现在";
            this.ckbNow1.ValueChanged += new HYC.WindowsControls.HTCheckBox.OnValueChanged(this.ckbNow1_ValueChanged);
            // 
            // dtPickerTime2
            // 
            this.dtPickerTime2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtPickerTime2.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtPickerTime2.Location = new System.Drawing.Point(196, 42);
            this.dtPickerTime2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtPickerTime2.Name = "dtPickerTime2";
            this.dtPickerTime2.ShowUpDown = true;
            this.dtPickerTime2.Size = new System.Drawing.Size(83, 23);
            this.dtPickerTime2.TabIndex = 8;
            // 
            // dtPickerTime1
            // 
            this.dtPickerTime1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtPickerTime1.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtPickerTime1.Location = new System.Drawing.Point(196, 11);
            this.dtPickerTime1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtPickerTime1.Name = "dtPickerTime1";
            this.dtPickerTime1.ShowUpDown = true;
            this.dtPickerTime1.Size = new System.Drawing.Size(83, 23);
            this.dtPickerTime1.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(6, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "截止：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // HistoryQueryCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbpnlMain);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "HistoryQueryCtrl";
            this.Size = new System.Drawing.Size(1068, 705);
            this.Load += new System.EventHandler(this.HistoryQueryCtrl_Load);
            this.tbpnlMain.ResumeLayout(false);
            this.tbpnlTop.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tbpnlMain;
        private HYC.WindowsControls.HTDataGridViewUnit dataGridViewUnit1;
        private System.Windows.Forms.TableLayoutPanel tbpnlTop;
        private HYC.WindowsControls.HTPanel panel1;
        private System.Windows.Forms.DateTimePicker dtPickerDate2;
        private System.Windows.Forms.DateTimePicker dtPickerDate1;
        private HYC.WindowsControls.HTLabel label1;
        private HYC.WindowsControls.HTCheckBox ckbNow1;
        private System.Windows.Forms.DateTimePicker dtPickerTime2;
        private System.Windows.Forms.DateTimePicker dtPickerTime1;
        private HYC.WindowsControls.HTLabel label2;
        private HYC.WindowsControls.HTPanel panel2;
        private HYC.WindowsControls.HTComboBox cboResult;
        private HYC.WindowsControls.HTLabel label4;
        private HYC.WindowsControls.HTTextBox txtPanelID;
        private HYC.WindowsControls.HTLabel label6;
        private HYC.WindowsControls.HTComboBox cmxDetectType;
        private HYC.WindowsControls.HTLabel htLabel1;
        private WindowsControls.HTButton btnQuery;
        private WindowsControls.HTComboBox cmxCellIDs;
        private WindowsControls.HTLabel htLabel2;
        private WindowsControls.HTComboBox cmxQueryType;
        private WindowsControls.HTLabel htLabel3;
    }
}
