using System;
using System.Collections;
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

            foreach(Control x in this.Controls)
            {
                if(x is PictureBox && x.Tag == "car")
                {
                    x.Top -= 10;
                }
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
            PictureBox car = new PictureBox();

            Random rnd = new Random();
            int whichCar = rnd.Next(1, 5);

            switch(whichCar)
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

            car.Tag = "car";

            car.Left = 340;

            car.Top = 580;

            this.Controls.Add(car);
        }
    }
}
