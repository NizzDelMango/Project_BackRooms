using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MusicTrigger : MonoBehaviour
{
    public AudioSource MusicAudioSource;

    private bool hasPlayed = false;

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (MusicAudioSource != null && hasPlayed == false)
            {
                MusicAudioSource.Play();

                hasPlayed = true;
            }
        }
    }
}