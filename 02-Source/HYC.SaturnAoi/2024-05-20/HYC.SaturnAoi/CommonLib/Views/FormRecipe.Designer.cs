namespace CommonLib.Views
{
    partial class FormRecipe
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
            this.lstRecipes = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAdd = new HYC.WindowsControls.Buttons.HCButton2();
            this.btnDelete = new HYC.WindowsControls.Buttons.HCButton2();
            this.btnRename = new HYC.WindowsControls.Buttons.HCButton2();
            this.SuspendLayout();
            // 
            // lstRecipes
            // 
            this.lstRecipes.FormattingEnabled = true;
            this.lstRecipes.ItemHeight = 17;
            this.lstRecipes.Location = new System.Drawing.Point(12, 65);
            this.lstRecipes.Name = "lstRecipes";
            this.lstRecipes.Size = new System.Drawing.Size(396, 599);
            this.lstRecipes.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "机种列表：";
            // 
            // btnAdd
            // 
            this.btnAdd.CustomerInformation = null;
            this.btnAdd.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAdd.ForeColor = System.Drawing.Color.Black;
            this.btnAdd.Location = new System.Drawing.Point(432, 65);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.OriginalColor = System.Drawing.Color.White;
            this.btnAdd.Size = new System.Drawing.Size(113, 37);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "新增";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.CustomerInformation = null;
            this.btnDelete.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDelete.ForeColor = System.Drawing.Color.Black;
            this.btnDelete.Location = new System.Drawing.Point(432, 151);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.OriginalColor = System.Drawing.Color.White;
            this.btnDelete.Size = new System.Drawing.Size(113, 37);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "删除";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnRename
            // 
            this.btnRename.CustomerInformation = null;
            this.btnRename.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRename.ForeColor = System.Drawing.Color.Black;
            this.btnRename.Location = new System.Drawing.Point(432, 108);
            this.btnRename.Name = "btnRename";
            this.btnRename.OriginalColor = System.Drawing.Color.White;
            this.btnRename.Size = new System.Drawing.Size(113, 37);
            this.btnRename.TabIndex = 4;
            this.btnRename.Text = "重命名";
            this.btnRename.Click += new System.EventHandler(this.btnRename_Click);
            // 
            // FormRecipe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(561, 669);
            this.CloseFormTimeLag = 100;
            this.Controls.Add(this.btnRename);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstRecipes);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "FormRecipe";
            this.ShowFormTimelag = 100;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormRecipe";
            this.Load += new System.EventHandler(this.FormRecipe_Load);
            this.Controls.SetChildIndex(this.lstRecipes, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.btnAdd, 0);
            this.Controls.SetChildIndex(this.btnDelete, 0);
            this.Controls.SetChildIndex(this.btnRename, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstRecipes;
        private System.Windows.Forms.Label label1;
        private HYC.WindowsControls.Buttons.HCButton2 btnAdd;
        private HYC.WindowsControls.Buttons.HCButton2 btnDelete;
        private HYC.WindowsControls.Buttons.HCButton2 btnRename;
    }
}