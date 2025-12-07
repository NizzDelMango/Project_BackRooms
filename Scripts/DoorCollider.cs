using UnityEngine;
using System.Collections;

public class DoorCollider : MonoBehaviour
{
    // 부모 객체의 애니메이터 컴포넌트
    private Animator DoorOpenAnimator;

    //  애니메이션이 이미 실행되었는지 확인하는 플래그 (딱 한 번만 실행되도록 제어)
    private bool hasTriggered = false;

    void Start()
    {
        
        DoorOpenAnimator = GetComponentInParent<Animator>();

    
        if (DoorOpenAnimator == null)
        {
            Debug.LogError("");
        }
    }

    // 3D Collider의 Is Trigger가 체크되어 있을 때, 다른 Collider와 접촉하면 호출됩니다.
    private void OnTriggerEnter(Collider other)
    {
        // 1. 이미 애니메이션을 실행했으면 동작하지 않습니다.
        if (hasTriggered)
        {
            return;
        }

        // 2. 닿은 객체가 플레이어인지 확인합니다.
        // 플레이어 오브젝트에 "Player" 태그가 붙어있다고 가정합니다.
        if (other.CompareTag("Player"))
        {
            // 3. Animator가 존재하고, 트리거를 실행할 수 있는 상태인지 확인합니다.
            if (DoorOpenAnimator != null)
            {
                // "Open" 트리거를 작동시킵니다.
                DoorOpenAnimator.SetTrigger("Open");

                // 실행 후 플래그를 true로 설정하여 다시는 동작하지 않게 합니다.
                hasTriggered = true;

                Debug.Log("DoorOpenAnimator 'Open' 트리거 작동!");
            }
        }
    }


    // Update는 이 기능에 필요 없으므로 주석 처리하거나 비워둡니다.
    // void Update()
    // {
    // 
    // }
}