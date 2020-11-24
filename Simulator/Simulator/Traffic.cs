using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;

namespace Simulator
{
    class Traffic
    {
        public Guid guid = Guid.NewGuid();

        public PictureBox x;

        // Which direction the picturebox should face
        public String direction = "straight";

        // Random path given to traffic object
        protected Path path = null;

        // Should this object be deleted from memory
        public bool toBeDeleted = false;

        private int oldRotation = 0;

        // Which node has been reached
        public int node = 0;

        // Size of picturebox
        protected int width = 20;
        protected int height = 33;

        public void spawn(Path path, int Left, int Top)
        {
            x = new PictureBox();

            // random color for car
            Random rnd = new Random();

            x.Image = Properties.Resources.bus;

            x.BackColor = Color.Transparent;

            x.SizeMode = PictureBoxSizeMode.StretchImage;

            x.Size = new Size(20, 60);

            this.path = path;
            x.Left = Left;
            x.Top = Top;
        }

        public virtual void move(int speed, bool carBrake)
        {
            bool brake = false;
            // Do not move if trafficlight is not green
            if (path.nodes[node].Reg != null && path.nodes[node].Reg.currentColor != RegLightSequence.Green)
            {
                brake = true;
                path.nodes[node].Reg.carInFront = true;
            }

            if (path.nodes[node].Reg == null)
                carBrake = false;

            if (path.nodes[node].Reg != null && brake is false)
                    path.nodes[node].Reg.carInFront = false;
                float tx = path.nodes[node].Left - x.Left;
                float ty = path.nodes[node].Top - x.Top;
                double length = Math.Sqrt(tx * tx + ty * ty);
                if (length > speed)
                {
                    turn(x.Left, x.Top);

                // move towards the next node
                if (carBrake is false)
                {
                    x.Left = (int)(x.Left + speed * tx / length);
                    x.Top = (int)(x.Top + speed * ty / length);
                }
                }
                else
                {
                    // reached the node
                    x.Left = path.nodes[node].Left;
                    x.Top = path.nodes[node].Top;
                    if (node < (path.nodes.Count - 1))
                    {
                        if(brake is false)
                            node++;
                    }
                    else
                    {
                        toBeDeleted = true;
                    }
                }
        }

        // Which direction the picturebox should face when moving towards a node
        public void turn(int left, int top)
        {

            // (southern spawned cars)
            if (left < path.nodes[node].Left)
            {
                direction = "right";
                Image img = x.Image;
                if (oldRotation == 0)
                {
                    img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    oldRotation = 90;
                }
                if (oldRotation == 90)
                {
                    oldRotation = 90;
                }
                if (oldRotation == 180)
                {
                    img.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    oldRotation = 90;
                }
                if (oldRotation == 270)
                {
                    img.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    oldRotation = 90;
                }
                x.Image = img;
                x.Size = new Size(height, width);
            }

            // (northern spawned cars)
            if (left > path.nodes[node].Left)
            {
                direction = "left";
                Image img = x.Image;
                if (oldRotation == 0)
                {
                    img.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    oldRotation = 270;
                }
                if (oldRotation == 90)
                {
                    img.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    oldRotation = 270;
                }
                if (oldRotation == 180)
                {
                    img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    oldRotation = 270;
                }
                if (oldRotation == 270)
                {
                    oldRotation = 270;
                }
                x.Image = img;
                x.Size = new Size(height, width);
            }

            // (eastern spawned cars)
            if (top < path.nodes[node].Top)
            {
                direction = "straightdown";
                Image img = x.Image;
                if (oldRotation == 0)
                {
                    img.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    oldRotation = 180;
                }
                if (oldRotation == 90)
                {
                    img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    oldRotation = 180;
                }
                if (oldRotation == 180)
                {
                    oldRotation = 180;
                }
                if (oldRotation == 270)
                {
                    img.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    oldRotation = 180;
                }
                x.Image = img;
                x.Size = new Size(width, height);
            }

            // (western spawned cars)
            if (top > path.nodes[node].Top)
            {
                direction = "straight";
                Image img = x.Image;
                if (oldRotation == 0)
                {
                    oldRotation = 0;
                }
                if (oldRotation == 90)
                {
                    img.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    oldRotation = 0;
                }
                if (oldRotation == 180)
                {
                    img.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    oldRotation = 0;
                }
                if (oldRotation == 270)
                {
                    img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    oldRotation = 0;
                }
                x.Image = img;
                x.Size = new Size(width, height);
            }
        }

        // Check if collision detected with other traffic
        public bool collisionDetection(List<Traffic> traffic)
        {
            List<Traffic> traffic2 = traffic.Where(traffic => traffic.guid != this.guid).ToList();

            // No traffic, always return false
            if (traffic2.Count == 0)
            {
                return false;
            }

            foreach (Traffic tr2 in traffic2)
            {
                if (x.Bounds.IntersectsWith(tr2.x.Bounds) && direction == "straight" && tr2.direction == direction && tr2.x.Top < x.Top)
                    return true;
                if (x.Bounds.IntersectsWith(tr2.x.Bounds) && direction == "straightdown" && tr2.direction == direction && tr2.x.Top > x.Top)
                    return true;
                if (x.Bounds.IntersectsWith(tr2.x.Bounds) && direction == "right" && tr2.direction == direction && tr2.x.Left > x.Left)
                    return true;
                if (x.Bounds.IntersectsWith(tr2.x.Bounds) && direction == "left" && tr2.direction == direction && tr2.x.Left < x.Left)
                    return true;
            }

            return false;
        }
    }
}
