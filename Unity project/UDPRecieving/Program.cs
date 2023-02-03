using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UDPRecieving // Note: actual namespace depends on the project name.
{
    public class Program
    {
        private static int portNumber = 17628;
        private static UdpClient udpServer;
        public static void Main()
        {
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
            Console.WriteLine("Starting");
            udpServer = new UdpClient(portNumber);
            var remoteEP = new IPEndPoint(IPAddress.Any, portNumber);
            while (true)
            {
                var data = udpServer.Receive(ref remoteEP);
                Console.WriteLine(ASCIIEncoding.ASCII.GetString(data));
                
            }
            
        }

        private static void CurrentDomain_ProcessExit(object? sender, EventArgs e)
        {
            udpServer.Close();
            Console.WriteLine("press any key to exit");
            Console.ReadKey();            
        }
    }
}