using System;
using System.Collections.Generic;
using System.Text;

namespace Simulator
{
    class Path
    {
        public List<Node> nodes = new List<Node>();

        public void addNode(int left, int top, RegularTrafficLight reg = null)
        {
            Node node = new Node(left, top, reg);

            nodes.Add(node);
        }
    }
}
