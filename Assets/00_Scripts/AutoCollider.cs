using UnityEngine;

public class AutoCollider : MonoBehaviour
{
    // 메시 콜라이더를 추가할 대상 오브젝트를 인스펙터 창에서 할당하세요.
    public GameObject targetObject;

    // 이 스크립트의 인스펙터 창이 변경될 때마다 호출됩니다.
    // 에디터에서만 작동하며, 게임 실행 중에는 호출되지 않습니다.
    private void OnValidate()
    {
        // targetObject가 null이 아닐 때만 실행합니다.
        if (targetObject != null)
        {
            // targetObject와 그 모든 자식 객체들을 순회합니다.
            // true는 비활성화된 자식 객체도 포함하도록 합니다.
            foreach (Transform child in targetObject.GetComponentsInChildren<Transform>(true))
            {
                // 현재 자식 객체에 MeshFilter 컴포넌트가 있는지 확인합니다.
                // MeshFilter가 있어야 메시 콜라이더를 추가할 수 있습니다.
                MeshFilter meshFilter = child.GetComponent<MeshFilter>();
                if (meshFilter != null)
                {
                    // 메시 콜라이더가 이미 있는지 확인합니다. 없으면 추가합니다.
                    if (child.GetComponent<MeshCollider>() == null)
                    {
                        child.gameObject.AddComponent<MeshCollider>();
                        Debug.Log("Added MeshCollider to: " + child.name);
                    }
                }
            }
        }
    }
}