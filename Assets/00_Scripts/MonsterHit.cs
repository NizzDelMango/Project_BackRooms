using UnityEngine;
using UnityEngine.SceneManagement; // 씬 관리를 위해 필수!

public class MonsterHit : MonoBehaviour
{
    // Inspector 창에서 스타트 씬의 이름을 설정할 수 있습니다.
    public string startSceneName = "Start"; //  여기에 실제 스타트 씬 이름을 정확히 입력하세요!

    // 이 스크립트가 붙은 오브젝트(몬스터)의 Trigger Collider에
    // 다른 Collider가 들어왔을 때 자동으로 호출되는 함수입니다.
    private void OnTriggerEnter(Collider other)
    {
        // 들어온 오브젝트의 태그가 "Player"인지 확인합니다.
        if (other.CompareTag("Player"))
        {
            // Debug.Log("플레이어와 충돌! 스타트 씬으로 이동합니다.");

            // 설정한 이름의 스타트 씬을 불러옵니다.
            SceneManager.LoadScene(startSceneName);
        }
    }
}