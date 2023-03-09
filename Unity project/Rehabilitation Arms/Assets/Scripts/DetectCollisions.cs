using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DetectCollisions : MonoBehaviour
{
    public ParticleSystem explosionParticle;
    public AudioClip collisionSound;
    private AudioSource rockAudio;

    // Start is called before the first frame update
    void Start()
    {
        rockAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }
     private void OnTriggerEnter(Collider other)
    {
       if (other.gameObject.CompareTag("Obstacle"))
      {
            Debug.Log("test");
            rockAudio.PlayOneShot(collisionSound, 1.0f);
     explosionParticle.Play();
     Destroy(gameObject);
     Destroy(other.gameObject);
     }
     }
    
}

        
    
    

