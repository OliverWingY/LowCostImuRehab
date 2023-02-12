using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Middleware
{
    public class UnityMonitoredVariables
    {
        public double[] Position { get; }
        public double[] Angles { get; }

        public void Update(double[] imuData)
        {
            //in here should be the logic for converting the imu data into the the position and angles for unity to use for the arm model
            throw new NotImplementedException();
        }
    }
}
