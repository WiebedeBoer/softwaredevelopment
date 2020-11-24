using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simulator
{
    public enum BusLightSequence
    {
        Left,
        Right,
        AnyDir,
        Straight,
        Stop,
        Slowdown
    }

    class BusTrafficLight : RegularTrafficLight
    {
        protected const int delayedLightChange = 3000; // in milliseconds

        public PictureBox regTrafficLight;

        protected Image img;

        // Make sure trafficlight is facing correctly on the form
        protected string facingDirection;

        protected const int imageWidth = 35;
        protected const int imageHeight = 35;

        public BusTrafficLight()
        {
            regTrafficLight = new PictureBox();
        }

        public void createTrafficLight(int left, int top, String name, string direction)
        {
            regTrafficLight.Image = Properties.Resources.stop_bus_lights;

            regTrafficLight.BackColor = Color.Transparent;

            regTrafficLight.SizeMode = PictureBoxSizeMode.StretchImage;

            img = regTrafficLight.Image;


            switch (direction)
            {
                // up/left/right/top -> direction traffic is coming from
                case "up":
                    regTrafficLight.Size = new Size(imageWidth, imageHeight);
                    facingDirection = "up";
                    break;
                case "left":
                    regTrafficLight.Size = new Size(imageHeight, imageWidth);
                    img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    facingDirection = "left";
                    break;
                case "right":
                    regTrafficLight.Size = new Size(imageHeight, imageWidth);
                    img.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    facingDirection = "right";
                    break;
                case "down":
                    regTrafficLight.Size = new Size(imageWidth, imageHeight);
                    img.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    facingDirection = "down";
                    break;
            }


            regTrafficLight.Left = left;

            regTrafficLight.Top = top;

            this.name = name;
        }

        // Make sure picturebox is facing correct direction
        protected void correctlyFacingTrafficLight(Image img, string direction)
        {
            switch (direction)
            {
                // up/left/right/top -> direction traffic is coming from
                case "up":
                    regTrafficLight.Size = new Size(imageWidth, imageHeight);
                    facingDirection = "up";
                    break;
                case "left":
                    regTrafficLight.Size = new Size(imageHeight, imageWidth);
                    img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    facingDirection = "left";
                    break;
                case "right":
                    regTrafficLight.Size = new Size(imageHeight, imageWidth);
                    img.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    facingDirection = "right";
                    break;
                case "down":
                    regTrafficLight.Size = new Size(imageWidth, imageHeight);
                    img.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    facingDirection = "down";
                    break;
            }
        }
        
        /*
         * 
         * Light sequence for differing bus trafficlights
         * 
         * */

        public async void LeftLightSequence(int i)
        {
            switch (i)
            {
                case 0:
                    if (currentBusColor == BusLightSequence.Left)
                    {
                        regTrafficLight.Image = Properties.Resources.stoppossibly_bus_lights;
                        correctlyFacingTrafficLight(regTrafficLight.Image, facingDirection);
                        currentBusColor = BusLightSequence.Slowdown;
                        await Task.Delay(delayedLightChange);
                        regTrafficLight.Image = Properties.Resources.stop_bus_lights;
                        correctlyFacingTrafficLight(regTrafficLight.Image, facingDirection);
                        currentBusColor = BusLightSequence.Stop;
                    }
                    break;
                case 1:
                    if (currentBusColor == BusLightSequence.Stop)
                    {
                        regTrafficLight.Image = Properties.Resources.left_bus_light;
                        correctlyFacingTrafficLight(regTrafficLight.Image, facingDirection);
                        currentBusColor = BusLightSequence.Left;
                    }
                    break;
            }
        }

        public async void RightLightSequence(int i)
        {
            switch (i)
            {
                case 0:
                    if (currentBusColor == BusLightSequence.Right)
                    {
                        regTrafficLight.Image = Properties.Resources.stoppossibly_bus_lights;
                        correctlyFacingTrafficLight(regTrafficLight.Image, facingDirection);
                        currentBusColor = BusLightSequence.Slowdown;
                        await Task.Delay(delayedLightChange);
                        regTrafficLight.Image = Properties.Resources.stop_bus_lights;
                        correctlyFacingTrafficLight(regTrafficLight.Image, facingDirection);
                        currentBusColor = BusLightSequence.Stop;
                    }
                    break;
                case 1:
                    if (currentBusColor == BusLightSequence.Stop)
                    {
                        regTrafficLight.Image = Properties.Resources.right_bus_lights;
                        correctlyFacingTrafficLight(regTrafficLight.Image, facingDirection);
                        currentBusColor = BusLightSequence.Right;
                    }
                    break;
            }
        }

        public async void StraightLightSequence(int i)
        {
            switch (i)
            {
                case 0:
                    if (currentBusColor == BusLightSequence.Straight)
                    {
                        regTrafficLight.Image = Properties.Resources.stoppossibly_bus_lights;
                        correctlyFacingTrafficLight(regTrafficLight.Image, facingDirection);
                        currentBusColor = BusLightSequence.Slowdown;
                        await Task.Delay(delayedLightChange);
                        regTrafficLight.Image = Properties.Resources.stop_bus_lights;
                        correctlyFacingTrafficLight(regTrafficLight.Image, facingDirection);
                        currentBusColor = BusLightSequence.Stop;
                    }
                    break;
                case 1:
                    if (currentBusColor == BusLightSequence.Stop)
                    {
                        regTrafficLight.Image = Properties.Resources.straight_bus_light;
                        correctlyFacingTrafficLight(regTrafficLight.Image, facingDirection);
                        currentBusColor = BusLightSequence.Straight;
                    }
                    break;
            }
        }

    }
}
