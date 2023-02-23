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
        print("starting up");
        CurrentPosition= new double[6];
        ArmPosition = new UnityMonitoredVariables();
        Middleware = new ImuDataConnector(17628, ArmPosition);   
        if (Middleware != null) { print("successfully started up middleware"); }
    }

    // Update is called once per frame
    void Update()
    {
        CurrentPosition = ArmPosition.Angles;
    }

    public bool ClassifyMotion(string expectedMotion)
    {
        return expectedMotion.Equals(Middleware.ClassifyMotion());
    }

    private void OnDisable()
    {
        print("Closing middleware");
        Middleware.Dispose();
        print("Closed middleware");
    }
}
