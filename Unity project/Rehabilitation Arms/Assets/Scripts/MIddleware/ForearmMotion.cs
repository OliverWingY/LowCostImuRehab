using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForearmMotion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Transform>().localEulerAngles = new Vector3((float)Startup.CurrentPosition[3], (float)Startup.CurrentPosition[4], (float)Startup.CurrentPosition[5]);
    }
}
