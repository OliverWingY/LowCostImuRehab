using Middleware;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace MiddlewareSpeedTest
{
    internal class Program
    {
        private static int listenPortNumber = 12345;
        private static int sendPortNumber = 12346;
        private static UdpClient listenServer;
        private static Socket sendSock;
        private static IPEndPoint sendEP;
        //Testing strategy:
        //Test 1:
        //1. Build UDP messages to be sent
        //2. Start up middleware
        //3. Send, wait for response, how long to do 10,000?
        //Test 2:
        //Build UDP messages to be sent
        //Start up middleware
        //send 100 messages
        //see how many get dropped
        static void Main(string[] args)
        {
            //arrange
            var remoteEP = new IPEndPoint(IPAddress.Any, listenPortNumber);
            listenServer = new UdpClient(remoteEP);
            sendEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), sendPortNumber);
            sendSock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);


            //message
            var messageDoubles = new double[21] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21 };
            var messageTimeStamp = DateTime.Now;
            var sb = new StringBuilder();
            foreach ( var messageDouble in messageDoubles ) 
            {
                sb.Append( messageDouble.ToString() );
                sb.Append(',');
            }
            sb.Append(messageTimeStamp.ToString());
            var messageString = sb.ToString();
            var message = Encoding.UTF8.GetBytes(messageString);

            //middleware
            //Console.WriteLine("Starting Middleware");
            //var unityVariables = new UnityMonitoredVariables();
            //var middleWare = new ImuDataConnector(sendPortNumber, listenPortNumber, unityVariables);
            //middleWare.NotifyStart();
            ////act
            //var startTime = DateTime.Now;
            //for (int  j = 0; j <9999; j++)
            //{
            //    sendSock.SendTo(message, sendEP);
            //    listenServer.Receive(ref remoteEP);
            //}
            //var totalTime = DateTime.Now - startTime;
            //Console.WriteLine($"Total ticks for 10,000 messages: {totalTime.Milliseconds}ms");
            //Console.WriteLine("Closing middleware");
            //middleWare.Dispose();
            //Console.ReadLine();

            Console.WriteLine("Starting Test 2");
            var unityVariables2 = new UnityVariableCounter();
            var middleWare = new ImuDataConnector(sendPortNumber, listenPortNumber, unityVariables2);
            middleWare.NotifyStart();
            var startTicks = DateTime.Now;
            int i = 0;
            while(i <= 999)
            {
                if ((DateTime.Now-startTicks).TotalMilliseconds > i*10) 
                {
                    sendSock.SendTo(message, sendEP);
                    listenServer.Receive(ref remoteEP);
                    i++;
                }                
            }

            Console.WriteLine($"{i} messages sent, {unityVariables2.accessCounter} messages recieved");
            Console.ReadLine();
            middleWare.Dispose();



        }
    }
}
