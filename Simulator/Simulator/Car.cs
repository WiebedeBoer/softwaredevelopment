using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;

namespace Simulator
{
    class Car : Traffic
    {
        public void spawn(Path path, int Left, int Top)
        {
            x = new PictureBox();

            // random color for x
            Random rnd = new Random();
            int whichx = rnd.Next(1, 5);

            switch (whichx)
            {
                case 1:
                    x.Image = Properties.Resources.blue_car;
                    break;
                case 2:
                    x.Image = Properties.Resources.red_car;
                    break;
                case 3:
                    x.Image = Properties.Resources.yellow_car;
                    break;
                case 4:
                    x.Image = Properties.Resources.green_car;
                    break;
            }

            x.BackColor = Color.Transparent;

            x.SizeMode = PictureBoxSizeMode.StretchImage;

            x.Size = new Size(20, 33);

            this.path = path;

            x.Left = Left;

            x.Top = Top;
        }

    }
}
