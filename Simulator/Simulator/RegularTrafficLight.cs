﻿using Moq.Protected;
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

        protected const int delayedLightChange = 3000; // in milliseconds
        public RegLightSequence currentColor = RegLightSequence.Red;

        public BusLightSequence currentBusColor = BusLightSequence.Stop;

        public PictureBox regTrafficLight;

        protected Image img;

        protected string facingDirection;

        public bool carInFront = false;

        protected int imageWidth = 18;
        protected int imageHeight = 53;

        public RegularTrafficLight()
        {
            regTrafficLight = new PictureBox();
        }

        public void createTrafficLight(int left, int top, String name, string direction)
        {
            regTrafficLight.Image = Properties.Resources.red_light;

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
         * Lightsequence of a regular traffic light.
         * Green -> Yellow -> Red
         * Red -> Green
         * 
         * With 3 second delay
         */
        public async void LightSequence(int i)
        {
            switch (i)
            {
                case 0:
                    if (currentColor == RegLightSequence.Green)
                    {
                        regTrafficLight.Image = Properties.Resources.yellow_light;
                        correctlyFacingTrafficLight(regTrafficLight.Image, facingDirection);
                        currentColor = RegLightSequence.Yellow;
                        await Task.Delay(delayedLightChange);
                        regTrafficLight.Image = Properties.Resources.red_light;
                        correctlyFacingTrafficLight(regTrafficLight.Image, facingDirection);
                        currentColor = RegLightSequence.Red;
                    }
                    break;
                case 1:
                    if (currentColor == RegLightSequence.Red)
                    {
                        regTrafficLight.Image = Properties.Resources.green_light;
                        correctlyFacingTrafficLight(regTrafficLight.Image, facingDirection);
                        currentColor = RegLightSequence.Green;
                    }
                    break;
            }
        }
    }
}
