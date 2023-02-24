using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] incomingObjects;
    public GameObject[] enemyObject;
    private float spawnRangeX = 4;
    private float spawnPosZ = 5;
    private float startDelay = 2;
    private float spawnInterval = 1.5f;
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
        Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 1.5f, spawnPosZ);
        Vector3 spawnPosX = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0.7f, spawnPosZ);
        Instantiate(incomingObjects[objectIndex], spawnPos, incomingObjects[objectIndex].transform.rotation);
        Instantiate(enemyObject[0], spawnPosX, enemyObject[0].transform.rotation);
    }

}
