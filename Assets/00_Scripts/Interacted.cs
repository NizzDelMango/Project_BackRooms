using UnityEngine;

// (참고) IInteractable 인터페이스가 이런 식으로 어딘가에 정의되어 있어야 합니다.
// public interface IInteractable
// {
//     void Interact();
// }

// 이 스크립트를 'Paper' 오브젝트에 붙여주세요.
public class PaperCollectible : MonoBehaviour, IInteractable
{
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

        // 2. 이 종이 오브젝트를 파괴
        Destroy(gameObject);
    }
}