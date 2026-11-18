
using HYC.HTPCBase.Extension.UserCtrl;

namespace HYC.SaturnMain.SubForms
{
    partial class FrmCabinet
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
            this.btnBufferToCabinet = new HYC.WindowsControls.HTButton();
            this.btnCabinetToBuffer = new HYC.WindowsControls.HTButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.rbtRow1 = new HYC.WindowsControls.HTRadioButton();
            this.rbtRow2 = new HYC.WindowsControls.HTRadioButton();
            this.rbtRow3 = new HYC.WindowsControls.HTRadioButton();
            this.rbtRow4 = new HYC.WindowsControls.HTRadioButton();
            this.rbtRow5 = new HYC.WindowsControls.HTRadioButton();
            this.rbtRow6 = new HYC.WindowsControls.HTRadioButton();
            this.rbtRow7 = new HYC.WindowsControls.HTRadioButton();
            this.rbtRow8 = new HYC.WindowsControls.HTRadioButton();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panelEx1 = new HYC.HTPCBase.Extension.UserCtrl.PanelEx();
            this.btnUpdate = new HYC.WindowsControls.HTButton();
            this.btnCabinetToUvw = new HYC.WindowsControls.HTButton();
            this.btnUvwToCabinet = new HYC.WindowsControls.HTButton();
            this.btnUvwToCabinetToUvw = new HYC.WindowsControls.HTButton();
            this.btnPause = new HYC.WindowsControls.HTButton();
            this.btnRun = new HYC.WindowsControls.HTButton();
            this.btnStop = new HYC.WindowsControls.HTButton();
            this.cabinetCellCtrl1 = new HYC.SaturnMain.UserControls.CabinetCellCtrl();
            this.cabinetCellCtrl2 = new HYC.SaturnMain.UserControls.CabinetCellCtrl();
            this.cabinetCellCtrl3 = new HYC.SaturnMain.UserControls.CabinetCellCtrl();
            this.cabinetCellCtrl4 = new HYC.SaturnMain.UserControls.CabinetCellCtrl();
            this.cabinetCellCtrl5 = new HYC.SaturnMain.UserControls.CabinetCellCtrl();
            this.cabinetCellCtrl6 = new HYC.SaturnMain.UserControls.CabinetCellCtrl();
            this.cabinetCellCtrl7 = new HYC.SaturnMain.UserControls.CabinetCellCtrl();
            this.cabinetCellCtrl8 = new HYC.SaturnMain.UserControls.CabinetCellCtrl();
            this.btnBufferStageOut = new HYC.WindowsControls.HTButton();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panelEx1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnBufferToCabinet
            // 
            this.btnBufferToCabinet.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBufferToCabinet.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnBufferToCabinet.FillColor = System.Drawing.Color.White;
            this.btnBufferToCabinet.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnBufferToCabinet.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.btnBufferToCabinet.Location = new System.Drawing.Point(1326, 53);
            this.btnBufferToCabinet.Name = "btnBufferToCabinet";
            this.btnBufferToCabinet.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.btnBufferToCabinet.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.btnBufferToCabinet.Size = new System.Drawing.Size(132, 35);
            this.btnBufferToCabinet.Style = HYC.WindowsControls.HTStyle.Custom;
            this.btnBufferToCabinet.TabIndex = 1;
            this.btnBufferToCabinet.Text = "缓存平台->料仓";
            this.btnBufferToCabinet.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnCabinetToBuffer
            // 
            this.btnCabinetToBuffer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCabinetToBuffer.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnCabinetToBuffer.FillColor = System.Drawing.Color.White;
            this.btnCabinetToBuffer.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCabinetToBuffer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.btnCabinetToBuffer.Location = new System.Drawing.Point(1326, 103);
            this.btnCabinetToBuffer.Name = "btnCabinetToBuffer";
            this.btnCabinetToBuffer.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.btnCabinetToBuffer.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.btnCabinetToBuffer.Size = new System.Drawing.Size(132, 35);
            this.btnCabinetToBuffer.Style = HYC.WindowsControls.HTStyle.Custom;
            this.btnCabinetToBuffer.TabIndex = 1;
            this.btnCabinetToBuffer.Text = "料仓->缓存平台";
            this.btnCabinetToBuffer.Click += new System.EventHandler(this.btnUnload_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.Controls.Add(this.cabinetCellCtrl1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cabinetCellCtrl2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.cabinetCellCtrl3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.cabinetCellCtrl4, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.cabinetCellCtrl5, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.cabinetCellCtrl6, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.cabinetCellCtrl7, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.cabinetCellCtrl8, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.rbtRow1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.rbtRow2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.rbtRow3, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.rbtRow4, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.rbtRow5, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.rbtRow6, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.rbtRow7, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.rbtRow8, 1, 7);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 8;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1313, 815);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // rbtRow1
            // 
            this.rbtRow1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbtRow1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rbtRow1.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.rbtRow1.Location = new System.Drawing.Point(1214, 1);
            this.rbtRow1.Margin = new System.Windows.Forms.Padding(1);
            this.rbtRow1.Name = "rbtRow1";
            this.rbtRow1.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.rbtRow1.Size = new System.Drawing.Size(98, 99);
            this.rbtRow1.TabIndex = 2;
            this.rbtRow1.Tag = "0";
            this.rbtRow1.ValueChanged += new HYC.WindowsControls.HTRadioButton.OnValueChanged(this.rbtRow1_ValueChanged);
            // 
            // rbtRow2
            // 
            this.rbtRow2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbtRow2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rbtRow2.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.rbtRow2.Location = new System.Drawing.Point(1214, 102);
            this.rbtRow2.Margin = new System.Windows.Forms.Padding(1);
            this.rbtRow2.Name = "rbtRow2";
            this.rbtRow2.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.rbtRow2.Size = new System.Drawing.Size(98, 99);
            this.rbtRow2.TabIndex = 2;
            this.rbtRow2.Tag = "1";
            this.rbtRow2.ValueChanged += new HYC.WindowsControls.HTRadioButton.OnValueChanged(this.rbtRow1_ValueChanged);
            // 
            // rbtRow3
            // 
            this.rbtRow3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbtRow3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rbtRow3.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.rbtRow3.Location = new System.Drawing.Point(1214, 203);
            this.rbtRow3.Margin = new System.Windows.Forms.Padding(1);
            this.rbtRow3.Name = "rbtRow3";
            this.rbtRow3.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.rbtRow3.Size = new System.Drawing.Size(98, 99);
            this.rbtRow3.TabIndex = 2;
            this.rbtRow3.Tag = "2";
            this.rbtRow3.ValueChanged += new HYC.WindowsControls.HTRadioButton.OnValueChanged(this.rbtRow1_ValueChanged);
            // 
            // rbtRow4
            // 
            this.rbtRow4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbtRow4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rbtRow4.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.rbtRow4.Location = new System.Drawing.Point(1214, 304);
            this.rbtRow4.Margin = new System.Windows.Forms.Padding(1);
            this.rbtRow4.Name = "rbtRow4";
            this.rbtRow4.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.rbtRow4.Size = new System.Drawing.Size(98, 99);
            this.rbtRow4.TabIndex = 2;
            this.rbtRow4.Tag = "3";
            this.rbtRow4.ValueChanged += new HYC.WindowsControls.HTRadioButton.OnValueChanged(this.rbtRow1_ValueChanged);
            // 
            // rbtRow5
            // 
            this.rbtRow5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbtRow5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rbtRow5.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.rbtRow5.Location = new System.Drawing.Point(1214, 405);
            this.rbtRow5.Margin = new System.Windows.Forms.Padding(1);
            this.rbtRow5.Name = "rbtRow5";
            this.rbtRow5.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.rbtRow5.Size = new System.Drawing.Size(98, 99);
            this.rbtRow5.TabIndex = 2;
            this.rbtRow5.Tag = "4";
            this.rbtRow5.ValueChanged += new HYC.WindowsControls.HTRadioButton.OnValueChanged(this.rbtRow1_ValueChanged);
            // 
            // rbtRow6
            // 
            this.rbtRow6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbtRow6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rbtRow6.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.rbtRow6.Location = new System.Drawing.Point(1214, 506);
            this.rbtRow6.Margin = new System.Windows.Forms.Padding(1);
            this.rbtRow6.Name = "rbtRow6";
            this.rbtRow6.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.rbtRow6.Size = new System.Drawing.Size(98, 99);
            this.rbtRow6.TabIndex = 2;
            this.rbtRow6.Tag = "5";
            this.rbtRow6.ValueChanged += new HYC.WindowsControls.HTRadioButton.OnValueChanged(this.rbtRow1_ValueChanged);
            // 
            // rbtRow7
            // 
            this.rbtRow7.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbtRow7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rbtRow7.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.rbtRow7.Location = new System.Drawing.Point(1214, 607);
            this.rbtRow7.Margin = new System.Windows.Forms.Padding(1);
            this.rbtRow7.Name = "rbtRow7";
            this.rbtRow7.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.rbtRow7.Size = new System.Drawing.Size(98, 99);
            this.rbtRow7.TabIndex = 2;
            this.rbtRow7.Tag = "6";
            this.rbtRow7.ValueChanged += new HYC.WindowsControls.HTRadioButton.OnValueChanged(this.rbtRow1_ValueChanged);
            // 
            // rbtRow8
            // 
            this.rbtRow8.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbtRow8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rbtRow8.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.rbtRow8.Location = new System.Drawing.Point(1214, 708);
            this.rbtRow8.Margin = new System.Windows.Forms.Padding(1);
            this.rbtRow8.Name = "rbtRow8";
            this.rbtRow8.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.rbtRow8.Size = new System.Drawing.Size(98, 106);
            this.rbtRow8.TabIndex = 2;
            this.rbtRow8.Tag = "7";
            this.rbtRow8.ValueChanged += new HYC.WindowsControls.HTRadioButton.OnValueChanged(this.rbtRow1_ValueChanged);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 138F));
            this.tableLayoutPanel2.Controls.Add(this.btnBufferToCabinet, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.btnCabinetToBuffer, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.panelEx1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnUpdate, 1, 9);
            this.tableLayoutPanel2.Controls.Add(this.btnCabinetToUvw, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.btnUvwToCabinet, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this.btnUvwToCabinetToUvw, 1, 5);
            this.tableLayoutPanel2.Controls.Add(this.btnPause, 1, 7);
            this.tableLayoutPanel2.Controls.Add(this.btnRun, 1, 6);
            this.tableLayoutPanel2.Controls.Add(this.btnStop, 1, 8);
            this.tableLayoutPanel2.Controls.Add(this.btnBufferStageOut, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 10;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1461, 825);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // panelEx1
            // 
            this.panelEx1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panelEx1.BorderSize = 1;
            this.panelEx1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelEx1.Controls.Add(this.tableLayoutPanel1);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.FillColor = System.Drawing.SystemColors.Control;
            this.panelEx1.Location = new System.Drawing.Point(1, 1);
            this.panelEx1.Margin = new System.Windows.Forms.Padding(1);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Padding = new System.Windows.Forms.Padding(3);
            this.tableLayoutPanel2.SetRowSpan(this.panelEx1, 10);
            this.panelEx1.Size = new System.Drawing.Size(1321, 823);
            this.panelEx1.TabIndex = 2;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUpdate.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnUpdate.FillColor = System.Drawing.Color.White;
            this.btnUpdate.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnUpdate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.btnUpdate.Location = new System.Drawing.Point(1326, 453);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.btnUpdate.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.btnUpdate.Size = new System.Drawing.Size(132, 35);
            this.btnUpdate.Style = HYC.WindowsControls.HTStyle.Custom;
            this.btnUpdate.TabIndex = 1;
            this.btnUpdate.Text = "刷新";
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnCabinetToUvw
            // 
            this.btnCabinetToUvw.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCabinetToUvw.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnCabinetToUvw.FillColor = System.Drawing.Color.White;
            this.btnCabinetToUvw.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCabinetToUvw.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.btnCabinetToUvw.Location = new System.Drawing.Point(1326, 153);
            this.btnCabinetToUvw.Name = "btnCabinetToUvw";
            this.btnCabinetToUvw.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.btnCabinetToUvw.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.btnCabinetToUvw.Size = new System.Drawing.Size(132, 35);
            this.btnCabinetToUvw.Style = HYC.WindowsControls.HTStyle.Custom;
            this.btnCabinetToUvw.TabIndex = 1;
            this.btnCabinetToUvw.Text = "料仓->UVW";
            this.btnCabinetToUvw.Click += new System.EventHandler(this.btnCabinetToUvw_Click);
            // 
            // btnUvwToCabinet
            // 
            this.btnUvwToCabinet.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUvwToCabinet.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnUvwToCabinet.FillColor = System.Drawing.Color.White;
            this.btnUvwToCabinet.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnUvwToCabinet.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.btnUvwToCabinet.Location = new System.Drawing.Point(1326, 203);
            this.btnUvwToCabinet.Name = "btnUvwToCabinet";
            this.btnUvwToCabinet.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.btnUvwToCabinet.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.btnUvwToCabinet.Size = new System.Drawing.Size(132, 35);
            this.btnUvwToCabinet.Style = HYC.WindowsControls.HTStyle.Custom;
            this.btnUvwToCabinet.TabIndex = 1;
            this.btnUvwToCabinet.Text = "UVW->料仓";
            this.btnUvwToCabinet.Click += new System.EventHandler(this.btnUvwToCabinet_Click);
            // 
            // btnUvwToCabinetToUvw
            // 
            this.btnUvwToCabinetToUvw.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUvwToCabinetToUvw.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnUvwToCabinetToUvw.FillColor = System.Drawing.Color.White;
            this.btnUvwToCabinetToUvw.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnUvwToCabinetToUvw.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.btnUvwToCabinetToUvw.Location = new System.Drawing.Point(1326, 253);
            this.btnUvwToCabinetToUvw.Name = "btnUvwToCabinetToUvw";
            this.btnUvwToCabinetToUvw.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.btnUvwToCabinetToUvw.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.btnUvwToCabinetToUvw.Size = new System.Drawing.Size(132, 35);
            this.btnUvwToCabinetToUvw.Style = HYC.WindowsControls.HTStyle.Custom;
            this.btnUvwToCabinetToUvw.TabIndex = 1;
            this.btnUvwToCabinetToUvw.Text = "UVW->料仓->UVW";
            this.btnUvwToCabinetToUvw.Click += new System.EventHandler(this.btnUvwToCabinetToUvw_Click);
            // 
            // btnPause
            // 
            this.btnPause.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPause.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnPause.FillColor = System.Drawing.Color.White;
            this.btnPause.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPause.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.btnPause.Location = new System.Drawing.Point(1326, 353);
            this.btnPause.Name = "btnPause";
            this.btnPause.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.btnPause.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.btnPause.Size = new System.Drawing.Size(132, 35);
            this.btnPause.Style = HYC.WindowsControls.HTStyle.Custom;
            this.btnPause.TabIndex = 1;
            this.btnPause.Text = "暂停";
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnRun
            // 
            this.btnRun.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRun.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnRun.FillColor = System.Drawing.Color.White;
            this.btnRun.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRun.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.btnRun.Location = new System.Drawing.Point(1326, 303);
            this.btnRun.Name = "btnRun";
            this.btnRun.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.btnRun.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.btnRun.Size = new System.Drawing.Size(132, 35);
            this.btnRun.Style = HYC.WindowsControls.HTStyle.Custom;
            this.btnRun.TabIndex = 1;
            this.btnRun.Text = "启动";
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // btnStop
            // 
            this.btnStop.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStop.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnStop.FillColor = System.Drawing.Color.White;
            this.btnStop.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnStop.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.btnStop.Location = new System.Drawing.Point(1326, 403);
            this.btnStop.Name = "btnStop";
            this.btnStop.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.btnStop.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.btnStop.Size = new System.Drawing.Size(132, 35);
            this.btnStop.Style = HYC.WindowsControls.HTStyle.Custom;
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = "停止";
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // cabinetCellCtrl1
            // 
            this.cabinetCellCtrl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cabinetCellCtrl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cabinetCellCtrl1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

            this.cabinetCellCtrl1.Location = new System.Drawing.Point(1, 1);
            this.cabinetCellCtrl1.Margin = new System.Windows.Forms.Padding(1);
            this.cabinetCellCtrl1.Name = "cabinetCellCtrl1";
            this.cabinetCellCtrl1.Size = new System.Drawing.Size(1211, 99);
            this.cabinetCellCtrl1.TabIndex = 1;
            this.cabinetCellCtrl1.Tag = "0";
            // 
            // cabinetCellCtrl2
            // 
            this.cabinetCellCtrl2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cabinetCellCtrl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cabinetCellCtrl2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cabinetCellCtrl2.Location = new System.Drawing.Point(1, 102);
            this.cabinetCellCtrl2.Margin = new System.Windows.Forms.Padding(1);
            this.cabinetCellCtrl2.Name = "cabinetCellCtrl2";
            this.cabinetCellCtrl2.Size = new System.Drawing.Size(1211, 99);
            this.cabinetCellCtrl2.TabIndex = 1;
            this.cabinetCellCtrl2.Tag = "1";
            // 
            // cabinetCellCtrl3
            // 
            this.cabinetCellCtrl3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cabinetCellCtrl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cabinetCellCtrl3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cabinetCellCtrl3.Location = new System.Drawing.Point(1, 203);
            this.cabinetCellCtrl3.Margin = new System.Windows.Forms.Padding(1);
            this.cabinetCellCtrl3.Name = "cabinetCellCtrl3";
            this.cabinetCellCtrl3.Size = new System.Drawing.Size(1211, 99);
            this.cabinetCellCtrl3.TabIndex = 1;
            this.cabinetCellCtrl3.Tag = "2";
            // 
            // cabinetCellCtrl4
            // 
            this.cabinetCellCtrl4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cabinetCellCtrl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cabinetCellCtrl4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cabinetCellCtrl4.Location = new System.Drawing.Point(1, 304);
            this.cabinetCellCtrl4.Margin = new System.Windows.Forms.Padding(1);
            this.cabinetCellCtrl4.Name = "cabinetCellCtrl4";
            this.cabinetCellCtrl4.Size = new System.Drawing.Size(1211, 99);
            this.cabinetCellCtrl4.TabIndex = 1;
            this.cabinetCellCtrl4.Tag = "3";
            // 
            // cabinetCellCtrl5
            // 
            this.cabinetCellCtrl5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cabinetCellCtrl5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cabinetCellCtrl5.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cabinetCellCtrl5.Location = new System.Drawing.Point(1, 405);
            this.cabinetCellCtrl5.Margin = new System.Windows.Forms.Padding(1);
            this.cabinetCellCtrl5.Name = "cabinetCellCtrl5";
            this.cabinetCellCtrl5.Size = new System.Drawing.Size(1211, 99);
            this.cabinetCellCtrl5.TabIndex = 1;
            this.cabinetCellCtrl5.Tag = "4";
            // 
            // cabinetCellCtrl6
            // 
            this.cabinetCellCtrl6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cabinetCellCtrl6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cabinetCellCtrl6.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cabinetCellCtrl6.Location = new System.Drawing.Point(1, 506);
            this.cabinetCellCtrl6.Margin = new System.Windows.Forms.Padding(1);
            this.cabinetCellCtrl6.Name = "cabinetCellCtrl6";
            this.cabinetCellCtrl6.Size = new System.Drawing.Size(1211, 99);
            this.cabinetCellCtrl6.TabIndex = 1;
            this.cabinetCellCtrl6.Tag = "5";
            // 
            // cabinetCellCtrl7
            // 
            this.cabinetCellCtrl7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cabinetCellCtrl7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cabinetCellCtrl7.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cabinetCellCtrl7.Location = new System.Drawing.Point(1, 607);
            this.cabinetCellCtrl7.Margin = new System.Windows.Forms.Padding(1);
            this.cabinetCellCtrl7.Name = "cabinetCellCtrl7";
            this.cabinetCellCtrl7.Size = new System.Drawing.Size(1211, 99);
            this.cabinetCellCtrl7.TabIndex = 1;
            this.cabinetCellCtrl7.Tag = "6";
            // 
            // cabinetCellCtrl8
            // 
            this.cabinetCellCtrl8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cabinetCellCtrl8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cabinetCellCtrl8.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cabinetCellCtrl8.Location = new System.Drawing.Point(1, 708);
            this.cabinetCellCtrl8.Margin = new System.Windows.Forms.Padding(1);
            this.cabinetCellCtrl8.Name = "cabinetCellCtrl8";
            this.cabinetCellCtrl8.Size = new System.Drawing.Size(1211, 106);
            this.cabinetCellCtrl8.TabIndex = 1;
            this.cabinetCellCtrl8.Tag = "7";
            // 
            // btnBufferStageOut
            // 
            this.btnBufferStageOut.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBufferStageOut.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnBufferStageOut.FillColor = System.Drawing.Color.White;
            this.btnBufferStageOut.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnBufferStageOut.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.btnBufferStageOut.Location = new System.Drawing.Point(1326, 3);
            this.btnBufferStageOut.Name = "btnBufferStageOut";
            this.btnBufferStageOut.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.btnBufferStageOut.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.btnBufferStageOut.Size = new System.Drawing.Size(132, 35);
            this.btnBufferStageOut.Style = HYC.WindowsControls.HTStyle.Custom;
            this.btnBufferStageOut.TabIndex = 1;
            this.btnBufferStageOut.Text = "缓存平台出";
            this.btnBufferStageOut.Click += new System.EventHandler(this.btnBufferStageOut_Click);
            // 
            // FrmCabinet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1461, 825);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Name = "FrmCabinet";
            this.Text = "FrmCabinet";
            this.Load += new System.EventHandler(this.FrmCabinet_Load);
            this.Controls.SetChildIndex(this.tableLayoutPanel2, 0);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panelEx1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private WindowsControls.HTButton btnBufferToCabinet;
        private WindowsControls.HTButton btnCabinetToBuffer;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private UserControls.CabinetCellCtrl cabinetCellCtrl1;
        private UserControls.CabinetCellCtrl cabinetCellCtrl2;
        private UserControls.CabinetCellCtrl cabinetCellCtrl3;
        private UserControls.CabinetCellCtrl cabinetCellCtrl4;
        private UserControls.CabinetCellCtrl cabinetCellCtrl5;
        private UserControls.CabinetCellCtrl cabinetCellCtrl6;
        private UserControls.CabinetCellCtrl cabinetCellCtrl7;
        private UserControls.CabinetCellCtrl cabinetCellCtrl8;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private PanelEx panelEx1;
        private WindowsControls.HTRadioButton rbtRow1;
        private WindowsControls.HTRadioButton rbtRow2;
        private WindowsControls.HTRadioButton rbtRow3;
        private WindowsControls.HTRadioButton rbtRow4;
        private WindowsControls.HTRadioButton rbtRow5;
        private WindowsControls.HTRadioButton rbtRow6;
        private WindowsControls.HTRadioButton rbtRow7;
        private WindowsControls.HTRadioButton rbtRow8;
        private WindowsControls.HTButton btnUpdate;
        private WindowsControls.HTButton btnCabinetToUvw;
        private WindowsControls.HTButton btnUvwToCabinet;
        private WindowsControls.HTButton btnUvwToCabinetToUvw;
        private WindowsControls.HTButton btnPause;
        private WindowsControls.HTButton btnRun;
        private WindowsControls.HTButton btnStop;
        private WindowsControls.HTButton btnBufferStageOut;
    }
}