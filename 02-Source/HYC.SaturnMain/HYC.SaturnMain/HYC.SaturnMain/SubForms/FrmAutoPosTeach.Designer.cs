
namespace HYC.SaturnMain.SubForms
{
    partial class FrmAutoPosTeach
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAutoPosTeach));
            this.label1 = new System.Windows.Forms.Label();
            this.cmxCameraList = new System.Windows.Forms.ComboBox();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnRight = new System.Windows.Forms.Button();
            this.btnLeft = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxMoveDis = new System.Windows.Forms.TextBox();
            this.btnZDown = new System.Windows.Forms.Button();
            this.btnZUp = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnClockRotate = new System.Windows.Forms.Button();
            this.btnReverseRotate = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.txtStage2ContactPos = new System.Windows.Forms.TextBox();
            this.txtStage2TPos = new System.Windows.Forms.TextBox();
            this.txtStage1ContactPos = new System.Windows.Forms.TextBox();
            this.txtStage1TPos = new System.Windows.Forms.TextBox();
            this.txtAlignX2Pos = new System.Windows.Forms.TextBox();
            this.txtAlignX1Pos = new System.Windows.Forms.TextBox();
            this.txtProbeGantryPos = new System.Windows.Forms.TextBox();
            this.btnAlignTest = new System.Windows.Forms.Button();
            this.btnReadPos = new System.Windows.Forms.Button();
            this.btnSavePosInfo = new System.Windows.Forms.Button();
            this.cmxStageList = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.powerCtrl1 = new HYC.SaturnMain.UserControls.PowerCtrl();
            this.btnStage1AlignPos = new System.Windows.Forms.Button();
            this.btnStage2AlignPos = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(6, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "相机名称";
            // 
            // cmxCameraList
            // 
            this.cmxCameraList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmxCameraList.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmxCameraList.FormattingEnabled = true;
            this.cmxCameraList.Items.AddRange(new object[] {
            "对位相机1",
            "对位相机2",
            "Contact相机",
            "Aoi相机1",
            "直视Mura相机1",
            "斜视Mura相机1",
            "多角度光谱仪1",
            "Aoi相机2",
            "直视Mura相机2",
            "斜视Mura相机2",
            "多角度光谱仪2",
            "平台1压接轴",
            "平台2压接轴",
            "平台1T轴",
            "平台2T轴"});
            this.cmxCameraList.Location = new System.Drawing.Point(77, 10);
            this.cmxCameraList.Name = "cmxCameraList";
            this.cmxCameraList.Size = new System.Drawing.Size(156, 28);
            this.cmxCameraList.TabIndex = 2;
            this.cmxCameraList.SelectedIndexChanged += new System.EventHandler(this.cmxCameraList_SelectedIndexChanged);
            // 
            // btnDown
            // 
            this.btnDown.Image = ((System.Drawing.Image)(resources.GetObject("btnDown.Image")));
            this.btnDown.Location = new System.Drawing.Point(55, 179);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(50, 50);
            this.btnDown.TabIndex = 3;
            this.toolTip1.SetToolTip(this.btnDown, "相机轴");
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnRight
            // 
            this.btnRight.Image = ((System.Drawing.Image)(resources.GetObject("btnRight.Image")));
            this.btnRight.Location = new System.Drawing.Point(104, 129);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(50, 50);
            this.btnRight.TabIndex = 3;
            this.toolTip1.SetToolTip(this.btnRight, "龙门轴");
            this.btnRight.UseVisualStyleBackColor = true;
            this.btnRight.Click += new System.EventHandler(this.btnRight_Click);
            // 
            // btnLeft
            // 
            this.btnLeft.Image = ((System.Drawing.Image)(resources.GetObject("btnLeft.Image")));
            this.btnLeft.Location = new System.Drawing.Point(6, 129);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(50, 50);
            this.btnLeft.TabIndex = 3;
            this.toolTip1.SetToolTip(this.btnLeft, "龙门轴");
            this.btnLeft.UseVisualStyleBackColor = true;
            this.btnLeft.Click += new System.EventHandler(this.btnLeft_Click);
            // 
            // btnUp
            // 
            this.btnUp.Image = ((System.Drawing.Image)(resources.GetObject("btnUp.Image")));
            this.btnUp.Location = new System.Drawing.Point(55, 79);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(50, 50);
            this.btnUp.TabIndex = 3;
            this.toolTip1.SetToolTip(this.btnUp, "相机轴");
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(6, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "移动距离";
            // 
            // tbxMoveDis
            // 
            this.tbxMoveDis.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbxMoveDis.Location = new System.Drawing.Point(77, 44);
            this.tbxMoveDis.Name = "tbxMoveDis";
            this.tbxMoveDis.Size = new System.Drawing.Size(156, 26);
            this.tbxMoveDis.TabIndex = 4;
            this.tbxMoveDis.Text = "1";
            // 
            // btnZDown
            // 
            this.btnZDown.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnZDown.Location = new System.Drawing.Point(180, 179);
            this.btnZDown.Name = "btnZDown";
            this.btnZDown.Size = new System.Drawing.Size(50, 50);
            this.btnZDown.TabIndex = 3;
            this.btnZDown.Text = "Z ↓";
            this.toolTip1.SetToolTip(this.btnZDown, "平台压接轴");
            this.btnZDown.UseVisualStyleBackColor = true;
            this.btnZDown.Click += new System.EventHandler(this.btnZDown_Click);
            // 
            // btnZUp
            // 
            this.btnZUp.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnZUp.Location = new System.Drawing.Point(180, 79);
            this.btnZUp.Name = "btnZUp";
            this.btnZUp.Size = new System.Drawing.Size(50, 50);
            this.btnZUp.TabIndex = 3;
            this.btnZUp.Text = "Z ↑";
            this.toolTip1.SetToolTip(this.btnZUp, "平台压接轴");
            this.btnZUp.UseVisualStyleBackColor = true;
            this.btnZUp.Click += new System.EventHandler(this.btnZUp_Click);
            // 
            // btnStop
            // 
            this.btnStop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnStop.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnStop.Location = new System.Drawing.Point(312, 39);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(107, 29);
            this.btnStop.TabIndex = 3;
            this.btnStop.Text = "停止";
            this.btnStop.UseVisualStyleBackColor = false;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(232, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 20);
            this.label3.TabIndex = 1;
            this.label3.Text = "mm";
            // 
            // btnClockRotate
            // 
            this.btnClockRotate.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClockRotate.Image = ((System.Drawing.Image)(resources.GetObject("btnClockRotate.Image")));
            this.btnClockRotate.Location = new System.Drawing.Point(236, 79);
            this.btnClockRotate.Name = "btnClockRotate";
            this.btnClockRotate.Size = new System.Drawing.Size(50, 50);
            this.btnClockRotate.TabIndex = 3;
            this.toolTip1.SetToolTip(this.btnClockRotate, "平台旋转轴");
            this.btnClockRotate.UseVisualStyleBackColor = true;
            this.btnClockRotate.Click += new System.EventHandler(this.btnClockRotate_Click);
            // 
            // btnReverseRotate
            // 
            this.btnReverseRotate.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnReverseRotate.Image = ((System.Drawing.Image)(resources.GetObject("btnReverseRotate.Image")));
            this.btnReverseRotate.Location = new System.Drawing.Point(236, 179);
            this.btnReverseRotate.Name = "btnReverseRotate";
            this.btnReverseRotate.Size = new System.Drawing.Size(50, 50);
            this.btnReverseRotate.TabIndex = 3;
            this.toolTip1.SetToolTip(this.btnReverseRotate, "平台旋转轴");
            this.btnReverseRotate.UseVisualStyleBackColor = true;
            this.btnReverseRotate.Click += new System.EventHandler(this.btnReverseRotate_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 5000;
            this.toolTip1.InitialDelay = 100;
            this.toolTip1.ReshowDelay = 100;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(4, 39);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(302, 264);
            this.tabControl1.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.tbxMoveDis);
            this.tabPage1.Controls.Add(this.cmxCameraList);
            this.tabPage1.Controls.Add(this.btnLeft);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.btnRight);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.btnDown);
            this.tabPage1.Controls.Add(this.btnZDown);
            this.tabPage1.Controls.Add(this.btnClockRotate);
            this.tabPage1.Controls.Add(this.btnUp);
            this.tabPage1.Controls.Add(this.btnZUp);
            this.tabPage1.Controls.Add(this.btnReverseRotate);
            this.tabPage1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(294, 234);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "相机移动";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.txtStage2ContactPos);
            this.tabPage3.Controls.Add(this.txtStage2TPos);
            this.tabPage3.Controls.Add(this.txtStage1ContactPos);
            this.tabPage3.Controls.Add(this.txtStage1TPos);
            this.tabPage3.Controls.Add(this.txtAlignX2Pos);
            this.tabPage3.Controls.Add(this.txtAlignX1Pos);
            this.tabPage3.Controls.Add(this.txtProbeGantryPos);
            this.tabPage3.Controls.Add(this.btnAlignTest);
            this.tabPage3.Controls.Add(this.btnReadPos);
            this.tabPage3.Controls.Add(this.btnSavePosInfo);
            this.tabPage3.Controls.Add(this.cmxStageList);
            this.tabPage3.Controls.Add(this.label10);
            this.tabPage3.Controls.Add(this.label12);
            this.tabPage3.Controls.Add(this.label11);
            this.tabPage3.Controls.Add(this.label9);
            this.tabPage3.Controls.Add(this.label8);
            this.tabPage3.Controls.Add(this.label7);
            this.tabPage3.Controls.Add(this.label6);
            this.tabPage3.Controls.Add(this.label5);
            this.tabPage3.Location = new System.Drawing.Point(4, 26);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(294, 234);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "对位位置保存";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // txtStage2ContactPos
            // 
            this.txtStage2ContactPos.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtStage2ContactPos.Location = new System.Drawing.Point(103, 196);
            this.txtStage2ContactPos.Name = "txtStage2ContactPos";
            this.txtStage2ContactPos.Size = new System.Drawing.Size(108, 23);
            this.txtStage2ContactPos.TabIndex = 11;
            // 
            // txtStage2TPos
            // 
            this.txtStage2TPos.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtStage2TPos.Location = new System.Drawing.Point(103, 147);
            this.txtStage2TPos.Name = "txtStage2TPos";
            this.txtStage2TPos.Size = new System.Drawing.Size(108, 23);
            this.txtStage2TPos.TabIndex = 11;
            // 
            // txtStage1ContactPos
            // 
            this.txtStage1ContactPos.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtStage1ContactPos.Location = new System.Drawing.Point(103, 171);
            this.txtStage1ContactPos.Name = "txtStage1ContactPos";
            this.txtStage1ContactPos.Size = new System.Drawing.Size(108, 23);
            this.txtStage1ContactPos.TabIndex = 11;
            // 
            // txtStage1TPos
            // 
            this.txtStage1TPos.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtStage1TPos.Location = new System.Drawing.Point(103, 122);
            this.txtStage1TPos.Name = "txtStage1TPos";
            this.txtStage1TPos.Size = new System.Drawing.Size(108, 23);
            this.txtStage1TPos.TabIndex = 11;
            // 
            // txtAlignX2Pos
            // 
            this.txtAlignX2Pos.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtAlignX2Pos.Location = new System.Drawing.Point(103, 96);
            this.txtAlignX2Pos.Name = "txtAlignX2Pos";
            this.txtAlignX2Pos.Size = new System.Drawing.Size(108, 23);
            this.txtAlignX2Pos.TabIndex = 11;
            // 
            // txtAlignX1Pos
            // 
            this.txtAlignX1Pos.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtAlignX1Pos.Location = new System.Drawing.Point(103, 69);
            this.txtAlignX1Pos.Name = "txtAlignX1Pos";
            this.txtAlignX1Pos.Size = new System.Drawing.Size(108, 23);
            this.txtAlignX1Pos.TabIndex = 11;
            // 
            // txtProbeGantryPos
            // 
            this.txtProbeGantryPos.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtProbeGantryPos.Location = new System.Drawing.Point(103, 43);
            this.txtProbeGantryPos.Name = "txtProbeGantryPos";
            this.txtProbeGantryPos.Size = new System.Drawing.Size(108, 23);
            this.txtProbeGantryPos.TabIndex = 11;
            // 
            // btnAlignTest
            // 
            this.btnAlignTest.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAlignTest.Location = new System.Drawing.Point(217, 7);
            this.btnAlignTest.Name = "btnAlignTest";
            this.btnAlignTest.Size = new System.Drawing.Size(74, 28);
            this.btnAlignTest.TabIndex = 10;
            this.btnAlignTest.Text = "对位测试";
            this.btnAlignTest.UseVisualStyleBackColor = true;
            this.btnAlignTest.Click += new System.EventHandler(this.btnAlignTest_Click);
            // 
            // btnReadPos
            // 
            this.btnReadPos.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnReadPos.Location = new System.Drawing.Point(217, 163);
            this.btnReadPos.Name = "btnReadPos";
            this.btnReadPos.Size = new System.Drawing.Size(74, 28);
            this.btnReadPos.TabIndex = 9;
            this.btnReadPos.Text = "读取点位";
            this.btnReadPos.UseVisualStyleBackColor = true;
            this.btnReadPos.Click += new System.EventHandler(this.btnReadPos_Click);
            // 
            // btnSavePosInfo
            // 
            this.btnSavePosInfo.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSavePosInfo.Location = new System.Drawing.Point(217, 192);
            this.btnSavePosInfo.Name = "btnSavePosInfo";
            this.btnSavePosInfo.Size = new System.Drawing.Size(74, 28);
            this.btnSavePosInfo.TabIndex = 9;
            this.btnSavePosInfo.Text = "保存点位";
            this.btnSavePosInfo.UseVisualStyleBackColor = true;
            this.btnSavePosInfo.Click += new System.EventHandler(this.btnSavePosInfo_Click);
            // 
            // cmxStageList
            // 
            this.cmxStageList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmxStageList.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmxStageList.FormattingEnabled = true;
            this.cmxStageList.Items.AddRange(new object[] {
            "平台1",
            "平台2"});
            this.cmxStageList.Location = new System.Drawing.Point(103, 8);
            this.cmxStageList.Name = "cmxStageList";
            this.cmxStageList.Size = new System.Drawing.Size(108, 28);
            this.cmxStageList.TabIndex = 8;
            this.cmxStageList.SelectedIndexChanged += new System.EventHandler(this.cmxStageList_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(30, 147);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(67, 20);
            this.label10.TabIndex = 7;
            this.label10.Text = "平台2T轴";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.Location = new System.Drawing.Point(10, 195);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(87, 20);
            this.label12.TabIndex = 7;
            this.label12.Text = "平台1压接轴";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(10, 171);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(87, 20);
            this.label11.TabIndex = 7;
            this.label11.Text = "平台1压接轴";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(30, 122);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(67, 20);
            this.label9.TabIndex = 7;
            this.label9.Text = "平台1T轴";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(6, 96);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(96, 20);
            this.label8.TabIndex = 7;
            this.label8.Text = "对位相机2X轴";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(6, 69);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(96, 20);
            this.label7.TabIndex = 7;
            this.label7.Text = "对位相机1X轴";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(24, 42);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 20);
            this.label6.TabIndex = 7;
            this.label6.Text = "Probe龙门";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(60, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 20);
            this.label5.TabIndex = 7;
            this.label5.Text = "平台";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.powerCtrl1);
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(294, 234);
            this.tabPage2.TabIndex = 3;
            this.tabPage2.Text = "光源控制";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // powerCtrl1
            // 
            this.powerCtrl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.powerCtrl1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.powerCtrl1.Location = new System.Drawing.Point(0, 0);
            this.powerCtrl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.powerCtrl1.Name = "powerCtrl1";
            this.powerCtrl1.Size = new System.Drawing.Size(294, 234);
            this.powerCtrl1.TabIndex = 0;
            // 
            // btnStage1AlignPos
            // 
            this.btnStage1AlignPos.BackColor = System.Drawing.SystemColors.Control;
            this.btnStage1AlignPos.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnStage1AlignPos.Location = new System.Drawing.Point(312, 72);
            this.btnStage1AlignPos.Name = "btnStage1AlignPos";
            this.btnStage1AlignPos.Size = new System.Drawing.Size(107, 29);
            this.btnStage1AlignPos.TabIndex = 3;
            this.btnStage1AlignPos.Text = "Stage1对位位置";
            this.btnStage1AlignPos.UseVisualStyleBackColor = false;
            this.btnStage1AlignPos.Click += new System.EventHandler(this.btnStage1AlignPos_Click);
            // 
            // btnStage2AlignPos
            // 
            this.btnStage2AlignPos.BackColor = System.Drawing.SystemColors.Control;
            this.btnStage2AlignPos.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnStage2AlignPos.Location = new System.Drawing.Point(312, 106);
            this.btnStage2AlignPos.Name = "btnStage2AlignPos";
            this.btnStage2AlignPos.Size = new System.Drawing.Size(107, 29);
            this.btnStage2AlignPos.TabIndex = 3;
            this.btnStage2AlignPos.Text = "Stage2对位位置";
            this.btnStage2AlignPos.UseVisualStyleBackColor = false;
            this.btnStage2AlignPos.Click += new System.EventHandler(this.btnStage2AlignPos_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.Control;
            this.button1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(312, 141);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(107, 45);
            this.button1.TabIndex = 3;
            this.button1.Text = "Contact相机监控位置";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FrmAutoPosTeach
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(423, 311);
            this.CloseFormTimeLag = 0;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnStage2AlignPos);
            this.Controls.Add(this.btnStage1AlignPos);
            this.Controls.Add(this.btnStop);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "FrmAutoPosTeach";
            this.Opacity = 1D;
            this.ShowFormTimelag = 0;
            this.Text = "FrmAxisMove";
            this.Load += new System.EventHandler(this.FrmAxisMove_Load);
            this.Controls.SetChildIndex(this.btnStop, 0);
            this.Controls.SetChildIndex(this.btnStage1AlignPos, 0);
            this.Controls.SetChildIndex(this.btnStage2AlignPos, 0);
            this.Controls.SetChildIndex(this.button1, 0);
            this.Controls.SetChildIndex(this.tabControl1, 0);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmxCameraList;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnRight;
        private System.Windows.Forms.Button btnLeft;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxMoveDis;
        private System.Windows.Forms.Button btnZDown;
        private System.Windows.Forms.Button btnZUp;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnClockRotate;
        private System.Windows.Forms.Button btnReverseRotate;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button btnAlignTest;
        private System.Windows.Forms.Button btnSavePosInfo;
        private System.Windows.Forms.ComboBox cmxStageList;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtStage2ContactPos;
        private System.Windows.Forms.TextBox txtStage2TPos;
        private System.Windows.Forms.TextBox txtStage1ContactPos;
        private System.Windows.Forms.TextBox txtStage1TPos;
        private System.Windows.Forms.TextBox txtAlignX2Pos;
        private System.Windows.Forms.TextBox txtAlignX1Pos;
        private System.Windows.Forms.TextBox txtProbeGantryPos;
        private System.Windows.Forms.Button btnReadPos;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TabPage tabPage2;
        private UserControls.PowerCtrl powerCtrl1;
        private System.Windows.Forms.Button btnStage1AlignPos;
        private System.Windows.Forms.Button btnStage2AlignPos;
        private System.Windows.Forms.Button button1;
    }
}