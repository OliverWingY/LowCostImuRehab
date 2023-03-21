using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPowerup : MonoBehaviour
{
    public float delay = 5f; // The delay in seconds before the object is destroyed

    // Start is called before the first frame update
    void Start()
    {
        // Call the Destroy method after the specified delay
        Destroy(gameObject, delay);
    }
}
