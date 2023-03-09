using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BycepMotion : MonoBehaviour
{
    // Start is called before the first frame update
    internal Startup startup;
    private float toDegrees = (float)(180 / 3.14159);
    public float xOffset = 270;
    public float yOffset = 0;
    public float zOffset = 0;

    void Start()
    {
        startup = GameObject.Find("Ground").GetComponent<Startup>();
        if (startup.Middleware != null) { print("bycep has found middleware"); }
    }

    // Update is called once per frame
    void Update()
    {        
        var transform = GetComponent<Transform>();
        transform.eulerAngles = new Vector3((float)startup.Bycep[1] * toDegrees, -(float)startup.Bycep[2] * toDegrees, -(float)startup.Bycep[0] * toDegrees);
        transform.Rotate(new Vector3(xOffset, yOffset, zOffset));
        var eul = GetComponent<Transform>().eulerAngles;
        print($"{this.gameObject.name} Euler angles:  {eul.x}, {eul.y}, {eul.z}");
    }
}
