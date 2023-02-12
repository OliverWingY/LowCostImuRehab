using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Middleware
{    
    public class Middleware : IDisposable
    {
        //variables for listening to imu data
        private static Task imuListen;
        private static UdpClient imuServer;
        private static CancellationTokenSource imuListenCancellation;

        //variables for sending data to unity
        private static Socket unitySocket;
        private static IPEndPoint unityEndPoint;

        //variables for creating metadata
        private static MatlabRunner matlabRunner;
        public static double[][] RecordedMotion = new double[80000][]; //8 second of xyz acceleration and angle

        public Middleware(int imuPortNumber, int unityPortNumber) 
        {
            unityEndPoint  = new IPEndPoint(IPAddress.Parse("127.0.0.1"), unityPortNumber);
            unitySocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            matlabRunner = new MatlabRunner();

            imuServer = new UdpClient(imuPortNumber);
            imuListen = StartListening(imuPortNumber, imuListenCancellation.Token);
        }

        public void Close()
        {
            imuListenCancellation.Cancel();
            imuListen.Wait();
            matlabRunner.Dispose();
            UpdateDatabase();
            Dispose();
        }

        private static Task StartListening(int portNumber, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() =>
            {
                int i = 0;
                var imuRemoteEP = new IPEndPoint(IPAddress.Any, portNumber);
                //this loop will currently run as the imu data comes in, all race conditions are dealt with using an ostridge algorithm 
                while (!cancellationToken.IsCancellationRequested)
                {                    
                    //the code will stop here and wait for the next imu packet
                    var imuData = imuServer.Receive(ref imuRemoteEP);
                    var decodedData = DecodeImuData(imuData);

                    RecordedMotion[i] = decodedData; 
                    Task.Run(() => ProcessAndSendUnityData(decodedData));

                    if (i == 80000)
                    {         
                        //this should run asyncrounously so the database stuff can take as long as it needs while the loop continues
                        Task.Run(() =>
                        {
                            var DBData = GetMetaDataWithMatlab(RecordedMotion);
                            SendMetaDataToDatabase(DBData);
                        });
                        i = 0;
                    }
                    else  i++; 
                }
            });
        }

        //functions for dealing with Imu data
        private static double[] DecodeImuData(byte[] data)
        {
            throw new NotImplementedException();
        }

        //functions for dealing with unity
        private static void ProcessAndSendUnityData(double[] decodedData)
        {
            var processedData = ProcessDataForUnity(decodedData);
            var dataToSend = GetBytes(processedData);
            unitySocket.SendTo(dataToSend, unityEndPoint);
        }
        private static double[] ProcessDataForUnity(double[] data)
        {
            throw new NotImplementedException();
        }        

        private static byte[] GetBytes(double[] values)
        {
            return values.SelectMany(value => BitConverter.GetBytes(value)).ToArray();
        }

        //functions for dealing with matlab and database
        private static object GetMetaDataWithMatlab(double[][] data)
        {
            throw new NotImplementedException();
        }
        private static void UpdateDatabase()
        {
            throw new NotImplementedException();
        }
        private static void SendMetaDataToDatabase(object dbData)
        {
            throw new NotImplementedException();
        }        

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}