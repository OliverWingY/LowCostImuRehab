
using UnityEngine;

namespace Middleware
{
    public interface IUnityVariables
    {
        int Status { get; set; }

        //xyzw
        double[] ForearmAngles { get; set; }

        double[] BycepAngles { get; set; }  
    }
}
