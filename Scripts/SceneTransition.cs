using UnityEngine;
using UnityEngine.SceneManagement; // 씬 관리를 위해 꼭 필요합니다!

public class SceneTransition : MonoBehaviour
{
    // Inspector 창에서 이동할 다음 씬의 이름을 적어줍니다.
    public string sceneToLoad;

    // Is Trigger가 켜진 Collider에 다른 Collider가 들어왔을 때 
    // "한 번" 호출되는 함수입니다.
    private void OnTriggerEnter(Collider other)
    {
        // 1. (태그 확인 제거) - 무엇이 닿든 씬을 전환합니다.

        // 2. 씬 이름이 비어있지 않다면,
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            // 3. 지정된 씬을 불러옵니다.
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogWarning("다음 씬 이름(Scene To Load)이 지정되지 않았습니다!");
        }
    }
}