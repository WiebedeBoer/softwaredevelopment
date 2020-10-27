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

        //public String path = "path1";

        public PictureBox car;

        private PictureBox upStraightCar;
        private PictureBox leftCar;
        private PictureBox rightCar;
        private PictureBox downStraightCar;

        public String direction = "straight";

        private Path path = null;

        public bool toBeDeleted = false;

        private int oldRotation = 0;


        // Which node(x,y coordinates) on the map is the car
        public int node = 0;

        public void spawnRandomCar(Path path, int Left, int Top)
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

            car.Size = new Size(20, 33);

            

            this.path = path;


            // random path for car
            //int whichPath = rnd.Next(1, 3);

            //switch (whichPath)
          //  {
              //  case 1:
                  //  this.path = "path1";
                   // break;
                //case 2:
                  //  this.path = "path2";
                  //  break;
            //}

            car.Left = Left;

            car.Top = Top;

            //setupImages(car);

            //this.carPic = car;
        }

        private void setupImages(PictureBox car)
        {
            upStraightCar = car;
            downStraightCar = car;
            leftCar = car;
            rightCar = car;

            // car úp
            Image img = car.Image;
            upStraightCar.Image = img;
            upStraightCar.Size = new Size(20, 33);

            // car down
            Image img2 = car.Image;
            downStraightCar.Image = img2;
            img2.RotateFlip(RotateFlipType.Rotate180FlipNone);
            upStraightCar.Size = new Size(20, 33);

            // car going to left
            Image img3 = car.Image;
            leftCar.Image = img3;
            img3.RotateFlip(RotateFlipType.Rotate270FlipNone);
            leftCar.Size = new Size(33, 20);

            // car going to right
            Image img4 = car.Image;
            rightCar.Image = img4;
            img4.RotateFlip(RotateFlipType.Rotate90FlipNone);
            rightCar.Size = new Size(33, 20);
        }

        public void move(int speed, bool brake)
        {
            if (path.nodes[node].Reg != null && path.nodes[node].Reg.currentColor != RegLightSequence.Green)
            {
                brake = true;
            } 

            if (brake is false)
            {
                float tx = path.nodes[node].Left - car.Left;
                float ty = path.nodes[node].Top - car.Top;
                double length = Math.Sqrt(tx * tx + ty * ty);
                if (length > speed)
                {
                    /*
                    if (car.Left < path.nodes[node].Left && direction == "straight")
                    {
                        Image img = car.Image;
                        img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        car.Image = img;
                        car.Size = new Size(33, 20);
                        direction = "right";
                    }
                    if (car.Top < path.nodes[node].Top && direction == "straight" || direction == "right")
                    {
                        Image img = car.Image;
                        if (direction == "straight")
                            img.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        if (direction == "right")
                            img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        car.Image = img;
                        car.Size = new Size(20, 33);
                        direction = "straightdown";
                    } 
                    if (car.Left > path.nodes[node].Left && direction == "straight" || direction == "straightdown")
                    {
                        Image img = car.Image;
                        if(direction == "straight")
                            img.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        if(direction == "straightdown")
                            img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        car.Image = img;
                        car.Size = new Size(33, 20);
                        direction = "left";
                    }
                    if (car.Top > path.nodes[node].Top && direction == "right")
                    {
                        Image img = car.Image;
                        img.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        car.Image = img;
                        car.Size = new Size(20, 33);
                        direction = "straight";
                    }*/

                    turn(car.Left, car.Top);

                    // move towards the goal
                    car.Left = (int)(car.Left + speed * tx / length);
                    car.Top = (int)(car.Top + speed * ty / length);
                }
                else
                {
                    // already there
                    car.Left = path.nodes[node].Left;
                    car.Top = path.nodes[node].Top;
                    if (node < (path.nodes.Count-1) && brake is false)
                    {
                        node++;
                    } else
                    {
                        toBeDeleted = true;
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

        public void turn(int left, int top)
        {

            // right(southern spawned cars)
            if (/*top > path.nodes[node].Top &&*/ left < path.nodes[node].Left)
            {
                Image img = car.Image;
                if (oldRotation == 0) {
                    img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    oldRotation = 90;
                }
                if(oldRotation == 90)
                {
                    oldRotation = 90;
                }
                if (oldRotation == 180)
                {
                    img.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    oldRotation = 90;
                }
                if (oldRotation == 270)
                {
                    img.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    oldRotation = 90;
                }
                car.Image = img;
                car.Size = new Size(33, 20);
                

            } 
            // right(northern spawned cars)
            if (/*top < path.nodes[node].Top &&*/ left > path.nodes[node].Left)
            {
                Image img = car.Image;
                if (oldRotation == 0)
                {
                    img.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    oldRotation = 270;
                }
                if (oldRotation == 90)
                {
                    img.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    oldRotation = 270;
                }
                if (oldRotation == 180)
                {
                    img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    oldRotation = 270;
                }
                if (oldRotation == 270)
                {
                    oldRotation = 270;
                }
                car.Image = img;
                car.Size = new Size(33, 20);
            } 

            /*
            // left(southern spawned cars)
            if (top > path.nodes[node].Top && left > path.nodes[node].Left)
            {
                leftCar.Left = left;
                leftCar.Top = top;
                car.Image = leftCar.Image;
            }
            // left(northern spawned cars)
            if (top < path.nodes[node].Top && left < path.nodes[node].Left)
            {
                rightCar.Left = left;
                rightCar.Top = top;
                car.Image = rightCar.Image;
            }*/
            

            // right(eastern spawned cars)
            if (top < path.nodes[node].Top/* && left < path.nodes[node].Left*/)
            {
                Image img = car.Image;
                if (oldRotation == 0)
                {
                    img.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    oldRotation = 180;
                }
                if (oldRotation == 90)
                {
                    img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    oldRotation = 180;
                }
                if (oldRotation == 180)
                {
                    oldRotation = 180;
                }
                if (oldRotation == 270)
                {
                    img.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    oldRotation = 180;
                }
                car.Image = img;
                car.Size = new Size(20, 33);
            }
            // right(western spawned cars)
            if (top > path.nodes[node].Top/* && left > path.nodes[node].Left*/)
            {
                Image img = car.Image;
                if (oldRotation == 0)
                {
                    oldRotation = 0;
                }
                if (oldRotation == 90)
                {
                    img.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    oldRotation = 0;
                }
                if (oldRotation == 180)
                {
                    img.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    oldRotation = 0;
                }
                if (oldRotation == 270)
                {
                    img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    oldRotation = 0;
                }
                car.Image = img;
                car.Size = new Size(20, 33);
            }


        }

        public bool collisionDetection(List<Car> cars)
        {
            List<Car> cars2 = cars.Where(car => car.guid != this.guid).ToList();

            if(cars2.Count == 0)
            {
                return false;
            }

            // Collisionboxes
            Rectangle rect = new Rectangle();
            if (direction == "straight")
            {
                 rect = new Rectangle(car.Left, (car.Top - 10), car.Width, 10);
            }

            if (direction == "straightdown")
            {
                rect = new Rectangle(car.Left, (car.Top + car.Height + 10), car.Width, 10);
            }

            if (direction == "right")
            {
                rect = new Rectangle((car.Left + car.Width), car.Top, 10, car.Top);
            }

            if (direction == "left")
            {
                rect = new Rectangle((car.Left - car.Width), car.Top, 10, car.Top);
            }

            int carInFront = 0;
            
            foreach (Car car2 in cars2)
            {
                if((rect.IntersectsWith(car2.car.Bounds)))
                {
                    carInFront++;
                }

                if (car.Bounds.IntersectsWith(car2.car.Bounds) && carInFront != 0)
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
