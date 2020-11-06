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

        // trafficligths
        private List<RegularTrafficLight> lights = new List<RegularTrafficLight>();

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
            lights.Add(regA44);
            RegularTrafficLight regA34 = createTrafficLight(737, 387, "A34", "left");
            lights.Add(regA34);
            RegularTrafficLight regA33 = createTrafficLight(737, 365, "A33", "left");
            lights.Add(regA33);

            RegularTrafficLight regA43 = createTrafficLight(320, 421, "A43", "up");
            lights.Add(regA43);
            RegularTrafficLight regA32 = createTrafficLight(737, 343, "A32", "left");
            lights.Add(regA32);
            RegularTrafficLight regA31 = createTrafficLight(737, 320, "A31", "left");
            lights.Add(regA31);

            RegularTrafficLight regA42 = createTrafficLight(298, 421, "A42", "up");
            lights.Add(regA42);
            RegularTrafficLight regA41 = createTrafficLight(276, 421, "A41", "up");
            lights.Add(regA41);

            RegularTrafficLight regA54 = createTrafficLight(178, 431, "A54", "left");
            lights.Add(regA54);
            RegularTrafficLight regA53 = createTrafficLight(178, 409, "A53", "left");
            lights.Add(regA53);
            RegularTrafficLight regA52 = createTrafficLight(178, 387, "A52", "left");
            lights.Add(regA52);
            RegularTrafficLight regA51 = createTrafficLight(178, 365, "A51", "left");
            lights.Add(regA51);

            RegularTrafficLight regA11 = createTrafficLight(726, 140, "A11", "down");
            lights.Add(regA11);
            RegularTrafficLight regA12 = createTrafficLight(750, 140, "A12", "down");
            lights.Add(regA12);
            RegularTrafficLight regA13 = createTrafficLight(772, 140, "A13", "down");
            lights.Add(regA13);

            RegularTrafficLight regA21 = createTrafficLight(850, 162, "A21", "right");
            lights.Add(regA21);
            RegularTrafficLight regA22 = createTrafficLight(850, 183, "A22", "right");
            lights.Add(regA22);
            RegularTrafficLight regA23 = createTrafficLight(850, 207, "A23", "right");
            lights.Add(regA23);
            RegularTrafficLight regA24 = createTrafficLight(850, 230, "A24", "right");
            lights.Add(regA24);

            RegularTrafficLight regA61 = createTrafficLight(320, 206, "A61", "right");
            lights.Add(regA61);
            RegularTrafficLight regA62 = createTrafficLight(320, 227, "A62", "right");
            lights.Add(regA62);
            RegularTrafficLight regA63 = createTrafficLight(320, 252, "A63", "right");
            lights.Add(regA63);
            RegularTrafficLight regA64 = createTrafficLight(320, 276, "A64", "right");
            lights.Add(regA64);

            BusTrafficLight busB11 = createBusTrafficLight(796, 140, "B11", "down");
            BusTrafficLight busB12 = createBusTrafficLight(796, 180, "B12", "down");
            BusTrafficLight busB41 = createBusTrafficLight(364, 421, "B41", "down");


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

            // a11 - links voorsorteer over a61
            Path patha11 = new Path();
            patha11.addNode(726, 1); //start
            patha11.addNode(726, 80); //light a11
            patha11.addNode(721, 195, regA11);
            patha11.addNode(572, 206);
            patha11.addNode(391, 206);
            patha11.addNode(353, 206, regA61); //light a61
            patha11.addNode(1, 206); //end
            paths.Add(patha11);

            // a11 - rechts voorsorteer over a62
            Path patha11r = new Path();
            patha11r.addNode(726, 1); //start
            patha11r.addNode(726, 80); //light a11
            patha11r.addNode(721, 195, regA11);
            patha11r.addNode(572, 227);
            patha11r.addNode(391, 227);
            patha11r.addNode(353, 227, regA62); //light a62
            patha11r.addNode(1, 227); //end
            paths.Add(patha11r);

            //  a12 - links voorsorteer over a63
            Path patha12 = new Path();
            patha12.addNode(750, 1); //start
            patha12.addNode(750, 80); //light a12
            patha12.addNode(750, 195, regA12);
            patha12.addNode(571, 252);
            patha12.addNode(389, 252);
            patha12.addNode(353, 252, regA64); //light a63
            patha12.addNode(227, 266);
            patha12.addNode(206, 345);
            patha12.addNode(206, 606); //end
            paths.Add(patha12);

            //  a12 - rechts voorsorteer over a64
            Path patha12r = new Path();
            patha12r.addNode(750, 1); //start
            patha12r.addNode(750, 80); //light a12
            patha12r.addNode(750, 195, regA12);
            patha12r.addNode(571, 276);
            patha12r.addNode(389, 276);
            patha12r.addNode(353, 276, regA64); //light a64
            patha12r.addNode(240, 282);
            patha12r.addNode(232, 345);
            patha12r.addNode(232, 606); //end
            paths.Add(patha12r);

            //  a13 - rechts voorsorteer 
            Path patha13 = new Path();
            patha13.addNode(772, 1); //start
            patha13.addNode(772, 80); //light a13
            patha13.addNode(772, 353, regA13);
            patha13.addNode(778, 388);
            patha13.addNode(1075, 388); //end
            paths.Add(patha13);

            // a21 
            Path patha21 = new Path();
            patha21.addNode(1075, 162); //start
            patha21.addNode(930, 162); //light a21
            patha21.addNode(866, 160, regA21);
            patha21.addNode(866, 105);
            patha21.addNode(866, 1); //end
            paths.Add(patha21);

            // a22   
            Path patha22 = new Path();
            patha22.addNode(1075, 183); //start
            patha22.addNode(930, 183); //light a22
            patha22.addNode(839, 105, regA21);
            patha22.addNode(839, 1); //end
            paths.Add(patha22);

            // a23 rechts voorsorteer over a61
            Path patha23 = new Path();
            patha23.addNode(1075, 207); //start
            patha23.addNode(930, 207); //light a23
            patha23.addNode(766, 207, regA23);
            patha23.addNode(581, 207);
            patha23.addNode(353, 207, regA61); //light a61
            patha23.addNode(1, 207); //end
            paths.Add(patha23);

            // a24 links voorsorteer over a64  
            Path patha24 = new Path();
            patha24.addNode(1075, 230); //start
            patha24.addNode(930, 230); //light a24
            patha24.addNode(765, 230, regA24);
            patha24.addNode(675, 253);
            patha24.addNode(579, 276);
            patha24.addNode(353, 276, regA64); //light a64
            patha24.addNode(240, 282);
            patha24.addNode(232, 345);
            patha24.addNode(232, 606); //end
            paths.Add(patha24);

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
            JSONTrafficLight trafficJSON = new JSONTrafficLight();
            foreach(RegularTrafficLight reg in lights)
            {
                if(reg.name == "A11")
                {
                    trafficJSON.A11 = reg.carInFront ? 1 : 0;
                }
                //listener.jsonToSend = trafficJSON;
            }

            JObject JSONtraffic = new JObject();

            foreach (RegularTrafficLight reg in lights)
            {
                JSONtraffic[reg.name] = reg.carInFront ? 1 : 0;
            }
            listener.jsonToSend = JSONtraffic;
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

        private BusTrafficLight createBusTrafficLight(int left, int top, string name, string direction)
        {
            BusTrafficLight reg = new BusTrafficLight();

            reg.createTrafficLight(left, top, name, direction);

            this.Controls.Add(reg.regTrafficLight);

            return reg;
        }
    }
}
