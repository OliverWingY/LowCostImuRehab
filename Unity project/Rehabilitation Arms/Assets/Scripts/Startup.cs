using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Middleware;

//attaching this to the ground and using it as a startup script because I'm a hack who cant be bothered to read the documentation
public class Startup : MonoBehaviour
{
    public ImuDataConnector Middleware;
    public UnityMonitoredVariables ArmPosition;
    public static double[] CurrentPosition;
    // Start is called before the first frame update
    void Start()
    {
        CurrentPosition= new double[6];
        ArmPosition = new UnityMonitoredVariables();
        Middleware = new ImuDataConnector(17628, ArmPosition);          
    }

    // Update is called once per frame
    void Update()
    {
        CurrentPosition = ArmPosition.Angles;
    }

    private void OnDisable()
    {
        Middleware.Close();
    }
}
