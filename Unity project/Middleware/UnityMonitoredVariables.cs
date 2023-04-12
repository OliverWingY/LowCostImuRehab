using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Text;
using System.Threading.Tasks;

namespace Middleware
{
    public class UnityMonitoredVariables : IUnityVariables
    {
        //xyzw
        public int Status { get; set; } = 0;
        public double[] ForearmAngles { get; set; }

        //xyzw
        public double[] BycepAngles { get; set; }
    }
}
