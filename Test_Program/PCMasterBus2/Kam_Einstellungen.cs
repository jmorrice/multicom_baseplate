using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PCMasterBus2
{
    public partial class Kam_Einstellungen : Form
    {
        private USB_ISS Zellenkontroller;
        private Zelle Zelle1;
        private Form1 main_window;
        enum state { ON, OFF, UNKNOWN };

        public Kam_Einstellungen(Form1 parentform, USB_ISS cell_controller, Zelle cell)
        {
            InitializeComponent();
            Zellenkontroller = cell_controller;
            Zelle1 = cell;
            main_window = parentform;

            read_exposure();
            read_gain();
            read_rgb_gain();
            AGC_on.Checked = AGC_enable(state.UNKNOWN);
            AWB_on.Checked = AWB_enable(state.UNKNOWN);
            AEC_on.Checked = AEC_enable(state.UNKNOWN);
        }

        private void write_cam_reg(byte adr, byte val)
        {
            byte[] command = new byte[2];
            command[0] = adr;
            Zellenkontroller.Send_Byte(Zelle1.Adresse, Zelle1.SEL_CAM_REG, command);
            command[0] = val;
            Zellenkontroller.Send_Byte(Zelle1.Adresse, Zelle1.WRITE_CAM_REG, command);
        }

        private byte read_cam_reg(byte adr)
        {
            byte[] command = new byte[2];
            byte[] value = new byte[1];
            command[0] = adr;
            Zellenkontroller.Send_Byte(Zelle1.Adresse, Zelle1.SEL_CAM_REG, command);
            value = Zellenkontroller.ReadByte(Zelle1.Adresse, Zelle1.READ_CAM_REG, 1);
            value = Zellenkontroller.ReadByte(Zelle1.Adresse, Zelle1.READ_CAM_REG, 1);

            return value[0];
        }

        private void cam_write_Click(object sender, EventArgs e)
        {
            write_cam_reg(Convert.ToByte(Convert.ToInt32(reg_address.Text, 16)), Convert.ToByte(Convert.ToInt32(reg_val.Text, 16)));

            //check status
            byte status = main_window.update_status();

            if ((status & 4) == 4)
                cam_reg_log.Text = "0x" + reg_val.Text + " in " + "0x" + reg_address.Text + " geschrieben";
            else
                cam_reg_log.Text = "Fehler beim Schreiben";
        }

        private void cam_read_Click(object sender, EventArgs e)
        {
            byte[] value = new byte[1];
            value[0] = read_cam_reg(Convert.ToByte(Convert.ToInt32(reg_address.Text, 16)));
            reg_val.Text = BitConverter.ToString(value, 0, 1);

            //check status
            byte status = main_window.update_status();

            if ((status & 4) == 4)
                cam_reg_log.Text = "0x" + reg_address.Text + " erfolgreich gelesen";
            else
                cam_reg_log.Text = "Fehler beim Lesen";
        }


        private bool AGC_enable(state control)
        {
            byte COM8 = read_cam_reg(0x13);
            if (control != state.UNKNOWN)
            {
                if (control == state.ON)
                    COM8 = Convert.ToByte(COM8 | 0x04);
                else if(control == state.OFF)
                    COM8 = Convert.ToByte(COM8 & ~0x04);

                write_cam_reg(0x13, COM8);
            }

            COM8 = read_cam_reg(0x13);
            if ((COM8 & 0x04) == 0x04)
                return true;
            else
                return false;
        }

        private bool AWB_enable(state control)
        {
            byte COM8 = read_cam_reg(0x13);
            if (control != state.UNKNOWN)
            {
                if (control == state.ON)
                    COM8 = Convert.ToByte(COM8 | 0x02);
                else if (control == state.OFF)
                    COM8 = Convert.ToByte(COM8 & ~0x02);

                write_cam_reg(0x13, COM8);
            }

            COM8 = read_cam_reg(0x13);
            if ((COM8 & 0x02) == 0x02)
                return true;
            else
                return false;
        }

        private bool AEC_enable(state control)
        {
            byte COM8 = read_cam_reg(0x13);
            if (control != state.UNKNOWN)
            {
                if (control == state.ON)
                    COM8 = Convert.ToByte(COM8 | 0x01);
                else if (control == state.OFF)
                    COM8 = Convert.ToByte(COM8 & ~0x01);

                write_cam_reg(0x13, COM8);
            }

            COM8 = read_cam_reg(0x13);
            if ((COM8 & 0x01) == 0x01)
                return true;
            else
                return false;
        }

        private void AWB_on_CheckedChanged(object sender, EventArgs e)
        {
            if (AWB_on.Checked)
                AWB_on.Checked = AWB_enable(state.ON);
            else
                AWB_on.Checked = AWB_enable(state.OFF);
        }

        private void AGC_on_CheckedChanged(object sender, EventArgs e)
        {
            if (AGC_on.Checked)
                AGC_on.Checked = AGC_enable(state.ON);
            else
                AGC_on.Checked = AGC_enable(state.OFF);
        }

        private void AEC_on_CheckedChanged(object sender, EventArgs e)
        {
            if (AEC_on.Checked)
                AEC_on.Checked = AEC_enable(state.ON);
            else
                AEC_on.Checked = AEC_enable(state.OFF);
        }

        private void unity_gain_Click(object sender, EventArgs e)
        {
            write_cam_reg(0x00, 0x00);
            write_cam_reg(0x03, 0x00);
            read_gain();
        }

        private void refresh_Click(object sender, EventArgs e)
        {
            read_exposure();
            read_gain();
            AGC_on.Checked = AGC_enable(state.UNKNOWN);
            AWB_on.Checked = AWB_enable(state.UNKNOWN);
            AEC_on.Checked = AEC_enable(state.UNKNOWN);
        }

        public void read_exposure()
        {
            byte COM1 = read_cam_reg(0x04);
            byte AEC = read_cam_reg(0x10);
            byte AECHH = read_cam_reg(0x07);
            byte COM1_bits = Convert.ToByte(COM1 & 0x03);
            byte AECHH_bits = Convert.ToByte(AECHH & 0x3F);

            //shift for concatenation
            int AECHH2 = AECHH_bits << 10;
            int AEC2 = AEC << 2;

            //concatenate
            int exposure_reg = AECHH2 | AEC2 | COM1_bits;
            int exposure = (int)Math.Round(exposure_reg * 1.9);

            exposuretime.Text = exposure.ToString() + " ms";
            Zelle1.exposure = exposure;
        }

        private void write_exposure(int exposure)
        {
            int exposure_reg = (int)Math.Round(exposure / 1.9);
            byte COM1 = Convert.ToByte(exposure_reg & 0x03);
            byte AEC = Convert.ToByte((exposure_reg >> 2) & 0xFF);
            byte AECHH = Convert.ToByte((exposure_reg >> 10) & 0x3F);
            write_cam_reg(0x04, COM1);
            write_cam_reg(0x10, AEC);
            write_cam_reg(0x07, AECHH);

            read_exposure();
        }

        public void read_gain()
        {
            byte gainreg = read_cam_reg(0x00);
            byte vref = read_cam_reg(0x03);

            byte a = Convert.ToByte((vref >> 7) & 0x01);
            byte b = Convert.ToByte((vref >> 6) & 0x01);
            byte c = Convert.ToByte((gainreg >> 7) & 0x01);
            byte d = Convert.ToByte((gainreg >> 6) & 0x01);
            byte e = Convert.ToByte((gainreg >> 5) & 0x01);
            byte f = Convert.ToByte((gainreg >> 4) & 0x01);
            byte g = Convert.ToByte(gainreg & 0x0F);

            int gainval = (a + 1) * (b + 1) * (c + 1) * (d + 1) * (e + 1) * (f + 1) * (g / 15 + 1);
            gain.Text = gainval.ToString();
            Zelle1.gain = gainval;
        }

        private void write_gain(int gain)
        {
            int n = Convert.ToInt16(Math.Round(Math.Log(gain, 2)));
            byte gainreg = new byte();
            byte vref = new byte();
            byte a = new byte();
            byte b = new byte();
            byte c = new byte();
            byte d = new byte();
            byte e = new byte();
            byte f = new byte();
            byte g = new byte();

            if (n > 0)
                a = 0x01 << 7;
            if (n > 1)
                b = 0x01 << 6;
            if (n > 2)
                c = 0x01 << 7;
            if (n > 3)
                d = 0x01 << 6;
            if (n > 4)
                e = 0x01 << 5;
            if (n > 5)
                f = 0x01 << 4;
            if (n > 6)
                g = 0x0F;

            vref = Convert.ToByte(a | b);
            gainreg = Convert.ToByte(c | d | e | f | g);
            write_cam_reg(0x00, gainreg);
            write_cam_reg(0x03, vref);

            read_gain();
        }

        public void read_rgb_gain()
        {
            byte r_gain = read_cam_reg(0x02);
            byte g_gain = read_cam_reg(0x6A);
            byte b_gain = read_cam_reg(0x01);

            Zelle1.r_gain = Convert.ToInt16(r_gain);
            Zelle1.g_gain = Convert.ToInt16(g_gain);
            Zelle1.b_gain = Convert.ToInt16(b_gain);

            rgb_gain.Text = r_gain.ToString() + "/" + g_gain.ToString() + "/" + b_gain.ToString();
        }

        private void write_r_gain(int gain)
        {
            byte r_gain = Convert.ToByte(gain);
            write_cam_reg(0x02, r_gain);
            read_rgb_gain();
        }

        private void write_g_gain(int gain)
        {
            byte g_gain = Convert.ToByte(gain);
            write_cam_reg(0x6A, g_gain);
            read_rgb_gain();
        }

        private void write_b_gain(int gain)
        {
            byte b_gain = Convert.ToByte(gain);
            write_cam_reg(0x01, b_gain);
            read_rgb_gain();
        }

        private void write_settings_Click(object sender, EventArgs e)
        {
                write_exposure(Convert.ToInt16(exposuretime_user.Text));
                write_gain(Convert.ToInt16(gain_user.Text));
                write_r_gain(Convert.ToInt16(R_Gain.Text));
                write_g_gain(Convert.ToInt16(G_Gain.Text));
                write_b_gain(Convert.ToInt16(B_Gain.Text));
        }
    }
}