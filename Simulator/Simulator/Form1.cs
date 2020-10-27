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

        // Lane 1
        public int[,] pointsAlongPath1 = new int[,] { { 340, 411 }, { 383, 388 }, { 600, 384 } };
        public int[,] pointsAlongPath2 = new int[,] { { 340, 411 }, { 383, 364 }, { 600, 360 } };


        private List<Path> paths = new List<Path>();


        private BackGroundListener listener = new BackGroundListener();

        private JSONTrafficLight json = null;

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

            
            RegularTrafficLight reg = createTrafficLight(342, 421, "A11");

            
            Path path = new Path();

            path.addNode(342, 580);
            path.addNode(342, 430);
            // A44 - trafficlight
            path.addNode(342, 411, reg);
            path.addNode(383, 388);
            path.addNode(1083, 386);
            paths.Add(path);

            
            Path path2 = new Path();

            path2.addNode(342, 580);
            path2.addNode(342, 430);
            // A44 - trafficlight -  Alternative path
            path2.addNode(342, 411);
            path2.addNode(383, 366);
            path2.addNode(1083, 362);
            paths.Add(path2);

            
            Path path3 = new Path();

            path3.addNode(320, 580);
            path3.addNode(320, 430);
            // A43 - trafficlight
            path3.addNode(332, 366);
            path3.addNode(369, 342);
            path3.addNode(815, 342);
            path3.addNode(855, 320);
            path3.addNode(860, 0);
            paths.Add(path3);

            Path path4 = new Path();

            path4.addNode(320, 580);
            path4.addNode(320, 430);
            // A43 - trafficlight -  Alternative path
            path4.addNode(368, 322);
            path4.addNode(369, 318);
            path4.addNode(811, 318);
            path4.addNode(835, 300);
            path4.addNode(836, 0);
            paths.Add(path4);

            Path path5 = new Path();

            path5.addNode(296, 580);
            path5.addNode(296, 430);
            // A42 - trafficlight
            path5.addNode(296, 248);
            path5.addNode(278, 210);
            path5.addNode(0, 206);
            paths.Add(path5);

            Path path6 = new Path();

            path6.addNode(274, 580);
            path6.addNode(274, 430);
            // A41 - trafficlight
            path6.addNode(274, 257);
            path6.addNode(240, 230);
            path6.addNode(0, 227);
            paths.Add(path6);

            Path path7 = new Path();

            path7.addNode(1, 433);
            path7.addNode(120, 433);
            // A54 - trafficlight
            path7.addNode(208, 433); // add trafficlight
            path7.addNode(206, 610);
            paths.Add(path7);

            //Path patha11 = new Path();
            //patha11.addNode(734, 111); //start (732,1)
            //patha11.addNode(729, 203); //light(none yet)
            //patha11.addNode(580, 215);
            //patha11.addNode(399, 214);
            //paths.Add(patha11);



            Thread listen = new Thread(listener.Connect);

            listen.Start();
            
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            
            json = listener.Json;

            //Console.WriteLine(json.A11.);

            if (json != null) {
                foreach (Path p in paths)
                {
                    foreach (Node n in p.nodes)
                    {
                        if (n.Reg.name == "A11")
                            n.Reg.LightSequence(json.A11);
                    }
                }
            }

            
                

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
                this.randomTimeSpawned = rnd.Next(5, 20);

                spawnTimer = 0;
            }


            foreach (Car x in this.cars)
            {
                
                //if (carInFront != null && !carInFront.guid.Equals(x.guid))
                //{
                // Check if car is detected in front, so they dont collide
                

                bool brake = x.collisionDetection(this.cars);

                x.move(10, brake);
                //}
                //if (x.path == "path1")
                //{
                //    x.move(paths[random], 10, brake);
                //}
                //if (x.path == "path2")
                //{
                //    x.move(paths[1], 10, brake);
               // }
                // je moet zorgen dat de carInFront per path is, anders registreert hij carInfront als auto op de rijbaan ernaast...
                //carInFront = x;
            }

            foreach (Car i in cars.Reverse<Car>())
            {
                if (i.toBeDeleted == true)
                {
                    cars.Remove(i);
                    this.Controls.Remove(i.car);
                }
            }

            foreach (Control x in this.Controls)
            {

               
                /*
                List<Car> copy = cars;
                foreach (Car z in copy)
                {
                
                    if(z.toBeDeleted == true)
                    {
                        cars.Remove(z);
                        this.Controls.Remove(z.car);
                    }
                }*/
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
            //reg.SwitchLight();

            //reg.LightSequence(1);
        }


        private void spawnRandomCar()
        {
            Random rnd = new Random();
            int random = rnd.Next(paths.Count);

            Car car = new Car();

            car.spawnRandomCar(paths[random], paths[random].nodes[0].Left, paths[random].nodes[0].Top);

            cars.Add(car);

            this.Controls.Add(car.car);
        }

        private RegularTrafficLight createTrafficLight(int left, int top, string name)
        {
            RegularTrafficLight reg = new RegularTrafficLight();

            reg.createTrafficLight(left, top, name);

            this.Controls.Add(reg.regTrafficLight);

            return reg;
        }
    }
}
