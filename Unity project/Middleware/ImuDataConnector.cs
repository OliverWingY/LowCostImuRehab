﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Numerics;

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
       // private static MatlabRunner matlabRunner;
        public static double[][] RecordedMotion = new double[800][]; //8 second of xyz acceleration and angle
        public static DateTime[] RecordedTimes = new DateTime[800];

        public ImuDataConnector(int imuPortNumber, ref UnityMonitoredVariables UnityMonitoredVariables) 
        {
            unityArm = UnityMonitoredVariables;
            imuServer = new UdpClient(imuPortNumber);
            imuListen = StartListening(imuPortNumber, imuListenCancellation.Token);
        }

        public string ClassifyMotion()
        {
            return "not implemented yet";
        }

        private Task StartListening(int portNumber, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() =>
            {
                int i = 0;
                var imuRemoteEP = new IPEndPoint(IPAddress.Any, portNumber);
                byte[] imuData;
                //this loop will currently run as the imu data comes in, all race conditions are dealt with using an ostridge algorithm 
                while (!cancellationToken.IsCancellationRequested)
                {

                    //the code will stop here and wait for the next imu packet 
                                       
                    var recieveTask = Task.Run(() => imuServer.Receive(ref imuRemoteEP));
                    if (recieveTask.Wait(TimeSpan.FromSeconds(1)))
                        imuData = recieveTask.Result;
                    else
                        continue;
                    
                    
                    //RecordedTimes[i] = DateTime.Now;
                    var decodedData = DecodeImuData(imuData);
                    RecordedMotion[i] = decodedData;
                    if (decodedData != null && decodedData != new double[21])
                        UpdateUnity(decodedData);

                    if (i == 799)
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
            var doubleArray = new double[21];
            var stringData = Encoding.UTF8.GetString(data);
            //todo replace this with some good catching logic
            try
            {
                 doubleArray = stringData.Split(',').Select(r => Convert.ToDouble(r)).ToArray();
            }
            catch 
            {
                var bleh = stringData;
                doubleArray = new double[21];
            }
            return doubleArray;
        }     
        
        private void UpdateUnity(double[] imuData)
        {  
            var imu2angle = ToEulerAngles(new Quaternion((float)imuData[5], (float)imuData[6], (float)imuData[7], (float)imuData[4]));
            var imu3angle = ToEulerAngles(new Quaternion((float)imuData[9], (float)imuData[10], (float)imuData[11], (float)imuData[8]));
            var forearmAngle = imu2angle;
            var bycepAngle = imu3angle;
            unityArm.ForearmAngles = new double[] { forearmAngle.X, forearmAngle.Y, forearmAngle.Z};
            unityArm.BycepAngles = new double[] { bycepAngle.X, bycepAngle.Y, bycepAngle.Z };
        }

        private static Vector3 ToEulerAngles(Quaternion q)
        {
            Vector3 angles = new Vector3();

            // roll / x
            double sinr_cosp = 2 * (q.W * q.X + q.Y * q.Z);
            double cosr_cosp = 1 - 2 * (q.X * q.X + q.Y * q.Y);
            angles.X = (float)Math.Atan2(sinr_cosp, cosr_cosp);

            // pitch / y
            double sinp = 2 * (q.W * q.Y - q.Z * q.X);
            if (Math.Abs(sinp) >= 1)
            {
                if(sinp >=0)
                    angles.Y = (float)(Math.PI / 2);
                else
                    angles.Y = (float)(-Math.PI / 2);
            }
            else
            {
                angles.Y = (float)Math.Asin(sinp);
            }

            // yaw / z
            double siny_cosp = 2 * (q.W * q.Z + q.X * q.Y);
            double cosy_cosp = 1 - 2 * (q.Y * q.Y + q.Z * q.Z);
            angles.Z = (float)Math.Atan2(siny_cosp, cosy_cosp);

            return angles;
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
            imuServer.Close();
            imuServer.Dispose();
        }
    }
}