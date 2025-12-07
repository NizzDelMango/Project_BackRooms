using UnityEngine;
using TMPro; // UI 업데이트를 위해 필요 (선택 사항)

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int maxPaper = 5;
    public int currentPaper = 0; // public으로 바꿔서 다른 스크립트가 볼 수 있게 함

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 바뀌어도 파괴되지 않음!
        }
        else
        {
            Destroy(gameObject); // 중복 생성 방지
        }
    }

    public void CollectPaper()
    {
        currentPaper++;
        Debug.Log("종이 획득! 현재 개수: " + currentPaper);

        // 씬에 있는 LevelManager에게 "UI 업데이트 해!"라고 알림
        if (LevelManager.instance != null)
        {
            LevelManager.instance.UpdateUI();
            LevelManager.instance.CheckDoor(); // 문 열 조건이 됐는지 확인
        }
    }

    // 게임 오버 등으로 돌아갈 때 초기화용
    public void ResetPaperCount()
    {
        currentPaper = 0;
    }
}