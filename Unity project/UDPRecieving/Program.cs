using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UDPRecieving 
{
    public class Program
    {
        private static int portNumber = 17628;
        private static UdpClient udpServer;
        public static void Main()
        {
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
            Console.WriteLine("Starting...");
            udpServer = new UdpClient(portNumber);
            var remoteEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), portNumber);
            var data = new byte[48];
            var doubleArray = new double[6];
            Console.WriteLine("Started");
            while (true)
            {
                data = udpServer.Receive(ref remoteEP);
                doubleArray = GetDoubles(data);
                for(int i= 0; i<6; i++)
                {
                    Console.Write($"{doubleArray[i]}, ");
                }
                Console.WriteLine();
            }            
        }

        private static void CurrentDomain_ProcessExit(object? sender, EventArgs e)
        {
            
            //udpServer.Close();
            Console.WriteLine("press any key to exit");
            Console.ReadKey();            
        }

        static double[] GetDoubles(byte[] bytes)
        {
            var result = new double[bytes.Length / sizeof(double)];
            Buffer.BlockCopy(bytes, 0, result, 0, bytes.Length);
            return result;
        }
    }
}