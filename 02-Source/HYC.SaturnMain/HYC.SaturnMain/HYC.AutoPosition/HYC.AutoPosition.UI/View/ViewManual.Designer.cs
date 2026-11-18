namespace HYC.AutoPosition.UI.View
{
    partial class ViewManual
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewManual));
            this.btnDetectTarget = new System.Windows.Forms.Button();
            this.btnDetectMark = new System.Windows.Forms.Button();
            this.imageViewBox = new HYC.AutoPosition.UI.Controls.ImageViewBox();
            this.tbpnlMain = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtExposureTime2 = new System.Windows.Forms.TextBox();
            this.btnSetExposureTime2 = new System.Windows.Forms.Button();
            this.btnGetExposureTime2 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnSetExposureTime1 = new System.Windows.Forms.Button();
            this.btnGetExposureTime1 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtExposureTime1 = new System.Windows.Forms.TextBox();
            this.btnManualCalibration = new System.Windows.Forms.Button();
            this.btnManualPos = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnGrabTestTarget = new System.Windows.Forms.Button();
            this.btnGrabTestMark = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cboGroup = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboStage = new System.Windows.Forms.ComboBox();
            this.txtMsg = new System.Windows.Forms.TextBox();
            this.tbpnlMain.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnDetectTarget
            // 
            resources.ApplyResources(this.btnDetectTarget, "btnDetectTarget");
            this.btnDetectTarget.Name = "btnDetectTarget";
            this.btnDetectTarget.UseVisualStyleBackColor = true;
            this.btnDetectTarget.Click += new System.EventHandler(this.btnDetectTarget_Click);
            // 
            // btnDetectMark
            // 
            resources.ApplyResources(this.btnDetectMark, "btnDetectMark");
            this.btnDetectMark.Name = "btnDetectMark";
            this.btnDetectMark.UseVisualStyleBackColor = true;
            this.btnDetectMark.Click += new System.EventHandler(this.btnDetectMark_Click);
            // 
            // imageViewBox
            // 
            resources.ApplyResources(this.imageViewBox, "imageViewBox");
            this.imageViewBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imageViewBox.CameraCount = 2;
            this.imageViewBox.GroupName = "";
            this.imageViewBox.Name = "imageViewBox";
            // 
            // tbpnlMain
            // 
            resources.ApplyResources(this.tbpnlMain, "tbpnlMain");
            this.tbpnlMain.Controls.Add(this.imageViewBox, 0, 1);
            this.tbpnlMain.Controls.Add(this.panel1, 0, 0);
            this.tbpnlMain.Controls.Add(this.txtMsg, 0, 2);
            this.tbpnlMain.Name = "tbpnlMain";
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Controls.Add(this.txtExposureTime2);
            this.panel1.Controls.Add(this.btnSetExposureTime2);
            this.panel1.Controls.Add(this.btnGetExposureTime2);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.btnSetExposureTime1);
            this.panel1.Controls.Add(this.btnGetExposureTime1);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.txtExposureTime1);
            this.panel1.Controls.Add(this.btnManualCalibration);
            this.panel1.Controls.Add(this.btnManualPos);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.btnGrabTestTarget);
            this.panel1.Controls.Add(this.btnGrabTestMark);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cboGroup);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cboStage);
            this.panel1.Controls.Add(this.btnDetectMark);
            this.panel1.Controls.Add(this.btnDetectTarget);
            this.panel1.Name = "panel1";
            // 
            // txtExposureTime2
            // 
            resources.ApplyResources(this.txtExposureTime2, "txtExposureTime2");
            this.txtExposureTime2.Name = "txtExposureTime2";
            // 
            // btnSetExposureTime2
            // 
            resources.ApplyResources(this.btnSetExposureTime2, "btnSetExposureTime2");
            this.btnSetExposureTime2.Name = "btnSetExposureTime2";
            this.btnSetExposureTime2.UseVisualStyleBackColor = true;
            this.btnSetExposureTime2.Click += new System.EventHandler(this.btnSetExposureTime2_Click);
            // 
            // btnGetExposureTime2
            // 
            resources.ApplyResources(this.btnGetExposureTime2, "btnGetExposureTime2");
            this.btnGetExposureTime2.Name = "btnGetExposureTime2";
            this.btnGetExposureTime2.UseVisualStyleBackColor = true;
            this.btnGetExposureTime2.Click += new System.EventHandler(this.btnGetExposureTime2_Click);
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // btnSetExposureTime1
            // 
            resources.ApplyResources(this.btnSetExposureTime1, "btnSetExposureTime1");
            this.btnSetExposureTime1.Name = "btnSetExposureTime1";
            this.btnSetExposureTime1.UseVisualStyleBackColor = true;
            this.btnSetExposureTime1.Click += new System.EventHandler(this.btnSetExposureTime1_Click);
            // 
            // btnGetExposureTime1
            // 
            resources.ApplyResources(this.btnGetExposureTime1, "btnGetExposureTime1");
            this.btnGetExposureTime1.Name = "btnGetExposureTime1";
            this.btnGetExposureTime1.UseVisualStyleBackColor = true;
            this.btnGetExposureTime1.Click += new System.EventHandler(this.btnGetExposureTime1_Click);
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // txtExposureTime1
            // 
            resources.ApplyResources(this.txtExposureTime1, "txtExposureTime1");
            this.txtExposureTime1.Name = "txtExposureTime1";
            // 
            // btnManualCalibration
            // 
            resources.ApplyResources(this.btnManualCalibration, "btnManualCalibration");
            this.btnManualCalibration.Name = "btnManualCalibration";
            this.btnManualCalibration.UseVisualStyleBackColor = true;
            this.btnManualCalibration.Click += new System.EventHandler(this.btnManualCalibration_Click);
            // 
            // btnManualPos
            // 
            resources.ApplyResources(this.btnManualPos, "btnManualPos");
            this.btnManualPos.Name = "btnManualPos";
            this.btnManualPos.UseVisualStyleBackColor = true;
            this.btnManualPos.Click += new System.EventHandler(this.btnManualPos_Click);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // btnGrabTestTarget
            // 
            resources.ApplyResources(this.btnGrabTestTarget, "btnGrabTestTarget");
            this.btnGrabTestTarget.Name = "btnGrabTestTarget";
            this.btnGrabTestTarget.UseVisualStyleBackColor = true;
            this.btnGrabTestTarget.Click += new System.EventHandler(this.btnGrabTestTarget_Click);
            // 
            // btnGrabTestMark
            // 
            resources.ApplyResources(this.btnGrabTestMark, "btnGrabTestMark");
            this.btnGrabTestMark.Name = "btnGrabTestMark";
            this.btnGrabTestMark.UseVisualStyleBackColor = true;
            this.btnGrabTestMark.Click += new System.EventHandler(this.btnGrabTestMark_Click);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // cboGroup
            // 
            resources.ApplyResources(this.cboGroup, "cboGroup");
            this.cboGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGroup.FormattingEnabled = true;
            this.cboGroup.Name = "cboGroup";
            this.cboGroup.SelectedIndexChanged += new System.EventHandler(this.cboGroup_SelectedIndexChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // cboStage
            // 
            resources.ApplyResources(this.cboStage, "cboStage");
            this.cboStage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStage.FormattingEnabled = true;
            this.cboStage.Name = "cboStage";
            this.cboStage.SelectedIndexChanged += new System.EventHandler(this.cboStage_SelectedIndexChanged);
            // 
            // txtMsg
            // 
            resources.ApplyResources(this.txtMsg, "txtMsg");
            this.txtMsg.Name = "txtMsg";
            this.txtMsg.ReadOnly = true;
            // 
            // ViewManual
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tbpnlMain);
            this.Name = "ViewManual";
            this.tbpnlMain.ResumeLayout(false);
            this.tbpnlMain.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnDetectTarget;
        private System.Windows.Forms.Button btnDetectMark;
        private HYC.AutoPosition.UI.Controls.ImageViewBox imageViewBox;
        private System.Windows.Forms.TableLayoutPanel tbpnlMain;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtMsg;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboGroup;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboStage;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnGrabTestTarget;
        private System.Windows.Forms.Button btnGrabTestMark;
        private System.Windows.Forms.Button btnManualPos;
        private System.Windows.Forms.Button btnManualCalibration;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtExposureTime1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnSetExposureTime1;
        private System.Windows.Forms.Button btnGetExposureTime1;
        private System.Windows.Forms.Button btnSetExposureTime2;
        private System.Windows.Forms.Button btnGetExposureTime2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtExposureTime2;
    }
}