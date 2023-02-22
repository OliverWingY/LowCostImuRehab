using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Middleware
{
    public class UnityMonitoredVariables
    {
        public double[] Position 
        { 
            get { return position; } 
            internal set { position = value; }
        }
        private double[] position;
        public double[] Angles 
        { 
            get { return angles; } 
            internal set { angles = value; } 
        }
        private double[] angles;

    }
}
