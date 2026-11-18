namespace CommonLib.Views
{
    partial class FormRecipeAdd
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnConfirm = new HYC.WindowsControls.Buttons.HCButton2();
            this.btnCacel = new HYC.WindowsControls.Buttons.HCButton2();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(93, 60);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(259, 23);
            this.textBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "名称：";
            // 
            // btnConfirm
            // 
            this.btnConfirm.CustomerInformation = null;
            this.btnConfirm.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnConfirm.ForeColor = System.Drawing.Color.Black;
            this.btnConfirm.Location = new System.Drawing.Point(109, 100);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.OriginalColor = System.Drawing.Color.White;
            this.btnConfirm.Size = new System.Drawing.Size(90, 34);
            this.btnConfirm.TabIndex = 3;
            this.btnConfirm.Text = "确定";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnCacel
            // 
            this.btnCacel.CustomerInformation = null;
            this.btnCacel.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCacel.ForeColor = System.Drawing.Color.Black;
            this.btnCacel.Location = new System.Drawing.Point(239, 100);
            this.btnCacel.Name = "btnCacel";
            this.btnCacel.OriginalColor = System.Drawing.Color.White;
            this.btnCacel.Size = new System.Drawing.Size(90, 34);
            this.btnCacel.TabIndex = 3;
            this.btnCacel.Text = "取消";
            this.btnCacel.Click += new System.EventHandler(this.btnCacel_Click);
            // 
            // FormRecipeAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(408, 146);
            this.CloseFormTimeLag = 100;
            this.Controls.Add(this.btnCacel);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "FormRecipeAdd";
            this.ShowFormTimelag = 100;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormRecipeAdd";
            this.Controls.SetChildIndex(this.textBox1, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.btnConfirm, 0);
            this.Controls.SetChildIndex(this.btnCacel, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private HYC.WindowsControls.Buttons.HCButton2 btnConfirm;
        private HYC.WindowsControls.Buttons.HCButton2 btnCacel;
    }
}