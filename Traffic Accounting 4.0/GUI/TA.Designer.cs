namespace Traffic_Accounting
{
    partial class TA
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TA));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownDay = new System.Windows.Forms.ToolStripDropDownButton();
            this.mondayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tuesdayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wednesdayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.thursdayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fridayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saturdayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sundayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownWeek = new System.Windows.Forms.ToolStripDropDownButton();
            this.currentWeekToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.previousWeekToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripConfiguration = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripAboutButton = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToFilterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownDay,
            this.toolStripDropDownWeek,
            this.toolStripSeparator1,
            this.toolStripConfiguration,
            this.toolStripSeparator2,
            this.toolStripAboutButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.ShowItemToolTips = false;
            this.toolStrip1.Size = new System.Drawing.Size(355, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripDropDownDay
            // 
            this.toolStripDropDownDay.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mondayToolStripMenuItem,
            this.tuesdayToolStripMenuItem,
            this.wednesdayToolStripMenuItem,
            this.thursdayToolStripMenuItem,
            this.fridayToolStripMenuItem,
            this.saturdayToolStripMenuItem,
            this.sundayToolStripMenuItem});
            this.toolStripDropDownDay.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.toolStripDropDownDay.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownDay.Image")));
            this.toolStripDropDownDay.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownDay.Name = "toolStripDropDownDay";
            this.toolStripDropDownDay.Size = new System.Drawing.Size(55, 22);
            this.toolStripDropDownDay.Text = "Day";
            this.toolStripDropDownDay.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStripDay_DropDownItemClicked);
            this.toolStripDropDownDay.DropDownOpening += new System.EventHandler(this.toolStripDay_DropDownOpening);
            // 
            // mondayToolStripMenuItem
            // 
            this.mondayToolStripMenuItem.Name = "mondayToolStripMenuItem";
            this.mondayToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.mondayToolStripMenuItem.Text = "Monday";
            // 
            // tuesdayToolStripMenuItem
            // 
            this.tuesdayToolStripMenuItem.Name = "tuesdayToolStripMenuItem";
            this.tuesdayToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.tuesdayToolStripMenuItem.Text = "Tuesday";
            // 
            // wednesdayToolStripMenuItem
            // 
            this.wednesdayToolStripMenuItem.Name = "wednesdayToolStripMenuItem";
            this.wednesdayToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.wednesdayToolStripMenuItem.Text = "Wednesday";
            // 
            // thursdayToolStripMenuItem
            // 
            this.thursdayToolStripMenuItem.Name = "thursdayToolStripMenuItem";
            this.thursdayToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.thursdayToolStripMenuItem.Text = "Thursday";
            // 
            // fridayToolStripMenuItem
            // 
            this.fridayToolStripMenuItem.Name = "fridayToolStripMenuItem";
            this.fridayToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.fridayToolStripMenuItem.Text = "Friday";
            // 
            // saturdayToolStripMenuItem
            // 
            this.saturdayToolStripMenuItem.Name = "saturdayToolStripMenuItem";
            this.saturdayToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.saturdayToolStripMenuItem.Text = "Saturday";
            // 
            // sundayToolStripMenuItem
            // 
            this.sundayToolStripMenuItem.Name = "sundayToolStripMenuItem";
            this.sundayToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.sundayToolStripMenuItem.Text = "Sunday";
            // 
            // toolStripDropDownWeek
            // 
            this.toolStripDropDownWeek.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.currentWeekToolStripMenuItem,
            this.previousWeekToolStripMenuItem});
            this.toolStripDropDownWeek.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.toolStripDropDownWeek.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownWeek.Image")));
            this.toolStripDropDownWeek.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownWeek.Name = "toolStripDropDownWeek";
            this.toolStripDropDownWeek.Size = new System.Drawing.Size(63, 22);
            this.toolStripDropDownWeek.Text = "Week";
            // 
            // currentWeekToolStripMenuItem
            // 
            this.currentWeekToolStripMenuItem.Name = "currentWeekToolStripMenuItem";
            this.currentWeekToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.currentWeekToolStripMenuItem.Text = "Current week";
            this.currentWeekToolStripMenuItem.Click += new System.EventHandler(this.currentWeekToolStripMenuItem_Click);
            // 
            // previousWeekToolStripMenuItem
            // 
            this.previousWeekToolStripMenuItem.Name = "previousWeekToolStripMenuItem";
            this.previousWeekToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.previousWeekToolStripMenuItem.Text = "Previous week";
            this.previousWeekToolStripMenuItem.Click += new System.EventHandler(this.previousWeekToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripConfiguration
            // 
            this.toolStripConfiguration.Image = ((System.Drawing.Image)(resources.GetObject("toolStripConfiguration.Image")));
            this.toolStripConfiguration.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripConfiguration.Name = "toolStripConfiguration";
            this.toolStripConfiguration.Size = new System.Drawing.Size(55, 22);
            this.toolStripConfiguration.Text = "Setup";
            this.toolStripConfiguration.Click += new System.EventHandler(this.toolStripConfiguration_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripAboutButton
            // 
            this.toolStripAboutButton.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.toolStripAboutButton.Image = ((System.Drawing.Image)(resources.GetObject("toolStripAboutButton.Image")));
            this.toolStripAboutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripAboutButton.Name = "toolStripAboutButton";
            this.toolStripAboutButton.Size = new System.Drawing.Size(56, 22);
            this.toolStripAboutButton.Text = "About";
            this.toolStripAboutButton.Click += new System.EventHandler(this.toolStripAboutButton_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 407);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.ShowItemToolTips = true;
            this.statusStrip1.Size = new System.Drawing.Size(355, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(10, 17);
            this.toolStripStatusLabel1.Text = " ";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listView1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox1.Location = new System.Drawing.Point(0, 25);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(355, 382);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // listView1
            // 
            this.listView1.AllowColumnReorder = true;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listView1.ContextMenuStrip = this.contextMenuStrip1;
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(3, 36);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(349, 343);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.DoubleClick += new System.EventHandler(this.openToolStripMenuItem_Click);
            this.listView1.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView1_ColumnClick_1);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "#";
            this.columnHeader1.Width = 41;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Website";
            this.columnHeader2.Width = 124;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Used traffic";
            this.columnHeader3.Width = 151;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.addToFilterToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(151, 48);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.openToolStripMenuItem.Text = "Copy address";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // addToFilterToolStripMenuItem
            // 
            this.addToFilterToolStripMenuItem.Name = "addToFilterToolStripMenuItem";
            this.addToFilterToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.addToFilterToolStripMenuItem.Text = "Add to filter";
            this.addToFilterToolStripMenuItem.Click += new System.EventHandler(this.addToFilterToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(3, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(349, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Traffic Statistic";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label1.UseMnemonic = false;
            // 
            // TA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(355, 429);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(370, 465);
            this.Name = "TA";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Traffic Accounting 4.0";
            this.Load += new System.EventHandler(this.TA_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownDay;
        private System.Windows.Forms.ToolStripMenuItem mondayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tuesdayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wednesdayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem thursdayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fridayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saturdayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sundayToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownWeek;
        private System.Windows.Forms.ToolStripMenuItem currentWeekToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem previousWeekToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripAboutButton;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addToFilterToolStripMenuItem;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripButton toolStripConfiguration;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}