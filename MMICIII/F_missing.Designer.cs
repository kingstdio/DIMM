namespace MMICIII
{
    partial class F_missing
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_missing));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.bt_mae = new System.Windows.Forms.Button();
            this.bt_calmean = new System.Windows.Forms.Button();
            this.bt_pd1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.sw_interval = new DevComponents.DotNetBar.Controls.SwitchButton();
            this.label19 = new System.Windows.Forms.Label();
            this.sw_marsk = new DevComponents.DotNetBar.Controls.SwitchButton();
            this.cmb_filleype = new System.Windows.Forms.ComboBox();
            this.sw_fill = new DevComponents.DotNetBar.Controls.SwitchButton();
            this.label12 = new System.Windows.Forms.Label();
            this.tb_minstay = new System.Windows.Forms.TextBox();
            this.sw_1p1u = new DevComponents.DotNetBar.Controls.SwitchButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tb_icd9 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tb_timespan = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tb_deathThresholds = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tb_maxstay = new System.Windows.Forms.TextBox();
            this.tb_inputFC = new System.Windows.Forms.TextBox();
            this.tb_relatedLevel = new System.Windows.Forms.TextBox();
            this.tb_chartFC = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.tb_knear = new System.Windows.Forms.TextBox();
            this.tb_threads = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tss1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tss_baselocation = new System.Windows.Forms.ToolStripStatusLabel();
            this.tss2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tss_icd9 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.check_badFile = new System.Windows.Forms.Button();
            this.tb_ckNoneFiled = new System.Windows.Forms.Button();
            this.bt_missingrate = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(-1, 1);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1089, 572);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.statusStrip1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1081, 546);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Construction";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.bt_missingrate);
            this.groupBox2.Controls.Add(this.bt_mae);
            this.groupBox2.Controls.Add(this.bt_calmean);
            this.groupBox2.Controls.Add(this.bt_pd1);
            this.groupBox2.Location = new System.Drawing.Point(18, 284);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1043, 142);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Actions";
            // 
            // bt_mae
            // 
            this.bt_mae.Location = new System.Drawing.Point(357, 35);
            this.bt_mae.Name = "bt_mae";
            this.bt_mae.Size = new System.Drawing.Size(125, 44);
            this.bt_mae.TabIndex = 14;
            this.bt_mae.Text = "MAE-MRE";
            this.bt_mae.UseVisualStyleBackColor = true;
            this.bt_mae.Click += new System.EventHandler(this.bt_mae_Click);
            // 
            // bt_calmean
            // 
            this.bt_calmean.Location = new System.Drawing.Point(177, 35);
            this.bt_calmean.Name = "bt_calmean";
            this.bt_calmean.Size = new System.Drawing.Size(125, 44);
            this.bt_calmean.TabIndex = 13;
            this.bt_calmean.Text = "Cacl Mean and Mse";
            this.bt_calmean.UseVisualStyleBackColor = true;
            this.bt_calmean.Click += new System.EventHandler(this.bt_calmean_Click);
            // 
            // bt_pd1
            // 
            this.bt_pd1.Location = new System.Drawing.Point(26, 35);
            this.bt_pd1.Name = "bt_pd1";
            this.bt_pd1.Size = new System.Drawing.Size(125, 44);
            this.bt_pd1.TabIndex = 12;
            this.bt_pd1.Text = "buildExpData";
            this.bt_pd1.UseVisualStyleBackColor = true;
            this.bt_pd1.Click += new System.EventHandler(this.bt_pd1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.sw_interval);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.sw_marsk);
            this.groupBox1.Controls.Add(this.cmb_filleype);
            this.groupBox1.Controls.Add(this.sw_fill);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.tb_minstay);
            this.groupBox1.Controls.Add(this.sw_1p1u);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.tb_icd9);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.tb_timespan);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.tb_deathThresholds);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.tb_maxstay);
            this.groupBox1.Controls.Add(this.tb_inputFC);
            this.groupBox1.Controls.Add(this.tb_relatedLevel);
            this.groupBox1.Controls.Add(this.tb_chartFC);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.tb_knear);
            this.groupBox1.Controls.Add(this.tb_threads);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(18, 20);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1043, 221);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Args";
            // 
            // sw_interval
            // 
            // 
            // 
            // 
            this.sw_interval.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.sw_interval.Location = new System.Drawing.Point(585, 173);
            this.sw_interval.Name = "sw_interval";
            this.sw_interval.OffText = "False";
            this.sw_interval.OnText = "True";
            this.sw_interval.Size = new System.Drawing.Size(98, 24);
            this.sw_interval.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.sw_interval.SwitchWidth = 48;
            this.sw_interval.TabIndex = 16;
            this.sw_interval.Value = true;
            this.sw_interval.ValueObject = "Y";
            this.sw_interval.ValueChanged += new System.EventHandler(this.swInterval_ValueChanged);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(482, 181);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(84, 13);
            this.label19.TabIndex = 15;
            this.label19.Text = "outputInterval：";
            // 
            // sw_marsk
            // 
            // 
            // 
            // 
            this.sw_marsk.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.sw_marsk.Location = new System.Drawing.Point(293, 176);
            this.sw_marsk.Name = "sw_marsk";
            this.sw_marsk.OffText = "False";
            this.sw_marsk.OnText = "True";
            this.sw_marsk.Size = new System.Drawing.Size(98, 24);
            this.sw_marsk.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.sw_marsk.SwitchWidth = 48;
            this.sw_marsk.TabIndex = 14;
            this.sw_marsk.Value = true;
            this.sw_marsk.ValueObject = "Y";
            this.sw_marsk.ValueChanged += new System.EventHandler(this.sw_marsk_ValueChanged);
            // 
            // cmb_filleype
            // 
            this.cmb_filleype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_filleype.FormattingEnabled = true;
            this.cmb_filleype.Location = new System.Drawing.Point(89, 133);
            this.cmb_filleype.Name = "cmb_filleype";
            this.cmb_filleype.Size = new System.Drawing.Size(90, 21);
            this.cmb_filleype.TabIndex = 13;
            this.cmb_filleype.SelectedIndexChanged += new System.EventHandler(this.cmb_filleype_SelectedIndexChanged);
            // 
            // sw_fill
            // 
            // 
            // 
            // 
            this.sw_fill.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.sw_fill.Location = new System.Drawing.Point(585, 86);
            this.sw_fill.Name = "sw_fill";
            this.sw_fill.OffText = "False";
            this.sw_fill.OnText = "True";
            this.sw_fill.Size = new System.Drawing.Size(98, 24);
            this.sw_fill.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.sw_fill.SwitchWidth = 48;
            this.sw_fill.TabIndex = 12;
            this.sw_fill.ValueChanged += new System.EventHandler(this.sw_fill_ValueChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(467, 92);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(99, 13);
            this.label12.TabIndex = 11;
            this.label12.Text = "Fill Missing Value：";
            // 
            // tb_minstay
            // 
            this.tb_minstay.Location = new System.Drawing.Point(886, 43);
            this.tb_minstay.Name = "tb_minstay";
            this.tb_minstay.Size = new System.Drawing.Size(72, 20);
            this.tb_minstay.TabIndex = 2;
            this.tb_minstay.TextChanged += new System.EventHandler(this.tb_minstay_TextChanged);
            // 
            // sw_1p1u
            // 
            // 
            // 
            // 
            this.sw_1p1u.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.sw_1p1u.Location = new System.Drawing.Point(288, 86);
            this.sw_1p1u.Name = "sw_1p1u";
            this.sw_1p1u.OffText = "False";
            this.sw_1p1u.OnText = "True";
            this.sw_1p1u.Size = new System.Drawing.Size(98, 24);
            this.sw_1p1u.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.sw_1p1u.SwitchWidth = 48;
            this.sw_1p1u.TabIndex = 10;
            this.sw_1p1u.Value = true;
            this.sw_1p1u.ValueObject = "Y";
            this.sw_1p1u.ValueChanged += new System.EventHandler(this.sw_1p1u_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "ICD9：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(818, 92);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(54, 13);
            this.label8.TabIndex = 9;
            this.label8.Text = "Max Stay:";
            // 
            // tb_icd9
            // 
            this.tb_icd9.Location = new System.Drawing.Point(89, 43);
            this.tb_icd9.Name = "tb_icd9";
            this.tb_icd9.Size = new System.Drawing.Size(90, 20);
            this.tb_icd9.TabIndex = 2;
            this.tb_icd9.Text = "41401";
            this.tb_icd9.TextChanged += new System.EventHandler(this.tb_icd9_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(821, 47);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Min Stay:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(211, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Time Span：";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(964, 47);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(39, 13);
            this.label10.TabIndex = 7;
            this.label10.Text = "(hours)";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(212, 184);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(75, 13);
            this.label18.TabIndex = 1;
            this.label18.Text = "outputMask：";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(241, 92);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(46, 13);
            this.label11.TabIndex = 1;
            this.label11.Text = "1P1U：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(964, 92);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(39, 13);
            this.label9.TabIndex = 7;
            this.label9.Text = "(hours)";
            // 
            // tb_timespan
            // 
            this.tb_timespan.Location = new System.Drawing.Point(288, 43);
            this.tb_timespan.Name = "tb_timespan";
            this.tb_timespan.Size = new System.Drawing.Size(98, 20);
            this.tb_timespan.TabIndex = 2;
            this.tb_timespan.TextChanged += new System.EventHandler(this.tb_timespan_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(693, 47);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "(days)";
            // 
            // tb_deathThresholds
            // 
            this.tb_deathThresholds.Location = new System.Drawing.Point(585, 43);
            this.tb_deathThresholds.Name = "tb_deathThresholds";
            this.tb_deathThresholds.Size = new System.Drawing.Size(98, 20);
            this.tb_deathThresholds.TabIndex = 2;
            this.tb_deathThresholds.TextChanged += new System.EventHandler(this.tb_deathThresholds_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(472, 47);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Death Thresholds:";
            // 
            // tb_maxstay
            // 
            this.tb_maxstay.Location = new System.Drawing.Point(886, 88);
            this.tb_maxstay.Name = "tb_maxstay";
            this.tb_maxstay.Size = new System.Drawing.Size(72, 20);
            this.tb_maxstay.TabIndex = 2;
            this.tb_maxstay.TextChanged += new System.EventHandler(this.tb_maxstay_TextChanged);
            // 
            // tb_inputFC
            // 
            this.tb_inputFC.Location = new System.Drawing.Point(886, 133);
            this.tb_inputFC.Name = "tb_inputFC";
            this.tb_inputFC.Size = new System.Drawing.Size(72, 20);
            this.tb_inputFC.TabIndex = 5;
            this.tb_inputFC.TextChanged += new System.EventHandler(this.tb_inputFC_TextChanged);
            // 
            // tb_relatedLevel
            // 
            this.tb_relatedLevel.Location = new System.Drawing.Point(288, 133);
            this.tb_relatedLevel.Name = "tb_relatedLevel";
            this.tb_relatedLevel.Size = new System.Drawing.Size(98, 20);
            this.tb_relatedLevel.TabIndex = 5;
            this.tb_relatedLevel.TextChanged += new System.EventHandler(this.tb_relatedLevel_TextChanged);
            // 
            // tb_chartFC
            // 
            this.tb_chartFC.Location = new System.Drawing.Point(585, 133);
            this.tb_chartFC.Name = "tb_chartFC";
            this.tb_chartFC.Size = new System.Drawing.Size(90, 20);
            this.tb_chartFC.TabIndex = 5;
            this.tb_chartFC.TextChanged += new System.EventHandler(this.tb_chartFC_TextChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(762, 137);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(110, 13);
            this.label14.TabIndex = 4;
            this.label14.Text = "InputFeature Count：";
            // 
            // tb_knear
            // 
            this.tb_knear.Location = new System.Drawing.Point(89, 180);
            this.tb_knear.Name = "tb_knear";
            this.tb_knear.Size = new System.Drawing.Size(90, 20);
            this.tb_knear.TabIndex = 5;
            this.tb_knear.TextChanged += new System.EventHandler(this.tb_knear_TextChanged);
            // 
            // tb_threads
            // 
            this.tb_threads.Location = new System.Drawing.Point(89, 88);
            this.tb_threads.Name = "tb_threads";
            this.tb_threads.Size = new System.Drawing.Size(90, 20);
            this.tb_threads.TabIndex = 5;
            this.tb_threads.TextChanged += new System.EventHandler(this.tb_threads_TextChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(455, 137);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(111, 13);
            this.label13.TabIndex = 4;
            this.label13.Text = "ChartFeature Count：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(409, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "(mins)";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(202, 137);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(85, 13);
            this.label16.TabIndex = 4;
            this.label16.Text = "Related Level：";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(27, 184);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(52, 13);
            this.label17.TabIndex = 4;
            this.label17.Text = "K-Near：";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(27, 137);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(58, 13);
            this.label15.TabIndex = 4;
            this.label15.Text = "Fill Type：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Threads：";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tss1,
            this.tss_baselocation,
            this.tss2,
            this.tss_icd9});
            this.statusStrip1.Location = new System.Drawing.Point(3, 519);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1075, 24);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tss1
            // 
            this.tss1.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)(((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tss1.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this.tss1.Name = "tss1";
            this.tss1.Size = new System.Drawing.Size(81, 19);
            this.tss1.Text = "baseLocation";
            // 
            // tss_baselocation
            // 
            this.tss_baselocation.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tss_baselocation.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this.tss_baselocation.Name = "tss_baselocation";
            this.tss_baselocation.Size = new System.Drawing.Size(122, 19);
            this.tss_baselocation.Text = "toolStripStatusLabel1";
            // 
            // tss2
            // 
            this.tss2.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tss2.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this.tss2.Name = "tss2";
            this.tss2.Size = new System.Drawing.Size(79, 19);
            this.tss2.Text = "Current ICD9";
            // 
            // tss_icd9
            // 
            this.tss_icd9.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tss_icd9.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this.tss_icd9.Name = "tss_icd9";
            this.tss_icd9.Size = new System.Drawing.Size(122, 19);
            this.tss_icd9.Text = "toolStripStatusLabel1";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.check_badFile);
            this.tabPage2.Controls.Add(this.tb_ckNoneFiled);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1081, 546);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Utils";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // check_badFile
            // 
            this.check_badFile.Location = new System.Drawing.Point(313, 58);
            this.check_badFile.Name = "check_badFile";
            this.check_badFile.Size = new System.Drawing.Size(156, 35);
            this.check_badFile.TabIndex = 1;
            this.check_badFile.Text = "检查损坏的文件";
            this.check_badFile.UseVisualStyleBackColor = true;
            this.check_badFile.Click += new System.EventHandler(this.check_badFile_Click);
            // 
            // tb_ckNoneFiled
            // 
            this.tb_ckNoneFiled.Location = new System.Drawing.Point(97, 58);
            this.tb_ckNoneFiled.Name = "tb_ckNoneFiled";
            this.tb_ckNoneFiled.Size = new System.Drawing.Size(156, 35);
            this.tb_ckNoneFiled.TabIndex = 0;
            this.tb_ckNoneFiled.Text = "检查全为空的特征";
            this.tb_ckNoneFiled.UseVisualStyleBackColor = true;
            this.tb_ckNoneFiled.Click += new System.EventHandler(this.tb_ckNoneFiled_Click);
            // 
            // bt_missingrate
            // 
            this.bt_missingrate.Location = new System.Drawing.Point(512, 35);
            this.bt_missingrate.Name = "bt_missingrate";
            this.bt_missingrate.Size = new System.Drawing.Size(103, 44);
            this.bt_missingrate.TabIndex = 15;
            this.bt_missingrate.Text = "Missing rate";
            this.bt_missingrate.UseVisualStyleBackColor = true;
            this.bt_missingrate.Click += new System.EventHandler(this.bt_missingrate_Click);
            // 
            // F_missing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1091, 572);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "F_missing";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Missing Value Tools";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TextBox tb_icd9;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripStatusLabel tss1;
        private System.Windows.Forms.ToolStripStatusLabel tss_baselocation;
        private System.Windows.Forms.ToolStripStatusLabel tss2;
        private System.Windows.Forms.ToolStripStatusLabel tss_icd9;
        private System.Windows.Forms.TextBox tb_timespan;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_threads;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tb_deathThresholds;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tb_maxstay;
        private System.Windows.Forms.TextBox tb_minstay;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private DevComponents.DotNetBar.Controls.SwitchButton sw_1p1u;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button bt_pd1;
        private DevComponents.DotNetBar.Controls.SwitchButton sw_fill;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tb_inputFC;
        private System.Windows.Forms.TextBox tb_chartFC;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox cmb_filleype;
        private System.Windows.Forms.Button bt_calmean;
        private System.Windows.Forms.TextBox tb_relatedLevel;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox tb_knear;
        private System.Windows.Forms.Label label17;
        private DevComponents.DotNetBar.Controls.SwitchButton sw_interval;
        private System.Windows.Forms.Label label19;
        private DevComponents.DotNetBar.Controls.SwitchButton sw_marsk;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button bt_mae;
        private System.Windows.Forms.Button tb_ckNoneFiled;
        private System.Windows.Forms.Button check_badFile;
        private System.Windows.Forms.Button bt_missingrate;
    }
}