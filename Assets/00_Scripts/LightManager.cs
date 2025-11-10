using UnityEngine;
using System.Collections;

public class LightManager : MonoBehaviour
{
    [Header("플레이어 머리 위 라이트")]
    public Light playerLight;

    [Header("제어할 발광 머티리얼")]
    public Material[] emissiveMaterials;

    // 사운드 제어를 위한 변수 추가
    [Header("사운드 설정")]
    public AudioSource ambienceAudioSource;
    public AudioClip brightModeClip; // Backrooms_LV_0_Ambience.wav를 할당할 클립 변수

    private bool isBright;

    private Color[] originalEmissionColors;
    private bool[] originalEmissionStates;
    private bool originalPlayerLightState;

    void Start()
    {
        // 원본 상태 저장 로직
        if (playerLight != null)
        {
            originalPlayerLightState = playerLight.enabled;
        }

        if (emissiveMaterials != null)
        {
            int count = emissiveMaterials.Length;
            originalEmissionColors = new Color[count];
            originalEmissionStates = new bool[count];

            for (int i = 0; i < count; i++)
            {
                Material mat = emissiveMaterials[i];
                if (mat != null)
                {
                    originalEmissionColors[i] = mat.GetColor("_EmissionColor");
                    originalEmissionStates[i] = mat.IsKeywordEnabled("_EMISSION");
                }
            }
        }

        // 씬 전환 후의 타이밍 로직 시작
        StartCoroutine(LightSequence());
    }

    void OnDestroy()
    {
        if (playerLight != null)
        {
            playerLight.enabled = originalPlayerLightState;
        }

        if (emissiveMaterials != null)
        {
            for (int i = 0; i < emissiveMaterials.Length; i++)
            {
                Material mat = emissiveMaterials[i];
                if (mat != null)
                {
                    mat.SetColor("_EmissionColor", originalEmissionColors[i]);
                    if (originalEmissionStates[i])
                    {
                        mat.EnableKeyword("_EMISSION");
                    }
                    else
                    {
                        mat.DisableKeyword("_EMISSION");
                    }
                }
            }
        }

        // 씬 파괴 시 사운드 정지
        if (ambienceAudioSource != null && ambienceAudioSource.isPlaying)
        {
            ambienceAudioSource.Stop();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            ToggleLighting();
        }
    }

    IEnumerator LightSequence()
    {
        SetDarkMode();

        yield return new WaitForSeconds(4.0f);

        SetBrightMode();

        while (true)
        {
            yield return new WaitForSeconds(60.0f);

            SetDarkMode();

            yield return new WaitForSeconds(60.0f);

            SetBrightMode();
        }
    }

    public void ToggleLighting()
    {
        if (isBright)
        {
            SetDarkMode();
        }
        else
        {
            SetBrightMode();
        }
    }

    public void SetBrightMode()
    {
        if (playerLight != null)
        {
            playerLight.enabled = true;
        }

        if (emissiveMaterials != null)
        {
            foreach (Material mat in emissiveMaterials)
            {
                if (mat != null)
                {
                    mat.EnableKeyword("_EMISSION");
                    mat.SetColor("_EmissionColor", Color.white);
                }
            }
        }
        isBright = true;

        // 빛이 켜지면 사운드 재생
        if (ambienceAudioSource != null && brightModeClip != null)
        {
            ambienceAudioSource.clip = brightModeClip;
            ambienceAudioSource.loop = true; // 반복 재생 설정
            ambienceAudioSource.Play();
        }
    }

    public void SetDarkMode()
    {
        if (playerLight != null)
        {
            playerLight.enabled = false;
        }

        if (emissiveMaterials != null)
        {
            foreach (Material mat in emissiveMaterials)
            {
                if (mat != null)
                {
                    mat.DisableKeyword("_EMISSION");
                    mat.SetColor("_EmissionColor", Color.black);
                }
            }
        }
        isBright = false;

        // 빛이 꺼지면 사운드 정지
        if (ambienceAudioSource != null && ambienceAudioSource.isPlaying)
        {
            ambienceAudioSource.Stop();
        }
    }
}