using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Middleware;

//attaching this to forarm
public class Startup : MonoBehaviour
{
    public ImuDataConnector Middleware;
    public UnityMonitoredVariables ArmPosition;
    public double[] Forearm;
    public double[] Bycep;
    // Start is called before the first frame update
    void Start()
    {
        print("starting up");
        Forearm = new double[4];
        Bycep = new double[4];
        ArmPosition = new UnityMonitoredVariables();
        Middleware = new ImuDataConnector(12346, 12347, ref ArmPosition);
        if (Middleware != null) 
        { 
            print("successfully started up middleware");
            Middleware.NotifyStart();
        }
        else print("failed to start middleware");
    }

    // Update is called once per frame
    void Update()
    {
        Bycep = ArmPosition.BycepAngles;
        Forearm = ArmPosition.ForearmAngles;
    }

    public bool ClassifyMotion(string expectedMotion)
    {
        return expectedMotion.Equals(Middleware.ClassifyMotion());
    }

    private void OnDisable()
    {
        print("Closing middleware");
        Middleware.NotifyEnd();
        Middleware.Dispose();
        print("Closed middleware");
    }
}
