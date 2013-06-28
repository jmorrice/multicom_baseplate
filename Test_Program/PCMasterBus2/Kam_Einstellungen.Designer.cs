namespace PCMasterBus2
{
    partial class Kam_Einstellungen
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
            this.cam_read = new System.Windows.Forms.Button();
            this.label17 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.cam_reg_log = new System.Windows.Forms.Label();
            this.cam_write = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.reg_val = new System.Windows.Forms.TextBox();
            this.reg_address = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.AGC_off = new System.Windows.Forms.RadioButton();
            this.AGC_on = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.AWB_off = new System.Windows.Forms.RadioButton();
            this.AWB_on = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.gain = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.exposuretime = new System.Windows.Forms.Label();
            this.refresh = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.AEC_off = new System.Windows.Forms.RadioButton();
            this.AEC_on = new System.Windows.Forms.RadioButton();
            this.write_settings = new System.Windows.Forms.Button();
            this.exposuretime_user = new System.Windows.Forms.TextBox();
            this.gain_user = new System.Windows.Forms.TextBox();
            this.R_Gain = new System.Windows.Forms.TextBox();
            this.rgb_gain = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.G_Gain = new System.Windows.Forms.TextBox();
            this.B_Gain = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // cam_read
            // 
            this.cam_read.Location = new System.Drawing.Point(273, 244);
            this.cam_read.Name = "cam_read";
            this.cam_read.Size = new System.Drawing.Size(78, 46);
            this.cam_read.TabIndex = 99;
            this.cam_read.Text = "Lesen";
            this.cam_read.UseVisualStyleBackColor = true;
            this.cam_read.Click += new System.EventHandler(this.cam_read_Click);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(111, 273);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(18, 13);
            this.label17.TabIndex = 98;
            this.label17.Text = "0x";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(111, 247);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(18, 13);
            this.label13.TabIndex = 97;
            this.label13.Text = "0x";
            // 
            // cam_reg_log
            // 
            this.cam_reg_log.AutoSize = true;
            this.cam_reg_log.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.cam_reg_log.Location = new System.Drawing.Point(190, 299);
            this.cam_reg_log.Name = "cam_reg_log";
            this.cam_reg_log.Size = new System.Drawing.Size(131, 13);
            this.cam_reg_log.TabIndex = 96;
            this.cam_reg_log.Text = "Kein Register beschrieben";
            // 
            // cam_write
            // 
            this.cam_write.Location = new System.Drawing.Point(189, 244);
            this.cam_write.Name = "cam_write";
            this.cam_write.Size = new System.Drawing.Size(78, 46);
            this.cam_write.TabIndex = 95;
            this.cam_write.Text = "Schreiben";
            this.cam_write.UseVisualStyleBackColor = true;
            this.cam_write.Click += new System.EventHandler(this.cam_write_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(19, 273);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(33, 13);
            this.label11.TabIndex = 94;
            this.label11.Text = "Wert:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(19, 247);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(48, 13);
            this.label8.TabIndex = 93;
            this.label8.Text = "Adresse:";
            // 
            // reg_val
            // 
            this.reg_val.Location = new System.Drawing.Point(129, 270);
            this.reg_val.Name = "reg_val";
            this.reg_val.Size = new System.Drawing.Size(49, 20);
            this.reg_val.TabIndex = 92;
            // 
            // reg_address
            // 
            this.reg_address.Location = new System.Drawing.Point(129, 244);
            this.reg_address.Name = "reg_address";
            this.reg_address.Size = new System.Drawing.Size(49, 20);
            this.reg_address.TabIndex = 90;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(-151, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 91;
            this.label6.Text = "Register:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.AGC_off);
            this.groupBox1.Controls.Add(this.AGC_on);
            this.groupBox1.Location = new System.Drawing.Point(12, 17);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(156, 48);
            this.groupBox1.TabIndex = 100;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Automatic Gain Control";
            // 
            // AGC_off
            // 
            this.AGC_off.AutoSize = true;
            this.AGC_off.Checked = true;
            this.AGC_off.Location = new System.Drawing.Point(64, 22);
            this.AGC_off.Name = "AGC_off";
            this.AGC_off.Size = new System.Drawing.Size(43, 17);
            this.AGC_off.TabIndex = 1;
            this.AGC_off.TabStop = true;
            this.AGC_off.Text = "Aus";
            this.AGC_off.UseVisualStyleBackColor = true;
            // 
            // AGC_on
            // 
            this.AGC_on.AutoSize = true;
            this.AGC_on.Location = new System.Drawing.Point(17, 22);
            this.AGC_on.Name = "AGC_on";
            this.AGC_on.Size = new System.Drawing.Size(38, 17);
            this.AGC_on.TabIndex = 0;
            this.AGC_on.Text = "An";
            this.AGC_on.UseVisualStyleBackColor = true;
            this.AGC_on.CheckedChanged += new System.EventHandler(this.AGC_on_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.AWB_off);
            this.groupBox2.Controls.Add(this.AWB_on);
            this.groupBox2.Location = new System.Drawing.Point(193, 17);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(155, 48);
            this.groupBox2.TabIndex = 101;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Automatic White Balance";
            // 
            // AWB_off
            // 
            this.AWB_off.AutoSize = true;
            this.AWB_off.Checked = true;
            this.AWB_off.Location = new System.Drawing.Point(63, 22);
            this.AWB_off.Name = "AWB_off";
            this.AWB_off.Size = new System.Drawing.Size(43, 17);
            this.AWB_off.TabIndex = 1;
            this.AWB_off.TabStop = true;
            this.AWB_off.Text = "Aus";
            this.AWB_off.UseVisualStyleBackColor = true;
            // 
            // AWB_on
            // 
            this.AWB_on.AutoSize = true;
            this.AWB_on.Location = new System.Drawing.Point(16, 22);
            this.AWB_on.Name = "AWB_on";
            this.AWB_on.Size = new System.Drawing.Size(38, 17);
            this.AWB_on.TabIndex = 0;
            this.AWB_on.Text = "An";
            this.AWB_on.UseVisualStyleBackColor = true;
            this.AWB_on.CheckedChanged += new System.EventHandler(this.AWB_on_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 147);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 102;
            this.label1.Text = "Gain:";
            // 
            // gain
            // 
            this.gain.AutoSize = true;
            this.gain.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.gain.Location = new System.Drawing.Point(111, 144);
            this.gain.Name = "gain";
            this.gain.Size = new System.Drawing.Size(16, 13);
            this.gain.TabIndex = 103;
            this.gain.Text = "---";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 177);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 107;
            this.label2.Text = "Belichtungszeit:";
            // 
            // exposuretime
            // 
            this.exposuretime.AutoSize = true;
            this.exposuretime.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.exposuretime.Location = new System.Drawing.Point(113, 177);
            this.exposuretime.Name = "exposuretime";
            this.exposuretime.Size = new System.Drawing.Size(16, 13);
            this.exposuretime.TabIndex = 108;
            this.exposuretime.Text = "---";
            // 
            // refresh
            // 
            this.refresh.Location = new System.Drawing.Point(193, 71);
            this.refresh.Name = "refresh";
            this.refresh.Size = new System.Drawing.Size(155, 48);
            this.refresh.TabIndex = 109;
            this.refresh.Text = "Aktualisieren";
            this.refresh.UseVisualStyleBackColor = true;
            this.refresh.Click += new System.EventHandler(this.refresh_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.AEC_off);
            this.groupBox3.Controls.Add(this.AEC_on);
            this.groupBox3.Location = new System.Drawing.Point(13, 71);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(155, 48);
            this.groupBox3.TabIndex = 102;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Automatic Exposure Control";
            // 
            // AEC_off
            // 
            this.AEC_off.AutoSize = true;
            this.AEC_off.Checked = true;
            this.AEC_off.Location = new System.Drawing.Point(63, 22);
            this.AEC_off.Name = "AEC_off";
            this.AEC_off.Size = new System.Drawing.Size(43, 17);
            this.AEC_off.TabIndex = 1;
            this.AEC_off.TabStop = true;
            this.AEC_off.Text = "Aus";
            this.AEC_off.UseVisualStyleBackColor = true;
            // 
            // AEC_on
            // 
            this.AEC_on.AutoSize = true;
            this.AEC_on.Location = new System.Drawing.Point(16, 22);
            this.AEC_on.Name = "AEC_on";
            this.AEC_on.Size = new System.Drawing.Size(38, 17);
            this.AEC_on.TabIndex = 0;
            this.AEC_on.Text = "An";
            this.AEC_on.UseVisualStyleBackColor = true;
            this.AEC_on.CheckedChanged += new System.EventHandler(this.AEC_on_CheckedChanged);
            // 
            // write_settings
            // 
            this.write_settings.Location = new System.Drawing.Point(256, 141);
            this.write_settings.Name = "write_settings";
            this.write_settings.Size = new System.Drawing.Size(92, 83);
            this.write_settings.TabIndex = 110;
            this.write_settings.Text = "Übernehmen";
            this.write_settings.UseVisualStyleBackColor = true;
            this.write_settings.Click += new System.EventHandler(this.write_settings_Click);
            // 
            // exposuretime_user
            // 
            this.exposuretime_user.Location = new System.Drawing.Point(166, 174);
            this.exposuretime_user.Name = "exposuretime_user";
            this.exposuretime_user.Size = new System.Drawing.Size(81, 20);
            this.exposuretime_user.TabIndex = 111;
            this.exposuretime_user.Text = "200";
            // 
            // gain_user
            // 
            this.gain_user.Location = new System.Drawing.Point(166, 141);
            this.gain_user.Name = "gain_user";
            this.gain_user.Size = new System.Drawing.Size(81, 20);
            this.gain_user.TabIndex = 112;
            this.gain_user.Text = "1";
            // 
            // R_Gain
            // 
            this.R_Gain.Location = new System.Drawing.Point(166, 204);
            this.R_Gain.Name = "R_Gain";
            this.R_Gain.Size = new System.Drawing.Size(23, 20);
            this.R_Gain.TabIndex = 115;
            this.R_Gain.Text = "15";
            // 
            // rgb_gain
            // 
            this.rgb_gain.AutoSize = true;
            this.rgb_gain.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.rgb_gain.Location = new System.Drawing.Point(113, 207);
            this.rgb_gain.Name = "rgb_gain";
            this.rgb_gain.Size = new System.Drawing.Size(16, 13);
            this.rgb_gain.TabIndex = 114;
            this.rgb_gain.Text = "---";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 207);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 113;
            this.label4.Text = "R/G/B Gain:";
            // 
            // G_Gain
            // 
            this.G_Gain.Location = new System.Drawing.Point(195, 204);
            this.G_Gain.Name = "G_Gain";
            this.G_Gain.Size = new System.Drawing.Size(23, 20);
            this.G_Gain.TabIndex = 116;
            this.G_Gain.Text = "15";
            // 
            // B_Gain
            // 
            this.B_Gain.Location = new System.Drawing.Point(224, 204);
            this.B_Gain.Name = "B_Gain";
            this.B_Gain.Size = new System.Drawing.Size(23, 20);
            this.B_Gain.TabIndex = 117;
            this.B_Gain.Text = "15";
            // 
            // Kam_Einstellungen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 321);
            this.Controls.Add(this.B_Gain);
            this.Controls.Add(this.G_Gain);
            this.Controls.Add(this.R_Gain);
            this.Controls.Add(this.rgb_gain);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.gain_user);
            this.Controls.Add(this.exposuretime_user);
            this.Controls.Add(this.write_settings);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.refresh);
            this.Controls.Add(this.exposuretime);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.gain);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cam_read);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.cam_reg_log);
            this.Controls.Add(this.cam_write);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.reg_val);
            this.Controls.Add(this.reg_address);
            this.Controls.Add(this.label6);
            this.Name = "Kam_Einstellungen";
            this.Text = "Kamera Einstellungen";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cam_read;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label cam_reg_log;
        private System.Windows.Forms.Button cam_write;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox reg_val;
        private System.Windows.Forms.TextBox reg_address;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton AGC_off;
        private System.Windows.Forms.RadioButton AGC_on;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton AWB_off;
        private System.Windows.Forms.RadioButton AWB_on;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label gain;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label exposuretime;
        private System.Windows.Forms.Button refresh;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton AEC_off;
        private System.Windows.Forms.RadioButton AEC_on;
        private System.Windows.Forms.Button write_settings;
        private System.Windows.Forms.TextBox exposuretime_user;
        private System.Windows.Forms.TextBox gain_user;
        private System.Windows.Forms.TextBox R_Gain;
        private System.Windows.Forms.Label rgb_gain;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox G_Gain;
        private System.Windows.Forms.TextBox B_Gain;
    }
}