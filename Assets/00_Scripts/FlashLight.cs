using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class FlashLight : MonoBehaviour
{
    public GameObject NormalFlashLight;
    public GameObject LongRangeFlashLight;

    public AudioClip soundOn;
    public AudioClip soundOff;

    private AudioSource audioSource; 
    // --- ---

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
           
            if (NormalFlashLight.activeSelf || LongRangeFlashLight.activeSelf)
            {
                if (soundOff != null)
                {
                    audioSource.PlayOneShot(soundOff);
                }
                NormalFlashLight.SetActive(false);
                LongRangeFlashLight.SetActive(false);
            }
            else
            {
                if (soundOn != null)
                {
                    audioSource.PlayOneShot(soundOn);
                }
                NormalFlashLight.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            if (NormalFlashLight.activeSelf || LongRangeFlashLight.activeSelf)
            {
                // --- T키 사운드 재생 (모드 전환 시) ---
                if (soundOn != null)
                {
                    audioSource.PlayOneShot(soundOn);
                }
                if (LongRangeFlashLight != null)
                {
                    if (NormalFlashLight.activeSelf)
                    {
                        NormalFlashLight.SetActive(false);
                        LongRangeFlashLight.SetActive(true);
                    }
                    else if (LongRangeFlashLight.activeSelf)
                    {
                        LongRangeFlashLight.SetActive(false);
                        NormalFlashLight.SetActive(true);
                    }
                }
            }
        }
    }
}