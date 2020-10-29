using Newtonsoft.Json.Linq;
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

        List<Traffic> traffic = new List<Traffic>();
        //List<Bus> busses = new List<Bus>();

        // Lane 1
        public int[,] pointsAlongPath1 = new int[,] { { 340, 411 }, { 383, 388 }, { 600, 384 } };
        public int[,] pointsAlongPath2 = new int[,] { { 340, 411 }, { 383, 364 }, { 600, 360 } };

        // cars
        private List<Path> paths = new List<Path>();

        // buspaths
        private List<Path> busPaths = new List<Path>();


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


            RegularTrafficLight regA44 = createTrafficLight(342, 421, "A44", "up");
            RegularTrafficLight regA34 = createTrafficLight(737, 387, "A34", "left");
            RegularTrafficLight regA33 = createTrafficLight(737, 365, "A33", "left");

            RegularTrafficLight regA43 = createTrafficLight(320, 421, "A43","up");
            RegularTrafficLight regA32 = createTrafficLight(737, 343, "A32", "left");
            RegularTrafficLight regA31 = createTrafficLight(737, 320, "A31", "left");

            RegularTrafficLight regA42 = createTrafficLight(298, 421, "A42", "up");
            RegularTrafficLight regA41 = createTrafficLight(276, 421, "A41", "up");

            RegularTrafficLight regA54 = createTrafficLight(178, 431, "A54", "left");
            RegularTrafficLight regA53 = createTrafficLight(178, 409, "A53", "left");
            RegularTrafficLight regA52 = createTrafficLight(178, 387, "A52", "left");
            RegularTrafficLight regA51 = createTrafficLight(178, 365, "A51", "left");

            Path path = new Path();

            path.addNode(342, 580);
            path.addNode(342, 500);
            // A44 - trafficlight
            path.addNode(342, 411, regA44);
            path.addNode(383, 388);
            path.addNode(700, 386);
            path.addNode(1083, 386, regA34);
            paths.Add(path);

            
            Path path2 = new Path();

            path2.addNode(342, 580);
            path2.addNode(342, 500);
            // A44 - trafficlight -  Alternative path
            path2.addNode(342, 411, regA44);
            path2.addNode(383, 366);
            path2.addNode(700, 386);
            path2.addNode(1083, 362, regA33);
            paths.Add(path2);

            
            Path path3 = new Path();

            path3.addNode(320, 580);
            path3.addNode(320, 500);
            // A43 - trafficlight
            path3.addNode(332, 366, regA43);
            path3.addNode(369, 342);
            path2.addNode(700, 342);
            path3.addNode(815, 342, regA32);
            path3.addNode(855, 320);
            path3.addNode(860, 0);
            paths.Add(path3);

            Path path4 = new Path();

            path4.addNode(320, 580);
            path4.addNode(320, 500);
            // A43 - trafficlight -  Alternative path
            path4.addNode(368, 322, regA43);
            path4.addNode(369, 318);
            path2.addNode(700, 318, regA31);
            path4.addNode(811, 318);
            path4.addNode(835, 300);
            path4.addNode(836, 0);
            paths.Add(path4);

            Path path5 = new Path();

            path5.addNode(296, 580);
            path5.addNode(296, 500);
            // A42 - trafficlight
            path5.addNode(296, 248, regA42);
            path5.addNode(278, 210);
            path5.addNode(0, 206);
            paths.Add(path5);

            Path path6 = new Path();

            path6.addNode(274, 580);
            path6.addNode(274, 500);
            // A41 - trafficlight
            path6.addNode(274, 257, regA41);
            path6.addNode(240, 230);
            path6.addNode(0, 227);
            paths.Add(path6);

            Path path7 = new Path();

            path7.addNode(1, 433);
            path7.addNode(120, 433);
            // A54 - trafficlight
            path7.addNode(208, 433, regA54); // add trafficlight
            path7.addNode(206, 610);
            paths.Add(path7);

            Path path8 = new Path();

            path8.addNode(1, 409);
            path8.addNode(120, 409);
            // A53 - trafficlight
            path8.addNode(208, 409, regA53);
            path8.addNode(224, 443);// add trafficlight
            path8.addNode(230, 610);
            paths.Add(path8);

            Path path9 = new Path();

            path9.addNode(1, 385);
            path9.addNode(120, 385);
            // A52 - trafficlight
            path9.addNode(700, 385, regA52);
            path9.addNode(1083, 385, regA34); // add trafficlight
            paths.Add(path9);

            Path path10 = new Path();

            path10.addNode(1, 385);
            path10.addNode(120, 385);
            // A52 - trafficlight - Aternative path
            path10.addNode(310, 385, regA52);
            path10.addNode(400, 363);
            path10.addNode(700, 363);
            path10.addNode(1083, 363, regA33); // add trafficlight
            paths.Add(path10);

            Path path11 = new Path();

            path11.addNode(1, 363);
            path11.addNode(120, 363);
            // A51 - trafficlight
            path11.addNode(272, 363, regA51);
            path11.addNode(401, 341);
            path11.addNode(700, 341);
            path11.addNode(837, 341, regA32); // add trafficlight
            path11.addNode(862, 299);
            path11.addNode(862, 0);

            Path path12 = new Path();

            path12.addNode(1, 363);
            path12.addNode(120, 363);
            // A51 - trafficlight
            path12.addNode(272, 363, regA51);
            path12.addNode(401, 318);
            path12.addNode(700, 318);
            path12.addNode(828, 312, regA31); // add trafficlight
            path12.addNode(839, 299);
            path12.addNode(839, 0);

            paths.Add(path12);

            /* WRONG PATH, SHOULD BE GOING NORTH...
            ////// BUSSES /////
            Path buspathb41 = new Path();
            buspathb41.addNode(364, 592);
            buspathb41.addNode(364, 505);
            buspathb41.addNode(364, 460); // add trafficlight
            buspathb41.addNode(342, 433);
            buspathb41.addNode(361, 387);
            buspathb41.addNode(1083, 387);

            busPaths.Add(buspathb41);

            */
            

            Thread listen = new Thread(listener.Connect);

            listen.Start();
            
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            
            //json = listener.Json;

            JObject json2 = listener.json2;
            //Console.WriteLine(json.A11.);

            if (json2 != null) {
                foreach (Path p in paths)
                {
                    foreach (Node n in p.nodes)
                    {
                        if (n.Reg != null)
                        {
                            for (int i = 1; i <= 6; i++)
                            {
                                for (int j = 0; j <= 4; j++)
                                {
                                    if (n.Reg.name == ("A" + i.ToString() + j.ToString()))
                                    {
                                        string name = ("A" + i.ToString() + j.ToString());
                                        //n.Reg.LightSequence(json["A11"].Value);
                                        if(json2[name] != null)
                                            n.Reg.LightSequence((int)json2[name]);
                                    }
                                }
                            }
                        }
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
                spawnRandomBus();

                Random rnd = new Random();
                this.randomTimeSpawned = rnd.Next(5, 20);

                spawnTimer = 0;
            }

            /*
            foreach (Bus x in this.busses)
            {
                bool brake = x.collisionDetection(this.busses);
                
                x.move(10, brake);
            }*/

            foreach (Traffic x in this.traffic)
            {
                
                //if (carInFront != null && !carInFront.guid.Equals(x.guid))
                //{
                // Check if car is detected in front, so they dont collide
                

                bool brake = x.collisionDetection(this.traffic);

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

            foreach (Traffic i in traffic.Reverse<Traffic>())
            {
                if (i.toBeDeleted == true)
                {
                    traffic.Remove(i);
                    this.Controls.Remove(i.x);
                }
            }
            /*
            foreach (Bus i in busses.Reverse<Bus>())
            {
                if (i.toBeDeleted == true)
                {
                    busses.Remove(i);
                    this.Controls.Remove(i.bus);
                }
            }*/

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

        private void spawnRandomBus()
        {
            Random rnd = new Random();
            int random = rnd.Next(busPaths.Count);

            
            if (busPaths.Count > 0)
            {
                Bus bus = new Bus();
                bus.spawn(busPaths[random], busPaths[random].nodes[0].Left, busPaths[random].nodes[0].Top);

                traffic.Add(bus);

                this.Controls.Add(bus.x);
            }

            
        }

        private void spawnRandomCar()
        {
            Random rnd = new Random();
            int random = rnd.Next(paths.Count);

            Car car = new Car();

            car.spawn(paths[random], paths[random].nodes[0].Left, paths[random].nodes[0].Top);

            traffic.Add(car);

            this.Controls.Add(car.x);
        }

        private RegularTrafficLight createTrafficLight(int left, int top, string name, string direction)
        {
            RegularTrafficLight reg = new RegularTrafficLight();

            reg.createTrafficLight(left, top, name, direction);

            this.Controls.Add(reg.regTrafficLight);

            return reg;
        }
    }
}
