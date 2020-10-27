using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace Simulator
{
    class BackGroundListener
    {
        private Int32 port = 54000;

        private IPAddress localAddr = IPAddress.Parse("127.0.0.1");

        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        private JSONTrafficLight json = null;

        public JSONTrafficLight Json { get => json; set => json = value; }


        //connect and receive, instead of listen

        public void StartListening()
        {

            byte[] buffer = new byte[1024];

            while (socket.Available > 0)
            {
                int receivedDataLength = socket.Receive(buffer);

                string stringData = Encoding.ASCII.GetString(buffer, 0, receivedDataLength);

                //stringData = Regex.Replace(stringData, @"\t|\n|\r", "");


                String header = "0";
                if (!String.IsNullOrWhiteSpace(stringData))
                {
                    int charLocation = stringData.IndexOf(":", StringComparison.Ordinal);

                    if (charLocation > 0)
                    {
                        header = stringData.Substring(0, charLocation);
                    }
                }

                if (header == stringData.Substring(4).Length.ToString())
                {
                    stringData = stringData.Substring(4);

                    json = JsonConvert.DeserializeObject<JSONTrafficLight>(stringData);

                    Console.WriteLine(json.A11);
                }
                else
                {
                    Console.WriteLine("JSON is not complete...");
                }



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
