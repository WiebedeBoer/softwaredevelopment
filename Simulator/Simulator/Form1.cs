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

        public int spawnTimer = 0; // car
        public int randomTimeSpawned = 10; // initial spawn time
        public int busSpawnTimer = 0; // bus
        public int busRandomTimeSpawned = 10; // initial spawn time
        public int cyclistSpawnTimer = 0; // cyclist
        public int cyclistRandomTimeSpawned = 10; // initial spawn time
        public int pedSpawnTimer = 0; // pedestrian
        public int pedRandomTimeSpawned = 10;// initial spawn time

        // All traffic in the simulation
        List<Traffic> traffic = new List<Traffic>();

        // Path for cars
        private List<Path> paths = new List<Path>();

        // Path for buses
        private List<Path> busPaths = new List<Path>();

        // Paths for cyclists
        private List<Path> cyclistPaths = new List<Path>();

        // Paths for pedestrians
        private List<Path> pedPaths = new List<Path>();

        // trafficligths
        private List<RegularTrafficLight> lights = new List<RegularTrafficLight>();
        private List<BusTrafficLight> busLights = new List<BusTrafficLight>();

        // Connection to controller for receiving and sending JSON messages
        private BackGroundListener listener = new BackGroundListener();


        public Form1()
        {
            // Initialize form
            InitializeComponent();

            /*
             * RegularTrafficLight for cars
             * BusTrafficLight for buses
             * CyclistTraficLight for cyclists
             * PedTrafficligh for pedestrians
             * 
             *  A = Auto(car), F = Fietser(cyclist), B = Bus, P = Voetganger(pedestrian)
             *  
             *  1-1 = Trafficlight Identification Number
             *  
             *  down/left/right/up = direction which trafficlight is facing
             *  
             *  10,10 = location on form
             */
            RegularTrafficLight regA11 = createTrafficLight(726, 140, "A1-1", "down");
            lights.Add(regA11);
            RegularTrafficLight regA12 = createTrafficLight(750, 140, "A1-2", "down");
            lights.Add(regA12);
            RegularTrafficLight regA13 = createTrafficLight(772, 140, "A1-3", "down");
            lights.Add(regA13);

            BusTrafficLight busB11 = createBusTrafficLight(796, 140, "B1-1", "down");
            busLights.Add(busB11);
            BusTrafficLight busB12 = createBusTrafficLight(796, 180, "B1-2", "down");
            busLights.Add(busB12);

            RegularTrafficLight regF11 = createCyclistTrafficLight(700, 125, "F1-1", "left");
            lights.Add(regF11);
            RegularTrafficLight regF12 = createCyclistTrafficLight(880, 125, "F1-2", "right");
            lights.Add(regF12);

            PedTrafficLight regV11 = createPedTrafficLight(700, 115, "V1-1", "left");
            lights.Add(regV11);
            PedTrafficLight regV12 = createPedTrafficLight(817, 115, "V1-2", "left");
            lights.Add(regV12);
            PedTrafficLight regV13 = createPedTrafficLight(817, 125, "V1-3", "right");
            lights.Add(regV13);
            PedTrafficLight regV14 = createPedTrafficLight(880, 115, "V1-4", "right");
            lights.Add(regV14);

            RegularTrafficLight regA21 = createTrafficLight(850, 162, "A2-1", "right");
            lights.Add(regA21);
            RegularTrafficLight regA22 = createTrafficLight(850, 183, "A2-2", "right");
            lights.Add(regA22);
            RegularTrafficLight regA23 = createTrafficLight(850, 207, "A2-3", "right");
            lights.Add(regA23);
            RegularTrafficLight regA24 = createTrafficLight(850, 230, "A2-4", "right");
            lights.Add(regA24);

            RegularTrafficLight regF21 = createCyclistTrafficLight(910, 145, "F2-1", "down");
            lights.Add(regF21);
            RegularTrafficLight regF22 = createCyclistTrafficLight(910, 400, "F2-2", "up");
            lights.Add(regF22);

            PedTrafficLight regV21 = createPedTrafficLight(920, 145, "V2-1", "down");
            lights.Add(regV21);
            PedTrafficLight regV22 = createPedTrafficLight(920, 250, "V2-2", "up");
            lights.Add(regV22);
            PedTrafficLight regV23 = createPedTrafficLight(920, 340, "V2-3", "down");
            lights.Add(regV23);
            PedTrafficLight regV24 = createPedTrafficLight(920, 400, "V2-4", "up");
            lights.Add(regV24);

            RegularTrafficLight regA31 = createTrafficLight(737, 320, "A3-1", "left");
            lights.Add(regA31);
            RegularTrafficLight regA32 = createTrafficLight(737, 343, "A3-2", "left");
            lights.Add(regA32);
            RegularTrafficLight regA33 = createTrafficLight(737, 365, "A3-3", "left");
            lights.Add(regA33);
            RegularTrafficLight regA34 = createTrafficLight(737, 387, "A3-4", "left");
            lights.Add(regA34);

            RegularTrafficLight regA41 = createTrafficLight(276, 421, "A4-1", "up");
            lights.Add(regA41);
            RegularTrafficLight regA42 = createTrafficLight(298, 421, "A4-2", "up");
            lights.Add(regA42);
            RegularTrafficLight regA43 = createTrafficLight(320, 421, "A4-3", "up");
            lights.Add(regA43);
            RegularTrafficLight regA44 = createTrafficLight(342, 421, "A4-4", "up");
            lights.Add(regA44);

            BusTrafficLight busB41 = createBusTrafficLight(364, 421, "B4-1", "down");
            busLights.Add(busB41);

            RegularTrafficLight regF41 = createCyclistTrafficLight(190, 475, "F4-1", "left");
            lights.Add(regF41);
            RegularTrafficLight regF42 = createCyclistTrafficLight(380, 475, "F4-2", "right");
            lights.Add(regF42);

            PedTrafficLight regV41 = createPedTrafficLight(190, 485, "V4-1", "left");
            lights.Add(regV41);
            PedTrafficLight regV42 = createPedTrafficLight(250, 475, "V4-2", "left");
            lights.Add(regV42);
            PedTrafficLight regV43 = createPedTrafficLight(250, 485, "V4-3", "right");
            lights.Add(regV43);
            PedTrafficLight regV44 = createPedTrafficLight(380, 485, "V4-4", "right");
            lights.Add(regV44);

            RegularTrafficLight regA51 = createTrafficLight(178, 365, "A5-1", "left");
            lights.Add(regA51);
            RegularTrafficLight regA52 = createTrafficLight(178, 387, "A5-2", "left");
            lights.Add(regA52);
            RegularTrafficLight regA53 = createTrafficLight(178, 409, "A5-3", "left");
            lights.Add(regA53);
            RegularTrafficLight regA54 = createTrafficLight(178, 431, "A5-4", "left");
            lights.Add(regA54);

            RegularTrafficLight regF51 = createCyclistTrafficLight(165, 186, "F5-1", "down");
            lights.Add(regF51);
            RegularTrafficLight regF52 = createCyclistTrafficLight(165, 450, "F5-2", "up");
            lights.Add(regF52);

            PedTrafficLight regV51 = createPedTrafficLight(155, 186, "V5-1", "down");
            lights.Add(regV51);
            PedTrafficLight regV52 = createPedTrafficLight(155, 250, "V5-2", "up");
            lights.Add(regV52);
            PedTrafficLight regV53 = createPedTrafficLight(155, 340, "V5-3", "down");
            lights.Add(regV53);
            PedTrafficLight regV54 = createPedTrafficLight(155, 450, "V5-4", "up");
            lights.Add(regV54);

            RegularTrafficLight regA61 = createTrafficLight(320, 206, "A6-1", "right");
            lights.Add(regA61);
            RegularTrafficLight regA62 = createTrafficLight(320, 227, "A6-2", "right");
            lights.Add(regA62);
            RegularTrafficLight regA63 = createTrafficLight(320, 252, "A6-3", "right");
            lights.Add(regA63);
            RegularTrafficLight regA64 = createTrafficLight(320, 276, "A6-4", "right");
            lights.Add(regA64);
            ///////////////////////////////////////


            /*
             * 
             * Regular car paths
             * Nodes represent location on form
             * 
             * */

            // Trafficlight A44 -> Secondary Trafficlight A34
            Path path = new Path();
            path.addNode(342, 580);
            path.addNode(342, 500, regA44);
            path.addNode(342, 411);
            path.addNode(383, 388);
            path.addNode(700, 388, regA34);
            path.addNode(1083, 388);
            paths.Add(path);

            // Trafficlight A44 -> Secondary Trafficlight A33
            Path path2 = new Path();
            path2.addNode(342, 580);
            path2.addNode(342, 500, regA44);
            path2.addNode(342, 411);
            path2.addNode(383, 366);
            path2.addNode(700, 366, regA33);
            path2.addNode(1083, 366);
            paths.Add(path2);

            // Trafficlight A43 -> Secondary Trafficlight A32
            Path path3 = new Path();
            path3.addNode(320, 580);
            path3.addNode(320, 500, regA43);
            path3.addNode(332, 366);
            path3.addNode(369, 342);
            path3.addNode(700, 342, regA32);
            path3.addNode(815, 342);
            path3.addNode(855, 320);
            path3.addNode(860, 0);
            paths.Add(path3);

            // Trafficlight A43 -> Secondary Trafficlight A31
            Path path4 = new Path();
            path4.addNode(320, 580);
            path4.addNode(320, 500, regA43);
            path4.addNode(368, 322);
            path4.addNode(369, 318);
            path4.addNode(700, 318, regA31);
            path4.addNode(811, 318);
            path4.addNode(835, 300);
            path4.addNode(836, 0);
            paths.Add(path4);

            // Trafficlight A42
            Path path5 = new Path();
            path5.addNode(296, 580);
            path5.addNode(296, 500, regA42);
            path5.addNode(296, 248);
            path5.addNode(278, 210);
            path5.addNode(0, 206);
            paths.Add(path5);

            // Trafficlight A41
            Path path6 = new Path();
            path6.addNode(274, 580);
            path6.addNode(274, 500, regA41);
            path6.addNode(274, 257);
            path6.addNode(240, 230);
            path6.addNode(0, 227);
            paths.Add(path6);

            // Trafficlight A54
            Path path7 = new Path();
            path7.addNode(-50, 433);
            path7.addNode(120, 433, regA54);
            path7.addNode(170, 433);
            path7.addNode(208, 433);
            path7.addNode(206, 610);
            paths.Add(path7);
            Path path8 = new Path();

            // Trafficlight A53
            path8.addNode(-50, 409);
            path8.addNode(120, 409, regA53);
            path8.addNode(170, 409);
            path8.addNode(208, 409);
            path8.addNode(224, 443);
            path8.addNode(230, 610);
            paths.Add(path8);

            // Trafficlight A52 -> Secondary Trafficlight A34
            Path path9 = new Path();
            path9.addNode(-50, 385);
            path9.addNode(120, 385, regA52);
            path9.addNode(170, 385);
            path9.addNode(700, 385, regA34);
            path9.addNode(1083, 385);
            paths.Add(path9);

            // Trafficlight A52 -> Secondary Trafficlight A33
            Path path10 = new Path();
            path10.addNode(-50, 385);
            path10.addNode(120, 385, regA52);
            path10.addNode(170, 385);
            path10.addNode(310, 385);
            path10.addNode(400, 363);
            path10.addNode(700, 363, regA33);
            path10.addNode(1083, 363);
            paths.Add(path10);

            // Trafficlight A51 -> Secondary Trafficlight A32
            Path path11 = new Path();
            path11.addNode(-50, 363);
            path11.addNode(120, 363, regA51);
            path11.addNode(170, 363);
            path11.addNode(272, 363);
            path11.addNode(401, 341);
            path11.addNode(700, 341, regA32);
            path11.addNode(837, 341);
            path11.addNode(862, 299);
            path11.addNode(862, 0);

            // Trafficlight A51 -> Secondary Trafficlight A31
            Path path12 = new Path();
            path12.addNode(-50, 363);
            path12.addNode(120, 363, regA51);
            path12.addNode(170, 363);
            path12.addNode(272, 363);
            path12.addNode(401, 318);
            path12.addNode(700, 318, regA31);
            path12.addNode(828, 312);
            path12.addNode(839, 299);
            path12.addNode(839, 0);

            // Trafficlight A11 -> Secondary Trafficlight A61
            paths.Add(path12);
            Path patha11 = new Path();
            patha11.addNode(726, -50);
            patha11.addNode(726, 80, regA11);
            patha11.addNode(721, 195);
            patha11.addNode(572, 206);
            patha11.addNode(391, 206, regA61);
            patha11.addNode(353, 206);
            patha11.addNode(1, 206); 
            paths.Add(patha11);

            // Trafficlight A11 -> Secondary Trafficlight A62
            Path patha11r = new Path();
            patha11r.addNode(726, -50);
            patha11r.addNode(726, 80, regA11);
            patha11r.addNode(721, 195);
            patha11r.addNode(572, 227);
            patha11r.addNode(391, 227, regA62);
            patha11r.addNode(353, 227);
            patha11r.addNode(1, 227);
            paths.Add(patha11r);

            // Trafficlight A12 -> Secondary Trafficlight A64
            Path patha12 = new Path();
            patha12.addNode(750, -50); 
            patha12.addNode(750, 80, regA12);
            patha12.addNode(750, 195);
            patha12.addNode(750, 252);
            patha12.addNode(571, 252);
            patha12.addNode(391, 252, regA64);
            patha12.addNode(353, 252);
            patha12.addNode(227, 266);
            patha12.addNode(206, 345);
            patha12.addNode(206, 606);
            paths.Add(patha12);

            // Trafficlight A12 -> Secondary Trafficlight A64
            Path patha12r = new Path();
            patha12r.addNode(750, -50); 
            patha12r.addNode(750, 80, regA12); 
            patha12r.addNode(750, 195);
            patha12r.addNode(750, 276);
            patha12r.addNode(571, 276);
            patha12r.addNode(391, 276, regA64);
            patha12r.addNode(353, 276); 
            patha12r.addNode(240, 282);
            patha12r.addNode(232, 345);
            patha12r.addNode(232, 606); 
            paths.Add(patha12r);

            // Trafficlight A13
            Path patha13 = new Path();
            patha13.addNode(772, -50); 
            patha13.addNode(772, 80, regA13); 
            patha13.addNode(772, 353);
            patha13.addNode(772, 388);
            patha13.addNode(1075, 388); 
            paths.Add(patha13);

            // Trafficlight A21
            Path patha21 = new Path();
            patha21.addNode(1075, 162);
            patha21.addNode(930, 162, regA21);
            patha21.addNode(866, 160);
            patha21.addNode(866, 105);
            patha21.addNode(866, 1); 
            paths.Add(patha21);

            // Trafficlight A21
            Path patha22 = new Path();
            patha22.addNode(1075, 183);
            patha22.addNode(930, 183, regA21);
            patha22.addNode(839, 105);
            patha22.addNode(839, 1); 
            paths.Add(patha22);

            // Trafficlight A23 -> Secondary Trafficlight A61
            Path patha23 = new Path();
            patha23.addNode(1075, 207);
            patha23.addNode(930, 207, regA23);
            patha23.addNode(766, 207);
            patha23.addNode(581, 207);
            patha23.addNode(391, 207, regA61);
            patha23.addNode(353, 207);
            patha23.addNode(1, 207); 
            paths.Add(patha23);

            // Trafficlight A24 -> Secondary Trafficlight A64
            Path patha24 = new Path();
            patha24.addNode(1075, 230);
            patha24.addNode(930, 230, regA24);
            patha24.addNode(765, 230);
            patha24.addNode(675, 253);
            patha24.addNode(579, 276);
            patha24.addNode(391, 276, regA64);
            patha24.addNode(240, 282);
            patha24.addNode(232, 345);
            patha24.addNode(232, 606);
            paths.Add(patha24);

            /*
             * 
             * Bus paths
             * Nodes represent location on form
             * 
             * */

            // Trafficlight B41 -> Secondary Trafficlight A32
            Path buspathb41 = new Path();
            buspathb41.addNode(364, 592);
            buspathb41.addNode(364, 500, busB41);
            buspathb41.addNode(364, 460); 
            buspathb41.addNode(342, 433);
            buspathb41.addNode(342, 342);
            buspathb41.addNode(700, 342, regA32);
            buspathb41.addNode(815, 342);
            buspathb41.addNode(855, 320);
            buspathb41.addNode(860, 0);
            busPaths.Add(buspathb41);

            // Trafficlight B11 -> Secondary Trafficlight A63
            Path pathb11 = new Path();
            pathb11.addNode(793, -50); 
            pathb11.addNode(793, 55, busB11);
            pathb11.addNode(793, 103); 
            pathb11.addNode(793, 195);
            pathb11.addNode(676, 252);
            pathb11.addNode(393, 252, regA63);
            pathb11.addNode(353, 252); 
            pathb11.addNode(227, 266);
            pathb11.addNode(206, 345);
            pathb11.addNode(206, 606);
            busPaths.Add(pathb11);

            // Trafficlight B12
            Path pathb12 = new Path();
            pathb12.addNode(793, -50); 
            pathb12.addNode(793, 55, busB12);
            pathb12.addNode(793, 103);
            pathb12.addNode(793, 195);
            pathb12.addNode(793, 365);
            pathb12.addNode(1075, 365);
            busPaths.Add(pathb12);
            Path pathb51 = new Path();

            // Trafficlight A51 -> Secondary Trafficlight A32
            pathb51.addNode(1, 363);
            pathb51.addNode(90, 363, regA51);
            pathb51.addNode(170, 363);
            pathb51.addNode(272, 363);
            pathb51.addNode(401, 341);
            pathb51.addNode(670, 341, regA32);
            pathb51.addNode(837, 341);
            pathb51.addNode(862, 299);
            pathb51.addNode(862, 0);
            busPaths.Add(pathb51);

            // BusTrafficlight A21
            Path pathb21 = new Path();
            pathb21.addNode(1075, 162);
            pathb21.addNode(930, 162, regA21);
            pathb21.addNode(866, 160);
            pathb21.addNode(866, 105);
            pathb21.addNode(866, 1); 
            busPaths.Add(pathb21);

            /*
             * 
             * Cyclist paths
             * Nodes represent location on form
             * 
             * */

            // Trafficlight F11
            Path pathf11 = new Path();
            pathf11.addNode(1, 167); 
            pathf11.addNode(672, 167);
            pathf11.addNode(694, 128, regF11);
            pathf11.addNode(716, 128); 
            pathf11.addNode(825, 128);
            pathf11.addNode(1075, 128); 
            cyclistPaths.Add(pathf11);

            // Trafficlight F52
            Path pathf52 = new Path();
            pathf52.addNode(1, 475); 
            pathf52.addNode(170, 475);
            pathf52.addNode(170, 465, regF52);
            pathf52.addNode(170, 265);
            pathf52.addNode(170, 168);
            pathf52.addNode(1, 168);
            cyclistPaths.Add(pathf52);

            // Trafficlight F51
            Path pathf51 = new Path();
            pathf51.addNode(1, 168);
            pathf51.addNode(170, 168);
            pathf51.addNode(170, 174, regF51);
            pathf51.addNode(170, 265);
            pathf51.addNode(170, 465);
            pathf51.addNode(170, 475);
            pathf51.addNode(1, 475);
            cyclistPaths.Add(pathf51);

            // Trafficlight F52 -> Secondary Trafficlight F11
            Path pathf52alt = new Path();
            pathf52alt.addNode(1, 475); 
            pathf52alt.addNode(170, 475);
            pathf52alt.addNode(170, 465, regF52);
            pathf52alt.addNode(170, 265);
            pathf52alt.addNode(170, 167);
            pathf52alt.addNode(672, 167);
            pathf52alt.addNode(694, 128, regF11);
            pathf52alt.addNode(716, 128); 
            pathf52alt.addNode(825, 128);
            pathf52alt.addNode(1075, 128); 
            cyclistPaths.Add(pathf52alt);

            // Trafficlight F41
            Path pathf41 = new Path();
            pathf41.addNode(1, 475); 
            pathf41.addNode(178, 475, regF41);
            pathf41.addNode(403, 475);
            pathf41.addNode(430, 437);
            pathf41.addNode(1083, 437);
            cyclistPaths.Add(pathf41);

            // Trafficlight F41 -> Secondary Trafficlight F22
            Path pathf22 = new Path();
            pathf22.addNode(1, 475);
            pathf22.addNode(178, 475, regF41);
            pathf22.addNode(403, 475);
            pathf22.addNode(430, 437);
            pathf22.addNode(911, 437);
            pathf22.addNode(911, 428, regF22);
            pathf22.addNode(911, 0);
            cyclistPaths.Add(pathf22);

            // Trafficlight F41 -> Secondary Trafficlight F22
            Path pathf22alt = new Path();
            pathf22alt.addNode(1, 475);
            pathf22alt.addNode(178, 475, regF41);
            pathf22alt.addNode(403, 475);
            pathf22alt.addNode(430, 437);
            pathf22alt.addNode(911, 437);
            pathf22alt.addNode(911, 428, regF22);
            pathf22alt.addNode(911, 128);
            pathf22alt.addNode(1075, 128);
            cyclistPaths.Add(pathf22);

            // Trafficlight F12
            Path pathf12 = new Path();
            pathf12.addNode(1075, 128); 
            pathf12.addNode(900, 128, regF12);
            pathf12.addNode(882, 128); 
            pathf12.addNode(698, 128);
            pathf12.addNode(666, 166);
            pathf12.addNode(1, 166);
            cyclistPaths.Add(pathf12);

            // Trafficlight F21
            Path pathf21 = new Path();
            pathf21.addNode(912, 1);
            pathf21.addNode(912, 137, regF21);
            pathf21.addNode(912, 258);
            pathf21.addNode(912, 359);
            pathf21.addNode(912, 436);
            pathf21.addNode(1075, 436);
            cyclistPaths.Add(pathf21);

            // Trafficlight F22
            Path pathf22right = new Path();
            pathf22right.addNode(1075, 438);
            pathf22right.addNode(914, 438);
            pathf22right.addNode(914, 428, regF22);
            pathf22right.addNode(914, 258);
            pathf22right.addNode(914, 142);
            pathf22right.addNode(914, 1);
            cyclistPaths.Add(pathf22right);

            // Trafficlight F42
            Path pathf42 = new Path();
            pathf42.addNode(1082, 437);
            pathf42.addNode(428, 437);
            pathf42.addNode(411, 475);
            pathf42.addNode(400, 475, regF42);
            pathf42.addNode(1, 475);
            cyclistPaths.Add(pathf42);

            /*
             * 
             * Cyclist paths
             * Nodes represent location on form
             * 
             * */

            // Trafficlight V11 -> Secondary Trafficlight V13
            Path pathv11 = new Path();
            pathv11.addNode(1, 155);
            pathv11.addNode(658, 155);
            pathv11.addNode(700, 115, regV11);
            pathv11.addNode(814, 115, regV13);
            pathv11.addNode(891, 115);
            pathv11.addNode(1075, 115);
            pedPaths.Add(pathv11);

            // Trafficlight V21 -> Secondary Trafficlight V23
            Path pathv21 = new Path();
            pathv21.addNode(925, 1);
            pathv21.addNode(925, 137, regV21);
            pathv21.addNode(925, 258);
            pathv21.addNode(925, 348, regV23);
            pathv21.addNode(925, 448);
            pathv21.addNode(1075, 448);
            pedPaths.Add(pathv21);

            // Trafficlight V14 -> Secondary Trafficlight V12
            Path pathv14 = new Path();
            pathv14.addNode(1075, 115);
            pathv14.addNode(900, 115, regV14);
            pathv14.addNode(882, 115);
            pathv14.addNode(820, 115, regV12);
            pathv14.addNode(698, 115);
            pathv14.addNode(666, 155);
            pathv14.addNode(1, 155);
            pedPaths.Add(pathv14);

            // Trafficlight V24 -> Secondary Trafficlight V22
            Path pathv24 = new Path();
            pathv24.addNode(1075, 448);
            pathv24.addNode(924, 448);
            pathv24.addNode(924, 428, regV24);
            pathv24.addNode(924, 258, regV22);
            pathv24.addNode(924, 142);
            pathv24.addNode(924, 1);
            pedPaths.Add(pathv24);

            // Trafficlight V44 -> Secondary Trafficlight V42
            Path pathv44 = new Path();
            pathv44.addNode(1082, 448);
            pathv44.addNode(428, 448);
            pathv44.addNode(420, 482);
            pathv44.addNode(400, 488, regV44);
            pathv44.addNode(252, 488, regV42);
            pathv44.addNode(1, 488);
            pedPaths.Add(pathv44);

            // Trafficlight V41 -> Secondary Trafficlight V43
            Path pathv41 = new Path();
            pathv41.addNode(1, 488); 
            pathv41.addNode(178, 488, regV41);
            pathv41.addNode(266, 488, regV43);
            pathv41.addNode(403, 488);
            pathv41.addNode(420, 482);
            pathv41.addNode(430, 448);
            pathv41.addNode(1083, 448);
            pedPaths.Add(pathv41);

            // Trafficlight V54 -> Secondary Trafficlight V52
            Path pathv54 = new Path();
            pathv54.addNode(1, 488); 
            pathv54.addNode(155, 488);
            pathv54.addNode(155, 465, regV54);
            pathv54.addNode(155, 265, regV52);
            pathv54.addNode(155, 155);
            pathv54.addNode(1, 155);
            pedPaths.Add(pathv54);

            // Trafficlight V51 -> Secondary Trafficlight V53
            Path pathv51 = new Path();
            pathv51.addNode(1, 155);
            pathv51.addNode(155, 155);
            pathv51.addNode(155, 182, regV51);
            pathv51.addNode(155, 340, regV53);
            pathv51.addNode(155, 465);
            pathv51.addNode(155, 488);
            pathv51.addNode(1, 488);
            pedPaths.Add(pathv51);

            // Open the BackGroundListener on another thread so the main thread can keep simulating
            Thread listen = new Thread(listener.Connect);
            listen.Start();
        }

        // Updating states of simulation
        private void timer1_Tick_1(object sender, EventArgs e)
        {

            JObject JSONtraffic = new JObject();

            foreach (RegularTrafficLight reg in lights)
            {
                JSONtraffic[reg.name] = reg.carInFront ? 1 : 0;
            }

            foreach (BusTrafficLight reg in busLights)
            {
                JSONtraffic[reg.name] = reg.carInFront ? 1 : 0;
            }

            listener.jsonToSend = JSONtraffic;

            JObject received = listener.jsonToReceive;

            if (received != null)
            {
                foreach (RegularTrafficLight regs in lights)
                {
                    int seq = (int)received[regs.name];
                    regs.LightSequence(seq);
                }

                foreach (BusTrafficLight bussen in busLights)
                {
                    int seq = (int)received[bussen.name];
                    if (bussen.name == "B4-1")
                        bussen.RightLightSequence(seq);
                    if (bussen.name == "B1-1")
                        bussen.StraightLightSequence(seq);
                    if (bussen.name == "B1-2")
                        bussen.LeftLightSequence(seq);
                }
            }

            spawnTimer += 1;

            // Spawn a car every 15 to 30 seconds
            if(spawnTimer == randomTimeSpawned)
            {
                spawnRandomCar();

                Random rnd = new Random();
                this.randomTimeSpawned = rnd.Next(15, 30);

                spawnTimer = 0;
            }

            busSpawnTimer += 1;

            // Spawn a bus every 100 to 150 seconds
            if (busSpawnTimer == busRandomTimeSpawned)
            {
                spawnRandomBus();

                Random rnd = new Random();
                this.busRandomTimeSpawned = rnd.Next(100, 150);

                busSpawnTimer = 0;
            }

            cyclistSpawnTimer += 1;

            // Spawn a cyclist every 20 to 70 seconds
            if (cyclistSpawnTimer == cyclistRandomTimeSpawned)
            {
                spawnRandomCyclist();

                Random rnd = new Random();
                this.cyclistRandomTimeSpawned = rnd.Next(20, 70);

                cyclistSpawnTimer = 0;
            }

            pedSpawnTimer += 1;

            // Spawn a pedestrian every 20 to 70 seconds
            if (pedSpawnTimer == pedRandomTimeSpawned)
            {
                spawnRandomPedestrian();

                Random rnd = new Random();
                this.pedRandomTimeSpawned = rnd.Next(20, 70);

                pedSpawnTimer = 0;
            }
            
            // For every traffic object on form, move it unless brake equals true
            foreach (Traffic x in this.traffic)
            {

                // Check if car is detected in front, so they dont collide
                bool brake = x.collisionDetection(this.traffic);
                
                // Different speeds per traffic type
                if(x is Pedestrian)
                    x.move(4, brake);
                else if (x is Cyclist)
                    x.move(10, brake);
                else
                    x.move(16, brake);
            }

            // Delete traffic object when it toBeDeleted equals true
            foreach (Traffic i in traffic.Reverse<Traffic>())
            {
                if (i.toBeDeleted == true)
                {
                    traffic.Remove(i);
                    this.Controls.Remove(i.x);
                }
            }
        }

        // Spawn a random bus anywhere
        private void spawnRandomBus()
        {
            Random rnd = new Random();
            int random = rnd.Next(busPaths.Count);

            int amount = 0;
            foreach (Traffic x in this.traffic)
            {
                if(x is Bus)
                    amount++;
            }

            // When pool of buses reaches 4, stop spawning
            if (busPaths.Count > 0 && amount < 4)
            {
                Bus bus = new Bus();
                bus.spawn(busPaths[random], busPaths[random].nodes[0].Left, busPaths[random].nodes[0].Top);

                traffic.Add(bus);

                this.Controls.Add(bus.x);
            }

            
        }

        // Spawn a random pedestrian anywhere
        private void spawnRandomPedestrian()
        {
            Random rnd = new Random();
            int random = rnd.Next(pedPaths.Count);

            int amount = 0;
            foreach (Traffic x in this.traffic)
            {
                if (x is Pedestrian)
                    amount++;
            }

            // When pool of pedestrians reaches 25, stop spawning
            if (pedPaths.Count > 0 && amount < 25)
            {
                Pedestrian ped = new Pedestrian();
                ped.spawn(pedPaths[random], pedPaths[random].nodes[0].Left, pedPaths[random].nodes[0].Top);

                traffic.Add(ped);

                this.Controls.Add(ped.x);
            }


        }

        // Spawn a random cyclist anywhere
        private void spawnRandomCyclist()
        {
            Random rnd = new Random();
            int random = rnd.Next(cyclistPaths.Count);

            int amount = 0;
            foreach (Traffic x in this.traffic)
            {
                if (x is Cyclist)
                    amount++;
            }

            // When pool of cyclists reaches 20, stop spawning
            if (cyclistPaths.Count > 0 && amount < 20)
            {
                Cyclist cyclist = new Cyclist();
                cyclist.spawn(cyclistPaths[random], cyclistPaths[random].nodes[0].Left, cyclistPaths[random].nodes[0].Top);

                traffic.Add(cyclist);

                this.Controls.Add(cyclist.x);
            }


        }

        // Spawn a random car anywhere
        private void spawnRandomCar()
        {
            Random rnd = new Random();
            int random = rnd.Next(paths.Count);

            int amount = 0;
            foreach (Traffic x in this.traffic)
            {
                if (x is Car)
                    amount++;
            }

            // When pool of buses reaches 50, stop spawning
            if (paths.Count > 0 && amount < 50)
            {
                Car car = new Car();

                car.spawn(paths[random], paths[random].nodes[0].Left, paths[random].nodes[0].Top);

                traffic.Add(car);

                this.Controls.Add(car.x);
            }
        }

        /*
         * 
         * Create trafficlight
         * left -> horizontal positioning
         * right -> vertical positioning
         * name -> ID of trafficlight
         * direction -> Which direction the trafficlight should face
         * 
         * */
        private RegularTrafficLight createTrafficLight(int left, int top, string name, string direction)
        {
            RegularTrafficLight reg = new RegularTrafficLight();

            reg.createTrafficLight(left, top, name, direction);

            this.Controls.Add(reg.regTrafficLight);

            return reg;
        }

        private RegularTrafficLight createCyclistTrafficLight(int left, int top, string name, string direction)
        {
            CyclistTrafficLight reg = new CyclistTrafficLight();

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

        private PedTrafficLight createPedTrafficLight(int left, int top, string name, string direction)
        {
            PedTrafficLight reg = new PedTrafficLight();

            reg.createTrafficLight(left, top, name, direction);

            this.Controls.Add(reg.regTrafficLight);

            return reg;
        }
    }
}
