using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Simulator
{
    class SocketClientServer
    {
        private int portnr = 12345;
        private String controllerIp = "127.0.0.1";

        private Socket socket;

        public SocketClientServer()
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void writeToSocket()
        {
            socket.Connect(controllerIp, portnr);

            Console.WriteLine("Connected to server...");


        }

        public void readFromSocket()
        {
            socket.Connect(controllerIp, portnr);

            Console.WriteLine("Connect to server...");
        }
    }
}
