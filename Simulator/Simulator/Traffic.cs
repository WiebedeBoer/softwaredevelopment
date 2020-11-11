using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Windows.Forms;

namespace Simulator
{
    class Traffic
    {
        public Guid guid = Guid.NewGuid();

        public PictureBox x;

        public String direction = "straight";

        protected Path path = null;

        public bool toBeDeleted = false;

        private int oldRotation = 0;

        public int node = 0;

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
            if (path.nodes[node].Reg != null && path.nodes[node].Reg.currentColor != RegLightSequence.Green)
            {
                brake = true;
                path.nodes[node].Reg.carInFront = true;
            }
            //else
            //{
            // brake = false;
            //}

            //if (brake is false)
            //{
            if (path.nodes[node].Reg != null && brake is false)
                    path.nodes[node].Reg.carInFront = false;
                float tx = path.nodes[node].Left - x.Left;
                float ty = path.nodes[node].Top - x.Top;
                double length = Math.Sqrt(tx * tx + ty * ty);
                if (length > speed)
                {
                    turn(x.Left, x.Top);

                // move towards the goal

                if (carBrake is false)
                {
                    x.Left = (int)(x.Left + speed * tx / length);
                    x.Top = (int)(x.Top + speed * ty / length);
                }
                }
                else
                {
                    // already there
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

            
            //}
        }

        public void turn(int left, int top)
        {

            // right(southern spawned cars)
            if (/*top > path.nodes[node].Top &&*/ left < path.nodes[node].Left)
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

            // right(northern spawned cars)
            if (/*top < path.nodes[node].Top &&*/ left > path.nodes[node].Left)
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

            // right(eastern spawned cars)
            if (top < path.nodes[node].Top/* && left < path.nodes[node].Left*/)
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

            // right(western spawned cars)
            if (top > path.nodes[node].Top/* && left > path.nodes[node].Left*/)
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

        public bool collisionDetection(List<Traffic> traffic)
        {
            List<Traffic> traffic2 = traffic.Where(traffic => traffic.guid != this.guid).ToList();

            if (traffic2.Count == 0)
            {
                return false;
            }

            // Collisionboxes
            Rectangle rect = new Rectangle();
            if (direction == "straight")
            {
                rect = new Rectangle(x.Left, (x.Top - 10), x.Width, 10);
            }

            if (direction == "straightdown")
            {
                rect = new Rectangle(x.Left, (x.Top + x.Height + 10), x.Width, 10);
            }

            if (direction == "right")
            {
                rect = new Rectangle((x.Left + x.Width), x.Top, 10, x.Top);
            }

            if (direction == "left")
            {
                rect = new Rectangle((x.Left - x.Width), x.Top, 10, x.Top);
            }

            int carInFront = 0;

            foreach (Traffic tr2 in traffic2)
            {
                if ((rect.IntersectsWith(tr2.x.Bounds)))
                {
                    carInFront++;
                }
                // directection === direction weer weghalen als niet werkt...
                if (x.Bounds.IntersectsWith(tr2.x.Bounds) && carInFront != 0 && tr2.direction == this.direction)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
