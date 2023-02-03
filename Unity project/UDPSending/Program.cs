using System;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace UDPSending
{
    public class Program
    {
        private static int portNumber = 17628;
        private static Socket sock;
        public static void Main()
        {
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
            Console.WriteLine("Starting");
            var remoteEP = new IPEndPoint(IPAddress.Parse("10.41.39.105"), portNumber);
            sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram,ProtocolType.Udp);
            while (true)
            {                
                string readLine = Console.ReadLine();
                var data = ASCIIEncoding.ASCII.GetBytes(readLine);
                sock.SendTo(data, remoteEP);
                Console.WriteLine("sent!");
            }
            
        }

        private static void CurrentDomain_ProcessExit(object? sender, EventArgs e)
        {
            sock.Close();
            Console.WriteLine("press any key to exit");
            Console.ReadKey();
        }
    }
}
