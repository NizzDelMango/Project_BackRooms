using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [Header("인스펙터 연결")]
    public TextMeshProUGUI paperCountText; // 여기에 UI 텍스트 연결
    public GameObject doorObject;          // 여기에 문 오브젝트 연결

    void Awake()
    {
        instance = this; // 싱글톤 (이 씬의 관리자는 나다!)
    }

    void Start()
    {
        // 씬 시작하자마자 GameManager의 점수를 가져와서 UI 갱신
        UpdateUI();

        // 혹시 1스테이지에서 이미 다 모아서 왔을 수도 있으니 확인
        CheckDoor();
    }

    public void UpdateUI()
    {
        // GameManager가 살아있다면 그 점수를 표시
        if (GameManager.instance != null && paperCountText != null)
        {
            paperCountText.text = $"{GameManager.instance.currentPaper} / {GameManager.instance.maxPaper}";
        }
    }

    public void CheckDoor()
    {
        // GameManager의 점수가 목표치 이상이면 문을 없앰
        if (GameManager.instance != null && GameManager.instance.currentPaper >= GameManager.instance.maxPaper)
        {
            if (doorObject != null)
            {
                Debug.Log("조건 달성! 문을 제거합니다.");
                doorObject.SetActive(false);
            }
        }
    }
}