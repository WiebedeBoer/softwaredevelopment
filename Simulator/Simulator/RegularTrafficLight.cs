using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Design;
using System.Text;
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
        const int delayedLightChange = 500; // in milliseconds
        RegLightSequence currentColor = RegLightSequence.Red;

        public PictureBox regTrafficLight;

        public RegularTrafficLight()
        {
            regTrafficLight = new PictureBox();
        }

        public void createTrafficLight(int left, int top)
        {
            regTrafficLight.Image = Properties.Resources.red_light;

            regTrafficLight.BackColor = Color.Transparent;

            regTrafficLight.SizeMode = PictureBoxSizeMode.StretchImage;

            regTrafficLight.Size = new Size(18, 53);

            regTrafficLight.Left = left;

            regTrafficLight.Top = top;
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
