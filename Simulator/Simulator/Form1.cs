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

        // Lane 1
        public int[,] pointsAlongPath1 = new int[,] { { 340, 411 }, { 383, 388 } };
        public int[,] pointsAlongPath2 = new int[,] { { 340, 411 }, { 383, 364 } };

        public Form1()
        {
            InitializeComponent();

           // var trafficLightsSequence = new ArrayList()
           //         {
           //             "..\\red_light.png",
           //             "..\\yellow_light.png",
            //            "..\\green_light.png"
             //       };
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
                if(x.carPic.Tag == "carP1")
                {
                    x.carPic.Top -= 10;
                }
                if (x.carPic.Tag == "carP2")
                {
                    x.carPic.Top -= 10;
                }
            }

            foreach(Control x in this.Controls)
            {
                //if(x is PictureBox && x.Tag == "carP1" || x is PictureBox && x.Tag == "carP2")
                //{
                //    x.Top -= 10;
                //}
                if (((PictureBox)x).Top < this.Height -650)
                {
                    this.Controls.Remove(x);
                }
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {

        }


        private void spawnRandomCar()
        {
            Car car = new Car();

            car.spawnRandomCar(340, 580);

            cars.Add(car);

            this.Controls.Add(car.carPic);
        }
    }
}
