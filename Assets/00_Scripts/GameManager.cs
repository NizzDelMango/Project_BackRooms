using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // 1. 씬 관리를 위해 추가!

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public TextMeshProUGUI paperCountText; // 이 참조는 씬이 바뀔 때마다 다시 찾아야 함
    public int maxPaper = 5;
    private int currentPaper = 0; // 이 변수가 유지됩니다!

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            // 2. 씬이 바뀌어도 이 GameManager 오브젝트를 파괴하지 않음
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // 씬 1에서 씬 2로 넘어왔을 때, 씬 1의 GameManager가 이미 있으므로
            // (만약 씬 2에도 GameManager가 실수로 배치되어 있다면) 씬 2의 GameManager는 파괴함
            Destroy(gameObject);
        }
    }

    // --- (UI를 다시 찾기 위한 로직 추가) ---

    // 3. 스크립트가 활성화될 때 씬 로드 이벤트를 구독
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // 4. 스크립트가 비활성화될 때 구독 해제 (메모리 누수 방지)
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // 5. 씬이 로드될 때마다 이 함수가 호출됨
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 6. "PaperCountUI"라는 태그를 가진 게임 오브젝트를 찾음
        GameObject textObject = GameObject.FindGameObjectWithTag("PaperCountUI");
        if (textObject != null)
        {
            // 7. 찾은 오브젝트에서 TextMeshProUGUI 컴포넌트를 가져와 연결
            paperCountText = textObject.GetComponent<TextMeshProUGUI>();

            // 8. UI를 현재 점수로 즉시 업데이트 (예: "1/5")
            UpdatePaperUI();
        }
        else
        {
            Debug.LogWarning("씬에서 'PaperCountUI' 태그를 가진 텍스트를 찾지 못했습니다.");
            paperCountText = null; // 못 찾았으면 참조를 비움
        }
    }

    // --- (기존 함수들은 그대로 둠) ---

    public void CollectPaper()
    {
        currentPaper++;
        UpdatePaperUI();

        if (currentPaper >= maxPaper)
        {
            Debug.Log("모든 종이 수집!");
        }
    }

    void UpdatePaperUI()
    {
        if (paperCountText != null)
        {
            paperCountText.text = $"{currentPaper} / {maxPaper}";
        }
    }
}