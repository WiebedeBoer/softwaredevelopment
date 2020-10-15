using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Simulator
{
    class SocketClientServer
    {
        private static byte[] buffer = new byte[1024];

        private const int portnr = 54000;
        private const String controllerIp = "127.0.0.1";

        private static Socket socket;

        // Socket we're connecting to
        private static Socket socketFound;

        public SocketClientServer()
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void writeToSocket()
        {
            socket.Connect(controllerIp, portnr);

            Console.WriteLine("Connected to server...");

            socket.Bind(new IPEndPoint(IPAddress.Any, portnr));

            socket.BeginAccept(new AsyncCallback(AcceptCallback), null);
        }

        private static void AcceptCallback(IAsyncResult AR)
        {
            socketFound = socket.EndAccept(AR);


            //socketFound.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(), socketFound);
            socketFound.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), null);

            socket.BeginAccept(new AsyncCallback(AcceptCallback), null);
        }

        private static void ReceiveCallback(IAsyncResult AR)
        {
            Socket socket = (Socket)AR.AsyncState;

            int received = socket.EndReceive(AR);

            byte[] databuff = new byte[received];

            Array.Copy(buffer, databuff, received);

            string text = Encoding.ASCII.GetString(databuff);

            Console.WriteLine("Text received: " + text);
        }

        public void readFromSocket()
        {
            socket.Connect(controllerIp, portnr);

            Console.WriteLine("Connect to server...");
        }
    }
}
