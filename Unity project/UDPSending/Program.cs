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
            running= true;
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
            Console.WriteLine("Starting");
            var remoteEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), portNumber);
            sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram,ProtocolType.Udp);

            var fullDataset = ReadInMovement();
            Console.WriteLine("Succesfully read full dataset");
            
            byte[] data = new byte[48];
            //todo: replace this with a task that can be cancelled
            while (true)
            {
                Console.WriteLine($"Starting loop at {DateTime.Now}");
                for(int i = 0; i<fullDataset.Length; i++)
                {                    
                    data = GetBytes(fullDataset[i]);
                    sock.SendTo(data, remoteEP);
                    Thread.Sleep(1);
                }
                
            }
            
        }

        private static void CurrentDomain_ProcessExit(object? sender, EventArgs e)
        {
            sock.Close();
            Console.WriteLine("press any key to exit");
            Console.ReadKey();
        }

        private static byte[] GetBytes(double[] values)
        {
            return values.SelectMany(value => BitConverter.GetBytes(value)).ToArray();
        }

        private static double[][] ReadInMovement()
        {
            var filePath = $"{AppDomain.CurrentDomain.BaseDirectory}\\DataToSendJustNumbers2.csv";
            string[][] stringData = File.ReadLines(filePath).Select(x => x.Split(',')).ToArray();
            double[][] data = new double[stringData.Length][];
            var i = 0;
            foreach (string[] stringRow in stringData)
            {
                data[i] = Array.ConvertAll(stringRow, Double.Parse);
                i++;
            }
            return data;
        }
    }
}
