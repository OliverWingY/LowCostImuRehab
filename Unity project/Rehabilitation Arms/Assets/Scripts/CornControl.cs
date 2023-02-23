using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornControl : MonoBehaviour
{
    [SerializeField]
    private Vector3 DefaultPosition;
    private Vector3 Velocity = new Vector3();
    private Vector3 Gravity = new Vector3(0, (float)-0.1, 0);
    private bool Active = false;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Active)
        {
            GetComponent<Transform>().TransformVector(Velocity);
            Velocity = Velocity - Gravity;
        }
    }

    public void Activate()
    {
        GetComponent<MeshRenderer>().enabled = true;
        Active = true;
        Velocity = new Vector3(NextFloat((float)-0.1, (float)0.1), NextFloat((float)0.2, (float)0.4), NextFloat((float)-0.1, (float)0.1));
    }

    public void SetReady()
    {
        Active = false;
        GetComponent<Transform>().position= DefaultPosition;
        GetComponent<MeshRenderer>().enabled = false;
    }

    private static float NextFloat(float min, float max)
    {
        System.Random random = new System.Random();
        double val = (random.NextDouble() * (max - min) + min);
        return (float)val;
    }
}
