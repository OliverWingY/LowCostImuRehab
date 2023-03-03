using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Middleware;

namespace UDPTesting
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var Bycep = new double[4];
            var Forarm = new double[4];
            var ArmPosition = new UnityMonitoredVariables();
            var Middleware = new ImuDataConnector(12346, ref ArmPosition);
            Console.ReadLine();
            while (true) 
            {           
                Console.ReadLine() ;
                Bycep = ArmPosition.BycepAngles;
                Forarm = ArmPosition.ForearmAngles;
                for (int i = 0; i <4; i++) 
                {
                    Console.Write($"{Bycep[i]}f, ");
                }
                Console.WriteLine();
                for (int i = 0; i < 4; i++)
                {
                    Console.Write($"{Forarm[i]}f, ");
                }
            }
        }

        private static double[] DecodeMessage(byte[] data) 
        {
            var stringData = Encoding.UTF8.GetString(data);
            try
            {
                return stringData.Split(',').Select(r => Convert.ToDouble(r)).ToArray();
            }
            catch(Exception ex) 
            {
                return new double[12];
            }

        }


    }
}