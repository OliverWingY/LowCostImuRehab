using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Middleware
{    
    public class ImuDataConnector : IDisposable
    {
        //variables for listening to imu data
        private static Task imuListen;
        private static UdpClient imuServer;
        private static CancellationTokenSource imuListenCancellation = new CancellationTokenSource();

        //variables for sending data to unity

        private UnityMonitoredVariables unityArm;

        //variables for creating metadata
        private static MatlabRunner matlabRunner;
        public static double[][] RecordedMotion = new double[800][]; //8 second of xyz acceleration and angle
        public static DateTime[] RecordedTimes = new DateTime[800];

        public ImuDataConnector(int imuPortNumber, UnityMonitoredVariables UnityMonitoredVariables) 
        {
            unityArm= UnityMonitoredVariables;

            matlabRunner = new MatlabRunner();

            imuServer = new UdpClient(imuPortNumber);
            imuListen = StartListening(imuPortNumber, imuListenCancellation.Token);
        }

        private Task StartListening(int portNumber, CancellationToken cancellationToken)
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
                    RecordedTimes[i] = DateTime.Now;
                    var decodedData = DecodeImuData(imuData);
                    
                    RecordedMotion[i] = decodedData;                    
                    Task.Run(() => UpdateUnity(decodedData));

                    if (i == 800)
                    {         
                        //this should run asyncrounously so the database stuff can take as long as it needs while the loop continues
                        Task.Run(() =>
                        {
                            //var DBData = GetMetaData(RecordedMotion);
                            //SendMetaDataToDatabase(DBData);
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
            //assuming 2 imus, angle + acceleration data
            var doubleArray = new double[12];
            for (int i = 0; i < 12; i++)
            {
                doubleArray[i] = BitConverter.ToDouble(data, i * 8);
            }
            return doubleArray;
        }     
        
        private void UpdateUnity(double[] imuData)
        {
            var imuAngles = (double[])imuData.Take(6);
            unityArm.Angles = imuAngles;
            var imuAcceleration = (double[])imuData.Skip(6).Take(6);
        }

        //functions for dealing with matlab and database
        private static object GetMetaData(double[][] data)
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
            imuListenCancellation.Cancel();
            imuListen.Wait();
            if (matlabRunner != null) matlabRunner.Dispose();        
            imuServer.Close();
            imuServer.Dispose();
        }
    }
}