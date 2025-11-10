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
            // FirstPersonController 스크립트 이름을 사용하고 계신지 확인하세요.
            // 예: firstPersonControllerScript = GetComponent<FirstPersonController>();
            // 만약 다른 스크립트 이름을 사용 중이라면 해당 이름으로 변경해야 합니다.
            // 여기서는 임시로 주석 처리합니다.
            // firstPersonControllerScript = GetComponent<FirstPersonController>();
        }

        // 게임 시작 시 UI가 혹시 켜져있다면 확실히 꺼줍니다.
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);
        }

        ResumeGame(); // 게임 시작 상태로 초기화 (마우스 잠금 등)
    }

    void Update()
    {
        // [수정됨]
        // ESC 키를 누르고, *아직 일시정지 상태가 아닐 때*만
        // PauseGame()을 호출합니다.
        if (Input.GetKeyDown(KeyCode.Escape) && !isGamePaused)
        {
            PauseGame();
        }
        // ESC 키를 눌러도 ResumeGame()이 호출되지 않습니다.
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

    // [수정됨]
    // UI 버튼에서 이 함수를 호출할 수 있도록 'public'으로 변경했습니다.
    public void ResumeGame()
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

    public void QuitGame()
    {
        Debug.Log("게임 종료 버튼이 클릭되었습니다.");

        // Unity 에디터에서 플레이 모드를 중지시킵니다.
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        // 실제 빌드된 게임에서 프로그램을 종료시킵니다.
#else
        Application.Quit();
#endif
    }
}