using UnityEngine;

public class LightManager : MonoBehaviour
{
    [Header("플레이어 머리 위 라이트")]
    public Light playerLight;

    [Header("제어할 발광 머티리얼")]
    public Material[] emissiveMaterials;

    private bool isBright;

    private Color[] originalEmissionColors;
    private bool[] originalEmissionStates;
    private bool originalPlayerLightState;

    void Start()
    {
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

        //어두운 상태로 게임 시작
        SetDarkMode();
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
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            ToggleLighting();
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
    }
}