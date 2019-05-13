using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsoleApplication11
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static Bitmap image;
        public static string full_name_of_image = "g\0";
        public static UInt32[,] pixel;
        //Bitmap pic;
        Bitmap pic1;
        string mode;
        int x1, y1;
        int xclick1, yclick1;
        int current_width;
        int current_height;
       

        public void createPic(int width,int height)
        {
            current_height = height;
            current_width = width;
            image = new Bitmap(width, height);
            pic1 = new Bitmap(width, height);
            pictureBox1.Location = new Point(0,0);
            pictureBox1.Size = new Size(width, height);
            pictureBox1.Image = new Bitmap(width,height);
            //получение матрицы с пикселями
            pixel = new UInt32[image.Height, image.Width];
            for (int y = 0; y < image.Height; y++)
                for (int x = 0; x < image.Width; x++)
                    pixel[y, x] = (UInt32)(image.GetPixel(x, y).ToArgb());
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            Pen pen;
            pen = new Pen(button2.BackColor, trackBar1.Value);
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            Graphics g = Graphics.FromImage(image);
            Graphics g1 = Graphics.FromImage(pic1);
            if(e.Button == MouseButtons.Left)
            {
                if (mode == "Карандаш")
                {
                    g.DrawLine(pen, x1, y1, e.X, e.Y);
                }
                if (mode == "Прямоугольник")
                {
                    g1.Clear(Color.White);
                    int x, y;
                    x = xclick1;
                    y = yclick1;
                    if (x > e.X) x = e.X;
                    if (y > e.Y) y = e.Y;
                    g1.DrawRectangle(pen, x, y, Math.Abs(e.X - xclick1), Math.Abs(e.Y - yclick1));
                }
                if (mode == "Окружность")
                {
                    g1.Clear(Color.White);
                    g1.DrawEllipse(pen, xclick1, yclick1, e.X - xclick1, e.Y - yclick1);
                }
                g1.DrawImage(image, 0, 0);
                pictureBox1.Image = pic1;
            }
            x1 = e.X;
            y1 = e.Y;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            ColorDialog MyDialog = new ColorDialog();
            MyDialog.AllowFullOpen = false;
            MyDialog.ShowHelp = true;
            MyDialog.Color = b.ForeColor;
            if (MyDialog.ShowDialog() == DialogResult.OK)
            button2.BackColor = MyDialog.Color;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {

        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                SaveFileDialog savedialog = new SaveFileDialog();
                savedialog.Title = "Сохранить картинку как...";
                savedialog.OverwritePrompt = true;
                savedialog.CheckPathExists = true;
                savedialog.Filter = "Image Files(*.BMP)|*.BMP|Image Files(*.JPG)|*.JPG|Image Files(*.GIF)|*.GIF|Image Files(*.PNG)|*.PNG|All files (*.*)|*.*";
                savedialog.ShowHelp = true;
                if (savedialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        image.Save(savedialog.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                    catch
                    {
                        MessageBox.Show("Невозможно сохранить изображение", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog open_dialog = new OpenFileDialog();
            open_dialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*";
            if (open_dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    full_name_of_image = open_dialog.FileName;
                    image = new Bitmap(open_dialog.FileName);
                    pic1 = new Bitmap(open_dialog.FileName);
                    //this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    this.Width = image.Width + 40;
                    this.Height = image.Height + 75;
                    this.pictureBox1.Size = image.Size;
                    pictureBox1.Image = image;
                    pictureBox1.Invalidate(); //????
                    //получение матрицы с пикселями
                    pixel = new UInt32[image.Height, image.Width];
                    for (int y = 0; y < image.Height; y++)
                        for (int x = 0; x < image.Width; x++)
                            pixel[y, x] = (UInt32)(image.GetPixel(x, y).ToArgb());
                }
                catch
                {
                    full_name_of_image = "\0";
                    DialogResult rezult = MessageBox.Show("Невозможно открыть выбранный файл",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            mode = "Прямоугольник";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            mode = "Окружность";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mode = "Карандаш";
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            xclick1 = e.X;
            yclick1 = e.Y;
        }

        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WindowCreatePicture wnd = new WindowCreatePicture(this);
            wnd.Show();
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            Pen pen;
            pen = new Pen(button2.BackColor, trackBar1.Value);
            Graphics g = Graphics.FromImage(image);
            if (mode == "Прямоугольник")
            {
                g.DrawRectangle(pen, xclick1, yclick1, e.X - xclick1, e.Y - yclick1);
            }
            if (mode == "Окружность")
            {
                g.DrawEllipse(pen, xclick1, yclick1, e.X - xclick1, e.Y - yclick1);
            }

            for (int y = 0; y < image.Height; y++)
                for (int x = 0; x < image.Width; x++)
                    pixel[y, x] = (UInt32)(image.GetPixel(x, y).ToArgb());
        }

        private void очиститьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            createPic(current_height,current_width);
        }
        //цветовой Баланс
        private void цветовойБалансToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 ColorBalanceForm = new Form3(this);
            ColorBalanceForm.ShowDialog();
        }
        //яркость контрастность
        private void яркостьКонтрастностьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 BrightnessForm = new Form2(this);
            BrightnessForm.ShowDialog();
        }
        //преобразование из UINT32 to Bitmap
        public static void FromPixelToBitmap()
        {
            for (int y = 0; y < image.Height; y++)
                for (int x = 0; x < image.Width; x++)
                    image.SetPixel(x, y, Color.FromArgb((int)pixel[y, x]));
        }
        //преобразование из UINT32 to Bitmap по одному пикселю
        public static void FromOnePixelToBitmap(int x, int y, UInt32 pixel)
        {
            image.SetPixel(y, x, Color.FromArgb((int)pixel));
        }
        //вывод на экран
        public void FromBitmapToScreen()
        {
            pictureBox1.Image = image;
        }

        private void файлToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
       

      
    }
}
