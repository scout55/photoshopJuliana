﻿using System;
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
    public partial class Form2 : Form
    {
        Form1 OwnerForm;
        public Form2(Form1 ownerForm)
        {
            this.OwnerForm = ownerForm;
            InitializeComponent();
            this.button1.Click += new System.EventHandler(this.button_Click);
            this.button2.Click += new System.EventHandler(this.button_Click);
            this.FormClosing += new FormClosingEventHandler(Form2Closing);
        }

        //изменение яркости
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (Form1.full_name_of_image != "\0")
            {
                UInt32 p;
                for (int i = 0; i < Form1.image.Height; i++)
                    for (int j = 0; j < Form1.image.Width; j++)
                    {
                        p = BrightnessContrast.Brightness(Form1.pixel[i, j], trackBar1.Value, trackBar1.Maximum);
                        Form1.FromOnePixelToBitmap(i, j, p);
                    }

                FromBitmapToScreen();
            }
        }

        //изменение контрастности
        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            if (Form1.full_name_of_image != "\0")
            {
                UInt32 p;
                for (int i = 0; i < Form1.image.Height; i++)
                    for (int j = 0; j < Form1.image.Width; j++)
                    {
                        p = BrightnessContrast.Contrast(Form1.pixel[i, j], trackBar2.Value, trackBar2.Maximum);
                        Form1.FromOnePixelToBitmap(i, j, p);
                    }

                FromBitmapToScreen();
            }
        }

        //сохранение изменений яркости или контрастности
        private void button_Click(object sender, EventArgs e)
        {
            if (Form1.full_name_of_image != "\0")
            {
                for (int i = 0; i < Form1.image.Height; i++)
                    for (int j = 0; j < Form1.image.Width; j++)
                        Form1.pixel[i, j] = (UInt32)(Form1.image.GetPixel(j, i).ToArgb());
                trackBar1.Value = 0;
                trackBar2.Value = 0;
            }
        }

        //вывод изображения на экран
        void FromBitmapToScreen()
        {
            OwnerForm.FromBitmapToScreen();
        }

        //обновление изображения в Bitmap и pictureBox при закрытии окна
        private void Form2Closing(object sender, System.EventArgs e)
        {
            if (Form1.full_name_of_image != "\0")
            {
                Form1.FromPixelToBitmap();
                FromBitmapToScreen();
            }
        }
    }
}
