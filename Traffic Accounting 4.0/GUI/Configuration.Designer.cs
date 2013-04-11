namespace Traffic_Accounting
{
    partial class Configuration
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
            this.chAutoStart = new System.Windows.Forms.CheckBox();
            this.groupGeneral = new System.Windows.Forms.GroupBox();
            this.cmbLanguage = new System.Windows.Forms.ComboBox();
            this.cmbLocation = new System.Windows.Forms.ComboBox();
            this.lLanguage = new System.Windows.Forms.Label();
            this.lLocation = new System.Windows.Forms.Label();
            this.chCache = new System.Windows.Forms.CheckBox();
            this.groupTraffic = new System.Windows.Forms.GroupBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.chFilter = new System.Windows.Forms.CheckBox();
            this.chTop10 = new System.Windows.Forms.CheckBox();
            this.chRoundUp = new System.Windows.Forms.CheckBox();
            this.lTrafficLimit = new System.Windows.Forms.Label();
            this.updTrafficLimit = new System.Windows.Forms.NumericUpDown();
            this.groupSystemTray = new System.Windows.Forms.GroupBox();
            this.cmbFont = new System.Windows.Forms.ComboBox();
            this.lFontName = new System.Windows.Forms.Label();
            this.cmbDigitsSize = new System.Windows.Forms.ComboBox();
            this.lDigitsSize = new System.Windows.Forms.Label();
            this.chNotify = new System.Windows.Forms.CheckBox();
            this.cmbDigitsColor = new System.Windows.Forms.ComboBox();
            this.lDigitsColor = new System.Windows.Forms.Label();
            this.cmbIconFashion = new System.Windows.Forms.ComboBox();
            this.lIconFashion = new System.Windows.Forms.Label();
            this.chDigits = new System.Windows.Forms.CheckBox();
            this.updTrafficWarning = new System.Windows.Forms.NumericUpDown();
            this.updTrafficCritical = new System.Windows.Forms.NumericUpDown();
            this.updTrafficExceed = new System.Windows.Forms.NumericUpDown();
            this.lTrafficRanges = new System.Windows.Forms.Label();
            this.groupSystem = new System.Windows.Forms.GroupBox();
            this.btnAdditional = new System.Windows.Forms.Button();
            this.chTrace = new System.Windows.Forms.CheckBox();
            this.groupGeneral.SuspendLayout();
            this.groupTraffic.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.updTrafficLimit)).BeginInit();
            this.groupSystemTray.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.updTrafficWarning)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.updTrafficCritical)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.updTrafficExceed)).BeginInit();
            this.groupSystem.SuspendLayout();
            this.SuspendLayout();
            // 
            // chAutoStart
            // 
            this.chAutoStart.AutoSize = true;
            this.chAutoStart.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.chAutoStart.Location = new System.Drawing.Point(13, 20);
            this.chAutoStart.Name = "chAutoStart";
            this.chAutoStart.Size = new System.Drawing.Size(213, 17);
            this.chAutoStart.TabIndex = 2;
            this.chAutoStart.Text = "Start automatically at Windows startup";
            this.chAutoStart.UseVisualStyleBackColor = true;
            this.chAutoStart.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // groupGeneral
            // 
            this.groupGeneral.Controls.Add(this.cmbLanguage);
            this.groupGeneral.Controls.Add(this.cmbLocation);
            this.groupGeneral.Controls.Add(this.lLanguage);
            this.groupGeneral.Controls.Add(this.lLocation);
            this.groupGeneral.Controls.Add(this.chAutoStart);
            this.groupGeneral.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupGeneral.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupGeneral.Location = new System.Drawing.Point(0, 0);
            this.groupGeneral.Name = "groupGeneral";
            this.groupGeneral.Size = new System.Drawing.Size(278, 106);
            this.groupGeneral.TabIndex = 1;
            this.groupGeneral.TabStop = false;
            this.groupGeneral.Text = " General ";
            // 
            // cmbLanguage
            // 
            this.cmbLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLanguage.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmbLanguage.FormattingEnabled = true;
            this.cmbLanguage.Items.AddRange(new object[] {
            "English",
            "Русский"});
            this.cmbLanguage.Location = new System.Drawing.Point(129, 71);
            this.cmbLanguage.Name = "cmbLanguage";
            this.cmbLanguage.Size = new System.Drawing.Size(121, 21);
            this.cmbLanguage.TabIndex = 4;
            this.cmbLanguage.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // cmbLocation
            // 
            this.cmbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocation.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmbLocation.FormattingEnabled = true;
            this.cmbLocation.Location = new System.Drawing.Point(129, 44);
            this.cmbLocation.Name = "cmbLocation";
            this.cmbLocation.Size = new System.Drawing.Size(121, 21);
            this.cmbLocation.TabIndex = 4;
            this.cmbLocation.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            // 
            // lLanguage
            // 
            this.lLanguage.AutoSize = true;
            this.lLanguage.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lLanguage.Location = new System.Drawing.Point(6, 74);
            this.lLanguage.Name = "lLanguage";
            this.lLanguage.Size = new System.Drawing.Size(54, 13);
            this.lLanguage.TabIndex = 0;
            this.lLanguage.Text = "Language";
            // 
            // lLocation
            // 
            this.lLocation.AutoSize = true;
            this.lLocation.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lLocation.Location = new System.Drawing.Point(6, 47);
            this.lLocation.Name = "lLocation";
            this.lLocation.Size = new System.Drawing.Size(69, 13);
            this.lLocation.TabIndex = 3;
            this.lLocation.Text = "Your location";
            // 
            // chCache
            // 
            this.chCache.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.chCache.Location = new System.Drawing.Point(13, 127);
            this.chCache.Name = "chCache";
            this.chCache.Size = new System.Drawing.Size(234, 36);
            this.chCache.TabIndex = 17;
            this.chCache.Text = "Use local cache to increase performance";
            this.chCache.UseVisualStyleBackColor = true;
            this.chCache.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // groupTraffic
            // 
            this.groupTraffic.Controls.Add(this.chCache);
            this.groupTraffic.Controls.Add(this.linkLabel1);
            this.groupTraffic.Controls.Add(this.chFilter);
            this.groupTraffic.Controls.Add(this.chTop10);
            this.groupTraffic.Controls.Add(this.chRoundUp);
            this.groupTraffic.Controls.Add(this.lTrafficLimit);
            this.groupTraffic.Controls.Add(this.updTrafficLimit);
            this.groupTraffic.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupTraffic.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupTraffic.Location = new System.Drawing.Point(0, 106);
            this.groupTraffic.Name = "groupTraffic";
            this.groupTraffic.Size = new System.Drawing.Size(278, 173);
            this.groupTraffic.TabIndex = 5;
            this.groupTraffic.TabStop = false;
            this.groupTraffic.Text = "Traffic ";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.linkLabel1.Location = new System.Drawing.Point(28, 111);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(182, 13);
            this.linkLabel1.TabIndex = 10;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "What websites are already is in filter";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // chFilter
            // 
            this.chFilter.AutoSize = true;
            this.chFilter.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.chFilter.Location = new System.Drawing.Point(13, 91);
            this.chFilter.Name = "chFilter";
            this.chFilter.Size = new System.Drawing.Size(101, 17);
            this.chFilter.TabIndex = 9;
            this.chFilter.Text = "Use traffic filter";
            this.chFilter.UseVisualStyleBackColor = true;
            this.chFilter.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // chTop10
            // 
            this.chTop10.AutoSize = true;
            this.chTop10.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.chTop10.Location = new System.Drawing.Point(13, 68);
            this.chTop10.Name = "chTop10";
            this.chTop10.Size = new System.Drawing.Size(150, 17);
            this.chTop10.TabIndex = 8;
            this.chTop10.Text = "Check your position in top";
            this.chTop10.UseVisualStyleBackColor = true;
            this.chTop10.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // chRoundUp
            // 
            this.chRoundUp.AutoSize = true;
            this.chRoundUp.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.chRoundUp.Location = new System.Drawing.Point(13, 45);
            this.chRoundUp.Name = "chRoundUp";
            this.chRoundUp.Size = new System.Drawing.Size(138, 17);
            this.chRoundUp.TabIndex = 7;
            this.chRoundUp.Text = "Round up traffic values";
            this.chRoundUp.UseVisualStyleBackColor = true;
            this.chRoundUp.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // lTrafficLimit
            // 
            this.lTrafficLimit.AutoSize = true;
            this.lTrafficLimit.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lTrafficLimit.Location = new System.Drawing.Point(93, 21);
            this.lTrafficLimit.Name = "lTrafficLimit";
            this.lTrafficLimit.Size = new System.Drawing.Size(125, 13);
            this.lTrafficLimit.TabIndex = 0;
            this.lTrafficLimit.Text = "Traffic limit for one week";
            // 
            // updTrafficLimit
            // 
            this.updTrafficLimit.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.updTrafficLimit.Location = new System.Drawing.Point(13, 19);
            this.updTrafficLimit.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.updTrafficLimit.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.updTrafficLimit.Name = "updTrafficLimit";
            this.updTrafficLimit.Size = new System.Drawing.Size(74, 21);
            this.updTrafficLimit.TabIndex = 6;
            this.updTrafficLimit.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.updTrafficLimit.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            // 
            // groupSystemTray
            // 
            this.groupSystemTray.Controls.Add(this.cmbFont);
            this.groupSystemTray.Controls.Add(this.lFontName);
            this.groupSystemTray.Controls.Add(this.cmbDigitsSize);
            this.groupSystemTray.Controls.Add(this.lDigitsSize);
            this.groupSystemTray.Controls.Add(this.chNotify);
            this.groupSystemTray.Controls.Add(this.cmbDigitsColor);
            this.groupSystemTray.Controls.Add(this.lDigitsColor);
            this.groupSystemTray.Controls.Add(this.cmbIconFashion);
            this.groupSystemTray.Controls.Add(this.lIconFashion);
            this.groupSystemTray.Controls.Add(this.chDigits);
            this.groupSystemTray.Controls.Add(this.updTrafficWarning);
            this.groupSystemTray.Controls.Add(this.updTrafficCritical);
            this.groupSystemTray.Controls.Add(this.updTrafficExceed);
            this.groupSystemTray.Controls.Add(this.lTrafficRanges);
            this.groupSystemTray.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupSystemTray.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupSystemTray.Location = new System.Drawing.Point(0, 279);
            this.groupSystemTray.Name = "groupSystemTray";
            this.groupSystemTray.Size = new System.Drawing.Size(278, 229);
            this.groupSystemTray.TabIndex = 9;
            this.groupSystemTray.TabStop = false;
            this.groupSystemTray.Text = "System Tray ";
            // 
            // cmbFont
            // 
            this.cmbFont.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFont.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmbFont.FormattingEnabled = true;
            this.cmbFont.Location = new System.Drawing.Point(129, 149);
            this.cmbFont.Name = "cmbFont";
            this.cmbFont.Size = new System.Drawing.Size(121, 21);
            this.cmbFont.TabIndex = 20;
            this.cmbFont.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            // 
            // lFontName
            // 
            this.lFontName.AutoSize = true;
            this.lFontName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lFontName.Location = new System.Drawing.Point(10, 152);
            this.lFontName.Name = "lFontName";
            this.lFontName.Size = new System.Drawing.Size(58, 13);
            this.lFontName.TabIndex = 19;
            this.lFontName.Text = "Font name";
            // 
            // cmbDigitsSize
            // 
            this.cmbDigitsSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDigitsSize.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmbDigitsSize.FormattingEnabled = true;
            this.cmbDigitsSize.Items.AddRange(new object[] {
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20"});
            this.cmbDigitsSize.Location = new System.Drawing.Point(129, 122);
            this.cmbDigitsSize.Name = "cmbDigitsSize";
            this.cmbDigitsSize.Size = new System.Drawing.Size(121, 21);
            this.cmbDigitsSize.TabIndex = 18;
            this.cmbDigitsSize.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            // 
            // lDigitsSize
            // 
            this.lDigitsSize.AutoSize = true;
            this.lDigitsSize.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lDigitsSize.Location = new System.Drawing.Point(10, 125);
            this.lDigitsSize.Name = "lDigitsSize";
            this.lDigitsSize.Size = new System.Drawing.Size(54, 13);
            this.lDigitsSize.TabIndex = 17;
            this.lDigitsSize.Text = "Digits size";
            // 
            // chNotify
            // 
            this.chNotify.AutoSize = true;
            this.chNotify.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.chNotify.Location = new System.Drawing.Point(13, 206);
            this.chNotify.Name = "chNotify";
            this.chNotify.Size = new System.Drawing.Size(163, 17);
            this.chNotify.TabIndex = 16;
            this.chNotify.Text = "Display notify every morning";
            this.chNotify.UseVisualStyleBackColor = true;
            this.chNotify.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // cmbDigitsColor
            // 
            this.cmbDigitsColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDigitsColor.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmbDigitsColor.FormattingEnabled = true;
            this.cmbDigitsColor.Location = new System.Drawing.Point(129, 96);
            this.cmbDigitsColor.Name = "cmbDigitsColor";
            this.cmbDigitsColor.Size = new System.Drawing.Size(121, 21);
            this.cmbDigitsColor.TabIndex = 14;
            this.cmbDigitsColor.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            // 
            // lDigitsColor
            // 
            this.lDigitsColor.AutoSize = true;
            this.lDigitsColor.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lDigitsColor.Location = new System.Drawing.Point(11, 99);
            this.lDigitsColor.Name = "lDigitsColor";
            this.lDigitsColor.Size = new System.Drawing.Size(59, 13);
            this.lDigitsColor.TabIndex = 12;
            this.lDigitsColor.Text = "Digits color";
            // 
            // cmbIconFashion
            // 
            this.cmbIconFashion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbIconFashion.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmbIconFashion.FormattingEnabled = true;
            this.cmbIconFashion.Items.AddRange(new object[] {
            "Square",
            "Circle"});
            this.cmbIconFashion.Location = new System.Drawing.Point(129, 177);
            this.cmbIconFashion.Name = "cmbIconFashion";
            this.cmbIconFashion.Size = new System.Drawing.Size(121, 21);
            this.cmbIconFashion.TabIndex = 15;
            this.cmbIconFashion.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            // 
            // lIconFashion
            // 
            this.lIconFashion.AutoSize = true;
            this.lIconFashion.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lIconFashion.Location = new System.Drawing.Point(10, 180);
            this.lIconFashion.Name = "lIconFashion";
            this.lIconFashion.Size = new System.Drawing.Size(66, 13);
            this.lIconFashion.TabIndex = 10;
            this.lIconFashion.Text = "Icon fashion";
            // 
            // chDigits
            // 
            this.chDigits.AutoSize = true;
            this.chDigits.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.chDigits.Location = new System.Drawing.Point(13, 73);
            this.chDigits.Name = "chDigits";
            this.chDigits.Size = new System.Drawing.Size(135, 17);
            this.chDigits.TabIndex = 13;
            this.chDigits.Text = "Draw digits on the icon";
            this.chDigits.UseVisualStyleBackColor = true;
            this.chDigits.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // updTrafficWarning
            // 
            this.updTrafficWarning.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.updTrafficWarning.Location = new System.Drawing.Point(173, 47);
            this.updTrafficWarning.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.updTrafficWarning.Name = "updTrafficWarning";
            this.updTrafficWarning.Size = new System.Drawing.Size(74, 21);
            this.updTrafficWarning.TabIndex = 12;
            this.updTrafficWarning.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.updTrafficWarning.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            this.updTrafficWarning.Leave += new System.EventHandler(this.numericUpDown5_Leave);
            // 
            // updTrafficCritical
            // 
            this.updTrafficCritical.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.updTrafficCritical.Location = new System.Drawing.Point(93, 47);
            this.updTrafficCritical.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.updTrafficCritical.Name = "updTrafficCritical";
            this.updTrafficCritical.Size = new System.Drawing.Size(74, 21);
            this.updTrafficCritical.TabIndex = 11;
            this.updTrafficCritical.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.updTrafficCritical.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            this.updTrafficCritical.Leave += new System.EventHandler(this.numericUpDown4_Leave);
            // 
            // updTrafficExceed
            // 
            this.updTrafficExceed.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.updTrafficExceed.Location = new System.Drawing.Point(13, 47);
            this.updTrafficExceed.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.updTrafficExceed.Name = "updTrafficExceed";
            this.updTrafficExceed.Size = new System.Drawing.Size(74, 21);
            this.updTrafficExceed.TabIndex = 10;
            this.updTrafficExceed.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.updTrafficExceed.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            // 
            // lTrafficRanges
            // 
            this.lTrafficRanges.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lTrafficRanges.Location = new System.Drawing.Point(10, 18);
            this.lTrafficRanges.Name = "lTrafficRanges";
            this.lTrafficRanges.Size = new System.Drawing.Size(237, 26);
            this.lTrafficRanges.TabIndex = 0;
            this.lTrafficRanges.Text = "Traffic Ranges: Exceed, Critical, Warning";
            // 
            // groupSystem
            // 
            this.groupSystem.Controls.Add(this.btnAdditional);
            this.groupSystem.Controls.Add(this.chTrace);
            this.groupSystem.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupSystem.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupSystem.Location = new System.Drawing.Point(0, 508);
            this.groupSystem.Name = "groupSystem";
            this.groupSystem.Size = new System.Drawing.Size(278, 55);
            this.groupSystem.TabIndex = 10;
            this.groupSystem.TabStop = false;
            this.groupSystem.Text = " System ";
            // 
            // btnAdditional
            // 
            this.btnAdditional.Location = new System.Drawing.Point(129, 16);
            this.btnAdditional.Name = "btnAdditional";
            this.btnAdditional.Size = new System.Drawing.Size(121, 23);
            this.btnAdditional.TabIndex = 1;
            this.btnAdditional.Text = "Additional";
            this.btnAdditional.UseVisualStyleBackColor = true;
            this.btnAdditional.Click += new System.EventHandler(this.btnAdditional_Click);
            // 
            // chTrace
            // 
            this.chTrace.AutoSize = true;
            this.chTrace.Location = new System.Drawing.Point(13, 20);
            this.chTrace.Name = "chTrace";
            this.chTrace.Size = new System.Drawing.Size(94, 17);
            this.chTrace.TabIndex = 0;
            this.chTrace.Text = "Trace Enabled";
            this.chTrace.UseVisualStyleBackColor = true;
            this.chTrace.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // Configuration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.groupSystem);
            this.Controls.Add(this.groupSystemTray);
            this.Controls.Add(this.groupTraffic);
            this.Controls.Add(this.groupGeneral);
            this.Name = "Configuration";
            this.Size = new System.Drawing.Size(278, 569);
            this.Load += new System.EventHandler(this.Configuration_Load);
            this.groupGeneral.ResumeLayout(false);
            this.groupGeneral.PerformLayout();
            this.groupTraffic.ResumeLayout(false);
            this.groupTraffic.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.updTrafficLimit)).EndInit();
            this.groupSystemTray.ResumeLayout(false);
            this.groupSystemTray.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.updTrafficWarning)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.updTrafficCritical)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.updTrafficExceed)).EndInit();
            this.groupSystem.ResumeLayout(false);
            this.groupSystem.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox chAutoStart;
        private System.Windows.Forms.GroupBox groupGeneral;
        private System.Windows.Forms.CheckBox chCache;
        private System.Windows.Forms.GroupBox groupTraffic;
        private System.Windows.Forms.Label lTrafficLimit;
        private System.Windows.Forms.NumericUpDown updTrafficLimit;
        private System.Windows.Forms.CheckBox chRoundUp;
        private System.Windows.Forms.GroupBox groupSystemTray;
        private System.Windows.Forms.Label lTrafficRanges;
        private System.Windows.Forms.NumericUpDown updTrafficWarning;
        private System.Windows.Forms.NumericUpDown updTrafficCritical;
        private System.Windows.Forms.NumericUpDown updTrafficExceed;
        private System.Windows.Forms.CheckBox chDigits;
        private System.Windows.Forms.ComboBox cmbIconFashion;
        private System.Windows.Forms.Label lIconFashion;
        private System.Windows.Forms.CheckBox chTop10;
        private System.Windows.Forms.ComboBox cmbLanguage;
        private System.Windows.Forms.Label lLanguage;
        private System.Windows.Forms.ComboBox cmbDigitsColor;
        private System.Windows.Forms.Label lDigitsColor;
        private System.Windows.Forms.CheckBox chFilter;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.CheckBox chNotify;
        private System.Windows.Forms.ComboBox cmbDigitsSize;
        private System.Windows.Forms.Label lDigitsSize;
        private System.Windows.Forms.ComboBox cmbFont;
        private System.Windows.Forms.Label lFontName;
        private System.Windows.Forms.ComboBox cmbLocation;
        private System.Windows.Forms.Label lLocation;
        private System.Windows.Forms.GroupBox groupSystem;
        private System.Windows.Forms.CheckBox chTrace;
        private System.Windows.Forms.Button btnAdditional;
    }
}
