using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Text;
using System.Threading.Tasks;

namespace Middleware
{
    public class UnityMonitoredVariables
    {
        //xyzw
        public Quaternion ForearmAngles 
        { 
            get { return forearmAngles; } 
            internal set { forearmAngles = value; } 
        }
        private Quaternion forearmAngles = new Quaternion();

        //xyzw
        public Quaternion BycepAngles
        {
            get { return bycepAngles; }
            internal set { bycepAngles = value; }
        }
        private Quaternion bycepAngles = new Quaternion();

    }
}
