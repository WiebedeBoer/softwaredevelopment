using System;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace Simulator
{
    static class Program
    {

        private static bool _hasRun;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            //RunOnce();
        }


        public static void RunOnce()
        {
            if (!_hasRun)
            {
                SocketListen socket = new SocketListen();
            }
            _hasRun = true;
        }
    }
}
