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

        public PictureBox car;

        public String direction = "straight";


        // Which node(x,y coordinates) on the map is the car
        public int node = 0;

        public void spawnRandomCar(int Left, int Top)
        {
            car = new PictureBox();

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

        public void move(int[,] pointsAlongPath, int speed)
        {
            float tx = pointsAlongPath[node, 0] - carPic.Left;
            float ty = pointsAlongPath[node, 1] - carPic.Top;
            double length = Math.Sqrt(tx * tx + ty * ty);
            if (length > speed)
            {
                if(carPic.Left < pointsAlongPath[node, 0] && direction == "straight")
                {
                    Image img = car.Image;
                    img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    car.Image = img;
                    car.Size = new Size(33, 23);
                    direction = "right";
                }
                // move towards the goal
                carPic.Left = (int)(carPic.Left + speed * tx / length);
                carPic.Top = (int)(carPic.Top + speed * ty / length);
            }
            else
            {
                // already there
                carPic.Left = pointsAlongPath[node, 0];
                carPic.Top = pointsAlongPath[node, 1];
                if(node < 2)
                {
                    int x = pointsAlongPath.GetLength(0);
                    node++;
                }
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
