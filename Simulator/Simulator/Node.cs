using System;
using System.Collections.Generic;
using System.Text;

namespace Simulator
{
    class Node
    {
        private int left;
        private int top;
        private RegularTrafficLight reg = null;

        public Node(int left, int top, RegularTrafficLight reg = null)
        {
            this.left = left;
            this.top = top;
            this.reg = reg;
        }

        public int Left { get => left; set => left = value; }
        public int Top { get => top; set => top = value; }
        public RegularTrafficLight Reg { get => reg; set => reg = value; }
    }
}
