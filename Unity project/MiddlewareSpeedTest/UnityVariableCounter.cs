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
        public Quaternion ForearmAngles 
        { get => angles;
            set 
            {
                angles = value;
                accessCounter += 1;
            } 
        }

        private Quaternion angles = new Quaternion();
        public Quaternion BycepAngles { get => angles; set { } }

    }
}
