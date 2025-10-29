using UnityEngine;
using UnityEngine.Audio;

public class FlashLight : MonoBehaviour
{
    public GameObject NormalFlashLight;
    public GameObject LongRangeFlashLight;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (NormalFlashLight.activeSelf || LongRangeFlashLight.activeSelf)
            {
                NormalFlashLight.SetActive(false);
                LongRangeFlashLight.SetActive(false);
            }
            else
            {
                NormalFlashLight.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            if (NormalFlashLight.activeSelf || LongRangeFlashLight.activeSelf)
            {
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