using UnityEngine;
using System.Collections;

public class LightManager : MonoBehaviour
{
    [Header("플레이어 머리 위 라이트")]
    public Light playerLight;

    [Header("제어할 발광 머티리얼")]
    public Material[] emissiveMaterials;

    [Header("사운드 설정")]
    public AudioSource ambienceAudioSource;
    public AudioClip brightModeClip;

    // --- 몬스터 설정 추가 ---
    [Header("몬스터 설정")]
    public GameObject bacteriaMonster; // Bacteria_Lifeform 할당
    public GameObject smilerMonster;   // Smiler 할당
    // --- ---

    private bool isBright;

    // (Start, OnDestroy, Update, LightSequence 함수는 
    //  이전과 동일하므로 공간을 위해 생략했습니다. 
    //  수정할 필요 없습니다.)

    // ... (Start, OnDestroy, Update, LightSequence 원본 코드)...
    // ... (아래 함수들만 수정 또는 확인하세요) ...

    // (이하 Start, OnDestroy, Update, LightSequence 원본 코드)
    #region 원본 코드 (수정 없음)
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
    #endregion


    // --- SetBrightMode 수정 ---
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

        if (ambienceAudioSource != null && brightModeClip != null)
        {
            ambienceAudioSource.clip = brightModeClip;
            ambienceAudioSource.loop = true;
            ambienceAudioSource.Play();
        }

        // --- 몬스터 활성화 로직 (밝을 때) ---
        if (bacteriaMonster != null)
        {
            bacteriaMonster.SetActive(true); // Bacteria 켜기
        }
        if (smilerMonster != null)
        {
            smilerMonster.SetActive(false); // Smiler 끄기
        }
        // --- ---
    }


    // --- SetDarkMode 수정 ---
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

        if (ambienceAudioSource != null && ambienceAudioSource.isPlaying)
        {
            ambienceAudioSource.Stop();
        }

        // --- 몬스터 활성화 로직 (어두울 때) ---
        if (bacteriaMonster != null)
        {
            bacteriaMonster.SetActive(false); // Bacteria 끄기
        }
        if (smilerMonster != null)
        {
            smilerMonster.SetActive(true); // Smiler 켜기
        }
        // --- ---
    }
}