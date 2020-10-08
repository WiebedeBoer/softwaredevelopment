using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simulator
{
    public partial class Form1 : Form
    {

        public int spawnTimer = 0;
        public int randomTimeSpawned = 10; // standard

        List<Car> cars = new List<Car>();

        RegularTrafficLight reg;

        // Lane 1
        public int[,] pointsAlongPath1 = new int[,] { { 340, 411 }, { 383, 388 }, { 600, 384 } };
        public int[,] pointsAlongPath2 = new int[,] { { 340, 411 }, { 383, 364 }, { 600, 361 } };
        private Car carInFront = null;

        public Form1()
        {
            InitializeComponent();

            // var trafficLightsSequence = new ArrayList()
            //         {
            //             "..\\red_light.png",
            //             "..\\yellow_light.png",
            //            "..\\green_light.png"
            //       };

            //spawnRandomCar();

            createTrafficLight(342, 421);
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            //pictureBox1.Top -= 10;

            //Image img = pictureBox1.Image;
            //img.RotateFlip(RotateFlipType.Rotate90FlipNone);
            //pictureBox1.Image = img;

            spawnTimer += 1;

            

            if(spawnTimer == randomTimeSpawned)
            {
                spawnRandomCar();

                Random rnd = new Random();
                this.randomTimeSpawned = rnd.Next(10, 30);

                spawnTimer = 0;
            }

            foreach(Car x in this.cars)
            {
                bool brake = false;
                if (carInFront != null && !carInFront.guid.Equals(x.guid))
                {
                    // Check if car is detected in front, so they dont collide
                    brake = x.collisionDetection(carInFront.car);
                }
                if(x.path == "path1")
                {
                    x.move(pointsAlongPath1, 10, brake);
                }
                if (x.path == "path2")
                {
                    x.move(pointsAlongPath2, 10, brake);
                }
                carInFront = x;
            }

            foreach(Control x in this.Controls)
            {
                //if(x is PictureBox && x.Tag == "carP1" || x is PictureBox && x.Tag == "carP2")
                //{
                //    x.Top -= 10;
                //}
                //if (((PictureBox)x).Top < this.Height - (this.Height-50) || ((PictureBox)x).Left > (this.Width-50))
                //{
                   // this.Controls.Remove(x);
               // }
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            // trafficlight
            reg.SwitchLight();
        }


        private void spawnRandomCar()
        {
            Car car = new Car();

            car.spawnRandomCar(340, 580);

            cars.Add(car);

            this.Controls.Add(car.car);
        }

        private void createTrafficLight(int left, int top)
        {
            reg = new RegularTrafficLight();

            reg.createTrafficLight(left, top);

            this.Controls.Add(reg.regTrafficLight);
        }
    }
}
