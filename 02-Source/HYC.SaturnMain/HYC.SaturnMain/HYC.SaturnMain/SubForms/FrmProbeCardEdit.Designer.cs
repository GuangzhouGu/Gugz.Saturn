
namespace HYC.SaturnMain.SubForms
{
    partial class FrmProbeCardEdit
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
            this.label5 = new System.Windows.Forms.Label();
            this.btnConfirm = new HYC.WindowsControls.HTButton();
            this.btnCancel = new HYC.WindowsControls.HTButton();
            this.tbxRFID = new HYC.WindowsControls.HTTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmxRecipeList = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxProbeName = new HYC.WindowsControls.HTTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtProbeHeadCount = new HYC.WindowsControls.HTTextBox();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(52, 140);
            this.label5.Margin = new System.Windows.Forms.Padding(1);
            this.label5.Name = "label5";
            this.label5.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.label5.Size = new System.Drawing.Size(48, 17);
            this.label5.TabIndex = 1;
            this.label5.Text = "RFID:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfirm.FillColor = System.Drawing.Color.White;
            this.btnConfirm.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnConfirm.ForeColor = System.Drawing.Color.Black;
            this.btnConfirm.Location = new System.Drawing.Point(55, 210);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.RectColor = System.Drawing.Color.Black;
            this.btnConfirm.Size = new System.Drawing.Size(85, 35);
            this.btnConfirm.Style = HYC.WindowsControls.HTStyle.Custom;
            this.btnConfirm.TabIndex = 4;
            this.btnConfirm.Text = "确认";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FillColor = System.Drawing.Color.White;
            this.btnCancel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(187, 210);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.RectColor = System.Drawing.Color.Black;
            this.btnCancel.Size = new System.Drawing.Size(85, 35);
            this.btnCancel.Style = HYC.WindowsControls.HTStyle.Custom;
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tbxRFID
            // 
            this.tbxRFID.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tbxRFID.FillColor = System.Drawing.Color.White;
            this.tbxRFID.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.tbxRFID.Location = new System.Drawing.Point(101, 136);
            this.tbxRFID.Maximum = 2147483647D;
            this.tbxRFID.Minimum = -2147483648D;
            this.tbxRFID.Name = "tbxRFID";
            this.tbxRFID.Padding = new System.Windows.Forms.Padding(5);
            this.tbxRFID.Size = new System.Drawing.Size(171, 26);
            this.tbxRFID.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 61);
            this.label1.Margin = new System.Windows.Forms.Padding(1);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.label1.Size = new System.Drawing.Size(95, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "RecipeName:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmxRecipeList
            // 
            this.cmxRecipeList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmxRecipeList.FormattingEnabled = true;
            this.cmxRecipeList.Location = new System.Drawing.Point(101, 58);
            this.cmxRecipeList.Name = "cmxRecipeList";
            this.cmxRecipeList.Size = new System.Drawing.Size(171, 25);
            this.cmxRecipeList.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 100);
            this.label2.Margin = new System.Windows.Forms.Padding(1);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.label2.Size = new System.Drawing.Size(91, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "ProbeName:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbxProbeName
            // 
            this.tbxProbeName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tbxProbeName.FillColor = System.Drawing.Color.White;
            this.tbxProbeName.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.tbxProbeName.Location = new System.Drawing.Point(101, 97);
            this.tbxProbeName.Maximum = 2147483647D;
            this.tbxProbeName.Minimum = -2147483648D;
            this.tbxProbeName.Name = "tbxProbeName";
            this.tbxProbeName.Padding = new System.Windows.Forms.Padding(5);
            this.tbxProbeName.Size = new System.Drawing.Size(171, 26);
            this.tbxProbeName.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 178);
            this.label3.Margin = new System.Windows.Forms.Padding(1);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.label3.Size = new System.Drawing.Size(69, 17);
            this.label3.TabIndex = 1;
            this.label3.Text = "压头数量:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtProbeHeadCount
            // 
            this.txtProbeHeadCount.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtProbeHeadCount.FillColor = System.Drawing.Color.White;
            this.txtProbeHeadCount.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.txtProbeHeadCount.Location = new System.Drawing.Point(101, 175);
            this.txtProbeHeadCount.Maximum = 2147483647D;
            this.txtProbeHeadCount.Minimum = -2147483648D;
            this.txtProbeHeadCount.Name = "txtProbeHeadCount";
            this.txtProbeHeadCount.Padding = new System.Windows.Forms.Padding(5);
            this.txtProbeHeadCount.Size = new System.Drawing.Size(171, 26);
            this.txtProbeHeadCount.TabIndex = 6;
            // 
            // FrmProbeCardEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(331, 264);
            this.CloseFormTimeLag = 0;
            this.Controls.Add(this.cmxRecipeList);
            this.Controls.Add(this.tbxProbeName);
            this.Controls.Add(this.txtProbeHeadCount);
            this.Controls.Add(this.tbxRFID);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label5);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "FrmProbeCardEdit";
            this.ShowFormTimelag = 0;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmCabinetEdit";
            this.Load += new System.EventHandler(this.FrmProbeCardEdit_Load);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.btnConfirm, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.tbxRFID, 0);
            this.Controls.SetChildIndex(this.txtProbeHeadCount, 0);
            this.Controls.SetChildIndex(this.tbxProbeName, 0);
            this.Controls.SetChildIndex(this.cmxRecipeList, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label5;
        private WindowsControls.HTButton btnConfirm;
        private WindowsControls.HTButton btnCancel;
        private WindowsControls.HTTextBox tbxRFID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmxRecipeList;
        private System.Windows.Forms.Label label2;
        private WindowsControls.HTTextBox tbxProbeName;
        private System.Windows.Forms.Label label3;
        private WindowsControls.HTTextBox txtProbeHeadCount;
    }
}