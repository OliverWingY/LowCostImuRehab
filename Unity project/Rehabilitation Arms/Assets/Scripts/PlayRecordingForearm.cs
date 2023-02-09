using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayRecordingForearm : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Transform>().eulerAngles = new Vector3((float)Startup.CurrentPosition[4], (float)Startup.CurrentPosition[5], (float)Startup.CurrentPosition[6]);
    }
}
