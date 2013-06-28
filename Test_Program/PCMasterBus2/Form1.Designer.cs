namespace PCMasterBus2
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.SelectPort = new System.Windows.Forms.ComboBox();
            this.btn_Init = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.button_adr = new System.Windows.Forms.Button();
            this.button_status = new System.Windows.Forms.Button();
            this.button_temp_messen = new System.Windows.Forms.Button();
            this.button_reset = new System.Windows.Forms.Button();
            this.label22 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.take_bin_pic = new System.Windows.Forms.Button();
            this.get_bin_pic = new System.Windows.Forms.Button();
            this.box_zelle = new System.Windows.Forms.GroupBox();
            this.wert_height = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.LED_setting = new System.Windows.Forms.Label();
            this.save_pic = new System.Windows.Forms.Button();
            this.load_pic = new System.Windows.Forms.Button();
            this.save_param = new System.Windows.Forms.Button();
            this.cam_settings = new System.Windows.Forms.Button();
            this.analyse_pic = new System.Windows.Forms.Button();
            this.take_test_pic = new System.Windows.Forms.Button();
            this.full_grey_pic = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.grey_pic_section = new System.Windows.Forms.NumericUpDown();
            this.LED_group3 = new System.Windows.Forms.GroupBox();
            this.LED3_aus = new System.Windows.Forms.RadioButton();
            this.LED3_an = new System.Windows.Forms.RadioButton();
            this.LED_group2 = new System.Windows.Forms.GroupBox();
            this.LED2_an = new System.Windows.Forms.RadioButton();
            this.LED2_aus = new System.Windows.Forms.RadioButton();
            this.LED_group1 = new System.Windows.Forms.GroupBox();
            this.LED1_an = new System.Windows.Forms.RadioButton();
            this.LED1_aus = new System.Windows.Forms.RadioButton();
            this.wert_druck = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.wert_temp = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.status_kam = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.status_druck = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.status_temp = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.LED_helligkeit = new System.Windows.Forms.TrackBar();
            this.label15 = new System.Windows.Forms.Label();
            this.get_grey_pic = new System.Windows.Forms.Button();
            this.take_grey_pic = new System.Windows.Forms.Button();
            this.Adresse = new System.Windows.Forms.TextBox();
            this.button_druck_messen = new System.Windows.Forms.Button();
            this.btn_userprog = new System.Windows.Forms.Button();
            this.AutoI_Adresse = new System.Windows.Forms.CheckBox();
            this.AutoI_UProg = new System.Windows.Forms.CheckBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusText = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.box_zelle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grey_pic_section)).BeginInit();
            this.LED_group3.SuspendLayout();
            this.LED_group2.SuspendLayout();
            this.LED_group1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LED_helligkeit)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Port :";
            // 
            // SelectPort
            // 
            this.SelectPort.FormattingEnabled = true;
            this.SelectPort.Location = new System.Drawing.Point(139, 13);
            this.SelectPort.Name = "SelectPort";
            this.SelectPort.Size = new System.Drawing.Size(87, 21);
            this.SelectPort.TabIndex = 4;
            // 
            // btn_Init
            // 
            this.btn_Init.Location = new System.Drawing.Point(256, 9);
            this.btn_Init.Name = "btn_Init";
            this.btn_Init.Size = new System.Drawing.Size(101, 23);
            this.btn_Init.TabIndex = 5;
            this.btn_Init.Text = "Init";
            this.btn_Init.UseVisualStyleBackColor = true;
            this.btn_Init.Click += new System.EventHandler(this.btn_Init_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(9, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 17);
            this.label2.TabIndex = 8;
            this.label2.Text = "Zellenadresse:";
            // 
            // button_adr
            // 
            this.button_adr.Location = new System.Drawing.Point(178, 45);
            this.button_adr.Name = "button_adr";
            this.button_adr.Size = new System.Drawing.Size(42, 23);
            this.button_adr.TabIndex = 9;
            this.button_adr.Text = "Adr. senden";
            this.button_adr.UseVisualStyleBackColor = true;
            this.button_adr.Click += new System.EventHandler(this.button_adr_Click);
            // 
            // button_status
            // 
            this.button_status.Location = new System.Drawing.Point(468, 10);
            this.button_status.Name = "button_status";
            this.button_status.Size = new System.Drawing.Size(62, 57);
            this.button_status.TabIndex = 17;
            this.button_status.Text = "Status abrufen";
            this.button_status.UseVisualStyleBackColor = true;
            this.button_status.Click += new System.EventHandler(this.button_status_Click);
            // 
            // button_temp_messen
            // 
            this.button_temp_messen.Location = new System.Drawing.Point(550, 11);
            this.button_temp_messen.Name = "button_temp_messen";
            this.button_temp_messen.Size = new System.Drawing.Size(91, 25);
            this.button_temp_messen.TabIndex = 23;
            this.button_temp_messen.Text = "Temp. messen";
            this.button_temp_messen.UseVisualStyleBackColor = true;
            this.button_temp_messen.Click += new System.EventHandler(this.button_temp_messen_Click_1);
            // 
            // button_reset
            // 
            this.button_reset.Location = new System.Drawing.Point(400, 11);
            this.button_reset.Name = "button_reset";
            this.button_reset.Size = new System.Drawing.Size(62, 57);
            this.button_reset.TabIndex = 24;
            this.button_reset.Text = "Soft. reset";
            this.button_reset.UseVisualStyleBackColor = true;
            this.button_reset.Click += new System.EventHandler(this.button_reset_Click);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(6, 260);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(68, 17);
            this.label22.TabIndex = 39;
            this.label22.Text = "Kamera:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(112, 260);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(320, 240);
            this.pictureBox1.TabIndex = 40;
            this.pictureBox1.TabStop = false;
            // 
            // take_bin_pic
            // 
            this.take_bin_pic.Location = new System.Drawing.Point(467, 260);
            this.take_bin_pic.Name = "take_bin_pic";
            this.take_bin_pic.Size = new System.Drawing.Size(78, 50);
            this.take_bin_pic.TabIndex = 41;
            this.take_bin_pic.Text = "Binärbild Aufnehmen";
            this.take_bin_pic.UseVisualStyleBackColor = true;
            this.take_bin_pic.Click += new System.EventHandler(this.take_bin_pic_Click);
            // 
            // get_bin_pic
            // 
            this.get_bin_pic.Location = new System.Drawing.Point(467, 316);
            this.get_bin_pic.Name = "get_bin_pic";
            this.get_bin_pic.Size = new System.Drawing.Size(78, 49);
            this.get_bin_pic.TabIndex = 43;
            this.get_bin_pic.Text = "Binärbild Übertragen";
            this.get_bin_pic.UseVisualStyleBackColor = true;
            this.get_bin_pic.Click += new System.EventHandler(this.get_bin_pic_Click);
            // 
            // box_zelle
            // 
            this.box_zelle.Controls.Add(this.wert_height);
            this.box_zelle.Controls.Add(this.label8);
            this.box_zelle.Controls.Add(this.LED_setting);
            this.box_zelle.Controls.Add(this.save_pic);
            this.box_zelle.Controls.Add(this.load_pic);
            this.box_zelle.Controls.Add(this.save_param);
            this.box_zelle.Controls.Add(this.cam_settings);
            this.box_zelle.Controls.Add(this.analyse_pic);
            this.box_zelle.Controls.Add(this.take_test_pic);
            this.box_zelle.Controls.Add(this.full_grey_pic);
            this.box_zelle.Controls.Add(this.label5);
            this.box_zelle.Controls.Add(this.grey_pic_section);
            this.box_zelle.Controls.Add(this.LED_group3);
            this.box_zelle.Controls.Add(this.LED_group2);
            this.box_zelle.Controls.Add(this.LED_group1);
            this.box_zelle.Controls.Add(this.wert_druck);
            this.box_zelle.Controls.Add(this.label12);
            this.box_zelle.Controls.Add(this.wert_temp);
            this.box_zelle.Controls.Add(this.label14);
            this.box_zelle.Controls.Add(this.label10);
            this.box_zelle.Controls.Add(this.status_kam);
            this.box_zelle.Controls.Add(this.label9);
            this.box_zelle.Controls.Add(this.status_druck);
            this.box_zelle.Controls.Add(this.label7);
            this.box_zelle.Controls.Add(this.status_temp);
            this.box_zelle.Controls.Add(this.label4);
            this.box_zelle.Controls.Add(this.label3);
            this.box_zelle.Controls.Add(this.label21);
            this.box_zelle.Controls.Add(this.label20);
            this.box_zelle.Controls.Add(this.label16);
            this.box_zelle.Controls.Add(this.LED_helligkeit);
            this.box_zelle.Controls.Add(this.label15);
            this.box_zelle.Controls.Add(this.label22);
            this.box_zelle.Controls.Add(this.pictureBox1);
            this.box_zelle.Controls.Add(this.get_grey_pic);
            this.box_zelle.Controls.Add(this.take_grey_pic);
            this.box_zelle.Controls.Add(this.get_bin_pic);
            this.box_zelle.Controls.Add(this.take_bin_pic);
            this.box_zelle.Enabled = false;
            this.box_zelle.Location = new System.Drawing.Point(12, 77);
            this.box_zelle.Name = "box_zelle";
            this.box_zelle.Size = new System.Drawing.Size(636, 551);
            this.box_zelle.TabIndex = 45;
            this.box_zelle.TabStop = false;
            this.box_zelle.Text = "Zelle";
            // 
            // wert_height
            // 
            this.wert_height.AutoSize = true;
            this.wert_height.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.wert_height.Location = new System.Drawing.Point(572, 68);
            this.wert_height.Name = "wert_height";
            this.wert_height.Size = new System.Drawing.Size(27, 13);
            this.wert_height.TabIndex = 95;
            this.wert_height.Text = "--- %";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(455, 68);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(36, 13);
            this.label8.TabIndex = 94;
            this.label8.Text = "Höhe:";
            // 
            // LED_setting
            // 
            this.LED_setting.AutoSize = true;
            this.LED_setting.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.LED_setting.Location = new System.Drawing.Point(405, 123);
            this.LED_setting.Name = "LED_setting";
            this.LED_setting.Size = new System.Drawing.Size(27, 13);
            this.LED_setting.TabIndex = 93;
            this.LED_setting.Text = "25%";
            // 
            // save_pic
            // 
            this.save_pic.Location = new System.Drawing.Point(223, 510);
            this.save_pic.Name = "save_pic";
            this.save_pic.Size = new System.Drawing.Size(105, 31);
            this.save_pic.TabIndex = 92;
            this.save_pic.Text = "Bild Speichern";
            this.save_pic.UseVisualStyleBackColor = true;
            this.save_pic.Click += new System.EventHandler(this.save_pic_Click);
            // 
            // load_pic
            // 
            this.load_pic.Location = new System.Drawing.Point(112, 510);
            this.load_pic.Name = "load_pic";
            this.load_pic.Size = new System.Drawing.Size(105, 31);
            this.load_pic.TabIndex = 91;
            this.load_pic.Text = "Bild Öffnen";
            this.load_pic.UseVisualStyleBackColor = true;
            this.load_pic.Click += new System.EventHandler(this.load_pic_Click);
            // 
            // save_param
            // 
            this.save_param.Location = new System.Drawing.Point(331, 510);
            this.save_param.Name = "save_param";
            this.save_param.Size = new System.Drawing.Size(105, 31);
            this.save_param.TabIndex = 90;
            this.save_param.Text = "Param. Speichern";
            this.save_param.UseVisualStyleBackColor = true;
            this.save_param.Click += new System.EventHandler(this.save_param_Click_1);
            // 
            // cam_settings
            // 
            this.cam_settings.Location = new System.Drawing.Point(467, 510);
            this.cam_settings.Name = "cam_settings";
            this.cam_settings.Size = new System.Drawing.Size(162, 31);
            this.cam_settings.TabIndex = 89;
            this.cam_settings.Text = "Kamera Einstellungen";
            this.cam_settings.UseVisualStyleBackColor = true;
            this.cam_settings.Click += new System.EventHandler(this.cam_settings_Click);
            // 
            // analyse_pic
            // 
            this.analyse_pic.Location = new System.Drawing.Point(552, 316);
            this.analyse_pic.Name = "analyse_pic";
            this.analyse_pic.Size = new System.Drawing.Size(78, 49);
            this.analyse_pic.TabIndex = 78;
            this.analyse_pic.Text = "Bearbeitung";
            this.analyse_pic.UseVisualStyleBackColor = true;
            this.analyse_pic.Click += new System.EventHandler(this.analyse_pic_Click);
            // 
            // take_test_pic
            // 
            this.take_test_pic.Location = new System.Drawing.Point(551, 260);
            this.take_test_pic.Name = "take_test_pic";
            this.take_test_pic.Size = new System.Drawing.Size(78, 50);
            this.take_test_pic.TabIndex = 77;
            this.take_test_pic.Text = "Testbild";
            this.take_test_pic.UseVisualStyleBackColor = true;
            this.take_test_pic.Click += new System.EventHandler(this.take_test_pic_Click);
            // 
            // full_grey_pic
            // 
            this.full_grey_pic.Location = new System.Drawing.Point(552, 449);
            this.full_grey_pic.Name = "full_grey_pic";
            this.full_grey_pic.Size = new System.Drawing.Size(78, 51);
            this.full_grey_pic.TabIndex = 76;
            this.full_grey_pic.Text = "Komplettes Graubild";
            this.full_grey_pic.UseVisualStyleBackColor = true;
            this.full_grey_pic.Click += new System.EventHandler(this.full_grey_pic_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(559, 396);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 13);
            this.label5.TabIndex = 75;
            this.label5.Text = "Abschnitt:";
            // 
            // grey_pic_section
            // 
            this.grey_pic_section.Location = new System.Drawing.Point(565, 412);
            this.grey_pic_section.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.grey_pic_section.Name = "grey_pic_section";
            this.grey_pic_section.Size = new System.Drawing.Size(40, 20);
            this.grey_pic_section.TabIndex = 74;
            // 
            // LED_group3
            // 
            this.LED_group3.BackColor = System.Drawing.Color.Transparent;
            this.LED_group3.Controls.Add(this.LED3_aus);
            this.LED_group3.Controls.Add(this.LED3_an);
            this.LED_group3.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.LED_group3.Location = new System.Drawing.Point(136, 189);
            this.LED_group3.Name = "LED_group3";
            this.LED_group3.Size = new System.Drawing.Size(137, 36);
            this.LED_group3.TabIndex = 73;
            this.LED_group3.TabStop = false;
            this.LED_group3.Text = "LED3";
            // 
            // LED3_aus
            // 
            this.LED3_aus.AutoSize = true;
            this.LED3_aus.ForeColor = System.Drawing.SystemColors.ControlText;
            this.LED3_aus.Location = new System.Drawing.Point(80, 14);
            this.LED3_aus.Name = "LED3_aus";
            this.LED3_aus.Size = new System.Drawing.Size(43, 17);
            this.LED3_aus.TabIndex = 56;
            this.LED3_aus.Text = "Aus";
            this.LED3_aus.UseVisualStyleBackColor = true;
            this.LED3_aus.CheckedChanged += new System.EventHandler(this.LED3_aus_CheckedChanged);
            // 
            // LED3_an
            // 
            this.LED3_an.AutoSize = true;
            this.LED3_an.Checked = true;
            this.LED3_an.ForeColor = System.Drawing.SystemColors.ControlText;
            this.LED3_an.Location = new System.Drawing.Point(21, 15);
            this.LED3_an.Name = "LED3_an";
            this.LED3_an.Size = new System.Drawing.Size(38, 17);
            this.LED3_an.TabIndex = 55;
            this.LED3_an.TabStop = true;
            this.LED3_an.Text = "An";
            this.LED3_an.UseVisualStyleBackColor = true;
            this.LED3_an.CheckedChanged += new System.EventHandler(this.LED3_an_CheckedChanged);
            // 
            // LED_group2
            // 
            this.LED_group2.BackColor = System.Drawing.Color.Transparent;
            this.LED_group2.Controls.Add(this.LED2_an);
            this.LED_group2.Controls.Add(this.LED2_aus);
            this.LED_group2.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.LED_group2.Location = new System.Drawing.Point(136, 154);
            this.LED_group2.Name = "LED_group2";
            this.LED_group2.Size = new System.Drawing.Size(137, 33);
            this.LED_group2.TabIndex = 72;
            this.LED_group2.TabStop = false;
            this.LED_group2.Text = "LED2";
            // 
            // LED2_an
            // 
            this.LED2_an.AutoSize = true;
            this.LED2_an.Checked = true;
            this.LED2_an.ForeColor = System.Drawing.SystemColors.ControlText;
            this.LED2_an.Location = new System.Drawing.Point(21, 13);
            this.LED2_an.Name = "LED2_an";
            this.LED2_an.Size = new System.Drawing.Size(38, 17);
            this.LED2_an.TabIndex = 52;
            this.LED2_an.TabStop = true;
            this.LED2_an.Text = "An";
            this.LED2_an.UseVisualStyleBackColor = true;
            this.LED2_an.CheckedChanged += new System.EventHandler(this.LED2_an_CheckedChanged);
            // 
            // LED2_aus
            // 
            this.LED2_aus.AutoSize = true;
            this.LED2_aus.ForeColor = System.Drawing.SystemColors.ControlText;
            this.LED2_aus.Location = new System.Drawing.Point(80, 13);
            this.LED2_aus.Name = "LED2_aus";
            this.LED2_aus.Size = new System.Drawing.Size(43, 17);
            this.LED2_aus.TabIndex = 53;
            this.LED2_aus.Text = "Aus";
            this.LED2_aus.UseVisualStyleBackColor = true;
            this.LED2_aus.CheckedChanged += new System.EventHandler(this.LED2_aus_CheckedChanged);
            // 
            // LED_group1
            // 
            this.LED_group1.BackColor = System.Drawing.Color.Transparent;
            this.LED_group1.Controls.Add(this.LED1_an);
            this.LED_group1.Controls.Add(this.LED1_aus);
            this.LED_group1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.LED_group1.Location = new System.Drawing.Point(136, 119);
            this.LED_group1.Name = "LED_group1";
            this.LED_group1.Size = new System.Drawing.Size(137, 34);
            this.LED_group1.TabIndex = 71;
            this.LED_group1.TabStop = false;
            this.LED_group1.Text = "LED1";
            // 
            // LED1_an
            // 
            this.LED1_an.AutoSize = true;
            this.LED1_an.Checked = true;
            this.LED1_an.ForeColor = System.Drawing.SystemColors.ControlText;
            this.LED1_an.Location = new System.Drawing.Point(21, 12);
            this.LED1_an.Name = "LED1_an";
            this.LED1_an.Size = new System.Drawing.Size(38, 17);
            this.LED1_an.TabIndex = 49;
            this.LED1_an.TabStop = true;
            this.LED1_an.Text = "An";
            this.LED1_an.UseVisualStyleBackColor = true;
            this.LED1_an.CheckedChanged += new System.EventHandler(this.LED1_an_CheckedChanged);
            // 
            // LED1_aus
            // 
            this.LED1_aus.AutoSize = true;
            this.LED1_aus.ForeColor = System.Drawing.SystemColors.ControlText;
            this.LED1_aus.Location = new System.Drawing.Point(80, 12);
            this.LED1_aus.Name = "LED1_aus";
            this.LED1_aus.Size = new System.Drawing.Size(43, 17);
            this.LED1_aus.TabIndex = 50;
            this.LED1_aus.Text = "Aus";
            this.LED1_aus.UseVisualStyleBackColor = true;
            this.LED1_aus.CheckedChanged += new System.EventHandler(this.LED1_aus_CheckedChanged);
            // 
            // wert_druck
            // 
            this.wert_druck.AutoSize = true;
            this.wert_druck.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.wert_druck.Location = new System.Drawing.Point(572, 46);
            this.wert_druck.Name = "wert_druck";
            this.wert_druck.Size = new System.Drawing.Size(36, 13);
            this.wert_druck.TabIndex = 70;
            this.wert_druck.Text = "--- PSI";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(455, 46);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(39, 13);
            this.label12.TabIndex = 69;
            this.label12.Text = "Druck:";
            // 
            // wert_temp
            // 
            this.wert_temp.AutoSize = true;
            this.wert_temp.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.wert_temp.Location = new System.Drawing.Point(572, 23);
            this.wert_temp.Name = "wert_temp";
            this.wert_temp.Size = new System.Drawing.Size(30, 13);
            this.wert_temp.TabIndex = 68;
            this.wert_temp.Text = "--- °C";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(455, 23);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(64, 13);
            this.label14.TabIndex = 67;
            this.label14.Text = "Temperatur:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(342, 23);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(56, 17);
            this.label10.TabIndex = 66;
            this.label10.Text = "Werte:";
            // 
            // status_kam
            // 
            this.status_kam.AutoSize = true;
            this.status_kam.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.status_kam.Location = new System.Drawing.Point(250, 68);
            this.status_kam.Name = "status_kam";
            this.status_kam.Size = new System.Drawing.Size(16, 13);
            this.status_kam.TabIndex = 65;
            this.status_kam.Text = "---";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(133, 68);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(46, 13);
            this.label9.TabIndex = 64;
            this.label9.Text = "Kamera:";
            // 
            // status_druck
            // 
            this.status_druck.AutoSize = true;
            this.status_druck.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.status_druck.Location = new System.Drawing.Point(250, 46);
            this.status_druck.Name = "status_druck";
            this.status_druck.Size = new System.Drawing.Size(16, 13);
            this.status_druck.TabIndex = 63;
            this.status_druck.Text = "---";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(133, 46);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 13);
            this.label7.TabIndex = 62;
            this.label7.Text = "Druck:";
            // 
            // status_temp
            // 
            this.status_temp.AutoSize = true;
            this.status_temp.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.status_temp.Location = new System.Drawing.Point(250, 23);
            this.status_temp.Name = "status_temp";
            this.status_temp.Size = new System.Drawing.Size(16, 13);
            this.status_temp.TabIndex = 61;
            this.status_temp.Text = "---";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(133, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 13);
            this.label4.TabIndex = 60;
            this.label4.Text = "Temperatur:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 17);
            this.label3.TabIndex = 59;
            this.label3.Text = "Status:";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(596, 125);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(25, 13);
            this.label21.TabIndex = 58;
            this.label21.Text = "100";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(455, 125);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(13, 13);
            this.label20.TabIndex = 57;
            this.label20.Text = "0";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(339, 123);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(53, 13);
            this.label16.TabIndex = 47;
            this.label16.Text = "Helligkeit:";
            // 
            // LED_helligkeit
            // 
            this.LED_helligkeit.Location = new System.Drawing.Point(467, 120);
            this.LED_helligkeit.Maximum = 100;
            this.LED_helligkeit.Name = "LED_helligkeit";
            this.LED_helligkeit.Size = new System.Drawing.Size(127, 45);
            this.LED_helligkeit.TabIndex = 46;
            this.LED_helligkeit.TickFrequency = 5;
            this.LED_helligkeit.Value = 25;
            this.LED_helligkeit.Scroll += new System.EventHandler(this.LED_helligkeit_Scroll);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(6, 119);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(51, 17);
            this.label15.TabIndex = 45;
            this.label15.Text = "LEDs:";
            // 
            // get_grey_pic
            // 
            this.get_grey_pic.Location = new System.Drawing.Point(467, 449);
            this.get_grey_pic.Name = "get_grey_pic";
            this.get_grey_pic.Size = new System.Drawing.Size(78, 51);
            this.get_grey_pic.TabIndex = 44;
            this.get_grey_pic.Text = "Graubild übertragen";
            this.get_grey_pic.UseVisualStyleBackColor = true;
            this.get_grey_pic.Click += new System.EventHandler(this.get_grey_pic_Click);
            // 
            // take_grey_pic
            // 
            this.take_grey_pic.Location = new System.Drawing.Point(467, 392);
            this.take_grey_pic.Name = "take_grey_pic";
            this.take_grey_pic.Size = new System.Drawing.Size(78, 51);
            this.take_grey_pic.TabIndex = 42;
            this.take_grey_pic.Text = "Graubild aufnehmen";
            this.take_grey_pic.UseVisualStyleBackColor = true;
            this.take_grey_pic.Click += new System.EventHandler(this.take_grey_pic_Click);
            // 
            // Adresse
            // 
            this.Adresse.Location = new System.Drawing.Point(139, 46);
            this.Adresse.Name = "Adresse";
            this.Adresse.Size = new System.Drawing.Size(33, 20);
            this.Adresse.TabIndex = 7;
            this.Adresse.TextChanged += new System.EventHandler(this.Adresse_TextChanged);
            // 
            // button_druck_messen
            // 
            this.button_druck_messen.Location = new System.Drawing.Point(550, 41);
            this.button_druck_messen.Name = "button_druck_messen";
            this.button_druck_messen.Size = new System.Drawing.Size(91, 25);
            this.button_druck_messen.TabIndex = 46;
            this.button_druck_messen.Text = "Druck messen";
            this.button_druck_messen.UseVisualStyleBackColor = true;
            this.button_druck_messen.Click += new System.EventHandler(this.button_druck_messen_Click);
            // 
            // btn_userprog
            // 
            this.btn_userprog.Location = new System.Drawing.Point(284, 44);
            this.btn_userprog.Name = "btn_userprog";
            this.btn_userprog.Size = new System.Drawing.Size(53, 23);
            this.btn_userprog.TabIndex = 47;
            this.btn_userprog.Text = "U Prog";
            this.btn_userprog.UseVisualStyleBackColor = true;
            this.btn_userprog.Click += new System.EventHandler(this.btn_userprog_Click);
            // 
            // AutoI_Adresse
            // 
            this.AutoI_Adresse.AutoSize = true;
            this.AutoI_Adresse.Location = new System.Drawing.Point(226, 48);
            this.AutoI_Adresse.Name = "AutoI_Adresse";
            this.AutoI_Adresse.Size = new System.Drawing.Size(47, 17);
            this.AutoI_Adresse.TabIndex = 48;
            this.AutoI_Adresse.Text = "auto";
            this.AutoI_Adresse.UseVisualStyleBackColor = true;
            // 
            // AutoI_UProg
            // 
            this.AutoI_UProg.AutoSize = true;
            this.AutoI_UProg.Location = new System.Drawing.Point(343, 49);
            this.AutoI_UProg.Name = "AutoI_UProg";
            this.AutoI_UProg.Size = new System.Drawing.Size(47, 17);
            this.AutoI_UProg.TabIndex = 49;
            this.AutoI_UProg.Text = "auto";
            this.AutoI_UProg.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusText});
            this.statusStrip1.Location = new System.Drawing.Point(0, 636);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(660, 22);
            this.statusStrip1.TabIndex = 50;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // StatusText
            // 
            this.StatusText.Name = "StatusText";
            this.StatusText.Size = new System.Drawing.Size(126, 17);
            this.StatusText.Text = "Warten auf Initalisierung";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(660, 658);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.AutoI_UProg);
            this.Controls.Add(this.AutoI_Adresse);
            this.Controls.Add(this.btn_userprog);
            this.Controls.Add(this.button_druck_messen);
            this.Controls.Add(this.box_zelle);
            this.Controls.Add(this.button_reset);
            this.Controls.Add(this.button_temp_messen);
            this.Controls.Add(this.button_status);
            this.Controls.Add(this.button_adr);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Adresse);
            this.Controls.Add(this.btn_Init);
            this.Controls.Add(this.SelectPort);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "PCMasterBus v2.0";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.box_zelle.ResumeLayout(false);
            this.box_zelle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grey_pic_section)).EndInit();
            this.LED_group3.ResumeLayout(false);
            this.LED_group3.PerformLayout();
            this.LED_group2.ResumeLayout(false);
            this.LED_group2.PerformLayout();
            this.LED_group1.ResumeLayout(false);
            this.LED_group1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LED_helligkeit)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox SelectPort;
        private System.Windows.Forms.Button btn_Init;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button_adr;
        private System.Windows.Forms.Button button_status;
        private System.Windows.Forms.Button button_temp_messen;
        private System.Windows.Forms.Button button_reset;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button take_bin_pic;
        private System.Windows.Forms.Button get_bin_pic;
        private System.Windows.Forms.GroupBox box_zelle;
        private System.Windows.Forms.Label wert_druck;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label wert_temp;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label status_kam;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label status_druck;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label status_temp;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.RadioButton LED3_aus;
        private System.Windows.Forms.RadioButton LED3_an;
        private System.Windows.Forms.RadioButton LED2_aus;
        private System.Windows.Forms.RadioButton LED2_an;
        private System.Windows.Forms.RadioButton LED1_aus;
        private System.Windows.Forms.RadioButton LED1_an;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TrackBar LED_helligkeit;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox Adresse;
        private System.Windows.Forms.Button button_druck_messen;
        private System.Windows.Forms.GroupBox LED_group3;
        private System.Windows.Forms.GroupBox LED_group2;
        private System.Windows.Forms.GroupBox LED_group1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown grey_pic_section;
        private System.Windows.Forms.Button take_test_pic;
        private System.Windows.Forms.Button analyse_pic;
        private System.Windows.Forms.Button cam_settings;
        private System.Windows.Forms.Button full_grey_pic;
        private System.Windows.Forms.Button get_grey_pic;
        private System.Windows.Forms.Button take_grey_pic;
        private System.Windows.Forms.Button save_param;
        private System.Windows.Forms.Button btn_userprog;
        private System.Windows.Forms.CheckBox AutoI_Adresse;
        private System.Windows.Forms.CheckBox AutoI_UProg;
        private System.Windows.Forms.Button save_pic;
        private System.Windows.Forms.Button load_pic;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel StatusText;
        private System.Windows.Forms.Label LED_setting;
        private System.Windows.Forms.Label wert_height;
        private System.Windows.Forms.Label label8;
    }
}

