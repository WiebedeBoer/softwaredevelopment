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


            ////// BUSSES /////
            Path buspathb41 = new Path();
            buspathb41.addNode(364, 592);
            buspathb41.addNode(364, 505);
            buspathb41.addNode(364, 460); // add trafficlight
            buspathb41.addNode(342, 433);
            buspathb41.addNode(361, 387);
            buspathb41.addNode(1083, 387);

            busPaths.Add(buspathb41);


            Path patha11 = new Path();
            patha11.addNode(726, 1); //start
            patha11.addNode(726, 103); //light a11
            patha11.addNode(721, 195);
            patha11.addNode(572, 206);
            patha11.addNode(391, 206);
            patha11.addNode(353, 206); //light a61
            patha11.addNode(1, 206); //end
            paths.Add(patha11);

            // a11 - rechts voorsorteer over a62
            Path patha11r = new Path();
            patha11r.addNode(726, 1); //start
            patha11r.addNode(726, 103); //light a11
            patha11r.addNode(721, 195);
            patha11r.addNode(572, 227);
            patha11r.addNode(391, 227);
            patha11r.addNode(353, 227); //light a62
            patha11r.addNode(1, 227); //end
            paths.Add(patha11r);

            //  a12 - links voorsorteer over a63
            Path patha12 = new Path();
            patha12.addNode(750, 1); //start
            patha12.addNode(750, 103); //light a12
            patha12.addNode(750, 195);
            patha12.addNode(571, 252);
            patha12.addNode(389, 252);
            patha12.addNode(353, 2252); //light a63
            patha12.addNode(227, 266);
            patha12.addNode(206, 345);
            patha12.addNode(206, 606); //end
            paths.Add(patha12);

            //  a12 - rechts voorsorteer over a64
            Path patha12r = new Path();
            patha12r.addNode(750, 1); //start
            patha12r.addNode(750, 103); //light a12
            patha12r.addNode(750, 195);
            patha12r.addNode(571, 276);
            patha12r.addNode(389, 276);
            patha12r.addNode(353, 276); //light a64
            patha12r.addNode(240, 282);
            patha12r.addNode(232, 345);
            patha12r.addNode(232, 606); //end
            paths.Add(patha12r);

            //  a13 - rechts voorsorteer 
            Path patha13 = new Path();
            patha13.addNode(772, 1); //start
            patha13.addNode(772, 103); //light a13
            patha13.addNode(779, 353);
            patha13.addNode(896, 365);
            patha13.addNode(1058, 365); //end
            paths.Add(patha13);

            // b11 - bus over a63
            Path pathb11 = new Path();
            pathb11.addNode(793, 1); //start 
            pathb11.addNode(793, 103); //light b11 
            pathb11.addNode(793, 195);
            pathb11.addNode(676, 252);
            pathb11.addNode(393, 252);
            pathb11.addNode(353, 252); //light a63
            pathb11.addNode(227, 266);
            pathb11.addNode(206, 345);
            pathb11.addNode(206, 606); //end
            paths.Add(pathb11);

            // b12 - bus  
            Path pathb12 = new Path();
            pathb12.addNode(793, 1); //start
            pathb12.addNode(793, 103); //light b12
            pathb12.addNode(793, 195);
            pathb12.addNode(807, 388);
            pathb12.addNode(1075, 388); //end
            paths.Add(pathb12);

            // a21 
            Path patha21 = new Path();
            patha21.addNode(1075, 162); //start
            patha21.addNode(955, 162); //light a21
            patha21.addNode(866, 160);
            patha21.addNode(866, 105);
            patha21.addNode(866, 1); //end
            paths.Add(patha21);

            // a22   
            Path patha22 = new Path();
            patha22.addNode(1075, 183); //start
            patha22.addNode(955, 183); //light a22
            patha22.addNode(839, 105);
            patha22.addNode(839, 1); //end
            paths.Add(patha22);

            // a23 rechts voorsorteer over a61
            Path patha23 = new Path();
            patha23.addNode(1075, 207); //start
            patha23.addNode(955, 207); //light a23
            patha23.addNode(766, 207);
            patha23.addNode(581, 207);
            patha23.addNode(353, 207); //light a61
            patha23.addNode(1, 207); //end
            paths.Add(patha23);

            // a24 links voorsorteer over a64  
            Path patha24 = new Path();
            patha24.addNode(1075, 230); //start
            patha24.addNode(955, 230); //light a24
            patha24.addNode(765, 230);
            patha24.addNode(675, 253);
            patha24.addNode(579, 276);
            patha24.addNode(353, 276); //light a64
            patha24.addNode(240, 282);
            patha24.addNode(232, 345);
            patha24.addNode(232, 606); //end
            paths.Add(patha24);

            /*
            //   
            Path patha11 = new Path();
            patha11.addNode(734, 111); //start (732,1)
            patha11.addNode(729, 203); //light
            patha11.addNode(580, 215);
            patha11.addNode(399, 214);
            paths.Add(patha11);

            //   
            Path patha12 = new Path();
            patha12.addNode(758, 111); //start (,)
            patha12.addNode(757, 204); //light
            patha12.addNode(579, 283);
            patha12.addNode(397, 284);
            paths.Add(patha12);

            //   
            Path patha13 = new Path();
            patha13.addNode(780, 112); //start (,)
            patha13.addNode(787, 361); //light
            patha13.addNode(904, 373);
            patha13.addNode(1066, 373);
            paths.Add(patha13);

            //   
            Path pathb11 = new Path();
            pathb11.addNode(801, 112); //start (,)
            pathb11.addNode(802, 205); //light
            pathb11.addNode(684, 261);
            pathb11.addNode(401, 260);
            paths.Add(pathb11);

            //   
            Path pathb12 = new Path();
            pathb12.addNode(801, 112); //start (,)
            pathb12.addNode(802, 205); //light
            pathb12.addNode(815, 391);
            pathb12.addNode(1065, 396);
            paths.Add(pathb12);

            //   
            Path patha21 = new Path();
            patha21.addNode(963, 170); //start (,)
            patha21.addNode(874, 160); //light
            patha21.addNode(870, 105);
            patha21.addNode(870, 9);
            paths.Add(patha21);

            //   
            Path patha22 = new Path();
            patha22.addNode(966, 191); //start (,)
            patha22.addNode(858, 175); //light
            patha22.addNode(845, 105);
            patha22.addNode(847, 9);
            paths.Add(patha22);

            //   
            Path patha23 = new Path();
            patha23.addNode(966, 215); //start (,)
            patha23.addNode(766, 215); //light
            patha23.addNode(581, 216);
            patha23.addNode(399, 216);
            paths.Add(patha23);

            //   
            Path patha24 = new Path();
            patha24.addNode(968, 237); //start (,)
            patha24.addNode(767, 239); //light
            patha24.addNode(581, 238);
            patha24.addNode(398, 238);
            paths.Add(patha24);

            //   
            Path patha31 = new Path();
            patha31.addNode(732, 330); //start (,)
            patha31.addNode(837, 326); //light
            patha31.addNode(848, 258);
            patha31.addNode(848, 10);
            paths.Add(patha31);

            //   
            Path patha32 = new Path();
            patha32.addNode(736, 352); //start (,)
            patha32.addNode(845, 345); //light
            patha32.addNode(871, 280);
            patha32.addNode(871, 10);
            paths.Add(patha32);

            //   
            Path patha33 = new Path();
            patha33.addNode(735, 373); //start (,)
            patha33.addNode(841, 373); //light
            patha33.addNode(905, 373);
            patha33.addNode(1066, 373);
            paths.Add(patha33);

            //   
            Path patha34 = new Path();
            patha34.addNode(735, 396); //start (,)
            patha34.addNode(842, 396); //light
            patha34.addNode(904, 396);
            patha34.addNode(1066, 396);
            paths.Add(patha34);

            //   
            Path pathv11 = new Path();
            pathv11.addNode(666, 172); //start (,)
            pathv11.addNode(708, 134); //light
            pathv11.addNode(822, 134);
            pathv11.addNode(899, 134);
            paths.Add(pathv11);

            //   
            Path pathv14 = new Path();
            pathv14.addNode(899, 126); //start (,)
            pathv14.addNode(823, 126); //light
            pathv14.addNode(704, 126);
            pathv14.addNode(666, 165);
            paths.Add(pathv14);

            //   
            Path pathv21 = new Path();
            pathv21.addNode(912, 141); //start (,)
            pathv21.addNode(912, 256); //light
            pathv21.addNode(912, 357);
            pathv21.addNode(913, 429);
            paths.Add(pathv21);

            //   
            Path pathv24 = new Path();
            pathv24.addNode(920, 430); //start (,)
            pathv24.addNode(920, 357); //light
            pathv24.addNode(920, 256);
            pathv24.addNode(920, 141);
            paths.Add(pathv24);

            //   
            Path pathf11 = new Path();
            pathf11.addNode(666, 158); //start (,)
            pathf11.addNode(698, 121); //light
            pathf11.addNode(825, 121);
            pathf11.addNode(900, 121);
            paths.Add(pathf11);

            //   
            Path pathf12 = new Path();
            pathf12.addNode(900, 122); //start (,)
            pathf12.addNode(825, 122); //light
            pathf12.addNode(698, 122);
            pathf12.addNode(666, 159);
            paths.Add(pathf12);

            //   
            Path pathf21 = new Path();
            pathf21.addNode(925, 142); //start (,)
            pathf21.addNode(925, 258); //light
            pathf21.addNode(925, 359);
            pathf21.addNode(925, 430);
            paths.Add(pathf21);

            //   
            Path pathf22 = new Path();
            pathf22.addNode(924, 430); //start (,)
            pathf22.addNode(924, 359); //light
            pathf22.addNode(924, 258);
            pathf22.addNode(924, 142);
            paths.Add(pathf22);

            */

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
                        if(n.Reg != null)
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

            Bus bus = new Bus();

            bus.spawn(busPaths[random], busPaths[random].nodes[0].Left, busPaths[random].nodes[0].Top);

            traffic.Add(bus);

            this.Controls.Add(bus.x);
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
