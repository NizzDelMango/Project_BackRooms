using UnityEngine;

public class AutoCollider : MonoBehaviour
{
    public GameObject targetObject;

    // 에디터 에서만 동작되게
    private void OnValidate()
    {
        if (targetObject != null)
        {
            foreach (Transform child in targetObject.GetComponentsInChildren<Transform>(true))
            {
                MeshFilter meshFilter = child.GetComponent<MeshFilter>();
                if (meshFilter != null)
                {
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