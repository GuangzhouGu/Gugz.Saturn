namespace HYC.SaturnMain.SubForms
{
    partial class FrmQuery
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
            this.tbxID = new System.Windows.Forms.TextBox();
            this.dtpDateEnd = new System.Windows.Forms.DateTimePicker();
            this.dtpDateStart = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxByID = new System.Windows.Forms.CheckBox();
            this.cbxByTime = new System.Windows.Forms.CheckBox();
            this.dtpTimeStart = new System.Windows.Forms.DateTimePicker();
            this.dtpTimeEnd = new System.Windows.Forms.DateTimePicker();
            this.htTabControlMenu1 = new HYC.WindowsControls.HTTabControlMenu();
            this.TosDataQuery = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_query = new HYC.WindowsControls.HTButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmxRowNumSelect = new System.Windows.Forms.ComboBox();
            this.cmxNgTypeSelect = new System.Windows.Forms.ComboBox();
            this.cmxSort = new System.Windows.Forms.ComboBox();
            this.cmxColNumSelect = new System.Windows.Forms.ComboBox();
            this.cmxGlassSelect = new System.Windows.Forms.ComboBox();
            this.cbxNow = new System.Windows.Forms.CheckBox();
            this.rbtNGInfo = new System.Windows.Forms.RadioButton();
            this.rbtCellInfo = new System.Windows.Forms.RadioButton();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.dgv_detectInfo = new HYC.WindowsControls.HTDataGridViewUnit();
            this.AoiMuraDataQuery = new System.Windows.Forms.TabPage();
            this.historyQueryCtrl1 = new HYC.SaturnMain.UserControls.HistoryQueryCtrl();
            this.ProbeInfoQuery = new System.Windows.Forms.TabPage();
            this.dgvProbeInfo = new HYC.WindowsControls.HTDataGridViewUnit();
            this.htTabControlMenu1.SuspendLayout();
            this.TosDataQuery.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.AoiMuraDataQuery.SuspendLayout();
            this.ProbeInfoQuery.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbxID
            // 
            this.tbxID.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbxID.Location = new System.Drawing.Point(420, 9);
            this.tbxID.Name = "tbxID";
            this.tbxID.Size = new System.Drawing.Size(119, 26);
            this.tbxID.TabIndex = 126;
            // 
            // dtpDateEnd
            // 
            this.dtpDateEnd.CustomFormat = "2001/12/31  01:01:01";
            this.dtpDateEnd.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtpDateEnd.Location = new System.Drawing.Point(92, 37);
            this.dtpDateEnd.Name = "dtpDateEnd";
            this.dtpDateEnd.Size = new System.Drawing.Size(157, 26);
            this.dtpDateEnd.TabIndex = 123;
            this.dtpDateEnd.Tag = "9999";
            // 
            // dtpDateStart
            // 
            this.dtpDateStart.CustomFormat = "2001/12/31  01:01:01";
            this.dtpDateStart.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtpDateStart.Location = new System.Drawing.Point(92, 6);
            this.dtpDateStart.Name = "dtpDateStart";
            this.dtpDateStart.Size = new System.Drawing.Size(157, 26);
            this.dtpDateStart.TabIndex = 124;
            this.dtpDateStart.Tag = "9999";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(372, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 20);
            this.label4.TabIndex = 117;
            this.label4.Text = "条码：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(7, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 20);
            this.label2.TabIndex = 119;
            this.label2.Text = "结束时间：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(7, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 20);
            this.label1.TabIndex = 120;
            this.label1.Text = "开始时间：";
            // 
            // cbxByID
            // 
            this.cbxByID.AutoSize = true;
            this.cbxByID.BackColor = System.Drawing.Color.White;
            this.cbxByID.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbxByID.ForeColor = System.Drawing.Color.Black;
            this.cbxByID.Location = new System.Drawing.Point(544, 38);
            this.cbxByID.Name = "cbxByID";
            this.cbxByID.Size = new System.Drawing.Size(56, 24);
            this.cbxByID.TabIndex = 127;
            this.cbxByID.Text = "条码";
            this.cbxByID.UseVisualStyleBackColor = false;
            // 
            // cbxByTime
            // 
            this.cbxByTime.AutoSize = true;
            this.cbxByTime.BackColor = System.Drawing.Color.White;
            this.cbxByTime.Checked = true;
            this.cbxByTime.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxByTime.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbxByTime.ForeColor = System.Drawing.Color.Black;
            this.cbxByTime.Location = new System.Drawing.Point(544, 7);
            this.cbxByTime.Name = "cbxByTime";
            this.cbxByTime.Size = new System.Drawing.Size(56, 24);
            this.cbxByTime.TabIndex = 128;
            this.cbxByTime.Text = "时间";
            this.cbxByTime.UseVisualStyleBackColor = false;
            // 
            // dtpTimeStart
            // 
            this.dtpTimeStart.CustomFormat = "2020/10/20  01:01:01";
            this.dtpTimeStart.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtpTimeStart.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpTimeStart.Location = new System.Drawing.Point(255, 5);
            this.dtpTimeStart.Name = "dtpTimeStart";
            this.dtpTimeStart.ShowUpDown = true;
            this.dtpTimeStart.Size = new System.Drawing.Size(91, 26);
            this.dtpTimeStart.TabIndex = 129;
            this.dtpTimeStart.Tag = "9999";
            // 
            // dtpTimeEnd
            // 
            this.dtpTimeEnd.CustomFormat = "2020/10/20  01:01:01";
            this.dtpTimeEnd.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtpTimeEnd.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpTimeEnd.Location = new System.Drawing.Point(255, 36);
            this.dtpTimeEnd.Name = "dtpTimeEnd";
            this.dtpTimeEnd.ShowUpDown = true;
            this.dtpTimeEnd.Size = new System.Drawing.Size(91, 26);
            this.dtpTimeEnd.TabIndex = 129;
            this.dtpTimeEnd.Tag = "9999";
            // 
            // htTabControlMenu1
            // 
            this.htTabControlMenu1.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.htTabControlMenu1.Controls.Add(this.TosDataQuery);
            this.htTabControlMenu1.Controls.Add(this.AoiMuraDataQuery);
            this.htTabControlMenu1.Controls.Add(this.ProbeInfoQuery);
            this.htTabControlMenu1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.htTabControlMenu1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.htTabControlMenu1.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.htTabControlMenu1.ItemSize = new System.Drawing.Size(40, 200);
            this.htTabControlMenu1.Location = new System.Drawing.Point(0, 0);
            this.htTabControlMenu1.MenuStyle = HYC.WindowsControls.HTMenuStyle.Custom;
            this.htTabControlMenu1.Multiline = true;
            this.htTabControlMenu1.Name = "htTabControlMenu1";
            this.htTabControlMenu1.SelectedIndex = 0;
            this.htTabControlMenu1.Size = new System.Drawing.Size(1539, 825);
            this.htTabControlMenu1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.htTabControlMenu1.Style = HYC.WindowsControls.HTStyle.Custom;
            this.htTabControlMenu1.TabBackColor = System.Drawing.Color.WhiteSmoke;
            this.htTabControlMenu1.TabIndex = 130;
            this.htTabControlMenu1.TabSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.htTabControlMenu1.TabSelectedForeColor = System.Drawing.Color.White;
            this.htTabControlMenu1.TabSelectedHighColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.htTabControlMenu1.TabUnSelectedForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.htTabControlMenu1.TextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.htTabControlMenu1.SelectedIndexChanged += new System.EventHandler(this.htTabControlMenu1_SelectedIndexChanged);
            // 
            // TosDataQuery
            // 
            this.TosDataQuery.Controls.Add(this.tableLayoutPanel1);
            this.TosDataQuery.Location = new System.Drawing.Point(201, 0);
            this.TosDataQuery.Name = "TosDataQuery";
            this.TosDataQuery.Size = new System.Drawing.Size(1338, 825);
            this.TosDataQuery.TabIndex = 0;
            this.TosDataQuery.Text = "TOS数据查询";
            this.TosDataQuery.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.Controls.Add(this.btn_query, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dgv_detectInfo, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 85F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1338, 825);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // btn_query
            // 
            this.btn_query.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_query.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_query.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.btn_query.Location = new System.Drawing.Point(1241, 3);
            this.btn_query.Margin = new System.Windows.Forms.Padding(3, 3, 3, 45);
            this.btn_query.Name = "btn_query";
            this.btn_query.Size = new System.Drawing.Size(94, 37);
            this.btn_query.TabIndex = 117;
            this.btn_query.Text = "查询";
            this.btn_query.Click += new System.EventHandler(this.btn_query_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.cmxRowNumSelect);
            this.panel1.Controls.Add(this.cmxNgTypeSelect);
            this.panel1.Controls.Add(this.cmxSort);
            this.panel1.Controls.Add(this.cmxColNumSelect);
            this.panel1.Controls.Add(this.cmxGlassSelect);
            this.panel1.Controls.Add(this.cbxNow);
            this.panel1.Controls.Add(this.rbtNGInfo);
            this.panel1.Controls.Add(this.rbtCellInfo);
            this.panel1.Controls.Add(this.dtpTimeEnd);
            this.panel1.Controls.Add(this.cbxByID);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.dtpDateStart);
            this.panel1.Controls.Add(this.dtpDateEnd);
            this.panel1.Controls.Add(this.cbxByTime);
            this.panel1.Controls.Add(this.dtpTimeStart);
            this.panel1.Controls.Add(this.tbxID);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1232, 79);
            this.panel1.TabIndex = 0;
            // 
            // cmxRowNumSelect
            // 
            this.cmxRowNumSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmxRowNumSelect.FormattingEnabled = true;
            this.cmxRowNumSelect.Location = new System.Drawing.Point(982, 38);
            this.cmxRowNumSelect.Name = "cmxRowNumSelect";
            this.cmxRowNumSelect.Size = new System.Drawing.Size(77, 28);
            this.cmxRowNumSelect.TabIndex = 134;
            // 
            // cmxNgTypeSelect
            // 
            this.cmxNgTypeSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmxNgTypeSelect.FormattingEnabled = true;
            this.cmxNgTypeSelect.Items.AddRange(new object[] {
            "全部",
            "Short",
            "Open"});
            this.cmxNgTypeSelect.Location = new System.Drawing.Point(788, 38);
            this.cmxNgTypeSelect.Name = "cmxNgTypeSelect";
            this.cmxNgTypeSelect.Size = new System.Drawing.Size(77, 28);
            this.cmxNgTypeSelect.TabIndex = 134;
            // 
            // cmxSort
            // 
            this.cmxSort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmxSort.FormattingEnabled = true;
            this.cmxSort.Items.AddRange(new object[] {
            "降序",
            "升序"});
            this.cmxSort.Location = new System.Drawing.Point(1122, 6);
            this.cmxSort.Name = "cmxSort";
            this.cmxSort.Size = new System.Drawing.Size(77, 28);
            this.cmxSort.TabIndex = 134;
            // 
            // cmxColNumSelect
            // 
            this.cmxColNumSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmxColNumSelect.FormattingEnabled = true;
            this.cmxColNumSelect.Location = new System.Drawing.Point(982, 5);
            this.cmxColNumSelect.Name = "cmxColNumSelect";
            this.cmxColNumSelect.Size = new System.Drawing.Size(77, 28);
            this.cmxColNumSelect.TabIndex = 134;
            // 
            // cmxGlassSelect
            // 
            this.cmxGlassSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmxGlassSelect.FormattingEnabled = true;
            this.cmxGlassSelect.Items.AddRange(new object[] {
            "全部",
            "Glass1",
            "Glass2"});
            this.cmxGlassSelect.Location = new System.Drawing.Point(788, 5);
            this.cmxGlassSelect.Name = "cmxGlassSelect";
            this.cmxGlassSelect.Size = new System.Drawing.Size(77, 28);
            this.cmxGlassSelect.TabIndex = 134;
            // 
            // cbxNow
            // 
            this.cbxNow.AutoSize = true;
            this.cbxNow.BackColor = System.Drawing.Color.White;
            this.cbxNow.Checked = true;
            this.cbxNow.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxNow.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbxNow.ForeColor = System.Drawing.Color.Black;
            this.cbxNow.Location = new System.Drawing.Point(352, 37);
            this.cbxNow.Name = "cbxNow";
            this.cbxNow.Size = new System.Drawing.Size(56, 24);
            this.cbxNow.TabIndex = 133;
            this.cbxNow.Text = "现在";
            this.cbxNow.UseVisualStyleBackColor = false;
            // 
            // rbtNGInfo
            // 
            this.rbtNGInfo.AutoSize = true;
            this.rbtNGInfo.Location = new System.Drawing.Point(606, 39);
            this.rbtNGInfo.Name = "rbtNGInfo";
            this.rbtNGInfo.Size = new System.Drawing.Size(83, 24);
            this.rbtNGInfo.TabIndex = 131;
            this.rbtNGInfo.Text = "不良信息";
            this.rbtNGInfo.UseVisualStyleBackColor = true;
            // 
            // rbtCellInfo
            // 
            this.rbtCellInfo.AutoSize = true;
            this.rbtCellInfo.Checked = true;
            this.rbtCellInfo.Location = new System.Drawing.Point(606, 4);
            this.rbtCellInfo.Name = "rbtCellInfo";
            this.rbtCellInfo.Size = new System.Drawing.Size(80, 24);
            this.rbtCellInfo.TabIndex = 132;
            this.rbtCellInfo.TabStop = true;
            this.rbtCellInfo.Text = "Cell信息";
            this.rbtCellInfo.UseVisualStyleBackColor = true;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.White;
            this.label14.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.ForeColor = System.Drawing.Color.Black;
            this.label14.Location = new System.Drawing.Point(895, 42);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(85, 20);
            this.label14.TabIndex = 117;
            this.label14.Text = "RowNum：";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.White;
            this.label15.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label15.ForeColor = System.Drawing.Color.Black;
            this.label15.Location = new System.Drawing.Point(1074, 10);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(51, 20);
            this.label15.TabIndex = 117;
            this.label15.Text = "排序：";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.White;
            this.label12.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(710, 43);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(72, 20);
            this.label12.TabIndex = 117;
            this.label12.Text = "NG类型：";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.White;
            this.label13.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(871, 8);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(109, 20);
            this.label13.TabIndex = 117;
            this.label13.Text = "ColumnNum：";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.White;
            this.label11.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(692, 8);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(90, 20);
            this.label11.TabIndex = 117;
            this.label11.Text = "GlassNum：";
            // 
            // dgv_detectInfo
            // 
            this.dgv_detectInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutPanel1.SetColumnSpan(this.dgv_detectInfo, 2);
            this.dgv_detectInfo.CountPerPage = 50;
            this.dgv_detectInfo.DataSource = null;
            this.dgv_detectInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_detectInfo.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dgv_detectInfo.Location = new System.Drawing.Point(3, 89);
            this.dgv_detectInfo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgv_detectInfo.Name = "dgv_detectInfo";
            this.dgv_detectInfo.Size = new System.Drawing.Size(1332, 732);
            this.dgv_detectInfo.TabIndex = 116;
            // 
            // AoiMuraDataQuery
            // 
            this.AoiMuraDataQuery.Controls.Add(this.historyQueryCtrl1);
            this.AoiMuraDataQuery.Location = new System.Drawing.Point(201, 0);
            this.AoiMuraDataQuery.Name = "AoiMuraDataQuery";
            this.AoiMuraDataQuery.Size = new System.Drawing.Size(1338, 825);
            this.AoiMuraDataQuery.TabIndex = 2;
            this.AoiMuraDataQuery.Text = "AoiMura检测查询";
            this.AoiMuraDataQuery.UseVisualStyleBackColor = true;
            // 
            // historyQueryCtrl1
            // 
            this.historyQueryCtrl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.historyQueryCtrl1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.historyQueryCtrl1.Location = new System.Drawing.Point(0, 0);
            this.historyQueryCtrl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.historyQueryCtrl1.Name = "historyQueryCtrl1";
            this.historyQueryCtrl1.Size = new System.Drawing.Size(1338, 825);
            this.historyQueryCtrl1.TabIndex = 0;
            // 
            // ProbeInfoQuery
            // 
            this.ProbeInfoQuery.Controls.Add(this.dgvProbeInfo);
            this.ProbeInfoQuery.Location = new System.Drawing.Point(201, 0);
            this.ProbeInfoQuery.Name = "ProbeInfoQuery";
            this.ProbeInfoQuery.Size = new System.Drawing.Size(1338, 825);
            this.ProbeInfoQuery.TabIndex = 1;
            this.ProbeInfoQuery.Text = "ProbeCard信息查询";
            this.ProbeInfoQuery.UseVisualStyleBackColor = true;
            // 
            // dgvProbeInfo
            // 
            this.dgvProbeInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvProbeInfo.CountPerPage = 50;
            this.dgvProbeInfo.DataSource = null;
            this.dgvProbeInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvProbeInfo.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dgvProbeInfo.Location = new System.Drawing.Point(0, 0);
            this.dgvProbeInfo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgvProbeInfo.Name = "dgvProbeInfo";
            this.dgvProbeInfo.Size = new System.Drawing.Size(1338, 825);
            this.dgvProbeInfo.TabIndex = 117;
            // 
            // FrmQuery
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1539, 825);
            this.Controls.Add(this.htTabControlMenu1);
            this.Name = "FrmQuery";
            this.Text = "FormQuery";
            this.Load += new System.EventHandler(this.FormQuery_Load);
            this.Controls.SetChildIndex(this.htTabControlMenu1, 0);
            this.htTabControlMenu1.ResumeLayout(false);
            this.TosDataQuery.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.AoiMuraDataQuery.ResumeLayout(false);
            this.ProbeInfoQuery.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox tbxID;
        public System.Windows.Forms.DateTimePicker dtpDateEnd;
        public System.Windows.Forms.DateTimePicker dtpDateStart;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbxByID;
        private System.Windows.Forms.CheckBox cbxByTime;
        public System.Windows.Forms.DateTimePicker dtpTimeStart;
        public System.Windows.Forms.DateTimePicker dtpTimeEnd;
        private HYC.WindowsControls.HTTabControlMenu htTabControlMenu1;
        private System.Windows.Forms.TabPage TosDataQuery;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TabPage ProbeInfoQuery;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabPage AoiMuraDataQuery;
        private System.Windows.Forms.CheckBox cbxNow;
        private System.Windows.Forms.RadioButton rbtNGInfo;
        private System.Windows.Forms.RadioButton rbtCellInfo;
        private System.Windows.Forms.ComboBox cmxNgTypeSelect;
        private System.Windows.Forms.ComboBox cmxGlassSelect;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cmxRowNumSelect;
        private System.Windows.Forms.ComboBox cmxColNumSelect;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cmxSort;
        private System.Windows.Forms.Label label15;
        private HYC.SaturnMain.UserControls.HistoryQueryCtrl historyQueryCtrl1;
        private WindowsControls.HTButton btn_query;
        private WindowsControls.HTDataGridViewUnit dgvProbeInfo;
        private WindowsControls.HTDataGridViewUnit dgv_detectInfo;
    }
}