using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SlideView
{
    public partial class FSlideView : Form
    {
        readonly string[] Images;
        private int Index = 0;

        public FSlideView(string[] images)
        {
            InitializeComponent();
            Images = images;
            LoadNextImage(0);
        }

        bool LoadNextImage(int idx)
        {
            if ((idx < 0) || (idx >= Images.Length)) return false;
            try
            {
                Image tmp = pbImage.Image;
                var img = Bitmap.FromFile(Images[Index]);
                pbImage.Image = img;
                tmp?.Dispose();
                Text = $"{Index + 1} / {Images.Length}";
                return true;
            }
            catch
            { }
            return false;
        }

        private void SlideView_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void SlideView_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                case Keys.Right:
                    GotoNextImage();
                    break;
                case Keys.Back:
                case Keys.Left:
                    GotoPreviousImage();
                    break;
                case Keys.Return:
                    if (e.Alt)
                    {
                        ToggleFullScreen();
                    }
                    break;
                case Keys.Escape:
                    Close();
                    break;
            }
        }

        private void ToggleFullScreen()
        {
            if (FormBorderStyle == FormBorderStyle.Sizable)
            {
                FormBorderStyle = FormBorderStyle.None;
                WindowState = FormWindowState.Maximized;
            }
            else
            {
                FormBorderStyle = FormBorderStyle.Sizable;
                WindowState = FormWindowState.Normal;
            }
        }

        private void GotoPreviousImage()
        {
            bool done = false;
            while (!done)
            {
                if (Index > 0)
                {
                    done = LoadNextImage(Index--);
                }
                else
                { break; }
            }
        }

        private void GotoNextImage()
        {
            bool done = false;
            while (!done)
            {
                if (Index < Images.Length - 1)
                {
                    done = LoadNextImage(Index++);
                }
                else { break; }
            }
        }
    }
}
