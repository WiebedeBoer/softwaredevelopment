using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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
        public int[,] pointsAlongPath2 = new int[,] { { 340, 411 }, { 383, 364 }, { 600, 360 } };


        private List<Path> paths = new List<Path>();

        private SocketListen socket = new SocketListen();

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

            //socket.LoopConnect();

            createTrafficLight(342, 421);

            Path path = new Path();

            path.addNode(340, 430);
            path.addNode(340, 411, reg);
            path.addNode(383, 388);
            path.addNode(600, 384);
            paths.Add(path);

            Path path2 = new Path();

            path2.addNode(340, 430);
            path2.addNode(340, 411, reg);
            path2.addNode(383, 364);
            path2.addNode(600, 360);
            paths.Add(path2);

            BackGroundListener listener = new BackGroundListener();

            Thread listen = new Thread(listener.Connect);

            listen.Start();
            
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            //pictureBox1.Top -= 10;

            //Image img = pictureBox1.Image;
            //img.RotateFlip(RotateFlipType.Rotate90FlipNone);
            //pictureBox1.Image = img;

            spawnTimer += 1;

            

            //if(socket.returnString.Equals("Bericht van socket"))
            //{
            //    reg.SwitchLight();
            //}

            if(spawnTimer == randomTimeSpawned)
            {
                spawnRandomCar();

                Random rnd = new Random();
                this.randomTimeSpawned = rnd.Next(10, 30);

                spawnTimer = 0;
            }


            foreach (Car x in this.cars)
            {
                
                //if (carInFront != null && !carInFront.guid.Equals(x.guid))
                //{
                    // Check if car is detected in front, so they dont collide
                    bool brake = x.collisionDetection(this.cars);
                //}
                if(x.path == "path1")
                {
                    x.move(paths[0], 10, brake);
                }
                if (x.path == "path2")
                {
                    x.move(paths[1], 10, brake);
                }
                // je moet zorgen dat de carInFront per path is, anders registreert hij carInfront als auto op de rijbaan ernaast...
                //carInFront = x;
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
