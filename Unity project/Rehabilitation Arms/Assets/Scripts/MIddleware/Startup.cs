using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Middleware;
using System;

//attaching this to forarm
public class Startup : MonoBehaviour
{
    public ImuDataConnector Middleware;
    public UnityMonitoredVariables ArmPosition;
    public Vector3 Forearm;
    public Vector3 Bycep;
    public float yRotation =0;
    public KeyCode Clockwise;
    public KeyCode AntiClockwise;
    // Start is called before the first frame update
    void Start()
    {
        print("starting up");
        Forearm = new Vector3();
        Bycep = new Vector3();
        ArmPosition = new UnityMonitoredVariables();
        Middleware = new ImuDataConnector(12345, 12347, ref ArmPosition);
        if (Middleware != null) 
        { 
            print("successfully started up middleware");
        }
        else print("failed to start middleware");
    }

    // Update is called once per frame
    void Update()
    {
        Bycep = ToEulerAngles(ArmPosition.BycepAngles);
        Forearm = ToEulerAngles(ArmPosition.ForearmAngles);
        if (Input.GetKey(Clockwise)) yRotation += 0.1f;
        if (Input.GetKey(AntiClockwise)) yRotation -= 0.1f;
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

    private static Vector3 ToEulerAngles(Quaternion q)
    {
        Vector3 angles = new Vector3();

        // roll / x
        double sinr_cosp = 2 * (q.w * q.x + q.y * q.z);
        double cosr_cosp = 1 - 2 * (q.x * q.x + q.y * q.y);
        angles.x = (float)Math.Atan2(sinr_cosp, cosr_cosp);

        // pitch / y
        double sinp = 2 * (q.w * q.y - q.z * q.x);
        if (Math.Abs(sinp) >= 1)
        {
            if (sinp >= 0)
                angles.y = (float)(Math.PI / 2);
            else
                angles.y = (float)(-Math.PI / 2);
        }
        else
        {
            angles.y = (float)Math.Asin(sinp);
        }

        // yaw / z
        double siny_cosp = 2 * (q.w * q.z + q.x * q.y);
        double cosy_cosp = 1 - 2 * (q.y * q.y + q.z * q.z);
        angles.z = (float)Math.Atan2(siny_cosp, cosy_cosp);

        return angles;
    }
}
