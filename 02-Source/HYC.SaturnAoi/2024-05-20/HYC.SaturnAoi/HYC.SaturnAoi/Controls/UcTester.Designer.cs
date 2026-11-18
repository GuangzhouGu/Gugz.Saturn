namespace HYC.Mt2Aoi.Controls
{
    partial class UcTester
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
            this.label1 = new HYC.WindowsControls.HTLabel();
            this.cboTester = new HYC.WindowsControls.HTComboBox();
            this.btnTesterNextPattern = new HYC.WindowsControls.HTButton();
            this.btnTesterPrePattern = new HYC.WindowsControls.HTButton();
            this.btnTesterPowerOff = new HYC.WindowsControls.HTButton();
            this.btnTesterPowerOn = new HYC.WindowsControls.HTButton();
            this.txtPattName = new HYC.WindowsControls.HTTextBox();
            this.txtResult = new HYC.WindowsControls.HTTextBox();
            this.num_R = new System.Windows.Forms.NumericUpDown();
            this.num_G = new System.Windows.Forms.NumericUpDown();
            this.num_B = new System.Windows.Forms.NumericUpDown();
            this.label8 = new HYC.WindowsControls.HTLabel();
            this.label14 = new HYC.WindowsControls.HTLabel();
            this.label15 = new HYC.WindowsControls.HTLabel();
            this.btnPowerOnSts = new HYC.WindowsControls.HTButton();
            this.btnSwitchByNo = new HYC.WindowsControls.HTButton();
            this.tbxPatternNo = new HYC.WindowsControls.HTTextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnShiftRecipe = new HYC.WindowsControls.HTButton();
            this.txtRecipeName = new HYC.WindowsControls.HTTextBox();
            this.btnGetCurPatternName = new HYC.WindowsControls.HTButton();
            this.btnGetCurRecipe = new HYC.WindowsControls.HTButton();
            this.htLabel4 = new HYC.WindowsControls.HTLabel();
            this.htLabel3 = new HYC.WindowsControls.HTLabel();
            this.htLabel2 = new HYC.WindowsControls.HTLabel();
            this.htLabel1 = new HYC.WindowsControls.HTLabel();
            this.btnReadVoltage = new HYC.WindowsControls.HTButton();
            this.htLabel5 = new HYC.WindowsControls.HTLabel();
            this.txtVoltage = new HYC.WindowsControls.HTTextBox();
            this.btnSetVoltage = new HYC.WindowsControls.HTButton();
            ((System.ComponentModel.ISupportInitialize)(this.num_R)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_G)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_B)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.label1.Location = new System.Drawing.Point(6, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 20);
            this.label1.TabIndex = 11;
            this.label1.Text = "检查机选择：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboTester
            // 
            this.cboTester.DropDownStyle = HYC.WindowsControls.HTDropDownStyle.DropDownList;
            this.cboTester.FillColor = System.Drawing.Color.White;
            this.cboTester.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.cboTester.FormattingEnabled = true;
            this.cboTester.Location = new System.Drawing.Point(115, 32);
            this.cboTester.MinimumSize = new System.Drawing.Size(63, 0);
            this.cboTester.Name = "cboTester";
            this.cboTester.Padding = new System.Windows.Forms.Padding(0, 0, 30, 0);
            this.cboTester.Size = new System.Drawing.Size(151, 25);
            this.cboTester.TabIndex = 10;
            this.cboTester.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnTesterNextPattern
            // 
            this.btnTesterNextPattern.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTesterNextPattern.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.btnTesterNextPattern.Location = new System.Drawing.Point(166, 118);
            this.btnTesterNextPattern.Name = "btnTesterNextPattern";
            this.btnTesterNextPattern.Size = new System.Drawing.Size(100, 32);
            this.btnTesterNextPattern.TabIndex = 9;
            this.btnTesterNextPattern.Text = "画面下切";
            this.btnTesterNextPattern.Click += new System.EventHandler(this.btnTesterNextPattern_Click);
            // 
            // btnTesterPrePattern
            // 
            this.btnTesterPrePattern.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTesterPrePattern.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.btnTesterPrePattern.Location = new System.Drawing.Point(6, 117);
            this.btnTesterPrePattern.Name = "btnTesterPrePattern";
            this.btnTesterPrePattern.Size = new System.Drawing.Size(100, 32);
            this.btnTesterPrePattern.TabIndex = 8;
            this.btnTesterPrePattern.Text = "画面上切";
            this.btnTesterPrePattern.Click += new System.EventHandler(this.btnTesterPrePattern_Click);
            // 
            // btnTesterPowerOff
            // 
            this.btnTesterPowerOff.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTesterPowerOff.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.btnTesterPowerOff.Location = new System.Drawing.Point(166, 64);
            this.btnTesterPowerOff.Name = "btnTesterPowerOff";
            this.btnTesterPowerOff.Size = new System.Drawing.Size(100, 32);
            this.btnTesterPowerOff.TabIndex = 7;
            this.btnTesterPowerOff.Text = "关电";
            this.btnTesterPowerOff.Click += new System.EventHandler(this.btnTesterPowerOff_Click);
            // 
            // btnTesterPowerOn
            // 
            this.btnTesterPowerOn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTesterPowerOn.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.btnTesterPowerOn.Location = new System.Drawing.Point(6, 64);
            this.btnTesterPowerOn.Name = "btnTesterPowerOn";
            this.btnTesterPowerOn.Size = new System.Drawing.Size(100, 32);
            this.btnTesterPowerOn.TabIndex = 6;
            this.btnTesterPowerOn.Text = "开电";
            this.btnTesterPowerOn.Click += new System.EventHandler(this.btnTesterPowerOn_Click);
            // 
            // txtPattName
            // 
            this.txtPattName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtPattName.FillColor = System.Drawing.Color.White;
            this.txtPattName.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPattName.Location = new System.Drawing.Point(372, 147);
            this.txtPattName.Maximum = 2147483647D;
            this.txtPattName.Minimum = -2147483648D;
            this.txtPattName.Name = "txtPattName";
            this.txtPattName.Padding = new System.Windows.Forms.Padding(5);
            this.txtPattName.Size = new System.Drawing.Size(100, 23);
            this.txtPattName.TabIndex = 15;
            this.txtPattName.Text = "White";
            // 
            // txtResult
            // 
            this.txtResult.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtResult.FillColor = System.Drawing.Color.White;
            this.txtResult.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtResult.Location = new System.Drawing.Point(372, 223);
            this.txtResult.Maximum = 2147483647D;
            this.txtResult.Minimum = -2147483648D;
            this.txtResult.Name = "txtResult";
            this.txtResult.Padding = new System.Windows.Forms.Padding(5);
            this.txtResult.Size = new System.Drawing.Size(100, 23);
            this.txtResult.TabIndex = 17;
            // 
            // num_R
            // 
            this.num_R.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.num_R.Location = new System.Drawing.Point(372, 31);
            this.num_R.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.num_R.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.num_R.Name = "num_R";
            this.num_R.Size = new System.Drawing.Size(100, 26);
            this.num_R.TabIndex = 61;
            this.num_R.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            // 
            // num_G
            // 
            this.num_G.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.num_G.Location = new System.Drawing.Point(372, 69);
            this.num_G.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.num_G.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.num_G.Name = "num_G";
            this.num_G.Size = new System.Drawing.Size(100, 26);
            this.num_G.TabIndex = 62;
            this.num_G.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            // 
            // num_B
            // 
            this.num_B.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.num_B.Location = new System.Drawing.Point(372, 107);
            this.num_B.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.num_B.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.num_B.Name = "num_B";
            this.num_B.Size = new System.Drawing.Size(100, 26);
            this.num_B.TabIndex = 63;
            this.num_B.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label8.Location = new System.Drawing.Point(344, 32);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(25, 25);
            this.label8.TabIndex = 64;
            this.label8.Text = "R:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label14
            // 
            this.label14.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.label14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label14.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label14.Location = new System.Drawing.Point(344, 70);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(25, 25);
            this.label14.TabIndex = 65;
            this.label14.Text = "G:";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label15
            // 
            this.label15.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.label15.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label15.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label15.Location = new System.Drawing.Point(344, 108);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(25, 25);
            this.label15.TabIndex = 66;
            this.label15.Text = "B:";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnPowerOnSts
            // 
            this.btnPowerOnSts.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPowerOnSts.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.btnPowerOnSts.Location = new System.Drawing.Point(166, 173);
            this.btnPowerOnSts.Name = "btnPowerOnSts";
            this.btnPowerOnSts.Size = new System.Drawing.Size(100, 29);
            this.btnPowerOnSts.TabIndex = 60;
            this.btnPowerOnSts.Text = "点灯状态";
            this.btnPowerOnSts.Click += new System.EventHandler(this.btnPowerOnSts_Click);
            // 
            // btnSwitchByNo
            // 
            this.btnSwitchByNo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSwitchByNo.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.btnSwitchByNo.Location = new System.Drawing.Point(6, 170);
            this.btnSwitchByNo.Name = "btnSwitchByNo";
            this.btnSwitchByNo.Size = new System.Drawing.Size(100, 32);
            this.btnSwitchByNo.TabIndex = 14;
            this.btnSwitchByNo.Text = "序号切图";
            this.btnSwitchByNo.Click += new System.EventHandler(this.btnSwitchByNo_Click);
            // 
            // tbxPatternNo
            // 
            this.tbxPatternNo.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tbxPatternNo.FillColor = System.Drawing.Color.White;
            this.tbxPatternNo.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbxPatternNo.Location = new System.Drawing.Point(372, 185);
            this.tbxPatternNo.Maximum = 2147483647D;
            this.tbxPatternNo.Minimum = -2147483648D;
            this.tbxPatternNo.Name = "tbxPatternNo";
            this.tbxPatternNo.Padding = new System.Windows.Forms.Padding(5);
            this.tbxPatternNo.Size = new System.Drawing.Size(100, 23);
            this.tbxPatternNo.TabIndex = 15;
            this.tbxPatternNo.Text = "0";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 617F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 47.44186F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(616, 394);
            this.tableLayoutPanel1.TabIndex = 68;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnPowerOnSts);
            this.groupBox1.Controls.Add(this.txtPattName);
            this.groupBox1.Controls.Add(this.btnShiftRecipe);
            this.groupBox1.Controls.Add(this.txtVoltage);
            this.groupBox1.Controls.Add(this.txtRecipeName);
            this.groupBox1.Controls.Add(this.txtResult);
            this.groupBox1.Controls.Add(this.btnSetVoltage);
            this.groupBox1.Controls.Add(this.btnReadVoltage);
            this.groupBox1.Controls.Add(this.btnGetCurPatternName);
            this.groupBox1.Controls.Add(this.btnGetCurRecipe);
            this.groupBox1.Controls.Add(this.tbxPatternNo);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnSwitchByNo);
            this.groupBox1.Controls.Add(this.btnTesterNextPattern);
            this.groupBox1.Controls.Add(this.cboTester);
            this.groupBox1.Controls.Add(this.num_R);
            this.groupBox1.Controls.Add(this.btnTesterPrePattern);
            this.groupBox1.Controls.Add(this.num_G);
            this.groupBox1.Controls.Add(this.btnTesterPowerOff);
            this.groupBox1.Controls.Add(this.num_B);
            this.groupBox1.Controls.Add(this.btnTesterPowerOn);
            this.groupBox1.Controls.Add(this.htLabel5);
            this.groupBox1.Controls.Add(this.htLabel4);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.htLabel3);
            this.groupBox1.Controls.Add(this.htLabel2);
            this.groupBox1.Controls.Add(this.htLabel1);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(611, 388);
            this.groupBox1.TabIndex = 69;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "检查机测试";
            // 
            // btnShiftRecipe
            // 
            this.btnShiftRecipe.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnShiftRecipe.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.btnShiftRecipe.Location = new System.Drawing.Point(6, 221);
            this.btnShiftRecipe.Name = "btnShiftRecipe";
            this.btnShiftRecipe.Size = new System.Drawing.Size(100, 29);
            this.btnShiftRecipe.TabIndex = 60;
            this.btnShiftRecipe.Text = "切换机种";
            this.btnShiftRecipe.Click += new System.EventHandler(this.btnShiftRecipe_Click);
            // 
            // txtRecipeName
            // 
            this.txtRecipeName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtRecipeName.FillColor = System.Drawing.Color.White;
            this.txtRecipeName.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtRecipeName.Location = new System.Drawing.Point(372, 262);
            this.txtRecipeName.Maximum = 2147483647D;
            this.txtRecipeName.Minimum = -2147483648D;
            this.txtRecipeName.Name = "txtRecipeName";
            this.txtRecipeName.Padding = new System.Windows.Forms.Padding(5);
            this.txtRecipeName.Size = new System.Drawing.Size(100, 23);
            this.txtRecipeName.TabIndex = 17;
            // 
            // btnGetCurPatternName
            // 
            this.btnGetCurPatternName.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGetCurPatternName.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.btnGetCurPatternName.Location = new System.Drawing.Point(6, 262);
            this.btnGetCurPatternName.Name = "btnGetCurPatternName";
            this.btnGetCurPatternName.Size = new System.Drawing.Size(100, 32);
            this.btnGetCurPatternName.TabIndex = 14;
            this.btnGetCurPatternName.Text = "读取画面";
            this.btnGetCurPatternName.Click += new System.EventHandler(this.btnGetCurPatternName_Click);
            // 
            // btnGetCurRecipe
            // 
            this.btnGetCurRecipe.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGetCurRecipe.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.btnGetCurRecipe.Location = new System.Drawing.Point(166, 221);
            this.btnGetCurRecipe.Name = "btnGetCurRecipe";
            this.btnGetCurRecipe.Size = new System.Drawing.Size(100, 32);
            this.btnGetCurRecipe.TabIndex = 14;
            this.btnGetCurRecipe.Text = "读取机种";
            this.btnGetCurRecipe.Click += new System.EventHandler(this.btnGetCurRecipe_Click);
            // 
            // htLabel4
            // 
            this.htLabel4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.htLabel4.ForeColor = System.Drawing.Color.Black;
            this.htLabel4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.htLabel4.Location = new System.Drawing.Point(302, 260);
            this.htLabel4.Name = "htLabel4";
            this.htLabel4.Size = new System.Drawing.Size(67, 25);
            this.htLabel4.TabIndex = 66;
            this.htLabel4.Text = "机种名称:";
            this.htLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // htLabel3
            // 
            this.htLabel3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.htLabel3.ForeColor = System.Drawing.Color.Black;
            this.htLabel3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.htLabel3.Location = new System.Drawing.Point(302, 221);
            this.htLabel3.Name = "htLabel3";
            this.htLabel3.Size = new System.Drawing.Size(67, 25);
            this.htLabel3.TabIndex = 66;
            this.htLabel3.Text = "指令结果:";
            this.htLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // htLabel2
            // 
            this.htLabel2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.htLabel2.ForeColor = System.Drawing.Color.Black;
            this.htLabel2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.htLabel2.Location = new System.Drawing.Point(302, 183);
            this.htLabel2.Name = "htLabel2";
            this.htLabel2.Size = new System.Drawing.Size(67, 25);
            this.htLabel2.TabIndex = 66;
            this.htLabel2.Text = "画面序号:";
            this.htLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // htLabel1
            // 
            this.htLabel1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.htLabel1.ForeColor = System.Drawing.Color.Black;
            this.htLabel1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.htLabel1.Location = new System.Drawing.Point(306, 147);
            this.htLabel1.Name = "htLabel1";
            this.htLabel1.Size = new System.Drawing.Size(60, 25);
            this.htLabel1.TabIndex = 66;
            this.htLabel1.Text = "画面名称:";
            this.htLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnReadVoltage
            // 
            this.btnReadVoltage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReadVoltage.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.btnReadVoltage.Location = new System.Drawing.Point(166, 262);
            this.btnReadVoltage.Name = "btnReadVoltage";
            this.btnReadVoltage.Size = new System.Drawing.Size(100, 32);
            this.btnReadVoltage.TabIndex = 14;
            this.btnReadVoltage.Text = "读取电压";
            this.btnReadVoltage.Click += new System.EventHandler(this.btnReadVoltage_Click);
            // 
            // htLabel5
            // 
            this.htLabel5.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.htLabel5.ForeColor = System.Drawing.Color.Black;
            this.htLabel5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.htLabel5.Location = new System.Drawing.Point(302, 293);
            this.htLabel5.Name = "htLabel5";
            this.htLabel5.Size = new System.Drawing.Size(67, 25);
            this.htLabel5.TabIndex = 66;
            this.htLabel5.Text = "电压:";
            this.htLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtVoltage
            // 
            this.txtVoltage.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtVoltage.FillColor = System.Drawing.Color.White;
            this.txtVoltage.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtVoltage.Location = new System.Drawing.Point(372, 295);
            this.txtVoltage.Maximum = 2147483647D;
            this.txtVoltage.Minimum = -2147483648D;
            this.txtVoltage.Name = "txtVoltage";
            this.txtVoltage.Padding = new System.Windows.Forms.Padding(5);
            this.txtVoltage.Size = new System.Drawing.Size(100, 23);
            this.txtVoltage.TabIndex = 17;
            // 
            // btnSetVoltage
            // 
            this.btnSetVoltage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSetVoltage.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.btnSetVoltage.Location = new System.Drawing.Point(6, 300);
            this.btnSetVoltage.Name = "btnSetVoltage";
            this.btnSetVoltage.Size = new System.Drawing.Size(100, 32);
            this.btnSetVoltage.TabIndex = 14;
            this.btnSetVoltage.Text = "设置电压";
            this.btnSetVoltage.Click += new System.EventHandler(this.btnSetVoltage_Click);
            // 
            // UcTester
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "UcTester";
            this.Size = new System.Drawing.Size(616, 394);
            this.Load += new System.EventHandler(this.UcTester_Load);
            ((System.ComponentModel.ISupportInitialize)(this.num_R)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_G)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_B)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private HYC.WindowsControls.HTLabel label1;
        private HYC.WindowsControls.HTComboBox cboTester;
        private HYC.WindowsControls.HTButton btnTesterNextPattern;
        private HYC.WindowsControls.HTButton btnTesterPrePattern;
        private HYC.WindowsControls.HTButton btnTesterPowerOff;
        private HYC.WindowsControls.HTButton btnTesterPowerOn;
        private HYC.WindowsControls.HTTextBox txtPattName;
        private HYC.WindowsControls.HTTextBox txtResult;
        private System.Windows.Forms.NumericUpDown num_R;
        private System.Windows.Forms.NumericUpDown num_G;
        private System.Windows.Forms.NumericUpDown num_B;
        private HYC.WindowsControls.HTLabel label8;
        private HYC.WindowsControls.HTLabel label14;
        private HYC.WindowsControls.HTLabel label15;
        private WindowsControls.HTButton btnPowerOnSts;
        private WindowsControls.HTButton btnSwitchByNo;
        private WindowsControls.HTTextBox tbxPatternNo;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private WindowsControls.HTLabel htLabel2;
        private WindowsControls.HTLabel htLabel1;
        private WindowsControls.HTLabel htLabel3;
        private WindowsControls.HTButton btnShiftRecipe;
        private WindowsControls.HTTextBox txtRecipeName;
        private WindowsControls.HTButton btnGetCurRecipe;
        private WindowsControls.HTLabel htLabel4;
        private WindowsControls.HTButton btnGetCurPatternName;
        private WindowsControls.HTButton btnReadVoltage;
        private WindowsControls.HTTextBox txtVoltage;
        private WindowsControls.HTLabel htLabel5;
        private WindowsControls.HTButton btnSetVoltage;
    }
}
