using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace PCMasterBus2
{
    public partial class Form2 : Form
    {
        public ZellenBild original;
        public ZellenBild edited;
        public ZellenBild undo;
        public Bitmap hough_pic;
        public Bitmap blob_pic;
        public int[] blob_sizes;
        public int[,] labels;
        public int[] avg_labels;

        public Form2()
        {
            InitializeComponent();
        }

        public Form2(Zelle cell)
        {
            InitializeComponent();

            original = new ZellenBild(cell.bin_pic, cell.colourCount);
            edited = (ZellenBild)original.Clone();
            undo = (ZellenBild)original.Clone();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //load original image
            orig_avg.Text = original.average.ToString();
            orig_min.Text = original.min.ToString();
            orig_max.Text = original.max.ToString();
            Series series = chart1.Series.Add("Count");
            foreach (int x in original.histogram)
                series.Points.Add(x);

            original_image.Image = original.pic;

            //load edited image
            draw();

            //create hough image
            hough_pic = new Bitmap(320, 240);
        }

        private void draw()
        {
            edited.load(); //update from pic
            edit_avg.Text = edited.average.ToString();
            edit_min.Text = edited.min.ToString();
            edit_max.Text = edited.max.ToString();
            chart2.Series.Clear();//draw histogram
            Series series2 = chart2.Series.Add("Count");
            foreach (int x in edited.histogram)
                series2.Points.Add(x);
            edited_image.Image = edited.pic; //draw image
            int firstrow_avg = 0, i, j;
            for(j = 0; j < 5; j++)
                for (i = 0; i < edited.pic.Width; i++)
                    firstrow_avg += edited.pic.GetPixel(i, j).R;
            firstrow_avg = firstrow_avg / (edited.pic.Width * j);
            upper_thresh.Text = (firstrow_avg - 25).ToString();
            lower_thresh.Text = (firstrow_avg- 25).ToString();
            //upper_thresh.Text = (edited.average - 20).ToString();
            //lower_thresh.Text = (edited.average - 20).ToString();
        }

        private void reset_Click(object sender, EventArgs e)
        {
            edited = (ZellenBild)original.Clone(); //restore original image
            draw();
        }

        private void normalise_Click(object sender, EventArgs e)
        {
            undo = edited.Clone();//vorherige version speichern
            double Nmin = Convert.ToDouble(normalise_min.Text), Nmax = Convert.ToDouble(normalise_max.Text);
            normalise(Nmin, Nmax);
        }

        private void normalise(double Nmin, double Nmax)
        {
            double Omin = Convert.ToDouble(edited.min), Omax = Convert.ToDouble(edited.max);
            double a = (Nmax - Nmin) / (Omax - Omin);
            for (int y = 0; y < edited.pic.Height; y++)
                for (int x = 0; x < edited.pic.Width; x++)
                {
                    int old_val = edited.pic.GetPixel(x, y).R;
                    Byte new_val = Convert.ToByte(a * (old_val - Omin) + Nmin);
                    edited.pic.SetPixel(x, y, Color.FromArgb(new_val, new_val, new_val));
                }
            draw();
        }

        private void apply_template_Click(object sender, EventArgs e)
        {
            undo = edited.Clone();//vorherige version speichern
            ZellenBild temp = edited.Clone();
            double[,] template = read_template();

            for (int y = 0; y < temp.pic.Height; y++)
                for (int x = 0; x < temp.pic.Width; x++)
                {
                    int old_val = temp.pic.GetPixel(x, y).R;
                    Byte new_val = 0;

                    //borders
                    if (x != 0 && y != 0 && x != temp.pic.Width - 1 && y != temp.pic.Height - 1)
                    {
                        double acc = 0;
                        for (int i = 0; i < 3; i++)
                            for (int j = 0; j < 3; j++)
                                acc += temp.pic.GetPixel(x - 1 + j, y - 1 + i).R * template[i, j];
                        if (acc < -255 | acc > 255)
                            new_val = 255;
                        else
                            new_val = Convert.ToByte(Math.Abs(acc));
                    }
                    edited.pic.SetPixel(x, y, Color.FromArgb(new_val, new_val, new_val));
                }
            draw();

        }

        private void direct_Click(object sender, EventArgs e)
        {
            double[,] temp = new double[3, 3];
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    temp[i, j] = 1.0 / 9.0;
            display_template(temp);
        }

        private void gauss_Click(object sender, EventArgs e)
        {
            double[,] temp = new double[3, 3];
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    temp[i, j] = gauss_coeff(-1 + j, -1 + i);
            display_template(temp);
        }

        private double gauss_coeff(int y, int x)
        {
            double variance = 0.5;
            return Math.Pow(Math.E, (-(x * x + y * y) / 2 * variance * variance));
        }

        private double[,] read_template()
        {
            double[,] template = new double[3, 3];
            template[0, 0] = Convert.ToDouble(template0_0.Text);
            template[0, 1] = Convert.ToDouble(template0_1.Text);
            template[0, 2] = Convert.ToDouble(template0_2.Text);
            template[1, 0] = Convert.ToDouble(template1_0.Text);
            template[1, 1] = Convert.ToDouble(template1_1.Text);
            template[1, 2] = Convert.ToDouble(template1_2.Text);
            template[2, 0] = Convert.ToDouble(template2_0.Text);
            template[2, 1] = Convert.ToDouble(template2_1.Text);
            template[2, 2] = Convert.ToDouble(template2_2.Text);
            return template;
        }

        private void display_template(double[,] temp)
        {
            template0_0.Text = temp[0, 0].ToString("0.0000");
            template0_1.Text = temp[0, 1].ToString("0.0000");
            template0_2.Text = temp[0, 2].ToString("0.0000");
            template1_0.Text = temp[1, 0].ToString("0.0000");
            template1_1.Text = temp[1, 1].ToString("0.0000");
            template1_2.Text = temp[1, 2].ToString("0.0000");
            template2_0.Text = temp[2, 0].ToString("0.0000");
            template2_1.Text = temp[2, 1].ToString("0.0000");
            template2_2.Text = temp[2, 2].ToString("0.0000");
        }

        private void normalise_temp_Click(object sender, EventArgs e)
        {
            double[,] temp = read_template();
            double sum = 0;

            foreach (double d in temp)
                sum += d;
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    temp[i, j] = temp[i, j] / sum;
            display_template(temp);
        }

        private void undo_button_Click(object sender, EventArgs e)
        {
            edited = undo.Clone();
            draw();
        }

        private void sobel_Click(object sender, EventArgs e)
        {
            double[,] temp = new double[3, 3];
            for (int j = 0; j < 3; j++)
                for (int i = 0; i < 3; i++)
                    temp[i, j] = -2 + 2 * j;
            display_template(temp);
        }

        private void hough_Click(object sender, EventArgs e)
        {
            int r = Convert.ToInt32(radius.Text), x0, y0; //radius
            double t;

            //initialise and clear accumulator
            int[,] accumulator = new int[320,240];
            for(int i = 0; i < 320; i++)
                for(int j = 0; j < 240; j++)
                    accumulator[i,j] = 0;

            for (int y = 0; y < edited.pic.Height; y++)
                for (int x = 0; x < edited.pic.Width; x++)
                {
                    int old_val = edited.pic.GetPixel(x, y).R;
                    if (old_val == 255)//nur kanten auswaehlen
                    {
                        for (int a = 0; a < 360; a++)
                        {
                            t = (a * Math.PI) / 180;
                            x0 = Convert.ToInt32(Math.Round(x - r * Math.Cos(t)));
                            y0 = Convert.ToInt32(Math.Round(y - r * Math.Sin(t)));
                            if (x0 < 320 && x0 > 0 && y0 < 240 && y0 > 0) //wenn mittelpunkt im bild ist
                                accumulator[x0, y0] += 1;
                        }
                    }

                }
            draw_acc(accumulator);
        }

        private void draw_acc(int[,] acc)
        {
            int acc_max = 0;
            //find max
            for (int y = 0; y < acc_space.Height; y++)
                for (int x = 0; x < acc_space.Width; x++)
                    if(acc[x,y] > acc_max)
                        acc_max = acc[x,y];

            //normalise and plot
            for (int y = 0; y < acc_space.Height; y++)
                for (int x = 0; x < acc_space.Width; x++)
                {
                    int new_val = acc[x, y] * (255/acc_max);
                    if(new_val > Convert.ToInt32(hough_thresh.Text))
                        hough_pic.SetPixel(x, y, Color.FromArgb(new_val, new_val, new_val));
                    else
                        hough_pic.SetPixel(x, y, Color.Black);
                }
            acc_space.Image = hough_pic;
        }

        private void binary_Click(object sender, EventArgs e)
        {
            undo = edited.Clone();//vorherige version speichern
            int upper = Convert.ToInt32(upper_thresh.Text), lower = Convert.ToInt32(lower_thresh.Text);
            make_bin(upper, lower);
        }

        private void make_bin(int upper, int lower)
        {
            Byte new_val;
            for (int y = 0; y < edited.pic.Height; y++)
            {
                new_val = 0;
                for (int x = 0; x < edited.pic.Width; x++)
                {
                    int old_val = edited.pic.GetPixel(x, y).R;
                    if (old_val > upper)
                        new_val = 255;
                    if (old_val < lower)
                        new_val = 0;

                    edited.pic.SetPixel(x, y, Color.FromArgb(new_val, new_val, new_val));
                }
            }
            draw();
        }

        private void log_Click(object sender, EventArgs e)
        {
            double[,] temp = new double[3, 3];
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    temp[i, j] = log_coeff(-1 + j, -1 + i);
            display_template(temp);
        }

        private double log_coeff(int y, int x)
        {
            double var = 0.2;
            return 1 / (var * var) * ((x * x + y * y) / (var * var) - 2) * Math.Pow(Math.E, (-(x * x + y * y) / 2 * var * var));
        }

        private void blob_Click(object sender, EventArgs e)
        {
            detect_blobs();
        }

        private void detect_blobs()
        {
            label_groups(edited.pic);
            find_circles();
            plot_labels();
        }

        public int avg_blobs(int[] sizes)
        {
            //Array.Sort(sizes);
            //sizes[sizes.Length-1] = 0;//delete two largest blobs rom average
            //sizes[sizes.Length-1] = 0;
            var nonzero_sizes = sizes.Where(element => element > 10);
            return Convert.ToInt16(nonzero_sizes.Average());
        }

        public void plot_labels()
        {
            System.Array colorsArray = Enum.GetValues(typeof(KnownColor));
            KnownColor[] allColors = new KnownColor[colorsArray.Length];
            Array.Copy(colorsArray, allColors, colorsArray.Length);

            blob_pic = new Bitmap(320, 240);

            //colour
            for (int j = 0; j < edited.pic.Height; j++)
                for (int i = 0; i < edited.pic.Width; i++)
                {
                    String col;
                    if (Array.IndexOf(avg_labels, labels[i, j]) >= 0) //make average blob red
                        col = "Red";

                    //colour rest
                    else if (labels[i, j] < 173)
                        col = allColors[labels[i, j]].ToString();
                    else if (labels[i, j] < 173 * 2)
                        col = allColors[labels[i, j] - 173].ToString();
                    else if (labels[i, j] < 173 * 3)
                        col = allColors[labels[i, j] - 173 * 2].ToString();
                    else if (labels[i, j] < 173 * 4)
                        col = allColors[labels[i, j] - 173 * 3].ToString();
                    else if (labels[i, j] < 173 * 5)
                        col = allColors[labels[i, j] - 173 * 4].ToString();
                    else if (labels[i, j] < 173 * 6)
                        col = allColors[labels[i, j] - 173 * 5].ToString();
                    else if (labels[i, j] < 173 * 7)
                        col = allColors[labels[i, j] - 173 * 6].ToString();
                    else if (labels[i, j] < 173 * 8)
                        col = allColors[labels[i, j] - 173 * 7].ToString();
                    else if (labels[i, j] < 173 * 9)
                        col = allColors[labels[i, j] - 173 * 8].ToString();
                    else if (labels[i, j] < 173 * 10)
                        col = allColors[labels[i, j] - 173 * 9].ToString();
                    else
                        col = allColors[labels[i, j] - 173 * 10].ToString();
                    blob_pic.SetPixel(i, j, Color.FromName(col));
                }
            acc_space.Image = blob_pic;
        }

        private void find_circles()
        {
            int max_label = 0;

            //find max
            for (int j = 0; j < edited.pic.Height; j++)
                for (int i = 0; i < edited.pic.Width; i++)
                    if (labels[i, j] > max_label)
                        max_label = labels[i, j];

            //count sizes of labels
            blob_sizes = new int[max_label + 1];
            for (int j = 0; j < edited.pic.Height; j++)
                for (int i = 0; i < edited.pic.Width; i++)
                    blob_sizes[labels[i, j]] += 1;

            blob_sizes[Array.IndexOf(blob_sizes, blob_sizes.Max())] = 0;//remove max (zeroes)
            //blob_sizes[Array.IndexOf(blob_sizes, blob_sizes.Max())] = 0;//remove max (bar on left)
            //blob_sizes[Array.IndexOf(blob_sizes, blob_sizes.Max())] = 0;//remove max (center)

            chart3.Series.Clear();
            Series series = chart3.Series.Add("Count");
            foreach (int x in blob_sizes)
                series.Points.Add(x);

            //get average size and respective indexes using number of blobs
            int avg = avg_blobs(blob_sizes);
            int[] avg_sizes = Array.FindAll(blob_sizes, element => (element < avg + avg * 0.6) && (element > avg - avg * 0.6));
            avg_labels = new int[avg_sizes.Length];
            blob_count.Text = avg_sizes.Length.ToString();
            avg_blob.Text = avg.ToString();
            height.Value = get_height(avg_sizes.Length);
            height_perc.Text = get_height(avg_sizes.Length).ToString() + "%";

            foreach (int l in avg_sizes) //find labels that match sizes
            {
                avg_labels[Array.IndexOf(avg_labels, 0)] = Array.IndexOf(blob_sizes, l);
                blob_sizes[Array.IndexOf(blob_sizes, l)] = 0;
            }
        }

        private int get_height(int circle_count)
        {
            int min = 20;
            int max = 300;
            double a = 100.0 / (max - min);
            return Convert.ToInt16(a * (circle_count - min));
        }

        private void label_groups(Bitmap img)
        {
            labels = new int[img.Width, img.Height];
            for (int j = 0; j < img.Height; j++) //clear array
                for (int i = 0; i < img.Width; i++)
                    labels[i, j] = 0;

            int new_label = 1;

            for (int y = 1; y < img.Height; y++)
                for (int x = 1; x < img.Width; x++)
                {
                    Boolean pix = img.GetPixel(x, y).R == 0;
                    int left = labels[x-1, y];
                    int above = labels[x, y-1];

                    if (pix) //if black
                    {
                        if (left > 0 && above == 0) //only left
                            labels[x,y] = left;
                        else if (above > 0 && left == 0) //only above
                            labels[x, y] = above;
                        else if (left == above)
                        {
                            if (left == 0) //none, new label
                            {
                                labels[x, y] = new_label;
                                new_label++;
                            }
                            else //same, copy label
                                labels[x, y] = left;
                        }
                        else //different and non-zero
                        {
                            for (int j = 0; j < img.Height; j++) //clear array
                                for (int i = 0; i < img.Width; i++)
                                    if (labels[i, j] == above)
                                        labels[i, j] = left;
                            labels[x, y] = left;
                        }
                    }                    
                }
        }

        private void acc_space_MouseClick(object sender, MouseEventArgs e)
        {
            Color test = blob_pic.GetPixel(e.X, e.Y);
            int label = labels[e.X, e.Y];
            int size = blob_sizes[label];
        }

        private void auto_Click(object sender, EventArgs e)
        {
            normalise(0, 255);
            make_bin(edited.average - 10, edited.average - 10);
            detect_blobs();
        }

        private void height_direct_Click(object sender, EventArgs e)
        {
            int w = original.pic.Width, h = original.pic.Height;

            //durchschnitt und maxima (der ersten 5 reihen)
            Byte local_max = 0, local_min = 255, bin_thresh;
            int reihen = h;
            double local_avg = 0;
            for (int j = 0; j < reihen; j++)
                for (int i = 0; i < w; i++)
                {
                    Byte pix = original.pic.GetPixel(i, j).R;
                    local_avg += pix;
                    if (pix > local_max)
                        local_max = pix;
                    if (pix < local_min)
                        local_min = pix;
                }
            local_avg = local_avg / (w * reihen);

            //normalisierung und umwandlung in binaerbild
            double a = 255.0 / (double)(local_max - local_min);
            local_avg = a * (local_avg - local_min); //durschnitt normalisieren
            bin_thresh = Convert.ToByte(local_avg - /*25*/20); //schwellwert fuer binaerbild

            int[] labels_above = new int[w], blobs = new int[w * h];
            int new_label = 1;

            for (int y = 1; y < h; y++)
            {
                int label_left = 0;
                for (int x = 1; x < w; x++)
                {
                    Byte pix = original.pic.GetPixel(x, y).R;
                    int label_above = labels_above[x], label;
                    if ((a * (pix - local_min)) <= bin_thresh) //schwarzer pixel im binaerbild
                    {
                        if (label_left > 0 && label_above == 0) //only left
                            label = label_left;
                        else if (label_above > 0 && label_left == 0) //only above
                            label = label_above;
                        else if (label_left == label_above)
                        {
                            if (label_left == 0) //none, new label
                            {
                                label = new_label;
                                new_label++;
                            }
                            else //same, copy label
                                label = label_left;
                        }
                        else //different and non-zero
                        {
                            label = label_left;
                            blobs[label] += blobs[label_above];
                            blobs[label_above] = 0;
                        }
                        blobs[label] += 1;
                        labels_above[x] = label;
                        label_left = label;
                    }
                    else
                    {
                        label_left = 0;
                        labels_above[x] = 0;
                    }
                }
            }

            //die drei groessten blobs entfernen
            blobs[Array.IndexOf(blobs, blobs.Max())] = 0;//remove max (zeroes)
            blobs[Array.IndexOf(blobs, blobs.Max())] = 0;//remove max (bar on left)
            blobs[Array.IndexOf(blobs, blobs.Max())] = 0;//remove max (center)

            //durchschnitt bilden (ohne nullen), punkte herausfiltern und zaehlen
            var nonzero_sizes = blobs.Where(element => element > 10);
            int avg = Convert.ToInt16(nonzero_sizes.Average()); //durchschnitt
            //aus blob array herausfiltern +-60%            
            int[] avg_sizes = Array.FindAll(blobs, element => (element < avg + avg * 0.6) && (element > avg - avg * 0.6));

            height.Value = get_height(avg_sizes.Length);
            height_perc.Text = get_height(avg_sizes.Length).ToString() + "%";
        }
    }
}