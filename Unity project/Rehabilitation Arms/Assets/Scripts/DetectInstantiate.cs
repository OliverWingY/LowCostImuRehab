using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectInstantiate : MonoBehaviour
{
    private Vector3 offset = new Vector3(0, -0.5f, 1);
    public GameObject ProjectilePrefab;
    //public SphereCollider col;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Banana"))
        {
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Spawn Rock"))
        {
            Instantiate(ProjectilePrefab, transform.position + offset, ProjectilePrefab.transform.rotation);
        }
    }
    }

