using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BycepMotion : MonoBehaviour
{
    // Start is called before the first frame update
    internal Startup startup;
    private float toDegrees = (float)(180 / 3.14159);
    void Start()
    {
        startup = GameObject.Find("Ground").GetComponent<Startup>();
        if (startup.Middleware != null) { print("bycep has found middleware"); }
    }

    // Update is called once per frame
    void Update()
    {
        //hi
        //GetComponent<Transform>().rotation = new Quaternion((float)startup.Bycep[0], (float)startup.Bycep[1], (float)startup.Bycep[2], (float)startup.Bycep[3]);
        GetComponent<Transform>().eulerAngles = new Vector3((float)startup.Bycep[0] * toDegrees, (float)startup.Bycep[1] * toDegrees, (float)startup.Bycep[2] * toDegrees);
    }
}
