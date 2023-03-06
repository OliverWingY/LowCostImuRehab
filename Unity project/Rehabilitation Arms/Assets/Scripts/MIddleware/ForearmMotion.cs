using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForearmMotion : MonoBehaviour
{
    // Start is called before the first frame update
    internal Startup startup;
    internal BycepMotion bycep;
    private float toDegrees = (float)(180 / 3.14159);
    void Start()
    {
        startup = GameObject.Find("Ground").GetComponent<Startup>();        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Transform>().eulerAngles = new Vector3((float)startup.Forearm[0] * toDegrees, (float)startup.Forearm[1] * toDegrees, (float)startup.Forearm[2] * toDegrees);
        print($"{this.gameObject.name} imu Euler angles:  {(float)startup.Forearm[0] * toDegrees}, {(float)startup.Forearm[1] * toDegrees}, {(float)startup.Forearm[2] * toDegrees}");
        var eul = GetComponent<Transform>().eulerAngles;
        print($"{this.gameObject.name} Euler angles:  {eul.x}, {eul.y}, {eul.z}");
    }
}
