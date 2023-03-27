using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForearmMotion : MonoBehaviour
{
    // Start is called before the first frame update
    internal Startup startup;
    internal BycepMotion bycep;
    private float toDegrees = (float)(180 / 3.14159);
    public float xOffset = 0;
    public float yOffset = 0;
    public float zOffset = 90;
    void Start()
    {
        startup = GameObject.Find("Ground").GetComponent<Startup>();        
    }

    // Update is called once per frame
    void Update()
    {
        var transform = GetComponent<Transform>();
        transform.eulerAngles = new Vector3((float)startup.Forearm.y * toDegrees, -(float)startup.Forearm.z * toDegrees, -(float)startup.Forearm.x * toDegrees);
        transform.Rotate(xOffset, yOffset, zOffset);
        transform.Rotate(0, startup.yRotation, 0, Space.World);
        print($"Euler angles: {startup.Forearm[0]*toDegrees}, {startup.Forearm[1] * toDegrees}, {startup.Forearm[2] * toDegrees}");
    }
}
