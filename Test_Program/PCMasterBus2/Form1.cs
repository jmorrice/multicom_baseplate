using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;
using System.IO;

namespace PCMasterBus2
{
    public partial class Form1 : Form
    {
        bool bIsInit;
        USB_ISS Zellenkontroller;
        Zelle Zelle1;
        UInt16[] IOPort = new UInt16[4] { 0x3fff, 0x3fff, 0x3fff, 0x3fff };

        public Form1()
        {
            Zellenkontroller = new USB_ISS();
            Zelle1 = new Zelle();
            InitializeComponent();
            SelectPort.Sorted = true;
            foreach (string s in SerialPort.GetPortNames())        // Get a list of available serial port names.
            {
                SelectPort.Items.Add(s);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btn_Init_Click(object sender, EventArgs e)
        {
            bIsInit = Zellenkontroller.Init(SelectPort.Text);
            if (!bIsInit)
            {
                StatusText.Text = "Fehler bei Initalisierung (" + Zellenkontroller.Status + ")";
                return;
            }

            StatusText.Text = "Port mit I²C Kommunikation initalisiert (" + Zellenkontroller.Status + ")";

            return;
        }

        private void button_adr_Click(object sender, EventArgs e)
        {
            int n;
            Zelle1.Adresse = Convert.ToByte(Adresse.Text);
            if (Zellenkontroller.Send_Generalcall(Zelle1.Adresse, Zelle1.SET_ADDRESS))
                box_zelle.Enabled = true;
            if (AutoI_Adresse.Checked)
            {
                n = Convert.ToInt32(Adresse.Text) + 1;
                Adresse.Text = n.ToString();
                Zelle1.Adresse = Convert.ToByte(Adresse.Text);
                box_zelle.Text = "Zelle " + Zelle1.Adresse.ToString();
            }
        }

        public byte update_status()
        {
            byte[] status = new byte[1];
            status = Zellenkontroller.ReadByte(Zelle1.Adresse, Zelle1.READ_STATUS, 1);
            if (status == null)
            {
                status_temp.Text = "---";
                status_druck.Text = "---";
                status_kam.Text = "---";
            }
            else
            {
                if ((status[0] & 1) == 1)
                {
                    status_temp.Text = "Okay";
                    temp_messen();
                }
                else
                    status_temp.Text = "Error";

                if ((status[0] & 2) == 2)
                {
                    status_druck.Text = "Okay";
                    druck_messen();
                }
                else
                    status_druck.Text = "Error";

                if ((status[0] & 4) == 4)
                {
                    status_kam.Text = "Okay";
                    height_messen();
                }
                else
                    status_kam.Text = "Error";
            }

            return status[0];
        }

        private void button_status_Click(object sender, EventArgs e)
        {
            update_status();
        }

        private void Adresse_TextChanged(object sender, EventArgs e)
        {
            Zelle1.Adresse = Convert.ToByte(Adresse.Text);
            box_zelle.Text = "Zelle " + Zelle1.Adresse.ToString();
        }

        private void button_temp_messen_Click_1(object sender, EventArgs e)
        {
            temp_messen();
        }

        private void temp_messen()
        {
            byte[] temp = new byte[2];
            temp = Zellenkontroller.ReadByte(Zelle1.Adresse, Zelle1.READ_TEMP, 2);
            temp = Zellenkontroller.ReadByte(Zelle1.Adresse, Zelle1.READ_TEMP, 2);

            if (temp == null)
                wert_temp.Text = "---";
            else
            {
                Zelle1.temperature = ((temp[1] << 8) | temp[0]) * 0.001;
                wert_temp.Text = (Zelle1.temperature).ToString("00.0") + " °C";
            }
        }

        private void height_messen()
        {
            byte[] height = new byte[2];
            height = Zellenkontroller.ReadByte(Zelle1.Adresse, Zelle1.HEIGHT, 2);
            Thread.Sleep(30);
            height = Zellenkontroller.ReadByte(Zelle1.Adresse, Zelle1.HEIGHT, 2);

            if (height == null)
                wert_height.Text = "---" + " %";
            else
            {
                Zelle1.height = Convert.ToInt32(height[0]) + Convert.ToInt32(height[1]) * 0.01;
                wert_height.Text = (Zelle1.height).ToString("##.00") + " %";
            }
        }

        private void button_druck_messen_Click(object sender, EventArgs e)
        {
            druck_messen();
        }

        private void druck_messen()
        {
            byte[] press = new byte[2];
            press = Zellenkontroller.ReadByte(Zelle1.Adresse, Zelle1.READ_PRESSURE, 2);
            press = Zellenkontroller.ReadByte(Zelle1.Adresse, Zelle1.READ_PRESSURE, 2);

            if (press == null)
                wert_druck.Text = "---";
            else
            {
                Zelle1.pressure = ((press[1] << 8) | press[0]) * 0.001;
                wert_druck.Text = (Zelle1.pressure).ToString("00.0") + " PSI";
            }
        }

        private void refresh_leds()
        {
            byte sel = 0, pwm = 0;
            if (LED1_an.Checked)
                sel |= 1;
            if (LED2_an.Checked)
                sel |= 2;
            if (LED3_an.Checked)
                sel |= 4;
            pwm = Convert.ToByte(LED_helligkeit.Value);
            byte[] data = new byte[2];
            data[0] = pwm;
            data[1] = sel;
            this.Zellenkontroller.Send_Byte(Zelle1.Adresse, Zelle1.PWM, data);
        }

        private void LED1_an_CheckedChanged(object sender, EventArgs e)
        {
            refresh_leds();
        }

        private void LED2_an_CheckedChanged(object sender, EventArgs e)
        {
            refresh_leds();
        }

        private void LED3_an_CheckedChanged(object sender, EventArgs e)
        {
            refresh_leds();
        }

        private void LED1_aus_CheckedChanged(object sender, EventArgs e)
        {
            refresh_leds();
        }

        private void LED2_aus_CheckedChanged(object sender, EventArgs e)
        {
            refresh_leds();
        }

        private void LED3_aus_CheckedChanged(object sender, EventArgs e)
        {
            refresh_leds();
        }

        private void LED_helligkeit_Scroll(object sender, EventArgs e)
        {
            refresh_leds();
            LED_setting.Text = LED_helligkeit.Value.ToString() + "%";
        }

        private void button_reset_Click(object sender, EventArgs e)
        {
            Zellenkontroller.ReadByte(Zelle1.Adresse, Zelle1.SOFT_RESET, 0);
            LED_helligkeit.Value = 50;
            LED1_an.Checked     = false;
            LED1_aus.Checked    = true;
            LED2_an.Checked     = false;
            LED2_aus.Checked    = true;
            LED3_an.Checked     = false;
            LED3_aus.Checked    = true;
        }

        private void take_bin_pic_Click(object sender, EventArgs e)
        {
            Zellenkontroller.ReadByte(Zelle1.Adresse, Zelle1.TAKE_BIN_PIC, 0);
        }

        private void get_bin_pic_Click(object sender, EventArgs e)
        {
            pictureBox1.Size = new Size(320, 240);
            byte[] row_data = new byte[40];
            Zelle1.bin_pic = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            int row = 0, bits = 0;

            for (row = 0; row < pictureBox1.Height; row++)
            {
                byte[] command = new byte[6];
                command[0] = Convert.ToByte(row);
                command[1] = 0;
                command[2] = 0;
                command[3] = 0;
                command[4] = 0;
                command[5] = 40;
                Zellenkontroller.Send_Byte(Zelle1.Adresse, Zelle1.SEL_START_PIX, command);
                row_data = Zellenkontroller.ReadByte(Zelle1.Adresse, Zelle1.READ_BIN_PIC, 40);

                for (int i = 0; i < 40; i++)
                {
                    for (bits = 0; bits < 8; bits++)
                    {
                        if (((row_data[i] >> bits) & 1) == 1)
                            Zelle1.bin_pic.SetPixel(8 * i + bits, row, Color.Black);
                        else
                            Zelle1.bin_pic.SetPixel(8 * i + bits, row, Color.White);
                    }
                }
            }
            pictureBox1.Image = Zelle1.bin_pic;
        }

        private void take_grey_pic_Click(object sender, EventArgs e)
        {
            take_grey_section(grey_pic_section.Value);
        }

        private void get_grey_pic_Click(object sender, EventArgs e)
        {
            load_grey_pic(grey_pic_section.Value);
        }

        private void take_grey_section(decimal pic_section)
        {
            byte[] command = new byte[1];
            command[0] = Convert.ToByte(pic_section);
            Zellenkontroller.Send_Byte(Zelle1.Adresse, Zelle1.SEL_GREY_AREA, command);
            Zellenkontroller.ReadByte(Zelle1.Adresse, Zelle1.TAKE_GREY_PIC, 0);
        }

        private void load_grey_pic(decimal pic_section)
        {
            pictureBox1.Size = new Size(320, 240);
            byte[] row_data = new byte[40];
            int row = 0, pixel = 0, section = 0;
            byte[] command = new byte[6];

            List<byte> rowtest = new List<byte>();

            for (row = 0; row < 60; row++)
            {
                for (section = 0; section < 8; section++)
                {
                    command[0] = Convert.ToByte(row);
                    command[1] = Convert.ToByte(section * 40 / 2);
                    command[2] = Convert.ToByte(section * 40 / 2);
                    command[3] = 0;
                    command[4] = 0;
                    command[5] = 40;
                    Zellenkontroller.Send_Byte(Zelle1.Adresse, Zelle1.SEL_START_PIX, command);
                    row_data = Zellenkontroller.ReadByte(Zelle1.Adresse, Zelle1.READ_GREY_PIC, 40);
                    foreach (byte b in row_data)
                        rowtest.Add(b);

                    for (pixel = 0; pixel < 40; pixel++)
                    {
                        //byte inverted = Convert.ToByte(255 - row_data[pixel]);
                        byte inverted = row_data[pixel];
                        Color RGB1 = Color.FromArgb(inverted, inverted, inverted);
                        Zelle1.bin_pic.SetPixel(section * 40 + pixel, (int)pic_section * 60 + row, RGB1);
                        Zelle1.colourCount[inverted] += 1;
                    }
                }
            }
            pictureBox1.Image = Zelle1.bin_pic;
        }

        private void full_grey_pic_Click(object sender, EventArgs e)
        {
            Thread.Sleep(3000);
            for (int k = 0; k < Zelle1.colourCount.Length; k++)
                Zelle1.colourCount[k] = 0;
            take_grey_section(0);
            Thread.Sleep(1000);
            load_grey_pic(0);
            take_grey_section(1);
            Thread.Sleep(1000);
            load_grey_pic(1);
            take_grey_section(2);
            Thread.Sleep(1000);
            load_grey_pic(2);
            take_grey_section(3);
            Thread.Sleep(1000);
            load_grey_pic(3);
            Kam_Einstellungen cam_settings = new Kam_Einstellungen(this, Zellenkontroller, Zelle1);
            cam_settings.read_gain();
            cam_settings.read_exposure();
        }

        private void take_test_pic_Click(object sender, EventArgs e)
        {
            Zellenkontroller.ReadByte(Zelle1.Adresse, Zelle1.TAKE_TEST_PIC, 0);
            Thread.Sleep(500);

              Color newColor = Color.FromArgb(255, 0, 0);
            for (int x = 0; x < Zelle1.bin_pic.Width; x++)
            {
                for (int y = 0; y < Zelle1.bin_pic.Height; y++)
                {
                    Zelle1.bin_pic.SetPixel(x, y, newColor);
                }
            }
            pictureBox1.Image= Zelle1.bin_pic;
            pictureBox1.Update();
            load_grey_pic(0);
            load_grey_pic(1);
            load_grey_pic(2);
            load_grey_pic(3);
        }

        private void analyse_pic_Click(object sender, EventArgs e)
        {
            Form2 Analysis = new Form2(Zelle1);
            Analysis.Show();
        }

        private void cam_settings_Click(object sender, EventArgs e)
        {
            Kam_Einstellungen cam_settings = new Kam_Einstellungen(this, Zellenkontroller, Zelle1);
            cam_settings.Show();
        }

        private void btn_userprog_Click(object sender, EventArgs e)
        {
            int n;
            Zelle1.Adresse = Convert.ToByte(Adresse.Text);
            Zellenkontroller.Send_SingleByte(Zelle1.Adresse,2);
            if (AutoI_UProg.Checked)
            {
                n = Convert.ToInt32(Adresse.Text) + 1;
                Adresse.Text = n.ToString();
                Zelle1.Adresse = Convert.ToByte(Adresse.Text);
                box_zelle.Text = "Zelle " + Zelle1.Adresse.ToString();
            }
            
        }

        private void save_pic_Click(object sender, EventArgs e)
        {
            Stream pic_location;
            SaveFileDialog filedialog = new SaveFileDialog();
            filedialog.Filter = "BMP file (*.bmp)|*.bmp|All files (*.*)|*.*";
            if (filedialog.ShowDialog() == DialogResult.OK)
                if ((pic_location = filedialog.OpenFile()) != null)
                {
                    Zelle1.bin_pic.Save(pic_location, System.Drawing.Imaging.ImageFormat.Bmp);
                    pic_location.Close();
                }
        }

        private void load_pic_Click(object sender, EventArgs e)
        {
            Stream pic_location;
            OpenFileDialog filedialog = new OpenFileDialog();
            filedialog.Filter = "BMP file (*.bmp)|*.bmp|All files (*.*)|*.*";
            if (filedialog.ShowDialog() == DialogResult.OK)
                if ((pic_location = filedialog.OpenFile()) != null)
                {
                    Zelle1.bin_pic = new Bitmap(pic_location);
                    pictureBox1.Image = Zelle1.bin_pic;
                    //pic_location.Close();
                }
        }

        private void save_param_Click_1(object sender, EventArgs e)
        {
            Bitmap pic_to_save = new Bitmap(Zelle1.bin_pic.Width, Zelle1.bin_pic.Height + 30);
            Graphics gr = Graphics.FromImage(pic_to_save);
            gr.DrawImageUnscaled(Zelle1.bin_pic, 0, 0);

            gr.DrawString("Gain: " + Zelle1.gain + "   R/G/B Gain: " + Zelle1.r_gain + "/" + Zelle1.g_gain + "/" + Zelle1.b_gain, SystemFonts.DefaultFont, Brushes.White, new RectangleF(0, Zelle1.bin_pic.Height, Zelle1.bin_pic.Width, 15));
            gr.DrawString("Belichtungszeit: " + Zelle1.exposure + "ms   LED Helligkeit: " + LED_helligkeit.Value, SystemFonts.DefaultFont, Brushes.White, new RectangleF(0, Zelle1.bin_pic.Height + 15, Zelle1.bin_pic.Width, 15));
            Stream pic_location;
            SaveFileDialog filedialog = new SaveFileDialog();
            filedialog.Filter = "BMP file (*.bmp)|*.bmp|All files (*.*)|*.*";
            if (filedialog.ShowDialog() == DialogResult.OK)
                if ((pic_location = filedialog.OpenFile()) != null)
                {
                    pic_to_save.Save(pic_location, System.Drawing.Imaging.ImageFormat.Bmp);
                    pic_location.Close();
                }
        }
    }
}