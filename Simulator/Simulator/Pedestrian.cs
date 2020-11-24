using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Simulator
{
    class Pedestrian : Traffic
    {
        public void spawn(Path path, int Left, int Top)
        {
            x = new PictureBox();

            x.Image = Properties.Resources.pedestrian;

            x.BackColor = Color.Transparent;

            x.SizeMode = PictureBoxSizeMode.StretchImage;

            x.Size = new Size(10, 10);
            Image img = x.Image;

            img.RotateFlip(RotateFlipType.Rotate270FlipNone);

            this.path = path;
            x.Left = Left;
            x.Top = Top;

            this.width = 10;
            this.height = 10;
        }
    }
}
