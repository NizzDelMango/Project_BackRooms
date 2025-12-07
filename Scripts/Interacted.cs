using UnityEngine;

// 이 스크립트를 'Paper' 오브젝트에 붙여주세요.
// IInteractable 인터페이스가 구현되어 있어야 합니다.
public class PaperCollectible : MonoBehaviour, IInteractable
{
    // Inspector에서 Paper.wav 파일을 여기에 할당하세요.
    public AudioClip collectSound;

    // 플레이어의 상호작용 스크립트가 이 함수를 호출할 겁니다.
    public void Interact()
    {
        // 1. GameManager의 인스턴스를 찾아 CollectPaper() 함수를 호출
        if (GameManager.instance != null)
        {
            GameManager.instance.CollectPaper();
        }
        else
        {
            Debug.LogError("GameManager가 씬에 없습니다!");
        }

        // 2. 사운드 재생 (오브젝트 파괴 후에도 재생될 수 있도록 처리)
        if (collectSound != null)
        {
            // 이 정적 함수는 새로운 임시 AudioSource를 생성하고 사운드를 재생한 다음,
            // 사운드 재생이 끝나면 자동으로 해당 오브젝트를 파괴합니다.
            // World Position에서 사운드를 재생합니다. 
            // 2D 사운드처럼 들리게 하려면 Camera의 위치에서 재생할 수도 있습니다.
            AudioSource.PlayClipAtPoint(collectSound, transform.position);
        }

        // 3. 이 종이 오브젝트를 즉시 파괴
        // 사운드는 AudioSource.PlayClipAtPoint를 통해 생성된 임시 오브젝트가 처리합니다.
        Destroy(gameObject);
    }
}