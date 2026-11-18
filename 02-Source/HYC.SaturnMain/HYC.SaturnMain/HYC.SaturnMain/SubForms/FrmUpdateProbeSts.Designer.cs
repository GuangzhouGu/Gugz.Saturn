
namespace HYC.SaturnMain.SubForms
{
    partial class FrmUpdateProbeSts
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
            this.btnUpdate = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmxProbeSts = new System.Windows.Forms.ComboBox();
            this.btnInsert = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.cmxRecipeList = new System.Windows.Forms.ComboBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tbxProbeName = new System.Windows.Forms.TextBox();
            this.tbxRFID = new System.Windows.Forms.TextBox();
            this.cmxProbeSts2 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(247, 9);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(68, 28);
            this.btnUpdate.TabIndex = 1;
            this.btnUpdate.Text = "更新";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Probe状态：";
            // 
            // cmxProbeSts
            // 
            this.cmxProbeSts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmxProbeSts.FormattingEnabled = true;
            this.cmxProbeSts.Items.AddRange(new object[] {
            "InUvw",
            "InCabinet",
            "OutEqp"});
            this.cmxProbeSts.Location = new System.Drawing.Point(103, 11);
            this.cmxProbeSts.Name = "cmxProbeSts";
            this.cmxProbeSts.Size = new System.Drawing.Size(137, 25);
            this.cmxProbeSts.TabIndex = 3;
            // 
            // btnInsert
            // 
            this.btnInsert.Location = new System.Drawing.Point(248, 10);
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Size = new System.Drawing.Size(68, 28);
            this.btnInsert.TabIndex = 6;
            this.btnInsert.Text = "插入";
            this.btnInsert.UseVisualStyleBackColor = true;
            this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(5, 48);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(94, 17);
            this.label7.TabIndex = 4;
            this.label7.Text = "RecipeName：";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmxRecipeList
            // 
            this.cmxRecipeList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmxRecipeList.FormattingEnabled = true;
            this.cmxRecipeList.Location = new System.Drawing.Point(105, 45);
            this.cmxRecipeList.Name = "cmxRecipeList";
            this.cmxRecipeList.Size = new System.Drawing.Size(137, 25);
            this.cmxRecipeList.TabIndex = 9;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(6, 39);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(340, 172);
            this.tabControl1.TabIndex = 10;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnUpdate);
            this.tabPage1.Controls.Add(this.cmxProbeSts);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(332, 142);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "更新";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tbxProbeName);
            this.tabPage2.Controls.Add(this.tbxRFID);
            this.tabPage2.Controls.Add(this.cmxProbeSts2);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.btnInsert);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.cmxRecipeList);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(332, 142);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "插入";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tbxProbeName
            // 
            this.tbxProbeName.Location = new System.Drawing.Point(105, 78);
            this.tbxProbeName.Name = "tbxProbeName";
            this.tbxProbeName.Size = new System.Drawing.Size(137, 23);
            this.tbxProbeName.TabIndex = 10;
            // 
            // tbxRFID
            // 
            this.tbxRFID.Location = new System.Drawing.Point(105, 110);
            this.tbxRFID.Name = "tbxRFID";
            this.tbxRFID.Size = new System.Drawing.Size(137, 23);
            this.tbxRFID.TabIndex = 10;
            // 
            // cmxProbeSts2
            // 
            this.cmxProbeSts2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmxProbeSts2.FormattingEnabled = true;
            this.cmxProbeSts2.Items.AddRange(new object[] {
            "InUvw",
            "InCabinet",
            "OutEqp"});
            this.cmxProbeSts2.Location = new System.Drawing.Point(105, 11);
            this.cmxProbeSts2.Name = "cmxProbeSts2";
            this.cmxProbeSts2.Size = new System.Drawing.Size(137, 25);
            this.cmxProbeSts2.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 17);
            this.label2.TabIndex = 7;
            this.label2.Text = "Probe状态：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 81);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 17);
            this.label4.TabIndex = 4;
            this.label4.Text = "ProbeName：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(52, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "RFID：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmUpdateProbeSts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 227);
            this.CloseFormTimeLag = 0;
            this.Controls.Add(this.tabControl1);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "FrmUpdateProbeSts";
            this.ShowFormTimelag = 0;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmUpdateProbeSts";
            this.Load += new System.EventHandler(this.FrmUpdateProbeSts_Load);
            this.Controls.SetChildIndex(this.tabControl1, 0);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmxProbeSts;
        private System.Windows.Forms.Button btnInsert;
        private System.Windows.Forms.ComboBox cmxRecipeList;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ComboBox cmxProbeSts2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxRFID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbxProbeName;
        private System.Windows.Forms.Label label4;
    }
}