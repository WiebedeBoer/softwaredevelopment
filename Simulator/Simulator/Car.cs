using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;

namespace Simulator
{
    class Car
    {
        public PictureBox carPic;


        public void spawnRandomCar(int Left, int Top)
        {
            PictureBox car = new PictureBox();

            // random color for car
            Random rnd = new Random();
            int whichCar = rnd.Next(1, 5);

            switch (whichCar)
            {
                case 1:
                    car.Image = Properties.Resources.blue_car;
                    break;
                case 2:
                    car.Image = Properties.Resources.red_car;
                    break;
                case 3:
                    car.Image = Properties.Resources.yellow_car;
                    break;
                case 4:
                    car.Image = Properties.Resources.green_car;
                    break;
            }

            car.BackColor = Color.Transparent;

            car.SizeMode = PictureBoxSizeMode.StretchImage;

            car.Size = new Size(23, 33);

            // random path for car
            int whichPath = rnd.Next(1, 3);

            switch (whichPath)
            {
                case 1:
                    car.Tag = "carP1";
                    break;
                case 2:
                    car.Tag = "carP2";
                    break;
            }

            car.Left = 340;

            car.Top = 580;

            this.carPic = car;
        }
    }
}
