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

        public String path = "path1";


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
                    this.path = "path1";
                    break;
                case 2:
                    this.path = "path2";
                    break;
            }

            car.Left = 340;

            car.Top = 580;

            this.carPic = car;
        }

        public void move(int leftPoint, int topPoint, int speed)
        {
            float tx = leftPoint - carPic.Left;
            float ty = topPoint - carPic.Top;
            double length = Math.Sqrt(tx * tx + ty * ty);
            if (length > speed)
            {
                // move towards the goal
                carPic.Left = (int)(carPic.Left + speed * tx / length);
                carPic.Top = (int)(carPic.Top + speed * ty / length);
            }
            else
            {
                // already there
                carPic.Left = leftPoint;
                carPic.Top = topPoint;
            }


            //if(carPic.Left < leftPoint && carPic.Top > topPoint)
            //{
            //   carPic.Left += 10;
            //  carPic.Top -= 10;
            // }
            //if (carPic.Left < leftPoint && carPic.Top <= topPoint)
            //{
            //    carPic.Left += 10;
            // }
            // if (carPic.Left >= leftPoint && carPic.Top > topPoint)
            // {
            // carPic.Top -= 10;
            // }
        }
    }
}
