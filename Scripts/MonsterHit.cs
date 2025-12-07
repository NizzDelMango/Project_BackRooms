using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterHit : MonoBehaviour
{
    // 인스펙터에서 "Level_01" (또는 첫 스테이지 이름)을 적어주세요
    public string sceneToLoad = "Level_01";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("플레이어 사망! 점수를 초기화하고 레벨 1로 이동합니다.");

            // 1. [핵심] 씬 이동 전에 GameManager 점수 리셋!
            // (GameManager가 존재한다면 0으로 만듭니다)
            if (GameManager.instance != null)
            {
                GameManager.instance.ResetPaperCount();
            }

            // 2. 그 다음 레벨 1 씬을 불러옵니다.
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}