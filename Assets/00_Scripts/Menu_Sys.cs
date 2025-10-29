using UnityEngine;

public class Menu_Sys : MonoBehaviour
{
    private bool isGamePaused = false;

    public MonoBehaviour firstPersonControllerScript;

    // --- 주석 해제 ---
    public GameObject pauseMenuUI;
    // --- ---

    void Start()
    {
        if (firstPersonControllerScript == null)
        {
            // 이 스크립트가 Player에 붙어있다는 가정
            firstPersonControllerScript = GetComponent<FirstPersonController>();
        }

        // 게임 시작 시 UI가 혹시 켜져있다면 확실히 꺼줍니다.
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);
        }

        ResumeGame(); // 게임 시작 상태로 초기화
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0f;
        isGamePaused = true;

        if (firstPersonControllerScript != null)
        {
            firstPersonControllerScript.enabled = false;
        }

        // --- 주석 해제 및 UI 활성화 ---
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(true);
        }
        // --- ---

        // --- 마우스 커서 보이기 및 잠금 해제 ---
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        // --- ---
    }

    void ResumeGame()
    {
        Time.timeScale = 1f;
        isGamePaused = false;

        if (firstPersonControllerScript != null)
        {
            firstPersonControllerScript.enabled = true;
        }

        // --- 주석 해제 및 UI 비활성화 ---
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);
        }
        // --- ---

        // --- 마우스 커서 숨기기 및 잠금 ---
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        // --- ---
    }
}