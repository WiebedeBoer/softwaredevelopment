using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;

namespace Simulator
{
    class Car
    {
        //public PictureBox carPic;

        public Guid guid = Guid.NewGuid();

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

            //this.carPic = car;
        }

        public void move(Path pointsAlongPath, int speed, bool brake)
        {
            if (pointsAlongPath.nodes[node].Reg != null && pointsAlongPath.nodes[node].Reg.currentColor == RegLightSequence.Red)
            {
                brake = true;
            }
            
            if (brake is false)
            {
                float tx = pointsAlongPath.nodes[node].Left - car.Left;
                float ty = pointsAlongPath.nodes[node].Top - car.Top;
                double length = Math.Sqrt(tx * tx + ty * ty);
                if (length > speed)
                {
                    if (car.Left < pointsAlongPath.nodes[node].Left && direction == "straight")
                    {
                        Image img = car.Image;
                        img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        car.Image = img;
                        car.Size = new Size(33, 23);
                        direction = "right";
                    }
                    // move towards the goal
                    car.Left = (int)(car.Left + speed * tx / length);
                    car.Top = (int)(car.Top + speed * ty / length);
                }
                else
                {
                    // already there
                    car.Left = pointsAlongPath.nodes[node].Left;
                    car.Top = pointsAlongPath.nodes[node].Top;
                    if (node < (pointsAlongPath.nodes.Count-1) && brake is false)
                    {
                        node++;
                    }
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

        public bool collisionDetection(List<Car> cars)
        {
            List<Car> cars2 = cars.Where(car => car.guid != this.guid).ToList();

            if(cars2.Count == 0)
            {
                return false;
            }


            foreach (Car car2 in cars2)
            {
                if (car.Bounds.IntersectsWith(car2.car.Bounds))
                {
                    return true;
                }
            }

            return false;


            //foreach (Car car2 in cars2)
            //{
                
              //  if (car.Left + car.Width < car2.car.Left)
                //    continue;
               // if (car2.car.Left + car2.car.Width < car.Left)
                //    continue;
               // if (car.Top + car.Height < car2.car.Top)
               //     continue;
              //  if (car2.car.Top + car2.car.Height < car.Top)
              //      continue;

               // return true;
           // }

            //return false;
        }
    }
}
