using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Simulator
{
    class Bus : Traffic
    {
        public void spawn(Path path, int Left, int Top)
        {
            x = new PictureBox();

            // random color for car
            Random rnd = new Random();

            x.Image = Properties.Resources.bus;

            x.BackColor = Color.Transparent;

            x.SizeMode = PictureBoxSizeMode.StretchImage;

            x.Size = new Size(20, 60);

            this.path = path;
            x.Left = Left;
            x.Top = Top;

            this.width = 20;
            this.height = 60;
        }
    }
}
