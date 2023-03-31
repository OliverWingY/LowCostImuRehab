using Microsoft.VisualBasic;
using Middleware;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UnityStressTest
{
    internal class Program
    {
        private static int listenPortNumber = 12347;
        private static int sendPortNumber = 12345;
        private static UdpClient listenServer;
        private static Socket sendSock;
        private static IPEndPoint sendEP;
        private static byte[] doneMessage = Encoding.UTF8.GetBytes("Done");
        private int messagesRecieved;
        static void Main()
        {
            //udp setup
            var listenEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), listenPortNumber);
            listenServer = new UdpClient(listenEP);
            sendEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), sendPortNumber);
            sendSock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            //get data to send
            var reader = new StreamReader("D:\\Repos\\LowCostImuRehab\\Unity project\\UnityStressTest\\StressTestData2.txt");
            string[] stringToSend = new string[1592];
            for (int i = 0; i < 1592; i++)
            {
                stringToSend[i] = reader.ReadLine();
            }
            byte[][] dataToSend = new byte[1592][];
            int j = 0;
            foreach(string str in stringToSend) 
            {
                dataToSend[j] = Encoding.UTF8.GetBytes(str);
                j++;
            }

            //run test
            
            List<int> testFrequencies = new List<int> { 50, 70, 80, 90, 100, 120, 140, 160, 180, 200, 250, 300, 400, 500, 750, 1000, 5000, 10000, 50000, 100000 };
            
            Console.WriteLine("Begin?");
            Console.ReadLine();
            Thread.Sleep(5000);
            StringBuilder resultsStringBuilder = new StringBuilder();
            foreach (int freq in testFrequencies)
            {
                
                Thread.Sleep(10);
                Console.WriteLine($"Sending at {freq}Hz");
                var interval = (int)Math.Round((double)10000000/(double)freq);
                //var cancellationTokenSource = new CancellationTokenSource();
                var listenTask = MessageListen(listenEP);

                var startTime = DateTime.Now;
                int k = 0;
                while (k < dataToSend.Count())
                {
                    if ((DateTime.Now - startTime).Ticks > k * interval)
                    {
                        sendSock.SendTo(dataToSend[k], sendEP);
                        k++;
                    }
                }
                //cancellationTokenSource.Cancel();
                sendSock.SendTo(doneMessage, listenEP);
                              
                listenTask.Wait();
                

                var RecievedPercent = (double)listenTask.Result / (double)dataToSend.Count();
                resultsStringBuilder.AppendLine($"{freq}, {RecievedPercent}");
                Console.WriteLine($"at {freq}Hz: {RecievedPercent * 100}% messages received");

            }

            File.WriteAllText(@"D:\Repos\LowCostImuRehab\Unity project\UnityStressTest\Results.txt", resultsStringBuilder.ToString());


        }

        private static Task<int> MessageListen(IPEndPoint remoteEP)
        {
            return Task<int>.Factory.StartNew(() =>
            {
                var going = true;
                var messagesRecieved = 0;
                while (true)
                {
                    var message = listenServer.Receive(ref remoteEP);

                    if (message.SequenceEqual(doneMessage))
                    {
                        break;
                    }
                    else
                    {
                        messagesRecieved++; 
                    }
                    
                }
                return messagesRecieved;
            
            });
        }            
    }
}