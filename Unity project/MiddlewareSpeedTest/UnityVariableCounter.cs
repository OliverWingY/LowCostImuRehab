using Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MiddlewareSpeedTest
{
    internal class UnityVariableCounter : IUnityVariables
    {
        
        internal int accessCounter = 0;
        public double[] ForearmAngles 
        { get => angles;
            set 
            {
                angles = value;
                accessCounter += 1;
            } 
        }

        private double[] angles = new double[4];
        public double[] BycepAngles { get => angles; set { } }

        public int Status { get; set; }
    }
}
