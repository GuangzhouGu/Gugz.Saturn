namespace HYC.SaturnMain.Devices.EfuController
{
    partial class EFUFans
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_set = new System.Windows.Forms.Button();
            this.lbl_station1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbl_setValue = new System.Windows.Forms.Label();
            this.lbl_sts = new System.Windows.Forms.Label();
            this.lbl_actValue = new System.Windows.Forms.Label();
            this.tbx_setValue = new System.Windows.Forms.TextBox();
            this.htAnalogMeter1 = new HYC.WindowsControls.HTAnalogMeter();
            this.lbl_index = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panelEx1 = new HYC.HTPCBase.Extension.UserCtrl.PanelEx();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panelEx1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 117F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 117F));
            this.tableLayoutPanel1.Controls.Add(this.btn_set, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.lbl_station1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lbl_setValue, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.lbl_sts, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.lbl_actValue, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbx_setValue, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.htAnalogMeter1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 234F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 26.34783F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 26.34783F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 23.57438F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 23.72996F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(237, 343);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // btn_set
            // 
            this.btn_set.BackColor = System.Drawing.Color.Silver;
            this.btn_set.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_set.FlatAppearance.BorderSize = 0;
            this.btn_set.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_set.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_set.Location = new System.Drawing.Point(120, 318);
            this.btn_set.Margin = new System.Windows.Forms.Padding(1);
            this.btn_set.Name = "btn_set";
            this.btn_set.Size = new System.Drawing.Size(115, 23);
            this.btn_set.TabIndex = 24;
            this.btn_set.Text = "设置";
            this.btn_set.UseVisualStyleBackColor = false;
            this.btn_set.Click += new System.EventHandler(this.btn_set_Click);
            // 
            // lbl_station1
            // 
            this.lbl_station1.BackColor = System.Drawing.SystemColors.Control;
            this.lbl_station1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_station1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_station1.Location = new System.Drawing.Point(2, 237);
            this.lbl_station1.Margin = new System.Windows.Forms.Padding(1);
            this.lbl_station1.Name = "lbl_station1";
            this.lbl_station1.Size = new System.Drawing.Size(115, 25);
            this.lbl_station1.TabIndex = 3;
            this.lbl_station1.Text = "实际值";
            this.lbl_station1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.Control;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(2, 293);
            this.label3.Margin = new System.Windows.Forms.Padding(1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 22);
            this.label3.TabIndex = 5;
            this.label3.Text = "状态";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.SystemColors.Control;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(2, 265);
            this.label4.Margin = new System.Windows.Forms.Padding(1);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(115, 25);
            this.label4.TabIndex = 5;
            this.label4.Text = "设定值";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_setValue
            // 
            this.lbl_setValue.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lbl_setValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_setValue.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_setValue.Location = new System.Drawing.Point(119, 264);
            this.lbl_setValue.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_setValue.Name = "lbl_setValue";
            this.lbl_setValue.Size = new System.Drawing.Size(117, 27);
            this.lbl_setValue.TabIndex = 4;
            this.lbl_setValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_sts
            // 
            this.lbl_sts.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lbl_sts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_sts.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_sts.Location = new System.Drawing.Point(119, 292);
            this.lbl_sts.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_sts.Name = "lbl_sts";
            this.lbl_sts.Size = new System.Drawing.Size(117, 24);
            this.lbl_sts.TabIndex = 4;
            this.lbl_sts.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_actValue
            // 
            this.lbl_actValue.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lbl_actValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_actValue.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_actValue.Location = new System.Drawing.Point(119, 236);
            this.lbl_actValue.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_actValue.Name = "lbl_actValue";
            this.lbl_actValue.Size = new System.Drawing.Size(117, 27);
            this.lbl_actValue.TabIndex = 4;
            this.lbl_actValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbx_setValue
            // 
            this.tbx_setValue.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbx_setValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbx_setValue.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbx_setValue.Location = new System.Drawing.Point(2, 318);
            this.tbx_setValue.Margin = new System.Windows.Forms.Padding(1);
            this.tbx_setValue.Name = "tbx_setValue";
            this.tbx_setValue.Size = new System.Drawing.Size(115, 28);
            this.tbx_setValue.TabIndex = 25;
            this.tbx_setValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // htAnalogMeter1
            // 
            this.htAnalogMeter1.BodyColor = System.Drawing.Color.CornflowerBlue;
            this.tableLayoutPanel1.SetColumnSpan(this.htAnalogMeter1, 2);
            this.htAnalogMeter1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.htAnalogMeter1.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.htAnalogMeter1.Location = new System.Drawing.Point(4, 4);
            this.htAnalogMeter1.MaxValue = 1500D;
            this.htAnalogMeter1.MinValue = 0D;
            this.htAnalogMeter1.Name = "htAnalogMeter1";
            this.htAnalogMeter1.NeedleColor = System.Drawing.Color.Gainsboro;
            this.htAnalogMeter1.Renderer = null;
            this.htAnalogMeter1.Size = new System.Drawing.Size(229, 228);
            this.htAnalogMeter1.Style = HYC.WindowsControls.HTStyle.Custom;
            this.htAnalogMeter1.TabIndex = 26;
            this.htAnalogMeter1.Text = "htAnalogMeter1";
            this.htAnalogMeter1.Value = 0D;
            // 
            // lbl_index
            // 
            this.lbl_index.BackColor = System.Drawing.Color.White;
            this.lbl_index.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_index.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_index.ForeColor = System.Drawing.Color.Black;
            this.lbl_index.Location = new System.Drawing.Point(4, 4);
            this.lbl_index.Margin = new System.Windows.Forms.Padding(1);
            this.lbl_index.Name = "lbl_index";
            this.lbl_index.Size = new System.Drawing.Size(237, 28);
            this.lbl_index.TabIndex = 24;
            this.lbl_index.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.lbl_index, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.panelEx1, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.Padding = new System.Windows.Forms.Padding(3);
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(245, 381);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // panelEx1
            // 
            this.panelEx1.BorderColor = System.Drawing.Color.White;
            this.panelEx1.BorderSize = 1;
            this.panelEx1.Controls.Add(this.tableLayoutPanel1);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.FillColor = System.Drawing.SystemColors.Control;
            this.panelEx1.Location = new System.Drawing.Point(4, 34);
            this.panelEx1.Margin = new System.Windows.Forms.Padding(1);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(237, 343);
            this.panelEx1.TabIndex = 25;
            // 
            // EFUFans
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.tableLayoutPanel2);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "EFUFans";
            this.Size = new System.Drawing.Size(245, 381);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panelEx1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbl_station1;
        private System.Windows.Forms.Label lbl_actValue;
        private System.Windows.Forms.Label lbl_setValue;
        private System.Windows.Forms.Label lbl_sts;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_set;
        private System.Windows.Forms.Label lbl_index;
        private System.Windows.Forms.TextBox tbx_setValue;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private HTPCBase.Extension.UserCtrl.PanelEx panelEx1;
        private WindowsControls.HTAnalogMeter htAnalogMeter1;
    }
}
