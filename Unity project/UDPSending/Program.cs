using System;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace UDPSending
{
    public class Program
    {
        private static int portNumber = 17629;
        private static Socket sock;
        private static CancellationTokenSource cts = new CancellationTokenSource();

        public static void Main()
        {
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
            Console.WriteLine("Starting");
            var remoteEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), portNumber);
            sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            var fullDataset = ReadInMovement();
            Console.WriteLine("Succesfully read full dataset");

            byte[] data = new byte[48];
            //todo: replace this with a task that can be cancelled

            RunLoop(remoteEP, fullDataset, data).Wait();
        }

        private static async Task RunLoop(IPEndPoint remoteEP, double[][] fullDataset, byte[] data)
        {
            while (!cts.IsCancellationRequested)
            {
                Console.WriteLine($"Starting loop at {DateTime.Now}");
                var timer = Task.Run(()=>Task.Delay(100));
                for (int i = 0; i < fullDataset.Length; i++)
                {
                    data = GetBytes(fullDataset[i]);
                    sock.SendTo(data, remoteEP);
                }
                await timer;
            }
        }

        private static void CurrentDomain_ProcessExit(object? sender, EventArgs e)
        {
            cts.Cancel();
            Thread.Sleep(1000);
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
