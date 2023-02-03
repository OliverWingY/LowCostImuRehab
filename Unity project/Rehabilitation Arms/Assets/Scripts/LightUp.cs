using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightUp : MonoBehaviour
{
    private Behaviour halo;
    // Start is called before the first frame update
    void Start()
    {
        halo = (Behaviour)GetComponent("Halo");
        halo.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        halo.enabled = true;
    }

    private void OnTriggerExit(Collider other)
    {
        halo.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
