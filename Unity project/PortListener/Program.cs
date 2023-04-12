using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UDPTesting
{
    internal class Program
    {
        private static int portNumber = 12345;
        private static CancellationTokenSource cancellation = new CancellationTokenSource();
        private static UdpClient udpServer;
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
            udpServer = new UdpClient(portNumber);
            var ipAddress = IPAddress.Parse("127.0.0.1");
            var remoteEP = new IPEndPoint(ipAddress, portNumber);
            while (!cancellation.IsCancellationRequested)
            {
                var data = udpServer.Receive(ref remoteEP);
                Console.WriteLine(Encoding.UTF8.GetString(data));
            }
        }

        private static void CurrentDomain_ProcessExit(object? sender, EventArgs e)
        {
            cancellation.Cancel();
            Thread.Sleep(2000);
            udpServer.Close();
        }
    }
}