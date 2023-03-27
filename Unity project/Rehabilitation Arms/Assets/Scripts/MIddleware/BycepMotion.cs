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
        transform.eulerAngles = new Vector3((float)startup.Bycep.y * toDegrees, -(float)startup.Bycep.z * toDegrees, -(float)startup.Bycep.x * toDegrees);
        transform.Rotate(new Vector3(xOffset, yOffset, zOffset));
        transform.Rotate(0, startup.yRotation, 0, Space.World);

    }
}
