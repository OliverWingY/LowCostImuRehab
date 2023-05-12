using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;


public class SpawnManager : MonoBehaviour
{
    public GameObject[] incomingObjects;
    private int frameCount = 50;
    private float spawnRangeX = 4;
    private float spawnPosZ = 5;
    private float startDelay = 2;
    private float spawnInterval = 1;
    public GameObject playersObject;
    public bool DemoMode = false;
    public KeyCode ModeSwitch;
    public KeyCode Banana;
    public KeyCode Crate;
    public KeyCode Powerup;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnRandomObject", startDelay, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(ModeSwitch)) { DemoMode = !DemoMode; }
        if (frameCount <= 0) 
        {
            if (Input.GetKey(Banana))
            { SpawnObject(0); }
            if (Input.GetKey(Crate))
            { SpawnObject(1); }
            if (Input.GetKey(Powerup))
            { SpawnObject(2); }
        }
        else frameCount--;
    }

    void SpawnObject(int objectIndex)
    {
        Vector3 spawnPos = new Vector3(playersObject.transform.position.x, 0f, spawnPosZ);
        float spawnHeight = objectIndex == 0 ? 1.5f : (objectIndex == 1 ? 0.7f : 2.0f);
        if (spawnHeight == 2.0f)
        {
            spawnPos = Camera.main.transform.position + new Vector3(-0.106f, -0.184f, 0.496f);
        }
        else
        {
            spawnPos.y = spawnHeight;
        }

        GameObject spawnedObject = Instantiate(incomingObjects[objectIndex], spawnPos, incomingObjects[objectIndex].transform.rotation);

        if (objectIndex == 2)
        {
            spawnedObject.transform.parent = playersObject.transform;
        }
        frameCount = 200;
    }

    void SpawnRandomObject()
    {
        if (DemoMode) return;
        int objectIndex = Random.Range(0, incomingObjects.Length);
        Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0f, spawnPosZ);

        // randomly choose between 3 different spawn heights
        float spawnHeight = objectIndex == 0 ? 1.5f : (objectIndex == 1 ? 0.7f : 2.0f);

        // if the third object is spawned, set its position at a set offset from the main camera
        if (spawnHeight == 2.0f)
        {
            spawnPos = Camera.main.transform.position + new Vector3(-0.106f, -0.184f, 0.496f);
        }
        else
        {
            spawnPos.y = spawnHeight;
        }

        GameObject spawnedObject = Instantiate(incomingObjects[objectIndex], spawnPos, incomingObjects[objectIndex].transform.rotation);

        if (objectIndex == 2)
        {
            spawnedObject.transform.parent = playersObject.transform;
        }
    }
}
