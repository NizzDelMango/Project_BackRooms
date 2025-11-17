using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public TextMeshProUGUI paperCountText;

    // --- [수정] ---
    // Animator 대신 활성화시킬 대상(빈 오브젝트)을 저장할 변수입니다.
    private GameObject objectToActivate;
    // (Animator 관련 변수 삭제)
    // --- [끝] ---

    public int maxPaper = 5;
    private int currentPaper = 0;

    void Awake()
    {
        // (기존 Awake 코드는 그대로 둡니다)
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        // (기존 OnEnable 코드는 그대로 둡니다)
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        // (기존 OnDisable 코드는 그대로 둡니다)
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // (UI 찾는 로직은 그대로 둡니다)
        GameObject textObject = GameObject.FindGameObjectWithTag("PaperCountUI");
        if (textObject != null)
        {
            paperCountText = textObject.GetComponent<TextMeshProUGUI>();
            UpdatePaperUI();
        }
        else
        {
            Debug.LogWarning("씬에서 'PaperCountUI' 태그를 가진 텍스트를 찾지 못했습니다.");
            paperCountText = null;
        }

        // --- [수정] "ActivationTarget"이라는 *이름*의 오브젝트를 찾습니다 ---
        // (주의: "Door_Panel"이 아니라, 우리가 활성화시킬 대상의 이름입니다)
        objectToActivate = GameObject.Find("trigger");

        if (objectToActivate != null)
        {
            // 씬이 시작될 때 이 오브젝트를 항상 비활성화 상태로 만듭니다.
            objectToActivate.SetActive(false);
        }
        else
        {
            Debug.LogWarning("씬에서 'ActivationTarget' 이름을 가진 오브젝트를 찾지 못했습니다.");
        }
        // --- [끝] ---
    }

    public void CollectPaper()
    {
        // (기존 CollectPaper 코드는 그대로 둡니다)
        currentPaper++;
        UpdatePaperUI();

        if (currentPaper >= maxPaper)
        {
            Debug.Log("모든 종이 수집!");
            OpenDoor(); // (이름은 OpenDoor지만 실제론 오브젝트를 활성화시킵니다)
        }
    }

    void UpdatePaperUI()
    {
        // (기존 UpdatePaperUI 코드는 그대로 둡니다)
        if (paperCountText != null)
        {
            paperCountText.text = $"{currentPaper} / {maxPaper}";
        }
    }

    // --- [수정] 문 열기 대신, 대상 오브젝트를 활성화하는 함수 ---
    void OpenDoor()
    {
        // objectToActivate 변수가 null이 아닌지 (씬에서 찾았는지) 확인
        if (objectToActivate != null)
        {
            Debug.Log("종이를 다 모았습니다! 'ActivationTarget'을 활성화합니다.");

            // "빈 오브젝트"를 활성화(SetActive(true))시킵니다.
            objectToActivate.SetActive(true);
        }
        else
        {
            Debug.LogWarning("대상을 활성화하려 했으나, 씬에서 'ActivationTarget'을 찾지 못했습니다.");
        }
    }
    public void ResetPaperCount()
    {
        currentPaper = 0;
        Debug.Log("종이 개수가 0으로 초기화되었습니다.");

        // 씬이 로드될 때 OnSceneLoaded에서 UpdatePaperUI()를 다시 호출하므로
        // 여기서 UI를 업데이트할 필요는 없지만, 안전하게 호출해 둡니다.
        UpdatePaperUI();
    }
}