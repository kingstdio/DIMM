namespace MMICIII
{
    partial class F_main
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_main));
            this.bt_calcApacheii = new System.Windows.Forms.Button();
            this.dgv_main = new System.Windows.Forms.DataGridView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rtb_outBox = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comb_scoreSystemType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.bt_writeCSV = new System.Windows.Forms.Button();
            this.bt_viewResult = new System.Windows.Forms.Button();
            this.bt_fill = new System.Windows.Forms.Button();
            this.tb_icustayid = new System.Windows.Forms.TextBox();
            this.tb_buildExpData = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tb_icustayidTest = new System.Windows.Forms.TextBox();
            this.tb_icd9Test = new System.Windows.Forms.TextBox();
            this.bt_ausofaTest = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.bt_comDBsource = new System.Windows.Forms.Button();
            this.bt_completion = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.bt_cunstructICUStay = new System.Windows.Forms.Button();
            this.bt_constructSofaFeature = new System.Windows.Forms.Button();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.bt_dividedFiles = new System.Windows.Forms.Button();
            this.bt_chooseBaseDataset = new System.Windows.Forms.Button();
            this.bt_tt = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.bt_findsame = new System.Windows.Forms.Button();
            this.slider2 = new DevComponents.DotNetBar.Controls.Slider();
            this.bt_cInOrNot = new System.Windows.Forms.Button();
            this.dgv_ana = new System.Windows.Forms.DataGridView();
            this.bt_dorder = new System.Windows.Forms.Button();
            this.bt_load_json = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.slider1 = new DevComponents.DotNetBar.Controls.Slider();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tb_los_end = new System.Windows.Forms.TextBox();
            this.tb_los_start = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.bt_autosofa = new System.Windows.Forms.Button();
            this.bt_removef = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.bt_checkSF = new System.Windows.Forms.Button();
            this.bt_auto_apachii = new System.Windows.Forms.Button();
            this.tb_icd9 = new System.Windows.Forms.TextBox();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.bt_saveLos = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lb_load = new System.Windows.Forms.ToolStripStatusLabel();
            this.lb_divided = new System.Windows.Forms.ToolStripStatusLabel();
            this.lb_score = new System.Windows.Forms.ToolStripStatusLabel();
            this.tss_scoreSystem = new System.Windows.Forms.ToolStripStatusLabel();
            this.pg_main = new System.Windows.Forms.ToolStripProgressBar();
            this.opf_load_json = new System.Windows.Forms.OpenFileDialog();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_main)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ana)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bt_calcApacheii
            // 
            this.bt_calcApacheii.Location = new System.Drawing.Point(760, 31);
            this.bt_calcApacheii.Name = "bt_calcApacheii";
            this.bt_calcApacheii.Size = new System.Drawing.Size(134, 26);
            this.bt_calcApacheii.TabIndex = 0;
            this.bt_calcApacheii.Text = "CalculateScore";
            this.bt_calcApacheii.UseVisualStyleBackColor = true;
            this.bt_calcApacheii.Click += new System.EventHandler(this.tb_calcScore_Click);
            // 
            // dgv_main
            // 
            this.dgv_main.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_main.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgv_main.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_main.Location = new System.Drawing.Point(7, 112);
            this.dgv_main.Name = "dgv_main";
            this.dgv_main.Size = new System.Drawing.Size(1364, 316);
            this.dgv_main.TabIndex = 2;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Location = new System.Drawing.Point(1, 1);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1393, 728);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.dgv_main);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1385, 702);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "数据处理";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.rtb_outBox);
            this.groupBox2.Location = new System.Drawing.Point(7, 433);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1367, 190);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            // 
            // rtb_outBox
            // 
            this.rtb_outBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb_outBox.Location = new System.Drawing.Point(3, 17);
            this.rtb_outBox.Name = "rtb_outBox";
            this.rtb_outBox.Size = new System.Drawing.Size(1361, 170);
            this.rtb_outBox.TabIndex = 0;
            this.rtb_outBox.Text = "";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.comb_scoreSystemType);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.bt_writeCSV);
            this.groupBox1.Controls.Add(this.bt_viewResult);
            this.groupBox1.Controls.Add(this.bt_fill);
            this.groupBox1.Controls.Add(this.bt_calcApacheii);
            this.groupBox1.Controls.Add(this.tb_icustayid);
            this.groupBox1.Controls.Add(this.tb_buildExpData);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(7, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1367, 101);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // comb_scoreSystemType
            // 
            this.comb_scoreSystemType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comb_scoreSystemType.FormattingEnabled = true;
            this.comb_scoreSystemType.Items.AddRange(new object[] {
            "APACHEII",
            "SOFA"});
            this.comb_scoreSystemType.Location = new System.Drawing.Point(152, 35);
            this.comb_scoreSystemType.Name = "comb_scoreSystemType";
            this.comb_scoreSystemType.Size = new System.Drawing.Size(121, 20);
            this.comb_scoreSystemType.TabIndex = 2;
            this.comb_scoreSystemType.SelectedIndexChanged += new System.EventHandler(this.comb_scoreSystemType_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(137, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "Choose A Score System:";
            // 
            // bt_writeCSV
            // 
            this.bt_writeCSV.Location = new System.Drawing.Point(1225, 31);
            this.bt_writeCSV.Name = "bt_writeCSV";
            this.bt_writeCSV.Size = new System.Drawing.Size(134, 26);
            this.bt_writeCSV.TabIndex = 0;
            this.bt_writeCSV.Text = "Write CSV";
            this.bt_writeCSV.UseVisualStyleBackColor = true;
            this.bt_writeCSV.Click += new System.EventHandler(this.bt_writeCSV_Click);
            // 
            // bt_viewResult
            // 
            this.bt_viewResult.Location = new System.Drawing.Point(1070, 31);
            this.bt_viewResult.Name = "bt_viewResult";
            this.bt_viewResult.Size = new System.Drawing.Size(134, 26);
            this.bt_viewResult.TabIndex = 0;
            this.bt_viewResult.Text = "View Result";
            this.bt_viewResult.UseVisualStyleBackColor = true;
            this.bt_viewResult.Click += new System.EventHandler(this.bt_viewResult_Click);
            // 
            // bt_fill
            // 
            this.bt_fill.Location = new System.Drawing.Point(915, 31);
            this.bt_fill.Name = "bt_fill";
            this.bt_fill.Size = new System.Drawing.Size(134, 26);
            this.bt_fill.TabIndex = 0;
            this.bt_fill.Text = "Filling Scores";
            this.bt_fill.UseVisualStyleBackColor = true;
            this.bt_fill.Click += new System.EventHandler(this.bt_fill_Click);
            // 
            // tb_icustayid
            // 
            this.tb_icustayid.Location = new System.Drawing.Point(402, 35);
            this.tb_icustayid.Name = "tb_icustayid";
            this.tb_icustayid.Size = new System.Drawing.Size(180, 21);
            this.tb_icustayid.TabIndex = 4;
            this.tb_icustayid.Text = "200001";
            // 
            // tb_buildExpData
            // 
            this.tb_buildExpData.Location = new System.Drawing.Point(605, 31);
            this.tb_buildExpData.Name = "tb_buildExpData";
            this.tb_buildExpData.Size = new System.Drawing.Size(134, 26);
            this.tb_buildExpData.TabIndex = 1;
            this.tb_buildExpData.Text = "Build Sample";
            this.tb_buildExpData.UseVisualStyleBackColor = true;
            this.tb_buildExpData.Click += new System.EventHandler(this.tb_conApacheII_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(303, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "Input ICU Stay ID:";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.tb_icustayidTest);
            this.tabPage2.Controls.Add(this.tb_icd9Test);
            this.tabPage2.Controls.Add(this.bt_ausofaTest);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1385, 702);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "样例构建";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(190, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "ICUStayID";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(38, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "ICD9";
            // 
            // tb_icustayidTest
            // 
            this.tb_icustayidTest.Location = new System.Drawing.Point(254, 24);
            this.tb_icustayidTest.Name = "tb_icustayidTest";
            this.tb_icustayidTest.Size = new System.Drawing.Size(100, 21);
            this.tb_icustayidTest.TabIndex = 1;
            this.tb_icustayidTest.Text = "200487";
            // 
            // tb_icd9Test
            // 
            this.tb_icd9Test.Location = new System.Drawing.Point(75, 24);
            this.tb_icd9Test.Name = "tb_icd9Test";
            this.tb_icd9Test.Size = new System.Drawing.Size(100, 21);
            this.tb_icd9Test.TabIndex = 1;
            this.tb_icd9Test.Text = "41401";
            // 
            // bt_ausofaTest
            // 
            this.bt_ausofaTest.Location = new System.Drawing.Point(381, 15);
            this.bt_ausofaTest.Name = "bt_ausofaTest";
            this.bt_ausofaTest.Size = new System.Drawing.Size(161, 37);
            this.bt_ausofaTest.TabIndex = 0;
            this.bt_ausofaTest.Text = "auSofaExtended-test";
            this.bt_ausofaTest.UseVisualStyleBackColor = true;
            this.bt_ausofaTest.Click += new System.EventHandler(this.bt_ausofaTest_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.bt_comDBsource);
            this.tabPage3.Controls.Add(this.bt_completion);
            this.tabPage3.Controls.Add(this.progressBar1);
            this.tabPage3.Controls.Add(this.bt_cunstructICUStay);
            this.tabPage3.Controls.Add(this.bt_constructSofaFeature);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1385, 702);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "SOFA特征构建";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // bt_comDBsource
            // 
            this.bt_comDBsource.Location = new System.Drawing.Point(757, 92);
            this.bt_comDBsource.Name = "bt_comDBsource";
            this.bt_comDBsource.Size = new System.Drawing.Size(200, 77);
            this.bt_comDBsource.TabIndex = 2;
            this.bt_comDBsource.Text = "填充数据源";
            this.bt_comDBsource.UseVisualStyleBackColor = true;
            this.bt_comDBsource.Click += new System.EventHandler(this.bt_comDBsource_Click);
            // 
            // bt_completion
            // 
            this.bt_completion.Location = new System.Drawing.Point(527, 92);
            this.bt_completion.Name = "bt_completion";
            this.bt_completion.Size = new System.Drawing.Size(200, 77);
            this.bt_completion.TabIndex = 2;
            this.bt_completion.Text = "补全序列";
            this.bt_completion.UseVisualStyleBackColor = true;
            this.bt_completion.Click += new System.EventHandler(this.bt_completion_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(4, 302);
            this.progressBar1.Maximum = 52678;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(1378, 21);
            this.progressBar1.TabIndex = 1;
            // 
            // bt_cunstructICUStay
            // 
            this.bt_cunstructICUStay.Location = new System.Drawing.Point(271, 92);
            this.bt_cunstructICUStay.Name = "bt_cunstructICUStay";
            this.bt_cunstructICUStay.Size = new System.Drawing.Size(200, 77);
            this.bt_cunstructICUStay.TabIndex = 0;
            this.bt_cunstructICUStay.Text = "构建实验ICUStay";
            this.bt_cunstructICUStay.UseVisualStyleBackColor = true;
            this.bt_cunstructICUStay.Click += new System.EventHandler(this.bt_cunstructICUStay_Click);
            // 
            // bt_constructSofaFeature
            // 
            this.bt_constructSofaFeature.Location = new System.Drawing.Point(22, 92);
            this.bt_constructSofaFeature.Name = "bt_constructSofaFeature";
            this.bt_constructSofaFeature.Size = new System.Drawing.Size(200, 77);
            this.bt_constructSofaFeature.TabIndex = 0;
            this.bt_constructSofaFeature.Text = "构建特征";
            this.bt_constructSofaFeature.UseVisualStyleBackColor = true;
            this.bt_constructSofaFeature.Click += new System.EventHandler(this.bt_constructSofaFeature_Click);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.groupBox6);
            this.tabPage4.Controls.Add(this.groupBox5);
            this.tabPage4.Controls.Add(this.groupBox4);
            this.tabPage4.Controls.Add(this.groupBox3);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(1385, 702);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "死亡模型";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.bt_dividedFiles);
            this.groupBox6.Controls.Add(this.bt_chooseBaseDataset);
            this.groupBox6.Controls.Add(this.bt_tt);
            this.groupBox6.Location = new System.Drawing.Point(1089, 33);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(272, 92);
            this.groupBox6.TabIndex = 12;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "划分测试训练集";
            // 
            // bt_dividedFiles
            // 
            this.bt_dividedFiles.Location = new System.Drawing.Point(19, 58);
            this.bt_dividedFiles.Name = "bt_dividedFiles";
            this.bt_dividedFiles.Size = new System.Drawing.Size(98, 21);
            this.bt_dividedFiles.TabIndex = 1;
            this.bt_dividedFiles.Text = "选择划分文件";
            this.bt_dividedFiles.UseVisualStyleBackColor = true;
            this.bt_dividedFiles.Click += new System.EventHandler(this.bt_dividedFiles_Click);
            // 
            // bt_chooseBaseDataset
            // 
            this.bt_chooseBaseDataset.Location = new System.Drawing.Point(19, 31);
            this.bt_chooseBaseDataset.Name = "bt_chooseBaseDataset";
            this.bt_chooseBaseDataset.Size = new System.Drawing.Size(98, 21);
            this.bt_chooseBaseDataset.TabIndex = 1;
            this.bt_chooseBaseDataset.Text = "选择基础目录";
            this.bt_chooseBaseDataset.UseVisualStyleBackColor = true;
            this.bt_chooseBaseDataset.Click += new System.EventHandler(this.bt_chooseBaseDataset_Click);
            // 
            // bt_tt
            // 
            this.bt_tt.Location = new System.Drawing.Point(179, 31);
            this.bt_tt.Name = "bt_tt";
            this.bt_tt.Size = new System.Drawing.Size(75, 21);
            this.bt_tt.TabIndex = 0;
            this.bt_tt.Text = "划分";
            this.bt_tt.UseVisualStyleBackColor = true;
            this.bt_tt.Click += new System.EventHandler(this.bt_tt_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.bt_findsame);
            this.groupBox5.Controls.Add(this.slider2);
            this.groupBox5.Controls.Add(this.bt_cInOrNot);
            this.groupBox5.Controls.Add(this.dgv_ana);
            this.groupBox5.Controls.Add(this.bt_dorder);
            this.groupBox5.Controls.Add(this.bt_load_json);
            this.groupBox5.Controls.Add(this.label9);
            this.groupBox5.Controls.Add(this.label8);
            this.groupBox5.Controls.Add(this.slider1);
            this.groupBox5.Location = new System.Drawing.Point(16, 318);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(1043, 367);
            this.groupBox5.TabIndex = 11;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "analytic ";
            // 
            // bt_findsame
            // 
            this.bt_findsame.Location = new System.Drawing.Point(838, 32);
            this.bt_findsame.Name = "bt_findsame";
            this.bt_findsame.Size = new System.Drawing.Size(127, 21);
            this.bt_findsame.TabIndex = 9;
            this.bt_findsame.Text = "找预测错误相同的";
            this.bt_findsame.UseVisualStyleBackColor = true;
            this.bt_findsame.Click += new System.EventHandler(this.bt_findsame_Click);
            // 
            // slider2
            // 
            // 
            // 
            // 
            this.slider2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.slider2.Location = new System.Drawing.Point(6, 77);
            this.slider2.Maximum = 540;
            this.slider2.Minimum = 1;
            this.slider2.Name = "slider2";
            this.slider2.Size = new System.Drawing.Size(358, 21);
            this.slider2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.slider2.TabIndex = 8;
            this.slider2.Text = "数量";
            this.slider2.Value = 2;
            this.slider2.ValueChanged += new System.EventHandler(this.slider2_ValueChanged);
            // 
            // bt_cInOrNot
            // 
            this.bt_cInOrNot.Location = new System.Drawing.Point(644, 33);
            this.bt_cInOrNot.Name = "bt_cInOrNot";
            this.bt_cInOrNot.Size = new System.Drawing.Size(172, 21);
            this.bt_cInOrNot.TabIndex = 7;
            this.bt_cInOrNot.Text = "分析是否在出错的序列里";
            this.bt_cInOrNot.UseVisualStyleBackColor = true;
            this.bt_cInOrNot.Click += new System.EventHandler(this.bt_cInOrNot_Click);
            // 
            // dgv_ana
            // 
            this.dgv_ana.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_ana.Location = new System.Drawing.Point(6, 150);
            this.dgv_ana.Name = "dgv_ana";
            this.dgv_ana.Size = new System.Drawing.Size(1030, 211);
            this.dgv_ana.TabIndex = 6;
            // 
            // bt_dorder
            // 
            this.bt_dorder.Location = new System.Drawing.Point(515, 33);
            this.bt_dorder.Name = "bt_dorder";
            this.bt_dorder.Size = new System.Drawing.Size(111, 21);
            this.bt_dorder.TabIndex = 5;
            this.bt_dorder.Text = "分析病序情况";
            this.bt_dorder.UseVisualStyleBackColor = true;
            this.bt_dorder.Click += new System.EventHandler(this.bt_dorder_Click);
            // 
            // bt_load_json
            // 
            this.bt_load_json.Location = new System.Drawing.Point(409, 34);
            this.bt_load_json.Name = "bt_load_json";
            this.bt_load_json.Size = new System.Drawing.Size(99, 21);
            this.bt_load_json.TabIndex = 4;
            this.bt_load_json.Text = "加载实验结果";
            this.bt_load_json.UseVisualStyleBackColor = true;
            this.bt_load_json.Click += new System.EventHandler(this.bt_load_json_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(198, 62);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(11, 12);
            this.label9.TabIndex = 3;
            this.label9.Text = "4";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(198, 18);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(11, 12);
            this.label8.TabIndex = 3;
            this.label8.Text = "4";
            // 
            // slider1
            // 
            // 
            // 
            // 
            this.slider1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.slider1.Location = new System.Drawing.Point(6, 33);
            this.slider1.Maximum = 39;
            this.slider1.Minimum = 1;
            this.slider1.Name = "slider1";
            this.slider1.Size = new System.Drawing.Size(358, 21);
            this.slider1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.slider1.TabIndex = 2;
            this.slider1.Text = "病情等级";
            this.slider1.Value = 4;
            this.slider1.ValueChanged += new System.EventHandler(this.slider1_ValueChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.tb_los_end);
            this.groupBox4.Controls.Add(this.tb_los_start);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Location = new System.Drawing.Point(16, 171);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(1043, 115);
            this.groupBox4.TabIndex = 10;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Experiment settings";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(207, 31);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(11, 12);
            this.label7.TabIndex = 2;
            this.label7.Text = "-";
            // 
            // tb_los_end
            // 
            this.tb_los_end.Location = new System.Drawing.Point(223, 29);
            this.tb_los_end.Name = "tb_los_end";
            this.tb_los_end.Size = new System.Drawing.Size(76, 21);
            this.tb_los_end.TabIndex = 1;
            this.tb_los_end.Text = "360";
            // 
            // tb_los_start
            // 
            this.tb_los_start.Location = new System.Drawing.Point(124, 29);
            this.tb_los_start.Name = "tb_los_start";
            this.tb_los_start.Size = new System.Drawing.Size(76, 21);
            this.tb_los_start.TabIndex = 1;
            this.tb_los_start.Text = "4";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 31);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "Length of stay: ";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.bt_autosofa);
            this.groupBox3.Controls.Add(this.bt_removef);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.bt_checkSF);
            this.groupBox3.Controls.Add(this.bt_auto_apachii);
            this.groupBox3.Controls.Add(this.tb_icd9);
            this.groupBox3.Location = new System.Drawing.Point(16, 33);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1043, 111);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "groupBox3";
            // 
            // bt_autosofa
            // 
            this.bt_autosofa.Location = new System.Drawing.Point(245, 18);
            this.bt_autosofa.Name = "bt_autosofa";
            this.bt_autosofa.Size = new System.Drawing.Size(184, 21);
            this.bt_autosofa.TabIndex = 6;
            this.bt_autosofa.Text = "AutoSofa";
            this.bt_autosofa.UseVisualStyleBackColor = true;
            this.bt_autosofa.Click += new System.EventHandler(this.bt_autosofa_Click);
            // 
            // bt_removef
            // 
            this.bt_removef.Location = new System.Drawing.Point(276, 65);
            this.bt_removef.Name = "bt_removef";
            this.bt_removef.Size = new System.Drawing.Size(132, 21);
            this.bt_removef.TabIndex = 8;
            this.bt_removef.Text = "removefile";
            this.bt_removef.UseVisualStyleBackColor = true;
            this.bt_removef.Click += new System.EventHandler(this.bt_removef_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(44, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "ICD9 Code:";
            // 
            // bt_checkSF
            // 
            this.bt_checkSF.Location = new System.Drawing.Point(96, 65);
            this.bt_checkSF.Name = "bt_checkSF";
            this.bt_checkSF.Size = new System.Drawing.Size(132, 21);
            this.bt_checkSF.TabIndex = 8;
            this.bt_checkSF.Text = "check_strings";
            this.bt_checkSF.UseVisualStyleBackColor = true;
            this.bt_checkSF.Click += new System.EventHandler(this.bt_checkSF_Click);
            // 
            // bt_auto_apachii
            // 
            this.bt_auto_apachii.Location = new System.Drawing.Point(435, 18);
            this.bt_auto_apachii.Name = "bt_auto_apachii";
            this.bt_auto_apachii.Size = new System.Drawing.Size(184, 21);
            this.bt_auto_apachii.TabIndex = 6;
            this.bt_auto_apachii.Text = "AutoApache";
            this.bt_auto_apachii.UseVisualStyleBackColor = true;
            // 
            // tb_icd9
            // 
            this.tb_icd9.Location = new System.Drawing.Point(113, 18);
            this.tb_icd9.Name = "tb_icd9";
            this.tb_icd9.Size = new System.Drawing.Size(115, 21);
            this.tb_icd9.TabIndex = 7;
            this.tb_icd9.Text = "41401";
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.groupBox7);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(1385, 702);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Statistics";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.chart1);
            this.groupBox7.Controls.Add(this.bt_saveLos);
            this.groupBox7.Location = new System.Drawing.Point(3, 6);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(972, 306);
            this.groupBox7.TabIndex = 1;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "ICU Length of Stay";
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(6, 18);
            this.chart1.Name = "chart1";
            this.chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SeaGreen;
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "s1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(950, 230);
            this.chart1.TabIndex = 3;
            this.chart1.Text = "chart1";
            // 
            // bt_saveLos
            // 
            this.bt_saveLos.Location = new System.Drawing.Point(202, 268);
            this.bt_saveLos.Name = "bt_saveLos";
            this.bt_saveLos.Size = new System.Drawing.Size(75, 21);
            this.bt_saveLos.TabIndex = 2;
            this.bt_saveLos.Text = "Save";
            this.bt_saveLos.UseVisualStyleBackColor = true;
            this.bt_saveLos.Click += new System.EventHandler(this.bt_saveLos_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lb_load,
            this.lb_divided,
            this.lb_score,
            this.tss_scoreSystem,
            this.pg_main});
            this.statusStrip1.Location = new System.Drawing.Point(0, 731);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1391, 26);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lb_load
            // 
            this.lb_load.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.lb_load.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this.lb_load.Name = "lb_load";
            this.lb_load.Size = new System.Drawing.Size(60, 21);
            this.lb_load.Text = "加载数据";
            // 
            // lb_divided
            // 
            this.lb_divided.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.lb_divided.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this.lb_divided.Name = "lb_divided";
            this.lb_divided.Size = new System.Drawing.Size(60, 21);
            this.lb_divided.Text = "切分片段";
            // 
            // lb_score
            // 
            this.lb_score.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.lb_score.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this.lb_score.Name = "lb_score";
            this.lb_score.Size = new System.Drawing.Size(60, 21);
            this.lb_score.Text = "计算分数";
            // 
            // tss_scoreSystem
            // 
            this.tss_scoreSystem.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tss_scoreSystem.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this.tss_scoreSystem.Name = "tss_scoreSystem";
            this.tss_scoreSystem.Size = new System.Drawing.Size(60, 21);
            this.tss_scoreSystem.Text = "评分系统";
            // 
            // pg_main
            // 
            this.pg_main.Name = "pg_main";
            this.pg_main.Size = new System.Drawing.Size(900, 20);
            // 
            // opf_load_json
            // 
            this.opf_load_json.DefaultExt = "*.json";
            this.opf_load_json.Filter = "JSON File|*.json|ALL Filses|*.*";
            this.opf_load_json.Multiselect = true;
            // 
            // tabPage6
            // 
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(1385, 702);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "MissingValue";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // F_main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1391, 757);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "F_main";
            this.Text = "MMIC III ";
            ((System.ComponentModel.ISupportInitialize)(this.dgv_main)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ana)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button bt_calcApacheii;
        private System.Windows.Forms.DataGridView dgv_main;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RichTextBox rtb_outBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lb_load;
        private System.Windows.Forms.ToolStripStatusLabel lb_divided;
        private System.Windows.Forms.ToolStripStatusLabel lb_score;
        private System.Windows.Forms.Button bt_viewResult;
        private System.Windows.Forms.Button bt_fill;
        private System.Windows.Forms.Button bt_writeCSV;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Button bt_constructSofaFeature;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comb_scoreSystemType;
        private System.Windows.Forms.TextBox tb_icustayid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button tb_buildExpData;
        private System.Windows.Forms.ToolStripStatusLabel tss_scoreSystem;
        private System.Windows.Forms.Button bt_cunstructICUStay;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ToolStripProgressBar pg_main;
        private System.Windows.Forms.Button bt_completion;
        private System.Windows.Forms.TextBox tb_icd9;
        private System.Windows.Forms.Button bt_autosofa;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button bt_checkSF;
        private System.Windows.Forms.Button bt_removef;
        private System.Windows.Forms.Button bt_comDBsource;
        private System.Windows.Forms.Button bt_ausofaTest;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tb_icustayidTest;
        private System.Windows.Forms.TextBox tb_icd9Test;
        private System.Windows.Forms.Button bt_auto_apachii;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tb_los_end;
        private System.Windows.Forms.TextBox tb_los_start;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox5;
        private DevComponents.DotNetBar.Controls.Slider slider1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button bt_load_json;
        private System.Windows.Forms.OpenFileDialog opf_load_json;
        private System.Windows.Forms.Button bt_dorder;
        private System.Windows.Forms.DataGridView dgv_ana;
        private System.Windows.Forms.Button bt_cInOrNot;
        private DevComponents.DotNetBar.Controls.Slider slider2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button bt_findsame;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button bt_tt;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button bt_saveLos;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Button bt_chooseBaseDataset;
        private System.Windows.Forms.Button bt_dividedFiles;
        private System.Windows.Forms.TabPage tabPage6;
    }
}

