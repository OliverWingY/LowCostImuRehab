using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Middleware;

//attaching this to the ground and using it as a startup script because I'm a hack who cant be bothered to read the documentation
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
        Middleware = new ImuDataConnector(12346, ref ArmPosition);
        if (Middleware != null) { print("successfully started up middleware"); }
        else print("failed to start middleware");
    }

    // Update is called once per frame
    void Update()
    {
        Bycep = ArmPosition.BycepAngles;
        Forearm = ArmPosition.ForearmAngles;
        if (Bycep != null && Forearm != null) print("Good arms");
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
