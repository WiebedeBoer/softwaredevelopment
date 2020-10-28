using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Design;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simulator
{
    public enum RegLightSequence
    {
        Green,
        Yellow,
        Red
    }

    public class RegularTrafficLight
    {
        public String name = null;

        const int delayedLightChange = 500; // in milliseconds
        public RegLightSequence currentColor = RegLightSequence.Red;

        public PictureBox regTrafficLight;

        public RegularTrafficLight()
        {
            regTrafficLight = new PictureBox();
        }

        public void createTrafficLight(int left, int top, String name, string direction)
        {
            regTrafficLight.Image = Properties.Resources.red_light;

            regTrafficLight.BackColor = Color.Transparent;

            regTrafficLight.SizeMode = PictureBoxSizeMode.StretchImage;

            Image img = regTrafficLight.Image;

            
            switch (direction)
            {
                // up/left/right/top -> direction traffic is coming from
                case "up":
                    regTrafficLight.Size = new Size(18, 53);
                    break;
                case "left":
                    regTrafficLight.Size = new Size(53, 18);
                    img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    break;
                case "right":
                    img.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    break;
                case "down":
                    //regTrafficLight.Size = new Size(18, 53);
                    break;
            }
            

            regTrafficLight.Left = left;

            regTrafficLight.Top = top;

            this.name = name;
        }

        public async void LightSequence(int i)
        {
            switch(i)
            {
                case 0:
                    if (currentColor == RegLightSequence.Green)
                    {
                        regTrafficLight.Image = Properties.Resources.yellow_light;
                        currentColor = RegLightSequence.Yellow;
                        await Task.Delay(3000);
                        regTrafficLight.Image = Properties.Resources.red_light;
                        currentColor = RegLightSequence.Red;
                    }
                    break;
                case 1:
                    if (currentColor == RegLightSequence.Red)
                    {
                        regTrafficLight.Image = Properties.Resources.green_light;
                        currentColor = RegLightSequence.Green;
                    }
                    break;
            }
        }

        // Switch trafficlight to yellow, after a delay switch to green
        public void SwitchLight()
        {
            if(currentColor == RegLightSequence.Red)
            {
                regTrafficLight.Image = Properties.Resources.green_light;
                currentColor = RegLightSequence.Green;
            } else
            {
                regTrafficLight.Image = Properties.Resources.red_light;
                currentColor = RegLightSequence.Red;
            }
            
        }


    }
}
