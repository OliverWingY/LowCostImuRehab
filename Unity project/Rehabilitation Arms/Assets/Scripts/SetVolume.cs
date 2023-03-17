using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour
{
    public AudioMixer audioMixer;
    public void SetVol (float volume)
    {
        audioMixer.SetFloat("noise", volume);
    }
}