using UnityEngine;
using UnityEngine.SceneManagement; // 씬 관리를 위해 꼭 필요합니다!

public class StartMenu : MonoBehaviour
{
    // 다음 씬(메인 게임 씬)의 이름을 Inspector 창에서 설정할 수 있도록 public으로 만듭니다.
    public string mainGameSceneName = "MainGame"; // "MainGame" 부분에 실제 게임 씬 이름을 적어주세요.

    
    // 이 스크립트가 켜질 때 (즉, 씬이 로드될 때) 자동으로 한 번 실행됩니다.
    void Start()
    {
        // 1. 마우스 커서가 보이도록 설정
        Cursor.visible = true;

        // 2. 마우스 커서 잠금(Lock)을 해제
        Cursor.lockState = CursorLockMode.None;

        // 3. (혹시나 게임이 멈춰있을 수 있으니) 게임 시간을 1배속으로
        Time.timeScale = 1f;

        //if (GameManager.instance != null)
        //{
        //    // 3. GameManager의 종이 개수를 0으로 리셋시킵니다.
        //    GameManager.instance.ResetPaperCount();
        //}
    }


    // 'Start' 버튼이 클릭되면 호출될 함수
    public void StartGame()
    {
        // Debug.Log("게임 시작!");

        // 1. 이름으로 씬 불러오기 (권장)
        // Inspector 창에서 설정한 mainGameSceneName 변수를 사용합니다.
        SceneManager.LoadScene(mainGameSceneName);

        // 2. 빌드 인덱스로 씬 불러오기 (참고)
        // (빌드 세팅에서 StartScene이 0번, MainGameScene이 1번일 경우)
        // SceneManager.LoadScene(1); 

        // 3. 현재 씬의 다음 인덱스로 불러오기 (참고)
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // 'Exit' 버튼이 클릭되면 호출될 함수
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