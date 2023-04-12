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
    public KeyCode Calibrate;
    private int calibrateTimer;
    // Start is called before the first frame update
    void Start()
    {
        print("starting up");
        Forearm = new Vector3();
        Bycep = new Vector3();
        ArmPosition = new UnityMonitoredVariables();
        Middleware = new ImuDataConnector(12345, 12347, ArmPosition);
        if (Middleware != null) 
        { 
            print("successfully started up middleware");
        }
        else print("failed to start middleware");
    }

    // Update is called once per frame
    void Update()
    {
        print($"Middleware Status: {ArmPosition.Status}");
        if (calibrateTimer > 0) calibrateTimer--;
        Bycep = ToEulerAngles(ArmPosition.BycepAngles);
        Forearm = ToEulerAngles(ArmPosition.ForearmAngles);
        if (Input.GetKey(Clockwise)) yRotation += 0.1f;
        if (Input.GetKey(AntiClockwise)) yRotation -= 0.1f;
        if (Input.GetKey(Calibrate) && calibrateTimer == 0)
        {
            Callibrate();
            calibrateTimer = 200;
        };
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

    private void Callibrate()
    {
        print("callibrating");
        var palm = GameObject.Find("Palm.R").GetComponent<Transform>();  
        var bicep = GameObject.Find("Bicep.R").GetComponent<Transform>();
        var vector = palm.position - bicep.position;
        var x = vector.x;
        print($"bicep: {bicep.position.x},{bicep.position.z}");
        print($"palm: {palm.position.x},{palm.position.z}");
        print($"vector: {vector.x}, {vector.z}");
        var z = vector.z;
        if (x >= 0 && z >= 0) yRotation = (float)(-Math.Atan(x / z) * 180 / Math.PI);
        else if (x >= 0 && z < 0) yRotation = (float)( - 90 - (Math.Atan(Math.Abs(z / x)) * 180 / Math.PI));
        else if (x < 0 && z < 0) yRotation = (float)(90 + (Math.Atan(Math.Abs(z / x)) * 180 / Math.PI));
        else if (x < 0 && z >= 0) yRotation = (float)(- Math.Atan(x / z) * 180 / Math.PI);
        print($"new yrotation: {yRotation}");
    }
    private static Vector3 ToEulerAngles(double[] quaternion)
    {
        var q = new Quaternion((float)quaternion[0], (float)quaternion[1], (float)quaternion[2], (float)quaternion[3]);
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
