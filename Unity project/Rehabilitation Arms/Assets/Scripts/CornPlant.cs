using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornPlant : MonoBehaviour
{
    private List<CornControl> Corns = new List<CornControl>();
    private CornState state = CornState.Ready;

    [SerializeField]
    private int halfMovementSeconds = 8;

    [SerializeField]
    private int resetTime = 2;    
    private TimeSpan halfMovementTime;
    private DateTime delayTime = DateTime.MaxValue;

    private Startup startup;
    // Start is called before the first frame update
    void Start()
    {
        print("corn is alive");
        startup = GameObject.Find("Ground").GetComponent<Startup>();
        halfMovementTime = new TimeSpan(0, 0, halfMovementSeconds);
        Corns.Add(GameObject.Find("Corn1").GetComponent<CornControl>());
        Corns.Add(GameObject.Find("Corn2").GetComponent<CornControl>());
        Corns.Add(GameObject.Find("Corn3").GetComponent<CornControl>());
    }

    // Update is called once per frame
    void Update()
    {
        if (state == CornState.Harvesting  && DateTime.Now >= delayTime) 
        {
            Harvest();
        }
        else if(state == CornState.Harvested && DateTime.Now >= delayTime) 
        {
            Replant();
        }
    }

    private void Harvest()
    {        
        //if (startup.ClassifyMotion("not implemented yet"))
        if (true)
        {
            print("Harvesting");
            GetComponent<MeshRenderer>().enabled = false;
            foreach (CornControl corn in Corns) { corn.Activate(); }
            state = CornState.Harvested;
            delayTime= DateTime.Now + new TimeSpan(0,0, resetTime);
        }
        else 
        {
            print("Not Harvesting");
            state = CornState.Ready;
        }
    }
    private void Replant()
    {
        state = CornState.Ready;
        GetComponent<MeshRenderer>().enabled = true;
        foreach (CornControl corn in Corns) { corn.SetReady(); }
        delayTime = DateTime.MaxValue;
    }

    private void OnTriggerEnter(Collider other)
    {
        print("Trigger at plant!");
    }

    private void OnCollisionEnter(Collision collision)
    {
        print("Collision at plant!");
    }

    private void OnTriggerExit(Collider other)
    {
        print("Trigger exit!");
        if (state == CornState.Ready)
        {
            state = CornState.Harvesting;
            delayTime = DateTime.Now + halfMovementTime;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        print("Collision exit!");
        if (state == CornState.Ready)
        {
            state = CornState.Harvesting;
            delayTime = DateTime.Now + halfMovementTime;
        }
    }
}

public enum CornState
{
    Ready, Harvesting, Harvested 
}
