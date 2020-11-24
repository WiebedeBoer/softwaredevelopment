using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Threading;

namespace Simulator
{
    public class BackGroundListener : IListener
    {
        private Int32 port = 54000;

        private IPAddress localAddr = IPAddress.Parse("127.0.0.1");

        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        // JSON send to controller
        public JObject jsonToSend = null;
        // JSON received by controller
        public JObject jsonToReceive = null;

        // Wait 1 second to send JSON 
        public async void WaitSequence() 
        {
            await System.Threading.Tasks.Task.Delay(1000);
            string sendData = JsonConvert.SerializeObject(jsonToSend);
            string length = sendData.Length.ToString();
            string headerJSON = length + ":";
            string package = headerJSON + sendData;
            byte[] dataBytes = Encoding.Default.GetBytes(package);

            // Check if still connected
            bool connected = SocketPolled(socket);
            if (connected ==true)
            {
                socket.Send(dataBytes);
            }
            else
            {
                Console.WriteLine("Controller closed...");
            }

        }

        bool SocketPolled(Socket s)
        {
            bool part1 = s.Poll(1000, SelectMode.SelectRead);
            bool part2 = (s.Available == 0);
            if (part1 && part2)
                return false;
            else
                return true;
        }

        //Listen to incoming JSON and send JSON back over corresponding port
        public void StartListening()
        {
            byte[] buffer = new byte[1024];

            while (socket.Available > 0)
            {
                int receivedDataLength = socket.Receive(buffer);

                string stringData = Encoding.ASCII.GetString(buffer, 0, receivedDataLength);

                var charsToRemove = new string[] { " " };
                foreach (var c in charsToRemove)
                {
                  stringData = stringData.Replace(c, string.Empty);
                }

                stringData.Replace(@"\", "");

                String header = "0";
                if (!String.IsNullOrWhiteSpace(stringData))
                {
                    int charLocation = stringData.IndexOf(":", StringComparison.Ordinal);

                    if (charLocation > 0)
                    {
                        header = stringData.Substring(0, charLocation);
                    }
                }
                // Check incoming JSON for unwanted characters
                stringData = Regex.Replace(stringData, @"\t|\n|\r", "");
                // Check if header is correctly received
                if (header == stringData.Substring(4).Length.ToString())
                {
                    try
                    {
                        stringData = stringData.Substring(4);

                        jsonToReceive = JsonConvert.DeserializeObject<JObject>(stringData);

                        Console.WriteLine("Received JSON: " + jsonToReceive);

                        Console.WriteLine("Send back JSON...");

                        Console.WriteLine("JSON: " + jsonToSend);
                    } catch(Exception)
                    {
                        Console.WriteLine("JSON wasn't correct and thus couldn't be parsed");
                    }

                    if (jsonToSend != null)
                    {
                        WaitSequence();
                    }
                }
                else
                {
                    Console.WriteLine("JSON is not complete...");
                }
            }
        }

        // Check if connection is made with controller
        public void Connect()
        {
            IPEndPoint localEndpoint = new IPEndPoint(localAddr, port);

            while (!socket.Connected)
            {
                try
                {
                    socket.Connect(localEndpoint);
                }
                catch (SocketException)
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
