
using UnityEngine;

namespace Middleware
{
    public interface IUnityVariables
    {
        Quaternion ForearmAngles { get; set; }

        Quaternion BycepAngles { get; set; }  
    }
}
