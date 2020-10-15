using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Simulator
{
    class SocketListen
    {
        private static byte[] buffer = new byte[1024];

        private const int portnr = 54000;
        private const String controllerIp = "127.0.0.1";

        private static Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);


        // Socket we're connecting to
        private static Socket socketFound;

        public SocketListen()
        {

            bool connected = LoopConnect();

            if(connected)
            {
                ReadIncomingJSON();
            }
        }

        private void ReadIncomingJSON()
        {
            //EndPoint endPoint = new IPEndPoint(IPAddress.Any, portnr);
            //socket.Bind(endPoint);
            //socket.Listen(5);

            while(true)
            {
                //socketFound = socket.Accept();

                MemoryStream memoryStream = new MemoryStream();

                int readBytes = socket.Receive(buffer);

                while (readBytes > 0)
                {
                    memoryStream.Write(buffer, 0, readBytes);
                    if (socket.Available > 0)
                    {
                        readBytes = socketFound.Receive(buffer);
                    }
                    else
                    {
                        break;
                    }
                }

                byte[] totalBytes = memoryStream.ToArray();

                memoryStream.Close();

                string readData = Encoding.Default.GetString(totalBytes);

                //RegularTrafficLight p = JsonConvert.DeserializeObject<RegularTrafficLight>(readData);

                Console.WriteLine(readData);

                //socket.Close();
            }
        }

        public bool LoopConnect()
        {
            //IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            //IPAddress ipAddress = ipHostInfo.AddressList[0];
            //IPEndPoint remoteEP = new IPEndPoint(ipAddress, portnr);

            //EndPoint endPoint = new IPEndPoint(IPAddress.Any, 9011);

            int nrOfConnections = 0;

            while (!socket.Connected) {
                try
                {
                    nrOfConnections++;
                    socket.Connect("127.0.0.1", portnr);

                    //socket.Bind(remoteEP);
                    //socket.Listen(5);
                } catch (SocketException)
                {
                    Console.WriteLine("Connection failed");
                }
            }
            
            Console.Clear();
            Console.WriteLine("Connected...");

            return true;

        }
    }
}
