using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayRecordingBicep : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Transform>().eulerAngles = new Vector3((float)Startup.CurrentPosition[1], (float)Startup.CurrentPosition[2], (float)Startup.CurrentPosition[3]);
    }
}
