using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnManager : MonoBehaviour
{
    public GameObject[] incomingObjects;
    private float spawnRangeX = 4;
    private float spawnPosZ = 5;
    private float startDelay = 2;
    private float spawnInterval = 3;
    public GameObject playersObject;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnRandomObject", startDelay, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnRandomObject()
    {
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
