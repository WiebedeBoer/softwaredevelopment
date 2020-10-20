using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using Newtonsoft.Json;

namespace Simulator
{
    class BackGroundListener
    {
        private Int32 port = 54000;

        private IPAddress localAddr = IPAddress.Parse("127.0.0.1");

        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        private JSONTrafficLight json = null;

        public JSONTrafficLight Json { get => json; set => json = value; }

        public void StartListening()
        {

            byte[] buffer = new byte[1024];

            while (socket.Available > 0)
            {
                int receivedDataLength = socket.Receive(buffer);
                string stringData = Encoding.ASCII.GetString(buffer, 0, receivedDataLength);
                json = JsonConvert.DeserializeObject<JSONTrafficLight>(stringData);
                Console.WriteLine(json.A11);
                //Console.WriteLine(nameof(json.A11));
            }

            //socket.Disconnect(false);
            //socket.Close();

            //Connect();
        }




        public void Connect()
        {
            IPEndPoint localEndpoint = new IPEndPoint(localAddr, port);

            while (!socket.Connected)
            {
                try
                {
                    socket.Connect(localEndpoint);
                }
                catch (SocketException e)
                {
                    Console.WriteLine("Unable to connect to server.");
                }
            }

            while (socket.Connected)
            {
                StartListening();
            }
        }
    }
}
