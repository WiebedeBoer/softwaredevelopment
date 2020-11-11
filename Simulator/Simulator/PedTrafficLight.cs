using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simulator
{
    class PedTrafficLight : RegularTrafficLight
    {
        public void createTrafficLight(int left, int top, String name, string direction)
        {
            this.imageWidth = 10;
            this.imageHeight = 20;

            regTrafficLight.Image = Properties.Resources.red_pedestrian;

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

        public async void LightSequence(int i)
        {
            switch (i)
            {
                case 0:
                    if (currentColor == RegLightSequence.Green)
                    {
                        regTrafficLight.Image = Properties.Resources.red_pedestrian;
                        correctlyFacingTrafficLight(regTrafficLight.Image, facingDirection);
                        currentColor = RegLightSequence.Red;
                    }
                    break;
                case 1:
                    if (currentColor == RegLightSequence.Red)
                    {
                        regTrafficLight.Image = Properties.Resources.green_pedestrian;
                        correctlyFacingTrafficLight(regTrafficLight.Image, facingDirection);
                        currentColor = RegLightSequence.Green;
                    }
                    break;
            }
        }
    }
}
