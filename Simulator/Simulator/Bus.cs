using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Simulator
{
    class Bus : Traffic
    {
        public void spawn(Path path, int Left, int Top)
        {
            x = new PictureBox();

            x.Image = Properties.Resources.bus;

            x.BackColor = Color.Transparent;

            x.SizeMode = PictureBoxSizeMode.StretchImage;

            x.Size = new Size(20, 60);

            this.path = path;
            x.Left = Left;
            x.Top = Top;

            this.width = 20;
            this.height = 60;
        }


         public override void move(int speed, bool carBrake)
        {
            bool brake = false;
            // Check if both bus trafficlight or regular trafficlight is 'green'
            if (path.nodes[node].Reg != null && path.nodes[node].Reg.currentBusColor != BusLightSequence.Straight
                && path.nodes[node].Reg.currentBusColor != BusLightSequence.AnyDir
                && path.nodes[node].Reg.currentBusColor != BusLightSequence.Left
                && path.nodes[node].Reg.currentBusColor != BusLightSequence.Right
                && path.nodes[node].Reg.currentColor != RegLightSequence.Green)
            {
                brake = true;
                path.nodes[node].Reg.carInFront = true;
            }

            if (path.nodes[node].Reg != null && brake is false)
                path.nodes[node].Reg.carInFront = false;
            float tx = path.nodes[node].Left - x.Left;
            float ty = path.nodes[node].Top - x.Top;
            double length = Math.Sqrt(tx * tx + ty * ty);
            if (length > speed)
            {
                turn(x.Left, x.Top);

                // move towards the node
                if (carBrake is false)
                {
                    x.Left = (int)(x.Left + speed * tx / length);
                    x.Top = (int)(x.Top + speed * ty / length);
                }
            }
            else
            {
                // reached the next node
                x.Left = path.nodes[node].Left;
                x.Top = path.nodes[node].Top;
                if (node < (path.nodes.Count - 1))
                {
                    if (brake is false)
                        node++;
                }
                else
                {
                    toBeDeleted = true;
                }
            }
        }
    }


}
