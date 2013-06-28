using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace PCMasterBus2
{
    public class ZellenBild
    {
        public Bitmap pic = new Bitmap(320, 240);
        public int[] histogram = new int[256];
        public Byte average;
        public Byte min;
        public Byte max;

        public ZellenBild(Bitmap picture, int[] counter)
        {
            pic = picture;
            //count = counter;
            load();
        }

        public void load()
        {
            this.load_histogram(); //überschreibt histogram das beim einlesen erstellt wird
            this.load_average();
            this.load_min();
            this.load_max();
        }

        public ZellenBild Clone() //erstellt 'deep copy' der instanz
        {
            ZellenBild other = (ZellenBild)this.MemberwiseClone();
            other.histogram = (int[])this.histogram.Clone();
            other.pic = (Bitmap)this.pic.Clone();
            return other;
        }

        private void load_average()
        {
            int avg = 0, averageCount = 0;
            for (int i = 0; i < this.histogram.Length; i++)
            {
                avg += i * this.histogram[i];
                averageCount += this.histogram[i];
            }
            this.average = Convert.ToByte(avg / averageCount);
        }

        private void load_min()
        {
            this.min = Convert.ToByte(Array.FindIndex(this.histogram, element => element != 0));
        }

        private void load_max()
        {
            this.max = Convert.ToByte(Array.FindLastIndex(this.histogram, element => element != 0));
        }

        private void load_histogram()
        {
            Array.Clear(this.histogram, 0, this.histogram.Length);
            for (int y = 0; y < this.pic.Height; y++)
                for (int x = 0; x < this.pic.Width; x++)
                    this.histogram[this.pic.GetPixel(x, y).R] += 1;
        }
    }
}
