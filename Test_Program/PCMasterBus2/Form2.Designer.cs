namespace PCMasterBus2
{
    partial class Form2
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            this.orig_max = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.orig_min = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.orig_avg = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.edit_max = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.edit_min = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.blob_count = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.chart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.edited_image = new System.Windows.Forms.PictureBox();
            this.reset = new System.Windows.Forms.Button();
            this.original_image = new System.Windows.Forms.PictureBox();
            this.normalise_button = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.normalise_min = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.normalise_max = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.normalise_temp = new System.Windows.Forms.Button();
            this.sobel = new System.Windows.Forms.Button();
            this.log = new System.Windows.Forms.Button();
            this.direct = new System.Windows.Forms.Button();
            this.gauss = new System.Windows.Forms.Button();
            this.apply_template = new System.Windows.Forms.Button();
            this.template2_2 = new System.Windows.Forms.TextBox();
            this.template2_1 = new System.Windows.Forms.TextBox();
            this.template2_0 = new System.Windows.Forms.TextBox();
            this.template1_2 = new System.Windows.Forms.TextBox();
            this.template1_1 = new System.Windows.Forms.TextBox();
            this.template1_0 = new System.Windows.Forms.TextBox();
            this.template0_2 = new System.Windows.Forms.TextBox();
            this.template0_1 = new System.Windows.Forms.TextBox();
            this.template0_0 = new System.Windows.Forms.TextBox();
            this.undo_button = new System.Windows.Forms.Button();
            this.acc_space = new System.Windows.Forms.PictureBox();
            this.label12 = new System.Windows.Forms.Label();
            this.hough = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lower_thresh = new System.Windows.Forms.TextBox();
            this.upper_thresh = new System.Windows.Forms.TextBox();
            this.binary = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.radius = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.hough_thresh = new System.Windows.Forms.TextBox();
            this.blob = new System.Windows.Forms.Button();
            this.edit_avg = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.chart3 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.avg_blob = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.auto = new System.Windows.Forms.Button();
            this.height = new System.Windows.Forms.TrackBar();
            this.label17 = new System.Windows.Forms.Label();
            this.height_perc = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.height_direct = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edited_image)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.original_image)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.acc_space)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart3)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.height)).BeginInit();
            this.SuspendLayout();
            // 
            // orig_max
            // 
            this.orig_max.AutoSize = true;
            this.orig_max.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.orig_max.Location = new System.Drawing.Point(230, 211);
            this.orig_max.Name = "orig_max";
            this.orig_max.Size = new System.Drawing.Size(13, 13);
            this.orig_max.TabIndex = 87;
            this.orig_max.Text = "--";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(191, 211);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 13);
            this.label4.TabIndex = 86;
            this.label4.Text = "Max.:";
            // 
            // orig_min
            // 
            this.orig_min.AutoSize = true;
            this.orig_min.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.orig_min.Location = new System.Drawing.Point(147, 211);
            this.orig_min.Name = "orig_min";
            this.orig_min.Size = new System.Drawing.Size(13, 13);
            this.orig_min.TabIndex = 85;
            this.orig_min.Text = "--";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(111, 211);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 84;
            this.label2.Text = "Min.:";
            // 
            // orig_avg
            // 
            this.orig_avg.AutoSize = true;
            this.orig_avg.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.orig_avg.Location = new System.Drawing.Point(74, 211);
            this.orig_avg.Name = "orig_avg";
            this.orig_avg.Size = new System.Drawing.Size(13, 13);
            this.orig_avg.TabIndex = 83;
            this.orig_avg.Text = "--";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(10, 211);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(58, 13);
            this.label9.TabIndex = 82;
            this.label9.Text = "Durschnitt:";
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Location = new System.Drawing.Point(12, 25);
            this.chart1.Name = "chart1";
            this.chart1.Size = new System.Drawing.Size(320, 183);
            this.chart1.TabIndex = 81;
            this.chart1.Text = "chart1";
            // 
            // edit_max
            // 
            this.edit_max.AutoSize = true;
            this.edit_max.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.edit_max.Location = new System.Drawing.Point(555, 211);
            this.edit_max.Name = "edit_max";
            this.edit_max.Size = new System.Drawing.Size(13, 13);
            this.edit_max.TabIndex = 94;
            this.edit_max.Text = "--";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(516, 211);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 93;
            this.label3.Text = "Max.:";
            // 
            // edit_min
            // 
            this.edit_min.AutoSize = true;
            this.edit_min.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.edit_min.Location = new System.Drawing.Point(472, 211);
            this.edit_min.Name = "edit_min";
            this.edit_min.Size = new System.Drawing.Size(13, 13);
            this.edit_min.TabIndex = 92;
            this.edit_min.Text = "--";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(436, 211);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 13);
            this.label6.TabIndex = 91;
            this.label6.Text = "Min.:";
            // 
            // blob_count
            // 
            this.blob_count.AutoSize = true;
            this.blob_count.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.blob_count.Location = new System.Drawing.Point(759, 641);
            this.blob_count.Name = "blob_count";
            this.blob_count.Size = new System.Drawing.Size(13, 13);
            this.blob_count.TabIndex = 90;
            this.blob_count.Text = "--";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(667, 641);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(79, 13);
            this.label8.TabIndex = 89;
            this.label8.Text = "Anzahl Punkte:";
            // 
            // chart2
            // 
            chartArea2.Name = "ChartArea1";
            this.chart2.ChartAreas.Add(chartArea2);
            this.chart2.Location = new System.Drawing.Point(338, 25);
            this.chart2.Name = "chart2";
            this.chart2.Size = new System.Drawing.Size(320, 183);
            this.chart2.TabIndex = 88;
            this.chart2.Text = "chart2";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(10, 9);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(45, 13);
            this.label10.TabIndex = 95;
            this.label10.Text = "Original:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(335, 9);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(58, 13);
            this.label11.TabIndex = 96;
            this.label11.Text = "Bearbeitet:";
            // 
            // edited_image
            // 
            this.edited_image.Location = new System.Drawing.Point(338, 227);
            this.edited_image.Name = "edited_image";
            this.edited_image.Size = new System.Drawing.Size(320, 240);
            this.edited_image.TabIndex = 98;
            this.edited_image.TabStop = false;
            // 
            // reset
            // 
            this.reset.Location = new System.Drawing.Point(903, 613);
            this.reset.Name = "reset";
            this.reset.Size = new System.Drawing.Size(87, 44);
            this.reset.TabIndex = 99;
            this.reset.Text = "Reset";
            this.reset.UseVisualStyleBackColor = true;
            this.reset.Click += new System.EventHandler(this.reset_Click);
            // 
            // original_image
            // 
            this.original_image.Location = new System.Drawing.Point(13, 228);
            this.original_image.Name = "original_image";
            this.original_image.Size = new System.Drawing.Size(320, 239);
            this.original_image.TabIndex = 100;
            this.original_image.TabStop = false;
            // 
            // normalise_button
            // 
            this.normalise_button.Location = new System.Drawing.Point(6, 19);
            this.normalise_button.Name = "normalise_button";
            this.normalise_button.Size = new System.Drawing.Size(85, 33);
            this.normalise_button.TabIndex = 103;
            this.normalise_button.Text = "Normalisieren";
            this.normalise_button.UseVisualStyleBackColor = true;
            this.normalise_button.Click += new System.EventHandler(this.normalise_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(95, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(27, 13);
            this.label5.TabIndex = 105;
            this.label5.Text = "Min:";
            // 
            // normalise_min
            // 
            this.normalise_min.Location = new System.Drawing.Point(123, 11);
            this.normalise_min.Name = "normalise_min";
            this.normalise_min.Size = new System.Drawing.Size(29, 20);
            this.normalise_min.TabIndex = 104;
            this.normalise_min.Text = "0";
            this.normalise_min.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(93, 39);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(30, 13);
            this.label7.TabIndex = 107;
            this.label7.Text = "Max:";
            // 
            // normalise_max
            // 
            this.normalise_max.Location = new System.Drawing.Point(123, 37);
            this.normalise_max.Name = "normalise_max";
            this.normalise_max.Size = new System.Drawing.Size(29, 20);
            this.normalise_max.TabIndex = 106;
            this.normalise_max.Text = "255";
            this.normalise_max.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.normalise_button);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.normalise_min);
            this.groupBox2.Controls.Add(this.normalise_max);
            this.groupBox2.Location = new System.Drawing.Point(670, 9);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(156, 65);
            this.groupBox2.TabIndex = 109;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Normalisieren";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.normalise_temp);
            this.groupBox3.Controls.Add(this.sobel);
            this.groupBox3.Controls.Add(this.log);
            this.groupBox3.Controls.Add(this.direct);
            this.groupBox3.Controls.Add(this.gauss);
            this.groupBox3.Controls.Add(this.apply_template);
            this.groupBox3.Controls.Add(this.template2_2);
            this.groupBox3.Controls.Add(this.template2_1);
            this.groupBox3.Controls.Add(this.template2_0);
            this.groupBox3.Controls.Add(this.template1_2);
            this.groupBox3.Controls.Add(this.template1_1);
            this.groupBox3.Controls.Add(this.template1_0);
            this.groupBox3.Controls.Add(this.template0_2);
            this.groupBox3.Controls.Add(this.template0_1);
            this.groupBox3.Controls.Add(this.template0_0);
            this.groupBox3.Location = new System.Drawing.Point(670, 75);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(320, 136);
            this.groupBox3.TabIndex = 110;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Konvolution";
            // 
            // normalise_temp
            // 
            this.normalise_temp.Location = new System.Drawing.Point(13, 97);
            this.normalise_temp.Name = "normalise_temp";
            this.normalise_temp.Size = new System.Drawing.Size(156, 33);
            this.normalise_temp.TabIndex = 111;
            this.normalise_temp.Text = "Normalisieren";
            this.normalise_temp.UseVisualStyleBackColor = true;
            this.normalise_temp.Click += new System.EventHandler(this.normalise_temp_Click);
            // 
            // sobel
            // 
            this.sobel.Location = new System.Drawing.Point(188, 58);
            this.sobel.Name = "sobel";
            this.sobel.Size = new System.Drawing.Size(57, 33);
            this.sobel.TabIndex = 110;
            this.sobel.Text = "Sobel";
            this.sobel.UseVisualStyleBackColor = true;
            this.sobel.Click += new System.EventHandler(this.sobel_Click);
            // 
            // log
            // 
            this.log.Location = new System.Drawing.Point(251, 58);
            this.log.Name = "log";
            this.log.Size = new System.Drawing.Size(57, 33);
            this.log.TabIndex = 109;
            this.log.Text = "LoG";
            this.log.UseVisualStyleBackColor = true;
            this.log.Click += new System.EventHandler(this.log_Click);
            // 
            // direct
            // 
            this.direct.Location = new System.Drawing.Point(188, 19);
            this.direct.Name = "direct";
            this.direct.Size = new System.Drawing.Size(57, 33);
            this.direct.TabIndex = 108;
            this.direct.Text = "Direkt";
            this.direct.UseVisualStyleBackColor = true;
            this.direct.Click += new System.EventHandler(this.direct_Click);
            // 
            // gauss
            // 
            this.gauss.Location = new System.Drawing.Point(251, 19);
            this.gauss.Name = "gauss";
            this.gauss.Size = new System.Drawing.Size(57, 33);
            this.gauss.TabIndex = 107;
            this.gauss.Text = "Gauß";
            this.gauss.UseVisualStyleBackColor = true;
            this.gauss.Click += new System.EventHandler(this.gauss_Click);
            // 
            // apply_template
            // 
            this.apply_template.Location = new System.Drawing.Point(188, 97);
            this.apply_template.Name = "apply_template";
            this.apply_template.Size = new System.Drawing.Size(120, 33);
            this.apply_template.TabIndex = 103;
            this.apply_template.Text = "Anwenden";
            this.apply_template.UseVisualStyleBackColor = true;
            this.apply_template.Click += new System.EventHandler(this.apply_template_Click);
            // 
            // template2_2
            // 
            this.template2_2.Location = new System.Drawing.Point(121, 71);
            this.template2_2.Name = "template2_2";
            this.template2_2.Size = new System.Drawing.Size(48, 20);
            this.template2_2.TabIndex = 10;
            this.template2_2.Text = "0";
            this.template2_2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // template2_1
            // 
            this.template2_1.Location = new System.Drawing.Point(67, 71);
            this.template2_1.Name = "template2_1";
            this.template2_1.Size = new System.Drawing.Size(48, 20);
            this.template2_1.TabIndex = 9;
            this.template2_1.Text = "2";
            this.template2_1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // template2_0
            // 
            this.template2_0.Location = new System.Drawing.Point(13, 71);
            this.template2_0.Name = "template2_0";
            this.template2_0.Size = new System.Drawing.Size(48, 20);
            this.template2_0.TabIndex = 8;
            this.template2_0.Text = "0";
            this.template2_0.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // template1_2
            // 
            this.template1_2.Location = new System.Drawing.Point(121, 45);
            this.template1_2.Name = "template1_2";
            this.template1_2.Size = new System.Drawing.Size(48, 20);
            this.template1_2.TabIndex = 6;
            this.template1_2.Text = "2";
            this.template1_2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // template1_1
            // 
            this.template1_1.Location = new System.Drawing.Point(67, 45);
            this.template1_1.Name = "template1_1";
            this.template1_1.Size = new System.Drawing.Size(48, 20);
            this.template1_1.TabIndex = 5;
            this.template1_1.Text = "0";
            this.template1_1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // template1_0
            // 
            this.template1_0.Location = new System.Drawing.Point(13, 45);
            this.template1_0.Name = "template1_0";
            this.template1_0.Size = new System.Drawing.Size(48, 20);
            this.template1_0.TabIndex = 4;
            this.template1_0.Text = "-2";
            this.template1_0.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // template0_2
            // 
            this.template0_2.Location = new System.Drawing.Point(121, 19);
            this.template0_2.Name = "template0_2";
            this.template0_2.Size = new System.Drawing.Size(48, 20);
            this.template0_2.TabIndex = 2;
            this.template0_2.Text = "0";
            this.template0_2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // template0_1
            // 
            this.template0_1.Location = new System.Drawing.Point(67, 19);
            this.template0_1.Name = "template0_1";
            this.template0_1.Size = new System.Drawing.Size(48, 20);
            this.template0_1.TabIndex = 1;
            this.template0_1.Text = "-2";
            this.template0_1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // template0_0
            // 
            this.template0_0.Location = new System.Drawing.Point(13, 19);
            this.template0_0.Name = "template0_0";
            this.template0_0.Size = new System.Drawing.Size(48, 20);
            this.template0_0.TabIndex = 0;
            this.template0_0.Text = "0";
            this.template0_0.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // undo_button
            // 
            this.undo_button.Location = new System.Drawing.Point(903, 563);
            this.undo_button.Name = "undo_button";
            this.undo_button.Size = new System.Drawing.Size(87, 44);
            this.undo_button.TabIndex = 111;
            this.undo_button.Text = "Rückgängig";
            this.undo_button.UseVisualStyleBackColor = true;
            this.undo_button.Click += new System.EventHandler(this.undo_button_Click);
            // 
            // acc_space
            // 
            this.acc_space.Location = new System.Drawing.Point(670, 227);
            this.acc_space.Name = "acc_space";
            this.acc_space.Size = new System.Drawing.Size(320, 240);
            this.acc_space.TabIndex = 112;
            this.acc_space.TabStop = false;
            this.acc_space.MouseClick += new System.Windows.Forms.MouseEventHandler(this.acc_space_MouseClick);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(667, 212);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(159, 13);
            this.label12.TabIndex = 113;
            this.label12.Text = "Hough Transf./Blob Erkennung:";
            // 
            // hough
            // 
            this.hough.Location = new System.Drawing.Point(61, 133);
            this.hough.Name = "hough";
            this.hough.Size = new System.Drawing.Size(87, 44);
            this.hough.TabIndex = 114;
            this.hough.Text = "Parameterraum berechnen";
            this.hough.UseVisualStyleBackColor = true;
            this.hough.Click += new System.EventHandler(this.hough_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label14);
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Controls.Add(this.lower_thresh);
            this.groupBox4.Controls.Add(this.upper_thresh);
            this.groupBox4.Controls.Add(this.binary);
            this.groupBox4.Location = new System.Drawing.Point(834, 9);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(156, 65);
            this.groupBox4.TabIndex = 115;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Binärbild";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(90, 41);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(33, 13);
            this.label14.TabIndex = 118;
            this.label14.Text = "Min2:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(90, 18);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(33, 13);
            this.label13.TabIndex = 116;
            this.label13.Text = "Min1:";
            // 
            // lower_thresh
            // 
            this.lower_thresh.Location = new System.Drawing.Point(123, 38);
            this.lower_thresh.Name = "lower_thresh";
            this.lower_thresh.Size = new System.Drawing.Size(29, 20);
            this.lower_thresh.TabIndex = 117;
            this.lower_thresh.Text = "40";
            this.lower_thresh.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // upper_thresh
            // 
            this.upper_thresh.Location = new System.Drawing.Point(123, 13);
            this.upper_thresh.Name = "upper_thresh";
            this.upper_thresh.Size = new System.Drawing.Size(29, 20);
            this.upper_thresh.TabIndex = 108;
            this.upper_thresh.Text = "60";
            this.upper_thresh.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // binary
            // 
            this.binary.Location = new System.Drawing.Point(6, 16);
            this.binary.Name = "binary";
            this.binary.Size = new System.Drawing.Size(81, 43);
            this.binary.TabIndex = 116;
            this.binary.Text = "Komprimieren";
            this.binary.UseVisualStyleBackColor = true;
            this.binary.Click += new System.EventHandler(this.binary_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(62, 78);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(43, 13);
            this.label15.TabIndex = 120;
            this.label15.Text = "Radius:";
            // 
            // radius
            // 
            this.radius.Location = new System.Drawing.Point(117, 74);
            this.radius.Name = "radius";
            this.radius.Size = new System.Drawing.Size(29, 20);
            this.radius.TabIndex = 119;
            this.radius.Text = "30";
            this.radius.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(62, 104);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(53, 13);
            this.label16.TabIndex = 122;
            this.label16.Text = "Schwelle:";
            // 
            // hough_thresh
            // 
            this.hough_thresh.Location = new System.Drawing.Point(117, 100);
            this.hough_thresh.Name = "hough_thresh";
            this.hough_thresh.Size = new System.Drawing.Size(29, 20);
            this.hough_thresh.TabIndex = 121;
            this.hough_thresh.Text = "30";
            this.hough_thresh.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // blob
            // 
            this.blob.Location = new System.Drawing.Point(63, 133);
            this.blob.Name = "blob";
            this.blob.Size = new System.Drawing.Size(87, 44);
            this.blob.TabIndex = 123;
            this.blob.Text = "Blobs finden";
            this.blob.UseVisualStyleBackColor = true;
            this.blob.Click += new System.EventHandler(this.blob_Click);
            // 
            // edit_avg
            // 
            this.edit_avg.AutoSize = true;
            this.edit_avg.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.edit_avg.Location = new System.Drawing.Point(399, 211);
            this.edit_avg.Name = "edit_avg";
            this.edit_avg.Size = new System.Drawing.Size(13, 13);
            this.edit_avg.TabIndex = 125;
            this.edit_avg.Text = "--";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(335, 211);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(58, 13);
            this.label18.TabIndex = 124;
            this.label18.Text = "Durschnitt:";
            // 
            // chart3
            // 
            chartArea3.Name = "ChartArea1";
            this.chart3.ChartAreas.Add(chartArea3);
            this.chart3.Location = new System.Drawing.Point(338, 491);
            this.chart3.Name = "chart3";
            this.chart3.Size = new System.Drawing.Size(320, 166);
            this.chart3.TabIndex = 126;
            this.chart3.Text = "chart3";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.hough);
            this.groupBox5.Controls.Add(this.radius);
            this.groupBox5.Controls.Add(this.label15);
            this.groupBox5.Controls.Add(this.hough_thresh);
            this.groupBox5.Controls.Add(this.label16);
            this.groupBox5.Location = new System.Drawing.Point(12, 474);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(156, 183);
            this.groupBox5.TabIndex = 127;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Hough Transformation";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.blob);
            this.groupBox6.Location = new System.Drawing.Point(174, 474);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(156, 183);
            this.groupBox6.TabIndex = 128;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Blob Erkennung";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(336, 474);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 13);
            this.label1.TabIndex = 129;
            this.label1.Text = "Blob Größenverteilung:";
            // 
            // avg_blob
            // 
            this.avg_blob.AutoSize = true;
            this.avg_blob.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.avg_blob.Location = new System.Drawing.Point(759, 620);
            this.avg_blob.Name = "avg_blob";
            this.avg_blob.Size = new System.Drawing.Size(13, 13);
            this.avg_blob.TabIndex = 131;
            this.avg_blob.Text = "--";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(667, 620);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(91, 13);
            this.label19.TabIndex = 130;
            this.label19.Text = "Durchsch. Größe:";
            // 
            // auto
            // 
            this.auto.Location = new System.Drawing.Point(903, 491);
            this.auto.Name = "auto";
            this.auto.Size = new System.Drawing.Size(87, 66);
            this.auto.TabIndex = 132;
            this.auto.Text = "Auto";
            this.auto.UseVisualStyleBackColor = true;
            this.auto.Click += new System.EventHandler(this.auto_Click);
            // 
            // height
            // 
            this.height.LargeChange = 10;
            this.height.Location = new System.Drawing.Point(834, 514);
            this.height.Maximum = 100;
            this.height.Name = "height";
            this.height.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.height.Size = new System.Drawing.Size(45, 143);
            this.height.SmallChange = 5;
            this.height.TabIndex = 133;
            this.height.TickFrequency = 10;
            this.height.TickStyle = System.Windows.Forms.TickStyle.Both;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(823, 498);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(36, 13);
            this.label17.TabIndex = 134;
            this.label17.Text = "Höhe:";
            // 
            // height_perc
            // 
            this.height_perc.AutoSize = true;
            this.height_perc.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.height_perc.Location = new System.Drawing.Point(861, 498);
            this.height_perc.Name = "height_perc";
            this.height_perc.Size = new System.Drawing.Size(13, 13);
            this.height_perc.TabIndex = 135;
            this.height_perc.Text = "--";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(804, 520);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(33, 13);
            this.label21.TabIndex = 136;
            this.label21.Text = "100%";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(815, 637);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(21, 13);
            this.label22.TabIndex = 137;
            this.label22.Text = "0%";
            // 
            // height_direct
            // 
            this.height_direct.Location = new System.Drawing.Point(670, 491);
            this.height_direct.Name = "height_direct";
            this.height_direct.Size = new System.Drawing.Size(128, 42);
            this.height_direct.TabIndex = 138;
            this.height_direct.Text = "Höhe Direkt";
            this.height_direct.UseVisualStyleBackColor = true;
            this.height_direct.Click += new System.EventHandler(this.height_direct_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(999, 663);
            this.Controls.Add(this.height_direct);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.height_perc);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.height);
            this.Controls.Add(this.auto);
            this.Controls.Add(this.avg_blob);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.chart3);
            this.Controls.Add(this.edit_avg);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.acc_space);
            this.Controls.Add(this.undo_button);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.original_image);
            this.Controls.Add(this.reset);
            this.Controls.Add(this.edited_image);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.edit_max);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.edit_min);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.blob_count);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.chart2);
            this.Controls.Add(this.orig_max);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.orig_min);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.orig_avg);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.chart1);
            this.Name = "Form2";
            this.Text = "Bildbearbeitung";
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edited_image)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.original_image)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.acc_space)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart3)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.height)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label orig_max;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label orig_min;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label orig_avg;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Label edit_max;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label edit_min;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label blob_count;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.PictureBox edited_image;
        private System.Windows.Forms.Button reset;
        private System.Windows.Forms.PictureBox original_image;
        private System.Windows.Forms.Button normalise_button;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox normalise_min;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox normalise_max;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox template0_0;
        private System.Windows.Forms.Button apply_template;
        private System.Windows.Forms.TextBox template2_2;
        private System.Windows.Forms.TextBox template2_1;
        private System.Windows.Forms.TextBox template2_0;
        private System.Windows.Forms.TextBox template1_2;
        private System.Windows.Forms.TextBox template1_1;
        private System.Windows.Forms.TextBox template1_0;
        private System.Windows.Forms.TextBox template0_2;
        private System.Windows.Forms.TextBox template0_1;
        private System.Windows.Forms.Button sobel;
        private System.Windows.Forms.Button log;
        private System.Windows.Forms.Button direct;
        private System.Windows.Forms.Button gauss;
        private System.Windows.Forms.Button normalise_temp;
        private System.Windows.Forms.Button undo_button;
        private System.Windows.Forms.PictureBox acc_space;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button hough;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button binary;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox lower_thresh;
        private System.Windows.Forms.TextBox upper_thresh;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox radius;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox hough_thresh;
        private System.Windows.Forms.Button blob;
        private System.Windows.Forms.Label edit_avg;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart3;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label avg_blob;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Button auto;
        private System.Windows.Forms.TrackBar height;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label height_perc;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Button height_direct;
    }
}