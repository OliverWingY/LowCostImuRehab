using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Middleware;

namespace MatlabSpeedTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting");
            var matlabRunner = new MatlabRunner();
            var angles = new double[] { 45, 30, 15, 45, 30, 15 };
            var newAngles = new double[6];
            var startTime = DateTime.Now;
            Console.WriteLine($"ImuConversion speed test starting");
            for (int i = 0; i < 1000; i++)
            {
                newAngles = matlabRunner.ImuAngleToGlobalAngle(angles);
            }
            var endTime = DateTime.Now;
            var averageFrequency = 1000/(endTime-startTime).TotalSeconds;
            Console.WriteLine($"Test complete, matlab function ran at {averageFrequency} Hz");
            Console.WriteLine($"Final angles:");
            foreach(double angle in newAngles) { Console.Write($"{angle}, "); }
            Console.ReadKey();

        }
    }
}
