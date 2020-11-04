using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace Simulator
{
    class BackGroundListener
    {
        private Int32 port = 54000;

        private IPAddress localAddr = IPAddress.Parse("127.0.0.1");

        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        private JSONTrafficLight json = null;

        public JObject json2 = null;

        // JSON To Send back
        //public JSONTrafficLight jsonToSend = null;
        public JObject jsonToSend = null;
        public JSONTrafficLight Json { get => json; set => json = value; }


        //connect and receive, instead of listen

        public void StartListening()
        {

            byte[] buffer = new byte[1024];

            while (socket.Available > 0)
            {
                int receivedDataLength = socket.Receive(buffer);

                string stringData = Encoding.ASCII.GetString(buffer, 0, receivedDataLength);

                


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
                    var charsToRemove = new string[] { "-" };
                    foreach (var c in charsToRemove)
                    {
                        stringData = stringData.Replace(c, string.Empty);
                    }
                    stringData = stringData.Substring(4);

                    json2 = JObject.Parse(stringData);

                    json = JsonConvert.DeserializeObject<JSONTrafficLight>(stringData);



                    Console.WriteLine(json2["A11"]);

                    Console.WriteLine("Send back JSON...");

                    if (jsonToSend != null)
                    {
                        
                        
                        
                        string sendData = JsonConvert.SerializeObject(jsonToSend);
                        
                        string length = sendData.Length.ToString();
                        string headerJSON = length + ":";
                        string package = headerJSON + sendData;
                        byte[] dataBytes = Encoding.Default.GetBytes(package);
                        socket.Send(dataBytes);
                    }
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
