using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace UDPTinkering
{
    public class PortListener 
    {
        private static int portNumber = 17628;
        private static UdpClient udpServer;
        public double[] currentAngles = new double[6];
        public PortListener(int portNumber)
        {
            udpServer = new UdpClient(portNumber);
            StartListening(portNumber); 
        }

        private async void StartListening(int portNumber)
        {
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
            await Task.Run(() =>
            {
                while (true)
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
        private double[] DecodeUDPMessage(byte[] data)
        {
            var doubleArray = new double[6];
            for(int i = 0; i < 6; i++)
            {
                doubleArray[i] = BitConverter.ToDouble(data, i * 8);
            }
            return doubleArray;
        }
        private static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            udpServer.Close();
        }
    }
}
