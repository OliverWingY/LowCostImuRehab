﻿using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
namespace Middleware
{    
    public class ImuDataConnector : IDisposable
    {
        //variables for communicating with python
        private static Task imuListen;
        private static UdpClient imuServer;
        private static Socket sendSock;
        private static IPEndPoint sendEP;
        private static CancellationTokenSource imuListenCancellation = new CancellationTokenSource();

        private static bool gameRunning = false;
        private static bool eventOccurred = false;
        //variables for sending data to unity

        private IUnityVariables unityArm;

        //variables for creating metadata
       // private static MatlabRunner matlabRunner;

        public ImuDataConnector(int imuRecievePortNumber, int imuSendPortNumber, IUnityVariables UnityMonitoredVariables) 
        {
            unityArm = UnityMonitoredVariables;
            imuServer = new UdpClient(imuRecievePortNumber);
            imuListen = StartListening(imuRecievePortNumber, imuListenCancellation.Token);
            sendEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), imuSendPortNumber);
            sendSock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
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
                byte[] backData;
                //this loop will currently run as the imu data comes in, all race conditions are dealt with using an ostridge algorithm 
                while (!cancellationToken.IsCancellationRequested)
                {

                    //the code will stop here and wait for the next imu packet 
                    var recieveTask = Task.Run(() => imuServer.Receive(ref imuRemoteEP));
                    if (recieveTask.Wait(TimeSpan.FromSeconds(1)))
                        imuData = recieveTask.Result;
                    else
                        continue;
                    var stringData = Encoding.UTF8.GetString(imuData);
                    var decodedData = DecodeImuData(stringData);
                    if (decodedData != null && decodedData != new double[21])
                        UpdateUnity(decodedData);


                    if (eventOccurred)
                    {
                        var backString = $"{stringData},{gameRunning},{true}";
                        backData = Encoding.UTF8.GetBytes(backString);
                        eventOccurred = false;
                    }
                    else
                    {
                        var backString = $"{stringData},{gameRunning},{false}";
                        backData = Encoding.UTF8.GetBytes(backString);
                    }

                    sendSock.SendTo(backData, sendEP);
                }
            });
        }

        //functions for dealing with Imu data
        private static double[] DecodeImuData(string stringData)
        {
            //assuming 3 imus, angle + acceleration data
            var doubleArray = new double[21];            
            try
            {
                var stringArray = stringData.Split(',');
                for (int i = 0; i < 21; i++)
                {
                    doubleArray[i] = Convert.ToDouble(stringArray[i]);
                }
            }
            catch
            {
                doubleArray = new double[21];
            }
            return doubleArray;
        }     
        
        private void UpdateUnity(double[] imuData)
        {
            unityArm.ForearmAngles = new Quaternion((float)imuData[5], (float)imuData[6], (float)imuData[7], (float)imuData[4]);
            unityArm.BycepAngles = new Quaternion((float)imuData[9], (float)imuData[10], (float)imuData[11], (float)imuData[8]);

        }
        public void NotifyStart()
        {
            gameRunning = true;
        }

        public void NotifyEnd() 
        {
            gameRunning = false;
        }

        public void NotifyEvent()
        {
            eventOccurred = true;            
        }       

        public void Dispose()
        {
            imuListenCancellation.Cancel();            
            imuListen.Wait(3000);
            imuServer.Close();
            imuServer.Dispose();
        }
    }
}