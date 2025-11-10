using UnityEngine;
using System.Collections;
using System.Collections.Generic;

interface IInteractable
{
    public void Interact();
}

public class Interactor : MonoBehaviour
{
    public Transform InteractorSource;
    public float InteractRange;

    [Header("UI 설정")]
    public GameObject interactionPromptUI; // 인스펙터에서 할당할 UI 패널

    void Start()
    {
        // 시작 시 UI를 비활성화 상태로 설정합니다.
        if (interactionPromptUI != null)
        {
            interactionPromptUI.SetActive(false);
        }
    }

    void Update()
    {

        bool isLookingAtPaper = CheckForPaper();

        // 1. UI 표시/숨김 로직
        if (interactionPromptUI != null)
        {
            // Paper를 바라보고 있다면 UI 활성화, 아니면 비활성화
            interactionPromptUI.SetActive(isLookingAtPaper);
        }

        // 2. 상호작용(E 키 입력) 로직
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray r = new Ray(InteractorSource.position, InteractorSource.forward);
            if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange))
            {
                if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable InteractObj))
                {
                    InteractObj.Interact();
                }
            }
        }
    }

    private bool CheckForPaper()
    {
        Ray r = new Ray(InteractorSource.position, InteractorSource.forward);

        // 레이캐스트를 쏘아 충돌 정보를 확인합니다.
        if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange))
        {
            // 충돌한 오브젝트의 태그가 "Paper"인지 확인합니다.
            if (hitInfo.collider.CompareTag("Paper"))
            {
                return true; // "Paper"를 바라보고 있음
            }
        }

        return false; // 그 외의 경우 (아무것도 안 닿거나, 다른 것에 닿았을 경우)
    }
}