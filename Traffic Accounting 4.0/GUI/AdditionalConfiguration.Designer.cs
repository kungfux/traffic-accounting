namespace Traffic_Accounting.GUI
{
    partial class AdditionalConfiguration
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.txtStatPattern = new System.Windows.Forms.TextBox();
            this.lblTopPattern = new System.Windows.Forms.Label();
            this.txtTopPattern = new System.Windows.Forms.TextBox();
            this.lblStatPattern = new System.Windows.Forms.Label();
            this.txtHtmlCut = new System.Windows.Forms.TextBox();
            this.lblHtmlCut = new System.Windows.Forms.Label();
            this.txtUrlWeekly = new System.Windows.Forms.TextBox();
            this.lblUrlWeekly = new System.Windows.Forms.Label();
            this.txtUrlDaily = new System.Windows.Forms.TextBox();
            this.lblUrlDaily = new System.Windows.Forms.Label();
            this.txtMachineName = new System.Windows.Forms.TextBox();
            this.lblMachineName = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnCancel);
            this.groupBox1.Controls.Add(this.btnApply);
            this.groupBox1.Controls.Add(this.txtStatPattern);
            this.groupBox1.Controls.Add(this.lblTopPattern);
            this.groupBox1.Controls.Add(this.txtTopPattern);
            this.groupBox1.Controls.Add(this.lblStatPattern);
            this.groupBox1.Controls.Add(this.txtHtmlCut);
            this.groupBox1.Controls.Add(this.lblHtmlCut);
            this.groupBox1.Controls.Add(this.txtUrlWeekly);
            this.groupBox1.Controls.Add(this.lblUrlWeekly);
            this.groupBox1.Controls.Add(this.txtUrlDaily);
            this.groupBox1.Controls.Add(this.lblUrlDaily);
            this.groupBox1.Controls.Add(this.txtMachineName);
            this.groupBox1.Controls.Add(this.lblMachineName);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(275, 493);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Additional";
            // 
            // btnCancel
            // 
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnCancel.Location = new System.Drawing.Point(3, 460);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(269, 23);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnApply
            // 
            this.btnApply.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnApply.Location = new System.Drawing.Point(3, 437);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(269, 23);
            this.btnApply.TabIndex = 19;
            this.btnApply.Text = "Apply settings";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // txtStatPattern
            // 
            this.txtStatPattern.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtStatPattern.Location = new System.Drawing.Point(3, 397);
            this.txtStatPattern.Multiline = true;
            this.txtStatPattern.Name = "txtStatPattern";
            this.txtStatPattern.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtStatPattern.Size = new System.Drawing.Size(269, 40);
            this.txtStatPattern.TabIndex = 18;
            // 
            // lblTopPattern
            // 
            this.lblTopPattern.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTopPattern.Location = new System.Drawing.Point(3, 367);
            this.lblTopPattern.Name = "lblTopPattern";
            this.lblTopPattern.Size = new System.Drawing.Size(269, 30);
            this.lblTopPattern.TabIndex = 17;
            this.lblTopPattern.Text = "Specify \"TOP10\" pattern";
            this.lblTopPattern.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // txtTopPattern
            // 
            this.txtTopPattern.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtTopPattern.Location = new System.Drawing.Point(3, 327);
            this.txtTopPattern.Multiline = true;
            this.txtTopPattern.Name = "txtTopPattern";
            this.txtTopPattern.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtTopPattern.Size = new System.Drawing.Size(269, 40);
            this.txtTopPattern.TabIndex = 16;
            // 
            // lblStatPattern
            // 
            this.lblStatPattern.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblStatPattern.Location = new System.Drawing.Point(3, 297);
            this.lblStatPattern.Name = "lblStatPattern";
            this.lblStatPattern.Size = new System.Drawing.Size(269, 30);
            this.lblStatPattern.TabIndex = 15;
            this.lblStatPattern.Text = "Specify traffic statistics pattern";
            this.lblStatPattern.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // txtHtmlCut
            // 
            this.txtHtmlCut.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtHtmlCut.Location = new System.Drawing.Point(3, 257);
            this.txtHtmlCut.Multiline = true;
            this.txtHtmlCut.Name = "txtHtmlCut";
            this.txtHtmlCut.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtHtmlCut.Size = new System.Drawing.Size(269, 40);
            this.txtHtmlCut.TabIndex = 14;
            // 
            // lblHtmlCut
            // 
            this.lblHtmlCut.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHtmlCut.Location = new System.Drawing.Point(3, 227);
            this.lblHtmlCut.Name = "lblHtmlCut";
            this.lblHtmlCut.Size = new System.Drawing.Size(269, 30);
            this.lblHtmlCut.TabIndex = 13;
            this.lblHtmlCut.Text = "Specify cutting options to skip parsing not needed html data";
            this.lblHtmlCut.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // txtUrlWeekly
            // 
            this.txtUrlWeekly.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtUrlWeekly.Location = new System.Drawing.Point(3, 187);
            this.txtUrlWeekly.Multiline = true;
            this.txtUrlWeekly.Name = "txtUrlWeekly";
            this.txtUrlWeekly.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtUrlWeekly.Size = new System.Drawing.Size(269, 40);
            this.txtUrlWeekly.TabIndex = 6;
            // 
            // lblUrlWeekly
            // 
            this.lblUrlWeekly.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblUrlWeekly.Location = new System.Drawing.Point(3, 157);
            this.lblUrlWeekly.Name = "lblUrlWeekly";
            this.lblUrlWeekly.Size = new System.Drawing.Size(269, 30);
            this.lblUrlWeekly.TabIndex = 5;
            this.lblUrlWeekly.Text = "Specify url to get weekly traffic statistics";
            this.lblUrlWeekly.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // txtUrlDaily
            // 
            this.txtUrlDaily.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtUrlDaily.Location = new System.Drawing.Point(3, 117);
            this.txtUrlDaily.Multiline = true;
            this.txtUrlDaily.Name = "txtUrlDaily";
            this.txtUrlDaily.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtUrlDaily.Size = new System.Drawing.Size(269, 40);
            this.txtUrlDaily.TabIndex = 4;
            // 
            // lblUrlDaily
            // 
            this.lblUrlDaily.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblUrlDaily.Location = new System.Drawing.Point(3, 87);
            this.lblUrlDaily.Name = "lblUrlDaily";
            this.lblUrlDaily.Size = new System.Drawing.Size(269, 30);
            this.lblUrlDaily.TabIndex = 3;
            this.lblUrlDaily.Text = "Specify url to get daily traffic statistics";
            this.lblUrlDaily.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // txtMachineName
            // 
            this.txtMachineName.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtMachineName.Location = new System.Drawing.Point(3, 47);
            this.txtMachineName.Multiline = true;
            this.txtMachineName.Name = "txtMachineName";
            this.txtMachineName.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMachineName.Size = new System.Drawing.Size(269, 40);
            this.txtMachineName.TabIndex = 2;
            // 
            // lblMachineName
            // 
            this.lblMachineName.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblMachineName.Location = new System.Drawing.Point(3, 17);
            this.lblMachineName.Name = "lblMachineName";
            this.lblMachineName.Size = new System.Drawing.Size(269, 30);
            this.lblMachineName.TabIndex = 1;
            this.lblMachineName.Text = "Specify machine name";
            this.lblMachineName.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // AdditionalConfiguration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.groupBox1);
            this.Name = "AdditionalConfiguration";
            this.Size = new System.Drawing.Size(278, 493);
            this.Load += new System.EventHandler(this.AdditionalConfiguration_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblMachineName;
        private System.Windows.Forms.TextBox txtMachineName;
        private System.Windows.Forms.Label lblUrlDaily;
        private System.Windows.Forms.TextBox txtUrlDaily;
        private System.Windows.Forms.TextBox txtUrlWeekly;
        private System.Windows.Forms.Label lblUrlWeekly;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.TextBox txtStatPattern;
        private System.Windows.Forms.Label lblTopPattern;
        private System.Windows.Forms.TextBox txtTopPattern;
        private System.Windows.Forms.Label lblStatPattern;
        private System.Windows.Forms.TextBox txtHtmlCut;
        private System.Windows.Forms.Label lblHtmlCut;
    }
}
