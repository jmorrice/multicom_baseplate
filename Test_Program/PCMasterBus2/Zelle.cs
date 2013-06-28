using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace PCMasterBus2
{
    public class Zelle
    {
        //System
        public byte FLASH           = 1;    //Master Write
        public byte START_SYSTEM    = 2;    //Master Write
        public byte STOP_SYSTEM     = 3;    //Master Write
        public byte SOFT_RESET      = 4;    //Master Write
        public byte SET_ADDRESS     = 5;    //Master Write
        //Sensoren
        public byte READ_STATUS     = 6;    //Master Read
        public byte READ_TEMP       = 7;    //Master Read
        public byte READ_PRESSURE   = 8;    //Master Read
        public byte PWM             = 9;    //Master Write
        //Kamera
        public byte SEL_CAM_REG     = 10;   //Master Write
        public byte WRITE_CAM_REG   = 11;   //Master Write
        public byte READ_CAM_REG    = 12;   //Master Read
        public byte SEL_START_PIX   = 13;   //Master Write
        public byte SEL_GREY_AREA   = 14;   //Master Write
        public byte TAKE_TEST_PIC   = 15;   //Master Write
        public byte SET_BW_THRESH   = 16;   //Master Write
        public byte TAKE_BIN_PIC    = 17;   //Master Write
        public byte READ_BIN_PIC    = 18;   //Master Read
        public byte TAKE_GREY_PIC   = 19;   //Master Write
        public byte READ_GREY_PIC   = 20;   //Master Read
        public byte BEARBEITEN      = 21;   //Master Write
        public byte COORDINATES     = 22;   //Master Read
        public byte DUMMY           = 39;   //Master Read
        public byte HEIGHT          = 40;   //Master Read

        public Byte Adresse;
        public double temperature;
        public double pressure;
        public double height;
        public Bitmap bin_pic = new Bitmap(320, 240);
        public int[] colourCount = new int[256];
        public int gain = 0;
        public int exposure = 0;
        public int r_gain = 0;
        public int g_gain = 0;
        public int b_gain = 0;

        public void count_colours()
        {
            Array.Clear(this.colourCount,0, this.colourCount.Length);
            for (int y = 0; y < this.bin_pic.Height; y++)
                for (int x = 0; x < this.bin_pic.Width; x++)
                    this.colourCount[this.bin_pic.GetPixel(x, y).R] += 1;
        }
    }
}
