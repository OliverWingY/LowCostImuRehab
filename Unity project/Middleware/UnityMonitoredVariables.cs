using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Middleware
{
    public class UnityMonitoredVariables
    {
        //xyzw
        public double[] ForearmAngles 
        { 
            get { return forearmAngles; } 
            internal set { forearmAngles = value; } 
        }
        private double[] forearmAngles = new double[4];

        //xyzw
        public double[] BycepAngles
        {
            get { return bycepAngles; }
            internal set { bycepAngles = value; }
        }
        private double[] bycepAngles = new double[4];

    }
}
