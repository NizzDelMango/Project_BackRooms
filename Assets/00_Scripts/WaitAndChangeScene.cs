using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; // 코루틴을 사용하기 위해 꼭 필요합니다!

public class WaitAndChangeScene : MonoBehaviour
{
    // 1. 인스펙터에서 이동할 다음 씬의 이름을 설정합니다.
    public string sceneToLoad; // 예: "Level_01"

    // 2. [중요] 인스펙터에서 이 비디오의 *정확한* 재생 시간(초)을 직접 입력합니다.
    // 예: 15.5초라면 15.5 라고 입력
    public float videoDuration;

    void Start()
    {
        // 비디오가 0초이거나 설정되지 않았다면 경고를 줍니다.
        if (videoDuration <= 0)
        {
            Debug.LogError("WaitAndChangeScene: Video Duration(비디오 재생 시간)을 인스펙터에 입력해야 합니다!");
            return; // 0초면 코루틴을 실행하지 않습니다.
        }

        // 3. 씬이 시작되자마자 "기다리기" 코루틴을 실행합니다.
        StartCoroutine(WaitForVideoEnd());
    }

    // "기다리기" 전용 함수
    IEnumerator WaitForVideoEnd()
    {
        // (디버깅) 코루틴이 시작되었는지 콘솔에서 확인
        Debug.Log("비디오 재생 시작. " + videoDuration + "초 후에 씬을 전환합니다.");

        // 4. 인스펙터에서 설정한 videoDuration 값(초)만큼 정확히 기다립니다.
        yield return new WaitForSeconds(videoDuration);

        // (디버깅) 기다림이 끝났는지 콘솔에서 확인
        Debug.Log("비디오 시간 종료. " + sceneToLoad + " 씬을 불러옵니다...");

        // 5. 시간이 다 되면 다음 씬을 불러옵니다.
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}