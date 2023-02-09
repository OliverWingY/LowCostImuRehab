using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace UDPTinkering
{
    public class PortListener 
    {
        private static UdpClient udpServer;
        public static double[] currentAngles = new double[6];
        private static CancellationTokenSource cancellation= new CancellationTokenSource();
        private static Task listen;
        public PortListener(int portNumber)
        {
            udpServer = new UdpClient(portNumber);
            listen = StartListening(portNumber, cancellation.Token); 
        }

        private static Task StartListening(int portNumber, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    var remoteEP = new IPEndPoint(IPAddress.Any, portNumber);
                    var data = udpServer.Receive(ref remoteEP);
                    lock (currentAngles)
                    {
                        currentAngles = DecodeUDPMessage(data);
                    }
                }
            });            
        }
        
        //converts array of 48 bytes into an array of 6 doubles
        private static double[] DecodeUDPMessage(byte[] data)
        {
            var doubleArray = new double[6];
            for(int i = 0; i < 6; i++)
            {
                doubleArray[i] = BitConverter.ToDouble(data, i * 8);
            }
            return doubleArray;
        }
        public static void Close()
        {
            cancellation.Cancel();
            listen.Dispose();              
            udpServer.Close();
        }
    }
}
