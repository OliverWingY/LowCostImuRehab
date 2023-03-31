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
        public Quaternion ForearmAngles 
        { 
            get { return forearmAngles; } 
            set { forearmAngles = value; } 
        }
        private Quaternion forearmAngles = new Quaternion();

        //xyzw
        public Quaternion BycepAngles
        {
            get { return bycepAngles; }
            set { bycepAngles = value; }
        }
        private Quaternion bycepAngles = new Quaternion();

    }
}
