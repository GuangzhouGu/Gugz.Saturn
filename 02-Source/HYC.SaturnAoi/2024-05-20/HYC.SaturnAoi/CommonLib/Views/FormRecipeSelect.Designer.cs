namespace CommonLib.Views
{
    partial class FormRecipeSelect
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnCacel = new HYC.WindowsControls.Buttons.HCButton2();
            this.btnConfirm = new HYC.WindowsControls.Buttons.HCButton2();
            this.btnRecipeManage = new HYC.WindowsControls.Buttons.HCButton2();
            this.lstRecipes = new System.Windows.Forms.ListBox();
            this.btnLoad = new HYC.WindowsControls.Buttons.HCButton2();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(12, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "请从下方列表中选择机种：";
            // 
            // btnCacel
            // 
            this.btnCacel.CustomerInformation = null;
            this.btnCacel.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCacel.ForeColor = System.Drawing.Color.Black;
            this.btnCacel.Location = new System.Drawing.Point(139, 470);
            this.btnCacel.Name = "btnCacel";
            this.btnCacel.OriginalColor = System.Drawing.Color.White;
            this.btnCacel.Size = new System.Drawing.Size(90, 34);
            this.btnCacel.TabIndex = 5;
            this.btnCacel.Text = "取消";
            this.btnCacel.Click += new System.EventHandler(this.btnCacel_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.CustomerInformation = null;
            this.btnConfirm.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnConfirm.ForeColor = System.Drawing.Color.Black;
            this.btnConfirm.Location = new System.Drawing.Point(15, 470);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.OriginalColor = System.Drawing.Color.White;
            this.btnConfirm.Size = new System.Drawing.Size(90, 34);
            this.btnConfirm.TabIndex = 6;
            this.btnConfirm.Text = "确定";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnRecipeManage
            // 
            this.btnRecipeManage.CustomerInformation = null;
            this.btnRecipeManage.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRecipeManage.ForeColor = System.Drawing.Color.Black;
            this.btnRecipeManage.Location = new System.Drawing.Point(389, 470);
            this.btnRecipeManage.Name = "btnRecipeManage";
            this.btnRecipeManage.OriginalColor = System.Drawing.Color.White;
            this.btnRecipeManage.Size = new System.Drawing.Size(90, 34);
            this.btnRecipeManage.TabIndex = 5;
            this.btnRecipeManage.Text = "机种管理";
            this.btnRecipeManage.Click += new System.EventHandler(this.btnRecipeManage_Click);
            // 
            // lstRecipes
            // 
            this.lstRecipes.FormattingEnabled = true;
            this.lstRecipes.ItemHeight = 17;
            this.lstRecipes.Location = new System.Drawing.Point(15, 64);
            this.lstRecipes.Name = "lstRecipes";
            this.lstRecipes.Size = new System.Drawing.Size(464, 395);
            this.lstRecipes.TabIndex = 7;
            // 
            // btnLoad
            // 
            this.btnLoad.CustomerInformation = null;
            this.btnLoad.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnLoad.ForeColor = System.Drawing.Color.Black;
            this.btnLoad.Location = new System.Drawing.Point(262, 470);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.OriginalColor = System.Drawing.Color.White;
            this.btnLoad.Size = new System.Drawing.Size(90, 34);
            this.btnLoad.TabIndex = 5;
            this.btnLoad.Text = "重新加载";
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // FormRecipeSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(491, 539);
            this.CloseFormTimeLag = 100;
            this.Controls.Add(this.lstRecipes);
            this.Controls.Add(this.btnRecipeManage);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.btnCacel);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.label1);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "FormRecipeSelect";
            this.ShowFormTimelag = 100;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormRecipeSelect";
            this.Load += new System.EventHandler(this.FormRecipeSelect_Load);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.btnConfirm, 0);
            this.Controls.SetChildIndex(this.btnCacel, 0);
            this.Controls.SetChildIndex(this.btnLoad, 0);
            this.Controls.SetChildIndex(this.btnRecipeManage, 0);
            this.Controls.SetChildIndex(this.lstRecipes, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private HYC.WindowsControls.Buttons.HCButton2 btnCacel;
        private HYC.WindowsControls.Buttons.HCButton2 btnConfirm;
        private HYC.WindowsControls.Buttons.HCButton2 btnRecipeManage;
        private System.Windows.Forms.ListBox lstRecipes;
        private HYC.WindowsControls.Buttons.HCButton2 btnLoad;
    }
}