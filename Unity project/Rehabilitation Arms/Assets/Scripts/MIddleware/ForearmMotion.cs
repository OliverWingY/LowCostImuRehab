using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForearmMotion : MonoBehaviour
{
    // Start is called before the first frame update
    internal Startup myStartup;
    internal BycepMotion bycep;
    private float toDegrees = (float)(180 / 3.14159);
    void Start()
    {
        myStartup = GameObject.Find("Ground").GetComponent<Startup>();
        bycep = GameObject.Find("Bycep.R").GetComponent<BycepMotion>();
    }

    // Update is called once per frame
    void Update()
    {
        //GetComponent<Transform>().rotation = new Quaternion((float)myStartup.Forearm[0], (float)myStartup.Forearm[1], (float)myStartup.Forearm[2], (float)myStartup.Forearm[3]);
        GetComponent<Transform>().eulerAngles = new Vector3((float)myStartup.Forearm[0] * toDegrees, (float)myStartup.Forearm[1] * toDegrees, (float)myStartup.Forearm[2] * toDegrees);
    }
}
