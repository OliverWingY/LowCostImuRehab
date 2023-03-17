using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDestroyer : MonoBehaviour
{
    public AudioClip audioClip;
    public ParticleSystem particleSystemPrefab;
    

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other)
    {
     
        if (other.gameObject.CompareTag("Enemy"))
        {
            // Instantiate the particle system prefab
            if (particleSystemPrefab != null)
            {
                ParticleSystem particleSystemInstance = Instantiate(particleSystemPrefab, transform.position, Quaternion.identity);
                particleSystemInstance.Play();
                Destroy(particleSystemInstance.gameObject, particleSystemInstance.main.duration);
            }

            // Play the audio clip
            audioSource.PlayOneShot(audioClip);

            // Wait for the clip to finish playing
            StartCoroutine(WaitAndDestroy(audioClip.length, other.gameObject));
        }
    }

    private IEnumerator WaitAndDestroy(float waitTime, GameObject otherObject)
    {
        // Wait for the clip to finish playing
        yield return new WaitForSeconds(waitTime);

        // Destroy both game objects
        Destroy(gameObject);
        Destroy(otherObject);
    }
}